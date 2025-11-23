<template>
  <a-modal
    v-model:open="visible"
    :title="isEdit ? t('workflow.instance.edit') : t('workflow.instance.add')"
    width="600px"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-form-item :label="t('workflow.instance.title')" name="instanceTitle">
        <a-input
          v-model:value="formData.instanceTitle"
          :placeholder="t('workflow.instance.titlePlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('workflow.instance.businessKey')" name="businessKey">
        <a-input
          v-model:value="formData.businessKey"
          :placeholder="t('workflow.instance.businessKeyPlaceholder')"
        />
      </a-form-item>

      <a-form-item :label="t('workflow.instance.priority')" name="priority">
        <a-select
          v-model:value="formData.priority"
          :placeholder="t('workflow.instance.priorityPlaceholder')"
        >
          <a-select-option :value="1">{{ t('workflow.instance.priority.low') }}</a-select-option>
          <a-select-option :value="2">{{ t('workflow.instance.priority.normal') }}</a-select-option>
          <a-select-option :value="3">{{ t('workflow.instance.priority.high') }}</a-select-option>
          <a-select-option :value="4">{{ t('workflow.instance.priority.urgent') }}</a-select-option>
          <a-select-option :value="5">{{ t('workflow.instance.priority.critical') }}</a-select-option>
        </a-select>
      </a-form-item>

      <a-form-item :label="t('workflow.instance.urgency')" name="urgency">
        <a-select
          v-model:value="formData.urgency"
          :placeholder="t('workflow.instance.urgencyPlaceholder')"
        >
          <a-select-option :value="1">{{ t('workflow.instance.urgency.normal') }}</a-select-option>
          <a-select-option :value="2">{{ t('workflow.instance.urgency.urgent') }}</a-select-option>
          <a-select-option :value="3">{{ t('workflow.instance.urgency.critical') }}</a-select-option>
        </a-select>
      </a-form-item>

      <a-form-item :label="t('workflow.instance.startTime')" name="startTime">
        <a-date-picker
          v-model:value="formData.startTime"
          :placeholder="t('workflow.instance.startTimePlaceholder')"
          show-time
          format="YYYY-MM-DD HH:mm:ss"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item :label="t('workflow.instance.endTime')" name="endTime">
        <a-date-picker
          v-model:value="formData.endTime"
          :placeholder="t('workflow.instance.endTimePlaceholder')"
          show-time
          format="YYYY-MM-DD HH:mm:ss"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item :label="t('workflow.instance.status')" name="status">
        <a-select
          v-model:value="formData.status"
          :placeholder="t('workflow.instance.statusPlaceholder')"
        >
          <a-select-option :value="0">{{ t('workflow.instance.status.draft') }}</a-select-option>
          <a-select-option :value="1">{{ t('workflow.instance.status.running') }}</a-select-option>
          <a-select-option :value="2">{{ t('workflow.instance.status.completed') }}</a-select-option>
          <a-select-option :value="3">{{ t('workflow.instance.status.stopped') }}</a-select-option>
        </a-select>
      </a-form-item>

      <a-form-item :label="t('workflow.instance.formType')" name="formType">
        <a-select
          v-model:value="formData.formType"
          :placeholder="t('workflow.instance.formTypePlaceholder')"
        >
          <a-select-option :value="1">{{ t('workflow.instance.formType.basic') }}</a-select-option>
          <a-select-option :value="2">{{ t('workflow.instance.formType.advanced') }}</a-select-option>
          <a-select-option :value="3">{{ t('workflow.instance.formType.custom') }}</a-select-option>
        </a-select>
      </a-form-item>

      <a-form-item :label="t('workflow.instance.variables')" name="variables">
        <a-textarea
          v-model:value="formData.variables"
          :placeholder="t('workflow.instance.variablesPlaceholder')"
          :rows="4"
        />
      </a-form-item>

      <a-form-item :label="t('workflow.instance.formData')" name="formData">
        <a-textarea
          v-model:value="formData.formData"
          :placeholder="t('workflow.instance.formDataPlaceholder')"
          :rows="4"
        />
      </a-form-item>

      <a-form-item :label="t('common.remark')" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          :placeholder="t('common.remarkPlaceholder')"
          :rows="3"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktInstance, TaktInstanceCreate, TaktInstanceUpdate } from '@/types/workflow/instance'
