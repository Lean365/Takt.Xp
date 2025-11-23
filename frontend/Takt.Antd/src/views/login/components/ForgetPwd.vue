<template>
  <div class="password-recovery-container">
    <div class="recovery-header">
      <h2>{{ t('identity.auth.passwordRecovery.title') }}</h2>
      <p>{{ t('identity.auth.passwordRecovery.subtitle') }}</p>
    </div>

    <!-- 步骤指示器 -->
    <a-steps :current="currentStep" class="recovery-steps">
      <a-step :title="t('identity.auth.passwordRecovery.step1')" />
      <a-step :title="t('identity.auth.passwordRecovery.step2')" />
      <a-step :title="t('identity.auth.passwordRecovery.step3')" />
      <a-step :title="t('identity.auth.passwordRecovery.step4')" />
    </a-steps>

    <!-- 步骤1: 验证码验证 -->
    <div v-if="currentStep === 0" class="step-content">
      <a-form
        :model="captchaForm"
        :rules="captchaRules"
        ref="captchaFormRef"
        @finish="handleCaptchaVerification"
        layout="vertical"
      >
        <a-form-item name="captcha" :label="t('identity.auth.fields.captcha.label')" v-if="showCaptcha">
          <!-- 滑块验证码 -->
          <TaktSliderCaptcha
            v-if="captchaType === 'Slider'"
            ref="captchaRef"
            v-model:captcha-token="captchaToken"
            v-model:captcha-offset="captchaForm.captcha"
            @refresh="refreshCaptcha"
            :loading="captchaLoading"
            @success="handleCaptchaSuccess"
            @error="handleCaptchaError"
          />
          
          <!-- 行为验证码 -->
          <TaktBehaviorCaptcha
            v-else-if="captchaType === 'Behavior'"
            ref="captchaRef"
            v-model:captcha-token="captchaToken"
            v-model:captcha-score="captchaForm.captcha"
            @refresh="refreshCaptcha"
            :loading="captchaLoading"
            @success="handleCaptchaSuccess"
            @error="handleCaptchaError"
          />
        </a-form-item>

        <a-form-item>
          <a-button
            type="primary"
            html-type="submit"
            size="large"
            :loading="loading"
            :disabled="!captchaValid || loading"
            block
          >
            {{ t('identity.auth.passwordRecovery.nextStep') }}
          </a-button>
        </a-form-item>
      </a-form>
    </div>

    <!-- 步骤2: 用户和邮件信息 -->
    <div v-if="currentStep === 1" class="step-content">
      <a-form
        :model="identityForm"
        :rules="identityRules"
        ref="identityFormRef"
        @finish="handleIdentityVerification"
        layout="vertical"
      >
        <a-form-item name="userName" :label="t('identity.auth.fields.username.label')">
          <a-input
            v-model:value="identityForm.userName"
            :placeholder="t('identity.auth.fields.username.placeholder')"
            size="large"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>

        <a-form-item name="email" :label="t('identity.auth.fields.email.label')">
          <a-input
            v-model:value="identityForm.email"
            :placeholder="t('identity.auth.fields.email.placeholder')"
            size="large"
            type="email"
          >
            <template #prefix>
              <mail-outlined />
            </template>
          </a-input>
        </a-form-item>

        <a-form-item>
          <div style="display: flex; justify-content: space-between;">
            <a-button
              @click="handleBack"
              size="large"
            >
              {{ t('identity.auth.passwordRecovery.back') }}
            </a-button>
            <a-button
              type="primary"
              html-type="submit"
              size="large"
              :loading="loading"
              :disabled="!identityValid || loading"
            >
              {{ t('identity.auth.passwordRecovery.nextStep') }}
            </a-button>
          </div>
        </a-form-item>
      </a-form>
    </div>

    <!-- 步骤3: 邮箱验证码 -->
    <div v-if="currentStep === 2" class="step-content">
      <div class="verification-info">
        <a-alert
          :message="t('identity.auth.passwordRecovery.emailSent')"
          :description="t('identity.auth.passwordRecovery.emailSentDesc', { email: identityForm.email })"
          type="info"
          show-icon
          class="verification-alert"
        />
      </div>

      <a-form
        :model="verificationForm"
        :rules="verificationRules"
        ref="verificationFormRef"
        @finish="handleEmailVerification"
        layout="vertical"
      >
        <a-form-item name="verificationCode" :label="t('identity.auth.fields.verificationCode.label')">
          <a-input
            v-model:value="verificationForm.verificationCode"
            :placeholder="t('identity.auth.fields.verificationCode.placeholder')"
            size="large"
            :maxlength="6"
          >
            <template #prefix>
              <key-outlined />
            </template>
          </a-input>
        </a-form-item>

        <a-form-item>
          <div style="display: flex; justify-content: space-between;">
            <a-button
              @click="handleBack"
              size="large"
            >
              {{ t('identity.auth.passwordRecovery.back') }}
            </a-button>
            <a-space>
              <a-button
                type="primary"
                html-type="submit"
                size="large"
                :loading="loading"
                :disabled="!verificationValid || loading"
              >
                {{ t('identity.auth.passwordRecovery.verify') }}
              </a-button>
              <a-button
                @click="resendVerificationCode"
                :loading="resendLoading"
                size="large"
              >
                {{ t('identity.auth.passwordRecovery.resendCode') }}
              </a-button>
            </a-space>
          </div>
        </a-form-item>
      </a-form>
    </div>

    <!-- 步骤4: 重置密码 -->
    <div v-if="currentStep === 3" class="step-content">
      <a-form
        :model="passwordForm"
        :rules="passwordRules"
        ref="passwordFormRef"
        @finish="handlePasswordReset"
        layout="vertical"
      >
        <a-form-item name="newPassword" :label="t('identity.auth.fields.newPassword.label')">
          <a-input-password
            v-model:value="passwordForm.newPassword"
            :placeholder="t('identity.auth.fields.newPassword.placeholder')"
            size="large"
          >
            <template #prefix>
              <lock-outlined />
            </template>
          </a-input-password>
        </a-form-item>

        <a-form-item name="confirmPassword" :label="t('identity.auth.fields.confirmPassword.label')">
          <a-input-password
            v-model:value="passwordForm.confirmPassword"
            :placeholder="t('identity.auth.fields.confirmPassword.placeholder')"
            size="large"
          >
            <template #prefix>
              <lock-outlined />
            </template>
          </a-input-password>
        </a-form-item>

        <a-form-item>
          <div style="display: flex; justify-content: space-between;">
            <a-button
              @click="handleBack"
              size="large"
            >
              {{ t('identity.auth.passwordRecovery.back') }}
            </a-button>
            <a-button
              type="primary"
              html-type="submit"
              size="large"
              :loading="loading"
              :disabled="!passwordValid || loading"
            >
              {{ t('identity.auth.passwordRecovery.resetPassword') }}
            </a-button>
          </div>
        </a-form-item>
      </a-form>
    </div>

    <!-- 成功页面 -->
    <div v-if="currentStep === 4" class="step-content">
      <div class="success-content">
        <a-result
          status="success"
          :title="t('identity.auth.passwordRecovery.successTitle')"
          :sub-title="t('identity.auth.passwordRecovery.successSubtitle')"
        >
          <template #extra>
            <a-button type="primary" @click="$emit('switchToLogin')">
              {{ t('identity.auth.passwordRecovery.backToLogin') }}
            </a-button>
          </template>
        </a-result>
      </div>
    </div>

    <!-- 底部操作 -->
    <div class="recovery-footer" v-if="currentStep < 4">
      <a @click="$emit('switchToLogin')" class="back-to-login">
        {{ t('identity.auth.passwordRecovery.backToLogin') }}
      </a>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import { 
  UserOutlined, 
  LockOutlined, 
  MailOutlined, 
  KeyOutlined 
} from '@ant-design/icons-vue'
import { getCaptchaConfig } from '@/api/security/captcha'
import { 
  verifyIdentity, 
  sendVerificationCode as sendVerificationCodeApi, 
  verifyEmailCode, 
  resetPassword 
} from '@/api/identity/auth/userForget'


