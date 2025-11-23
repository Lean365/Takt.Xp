<template>
  <a-modal
    :open="visible"
    :title="title"
    width="700px"
    @cancel="handleCancel"
    @ok="handleOk"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item :label="t('mailtmpl.templateName')" name="mailTplName">
        <a-input
          v-model:value="formState.mailTplName"
          :placeholder="t('mailtmpl.placeholder.templateName')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateType')" name="mailTplCode">
        <Takt-select
          v-model:value="formState.mailTplCode"
          dict-type="email_template_type"
          type="radio"
          :placeholder="t('mailtmpl.placeholder.templateType')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateSubject')" name="mailTplSubject">
        <a-input
          v-model:value="formState.mailTplSubject"
          :placeholder="t('mailtmpl.placeholder.templateSubject')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateContent')" name="mailTplBody">
        <a-textarea
          v-model:value="formState.mailTplBody"
          :placeholder="t('mailtmpl.placeholder.templateContent')"
          :rows="10"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateIsHtml')" name="mailTplIsHtml">
        <a-switch
          v-model:checked="formState.mailTplIsHtml"
          :checked-children="t('common.yes')"
          :un-checked-children="t('common.no')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.templateStatus')" name="status">
        <Takt-select
          v-model:value="formState.status"
          dict-type="email_template_status"
          type="radio"
          :placeholder="t('mailtmpl.placeholder.templateStatus')"
        />
      </a-form-item>
      <a-form-item :label="t('mailtmpl.remark')" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('mailtmpl.placeholder.remark')"
          :rows="4"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import { getMailTplById, createMailTpl, updateMailTpl } from '@/api/routine/mailtpl'
import type { RuleObject } from 'ant-design-vue/es/form/interface'
import type { TaktMailTpl, TaktMailTplCreate, TaktMailTplUpdate } from '@/types/routine/mailtpl'
import type { TaktApiResponse } from '@/types/common'
import type { AxiosResponse } from 'axios'

const props = defineProps<{
  visible: boolean
  title: string
  templateId?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const formState = ref<Partial<TaktMailTpl>>({
  mailTplId: undefined,
  mailTplName: '',
  mailTplCode: '',
  mailTplSubject: '',
  mailTplBody: '',
  mailTplIsHtml: false,
  status: 0,
  mailTplParameters: '',
  createTime: undefined,
  updateTime: undefined,
  createBy: '',
  updateBy: ''
})

// 表单校验规则
const rules: Record<string, RuleObject[]> = {
  mailTplName: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateName.required'), trigger: 'blur' },
    { type: 'string', max: 100, message: t('mailtmpl.validation.templateName.maxLength'), trigger: 'blur' }
  ],
  mailTplCode: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateType.required'), trigger: 'change' }
  ],
  mailTplSubject: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateSubject.required'), trigger: 'blur' },
    { type: 'string', max: 200, message: t('mailtmpl.validation.templateSubject.maxLength'), trigger: 'blur' }
  ],
  mailTplBody: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateContent.required'), trigger: 'blur' },
    { type: 'string', max: 4000, message: t('mailtmpl.validation.templateContent.maxLength'), trigger: 'blur' }
  ],
  status: [
    { type: 'string', required: true, message: t('mailtmpl.validation.templateStatus.required'), trigger: 'change' }
  ]
}

// 监听模板ID变化
watch(() => props.templateId, async (newVal) => {
  if (newVal) {
    try {
      const res: AxiosResponse<TaktApiResponse<TaktMailTpl>> = await getMailTplById(Number(newVal))
      if (res.data && res.data.data) {
        formState.value = { ...res.data.data }
      }
    } catch (error) {
      console.error('获取邮件模板详情失败:', error)
    }
  } else {
    formState.value = {
      mailTplId: undefined,
      mailTplName: '',
      mailTplCode: '',
      mailTplSubject: '',
      mailTplBody: '',
      mailTplIsHtml: false,
      status: 0,
      mailTplParameters: '',
      createTime: undefined,
      updateTime: undefined,
      createBy: '',
      updateBy: ''
    }
  }
}, { immediate: true })

// 取消按钮
const handleCancel = () => {
  emit('update:visible', false)
}

// 确定按钮
const handleOk = async () => {
  try {
    // 使用validateFields()进行表单验证
    const values = await formRef.value?.validateFields()
    if (!values) return
    
    if (formState.value.mailTplId) {
      // 更新操作
      const updateData: TaktMailTplUpdate = {
        mailTplId: BigInt(formState.value.mailTplId),
        mailTplName: values.mailTplName,
        mailTplCode: values.mailTplCode,
        mailTplSubject: values.mailTplSubject,
        mailTplBody: values.mailTplBody,
        mailTplIsHtml: values.mailTplIsHtml || false,
        status: values.status,
        mailTplParameters: values.mailTplParameters || '',
        remark: values.remark || ''
      }
      await updateMailTpl(Number(formState.value.mailTplId), updateData)
      message.success(t('mailtmpl.operation.success.edit'))
    } else {
      // 创建操作
      const createData: TaktMailTplCreate = {
        mailTplName: values.mailTplName,
        mailTplCode: values.mailTplCode,
        mailTplSubject: values.mailTplSubject,
        mailTplBody: values.mailTplBody,
        mailTplIsHtml: values.mailTplIsHtml || false,
        status: values.status,
        mailTplParameters: values.mailTplParameters || '',
        remark: values.remark || ''
      }
      await createMailTpl(createData)
      message.success(t('mailtmpl.operation.success.add'))
    }
    emit('update:visible', false)
    emit('success')
  } catch (error) {
    // 表单验证失败或API调用失败
    if (error instanceof Error) {
      message.error(error.message)
    }
    console.error('保存邮件模板失败:', error)
  }
}
</script> 
