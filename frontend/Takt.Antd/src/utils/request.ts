import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from 'axios'
import { getToken } from '@/utils/authUtil'
import { useUserStore } from '@/stores/userStore'
import { message } from 'ant-design-vue'
import { COOKIE_KEYS, cookieUtils } from './cookieConfigUtil'
import router from '@/router'



// 日志分级系统
const log = {
  debug: import.meta.env.DEV ? console.log : () => {},
  warn: console.warn,
  error: console.error,
  info: console.info
};

// 定义业务状态码枚举
export enum ApiCode {
  SUCCESS = 200,        // 成功
  BAD_REQUEST = 400,    // 客户端错误
  UNAUTHORIZED = 401,   // 未授权
  FORBIDDEN = 403,      // 禁止访问
  NOT_FOUND = 404,      // 资源不存在
  SERVER_ERROR = 500,   // 服务器错误
  CUSTOM_ERROR = 1001   // 自定义业务错误
}

// 统一的认证错误处理方法
async function handleAuthError(code: number, skipLogout = false) {
  // 避免无限循环：只有在用户已登录时才执行登出
  const userStore = useUserStore()
  const hasUserInfo = userStore.userInfo !== undefined && userStore.userInfo !== null
  
  if (!skipLogout && hasUserInfo) {
    log.debug(`[Auth] 认证错误 ${code}，准备登出`)
    await userStore.logout(false)
    
    // 使用路由跳转到登录页面，而不是直接修改window.location
    try {
      await router.push('/login')
    } catch (error) {
      log.error(`[Auth] 路由跳转失败，使用window.location作为备用方案:`, error)
      window.location.href = '/login'
    }
  } else {
    log.debug(`[Auth] 用户未登录或跳过登出，忽略认证错误 ${code}`)
  }
}

// 定义标准业务响应结构
interface ApiResponse<T = any> {
  code: number;       // 业务状态码
  msg: string;        // 提示信息
  data: T;            // 业务数据
}

// 创建axios实例
const service: AxiosInstance = axios.create({
  baseURL: '/api',  // 使用相对路径，让Vite代理处理
  timeout: 180000, // 增加超时时间到3分钟
  withCredentials: false, // JWT Bearer Token 模式，无需携带 Cookie
  maxContentLength: Infinity, // 允许上传大文件
  maxBodyLength: Infinity,
  headers: {
    'Accept': 'application/json, text/plain, */*'
  }
})

// Token过期时间配置（单位：毫秒）
const ACCESS_TOKEN_EXPIRE_TIME = 120 * 60 * 1000 // 120分钟，与后端JWT令牌过期时间保持一致
const REFRESH_TOKEN_EXPIRE_TIME = 7 * 24 * 60 * 60 * 1000 // 7天，与后端刷新令牌过期时间保持一致
const TOKEN_REFRESH_THRESHOLD = 10 * 60 * 1000 // 10分钟内过期时刷新

// 从 cookie 中获取 CSRF 令牌
export function getCsrfToken(): string | null {
  const csrfToken = cookieUtils.get(COOKIE_KEYS.CSRF_TOKEN)
  if (csrfToken) {
    //console.log('[CSRF] 从cookie中获取到完整令牌:', csrfToken)
    return csrfToken
  }
  log.warn('[CSRF] 未在cookie中找到XSRF-TOKEN令牌')
  return null
}

