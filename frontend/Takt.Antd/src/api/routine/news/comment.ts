import {request} from '@/utils/request'
import type { 
  TaktNewsComment, 
  TaktNewsCommentQuery, 
  TaktNewsCommentCreate, 
  TaktNewsCommentUpdate,
  TaktNewsCommentAudit,
  TaktNewsCommentBatchAudit,
  TaktCommentLengthLimit,
  TaktCommentValidationResult,
  TaktCommentAuditStatistics,
  TaktCommentAuditorWorkload
} from '@/types/routine/news/comment'
import type { TaktPagedResult, TaktApiResponse } from '@/types/common'

/**
 * 获取新闻评论列表
 */
export function getNewsCommentList(params: TaktNewsCommentQuery) {
  return request<TaktPagedResult<TaktNewsComment>>({
    url: '/api/TaktNewsComment/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻评论详情
 */
export function getNewsCommentById(id: number | bigint) {
  return request<TaktNewsComment>({
    url: `/api/TaktNewsComment/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻评论
 */
export function createNewsComment(data: TaktNewsCommentCreate) {
  return request<number | bigint>({
    url: '/api/TaktNewsComment',
    method: 'post',
    data
  })
}

/**
 * 更新新闻评论
 */
export function updateNewsComment(data: TaktNewsCommentUpdate) {
  return request<boolean>({
    url: '/api/TaktNewsComment',
    method: 'put',
    data
  })
}

/**
 * 删除新闻评论
 */
export function deleteNewsComment(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsComment/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻评论
 */
export function batchDeleteNewsComment(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktNewsComment/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 获取新闻评论列表
 */
export function getCommentsByNewsId(newsId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsComment>>({
    url: `/api/TaktNewsComment/news/${newsId}`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 获取评论回复列表
 */
export function getRepliesByCommentId(commentId: number | bigint, pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsComment>>({
    url: `/api/TaktNewsComment/${commentId}/replies`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 审核新闻评论
 */
export function auditNewsComment(data: TaktNewsCommentAudit) {
  return request<boolean>({
    url: '/api/TaktNewsComment/audit',
    method: 'post',
    data
  })
}

/**
 * 批量审核新闻评论
 */
export function batchAuditNewsComment(data: TaktNewsCommentBatchAudit) {
  return request<boolean>({
    url: '/api/TaktNewsComment/batch-audit',
    method: 'post',
    data
  })
}

/**
 * 通过新闻评论
 */
export function approveNewsComment(commentId: number | bigint, auditRemark?: string) {
  return request<boolean>({
    url: `/api/TaktNewsComment/${commentId}/approve`,
    method: 'post',
    params: { auditRemark }
  })
}

/**
 * 拒绝新闻评论
 */
export function rejectNewsComment(commentId: number | bigint, auditRemark?: string) {
  return request<boolean>({
    url: `/api/TaktNewsComment/${commentId}/reject`,
    method: 'post',
    params: { auditRemark }
  })
}

/**
 * 获取待审核评论列表
 */
export function getPendingAuditList(pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsComment>>({
    url: '/api/TaktNewsComment/pending-audit',
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 获取已审核评论列表
 */
export function getAuditedList(pageIndex = 1, pageSize = 20) {
  return request<TaktPagedResult<TaktNewsComment>>({
    url: '/api/TaktNewsComment/audited',
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 处理评论审核工作流
 */
export function processAuditWorkflow(commentId: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsComment/${commentId}/audit-workflow`,
    method: 'post'
  })
}

/**
 * 获取审核统计信息
 */
export function getAuditStatistics() {
  return request<TaktCommentAuditStatistics>({
    url: '/api/TaktNewsComment/audit-statistics',
    method: 'get'
  })
}

/**
 * 获取审核员工作量统计
 */
export function getAuditorWorkload(startDate: Date, endDate: Date) {
  return request<TaktCommentAuditorWorkload[]>({
    url: '/api/TaktNewsComment/auditor-workload',
    method: 'get',
    params: { startDate, endDate }
  })
}

/**
 * 获取评论字数限制
 */
export function getCommentLengthLimit() {
  return request<TaktCommentLengthLimit>({
    url: '/api/TaktNewsComment/length-limit',
    method: 'get'
  })
}

/**
 * 验证评论内容
 */
export function validateCommentContent(content: string) {
  return request<TaktCommentValidationResult>({
    url: '/api/TaktNewsComment/validate-content',
    method: 'post',
    data: content
  })
}
