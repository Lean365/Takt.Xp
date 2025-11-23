import {request} from '@/utils/request'
import type { TaktSchedule, TaktScheduleQuery, TaktScheduleCreate, TaktScheduleUpdate } from '@/types/routine/schedule/schedule'
import type { TaktPagedResult } from '@/types/common'

/**
 * 获取日程分页列表
 */
export function getScheduleList(params: TaktScheduleQuery) {
  return request<TaktPagedResult<TaktSchedule>>({
    url: '/api/TaktSchedule/list',
    method: 'get',
    params
  })
}

/**
 * 获取日程详情
 */
export function getScheduleById(scheduleId: number | bigint) {
  return request<TaktSchedule>({
    url: `/api/TaktSchedule/${scheduleId}`,
    method: 'get'
  })
}

/**
 * 创建日程
 */
export function createSchedule(data: TaktScheduleCreate) {
  return request<number>({
    url: '/api/TaktSchedule',
    method: 'post',
    data
  })
}

/**
 * 更新日程
 */
export function updateSchedule(data: TaktScheduleUpdate) {
  return request<boolean>({
    url: '/api/TaktSchedule',
    method: 'put',
    data
  })
}

/**
 * 删除日程
 */
export function deleteSchedule(scheduleId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktSchedule/${scheduleId}`,
    method: 'delete'
  })
}

/**
 * 批量删除日程
 */
export function batchDeleteSchedule(scheduleIds: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktSchedule/batch',
    method: 'delete',
    data: scheduleIds
  })
}

/**
 * 导入日程数据
 */
export function importSchedule(file: File, sheetName = '日程信息') {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktSchedule/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出日程数据
 */
export function exportSchedule(params: TaktScheduleQuery, sheetName = '日程信息') {
  return request<Blob>({
    url: '/api/TaktSchedule/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取日程导入模板
 */
export function getScheduleTemplate(sheetName = '日程信息') {
  return request<Blob>({
    url: '/api/TaktSchedule/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

/**
 * 获取我的日程列表
 */
export function getMyScheduleList(params: TaktScheduleQuery) {
  return request<TaktPagedResult<TaktSchedule>>({
    url: '/api/TaktSchedule/my',
    method: 'get',
    params
  })
}

/**
 * 获取团队日程列表
 */
export function getTeamScheduleList(params: TaktScheduleQuery) {
  return request<TaktPagedResult<TaktSchedule>>({
    url: '/api/TaktSchedule/team',
    method: 'get',
    params
  })
}

/**
 * 获取我的日程统计
 */
export function getMyScheduleStats() {
  return request<{
    total: number
    today: number
    thisWeek: number
    thisMonth: number
    pending: number
    completed: number
  }>({
    url: '/api/TaktSchedule/my/stats',
    method: 'get'
  })
}

/**
 * 获取团队日程统计
 */
export function getTeamScheduleStats() {
  return request<{
    total: number
    today: number
    thisWeek: number
    thisMonth: number
    pending: number
    completed: number
  }>({
    url: '/api/TaktSchedule/team/stats',
    method: 'get'
  })
}

/**
 * 更新日程状态
 */
export function updateScheduleStatus(scheduleId: number, status: number) {
  return request<boolean>({
    url: `/api/TaktSchedule/${scheduleId}/status`,
    method: 'put',
    data: { status }
  })
}

/**
 * 批量更新日程状态
 */
export function batchUpdateScheduleStatus(scheduleIds: number[], status: number) {
  return request<boolean>({
    url: '/api/TaktSchedule/batch/status',
    method: 'put',
    data: { scheduleIds, status }
  })
} 
