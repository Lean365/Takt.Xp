<template>
  <a-modal
    :open="visible"
    :title="title"
    :confirm-loading="loading"
    @ok="handleOk"
    @cancel="handleCancel"
    width="800px"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
      layout="horizontal"
    >
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('routine.file.filePath')" name="filePath">
            <Takt-select
              v-model:value="formState.filePath"
              dict-type="file_path"
              :allow-clear="true"
              show-search
              type="radio-button"
              :placeholder="t('routine.file.placeholder.filePath')"
              :value-type="'string'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('routine.file.fileStorageLocation')" name="fileStorageLocation">
            <Takt-select
              v-model:value="formState.fileStorageLocation"
              dict-type="file_storage_location"
              :allow-clear="true"
              show-search
              type="radio-button"
              :placeholder="t('routine.file.placeholder.fileStorageLocation')"
              :value-type="'string'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('routine.file.fileStorageType')" name="fileStorageType">
            <Takt-select
              v-model:value="formState.fileStorageType"
              dict-type="file_storage_type"
              :allow-clear="true"
              type="radio-button"
              show-search
              :placeholder="t('routine.file.placeholder.fileStorageType')"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('routine.file.fileName')" name="fileName">
            <Takt-select
              v-model:value="formState.fileName"
              dict-type="file_name_rule"
              type="radio-button"
              :allow-clear="true"
              show-search
              :placeholder="t('routine.file.placeholder.fileName')"
              :value-type="'string'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-tabs v-model:activeKey="activeTab">
            <a-tab-pane key="image" tab="图片上传">
              <div style="display: flex; justify-content: center;">
                
                  <Takt-image-upload
                    upload-url="/api/TaktFile/upload"
                    :save-path="formState.filePath"
                    :headers="uploadHeaders"
                    style="width: 100%; height: 100%;"
                    @error="handleFileError"
                    @file-selected="onUploadSuccess"
                  />
                </div>
           
            </a-tab-pane>
            <a-tab-pane key="avatar" tab="头像上传">
              <div style="display: flex; justify-content: center;">
                
                  <Takt-avatar-upload
                    upload-url="/api/TaktFile/upload"
                    :save-path="formState.filePath"
                    :headers="uploadHeaders"
                    style="width: 100%; height: 100%;"
                    @error="handleFileError"
                    @file-selected="onUploadSuccess"
                  />
                </div>
           
            </a-tab-pane>
            <a-tab-pane key="file" tab="文件上传">
              <div style="display: flex; justify-content: center;">
               
                  <Takt-file-upload
                    upload-url="/api/TaktFile/upload"
                    :save-path="formState.filePath"
                    :headers="uploadHeaders"
                    style="width: 100%; height: 100%;"
                    @error="handleFileError"
                    @file-selected="onUploadSuccess"
                  />
                </div>
             
            </a-tab-pane>
          </a-tabs>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('routine.file.status')" name="fileStatus">
            <Takt-select
              v-model:value="formState.fileStatus"
              dict-type="sys_normal_disable"
              type="radio-button"
              :allow-clear="true"
              :show-all="false"
              show-search
              :placeholder="t('routine.file.placeholder.status')"
            />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('routine.file.remark')" name="remark">
            <a-textarea v-model:value="formState.remark" :rows="2" :placeholder="t('routine.file.placeholder.remark')" />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { getFileDetail, createFile, updateFile, uploadFile } from '@/api/routine/document/file'
import type { TaktFile, TaktFileCreate, TaktFileUpload } from '@/types/routine/document/file'
import type { Rule } from 'ant-design-vue/es/form'
import { getToken } from '@/utils/authUtil'
import request from '@/utils/request'
import { useDictStore } from '@/stores/dictStore'
import CryptoJS from 'crypto-js'

const activeTab = ref('image')
const props = defineProps<{
  visible: boolean
  title: string
  fileId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)
const fileList = ref<any[]>([])

const formState = reactive<TaktFileUpload>({
  fileOriginalName: '',
  fileExtension: '',
  fileName: 'random',
  filePath: 'uploads/images',
  fileType: '',
  fileSize: 0,
  fileStorageType: 'local',
  fileStorageLocation: 'default',
  fileAccessUrl: '',
  fileMd5: '',
  fileStatus: 0,
  remark: ''
})

