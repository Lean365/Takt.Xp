<template>
  <div class="Takt-file-upload">
    <div class="Takt-upload-list">
      <a-upload-dragger
        :action="uploadUrl"
        :before-upload="handleBeforeUpload"
        :custom-request="customRequest"
        :show-upload-list="true"
        :headers="headers"
        :file-list="fileList"
        :max-count="maxCount"
        @change="handleChange"
      >
        <p class="ant-upload-drag-icon">
          <inbox-outlined />
        </p>
        <p class="ant-upload-text">点击或拖拽文件到此区域上传</p>
        <p class="ant-upload-hint">支持单个或批量上传</p>
      </a-upload-dragger>
    </div>

    <!-- 上传进度 -->
    <a-progress
      v-if="uploadProgress > 0 && uploadProgress < 100"
      :percent="uploadProgress"
      :format="progressFormat"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { message, Upload } from 'ant-design-vue'
import { UploadOutlined, FileOutlined, InboxOutlined } from '@ant-design/icons-vue'
import type { UploadChangeParam, UploadFile } from 'ant-design-vue'
import type { UploadRequestOption } from 'ant-design-vue/es/vc-upload/interface'
import fileUploader, { ChunkInfo } from '@/utils/uploadUtil'
import { PropType } from 'vue'
import { COOKIE_KEYS, cookieUtils } from '@/utils/cookieConfig'

interface FileItem {
  uid: string
  name: string
  status?: string
  url?: string
  percent?: number
  size?: number
}

// 配置参数
const props = defineProps({
  uploadUrl: {
    type: String,
    required: true
  },
  // 保存路径配置
  savePath: {
    type: String,
    default: 'uploads' // 默认保存目录
  },
  // 文件数量限制
  maxCount: {
    type: Number,
    default: 5 // 默认最多5个文件
  },
  // 文件大小限制
  maxSize: {
    type: Number,
    default: 500 // 默认最大500MB
  },
  // 文件类型限制
  accept: {
    type: String,
    default: '' // 接受的文件类型，例如 '.jpg,.png,.pdf'
  },
  fileTypes: {
    type: Array as PropType<string[]>,
    default: () => [] // 允许的文件类型数组，例如 ['image/jpeg', 'image/png']
  },
  // 文件名称处理方式
  nameStrategy: {
    type: String as PropType<'original' | 'random' | 'custom'>,
    default: 'original' // original: 原文件名, random: 随机文件名, custom: 自定义
  },
  // 自定义文件名模板
  nameTemplate: {
    type: String,
    default: '{filename}{ext}' // 支持 {filename} {ext} {timestamp} {random} 变量
  },
  // 分片上传配置
  chunkSize: {
    type: Number,
    default: 5 * 1024 * 1024 // 默认分块大小5MB
  }
})

// 上传状态
const uploadRef = ref()
const fileList = ref<UploadFile[]>([])
const uploadProgress = ref(0)
const currentFile = ref<File | null>(null)
const chunks = ref<ChunkInfo[]>([])
const uploadedChunks = ref(new Set<string>())

// 计算属性
const headers = computed(() => ({
  Authorization: 'Bearer ' + cookieUtils.get(COOKIE_KEYS.ACCESS_TOKEN)
}))

// 格式化进度
const progressFormat = (percent?: number) => {
  if (percent === undefined) return ''
  return percent === 100 ? '上传完成' : `${percent}%`
}

// 格式化文件大小
const formatFileSize = (size?: number) => {
  if (!size) return '0 B'
  const units = ['B', 'KB', 'MB', 'GB', 'TB']
  let index = 0
  while (size >= 1024 && index < units.length - 1) {
    size /= 1024
    index++
  }
  return `${size.toFixed(2)} ${units[index]}`
}

// 生成文件名
const generateFileName = (file: File): string => {
  const ext = file.name.substring(file.name.lastIndexOf('.'))
  const filename = file.name.substring(0, file.name.lastIndexOf('.'))
  const timestamp = Date.now()
  const random = Math.random().toString(36).substring(2, 8)

  switch (props.nameStrategy) {
    case 'original':
      return file.name
    case 'random':
      return `${random}${ext}`
    case 'custom':
      return props.nameTemplate
        .replace('{filename}', filename)
        .replace('{ext}', ext)
        .replace('{timestamp}', timestamp.toString())
        .replace('{random}', random)
    default:
      return file.name
  }
}

// 生成日期路径
const getDatePath = () => {
  const date = new Date()
  const year = date.getFullYear()
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  return `${year}${month}${day}`
}

