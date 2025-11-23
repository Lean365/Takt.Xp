//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktDictType.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型相关接口
//===================================================================

import { request } from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  TaktDictType,
  TaktDictTypeQuery,
  TaktDictTypeCreate,
  TaktDictTypeUpdate,
  TaktDictTypeStatus
} from '@/types/routine/core/dictType'

/**
 * 获取字典类型分页列表
 * @param query 查询参数
 * @returns 字典类型分页列表
 */
export function getDictTypeList(query: TaktDictTypeQuery) {
  return request<TaktPagedResult<TaktDictType>>({
    url: '/api/TaktDictType/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取字典类型详情
 * @param id 字典类型ID
 * @returns 字典类型详情
 */
export function getByIdAsync(id: string) {
  return request<TaktDictType>({
    url: `/api/TaktDictType/${id}`,
    method: 'get'
  })
}

/**
 * 创建字典类型
 * @param data 创建参数
 * @returns 字典类型ID
 */
export function createDictType(data: TaktDictTypeCreate) {
  return request<number>({
    url: '/api/TaktDictType',
    method: 'post',
    data
  })
}

/**
 * 更新字典类型
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateDictType(data: TaktDictTypeUpdate) {
  return request<boolean>({
    url: '/api/TaktDictType',
    method: 'put',
    data
  })
}

/**
 * 删除字典类型
 * @param id 字典类型ID
 * @returns 是否成功
 */
export function deleteDictType(id: string) {
  return request<boolean>({
    url: `/api/TaktDictType/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典类型
 * @param ids 字典类型ID列表
 * @returns 是否成功
 */
export function batchDeleteDictType(ids: string[]) {
  return request<boolean>({
    url: '/api/TaktDictType/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新字典类型状态
 * @param id 字典类型ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateDictTypeStatus(data: TaktDictTypeStatus) {
  return request<boolean>({
    url: `/api/TaktDictType/${data.dictTypeId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 获取所有字典类型列表
 * @returns 所有字典类型列表
 */
export function getAllDictTypes() {
  return request<TaktDictType[]>({
    url: '/api/TaktDictType/all',
    method: 'get'
  })
}

/**
 * 导入字典类型数据
 * @param file 文件
 * @returns 导入结果
 */
export function importDictType(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ code: number; msg: string; data: { success: number; fail: number } }>({
    url: '/api/TaktDictType/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典类型数据
 * @param query 查询参数
 * @returns 导出文件
 */
export function exportDictType(query: TaktDictTypeQuery) {
  return request<Blob>({
    url: '/api/TaktDictType/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取字典类型导入模板
 * @returns 模板文件
 */
export function getDictTypeTemplate() {
  return request<Blob>({
    url: '/api/TaktDictType/template',
    method: 'get',
    responseType: 'blob'
  })
}
