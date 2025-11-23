import {request} from '@/utils/request'
import type { TaktSchemeQuery, TaktScheme, TaktSchemeStatus, TaktSchemeUpdate, TaktSchemeCreate } from '@/types/workflow/scheme'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'

// 获取工作流定义分页列表
export function getSchemeList(query: TaktSchemeQuery) {
  return request<TaktPagedResult<TaktScheme>>({
    url: '/api/TaktScheme/list',
    method: 'get',
    params: query
  })
}

/**
 * 根据ID获取工作流方案
 */
export function getSchemeById(schemeId: string) {
  return request<TaktScheme>({
    url: `/api/TaktScheme/${schemeId}`,
    method: 'get'
  })
}

// 根据键获取工作流定义
export function getSchemeByKey(schemeKey: string) {
  return request<TaktScheme>({
    url: `/api/TaktScheme/key/${schemeKey}`,
    method: 'get'
  })
}

// 获取我的工作流定义列表
export function getMySchemes(query: TaktSchemeQuery) {
  return request<TaktPagedResult<TaktScheme>>({
    url: '/api/TaktScheme/my',
    method: 'get',
    params: query
  })
}

// 获取工作流定义选项列表
export function getSchemeOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktScheme/options',
    method: 'get'
  })
}

// 创建工作流定义
export function createScheme(data: TaktSchemeCreate) {
  return request<number>({
    url: '/api/TaktScheme',
    method: 'post',
    data
  })
}

// 更新工作流定义
export function updateScheme(data: TaktSchemeUpdate) {
  return request<boolean>({
    url: '/api/TaktScheme',
    method: 'put',
    data
  })
}

/**
 * 删除工作流方案
 */
export function deleteScheme(schemeId: string) {
  return request<boolean>({
    url: `/api/TaktScheme/${schemeId}`,
    method: 'delete'
  })
}

/**
 * 批量删除工作流方案
 */
export function batchDeleteScheme(schemeIds: string[]) {
  return request<boolean>({
    url: '/api/TaktScheme/batch',
    method: 'delete',
    data: schemeIds
  })
}

// 克隆工作流定义
export function cloneScheme(schemeId: string, newSchemeName: string) {
  return request<number>({
    url: `/api/TaktScheme/${schemeId}/clone`,
    method: 'post',
    data: newSchemeName
  })
}

// 更新工作流定义状态
export function updateSchemeStatus(data: TaktSchemeStatus) {
  return request<boolean>({
    url: `/api/TaktScheme/${data.schemeId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 获取导入模板
export function getSchemeTemplate() {
  return request<Blob>({
    url: '/api/TaktScheme/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 导入工作流定义
export function importScheme(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktScheme/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出工作流定义
export function exportScheme(query: TaktSchemeQuery) {
  return request<Blob>({
    url: '/api/TaktScheme/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取已发布的工作流定义列表
export function getPublishedSchemeList(query: TaktSchemeQuery) {
  return request<TaktPagedResult<TaktScheme>>({
    url: '/api/TaktScheme/published',
    method: 'get',
    params: query
  })
}

// 注意：以下接口在后端暂未实现，如需要请先在后端添加对应接口
// /**
//  * 复制工作流方案
//  */
// export function copyScheme(schemeId: string, newSchemeName: string) {
//   return request<TaktScheme>({
//     url: `/api/TaktScheme/${schemeId}/copy`,
//     method: 'post',
//     data: { newSchemeName }
//   })
// }

// /**
//  * 获取工作流定义版本历史
//  */
// export function getSchemeVersions(schemeKey: string) {
//   return request<TaktScheme[]>({
//     url: `/api/TaktScheme/${schemeKey}/versions`,
//     method: 'get'
//   })
// }
