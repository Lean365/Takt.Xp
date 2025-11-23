<template>
  <Takt-modal
    :open="open"
    :title="t('core.language.detail.title')"
    width="800px"
    @update:open="handleVisibleChange"
    @cancel="handleCancel"
  >
    <a-spin :spinning="loading">
      <a-descriptions
        :column="2"
        bordered
        class="language-detail"
        size="small"
      >
        <a-descriptions-item :label="t('core.language.fields.langCode.label')">
          {{ detail?.langCode }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.langName.label')">
          {{ detail?.langName }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.langIcon.label')">
          {{ detail?.langIcon }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.orderNum.label')">
          {{ detail?.orderNum }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.isBuiltin.label')">
          <Takt-dict-tag dict-type="sys_yes_no" :value="detail?.isBuiltin ?? 0" />
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.isDefault.label')">
          <Takt-dict-tag dict-type="sys_yes_no" :value="detail?.isDefault ?? 0" />
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.status.label')">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="detail?.status ?? 0" />
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('core.language.fields.remark.label')">
          {{ detail?.remark }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('table.columns.createBy')">
          {{ detail?.createBy }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('table.columns.createTime')">
          {{ detail?.createTime }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('table.columns.updateBy')">
          {{ detail?.updateBy }}
        </a-descriptions-item>
        
        <a-descriptions-item :label="t('table.columns.updateTime')">
          {{ detail?.updateTime }}
        </a-descriptions-item>
      </a-descriptions>
    </a-spin>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktLanguage } from '@/types/routine/i18n/language'
import { getByIdAsync } from '@/api/routine/i18n/language'

const { t } = useI18n()

// ==================== Props 和 Emits ====================
const props = defineProps<{
  open: boolean
  languageId: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

// ==================== 状态定义 ====================
const loading = ref(false)
const detail = ref<TaktLanguage>()

// ==================== 方法定义 ====================
// 获取语言详情
const fetchLanguageDetail = async () => {
  if (!props.languageId) return

  try {
    loading.value = true
    const res = await getByIdAsync(props.languageId)
    if (res.data) {
      detail.value = res.data
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取语言详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:open', false)
}

// 处理可见性变化
const handleVisibleChange = (visible: boolean) => {
  emit('update:open', visible)
}

// ==================== 监听器 ====================
// 监听对话框打开和语言ID变化
watch(
  () => [props.open, props.languageId],
  ([newOpen, newLanguageId]) => {
    if (newOpen && newLanguageId) {
      fetchLanguageDetail()
    }
  },
  { immediate: true }
)

// ==================== 生命周期 ====================
onMounted(() => {
  if (props.open && props.languageId) {
    fetchLanguageDetail()
  }
})
</script>

<style lang="less" scoped>
.language-detail {
  padding: 24px;

  :deep(.ant-descriptions-item-label) {
    width: 120px;
    font-weight: 500;
  }
}
</style>
