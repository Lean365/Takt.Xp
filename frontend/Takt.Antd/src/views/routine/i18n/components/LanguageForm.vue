<template>
  <Takt-modal
    :open="open"
    :title="title"
    :loading="loading"
    width="800px"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      class="language-form"
    >
      <a-tabs>
        <!-- 语言信息 -->
        <a-tab-pane :key="'languageInfo'" :tab="t('core.language.tabs.languageInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.langCode.label')" name="langCode">
                <Takt-flag-icon
                  v-model="formData.langCode"
                  :placeholder="t('core.language.fields.langCode.placeholder')"
                  :disabled="!!languageId"
                  @change="handleLangCodeChange"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.langName.label')" name="langName">
                <a-input
                  v-model:value="formData.langName"
                  :placeholder="t('core.language.fields.langName.placeholder')"
                  disabled
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.langIcon.label')" name="langIcon">
                <a-input
                  v-model:value="formData.langIcon"
                  :placeholder="t('core.language.fields.langIcon.placeholder')"
                  disabled
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.orderNum.label')" name="orderNum">
                <a-input-number
                  v-model:value="formData.orderNum"
                  :placeholder="t('core.language.fields.orderNum.placeholder')"
                  :min="0"
                  :max="9999"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.isBuiltin.label')" name="isBuiltin">
                <Takt-select
                  v-model:value="formData.isBuiltin"
                  dict-type="sys_yes_no"
                  :placeholder="t('core.language.fields.isBuiltin.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.isDefault.label')" name="isDefault">
                <Takt-select
                  v-model:value="formData.isDefault"
                  dict-type="sys_yes_no"
                  :placeholder="t('core.language.fields.isDefault.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.status.label')" name="status">
                <Takt-select
                  v-model:value="formData.status"
                  dict-type="sys_normal_disable"
                  :placeholder="t('core.language.fields.status.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('core.language.fields.remark.label')" name="remark">
                <a-textarea
                  v-model:value="formData.remark"
                  :placeholder="t('core.language.fields.remark.placeholder')"
                  :rows="3"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>
      </a-tabs>
    </a-form>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktLanguage, TaktLanguageCreate, TaktLanguageUpdate } from '@/types/routine/i18n/language'
import { getByIdAsync, createLanguage, updateLanguage } from '@/api/routine/i18n/language'

const { t } = useI18n()

// ==================== Props 和 Emits ====================
const props = defineProps<{
  open: boolean
  title: string
  languageId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

// ==================== 状态定义 ====================
const loading = ref(false)
const formRef = ref<FormInstance>()

// ==================== 表单数据 ====================
const formData = reactive<TaktLanguageCreate>({
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  isBuiltin: 0,
  isDefault: 0,
  status: 0,
  remark: ''
})

// ==================== 表单验证规则 ====================
const rules = {
  langCode: [
    { required: true, message: t('core.language.fields.langCode.required') },
    { min: 2, max: 10, message: t('core.language.fields.langCode.length') }
  ],
  langName: [
    { required: true, message: t('core.language.fields.langName.required') }
  ],
  orderNum: [
    { required: true, message: t('core.language.fields.orderNum.required') }
  ],
  isBuiltin: [
    { required: true, message: t('core.language.fields.isBuiltin.required') }
  ],
  isDefault: [
    { required: true, message: t('core.language.fields.isDefault.required') }
  ],
  status: [
    { required: true, message: t('core.language.fields.status.required') }
  ]
}

// ==================== 方法定义 ====================
// 获取语言详情
const fetchLanguageDetail = async () => {
  if (!props.languageId) return

  try {
    loading.value = true
    const res = await getByIdAsync(props.languageId)
    if (res.data) {
      Object.assign(formData, res.data)
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

// 处理语言代码变化
const handleLangCodeChange = (value: { code: string; name: string }) => {
  const langCode = value.code
  // 根据语言代码自动设置语言名称和图标
  const languageMap: Record<string, { name: string; icon: string }> = {
    'zh-CN': { name: '简体中文', icon: '🇨🇳' },
    'en-US': { name: 'English', icon: '🇺🇸' },
    'ja-JP': { name: '日本語', icon: '🇯🇵' },
    'ko-KR': { name: '한국어', icon: '🇰🇷' },
    'fr-FR': { name: 'Français', icon: '🇫🇷' },
    'de-DE': { name: 'Deutsch', icon: '🇩🇪' },
    'es-ES': { name: 'Español', icon: '🇪🇸' },
    'ru-RU': { name: 'Русский', icon: '🇷🇺' }
  }

  const langInfo = languageMap[langCode]
  if (langInfo) {
    formData.langName = langInfo.name
    formData.langIcon = langInfo.icon
  }
}

// 处理提交
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    const api = props.languageId ? updateLanguage : createLanguage
    const res = await api(formData as TaktLanguageUpdate)
    
    if (res) {
      message.success(t('common.save.success'))
      emit('success')
      handleCancel()
    } else {
      message.error(t('common.save.failed'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error(t('common.save.failed'))
  } finally {
    loading.value = false
  }
}

// 处理取消
const handleCancel = () => {
  formRef.value?.resetFields()
  emit('update:open', false)
}

// 处理可见性变化
const handleVisibleChange = (visible: boolean) => {
  emit('update:open', visible)
}

// ==================== 监听器 ====================
// 监听对话框打开
watch(() => props.open, (newOpen) => {
  if (newOpen) {
    if (props.languageId) {
      fetchLanguageDetail()
    } else {
      // 重置表单
      Object.assign(formData, {
        langCode: '',
        langName: '',
        langIcon: '',
        orderNum: 0,
        isBuiltin: 0,
        isDefault: 0,
        status: 0,
        remark: ''
      })
    }
  }
})

// ==================== 生命周期 ====================
onMounted(() => {
  // 组件挂载时的初始化逻辑
})
</script>

<style lang="less" scoped>
.language-form {
  .ant-tabs-content-holder {
    padding-top: 16px;
  }
}
</style>
