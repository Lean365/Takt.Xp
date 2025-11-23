import {request} from '@/utils/request'
import type { TaktRegulation, TaktRegulationQuery, TaktRegulationCreate, TaktRegulationUpdate } from '@/types/routine/document/regulation'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取内部规章制度列表
 */
export function getRegulationList(params: TaktRegulationQuery) {
  return request<TaktPagedResult<TaktRegulation>>({
    url: '/api/TaktRegulation/list',
    method: 'get',
    params
  })
}

/**
 * 获取内部规章制度详情
 */
export function getRegulationById(id: number | bigint) {
  return request<TaktRegulation>({
    url: `/api/TaktRegulation/${id}`,
    method: 'get'
  })
}

/**
 * 创建内部规章制度
 */
export function createRegulation(data: TaktRegulationCreate) {
  return request<number | bigint>({
    url: '/api/TaktRegulation',
    method: 'post',
    data
  })
}

/**
 * 更新内部规章制度
 */
export function updateRegulation(id: number | bigint, data: TaktRegulationUpdate) {
  return request<boolean>({
    url: `/api/TaktRegulation/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除内部规章制度
 */
export function deleteRegulation(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktRegulation/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除内部规章制度
 */
export function batchDeleteRegulation(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktRegulation/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出内部规章制度数据
 */
export function exportRegulation(params: TaktRegulationQuery, sheetName = '内部规章制度数据') {
  return request<Blob>({
    url: '/api/TaktRegulation/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入内部规章制度数据
 */
export function importRegulation(file: File, sheetName = '内部规章制度数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktRegulation/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取内部规章制度导入模板
 */
export function getRegulationTemplate(sheetName = '内部规章制度数据') {
  return request<Blob>({
    url: '/api/TaktRegulation/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 
