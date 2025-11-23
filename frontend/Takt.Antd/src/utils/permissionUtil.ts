//===================================================================
// 项目名 : Takt.Xp
// 文件名 : permission.ts
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 权限检查工具函数
//===================================================================

import { message } from 'ant-design-vue'
import { useUserStore } from '@/stores/userStore'
import request from './request'

// 菜单权限类型定义
interface MenuPermission {
  id: number
  name: string
  code: string
  path: string
  component: string
  icon: string
  parentId: number | null
  order: number
  type: number // 1: 目录, 2: 菜单, 3: 按钮
  status: number
  permissions: string[] // 按钮权限列表
}

// 权限缓存键
const PERMISSION_CACHE_KEY = 'user_permissions'
const ROLE_CACHE_KEY = 'user_roles'

// 缓存权限数据
export const cachePermissions = (permissions: string[]) => {
  const cacheData = {
    permissions,
    timestamp: Date.now()
  }
  localStorage.setItem(PERMISSION_CACHE_KEY, JSON.stringify(cacheData))
}

// 缓存角色数据
export const cacheRoles = (roles: string[]) => {
  const cacheData = {
    roles,
    timestamp: Date.now()
  }
  localStorage.setItem(ROLE_CACHE_KEY, JSON.stringify(cacheData))
}

// 获取缓存的权限
export const getCachedPermissions = (): string[] => {
  try {
    const permissionsStr = localStorage.getItem('user_permissions')
    return permissionsStr ? JSON.parse(permissionsStr) : []
  } catch (error) {
    console.error('解析权限缓存失败:', error)
    return []
  }
}

// 获取缓存的角色
export const getCachedRoles = (): string[] => {
  try {
    const rolesStr = localStorage.getItem('user_roles')
    return rolesStr ? JSON.parse(rolesStr) : []
  } catch (error) {
    console.error('解析角色缓存失败:', error)
    return []
  }
}

// 清除权限缓存
export const clearPermissionCache = () => {
  localStorage.removeItem('user_permissions')
  localStorage.removeItem('user_roles')
}

// 加载权限
export const loadPermissions = async () => {
  try {
    const cached = getCachedPermissions()
    if (cached.length > 0) return cached

    // 从后端获取菜单权限
    const response = await request.get('/api/menus')
    const menus: MenuPermission[] = response.data
    
    // 提取所有权限码
    const allPermissions = menus.reduce((acc: string[], menu: MenuPermission) => {
      // 添加菜单权限
      if (menu.code) {
        acc.push(menu.code)
      }
      // 添加按钮权限
      if (menu.permissions && menu.permissions.length > 0) {
        acc.push(...menu.permissions)
      }
      return acc
    }, [])
    
    cachePermissions(allPermissions)
    return allPermissions
  } catch (error) {
    console.error('加载权限失败:', error)
    return []
  }
}

// 权限检查方法
export const hasPermission = (permission: string): boolean => {
  const permissions = getCachedPermissions()
  return permissions.includes(permission)
}

// 角色检查方法
export const hasRole = (role: string): boolean => {
  const roles = getCachedRoles()
  return roles.includes(role)
}

export const hasAnyPermission = (permissions: string[]): boolean => {
  const userPermissions = getCachedPermissions()
  return permissions.some(permission => userPermissions.includes(permission))
}

export const hasAllPermissions = (permissions: string[]): boolean => {
  const userPermissions = getCachedPermissions()
  return permissions.every(permission => userPermissions.includes(permission))
}

export const hasAnyRole = (roles: string[]): boolean => {
  const userRoles = getCachedRoles()
  return roles.some(role => userRoles.includes(role))
}

export const hasAllRoles = (roles: string[]): boolean => {
  const userRoles = getCachedRoles()
  return roles.every(role => userRoles.includes(role))
}

// 新增：权限检查并提示错误
export const checkPermission = (permission: string, showError = true): boolean => {
  const userStore = useUserStore()
  const has = userStore.permissions.includes(permission)
  
  if (!has && showError) {
    message.error(`权限不足！缺少权限：${permission}`)
  }
  
  return has
}

