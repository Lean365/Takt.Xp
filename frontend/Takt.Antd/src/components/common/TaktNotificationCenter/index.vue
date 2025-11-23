//===================================================================
// 项目名 : Lean.Takt
// 文件名称: NotificationCenter.vue
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 通知中心组件
//===================================================================

<template>
  <a-dropdown :trigger="['click']" placement="bottomRight">
    <a-badge :count="unreadCount" :dot="false" class="notification-badge">
      <a-button type="text">
        <template #icon>
          <bell-outlined />
        </template>
      </a-button>
    </a-badge>

    <template #overlay>
      <a-menu>
      <div class="notification-panel">
        <!-- 面板头部 -->
        <div class="panel-header">
          <div class="header-item">
            <h4>{{ t('notification.title') }}</h4>
          </div>
          <div class="header-item center">
            <a-button 
              type="primary"
              size="small" 
              @click="readAll"
              :disabled="unreadCount === 0"
              class="read-all-btn"
            >
              {{ t('notification.readAll') }}
            </a-button>
          </div>
          <div class="header-item right">
            <a-button type="text" size="small" @click="showSettings" class="settings-btn">
              <template #icon>
                <setting-outlined />
              </template>
            </a-button>
          </div>
        </div>

        <!-- 标签页 -->
        <a-tabs v-model:activeKey="activeTab">
          <a-tab-pane :key="TabKeys.ALL" :tab="t('notification.all')">
            <div class="notification-list">
              <a-empty v-if="notifications.length === 0" :description="t('notification.empty')" />
              <a-list v-else :dataSource="notifications">
                <template #renderItem="{ item }">
                  <a-list-item :data-unread="item.status === 'unread'">
                    <Takt-notification-item
                      :data="item"
                      @read="markAsRead"
                      @unread="markAsUnread"
                      @delete="deleteNotification"
                    />
                  </a-list-item>
                </template>
              </a-list>
            </div>
          </a-tab-pane>
          <a-tab-pane :key="TabKeys.UNREAD" :tab="t('notification.unread')">
            <div class="notification-list">
              <a-empty v-if="unreadNotifications.length === 0" :description="t('notification.emptyUnread')" />
              <a-list v-else :dataSource="unreadNotifications">
                <template #renderItem="{ item }">
                  <a-list-item :data-unread="true">
                    <Takt-notification-item
                      :data="item"
                      @read="markAsRead"
                      @unread="markAsUnread"
                      @delete="deleteNotification"
                    />
                  </a-list-item>
                </template>
              </a-list>
            </div>
          </a-tab-pane>
        </a-tabs>

        <!-- 加载更多 -->
        <div v-if="hasMore" class="load-more">
          <a-button type="text" block @click="loadMore">
            {{ t('notification.loadMore') }}
          </a-button>
        </div>
        <div v-else-if="notifications.length > 0" class="no-more">
          {{ t('notification.noMore') }}
        </div>
      </div>
    </a-menu>
    </template>
  </a-dropdown>

  <!-- 设置抽屉 -->
  <a-drawer
    v-model:open="showSettingsDrawer"
    :title="t('notification.settings.title')"
    placement="right"
    width="400"
  >
    <div class="settings-content">
      <h3>{{ t('notification.settings.preferences') }}</h3>
      <a-form layout="vertical">
        <a-form-item :label="t('notification.settings.system')">
          <a-switch v-model:checked="settings.system" />
        </a-form-item>
        <a-form-item :label="t('notification.settings.task')">
          <a-switch v-model:checked="settings.task" />
        </a-form-item>
        <a-form-item :label="t('notification.settings.message')">
          <a-switch v-model:checked="settings.message" />
        </a-form-item>
      </a-form>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted, onUnmounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { BellOutlined, SettingOutlined, ScheduleOutlined, MessageOutlined } from '@ant-design/icons-vue'
