import {request} from '@/utils/request'
import type { TaktSignalROnlineQuery, TaktSignalROnline } from '@/types/signalr/signalROnline'
import type { TaktPagedResult } from '@/types/common'

/** 获取在线用户列表 */
export function getSignalROnlineList(params: TaktSignalROnlineQuery) {
  return request<TaktPagedResult<TaktSignalROnline>>({
    url: '/api/TaktSignalROnline/list',
    method: 'get',
    params
  })
}

/** 获取用户连接ID列表 */
export function getSignalROnlineConnectionIds(userId: string) {
  return request<string[]>({
    url: `/api/TaktSignalROnline/connection-ids/${userId}`,
    method: 'get'
  })
}

/** 根据条件获取在线用户信息 */
export function getSignalROnlineFirst(predicate: any) {
  return request<TaktSignalROnline>({
    url: '/api/TaktSignalROnline/first',
    method: 'post',
    data: predicate
  })
}

/** 获取所有在线用户 */
export function getSignalROnlineAll() {
  return request<TaktSignalROnline[]>({
    url: '/api/TaktSignalROnline/all',
    method: 'get'
  })
}

/** 保存在线用户 */
export function saveSignalROnline(data: TaktSignalROnline) {
  return request<boolean>({
    url: '/api/TaktSignalROnline/save',
    method: 'post',
    data
  })
}

/** 更新在线用户信息 */
export function updateSignalROnline(data: TaktSignalROnline) {
  return request<boolean>({
    url: '/api/TaktSignalROnline/update',
    method: 'put',
    data
  })
}

/** 删除在线用户 */
export function deleteSignalROnline(id: string) {
  return request<boolean>({
    url: `/api/TaktSignalROnline/delete/${id}`,
    method: 'delete'
  })
}

/** 强制用户下线 */
export function forceSignalROnline(deviceId: string) {
  return request<boolean>({
    url: `/api/TaktSignalROnline/force-offline/${deviceId}`,
    method: 'post'
  })
}

/** 更新用户心跳 */
export function updateSignalROnlineHeartbeat() {
  return request<string>({
    url: '/api/TaktSignalROnline/heartbeat',
    method: 'post'
  })
}

/** 清理过期用户 */
export function cleanupExpiredSignalROnline(minutes: number = 20) {
  return request<number>({
    url: '/api/TaktSignalROnline/cleanup',
    method: 'post',
    params: { minutes }
  })
}

/** 导出在线用户数据 */
export function exportSignalROnline(params: TaktSignalROnlineQuery, sheetName: string) {
  return request<Blob>({
    url: '/api/TaktSignalROnline/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
}
