//===================================================================
// 项目名 : Lean.Takt
// 文件名 : generalSettings.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用设置API
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  TaktGeneralSettings,
  TaktGeneralSettingsQuery,
  TaktGeneralSettingsCreate,
  TaktGeneralSettingsUpdate,
  TaktGeneralSettingsStatusUpdate} from '@/types/routine/core/generalSettings'

/**
 * 获取设置分页列表
 * @param query 查询参数
 * @returns 设置分页列表
 */
export function getGeneralSettingsList(query: TaktGeneralSettingsQuery) {
  return request<TaktPagedResult<TaktGeneralSettings>>({
    url: '/api/TaktGeneralSettings/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取设置详情
 * @param settingsId 设置ID
 * @returns 设置详情
 */
export function getByIdAsync(settingsId: string) {
  return request<TaktGeneralSettings>({
    url: `/api/TaktGeneralSettings/${settingsId}`,
    method: 'get'
  })
}

/**
 * 创建设置
 * @param data 创建参数
 * @returns 设置ID
 */
export function createGeneralSettings(data: TaktGeneralSettingsCreate) {
  return request<number>({
    url: '/api/TaktGeneralSettings',
    method: 'post',
    data
  })
}

/**
 * 更新设置
 * @param data 更新参数
 * @returns 是否成功
 */
export function updateGeneralSettings(data: TaktGeneralSettingsUpdate) {
  return request<boolean>({
    url: '/api/TaktGeneralSettings',
    method: 'put',
    data,
    headers: {
      'Content-Type': 'application/json'
    }
  })
}

/**
 * 删除设置
 * @param settingsId 设置ID
 * @returns 是否成功
 */
export function deleteGeneralSettings(settingsId: string) {
  return request<boolean>({
    url: `/api/TaktGeneralSettings/${settingsId}`,
    method: 'delete'
  })
}

/**
 * 批量删除设置
 * @param settingsIds 设置ID列表
 * @returns 是否成功
 */
export function batchDeleteGeneralSettings(settingsIds: string[]) {
  return request<boolean>({
    url: '/api/TaktGeneralSettings/batch',
    method: 'delete',
    data: settingsIds
  })
}

/**
 * 更新设置状态
 * @param settingsId 设置ID
 * @param status 状态
 * @returns 是否成功
 */
export function updateStatus(data: TaktGeneralSettingsStatusUpdate) {
  return request<boolean>({
    url: `/api/TaktGeneralSettings/${data.settingsId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 导出设置
 * @param query 查询参数
 * @returns 文件流
 */
export function exportGeneralSettings(query: TaktGeneralSettingsQuery) {
  return request<Blob>({
    url: '/api/TaktGeneralSettings/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取导入模板
 * @returns 文件流
 */
export function getGeneralSettingsTemplate() {
  return request<Blob>({
    url: '/api/TaktGeneralSettings/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 导入系统设置
 * @param file 文件对象
 * @returns 导入结果
 */
export function importGeneralSettings(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<boolean>({
    url: '/api/TaktGeneralSettings/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取设置值
 * @param settingsKey 设置键
 * @returns 设置值
 */
export function getGeneralSettingsValue(settingsKey: string) {
  return request<string>({
    url: `/api/TaktGeneralSettings/value/${settingsKey}`,
    method: 'get'
  })
}
