import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { 
  TaktDeviceLog, 
  TaktDeviceLogQuery
} from '@/types/audit/deviceLog'

// 获取设备日志分页列表
export function getDeviceLogList(query: TaktDeviceLogQuery) {
  return request<TaktPagedResult<TaktDeviceLog>>({
    url: '/api/TaktDeviceLog/list',
    method: 'get',
    params: query
  })
}

// 获取设备日志详情
export function getDeviceLog(id: string) {
  return request<TaktDeviceLog>({
    url: `/api/TaktDeviceLog/${id}`,
    method: 'get'
  })
}

// 删除设备日志
export function deleteDeviceLog(id: string) {
  return request<boolean>({
    url: `/api/TaktDeviceLog/${id}`,
    method: 'delete'
  })
}

// 批量删除设备日志
export function batchDeleteDeviceLog(ids: string[]) {
  return request<boolean>({
    url: '/api/TaktDeviceLog/batch',
    method: 'delete',
    data: ids
  })
}

// 记录设备登录日志
export function recordDeviceLogin(input: TaktDeviceLog) {
  return request<boolean>({
    url: '/api/TaktDeviceLog/record-login',
    method: 'post',
    data: input
  })
}

// 更新设备在线状态
export function updateDeviceOnlineStatus(deviceId: string, isOnline: boolean) {
  return request<boolean>({
    url: `/api/TaktDeviceLog/${deviceId}/online-status`,
    method: 'put',
    params: { isOnline }
  })
}

// 获取用户在线设备数量
export function getOnlineDeviceCount(userId: string) {
  return request<number>({
    url: `/api/TaktDeviceLog/online-count/${userId}`,
    method: 'get'
  })
}

// 更新今日在线时段
export function updateTodayOnlinePeriods(deviceId: string, onlinePeriods: string[]) {
  return request<boolean>({
    url: `/api/TaktDeviceLog/${deviceId}/online-periods`,
    method: 'put',
    data: onlinePeriods
  })
}

// 更新连续登录天数
export function updateContinuousLoginDays(deviceId: string, continuousDays: number) {
  return request<boolean>({
    url: `/api/TaktDeviceLog/${deviceId}/continuous-days`,
    method: 'put',
    params: { continuousDays }
  })
}

// 更新登录次数
export function updateLoginCount(deviceId: string, loginCount: number) {
  return request<boolean>({
    url: `/api/TaktDeviceLog/${deviceId}/login-count`,
    method: 'put',
    params: { loginCount }
  })
}

// 清空设备日志数据
export function clearDeviceLog(query: TaktDeviceLogQuery) {
  return request<boolean>({
    url: '/api/TaktDeviceLog/clear',
    method: 'delete',
    params: query
  })
}

// 导出设备日志数据
export function exportDeviceLog(query: TaktDeviceLogQuery) {
  return request<Blob>({
    url: '/api/TaktDeviceLog/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