const rules: Record<string, Rule[]> = {
  fileName: [
    { required: true, message: t('routine.file.validation.fileName.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('routine.file.validation.fileName.maxLength'), trigger: 'blur' }
  ],
  fileType: [
    { required: true, message: t('routine.file.validation.fileType.required'), trigger: 'change' }
  ],
  fileStatus: [
    { required: true, message: t('routine.file.validation.status.required'), trigger: 'change' }
  ]
}

const dictStore = useDictStore()

onMounted(async () => {
  // 加载字典数据
  await dictStore.loadDict('file_name_rule')
  
  // 调试输出
  console.log('file_name_rule options:', dictStore.getDictOptions('file_name_rule'))
  console.log('current fileName value:', formState.fileName)
})

// 监听字典数据变化
watch(() => dictStore.getDictOptions('file_name_rule'), (newOptions) => {
  console.log('file_name_rule options changed:', newOptions)
}, { immediate: true })

// 监听fileName变化
watch(() => formState.fileName, (newValue) => {
  console.log('fileName changed:', newValue)
}, { immediate: true })

// 文件名生成函数，支持模板和规则
function generateFileNameByRule(rule: string, originalName: string, extension: string): string {
  const ext = extension || originalName.substring(originalName.lastIndexOf('.'))
  const base = originalName.replace(ext, '')
  const timestamp = Date.now()
  const random = Math.random().toString(36).substr(2, 8)
  const date = new Date()
  const yyyyMMdd = `${date.getFullYear()}${(date.getMonth()+1).toString().padStart(2,'0')}${date.getDate().toString().padStart(2,'0')}`

  // 支持自定义模板
  if (rule && rule.includes('{')) {
    return rule
      .replace('{filename}', base)
      .replace('{ext}', ext)
      .replace('{timestamp}', timestamp.toString())
      .replace('{random}', random)
      .replace('{yyyyMMdd}', yyyyMMdd)
  }

  switch(rule) {
    case 'random':
      const randomStr = Math.random().toString(36).substr(2, 8)
      return `${randomStr}${ext}`
    case 'original':
      return originalName
    case 'timestamp':
      return `${base}_${timestamp}${ext}`
    default:
      return originalName
  }
}

let isSettingForm = false
let selectedFile: File | null = null

watch(() => props.fileId, async (newVal) => {
  if (isSettingForm) return
  if (newVal) {
    isSettingForm = true
    try {
      const res = await getFileDetail(newVal)
      if (res.data) {
        const data = res.data
        formState.filePath = data.filePath || ''
        formState.fileStorageLocation = data.fileStorageLocation || ''
        formState.fileStorageType = String(data.fileStorageType || '')
        formState.fileName = data.fileName || 'random'
        formState.fileStatus = Number(data.fileStatus || 0)
        formState.remark = data.remark || ''
      }
    } finally {
      isSettingForm = false
    }
  } else {
    formState.filePath = ''
    formState.fileStorageLocation = ''
    formState.fileStorageType = ''
    formState.fileName = 'random'
    formState.fileStatus = 0
    formState.remark = ''
  }
})

// 生成包含年月日的具体目录
const generateActualPath = (basePath: string): string => {
  if (!basePath) return ''
  
  const date = new Date()
  const year = date.getFullYear()
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  
  return `${basePath}/${year}/${month}/${day}`
}

// 根据文件名规则生成具体的文件名称
const generateActualFileName = (rule: string, originalName: string, extension: string): string => {
  if (!originalName) return ''
  
  const ext = extension || originalName.substring(originalName.lastIndexOf('.'))
  const base = originalName.replace(ext, '')
  
  switch(rule) {
    case 'random':
      const randomStr = Math.random().toString(36).substr(2, 8)
      return `${randomStr}${ext}`
    case 'original':
      return originalName
    case 'timestamp':
      const timestamp = new Date().toISOString().replace(/[-:T]/g, '').slice(0, 14)
      return `${base}_${timestamp}${ext}`
    default:
      return originalName
  }
}

// 计算上传请求头
const uploadHeaders = computed(() => {
  const csrfToken = document.cookie
    .split('; ')
    .find(row => row.startsWith('CSRF-TOKEN='))
    ?.split('=')[1]
  
  return {
    'X-CSRF-TOKEN': csrfToken || '',
    'Content-Type': 'multipart/form-data'
  }
})

// 处理文件上传
const handleUpload = async (file: File) => {
  if (file.size === 0) {
    message.error(t('routine.file.upload.fileEmpty'))
    throw new Error(t('routine.file.upload.fileEmpty'))
  }
  selectedFile = file
  formState.fileOriginalName = file.name
  formState.fileExtension = file.name.substring(file.name.lastIndexOf('.'))
  formState.fileType = file.type
  formState.fileSize = file.size
  // 其他字段在 handleOk 统一生成
}

// 计算文件md5
async function calcFileMd5(file: File): Promise<string> {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.onload = function (e) {
      try {
        const wordArray = CryptoJS.lib.WordArray.create(e.target?.result as ArrayBuffer)
        const md5 = CryptoJS.MD5(wordArray).toString()
        resolve(md5)
      } catch (error) {
        reject(error)
      }
    }
    reader.onerror = function (e) { reject(e) }
    reader.readAsArrayBuffer(file)
  })
}

