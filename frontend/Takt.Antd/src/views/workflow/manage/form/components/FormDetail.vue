<template>
  <Takt-modal
    v-model:open="dialogVisible"
    title="表单详情"
    :loading="loading"
    :show-footer="false"
    @cancel="handleCancel"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item label="表单键">
        {{ formData.formKey }}
      </a-descriptions-item>
      <a-descriptions-item label="表单名称">
        {{ formData.formName }}
      </a-descriptions-item>
      <a-descriptions-item label="表单分类">
        <Takt-dict-tag dict-type="workflow_form_category" :value="formData.formCategory" />
      </a-descriptions-item>
      <a-descriptions-item label="表单类型">
        <Takt-dict-tag dict-type="wf_form_type" :value="formData.formType" />
      </a-descriptions-item>
      <a-descriptions-item label="表单版本">
        {{ formData.version }}
      </a-descriptions-item>
      <a-descriptions-item label="状态">
        <Takt-dict-tag dict-type="wf_form_status" :value="formData.status" />
      </a-descriptions-item>
      <a-descriptions-item label="业务表名">
        {{ formData.businessTableName || '无' }}
      </a-descriptions-item>
      <a-descriptions-item label="创建时间">
        {{ formData.createTime }}
      </a-descriptions-item>
      <a-descriptions-item label="注意事项" :span="2">
        {{ formData.notes || '暂无注意事项' }}
      </a-descriptions-item>
      <a-descriptions-item label="表单配置" :span="2">
        <div class="form-config-preview">
          <Takt-form
            v-if="formData.formConfig"
            :model-value="formData.formConfig"
            :readonly="true"
            :height="'300px'"
          />
          <a-empty v-else description="暂无表单配置" />
        </div>
      </a-descriptions-item>
      <a-descriptions-item label="备注" :span="2">
        {{ formData.remark || '暂无备注' }}
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, reactive, computed, watch } from 'vue'
import { message } from 'ant-design-vue'
import { getFormById } from '@/api/workflow/form'
import type { TaktForm } from '@/types/workflow/form'

// Props
interface Props {
  open: boolean
  formId?: string
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  formId: undefined
})

// Emits
const emit = defineEmits<{
  'update:open': [value: boolean]
}>()

// 响应式数据
const loading = ref(false)

// 表单数据
const formData = reactive<TaktForm>({
  formId: '',
  formKey: '',
  formName: '',
  formCategory: 1,
  formType: 1,
  version: '1.0',
  formConfig: '',
  businessTableName: '',
  status: 0,
  notes: '',
  remark: '',
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0
})

// 计算属性
const dialogVisible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

// 方法
const loadFormData = async () => {
  if (!props.formId) return
  
  loading.value = true
  try {
    const result = await getFormById(props.formId)
    Object.assign(formData, result)
  } catch (error) {
    console.error('加载表单数据失败:', error)
    message.error('加载表单数据失败')
  } finally {
    loading.value = false
  }
}

const resetForm = () => {
  Object.assign(formData, {
    formId: '',
    formKey: '',
    formName: '',
    formCategory: 1,
    formType: 1,
    version: '1.0',
    status: 0,
    formConfig: '',
    businessTableName: '',
    notes: '',
    remark: '',
    createTime: '',
    updateTime: '',
    createBy: '',
    updateBy: '',
    isDeleted: 0
  })
}

const handleCancel = () => {
  dialogVisible.value = false
  resetForm()
}

// 监听器
watch(() => props.open, (newVal) => {
  if (newVal && props.formId) {
    loadFormData()
  } else if (!newVal) {
    resetForm()
  }
})
</script>

<style lang="less" scoped>
.form-config-preview {
  .Takt-form {
    border: 1px solid #d9d9d9;
    border-radius: 4px;
  }
}

.form-data-preview {
  .json-preview {
    background-color: #f5f5f5;
    border: 1px solid #d9d9d9;
    border-radius: 4px;
    padding: 12px;
    margin: 0;
    font-family: 'Courier New', monospace;
    font-size: 12px;
    line-height: 1.5;
    max-height: 200px;
    overflow-y: auto;
    white-space: pre-wrap;
    word-break: break-all;
  }
}
</style> 
