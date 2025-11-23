import {request} from '@/utils/request'
import type { TaktApiResponse } from '@/types/common'

/**
 * 解锁用户
 * @param userId 用户ID
 * @returns 解锁结果
 */
export function unlockUser(userId: string) {
  return request<boolean>({
    url: `/api/login-policy/unlock/${userId}`,
    method: 'post'
  })
}

/**
 * 获取用户登录尝试剩余次数
 * @param userName 用户名
 * @returns 剩余尝试次数
 */
export function getRemainingAttempts(userName: string) {
  return request<number>({
    url: `/api/login-policy/attempts/${userName}`,
    method: 'get'
  })
}

/**
 * 获取账户锁定剩余时间(秒)
 * @param userName 用户名
 * @returns 剩余锁定时间(秒)
 */
export function getLockoutRemainingSeconds(userName: string) {
  return request<number>({
    url: `/api/login-policy/lockout/${userName}`,
    method: 'get'
  })
} 
