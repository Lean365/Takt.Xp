import type { TaktBaseEntity } from '@/types/common'

/**
 * 角色部门关联
 */
export interface TaktRoleDept extends TaktBaseEntity {
  /** 角色ID */
  roleId: string
  /** 部门ID */
  deptId: number
}

/**
 * 角色部门分配参数
 */
export interface RoleDeptAllocate {
  roleId: string
  deptIds: number[]
} 