import { notification } from 'ant-design-vue'
import TaktNotificationItem from '../TaktNotificationItem/index.vue'

import { signalRService } from '@/utils/SignalR/service'
import { MessageType, type TaktSignalRMessage } from '@/types/routine/signalr/signalRMessage'
import type { NotificationItem as NotificationItemType } from '@/types/settings'
import { getSignalRMessageList, deleteSignalRMessage, markSignalRMessageAsRead, markSignalRMessageAsUnread, markAllSignalRMessagesAsUnread } from '@/api/routine/signalr/signalRMessage'
import { getMailList, markMailAsRead } from '@/api/routine/email/mail'
import { getNoticeList, markNoticeAsRead } from '@/api/routine/notice/notice'
import { useUserStore } from '@/stores/userStore'
import { getToken } from '@/utils/authUtil'
import type { TaktMail } from '@/types/routine/email/mail'
import type { TaktNotice } from '@/types/routine/notice/notice'

const { t } = useI18n()
const userStore = useUserStore()
const currentUserId = userStore.userInfo?.userId?.toString() || ''

// 定义props
const props = defineProps({
  autoShow: { type: Boolean, default: true },
  autoShowSystem: { type: Boolean, default: true },
  notificationDuration: { type: Number, default: 4.5 },
  systemMessageDuration: { type: Number, default: 4.5 },
  onNotificationClick: { type: Function, default: null }
})

// 标签页类型
type TabKey = 'all' | 'unread'

const TabKeys = {
  ALL: 'all',
  UNREAD: 'unread'
} as const

// 状态管理
const activeTab = ref<TabKey>(TabKeys.ALL)
const loading = ref(false)
const hasMore = ref(true)
const showSettingsDrawer = ref(false)
const notifications = ref<NotificationItemType[]>([])

// 从本地存储加载通知
const loadNotificationsFromStorage = () => {
  const storedNotifications = localStorage.getItem('Takt_notifications')
  if (storedNotifications) {
    notifications.value = JSON.parse(storedNotifications)
  }
}

// 保存通知到本地存储
const saveNotificationsToStorage = () => {
  localStorage.setItem('Takt_notifications', JSON.stringify(notifications.value))
}

// 计算属性
const unreadNotifications = computed(() => {
  return notifications.value.filter((item: NotificationItemType) => item.status === 'unread')
})
const unreadCount = computed(() => unreadNotifications.value.length)

// 通知设置
const settings = ref({
  system: true,
  task: true,
  message: true
})

/** 通知类型 */
type NotificationType = 'online' | 'mail' | 'notice'

// 将消息转换为统一的通知项格式
const convertToNotificationItem = (message: TaktSignalRMessage | TaktMail | TaktNotice, type: NotificationType): NotificationItemType => {
  switch (type) {
    case 'online':
      const onlineMessage = message as TaktSignalRMessage
      // 如果signalRMessageId不存在，说明这是系统消息，使用特殊前缀
      const onlineId = onlineMessage.signalRMessageId || `sys_${Date.now()}`
      return {
        id: `online_${onlineId}`,
        title: `消息类型 ${onlineMessage.messageType}` || '在线消息',
        content: onlineMessage.content || '',
        createTime: (onlineMessage.createTime as any) || new Date().toISOString(),
        status: onlineMessage.isRead === 0 ? 'unread' : 'read',
        type: 'system'
      }
    case 'mail':
      const mailMessage = message as TaktMail
      const mailId = mailMessage.mailId || `sys_${Date.now()}`
      return {
        id: `mail_${mailId}`,
        title: mailMessage.mailSubject || '邮件',
        content: mailMessage.mailBody || '',
        createTime: mailMessage.createTime ? (typeof mailMessage.createTime === 'string' ? mailMessage.createTime : mailMessage.createTime) : new Date().toISOString(),
        status: mailMessage.mailStatus === 0 ? 'unread' : 'read',
        type: 'message'
      }
    case 'notice':
      const noticeMessage = message as TaktNotice
      const noticeId = noticeMessage.noticeId || `sys_${Date.now()}`
      return {
        id: `notice_${noticeId}`,
        title: noticeMessage.noticeTitle || '通知',
        content: noticeMessage.noticeContent || '',
        createTime: noticeMessage.createTime ? (typeof noticeMessage.createTime === 'string' ? noticeMessage.createTime : noticeMessage.createTime) : new Date().toISOString(),
        status: noticeMessage.noticeStatus === 0 ? 'unread' : 'read',
        type: 'task'
      }
    default:
      throw new Error(`未知的消息类型: ${type}`)
  }
}

