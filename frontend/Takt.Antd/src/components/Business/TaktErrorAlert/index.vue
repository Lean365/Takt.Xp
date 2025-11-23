<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: ErrorAlert/index.vue
创建日期: 2024-03-21
描述: 全局错误提示组件
=================================================================== 
-->

<template>
  <a-alert
    v-if="visible"
    :message="message"
    :description="description"
    :type="type"
    show-icon
    banner
    class="Takt-error-alert"
    @close="handleClose"
  >
    <template #icon>
      <warning-outlined v-if="type === 'warning'" />
      <close-circle-outlined v-else-if="type === 'error'" />
    </template>
    <template #action>
      <a-space>
        <a-button type="link" @click="handleRetry" v-if="showRetry">
          <template #icon><reload-outlined /></template>
          重试
        </a-button>
        <a-button type="link" @click="handleClose">
          <template #icon><close-outlined /></template>
          关闭
        </a-button>
      </a-space>
    </template>
  </a-alert>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  WarningOutlined,
  CloseCircleOutlined,
  ReloadOutlined,
  CloseOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()

interface Props {
  visible?: boolean
  type?: 'warning' | 'error'
  message?: string
  description?: string
  showRetry?: boolean
  onRetry?: () => void
  onClose?: () => void
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  type: 'warning',
  message: '',
  description: '',
  showRetry: true,
  onRetry: () => {},
  onClose: () => {}
})

const emit = defineEmits<{
  'update:visible': [value: boolean]
}>()

// 处理重试
const handleRetry = () => {
  props.onRetry()
}

// 处理关闭
const handleClose = () => {
  emit('update:visible', false)
  props.onClose()
}
</script>

<style lang="less" scoped>
.Takt-error-alert {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 1000;
  margin: 0;
  border-radius: 0;
  
  :deep(.ant-alert-message) {
    margin-bottom: 4px;
  }
  
  :deep(.ant-alert-action) {
    margin-top: 4px;
  }
}
</style> 
