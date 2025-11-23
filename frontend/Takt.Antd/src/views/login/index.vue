<template>
  <div class="login-container">
    <Takt-header-login />

    <div class="login-content">
      <!-- Left Brand Display Card -->
      <a-card class="brand-card" :bordered="false">
        <div class="brand-content">
          <img :src="defaultConfig.logo.src" :alt="defaultConfig.logo.alt" class="brand-logo" />
          <h1 class="brand-title">{{ defaultConfig.title }}</h1>
          <p class="brand-slogan">{{ t('menu.logistics.quality.basic.category') }}</p>
        </div>
      </a-card>

      <!-- Right Login Card -->
      <a-card class="login-card" :bordered="false">
        <h2 class="login-title">{{ t('identity.auth.login.title') }}</h2>
        

        
        <!-- 登录方式选择已移除，仅支持密码登录 -->
        

        
        <!-- Password Login Form -->
        <div class="login-form-container">
          <a-form
            :model="loginForm"
            :rules="loginRules"
            ref="loginFormRef"
            @finish="handleLogin"
          >
            <a-form-item name="userName">
              <a-input
                v-model:value="loginForm.userName"
                :placeholder="t('identity.auth.fields.username.placeholder')"
                class="login-input"
                autocomplete="username"
                @change="handleUserNameChange"
              >
                <template #prefix>
                  <user-outlined v-icon-random="'login-user'" />
                </template>
              </a-input>
            </a-form-item>
            <a-form-item name="password">
              <a-input-password
                v-model:value="loginForm.password"
                :placeholder="t('identity.auth.fields.password.placeholder')"
                class="login-input"
                autocomplete="current-password"
              >
                <template #prefix>
                  <lock-outlined v-icon-random="'login-password'" />
                </template>
              </a-input-password>
            </a-form-item>

            <a-form-item>
              <a-button
                type="primary"
                html-type="submit"
                class="login-button"
                :loading="loading"
                :disabled="!isLoginButtonEnabled"
                block
              >
                {{ 
                  !allConfigsLoaded 
                    ? t('identity.auth.config.loading') 
                    : t('identity.auth.login.submit') 
                }}
              </a-button>
              <!-- Configuration Loading Status Tip -->
              <div v-if="!allConfigsLoaded" class="config-loading-tip">
                <a-spin size="small" />
                <span style="margin-left: 8px; color: #666; font-size: 12px;">
                  {{ t('identity.auth.config.loadingLoginConfig') }}
                </span>
              </div>
            </a-form-item>
          </a-form>
          <div class="login-options">
            <a-checkbox
              v-model:checked="rememberMe"
              class="remember-me"
            >
              {{ t('identity.auth.login.rememberMe') }}
            </a-checkbox>
            <a class="forgot-password" @click="showPasswordRecovery">
              {{ t('identity.auth.login.forgotPassword') }}
            </a>
            <a class="device-fingerprint" @click="showDeviceFingerprint">
              {{ t('identity.auth.device.collectionComponent.title') }}
            </a>
          </div>
        </div>

        <!-- SMS和OAuth登录组件已移除 -->
        

        
        <!-- Registration Link (Show/Hide based on backend configuration) -->
        <div class="register-link" v-if="settingsStore.registerEnabled">
          <span>{{ t('identity.auth.login.noAccount') }}</span>
          <a @click="handleShowRegisterModal">
            {{ t('identity.auth.login.registerNow') }}
          </a>
        </div>
        
        <!-- 配置不一致错误提示 -->
        <div v-if="showConfigError" class="config-error-message">
          <a-alert
            message="配置不一致"
            description="前端和后端的注册配置不一致，请联系管理员检查配置文件。"
            type="error"
            show-icon
            closable
            @close="showConfigError = false"
          />
        </div>
      </a-card>
    </div>
    
    <!-- Captcha Modal -->
    <a-modal
      v-model:open="showCaptcha"
      :title="t('identity.auth.captcha.title')"
      :footer="null"
      :maskClosable="false"
      :closable="false"
      width="420px"
      centered
    >
      <div class="captcha-modal-content">
        <TaktSliderCaptcha
          v-if="captchaType === 'Slider'"
          ref="captchaRef"
          v-model:captcha-token="loginForm.captchaToken"
          v-model:captcha-offset="loginForm.captchaOffset"
          @refresh="refreshCaptcha"
          :loading="captchaLoading"
          @success="handleCaptchaSuccess"
          @error="handleCaptchaError"
        />
        <TaktBehaviorCaptcha
          v-else-if="captchaType === 'Behavior'"
          ref="captchaRef"
          v-model:captcha-token="loginForm.captchaToken"
          v-model:captcha-score="loginForm.captchaOffset"
          @refresh="refreshCaptcha"
          :loading="captchaLoading"
          @success="handleCaptchaSuccess"
          @error="handleCaptchaError"
        />
      </div>
    </a-modal>

    <!-- 找回密码弹窗 -->
    <Takt-modal
      :open="showPasswordRecoveryModal"
      :title="t('identity.auth.passwordRecovery.title')"
      :footer="false"
      :mask-closable="false"
      :width="600"
      :centered="true"
      @update:open="handlePasswordRecoveryCancel"
      @cancel="handlePasswordRecoveryCancel"
    >
      <ForgetPwd
        ref="passwordRecoveryRef"
        @switch-to-login="handleSwitchToLogin"
        @recovery-success="handleRecoverySuccess"
      />
    </Takt-modal>

    <!-- 注册弹窗 -->
    <Takt-modal
      :open="showRegisterModal"
      :title="t('identity.auth.register.title')"
      :footer="false"
      :mask-closable="false"
      :width="600"
      :centered="true"
      @update:open="handleRegisterModalCancel"
      @cancel="handleRegisterModalCancel"
    >
      <UserRegistration
        ref="userRegistrationRef"
        @switch-to-login="handleRegisterModalCancel"
        @registration-success="handleRegisterSuccess"
      />
    </Takt-modal>

    <!-- 设备指纹弹窗 -->
    <Takt-modal
      :open="showDeviceFingerprintModal"
      :title="t('identity.auth.device.collectionComponent.title')"
      :footer="false"
      :mask-closable="false"
      :width="800"
      :centered="true"
      @update:open="handleDeviceFingerprintCancel"
      @cancel="handleDeviceFingerprintCancel"
    >
      <DeviceFingerprint />
    </Takt-modal>
  </div>
