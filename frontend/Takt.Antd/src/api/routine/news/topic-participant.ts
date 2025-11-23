import {request} from '@/utils/request'
import type { 
  TaktNewsTopicParticipant, 
  TaktNewsTopicParticipantQuery, 
  TaktNewsTopicParticipantCreate, 
  TaktNewsTopicParticipantUpdate,
  TaktNewsTopicParticipantStatusUpdate,
  TaktNewsTopicJoinRequest,
  TaktNewsTopicLeaveRequest,
  TaktNewsTopicCheckJoinedRequest,
  TaktNewsTopicParticipantStatistics
} from '@/types/routine/news/topic-participant'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取新闻话题参与者列表
 */
export function getNewsTopicParticipantList(params: TaktNewsTopicParticipantQuery) {
  return request<TaktPagedResult<TaktNewsTopicParticipant>>({
    url: '/api/TaktNewsTopicParticipant/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻话题参与者详情
 */
export function getNewsTopicParticipantById(id: number | bigint) {
  return request<TaktNewsTopicParticipant>({
    url: `/api/TaktNewsTopicParticipant/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻话题参与者
 */
export function createNewsTopicParticipant(data: TaktNewsTopicParticipantCreate) {
  return request<number | bigint>({
    url: '/api/TaktNewsTopicParticipant',
    method: 'post',
    data
  })
}

/**
 * 更新新闻话题参与者
 */
export function updateNewsTopicParticipant(data: TaktNewsTopicParticipantUpdate) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant',
    method: 'put',
    data
  })
}

/**
 * 删除新闻话题参与者
 */
export function deleteNewsTopicParticipant(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsTopicParticipant/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻话题参与者
 */
export function batchDeleteNewsTopicParticipant(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新新闻话题参与者状态
 */
export function updateNewsTopicParticipantStatus(data: TaktNewsTopicParticipantStatusUpdate) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/status',
    method: 'put',
    data
  })
}

/**
 * 用户参与话题
 */
export function joinTopic(data: TaktNewsTopicJoinRequest) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/join',
    method: 'post',
    data
  })
}

/**
 * 用户退出话题
 */
export function leaveTopic(data: TaktNewsTopicLeaveRequest) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/leave',
    method: 'post',
    data
  })
}

/**
 * 检查用户是否已参与话题
 */
export function isUserJoinedTopic(topicId: number, userId: number) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/check-joined',
    method: 'get',
    params: { topicId, userId }
  })
}

/**
 * 获取话题参与者统计信息
 */
export function getTopicParticipantStatistics(topicId: number | bigint) {
  return request<TaktNewsTopicParticipantStatistics>({
    url: `/api/TaktNewsTopicParticipant/topic/${topicId}/statistics`,
    method: 'get'
  })
}

/**
 * 获取话题参与者列表
 */
export function getTopicParticipants(topicId: number, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsTopicParticipant>>({
    url: `/api/TaktNewsTopicParticipant/topic/${topicId}/participants`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 获取用户参与的话题列表
 */
export function getUserJoinedTopics(userId: number, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsTopicParticipant>>({
    url: `/api/TaktNewsTopicParticipant/user/${userId}/topics`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 更新用户活跃时间
 */
export function updateUserActiveTime(topicId: number, userId: number) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/active-time',
    method: 'put',
    params: { topicId, userId }
  })
}

/**
 * 增加用户贡献分数
 */
export function increaseContributionScore(topicId: number, userId: number, score = 1) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/contribution-score',
    method: 'put',
    params: { topicId, userId, score }
  })
}

/**
 * 增加用户内容数量
 */
export function increaseContentCount(topicId: number, userId: number) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/content-count',
    method: 'put',
    params: { topicId, userId }
  })
}

/**
 * 增加用户评论数量
 */
export function increaseCommentCount(topicId: number, userId: number) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/comment-count',
    method: 'put',
    params: { topicId, userId }
  })
}

/**
 * 增加用户点赞数量
 */
export function increaseLikeCount(topicId: number, userId: number) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/like-count',
    method: 'put',
    params: { topicId, userId }
  })
}

/**
 * 增加用户分享数量
 */
export function increaseShareCount(topicId: number, userId: number) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/share-count',
    method: 'put',
    params: { topicId, userId }
  })
}

/**
 * 设置用户通知设置
 */
export function setNotificationSettings(topicId: number, userId: number, receiveNotification: boolean, notificationType = 1) {
  return request<boolean>({
    url: '/api/TaktNewsTopicParticipant/notification-settings',
    method: 'put',
    params: { topicId, userId, receiveNotification, notificationType }
  })
}

/**
 * 获取话题活跃用户列表
 */
export function getActiveUsers(topicId: number, count = 10) {
  return request<TaktNewsTopicParticipant[]>({
    url: `/api/TaktNewsTopicParticipant/topic/${topicId}/active-users`,
    method: 'get',
    params: { count }
  })
}

/**
 * 获取话题贡献者列表
 */
export function getTopContributors(topicId: number, count = 10) {
  return request<TaktNewsTopicParticipant[]>({
    url: `/api/TaktNewsTopicParticipant/topic/${topicId}/top-contributors`,
    method: 'get',
    params: { count }
  })
}

/**
 * 搜索参与者
 */
export function searchParticipants(keyword: string, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsTopicParticipant>>({
    url: '/api/TaktNewsTopicParticipant/search',
    method: 'get',
    params: { keyword, pageIndex, pageSize }
  })
}

/**
 * 获取参与者统计信息
 */
export function getParticipantStatistics() {
  return request<{
    totalParticipants: number
    activeParticipants: number
    inactiveParticipants: number
  }>({
    url: '/api/TaktNewsTopicParticipant/statistics',
    method: 'get'
  })
}

/**
 * 获取用户参与的话题统计
 */
export function getUserTopicStatistics(userId: number | bigint) {
  return request<{
    totalTopics: number
    activeTopics: number
    totalContribution: number
    totalContent: number
    totalComments: number
    totalLikes: number
    totalShares: number
  }>({
    url: `/api/TaktNewsTopicParticipant/user/${userId}/statistics`,
    method: 'get'
  })
}
