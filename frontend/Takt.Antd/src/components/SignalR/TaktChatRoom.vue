<template>
  <div class="chat-room">
    <div class="chat-messages" ref="messagesContainer">
      <div v-for="(msg, index) in messages" :key="index" class="message">
        <span class="time">{{ msg.time }}</span>
        <span class="content">{{ msg.content }}</span>
      </div>
    </div>
    <div class="chat-input">
      <a-input
        v-model:value="inputMessage"
        placeholder="请输入消息"
        @pressEnter="sendMessage"
      >
        <template #suffix>
          <a-button type="primary" @click="sendMessage">发送</a-button>
        </template>
      </a-input>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { signalRService } from '@/utils/SignalR/service'
import { message } from 'ant-design-vue'

interface ChatMessage {
  content: string
  time: string
}

const messages = ref<ChatMessage[]>([])
const inputMessage = ref('')
const messagesContainer = ref<HTMLElement | null>(null)

// 发送消息
const sendMessage = async () => {
  if (!inputMessage.value.trim()) return

  try {
    await signalRService.sendMessage({ userId: '', content: inputMessage.value })
    inputMessage.value = ''
  } catch (error) {
    message.error('发送消息失败')
  }
}

// 滚动到底部
const scrollToBottom = () => {
  if (messagesContainer.value) {
    messagesContainer.value.scrollTop = messagesContainer.value.scrollHeight
  }
}

// 监听消息
const handleReceiveMessage = (content: string) => {
  messages.value.push({
    content,
    time: new Date().toLocaleTimeString()
  })
  scrollToBottom()
}

onMounted(() => {
  // 注册消息处理
  signalRService.on('ReceiveMessage', handleReceiveMessage)
})

onUnmounted(() => {
  // 移除消息处理
  signalRService.off('ReceiveMessage', handleReceiveMessage)
})
</script>

<style lang="less" scoped>
.chat-room {
  display: flex;
  flex-direction: column;
  height: 100%;
  padding: 16px;
  background: #fff;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);

  .chat-messages {
    flex: 1;
    overflow-y: auto;
    margin-bottom: 16px;
    padding: 16px;
    background: #f5f5f5;
    border-radius: 4px;

    .message {
      margin-bottom: 8px;
      
      .time {
        color: #999;
        font-size: 12px;
        margin-right: 8px;
      }

      .content {
        color: #333;
      }
    }
  }

  .chat-input {
    :deep(.ant-input-affix-wrapper) {
      .ant-input-suffix {
        padding-right: 0;
      }
    }
  }
}
</style> 