</template>

<script lang="ts" setup>
// 类型导入
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { LoginParams } from '@/types/identity/auth'

// API和组件导入
import { checkAccountLockout } from '@/api/identity/auth/auth'
import { getCaptchaConfig } from '@/api/security/captcha'
// 登录方式API已移除
import { checkSingleUserLogin, forceUserOffline } from '@/api/identity/auth/singleUserLogin'
// import { PasswordEncryptor } from '@/utils/cryptoUtil' // 不再需要前端加密
import { useUserStore } from '@/stores/userStore'
import { useMenuStore } from '@/stores/menuStore'
import { useAppStore } from '@/stores/appStore'
import { useSettingsStore } from '@/stores/settingsStore'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import { ref,  onMounted, onUnmounted, nextTick, watch, computed, h } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { getDeviceInfo, deviceNativeCollector } from '@/utils/loginFingerprintNativeUtil'
import { LOGIN_POLICY, LOGIN_STORAGE_KEYS, SPECIAL_USERS } from '@/types/identity/auth'
import { getDefaultAppConfig } from '@/setting'
import ForgetPwd from '@/views/login/components/ForgetPwd.vue'
import UserRegistration from '@/views/login/components/UserRegistration.vue'
import DeviceFingerprint from '@/views/login/components/LoginFingerprint.vue'
import { signalRService } from '@/utils/SignalR/service'


const { t } = useI18n()
const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const menuStore = useMenuStore()
const appStore = useAppStore()
const settingsStore = useSettingsStore()

// 获取 setting.ts 的默认配置
const defaultConfig = computed(() => getDefaultAppConfig(false))

// 表单引用
const loginFormRef = ref<FormInstance>()
const captchaRef = ref()

// 登录表单数据
const loginForm = ref({
  userName: 'admin',
  password: '123456',
  captchaToken: '',
  captchaOffset: 0,
  captchaType: '',
  loginSource: 0,
  deviceType: 'web',
  loginFingerprint: null as any
})

// 表单验证规则
const loginRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    userName: [
      { required: true, type: 'string', message: t('identity.auth.fields.username.validation.required'), trigger: 'blur' },
      { type: 'string', min: 3, max: 50, message: t('identity.auth.fields.username.validation.length'), trigger: 'blur' }
    ],
    password: [
      { required: true, type: 'string', message: t('identity.auth.fields.password.validation.required'), trigger: 'blur' },
      { type: 'string', min: 6, max: 100, message: t('identity.auth.fields.password.validation.length'), trigger: 'blur' }
    ]
  }
  return rules
})

// 是否显示验证码
const showCaptcha = ref(false)
// 记住我
const rememberMe = ref(false)
// 加载状态
const loading = ref(false)
// 验证码加载状态
const captchaLoading = ref(false)
// 是否显示找回密码弹窗
const showPasswordRecoveryModal = ref(false)
// 是否显示注册弹窗
const showRegisterModal = ref(false)
// 是否显示设备指纹弹窗
const showDeviceFingerprintModal = ref(false)
// 是否显示配置错误
const showConfigError = ref(false)

