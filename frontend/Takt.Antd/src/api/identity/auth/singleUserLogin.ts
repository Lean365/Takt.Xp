//===================================================================
// 项目名 : Lean.Takt
// 文件名 : singleUserLogin.ts
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V1.0.0
// 描述    : 单一用户登录API接口
//===================================================================

import {request} from '@/utils/request'

/**
 * 单一用户登录检查结果
 */
export interface SingleUserLoginCheckResult {
  canLogin: boolean
  reason?: string
  onlineDeviceCount: number
}

/**
 * 检查单一用户登录状态
 */
export function checkSingleUserLogin(userId: string, deviceId: string, ipAddress?: string) {
  return request<SingleUserLoginCheckResult>({
    url: '/api/TaktSingleUserLogin/check-login',
    method: 'POST',
    data: {
      UserId: userId,
      DeviceId: deviceId,
      IpAddress: ipAddress
    }
  })
}

/**
 * 强制用户下线（用于单一用户登录模式）
 */
export function forceUserOffline(userId: string, reason?: string) {
  return request<boolean>({
    url: `/api/TaktSingleUserLogin/force-offline/${userId}`,
    method: 'POST',
    params: { reason }
  })
}

/**
 * 获取用户在线设备数量
 */
export function getUserOnlineDeviceCount(userId: string) {
  return request<number>({
    url: `/api/TaktSingleUserLogin/online-count/${userId}`,
    method: 'GET'
  })
}



