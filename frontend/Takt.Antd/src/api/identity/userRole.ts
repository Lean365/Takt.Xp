import {request} from '@/utils/request'
import type { TaktApiResponse } from '@/types/common'
import type { UserRoleAllocate } from '@/types/identity/userRole'

/**
 * 获取用户角色列表
 */
export function getUserRoleList(userId: string) {
  return request<number[]>({
    url: `/api/TaktUser/${userId}/roles`,
    method: 'get'
  })
}

/**
 * 分配用户角色
 */
export function allocateUserRole(data: UserRoleAllocate) {
  return request<boolean>({
    url: `/api/TaktUser/${data.userId}/roles`,
    method: 'put',
    data: data.roleIds
  })
} 
