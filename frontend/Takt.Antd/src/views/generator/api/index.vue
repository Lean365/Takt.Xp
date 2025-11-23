<template>
  <div style="height: calc(100vh - 94.5px);">
    <iframe 
      v-show="!loading"
      :src="src" 
      style="width: 100%; height: 100%; border: none;" 
      frameborder="no" 
      scrolling="auto" 
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'

const src = ref(import.meta.env.VITE_API_BASE_URL + '/swagger/index.html')
const loading = ref(true)

const updateHeight = () => {
  const container = document.querySelector('.api-container') as HTMLElement
  if (container) {
    container.style.height = `${document.documentElement.clientHeight - 94.5}px`
  }
}

onMounted(() => {
  updateHeight()
  window.addEventListener('resize', updateHeight)
  
  setTimeout(() => {
    loading.value = false
  }, 230)
})

onUnmounted(() => {
  window.removeEventListener('resize', updateHeight)
})
</script>

<style lang="less" scoped>
.api-layout {
  height: 100%;
  background: #fff;
}

.api-content {
  padding: 0;
  margin: 0;
  height: 100%;
  overflow: hidden;
}

.api-iframe {
  width: 100%;
  height: 100%;
  border: none;
}
</style> 