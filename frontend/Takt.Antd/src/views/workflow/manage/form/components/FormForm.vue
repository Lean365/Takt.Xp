<template>
  <Takt-modal
    v-model:open="dialogVisible"
    :title="dialogTitle"
    :loading="loading"
    :width="1200"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      layout="vertical"
    >
      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="表单键" name="formKey">
            <a-input v-model:value="formData.formKey" placeholder="请输入表单键" />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="表单名称" name="formName">
            <a-input v-model:value="formData.formName" placeholder="请输入表单名称" />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="表单分类" name="formCategory">
            <Takt-select
              v-model:value="formData.formCategory"
              dict-type="workflow_form_category"
              placeholder="请选择表单分类"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="表单类型" name="formType">
            <Takt-select
              v-model:value="formData.formType"
              dict-type="wf_form_type"
              placeholder="请选择表单类型"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="表单版本" name="version">
            <a-input v-model:value="formData.version" placeholder="请输入表单版本" />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="表单状态" name="status">
            <Takt-select
              v-model:value="formData.status"
              dict-type="wf_form_status"
              placeholder="请选择表单状态"
            />
          </a-form-item>
        </a-col>
      </a-row>
      
      <a-form-item label="表单配置" name="formConfig">
        <div class="form-config-editor">
          <div class="editor-header">
            <span class="editor-title">表单设计器</span>
          </div>
          <div class="editor-content">
            <Takt-form
              :key="formDesignerKey"
              :model-value="formData.formConfig"
              :readonly="false"
              :height="'600px'"
              @update:modelValue="handleFormConfigChange"
            />
          </div>
        </div>
      </a-form-item>

      <a-form-item label="注意事项" name="notes">
        <a-textarea v-model:value="formData.notes" :rows="3" placeholder="请输入注意事项" />
      </a-form-item>
      
      <a-form-item label="备注" name="remark">
        <a-textarea v-model:value="formData.remark" :rows="3" placeholder="请输入备注" />
      </a-form-item>
    </a-form>
  </Takt-modal>


</template>

<script setup lang="ts">
import { ref, reactive, computed, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { EyeOutlined } from '@ant-design/icons-vue'
import { getFormById, createForm, updateForm } from '@/api/workflow/form'
import type { TaktFormCreate, TaktFormUpdate } from '@/types/workflow/form'

// Props
interface Props {
  open: boolean
  formId?: string
  isClone?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  formId: undefined,
  isClone: false
})

// Emits
const emit = defineEmits<{
  'update:open': [value: boolean]
  'success': []
}>()

// 响应式数据
const loading = ref(false)
const formRef = ref<FormInstance>()
const formDesignerKey = ref(0) // 用于强制表单设计器重新渲染

// 表单数据
const formData = reactive<TaktFormCreate>({
  formKey: '',
  formName: '',
  formCategory: 1,
  formType: 1,
  version: '1.0',
  formConfig: '',
  status: 0,
  notes: '',
  remark: ''
})

// 表单验证规则
const formRules: Record<string, any> = {
  formName: [
    { required: true, message: '请输入表单名称', trigger: 'blur' }
  ],
  formKey: [
    { required: true, message: '请输入表单键', trigger: 'blur' }
  ],
  formCategory: [
    { required: true, message: '请选择表单分类', trigger: 'change' }
  ],
  formType: [
    { required: true, message: '请选择表单类型', trigger: 'change' }
  ],
  status: [
    { required: true, message: '请选择表单状态', trigger: 'change' }
  ]
}

// 计算属性
const dialogVisible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const dialogTitle = computed(() => {
  if (props.isClone) return '克隆表单'
  return props.formId ? '编辑表单' : '新增表单'
})
const isEdit = computed(() => !!props.formId && !props.isClone)

// 方法
const loadFormData = async () => {
  if (!props.formId) return
  
  console.log('FormForm - loadFormData 开始执行, formId:', props.formId)
  loading.value = true
  try {
    const result = await getFormById(props.formId)
    const form = result.data
    console.log('FormForm - 加载到的表单数据:', form)
    console.log('FormForm - form.formConfig:', form.formConfig)
    
    Object.assign(formData, {
      formKey: form.formKey,
      formName: form.formName,
      formCategory: form.formCategory,
      formType: form.formType,
      version: form.version,
      formConfig: form.formConfig || '',
      status: form.status,
      notes: form.notes || '',
      remark: form.remark || ''
    })
    
    console.log('FormForm - 设置后的formData.formConfig:', formData.formConfig)
  } catch (error) {
    console.error('加载表单数据失败:', error)
    message.error('加载表单数据失败')
  } finally {
    loading.value = false
  }
}

