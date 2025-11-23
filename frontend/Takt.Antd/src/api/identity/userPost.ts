import {request} from '@/utils/request'
import type { TaktApiResponse } from '@/types/common'
import type { UserPostAllocate } from '@/types/identity/userPost'

/**
 * 获取用户岗位列表
 */
export function getUserPostList(userId: string) {
  return request<number[]>({
    url: `/api/TaktUser/${userId}/posts`,
    method: 'get'
  })
}

/**
 * 分配用户岗位
 */
export function allocateUserPost(data: UserPostAllocate) {
  return request<boolean>({
    url: `/api/TaktUser/${data.userId}/posts`,
    method: 'put',
    data: data.postIds
  })
} 