// 验证码状态
const captchaVerified = ref(false)



// 组件引用
const passwordRecoveryRef = ref()
const userRegistrationRef = ref()

// 验证码类型（完全由后端配置）
const captchaType = ref<'Slider' | 'Behavior'>('Behavior')
// 验证码是否启用（由后端配置）
const captchaEnabled = ref(true)

// 配置加载状态
const captchaConfigLoaded = ref(false)
const deviceInfoLoaded = ref(false)

// 计算属性：是否所有配置都已加载完成
const allConfigsLoaded = computed(() => {
  return captchaConfigLoaded.value && deviceInfoLoaded.value
})

// 计算属性：登录按钮是否可用
const isLoginButtonEnabled = computed(() => {
  return allConfigsLoaded.value && !loading.value
})

// 获取后端验证码配置
const loadCaptchaConfig = async () => {
  try {
    const response = await getCaptchaConfig('login')
    
    // 检查数据结构：response.data 是实际的配置数据
    const configData = response.data
    if (configData) {
      // 检查验证码是否启用
      if (configData.enabled !== undefined) {
        captchaEnabled.value = configData.enabled
      }
      
      if (configData.type) {
        captchaType.value = configData.type as 'Slider' | 'Behavior'
      }
      
      captchaConfigLoaded.value = true
      console.log(t('identity.auth.config.captchaConfigSuccess'))
    } else {
      captchaConfigLoaded.value = false
      message.error(t('identity.auth.config.captchaConfigFailed'))
    }
  } catch (error) {
    captchaConfigLoaded.value = false
    console.error(t('identity.auth.config.captchaConfigError'), error)
    message.error(t('identity.auth.config.captchaConfigFailed'))
  }
}

// 登录方式配置已移除，仅支持密码登录



// 监听用户名变化
const handleUserNameChange = async (e: Event) => {
  const value = (e.target as HTMLInputElement).value;
}

// 刷新验证码
const refreshCaptcha = async () => {
  captchaLoading.value = true
  try {
    // 重置验证码相关状态
    loginForm.value.captchaToken = ''
    loginForm.value.captchaOffset = 0
    captchaVerified.value = false
    
    // 验证码组件会自动调用API获取新的验证码
    console.log(t('identity.auth.captchaComponent.refreshSuccess'))
  } catch (error) {
    console.error(t('identity.auth.captchaComponent.refreshFailed'), error)
    message.error(t('identity.auth.captchaComponent.getFailed'))
  } finally {
    captchaLoading.value = false
  }
}



// 获取失败次数
const getFailedAttempts = (username: string) => {
  const storedAttempts = localStorage.getItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS)
  if (!storedAttempts) return 0
  
  const attempts = JSON.parse(storedAttempts)
  return attempts[username] || 0
}

// 设置失败次数
const setFailedAttempts = (username: string, count: number) => {
  const attempts = JSON.parse(localStorage.getItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS) || '{}')
  attempts[username] = count
  localStorage.setItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS, JSON.stringify(attempts))
}

// 重置失败次数
const resetFailedAttempts = (username: string) => {
  const attempts = JSON.parse(localStorage.getItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS) || '{}')
  delete attempts[username]
  localStorage.setItem(LOGIN_STORAGE_KEYS.FAILED_ATTEMPTS, JSON.stringify(attempts))
}

// 在文件顶部添加 isAdmin 变量定义
const isAdmin = computed(() => loginForm.value.userName.toLowerCase() === SPECIAL_USERS.ADMIN)

// 检查是否需要显示验证码（完全依赖后端配置）
const needShowCaptcha = computed(() => {
  return captchaEnabled.value
})

