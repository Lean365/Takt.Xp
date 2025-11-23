import {request} from '@/utils/request'
import type { TaktApiResponse } from '@/types/common'
import type { RoleDeptAllocate } from '@/types/identity/roleDept'

/**
 * 获取角色部门列表
 */
export function getRoleDeptList(roleId: string) {
  return request<number[]>({
    url: `/api/TaktRole/${roleId}/depts`,
    method: 'get'
  })
}

/**
 * 分配角色部门
 */
export function allocateRoleDept(data: RoleDeptAllocate) {
  return request<boolean>({
    url: `/api/TaktRole/${data.roleId}/depts`,
    method: 'put',
    data: data.deptIds
  })
} 
