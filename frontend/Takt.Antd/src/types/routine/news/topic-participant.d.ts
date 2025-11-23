/** 新闻话题参与者相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 参与类型枚举 */
export enum TaktParticipationType {
  Creator = 1,    // 创建者
  Admin = 2,      // 管理员
  Member = 3,     // 普通成员
  Guest = 4       // 访客
}

/** 参与状态枚举 */
export enum TaktParticipationStatus {
  Active = 1,     // 活跃
  Inactive = 2,   // 非活跃
  Banned = 3      // 被禁言
}

/** 通知类型枚举 */
export enum TaktNotificationType {
  All = 1,        // 全部通知
  Important = 2,  // 重要通知
  None = 3        // 不通知
}

/** 新闻话题参与者查询参数 */
export interface TaktNewsTopicParticipantQuery extends TaktPagedQuery {
  topicId?: number
  userId?: number
  userName?: string
  participationType?: TaktParticipationType
  participationStatus?: TaktParticipationStatus
  receiveNotification?: number
  notificationType?: TaktNotificationType
  startTime?: Date
  endTime?: Date
}

/** 新闻话题参与者数据传输对象 */
export interface TaktNewsTopicParticipant extends TaktBaseEntity {
  id: number
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: TaktParticipationType
  participationTime: Date
  participationStatus: TaktParticipationStatus
  contributionScore: number
  contentCount: number
  commentCount: number
  likeCount: number
  shareCount: number
  lastActiveTime?: Date
  participationRemark?: string
  receiveNotification: number
  notificationType: TaktNotificationType
  orderNum: number
  remark?: string
  topic?: TaktNewsTopic
}

/** 新闻话题参与者创建参数 */
export interface TaktNewsTopicParticipantCreate {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: TaktParticipationType
  participationStatus: TaktParticipationStatus
  participationRemark?: string
  receiveNotification: number
  notificationType: TaktNotificationType
  orderNum: number
  remark?: string
}

/** 新闻话题参与者更新参数 */
export interface TaktNewsTopicParticipantUpdate extends TaktNewsTopicParticipantCreate {
  id: number
}

/** 新闻话题参与者状态更新参数 */
export interface TaktNewsTopicParticipantStatusUpdate {
  id: number
  participationStatus: TaktParticipationStatus
}

/** 新闻话题参与者删除参数 */
export interface TaktNewsTopicParticipantDelete {
  id: number
}

/** 新闻话题参与者批量删除参数 */
export interface TaktNewsTopicParticipantBatchDelete {
  ids: number[]
}

/** 新闻话题参与者导入参数 */
export interface TaktNewsTopicParticipantImport {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: TaktParticipationType
  participationStatus: TaktParticipationStatus
  participationRemark?: string
  receiveNotification: number
  notificationType: TaktNotificationType
  orderNum: number
  remark?: string
}

/** 新闻话题参与者导出参数 */
export interface TaktNewsTopicParticipantExport {
  id: number
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: TaktParticipationType
  participationTime: Date
  participationStatus: TaktParticipationStatus
  contributionScore: number
  contentCount: number
  commentCount: number
  likeCount: number
  shareCount: number
  lastActiveTime?: Date
  participationRemark?: string
  receiveNotification: number
  notificationType: TaktNotificationType
  orderNum: number
  createTime: Date
}

/** 新闻话题参与者导入模板参数 */
export interface TaktNewsTopicParticipantTemplate {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType: TaktParticipationType
  participationStatus: TaktParticipationStatus
  participationRemark?: string
  receiveNotification: number
  notificationType: TaktNotificationType
  orderNum: number
  remark?: string
}

/** 新闻话题参与者分页结果 */
export type TaktNewsTopicParticipantPagedResult = TaktPagedResult<TaktNewsTopicParticipant>

/** 用户参与话题请求参数 */
export interface TaktNewsTopicJoinRequest {
  topicId: number
  userId: number
  userName: string
  userAvatar?: string
  participationType?: TaktParticipationType
  participationRemark?: string
}

/** 用户退出话题请求参数 */
export interface TaktNewsTopicLeaveRequest {
  topicId: number
  userId: number
}

/** 检查用户参与状态请求参数 */
export interface TaktNewsTopicCheckJoinedRequest {
  topicId: number
  userId: number
}

/** 参与者统计信息 */
export interface TaktNewsTopicParticipantStatistics {
  totalParticipants: number
  activeParticipants: number
  creatorCount: number
  adminCount: number
  memberCount: number
  guestCount: number
  bannedCount: number
  totalContribution: number
  totalContent: number
  totalComments: number
  totalLikes: number
  totalShares: number
}
