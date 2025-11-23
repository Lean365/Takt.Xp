<template>
  <a-tooltip :title="t('header.fontSize.title')">
    <a-dropdown :trigger="['click']" placement="bottom" class="font-size-dropdown">
      <a-button type="text">
        <template #icon>
          <font-size-outlined />
        </template>
      </a-button>
      <template #overlay>
        <div class="font-size-panel">
          <h4>{{ t('header.fontSize.title') }}</h4>
          <a-slider
            v-model:value="fontSize"
            :min="12"
            :max="20"
            :step="1"
            @change="handleFontSizeChange"
          />
          <div class="font-size-preview">
            <span class="small">A</span>
            <span class="medium">A</span>
            <span class="large">A</span>
          </div>
        </div>
      </template>
    </a-dropdown>
  </a-tooltip>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { FontSizeOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()
const fontSize = ref(14)

// 更新字体大小的函数
const updateFontSize = (size: number) => {
  // 更新根元素字体大小
  document.documentElement.style.fontSize = `${size}px`
  
  // 更新所有文本元素的字体大小
  const elements = document.querySelectorAll('.ant-typography, p, span:not(.anticon), div:not(.ant-dropdown):not(.ant-select-dropdown), h1, h2, h3, h4, h5, h6')
  elements.forEach(el => {
    if (el instanceof HTMLElement) {
      const computedStyle = window.getComputedStyle(el)
      const currentSize = parseFloat(computedStyle.fontSize)
      const ratio = size / 14 // 14px是基准字体大小
      el.style.fontSize = `${currentSize * ratio}px`
    }
  })
}

const handleFontSizeChange = (value: number | [number, number]) => {
  fontSize.value = Array.isArray(value) ? value[0] : value
  updateFontSize(fontSize.value)
  localStorage.setItem('app-font-size', fontSize.value.toString())
}

onMounted(() => {
  // 从本地存储加载字体大小设置
  const savedFontSize = localStorage.getItem('app-font-size')
  if (savedFontSize) {
    const size = parseInt(savedFontSize)
    fontSize.value = size
    updateFontSize(size)
  } else {
    // 如果没有保存的设置，使用默认值
    updateFontSize(14)
  }
})
</script>

<style lang="less" scoped>
.font-size-dropdown {
  display: flex;
  align-items: center;
  justify-content: center;
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
}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  line-height: 1;
}

.font-size-panel {
  padding: 16px;
  width: 200px;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  box-shadow: var(--ant-box-shadow);

  h4 {
    margin: 0 0 16px;
    color: var(--ant-color-text);
    font-size: 14px;
  }
}

.font-size-preview {
  display: flex;
  justify-content: space-between;
  align-items: baseline;
  margin-top: 16px;
  color: var(--ant-color-text);

  .small { font-size: 12px; }
  .medium { font-size: 16px; }
  .large { font-size: 20px; }
}
</style> 