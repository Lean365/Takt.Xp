import { get, post, request } from '@/utils/request'
import type { 
  LoginParams, 
  LoginResultData, 
  SaltResponse, 
  UserInfo, 
  CaptchaResponse, 
  CaptchaResult, 
  LockoutStatus, 
  LoginCheckResultData 
} from '@/types/identity/auth'
import { maskName, maskCustom } from '@/utils/maskUtil'

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LoginParams) {
  // 详细记录所有参数
  console.log('[Auth] 开始登录，完整参数:', {
    用户名: data.userName,
    密码: data.password ? `${data.password.substring(0, 8)}...` : '空',
    验证码Token: data.captchaToken || '空',
    验证码偏移量: data.captchaOffset,
    验证码类型: data.captchaType || '空',
    登录IP: data.loginIp || '空',
    用户代理: data.userAgent ? `${data.userAgent.substring(0, 50)}...` : '空',
    登录类型: data.loginType,
    登录来源: data.loginSource,
    设备类型: data.deviceType || '空',
    设备指纹: data.loginFingerprint ? {
      loginFingerprint: data.loginFingerprint.loginFingerprint,
      language: data.loginFingerprint.language,
      timezone: data.loginFingerprint.timezone
    } : '空'
  })
  
  // 验证关键参数
  if (!data.userName || !data.password) {
    throw new Error('用户名和密码不能为空')
  }
  
  if (!data.deviceType) {
    throw new Error('设备类型不能为空')
  }
  
  if (!data.userAgent) {
    throw new Error('用户代理不能为空')
  }
  
  return post<LoginResultData>('/api/TaktAuth/login', data, {
    timeout: 300000, // 登录请求单独设置5分钟超时
    headers: {
      'Content-Type': 'application/json',
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    }
  })
}

/**
 * 获取盐值
 * @param username 用户名
 */
export function getSalt(username: string) {
  console.log('[Auth] 开始获取盐值:', maskName(username))
  return get<SaltResponse>('/api/TaktAuth/salt', {
    params: { username },
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    },
    validateStatus: function(status: number) {
      console.log('[Auth] 盐值请求状态码:', status)
      return status >= 200 && status < 300
    }
  })
}

/**
 * 登出
 * @param params 登出参数
 */
export function logout(params?: { isSystemRestart?: boolean }) {
  return request<void>({
    url: '/api/TaktAuth/logout',
    method: 'post',
    params
  })
}

/**
 * 刷新Token
 * @param refreshToken 刷新令牌
 */
export function refreshToken(refreshToken: string) {
  return request<LoginResultData>({
    url: '/api/TaktAuth/refresh-token',
    method: 'post',
    data: refreshToken, // 直接发送字符串
    headers: {
      'Content-Type': 'text/plain' // 明确指定Content-Type
    }
  })
}

/**
 * 获取用户信息
 */
export function getInfo() {
  return get<UserInfo>('/api/TaktAuth/info')
}

/**
 * 获取验证码
 */
export function getCaptcha() {
  return request<CaptchaResponse>({
    url: '/api/TaktAuth/captcha',
    method: 'get'
  })
}

/**
 * 验证验证码
 * @param data 验证参数
 */
export function verifyCaptcha(data: { token: string; offset: number }) {
  return request<CaptchaResult>({
    url: '/api/TaktAuth/verify-captcha',
    method: 'post',
    data
  })
}

/**
 * 检查账号锁定状态
 * @param username 用户名
 */
export function checkAccountLockout(userName: string) {
  console.log('[Auth] 检查账号锁定状态:', maskName(userName))
  return request<LockoutStatus>({
    url: `/api/TaktAuth/lockout/${userName}`,
    method: 'get',
    headers: {
      'Cache-Control': 'no-cache',
      'Pragma': 'no-cache'
    }
  })
}

/**
 * 获取剩余尝试次数
 * @param username 用户名
 */
export function getRemainingAttempts(username: string) {
  return request<number>({
    url: `/api/TaktAuth/attempts/${username}`,
    method: 'get'
  })
}

/**
 * 解锁用户
 * @param username 用户名
 */
export function unlockUser(username: string) {
  return request<boolean>({
    url: `/api/TaktAuth/unlock/${username}`,
    method: 'post'
  })
}

/**
 * 检查登录状态
 * @param data 登录参数
 */
export function checkLogin(data: LoginParams) {
  // 构建符合TaktAuthDto的参数
  const loginDto = {
    userName: data.userName,
    password: data.password,
    captchaToken: data.captchaToken,
    captchaOffset: data.captchaOffset,
    captchaType: data.captchaType,
    userAgent: data.userAgent ?? navigator.userAgent,
    loginType: data.loginType ?? 0, // 默认使用密码登录
    loginSource: data.loginSource ?? 0, // 默认使用Web登录
    deviceType: data.deviceType,
    loginFingerprint: data.loginFingerprint
  }

  return request<LoginCheckResultData>({
    url: '/api/TaktAuth/check-login',
    method: 'post',
    data: loginDto
  })
}

/**
 * 刷新用户令牌
 * @param refreshToken 刷新令牌
 */
export function refreshUserToken(refreshToken: string) {
  return request<LoginResultData>({
    url: '/api/TaktAuth/refresh-token',
    method: 'post',
    data: refreshToken, // 直接发送字符串
    headers: {
      'Content-Type': 'text/plain' // 明确指定Content-Type
    },
    skipAuth: true // 跳过Authorization头，因为这是获取Token的接口
  })
} 

/**
 * 获取注册配置
 */
export function getRegistrationConfig() {
  return request<any>({
    url: '/api/TaktAuth/registration-config',
    method: 'get'
  })
} 
