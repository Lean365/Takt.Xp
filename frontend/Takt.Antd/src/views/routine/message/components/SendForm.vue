<template>
  <Takt-modal
    :open="visible"
    title="发送消息"
    width="600px"
    :mask-closable="false"
    @cancel="handleClose"
    @update:open="handleVisibleChange"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="接收用户" name="userId">
        <a-select
          v-model:value="form.userId"
          show-search
          placeholder="请选择接收用户"
          :filter-option="false"
          :not-found-content="null"
          :options="userOptions"
          @search="handleSearch"
        />
      </a-form-item>
      <a-form-item label="消息内容" name="content">
        <a-textarea
          v-model:value="form.content"
          :rows="4"
          placeholder="请输入消息内容"
        />
      </a-form-item>
    </a-form>

    <template #footer>
      <a-space>
        <a-button @click="handleClose">取消</a-button>
        <a-button type="primary" @click="handleSubmit" :loading="submitting">
          发送
        </a-button>
      </a-space>
    </template>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { searchUser } from '@/api/identity/user'
import { signalRService } from '@/utils/SignalR/service'
import { useUserStore } from '@/stores/userStore'

const userStore = useUserStore()

const props = defineProps<{
  visible: boolean
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const formRef = ref<FormInstance>()
const userOptions = ref<{ label: string; value: string }[]>([])
const loading = ref(false)
const submitting = ref(false)

const form = reactive({
  userId: '',
  content: ''
})

const rules: Record<string, Rule[]> = {
  userId: [{ required: true, message: '接收用户不能为空' }],
  content: [{ required: true, message: '请输入消息内容', trigger: 'blur' }]
}

const handleSearch = async (value: string) => {
  if (!value) {
    userOptions.value = []
    return
  }
  loading.value = true
  try {
    const { data: res } = await searchUser({ keyword: value })
    userOptions.value = res.rows.map((user: any) => ({
      label: `${user.userName} (${user.nickName})`,
      value: user.userId.toString()
    }))
  } catch (error) {
    console.error('搜索用户失败:', error)
    message.error('搜索用户失败')
  } finally {
    loading.value = false
  }
}

const handleSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    submitting.value = true
    try {
      await signalRService.sendMessage({
        userId: form.userId,
        content: form.content
      })
      message.success('消息发送成功')
      emit('success')
      handleClose()
    } catch (error) {
      console.error('发送消息失败:', error)
      message.error('发送消息失败')
    } finally {
      submitting.value = false
    }
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

const handleClose = () => {
  emit('update:visible', false)
  if (formRef.value) {
    formRef.value.resetFields()
  }
}

const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}
</script>

<style scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 
