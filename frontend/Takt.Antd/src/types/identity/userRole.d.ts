import type { TaktBaseEntity } from '@/types/common'

/**
 * 用户角色关联
 */
export interface TaktUserRole extends TaktBaseEntity {
  /** 用户ID */
  userId: string
  /** 角色ID */
  roleId: string
}

/**
 * 用户角色分配参数
 */
export interface UserRoleAllocate {
  userId: string
  roleIds: string[]
} 