// 加载通知
const loadNotifications = async () => {
  if (loading.value) return
  loading.value = true
  let retryCount = 0
  const maxRetries = 3

  const loadWithRetry = async () => {
    try {
      // 检查用户是否已登录
      const userStore = useUserStore()
      const hasUserInfo = userStore.userInfo !== undefined && userStore.userInfo !== null
      const hasToken = getToken() !== null
      
      // 如果用户信息不存在且Token也不存在，则跳过加载
      if (!hasUserInfo && !hasToken) {
        console.log('[通知中心] 用户未登录且Token不存在，跳过加载消息')
        return
      }

      // 并行加载所有类型的消息
      const [onlineMessages, mailMessages, noticeMessages] = await Promise.all([
        getSignalRMessageList({ pageIndex: 1, pageSize: 10 }),
        getMailList({ pageIndex: 1, pageSize: 10 }),
        getNoticeList({ pageIndex: 1, pageSize: 10 })
      ])

      // 使用 Map 来去重
      const messageMap = new Map()

      // 处理在线消息
      if (onlineMessages?.data?.rows?.length > 0) {
        onlineMessages.data.rows.forEach((msg: TaktSignalRMessage) => {
          const item = convertToNotificationItem(msg, 'online')
          messageMap.set(item.id, item)
        })
      }

      // 处理邮件
      if (mailMessages?.data?.rows?.length > 0) {
        mailMessages.data.rows.forEach((msg: TaktMail) => {
          const item = convertToNotificationItem(msg, 'mail')
          messageMap.set(item.id, item)
        })
      }

      // 处理通知
      if (noticeMessages?.data?.rows?.length > 0) {
        noticeMessages.data.rows.forEach((msg: any) => {
          const item = convertToNotificationItem(msg, 'notice')
          messageMap.set(item.id, item)
        })
      }

      // 如果没有任何消息，直接返回
      if (messageMap.size === 0) {
        console.log('没有新消息')
        notifications.value = []
        saveNotificationsToStorage()
        return
      }

      // 转换为数组并按时间排序
      const newNotifications = Array.from(messageMap.values())
        .sort((a, b) => new Date(b.createTime).getTime() - new Date(a.createTime).getTime())
      
      notifications.value = newNotifications
      saveNotificationsToStorage()
    } catch (error: any) {
      console.error(`加载消息失败 (尝试 ${retryCount + 1}/${maxRetries}):`, {
        error,
        retryCount,
        maxRetries
      })

      // 如果是401未授权错误或403禁止访问错误，不进行重试
      if (error?.response?.status === 401 || error?.response?.status === 403) {
        console.log('[通知中心] 用户未授权或禁止访问，停止重试')
        return
      }

      // 如果是网络错误或其他错误，且还有重试次数，则重试
      if (retryCount < maxRetries) {
        retryCount++
        console.log(`[通知中心] 重试加载消息 (${retryCount}/${maxRetries})`)
        await new Promise(resolve => setTimeout(resolve, 1000))
        return loadWithRetry()
      }
      
      // 重试次数用完，记录错误但不抛出异常
      console.error('[通知中心] 加载消息最终失败，已达到最大重试次数')
    }
  }

  try {
    await loadWithRetry()
  } catch (error) {
    console.error('加载消息最终失败:', error)
  } finally {
    loading.value = false
  }
}

