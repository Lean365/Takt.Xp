<template>
  <component :is="currentLayout" />
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useSettingsStore } from '@/stores/settingsStore'
import SideLayout from './side/index.vue'
import TopLayout from './top/index.vue'
import MixLayout from './mix/index.vue'

const settingsStore = useSettingsStore()

// 根据导航模式动态选择布局组件
const currentLayout = computed(() => {
  const navMode = settingsStore.currentNavMode
  switch (navMode) {
    case 'side':
      return SideLayout
    case 'top':
      return TopLayout
    case 'mix':
      return MixLayout
    default:
      return SideLayout // 默认使用侧边栏布局
  }
})
</script>