// 请求拦截器
service.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    //console.log('=== 请求拦截器开始 ===')
    //console.log('请求配置:', config)

    // 设置 JWT Bearer Token
    const skipAuth = config.skipAuth
    if (!skipAuth) {
      const token = getToken()
      if (token) {
        config.headers['Authorization'] = `Bearer ${token}`
        log.debug('[Request] 使用 JWT Bearer Token 进行认证')
      } else {
        log.debug('[Request] 未找到 Token，请求将作为匿名请求处理')
      }
    } else {
      log.debug('[Request] 跳过认证')
    }

    // 只在以下情况下检查CSRF令牌：
    // 1. 当前请求是修改操作（POST/PUT/DELETE/PATCH）
    // 2. 不是跳过认证的请求
    let csrfToken = null
    const isModifyingRequest = ['POST', 'PUT', 'DELETE', 'PATCH'].includes(config.method?.toUpperCase() || '')
    
    if (!skipAuth && isModifyingRequest) {
      // 从Cookie获取CSRF令牌（用于防止CSRF攻击）
      csrfToken = getCsrfToken()
      
      if (csrfToken) {
        config.headers['X-CSRF-Token'] = csrfToken
        log.debug('[CSRF] 已设置CSRF令牌到请求头:', csrfToken.substring(0, 10) + '...')
      } else {
        log.warn('[CSRF] 未找到CSRF令牌，但后端会自动生成')
        // 不设置CSRF令牌，让后端自动生成
      }
    }
    
    // 记录完整的请求信息
    log.debug('[Request] 请求信息:', {
      url: config.url,
      method: config.method,
      hasCsrfToken: !!csrfToken
    })

    // 确保请求头中包含必要的字段
    config.headers['Accept'] = 'application/json, text/plain, */*'
    config.headers['Cache-Control'] = 'no-cache'
    config.headers['Pragma'] = 'no-cache'

    //console.log('[Request] 最终请求头:', config.headers)
    //console.log('=== 请求拦截器结束 ===')
    return config
  },
  (error: any) => {
    return Promise.reject(error);
  }
)



// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse) => {
    // 如果是 blob 响应，直接返回原始响应
    if (response.config.responseType === 'blob') {
      return response;
    }
    
    const res = response.data;
    
    // 如果业务状态码不是 200，则视为错误
    if (res.code !== 200) {
      // 处理认证错误
      if (res.code === ApiCode.UNAUTHORIZED || res.code === ApiCode.FORBIDDEN) {
        console.error('认证错误:', res.msg);
        handleAuthError(res.code).catch(error => {
          console.error('处理认证错误失败:', error);
        });
        return Promise.reject(new Error(res.msg || '认证失败'));
      }
      
      // 处理其他业务错误
      console.error('业务错误:', res.msg);
      return Promise.reject(new Error(res.msg || 'Error'));
    }
    
    // 返回标准业务结构
    return res;
  },
  (error: any) => {
    // 处理 HTTP 错误
    console.error('请求错误:', error.message);
    
    // 处理 HTTP 状态码错误
    const status = error.response?.status;
    if (status === 401 || status === 403) {
      console.error('HTTP认证错误:', status);
      handleAuthError(status).catch(error => {
        console.error('处理HTTP认证错误失败:', error);
      });
    } else if (status === 500) {
      // 对于500错误，如果是获取用户信息的请求，也当作认证错误处理
      const url = error.config?.url || '';
      if (url.includes('/info') || url.includes('/TaktAuth/info')) {
        console.error('用户信息接口500错误，当作认证错误处理:', status);
        handleAuthError(500).catch(error => {
          console.error('处理用户信息500错误失败:', error);
        });
      }
    }
    
    // 返回一个标准错误结构
    return Promise.reject({
      code: status || 500,
      msg: error.message || '请求失败',
      data: null
    });
  }
)

/**
 * 自定义的request函数，返回业务响应类型
 */
export function request<T = any>(config: AxiosRequestConfig & { responseType?: never }): Promise<ApiResponse<T>>
export function request<T = Blob>(config: AxiosRequestConfig & { responseType: 'blob' }): Promise<AxiosResponse<T>>
export function request<T = any>(config: AxiosRequestConfig): Promise<ApiResponse<T> | AxiosResponse<T>> {
  return service(config) as Promise<ApiResponse<T> | AxiosResponse<T>>
}

/**
 * GET 请求
 */
export function get<T = any>(url: string, params?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
  return service.get(url, { params, ...config }) as Promise<ApiResponse<T>>
}

/**
 * POST 请求
 */
export function post<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
  return service.post(url, data, config) as Promise<ApiResponse<T>>
}

/**
 * PUT 请求
 */
export function put<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
  return service.put(url, data, config) as Promise<ApiResponse<T>>
}

/**
 * DELETE 请求
 */
export function del<T = any>(url: string, params?: any, config?: AxiosRequestConfig): Promise<ApiResponse<T>> {
  return service.delete(url, { params, ...config }) as Promise<ApiResponse<T>>
}

export default service
