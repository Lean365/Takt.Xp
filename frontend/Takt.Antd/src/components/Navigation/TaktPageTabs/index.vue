<template>
  <div class="page-tabs-wrapper">
    <!-- 面包屑导航 - 当启用面包屑时显示 -->
    <div v-if="settingsStore.isShowBreadcrumb" class="breadcrumb-section">
      <a-breadcrumb>
        <a-breadcrumb-item v-for="item in breadcrumbItems" :key="item.path">
          <router-link :to="item.path">
            <component :is="item.icon" v-if="item.icon" :key="item.icon" />
            <span>{{ item.title }}</span>
          </router-link>
        </a-breadcrumb-item>
      </a-breadcrumb>
    </div>

    <!-- 标签页 - 当有标签页时显示 -->
    <div v-if="panes.length > 0" class="tabs-section">
      <a-tabs
        v-model:activeKey="activeKey"
        type="editable-card"
        :hide-add="true"
        :animated="false"
        class="page-tabs"
        @edit="onEdit"
        @change="onChange"
      >
        <a-tab-pane
          v-for="pane in panes"
          :key="pane.key"
          :closable="pane.closable"
        >
          <template #tab>
            <span class="tab-item">
              <component :is="pane.icon" v-if="pane.icon && settingsStore.isShowTabIcon" />
              <span>{{ pane.title }}</span>
            </span>
          </template>
        </a-tab-pane>
      </a-tabs>
      
      <a-dropdown :trigger="['click']" class="tabs-actions" v-if="panes.length > 1">
        <a-button type="text">
          <template #icon><down-outlined /></template>
        </a-button>
        <template #overlay>
          <a-menu @click="handleTabAction">
            <a-menu-item key="closeOthers">
              {{ t('tabs.closeOthers') }}
            </a-menu-item>
            <a-menu-item key="closeRight">
              {{ t('tabs.closeRight') }}
            </a-menu-item>
            <a-menu-item key="closeAll">
              {{ t('tabs.closeAll') }}
            </a-menu-item>
          </a-menu>
        </template>
      </a-dropdown>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted, onUnmounted, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { useSettingsStore } from '@/stores/settingsStore'