// 处理登录
const handleLogin = async () => {
  try {
    loading.value = true
    
    // 验证表单
    if (!loginForm.value.userName) {
      message.error(t('identity.auth.fields.username.validation.required'))
      loading.value = false
      return
    }
    
    // 检查是否需要验证码（只根据配置判断）
    if (needShowCaptcha.value && !captchaVerified.value) {
      showCaptcha.value = true
      loading.value = false
      return
    }
    
    // 验证凭证
    const isValid = await validateCredentials()
    if (!isValid) {
      loading.value = false
      return
    }
    
    // 1. 直接发送用户名和密码，由后端进行验证
    const password = loginForm.value.password

    // 2. 获取设备指纹信息和设备类型
    const deviceInfo = await getDeviceInfo()
    console.log(t('identity.auth.device.getDeviceInfo'), deviceInfo)
    console.log(t('identity.auth.device.deviceFingerprintType'), typeof deviceInfo)
    console.log(t('identity.auth.device.deviceFingerprintNull'), deviceInfo === null)
    console.log(t('identity.auth.device.deviceFingerprintUndefined'), deviceInfo === undefined)
    console.log(t('identity.auth.device.deviceFingerprintKeyCount'), deviceInfo ? Object.keys(deviceInfo).length : 0)
    if (deviceInfo) {
      console.log(t('identity.auth.device.deviceFingerprintKeyList'), Object.keys(deviceInfo))
      console.log(t('identity.auth.device.deviceFingerprintField'), deviceInfo.loginFingerprint)
    }
    
    // 确保所有参数都有有效值
    const loginParams: LoginParams = {
      userName: loginForm.value.userName || '',
      password: password || '',
      captchaToken: loginForm.value.captchaToken || '',
      captchaOffset: loginForm.value.captchaOffset || 0,
      captchaType: loginForm.value.captchaType || '',
      loginIp: '', // 后端自动获取客户端IP
      userAgent: navigator.userAgent || '',
      loginType: 1, // 密码登录 (Password = 1)
      loginSource: 0, // Web登录
      deviceType: deviceNativeCollector.getDeviceType() || 'Desktop', // 使用专门的设备类型检测方法，默认桌面设备
      loginFingerprint: deviceInfo || null
    }

    // 详细记录每个参数的值
    console.log(t('identity.auth.device.loginParamsDetail'), {
      userName: loginParams.userName,
      password: loginParams.password ? `${loginParams.password.substring(0, 8)}...` : t('identity.auth.device.empty'),
      captchaToken: loginParams.captchaToken || t('identity.auth.device.empty'),
      captchaOffset: loginParams.captchaOffset,
      captchaType: loginParams.captchaType || t('identity.auth.device.empty'),
      loginIp: loginParams.loginIp || t('identity.auth.device.backendHandled'),
      userAgent: loginParams.userAgent ? `${loginParams.userAgent.substring(0, 50)}...` : t('identity.auth.device.empty'),
      loginType: loginParams.loginType,
      loginSource: loginParams.loginSource,
      deviceInfo: loginParams.loginFingerprint ? t('identity.auth.device.set') : t('identity.auth.device.empty')
    })
    
    // 详细记录设备指纹信息
    console.log(t('identity.auth.device.deviceFingerprintDetail'), {
      loginFingerprint: loginParams.loginFingerprint,
      loginFingerprintType: typeof loginParams.loginFingerprint,
      loginFingerprintKeys: loginParams.loginFingerprint ? Object.keys(loginParams.loginFingerprint) : [],
      loginFingerprintLength: loginParams.loginFingerprint ? Object.keys(loginParams.loginFingerprint).length : 0,
      loginFingerprintContent: loginParams.loginFingerprint ? JSON.stringify(loginParams.loginFingerprint, null, 2) : 'null'
    })
    
    // 验证关键参数
    if (!loginParams.userName || !loginParams.password) {
      throw new Error(t('identity.auth.validation.usernamePasswordRequired'))
    }
    
    if (!loginParams.loginFingerprint || Object.keys(loginParams.loginFingerprint).length === 0) {
      throw new Error(t('identity.auth.validation.deviceInfoRequired'))
    }
    
    // 验证设备信息的关键字段
    if (!loginParams.loginFingerprint.loginFingerprint) {
      throw new Error(t('identity.auth.validation.deviceFingerprintRequired'))
    }
    
    // 验证用户代理
    if (!loginParams.userAgent || loginParams.userAgent.trim() === '') {
      throw new Error(t('identity.auth.validation.userAgentRequired'))
    }
    
    // 验证登录类型和来源
    if (loginParams.loginType === undefined || loginParams.loginSource === undefined) {
      throw new Error(t('identity.auth.validation.loginTypeSourceRequired'))
    }
    
    // 记录序列化后的参数（用于调试）
    try {
      const serializedParams = JSON.stringify(loginParams)
      console.log(t('identity.auth.loginFlow.paramsSerialized'), serializedParams.length)
      console.log(t('identity.auth.loginFlow.paramsPreview'), serializedParams.substring(0, 200) + '...')
      
      // 检查参数大小，如果过大则警告
      if (serializedParams.length > 10000) {
        console.warn(t('identity.auth.loginFlow.paramsTooLarge'), serializedParams.length)
      }
    } catch (error) {
      console.error(t('identity.auth.loginFlow.serializationFailed'), error)
    }

    // 5. 发起登录请求
    const loginResponse = await userStore.login(loginParams)
    
    // 6. 登录成功后的处理
    message.success(t('identity.auth.login.success'))
    
    // 登录成功后的处理
    await handleLoginSuccess()
  } catch (error: any) {
    console.error(t('identity.auth.loginFlow.loginFailed'), error)
    
    // 改进错误处理
    let errorMessage = error.message || t('identity.auth.login.error.unknown')
    
    // 处理特定的错误类型
    if (error.response?.data?.msg) {
      errorMessage = error.response.data.msg
    } else if (error.response?.status === 401) {
      errorMessage = t('identity.auth.login.error.invalidCredentials')
    } else if (error.response?.status === 403) {
      errorMessage = t('identity.auth.login.error.userDisabled')
    } else if (error.response?.status === 404) {
      errorMessage = t('identity.auth.login.error.userNotFound')
    } else if (error.response?.status >= 500) {
      errorMessage = t('identity.auth.login.error.serverError')
    }
    
    message.error(errorMessage)
  } finally {
    loading.value = false
  }
}

