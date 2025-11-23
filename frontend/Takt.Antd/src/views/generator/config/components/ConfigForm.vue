<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-tabs>
        <!-- 基本信息 -->
        <a-tab-pane :key="'basicInfo'" :tab="t('generator.config.tabs.basicInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.genConfigName')" name="genConfigName">
                <a-input
                  v-model:value="form.genConfigName"
                  :placeholder="t('generator.config.placeholder.genConfigName')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.author')" name="author">
                <a-input
                  v-model:value="form.author"
                  :placeholder="t('generator.config.placeholder.author')"
                  disabled
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.projectName')" name="projectName">
                <Takt-select
                  v-model:value="form.projectName"
                  dict-type="gen_project_name"
                  type="radio"
                  :show-all="false"
                  :placeholder="t('generator.config.placeholder.projectName')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.moduleName')" name="moduleName">
                <Takt-select
                  v-model:value="form.moduleName"
                  dict-type="gen_module_name"
                  type="radio"
                  :show-all="false"
                  :placeholder="t('generator.config.placeholder.moduleName')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.businessName')" name="businessName">
                <a-input
                  v-model:value="form.businessName"
                  :placeholder="t('generator.config.placeholder.businessName')"
                  @input="handleBusinessNameInput"
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.functionName')" name="functionName">
                <a-input
                  v-model:value="form.functionName"
                  :placeholder="t('generator.config.placeholder.functionName')"
                  @input="handleFunctionNameInput"
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 生成配置 -->
        <a-tab-pane :key="'genConfig'" :tab="t('generator.config.tabs.genConfig')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.genMethod')" name="genMethod">
                <Takt-select
                  v-model:value="form.genMethod"
                  dict-type="gen_method"
                  type="radio"
                  :show-all="false"
                  :placeholder="t('generator.config.placeholder.genMethod')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.genTplType')" name="genTplType">
                <Takt-select
                  v-model:value="form.genTplType"
                  dict-type="gen_tpl_type"
                  type="radio"
                  :show-all="false"
                  :placeholder="t('generator.config.placeholder.genTplType')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16" v-if="form.genMethod === 0">
            <a-col :span="24">
              <a-form-item :label="t('generator.config.fields.genPath')" name="genPath">
                <a-input
                  v-model:value="form.genPath"
                  :placeholder="t('generator.config.placeholder.genPath')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('generator.config.fields.options')" name="options">
                <a-textarea
                  v-model:value="form.options"
                  :rows="4"
                  :placeholder="t('generator.config.placeholder.options')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('generator.config.fields.status')" name="status">
                <Takt-select
                  v-model:value="form.status"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :show-all="false"
                  :placeholder="t('generator.config.placeholder.status')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>
      </a-tabs>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
// ==================== 导入语句 ====================
import { ref, reactive, watch, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'

import type { TaktGenConfig, TaktGenConfigCreate, TaktGenConfigUpdate } from '@/types/generator/genConfig'

import { getGenConfig, createGenConfig, updateGenConfig } from '@/api/generator/genConfig'
import { useUserStore } from '@/stores/userStore'

// ==================== 类型定义 ====================
type TaktGenConfigForm = Omit<TaktGenConfigCreate, 'genConfigId'> & {
  remark?: string
}

// ==================== Props和Emits ====================
const props = defineProps<{
  visible: boolean
  title: string
  configId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const userStore = useUserStore()

// ==================== 响应式数据 ====================
// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const form = reactive<TaktGenConfigForm>({
  genConfigName: '',
  author: '',
  projectName: 'Lean.Takt',
  moduleName: 'Core',
  businessName: '',
  functionName: '',
  genMethod: 0,
  genTplType: 0,
  genPath: '',
  options: '',
  status: 0
})

// ==================== 计算属性 ====================
const visible = computed({
  get: () => props.visible,
  set: (value) => emit('update:visible', value)
})

