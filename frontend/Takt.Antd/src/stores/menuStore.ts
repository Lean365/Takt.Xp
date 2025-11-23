import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { TaktMenu } from '@/types/identity/menu'
import { getCurrentUserMenus } from '@/api/identity/menu'
import { registerDynamicRoutes } from '@/router'
import i18n from '@/locales'
import type { Router } from 'vue-router'
import { getToken } from '@/utils/authUtil'
import type { MenuProps } from 'ant-design-vue'
import type { TaktApiResponse } from '@/types/common'
import { useUserStore } from './userStore'

export const useMenuStore = defineStore('menu', () => {
  const rawMenuList = ref<TaktMenu[]>([])
  const menuList = ref<TaktMenu[]>([])
  const isLoading = ref(false)
  const isMenuLoaded = ref(false)
  const menus = ref<MenuProps['items']>([])
  const loading = ref(false)
  const initialized = ref(false)

  const loadUserMenus = async (forceRefresh = false) => {
    try {
      const token = getToken()
      if (!token) {
        return []
      }

      // 如果不是强制刷新，且有缓存数据，则使用缓存
      if (!forceRefresh && isMenuLoaded.value && rawMenuList.value?.length) {
        return rawMenuList.value
      }

      const userStore = useUserStore()
      const userId = userStore.userId
      
      if (!userId) {
        console.warn('[菜单] 用户ID不存在，无法获取菜单')
        return []
      }
      
      const response = await getCurrentUserMenus(userId)
      
      // 添加调试日志
      // console.log('[MenuStore] 原始响应:', response)
      // console.log('[MenuStore] response.data:', response?.data)
      
      if (response?.data) {
        const menuData = response.data as TaktMenu[]
        if (!menuData || menuData.length === 0) {
          return []
        }

        // 过滤掉无效的菜单项
        const validMenus = menuData.filter(menu => {
          const isValid = menu.menuType !== undefined && menu.menuId !== undefined
          return isValid
        })

        if (validMenus.length === 0) {
          return []
        }

        // 更新菜单数据
        rawMenuList.value = validMenus
        menuList.value = validMenus
        isMenuLoaded.value = true

        return validMenus
      }
      throw new Error('获取菜单数据失败')
    } catch (error) {
      console.error('[菜单] 加载菜单失败:', error)
      // 只有在强制刷新（登录）时才清除菜单数据
      if (forceRefresh) {
        clearMenus()
      }
      return []
    }
  }

  const clearMenus = () => {
    rawMenuList.value = []
    menuList.value = []
    isMenuLoaded.value = false
    isLoading.value = false
    initialized.value = false
  }

  const reloadMenus = async (router: Router, forceRefresh = false) => {
    try {
      const menus = await loadUserMenus(forceRefresh)
      
      if (!menus || menus.length === 0) {
        return []
      }

      // 注册动态路由
      const success = await registerDynamicRoutes(router, menus)
      if (!success) {
        console.error('[菜单] 动态路由注册失败')
        // 只有在强制刷新（登录）时才清除菜单数据
        if (forceRefresh) {
          clearMenus()
        }
        return []
      }

      return menus
    } catch (error) {
      console.error('[菜单] 重新加载菜单失败:', error)
      // 只有在强制刷新（登录）时才清除菜单数据
      if (forceRefresh) {
        clearMenus()
      }
      return []
    }
  }

  // 获取Home菜单
  const getHomeMenu = () => {
    if (!isMenuLoaded.value || !rawMenuList.value?.length) {
      return null
    }
    
    return rawMenuList.value.find(menu => 
      menu.path === 'home' || menu.path === '/home' || menu.menuName === '首页'
    )
  }

  return {
    rawMenuList,
    menuList,
    isLoading,
    isMenuLoaded,
    menus,
    loading,
    initialized,
    loadUserMenus,
    clearMenus,
    reloadMenus,
    getHomeMenu
  }
})

