<template>
  <router-view v-slot="{ Component }">
    <keep-alive>
      <component :is="Component" :key="rerenderKey" />
    </keep-alive>
  </router-view>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted, watch } from 'vue'
import { useAppStore } from '@/stores/appStore'

// 父视图组件，用于嵌套路由
const appStore = useAppStore()
const rerenderKey = ref(0)

// 监听语言变化，强制重新渲染组件
watch(() => appStore.language, () => {
  rerenderKey.value++
  console.log('[ParentView] 语言变化，强制重新渲染组件')
})

// 监听强制重新渲染事件
const handleForceRerender = () => {
  rerenderKey.value++
  console.log('[ParentView] 收到强制重新渲染事件')
}

onMounted(() => {
  // 监听全局强制重新渲染事件
  window.addEventListener('force-rerender', handleForceRerender)
})

onUnmounted(() => {
  // 清理事件监听器
  window.removeEventListener('force-rerender', handleForceRerender)
})
</script> 