const { t } = useI18n()
const emit = defineEmits(['switchToLogin', 'recoverySuccess'])

// 表单引用
const captchaFormRef = ref<FormInstance>()
const identityFormRef = ref<FormInstance>()
const verificationFormRef = ref<FormInstance>()
const passwordFormRef = ref<FormInstance>()

// 验证码组件引用
const captchaRef = ref()

// 步骤表单有效性
const captchaValid = ref(false)
const identityValid = ref(false)
const verificationValid = ref(false)
const passwordValid = ref(false)

const validateCaptcha = async () => {
  try {
    await captchaFormRef.value?.validate()
    captchaValid.value = true
  } catch {
    captchaValid.value = false
  }
}

const validateIdentity = async () => {
  try {
    await identityFormRef.value?.validate()
    identityValid.value = true
  } catch {
    identityValid.value = false
  }
}

const validateVerification = async () => {
  try {
    await verificationFormRef.value?.validate()
    verificationValid.value = true
  } catch {
    verificationValid.value = false
  }
}

const validatePassword = async () => {
  try {
    await passwordFormRef.value?.validate()
    passwordValid.value = true
  } catch {
    passwordValid.value = false
  }
}

// 当前步骤
const currentStep = ref(0)

// 验证码表单
const captchaForm = reactive({
  captcha: ''
})

