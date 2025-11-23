<template>
  <div class="mix-menu-container">
    <!-- 侧边栏所有菜单 -->
    <a-menu
      v-model:selectedKeys="sideSelectedKeys"
      v-model:openKeys="openKeys"
      :theme="theme"
      mode="inline"
      :items="sideMenuItems"
      class="side-menu"
      @select="handleSideMenuSelect"
      @openChange="handleOpenChange"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, watch, h, type VNode } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter, useRoute } from 'vue-router'
import type { MenuProps } from 'ant-design-vue'
import type { SelectInfo } from 'ant-design-vue/es/menu/src/interface'
import { useThemeStore } from '@/stores/themeStore'
import { useSettingsStore } from '@/stores/settingsStore'
import { useMenuStore } from '@/stores/menuStore'
import type { TaktMenu } from '@/types/identity/menu'
import { TaktMenuType } from '@/types/common'
import * as Icons from '@ant-design/icons-vue'
import { handleRouteNavigation } from '@/router'

const { t } = useI18n()
const router = useRouter()
const route = useRoute()
const themeStore = useThemeStore()
const settingsStore = useSettingsStore()
const menuStore = useMenuStore()

// 响应式状态
const sideSelectedKeys = ref<string[]>([])
const openKeys = ref<(string | number)[]>([])
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

// 监听主题变化
watch(() => themeStore.isDarkMode, (newValue) => {
  console.log('[TaktMixMenu] 主题模式变化:', newValue ? 'dark' : 'light')
}, { immediate: true })

// 监听主色调变化
watch(() => settingsStore.currentPrimaryColor, (newColor) => {
  console.log('[TaktMixMenu] 主色调变化:', newColor)
}, { immediate: true })

// 图标映射
const iconMap = Icons

// 安全的图标获取函数
const getSafeIcon = (iconName: string | undefined) => {
  if (!iconName) {
    return undefined
  }

  // 尝试直接使用图标名称
  const IconComponent = (iconMap as any)[iconName]
  if (IconComponent) {
    return () => h(IconComponent)
  }

  // 如果没找到，尝试添加 Outlined 后缀
  const outlinedIconName = iconName.endsWith('Outlined') ? iconName : `${iconName}Outlined`
  const OutlinedIconComponent = (iconMap as any)[outlinedIconName]
  if (OutlinedIconComponent) {
    return () => h(OutlinedIconComponent)
  }

  // 如果还是没找到，返回默认图标
  return () => h(iconMap.MenuOutlined)
}

// 处理菜单项
const processMenuItem = (menu: TaktMenu, parentPath: string = ''): any => {
  // 确保menu对象存在
  if (!menu) {
    return {
      key: parentPath || '/',
      label: '未命名菜单'
    }
  }

  // 处理路径
  const menuPath = menu.path || ''
  let fullPath = ''

  if (menu.menuType === TaktMenuType.Directory) {
    // 目录类型：如果是子目录，需要包含父路径
    if (parentPath) {
      const relativePath = menuPath.startsWith('/') ? menuPath.slice(1) : menuPath
      fullPath = `${parentPath}/${relativePath}`.replace(/\/+/g, '/')
    } else {
      // 顶级目录：直接使用路径
      fullPath = menuPath.startsWith('/') ? menuPath : `/${menuPath}`
    }
  } else {
    // 页面类型：需要包含完整的父路径
    const relativePath = menuPath.startsWith('/') ? menuPath.slice(1) : menuPath
    if (parentPath) {
      fullPath = `${parentPath}/${relativePath}`.replace(/\/+/g, '/')
    } else {
      // 顶级页面：直接使用路径
      fullPath = `/${relativePath}`
    }
  }

  // 构建菜单项
  const result: any = {
    key: fullPath,
    icon: menu.icon ? getSafeIcon(menu.icon) : undefined,
    label: menu.transKey ? t(menu.transKey) : menu.menuName
  }

  // 处理子菜单
  if (menu.children?.length) {
    result.children = menu.children.map((child: TaktMenu) => processMenuItem(child, fullPath)).filter(Boolean)
  }

  return result
}

