import type { TaktBaseEntity } from '@/types/common'

/**
 * 用户岗位关联
 */
export interface TaktUserPost extends TaktBaseEntity {
  /** 用户ID */
  userId: string
  /** 岗位ID */
  postId: string
}

/**
 * 用户岗位分配参数
 */
export interface UserPostAllocate {
  userId: string
  postIds: string[]
} 