// 身份验证表单
const identityForm = reactive({
  userName: '',
  email: ''
})

// 邮箱验证表单
const verificationForm = reactive({
  verificationCode: ''
})

// 密码重置表单
const passwordForm = reactive({
  newPassword: '',
  confirmPassword: ''
})

// 状态
const loading = ref(false)
const captchaLoading = ref(false)
const resendLoading = ref(false)
const showCaptcha = ref(false)
const captchaToken = ref('')
const verificationToken = ref('')

// 验证码类型（完全由后端配置）
const captchaType = ref<'Slider' | 'Behavior'>('Behavior')

// 获取后端验证码配置
const loadCaptchaConfig = async () => {
  try {
    console.log('[找回密码验证码配置] 开始获取后端验证码配置（密码取回场景）')
    const { data } = await getCaptchaConfig('password-recovery')
    console.log('[找回密码验证码配置] 后端返回的完整数据:', data)
    
    // 检查数据结构：data 是实际的配置数据
    const configData = data
    if (configData) {
      // 检查验证码是否启用
      if (configData.enabled !== undefined) {
        showCaptcha.value = configData.enabled
      }
      
      if (configData.type) {
        captchaType.value = configData.type as 'Slider' | 'Behavior'
        console.log('[找回密码验证码配置] 后端配置的验证码类型:', configData.type)
      }
    } else {
      console.error('获取验证码配置失败: 后端未返回验证码类型')
      console.error('[找回密码验证码配置] 配置数据结构:', configData)
      message.error('获取验证码配置失败')
    }
  } catch (error) {
    console.error('获取验证码配置失败:', error)
    message.error('获取验证码配置失败')
  }
}

// 验证码表单规则
const captchaRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    captcha: [
      { required: true, message: t('identity.auth.passwordRecovery.form.captchaRequired'), trigger: 'blur' }
    ]
  }
  return rules
})

// 身份验证表单规则
const identityRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    userName: [
      { required: true, message: t('identity.auth.passwordRecovery.form.userNameRequired'), trigger: 'blur' },
      { min: 3, max: 20, message: t('identity.auth.passwordRecovery.form.userNameLength'), trigger: 'blur' }
    ],
    email: [
      { required: true, message: t('identity.auth.passwordRecovery.form.emailRequired'), trigger: 'blur' },
      { type: 'email', message: t('identity.auth.passwordRecovery.form.emailFormat'), trigger: 'blur' }
    ]
  }
  return rules
})

