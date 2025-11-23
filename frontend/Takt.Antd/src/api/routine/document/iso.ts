import {request} from '@/utils/request'
import type { TaktIsoDocument, TaktIsoDocumentQuery, TaktIsoDocumentCreate, TaktIsoDocumentUpdate } from '@/types/routine/document/iso'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取ISO标准文档列表
 */
export function getIsoDocumentList(params: TaktIsoDocumentQuery) {
  return request<TaktPagedResult<TaktIsoDocument>>({
    url: '/api/TaktIsoDocument/list',
    method: 'get',
    params
  })
}

/**
 * 获取ISO标准文档详情
 */
export function getIsoDocumentById(id: number | bigint) {
  return request<TaktIsoDocument>({
    url: `/api/TaktIsoDocument/${id}`,
    method: 'get'
  })
}

/**
 * 创建ISO标准文档
 */
export function createIsoDocument(data: TaktIsoDocumentCreate) {
  return request<number | bigint>({
    url: '/api/TaktIsoDocument',
    method: 'post',
    data
  })
}

/**
 * 更新ISO标准文档
 */
export function updateIsoDocument(id: number | bigint, data: TaktIsoDocumentUpdate) {
  return request<boolean>({
    url: `/api/TaktIsoDocument/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ISO标准文档
 */
export function deleteIsoDocument(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktIsoDocument/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ISO标准文档
 */
export function batchDeleteIsoDocument(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktIsoDocument/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出ISO标准文档数据
 */
export function exportIsoDocument(params: TaktIsoDocumentQuery, sheetName = 'ISO标准文档数据') {
  return request<Blob>({
    url: '/api/TaktIsoDocument/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 导入ISO标准文档数据
 */
export function importIsoDocument(file: File, sheetName = 'ISO标准文档数据') {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktIsoDocument/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取ISO标准文档导入模板
 */
export function getIsoDocumentTemplate(sheetName = 'ISO标准文档数据') {
  return request<Blob>({
    url: '/api/TaktIsoDocument/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
} 
