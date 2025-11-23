<template>
  <Takt-modal
    :open="open"
    @update:open="emit('update:open', $event)"
    title="邮件详情"
    :width="800"
    :mask-closable="false"
    @cancel="handleCancel"
    :footer="null"
  >
    <a-descriptions :column="1" bordered>
      <a-descriptions-item label="发件人">
        {{ mailData.mailFrom }}
      </a-descriptions-item>
      <a-descriptions-item label="收件人">
        {{ mailData.mailTo }}
      </a-descriptions-item>
      <a-descriptions-item label="主题">
        {{ mailData.mailSubject }}
      </a-descriptions-item>
      <a-descriptions-item label="内容">
        <div class="mail-body" v-html="mailData.mailBody"></div>
      </a-descriptions-item>
      <a-descriptions-item label="抄送" v-if="mailData.mailCc">
        {{ mailData.mailCc }}
      </a-descriptions-item>
      <a-descriptions-item label="附件" v-if="mailData.mailAttachments">
        <div class="attachments">
          <a-tag v-for="(attachment, index) in attachments" :key="index">
            {{ attachment }}
          </a-tag>
        </div>
      </a-descriptions-item>
      <a-descriptions-item label="发送状态">
        <Takt-dict-tag dict-type="sys_yes_no" :value="mailData.mailStatus" />
      </a-descriptions-item>
      <a-descriptions-item label="发送时间" v-if="mailData.mailSendTime">
        {{ mailData.mailSendTime }}
      </a-descriptions-item>
      <a-descriptions-item label="创建时间" v-if="mailData.createTime">
        {{ mailData.createTime }}
      </a-descriptions-item>
      <a-descriptions-item label="备注" v-if="mailData.remark">
        {{ mailData.remark }}
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { TaktMailDto } from '@/types/routine/mail.d'
import { getMailDetail } from '@/api/routine/mail'

const props = defineProps<{
  open: boolean
  mailId?: number | bigint
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
}>()

// 邮件数据
const mailData = ref<TaktMailDto>({} as TaktMailDto)

// 附件列表
const attachments = computed(() => {
  return mailData.value.mailAttachments?.split(',').filter(Boolean) || []
})

// 监听对话框显示状态
watch(
  () => props.open,
  async (val) => {
    if (val && props.mailId) {
      try {
        const res = await getMailDetail(props.mailId)
        if (res.data.code === 200) {
          mailData.value = res.data.data
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

// 取消
const handleCancel = () => {
  emit('update:open', false)
}
</script>

<style lang="less" scoped>
.mail-body {
  white-space: pre-wrap;
  word-break: break-all;
}

.attachments {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}
</style> 