// 加载表单数据用于克隆
const loadFormDataForClone = async () => {
  if (!props.formId) return
  
  console.log('FormForm - loadFormDataForClone 开始执行, formId:', props.formId)
  loading.value = true
  try {
    const result = await getFormById(props.formId)
    const form = result.data
    console.log('FormForm - 加载到的表单数据:', form)
    console.log('FormForm - form.formConfig:', form.formConfig)
    
    // 克隆时清空关键字段，让用户重新输入
    Object.assign(formData, {
      formKey: (form.formKey || '') + '_copy', // 添加副本标识
      formName: (form.formName || '') + ' - 副本', // 添加副本标识
      formCategory: form.formCategory,
      formType: form.formType,
      version: '1.0', // 重置版本
      formConfig: form.formConfig || '',
      status: 0, // 重置状态为草稿
      notes: form.notes || '',
      remark: form.remark || ''
    })
    
    console.log('FormForm - 克隆数据设置完成:', formData)
  } catch (error) {
    console.error('加载表单数据失败:', error)
    message.error('加载表单数据失败')
  } finally {
    loading.value = false
  }
}

const resetForm = () => {
  Object.assign(formData, {
    formKey: '',
    formName: '',
    formCategory: 1,
    formType: 1,
    version: '1.0',
    formConfig: '',
    status: 0,
    notes: '',
    remark: ''
  })
  formRef.value?.clearValidate()
  // 强制表单设计器重新渲染
  formDesignerKey.value++
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    
    loading.value = true
    const result = isEdit.value 
      ? await updateForm({ ...formData, formId: props.formId! } as TaktFormUpdate)
      : await createForm(formData)
    
    // 检查返回结果
    if (result.code === 200) {
      message.success(isEdit.value ? '更新成功' : '创建成功')
      // 成功完成后重置表单和关闭对话框
      resetForm()
      dialogVisible.value = false
      emit('success')
    } else {
      message.error(result.msg || (isEdit.value ? '更新失败' : '创建失败'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  dialogVisible.value = false
  resetForm()
}

// 表单设计器相关方法
const handleFormConfigChange = (config: string) => {
  console.log('FormForm - handleFormConfigChange 接收到配置:', config)
  if (config) {
    formData.formConfig = config
  }
}


// 监听器 - 监听对话框打开状态
watch(() => props.open, (newVal) => {
  if (newVal) {
    // 对话框打开时，先重置表单
    resetForm()
    
    // 然后根据情况加载数据
    if (props.formId && !props.isClone) {
      // 只有编辑模式且有formId时才加载数据
      loadFormData()
    } else if (props.formId && props.isClone) {
      // 克隆模式：加载源数据但清空关键字段
      loadFormDataForClone()
    }
    // 创建模式（没有formId）或其他情况，表单已经通过resetForm()重置了
  }
})

// 监听器 - 监听formId变化，确保创建模式时formId为undefined
watch(() => props.formId, (newFormId, oldFormId) => {
  // 当formId从有值变为undefined时，说明切换到了创建模式，需要重置表单
  if (oldFormId && !newFormId && props.open) {
    resetForm()
  }
})
</script>

<style lang="less" scoped>
.form-config-editor,
.form-data-editor {
  width: 100%;
  
  .editor-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 8px;
    
    .editor-title {
      font-weight: 500;
      color: #262626;
    }
  }
  
  .editor-content {
    width: 100%;
    
    .ant-input {
      font-family: 'Courier New', monospace;
      font-size: 12px;
    }
  }
}

.form-config-editor {
  .editor-content {
    .ant-input {
      background-color: #fafafa;
    }
  }
}



// 确保表单项目有足够的宽度
:deep(.ant-form-item) {
  width: 100%;
}

:deep(.ant-form-item-control) {
  width: 100%;
}

:deep(.ant-input),
:deep(.ant-select),
:deep(.ant-textarea) {
  width: 100%;
}
</style>
  
