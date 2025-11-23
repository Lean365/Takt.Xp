<template>
  <div class="menu-wrapper">
    <a-menu
      v-model:selectedKeys="selectedKeys"
      v-model:openKeys="openKeys"
      mode="inline"
      :theme="theme"
      :items="menuItems"
      @click="handleMenuClick"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, watch, h, type VNode } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRoute, useRouter, type RouteRecordRaw } from 'vue-router'
import type { MenuProps } from 'ant-design-vue'
import type { MenuInfo } from 'ant-design-vue/lib/menu/src/interface'
import { useMenuStore } from '@/stores/menuStore'
import { useThemeStore } from '@/stores/themeStore'
import { useSettingsStore } from '@/stores/settingsStore'
import type { TaktMenu } from '@/types/identity/menu'
import { TaktMenuType } from '@/types/common'
import * as Icons from '@ant-design/icons-vue'
import {  handleRouteNavigation } from '@/router'

const { t } = useI18n()
const route = useRoute()
const router = useRouter()
const menuStore = useMenuStore()
const themeStore = useThemeStore()
const settingsStore = useSettingsStore()

// 主题计算属性
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

// 响应式状态
const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])

// 监听主题变化，确保菜单能够正确响应主题切换
watch(() => themeStore.isDarkMode, (newValue) => {
  console.log('[TaktSiderMenu] 主题模式变化:', newValue ? 'dark' : 'light')
}, { immediate: true })

