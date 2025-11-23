<template>
  <div ref="container" class="error-animation"></div>
</template>

<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount, watch } from 'vue'
import lottie, { AnimationItem } from 'lottie-web'
import error404 from '@/assets/animations/404.json'
import error403 from '@/assets/animations/403.json'
import error401 from '@/assets/animations/401.json'
import error500 from '@/assets/animations/500.json'
import error503 from '@/assets/animations/503.json'

const props = defineProps<{
  type: '404' | '403' | '401' | '500' | '503'
}>()

const container = ref<HTMLElement | null>(null)
let animation: AnimationItem | null = null

const animationMap = {
  '404': error404,
  '403': error403,
  '401': error401,
  '500': error500,
  '503': error503
}

const loadAnimation = () => {
  if (!container.value) return
  
  try {
    // 销毁现有动画
    if (animation) {
      animation.destroy()
    }
    
    // 创建新动画
    animation = lottie.loadAnimation({
      container: container.value,
      renderer: 'svg',
      loop: true,
      autoplay: true,
      animationData: animationMap[props.type]
    })

    // 设置动画速度
    animation.setSpeed(0.8)
  } catch (error) {
    console.error('加载动画失败:', error)
  }
}

// 监听类型变化重新加载动画
watch(() => props.type, loadAnimation)

onMounted(() => {
  loadAnimation()
})

onBeforeUnmount(() => {
  if (animation) {
    animation.destroy()
  }
})
</script>

<style lang="less" scoped>
.error-animation {
  width: 120px;
  height: 120px;
  margin: 0 auto 16px;
  display: flex;
  justify-content: center;
  align-items: center;
  
  :deep(svg) {
    width: 100% !important;
    height: 100% !important;
  }
}
</style> 