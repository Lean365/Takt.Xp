<template>
  <a-dropdown>
    <a-button type="text" class="memorial-button">
      <template #icon>
        <heart-outlined :class="{ active: isMemorialMode }" />
      </template>
    </a-button>
    <template #overlay>
      <a-menu @click="handleMenuClick">
        <a-menu-item key="memorialMode">
          <template #icon>
            <heart-outlined :class="{ active: isMemorialMode }" />
          </template>
          {{ $t('memorial.mode') }}
        </a-menu-item>
        <a-menu-item key="autoMode" :disabled="true">
          <template #icon>
            <sync-outlined :spin="autoMode" />
          </template>
          {{ $t('memorial.autoMode') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { computed, onMounted, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { useMemorialStore } from '@/stores/memorialStore'
import {
  HeartOutlined,
  SyncOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
const memorialStore = useMemorialStore()

const isMemorialMode = computed(() => memorialStore.isMemorialMode)
const autoMode = computed(() => memorialStore.isAutoMode)

const handleMenuClick = (e: { key: string }) => {
  if (e.key === 'memorialMode') {
    if (isMemorialMode.value) {
      memorialStore.disableMemorialMode()
    } else {
      memorialStore.enableMemorialMode()
    }
    persistState()
  }
}

// 从 localStorage 获取持久化的状态
const loadPersistedState = () => {
  const persistedState = localStorage.getItem('memorialState')
  if (persistedState) {
    const { isMemorialMode } = JSON.parse(persistedState)
    if (isMemorialMode) {
      memorialStore.enableMemorialMode()
    }
  }
}

// 持久化状态到 localStorage
const persistState = () => {
  const state = {
    isMemorialMode: memorialStore.isMemorialMode
  }
  localStorage.setItem('memorialState', JSON.stringify(state))
}

// 监听纪念模式的变化
watch(() => memorialStore.isMemorialMode, (newValue) => {
  if (newValue) {
    // 开启纪念模式时，自动关闭自动模式
    memorialStore.isAutoMode = false
  } else {
    // 关闭纪念模式时，自动开启自动模式
    memorialStore.isAutoMode = true
    // 立即检查并应用节日主题
    nextTick(() => {
      memorialStore.checkHolidays()
    })
  }
})

// 组件挂载时加载持久化的状态
onMounted(() => {
  loadPersistedState()
  memorialStore.initMemorialMode()
  // 如果不在纪念模式，确保自动模式开启
  if (!memorialStore.isMemorialMode) {
    memorialStore.isAutoMode = true
    memorialStore.checkHolidays()
  }
})
</script>

<style lang="less" scoped>
.memorial-button {
  .anticon {
    font-size: 16px;
    transition: all 0.3s;
    
    &.active {
      color: var(--ant-color-primary);
    }
  }
}
</style> 