// 登录成功处理
const handleLoginSuccess = async () => {
  // 记录登录时间
  await userStore.recordLoginTime()
  
  // 重置失败次数
  await userStore.resetLoginFailCount()
  
  // 重置验证码状态
  captchaVerified.value = false
  loginForm.value.captchaToken = ''
  loginForm.value.captchaOffset = 0
  loginForm.value.captchaType = ''
  
  // 开始加载菜单
  await menuStore.reloadMenus(router)
  
  // 等待路由更新完成
  await nextTick()
  
  // 翻译已通过 locales/index.ts 自动初始化
  
  // 准备跳转到首页
  const targetPath = route.query.redirect as string || '/home'
  await router.push(targetPath)
  
  // 登录成功后初始化 SignalR 连接
  try {
    console.log(t('identity.auth.loginFlow.signalRInit'))
    await signalRService.start()
    console.log(t('identity.auth.loginFlow.signalRInitSuccess'))
  } catch (error) {
    console.error(t('identity.auth.loginFlow.signalRInitFailed'), error)
    // 不阻止用户继续使用，只记录错误
  }
  
  // 登录成功后重新初始化自动登出功能
  try {
    console.log(t('identity.auth.loginFlow.autoLogoutInit'))
    const { initAutoLogout } = await import('@/utils/autoLogoutUtil')
    initAutoLogout(userStore, true) // 强制重新初始化
    console.log(t('identity.auth.loginFlow.autoLogoutInitSuccess'))
  } catch (error) {
    console.error(t('identity.auth.loginFlow.autoLogoutInitFailed'), error)
    // 不阻止用户继续使用，只记录错误
  }
}

// 处理登录错误
const handleLoginError = (error: any) => {
  const username = loginForm.value.userName
  const failedAttempts = getFailedAttempts(username) + 1
  setFailedAttempts(username, failedAttempts)
  
  const isAdminUser = username.toLowerCase() === SPECIAL_USERS.ADMIN
  const maxAttempts = isAdminUser ? LOGIN_POLICY.ADMIN.MAX_ATTEMPTS : LOGIN_POLICY.USER.MAX_ATTEMPTS
  
  // 检查是否达到最大尝试次数
  if (failedAttempts > maxAttempts) {
    // 锁定账号
    if (isAdminUser) {
      message.error(t('identity.auth.error.adminLocked', { minutes: LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES }))
    } else {
      message.error(t('identity.auth.error.userDisabled', { days: LOGIN_POLICY.USER.LOCKOUT_DAYS }))
    }
    return
  }
  
  // 显示剩余尝试次数
  const remainingAttempts = maxAttempts - failedAttempts
  message.error(t('identity.auth.error.remainingAttempts', { count: remainingAttempts }))
}

// 验证用户名密码
const validateCredentials = async () => {
  try {
    const username = loginForm.value.userName
    
    // 检查账号是否被锁定
    const response = await checkAccountLockout(username)
    const lockStatus = response.data as unknown as number
    
    if (lockStatus === 2) {
      // 永久锁定
      message.error(t('identity.auth.error.permanentlyLocked'))
      return false
    } else if (lockStatus === 1) {
      // 临时锁定30分钟
      const isAdmin = username.toLowerCase() === SPECIAL_USERS.ADMIN
      const minutes = isAdmin ? LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES : 30
      message.error(t('identity.auth.error.temporarilyLocked', { minutes }))
      return false
    }
    
    // 检查失败次数
    const failedAttempts = getFailedAttempts(username)
    const isAdminUser = username.toLowerCase() === SPECIAL_USERS.ADMIN
    const maxAttempts = isAdminUser ? LOGIN_POLICY.ADMIN.MAX_ATTEMPTS : LOGIN_POLICY.USER.MAX_ATTEMPTS
    
    if (failedAttempts >= maxAttempts) {
      message.error(t('identity.auth.error.tooManyAttempts'))
      return false
    }
    
    return true
  } catch (error) {
    handleLoginError(error)
    return false
  }
}