// ==================== 表单验证规则 ====================
const rules: Record<string, Rule[]> = {
  genConfigName: [
    { required: true, message: t('generator.config.placeholder.genConfigName'), trigger: 'blur' }
  ],
  author: [
    { required: true, message: t('generator.config.placeholder.author'), trigger: 'blur' }
  ],
  moduleName: [
    { required: true, message: t('generator.config.placeholder.moduleName'), trigger: 'blur' }
  ],
  projectName: [
    { required: true, message: t('generator.config.placeholder.projectName'), trigger: 'blur' }
  ],
  businessName: [
    { required: true, message: t('generator.config.placeholder.businessName'), trigger: 'blur' },
    { 
      pattern: /^[A-Z][a-zA-Z]{3,19}$/, 
      message: t('generator.config.validation.pascalCase'), 
      trigger: 'blur' 
    }
  ],
  functionName: [
    { required: true, message: t('generator.config.placeholder.functionName'), trigger: 'blur' },
    { 
      pattern: /^[A-Z][a-zA-Z]{3,19}$/, 
      message: t('generator.config.validation.pascalCase'), 
      trigger: 'blur' 
    }
  ],
  genMethod: [
    { required: true, message: t('generator.config.placeholder.genMethod'), trigger: 'change' }
  ],
  genTplType: [
    { required: true, message: t('generator.config.placeholder.genTplType'), trigger: 'change' }
  ],
  genPath: [
    { required: true, message: t('generator.config.placeholder.genPath'), trigger: 'blur' }
  ]
}

// ==================== 方法定义 ====================
// 处理业务名称输入
const handleBusinessNameInput = (e: Event) => {
  const input = e.target as HTMLInputElement
  let value = input.value.replace(/[^a-zA-Z]/g, '') // 只保留字母
  if (value.length > 0) {
    value = value.charAt(0).toUpperCase() + value.slice(1) // 首字母转大写
  }
  form.businessName = value
}

// 处理功能名称输入
const handleFunctionNameInput = (e: Event) => {
  const input = e.target as HTMLInputElement
  let value = input.value.replace(/[^a-zA-Z]/g, '') // 只保留字母
  if (value.length > 0) {
    value = value.charAt(0).toUpperCase() + value.slice(1) // 首字母转大写
  }
  form.functionName = value
}

// 获取配置详情
const fetchData = async () => {
  if (!props.configId) return
  
  try {
    loading.value = true
    const res = await getGenConfig(String(props.configId))
    if (res.code === 200 && res.data) {
      const configData = res.data
      Object.assign(form, {
        genConfigName: configData.genConfigName,
        author: configData.author,
        moduleName: configData.moduleName,
        projectName: configData.projectName,
        businessName: configData.businessName,
        functionName: configData.functionName,
        genMethod: configData.genMethod,
        genTplType: configData.genTplType,
        genPath: configData.genPath,
        status: configData.status,
        options: configData.options
      })
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取配置详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(form, {
    genConfigName: '',
    author: userStore.userInfo?.userName || '',
    projectName: 'Lean.Takt',
    moduleName: 'Core',
    businessName: '',
    functionName: '',
    genMethod: 0,
    genTplType: 0,
    genPath: '',
    options: '',
    status: 0
  })
  formRef.value?.resetFields()
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}

// 处理提交
const handleSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    loading.value = true
    
    if (props.configId) {
      await updateGenConfig(String(props.configId), form as any)
      message.success(t('common.update.success'))
    } else {
      await createGenConfig(form as any)
      message.success(t('common.create.success'))
    }
    
    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error('提交失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// ==================== 监听器 ====================
// 监听配置ID变化
watch(
  () => props.configId,
  async (newVal) => {
    if (newVal) {
      await fetchData()
    } else {
      resetForm()
    }
  },
  { immediate: true }
)

// 监听可见性变化
watch(
  () => props.visible,
  (newVal) => {
    if (newVal && !props.configId) {
      resetForm()
    }
  }
)

// ==================== 生命周期 ====================
onMounted(() => {
  if (!props.configId) {
    resetForm()
  }
})

// ==================== 暴露方法 ====================
defineExpose({
  formRef
})
</script>

<style lang="less" scoped>
.avatar-container {
  padding: 16px 0;
}

.avatar-management {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.avatar-containers {
  display: flex;
  gap: 24px;
  align-items: flex-start;
}

.avatar-preview-section,
.avatar-upload-section {
  flex: 1;
}

.container-label {
  font-size: 14px;
  font-weight: 500;
  margin-bottom: 8px;
  color: rgba(0, 0, 0, 0.85);
}

.avatar-preview {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100px;
}

.avatar-upload-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100px;
  border: 2px dashed #d9d9d9;
  border-radius: 6px;
  background-color: #fafafa;
}
</style>
