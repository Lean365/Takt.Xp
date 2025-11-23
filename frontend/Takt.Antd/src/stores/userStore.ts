import { defineStore } from 'pinia'
import type { MenuProps } from 'ant-design-vue'
import type { UserInfo, LoginResultData } from '@/types/identity/auth.d'
import { login as userLogin, logout as userLogout, getInfo as fetchUserInfo, refreshUserToken } from '@/api/identity/auth/auth'
import { removeToken, removeRefreshToken, setToken, setRefreshToken, getToken, getRefreshToken } from '@/utils/authUtil'
import { ref, computed } from 'vue'
import { clearAutoLogout } from '@/utils/autoLogoutUtil'
import { maskName } from '@/utils/maskUtil'
import Cookies from 'js-cookie'
import { COOKIE_KEYS, LONG_COOKIE_OPTIONS, TEMP_COOKIE_OPTIONS, cookieUtils } from '@/utils/cookieConfigUtil'
import { signalRService } from '@/utils/SignalR/service'

// 扩展UserInfo类型以包含额外的字段
export interface UserInfoResponse extends UserInfo {
  menus?: MenuProps['items']
}

export const useUserStore = defineStore('user', () => {
  // 用户信息
  const userInfo = ref<UserInfoResponse>()
  // 是否需要验证码
  const needCaptcha = ref<boolean>(false)
  // 最后登录时间
  const lastLoginTime = ref<string>('')
  // 登录失败次数
  const loginFailCount = ref<number>(0)
  // 用户信息是否已加载
  const isUserInfoLoaded = ref<boolean>(false)
  // 防止并发刷新
  const isRefreshing = ref<boolean>(false)

  // 获取用户信息
  const getUserInfo = async (forceRefresh = false) => {
    try {
      // 检查 JWT Bearer Token
      const token = getToken()
      if (!token) {
        return null
      }

      // 如果不是强制刷新，且有缓存数据，则使用缓存
      if (!forceRefresh && isUserInfoLoaded.value && userInfo.value) {
        return userInfo.value
      }

      const response = await fetchUserInfo()
      
      // 添加调试日志
      //console.log('[UserStore] 获取用户信息响应:', response)
      console.log('[UserStore] response.code:', response?.code)
      console.log('[UserStore] response.data:', response?.data)
      
      if (response && response.code === 200 && response.data) {
        const userData = response.data as UserInfoResponse
        
        userInfo.value = userData
        isUserInfoLoaded.value = true
        
        return userInfo.value
      }
      
      throw new Error(response?.msg || '获取用户信息失败')
    } catch (error) {
      console.error('[User] 获取用户信息失败:', error)
      // 只有在强制刷新（登录）时才清除token
      if (forceRefresh) {
        removeToken()
        removeRefreshToken()
      }
      return null
    }
  }

  // 设置是否需要验证码
  const setNeedCaptcha = (value: boolean) => {
    needCaptcha.value = value
  }

  // 记录登录时间
  const recordLoginTime = () => {
    const now = new Date().toISOString()
    lastLoginTime.value = now
    cookieUtils.set(COOKIE_KEYS.LAST_LOGIN_TIME, now, LONG_COOKIE_OPTIONS)
  }

  // 重置登录失败次数
  const resetLoginFailCount = () => {
    loginFailCount.value = 0
    cookieUtils.remove(COOKIE_KEYS.LOGIN_FAIL_COUNT)
  }

  // 增加登录失败次数
  const incrementLoginFailCount = () => {
    loginFailCount.value++
    cookieUtils.set(COOKIE_KEYS.LOGIN_FAIL_COUNT, loginFailCount.value.toString(), TEMP_COOKIE_OPTIONS)
    // 如果失败次数达到阈值，需要验证码
    if (loginFailCount.value >= 3) {
      setNeedCaptcha(true)
    }
  }

  // 登录
  const login = async (loginData: any) => {
    try {
      // 登录前清除所有缓存
      clearUserInfo()
      
      const response = await userLogin(loginData)
      
      // 添加调试日志
      console.log('[UserStore] 登录响应:', response)
      console.log('[UserStore] response.code:', response?.code)
      console.log('[UserStore] response.data:', response?.data)
      
      if (response && response.code === 200 && response.data) {
        const loginResult = response.data as LoginResultData
          
          // 保存 JWT Bearer Token 到 localStorage
          setToken(loginResult.accessToken)
          setRefreshToken(loginResult.refreshToken)
        
        // 强制从后端获取最新用户信息
        const userInfo = await getUserInfo(true)
        if (!userInfo) {
          throw new Error('获取用户信息失败')
        }
        
        // 记录登录时间
        recordLoginTime()
        // 重置登录失败次数
        resetLoginFailCount()
        
        return response
      }
      
      // 登录失败，增加失败次数
      incrementLoginFailCount()
      throw new Error(response?.msg || '登录失败')
    } catch (error) {
      console.error('[User] 登录失败:', error)
        // 登录失败时清除所有数据
        clearUserInfo()
        // 清除 JWT Bearer Token
        removeToken()
        removeRefreshToken()
      throw error
    }
  }

  // 清除用户信息
  const clearUserInfo = () => {
    userInfo.value = undefined
    isUserInfoLoaded.value = false
  }

  // 登出
  const logout = async (callApi = true) => {
    try {
      // 先停止 SignalR 连接
      try {
        //console.log('[UserStore] 开始停止 SignalR 连接')
        await signalRService.stop()
        console.log('[UserStore] SignalR 连接已停止')
      } catch (error) {
        console.error('[UserStore] 停止 SignalR 连接失败:', error)
      }
      
      if (callApi) {
        await userLogout()
      }
        // 清除用户信息
        clearUserInfo()
        // 清除 JWT Bearer Token
        removeToken()
        removeRefreshToken()
      // 清除登录时间
      lastLoginTime.value = ''
      // 重置登录失败次数
      resetLoginFailCount()
      // 清理自动登出
      clearAutoLogout()
    } catch (error) {
      console.error('登出失败:', error)
      // 即使 API 调用失败，也要清除本地状态
      clearUserInfo()
      removeToken()
      removeRefreshToken()
      lastLoginTime.value = ''
      resetLoginFailCount()
      clearAutoLogout()
    }
  }

  // 刷新Token
  const refreshToken = async () => {
    try {
      const refreshTokenValue = getRefreshToken()
      if (!refreshTokenValue) {
        console.log('[UserStore] 没有刷新Token，无法刷新')
        return false
      }

      console.log('[UserStore] 开始刷新Token')
      const response = await refreshUserToken(refreshTokenValue)
      
      if (response && response.code === 200 && response.data) {
        const result = response.data
        setToken(result.accessToken)
        if (result.refreshToken) {
          setRefreshToken(result.refreshToken)
        }
        console.log('[UserStore] Token刷新成功')
        return true
      } else {
        console.log('[UserStore] Token刷新失败')
        return false
      }
    } catch (error) {
      console.error('[UserStore] Token刷新异常:', error)
      return false
    }
  }

  return {
    userInfo,
    needCaptcha,
    lastLoginTime,
    loginFailCount,
    isUserInfoLoaded,
    getUserInfo,
    setNeedCaptcha,
    recordLoginTime,
    resetLoginFailCount,
    incrementLoginFailCount,
    login,
    logout,
    refreshToken,
    clearUserInfo,
    permissions: computed(() => userInfo.value?.permissions || []),
    roles: computed(() => userInfo.value?.roles || []),
    userId: computed(() => userInfo.value?.userId || ''),
    isLoggedIn: computed(() => !!userInfo.value && !!getToken())
  }
}) 