<template>
  <a-config-provider :theme="themeConfig" :locale="antdLocale">
    <div class="app-container">
      <TaktErrorAlert
        v-model:visible="showError"
        :type="errorType"
        :message="errorMessage"
        :description="errorDescription"
        :show-retry="true"
        @retry="handleRetry"
        @close="handleErrorClose"
      />
      <TaktWatermark>
        <router-view></router-view>
      </TaktWatermark>
      
      <!-- 登录尝试通知组件 -->
      <LoginAttemptNotification ref="notificationComponent" />
    </div>
  </a-config-provider>
</template>

<script lang="ts" setup>
import 'ant-design-vue/dist/reset.css'
import { ConfigProvider } from 'ant-design-vue'
import { onMounted, computed, watch, onUnmounted, ref, nextTick } from 'vue'
import { theme } from 'ant-design-vue'
import { useThemeStore } from '@/stores/themeStore'
import { useMemorialStore } from '@/stores/memorialStore'
import { useAppStore } from '@/stores/appStore'
import zhCN from 'ant-design-vue/es/locale/zh_CN'
import enUS from 'ant-design-vue/es/locale/en_US'
import arEG from 'ant-design-vue/es/locale/ar_EG'
import esES from 'ant-design-vue/es/locale/es_ES'
import frFR from 'ant-design-vue/es/locale/fr_FR'
import jaJP from 'ant-design-vue/es/locale/ja_JP'
import koKR from 'ant-design-vue/es/locale/ko_KR'
import ruRU from 'ant-design-vue/es/locale/ru_RU'
import zhTW from 'ant-design-vue/es/locale/zh_TW'
import { initAutoLogout, clearAutoLogout } from '@/utils/autoLogoutUtil'
import { useDictStore } from '@/stores/dictStore'
import { useWebSocketStore } from '@/stores/websocketStore'
import { useUserStore } from '@/stores/userStore'
import { signalRService } from '@/utils/SignalR/service'
import LoginAttemptNotification from '@/components/security/LoginAttemptNotification.vue'

// 组件引用
const notificationComponent = ref()

// 全局测试方法
window.testLoginNotification = () => {
  console.log('[App] 开始测试登录通知功能')
  
  // 尝试通过 ref 获取组件实例
  if (notificationComponent.value) {
    const component = notificationComponent.value
    if (component && component.testNotification) {
      console.log('[App] 找到测试通知方法，开始执行')
      component.testNotification()
    } else {
      console.error('[App] 无法找到测试通知方法')
    }
  } else {
    console.error('[App] 无法找到LoginAttemptNotification组件')
  }
}

// 添加调试信息
window.debugSignalR = () => {
  console.log('[App] SignalR连接状态:', signalRService.getConnectionState())
  console.log('[App] SignalR连接ID:', signalRService.getConnectionId())
  console.log('[App] 测试发送登录尝试通知')
  
  // 模拟后端发送的通知
  const testDevice = {
    DeviceId: 'test-device-123',
    IpAddress: '192.168.1.100',
    Browser: 'Chrome 140.0.0',
    OS: 'Windows 10',
    Message: '测试消息'
  }
  
  // 直接触发事件
  signalRService.emit('LoginAttemptDetected', testDevice, new Date())
}

const themeStore = useThemeStore()
const memorialStore = useMemorialStore()
const appStore = useAppStore()
const wsStore = useWebSocketStore()
const userStore = useUserStore()
const isDark = computed(() => themeStore.isDarkMode)
const currentTheme = computed(() => memorialStore.currentTheme)
const isMemorialMode = computed(() => memorialStore.isMemorialMode)

// 语言包映射
const localeMap = {
  'en-US': enUS,
  'zh-CN': zhCN,
  'ar-SA': arEG,
  'es-ES': esES,
  'fr-FR': frFR,
  'ja-JP': jaJP,
  'ko-KR': koKR,
  'ru-RU': ruRU,
  'zh-TW': zhTW
}

// 当前 Ant Design Vue 的语言包
const currentAntdLocale = ref(localeMap[appStore.language as keyof typeof localeMap] || zhCN)

