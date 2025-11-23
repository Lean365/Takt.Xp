import {request} from '@/utils/request'

// 验证码相关类型定义
export interface SliderCaptchaDto {
  backgroundImage: string
  sliderImage: string
  token: string
}

export interface SliderValidateDto {
  token: string
  xOffset: number
}

export interface CaptchaResultDto {
  success: boolean
  message: string
}

// 兼容性别名
export type SliderCaptchaResponse = SliderCaptchaDto
export type SliderValidateRequest = SliderValidateDto
export type CaptchaResult = CaptchaResultDto

export interface BehaviorDataRequest {
  userId: string
  mouseTrack: Array<{
    x: number
    y: number
    timestamp: number
  }>
  keyIntervals: number[]
  duration: number
}

export interface CaptchaConfig {
  enabled: boolean
  type: string
  slider: {
    width: number
    height: number
    sliderWidth: number
    tolerance: number
    expirationMinutes: number
  }
  behavior: {
    scoreThreshold: number
    dataExpirationMinutes: number
    enableMachineLearning: boolean
  }
}

/**
 * 获取验证码配置信息
 */
export function getCaptchaConfig(scenario?: 'login' | 'register' | 'password-recovery') {
  const params = scenario ? { scenario } : {}
  return request<CaptchaConfig>({
    url: '/api/TaktCaptcha/config',
    method: 'GET',
    params
  })
}

/**
 * 获取验证码（统一接口）
 */
export function getCaptcha() {
  return request<SliderCaptchaDto>({
    url: '/api/TaktCaptcha/slider',
    method: 'GET'
  })
}

/**
 * 验证验证码（统一接口）
 */
export function verifyCaptcha(data: SliderValidateDto) {
  return request<CaptchaResultDto>({
    url: '/api/TaktCaptcha/slider/validate',
    method: 'POST',
    data
  })
}

/**
 * 生成滑块验证码（兼容性）
 */
export function generateSlider() {
  return request<SliderCaptchaResponse>({
    url: '/api/TaktCaptcha/slider',
    method: 'GET'
  })
}

/**
 * 验证滑块（兼容性）
 */
export function validateSlider(data: SliderValidateRequest) {
  return request<CaptchaResult>({
    url: '/api/TaktCaptcha/slider/validate',
    method: 'POST',
    data
  })
}

/**
 * 收集行为数据
 */
export function collectBehavior(data: BehaviorDataRequest) {
  return request<string>({
    url: '/api/TaktCaptcha/behavior/collect',
    method: 'POST',
    data
  })
}

/**
 * 验证行为特征
 */
export function validateBehavior(token: string) {
  return request<CaptchaResultDto>({
    url: '/api/TaktCaptcha/behavior/validate',
    method: 'POST',
    data: { token }
  })
} 
