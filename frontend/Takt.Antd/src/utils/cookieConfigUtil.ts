import Cookies from 'js-cookie'

// Cookie配置常量
export const COOKIE_KEYS = {
  ACCESS_TOKEN: 'access_token',
  REFRESH_TOKEN: 'refresh_token',
  CSRF_TOKEN: 'XSRF-TOKEN',
  LAST_LOGIN_TIME: 'lastLoginTime',
  LOGIN_FAIL_COUNT: 'loginFailCount'
} as const

// 检测是否为开发环境
const isDevelopment = import.meta.env.DEV

// 长期Cookie配置（7天）
export const LONG_COOKIE_OPTIONS = {
  expires: 7,
  secure: !isDevelopment, // 开发环境不使用HTTPS要求
  sameSite: isDevelopment ? 'lax' as const : 'strict' as const, // 开发环境使用Lax模式
  path: '/',
  httpOnly: false // 前端无法设置 HttpOnly，由后端设置
}

// 短期Cookie配置（30分钟）
export const SHORT_COOKIE_OPTIONS = {
  expires: 1/48, // 30分钟 (1/48天)
  secure: !isDevelopment, // 开发环境不使用HTTPS要求
  sameSite: isDevelopment ? 'lax' as const : 'strict' as const, // 开发环境使用Lax模式
  path: '/',
  httpOnly: false // 前端无法设置 HttpOnly，由后端设置
}

// 临时Cookie配置（1天）
export const TEMP_COOKIE_OPTIONS = {
  expires: 1,
  secure: !isDevelopment, // 开发环境不使用HTTPS要求
  sameSite: isDevelopment ? 'lax' as const : 'strict' as const, // 开发环境使用Lax模式
  path: '/'
}

// Cookie工具函数
export const cookieUtils = {
  // 设置Cookie
  set: (key: string, value: string, options?: Cookies.CookieAttributes) => {
    try {
      Cookies.set(key, value, options || LONG_COOKIE_OPTIONS)
      return true
    } catch (error) {
      console.error(`[Cookie] 设置Cookie失败 ${key}:`, error)
      return false
    }
  },

  // 获取Cookie
  get: (key: string): string | undefined => {
    try {
      return Cookies.get(key)
    } catch (error) {
      console.error(`[Cookie] 获取Cookie失败 ${key}:`, error)
      return undefined
    }
  },

  // 移除Cookie
  remove: (key: string, options?: Cookies.CookieAttributes) => {
    try {
      Cookies.remove(key, options || { path: '/' })
      return true
    } catch (error) {
      console.error(`[Cookie] 移除Cookie失败 ${key}:`, error)
      return false
    }
  },

  // 检查Cookie是否存在
  exists: (key: string): boolean => {
    try {
      return Cookies.get(key) !== undefined
    } catch (error) {
      console.error(`[Cookie] 检查Cookie失败 ${key}:`, error)
      return false
    }
  }
}