// 加载更多通知
const loadMore = async () => {
  if (loading.value) return
  loading.value = true
  try {
    const pageIndex = Math.ceil(notifications.value.length / 10) + 1
    const pageSize = 10

    // 并行加载所有类型的消息
    const [onlineMessages, mailMessages, noticeMessages] = await Promise.all([
      getSignalRMessageList({ pageIndex, pageSize }),
      getMailList({ pageIndex, pageSize }),
      getNoticeList({ pageIndex, pageSize })
    ])

    // 使用 Map 来去重
    const messageMap = new Map()

    // 处理在线消息
    if (onlineMessages?.data?.rows?.length > 0) {
              onlineMessages.data.rows.forEach((msg: any) => {
        const item = convertToNotificationItem(msg, 'online')
        messageMap.set(item.id, item)
      })
    }

    // 处理邮件
    if (mailMessages?.data?.rows?.length > 0) {
              mailMessages.data.rows.forEach((msg: any) => {
        const item = convertToNotificationItem(msg, 'mail')
        messageMap.set(item.id, item)
      })
    }

    // 处理通知
    if (noticeMessages?.data?.rows?.length > 0) {
              noticeMessages.data.rows.forEach((msg: any) => {
        const item = convertToNotificationItem(msg, 'notice')
        messageMap.set(item.id, item)
      })
    }

    // 如果没有任何新消息，标记为没有更多
    if (messageMap.size === 0) {
      hasMore.value = false
      return
    }

    // 转换为数组并按时间排序
    const newNotifications = Array.from(messageMap.values())
      .sort((a, b) => new Date(b.createTime).getTime() - new Date(a.createTime).getTime())
    
    // 添加新通知
    notifications.value.push(...newNotifications)
  } catch (error) {
    console.error('加载更多消息失败:', error)
  } finally {
    loading.value = false
  }
}

// 标记通知为已读
const markAsRead = async (id: string) => {
  const item = notifications.value.find((item: NotificationItemType) => item.id === id)
  if (!item) return

  const parts = item.id.split('_')
  if (parts.length < 2) {
    console.error('无效的通知ID格式:', item.id)
    return
  }
  
  const [type, realId] = parts
  
  // 检查是否是系统消息（以sys_开头的ID）
  if (realId.startsWith('sys_')) {
    console.log('系统消息，跳过API调用:', item.id)
    // 只更新本地状态
    item.status = 'read'
    saveNotificationsToStorage()
    
    // 如果当前在未读标签页且没有更多未读消息，自动切换到全部标签页
    if (activeTab.value === TabKeys.UNREAD && unreadCount.value === 0) {
      activeTab.value = TabKeys.ALL
    }
    return
  }
  
  if (!realId || isNaN(Number(realId))) {
    console.error('无效的ID:', realId)
    return
  }
  
  try {
    switch (type) {
      case 'mail':
        await markMailAsRead(Number(realId))
        break
      case 'notice':
        await markNoticeAsRead(Number(realId))
        break
      case 'online':
        await markSignalRMessageAsRead(realId)
        break
    }
    item.status = 'read'
    saveNotificationsToStorage() // 保存更新后的状态到本地存储
    
    // 如果当前在未读标签页且没有更多未读消息，自动切换到全部标签页
    if (activeTab.value === TabKeys.UNREAD && unreadCount.value === 0) {
      activeTab.value = TabKeys.ALL
    }
  } catch (error) {
    console.error('标记已读失败:', error)
  }
}

