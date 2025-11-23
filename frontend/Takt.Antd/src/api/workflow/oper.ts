import {request} from '@/utils/request'
import type { TaktInstanceOperQuery, TaktInstanceOper, TaktInstanceApprove } from '@/types/workflow/oper'
import type { TaktPagedResult } from '@/types/common'

// 获取工作流实例操作记录分页列表
export function getInstanceOperList(query: TaktInstanceOperQuery) {
  return request<TaktPagedResult<TaktInstanceOper>>({
    url: '/api/TaktInstanceOper/list',
    method: 'get',
    params: query
  })
}

/**
 * 根据ID获取工作流实例操作
 */
export function getInstanceOperById(instanceOperId: string) {
  return request<TaktInstanceOper>({
    url: `/api/TaktInstanceOper/${instanceOperId}`,
    method: 'get'
  })
}


/**
 * 删除工作流实例操作
 */
export function deleteInstanceOper(instanceOperId: string) {
  return request<boolean>({
    url: `/api/TaktInstanceOper/${instanceOperId}`,
    method: 'delete'
  })
}

/**
 * 批量删除工作流实例操作
 */
export function batchDeleteInstanceOper(instanceOperIds: string[]) {
  return request<boolean>({
    url: '/api/TaktInstanceOper/batch',
    method: 'delete',
    data: instanceOperIds
  })
}

// 工作流审批
export function approveInstance(data: TaktInstanceApprove) {
  return request<boolean>({
    url: '/api/TaktInstanceOper/approve',
    method: 'post',
    data
  })
}


// 导出工作流实例操作记录
export function exportInstanceOper(query: TaktInstanceOperQuery) {
  return request<Blob>({
    url: '/api/TaktInstanceOper/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}


// 获取工作流实例操作记录选项列表
export function getInstanceOperOptions() {
  return request<{ label: string; value: number }[]>({
    url: '/api/TaktInstanceOper/options',
    method: 'get'
  })
}

// 获取我的操作记录列表
export function getMyInstanceOperList(query: TaktInstanceOperQuery) {
  return request<TaktPagedResult<TaktInstanceOper>>({
    url: '/api/TaktInstanceOper/my',
    method: 'get',
    params: query
  })
}

/**
 * 根据实例ID获取操作列表
 */
export function getInstanceOperListByInstance(instanceId: string, query: TaktInstanceOperQuery) {
  return request<TaktPagedResult<TaktInstanceOper>>({
    url: `/api/TaktInstanceOper/instance/${instanceId}`,
    method: 'get',
    params: query
  })
}

