//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktDictData.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据相关接口
//===================================================================

import { request } from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  TaktDictData,
  TaktDictDataQuery,
  TaktDictDataCreate,
  TaktDictDataUpdate,
  TaktDictDataStatus
} from '@/types/routine/core/dictData'

/**
 * 获取字典数据分页列表
 * @param query 查询参数
 * @returns 字典数据分页列表
 */
export function getDictDataList(query: TaktDictDataQuery) {
  return request<TaktPagedResult<TaktDictData>>({
    url: '/api/TaktDictData/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取字典数据详情
 * @param id 字典数据ID
 * @returns 字典数据详情
 */
export function getByIdAsync(id: string) {
  return request<TaktDictData>({
    url: `/api/TaktDictData/${id}`,
    method: 'get'
  })
}

/**
 * 创建字典数据
 * @param data 创建参数
 * @returns 字典数据ID
 */
export function createDictData(data: TaktDictDataCreate) {
  return request<number>({
    url: '/api/TaktDictData',
    method: 'post',
    data
  })
}

/**
 * 更新字典数据
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateDictData(data: TaktDictDataUpdate) {
  return request<boolean>({
    url: '/api/TaktDictData',
    method: 'put',
    data
  })
}

/**
 * 删除字典数据
 * @param id 字典数据ID
 * @returns 是否成功
 */
export function deleteDictData(id: string) {
  return request<boolean>({
    url: `/api/TaktDictData/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典数据
 * @param ids 字典数据ID列表
 * @returns 是否成功
 */
export function batchDeleteDictData(ids: string[]) {
  return request<boolean>({
    url: '/api/TaktDictData/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新字典数据状态
 * @param id 字典数据ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateDictDataStatus(data: TaktDictDataStatus) {
  return request<boolean>({
    url: `/api/TaktDictData/${data.dictDataId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 根据字典类型获取字典数据
 * @param dictType 字典类型
 * @returns 字典数据列表
 */
export function getDictDataByType(dictType: string) {
  return request<TaktDictData[]>({
    url: `/api/TaktDictData/type/${dictType}`,
    method: 'get'
  })
}

/**
 * 导入字典数据
 * @param file 文件
 * @returns 导入结果
 */
export function importDictData(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ code: number; msg: string; data: { success: number; fail: number } }>({
    url: '/api/TaktDictData/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典数据
 * @param query 查询参数
 * @returns 导出文件
 */
export function exportDictData(query: TaktDictDataQuery) {
  return request<Blob>({
    url: '/api/TaktDictData/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取字典数据导入模板
 * @returns 模板文件
 */
export function getDictDataTemplate() {
  return request<Blob>({
    url: '/api/TaktDictData/template',
    method: 'get',
    responseType: 'blob'
  })
}
