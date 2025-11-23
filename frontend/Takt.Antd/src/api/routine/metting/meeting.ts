import {request} from '@/utils/request'
import type { 
  TaktMeeting, 
  TaktMeetingQuery, 
  TaktMeetingCreate, 
  TaktMeetingUpdate 
} from '@/types/routine/meeting/meeting'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取会议列表
 */
export function getMeetingList(params: TaktMeetingQuery) {
  return request<TaktPagedResult<TaktMeeting>>({
    url: '/api/TaktMeeting/list',
    method: 'get',
    params
  })
}

/**
 * 获取会议详情
 */
export function getMeetingById(id: number | bigint) {
  return request<TaktMeeting>({
    url: `/api/TaktMeeting/${id}`,
    method: 'get'
  })
}

/**
 * 创建会议
 */
export function createMeeting(data: TaktMeetingCreate) {
  return request<number | bigint>({
    url: '/api/TaktMeeting',
    method: 'post',
    data
  })
}

/**
 * 更新会议
 */
export function updateMeeting(data: TaktMeetingUpdate) {
  return request<boolean>({
    url: '/api/TaktMeeting',
    method: 'put',
    data
  })
}

/**
 * 删除会议
 */
export function deleteMeeting(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktMeeting/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除会议
 */
export function batchDeleteMeeting(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktMeeting/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 获取我的会议列表
 */
export function getMyMeetingList(params: TaktMeetingQuery) {
  return request<TaktPagedResult<TaktMeeting>>({
    url: '/api/TaktMeeting/my',
    method: 'get',
    params
  })
}

/**
 * 获取热门会议
 */
export function getHotMeetings(count = 10) {
  return request<TaktMeeting[]>({
    url: '/api/TaktMeeting/hot',
    method: 'get',
    params: { count }
  })
}

/**
 * 获取推荐会议
 */
export function getRecommendedMeetings(count = 10) {
  return request<TaktMeeting[]>({
    url: '/api/TaktMeeting/recommended',
    method: 'get',
    params: { count }
  })
} 
