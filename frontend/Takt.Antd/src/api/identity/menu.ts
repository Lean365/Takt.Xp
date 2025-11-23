import {request} from '@/utils/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { 
  TaktMenuQuery, 
  TaktMenu,
  TaktMenuCreate,
  TaktMenuUpdate,
  TaktMenuStatus,
  TaktMenuOrder
} from '@/types/identity/menu'

/**
 * 获取菜单分页列表
 */
export function getMenuList(params: TaktMenuQuery) {
  return request<TaktPagedResult<TaktMenu>>({
    url: '/api/TaktMenu/list',
    method: 'get',
    params
  })
}

/**
 * 获取菜单详情
 */
export function getByIdAsync(menuId: string) {
  return request<TaktMenu>({
    url: `/api/TaktMenu/${menuId}`,
    method: 'get'
  })
}

/**
 * 创建菜单
 */
export function createMenu(data: TaktMenuCreate) {
  return request<number>({
    url: '/api/TaktMenu',
    method: 'post',
    data
  })
}

/**
 * 更新菜单
 */
export function updateMenu(data: TaktMenuUpdate) {
  return request<boolean>({
    url: '/api/TaktMenu',
    method: 'put',
    data
  })
}

/**
 * 删除菜单
 */
export function deleteMenu(menuId: string) {
  return request<boolean>({
    url: `/api/TaktMenu/${menuId}`,
    method: 'delete'
  })
}

/**
 * 批量删除菜单
 */
export function batchDeleteMenu(menuIds: string[]) {
  return request<boolean>({
    url: '/api/TaktMenu/batch',
    method: 'delete',
    data: menuIds
  })
}

/**
 * 更新菜单状态
 */
export function updateMenuStatus(data: TaktMenuStatus) {
  return request<boolean>({
    url: `/api/TaktMenu/${data.menuId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 更新菜单排序
 */
export function updateMenuOrder(data: TaktMenuOrder) {
  return request<boolean>({
    url: '/api/TaktMenu/order',
    method: 'put',
    data
  })
}

/**
 * 获取菜单树形结构
 */
export function getMenuTree(params?: TaktMenuQuery) {
  return request<TaktMenu[]>({
    url: '/api/TaktMenu/tree',
    method: 'get',
    params
  })
}

/**
 * 获取当前用户的菜单树
 */
export function getCurrentUserMenus(userId: string) {
  return request<TaktMenu[]>({
    url: `/api/TaktMenu/current-user/${userId}`,
    method: 'get'
  })
}

/**
 * 获取菜单选项列表
 */
export function getMenuOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktMenu/options',
    method: 'get'
  })
}

/**
 * 导入菜单数据
 */
export function importMenu(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktMenu/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出菜单数据
 */
export function exportMenu(params?: TaktMenuQuery) {
  return request({
    url: '/api/TaktMenu/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取菜单导入模板
 */
export function getTemplate() {
  return request({
    url: '/api/TaktMenu/template',
    method: 'get',
    responseType: 'blob'
  })
} 
