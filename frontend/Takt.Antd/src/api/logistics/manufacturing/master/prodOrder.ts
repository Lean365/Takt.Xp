import {request} from '@/utils/request'
import type { TaktProdOrder, TaktProdOrderQuery, TaktProdOrderCreate, TaktProdOrderUpdate, TaktProdOrderStatus, TaktProdOrderChangeLog } from '@/types/logistics/manufacturing/master/prodOrder'

import type { TaktPagedResult, TaktApiResponse, TaktSelectOption } from '@/types/common'

/**
 * 获取生产工单分页列表
 */
export function getProdOrderList(params: TaktProdOrderQuery) {
  return request<TaktPagedResult<TaktProdOrder>>({
    url: '/api/TaktProdOrder/list',
    method: 'get',
    params
  })
}

/**
 * 获取生产工单详情
 */
export function getProdOrderById(prodOrderId: number | bigint) {
  return request<TaktProdOrder>({
    url: `/api/TaktProdOrder/${prodOrderId}`,
    method: 'get'
  })
}

/**
 * 获取生产工单选项列表
 */
export function getProdOrderOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktProdOrder/options',
    method: 'get'
  })
}

/**
 * 创建生产工单
 */
export function createProdOrder(data: TaktProdOrderCreate) {
  return request<number>({
    url: '/api/TaktProdOrder',
    method: 'post',
    data
  })
}

/**
 * 更新生产工单
 */
export function updateProdOrder(data: TaktProdOrderUpdate) {
  return request<boolean>({
    url: '/api/TaktProdOrder',
    method: 'put',
    data
  })
}

/**
 * 删除生产工单
 */
export function deleteProdOrder(prodOrderId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktProdOrder/${prodOrderId}`,
    method: 'delete'
  })
}

/**
 * 批量删除生产工单
 */
export function batchDeleteProdOrder(prodOrderIds: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktProdOrder/batch',
    method: 'delete',
    data: prodOrderIds
  })
}

/**
 * 更新生产工单状态
 */
export function updateProdOrderStatus(prodOrderId: number | bigint, status: number) {
  return request<boolean>({
    url: `/api/TaktProdOrder/${prodOrderId}/status`,
    method: 'put',
    data: status
  })
}

/**
 * 更新生产工单数量
 */
export function updateProdOrderQuantity(prodOrderId: number | bigint, producedQuantity: number) {
  return request<boolean>({
    url: `/api/TaktProdOrder/${prodOrderId}/quantity`,
    method: 'put',
    data: producedQuantity
  })
}

/**
 * 获取生产工单变更记录
 */
export function getProdOrderChangeLogs(prodOrderId: number | bigint) {
  return request<TaktProdOrderChangeLog[]>({
    url: `/api/TaktProdOrder/${prodOrderId}/changelogs`,
    method: 'get'
  })
}

/**
 * 导入生产工单数据
 */
export function importProdOrder(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktProdOrder/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出生产工单数据
 */
export function exportProdOrder(params: TaktProdOrderQuery) {
  return request<Blob>({
    url: '/api/TaktProdOrder/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取生产工单导入模板
 */
export function getProdOrderTemplate() {
  return request<Blob>({
    url: '/api/TaktProdOrder/template',
    method: 'get',
    responseType: 'blob'
  })
}

