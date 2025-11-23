/** 新闻话题相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 话题状态枚举 */
export enum TaktTopicStatus {
  Active = 1,     // 活跃
  Inactive = 2,   // 非活跃
  Archived = 3    // 已归档
}

/** 话题类型枚举 */
export enum TaktTopicType {
  Normal = 1,     // 普通话题
  Event = 2,      // 事件话题
  Special = 3     // 专题话题
}

/** 新闻话题查询参数 */
export interface TaktNewsTopicQuery extends TaktPagedQuery {
  topicName?: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  status?: TaktTopicStatus
  topicType?: TaktTopicType
  topicIsHot?: number
  topicIsRecommend?: number
  topicIsTop?: number
  topicCreatorId?: number
  topicCreatorName?: string
  startTime?: Date
  endTime?: Date
}

/** 新闻话题数据传输对象 */
export interface TaktNewsTopic extends TaktBaseEntity {
  topicId: number
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: TaktTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: TaktTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicParticipantCount: number
  topicNewsCount: number
  topicCommentCount: number
  topicLikeCount: number
  topicShareCount: number
  topicReadCount: number
  topicCreatorId?: number
  topicCreatorName?: string
  topicAdminIds?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
  topicNewsRelations?: TaktNewsTopicRelation[]
  topicParticipants?: TaktNewsTopicParticipant[]
}

/** 新闻话题创建参数 */
export interface TaktNewsTopicCreate {
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: TaktTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: TaktTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicCreatorId?: number
  topicCreatorName?: string
  topicAdminIds?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
}

/** 新闻话题更新参数 */
export interface TaktNewsTopicUpdate extends TaktNewsTopicCreate {
  topicId: number
}

/** 新闻话题状态更新参数 */
export interface TaktNewsTopicStatusUpdate {
  topicId: number
  status: TaktTopicStatus
}

/** 新闻话题删除参数 */
export interface TaktNewsTopicDelete {
  topicId: number
}

/** 新闻话题批量删除参数 */
export interface TaktNewsTopicBatchDelete {
  topicIds: number[]
}

/** 新闻话题导入参数 */
export interface TaktNewsTopicImport {
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: TaktTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: TaktTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicCreatorName?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
}

/** 新闻话题导出参数 */
export interface TaktNewsTopicExport {
  topicId: number
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: TaktTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: TaktTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicParticipantCount: number
  topicNewsCount: number
  topicCommentCount: number
  topicLikeCount: number
  topicShareCount: number
  topicReadCount: number
  topicCreatorName?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  createTime: Date
}

/** 新闻话题导入模板参数 */
export interface TaktNewsTopicTemplate {
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  topicCategory?: string
  topicTags?: string
  topicIcon?: string
  topicCover?: string
  topicColor?: string
  status: TaktTopicStatus
  topicIsHot: number
  topicIsRecommend: number
  topicIsTop: number
  topicType: TaktTopicType
  topicStartTime?: Date
  topicEndTime?: Date
  topicCreatorName?: string
  topicAdminNames?: string
  topicRules?: string
  topicSettings?: string
  seoTitle?: string
  seoKeywords?: string
  seoDescription?: string
  orderNum: number
  remark?: string
}

/** 新闻话题分页结果 */
export type TaktNewsTopicPagedResult = TaktPagedResult<TaktNewsTopic>

/** 话题统计信息 */
export interface TaktNewsTopicStatistics {
  totalTopics: number
  activeTopics: number
  hotTopics: number
  recommendedTopics: number
  totalParticipants: number
  totalNews: number
  totalComments: number
  totalLikes: number
  totalShares: number
  totalReads: number
}
