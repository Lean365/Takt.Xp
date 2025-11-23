<template>
  <Takt-modal
    :title="t('identity.user.resetPwd')"
    :open="visible"
    :loading="loading"
    @update:open="handleCancel"
    @ok="handleOk"
  >
    <p>{{ t('identity.user.messages.resetPasswordConfirm') }}</p>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { resetPassword } from '@/api/identity/user'
import type { TaktUserPasswordReset } from '@/types/identity/user'

const props = defineProps<{
  visible: boolean
  userId?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const loading = ref(false)

// 使用默认密码，而不是从配置中获取
const defaultPassword = 'Takt@123852'

const formData = ref({
  userId: props.userId,
  password: defaultPassword
})

const handleOk = async () => {
  const userId = props.userId
      if (typeof userId !== 'string') {
    message.error(t('common.failed'))
    return
  }

  loading.value = true
  try {
    const data: TaktUserPasswordReset = {
      userId: userId,
      newPassword: formData.value.password
    }
    await resetPassword(data)
    message.success(t('identity.user.messages.resetPasswordSuccess'))
    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  emit('update:visible', false)
}
</script> 