// 邮箱验证表单规则
const verificationRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    verificationCode: [
      { required: true, message: t('identity.auth.passwordRecovery.form.verificationCodeRequired'), trigger: 'blur' },
      { len: 6, message: t('identity.auth.passwordRecovery.form.verificationCodeLength'), trigger: 'blur' },
      { pattern: /^\d{6}$/, message: t('identity.auth.passwordRecovery.form.verificationCodeFormat'), trigger: 'blur' }
    ]
  }
  return rules
})

// 密码重置表单规则
const passwordRules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    newPassword: [
      { required: true, message: t('identity.auth.passwordRecovery.form.newPasswordRequired'), trigger: 'blur' },
      { min: 6, max: 20, message: t('identity.auth.passwordRecovery.form.newPasswordLength'), trigger: 'blur' },
      { pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/, message: t('identity.auth.passwordRecovery.form.newPasswordFormat'), trigger: 'blur' }
    ],
    confirmPassword: [
      { required: true, message: t('identity.auth.passwordRecovery.form.confirmPasswordRequired'), trigger: 'blur' },
      {
        validator: (rule: any, value: string) => {
          if (value !== passwordForm.newPassword) {
            return Promise.reject(t('identity.auth.passwordRecovery.form.passwordMismatch'))
          }
          return Promise.resolve()
        },
        trigger: 'blur'
      }
    ]
  }
  return rules
})

// 刷新验证码
const refreshCaptcha = async () => {
  captchaLoading.value = true
  try {
    // 重置验证码相关状态
    captchaToken.value = ''
    captchaForm.captcha = ''
    captchaValid.value = false // 重置验证状态
    
    // 验证码组件会自动调用API获取新的验证码
    console.log('[找回密码] 验证码已刷新')
  } catch (error) {
    console.error('[找回密码] 刷新验证码失败:', error)
    message.error('获取验证码失败')
  } finally {
    captchaLoading.value = false
  }
}

// 验证码成功回调
const handleCaptchaSuccess = (params: { token: string; xOffset: number }) => {
  try {
    console.log('[验证码成功] 验证码验证通过，参数:', params)
    
    // 验证参数
    if (!params || !params.token) {
      console.error('[验证码成功] 参数无效:', params)
      message.error('验证码参数无效')
      return
    }
    
    // 更新表单数据
    captchaToken.value = params.token
    captchaForm.captcha = params.xOffset?.toString() || '0'
    
    // 设置验证码为有效
    captchaValid.value = true
    
    console.log('[验证码成功] 验证码状态已更新:', {
      token: captchaToken.value ? `${captchaToken.value.substring(0, 8)}****${captchaToken.value.substring(captchaToken.value.length - 8)}` : '',
      captcha: captchaForm.captcha,
      valid: captchaValid.value
    })
  } catch (error) {
    console.error('[验证码成功] 处理验证码成功回调时出错:', error)
    message.error('验证码处理失败，请重试')
    captchaValid.value = false
  }
}

// 验证码错误回调
const handleCaptchaError = (error: any) => {
  try {
    console.log('[验证码错误] 验证码验证失败:', error)
    captchaValid.value = false
    captchaToken.value = ''
    captchaForm.captcha = ''
    message.error(t('identity.auth.captcha.error'))
  } catch (err) {
    console.error('[验证码错误] 处理验证码错误回调时出错:', err)
    message.error('验证码错误处理失败')
  }
}

