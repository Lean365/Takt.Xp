//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktTranslation.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译相关接口
//===================================================================

import { request } from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  TaktTranslation,
  TaktTranslationQuery,
  TaktTranslationCreate,
  TaktTranslationUpdate,
  TaktTranslationStatus,
  TaktTransposedData
} from '@/types/routine/i18n/translation'

/**
 * 获取翻译分页列表
 * @param query 查询参数
 * @returns 翻译分页列表
 */
export function getTranslationList(query: TaktTranslationQuery) {
  return request<TaktPagedResult<TaktTranslation>>({
    url: '/api/TaktTranslation/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取翻译详情
 * @param transId 翻译ID
 * @returns 翻译详情
 */
export function getByIdAsync(transId: string) {
  return request<TaktTranslation>({
    url: `/api/TaktTranslation/${transId}`,
    method: 'get'
  })
}

/**
 * 创建翻译
 * @param data 创建参数
 * @returns 翻译ID
 */
export function createTranslation(data: TaktTranslationCreate) {
  return request<number>({
    url: '/api/TaktTranslation',
    method: 'post',
    data
  })
}

/**
 * 更新翻译
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateTranslation(data: TaktTranslationUpdate) {
  return request<boolean>({
    url: '/api/TaktTranslation',
    method: 'put',
    data
  })
}

/**
 * 删除翻译
 * @param transId 翻译ID
 * @returns 是否成功
 */
export function deleteTranslation(transId: string) {
  return request<boolean>({
    url: `/api/TaktTranslation/${transId}`,
    method: 'delete'
  })
}

/**
 * 批量删除翻译
 * @param transIds 翻译ID列表
 * @returns 是否成功
 */
export function batchDeleteTranslation(transIds: string[]) {
  return request<boolean>({
    url: '/api/TaktTranslation/batch',
    method: 'delete',
    data: transIds
  })
}

/**
 * 更新翻译状态
 * @param translationId 翻译ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateTranslationStatus(data: TaktTranslationStatus) {
  return request<boolean>({
    url: `/api/TaktTranslation/${data.translationId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 获取翻译值
 * @param langCode 语言代码
 * @param transKey 翻译键
 * @returns 翻译值
 */
export function getTranslationValue(langCode: string, transKey: string) {
  return request<string>({
    url: '/api/TaktTranslation/value',
    method: 'get',
    params: { langCode, transKey }
  })
}

/**
 * 根据模块获取翻译
 * @param langCode 语言代码
 * @param transType 翻译类型
 * @returns 翻译列表
 */
export function getListByModuleAsync(langCode: string, transType: number = 0) {
  return request<TaktTranslation[]>({
    url: '/api/TaktTranslation/module',
    method: 'get',
    params: { langCode, transType }
  })
}

/**
 * 获取转置翻译数据列表
 * @param query 查询参数
 * @returns 转置翻译数据列表
 */
export function getTransposedData(query: any) {
  return request<TaktPagedResult<TaktTransposedData>>({
    url: '/api/TaktTranslation/transposed',
    method: 'get',
    params: query
  })
}

/**
 * 获取转置翻译详情
 * @param transKey 翻译键
 * @returns 转置翻译详情
 */
export function getTransposedDetail(transKey: string) {
  return request<TaktTransposedData>({
    url: `/api/TaktTranslation/transposed/${transKey}`,
    method: 'get'
  })
}

/**
 * 导入翻译数据
 * @param file 文件
 * @returns 导入结果
 */
export function importTranslation(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ code: number; msg: string; data: { success: number; fail: number } }>({
    url: '/api/TaktTranslation/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出翻译数据
 * @param query 查询参数
 * @returns 导出文件
 */
export function exportTranslation(query: TaktTranslationQuery) {
  return request<Blob>({
    url: '/api/TaktTranslation/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取翻译导入模板
 * @returns 模板文件
 */
export function getTranslationTemplate() {
  return request<Blob>({
    url: '/api/TaktTranslation/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 创建转置翻译数据
 * @param data 转置数据创建参数
 * @returns 是否成功
 */
export function createTransposedTranslation(data: TaktTransposedData) {
  return request<boolean>({
    url: '/api/TaktTranslation/transposed',
    method: 'post',
    data
  })
}

/**
 * 更新转置翻译数据
 * @param data 转置数据更新参数
 * @returns 是否成功
 */
export function updateTransposedTranslation(data: TaktTransposedData) {
  return request<boolean>({
    url: '/api/TaktTranslation/transposed',
    method: 'put',
    data
  })
}
