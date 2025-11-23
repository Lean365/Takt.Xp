<template>
  <div class="connection-status">
    <a-tooltip :title="statusText">
      <div class="status-indicator" :class="statusClass">
        <a-spin v-if="status === 'Connecting'" size="small" />
        <a-icon v-else :type="statusIcon" />
      </div>
    </a-tooltip>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted, computed, watch } from 'vue'
import { signalRService } from '@/utils/SignalR/service'

const status = ref(signalRService.getConnectionState())
const statusText = ref('')

// 状态样式映射
const statusClass = computed(() => {
  switch (status.value) {
    case 'Connected':
      return 'status-connected'
    case 'Connecting':
      return 'status-connecting'
    case 'Disconnected':
      return 'status-disconnected'
    default:
      return 'status-disconnected'
  }
})

// 状态图标映射
const statusIcon = computed(() => {
  switch (status.value) {
    case 'Connected':
      return 'check-circle'
    case 'Connecting':
      return 'sync'
    case 'Disconnected':
      return 'close-circle'
    default:
      return 'close-circle'
  }
})

// 状态文本映射
watch(status, (newStatus) => {
  switch (newStatus) {
    case 'Connected':
      statusText.value = '已连接'
      break
    case 'Connecting':
      statusText.value = '连接中...'
      break
    case 'Disconnected':
      statusText.value = '未连接'
      break
    default:
      statusText.value = '未连接'
  }
}, { immediate: true })

// 监听连接状态变化
const handleConnectionStateChange = (newState: string) => {
  status.value = newState
}

onMounted(() => {
  signalRService.on('ConnectionStateChanged', handleConnectionStateChange)
})

onUnmounted(() => {
  signalRService.off('ConnectionStateChanged', handleConnectionStateChange)
})
</script>

<style lang="less" scoped>
.connection-status {
  display: inline-flex;
  align-items: center;
  padding: 4px 8px;
  border-radius: 4px;
  background: #f5f5f5;

  .status-indicator {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 20px;
    height: 20px;
    border-radius: 50%;

    &.status-connected {
      color: #52c41a;
    }

    &.status-connecting {
      color: #1890ff;
    }

    &.status-disconnected {
      color: #ff4d4f;
    }
  }
}
</style> 