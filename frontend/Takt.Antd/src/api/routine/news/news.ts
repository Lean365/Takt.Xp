import {request} from '@/utils/request'
import type { 
  TaktNews, 
  TaktNewsQuery, 
  TaktNewsCreate, 
  TaktNewsUpdate, 
  TaktNewsStatusUpdate 
} from '@/types/routine/news/news'
import type { TaktPagedResult } from '@/types/common'

/**
 * 获取新闻列表
 */
export function getNewsList(params: TaktNewsQuery) {
  return request<TaktPagedResult<TaktNews>>({
    url: '/api/TaktNews/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻详情
 */
export function getNewsById(id: number | bigint) {
  return request<TaktNews>({
    url: `/api/TaktNews/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻
 */
export function createNews(data: TaktNewsCreate) {
  return request<number>({
    url: '/api/TaktNews',
    method: 'post',
    data
  })
}

/**
 * 更新新闻
 */
export function updateNews(data: TaktNewsUpdate) {
  return request<boolean>({
    url: '/api/TaktNews',
    method: 'put',
    data
  })
}

/**
 * 删除新闻
 */
export function deleteNews(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNews/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻
 */
export function batchDeleteNews(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktNews/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 更新新闻状态
 */
export function updateNewsStatus(data: TaktNewsStatusUpdate) {
  return request<boolean>({
    url: '/api/TaktNews/status',
    method: 'put',
    data
  })
}

/**
 * 获取热门新闻
 */
export function getHotNews(count = 10) {
  return request<TaktNews[]>({
    url: '/api/TaktNews/hot',
    method: 'get',
    params: { count }
  })
}

/**
 * 获取推荐新闻
 */
export function getRecommendedNews(count = 10) {
  return request<TaktNews[]>({
    url: '/api/TaktNews/recommended',
    method: 'get',
    params: { count }
  })
}

/**
 * 搜索新闻
 */
export function searchNews(keyword: string, count = 20) {
  return request<TaktNews[]>({
    url: '/api/TaktNews/search',
    method: 'get',
    params: { keyword, count }
  })
}

/**
 * 导出新闻数据
 */
export function exportNews(params: TaktNewsQuery) {
  return request<Blob>({
    url: '/api/TaktNews/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入新闻数据
 */
export function importNews(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktNews/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 获取新闻导入模板
 */
export function getNewsTemplate() {
  return request<Blob>({
    url: '/api/TaktNews/template',
    method: 'get',
    responseType: 'blob'
  })
} 