// 处理步骤1：验证码验证
const handleCaptchaVerification = async () => {
  try {
    loading.value = true
    console.log('[找回密码步骤1] 开始验证码验证')
    
    // 验证表单
    await captchaFormRef.value?.validate()
    
    console.log('[找回密码步骤1] 验证码验证完成，进入下一步')
    message.success(t('identity.auth.passwordRecovery.captchaVerified'))
    
    // 进入下一步
    currentStep.value = 1
    
  } catch (error: any) {
    console.error('[找回密码步骤1] 失败:', error)
    message.error(error.message || t('identity.auth.passwordRecovery.error.captchaVerificationFailed'))
  } finally {
    loading.value = false
  }
}

// 处理步骤2：身份验证
const handleIdentityVerification = async () => {
  try {
    loading.value = true
    console.log('[找回密码步骤2] 开始身份验证')
    
    // 验证表单
    await identityFormRef.value?.validate()
    
    // 调用身份验证API
    const { data } = await verifyIdentity({
      userName: identityForm.userName,
      email: identityForm.email,
      captcha: parseInt(captchaForm.captcha) || 0,
      captchaToken: captchaToken.value
    })
    
    if (data.success) {
      console.log('[找回密码步骤2] 身份验证成功，发送验证码')
      message.success(t('identity.auth.passwordRecovery.identityVerified'))
      
      // 发送邮箱验证码
      await sendVerificationCode()
      
      // 进入下一步
      currentStep.value = 2
    } else {
      message.error(data.message || t('identity.auth.passwordRecovery.error.identityVerificationFailed'))
    }
    
  } catch (error: any) {
    console.error('[找回密码步骤2] 失败:', error)
    message.error(error.message || t('identity.auth.passwordRecovery.error.identityVerificationFailed'))
  } finally {
    loading.value = false
  }
}

// 发送验证码
const sendVerificationCode = async () => {
  try {
    resendLoading.value = true
    const { data } = await sendVerificationCodeApi({
      userName: identityForm.userName,
      email: identityForm.email
    })
    
    if (data.success) {
      message.success(t('identity.auth.passwordRecovery.codeSent'))
    } else {
      message.error(data.message || t('identity.auth.passwordRecovery.error.sendCodeFailed'))
    }
  } catch (error: any) {
    console.error('[发送验证码] 失败:', error)
    message.error(error.message || t('identity.auth.passwordRecovery.error.sendCodeFailed'))
  } finally {
    resendLoading.value = false
  }
}

// 重新发送验证码
const resendVerificationCode = async () => {
  await sendVerificationCode()
}

// 处理步骤3：邮箱验证
const handleEmailVerification = async () => {
  try {
    loading.value = true
    console.log('[找回密码步骤3] 开始邮箱验证')
    
    // 验证表单
    await verificationFormRef.value?.validate()
    
    // 调用邮箱验证API
    const { data } = await verifyEmailCode({
      userName: identityForm.userName,
      email: identityForm.email,
      verificationCode: verificationForm.verificationCode
    })
    
    if (data.success) {
      verificationToken.value = data.resetToken
      console.log('[找回密码步骤3] 邮箱验证成功，进入下一步')
      message.success(t('identity.auth.passwordRecovery.emailVerified'))
      
      // 进入下一步
      currentStep.value = 3
    } else {
      message.error(data.message || t('identity.auth.passwordRecovery.error.emailVerificationFailed'))
    }
    
  } catch (error: any) {
    console.error('[找回密码步骤3] 失败:', error)
    message.error(error.message || t('identity.auth.passwordRecovery.error.emailVerificationFailed'))
  } finally {
    loading.value = false
  }
}

