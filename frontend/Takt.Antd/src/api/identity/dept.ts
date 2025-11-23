import {request} from '@/utils/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { 
  TaktDeptQuery, 
  TaktDept,
  TaktDeptCreate,
  TaktDeptUpdate
} from '@/types/identity/dept'

/**
 * 获取部门分页列表
 */
export function getDeptList(params: TaktDeptQuery) {
  return request<TaktPagedResult<TaktDept>>({
    url: '/api/TaktDept/list',
    method: 'get',
    params
  })
}

/**
 * 获取部门树形结构
 */
export function getDeptTree(params: TaktDeptQuery) {
  return request<TaktDept[]>({
    url: '/api/TaktDept/tree',
    method: 'get',
    params
  })
}

/**
 * 获取部门详情
 */
export function getByIdAsync(deptId: string) {
  return request<TaktDept>({
    url: `/api/TaktDept/${deptId}`,
    method: 'get'
  })
}

/**
 * 创建部门
 */
export function createDept(data: TaktDeptCreate) {
  return request<number>({
    url: '/api/TaktDept',
    method: 'post',
    data
  })
}

/**
 * 更新部门
 */
export function updateDept(data: TaktDeptUpdate) {
  return request<boolean>({
    url: '/api/TaktDept',
    method: 'put',
    data
  })
}

/**
 * 删除部门
 */
export function deleteDept(deptId: string) {
  return request<boolean>({
    url: `/api/TaktDept/${deptId}`,
    method: 'delete'
  })
}

/**
 * 批量删除部门
 */
export function batchDeleteDept(deptIds: string[]) {
  return request<boolean>({
    url: '/api/TaktDept/batch',
    method: 'delete',
    data: deptIds
  })
}

/**
 * 修改部门状态
 */
export function updateDeptStatus(deptId: string, status: number) {
  return request<boolean>({
    url: `/api/TaktDept/${deptId}/status`,
    method: 'put',
    params: { status }
  })
}

/**
 * 获取部门选项列表
 */
export function getDeptOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktDept/options',
    method: 'get'
  })
}

/**
 * 获取用户部门列表
 */
export function getUserDepts(deptId: string, params: any) {
  return request<TaktPagedResult<any>>({
    url: `/api/TaktDept/${deptId}/users`,
    method: 'get',
    params
  })
}

/**
 * 分配用户部门
 */
export function assignUserDepts(deptId: string, userIds: string[]) {
  return request<boolean>({
    url: `/api/TaktDept/${deptId}/users`,
    method: 'post',
    data: userIds
  })
}

/**
 * 获取角色部门列表
 */
export function getRoleDepts(deptId: string, params: any) {
  return request<TaktPagedResult<any>>({
    url: `/api/TaktDept/${deptId}/roles`,
    method: 'get',
    params
  })
}

/**
 * 分配角色部门
 */
export function assignRoleDepts(deptId: string, roleIds: string[]) {
  return request<boolean>({
    url: `/api/TaktDept/${deptId}/roles`,
    method: 'post',
    data: roleIds
  })
}

/**
 * 导入部门数据
 */
export function importDept(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktDept/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出部门数据
 */
export function exportDept(params?: TaktDeptQuery) {
  return request({
    url: '/api/TaktDept/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getTemplate() {
  return request({
    url: '/api/TaktDept/template',
    method: 'get',
    responseType: 'blob'
  })
} 
