/**
 * 应用通用配置类型定义
 */

/**
 * 水印配置
 */
export interface TaktWatermarkConfig {
  /** 是否启用水印 */
  enabled: boolean
  /** 水印内容（支持单行字符串或多行字符串数组） */
  content: string | string[]
  /** 水印字体大小 */
  fontSize: number
  /** 水印颜色（可选，不设置则使用默认颜色） */
  color?: string
  /** 水印透明度 */
  opacity: number
  /** 水印旋转角度 */
  rotate: number
  /** 水印间距 */
  gap: [number, number]
}

/**
 * 用户注册配置
 */
export interface TaktRegisterConfig {
  /** 是否启用注册功能 */
  enabled: boolean
  /** 注册是否需要邮箱验证 */
  requireEmailVerification: boolean
  /** 注册是否需要手机验证 */
  requirePhoneVerification: boolean
}

/**
 * 系统功能配置
 */
export interface TaktFeatureConfig {
  /** 是否显示帮助文档 */
  showHelp: boolean
  /** 是否显示反馈功能 */
  showFeedback: boolean
  /** 是否显示在线用户 */
  showOnlineUsers: boolean
  /** 是否显示系统公告 */
  showAnnouncement: boolean
}

/**
 * 安全配置
 */
export interface TaktSecurityConfig {
  /** 是否启用登录限制（对应后端 EnableLoginRestriction） */
  enableLoginRestriction: boolean
  /** 是否启用验证码（对应后端 Captcha:Enabled） */
  enableCaptcha: boolean
  /** 密码最小长度（对应后端 PasswordPolicy:MinLength） */
  passwordMinLength: number
  /** 密码复杂度要求（对应后端 PasswordPolicy 相关配置） */
  passwordComplexity: {
    requireUppercase: boolean
    requireLowercase: boolean
    requireNumbers: boolean
    requireSpecialChars: boolean
  }
  /** 登录失败锁定次数（对应后端 LoginPolicy:MaxFailedAttempts） */
  loginFailLockCount: number
  /** 登录失败锁定时间（分钟）（对应后端 LoginPolicy:LockoutMinutes） */
  loginFailLockTime: number
  /** 需要验证码的失败次数阈值（对应后端 LoginPolicy:CaptchaRequiredAttempts） */
  captchaRequiredAttempts: number
  /** 验证码有效期（分钟）（对应后端 LoginPolicy:CaptchaRequiredMinutes） */
  captchaRequiredMinutes: number
  /** 重复登录检测时间窗口（分钟）（对应后端 LoginPolicy:RepeatLoginMinutes） */
  repeatLoginMinutes: number
}

/**
 * 主题配置
 */
export interface TaktThemeConfig {
  /** 主题模式 */
  mode: 'light' | 'dark' | 'auto'
  /** 主色调 */
  primaryColor: string
  /** 是否启用动画 */
  enableAnimation: boolean
  /** 是否启用紧凑模式 */
  compact: boolean
}

/**
 * Logo配置
 */
export interface TaktLogoConfig {
  /** Logo图片路径 */
  src: string
  /** Logo图片alt属性 */
  alt: string
  /** Logo宽度 */
  width: number
  /** Logo高度 */
  height: number
  /** 是否显示文字 */
  showText: boolean
  /** Logo文字 */
  text: string
  /** 文字大小 */
  textSize: number
  /** 文字粗细 */
  textWeight: number
}

/**
 * 应用全局配置
 */
export interface TaktAppConfig {
  /** 水印配置 */
  watermark: TaktWatermarkConfig
  /** 注册配置 */
  register: TaktRegisterConfig
  /** 功能配置 */
  features: TaktFeatureConfig
  /** 安全配置 */
  security: TaktSecurityConfig
  /** 主题配置 */
  theme: TaktThemeConfig
  /** 系统标题 */
  title: string
  /** 系统副标题 */
  subtitle: string
  /** 系统Logo配置 */
  logo: TaktLogoConfig
  /** 版权信息（可选，使用国际化文件） */
  copyright?: string
  /** 版本号（可选，从后端获取） */
  version?: string
}

/**
 * 配置更新请求
 */
export interface TaktConfigUpdateRequest {
  /** 配置键 */
  key: string
  /** 配置值 */
  value: any
}

/**
 * 配置响应
 */
export interface TaktConfigResponse {
  /** 响应码 */
  code: number
  /** 响应消息 */
  message: string
  /** 配置数据 */
  data: TaktAppConfig
} 
