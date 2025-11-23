import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult, TaktApiResponse } from '@/types/common'
import type { TaktDeviceLog } from '@/types/audit/deviceLog'
import type { TaktLoginFingerprint } from '@/types/common/loginFingerprint'

/**
 * 登录参数
 */
export interface LoginParams {
  /** 用户名 */
  userName: string;
  /** 密码 */
  password: string;
  /** 验证码Token */
  captchaToken?: string;
  /** 验证码偏移量 */
  captchaOffset: number;
  /** 验证码类型（Slider/Behavior） */
  captchaType?: string;
  /** 登录IP */
  loginIp: string;
  /** 用户代理 */
  userAgent: string;
  /** 登录类型 */
  loginType: number;
  /** 登录来源 */
  loginSource: number;
  /** 设备类型（PC、Android、iOS、MacOS、Linux、其他） */
  deviceType: string;
  /** 设备指纹信息 */
  loginFingerprint?: TaktLoginFingerprint;
}

/**
 * 用户信息DTO
 */
export interface UserInfo {
  /** 用户ID */
  userId: string
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 英文名 */
  englishName: string
  /** 用户类型 */
  userType: number
  /** 角色列表 */
  roles: string[]
  /** 权限列表 */
  permissions: string[]
  /** 头像 */
  avatar: string
  /** 全名 */
  fullName: string
  /** 真实姓名 */
  realName: string
}

/**
 * 登录结果数据
 */
export interface LoginResultData {
  /** 访问令牌 */
  accessToken: string
  /** 刷新令牌 */
  refreshToken: string
  /** 过期时间(秒) */
  expiresIn: number
  /** 用户信息 */
  userInfo: UserInfo
}

/**
 * 登录响应
 */
export type LoginResult = TaktApiResponse<LoginResultData>

/**
 * 盐值响应
 */
export interface SaltResponse {
  /** 盐值 */
  salt: string;
  /** 迭代次数 */
  iterations: number;
}

/**
 * 验证码响应
 */
export interface CaptchaResponse {
  /** 背景图片 */
  backgroundImage: string;
  /** 滑块图片 */
  sliderImage: string;
  /** 令牌 */
  token: string;
  /** UUID */
  uuid?: string;
}

/**
 * 验证码验证结果
 */
export interface CaptchaResult {
  /** 是否成功 */
  success: boolean;
  /** 消息 */
  message?: string;
}

/**
 * 账号锁定状态
 */
export type LockoutStatus = number

/**
 * 登录策略配置
 */
export const LOGIN_POLICY = {
  ADMIN: {
    MAX_ATTEMPTS: 3,              // 管理员最大尝试次数（3次后第4次如果还错误就锁定）
    LOCKOUT_MINUTES: 30          // 管理员锁定时间（分钟）
  },
  USER: {
    MAX_ATTEMPTS: 4,             // 普通用户最大尝试次数（4次后第5次如果还错误就禁用）
    LOCKOUT_DAYS: 999           // 普通用户禁用时间（天）
  },
  CAPTCHA: {
    REQUIRED_ATTEMPTS: 3,        // 错误3次后需要验证码
    REQUIRED_MINUTES: 5          // 5分钟内重复登录需要验证码
  }
} as const

/**
 * 登录相关的本地存储键
 */
export const LOGIN_STORAGE_KEYS = {
  LAST_LOGIN_TIME: 'lastLoginTime',
  FAILED_ATTEMPTS: 'failedAttempts',
  USERNAME: 'lastUsername'
} as const

/**
 * 特殊用户名
 */
export const SPECIAL_USERS = {
  ADMIN: 'admin'
} as const

/**
 * 登录检查结果
 */
export interface LoginCheckResultData {
  /** 是否存在会话 */
  existingSession: boolean
  /** 设备类型 */
  deviceType?: string
  /** 是否可以登录 */
  canLogin: boolean
  /** 现有设备信息 */
  existingDeviceInfo?: string
}

/**
 * 登录检查响应
 */
export interface LoginCheckResult {
  /** 响应代码 */
  code: number
  /** 响应消息 */
  msg: string
  /** 响应数据 */
  data: LoginCheckResultData
}

export { UserInfo, LoginResult, LoginParams }
