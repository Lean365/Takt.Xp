<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: Modal/index.vue
创建日期: 2024-03-20
描述: 统一封装的对话框组件
功能特点:
1. 统一管理对话框的显示和隐藏
2. 支持加载状态显示
3. 统一的按钮文本国际化
4. 支持自定义页脚和内容
5. 提供标准的确认和取消事件处理
=================================================================== 
-->

<template>
  <a-modal
    v-model:open="modalOpen"
    :title="title"
    :width="width"
    :confirm-loading="loading"
    :footer="footer === false || footer === null ? null : undefined"
    :mask-closable="maskClosable"
    :keyboard="keyboard"
    :centered="centered"
    :destroy-on-close="destroyOnClose"
    :class="modalClass"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <template v-if="loading">
      <div class="Takt-modal-loading">
        <a-spin />
      </div>
    </template>
    <template v-else>
      <slot></slot>
    </template>
    <template #footer v-if="footer !== false && footer !== null">
      <slot name="footer">
        <a-button key="cancel" @click="handleCancel">{{ finalCancelText }}</a-button>
        <a-button
          key="submit"
          type="primary"
          :loading="confirmLoading"
          @click="handleSubmit"
        >
          {{ finalSubmitText }}
        </a-button>
      </slot>
    </template>
  </a-modal>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'

// === Props 定义 ===
interface Props {
  open: boolean
  title?: string
  width?: number | string
  loading?: boolean
  footer?: null | boolean
  maskClosable?: boolean
  keyboard?: boolean
  centered?: boolean
  destroyOnClose?: boolean
  modalClass?: string
  confirmLoading?: boolean
  submitText?: string
  cancelText?: string
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  title: '',
  width: 520,
  loading: false,
  footer: undefined,
  maskClosable: false,
  keyboard: true,
  centered: false,
  destroyOnClose: false,
  modalClass: '',
  confirmLoading: false,
  submitText: '',
  cancelText: ''
})

// === i18n ===
const { t } = useI18n()

// === 计算属性 ===
const finalSubmitText = computed(() => props.submitText || t('common.button.submit'))
const finalCancelText = computed(() => props.cancelText || t('common.actions.cancel'))

const modalOpen = computed({
  get: () => props.open,
  set: (value: boolean) => {
    emit('update:open', value)
  }
})

// === Emits 定义 ===
const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'ok'): void
  (e: 'cancel'): void
}>()

// === 方法定义 ===
const handleSubmit = () => {
  emit('ok')
}

const handleCancel = () => {
  modalOpen.value = false
  emit('cancel')
}
</script>

<style lang="less" scoped>
.Takt-modal-loading {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 200px;
}
</style> 
