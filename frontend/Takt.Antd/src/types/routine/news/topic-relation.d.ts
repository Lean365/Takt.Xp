/** 新闻话题关系相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 新闻话题关系查询参数 */
export interface TaktNewsTopicRelationQuery extends TaktPagedQuery {
  newsId?: number
  topicId?: number
  relationType?: number
  relationWeight?: number
  relationUserId?: number
  relationUserName?: string
  isAutoRelation?: number
  relationStatus?: number
  startTime?: Date
  endTime?: Date
}

/** 新闻话题关系数据传输对象 */
export interface TaktNewsTopicRelation extends TaktBaseEntity {
  id: number
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationTime: Date
  relationUserId?: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
  news?: TaktNews
  topic?: TaktNewsTopic
}

/** 新闻话题关系创建参数 */
export interface TaktNewsTopicRelationCreate {
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationUserId?: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
}

/** 新闻话题关系更新参数 */
export interface TaktNewsTopicRelationUpdate extends TaktNewsTopicRelationCreate {
  id: number
}

/** 新闻话题关系状态更新参数 */
export interface TaktNewsTopicRelationStatusUpdate {
  id: number
  relationStatus: number
}

/** 新闻话题关系删除参数 */
export interface TaktNewsTopicRelationDelete {
  id: number
}

/** 新闻话题关系批量删除参数 */
export interface TaktNewsTopicRelationBatchDelete {
  ids: number[]
}

/** 新闻话题关系导入参数 */
export interface TaktNewsTopicRelationImport {
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
}

/** 新闻话题关系导出参数 */
export interface TaktNewsTopicRelationExport {
  id: number
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationTime: Date
  relationUserId?: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  createTime: Date
}

/** 新闻话题关系导入模板参数 */
export interface TaktNewsTopicRelationTemplate {
  newsId: number
  topicId: number
  relationType: number
  relationWeight: number
  relationUserName?: string
  relationRemark?: string
  isAutoRelation: number
  relationStatus: number
  orderNum: number
  remark?: string
}

/** 新闻话题关系分页结果 */
export type TaktNewsTopicRelationPagedResult = TaktPagedResult<TaktNewsTopicRelation>

/** 自动关联请求参数 */
export interface TaktNewsTopicAutoRelateRequest {
  newsId: number
  newsTitle: string
  newsContent: string
  newsKeywords?: string
}

/** 关键词匹配话题结果 */
export interface TaktNewsTopicKeywordMatch {
  topicId: number
  topicName: string
  topicDescription?: string
  topicKeywords?: string
  matchScore: number
  relationType: number
  relationWeight: number
}
