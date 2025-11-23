import {request} from '@/utils/request'
import { maskName, maskEmail, maskCustom } from '@/utils/maskUtil'

/**
 * 身份验证请求参数
 */
export interface IdentityVerificationRequest {
  /** 用户名 */
  userName: string
  /** 邮箱地址 */
  email: string
  /** 验证码 */
  captcha: number
  /** 验证码Token */
  captchaToken: string
}

/**
 * 身份验证响应
 */
export interface IdentityVerificationResponse {
  /** 是否验证成功 */
  success: boolean
  /** 验证Token */
  verificationToken: string
  /** 消息 */
  message: string
}

/**
 * 发送验证码请求参数
 */
export interface SendVerificationCodeRequest {
  /** 用户名 */
  userName: string
  /** 邮箱地址 */
  email: string
}

/**
 * 发送验证码响应
 */
export interface SendVerificationCodeResponse {
  /** 是否发送成功 */
  success: boolean
  /** 消息 */
  message: string
  /** 验证码有效期（分钟） */
  expirationMinutes: number
}

/**
 * 邮箱验证请求参数
 */
export interface EmailVerificationRequest {
  /** 用户名 */
  userName: string
  /** 邮箱地址 */
  email: string
  /** 验证码 */
  verificationCode: string
}

/**
 * 邮箱验证响应
 */
export interface EmailVerificationResponse {
  /** 是否验证成功 */
  success: boolean
  /** 重置Token */
  resetToken: string
  /** 消息 */
  message: string
}

/**
 * 密码重置请求参数
 */
export interface PasswordResetRequest {
  /** 用户名 */
  userName: string
  /** 邮箱地址 */
  email: string
  /** 重置Token */
  resetToken: string
  /** 新密码 */
  newPassword: string
}

/**
 * 密码重置响应
 */
export interface PasswordResetResponse {
  /** 是否重置成功 */
  success: boolean
  /** 消息 */
  message: string
}

/**
 * 验证用户身份
 * @param data 身份验证请求参数
 */
export function verifyIdentity(data: IdentityVerificationRequest) {
  console.log('[找回密码] 开始验证用户身份:', {
    userName: maskName(data.userName),
    email: maskEmail(data.email),
    captcha: maskCustom(data.captcha.toString(), 1, 1),
    captchaToken: maskCustom(data.captchaToken, 8, 8)
  })
  return request<IdentityVerificationResponse>({
    url: '/api/TaktPasswordRecovery/verify-identity',
    method: 'post',
    data
  }).then(response => {
    console.log('[找回密码] 身份验证响应:', response.data)
    return response
  }).catch(error => {
    console.error('[找回密码] 身份验证失败:', error)
    throw error
  })
}

/**
 * 发送邮箱验证码
 * @param data 发送验证码请求参数
 */
export function sendVerificationCode(data: SendVerificationCodeRequest) {
  console.log('[找回密码] 开始发送验证码:', {
    userName: maskName(data.userName),
    email: maskEmail(data.email)
  })
  return request<SendVerificationCodeResponse>({
    url: '/api/TaktPasswordRecovery/send-verification-code',
    method: 'post',
    data
  }).then(response => {
    console.log('[找回密码] 发送验证码响应:', response.data)
    return response
  }).catch(error => {
    console.error('[找回密码] 发送验证码失败:', error)
    throw error
  })
}

/**
 * 验证邮箱验证码
 * @param data 邮箱验证请求参数
 */
export function verifyEmailCode(data: EmailVerificationRequest) {
  console.log('[找回密码] 开始验证邮箱验证码:', {
    userName: maskName(data.userName),
    email: maskEmail(data.email),
    verificationCode: maskCustom(data.verificationCode, 1, 1)
  })
  return request<EmailVerificationResponse>({
    url: '/api/TaktPasswordRecovery/verify-email-code',
    method: 'post',
    data
  }).then(response => {
    console.log('[找回密码] 邮箱验证响应:', response.data)
    return response
  }).catch(error => {
    console.error('[找回密码] 邮箱验证失败:', error)
    throw error
  })
}

/**
 * 重置密码
 * @param data 密码重置请求参数
 */
export function resetPassword(data: PasswordResetRequest) {
  console.log('[找回密码] 开始重置密码:', {
    userName: maskName(data.userName),
    email: maskEmail(data.email),
    resetToken: maskCustom(data.resetToken, 8, 8),
    newPassword: '********'
  })
  return request<PasswordResetResponse>({
    url: '/api/TaktPasswordRecovery/reset-password',
    method: 'post',
    data
  }).then(response => {
    console.log('[找回密码] 密码重置响应:', response.data)
    return response
  }).catch(error => {
    console.error('[找回密码] 密码重置失败:', error)
    throw error
  })
} 
