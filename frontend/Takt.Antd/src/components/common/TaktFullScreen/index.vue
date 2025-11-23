//===================================================================
// 项目名 : Lean.Takt
// 文件名称: FullScreen.vue
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 全屏切换组件
//===================================================================

<template>
  <a-tooltip :title="isFullscreen ? t('header.fullscreen.exit') : t('header.fullscreen.enter')">
    <a-button type="text" @click="toggleFullscreen">
      <template #icon>
        <component :is="isFullscreen ? 'FullscreenExitOutlined' : 'FullscreenOutlined'" />
      </template>
    </a-button>
  </a-tooltip>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'

const { t } = useI18n()
const isFullscreen = ref(false)

// 切换全屏
const toggleFullscreen = async () => {
  try {
    if (!document.fullscreenElement) {
      await document.documentElement.requestFullscreen()
    } else {
      await document.exitFullscreen()
    }
  } catch (err) {
    message.error(t('header.fullscreen.error'))
  }
}

// 监听全屏变化
const handleFullscreenChange = () => {
  isFullscreen.value = !!document.fullscreenElement
}

onMounted(() => {
  document.addEventListener('fullscreenchange', handleFullscreenChange)
})

onUnmounted(() => {
  document.removeEventListener('fullscreenchange', handleFullscreenChange)
})
</script>

<style lang="less" scoped>
// 全屏组件样式
</style> 
