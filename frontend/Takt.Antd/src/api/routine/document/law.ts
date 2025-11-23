import {request} from '@/utils/request'
import type { TaktLaw, TaktLawQuery, TaktLawCreate, TaktLawUpdate } from '@/types/routine/document/law'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取法律法规列表
 */
export function getLawList(params: TaktLawQuery) {
  return request<TaktPagedResult<TaktLaw>>({
    url: '/api/TaktLaw/list',
    method: 'get',
    params
  })
}

/**
 * 获取法律法规详情
 */
export function getLawById(id: number | bigint) {
  return request<TaktLaw>({
    url: `/api/TaktLaw/${id}`,
    method: 'get'
  })
}

/**
 * 创建法律法规
 */
export function createLaw(data: TaktLawCreate) {
  return request<number | bigint>({
    url: '/api/TaktLaw',
    method: 'post',
    data
  })
}

/**
 * 更新法律法规
 */
export function updateLaw(id: number | bigint, data: TaktLawUpdate) {
  return request<boolean>({
    url: `/api/TaktLaw/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除法律法规
 */
export function deleteLaw(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktLaw/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除法律法规
 */
export function batchDeleteLaw(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktLaw/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出法律法规数据
 */
export function exportLaw(params: TaktLawQuery, sheetName = '法律法规数据') {
  return request<Blob>({
    url: '/api/TaktLaw/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入法律法规数据
 */
export function importLaw(file: File, sheetName = '法律法规数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktLaw/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取法律法规导入模板
 */
export function getLawTemplate(sheetName = '法律法规数据') {
  return request<Blob>({
    url: '/api/TaktLaw/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 
