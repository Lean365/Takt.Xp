<template>
  <a-modal
    v-model:open="visible"
    :title="t('workflow.instance.detail')"
    width="800px"
    :footer="null"
    @cancel="handleCancel"
  >
    <div v-if="instanceData" class="instance-detail">
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.instance.title')">
          {{ instanceData.instanceTitle }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.businessKey')">
          {{ instanceData.businessKey || t('common.none') }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.status')">
          <a-tag :color="getStatusColor(instanceData.status)">
            {{ getStatusText(instanceData.status) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.priority')">
          <a-tag :color="getPriorityColor(instanceData.priority)">
            {{ getPriorityText(instanceData.priority) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.urgency')">
          <a-tag :color="getUrgencyColor(instanceData.urgency)">
            {{ getUrgencyText(instanceData.urgency) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.currentNode')">
          {{ instanceData.currentNodeName || t('common.none') }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.startTime')">
          {{ instanceData.startTime || t('common.none') }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.endTime')">
          {{ instanceData.endTime || t('common.none') }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.formType')">
          {{ getFormTypeText(instanceData.formType) }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.createTime')">
          {{ instanceData.createTime }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.updateTime')">
          {{ instanceData.updateTime }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.remark')" :span="2">
          {{ instanceData.remark || t('common.none') }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 流程变量 -->
      <div v-if="instanceData.variables" class="variables-section">
        <h4>{{ t('workflow.instance.variables') }}</h4>
        <pre class="variables-content">{{ formatVariables(instanceData.variables) }}</pre>
      </div>

      <!-- 表单数据 -->
      <div v-if="instanceData.formData" class="form-data-section">
        <h4>{{ t('workflow.instance.formData') }}</h4>
        <pre class="form-data-content">{{ formatFormData(instanceData.formData) }}</pre>
      </div>
    </div>

    <div v-else class="loading-container">
      <a-spin size="large" />
    </div>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktInstance } from '@/types/workflow/instance'
import { getInstanceById } from '@/api/workflow/instance'

// 国际化
const { t } = useI18n()

// Props
interface Props {
  visible: boolean
  instanceId: string
}

const props = defineProps<Props>()

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  refresh: []
}>()

// 响应式数据
const instanceData = ref<TaktInstance | null>(null)
const loading = ref(false)

// 计算属性
const visible = computed({
  get: () => props.visible,
  set: (value) => emit('update:visible', value)
})

// 获取状态颜色
const getStatusColor = (status: number) => {
  const colors = {
    0: 'default',    // 草稿
    1: 'processing', // 运行中
    2: 'success',    // 已完成
    3: 'error'       // 已停用
  }
  return colors[status as keyof typeof colors] || 'default'
}

// 获取状态文本
const getStatusText = (status: number) => {
  const texts = {
    0: t('workflow.instance.status.draft'),
    1: t('workflow.instance.status.running'),
    2: t('workflow.instance.status.completed'),
    3: t('workflow.instance.status.stopped')
  }
  return texts[status as keyof typeof texts] || t('common.unknown')
}

// 获取优先级颜色
const getPriorityColor = (priority: number) => {
  const colors = {
    1: 'default',    // 低
    2: 'blue',       // 普通
    3: 'orange',     // 高
    4: 'red',        // 紧急
    5: 'purple'      // 特急
  }
  return colors[priority as keyof typeof colors] || 'default'
}

// 获取优先级文本
const getPriorityText = (priority: number) => {
  const texts = {
    1: t('workflow.instance.priority.low'),
    2: t('workflow.instance.priority.normal'),
    3: t('workflow.instance.priority.high'),
    4: t('workflow.instance.priority.urgent'),
    5: t('workflow.instance.priority.critical')
  }
  return texts[priority as keyof typeof texts] || t('common.unknown')
}

// 获取紧急程度颜色
const getUrgencyColor = (urgency: number) => {
  const colors = {
    1: 'default',    // 普通
    2: 'orange',     // 加急
    3: 'red'         // 特急
  }
  return colors[urgency as keyof typeof colors] || 'default'
}

// 获取紧急程度文本
const getUrgencyText = (urgency: number) => {
  const texts = {
    1: t('workflow.instance.urgency.normal'),
    2: t('workflow.instance.urgency.urgent'),
    3: t('workflow.instance.urgency.critical')
  }
  return texts[urgency as keyof typeof texts] || t('common.unknown')
}

// 获取表单类型文本
const getFormTypeText = (formType: number) => {
  const texts = {
    1: t('workflow.instance.formType.basic'),
    2: t('workflow.instance.formType.advanced'),
    3: t('workflow.instance.formType.custom')
  }
  return texts[formType as keyof typeof texts] || t('common.unknown')
}

// 格式化流程变量
const formatVariables = (variables: string) => {
  try {
    const parsed = JSON.parse(variables)
    return JSON.stringify(parsed, null, 2)
  } catch {
    return variables
  }
}

// 格式化表单数据
const formatFormData = (formData: string) => {
  try {
    const parsed = JSON.parse(formData)
    return JSON.stringify(parsed, null, 2)
  } catch {
    return formData
  }
}

// 获取实例详情
const fetchInstanceDetail = async () => {
  if (!props.instanceId) return
  
  try {
    loading.value = true
    const response = await getInstanceById(props.instanceId)
    if (response.code === 200) {
      instanceData.value = response.data
    } else {
      message.error(response.msg || t('common.loadFailed'))
    }
  } catch (error) {
    console.error('获取流程实例详情失败:', error)
    message.error(t('common.loadFailed'))
  } finally {
    loading.value = false
  }
}

// 取消处理
const handleCancel = () => {
  visible.value = false
}

// 监听visible变化
watch(() => props.visible, (newVal) => {
  if (newVal && props.instanceId) {
    fetchInstanceDetail()
  } else {
    instanceData.value = null
  }
})

// 监听instanceId变化
watch(() => props.instanceId, (newVal) => {
  if (newVal && props.visible) {
    fetchInstanceDetail()
  }
})
</script>

<style scoped>
.instance-detail {
  max-height: 600px;
  overflow-y: auto;
}

.variables-section,
.form-data-section {
  margin-top: 16px;
}

.variables-section h4,
.form-data-section h4 {
  margin-bottom: 8px;
  color: #1890ff;
}

.variables-content,
.form-data-content {
  background: #f5f5f5;
  padding: 12px;
  border-radius: 4px;
  font-size: 12px;
  line-height: 1.5;
  max-height: 200px;
  overflow-y: auto;
  white-space: pre-wrap;
  word-break: break-all;
}

.loading-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 200px;
}
</style>

