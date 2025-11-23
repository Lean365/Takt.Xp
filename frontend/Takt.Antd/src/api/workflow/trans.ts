import {request} from '@/utils/request'
import type { TaktInstanceTransQuery, TaktInstanceTrans } from '@/types/workflow/trans'
import type { TaktPagedResult } from '@/types/common'

// 获取工作流实例流转历史分页列表
export function getInstanceTransList(query: TaktInstanceTransQuery) {
  return request<TaktPagedResult<TaktInstanceTrans>>({
    url: '/api/TaktInstanceTrans/list',
    method: 'get',
    params: query
  })
}

/**
 * 根据ID获取工作流实例转换
 */
export function getInstanceTransById(instanceTransId: string) {
  return request<TaktInstanceTrans>({
    url: `/api/TaktInstanceTrans/${instanceTransId}`,
    method: 'get'
  })
}


/**
 * 删除工作流实例转换
 */
export function deleteInstanceTrans(instanceTransId: string) {
  return request<boolean>({
    url: `/api/TaktInstanceTrans/${instanceTransId}`,
    method: 'delete'
  })
}

/**
 * 批量删除工作流实例转换
 */
export function batchDeleteInstanceTrans(instanceTransIds: string[]) {
  return request<boolean>({
    url: '/api/TaktInstanceTrans/batch',
    method: 'delete',
    data: instanceTransIds
  })
}


// 导出工作流实例流转历史
export function exportInstanceTrans(query: TaktInstanceTransQuery) {
  return request<Blob>({
    url: '/api/TaktInstanceTrans/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}


// 获取工作流实例流转历史选项列表
export function getInstanceTransOptions() {
  return request<{ label: string; value: number }[]>({
    url: '/api/TaktInstanceTrans/options',
    method: 'get'
  })
}

/**
 * 根据实例ID获取转换列表
 */
export function getInstanceTransListByInstance(instanceId: string, query: TaktInstanceTransQuery) {
  return request<TaktPagedResult<TaktInstanceTrans>>({
    url: `/api/TaktInstanceTrans/instance/${instanceId}`,
    method: 'get',
    params: query
  })
}

/**
 * 获取实例转换路径
 */
export function getInstanceTransPath(instanceId: string) {
  return request<any>({
    url: `/api/TaktInstanceTrans/${instanceId}/path`,
    method: 'get'
  })
}

/**
 * 获取实例转换统计
 */
export function getInstanceTransStats(instanceId: string) {
  return request<any>({
    url: `/api/TaktInstanceTrans/${instanceId}/stats`,
    method: 'get'
  })
}

// 获取我的待办任务列表
export function getMyTodoList(query: TaktInstanceTransQuery) {
  return request<TaktPagedResult<TaktInstanceTrans>>({
    url: '/api/TaktInstanceTrans/my/todo',
    method: 'get',
    params: query
  })
}

// 获取我的已办任务列表
export function getMyDoneList(query: TaktInstanceTransQuery) {
  return request<TaktPagedResult<TaktInstanceTrans>>({
    url: '/api/TaktInstanceTrans/my/done',
    method: 'get',
    params: query
  })
}

// 获取我的流程列表
export function getMyProcessList(query: TaktInstanceTransQuery) {
  return request<TaktPagedResult<TaktInstanceTrans>>({
    url: '/api/TaktInstanceTrans/my/process',
    method: 'get',
    params: query
  })
}

