import {request} from '@/utils/request'
import type { TaktApiResponse } from '@/types/common'
import type { UserDeptAllocate } from '@/types/identity/userDept'

/**
 * 获取用户部门
 */
export function getUserDept(userId: string) {
  return request<number>({
    url: `/api/TaktUser/${userId}/dept`,
    method: 'get'
  })
}

/**
 * 分配用户部门
 */
export function allocateUserDept(data: UserDeptAllocate) {
  return request<boolean>({
    url: `/api/TaktUser/${data.userId}/dept`,
    method: 'put',
    data: { deptId: data.deptId }
  })
} 