// 生成设备ID
const generateDeviceId = () => {
  // 生成一个基于浏览器指纹的唯一设备ID
  const canvas = document.createElement('canvas')
  const ctx = canvas.getContext('2d')
  if (ctx) {
    ctx.textBaseline = 'top'
    ctx.font = '14px Arial'
    ctx.fillText('Device fingerprint', 2, 2)
    const fingerprint = canvas.toDataURL()
    return btoa(fingerprint + navigator.userAgent + screen.width + screen.height + new Date().getTime())
  }
  return 'device-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9)
}

// 显示单一用户登录对话框
const showSingleUserLoginDialog = async (checkResult: any) => {
  return new Promise<boolean>((resolve) => {
    Modal.confirm({
      title: t('identity.auth.loginFlow.singleUserCheck.title'),
      content: t('identity.auth.loginFlow.singleUserCheck.content', { onlineDeviceCount: checkResult.onlineDeviceCount, reason: checkResult.reason || '' }),
      okText: t('identity.auth.loginFlow.singleUserCheck.kickout'),
      cancelText: t('identity.auth.loginFlow.singleUserCheck.cancel'),
      onOk: () => resolve(true),
      onCancel: () => resolve(false)
    })
  })
}

// 处理验证码成功
const handleCaptchaSuccess = (params: { token: string; xOffset: number }) => {
  try {
    console.log(t('identity.auth.captchaComponent.verifySuccess'), params)
    
    // 验证参数
    if (!params || !params.token) {
      console.error(t('identity.auth.captchaComponent.invalidParams'), params)
      message.error(t('identity.auth.captchaComponent.invalidParams'))
      return
    }
    
    // 更新表单数据
    loginForm.value.captchaToken = params.token
    loginForm.value.captchaOffset = params.xOffset || 0
    loginForm.value.captchaType = captchaType.value // 设置验证码类型
    
    // 设置验证码为已验证
    captchaVerified.value = true
    
    // 关闭验证码弹窗
    showCaptcha.value = false
    
    // 继续登录流程
    handleLogin()
    
    console.log(t('identity.auth.captchaComponent.statusUpdated'), {
      token: loginForm.value.captchaToken ? `${loginForm.value.captchaToken.substring(0, 8)}****${loginForm.value.captchaToken.substring(loginForm.value.captchaToken.length - 8)}` : '',
      offset: loginForm.value.captchaOffset,
      type: loginForm.value.captchaType,
      verified: captchaVerified.value
    })
  } catch (error) {
    console.error(t('identity.auth.captchaComponent.processError'), error)
    message.error(t('identity.auth.captchaComponent.processFailed'))
    captchaVerified.value = false
  }
}

// 处理验证码错误
const handleCaptchaError = (error: any) => {
  try {
    console.log(t('identity.auth.captchaComponent.verifyFailed'), error)
    captchaVerified.value = false
    loginForm.value.captchaToken = ''
    loginForm.value.captchaOffset = 0
    loginForm.value.captchaType = ''
    message.error(t('identity.auth.captchaComponent.error'))
  } catch (err) {
    console.error(t('identity.auth.captchaComponent.errorProcessFailed'), err)
    message.error(t('identity.auth.captchaComponent.errorProcessFailed'))
  }
}

// 显示找回密码弹窗
const showPasswordRecovery = () => {
  showPasswordRecoveryModal.value = true
}

// 处理找回密码弹窗取消
const handlePasswordRecoveryCancel = () => {
  showPasswordRecoveryModal.value = false
  // 重置找回密码组件状态
  if (passwordRecoveryRef.value?.resetAllStates) {
    passwordRecoveryRef.value.resetAllStates()
  }
}

// 处理切换到登录
const handleSwitchToLogin = () => {
  showPasswordRecoveryModal.value = false
  // 重置找回密码组件状态
  if (passwordRecoveryRef.value?.resetAllStates) {
    passwordRecoveryRef.value.resetAllStates()
  }
}

// 处理找回密码成功
const handleRecoverySuccess = (userName: string) => {
  showPasswordRecoveryModal.value = false
  // 重置找回密码组件状态
  if (passwordRecoveryRef.value?.resetAllStates) {
    passwordRecoveryRef.value.resetAllStates()
  }
  message.success(t('identity.auth.passwordRecovery.successMessage', { userName }))
}

// 显示注册弹窗
const handleShowRegisterModal = () => {
  showRegisterModal.value = true
}

// 处理注册弹窗取消
const handleRegisterModalCancel = () => {
  showRegisterModal.value = false
  // 重置注册组件状态
  if (userRegistrationRef.value?.resetAllStates) {
    userRegistrationRef.value.resetAllStates()
  }
}