// 标记所有通知为已读
const readAll = async () => {
  if (unreadCount.value === 0) return
  
  try {
    const promises = notifications.value
      .filter(item => item.status === 'unread')
      .map(item => {
        const [type, realId] = item.id.split('_')
        switch (type) {
          case 'mail':
            return markMailAsRead(Number(realId))
          case 'notice':
            return markNoticeAsRead(Number(realId))
          case 'online':
            return markSignalRMessageAsRead(realId)
        }
      })

    await Promise.all(promises)
    notifications.value.forEach(item => {
      if (item.status === 'unread') {
        item.status = 'read'
      }
    })
    saveNotificationsToStorage() // 保存更新后的状态到本地存储
    
    // 标记全部已读后，自动切换到全部标签页
    activeTab.value = TabKeys.ALL
  } catch (error) {
    console.error('标记全部已读失败:', error)
  }
}

// 删除通知
const deleteNotification = async (id: string) => {
  const [type, realId] = id.split('_')
  const index = notifications.value.findIndex(item => item.id === id)
  if (index === -1) return

  try {
    switch (type) {
      case 'online':
        await deleteSignalRMessage(realId)
        break
      // 暂不支持删除邮件和通知
      default:
        return
    }
    notifications.value.splice(index, 1)
  } catch (error) {
    console.error('删除通知失败:', error)
  }
}

// 标记消息为未读
const markAsUnread = async (id: string) => {
  try {
    const parts = id.split('_')
    if (parts.length < 2) {
      console.error('无效的通知ID格式:', id)
      return
    }
    
    const messageId = parts[1]
    // 检查是否是系统消息（以sys_开头的ID）
    if (messageId.startsWith('sys_')) {
      console.log('系统消息，跳过API调用:', id)
      // 只更新本地状态
      const index = notifications.value.findIndex(item => item.id === id)
      if (index !== -1) {
        notifications.value[index].status = 'unread'
        saveNotificationsToStorage()
      }
      return
    }
    
    if (!messageId || isNaN(Number(messageId))) {
      console.error('无效的消息ID:', messageId)
      return
    }
    
    await markSignalRMessageAsUnread(messageId)
    const index = notifications.value.findIndex(item => item.id === id)
    if (index !== -1) {
      notifications.value[index].status = 'unread'
      saveNotificationsToStorage()
    }
  } catch (error) {
    console.error('标记消息为未读失败:', error)
    notification.error({
      message: t('notification.error.unread'),
      description: t('notification.error.unreadDesc')
    })
  }
}

// 标记所有消息为未读
const markAllAsUnread = async () => {
  try {
    await markAllSignalRMessagesAsUnread()
    notifications.value.forEach(item => {
      item.status = 'unread'
    })
    saveNotificationsToStorage()
  } catch (error) {
    console.error('标记所有消息为未读失败:', error)
    notification.error({
      message: t('notification.error.unreadAll'),
      description: t('notification.error.unreadAllDesc')
    })
  }
}

// 显示设置抽屉
const showSettings = () => {
  showSettingsDrawer.value = true
}

// 关闭设置抽屉
const closeSettings = () => {
  showSettingsDrawer.value = false
}

// 处理新消息
const handleNewMessage = (message: any) => {
  console.log('[通知中心] 开始处理新消息')
  console.log('[通知中心] 收到的原始消息:', message)
  
  try {
    // 如果是字符串类型的消息
    if (typeof message === 'string') {
      // 检查是否是连接成功消息
      if (message === '连接成功') {
        console.log('[通知中心] 收到连接成功消息，跳过处理')
        return
      }
      
      // 尝试解析为JSON
      try {
        message = JSON.parse(message)
      } catch (e) {
        console.warn('[通知中心] 消息不是JSON格式，使用原始内容:', message)
        // 如果不是JSON格式，直接作为系统消息处理
        handleSystemMessage({
          type: 'system',
          content: message,
          timestamp: new Date().toISOString()
        })
        return
      }
    }
    
    // 处理不同类型的消息
    if (message && typeof message === 'object') {
      // 处理连接成功消息
      if (message.type === 'connection' && message.status === 'success') {
        console.log('[通知中心] 收到连接成功消息，跳过处理')
        return
      }
      
      // 处理通知消息
      if (message.type === 'notification') {
        handleNotification(message)
      }
      // 处理系统消息（包括 'System' 和 'system'）
      else if (message.type === 'system' || message.type === 'System') {
        handleSystemMessage(message)
      }
      // 处理其他类型的消息
      else {
        console.log('[通知中心] 收到未知类型的消息:', message)
        handleSystemMessage({
          type: 'system',
          content: JSON.stringify(message),
          timestamp: new Date().toISOString()
        })
      }
    }
  } catch (error) {
    console.error('[通知中心] 处理消息时发生错误:', error)
    // 发生错误时，将消息作为系统消息处理
    handleSystemMessage({
      type: 'system',
      content: typeof message === 'string' ? message : JSON.stringify(message),
      timestamp: new Date().toISOString(),
      error: true
    })
  }
}

