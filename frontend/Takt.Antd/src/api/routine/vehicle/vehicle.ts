import {request} from '@/utils/request'
import type { 
  TaktVehicle, 
  TaktVehicleQuery, 
  TaktVehicleCreate, 
  TaktVehicleUpdate,
  TaktVehicleBatchDelete,
  TaktVehicleImport,
  TaktVehicleExport,
  TaktVehicleTemplate,
  TaktVehicleStatistics
} from '@/types/routine/vehicle/vehicle'
import type { TaktPagedResult } from '@/types/common'

/**
 * 获取用车列表
 */
export function getVehicleList(params: TaktVehicleQuery) {
  return request<TaktPagedResult<TaktVehicle>>({
    url: '/api/TaktVehicle/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取车辆详情
 */
export function getVehicleById(id: string) {
  return request<TaktVehicle>({
    url: `/api/TaktVehicle/${id}`,
    method: 'get'
  })
}

/**
 * 创建用车
 */
export function createVehicle(data: TaktVehicleCreate) {
  return request<number>({
    url: '/api/TaktVehicle',
    method: 'post',
    data
  })
}

/**
 * 更新用车
 */
export function updateVehicle(data: TaktVehicleUpdate) {
  return request<boolean>({
    url: '/api/TaktVehicle',
    method: 'put',
    data
  })
}

/**
 * 删除车辆
 */
export function deleteVehicle(id: string) {
  return request<boolean>({
    url: `/api/TaktVehicle/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除用车
 */
export function batchDeleteVehicle(data: TaktVehicleBatchDelete) {
  return request<boolean>({
    url: '/api/TaktVehicle/batch',
    method: 'delete',
    data
  })
}

/**
 * 导入用车数据
 */
export function importVehicle(file: File, sheetName = '用车信息') {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('sheetName', sheetName)
  
  return request<boolean>({
    url: '/api/TaktVehicle/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出用车数据
 */
export function exportVehicle(params: TaktVehicleQuery, sheetName = '用车信息') {
  return request<Blob>({
    url: '/api/TaktVehicle/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 */
export function getVehicleTemplate(sheetName = '用车信息') {
  return request<Blob>({
    url: '/api/TaktVehicle/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取用车统计信息
 */
export function getVehicleStatistics() {
  return request<TaktVehicleStatistics>({
    url: '/api/TaktVehicle/statistics',
    method: 'get'
  })
} 
