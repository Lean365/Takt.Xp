import {request} from '@/utils/request'
import type { TaktApiResponse } from '@/types/common'
import type { RoleMenuAllocate } from '@/types/identity/roleMenu'

/**
 * 获取角色菜单列表
 */
export function getRoleMenuList(roleId: string) {
  return request<number[]>({
    url: `/api/TaktRole/${roleId}/menus`,
    method: 'get'
  })
}

/**
 * 分配角色菜单
 */
export function allocateRoleMenu(data: RoleMenuAllocate) {
  return request<boolean>({
    url: `/api/TaktRole/${data.roleId}/menus`,
    method: 'put',
    data: data.menuIds
  })
} 
