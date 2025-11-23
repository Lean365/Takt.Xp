<template>
  <Takt-modal
    :open="open"
    @update:open="emit('update:open', $event)"
    title="发送邮件"
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
      <a-form-item label="收件人" name="mailTo">
        <a-input
          v-model:value="formData.mailTo"
          placeholder="请输入收件人"
          :maxLength="100"
          disabled
        />
      </a-form-item>
      <a-form-item label="主题" name="mailSubject">
        <a-input
          v-model:value="formData.mailSubject"
          placeholder="请输入邮件主题"
          :maxLength="200"
          disabled
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
          placeholder="请输入抄送人，多个收件人用英文逗号分隔"
          :maxLength="200"
        />
      </a-form-item>
      <a-form-item label="附件" name="mailAttachments">
        <a-upload
          v-model:file-list="fileList"
          :action="uploadUrl"
          :headers="uploadHeaders"
          :before-upload="beforeUpload"
          :max-count="5"
          multiple
        >
          <a-button>
            <template #icon><upload-outlined /></template>
            选择文件
          </a-button>
        </a-upload>
      </a-form-item>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import type { FormInstance, UploadProps } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form/interface'
import { message } from 'ant-design-vue'
import { UploadOutlined } from '@ant-design/icons-vue'
import type { TaktMail, TaktMailCreate } from '@/types/routine/email/mail'
import { getMailDetail, sendMail } from '@/api/routine/email/mail'
import { getToken } from '@/utils/auth'

const props = defineProps<{
  open: boolean
  mailId?: number | bigint
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
  (e: 'success'): void
}>()

// 表单实例
const formRef = ref<FormInstance>()

// 表单数据
const formData = ref<{
  mailTo: string
  mailSubject: string
  mailBody: string
  mailCc?: string
  mailAttachments?: string
}>({
  mailTo: '',
  mailSubject: '',
  mailBody: '',
  mailCc: '',
  mailAttachments: ''
})

// 文件列表
const fileList = ref<any[]>([])

// 上传地址
const uploadUrl = import.meta.env.VITE_APP_BASE_API + '/api/TaktMail/upload'

// 上传请求头
const uploadHeaders = {
  Authorization: 'Bearer ' + getToken()
}

// 表单校验规则
const rules: Record<string, RuleObject[]> = {
  mailBody: [
    { required: true, message: '请输入邮件内容', trigger: 'blur' },
    { max: 4000, message: '邮件内容长度不能超过4000个字符', trigger: 'blur' }
  ],
  mailCc: [
    { max: 200, message: '抄送人长度不能超过200个字符', trigger: 'blur' }
  ]
}

// 监听对话框显示状态
watch(
  () => props.open,
  async (val) => {
    if (val && props.mailId) {
      // 重置表单
      formRef.value?.resetFields()
      fileList.value = []
      
      // 加载邮件数据
      try {
        const res = await getMailDetail(props.mailId)
        if (res.data.code === 200) {
          const mail = res.data.data
          formData.value = {
            mailTo: mail.mailTo,
            mailSubject: mail.mailSubject,
            mailBody: mail.mailBody || '',
            mailCc: mail.mailCc || '',
            mailAttachments: mail.mailAttachments || ''
          }
        } else {
          message.error(res.data.msg || '获取邮件数据失败')
          handleCancel()
        }
      } catch (error) {
        console.error(error)
        message.error('获取邮件数据失败')
        handleCancel()
      }
    }
  }
)

// 上传前检查
const beforeUpload: UploadProps['beforeUpload'] = (file) => {
  const isLt10M = file.size / 1024 / 1024 < 10
  if (!isLt10M) {
    message.error('文件大小不能超过10MB!')
  }
  return isLt10M
}

// 取消
const handleCancel = () => {
  emit('update:open', false)
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    
    // 处理附件
    const attachments = fileList.value.map(file => file.response?.data || file.url).filter(Boolean).join(',')
    
    const submitData: TaktMailCreate = {
      ...formData.value,
      mailFrom: 'system@example.com', // 系统默认发件人
      mailIsHtml: 0,
      mailStatus: 0,
      mailAttachments: attachments
    }
    
    const res = await sendMail(submitData)
    
    if (res.data.code === 200) {
      message.success('发送成功')
      emit('success')
      handleCancel()
    } else {
      message.error(res.data.msg || '发送失败')
    }
  } catch (error) {
    console.error(error)
    message.error('发送失败')
  }
}
</script> 