// 处理通知消息
const handleNotification = (message: any) => {
  console.log('[通知中心] 处理通知消息:', message)
  
  // 添加消息到通知列表
  notifications.value.push({
    id: Date.now().toString(),
    type: 'message',
    title: message.title || '通知',
    content: message.content || '',
    createTime: message.timestamp || new Date().toISOString(),
    status: 'unread'
  })
  
  // 如果设置了自动显示通知，则显示通知
  if (props.autoShow) {
    showNotification(message)
  }
}

// 处理系统消息
const handleSystemMessage = (message: any) => {
  console.log('[通知中心] 处理系统消息:', message)
  
  // 添加消息到系统消息列表
  notifications.value.push({
    id: Date.now().toString(),
    type: 'system',
    title: message.title || '系统消息',
    content: message.content || '',
    createTime: message.timestamp || new Date().toISOString(),
    status: 'unread'
  })
  
  // 如果设置了自动显示系统消息，则显示消息
  if (props.autoShowSystem) {
    showSystemMessage(message)
  }
}

// 显示通知
const showNotification = (message: any) => {
  notification.success({
    message: message.title || '通知',
    description: message.content || '',
    duration: props.notificationDuration,
    onClick: () => {
      // 点击通知时的处理
      handleNotificationClick(message)
    }
  })
}

// 显示系统消息
const showSystemMessage = (message: any) => {
  if (message.error) {
    notification.error({
      message: '系统消息',
      description: message.content || '系统消息',
      duration: props.systemMessageDuration
    })
  } else {
    notification.info({
      message: '系统消息',
      description: message.content || '系统消息',
      duration: props.systemMessageDuration
    })
  }
}

// 处理通知点击
const handleNotificationClick = (message: any) => {
  const notification = notifications.value.find(n => n.id === message.id)
  if (notification) {
    notification.status = 'read'
  }
  
  // 执行自定义点击处理
  if (props.onNotificationClick) {
    props.onNotificationClick(message)
  }
}

// 处理新通知
const handleNewNotification = (notification: any) => {
  console.log('[通知中心] 收到新通知:', notification)
  
  // 创建新通知对象
  const newNotification: NotificationItemType = {
    id: Date.now().toString(),
    title: notification.title,
    content: notification.content,
    type: notification.type || 'notification',
    status: 'unread',
    createTime: notification.timestamp || new Date().toISOString()
  }

  // 检查是否已存在相同内容的通知
  const isDuplicate = notifications.value.some(item => 
    item.content === notification.content && 
    Math.abs(new Date(item.createTime).getTime() - new Date(notification.timestamp).getTime()) < 1000
  )

  if (!isDuplicate) {
    // 使用 nextTick 确保在 DOM 更新后添加新通知
    nextTick(() => {
      notifications.value = [newNotification, ...notifications.value]
      saveNotificationsToStorage()
      // 显示通知提醒
      showNotification(newNotification)
    })
  }
}

