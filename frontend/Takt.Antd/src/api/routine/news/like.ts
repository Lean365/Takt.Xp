import {request} from '@/utils/request'
import type { 
  TaktNewsLike, 
  TaktNewsLikeQuery, 
  TaktNewsLikeCreate, 
  TaktNewsLikeUpdate,
  TaktNewsLikeRequest,
  TaktNewsUnlikeRequest,
  TaktNewsCheckLikedRequest,
  TaktNewsGetLikedNewsRequest,
  TaktNewsGetLikedUsersRequest
} from '@/types/routine/news/like'
import type { TaktPagedResult } from '@/types/common'

/**
 * 获取新闻点赞列表
 */
export function getNewsLikeList(params: TaktNewsLikeQuery) {
  return request<TaktPagedResult<TaktNewsLike>>({
    url: '/api/TaktNewsLike/list',
    method: 'get',
    params
  })
}

/**
 * 获取新闻点赞详情
 */
export function getNewsLikeById(id: number | bigint) {
  return request<TaktNewsLike>({
    url: `/api/TaktNewsLike/${id}`,
    method: 'get'
  })
}

/**
 * 创建新闻点赞
 */
export function createNewsLike(data: TaktNewsLikeCreate) {
  return request<number>({
    url: '/api/TaktNewsLike',
    method: 'post',
    data
  })
}

/**
 * 更新新闻点赞
 */
export function updateNewsLike(data: TaktNewsLikeUpdate) {
  return request<boolean>({
    url: '/api/TaktNewsLike',
    method: 'put',
    data
  })
}

/**
 * 删除新闻点赞
 */
export function deleteNewsLike(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktNewsLike/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除新闻点赞
 */
export function batchDeleteNewsLike(ids: (number | bigint)[]) {
  return request<boolean>({
    url: '/api/TaktNewsLike/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 点赞新闻
 */
export function likeNews(data: TaktNewsLikeRequest) {
  return request<boolean>({
    url: '/api/TaktNewsLike/like',
    method: 'post',
    data
  })
}

/**
 * 取消点赞新闻
 */
export function unlikeNews(data: TaktNewsUnlikeRequest) {
  return request<boolean>({
    url: '/api/TaktNewsLike/unlike',
    method: 'post',
    data
  })
}

/**
 * 检查用户是否已点赞新闻
 */
export function isUserLiked(data: TaktNewsCheckLikedRequest) {
  return request<boolean>({
    url: '/api/TaktNewsLike/check-liked',
    method: 'post',
    data
  })
}

/**
 * 获取用户点赞的新闻列表
 */
export function getLikedNewsByUserId(params: TaktNewsGetLikedNewsRequest) {
  return request<TaktPagedResult<any>>({
    url: '/api/TaktNewsLike/user-liked-news',
    method: 'get',
    params
  })
}

/**
 * 获取新闻点赞用户列表
 */
export function getLikedUsersByNewsId(params: TaktNewsGetLikedUsersRequest) {
  return request<TaktPagedResult<any>>({
    url: '/api/TaktNewsLike/news-liked-users',
    method: 'get',
    params
  })
}
