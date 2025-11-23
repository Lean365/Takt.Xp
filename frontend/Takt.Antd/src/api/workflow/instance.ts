import {request} from '@/utils/request'
import type { TaktInstanceQuery, TaktInstance, TaktInstanceStatus, TaktInstanceUpdate, TaktInstanceCreate } from '@/types/workflow/instance'
import type { TaktPagedResult } from '@/types/common'

// 获取工作流实例分页列表
export function getInstanceList(query: TaktInstanceQuery) {
  return request<TaktPagedResult<TaktInstance>>({
    url: '/api/TaktInstance/list',
    method: 'get',
    params: query
  })
}

/**
 * 根据ID获取工作流实例
 */
export function getInstanceById(instanceId: string) {
  return request<TaktInstance>({
    url: `/api/TaktInstance/${instanceId}`,
    method: 'get'
  })
}

// 根据业务键获取工作流实例
export function getInstanceByBusinessKey(businessKey: string) {
  return request<TaktInstance>({
    url: `/api/TaktInstance/business/${businessKey}`,
    method: 'get'
  })
}

// 创建工作流实例
export function createInstance(data: TaktInstanceCreate) {
  return request<number>({
    url: '/api/TaktInstance',
    method: 'post',
    data
  })
}

// 更新工作流实例
export function updateInstance(data: TaktInstanceUpdate) {
  return request<boolean>({
    url: '/api/TaktInstance',
    method: 'put',
    data
  })
}

/**
 * 删除工作流实例
 */
export function deleteInstance(instanceId: string) {
  return request<boolean>({
    url: `/api/TaktInstance/${instanceId}`,
    method: 'delete'
  })
}

/**
 * 批量删除工作流实例
 */
export function batchDeleteInstance(instanceIds: string[]) {
  return request<boolean>({
    url: '/api/TaktInstance/batch',
    method: 'delete',
    data: instanceIds
  })
}

// 导入工作流实例
export function importInstance(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktInstance/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流实例
export function exportInstance(query: TaktInstanceQuery) {
  return request<Blob>({
    url: '/api/TaktInstance/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getInstanceTemplate() {
  return request<Blob>({
    url: '/api/TaktInstance/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 更新工作流实例状态
export function updateInstanceStatus(data: TaktInstanceStatus) {
  return request<boolean>({
    url: `/api/TaktInstance/${data.instanceId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 设置工作流实例变量
 */
export function setInstanceVariables(instanceId: string, variables: Record<string, any>) {
  return request<boolean>({
    url: `/api/TaktInstance/${instanceId}/variables`,
    method: 'put',
    data: variables
  })
}

// 获取我的工作流实例列表
export function getMyInstances(query: TaktInstanceQuery) {
  return request<TaktPagedResult<TaktInstance>>({
    url: '/api/TaktInstance/my',
    method: 'get',
    params: query
  })
}

