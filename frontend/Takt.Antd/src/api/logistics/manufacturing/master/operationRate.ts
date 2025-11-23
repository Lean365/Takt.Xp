import {request} from '@/utils/request'
import type { TaktOperationRate, TaktOperationRateQuery, TaktOperationRateCreate, TaktOperationRateUpdate, TaktOperationRateStatus } from '@/types/logistics/manufacturing/master/operationRate'
import type { TaktPagedResult, TaktApiResponse, TaktSelectOption } from '@/types/common'

/**
 * 获取稼动率分页列表
 */
export function getOperationRateList(params: TaktOperationRateQuery) {
  return request<TaktPagedResult<TaktOperationRate>>({
    url: '/api/TaktOperationRate/list',
    method: 'get',
    params
  })
}

/**
 * 获取稼动率详情
 */
export function getOperationRateById(operationRateId: number | bigint) {
  return request<TaktOperationRate>({
    url: `/api/TaktOperationRate/${operationRateId}`,
    method: 'get'
  })
}

/**
 * 创建稼动率
 */
export function createOperationRate(data: TaktOperationRateCreate) {
  return request<number>({
    url: '/api/TaktOperationRate',
    method: 'post',
    data
  })
}

/**
 * 更新稼动率
 */
export function updateOperationRate(data: TaktOperationRateUpdate) {
  return request<boolean>({
    url: '/api/TaktOperationRate',
    method: 'put',
    data
  })
}

/**
 * 删除稼动率
 */
export function deleteOperationRate(operationRateId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktOperationRate/${operationRateId}`,
    method: 'delete'
  })
}

/**
 * 批量删除稼动率
 */
export function batchDeleteOperationRate(operationRateIds: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktOperationRate/batch',
    method: 'delete',
    data: operationRateIds
  })
}

/**
 * 更新稼动率状态
 */
export function updateOperationRateStatus(data: TaktOperationRateStatus) {
  return request<boolean>({
    url: '/api/TaktOperationRate/status',
    method: 'put',
    data
  })
}

/**
 * 获取稼动率选项列表
 */
export function getOperationRateOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktOperationRate/options',
    method: 'get'
  })
}

/**
 * 导入稼动率数据
 */
export function importOperationRate(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  
  return request<{ success: number; fail: number }>({
    url: '/api/TaktOperationRate/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出稼动率数据
 */
export function exportOperationRate(params: TaktOperationRateQuery) {
  return request({
    url: '/api/TaktOperationRate/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取稼动率导入模板
 */
export function getOperationRateTemplate() {
  return request({
    url: '/api/TaktOperationRate/template',
    method: 'get',
    responseType: 'blob'
  })
}

