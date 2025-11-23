import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useWebSocketStore = defineStore('websocket', () => {
  const ws = ref<WebSocket | null>(null)
  const connected = ref(false)
  const error = ref<string | null>(null)
  const reconnectAttempts = ref(0)
  const maxReconnectAttempts = 5
  const reconnectTimeout = ref<number | null>(null)
  const lastErrorTime = ref<number>(0)
  const errorDisplayInterval = 120000 // 错误提示最小间隔时间（2分钟）
  
  // 获取WebSocket配置
  const getWebSocketConfig = () => {
    const protocol = window.location.protocol === 'https:' ? 'wss:' : 'ws:'
    const host = window.location.hostname
    
    // 根据环境设置不同的端口和路径
    let port: string
    let path: string
    
    if (import.meta.env.DEV) {
      // 开发环境 - 使用Vite的WebSocket服务
      const viteClientPort = '5349'  // Vite默认端口
      const hmrPort = new URL(import.meta.env.VITE_DEV_SERVER_URL || '').port || viteClientPort
      port = hmrPort
      path = ''  // Vite的HMR会自动处理路径
      // console.log('当前环境: 开发环境')
    } else if (import.meta.env.MODE === 'test') {
      // 测试环境
      port = import.meta.env.VITE_TEST_PORT || '5350'
      path = '/ws'
      // console.log('当前环境: 测试环境')
    } else {
      // 生产环境
      port = import.meta.env.VITE_PROD_PORT || '5000'
      path = '/ws'
      console.log('当前环境: 生产环境')
    }
    
    return {
      url: `${protocol}//${host}:${port}${path}`,
      heartbeatInterval: 30000 // 统一心跳间隔30秒，与SignalR保持一致
    }
  }
  
  // 连接 WebSocket
  const connect = () => {
    // 如果是开发环境，不需要手动建立WebSocket连接
    if (import.meta.env.DEV) {
      console.log('开发环境下，Vite会自动处理HMR连接')
      connected.value = true
      error.value = null
      return
    }

    try {
      // 清除之前的连接
      disconnect()
      
      // 获取配置
      const config = getWebSocketConfig()
      console.log('WebSocket连接配置:', config)
      
      // 创建新的 WebSocket 连接
      ws.value = new WebSocket(config.url)
      
      // 连接成功
      ws.value.onopen = () => {
        console.log('服务连接成功')
        connected.value = true
        error.value = null
        reconnectAttempts.value = 0
      }
      
      // 连接关闭
      ws.value.onclose = (event) => {
        console.log('服务连接关闭:', event.code, event.reason)
        connected.value = false
        
        // 只有在非正常关闭时才重连和显示错误
        if (event.code !== 1000 && event.code !== 1001) {
          handleReconnect()
        }
      }
      
      // 连接错误
      ws.value.onerror = (event) => {
        console.log('服务连接错误:', event)
        connected.value = false
        
        // 检查是否应该显示错误提示
        const now = Date.now()
        if (now - lastErrorTime.value > errorDisplayInterval) {
          error.value = '服务连接中断，正在重试...'
          lastErrorTime.value = now
        }
        
        handleReconnect()
      }
      
      // 接收消息
      ws.value.onmessage = (event) => {
        try {
          const data = JSON.parse(event.data)
          console.log('收到消息:', data)
          
          // 处理不同类型的消息
          handleMessage(data)
        } catch (error) {
          console.error('解析消息失败:', error)
        }
      }
      
    } catch (err) {
      console.error('创建WebSocket连接失败:', err)
      error.value = '连接失败，请检查网络设置'
    }
  }
  
  // 断开连接
  const disconnect = () => {
    if (ws.value) {
      ws.value.close(1000, '主动断开连接')
      ws.value = null
    }
    connected.value = false
    error.value = null
    
    // 清除重连定时器
    if (reconnectTimeout.value) {
      clearTimeout(reconnectTimeout.value)
      reconnectTimeout.value = null
    }
  }
  
  // 发送消息
  const send = (message: any) => {
    if (ws.value && connected.value) {
      try {
        const data = JSON.stringify(message)
        ws.value.send(data)
        console.log('发送消息:', data)
        return true
      } catch (error) {
        console.error('发送消息失败:', error)
        return false
      }
    } else {
      console.warn('WebSocket未连接，无法发送消息')
      return false
    }
  }
  
  // 处理重连
  const handleReconnect = () => {
    if (reconnectAttempts.value >= maxReconnectAttempts) {
      error.value = '连接失败，已达到最大重试次数'
      console.log('WebSocket重连失败，已达到最大重试次数')
      return
    }
    
    reconnectAttempts.value++
    const delay = Math.min(1000 * Math.pow(2, reconnectAttempts.value - 1), 30000) // 指数退避，最大30秒
    
    console.log(`WebSocket重连尝试 ${reconnectAttempts.value}/${maxReconnectAttempts}，延迟 ${delay}ms`)
    
    reconnectTimeout.value = window.setTimeout(() => {
      connect()
    }, delay)
  }
  
  // 处理接收到的消息
  const handleMessage = (data: any) => {
    // 根据消息类型进行处理
    switch (data.type) {
      case 'heartbeat':
        // 心跳响应
        console.log('收到心跳响应')
        break
      case 'notification':
        // 通知消息
        console.log('收到通知:', data.message)
        // 这里可以触发全局通知
        break
      case 'data':
        // 数据消息
        console.log('收到数据:', data.payload)
        break
      default:
        console.log('未知消息类型:', data.type)
    }
  }
  
  // 发送心跳
  const sendHeartbeat = () => {
    if (connected.value) {
      send({ type: 'heartbeat', timestamp: Date.now() })
    }
  }
  
  // 清除错误
  const clearError = () => {
    error.value = null
  }
  
  // 重置状态
  const reset = () => {
    disconnect()
    reconnectAttempts.value = 0
    error.value = null
  }
  
  return {
    // 状态
    ws,
    connected,
    error,
    reconnectAttempts,
    maxReconnectAttempts,
    
    // 方法
    connect,
    disconnect,
    send,
    sendHeartbeat,
    clearError,
    reset,
    getWebSocketConfig
  }
})
