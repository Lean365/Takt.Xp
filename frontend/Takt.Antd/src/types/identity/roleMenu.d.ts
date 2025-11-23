import type { TaktBaseEntity } from '@/types/common'

/**
 * 角色菜单关联
 */
export interface TaktRoleMenu extends TaktBaseEntity {
  /** 角色ID */
  roleId: string
  /** 菜单ID */
  menuId: string
}

/**
 * 角色菜单分配参数
 */
export interface RoleMenuAllocate {
  roleId: string
  menuIds: string[]
} 
