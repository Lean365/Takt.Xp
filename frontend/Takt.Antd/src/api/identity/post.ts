import {request} from '@/utils/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type { 
  TaktPostQuery, 
  TaktPost,
  TaktPostCreate,
  TaktPostUpdate
} from '@/types/identity/post'

/**
 * 获取岗位分页列表
 */
export function getPostList(params: TaktPostQuery) {
  return request<TaktPagedResult<TaktPost>>({
    url: '/api/TaktPost/list',
    method: 'get',
    params
  })
}

/**
 * 获取岗位详情
 */
export function getByIdAsync(postId: string) {
  return request<TaktPost>({
    url: `/api/TaktPost/${postId}`,
    method: 'get'
  })
}

/**
 * 创建岗位
 */
export function createPost(data: TaktPostCreate) {
  return request<number>({
    url: '/api/TaktPost',
    method: 'post',
    data
  })
}

/**
 * 更新岗位
 */
export function updatePost(data: TaktPostUpdate) {
  return request<boolean>({
    url: '/api/TaktPost',
    method: 'put',
    data
  })
}

/**
 * 删除岗位
 */
export function deletePost(postId: string) {
  return request<boolean>({
    url: `/api/TaktPost/${postId}`,
    method: 'delete'
  })
}

/**
 * 批量删除岗位
 */
export function batchDeletePost(postIds: string[]) {
  return request<boolean>({
    url: '/api/TaktPost/batch',
    method: 'delete',
    data: postIds
  })
}

/**
 * 更新岗位状态
 */
export function updatePostStatus(data: { postId: string; status: number }) {
  return request<boolean>({
    url: `/api/TaktPost/${data.postId}/status`,
    method: 'put',
    params: { status: data.status }
  })
}

/**
 * 获取岗位选项列表
 */
export function getPostOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktPost/options',
    method: 'get'
  })
}

/**
 * 获取用户岗位列表
 */
export function getUserPosts(postId: string) {
  return request<any>({
    url: `/api/TaktPost/${postId}/users`,
    method: 'get'
  })
}

/**
 * 分配用户岗位
 */
export function assignUserPosts(postId: string, userIds: string[]) {
  return request<boolean>({
    url: `/api/TaktPost/${postId}/users`,
    method: 'post',
    data: userIds
  })
}

/**
 * 导入岗位数据
 */
export function importPost(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktPost/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出岗位数据
 */
export function exportPost(params?: TaktPostQuery) {
  return request({
    url: '/api/TaktPost/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 获取岗位导入模板
 */
export function getTemplate() {
  return request({
    url: '/api/TaktPost/template',
    method: 'get',
    responseType: 'blob'
  })
}
