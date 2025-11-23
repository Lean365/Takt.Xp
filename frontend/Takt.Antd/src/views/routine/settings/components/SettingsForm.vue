//===================================================================
// 项目名 : Lean.Takt
// 文件名 : SettingsForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用设置表单组件
//===================================================================

<template>
  <a-modal
    v-model:open="visible"
    :title="title"
    width="800px"
    :mask-closable="false"
    :confirm-loading="loading"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
      layout="horizontal"
    >
      <a-form-item :label="t('core.settings.fields.settingsName.label')" name="settingsName">
        <a-input
          v-model:value="formState.settingsName"
          :placeholder="t('core.settings.fields.settingsName.placeholder')"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.settingsKey.label')" name="settingsKey">
        <a-input
          v-model:value="formState.settingsKey"
          :placeholder="t('core.settings.fields.settingsKey.placeholder')"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.settingsValue.label')" name="settingsValue">
        <a-textarea
          v-model:value="formState.settingsValue"
          :placeholder="t('core.settings.fields.settingsValue.placeholder')"
          :rows="4"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.settingsType.label')" name="settingsType">
        <Takt-select
          v-model:value="formState.settingsType"
          dict-type="sys_settings_type"
          type="radio"
          :show-all="false"
          :placeholder="t('core.settings.fields.settingsType.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.isBuiltin.label')" name="isBuiltin">
        <Takt-select
          v-model:value="formState.isBuiltin"
          dict-type="sys_yes_no"
          type="radio"
          :show-all="false"
          :placeholder="t('core.settings.fields.isBuiltin.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.orderNum.label')" name="orderNum">
        <a-input-number
          v-model:value="formState.orderNum"
          :min="0"
          :max="9999"
          :placeholder="t('core.settings.fields.orderNum.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.status.label')" name="status">
        <Takt-select
          v-model:value="formState.status"
          dict-type="sys_normal_disable"
          type="radio"
          :show-all="false"
          :placeholder="t('core.settings.fields.status.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('core.settings.fields.isEncrypted.label')" name="isEncrypted">
        <Takt-select
          v-model:value="formState.isEncrypted"
          dict-type="sys_yes_no"
          type="radio"
          :show-all="false"
          :placeholder="t('core.settings.fields.isEncrypted.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('table.columns.remark')" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('table.columns.remark')"
          :rows="4"
          allow-clear
        />
      </a-form-item>
    </a-form>

    <template #footer>
      <a-space>
        <a-button @click="handleCancel" :disabled="loading">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" @click="handleSubmit" :loading="loading">{{ t('common.button.submit') }}</a-button>
      </a-space>
    </template>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { TaktGeneralSettings, TaktGeneralSettingsCreate, TaktGeneralSettingsUpdate } from '@/types/routine/settings/settings'
import { getByIdAsync, createGeneralSettings, updateGeneralSettings } from '@/api/routine/settings/settings'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  title: string
  settingsId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
  (e: 'success'): void
}>()

// 计算属性：处理模态框的显示状态
const visible = computed({
  get: () => props.open,
  set: (val) => emit('update:open', val)
})

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单初始状态
const initialFormState: TaktGeneralSettingsCreate = {
  settingsName: '',
  settingsKey: '',
  settingsValue: '',
  settingsType: 1,
  isBuiltin: 0,
  orderNum: 0,
  remark: '',
  status: 0,
  isEncrypted: 0
}

// 表单数据
const formState = reactive<TaktGeneralSettingsCreate>({ ...initialFormState })

// 表单校验规则
const rules: Record<string, Rule[]> = {
  settingsName: [
    { required: true, message: t('core.settings.fields.settingsName.validation.required'), trigger: 'blur' },
    { max: 50, message: t('core.settings.fields.settingsName.validation.maxLength'), trigger: 'blur' }
  ],
  settingsKey: [
    { required: true, message: t('core.settings.fields.settingsKey.validation.required'), trigger: 'blur' },
    { max: 50, message: t('core.settings.fields.settingsKey.validation.maxLength'), trigger: 'blur' },
    { pattern: /^[a-zA-Z][a-zA-Z0-9_.:]*$/, message: t('core.settings.fields.settingsKey.validation.pattern'), trigger: 'blur' }
  ],
  settingsValue: [
    { required: true, message: t('core.settings.fields.settingsValue.validation.required'), trigger: 'blur' },
    { max: 500, message: t('core.settings.fields.settingsValue.validation.maxLength'), trigger: 'blur' }
  ],
  settingsType: [
    { required: true, message: t('core.settings.fields.settingsType.validation.required'), trigger: 'change' }
  ],
  isBuiltin: [
    { required: true, message: t('core.settings.fields.isBuiltin.validation.required'), trigger: 'change' }
  ],
  orderNum: [
    { required: true, message: t('core.settings.fields.orderNum.validation.required'), trigger: 'blur' },
    { type: 'number', min: 0, max: 9999, message: t('core.settings.fields.orderNum.validation.range'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('core.settings.fields.status.validation.required'), trigger: 'change' }
  ],
  isEncrypted: [
    { required: true, message: t('core.settings.fields.isEncrypted.validation.required'), trigger: 'change' }
  ]
}

// 获取设置信息
const getInfo = async (settingsId: string) => {
  try {
    loading.value = true
    const res = await getByIdAsync(settingsId)
    Object.assign(formState, res.data)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(formState, initialFormState)
  formRef.value?.resetFields()
}

// 处理提交
const handleSubmit = async () => {
  try {
    // 表单校验
    await formRef.value?.validate()
    loading.value = true

    // 提交数据
    const res = props.settingsId
      ? await updateGeneralSettings({ ...formState, settingsId: props.settingsId })
      : await createGeneralSettings(formState)

    message.success(t('admin.settings.message.editSuccess'))
    emit('success')
    handleCancel()
    // 完全重置表单状态
    Object.assign(formState, initialFormState)
    formRef.value?.resetFields()
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理对话框显示状态变化
const handleCancel = () => {
  emit('update:open', false)
  resetForm()
}

// 监听设置ID变化
watch(
  () => props.settingsId,
  (val) => {
    if (val) {
      getInfo(val)
    } else {
      resetForm()
    }
  }
)

// 监听对话框显示状态
watch(
  () => props.open,
  (val) => {
    if (!val) {
      resetForm()
    }
  }
)
</script>

<style lang="less" scoped>
.ant-form {
  padding: 24px 0;
}

.ant-form-item {
  margin-bottom: 24px;
}

.ant-input-number {
  width: 100%;
}

// 调整标签和输入框的布局
:deep(.ant-form-item-label) {
  min-width: 90px;
  text-align: right;
  padding-right: 8px;
}

:deep(.ant-form-item-control) {
  flex: 1;
}

// 调整文本域的样式
:deep(.ant-input-textarea) {
  width: 100%;
}
</style> 
