/** 新闻点赞相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'
import type { TaktNews } from './news'

/** 新闻点赞查询参数 */
export interface TaktNewsLikeQuery extends TaktPagedQuery {
  newsId?: number
  userId?: number
  userName?: string
  likeTime?: Date
  startTime?: Date
  endTime?: Date
}

/** 新闻点赞数据传输对象 */
export interface TaktNewsLike extends TaktBaseEntity {
  id: number
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  remark?: string
  news?: TaktNews
}

/** 新闻点赞创建参数 */
export interface TaktNewsLikeCreate {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  remark?: string
}

/** 新闻点赞更新参数 */
export interface TaktNewsLikeUpdate extends TaktNewsLikeCreate {
  id: number
}

/** 新闻点赞删除参数 */
export interface TaktNewsLikeDelete {
  id: number
}

/** 新闻点赞批量删除参数 */
export interface TaktNewsLikeBatchDelete {
  ids: number[]
}

/** 新闻点赞导入参数 */
export interface TaktNewsLikeImport {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻点赞导出参数 */
export interface TaktNewsLikeExport {
  id: number
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  userIp?: string
  userAgent?: string
  likeTime: Date
  createTime: Date
}

/** 新闻点赞导入模板参数 */
export interface TaktNewsLikeTemplate {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
  likeTime?: Date
  remark?: string
}

/** 新闻点赞分页结果 */
export type TaktNewsLikePagedResult = TaktPagedResult<TaktNewsLike>

/** 新闻点赞状态更新参数 */
export interface TaktNewsLikeStatusUpdate {
  id: number
  status: number
}

/** 点赞新闻请求参数 */
export interface TaktNewsLikeRequest {
  newsId: number
  userId: number
  userName: string
  userAvatar?: string
}

/** 取消点赞新闻请求参数 */
export interface TaktNewsUnlikeRequest {
  newsId: number
  userId: number
}

/** 检查用户点赞状态请求参数 */
export interface TaktNewsCheckLikedRequest {
  newsId: number
  userId: number
}

/** 获取用户点赞的新闻请求参数 */
export interface TaktNewsGetLikedNewsRequest {
  userId: number
  pageIndex?: number
  pageSize?: number
}

/** 获取新闻点赞用户请求参数 */
export interface TaktNewsGetLikedUsersRequest {
  newsId: number
  pageIndex?: number
  pageSize?: number
}