// 监听侧边栏颜色变化
watch(() => settingsStore.currentSidebarColor, (newColor) => {
  console.log('[TaktSiderMenu] 侧边栏颜色变化:', newColor)
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

interface MenuItemType {
  key: string
  icon?: () => VNode
  label: string
  children?: MenuItemType[]
}

// 查找指定路径的菜单项
const findMenuByPath = (menus: TaktMenu[] | undefined, path: string): TaktMenu | undefined => {
  if (!menus) return undefined

  for (const menu of menus) {
    if (menu.path === path) {
      return menu
    }
    if (menu.children?.length) {
      const found = findMenuByPath(menu.children, path)
      if (found) return found
    }
  }
  return undefined
}

// 处理基础菜单项
const processBaseMenu = (child: RouteRecordRaw) => {
  const meta = child.meta
  if (!meta?.title || meta.requiresAuth === false) return null

  return {
    key: child.path.startsWith('/') ? child.path : `/${child.path}`,
    icon: meta.icon ? getSafeIcon(meta.icon) : undefined,
    label: t(meta.title as string)
  }
}

// 处理带子菜单的菜单项
const processSubMenus = (child: RouteRecordRaw) => {
  const meta = child.meta
  if (!meta?.title || meta.requiresAuth === false || !child.children) return null

  const parentPath = child.path.startsWith('/') ? child.path : `/${child.path}`

  return {
    key: parentPath,
    icon: meta.icon ? getSafeIcon(meta.icon) : undefined,
    label: t(meta.title as string),
    children: child.children.map((subChild: RouteRecordRaw) => {
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
}

// 处理动态菜单项
const processMenuItem = (menu: TaktMenu, parentPath: string = ''): MenuItemType => {
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
  const result: MenuItemType = {
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

// 菜单配置
const menuItems = computed<MenuProps['items']>(() => {
  if (menuStore.isLoading) {
    return []
  }



  // 获取根路由
  const rootRoute = router.getRoutes().find(route => route.path === '/')
  if (!rootRoute?.children) return []

  // 主页、仪表盘和组件菜单
  const baseMenus = rootRoute.children
    .filter(child => ['home', 'dashboard', 'components'].includes(child.path))
    .map(child => (child.children ? processSubMenus(child) : processBaseMenu(child)))
    .filter(Boolean)

  // 动态菜单（从后端获取）
  const dynamicMenus = (menuStore.rawMenuList || []).map(menu => processMenuItem(menu))

  // 关于菜单
  const aboutMenu = rootRoute.children
    .filter(child => child.path === '/about')
    .map(child => processSubMenus(child))
    .filter(Boolean)

  // 合并所有菜单：主页/仪表盘/组件 + 动态菜单 + About菜单
  const allMenus = [...baseMenus, ...dynamicMenus, ...aboutMenu]

  return allMenus
})

// 监听路由变化，更新选中状态
watch(
  () => route.path,
  path => {
    const normalizedPath = path.startsWith('/') ? path : `/${path}`

    // 设置选中的菜单项
    selectedKeys.value = [normalizedPath]

    // 展开当前路径的父级菜单
    const pathParts = normalizedPath.split('/').filter(Boolean)
    if (pathParts.length > 1) {
      const parentKeys = []
      let currentPath = ''
      for (const part of pathParts.slice(0, -1)) {
        currentPath += `/${part}`
        // 查找对应的目录菜单
        const menuItem = findMenuByPath(menuStore.rawMenuList, currentPath)
        if (menuItem && menuItem.menuType === TaktMenuType.Directory) {
          parentKeys.push(`dir_${menuItem.menuId}`)
        }
      }
      openKeys.value = parentKeys
    }
  },
  { immediate: true }
)



// 查找菜单项
const findMenuItem = (menus: TaktMenu[] | undefined, key: string): TaktMenu | undefined => {
  if (!menus) {
    return undefined
  }

  // 标准化查找的key
  const normalizedKey = key.replace(/\/+/g, '/')

  // 递归查找菜单项
  const findMenuWithKey = (menu: TaktMenu, parentPath: string = ''): TaktMenu | undefined => {
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

    // 规范化路径
    fullPath = fullPath.replace(/\/+/g, '/')

    if (fullPath === normalizedKey) {
      return menu
    }

    // 递归查找子菜单
    if (menu.children?.length) {
      for (const child of menu.children) {
        const found = findMenuWithKey(child, fullPath)
        if (found) {
          return found
        }
      }
    }

    return undefined
  }

  // 遍历顶层菜单
  for (const menu of menus) {
    const found = findMenuWithKey(menu, '')
    if (found) return found
  }

  return undefined
}

// 菜单点击处理
const handleMenuClick = async ({ key }: MenuInfo) => {
  if (typeof key === 'string') {
    try {
      console.log('[菜单点击] 尝试导航到:', key)
      const success = await handleRouteNavigation(key)
      if (!success) {
        console.warn('[菜单点击] 导航失败:', key)
        // 可以在这里添加用户提示
      }
    } catch (error) {
      console.error('[菜单点击] 导航异常:', error)
    }
  }
}
</script>

<style lang="less" scoped>
// 确保侧边栏菜单能够正确响应主题变化
:deep(.ant-menu) {
  // 继承父容器的背景色
  background-color: inherit;
  
  // 确保菜单项在主题切换时能够正确显示
  .ant-menu-item,
  .ant-menu-submenu {
    transition: all 0.3s ease;
    
    &:hover {
      background-color: var(--ant-primary-1);
    }
    
    &.ant-menu-item-selected {
      background-color: var(--ant-primary-color);
      color: var(--ant-color-white);
    }
  }
  
  // 优化菜单项内边距，让内容更贴近左边界
  .ant-menu-item {
    padding-left: 8px !important;
    padding-right: 8px !important;
  }
  
  .ant-menu-submenu-title {
    padding-left: 8px !important;
    padding-right: 8px !important;
  }
  
  // 子菜单项保持适当的缩进
  .ant-menu-sub .ant-menu-item {
    padding-left: 24px !important;
  }
  
  .ant-menu-sub .ant-menu-submenu-title {
    padding-left: 24px !important;
  }
}

// 菜单包装器样式 - 根据内容自动调整
.menu-wrapper {
  width: 100%;
  padding: 0;
  margin: 0;
}
</style>
