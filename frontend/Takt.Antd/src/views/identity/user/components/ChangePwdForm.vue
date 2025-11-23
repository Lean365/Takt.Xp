//===================================================================
// 项目名 : Lean.Takt
// 文件名称: ChangePwdForm.vue
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 修改密码表单组件
//===================================================================

<template>
  <div class="change-password">
    <a-card :bordered="false">
      <a-form
        ref="formRef"
        :model="form"
        :rules="rules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
      >
        <a-form-item :label="t('identity.user.password.old.label')" name="oldPassword">
          <a-input-password
            v-model:value="form.oldPassword"
            :placeholder="t('identity.user.password.old.placeholder')"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.password.new.label')" name="newPassword">
          <a-input-password
            v-model:value="form.newPassword"
            :placeholder="t('identity.user.password.new.placeholder')"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.password.confirm.label')" name="confirmPassword">
          <a-input-password
            v-model:value="form.confirmPassword"
            :placeholder="t('identity.user.password.confirm.placeholder')"
          />
        </a-form-item>
        <a-form-item :wrapper-col="{ span: 16, offset: 6 }">
          <a-button type="primary" :loading="submitting" @click="handleSubmit">
            {{ t('common.button.submit') }}
          </a-button>
          <a-button style="margin-left: 8px" @click="handleReset">
            {{ t('common.button.reset') }}
          </a-button>
        </a-form-item>
      </a-form>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useUserStore } from '@/stores/userStore'
import { useRouter } from 'vue-router'
import { changePassword } from '@/api/identity/user'

const { t } = useI18n()
const userStore = useUserStore()
const router = useRouter()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive({
  oldPassword: '',
  newPassword: '',
  confirmPassword: ''
})

// 表单校验规则
const validateConfirmPassword = async (_rule: Rule, value: string) => {
  if (value === '') {
    return Promise.reject(t('identity.user.password.confirm.validation.required'))
  }
  if (value !== form.newPassword) {
    return Promise.reject(t('identity.user.password.confirm.validation.notMatch'))
  }
  return Promise.resolve()
}

const rules: Record<string, Rule[]> = {
  oldPassword: [
    { required: true, message: t('identity.user.password.old.validation.required') }
  ],
  newPassword: [
    { required: true, message: t('identity.user.password.new.validation.required') },
    { min: 6, max: 20, message: t('identity.user.password.new.validation.length') },
    { pattern: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,20}$/, message: t('identity.user.password.new.validation.format') }
  ],
  confirmPassword: [{ validator: validateConfirmPassword, trigger: 'change' }]
}

// 提交状态
const submitting = ref(false)


// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    const params = {
      oldPassword: form.oldPassword,
      newPassword: form.newPassword
    }
    const success = await changePassword(params)
    if (success) {
      message.success(t('identity.user.changePassword.success'))
      // 清除表单数据
      form.oldPassword = ''
      form.newPassword = ''
      form.confirmPassword = ''
      
      formRef.value?.resetFields()
    } else {
      message.error(t('identity.user.changePassword.failed'))
    }
  } catch (error) {
    console.error(t('identity.user.changePassword.changeFailed'), error)
    message.error(t('identity.user.changePassword.failed'))
  }
}

// 重置表单
const handleReset = () => {
  formRef.value?.resetFields()
}
</script>

<style lang="less" scoped>
.change-password {
  padding: 24px;

  :deep(.ant-form) {
    max-width: 600px;
    margin: 0 auto;
  }

  :deep(.ant-btn) {
    min-width: 100px;
  }
}
</style> 
