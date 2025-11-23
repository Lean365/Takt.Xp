<template>
  <Takt-modal
    :open="open"
    @update:open="emit('update:open', $event)"
    :title="title"
    :width="800"
    :mask-closable="false"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item label="发件人" name="mailFrom">
        <a-input
          v-model:value="formData.mailFrom"
          placeholder="请输入发件人"
          :maxLength="255"
        />
      </a-form-item>
      <a-form-item label="收件人" name="mailTo">
        <a-input
          v-model:value="formData.mailTo"
          placeholder="请输入收件人"
          :maxLength="255"
        />
      </a-form-item>
      <a-form-item label="主题" name="mailSubject">
        <a-input
          v-model:value="formData.mailSubject"
          placeholder="请输入邮件主题"
          :maxLength="255"
        />
      </a-form-item>
      <a-form-item label="内容" name="mailBody">
        <a-textarea
          v-model:value="formData.mailBody"
          placeholder="请输入邮件内容"
          :rows="10"
          :maxLength="4000"
        />
      </a-form-item>
      <a-form-item label="抄送" name="mailCc">
        <a-input
          v-model:value="formData.mailCc"
          placeholder="请输入抄送"
          :maxLength="255"
        />
      </a-form-item>
      <a-form-item label="附件" name="mailAttachments">
        <a-input
          v-model:value="formData.mailAttachments"
          placeholder="请输入附件"
          :maxLength="1000"
        />
      </a-form-item>
      <a-form-item label="状态" name="mailStatus">
        <Takt-select
          v-model:value="formData.mailStatus"
          dict-type="sys_yes_no"
          type="radio"
          placeholder="请选择状态"
        />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          placeholder="请输入备注"
          :rows="4"
          :maxLength="500"
        />
      </a-form-item>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form/interface'
import { message } from 'ant-design-vue'
import type { TaktMailDto, TaktMailCreateDto, TaktMailUpdateDto } from '@/types/routine/mail.d'
import { getMailDetail, createMail, updateMail } from '@/api/routine/mail'

const props = defineProps<{
  open: boolean
  title: string
  mailId?: number | bigint
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
  (e: 'success'): void
}>()

// 表单实例
const formRef = ref<FormInstance>()

// 表单数据
const formData = ref<TaktMailCreateDto | TaktMailUpdateDto>({
  mailFrom: '',
  mailTo: '',
  mailSubject: '',
  mailBody: '',
  mailIsHtml: 0,
  mailCc: '',
  mailAttachments: '',
  mailStatus: 0,
  remark: '',
  ...(props.mailId ? { mailId: props.mailId } : {})
})

// 表单校验规则
const rules: Record<string, RuleObject[]> = {
  mailFrom: [
    { required: true, message: '请输入发件人', trigger: 'blur' },
    { max: 255, message: '发件人长度不能超过255个字符', trigger: 'blur' },
    { type: 'email', message: '发件人邮箱格式不正确', trigger: 'blur' }
  ],
  mailTo: [
    { required: true, message: '请输入收件人', trigger: 'blur' },
    { max: 255, message: '收件人长度不能超过255个字符', trigger: 'blur' },
    { type: 'email', message: '收件人邮箱格式不正确', trigger: 'blur' }
  ],
  mailSubject: [
    { required: true, message: '请输入邮件主题', trigger: 'blur' },
    { max: 255, message: '邮件主题长度不能超过255个字符', trigger: 'blur' }
  ],
  mailBody: [
    { required: true, message: '请输入邮件内容', trigger: 'blur' }
  ],
  mailCc: [
    { max: 255, message: '抄送长度不能超过255个字符', trigger: 'blur' },
    { type: 'email', message: '抄送邮箱格式不正确', trigger: 'blur' }
  ],
  mailAttachments: [
    { max: 1000, message: '附件长度不能超过1000个字符', trigger: 'blur' }
  ],
  remark: [
    { max: 500, message: '备注长度不能超过500个字符', trigger: 'blur' }
  ]
}

// 监听对话框显示状态
watch(
  () => props.open,
  async (val) => {
    if (val) {
      // 重置表单
      formRef.value?.resetFields()
      
      // 如果是编辑模式，加载邮件数据
      if (props.mailId) {
        try {
          const res = await getMailDetail(props.mailId)
          if (res.data.code === 200) {
            const mail = res.data.data
            formData.value = {
              mailId: mail.mailId,
              mailFrom: mail.mailFrom,
              mailTo: mail.mailTo,
              mailSubject: mail.mailSubject,
              mailBody: mail.mailBody,
              mailIsHtml: mail.mailIsHtml,
              mailCc: mail.mailCc,
              mailAttachments: mail.mailAttachments,
              mailStatus: mail.mailStatus,
              remark: mail.remark
            }
          } else {
            message.error(res.data.msg || '获取邮件数据失败')
            handleCancel()
          }
        } catch (error) {
          console.error('获取邮件数据失败:', error)
          message.error('获取邮件数据失败')
          handleCancel()
        }
      }
    }
  }
)

// 取消
const handleCancel = () => {
  emit('update:open', false)
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    
    const submitData = { ...formData.value }
    let res
    
    if (props.mailId) {
      // 编辑模式
      res = await updateMail(props.mailId, submitData as TaktMailUpdateDto)
    } else {
      // 新增模式
      res = await createMail(submitData as TaktMailCreateDto)
    }
    
    if (res.data.code === 200) {
      message.success(props.mailId ? '修改成功' : '新增成功')
      emit('success')
      handleCancel()
    } else {
      message.error(res.data.msg || (props.mailId ? '修改失败' : '新增失败'))
    }
  } catch (error) {
    console.error(error)
    message.error(props.mailId ? '修改失败' : '新增失败')
  }
}
</script> 
