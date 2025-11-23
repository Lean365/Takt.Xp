/** 新闻相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 新闻状态枚举 */
export enum TaktNewsStatus {
  Draft = 0,      // 草稿
  Published = 1,  // 已发布
  Offline = 2,    // 已下线
  Deleted = 3     // 已删除
}

/** 新闻查询参数 */
export interface TaktNewsQuery extends TaktPagedQuery {
  newsTitle?: string
  newsContent?: string
  newsCategory?: string
  newsTags?: string
  newsKeywords?: string
  status?: TaktNewsStatus
  isTop?: number
  isRecommend?: number
  isHot?: number
  authorId?: number
  authorName?: string
  publishDepartment?: string
  startTime?: Date
  endTime?: Date
}

/** 新闻数据传输对象 */
export interface TaktNews extends TaktBaseEntity {
  newsId: number
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsCover?: string
  newsImages?: string
  newsVideo?: string
  newsAudio?: string
  newsAttachments?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorId?: number
  authorName?: string
  authorAvatar?: string
  publishDepartment?: string
  publishTime?: Date
  expiryTime?: Date
  readCount: number
  likeCount: number
  commentCount: number
  shareCount: number
  recommendCount: number
  status: TaktNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
  newsTopicRelations?: TaktNewsTopicRelation[]
}

/** 新闻创建参数 */
export interface TaktNewsCreate {
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsCover?: string
  newsImages?: string
  newsVideo?: string
  newsAudio?: string
  newsAttachments?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorId?: number
  authorName?: string
  authorAvatar?: string
  publishDepartment?: string
  publishTime?: Date
  expiryTime?: Date
  status: TaktNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
}

/** 新闻更新参数 */
export interface TaktNewsUpdate extends TaktNewsCreate {
  newsId: number
}

/** 新闻状态更新参数 */
export interface TaktNewsStatusUpdate {
  newsId: number
  status: TaktNewsStatus
}

/** 新闻删除参数 */
export interface TaktNewsDelete {
  newsId: number
}

/** 新闻批量删除参数 */
export interface TaktNewsBatchDelete {
  newsIds: number[]
}

/** 新闻导入参数 */
export interface TaktNewsImport {
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorName?: string
  publishDepartment?: string
  publishTime?: Date
  status: TaktNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
}

/** 新闻导出参数 */
export interface TaktNewsExport {
  newsId: number
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorName?: string
  publishDepartment?: string
  publishTime?: Date
  readCount: number
  likeCount: number
  commentCount: number
  shareCount: number
  recommendCount: number
  status: TaktNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  createTime: Date
}

/** 新闻导入模板参数 */
export interface TaktNewsTemplate {
  newsTitle: string
  newsSubtitle?: string
  newsContent: string
  newsSummary?: string
  newsCategory: string
  newsTags?: string
  newsKeywords?: string
  newsSource?: string
  newsSourceUrl?: string
  newsAuthor?: string
  authorName?: string
  publishDepartment?: string
  publishTime?: Date
  status: TaktNewsStatus
  isTop: number
  isRecommend: number
  isHot: number
  isOriginal: number
  isBreaking: number
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  remark?: string
}

/** 新闻分页结果 */
export type TaktNewsPagedResult = TaktPagedResult<TaktNews> 
