<template>
  <div class="registration-container">
    <div class="registration-header">
      <h2>{{ t('identity.auth.register.title') }}</h2>
      <p>{{ t('identity.auth.register.subtitle') }}</p>
    </div>

    <!-- 步骤指示器 -->
    <a-steps :current="currentStep" class="registration-steps">
      <a-step :title="t('identity.auth.register.step1')" />
      <a-step :title="t('identity.auth.register.step2')" />
      <a-step :title="t('identity.auth.register.step3')" />
      <a-step :title="t('identity.auth.register.step4')" />
    </a-steps>

    <!-- 步骤1: 验证码验证 -->
    <div v-if="currentStep === 0" class="step-content">
      <a-form
        :model="step1Form"
        :rules="step1Rules"
        ref="step1FormRef"
        @fields-change="validateStep1"
        @input="validateStep1"
        @change="validateStep1"
        @blur="validateStep1"
        @finish="handleStep1"
        layout="vertical"
      >
        <a-form-item name="captcha" :label="t('identity.auth.fields.captcha.label')" v-if="showCaptcha">
          <TaktSliderCaptcha
            v-if="captchaType === 'Slider'"
            ref="captchaRef"
            v-model:captcha-token="captchaToken"
            v-model:captcha-offset="step1Form.captcha"
            @refresh="refreshCaptcha"
            :loading="captchaLoading"
            @success="handleCaptchaSuccess"
            @error="handleCaptchaError"
          />
          <TaktBehaviorCaptcha
            v-else-if="captchaType === 'Behavior'"
            ref="captchaRef"
            v-model:captcha-token="captchaToken"
            v-model:captcha-score="step1Form.captcha"
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
            :disabled="!step1Valid || loading"
            block
          >
            {{ t('identity.auth.register.nextStep') }}
          </a-button>
        </a-form-item>
      </a-form>
    </div>
    <!-- 步骤2: 用户基本信息 -->
    <div v-if="currentStep === 1" class="step-content">
      <a-form
        :model="step2Form"
        :rules="step2Rules"
        ref="step2FormRef"
        @fields-change="validateStep2"
        @input="validateStep2"
        @change="validateStep2"
        @blur="validateStep2"
        @finish="handleStep2"
        layout="vertical"
      >
        <a-form-item name="userName" :label="t('identity.auth.fields.username.label')">
          <a-input
            v-model:value="step2Form.userName"
            :placeholder="t('identity.auth.fields.username.placeholder')"
            size="large"
            @change="handleUserNameChange"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="email" :label="t('identity.auth.fields.email.label')">
          <a-input
            v-model:value="step2Form.email"
            :placeholder="t('identity.auth.fields.email.placeholder')"
            size="large"
            type="email"
          >
            <template #prefix>
              <mail-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="password" :label="t('identity.auth.fields.password.label')">
          <a-input-password
            v-model:value="step2Form.password"
            :placeholder="t('identity.auth.fields.password.placeholder')"
            size="large"
          >
            <template #prefix>
              <lock-outlined />
            </template>
          </a-input-password>
        </a-form-item>
        <a-form-item name="confirmPassword" :label="t('identity.auth.register.confirmPassword')">
          <a-input-password
            v-model:value="step2Form.confirmPassword"
            :placeholder="t('identity.auth.register.confirmPasswordPlaceholder')"
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
              {{ t('identity.auth.register.back') }}
            </a-button>
            <a-button
              type="primary"
              html-type="submit"
              size="large"
              :loading="loading"
              :disabled="!step2Valid || loading"
            >
              {{ t('identity.auth.register.nextStep') }}
            </a-button>
          </div>
        </a-form-item>
      </a-form>
    </div>
    <!-- 步骤3: 其它信息 -->
    <div v-if="currentStep === 2" class="step-content">
      <a-form
        :model="step3Form"
        :rules="step3Rules"
        ref="step3FormRef"
        @fields-change="validateStep3"
        @input="validateStep3"
        @change="validateStep3"
        @blur="validateStep3"
        @finish="handleStep3"
        layout="vertical"
      >
        <a-form-item name="nickName" :label="t('identity.auth.fields.nickName.label')">
          <a-input
            v-model:value="step3Form.nickName"
            :placeholder="t('identity.auth.fields.nickName.placeholder')"
            size="large"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="realName" :label="t('identity.auth.fields.realName.label')">
          <a-input
            v-model:value="step3Form.realName"
            :placeholder="t('identity.auth.fields.realName.placeholder')"
            size="large"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="fullName" :label="t('identity.auth.fields.fullName.label')">
          <a-input
            v-model:value="step3Form.fullName"
            :placeholder="t('identity.auth.fields.fullName.placeholder')"
            size="large"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="englishName" :label="t('identity.auth.fields.englishName.label')">
          <a-input
            v-model:value="step3Form.englishName"
            :placeholder="t('identity.auth.fields.englishName.placeholder')"
            size="large"
          >
            <template #prefix>
              <user-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="phoneNumber" :label="t('identity.auth.fields.phone.label')">
          <a-input
            v-model:value="step3Form.phoneNumber"
            :placeholder="t('identity.auth.fields.phone.placeholder')"
            size="large"
          >
            <template #prefix>
              <phone-outlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item name="gender" :label="t('identity.auth.fields.gender.label')">
          <a-radio-group v-model:value="step3Form.gender" size="large">
            <a-radio :value="0">{{ t('identity.auth.fields.gender.options.unknown') }}</a-radio>
            <a-radio :value="1">{{ t('identity.auth.fields.gender.options.male') }}</a-radio>
            <a-radio :value="2">{{ t('identity.auth.fields.gender.options.female') }}</a-radio>
          </a-radio-group>
        </a-form-item>
        <a-form-item>
          <div style="display: flex; justify-content: space-between;">
            <a-button
              @click="handleBack"
              size="large"
            >
              {{ t('identity.auth.register.back') }}
            </a-button>
            <a-button
              type="primary"
              html-type="submit"
              size="large"
              :loading="loading"
              :disabled="!step3Valid || loading"
            >
              {{ t('identity.auth.register.nextStep') }}
            </a-button>
          </div>
        </a-form-item>
      </a-form>
    </div>
    <!-- 步骤4: 权限设置 -->
    <div v-if="currentStep === 3" class="step-content">
      <a-form
        :model="step4Form"
        :rules="step4Rules"
        ref="step4FormRef"
        @fields-change="validateStep4"
        @input="validateStep4"
        @change="validateStep4"
        @blur="validateStep4"
        @finish="handleStep4"
        layout="vertical"
      >
        <a-form-item name="userType" :label="t('identity.auth.fields.userType.label')">
          <a-select
            v-model:value="step4Form.userType"
            :placeholder="t('identity.auth.fields.userType.placeholder')"
            size="large"
          >
            <a-select-option :value="1">{{ t('identity.auth.fields.userType.options.normal') }}</a-select-option>
            <a-select-option :value="2">{{ t('identity.auth.fields.userType.options.admin') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item name="status" :label="t('identity.auth.fields.status.label')">
          <a-select
            v-model:value="step4Form.status"
            :placeholder="t('identity.auth.fields.status.placeholder')"
            size="large"
          >
            <a-select-option :value="0">{{ t('identity.auth.fields.status.options.normal') }}</a-select-option>
            <a-select-option :value="1">{{ t('identity.auth.fields.status.options.disabled') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item name="deptId" :label="t('identity.auth.register.deptId')">
          <a-input-number
            v-model:value="step4Form.deptId"
            :placeholder="t('identity.auth.register.deptIdPlaceholder')"
            size="large"
            style="width: 100%"
            :min="1"
          />
        </a-form-item>
        <a-form-item name="roleIds" :label="t('identity.auth.register.roleIds')">
          <a-select
            v-model:value="step4Form.roleIds"
            :placeholder="t('identity.auth.register.roleIdsPlaceholder')"
            size="large"
            mode="multiple"
          >
            <a-select-option :value="1">{{ t('identity.auth.register.roleUser') }}</a-select-option>
            <a-select-option :value="2">{{ t('identity.auth.register.roleAdmin') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item name="postIds" :label="t('identity.auth.register.postIds')">
          <a-select
            v-model:value="step4Form.postIds"
            :placeholder="t('identity.auth.register.postIdsPlaceholder')"
            size="large"
            mode="multiple"
          >
            <a-select-option :value="1">{{ t('identity.auth.register.postEmployee') }}</a-select-option>
            <a-select-option :value="2">{{ t('identity.auth.register.postManager') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item name="deptIds" :label="t('identity.auth.register.deptIds')">
          <a-select
            v-model:value="step4Form.deptIds"
            :placeholder="t('identity.auth.register.deptIdsPlaceholder')"
            size="large"
            mode="multiple"
          >
            <a-select-option :value="1">{{ t('identity.auth.register.deptIT') }}</a-select-option>
            <a-select-option :value="2">{{ t('identity.auth.register.deptHR') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item name="remark" :label="t('identity.auth.register.remark')">
          <a-textarea
            v-model:value="step4Form.remark"
            :placeholder="t('identity.auth.register.remarkPlaceholder')"
            size="large"
            :rows="3"
          />
        </a-form-item>
        <a-form-item name="agreement">
          <a-checkbox v-model:checked="agreement">
            {{ t('identity.auth.register.agreementPrefix') }}
            <a-button type="link" @click="showAgreement" style="padding: 0; height: auto;">
              {{ t('identity.auth.register.agreementLink') }}
            </a-button>
            {{ t('identity.auth.register.agreementSuffix') }}
          </a-checkbox>
        </a-form-item>
        <a-form-item>
          <div style="display: flex; justify-content: space-between;">
            <a-button
              @click="handleBack"
              size="large"
            >
              {{ t('identity.auth.register.back') }}
            </a-button>
            <a-button
              type="primary"
              html-type="submit"
              size="large"
              :loading="loading"
              :disabled="!step4Valid || !agreement || loading"
            >
              {{ t('identity.auth.register.submit') }}
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
          :title="t('identity.auth.register.successTitle')"
          :sub-title="t('identity.auth.register.successSubtitle')"
        >
          <template #extra>
            <a-button type="primary" @click="$emit('switchToLogin')">
              {{ t('identity.auth.register.backToLogin') }}
            </a-button>
          </template>
        </a-result>
      </div>
    </div>

    <!-- 底部操作 -->
    <div class="registration-footer" v-if="currentStep < 4">
      <a-button type="link" @click="$emit('switchToLogin')" class="back-to-login">
        {{ t('identity.auth.register.backToLogin') }}
      </a-button>
    </div>

    <!-- 用户协议弹窗 -->
    <a-modal
      v-model:open="showAgreementModal"
      :title="t('identity.auth.register.agreementTitle')"
      width="600px"
      :footer="null"
    >
      <div class="agreement-content">
        <h3>{{ t('identity.auth.register.agreementTitle') }}</h3>
        <p>{{ t('identity.auth.register.agreementContent') }}</p>
      </div>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import { UserOutlined, LockOutlined, MailOutlined, PhoneOutlined } from '@ant-design/icons-vue'
import { getCaptchaConfig } from '@/api/security/captcha'
import type { TaktUserCreate } from '@/types/identity/user'

import { maskName } from '@/utils/maskUtil'

const { t } = useI18n()
const emit = defineEmits(['switchToLogin', 'registrationSuccess'])

// 表单引用
const step1FormRef = ref<FormInstance>()
const step2FormRef = ref<FormInstance>()
const step3FormRef = ref<FormInstance>()
const step4FormRef = ref<FormInstance>()

// 验证码组件引用
const captchaRef = ref()

// 步骤表单有效性
const step1Valid = ref(false)
const step2Valid = ref(false)
const step3Valid = ref(false)
const step4Valid = ref(false)

const validateStep1 = async () => {
  try {
    await step1FormRef.value?.validate()
    step1Valid.value = true
  } catch {
    step1Valid.value = false
  }
}
const validateStep2 = async () => {
  try {
    await step2FormRef.value?.validate()
    step2Valid.value = true
  } catch {
    step2Valid.value = false
  }
}
const validateStep3 = async () => {
  try {
    await step3FormRef.value?.validate()
    step3Valid.value = true
  } catch {
    step3Valid.value = false
  }
}
const validateStep4 = async () => {
  try {
    await step4FormRef.value?.validate()
    step4Valid.value = true
  } catch {
    step4Valid.value = false
  }
}

// 当前步骤
const currentStep = ref(0)

// 步骤1表单数据（验证码）
const step1Form = reactive({
  captcha: ''
})
// 步骤2表单数据（用户基本信息）
const step2Form = reactive({
  userName: '',
  email: '',
  password: '',
  confirmPassword: ''
})
// 步骤3表单数据（其它信息）
const step3Form = reactive({
  nickName: '',
  realName: '',
  fullName: '',
  englishName: '',
  phoneNumber: '',
  gender: 0
})
// 步骤4表单数据（权限设置）
const step4Form = reactive({
  userType: 1,
  status: 0,
  deptId: 1,
  roleIds: [1],
  postIds: [1],
  deptIds: [1],
  remark: ''
})
// 步骤5表单数据（验证码）
const step5Form = reactive({
  captcha: ''
})

// 额外字段
const agreement = ref(false)

// 状态
const loading = ref(false)
const captchaLoading = ref(false)
const showCaptcha = ref(false)
const captchaToken = ref('')
const showAgreementModal = ref(false)



// 验证码类型（完全由后端配置）
const captchaType = ref<'Slider' | 'Behavior'>('Behavior')

// 获取后端验证码配置
const loadCaptchaConfig = async () => {
  try {
    console.log('[注册验证码配置] 开始获取后端验证码配置（注册场景）')
    const { data } = await getCaptchaConfig('register')
    console.log('[注册验证码配置] 后端返回的完整数据:', data)
    
    // 检查数据结构：data 是实际的配置数据
    const configData = data
    if (configData) {
      // 检查验证码是否启用
      if (configData.enabled !== undefined) {
        showCaptcha.value = configData.enabled
      }
      
      if (configData.type) {
        captchaType.value = configData.type as 'Slider' | 'Behavior'
        console.log('[注册验证码配置] 后端配置的验证码类型:', configData.type)
      }
    } else {
      console.error('获取验证码配置失败: 后端未返回验证码类型')
      console.error('[注册验证码配置] 配置数据结构:', configData)
      message.error('获取验证码配置失败')
    }
  } catch (error) {
    console.error('获取验证码配置失败:', error)
    message.error('获取验证码配置失败')
  }
}

// 步骤1表单验证规则（验证码）
const step1Rules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    captcha: [
      { required: true, message: t('identity.auth.register.form.captchaRequired'), trigger: 'blur' }
    ]
  }
  return rules
})
// 步骤2表单验证规则（用户基本信息）
const step2Rules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    userName: [
      { required: true, message: t('identity.auth.register.form.usernameRequired'), trigger: 'blur' },
      { min: 3, max: 20, message: t('identity.auth.register.form.usernameLength'), trigger: 'blur' },
      { pattern: /^[a-zA-Z0-9_]+$/, message: t('identity.auth.register.form.usernameFormat'), trigger: 'blur' }
    ],
    email: [
      { required: true, message: t('identity.auth.register.form.emailRequired'), trigger: 'blur' },
      { type: 'email', message: t('identity.auth.register.form.emailFormat'), trigger: 'blur' }
    ],
    password: [
      { required: true, message: t('identity.auth.register.form.passwordRequired'), trigger: 'blur' },
      { min: 6, max: 20, message: t('identity.auth.register.form.passwordLength'), trigger: 'blur' },
      { pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/, message: t('identity.auth.register.form.passwordFormat'), trigger: 'blur' }
    ],
    confirmPassword: [
      { required: true, message: t('identity.auth.register.form.confirmPasswordRequired'), trigger: 'blur' },
      {
        validator: (rule: any, value: string) => {
          if (value !== step2Form.password) {
            return Promise.reject(t('identity.auth.register.form.passwordMismatch'))
          }
          return Promise.resolve()
        },
        trigger: 'blur'
      }
    ]
  }
  return rules
})
// 步骤3表单验证规则（其它信息）
const step3Rules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    nickName: [
      { required: true, message: t('identity.auth.register.form.nickNameRequired'), trigger: 'blur' },
      { min: 2, max: 20, message: t('identity.auth.register.form.nickNameLength'), trigger: 'blur' }
    ],
    realName: [
      { required: true, message: t('identity.auth.register.form.realNameRequired'), trigger: 'blur' },
      { min: 2, max: 20, message: t('identity.auth.register.form.realNameLength'), trigger: 'blur' }
    ],
    fullName: [
      { required: true, message: t('identity.auth.register.form.fullNameRequired'), trigger: 'blur' },
      { min: 2, max: 50, message: t('identity.auth.register.form.fullNameLength'), trigger: 'blur' }
    ],
    englishName: [
      { min: 2, max: 50, message: t('identity.auth.register.form.englishNameLength'), trigger: 'blur' },
      { pattern: /^[a-zA-Z\s]+$/, message: t('identity.auth.register.form.englishNameFormat'), trigger: 'blur' }
    ],
    phoneNumber: [
      { pattern: /^1[3-9]\d{9}$/, message: t('identity.auth.register.form.phoneNumberFormat'), trigger: 'blur' }
    ]
  }
  return rules
})
// 步骤4表单验证规则（权限设置）
const step4Rules = computed(() => {
  const rules: Record<string, RuleObject[]> = {
    userType: [
      { required: true, message: t('identity.auth.register.form.userTypeRequired'), trigger: 'change' }
    ],
    status: [
      { required: true, message: t('identity.auth.register.form.statusRequired'), trigger: 'change' }
    ],
    deptId: [
      { required: true, message: t('identity.auth.register.form.deptIdRequired'), trigger: 'change' }
    ],
    roleIds: [
      { required: true, message: t('identity.auth.register.form.roleIdsRequired'), trigger: 'change' }
    ],
    postIds: [
      { required: true, message: t('identity.auth.register.form.postIdsRequired'), trigger: 'change' }
    ],
    deptIds: [
      { required: true, message: t('identity.auth.register.form.deptIdsRequired'), trigger: 'change' }
    ],
    agreement: [
      {
        validator: (rule: any, value: boolean) => {
          if (!value) {
            return Promise.reject(t('identity.auth.register.form.agreementRequired'))
          }
          return Promise.resolve()
        },
        trigger: 'change'
      }
    ]
  }
  return rules
})

// 监听用户名变化
const handleUserNameChange = async (e: Event) => {
  const value = (e.target as HTMLInputElement).value
  console.log('[用户名变化] 用户名:', maskName(value))
  
  // 检查用户名是否已存在
  if (value.length >= 3) {
    // 这里可以调用API检查用户名是否已存在
    // const exists = await checkUserNameExists(value)
    // if (exists) {
    //   message.warning('用户名已存在，请选择其他用户名')
    // }
  }
}

// 刷新验证码
const refreshCaptcha = async () => {
  captchaLoading.value = true
  try {
    // 重置验证码相关状态
    captchaToken.value = ''
    step1Form.captcha = ''
    step1Valid.value = false // 重置验证状态
    
    // 验证码组件会自动调用API获取新的验证码
    console.log('[注册] 验证码已刷新')
  } catch (error) {
    console.error('[注册] 刷新验证码失败:', error)
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
    step1Form.captcha = params.xOffset?.toString() || '0'
    
    // 设置步骤1为有效
    step1Valid.value = true
    
    console.log('[验证码成功] 验证码状态已更新:', {
      token: captchaToken.value ? `${captchaToken.value.substring(0, 8)}****${captchaToken.value.substring(captchaToken.value.length - 8)}` : '',
      captcha: step1Form.captcha,
      valid: step1Valid.value
    })
  } catch (error) {
    console.error('[验证码成功] 处理验证码成功回调时出错:', error)
    message.error('验证码处理失败，请重试')
    step1Valid.value = false
  }
}

// 验证码错误回调
const handleCaptchaError = (error: any) => {
  try {
    console.log('[验证码错误] 验证码验证失败:', error)
    step1Valid.value = false
    captchaToken.value = ''
    step1Form.captcha = ''
    message.error(t('identity.auth.captcha.error'))
  } catch (err) {
    console.error('[验证码错误] 处理验证码错误回调时出错:', err)
    message.error('验证码错误处理失败')
  }
}

// 显示用户协议
const showAgreement = () => {
  showAgreementModal.value = true
}

// 处理步骤1：验证码验证
const handleStep1 = async () => {
  try {
    loading.value = true
    console.log('[注册步骤1] 开始验证码验证')
    
    // 验证表单
    await step1FormRef.value?.validate()
    
    // 这里可以添加验证码验证逻辑
    // 例如：调用后端API验证验证码是否正确
    
    console.log('[注册步骤1] 验证码验证完成，进入下一步')
    message.success(t('identity.auth.register.step1Success'))
    
    // 进入下一步
    currentStep.value = 1
    
  } catch (error: any) {
    console.error('[注册步骤1] 失败:', error)
    message.error(error.message || t('identity.auth.register.error.step1Failed'))
  } finally {
    loading.value = false
  }
}

// 处理步骤2：用户基本信息
const handleStep2 = async () => {
  try {
    loading.value = true
    console.log('[注册步骤2] 开始验证用户基本信息')
    
    // 验证表单
    await step2FormRef.value?.validate()
    
    // 这里可以添加用户名和邮箱的验证逻辑
    // 例如：检查用户名是否已存在，发送邮箱验证码等
    
    console.log('[注册步骤2] 验证通过，进入下一步')
    message.success(t('identity.auth.register.step2Success'))
    
    // 进入下一步
    currentStep.value = 2
    
  } catch (error: any) {
    console.error('[注册步骤2] 失败:', error)
    message.error(error.message || t('identity.auth.register.error.step2Failed'))
  } finally {
    loading.value = false
  }
}

// 处理步骤3：其它信息
const handleStep3 = async () => {
  try {
    loading.value = true
    console.log('[注册步骤3] 开始验证其它信息')
    
    // 验证表单
    await step3FormRef.value?.validate()
    
    console.log('[注册步骤3] 信息验证完成，进入下一步')
    message.success(t('identity.auth.register.step3Success'))
    
    // 进入下一步
    currentStep.value = 3
    
  } catch (error: any) {
    console.error('[注册步骤3] 失败:', error)
    message.error(error.message || t('identity.auth.register.error.step3Failed'))
  } finally {
    loading.value = false
  }
}

// handleStep4 逻辑
const handleStep4 = async () => {
  try {
    loading.value = true
    await step4FormRef.value?.validate()
    message.success(t('identity.auth.register.success'))
    emit('registrationSuccess')
  } catch (error: any) {
    message.error(error.message || t('identity.auth.register.error.step4Failed'))
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
  console.log('[注册页面] 开始重置所有状态')
  
  // 重置步骤
  currentStep.value = 0
  
  // 重置表单数据
  Object.assign(step1Form, { captcha: '' })
  Object.assign(step2Form, { userName: '', email: '', password: '', confirmPassword: '' })
  Object.assign(step3Form, { nickName: '', realName: '', fullName: '', englishName: '', phoneNumber: '', gender: 0 })
  Object.assign(step4Form, { userType: 1, status: 0, deptId: 1, roleIds: [1], postIds: [1], deptIds: [1], remark: '' })
  Object.assign(step5Form, { captcha: '' })
  
  // 重置状态
  loading.value = false
  captchaLoading.value = false
  captchaToken.value = ''
  showAgreementModal.value = false
  agreement.value = false
  
  // 重置表单验证状态
  step1Valid.value = false
  step2Valid.value = false
  step3Valid.value = false
  step4Valid.value = false
  
  // 重置表单引用
  step1FormRef.value?.resetFields()
  step2FormRef.value?.resetFields()
  step3FormRef.value?.resetFields()
  step4FormRef.value?.resetFields()
  
  // 重新加载验证码配置并显示验证码
  loadCaptchaConfig().then(() => {
    showCaptcha.value = true
    
    // 重置验证码组件状态
    if (captchaRef.value?.resetCaptcha) {
      captchaRef.value.resetCaptcha()
    }
  })
  
  console.log('[注册页面] 状态重置完成')
}

// 暴露重置方法给父组件
defineExpose({
  resetAllStates
})

// 组件挂载时初始化
onMounted(async () => {
  console.log('[注册页面] 开始初始化')
  
  // 重置所有状态
  resetAllStates()
  
  // 加载验证码配置
  await loadCaptchaConfig()
  
  // 显示验证码
  showCaptcha.value = true
  
  console.log('[注册页面] 初始化完成')
})
</script>

<style lang="less" scoped>
.registration-container {
  width: 100%;
  max-width: 600px;
  margin: 0 auto;
  padding: 24px;
}

.registration-header {
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

.registration-steps {
  margin-bottom: 32px;
}

.step-content {
  margin-bottom: 24px;
}

.success-content {
  text-align: center;
  padding: 32px 0;
}

.registration-footer {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 24px;
  padding-top: 16px;
  border-top: 1px solid var(--ant-color-border-split);
  
  .back-to-login {
    padding: 0;
    height: auto;
    border: none;
    box-shadow: none;
    
    &:hover {
      background: transparent;
    }
  }
}

.agreement-content {
  max-height: 400px;
  overflow-y: auto;
  
  h3 {
    margin-bottom: 16px;
    color: var(--ant-color-text);
  }
  
  p {
    line-height: 1.6;
    color: var(--ant-color-text-secondary);
  }
}

:deep(.ant-form-item-label) {
  font-weight: 500;
}

:deep(.ant-input-prefix) {
  color: var(--ant-color-text-secondary);
}

:deep(.ant-radio-group) {
  display: flex;
  gap: 16px;
}

:deep(.ant-select) {
  width: 100%;
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