import { createInstance, updateInstance, getInstanceById } from '@/api/workflow/instance'
import dayjs from 'dayjs'

// 国际化
const { t } = useI18n()

// Props
interface Props {
  visible: boolean
  instanceId: string
  isEdit: boolean
}

const props = defineProps<Props>()

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  refresh: []
}>()

// 响应式数据
const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单数据
const formData = reactive<TaktInstanceCreate>({
  instanceTitle: '',
  schemeId: '',
  businessKey: '',
  initiatorId: '',
  priority: 2,
  urgency: 1,
  startTime: '',
  endTime: '',
  variables: '',
  formId: '',
  formType: 1,
  formData: '',
  status: 0,
  remark: ''
})

// 表单验证规则
const formRules = {
  instanceTitle: [
    { required: true, message: t('workflow.instance.titleRequired'), trigger: 'blur' }
  ],
  schemeId: [
    { required: true, message: t('workflow.instance.schemeIdRequired'), trigger: 'change' }
  ],
  initiatorId: [
    { required: true, message: t('workflow.instance.initiatorIdRequired'), trigger: 'change' }
  ],
  priority: [
    { required: true, message: t('workflow.instance.priorityRequired'), trigger: 'change' }
  ],
  urgency: [
    { required: true, message: t('workflow.instance.urgencyRequired'), trigger: 'change' }
  ],
  formType: [
    { required: true, message: t('workflow.instance.formTypeRequired'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('workflow.instance.statusRequired'), trigger: 'change' }
  ]
}

// 计算属性
const visible = computed({
  get: () => props.visible,
  set: (value) => emit('update:visible', value)
})

// 重置表单
const resetForm = () => {
  Object.assign(formData, {
    instanceTitle: '',
    schemeId: '',
    businessKey: '',
    initiatorId: '',
    priority: 2,
    urgency: 1,
    startTime: '',
    endTime: '',
    variables: '',
    formId: '',
    formType: 1,
    formData: '',
    status: 0,
    remark: ''
  })
  formRef.value?.resetFields()
}

// 获取实例详情
const fetchInstanceDetail = async () => {
  if (!props.instanceId || !props.isEdit) return
  
  try {
    loading.value = true
    const response = await getInstanceById(props.instanceId)
    if (response.code === 200) {
      const data = response.data
      Object.assign(formData, {
        instanceTitle: data.instanceTitle,
        schemeId: data.schemeId,
        businessKey: data.businessKey || '',
        initiatorId: data.initiatorId,
        priority: data.priority,
        urgency: data.urgency,
        startTime: data.startTime ? dayjs(data.startTime) : null,
        endTime: data.endTime ? dayjs(data.endTime) : null,
        variables: data.variables || '',
        formId: data.formId || '',
        formType: data.formType,
        formData: data.formData || '',
        status: data.status,
        remark: data.remark || ''
      })
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

// 确定处理
const handleOk = async () => {
  try {
    await formRef.value?.validate()
    
    loading.value = true
    
    // 处理时间格式
    const submitData = {
      ...formData,
      startTime: formData.startTime ? dayjs(formData.startTime).format('YYYY-MM-DD HH:mm:ss') : '',
      endTime: formData.endTime ? dayjs(formData.endTime).format('YYYY-MM-DD HH:mm:ss') : ''
    }
    
    if (props.isEdit) {
      const updateData: TaktInstanceUpdate = {
        ...submitData,
        instanceId: props.instanceId
      }
      await updateInstance(updateData)
      message.success(t('common.updateSuccess'))
    } else {
      await createInstance(submitData)
      message.success(t('common.createSuccess'))
    }
    
    visible.value = false
    emit('refresh')
  } catch (error) {
    console.error('保存流程实例失败:', error)
    message.error(t('common.saveFailed'))
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
  if (newVal) {
    if (props.isEdit && props.instanceId) {
      fetchInstanceDetail()
    } else {
      resetForm()
    }
  }
})

// 监听instanceId变化
watch(() => props.instanceId, (newVal) => {
  if (newVal && props.visible && props.isEdit) {
    fetchInstanceDetail()
  }
})
</script>

<style scoped>
.ant-form-item {
  margin-bottom: 16px;
}
</style>

