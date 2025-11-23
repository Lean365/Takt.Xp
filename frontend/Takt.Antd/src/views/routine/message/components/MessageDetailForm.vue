<template>
  <Takt-modal
    :open="visible"
    title="消息详情"
    width="600px"
    :mask-closable="false"
    @cancel="handleClose"
    @update:open="handleVisibleChange"
  >
    <a-form layout="vertical" class="message-detail-form">
      <a-form-item label="发送者ID">
        <a-input v-model:value="formData.senderId" disabled />
      </a-form-item>
      
      <a-form-item label="接收者ID">
        <a-input v-model:value="formData.receiverId" disabled />
      </a-form-item>
      
      <a-form-item label="发送时间">
        <a-input v-model:value="formData.createTime" disabled />
      </a-form-item>
      
      <a-form-item label="消息类型">
        <a-input v-model:value="formData.messageType" disabled />
      </a-form-item>
      
      <a-form-item label="消息状态">
        <a-input v-model:value="formData.isRead" disabled />
      </a-form-item>
      
      <a-form-item label="消息内容">
        <a-textarea
          v-model:value="formData.content"
          :rows="6"
          disabled
          class="message-content"
        />
      </a-form-item>
    </a-form>

    <template #footer>
      <a-space>
        <a-button @click="handleClose">关闭</a-button>
      </a-space>
    </template>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import type { TaktSignalRMessage } from '@/types/signalr/signalRMessage'

interface Props {
  visible: boolean
  messageData?: TaktSignalRMessage
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  messageData: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
}>()

const formData = ref({
  senderId: '',
  receiverId: '',
  createTime: '',
  messageType: '',
  isRead: '',
  content: ''
})

// 监听消息数据变化
watch(() => props.messageData, (newVal) => {
  if (newVal) {
    formData.value = {
      senderId: newVal.senderId || '',
      receiverId: newVal.receiverId || '',
      createTime: newVal.createTime ? new Date(newVal.createTime).toLocaleString() : '',
      messageType: getMessageTypeText(newVal.messageType),
      isRead: getMessageStatusText(newVal.isRead),
      content: newVal.content || ''
    }
  }
}, { immediate: true })

// 获取消息类型文本
const getMessageTypeText = (type: number) => {
  const texts: Record<number, string> = {
    1: '系统消息',
    2: '邮件通知',
    3: '通知状态',
    4: '任务状态',
    5: '个人通知',
    6: '系统广播'
  }
  return texts[type] || type.toString()
}

// 获取消息状态文本
const getMessageStatusText = (status: number) => {
  switch (status) {
    case 0:
      return '未读'
    case 1:
      return '已读'
    default:
      return '未知'
  }
}

const handleClose = () => {
  emit('update:visible', false)
}

const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}
</script>

<style lang="less" scoped>
.message-detail-form {
  padding: 16px 0;
}

.message-content {
  font-family: monospace;
  background-color: var(--ant-color-bg-container);
  border: 1px solid var(--ant-color-border);
  border-radius: 4px;
}
</style> 
