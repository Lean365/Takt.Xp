import {request} from '@/utils/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { 
  TaktRoleQuery, 
  TaktRole,
  TaktRoleCreate,
  TaktRoleUpdate,
  TaktRoleStatus
} from '@/types/identity/role'

/**
 * 获取角色分页列表
 */
export function getRoleList(params: TaktRoleQuery) {
  return request<TaktPagedResult<TaktRole>>({
    url: '/api/TaktRole/list',
    method: 'get',
    params
  })
}

/**
 * 获取角色详情
 */
export function getByIdAsync(roleId: string) {
  return request<TaktRole>({
    url: `/api/TaktRole/${roleId}`,
    method: 'get'
  })
}

/**
 * 创建角色
 */
export function createRole(data: TaktRoleCreate) {
  return request<number>({
    url: '/api/TaktRole',
    method: 'post',
    data
  })
}

/**
 * 更新角色
 */
export function updateRole(data: TaktRoleUpdate) {
  return request<boolean>({
    url: '/api/TaktRole',
    method: 'put',
    data
  })
}

/**
 * 删除角色
 */
export function deleteRole(roleId: string) {
  return request<boolean>({
    url: `/api/TaktRole/${roleId}`,
    method: 'delete'
  })
}

/**
 * 批量删除角色
 */
export function batchDeleteRole(roleIds: string[]) {
  return request<boolean>({
    url: '/api/TaktRole/batch',
    method: 'delete',
    data: roleIds
  })
}

/**
 * 更新角色状态
 */
export function updateRoleStatus(data: TaktRoleStatus) {
  return request<boolean>({
    url: `/api/TaktRole/${data.roleId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 获取角色选项列表
 */
export function getRoleOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktRole/options',
    method: 'get'
  })
}

/**
 * 获取角色部门列表
 */
export function getRoleDepts(roleId: string) {
  return request<any[]>({
    url: `/api/TaktRole/${roleId}/depts`,
    method: 'get'
  })
}

/**
 * 获取角色菜单列表
 */
export function getRoleMenus(roleId: string) {
  return request<any[]>({
    url: `/api/TaktRole/${roleId}/menus`,
    method: 'get'
  })
}

/**
 * 分配角色菜单
 */
export function assignRoleMenus(roleId: string, menuIds: string[]) {
  return request<boolean>({
    url: `/api/TaktRole/${roleId}/menus`,
    method: 'put',
    data: menuIds
  })
}

/**
 * 分配角色用户
 */
export function assignRoleUsers(roleId: string, userIds: string[]) {
  return request<boolean>({
    url: `/api/TaktRole/${roleId}/users`,
    method: 'put',
    data: userIds
  })
}

/**
 * 分配角色部门
 */
export function assignRoleDepts(roleId: string, deptIds: string[]) {
  return request<boolean>({
    url: `/api/TaktRole/${roleId}/depts`,
    method: 'put',
    data: deptIds
  })
}

/**
 * 导入角色数据
 */
export function importRole(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktRole/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出角色数据
 */
export function exportRole(params?: TaktRoleQuery) {
  return request({
    url: '/api/TaktRole/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取角色导入模板
 */
export function getTemplate() {
  return request({
    url: '/api/TaktRole/template',
    method: 'get',
    responseType: 'blob'
  })
}