import { message } from 'ant-design-vue'
import * as Icons from '@ant-design/icons-vue'
import {
  HomeOutlined,
  SettingOutlined,
  DownOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const settingsStore = useSettingsStore()

// 最大标签页数量限制
const MAX_TABS_COUNT = 5

interface TabPane {
  key: string
  title: string
  icon?: any
  closable: boolean
  path: string
}

const activeKey = ref(route.path)

const panes = ref<TabPane[]>([])

// 计算属性：获取首页标签页
const homeTab = computed(() => {
  // 从路由配置中获取首页信息，优先使用transKey
  const homeRoute = router.getRoutes().find(route => route.path === '/home')
  let title = t('menu.home') // 默认翻译
  
  if (homeRoute?.meta) {
    const titleKey = homeRoute.meta.transKey || homeRoute.meta.title
    title = titleKey ? t(titleKey as string) : t('menu.home')
  }
  
  return {
    title,
    key: '/',
    icon: HomeOutlined,
    closable: false, // 首页不能关闭
    path: '/'
  }
})

// 初始化标签页
const initPanes = () => {
  panes.value = [homeTab.value]
}

// 面包屑项计算属性
const breadcrumbItems = computed(() => {
  const matched = route.matched.filter(item => item.meta && (item.meta.title || item.meta.transKey))
  return matched.map(item => {
    // 优先使用 transKey，如果没有则使用 title
    const titleKey = item.meta?.transKey || item.meta?.title
    const title = titleKey ? t(titleKey as string) : item.path
    
    return {
      path: item.path,
      title,
      icon: item.meta?.icon ? Icons[item.meta.icon as keyof typeof Icons] : undefined
    }
  })
})

// 从localStorage加载标签页状态
const loadTabsFromStorage = () => {
  // 如果标签持久化功能未启用，则不加载保存的标签页
  if (!settingsStore.isKeepTabs) {
    console.log('[TaktPageTabs] 标签持久化功能未启用，使用默认标签页')
    return
  }
  
  try {
    const savedTabs = localStorage.getItem('page-tabs')
    if (savedTabs) {
      const parsedTabs = JSON.parse(savedTabs)
      
      if (Array.isArray(parsedTabs) && parsedTabs.length > 0) {
        // 确保首页在第一个位置，不能关闭
        const currentHomeTab = homeTab.value
        
        // 过滤掉首页标签页，只保留其他页面
        const otherTabs = parsedTabs.filter(tab => tab.key !== '/' && tab.key !== '/home')
        
        // 处理其他标签页：将字符串形式的图标名称转换为图标组件
        // 同时重新获取标题，确保翻译正确
        const processedOtherTabs = otherTabs.map(tab => {
          // 重新获取页面信息，确保标题翻译正确
          const pageInfo = getPageInfoFromRoute(tab.path)
          
          return {
            ...tab,
            title: pageInfo.title,
            icon: typeof tab.icon === 'string' ? getIconComponent(tab.icon) : tab.icon,
            closable: true // 其他标签都可以关闭
          }
        })
        
        // 首页 + 其他标签页
        const allTabs = [currentHomeTab, ...processedOtherTabs]
        
        // 检查标签页数量限制，最多MAX_TABS_COUNT个标签页
        if (allTabs.length > MAX_TABS_COUNT) {
          // 如果超过限制，只保留首页和前(MAX_TABS_COUNT-1)个其他标签页
          const limitedTabs = [currentHomeTab, ...processedOtherTabs.slice(0, MAX_TABS_COUNT - 1)]
          panes.value = limitedTabs
          console.log(`[TaktPageTabs] 标签页数量超过限制(${MAX_TABS_COUNT})，已自动截取前${MAX_TABS_COUNT}个标签页`)
          // 提示用户有标签页被截取
          message.info(`检测到${allTabs.length}个标签页，已自动保留前${MAX_TABS_COUNT}个标签页`)
        } else {
          panes.value = allTabs
        }
        
        console.log('[TaktPageTabs] 从本地存储加载标签页成功:', panes.value)
      }
    }
  } catch (error) {
    console.error('[TaktPageTabs] 加载标签页失败:', error)
  }
}

// 保存标签页状态到localStorage
const saveTabsToStorage = () => {
  // 如果标签持久化功能未启用，则不保存标签页
  if (!settingsStore.isKeepTabs) {
    console.log('[TaktPageTabs] 标签持久化功能未启用，不保存标签页')
    return
  }
  
  try {
    // 处理图标：将图标组件转换为字符串形式以便存储
    const tabsToSave = panes.value.map(tab => ({
      ...tab,
      icon: typeof tab.icon === 'string' ? tab.icon : tab.icon?.name || 'HomeOutlined'
    }))
    
    localStorage.setItem('page-tabs', JSON.stringify(tabsToSave))
    console.log('[TaktPageTabs] 标签页保存成功:', tabsToSave)
  } catch (error) {
    console.error('[TaktPageTabs] 保存标签页失败:', error)
  }
}

// 从路由配置中获取页面信息
const getPageInfoFromRoute = (path: string): { title: string; icon?: any } => {
  
  // 特殊处理首页路径
  if (path === '/' || path === '/home') {
    // 从路由配置中获取首页信息，优先使用transKey
    const homeRoute = router.getRoutes().find(route => route.path === '/home')
    if (homeRoute?.meta) {
      const titleKey = homeRoute.meta.transKey || homeRoute.meta.title
      const title = titleKey ? t(titleKey as string) : t('menu.home')
      return {
        title,
        icon: HomeOutlined
      }
    }
    // 如果找不到路由配置，使用默认翻译
    return {
      title: t('menu.home'),
      icon: HomeOutlined
    }
  }
  
  // 如果路径是当前路由，直接使用 route.matched
  if (path === route.path) {
    // 获取当前路由的最后一个匹配项（通常是页面本身）
    const matchedRoute = route.matched[route.matched.length - 1]
    
    if (matchedRoute?.meta) {
      // 优先使用 transKey，如果没有则使用 title
      const titleKey = matchedRoute.meta.transKey || matchedRoute.meta.title
      const title = titleKey ? t(titleKey as string) : path
      
      // 从路由meta中获取图标，使用与 TaktBreadcrumb 相同的方式
      const icon = matchedRoute.meta?.icon ? (Icons as any)[matchedRoute.meta.icon] : undefined
      
      return { title, icon }
    }
  }
  
  // 从所有路由中查找（包括固定菜单和动态菜单）
  const matchedRoute = router.getRoutes().find(route => route.path === path)
  
  if (matchedRoute?.meta) {
    // 优先使用 transKey，如果没有则使用 title
    const titleKey = matchedRoute.meta.transKey || matchedRoute.meta.title
    const title = titleKey ? t(titleKey as string) : path
    
    // 从路由meta中获取图标，使用与 TaktBreadcrumb 相同的方式
    const icon = matchedRoute.meta?.icon ? (Icons as any)[matchedRoute.meta.icon] : undefined
    
    return { title, icon }
  }
  
  // 默认返回路径作为标题
  return { title: path, icon: SettingOutlined }
}

// 根据图标名称获取图标组件
const getIconComponent = (iconName?: string) => {
  if (!iconName) return SettingOutlined
  
  // 使用与 TaktBreadcrumb 相同的方式获取图标
  return (Icons as any)[iconName] || SettingOutlined
}

onMounted(() => {
  // 如果没有启用标签持久化，先初始化默认标签页
  if (!settingsStore.isKeepTabs) {
    initPanes()
  }
  loadTabsFromStorage()
})

// 监听keepTabs配置变化
watch(
  () => settingsStore.isKeepTabs,
  (newKeepTabs) => {
    console.log('[TaktPageTabs] keepTabs配置变化:', newKeepTabs)
    
    if (newKeepTabs) {
      // 启用标签持久化时，重新加载保存的标签页
      loadTabsFromStorage()
    } else {
      // 禁用标签持久化时，清除本地存储并重置为默认标签页
      try {
        localStorage.removeItem('page-tabs')
        console.log('[TaktPageTabs] 已清除标签页本地存储')
      } catch (error) {
        console.error('[TaktPageTabs] 清除标签页本地存储失败:', error)
      }
      
      // 重置为默认标签页（只保留首页）
      initPanes()
    }
  }
)

// 监听showTabIcon配置变化
watch(
  () => settingsStore.isShowTabIcon,
  (newShowTabIcon) => {
    console.log('[TaktPageTabs] showTabIcon配置变化:', newShowTabIcon)
  }
)

// 监听i18n实例的语言变化
const { locale } = useI18n()
watch(
  () => locale.value,
  (newLocale) => {
    console.log('[TaktPageTabs] 语言变化，更新标签页标题:', newLocale)
    
    // 延迟一点时间，确保翻译已经更新
    setTimeout(() => {
      // 更新所有标签页的标题
      panes.value = panes.value.map(pane => {
        // 重新获取页面信息，确保翻译正确
        const pageInfo = getPageInfoFromRoute(pane.path)
        return {
          ...pane,
          title: pageInfo.title
        }
      })
      
      // 如果启用了标签持久化，保存更新后的标签页
      if (settingsStore.isKeepTabs) {
        saveTabsToStorage()
      }
    }, 100)
  }
)

// 监听路由变化
watch(
  () => route.path,
  (newPath) => {
    activeKey.value = newPath
    
    // 首页路径不重复添加（包括 / 和 /home）
    if (newPath === '/' || newPath === '/home') {
      return
    }
    
    // 如果标签不存在，添加新标签（除了首页）
    if (!panes.value.find(pane => pane.key === newPath)) {
      // 检查标签页数量限制，最多MAX_TABS_COUNT个标签页（包括首页）
      if (panes.value.length >= MAX_TABS_COUNT) {
        // 如果已达到最大数量，提示用户并阻止添加新标签页
        console.log(`[TaktPageTabs] 标签页数量已达上限(${MAX_TABS_COUNT})，阻止添加新标签页`)
        message.warning(`标签页数量已达上限(${MAX_TABS_COUNT})，请先关闭一些标签页再打开新页面。您可以使用标签页右侧的下拉菜单快速关闭多个标签页。`)
        return // 阻止添加新标签页
      }
      
      const pageInfo = getPageInfoFromRoute(newPath)
      
      panes.value.push({
        title: pageInfo.title,
        key: newPath,
        icon: pageInfo.icon,
        closable: true, // 非首页标签都可以关闭
        path: newPath
      })
      
      // 只有在启用标签持久化时才保存
      if (settingsStore.isKeepTabs) {
        saveTabsToStorage()
      }
    }
  },
  { immediate: true }
)

// 标签切换
const onChange = (key: string | number) => {
  router.push(String(key))
}

// 关闭标签
const onEdit = (targetKey: string | number | MouseEvent | KeyboardEvent, action: 'add' | 'remove') => {
  if (action === 'remove' && (typeof targetKey === 'string' || typeof targetKey === 'number')) {
    closeTab(String(targetKey))
  }
}

// 关闭标签页
const closeTab = (targetKey: string) => {
  
  // 检查是否是首页路径，首页不能关闭
  if (targetKey === '/' || targetKey === '/home') {
    return
  }
  
  // 找到要关闭的标签页索引
  const targetIndex = panes.value.findIndex(pane => pane.key === targetKey)
  if (targetIndex === -1) {
    return
  }
  
  // 移除标签页
  panes.value.splice(targetIndex, 1)
  
  // 如果关闭的是当前激活的标签页，需要跳转到其他标签页
  if (activeKey.value === targetKey) {
    // 优先跳转到右侧标签页，如果没有则跳转到左侧标签页
    const nextTab = panes.value[targetIndex] || panes.value[targetIndex - 1]
    if (nextTab) {
      router.push(nextTab.key)
    } else {
      // 如果没有其他标签页，跳转到首页
      router.push('/')
    }
  }
  
  // 只有在启用标签持久化时才保存
  if (settingsStore.isKeepTabs) {
    saveTabsToStorage()
  }
}

// 标签页操作
const handleTabAction = (info: { key: string | number }) => {
  const action = String(info.key)
  
  switch (action) {
    case 'closeOthers':
      // 关闭其他标签页，保留当前标签页和首页
      panes.value = panes.value.filter(
        pane => pane.key === '/' || pane.key === activeKey.value
      )
      break
    case 'closeRight':
      // 关闭右侧标签页，但保留首页
      const currentIndex = panes.value.findIndex(pane => pane.key === activeKey.value)
      if (currentIndex !== -1) {
        panes.value = panes.value.filter(
          (pane, index) => pane.key === '/' || index <= currentIndex
        )
      }
      break
    case 'closeAll':
      // 关闭所有标签页，但保留首页
      panes.value = panes.value.filter(pane => pane.key === '/' || pane.key === '/home')
      if (activeKey.value !== '/' && activeKey.value !== '/home') {
        router.push('/')
      }
      break
  }
  
  // 只有在启用标签持久化时才保存
  if (settingsStore.isKeepTabs) {
    saveTabsToStorage()
  }
}
</script>

<style lang="less" scoped>
.page-tabs-wrapper {
  background: var(--ant-component-background);
  border-top: 1px solid var(--ant-border-color-split);
  
  .breadcrumb-section {
    padding: 8px 16px;
    background: var(--ant-color-bg-container);
    border-bottom: 1px solid var(--ant-border-color-split);

    :deep(.ant-breadcrumb) {
      color: var(--ant-color-text);
    }

    :deep(.ant-breadcrumb-link) {
      color: var(--ant-color-text);
    }

    :deep(.ant-breadcrumb-separator) {
      color: var(--ant-color-text-secondary);
    }
  }
  
  .tabs-section {
    position: relative;
    height: 40px;
    
    .page-tabs {
      height: 40px;
      padding-right: 40px;
      
      :deep(.ant-tabs-nav) {
        margin: 0;
        
        &::before {
          display: none;
        }
        
        .ant-tabs-tab {
          margin: 0 !important;
          padding: 8px 20px 8px 16px;
          background: transparent;
          border: none;
          transition: all 0.3s;
          position: relative;
          
          &:hover {
            color: var(--ant-primary-color);
          }
          
          &.ant-tabs-tab-active {
            .ant-tabs-tab-btn {
              color: var(--ant-primary-color);
            }
          }
          
          .ant-tabs-tab-remove {
            margin: 0;
            padding: 0;
            position: absolute;
            top: 2px;
            right: 2px;
            width: 12px;
            height: 12px;
            font-size: 10px;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
            background: transparent;
            transition: all 0.2s ease;
            
            &:hover {
              color: #ff4d4f !important;
              background: rgba(255, 77, 79, 0.1);
            }
          }
        }
      }
    }
    
    .tab-item {
      display: inline-flex;
      align-items: center;
      
      .anticon {
        margin-right: 8px;
        font-size: 14px;
      }
    }
    
    .tabs-actions {
      position: absolute;
      right: 0;
      top: 0;
      width: 40px;
      height: 40px;
      display: flex;
      justify-content: center;
      align-items: center;
      border-left: 1px solid var(--ant-border-color-split);
      
      .ant-btn {
        width: 40px;
        height: 40px;
        
        &:hover {
          color: var(--ant-primary-color);
        }
      }
    }
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .page-tabs-wrapper {
    .breadcrumb-section {
      padding: 8px;
    }
  }
}
</style> 
