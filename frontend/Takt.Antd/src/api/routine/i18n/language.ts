//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktLanguage.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言相关接口
//===================================================================

import { request } from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  TaktLanguage,
  TaktLanguageQuery,
  TaktLanguageCreate,
  TaktLanguageUpdate,
  TaktLanguageStatus
} from '@/types/routine/i18n/language'
import type { TaktSelectOption } from '@/types/common'

/**
 * 获取语言分页列表
 * @param query 查询参数
 * @returns 语言分页列表
 */
export function getLanguageList(query: TaktLanguageQuery) {
  return request<TaktPagedResult<TaktLanguage>>({
    url: '/api/TaktLanguage/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取语言详情
 * @param languageId 语言ID
 * @returns 语言详情
 */
export function getByIdAsync(languageId: string) {
  return request<TaktLanguage>({
    url: `/api/TaktLanguage/${languageId}`,
    method: 'get'
  })
}

/**
 * 创建语言
 * @param data 创建参数
 * @returns 语言ID
 */
export function createLanguage(data: TaktLanguageCreate) {
  return request<number>({
    url: '/api/TaktLanguage',
    method: 'post',
    data
  })
}

/**
 * 更新语言
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateLanguage(data: TaktLanguageUpdate) {
  return request<boolean>({
    url: '/api/TaktLanguage',
    method: 'put',
    data
  })
}

/**
 * 删除语言
 * @param languageId 语言ID
 * @returns 是否成功
 */
export function deleteLanguage(languageId: string) {
  return request<boolean>({
    url: `/api/TaktLanguage/${languageId}`,
    method: 'delete'
  })
}

/**
 * 批量删除语言
 * @param languageIds 语言ID列表
 * @returns 是否成功
 */
export function batchDeleteLanguage(languageIds: string[]) {
  return request<boolean>({
    url: '/api/TaktLanguage/batch',
    method: 'delete',
    data: languageIds
  })
}

/**
 * 更新语言状态
 * @param languageId 语言ID
 * @param status 状态(0=禁用,1=启用)
 * @returns 是否成功
 */
export function updateLanguageStatus(data: TaktLanguageStatus) {
  return request<boolean>({
    url: `/api/TaktLanguage/${data.languageId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 获取语言选项列表
 * @returns 语言选项列表
 */
export function getLanguageOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktLanguage/options',
    method: 'get'
  })
}

/**
 * 获取所有语言列表
 * @returns 所有语言列表
 */
export function getAllLanguages() {
  return request<TaktLanguage[]>({
    url: '/api/TaktLanguage/all',
    method: 'get'
  })
}

/**
 * 导入语言数据
 * @param file 文件
 * @returns 导入结果
 */
export function importLanguage(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ code: number; msg: string; data: { success: number; fail: number } }>({
    url: '/api/TaktLanguage/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出语言数据
 * @param query 查询参数
 * @returns 导出文件
 */
export function exportLanguage(query: TaktLanguageQuery) {
  return request<Blob>({
    url: '/api/TaktLanguage/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取语言导入模板
 * @returns 模板文件
 */
export function getLanguageTemplate() {
  return request<Blob>({
    url: '/api/TaktLanguage/template',
    method: 'get',
    responseType: 'blob'
  })
}
