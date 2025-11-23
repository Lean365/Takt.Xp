<template>
  <div class="top-menu-container">
    <a-menu
      v-model:selectedKeys="topSelectedKeys"
      :theme="theme"
      mode="horizontal"
      :items="topMenuItems"
      class="top-menu"
      @select="handleTopMenuSelect"
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
const topSelectedKeys = ref<string[]>([])
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

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

// 获取所有菜单项
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





// 顶部菜单配置（显示完整的目录树）
const topMenuItems = computed<MenuProps['items']>(() => {
  return getAllMenuItems()
})

// 监听路由变化，更新选中状态
watch(
  () => route.path,
  path => {
    const normalizedPath = path.startsWith('/') ? path : `/${path}`
    
    // 更新顶部菜单选中状态 - 选中当前路径对应的菜单项
    topSelectedKeys.value = [normalizedPath]
  },
  { immediate: true }
)

// 处理顶部菜单选择
const handleTopMenuSelect = async (info: SelectInfo) => {
  if (typeof info.key === 'string') {
    await handleRouteNavigation(info.key)
  }
}
</script>

<style lang="less" scoped>
.top-menu-container {
  flex: 1;
  margin: 0 24px;
  overflow-x: auto;

  .top-menu {
    height: 64px;
    line-height: 64px;
    border: none;
    background: transparent;
    white-space: nowrap;

    :deep(.ant-menu-item),
    :deep(.ant-menu-submenu) {
      height: 64px;
      line-height: 64px;
      padding: 0 12px;
      
      .anticon {
        margin-right: 6px;
      }
      
      &::after {
        display: none;
      }
      
      &:hover {
        background: var(--ant-primary-1);
      }
    }

    :deep(.ant-menu-submenu-title) {
      height: 64px;
      line-height: 64px;
    }

    :deep(.ant-menu-sub) {
      background: var(--ant-color-bg-container);
      border: 1px solid var(--ant-color-split);
      border-radius: 6px;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    }

    &.ant-menu-dark {
      :deep(.ant-menu-item),
      :deep(.ant-menu-submenu) {
        &:hover {
          background: var(--ant-primary-1);
        }
      }

      :deep(.ant-menu-sub) {
        background: var(--ant-color-bg-container);
        border: 1px solid var(--ant-color-split);
      }
    }
  }

  &::-webkit-scrollbar {
    height: 4px;
  }

  &::-webkit-scrollbar-thumb {
    border-radius: 2px;
    background: var(--ant-color-text-quaternary);
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .top-menu-container {
    margin: 0 8px;

    .top-menu {
      :deep(.ant-menu-item),
      :deep(.ant-menu-submenu) {
        padding: 0 8px;
        
        .anticon {
          margin-right: 4px;
        }
      }
    }
  }
}
</style> 