// 获取所有菜单项（用于侧边栏）
const getAllMenuItems = (): any[] => {
  if (menuStore.isLoading) {
    return []
  }

  // 获取根路由
  const rootRoute = router.getRoutes().find(route => route.path === '/')
  if (!rootRoute?.children) return []

  const allMenus: any[] = []

  // 主页、仪表盘和组件菜单
  const baseMenus: any[] = rootRoute.children
    .filter(child => ['home', 'dashboard', 'components'].includes(child.path))
    .map(child => {
      const meta = child.meta
      if (!meta?.title || meta.requiresAuth === false) return null

      const parentPath = child.path.startsWith('/') ? child.path : `/${child.path}`

      if (child.children) {
        return {
          key: parentPath,
          icon: meta.icon ? getSafeIcon(meta.icon) : undefined,
          label: t(meta.title as string),
          children: child.children.map((subChild: any) => {
            const subMeta = subChild.meta
            const childPath = subChild.path.startsWith('/')
              ? subChild.path
              : `${parentPath}/${subChild.path}`
            return {
              key: childPath,
              icon: subMeta?.icon ? getSafeIcon(subMeta.icon) : undefined,
              label: t((subMeta?.title as string) || '')
            }
          })
        }
      } else {
        return {
          key: parentPath,
          icon: meta.icon ? getSafeIcon(meta.icon) : undefined,
          label: t(meta.title as string)
        }
      }
    })
    .filter((item): item is any => item !== null)

  allMenus.push(...baseMenus.filter(Boolean))

  // 动态菜单（从后端获取）
  const dynamicMenus = (menuStore.rawMenuList || []).map(menu => processMenuItem(menu))
  allMenus.push(...dynamicMenus)

  // 关于菜单
  const aboutMenu = rootRoute.children
    .filter(child => child.path === '/about')
    .map(child => {
      const meta = child.meta
      if (!meta?.title || meta.requiresAuth === false) return null

      const parentPath = child.path.startsWith('/') ? child.path : `/${child.path}`

      return {
        key: parentPath,
        icon: meta.icon ? getSafeIcon(meta.icon) : undefined,
        label: t(meta.title as string),
        children: child.children?.map((subChild: any) => {
          const subMeta = subChild.meta
          const childPath = subChild.path.startsWith('/')
            ? subChild.path
            : `${parentPath}/${subChild.path}`
          return {
            key: childPath,
            icon: subMeta?.icon ? getSafeIcon(subMeta.icon) : undefined,
            label: t((subMeta?.title as string) || '')
          }
        })
      }
    })
    .filter(Boolean)

  allMenus.push(...aboutMenu.filter(Boolean))

  return allMenus
}

// 查找当前菜单的父级菜单
const findParentMenu = (currentPath: string, allMenus: any[]): any | null => {
  for (const menu of allMenus) {
    if (menu.children) {
      // 检查是否是直接父级
      if (menu.children.some((child: any) => child.key === currentPath)) {
        return menu
      }
      // 递归检查子菜单
      const found = findParentMenu(currentPath, menu.children)
      if (found) {
        return found
      }
    }
  }
  return null
}

// 侧边菜单配置（显示所有菜单）
const sideMenuItems = computed<MenuProps['items']>(() => {
  return getAllMenuItems()
})

// 监听路由变化，更新选中状态
watch(
  () => route.path,
  path => {
    const normalizedPath = path.startsWith('/') ? path : `/${path}`
    
    // 更新侧边菜单选中状态
    sideSelectedKeys.value = [normalizedPath]
    
    // 查找并设置父菜单为展开状态
    const allMenus = getAllMenuItems()
    const parentMenu = findParentMenu(normalizedPath, allMenus)
    if (parentMenu) {
      openKeys.value = [parentMenu.key]
    }
  },
  { immediate: true }
)

// 处理侧边菜单选择
const handleSideMenuSelect = async (info: SelectInfo) => {
  if (typeof info.key === 'string') {
    await handleRouteNavigation(info.key)
  }
}

// 处理菜单展开/收起
const handleOpenChange = (keys: (string | number)[]) => {
  openKeys.value = keys
}
</script>

<style lang="less" scoped>
.mix-menu-container {
  height: 100%;
  width: 100%;
  overflow-y: auto;

  .side-menu {
    border: none;
    background: transparent;

    :deep(.ant-menu-item) {
      height: 40px;
      line-height: 40px;
      margin: 4px 8px;
      border-radius: 6px;
      
      .anticon {
        margin-right: 8px;
      }
      
      &:hover {
        background: var(--ant-primary-1);
      }
      
      &.ant-menu-item-selected {
        background: var(--ant-primary-color);
        color: #fff;
        
        &:hover {
          background: var(--ant-primary-color-hover);
        }
      }
    }

    :deep(.ant-menu-submenu) {
      .ant-menu-submenu-title {
        height: 40px;
        line-height: 40px;
        margin: 4px 8px;
        border-radius: 6px;
        
        .anticon {
          margin-right: 8px;
        }
        
        &:hover {
          background: var(--ant-primary-1);
        }
      }
      
      .ant-menu-sub {
        background: transparent;
        
        .ant-menu-item {
          margin-left: 16px;
          padding-left: 16px;
        }
      }
    }

    &.ant-menu-dark {
      :deep(.ant-menu-item) {
        &:hover {
          background: var(--ant-primary-1);
        }
        
        &.ant-menu-item-selected {
          background: var(--ant-primary-color);
          color: #fff;
          
          &:hover {
            background: var(--ant-primary-color-hover);
          }
        }
      }

      :deep(.ant-menu-submenu) {
        .ant-menu-submenu-title {
          &:hover {
            background: var(--ant-primary-1);
          }
        }
      }
    }
  }

  &::-webkit-scrollbar {
    width: 6px;
    height: 6px;
  }

  &::-webkit-scrollbar-thumb {
    border-radius: 3px;
    background: var(--ant-color-text-quaternary);
  }
}
</style> 
