import {request} from '@/utils/request'
import type { TaktStandardTime, TaktStandardTimeQuery, TaktStandardTimeCreate, TaktStandardTimeUpdate, TaktStandardTimeStatus } from '@/types/logistics/manufacturing/master/standardTime'
import type { TaktPagedResult, TaktApiResponse, TaktSelectOption } from '@/types/common'

/**
 * 获取标准工时分页列表
 */
export function getStandardTimeList(params: TaktStandardTimeQuery) {
  return request<TaktPagedResult<TaktStandardTime>>({
    url: '/api/TaktStandardTime/list',
    method: 'get',
    params
  })
}

/**
 * 获取标准工时详情
 */
export function getStandardTimeById(standardTimeId: number | bigint) {
  return request<TaktStandardTime>({
    url: `/api/TaktStandardTime/${standardTimeId}`,
    method: 'get'
  })
}

/**
 * 创建标准工时
 */
export function createStandardTime(data: TaktStandardTimeCreate) {
  return request<number>({
    url: '/api/TaktStandardTime',
    method: 'post',
    data
  })
}

/**
 * 更新标准工时
 */
export function updateStandardTime(data: TaktStandardTimeUpdate) {
  return request<boolean>({
    url: '/api/TaktStandardTime',
    method: 'put',
    data
  })
}

/**
 * 删除标准工时
 */
export function deleteStandardTime(standardTimeId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktStandardTime/${standardTimeId}`,
    method: 'delete'
  })
}

/**
 * 批量删除标准工时
 */
export function batchDeleteStandardTime(standardTimeIds: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktStandardTime/batch',
    method: 'delete',
    data: standardTimeIds
  })
}

/**
 * 更新标准工时状态
 */
export function updateStandardTimeStatus(data: TaktStandardTimeStatus) {
  return request<boolean>({
    url: '/api/TaktStandardTime/status',
    method: 'put',
    data
  })
}

/**
 * 获取标准工时选项列表
 */
export function getStandardTimeOptions(materialCode?: string) {
  return request<TaktSelectOption[]>({
    url: '/api/TaktStandardTime/options',
    method: 'get',
    params: { materialCode }
  })
}

/**
 * 导入标准工时数据
 */
export function importStandardTime(file: File, updateSupport: boolean = false) {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('updateSupport', updateSupport.toString())
  
  return request<{ success: number; fail: number }>({
    url: '/api/TaktStandardTime/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出标准工时数据
 */
export function exportStandardTime(params: TaktStandardTimeQuery) {
  return request({
    url: '/api/TaktStandardTime/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取标准工时导入模板
 */
export function getStandardTimeTemplate() {
  return request({
    url: '/api/TaktStandardTime/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 审核标准工时
 */
export function approveStandardTime(standardTimeId: number | bigint, approver: string) {
  return request<boolean>({
    url: `/api/TaktStandardTime/${standardTimeId}/approve`,
    method: 'put',
    data: approver
  })
}

/**
 * 计算转换后标准工时
 */
export function calculateConvertedMinutes(standardMinutes: number, standardShorts: number, pointsToMinutesRate: number) {
  return request<number>({
    url: '/api/TaktStandardTime/calculate',
    method: 'get',
    params: { standardMinutes, standardShorts, pointsToMinutesRate }
  })
}

/**
 * 获取标准工时变更记录
 */
export function getStandardTimeChangeLogs(standardTimeId: number | bigint) {
  return request<any[]>({
    url: `/api/TaktStandardTime/${standardTimeId}/changelogs`,
    method: 'get'
  })
}