// 处理邮件状态更新
const handleMailStatus = (notification: any) => {
  console.log('[通知中心] 收到邮件状态更新:', notification)
  handleNewMessage(notification)
}

// 处理通知状态更新
const handleNoticeStatus = (notification: any) => {
  console.log('[通知中心] 收到通知状态更新:', notification)
  handleNewMessage(notification)
}

onMounted(() => {
  // 先从本地存储加载通知
  loadNotificationsFromStorage()
  
  // 然后加载服务器上的通知
  loadNotifications()
  
  // 确保 SignalR 连接已建立
  const initSignalR = async () => {
    try {
      // 等待 SignalR 连接建立
      if (!signalRService.getConnectionState()) {
        await signalRService.start()
      }

      // 注册消息处理
      signalRService.on('ReceivePersonalNotice', handleNewNotification)
      signalRService.on('ReceiveMailStatus', handleMailStatus)
      signalRService.on('ReceiveNoticeStatus', handleNoticeStatus)
      signalRService.on('ReceiveMessage', handleNewMessage)

      console.log('[通知中心] SignalR 连接已建立，消息处理已注册')
    } catch (error) {
      console.error('[通知中心] SignalR 初始化失败:', error)
    }
  }

  // 初始化 SignalR
  initSignalR()

  // 添加连接状态监听
  signalRService.on('ConnectionStateChanged', (state) => {
    console.log('[通知中心] SignalR 连接状态变化:', state)
    if (state) {
      // 重新注册事件监听
      signalRService.on('ReceivePersonalNotice', handleNewNotification)
      signalRService.on('ReceiveMailStatus', handleMailStatus)
      signalRService.on('ReceiveNoticeStatus', handleNoticeStatus)
      signalRService.on('ReceiveMessage', handleNewMessage)
    }
  })
})

onUnmounted(() => {
  // 注销所有消息处理
  signalRService.off('ReceivePersonalNotice', handleNewNotification)
  signalRService.off('ReceiveMailStatus', handleMailStatus)
  signalRService.off('ReceiveNoticeStatus', handleNoticeStatus)
  signalRService.off('ReceiveMessage', handleNewMessage)
})
</script>

<style lang="less" scoped>
.notification-panel {
  width: 480px;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  box-shadow: 0 6px 16px 0 rgba(0, 0, 0, 0.08);
  z-index: 1100;
  
  .panel-header {
    display: flex;
    align-items: center;
    padding: 16px 20px;
    border-bottom: 1px solid var(--ant-color-border);

    .header-item {
      flex: 1;
      display: flex;
      align-items: center;

      h4 {
        margin: 0;
        color: var(--ant-color-text);
        font-weight: 500;
        font-size: 16px;
      }

      &.center {
        justify-content: center;

        .read-all-btn {
          min-width: 72px;
          height: 28px;
          padding: 0 12px;
          border-radius: 4px;
          font-size: 13px;
          display: inline-flex;
          align-items: center;
          justify-content: center;
          line-height: 1;
          
          &:disabled {
            cursor: not-allowed;
            opacity: 0.65;
            background: var(--ant-color-bg-container-disabled);
            border-color: var(--ant-color-border-disabled);
            color: var(--ant-color-text-disabled);
          }

          &:not(:disabled):hover {
            background: var(--ant-color-primary-hover);
          }
        }
      }

      &.right {
        justify-content: flex-end;
      }
    }

    .settings-btn {
      width: 32px;
      height: 32px;
      padding: 0;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;

      &:hover {
        background-color: var(--ant-color-bg-container-hover);
      }
    }
  }

  .notification-list {
    display: flex;
    flex-direction: column;
    min-height: 200px;
  }

  .notification-list {
    flex: 1;
    padding: 12px;
    
    :deep(.ant-list-item) {
      padding: 16px;
      margin: 0 0 12px;
      border-radius: 8px;
      background: var(--ant-color-bg-container-hover);
      border: 1px solid var(--ant-color-border);

      &:last-child {
        margin-bottom: 0;
      }

      &:hover {
        background-color: var(--ant-color-primary-bg-hover);
        border-color: var(--ant-color-primary-border-hover);
      }
    }
  }

  .load-more {
    text-align: center;
    padding: 12px;
    border-top: 1px solid var(--ant-color-border);
    background: var(--ant-color-bg-container);
    margin-top: auto;  // 将加载更多固定在底部

    .ant-btn {
      width: 100%;
      height: 32px;
      
      &:hover {
        background-color: var(--ant-color-primary-bg);
      }
    }
  }

  .no-more {
    text-align: center;
    padding: 12px;
    border-top: 1px solid var(--ant-color-border);
    background: var(--ant-color-bg-container);
    margin-top: auto;
    color: var(--ant-color-text-secondary);
    font-size: 13px;
  }
}

