import {request} from '@/utils/request'
import type { TaktOfficialDocument, TaktOfficialDocumentQuery, TaktOfficialDocumentCreate, TaktOfficialDocumentUpdate } from '@/types/routine/document/officialdoc'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取公文文档列表
 */
export function getOfficialDocumentList(params: TaktOfficialDocumentQuery) {
  return request<TaktPagedResult<TaktOfficialDocument>>({
    url: '/api/TaktOfficialDocument/list',
    method: 'get',
    params
  })
}

/**
 * 获取公文文档详情
 */
export function getOfficialDocumentById(id: number | bigint) {
  return request<TaktOfficialDocument>({
    url: `/api/TaktOfficialDocument/${id}`,
    method: 'get'
  })
}

/**
 * 创建公文文档
 */
export function createOfficialDocument(data: TaktOfficialDocumentCreate) {
  return request<number | bigint>({
    url: '/api/TaktOfficialDocument',
    method: 'post',
    data
  })
}

/**
 * 更新公文文档
 */
export function updateOfficialDocument(id: number | bigint, data: TaktOfficialDocumentUpdate) {
  return request<boolean>({
    url: `/api/TaktOfficialDocument/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除公文文档
 */
export function deleteOfficialDocument(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktOfficialDocument/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除公文文档
 */
export function batchDeleteOfficialDocument(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktOfficialDocument/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出公文文档数据
 */
export function exportOfficialDocument(params: TaktOfficialDocumentQuery, sheetName = '公文文档数据') {
  return request<Blob>({
    url: '/api/TaktOfficialDocument/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入公文文档数据
 */
export function importOfficialDocument(file: File, sheetName = '公文文档数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktOfficialDocument/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取公文文档导入模板
 */
export function getOfficialDocumentTemplate(sheetName = '公文文档数据') {
  return request<Blob>({
    url: '/api/TaktOfficialDocument/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 
