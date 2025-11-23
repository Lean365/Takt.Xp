import {request} from '@/utils/request'
import type { TaktFile, TaktFileQuery, TaktFileCreate, TaktFileUpdate } from '@/types/routine/document/file'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取文件列表
 * @param params 查询参数
 * @returns 文件列表
 */
export function getFileList(params: TaktFileQuery) {
  return request<TaktPagedResult<TaktFile>>({
    url: '/api/TaktFile/list',
    method: 'get',
    params
  })
}

/**
 * 获取文件详情
 * @param id 文件ID
 * @returns 文件详情
 */
export function getFileDetail(id: number | bigint) {
  return request<TaktFile>({
    url: `/api/TaktFile/${id}`,
    method: 'get'
  })
}

/**
 * 创建文件
 * @param data 文件数据
 * @returns 创建结果
 */
export function createFile(data: TaktFileCreate) {
  return request<TaktFile>({
    url: '/api/TaktFile',
    method: 'post',
    data
  })
}

/**
 * 更新文件
 * @param id 文件ID
 * @param data 文件数据
 * @returns 更新结果
 */
export function updateFile(id: number | bigint, data: TaktFileUpdate) {
  return request<TaktFile>({
    url: `/api/TaktFile/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除文件
 * @param id 文件ID
 * @returns 删除结果
 */
export function deleteFile(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktFile/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除文件
 * @param ids 文件ID列表
 * @returns 删除结果
 */
export function batchDeleteFile(ids: number[] | bigint[]) {
  return request<boolean>({
    url: '/api/TaktFile/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出文件列表
 * @param params 查询参数
 * @returns 导出结果
 */
export function exportFileList(params: TaktFileQuery) {
  return request<Blob>({
    url: '/api/TaktFile/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入文件列表
 * @param file 文件
 * @returns 导入结果
 */
export function importFileList(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<boolean>({
    url: '/api/TaktFile/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 下载文件
 * @param id 文件ID
 * @returns 下载结果
 */
export function downloadFile(id: number | bigint) {
  return request<Blob>({
    url: `/api/TaktFile/download/${id}`,
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 上传文件
 * @param file 文件
 * @returns 上传结果
 */
export function uploadFile(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<TaktFile>({
    url: '/api/TaktFile/upload',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 上传头像
 * @param file 头像文件
 * @returns 上传结果
 */
export function uploadAvatar(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{
    url: string
    fileName: string
    originalName: string
    fileSize: number
  }>({
    url: '/api/TaktFile/upload-avatar',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
} 
