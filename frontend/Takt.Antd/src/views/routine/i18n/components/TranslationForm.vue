<template>
  <Takt-modal
    :open="open"
    :title="transKey ? t('common.actions.edit') : t('common.actions.add')"
    :loading="loading"
    width="800px"
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
        <!-- 翻译信息 -->
        <a-tab-pane :key="'translationInfo'" :tab="t('core.translation.tabs.translationInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('core.translation.fields.transType.label')" name="transType">
                <Takt-select
                  v-model:value="form.transType"
                  type="radio"
                  :show-all="false"
                  :placeholder="t('core.translation.fields.transType.placeholder')"
                  :disabled="!!props.transKey"
                  dict-type="sys_translation_category"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('core.translation.fields.transKey.label')" name="transKey">
                <a-input 
                  v-model:value="form.transKey" 
                  :placeholder="t('core.translation.fields.transKey.placeholder')"
                  :disabled="!!props.transKey"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('core.translation.fields.remark.label')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('core.translation.fields.remark.placeholder')"
                  :rows="3"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 翻译内容 -->
        <a-tab-pane :key="'translations'" :tab="t('core.translation.tabs.translations')">
          <div class="translation-list">
            <h3 class="section-title">{{ t('core.translation.title') }}</h3>
            <div v-for="lang in languageList" :key="lang.langCode" class="translation-item">
              <a-form-item
                :label="lang.langName + ' (' + lang.langCode + ')'"
                :name="['translations', lang.langCode, 'transValue']"
                :rules="[
                  { 
                    required: true, 
                    message: t('core.translation.fields.transValue.required', { lang: lang.langName })
                  }
                ]"
              >
                <a-textarea
                  v-model:value="form.translations[lang.langCode].transValue"
                  :placeholder="t('core.translation.fields.transValue.placeholder', { lang: lang.langName })"
                  :rows="3"
                />
              </a-form-item>
            </div>
          </div>
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
import type { TaktTranslationCreate, TaktTranslationUpdate, TaktTransposedData } from '@/types/routine/i18n/translation'
import type { TaktLanguage } from '@/types/routine/i18n/language'
import { getByIdAsync, createTranslation, updateTranslation, getTransposedDetail, createTransposedTranslation, updateTransposedTranslation } from '@/api/routine/i18n/translation'
import { getLanguageList } from '@/api/routine/i18n/language'

const { t } = useI18n()

// ==================== Props 和 Emits ====================
const props = defineProps<{
  open: boolean
  translationId?: string
  transKey?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

// ==================== 状态定义 ====================
const loading = ref(false)
const formRef = ref<FormInstance>()
const languageList = ref<TaktLanguage[]>([])

// ==================== 表单数据 ====================
interface TranslationFormData {
  transKey: string
  transType: number
  orderNum: number
  status: number
  remark: string
  translations: Record<string, {
    translationId?: string
    langCode: string
    transValue: string
    status: number
  }>
}

const form = reactive<TranslationFormData>({
  transKey: '',
  transType: 0,
  orderNum: 0,
  status: 0,
  remark: '',
  translations: {}
})

// ==================== 表单验证规则 ====================
const rules = {
  transType: [
    { required: true, message: t('core.translation.fields.transType.required') }
  ],
  transKey: [
    { required: true, message: t('core.translation.fields.transKey.required') },
    { min: 1, max: 200, message: t('core.translation.fields.transKey.length') }
  ],
}

// ==================== 方法定义 ====================
// 获取语言列表
const fetchLanguageList = async () => {
  try {
    const res = await getLanguageList({ pageIndex: 1, pageSize: 1000 })
    if (res.data) {
      languageList.value = res.data.rows
      // 初始化翻译表单
      initTranslations()
    }
  } catch (error) {
    console.error('获取语言列表失败:', error)
  }
}

// 初始化翻译表单
const initTranslations = () => {
  languageList.value.forEach(lang => {
    if (!form.translations[lang.langCode]) {
      form.translations[lang.langCode] = {
        langCode: lang.langCode,
        transValue: '',
        status: 0
      }
    }
  })
}

// 获取翻译详情
const fetchTranslationDetail = async () => {
  if (!props.transKey) return

  try {
    loading.value = true
    const res = await getTransposedDetail(props.transKey)
    if (res.data) {
      const data = res.data
      form.transKey = data.transKey
      form.transType = data.transType
      form.remark = data.remark || ''
      
      // 初始化翻译数据
      initTranslations()
      
      // 填充现有翻译数据
      if (data.translations) {
        Object.keys(data.translations).forEach(langCode => {
          if (form.translations[langCode]) {
            form.translations[langCode].translationId = data.translations[langCode].translationId
            form.translations[langCode].transValue = data.translations[langCode].transValue || ''
            form.translations[langCode].status = data.translations[langCode].status
          }
        })
      }
    }
  } catch (error) {
    console.error('获取翻译详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理提交
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    // 构建转置翻译数据
    const transposedData: TaktTransposedData = {
      transKey: form.transKey,
      transType: form.transType,
      status: form.status,
      remark: form.remark,
      createBy: '',
      createTime: '',
      updateBy: '',
      updateTime: '',
      translations: {}
    }

    // 填充各语言翻译数据
    Object.keys(form.translations).forEach(langCode => {
      const translation = form.translations[langCode]
      if (translation.transValue.trim()) {
        transposedData.translations[langCode] = {
          translationId: translation.translationId || '',
          langCode: translation.langCode,
          transValue: translation.transValue,
          status: translation.status
        }
      }
    })

    // 检查是否有有效的翻译数据
    if (Object.keys(transposedData.translations).length === 0) {
      message.error(t('core.translation.fields.transValue.required'))
      return
    }

    let res: any
    if (props.transKey) {
      // 更新转置翻译数据
      res = await updateTransposedTranslation(transposedData)
    } else {
      // 创建转置翻译数据
      res = await createTransposedTranslation(transposedData)
    }

    if (res.data) {
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
    if (props.transKey) {
      fetchTranslationDetail()
    } else {
      // 重置表单
      Object.assign(form, {
        transKey: '',
        transType: 0,
        orderNum: 0,
        status: 0,
        remark: '',
        translations: {}
      })
      initTranslations()
    }
  }
})

// ==================== 生命周期 ====================
onMounted(() => {
  fetchLanguageList()
})
</script>

<style lang="less" scoped>
.translation-list {
  .section-title {
    margin-bottom: 16px;
    font-size: 16px;
    font-weight: 500;
  }

  .translation-item {
    margin-bottom: 16px;
  }
}
</style>
