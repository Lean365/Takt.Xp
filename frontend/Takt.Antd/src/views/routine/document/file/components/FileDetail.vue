<template>
  <a-modal
    v-model:open="visible"
    :title="t('routine.file.detail.title')"
    width="800px"
    :mask-closable="false"
    :confirm-loading="loading"
    @cancel="handleCancel"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('routine.file.fileName')" :span="1">
        {{ form.fileName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.fileType')" :span="1">
        {{ form.fileType }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.fileSize')" :span="1">
        {{ formatFileSize(form.fileSize) }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.status')" :span="1">
        <Takt-dict-tag dict-type="sys_normal_disable" :value="form.fileStatus" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.filePath')" :span="2">
        {{ form.filePath }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.fileUrl')" :span="2">
        {{ form.fileAccessUrl }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.fileMd5')" :span="2">
        {{ form.fileMd5 }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.uploader')" :span="1">
        {{ form.fileUploaderName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('routine.file.createTime')" :span="1">
        {{ formatDateTime(form.createTime) }}
      </a-descriptions-item>
    </a-descriptions>

    <template #footer>
      <a-space>
        <a-button @click="handleCancel">{{ t('common.button.close') }}</a-button>
        <a-button type="primary" @click="handleDownload">{{ t('common.button.download') }}</a-button>
      </a-space>
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktFile } from '@/types/routine/document/file'
import { getFileDetail, downloadFile } from '@/api/routine/document/file'
import { formatDateTime } from '@/utils/formatUtil'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  fileId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
}>()

// 计算属性：处理模态框的显示状态
const visible = computed({
  get: () => props.open,
  set: (val) => emit('update:open', val)
})

// 加载状态
const loading = ref(false)

// 表单数据
const form = reactive<TaktFile & { fileUploaderName?: string }>(Object.assign({
  id: 0,
  createBy: '',
  isDeleted: 0,
  fileId: 0,
  fileOriginalName: '',
  fileExtension: '',
  fileName: '',
  fileType: '',
  fileSize: 0,
  fileStorageType: 0,
  fileStorageLocation: '',
  filePath: '',
  fileAccessUrl: '',
  fileMd5: '',
  fileStatus: 0,
  fileDownloadCount: 0,
  fileUploaderId: 0,
  fileUploaderName: '',
  createTime: '',
  updateTime: '',
  remark: '',
}, {}))

// 格式化文件大小
const formatFileSize = (size: number) => {
  if (size < 1024) {
    return size + ' ' + t('routine.file.unit.B')
  } else if (size < 1024 * 1024) {
    return (size / 1024).toFixed(2) + ' ' + t('routine.file.unit.KB')
  } else if (size < 1024 * 1024 * 1024) {
    return (size / (1024 * 1024)).toFixed(2) + ' ' + t('routine.file.unit.MB')
  } else {
    return (size / (1024 * 1024 * 1024)).toFixed(2) + ' ' + t('routine.file.unit.GB')
  }
}

// 获取文件信息
const getInfo = async (fileId: number) => {
  try {
    loading.value = true
    const res = await getFileDetail(fileId)
    if (res.code === 200) {
      Object.assign(form, res.data)
    } else {
      message.error(res.msg || t('routine.file.downloadFailed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 下载文件
const handleDownload = async () => {
  try {
    const response = await downloadFile(form.fileId)
    const blob = new Blob([response.data], { type: response.headers['content-type'] })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = form.fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载文件失败:', error)
    message.error(t('common.failed'))
  }
}

// 处理对话框显示状态变化
const handleCancel = () => {
  emit('update:open', false)
}

// 监听文件ID变化
watch(
  () => props.fileId,
  (val) => {
    if (val) {
      getInfo(val)
    }
  }
)
</script>

<style lang="less" scoped>
:deep(.ant-descriptions) {
  padding: 24px;
}

:deep(.ant-descriptions-item-label) {
  width: 120px;
  text-align: right;
  background-color: #fafafa;
}

:deep(.ant-descriptions-item-content) {
  padding: 12px 24px;
}
</style> 
