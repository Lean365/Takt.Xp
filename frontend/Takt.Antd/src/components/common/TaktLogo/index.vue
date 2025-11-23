<template>
  <div class="Takt-logo" :class="{ collapsed }" @click="handleClick">
    <img 
      v-if="settingsStore.isShowLogo"
      :src="logoSrc" 
      :alt="logoConfig.alt"
      :width="logoDimensions.width"
      :height="logoDimensions.height"
      class="logo-image"
      :style="{ width: logoDimensions.width + 'px', height: logoDimensions.height + 'px' }"
    />
    <h1 
      v-show="!collapsed && settingsStore.isShowTitle && (logoConfig.showText !== false)" 
      class="logo-text"
      :class="{ 'animate-title': settingsStore.isAnimateTitle }"
      :style="textStyle"
    >
      {{ logoConfig.text || 'Lean.Takt' }}
    </h1>
  </div>
</template>

<script lang="ts" setup>
import { computed, onMounted, watch } from 'vue'
import { useSettingsStore } from '@/stores/settingsStore'

interface Props {
  collapsed?: boolean
  clickable?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  collapsed: false,
  clickable: true
})

const emit = defineEmits<{
  click: [event: MouseEvent]
}>()

const settingsStore = useSettingsStore()

// 获取Logo配置
const logoConfig = computed(() => {
  // 如果配置不完整，提供默认配置
  if (!settingsStore.config.logo || !settingsStore.config.logo.showText) {
    return {
      src: '@/assets/images/logo.svg',
      alt: 'Lean.Takt Logo',
      width: 32,
      height: 32,
      showText: true,
      text: 'Lean.Takt',
      textSize: 18,
      textWeight: 600
    }
  }
  
  return settingsStore.config.logo
})

// Logo尺寸固定为medium大小
const logoDimensions = computed(() => ({
  width: 32,
  height: 32
}))

// 获取Logo图片路径
const logoSrc = computed(() => {
  // 安全检查
  if (!logoConfig.value || !logoConfig.value.src) {
    return ''
  }
  
  // 如果是@开头的路径，使用动态导入
  if (logoConfig.value.src.startsWith('@/')) {
    return new URL('@/assets/images/logo.svg', import.meta.url).href
  }
  
  // 如果是旧的路径，使用新的路径
  if (logoConfig.value.src === '/logo.svg') {
    return new URL('@/assets/images/logo.svg', import.meta.url).href
  }
  
  return logoConfig.value.src
})

// 文字样式
const textStyle = computed(() => ({
  fontSize: `${logoConfig.value?.textSize || 18}px`,
  fontWeight: logoConfig.value?.textWeight || 600
}))

// 点击处理
const handleClick = (event: MouseEvent) => {
  if (props.clickable) {
    emit('click', event)
  }
}

// 组件挂载时检查配置完整性
onMounted(() => {
  // 如果logo配置不完整，重置配置
  if (!settingsStore.config.logo || !settingsStore.config.logo.showText) {
    settingsStore.resetConfig()
  }
})

// 监听animateTitle配置变化
watch(
  () => settingsStore.isAnimateTitle,
  (newAnimateTitle) => {
    console.log('[TaktLogo] animateTitle配置变化:', newAnimateTitle)
  }
)
</script>

<style lang="less" scoped>
.Takt-logo {
  height: 64px;
  padding: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.3s;
  background: var(--ant-menu-inline-bg);

  &.collapsed {
    padding: 16px 8px;
  }

  .logo-image {
    transition: all 0.3s;
  }

  .logo-text {
    height: 32px;
    margin: 0 0 0 12px;
    line-height: 32px;
    vertical-align: middle;
    animation: fade-in 0.3s;
    color: var(--ant-color-text);
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }

  &:hover {
    background: var(--ant-menu-inline-bg-hover);
  }
}

@keyframes fade-in {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

// 标题动画效果
.animate-title {
  animation: title-glow 3s ease-in-out infinite;
  background: linear-gradient(45deg, var(--ant-color-primary), var(--ant-color-primary-hover), var(--ant-color-primary));
  background-size: 200% 200%;
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
}

@keyframes title-glow {
  0%, 100% {
    background-position: 0% 50%;
    transform: scale(1);
  }
  25% {
    background-position: 100% 50%;
    transform: scale(1.02);
  }
  50% {
    background-position: 100% 50%;
    transform: scale(1.05);
  }
  75% {
    background-position: 0% 50%;
    transform: scale(1.02);
  }
}

// 响应式设计
@media screen and (max-width: 768px) {
  .Takt-logo {
    padding: 12px;
    
    .logo-text {
      font-size: 16px !important;
    }
  }
}
</style> 