// 根据当前语言获取 Ant Design Vue 的语言包
const antdLocale = computed(() => currentAntdLocale.value)

// 监听语言变化，更新 Ant Design Vue 的语言包
watch(() => appStore.language, async (newLocale) => {
  currentAntdLocale.value = localeMap[newLocale as keyof typeof localeMap] || zhCN
  
  // 等待下一个tick，确保语言切换生效
  await nextTick()
  
  // 触发全局强制重新渲染事件
  window.dispatchEvent(new CustomEvent('force-rerender', {
    detail: { language: newLocale }
  }))
  
  console.log('[App] 语言切换完成，已触发页面重新渲染')
})

// 计算主题配置
const themeConfig = computed(() => {
  const memorialTheme = memorialStore.currentTheme?.token || {}
  const isDarkMode = themeStore.isDarkMode

  return {
    algorithm: isDarkMode ? theme.darkAlgorithm : theme.defaultAlgorithm,
    token: {
      ...memorialTheme,
      // 优先使用用户设置的主色调，只有在纪念模式下才使用纪念主题的主色调
      colorPrimary: memorialStore.isMemorialMode && memorialTheme.colorPrimary 
        ? memorialTheme.colorPrimary 
        : themeStore.primaryColor,
      borderRadius: 6,
      // 添加更多全局 token
      wireframe: false, // 线框模式
      colorBgContainer: isDarkMode ? '#141414' : '#ffffff',
      colorBgLayout: isDarkMode ? '#000000' : '#f5f5f5',
      colorBgElevated: isDarkMode ? '#1f1f1f' : '#ffffff',
      // 文字颜色
      colorText: isDarkMode ? 'rgba(255, 255, 255, 0.85)' : 'rgba(0, 0, 0, 0.88)',
      colorTextSecondary: isDarkMode ? 'rgba(255, 255, 255, 0.65)' : 'rgba(0, 0, 0, 0.65)',
      colorTextTertiary: isDarkMode ? 'rgba(255, 255, 255, 0.45)' : 'rgba(0, 0, 0, 0.45)',
      colorTextQuaternary: isDarkMode ? 'rgba(255, 255, 255, 0.25)' : 'rgba(0, 0, 0, 0.25)',
      // 输入框
      colorBgContainer_input: isDarkMode ? '#000000' : '#ffffff',
      colorBorder: isDarkMode ? '#424242' : '#d9d9d9',
      colorBorderSecondary: isDarkMode ? '#303030' : '#f0f0f0'
    },
    components: {
      Layout: {
        colorBgHeader: 'var(--ant-color-bg-container)',
        colorBgBody: 'var(--ant-color-bg-layout)',
        colorBgTrigger: 'var(--ant-color-bg-container)'
      },
      Menu: {
        colorItemBg: 'var(--ant-color-bg-container)',
        colorSubItemBg: 'var(--ant-color-bg-container)',
        colorItemBgSelected: 'var(--ant-color-primary-1)',
        colorItemBgActive: 'var(--ant-color-primary-1)'
      },
      Card: {
        colorBgContainer: 'var(--ant-color-bg-container)'
      },
      Input: {
        colorBgContainer: 'var(--ant-color-bg-container_input)',
        colorBorder: 'var(--ant-color-border)',
        colorText: 'var(--ant-color-text)'
      },
      Form: {
        labelColor: 'var(--ant-color-text)',
        colorText: 'var(--ant-color-text)'
      }
    }
  }
})

// 错误提示相关
const showError = ref(false)
const errorType = ref<'warning' | 'error'>('warning')
const errorMessage = ref('')
const errorDescription = ref('')

// 监听 WebSocket 错误
const handleWebSocketError = () => {
  if (wsStore.error) {
    errorType.value = 'error'
    errorMessage.value = '连接错误'
    errorDescription.value = wsStore.error
    showError.value = true
  }
}

