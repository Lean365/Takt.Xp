import type { TaktBaseEntity } from '@/types/common'

/**
 * 用户部门关联
 */
export interface TaktUserDept extends TaktBaseEntity {
  /** 用户ID */
  userId: string
  /** 部门ID */
  deptId: string
}

/**
 * 用户部门分配参数
 */
export interface UserDeptAllocate {
  userId: string
  deptId: string
} 
