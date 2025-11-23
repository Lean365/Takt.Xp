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
      <a-form-item :label="t('notice.noticeTitle')" name="noticeTitle">
        <a-input
          v-model:value="formState.noticeTitle"
          :placeholder="t('notice.placeholder.noticeTitle')"
        />
      </a-form-item>
      <a-form-item :label="t('notice.noticeType')" name="noticeType">
        <Takt-select
          v-model:value="formState.noticeType"
          dict-type="notice_type"
          type="radio"
          :placeholder="t('notice.placeholder.noticeType')"
        />
      </a-form-item>
      <a-form-item :label="t('notice.noticeContent')" name="noticeContent">
        <a-textarea
          v-model:value="formState.noticeContent"
          :placeholder="t('notice.placeholder.noticeContent')"
          :rows="10"
        />
      </a-form-item>
      <a-form-item :label="t('notice.noticeStatus')" name="noticeStatus">
        <Takt-select
          v-model:value="formState.noticeStatus"
          dict-type="notice_status"
          type="radio"
          :placeholder="t('notice.placeholder.noticeStatus')"
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
import { getNoticeDetail, createNotice, updateNotice } from '@/api/routine/notice'
import type { RuleObject } from 'ant-design-vue/es/form/interface'
import type { TaktNoticeDto, TaktNoticeCreateDto, TaktNoticeUpdateDto } from '@/types/routine/notice'
import type { TaktApiResponse } from '@/types/common'
import type { AxiosResponse } from 'axios'

const props = defineProps<{
  visible: boolean
  title: string
  noticeId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const formState = ref<Partial<TaktNoticeDto>>({
  noticeId: undefined,
  noticeTitle: '',
  noticeContent: '',
  noticeType: 1,
  noticeStatus: 0,
  noticeSendTime: undefined,
  noticeReadTime: undefined,
  noticeReceiverId: 0,
  noticeReceiverName: ''
})

// 表单校验规则
const rules: Record<string, RuleObject[]> = {
  noticeTitle: [
    { type: 'string', required: true, message: t('notice.validation.noticeTitle.required'), trigger: 'blur' },
    { type: 'string', max: 100, message: t('notice.validation.noticeTitle.maxLength'), trigger: 'blur' }
  ],
  noticeType: [
    { type: 'number', required: true, message: t('notice.validation.noticeType.required'), trigger: 'change' }
  ],
  noticeContent: [
    { type: 'string', required: true, message: t('notice.validation.noticeContent.required'), trigger: 'blur' },
    { type: 'string', max: 4000, message: t('notice.validation.noticeContent.maxLength'), trigger: 'blur' }
  ],
  noticeStatus: [
    { type: 'number', required: true, message: t('notice.validation.noticeStatus.required'), trigger: 'change' }
  ]
}

// 监听通知ID变化
watch(() => props.noticeId, async (newVal) => {
  if (newVal) {
    try {
      const res: AxiosResponse<TaktApiResponse<TaktNoticeDto>> = await getNoticeDetail(newVal)
      if (res.data && res.data.data) {
        formState.value = { ...res.data.data }
      }
    } catch (error) {
      console.error('获取通知详情失败:', error)
    }
  } else {
    formState.value = {
      noticeId: undefined,
      noticeTitle: '',
      noticeContent: '',
      noticeType: 1,
      noticeStatus: 0,
      noticeSendTime: undefined,
      noticeReadTime: undefined,
      noticeReceiverId: 0,
      noticeReceiverName: ''
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
    await formRef.value?.validate()
    if (formState.value.noticeId) {
      const updateData: TaktNoticeUpdateDto = {
        noticeId: BigInt(formState.value.noticeId),
        noticeTitle: formState.value.noticeTitle!,
        noticeContent: formState.value.noticeContent!,
        noticeType: formState.value.noticeType!,
        noticeReceiverId: BigInt(formState.value.noticeReceiverId!),
        noticeReceiverName: formState.value.noticeReceiverName!
      }
      await updateNotice(Number(formState.value.noticeId), updateData)
      message.success(t('notice.operation.success.edit'))
    } else {
      const createData: TaktNoticeCreateDto = {
        noticeTitle: formState.value.noticeTitle!,
        noticeContent: formState.value.noticeContent!,
        noticeType: formState.value.noticeType!,
        noticeReceiverId: BigInt(formState.value.noticeReceiverId!),
        noticeReceiverName: formState.value.noticeReceiverName!
      }
      await createNotice(createData)
      message.success(t('notice.operation.success.add'))
    }
    emit('success')
  } catch (error) {
    console.error('保存通知失败:', error)
  }
}
</script> 
