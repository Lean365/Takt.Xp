<template>
  <!-- 使用 notification 而不是 modal -->
</template>

<style scoped>
/* 确保通知在 Chrome 中正确显示 */
:deep(.ant-notification) {
  z-index: 9999 !important;
  position: fixed !important;
}

:deep(.ant-notification-notice) {
  z-index: 9999 !important;
  position: relative !important;
  box-shadow: 0 6px 16px 0 rgba(0, 0, 0, 0.08), 0 3px 6px -4px rgba(0, 0, 0, 0.12), 0 9px 28px 8px rgba(0, 0, 0, 0.05) !important;
}

/* 确保在纪念模式下也能正常显示 */
:deep(.memorial-mode .ant-notification) {
  filter: none !important;
}
</style>

<script setup lang="ts">
import { onMounted, onUnmounted, h } from 'vue'
import { ExclamationCircleOutlined } from '@ant-design/icons-vue'
import { notification } from 'ant-design-vue'
import { signalRService } from '@/utils/SignalR/service'

// 格式化时间
const formatTime = (time: Date) => {
  if (!time) return '未知'
  return new Date(time).toLocaleString('zh-CN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit'
  })
}


// 显示通知
const showNotification = (device: any, time: Date) => {
  console.log('[LoginAttemptNotification] 显示登录尝试通知:', { device, time })
  
  try {
    // 使用 notification API 显示通知
    const key = `login-attempt-${Date.now()}`
    
    // 先销毁可能存在的旧通知
    notification.destroy()
    
    // 延迟显示，确保DOM更新完成
    setTimeout(() => {
      notification.warning({
        key,
        message: '登录安全提醒',
        description: '您的账号正在其他设备尝试登录，已自动阻止该登录请求。',
        duration: 0, // 不自动关闭
        placement: 'topRight',
        icon: h(ExclamationCircleOutlined, { style: 'color: #faad14' }),
        style: {
          width: '400px',
          zIndex: 9999 // 确保在最顶层
        }
      })
      
      // 显示详细信息
      if (device) {
        const detailKey = `login-detail-${Date.now()}`
        notification.info({
          key: detailKey,
          message: '登录尝试详情',
          description: `尝试时间: ${formatTime(time)}\n设备ID: ${device.deviceId}\nIP地址: ${device.ipAddress}${device.browser ? `\n浏览器: ${device.browser}` : ''}${device.os ? `\n操作系统: ${device.os}` : ''}`,
          duration: 10, // 10秒后自动关闭
          placement: 'topRight',
          style: {
            zIndex: 9998 // 确保在警告通知下方
          }
        })
      }
      
      console.log('[LoginAttemptNotification] 通知显示成功')
    }, 100) // 延迟100ms显示
    
  } catch (error) {
    console.error('[LoginAttemptNotification] 通知显示失败:', error)
    
    // 降级处理：使用浏览器原生 alert
    const message = `登录安全提醒\n\n您的账号正在其他设备尝试登录，已自动阻止该登录请求。\n\n尝试时间: ${formatTime(time)}\n设备ID: ${device?.deviceId || 'Unknown'}\nIP地址: ${device?.ipAddress || 'Unknown'}`
    alert(message)
  }
}

// 监听SignalR事件
const handleLoginAttempt = (device: any, time: Date) => {
  console.log('[LoginAttemptNotification] 收到登录尝试通知:', { device, time })
  console.log('[LoginAttemptNotification] 设备信息类型:', typeof device)
  console.log('[LoginAttemptNotification] 时间信息类型:', typeof time)
  console.log('[LoginAttemptNotification] 设备信息详情:', JSON.stringify(device, null, 2))
  console.log('[LoginAttemptNotification] 时间信息详情:', time)
  
  // 检查设备信息的各个字段
  if (device) {
    console.log('[LoginAttemptNotification] deviceId:', device.deviceId)
    console.log('[LoginAttemptNotification] ipAddress:', device.ipAddress)
    console.log('[LoginAttemptNotification] 设备信息的所有键:', Object.keys(device))
    console.log('[LoginAttemptNotification] 设备信息的所有值:', Object.values(device))
  } else {
    console.warn('[LoginAttemptNotification] 设备信息为空或undefined')
  }
  
  showNotification(device, time)
}

// 防止重复注册的标记
let isRegistered = false

// 注册SignalR事件监听
const registerSignalREvent = () => {
  if (isRegistered) {
    console.log('[LoginAttemptNotification] 事件监听器已注册，跳过重复注册')
    return
  }
  
  console.log('[LoginAttemptNotification] 注册SignalR事件监听')
  // 先清理可能存在的旧监听器，防止热重载导致的重复注册
  signalRService.off('LoginAttemptDetected', handleLoginAttempt)
  signalRService.on('LoginAttemptDetected', handleLoginAttempt)
  isRegistered = true
}

// 在组件挂载时注册事件监听
onMounted(() => {
  console.log('[LoginAttemptNotification] 组件挂载，开始注册事件监听')
  console.log('[LoginAttemptNotification] 当前SignalR连接状态:', signalRService.getConnectionState())
  
  // 如果SignalR已经连接，直接注册
  if (signalRService.getConnectionState()) {
    console.log('[LoginAttemptNotification] SignalR已连接，直接注册事件监听')
    registerSignalREvent()
  } else {
    console.log('[LoginAttemptNotification] SignalR未连接，监听连接状态变化')
    // 如果SignalR未连接，监听连接状态变化
    signalRService.on('ConnectionStateChanged', (state) => {
      console.log('[LoginAttemptNotification] SignalR连接状态变化:', state)
      if (state) {
        console.log('[LoginAttemptNotification] SignalR连接成功，注册事件监听')
        // 只有在未注册时才注册事件监听
        if (!isRegistered) {
          registerSignalREvent()
        } else {
          console.log('[LoginAttemptNotification] 事件监听器已注册，跳过重复注册')
        }
      }
    })
    
    // 也监听连接成功事件
    signalRService.on('Connected', (connectionId) => {
      console.log('[LoginAttemptNotification] SignalR连接成功事件触发，连接ID:', connectionId)
      // 只有在未注册时才注册事件监听
      if (!isRegistered) {
        registerSignalREvent()
      } else {
        console.log('[LoginAttemptNotification] 事件监听器已注册，跳过重复注册')
      }
    })
  }
})

// 在组件卸载时注销事件监听
onUnmounted(() => {
  console.log('[LoginAttemptNotification] 注销SignalR事件监听')
  signalRService.off('LoginAttemptDetected', handleLoginAttempt)
  signalRService.off('ConnectionStateChanged', registerSignalREvent)
  signalRService.off('Connected', registerSignalREvent)
  isRegistered = false
  console.log('[LoginAttemptNotification] 事件监听器已清理，isRegistered重置为false')
})

// 暴露方法给父组件
defineExpose({
  showNotification
})
</script>
