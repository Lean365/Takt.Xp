import {request} from '@/utils/request'
import type { TaktSignalRMessageQueryParams, TaktSignalRMessage } from '@/types/signalr/signalRMessage'
import type { TaktPagedResult } from '@/types/common'

/** 获取在线消息列表 */
export function getSignalRMessageList(params: TaktSignalRMessageQueryParams) {
  return request<TaktPagedResult<TaktSignalRMessage>>({
    url: '/api/TaktSignalRMessage/list',
    method: 'get',
    params
  })
}

/** 获取消息详情 */
export function getSignalRMessage(id: string) {
  return request<TaktSignalRMessage>({
    url: `/api/TaktSignalRMessage/${id}`,
    method: 'get'
  })
}

/** 保存消息 */
export function saveSignalRMessage(data: TaktSignalRMessage) {
  return request<number>({
    url: '/api/TaktSignalRMessage',
    method: 'post',
    data
  })
}

/** 删除在线消息 */
export function deleteSignalRMessage(id: string) {
  return request<boolean>({
    url: `/api/TaktSignalRMessage/${id}`,
    method: 'delete'
  })
}

/** 标记消息为已读 */
export function markSignalRMessageAsRead(id: string) {
  return request<boolean>({
    url: `/api/TaktSignalRMessage/${id}/read`,
    method: 'put'
  })
}

/** 标记所有消息为已读 */
export function markAllSignalRMessagesAsRead() {
  return request<boolean>({
    url: '/api/TaktSignalRMessage/read-all',
    method: 'put'
  })
}

/** 标记消息为未读 */
export function markSignalRMessageAsUnread(id: string) {
  return request<boolean>({
    url: `/api/TaktSignalRMessage/${id}/unread`,
    method: 'put'
  })
}

/** 标记所有消息为未读 */
export function markAllSignalRMessagesAsUnread() {
  return request<boolean>({
    url: '/api/TaktSignalRMessage/unread-all',
    method: 'put'
  })
}

/** 清理过期消息 */
export function cleanupExpiredSignalRMessages(days: number = 7) {
  return request<number>({
    url: '/api/TaktSignalRMessage/cleanup',
    method: 'post',
    params: { days }
  })
}

/** 导出在线消息数据 */
export function exportSignalRMessage(params: TaktSignalRMessageQueryParams, sheetName: string = '在线消息信息') {
  return request<Blob>({
    url: '/api/TaktSignalRMessage/export',
    method: 'get',
    params: { ...params, sheetName },
    responseType: 'blob'
  })
} 