:deep(.ant-tabs) {
  .ant-tabs-nav {
    margin-bottom: 8px;
    padding: 0 20px;
  }

  .ant-tabs-content {
    padding: 0 8px;
  }
}

.setting-block {
  h4 {
    margin-bottom: 16px;
    color: var(--ant-color-text);
    font-weight: 500;
  }

  .setting-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 16px;
    padding: 8px 12px;
    border-radius: 4px;
    transition: all 0.3s;

    &:hover {
      background-color: var(--ant-color-bg-container-hover);
    }

    span {
      color: var(--ant-color-text);
    }
  }
}

// 修改下拉菜单的样式
:deep(.ant-dropdown) {
  z-index: 1100;
}

:deep(.ant-dropdown-trigger) {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-btn) {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 32px;
  width: 32px;
  padding: 0;
  border-radius: 50%;
  
  &:hover {
    background-color: var(--ant-color-bg-container-hover);
  }

}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
  line-height: 1;
}

// 全局通知样式
.Takt-notification {
  cursor: pointer;
  
  .ant-notification-notice-icon {
    font-size: 24px;
    margin-top: 4px;
  }
  
  .ant-notification-notice-message {
    margin-bottom: 8px;
    color: var(--ant-color-text);
    font-weight: 600;
    font-size: 16px;
  }
  
  .ant-notification-notice-description {
    color: var(--ant-color-text-secondary);
    font-size: 14px;
  }

  .notification-content {
    .message-content {
      margin-bottom: 12px;
      color: var(--ant-color-text-secondary);
    }
  }

  .notification-actions {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-top: 8px;

    .notification-prompt {
      color: var(--ant-color-text-secondary);
      font-size: 13px;
    }

    .read-now-btn {
      margin-left: 12px;
      height: 24px;
      padding: 0 12px;
      font-size: 13px;
      border-radius: 4px;
      
      &:hover {
        opacity: 0.85;
      }
    }
  }
}

// 警告样式通知
.warning-notification {
  .ant-notification-notice {
    background: var(--ant-color-warning-bg);
    border: 1px solid var(--ant-color-warning-border);
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
  }

  .ant-notification-notice-message {
    color: var(--ant-color-warning);
  }

  .ant-notification-notice-description {
    color: var(--ant-color-text);
  }

  .notification-content {
    .message-content {
      color: var(--ant-color-text);
    }
  }

  .notification-actions {
    .notification-prompt {
      color: var(--ant-color-text);
    }

    .read-now-btn {
      background: var(--ant-color-warning);
      border-color: var(--ant-color-warning);
      
      &:hover {
        background: var(--ant-color-warning-hover);
        border-color: var(--ant-color-warning-hover);
      }
    }
  }
}

.notification-badge {
  :deep(.ant-badge-count) {
    box-shadow: 0 0 0 1px #fff;
    font-size: 12px;
    height: 20px;
    min-width: 20px;
    line-height: 20px;
    padding: 0 6px;
  }
}
</style> 