// 获取完整保存路径
const getSavePath = computed(() => {
  return `${props.savePath}/${getDatePath()}`
})

// 上传前处理
const handleBeforeUpload = (file: File) => {
  // 检查文件大小
  if (file.size > props.maxSize * 1024 * 1024) {
    message.error(`文件大小不能超过${props.maxSize}MB`)
    return Upload.LIST_IGNORE
  }

  // 检查文件类型
  if (props.accept && !file.name.toLowerCase().match(props.accept)) {
    message.error('不支持的文件类型')
    return Upload.LIST_IGNORE
  }

  // 检查MIME类型
  if (props.fileTypes.length > 0 && !props.fileTypes.includes(file.type)) {
    message.error('不支持的文件类型')
    return Upload.LIST_IGNORE
  }

  // 检查数量限制
  if (fileList.value.length >= props.maxCount) {
    message.error(`最多只能上传${props.maxCount}个文件`)
    return Upload.LIST_IGNORE
  }

  return true
}

// 自定义上传
const customRequest = async (options: UploadRequestOption) => {
  const { file, onProgress, onSuccess, onError } = options
  try {
    await uploadFile(file as File, (percent: number) => {
      onProgress?.({ percent })
    })
    onSuccess?.({})
  } catch (error) {
    onError?.(error as Error)
  }
}

// 上传文件
const uploadFile = async (file: File, onProgress?: (percent: number) => void) => {
  try {
    currentFile.value = file
    const fileMd5 = await fileUploader.calculateFileMD5(file, (progress) => {
      console.log('MD5 计算进度:', progress)
    }, (error) => {
      console.error('MD5 计算错误:', error)
    })
    const fileName = generateFileName(file)
    const fileChunks = fileUploader.createFileChunks(file)
    chunks.value = fileChunks.map(chunk => ({
      ...chunk,
      hash: `${fileMd5}-${chunk.index}`
    }))

    // 检查已上传的块
    const response = await fetch(`${props.uploadUrl}/check`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...headers.value
      },
      body: JSON.stringify({
        filename: fileName,
        fileMd5,
        savePath: getSavePath.value
      })
    })

    const { uploaded } = await response.json()
    uploadedChunks.value = new Set(uploaded)

    // 开始上传未完成的块
    await fileUploader.uploadChunks(
      chunks.value,
      props.uploadUrl,
      headers.value,
      (progress) => {
        uploadProgress.value = progress
        onProgress?.(progress)
      },
      uploadedChunks.value
    )

    // 所有分块上传完成，通知服务器合并
    if (uploadedChunks.value.size === chunks.value.length) {
      const mergeResponse = await fileUploader.mergeChunks(
        props.uploadUrl,
        headers.value,
        {
          filename: fileName,
          size: file.size,
          chunks: chunks.value.length
        }
      )

      const result = await mergeResponse.json()
      if (result.code === 200) {
        message.success('上传成功')
        emit('success', {
          ...result,
          fileOriginalName: file.name,
          fileExtension: file.name.substring(file.name.lastIndexOf('.')),
          fileType: file.type,
          fileSize: file.size,
          fileMd5
        })
      } else {
        throw new Error('文件合并失败')
      }
    }
  } catch (error) {
    message.error('上传失败')
    emit('error', error)
  }
}

// 处理上传状态改变
const handleChange = (info: UploadChangeParam) => {
  // 如果文件被忽略，不做任何处理
  if (info.file.status === 'error' && info.file.response === Upload.LIST_IGNORE) {
    return
  }
  
  fileList.value = info.fileList
  if ((info.file.status === 'done' || info.file.status === 'success') && info.file.originFileObj) {
    console.log('emit file-selected', info.file.originFileObj)
    emit('file-selected', info.file.originFileObj)
  }
}

// 定义事件
const emit = defineEmits<{
  (e: 'file-selected', file: File): void
  (e: 'success', result: { fileOriginalName: string, fileExtension: string, fileType: string, fileSize: number, fileMd5: string }): void
  (e: 'error', error: any): void
}>()
</script>

<style lang="less" scoped>
.Takt-file-upload {
  .Takt-upload-list {
    :deep(.ant-upload-list) {
      .ant-upload-list-item {
        padding: 8px;
        border: 1px solid #d9d9d9;
        border-radius: 4px;
        margin-bottom: 8px;
      }
    }
  }

  .Takt-upload-item {
    display: flex;
    align-items: center;
    gap: 8px;

    .Takt-file-name {
      flex: 1;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }

    .Takt-file-size {
      color: rgba(0, 0, 0, 0.45);
      font-size: 12px;
    }
  }
}
</style> 