// 处理步骤4：重置密码
const handlePasswordReset = async () => {
  try {
    loading.value = true
    console.log('[找回密码步骤4] 开始重置密码')
    
    // 验证表单
    await passwordFormRef.value?.validate()
    
    // 调用重置密码API
    const { data } = await resetPassword({
      userName: identityForm.userName,
      email: identityForm.email,
      resetToken: verificationToken.value,
      newPassword: passwordForm.newPassword
    })
    
    if (data.success) {
      console.log('[找回密码步骤4] 密码重置成功')
      message.success(t('identity.auth.passwordRecovery.passwordResetSuccess'))
      
      // 进入成功页面
      currentStep.value = 4
      
      // 触发成功事件
      emit('recoverySuccess')
    } else {
      message.error(data.message || t('identity.auth.passwordRecovery.error.passwordResetFailed'))
    }
    
  } catch (error: any) {
    console.error('[找回密码步骤4] 失败:', error)
    message.error(error.message || t('identity.auth.passwordRecovery.error.passwordResetFailed'))
  } finally {
    loading.value = false
  }
}

// 返回上一步
const handleBack = () => {
  if (currentStep.value > 0) {
    currentStep.value--
  }
}

// 重置所有状态
const resetAllStates = () => {
  console.log('[找回密码页面] 开始重置所有状态')
  
  // 重置步骤
  currentStep.value = 0
  
  // 重置表单数据
  Object.assign(captchaForm, { captcha: '' })
  Object.assign(identityForm, { userName: '', email: '' })
  Object.assign(verificationForm, { verificationCode: '' })
  Object.assign(passwordForm, { newPassword: '', confirmPassword: '' })
  
  // 重置状态
  loading.value = false
  captchaLoading.value = false
  resendLoading.value = false
  captchaToken.value = ''
  verificationToken.value = ''
  
  // 重置表单验证状态
  captchaValid.value = false
  identityValid.value = false
  verificationValid.value = false
  passwordValid.value = false
  
  // 重置表单引用
  captchaFormRef.value?.resetFields()
  identityFormRef.value?.resetFields()
  verificationFormRef.value?.resetFields()
  passwordFormRef.value?.resetFields()
  
  // 重新加载验证码配置并显示验证码
  loadCaptchaConfig().then(() => {
    showCaptcha.value = true
    
    // 重置验证码组件状态
    if (captchaRef.value?.resetCaptcha) {
      captchaRef.value.resetCaptcha()
    }
  })
  
  console.log('[找回密码页面] 状态重置完成')
}

// 暴露重置方法给父组件
defineExpose({
  resetAllStates
})

// 组件挂载时初始化
onMounted(async () => {
  console.log('[找回密码页面] 开始初始化')
  
  // 重置所有状态
  resetAllStates()
  
  // 加载验证码配置
  await loadCaptchaConfig()
  
  // 显示验证码
  showCaptcha.value = true
  
  console.log('[找回密码页面] 初始化完成')
})
</script>

<style lang="less" scoped>
.password-recovery-container {
  width: 100%;
  max-width: 600px;
  margin: 0 auto;
  padding: 24px;
}

.recovery-header {
  text-align: center;
  margin-bottom: 32px;
  
  h2 {
    font-size: 24px;
    font-weight: 600;
    color: var(--ant-color-text);
    margin-bottom: 8px;
  }
  
  p {
    color: var(--ant-color-text-secondary);
    font-size: 14px;
  }
}

.recovery-steps {
  margin-bottom: 32px;
}

.step-content {
  margin-bottom: 24px;
}

.verification-info {
  margin-bottom: 24px;
}

.verification-alert {
  margin-bottom: 16px;
}

.success-content {
  text-align: center;
  padding: 32px 0;
}

.recovery-footer {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 24px;
  padding-top: 16px;
  border-top: 1px solid var(--ant-color-border-split);
  
  .back-to-login {
    color: var(--ant-color-primary);
    
    &:hover {
      color: var(--ant-color-primary-hover);
    }
  }
}

:deep(.ant-form-item-label) {
  font-weight: 500;
}

:deep(.ant-input-prefix) {
  color: var(--ant-color-text-secondary);
}

:deep(.ant-steps-item-title) {
  font-size: 14px;
}

:deep(.ant-result-title) {
  font-size: 20px;
}

:deep(.ant-result-subtitle) {
  font-size: 14px;
}
</style> 
