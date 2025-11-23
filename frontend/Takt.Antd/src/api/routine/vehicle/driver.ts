import {request} from '@/utils/request'
import type {
  TaktDriver,
  TaktDriverQuery,
  TaktDriverCreate,
  TaktDriverUpdate,
  TaktDriverBatchDelete,
  TaktDriverImport,
  TaktDriverExport,
  TaktDriverTemplate,
  TaktDriverStatistics
} from '@/types/routine/vehicle/driver'
import type { TaktPagedResult } from '@/types/common'

/**
 * 获取驾驶员列表
 */
export function getDriverList(params: TaktDriverQuery) {
  return request<TaktPagedResult<TaktDriver>>({
    url: '/api/TaktDriver/list',
    method: 'get',
    params
  })
}

/**
 * 获取驾驶员详情
 */
export function getDriverById(id: number) {
  return request<TaktDriver>({
    url: `/api/TaktDriver/${id}`,
    method: 'get'
  })
}

/**
 * 创建驾驶员
 */
export function createDriver(data: TaktDriverCreate) {
  return request<number>({
    url: '/api/TaktDriver',
    method: 'post',
    data
  })
}

/**
 * 更新驾驶员
 */
export function updateDriver(data: TaktDriverUpdate) {
  return request<boolean>({
    url: '/api/TaktDriver',
    method: 'put',
    data
  })
}

/**
 * 删除驾驶员
 */
export function deleteDriver(id: number) {
  return request<boolean>({
    url: `/api/TaktDriver/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除驾驶员
 */
export function batchDeleteDriver(data: TaktDriverBatchDelete) {
  return request<boolean>({
    url: '/api/TaktDriver/batch',
    method: 'delete',
    data
  })
}

/**
 * 导入驾驶员数据
 */
export function importDriver(file: File, sheetName = '驾驶员信息') {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('sheetName', sheetName)

  return request<boolean>({
    url: '/api/TaktDriver/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出驾驶员数据
 */
export function exportDriver(params: TaktDriverQuery, sheetName = '驾驶员信息') {
  return request<Blob>({
    url: '/api/TaktDriver/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getDriverTemplate(sheetName = '驾驶员信息') {
  return request<Blob>({
    url: '/api/TaktDriver/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取驾驶员统计信息
 */
export function getDriverStatistics() {
  return request<TaktDriverStatistics>({
    url: '/api/TaktDriver/statistics',
    method: 'get'
  })
} 