// 处理注册成功
const handleRegisterSuccess = (userName: string) => {
  showRegisterModal.value = false
  // 重置注册组件状态
  if (userRegistrationRef.value?.resetAllStates) {
    userRegistrationRef.value.resetAllStates()
  }
  message.success(t('identity.auth.register.successMessage', { userName }))
}

// 处理忘记密码（保留原有函数名以兼容）
const handleForgotPassword = () => {
  showPasswordRecovery()
}

// OAuth登录功能已移除

// 显示设备指纹弹窗
const showDeviceFingerprint = () => {
  showDeviceFingerprintModal.value = true
}

// 处理设备指纹弹窗取消
const handleDeviceFingerprintCancel = () => {
  showDeviceFingerprintModal.value = false
}

// 初始化设备信息
const initDeviceInfo = async () => {
  try {
    loginForm.value.loginFingerprint = await getDeviceInfo()
    deviceInfoLoaded.value = true
    console.log(t('identity.auth.device.initSuccess'))
  } catch (error) {
    deviceInfoLoaded.value = false
    console.error(t('identity.auth.device.initFailed'), error)
  }
}



// 在组件挂载时初始化设备信息
onMounted(async () => {
  try {
    // 监听配置不一致错误事件
    window.addEventListener('config-mismatch-error', (event: any) => {
      console.error('检测到配置不一致:', event.detail)
      showConfigError.value = true
      message.error('前后端配置不一致，请联系管理员')
    })
    
    // 初始化配置
    await settingsStore.initialize()
    
    // 加载后端验证码配置
    await loadCaptchaConfig()
    
    // 初始化设备信息
    await initDeviceInfo()

    // 清理登录状态
    resetFailedAttempts(loginForm.value.userName)
    userStore.setNeedCaptcha(false)
    
    // 重置验证码状态
    captchaVerified.value = false
    loginForm.value.captchaToken = ''
    loginForm.value.captchaOffset = 0
    loginForm.value.captchaType = ''
    
    // 初始化时不显示验证码弹窗
    showCaptcha.value = false

    // 如果之前记住了用户名，自动填充
    const lastUsername = localStorage.getItem('lastUsername')
    if (lastUsername) {
      loginForm.value.userName = lastUsername
      rememberMe.value = true
    }
  } catch (error) {
    console.error(t('identity.auth.loginFlow.pageInitFailed'), error)
    message.error(t('identity.auth.loginFlow.pageInitError'))
  }
})



// 监听验证码弹窗显示状态
watch(showCaptcha, async (newValue) => {
  if (newValue && captchaRef.value) {
    await nextTick()
    try {
      await captchaRef.value.initCaptcha()
    } catch (error) {
      console.error(t('identity.auth.captchaComponent.initFailed'), error)
      message.error(t('identity.auth.captcha.error.initFailed'))
    }
  }
})

// 登录方式tabs配置已移除

// 监听配置初始化完成，检查配置一致性
watch(
  () => settingsStore.initialized,
  (initialized) => {
    if (initialized) {
      // 延迟检查，确保配置已加载
      nextTick(() => {
        // 这里可以添加额外的配置一致性检查逻辑
        // 目前配置检查已经在 settingsStore 中完成
      })
    }
  }
)

// 组件卸载时清理定时器和状态
onUnmounted(() => {
  // 清理工作
})

const handleLockoutCheck = async (username: string) => {
  try {
    const response = await checkAccountLockout(username)
    const lockStatus = response.data as unknown as number
    if (lockStatus === 2) {
      message.error(t('identity.auth.error.permanentlyLocked'))
      return false
    } else if (lockStatus === 1) {
      const isAdmin = username.toLowerCase() === SPECIAL_USERS.ADMIN
      const minutes = isAdmin ? LOGIN_POLICY.ADMIN.LOCKOUT_MINUTES : 30
      message.error(t('identity.auth.error.accountLocked', { minutes }))
      return false
    }
    return true
  } catch (error) {
    console.error(t('identity.auth.loginFlow.checkLockStatusFailed'), error)
    return true // 如果检查失败，默认允许继续
  }
}
</script>

<style lang="less" scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  padding: 24px;
  background: var(--ant-color-bg-layout);
  position: relative;

  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: color-mix(in srgb, var(--ant-color-primary) 4%, transparent);
  }

  :global(.dark) & {
    background: color-mix(in srgb, var(--ant-color-bg-layout) 90%, black);
  }
}

