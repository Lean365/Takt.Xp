/** 新闻评论相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 评论审核类型枚举 */
export enum TaktCommentAuditType {
  EditorAudit = 1,  // 责任编辑审核
  SystemAudit = 2,  // 系统自动审核
  ManualAudit = 3   // 人工审核
}

/** 新闻评论查询参数 */
export interface TaktNewsCommentQuery extends TaktPagedQuery {
  newsId?: number
  commentContent?: string
  commentUserId?: number
  commentUserName?: string
  parentCommentId?: number
  commentStatus?: number
  auditType?: TaktCommentAuditType
  auditorBy?: string
  startTime?: Date
  endTime?: Date
}

/** 新闻评论数据传输对象 */
export interface TaktNewsComment extends TaktBaseEntity {
  commentId: number
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  commentUserIp?: string
  commentUserAgent?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  auditType?: TaktCommentAuditType
  auditorBy?: string
  auditTime?: Date
  auditRemark?: string
  likeCount: number
  replyCount: number
  reportCount: number
  remark?: string
  parentComment?: TaktNewsComment
  replies?: TaktNewsComment[]
}

/** 新闻评论创建参数 */
export interface TaktNewsCommentCreate {
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  commentUserIp?: string
  commentUserAgent?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  remark?: string
}

/** 新闻评论更新参数 */
export interface TaktNewsCommentUpdate extends TaktNewsCommentCreate {
  commentId: number
}

/** 新闻评论审核参数 */
export interface TaktNewsCommentAudit {
  commentId: number
  commentStatus: number
  auditRemark?: string
}

/** 新闻评论批量审核参数 */
export interface TaktNewsCommentBatchAudit {
  commentIds: number[]
  commentStatus: number
  auditRemark?: string
}

/** 新闻评论删除参数 */
export interface TaktNewsCommentDelete {
  commentId: number
}

/** 新闻评论批量删除参数 */
export interface TaktNewsCommentBatchDelete {
  commentIds: number[]
}

/** 新闻评论导入参数 */
export interface TaktNewsCommentImport {
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  remark?: string
}

/** 新闻评论导出参数 */
export interface TaktNewsCommentExport {
  commentId: number
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  commentUserIp?: string
  commentUserAgent?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  auditType?: TaktCommentAuditType
  auditorBy?: string
  auditTime?: Date
  auditRemark?: string
  likeCount: number
  replyCount: number
  reportCount: number
  createTime: Date
}

/** 新闻评论导入模板参数 */
export interface TaktNewsCommentTemplate {
  newsId: number
  commentContent: string
  commentUserId: number
  commentUserName: string
  commentUserAvatar?: string
  parentCommentId?: number
  replyToCommentId?: number
  replyToUserId?: number
  replyToUserName?: string
  commentStatus: number
  remark?: string
}

/** 新闻评论分页结果 */
export type TaktNewsCommentPagedResult = TaktPagedResult<TaktNewsComment>

/** 评论字数限制信息 */
export interface TaktCommentLengthLimit {
  minLength: number
  maxLength: number
}

/** 评论内容验证结果 */
export interface TaktCommentValidationResult {
  isValid: boolean
  message: string
}

/** 审核统计信息 */
export interface TaktCommentAuditStatistics {
  totalPending: number
  totalApproved: number
  totalRejected: number
  todayPending: number
  todayApproved: number
  todayRejected: number
}

/** 审核员工作量统计 */
export interface TaktCommentAuditorWorkload {
  auditorId: number
  auditorName: string
  totalAudited: number
  approvedCount: number
  rejectedCount: number
  approvalRate: number
}