// 监听 WebSocket 连接状态
const handleWebSocketConnection = () => {
  if (!wsStore.connected) {
    errorType.value = 'warning'
    errorMessage.value = '连接断开'
    errorDescription.value = '正在尝试重新连接...'
    showError.value = true
  } else {
    showError.value = false
  }
}

// 处理重试
const handleRetry = () => {
  wsStore.connect()
}

// 处理关闭错误提示
const handleErrorClose = () => {
  showError.value = false
}

// 组件挂载时初始化
onMounted(async () => {
  const dictStore = useDictStore()
  dictStore.clearCache()
  themeStore.initTheme()
  memorialStore.initMemorialMode()
  document.documentElement.style.colorScheme = isDark.value ? 'dark' : 'light'
  initAutoLogout(userStore)
  wsStore.connect()
  
  // 如果用户已登录，启动SignalR连接以接收登录尝试通知
  if (userStore.isLoggedIn) {
    try {
      console.log('[App] 用户已登录，开始启动SignalR连接...')
      await signalRService.start()
      console.log('[App] SignalR连接已启动，可以接收登录尝试通知')
      console.log('[App] SignalR连接状态:', signalRService.getConnectionState())
    } catch (error) {
      console.error('[App] SignalR连接启动失败:', error)
    }
  } else {
    console.log('[App] 用户未登录，跳过SignalR连接启动')
  }
})

onUnmounted(() => {
  clearAutoLogout()
  wsStore.disconnect()
})

watch(isDark, (newValue) => {
  document.documentElement.style.colorScheme = newValue ? 'dark' : 'light'
})

// 监听纪念模式的变化
watch(isMemorialMode, (newValue) => {
  if (newValue) {
    document.body.classList.add('memorial-mode')
  } else {
    document.body.classList.remove('memorial-mode')
    // 当纪念模式关闭时，确保自动模式开启
    nextTick(() => {
      memorialStore.checkHolidays()
    })
  }
})

// 监听 WebSocket 状态变化
watch(() => wsStore.error, handleWebSocketError)
watch(() => wsStore.connected, handleWebSocketConnection)

// 监听用户登录状态变化，启动SignalR连接
watch(() => userStore.isLoggedIn, async (isLoggedIn) => {
  console.log('[App] 用户登录状态变化:', isLoggedIn)
  if (isLoggedIn) {
    try {
      console.log('[App] 用户登录，开始启动SignalR连接...')
      await signalRService.start()
      console.log('[App] 用户登录，SignalR连接已启动，可以接收登录尝试通知')
      console.log('[App] SignalR连接状态:', signalRService.getConnectionState())
    } catch (error) {
      console.error('[App] 用户登录后SignalR连接启动失败:', error)
    }
  } else {
    // 用户登出时停止SignalR连接
    try {
      console.log('[App] 用户登出，开始停止SignalR连接...')
      await signalRService.stop()
      console.log('[App] 用户登出，SignalR连接已停止')
    } catch (error) {
      console.error('[App] 用户登出后SignalR连接停止失败:', error)
    }
  }
})
</script>

<style lang="less">
.app-container {
  min-height: 100vh;
  background-color: var(--ant-color-bg-layout);
}

// 亮色主题下的纪念模式
body:not(.dark-mode).memorial-mode,
body:not(.dark-mode).memorial-mode .ant-dropdown,
body:not(.dark-mode).memorial-mode .ant-modal-root,
body:not(.dark-mode).memorial-mode .ant-message,
body:not(.dark-mode).memorial-mode .ant-notification,
body:not(.dark-mode).memorial-mode .ant-drawer {
  filter: grayscale(100%) contrast(90%) brightness(90%);
  transition: filter 0.3s ease;
}

// 暗黑主题下的纪念模式
body.dark-mode.memorial-mode,
body.dark-mode.memorial-mode .ant-dropdown,
body.dark-mode.memorial-mode .ant-modal-root,
body.dark-mode.memorial-mode .ant-message,
body.dark-mode.memorial-mode .ant-notification,
body.dark-mode.memorial-mode .ant-drawer {
  filter: grayscale(100%) contrast(70%) brightness(70%);
  transition: filter 0.3s ease;
}
</style>


