import axios from 'axios'
import { COOKIE_KEYS, SHORT_COOKIE_OPTIONS, LONG_COOKIE_OPTIONS, cookieUtils } from './cookieConfigUtil'

/**
 * 获取Token
 * 从 localStorage 获取 JWT Bearer Token
 */
export function getToken(): string | null {
  return localStorage.getItem('access_token')
}

/**
 * 设置Token
 * 将 JWT Bearer Token 保存到 localStorage
 */
export function setToken(token: string): void {
  localStorage.setItem('access_token', token)
}

/**
 * 移除Token
 * 从 localStorage 移除 JWT Bearer Token
 */
export function removeToken(): void {
  localStorage.removeItem('access_token')
}

/**
 * 获取刷新Token
 * 从 localStorage 获取刷新 Token
 */
export function getRefreshToken(): string | null {
  return localStorage.getItem('refresh_token')
}

/**
 * 设置刷新Token
 * 将刷新 Token 保存到 localStorage
 */
export function setRefreshToken(token: string): void {
  localStorage.setItem('refresh_token', token)
}

/**
 * 移除刷新Token
 * 从 localStorage 移除刷新 Token
 */
export function removeRefreshToken(): void {
  localStorage.removeItem('refresh_token')
}