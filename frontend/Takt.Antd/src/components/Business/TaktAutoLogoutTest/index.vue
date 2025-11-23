<!--
===================================================================
项目名 : Lean.Takt
文件名 : TaktAutoLogoutTest/index.vue
创建者 : Claude
创建时间: 2024-03-27
版本号 : v1.0.0
描述    : 自动登出功能测试组件

功能说明:
1. 显示自动登出状态信息
2. 显示Token信息
3. 提供手动测试功能
4. 实时监控状态变化

使用方式:
- 仅在开发环境显示
- 通过控制台 window.TaktAutoLogout 访问调试函数
- 使用抽屉组件，默认隐藏
===================================================================
-->

<template>
  <div>
    <!-- 调试信息 -->
    <div v-if="!isDev" style="position: fixed; top: 10px; right: 10px; background: red; color: white; padding: 5px; z-index: 9999;">
      非开发环境: {{ envMode }}
    </div>
    
    <!-- 触发按钮已移除，改为受控弹窗 -->
    <a-drawer
      :open="props.visible"
      title="自动登出功能测试"
      placement="right"
      width="500"
      :closable="true"
      :mask-closable="true"
      @close="$emit('close')"
    >
      <template #extra>
        <a-button size="small" @click="refreshStatus">刷新状态</a-button>
      </template>
      
      <!-- 环境信息 -->
      <a-alert
        :message="`当前环境: ${envMode}, 开发环境: ${isDev ? '是' : '否'}`"
        type="info"
        show-icon
        style="margin-bottom: 16px;"
      />
      
      <!-- 状态信息 -->
      <a-descriptions title="自动登出状态" :column="2" bordered size="small">
        <a-descriptions-item label="功能启用">
          <a-tag :color="status.isEnabled ? 'green' : 'red'">
            {{ status.isEnabled ? '已启用' : '已禁用' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="用户登录">
          <a-tag :color="status.isLoggedIn ? 'green' : 'red'">
            {{ status.isLoggedIn ? '已登录' : '未登录' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="当前页面">
          <a-tag :color="status.isOnLoginPage ? 'orange' : 'blue'">
            {{ status.isOnLoginPage ? '登录页' : '应用页' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="定时器状态">
          <a-tag :color="status.hasTimer ? 'green' : 'red'">
            {{ status.hasTimer ? '运行中' : '已停止' }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="剩余时间">
          {{ formatTime(status.remainingTime) }}
        </a-descriptions-item>
        <a-descriptions-item label="最后活动">
          {{ formatTime(status.lastActivityTime) }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- Token信息 -->
      <a-descriptions title="Token信息" :column="2" bordered size="small" style="margin-top: 16px;">
        <template #extra>
          <a-switch 
            v-model:checked="showLocalTime" 
            size="small"
            :checked-children="'本地时间'"
            :un-checked-children="'UTC时间'"
          />
        </template>
        <a-descriptions-item label="Token状态">
          <a-tag :color="tokenInfo ? (tokenInfo.isExpired ? 'red' : tokenInfo.isExpiringSoon ? 'orange' : 'green') : 'red'">
            {{ getTokenStatusText() }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="剩余时间">
          {{ tokenInfo ? `${tokenInfo.remainingMinutes}分钟` : '无Token' }}
        </a-descriptions-item>
        <a-descriptions-item label="签发时间">
          {{ tokenInfo?.issuedAt ? formatDate(tokenInfo.issuedAt, showLocalTime) : '无签发时间' }}
        </a-descriptions-item>
        <a-descriptions-item label="过期时间">
          {{ tokenInfo?.expiresAt ? formatDate(tokenInfo.expiresAt, showLocalTime) : '无过期时间' }}
        </a-descriptions-item>
      </a-descriptions>
      
      <!-- 时间说明 -->
      <a-alert
        :message="`JWT Token 使用 UTC 时间以确保跨时区兼容性。当前显示${showLocalTime ? '本地时间（北京时区）' : 'UTC时间'}。`"
        type="info"
        show-icon
        style="margin-top: 8px;"
      />

      <!-- 测试按钮 -->
      <div style="margin-top: 16px;">
        <a-space>
          <a-button size="small" @click="triggerActivity">触发用户活动</a-button>
          <a-button size="small" @click="resetTimer">重置定时器</a-button>
          <a-button size="small" @click="checkAndRefreshTokenAction">检查并刷新Token</a-button>
          <a-button size="small" @click="testAutoRefresh">测试自动刷新</a-button>
        </a-space>
      </div>

      <!-- 日志信息 -->
      <div style="margin-top: 16px;">
        <a-alert
          :message="`调试信息: 使用 window.TaktAutoLogout 在控制台访问更多调试函数`"
          type="info"
          show-icon
        />
      </div>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { getAutoLogoutStatus, resetAutoLogoutTimer, checkAndRefreshToken } from '@/utils/autoLogoutUtil'
import { getToken } from '@/utils/authUtil'

const props = defineProps({
  visible: Boolean
})
const emit = defineEmits(['close'])

// 仅在开发环境显示
const isDev = computed(() => import.meta.env.DEV)
// 当前环境模式
const envMode = import.meta.env.MODE
// 状态数据
const status = ref(getAutoLogoutStatus())
const tokenInfo = ref<any>(null)
// 控制时间显示格式（本地时间 vs UTC时间）
const showLocalTime = ref(true)

// 更新状态
const refreshStatus = () => {
  status.value = getAutoLogoutStatus()
  updateTokenInfo()
}

// 更新Token信息
const updateTokenInfo = () => {
  if (typeof window !== 'undefined' && (window as any).TaktAutoLogout) {
    const tokenInfoData = (window as any).TaktAutoLogout.getTokenInfo()
    // 移除频繁的日志输出，只在开发环境且手动触发时输出
    tokenInfo.value = tokenInfoData
  }
}

// 触发用户活动
const triggerActivity = () => {
  if (typeof window !== 'undefined' && (window as any).TaktAutoLogout) {
    (window as any).TaktAutoLogout.triggerActivity()
    refreshStatus()
  }
}

// 重置定时器
const resetTimer = () => {
  resetAutoLogoutTimer()
  refreshStatus()
}

// 检查并刷新Token
const checkAndRefreshTokenAction = async () => {
  await checkAndRefreshToken()
  refreshStatus()
}

// 测试自动刷新
const testAutoRefresh = async () => {
  if (typeof window !== 'undefined' && (window as any).TaktAutoLogout) {
    await (window as any).TaktAutoLogout.testAutoRefresh()
    refreshStatus()
  }
}

// 获取Token状态文本
const getTokenStatusText = () => {
  if (!tokenInfo.value) return '无Token'
  if (tokenInfo.value.error) return 'Token错误'
  if (tokenInfo.value.isExpired) return '已过期'
  if (tokenInfo.value.isExpiringSoon) return '即将过期'
  return '正常'
}

// 格式化时间
const formatTime = (time: number) => {
  if (!time) return '0秒'
  const minutes = Math.floor(time / 1000 / 60)
  const seconds = Math.floor((time / 1000) % 60)
  return `${minutes}分${seconds}秒`
}

// 格式化日期
const formatDate = (date: Date, useLocalTime: boolean = true) => {
  // 检查日期是否有效
  if (!date || isNaN(date.getTime())) {
    return '无效日期'
  }
  
  if (useLocalTime) {
    // 显示本地时间（服务器时区）
    return date.toLocaleString('zh-CN', {
      timeZone: 'Asia/Shanghai', // 假设服务器在北京时区
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit'
    })
  } else {
    // 显示 UTC 时间
    return date.toLocaleString('zh-CN', {
      timeZone: 'UTC',
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit'
    }) + ' UTC'
  }
}

// 定时更新状态
let statusInterval: NodeJS.Timeout | null = null

onMounted(() => {
  refreshStatus()
  
  // 每30秒更新一次状态，减少不必要的更新
  statusInterval = setInterval(() => {
    refreshStatus()
  }, 30000)
})

onUnmounted(() => {
  if (statusInterval) {
    clearInterval(statusInterval)
  }
})
</script>

<style lang="less" scoped>
// 移除原有的固定定位样式，因为现在使用抽屉组件
</style> 
