import {request} from '@/utils/request'
import type { 
  TaktNewsTopic, 
  TaktNewsTopicQuery, 
  TaktNewsTopicCreate, 
  TaktNewsTopicUpdate,
  TaktNewsTopicStatusUpdate,
  TaktNewsTopicStatistics
} from '@/types/routine/news/topic'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取新闻话题列表
 */
export function getNewsTopicList(params: TaktNewsTopicQuery) {
  return request<TaktPagedResult<TaktNewsTopic>>({
    url: '/api/TaktNewsTopic/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻话题详情
 */
export function getNewsTopicById(id: number | bigint) {
  return request<TaktNewsTopic>({
    url: `/api/TaktNewsTopic/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻话题
 */
export function createNewsTopic(data: TaktNewsTopicCreate) {
  return request<number | bigint>({
    url: '/api/TaktNewsTopic',
    method: 'post',
    data
  })
}

/**
 * 更新新闻话题
 */
export function updateNewsTopic(data: TaktNewsTopicUpdate) {
  return request<boolean>({
    url: '/api/TaktNewsTopic',
    method: 'put',
    data
  })
}

/**
 * 删除新闻话题
 */
export function deleteNewsTopic(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsTopic/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻话题
 */
export function batchDeleteNewsTopic(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktNewsTopic/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新新闻话题状态
 */
export function updateNewsTopicStatus(data: TaktNewsTopicStatusUpdate) {
  return request<boolean>({
    url: '/api/TaktNewsTopic/status',
    method: 'put',
    data
  })
}

/**
 * 获取热门话题
 */
export function getHotTopics(count = 10) {
  return request<TaktNewsTopic[]>({
    url: '/api/TaktNewsTopic/hot',
    method: 'get',
    params: { count }
  })
}

/**
 * 获取推荐话题
 */
export function getRecommendedTopics(count = 10) {
  return request<TaktNewsTopic[]>({
    url: '/api/TaktNewsTopic/recommended',
    method: 'get',
    params: { count }
  })
}

/**
 * 获取话题下的新闻列表
 */
export function getNewsByTopicId(topicId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<any>>({
    url: `/api/TaktNewsTopic/${topicId}/news`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 获取话题参与者列表
 */
export function getTopicParticipants(topicId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<any>>({
    url: `/api/TaktNewsTopic/${topicId}/participants`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 用户参与话题
 */
export function joinTopic(topicId: number | bigint, userId: number | bigint, userName: string, userAvatar?: string) {
  return request<boolean>({
    url: `/api/TaktNewsTopic/${topicId}/join`,
    method: 'post',
    params: { userId, userName, userAvatar }
  })
}

/**
 * 用户退出话题
 */
export function leaveTopic(topicId: number | bigint, userId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsTopic/${topicId}/leave`,
    method: 'delete',
    params: { userId }
  })
}

/**
 * 检查用户是否已参与话题
 */
export function isUserJoinedTopic(topicId: number | bigint, userId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsTopic/${topicId}/check-joined`,
    method: 'get',
    params: { userId }
  })
}

/**
 * 获取用户参与的话题列表
 */
export function getUserJoinedTopics(userId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsTopic>>({
    url: `/api/TaktNewsTopic/user/${userId}/joined`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 搜索话题
 */
export function searchTopics(keyword: string, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsTopic>>({
    url: '/api/TaktNewsTopic/search',
    method: 'get',
    params: { keyword, pageIndex, pageSize }
  })
}

/**
 * 获取话题统计信息
 */
export function getTopicStatistics() {
  return request<TaktNewsTopicStatistics>({
    url: '/api/TaktNewsTopic/statistics',
    method: 'get'
  })
}
