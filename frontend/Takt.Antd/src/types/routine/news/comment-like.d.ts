/** 新闻评论点赞相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 新闻评论点赞查询参数 */
export interface TaktNewsCommentLikeQuery extends TaktPagedQuery {
  commentId?: number
  userId?: number
  userName?: string
  likeTime?: Date
  startTime?: Date
  endTime?: Date
}

/** 新闻评论点赞数据传输对象 */
export interface TaktNewsCommentLike extends TaktBaseEntity {
  id: number
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  remark?: string
  comment?: TaktNewsComment
}

/** 新闻评论点赞创建参数 */
export interface TaktNewsCommentLikeCreate {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  remark?: string
}

/** 新闻评论点赞更新参数 */
export interface TaktNewsCommentLikeUpdate extends TaktNewsCommentLikeCreate {
  id: number
}

/** 新闻评论点赞删除参数 */
export interface TaktNewsCommentLikeDelete {
  id: number
}

/** 新闻评论点赞批量删除参数 */
export interface TaktNewsCommentLikeBatchDelete {
  ids: number[]
}

/** 新闻评论点赞导入参数 */
export interface TaktNewsCommentLikeImport {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻评论点赞导出参数 */
export interface TaktNewsCommentLikeExport {
  id: number
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  createTime: Date
}

/** 新闻评论点赞导入模板参数 */
export interface TaktNewsCommentLikeTemplate {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻评论点赞分页结果 */
export type TaktNewsCommentLikePagedResult = TaktPagedResult<TaktNewsCommentLike>

/** 新闻评论点赞状态更新参数 */
export interface TaktNewsCommentLikeStatusUpdate {
  id: number
  status: number
}

/** 点赞评论请求参数 */
export interface TaktNewsCommentLikeRequest {
  commentId: number
  userId: number
  userName: string
  userAvatar?: string
}

/** 取消点赞评论请求参数 */
export interface TaktNewsCommentUnlikeRequest {
  commentId: number
  userId: number
}

/** 检查用户点赞评论状态请求参数 */
export interface TaktNewsCommentCheckLikedRequest {
  commentId: number
  userId: number
}

/** 获取用户点赞的评论请求参数 */
export interface TaktNewsCommentGetLikedCommentsRequest {
  userId: number
  pageIndex?: number
  pageSize?: number
}

/** 获取评论点赞用户请求参数 */
export interface TaktNewsCommentGetLikedUsersRequest {
  commentId: number
  pageIndex?: number
  pageSize?: number
}