// 新增：权限列表检查并提示错误
export const checkPermissions = (permissions: string[], showError = true): boolean => {
  const userStore = useUserStore()
  const missingPermissions = permissions.filter(permission => !userStore.permissions.includes(permission))
  const has = missingPermissions.length === 0
  
  if (!has && showError) {
    message.error(`权限不足！缺少以下权限：${missingPermissions.join(', ')}`)
  }
  
  return has
}

// 新增：角色检查并提示错误
export const checkRole = (role: string, showError = true): boolean => {
  const userStore = useUserStore()
  const has = userStore.roles.includes(role)
  
  if (!has && showError) {
    message.error(`角色不足！缺少角色：${role}`)
  }
  
  return has
}

// 新增：角色列表检查并提示错误
export const checkRoles = (roles: string[], showError = true): boolean => {
  const userStore = useUserStore()
  const missingRoles = roles.filter(role => !userStore.roles.includes(role))
  const has = missingRoles.length === 0
  
  if (!has && showError) {
    message.error(`角色不足！缺少以下角色：${missingRoles.join(', ')}`)
  }
  
  return has
}

// 新增：权限不足时的友好提示
export const showPermissionError = (permissions: string[], action = '执行此操作') => {
  const userStore = useUserStore()
  const missingPermissions = permissions.filter(permission => !userStore.permissions.includes(permission))
  
  if (missingPermissions.length > 0) {
    message.error({
      content: `权限不足！${action}需要以下权限：${missingPermissions.join(', ')}`,
      duration: 5
    })
    return false
  }
  
  return true
}

// 新增：角色不足时的友好提示
export const showRoleError = (roles: string[], action = '执行此操作') => {
  const userStore = useUserStore()
  const missingRoles = roles.filter(role => !userStore.roles.includes(role))
  
  if (missingRoles.length > 0) {
    message.error({
      content: `角色不足！${action}需要以下角色：${missingRoles.join(', ')}`,
      duration: 5
    })
    return false
  }
  
  return true
}

// 获取菜单树
export const getMenuTree = async (): Promise<MenuPermission[]> => {
  try {
    const response = await request.get('/api/menus')
    const menus: MenuPermission[] = response.data
    
    // 构建菜单树
    const buildTree = (items: MenuPermission[], parentId: number | null = null): MenuPermission[] => {
      return items
        .filter(item => item.parentId === parentId)
        .map(item => ({
          ...item,
          children: buildTree(items, item.id)
        }))
    }
    
    return buildTree(menus)
  } catch (error) {
    console.error('获取菜单树失败:', error)
    return []
  }
}

// 动态权限加载
export const loadDynamicPermissions = async () => {
  try {
    // 从后端获取动态权限配置
    const response = await request.get('/api/permissions/dynamic')
    const { menus, buttons } = response.data
    
    // 合并菜单权限和按钮权限
    const allPermissions = [
      ...menus.map((menu: MenuPermission) => menu.code),
      ...buttons
    ]
    
    // 更新缓存
    cachePermissions(allPermissions)
    
    return {
      menus,
      buttons,
      allPermissions
    }
  } catch (error) {
    console.error('加载动态权限失败:', error)
    return {
      menus: [],
      buttons: [],
      allPermissions: []
    }
  }
}

// 检查动态权限
export const hasDynamicPermission = (permission: string) => {
  const permissions = getCachedPermissions()
  return permissions.includes(permission)
}

// 批量检查动态权限
export const hasAnyDynamicPermission = (permissions: string[]) => {
  const userPermissions = getCachedPermissions()
  return permissions.some(permission => userPermissions.includes(permission))
}

export const hasAllDynamicPermissions = (permissions: string[]) => {
  const userPermissions = getCachedPermissions()
  return permissions.every(permission => userPermissions.includes(permission))
} 