.login-content {
  display: flex;
  align-items: stretch;
  max-width: 900px;
  width: 100%;
  position: relative;
  z-index: 1;
  background: var(--ant-color-bg-container);
  border-radius: var(--ant-border-radius-lg);
  box-shadow: 0 6px 16px -8px rgba(0, 0, 0, 0.08),
              0 9px 28px 0 rgba(0, 0, 0, 0.05),
              0 12px 48px 16px rgba(0, 0, 0, 0.03);

  :global(.dark) & {
    background:  #e6e3e3;
    border: 2px solid rgba(255, 255, 255, 0.8);
    box-shadow: 0 0 30px rgba(255, 255, 255, 0.3),    /* 近层：30px 柔和白光 */
                0 0 80px rgba(255, 255, 255, 0.2),    /* 中层：80px 扩散白光 */
                0 0 150px rgba(255, 255, 255, 0.1);   /* 远层：150px 环境白光 */
  }
}

.brand-card {
  flex: 1;
  margin: 0;
  border-right: 1px solid var(--ant-color-split);
  display: flex;
  
  :deep(.ant-card) {
    background: transparent;
    border: none;
    height: 100%;
    width: 100%;
    margin: 0;
    padding: 0;
  }

  :deep(.ant-card-body) {
    padding: 48px;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
  }
}

.brand-content {
  text-align: center;
}

.brand-logo {
  width: 120px;
  height: auto;
  margin-bottom: 24px;
}

.brand-title {
  font-size: 36px;
  font-weight: 600;
  color: var(--ant-color-text);
  margin-bottom: 16px;
}

.brand-slogan {
  font-size: 16px;
  color: var(--ant-color-text-secondary);
  line-height: 1.6;
}

.divider-line {
  display: none;
}

.login-card {
  flex: 1;
  margin: 0;
  display: flex;

  :deep(.ant-card) {
    background: transparent;
    border: none;
    width: 100%;
    margin: 0;
    padding: 0;
    box-shadow: none;
  }

  :deep(.ant-card-body) {
    padding: 32px 48px;
    width: 100%;
  }
}

.login-title {
  text-align: center;
  margin-bottom: 32px;
  font-size: 28px;
  font-weight: 600;
  color: var(--ant-color-text);
}

.login-input {
  width: 100%;

}

// 登录页面图标样式优化
:deep(.ant-input-prefix),
:deep(.ant-select-suffix) {
  .anticon {
    transition: all 0.3s ease;
    
    &:hover {
      transform: scale(1.1);
    }
  }
}

.login-button {
  width: 100%;
  height: 40px;
  font-size: 16px;
  margin-top: 8px;
}

.login-options {
  margin: 24px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;

  .remember-me {
    color: var(--ant-color-text);
  }

  .forgot-password {
    color: var(--ant-color-primary);

    &:hover {
      color: var(--ant-color-primary-hover);
    }
  }

  .device-fingerprint {
    color: var(--ant-color-primary);
    margin-left: 16px;

    &:hover {
      color: var(--ant-color-primary-hover);
    }
  }
}

.register-link {
  text-align: center;
  margin: 16px 0;
  color: var(--ant-color-text-secondary);
  
  a {
    color: var(--ant-color-primary);
    margin-left: 4px;
    
    &:hover {
      color: var(--ant-color-primary-hover);
    }
  }
}

.other-login {
  .divider {
    position: relative;
    text-align: center;
    margin: 24px 0;
    
    &::before,
    &::after {
      content: '';
      position: absolute;
      top: 50%;
      width: 45%;
      height: 1px;
      background: var(--ant-color-border-split);
    }
    
    &::before {
      left: 0;
    }
    
    &::after {
      right: 0;
    }
    
    span {
      display: inline-block;
      padding: 0 12px;
      color: var(--ant-color-text-secondary);
      background: var(--ant-color-bg-container);
    }
  }

  /* OAuth按钮样式已移除 */
    display: flex;
    justify-content: center;
    gap: 16px;

    :deep(.ant-btn) {
      color: var(--ant-color-text);

      &:hover {
        color: var(--ant-color-primary-hover);
      }
    }

    :deep(.anticon) {
      font-size: 18px;
    }
  }


.captcha-container {
  display: flex;
  gap: 8px;
  
  .captcha-input {
    flex: 1;
  }
}

.captcha-modal-content {
  padding: 16px 0;
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 120px;
}

:deep(.ant-modal-body) {
  padding: 24px;
  background: var(--ant-color-bg-container);
}

.loading-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 24px;
}

.loading-text {
  margin-top: 16px;
  color: var(--ant-color-text-secondary);
}

.config-loading-tip {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-top: 8px;
  padding: 8px;
  background: var(--ant-color-fill-quaternary);
  border-radius: var(--ant-border-radius-sm);
  border: 1px solid var(--ant-color-border);
  
  :global(.dark) & {
    background: rgba(255, 255, 255, 0.05);
    border-color: rgba(255, 255, 255, 0.1);
  }
}

.config-error-message {
  margin-top: 16px;
  
  :deep(.ant-alert) {
    border-radius: var(--ant-border-radius-sm);
  }
}
</style> 