// 处理上传成功
const onUploadSuccess = async (file: File) => {
  try {
    await handleUpload(file)
  } catch (error: any) {
    console.error('文件处理失败:', error)
    message.error(t('routine.file.upload.failed'))
  }
}

// 处理文件错误
const handleFileError = (error: any) => {
  console.error('文件上传失败:', error)
  message.error(t('routine.file.upload.failed'))
}

// 处理文件选择
const handleFileSelected = async (file: File) => {
  try {
    await handleUpload(file)
  } catch (error) {
    message.error(t('routine.file.upload.failed'))
  }
}

// 处理图片文件选择
const handleImageFileSelected = async (file: File) => {
  try {
    await handleUpload(file)
  } catch (error) {
    message.error(t('routine.file.upload.failed'))
  }
}

// 添加访问URL生成函数
const generateFileAccessUrl = (filePath: string, fileName: string, storageType: string, storageLocation: string): string => {
  // 获取当前域名
  const protocol = window.location.protocol;
  const host = window.location.host;
  const baseUrl = `${protocol}//${host}`;
  
  // 根据存储类型生成不同的访问URL
  switch(storageType) {
    case 'local':
      return `/api/files/${filePath}/${fileName}`;
    case 'aliyun':
      return `https://${storageLocation}.oss-cn-hangzhou.aliyuncs.com/${filePath}/${fileName}`;
    case 'tencent':
      return `https://${storageLocation}.cos.ap-guangzhou.myqcloud.com/${filePath}/${fileName}`;
    case 'qiniu':
      return `https://${storageLocation}.qiniucdn.com/${filePath}/${fileName}`;
    default:
      return `/api/files/${filePath}/${fileName}`;
  }
}

// 重置表单
const resetForm = () => {
  formRef.value?.resetFields()
  Object.assign(formState, {
    fileOriginalName: '',
    fileExtension: '',
    fileName: 'random',
    filePath: 'uploads/images',
    fileType: '',
    fileSize: 0,
    fileStorageType: 'local',
    fileStorageLocation: 'default',
    fileAccessUrl: '',
    fileMd5: '',
    fileStatus: 0,
    remark: ''
  })
  activeTab.value = 'image'
  fileList.value = []
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 确定
const handleOk = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    if (!selectedFile) {
      message.error(t('routine.file.upload.fileEmpty'))
      return
    }
    // 统一生成 fileName、fileAccessUrl、fileMd5 等前端信息
    const fileNameRule = formState.fileName || 'random'
    const baseFilePath = formState.filePath || 'uploads/images'
    const date = new Date()
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')
    const physicalPath = `${baseFilePath}/${year}/${month}/${day}`
    const storageType = String(formState.fileStorageType || 'local')
    const storageLocation = formState.fileStorageLocation || 'default'
    const actualFileName = generateFileNameByRule(fileNameRule, formState.fileOriginalName, formState.fileExtension)
    const fileAccessUrl = generateFileAccessUrl(physicalPath, actualFileName, storageType, storageLocation)
    const fileMd5 = await calcFileMd5(selectedFile)
    // 组装FormData，统一上传
    const formData = new FormData()
    formData.append('file', selectedFile)
    formData.append('fileOriginalName', formState.fileOriginalName || '')
    formData.append('fileExtension', formState.fileExtension || '')
    formData.append('fileName', actualFileName || '')
    formData.append('filePath', physicalPath || '')
    formData.append('fileType', formState.fileType || '')
    formData.append('fileSize', String(formState.fileSize || 0))
    formData.append('fileStorageType', storageType || '')
    formData.append('fileStorageLocation', storageLocation || '')
    formData.append('fileAccessUrl', fileAccessUrl || '')
    formData.append('fileMd5', fileMd5 || '')
    formData.append('fileStatus', String(formState.fileStatus || 0))
    formData.append('remark', formState.remark || '')
    const response = await request({
      url: '/api/TaktFile/upload',
      method: 'post',
      data: formData,
      headers: { 'Content-Type': 'multipart/form-data' }
    })
    console.log('上传响应:', response)
    if (response.code === 200) {
      message.success(t('routine.file.upload.success'))
      emit('success')
      handleCancel()
    } else {
      message.error(response.msg || t('routine.file.upload.failed'))
    }
  } catch (error: any) {
    console.error('文件操作失败:', error)
    if (error?.response?.data?.msg) {
      message.error(error.response.data.msg)
    } else if (error?.message) {
      message.error(error.message)
    } else {
      message.error(t('routine.file.upload.failed'))
    }
  } finally {
    loading.value = false
  }
}
</script>

<style lang="less" scoped>
.ant-form {
  margin-top: 16px;
}
</style> 
