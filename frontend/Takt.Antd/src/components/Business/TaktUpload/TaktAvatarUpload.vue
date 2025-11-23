<template>
  <div class="Takt-avatar-upload">
    <div class="Takt-upload-list">
      <a-upload
        ref="uploadRef"
        list-type="picture-card"
        :action="uploadUrl"
        :before-upload="handleBeforeUpload"
        :show-upload-list="true"
        :headers="headers"
        :file-list="fileList"
        @preview="handlePreview"
        @change="handleChange"
        @remove="handleRemove"
        drag
      >
        <div v-if="fileList.length < 1">
          <plus-outlined />
          <div style="margin-top: 8px">上传头像（点击或拖拽）</div>
        </div>
      </a-upload>
    </div>

    <a-image
      v-if="previewImage"
      :style="{ display: 'none' }"
      :src="previewImage"
      :preview="{
        visible: previewVisible,
        onVisibleChange: handlePreviewVisibleChange
      }"
    />

    <!-- 图片裁剪 -->
    <Takt-images-cropper
      v-model:visible="cropperVisible"
      :image-url="previewImage"
      :title="cropperTitle"
      @success="handleCropperSuccess"
      @error="handleCropperError"
      @cancel="handleCropperCancel"
    />
    
    <!-- 上传进度 -->
    <a-progress
      v-if="uploadProgress > 0 && uploadProgress < 100"
      :percent="uploadProgress"
      :format="progressFormat"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount, nextTick, watch } from 'vue'
import { message, Upload } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { UploadChangeParam,  UploadFile } from 'ant-design-vue'
import fileUploader, { ChunkInfo } from '@/utils/uploadUtil'
import imageProcessor from '@/utils/imageUtil'
import { PropType } from 'vue'
import { getToken } from '@/utils/authUtil'
import TaktImagesCropper from './TaktImagesCropper.vue'

// 配置参数
interface Props {
  uploadUrl: string
  savePath?: string
  fileName?: string
  maxSize?: number
  accept?: string
  fileTypes?: string[]
  nameStrategy?: 'original' | 'random' | 'custom'
  nameTemplate?: string
  chunkSize?: number
  compress?: {
    quality: number
    maxWidth: number
    maxHeight: number
  }
}

const props = withDefaults(defineProps<Props>(), {
  savePath: 'uploads/avatars',
  fileName: '',
  maxSize: 5,
  accept: '.jpg,.jpeg,.png',
  fileTypes: () => ['image/jpeg', 'image/png'],
  nameStrategy: 'random',
  nameTemplate: '{random}{ext}',
  chunkSize: 2 * 1024 * 1024,
  compress: () => ({
    quality: 0.9,
    maxWidth: 800,
    maxHeight: 800
  })
})

// 上传状态
const uploadRef = ref()
const fileList = ref<UploadFile[]>([])
const uploadProgress = ref(0)
const currentFile = ref<File | null>(null)
const chunks = ref<ChunkInfo[]>([])
const uploadedChunks = ref(new Set<string>())

// 预览状态
const previewVisible = ref(false)
const previewImage = ref('')
const previewTitle = ref('')

// 裁剪状态
const cropperVisible = ref(false)
const cropperTitle = ref('头像裁剪')

// 计算属性
const headers = computed(() => ({
  Authorization: `Bearer ${getToken()}`
}))

// 格式化进度
const progressFormat = (percent?: number) => {
  if (percent === undefined) return ''
  return percent === 100 ? '上传完成' : `${percent}%`
}

// 定义组件事件
const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success', result: { blob: Blob; data: any; dataUrl: string }): void
  (e: 'error', error: any): void
  (e: 'cancel'): void
  (e: 'file-selected', file: File): void
}>()

// 组件引用
const containerRef = ref<HTMLElement>()
const cropperCanvas = ref<any>()
const cropperImage = ref<any>()
const cropperSelection = ref<any>()

// 清理函数
const cleanup = () => {
  previewImage.value = ''
  previewVisible.value = false
  cropperVisible.value = false
  currentFile.value = null
  fileList.value = []
}

// 组件卸载前清理
onBeforeUnmount(() => {
  cleanup()
})

// 监听裁剪弹窗
watch(cropperVisible, async (visible) => {
  if (visible) {
    await nextTick()
  } else {
    cleanup()
  }
})

// 处理预览
const handlePreview = async (file: UploadFile) => {
  try {
    if (!file) {
      console.warn('预览文件为空')
      return
    }

    // 如果文件没有预览图，生成 base64 预览
    if (!file.url && !file.preview && file.originFileObj) {
      try {
        file.preview = await getBase64(file.originFileObj as File)
      } catch (error) {
        console.error('生成预览图失败:', error)
        message.error('生成预览图失败')
        return
      }
    }

    // 设置预览状态
    if (file.preview) {
      previewImage.value = file.preview
    } else if (file.url) {
      previewImage.value = file.url
    } else {
      previewImage.value = ''
    }
    previewTitle.value = file.name || ''
    
    // 使用 nextTick 确保 DOM 更新完成后再显示预览
    await nextTick()
    previewVisible.value = true
  } catch (error) {
    console.error('处理预览时出错:', error)
    message.error('预览失败')
  }
}

// 处理预览可见性变化
const handlePreviewVisibleChange = (visible: boolean) => {
  previewVisible.value = visible
  if (!visible) {
    previewImage.value = ''
    previewTitle.value = ''
  }
}

// 上传前处理
const handleBeforeUpload = async (file: File) => {
  try {
    // 检查文件类型
    if (!props.fileTypes.includes(file.type)) {
      message.error(`不支持的图片格式，请上传${props.accept}格式的图片`)
      return Upload.LIST_IGNORE
    }

    // 检查文件大小
    const maxSize = props.maxSize * 1024 * 1024 // 转换为字节
    if (file.size > maxSize) {
      message.error(`文件大小不能超过${props.maxSize}MB`)
      return Upload.LIST_IGNORE
    }

    // 检查数量限制
    if (fileList.value.length >= 1) {
      message.error('只能上传一张头像')
      return Upload.LIST_IGNORE
    }

    // 弹出裁剪框
    currentFile.value = file
    const imageUrl = await getBase64(file)
    console.log('文件转换为base64成功，长度:', imageUrl.length)
    previewImage.value = imageUrl
    console.log('previewImage已设置，准备打开裁剪器')
    cropperVisible.value = true

    return false // 阻止默认上传
  } catch (error) {
    console.error('处理上传前出错:', error)
    message.error('处理文件失败')
    return Upload.LIST_IGNORE
  }
}

// 处理上传状态改变
const handleChange = async (info: UploadChangeParam) => {
  try {
    // 如果文件被忽略，不做任何处理
    if (info.file.status === 'error' && info.file.response === Upload.LIST_IGNORE) {
      return
    }
    
    if (!info.fileList) {
      console.warn('文件列表为空')
      return
    }

    const file = info.file
    // 使用 nextTick 确保状态更新完成
    await nextTick()
    fileList.value = info.fileList

    if ((file.status === 'done' || file.status === 'success') && file.originFileObj) {
      // 生成预览图
      if (!file.preview && file.originFileObj) {
        try {
          file.preview = await getBase64(file.originFileObj)
        } catch (error) {
          console.error('生成预览图失败:', error)
        }
      }

      // 设置预览状态
      if (file.preview) {
        previewImage.value = file.preview
      } else if (file.url) {
        previewImage.value = file.url
      } else {
        previewImage.value = ''
      }
      previewTitle.value = file.name || ''

      // 触发文件选择事件
      emit('file-selected', file.originFileObj)
    }
  } catch (error) {
    console.error('处理文件变化时出错:', error)
    message.error('处理文件变化时出错')
  }
}

// 处理删除
const handleRemove = async (file: UploadFile) => {
  try {
    if (!fileList.value || !file) {
      console.warn('文件列表或文件对象为空')
      return
    }

    const index = fileList.value.findIndex(f => f.uid === file.uid)
    if (index === -1) {
      console.warn('未找到要删除的文件')
      return
    }

    // 创建新的文件列表数组
    const newFileList = [...fileList.value]
    newFileList.splice(index, 1)
    
    // 使用 nextTick 确保状态更新完成
    await nextTick()
    fileList.value = newFileList

    // 清除预览状态
    if (previewImage.value === file.url || previewImage.value === file.preview) {
      cleanup()
    }
  } catch (error) {
    console.error('删除文件时出错:', error)
    message.error('删除文件时出错')
  }
}

// 裁剪成功
const handleCropperSuccess = async (result: { blob: Blob; data: any; dataUrl: string }) => {
  try {
    // 检查必要的参数
    if (!result || !result.blob) {
      console.error('裁剪结果为空或无效:', result)
      message.error('裁剪结果无效')
      return
    }

    const { blob, dataUrl } = result
    // 生成 base64 预览
    const preview = await getBase64(blob)
    
    // 安全地获取文件名
    let fileName = 'cropped_image.jpg'
    if (currentFile.value && currentFile.value.name) {
      fileName = currentFile.value.name
    } else {
      // 如果没有文件名，生成一个带时间戳的文件名
      const timestamp = Date.now()
      fileName = `cropped_image_${timestamp}.jpg`
    }
    
    const file = new File([blob], fileName, {
      type: blob.type || 'image/jpeg',
      lastModified: Date.now()
    })
    
    // 计算 fileMd5
    const fileMd5 = await fileUploader.calculateFileMD5(file)
    
    // 更新预览图片 - 这是关键！
    previewImage.value = preview
    
    // 简化处理：直接创建新的文件项，避免复杂的类型问题
    const newFileItem: UploadFile = {
      uid: `cropped_${Date.now()}`,
      name: fileName,
      status: 'done' as const,
      originFileObj: file as any, // 使用类型断言避免类型错误
      preview,
      thumbUrl: preview,
      size: file.size,
      type: file.type
    }
    
    // 清空原有文件列表，添加新的裁剪后的文件
    fileList.value = [newFileItem]
    
    cropperVisible.value = false
    message.success('裁剪成功')
    emit('file-selected', file)
  } catch (error) {
    console.error('处理裁剪结果时出错:', error)
    message.error('处理裁剪结果失败')
  }
}

// 裁剪错误
const handleCropperError = (error: any) => {
  console.error('裁剪失败:', error)
  message.error('图片裁剪失败')
  cropperVisible.value = false
}

// 裁剪取消
const handleCropperCancel = () => {
  cropperVisible.value = false
  previewImage.value = ''
  currentFile.value = null
}

// 生成文件名
const generateFileName = (file: File): string => {
  const ext = file.name.substring(file.name.lastIndexOf('.'))
  const filename = props.fileName || file.name.substring(0, file.name.lastIndexOf('.'))
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

// 生命周期
onMounted(() => {
  // 移除cropperRef相关代码
})

onBeforeUnmount(() => {
  // 移除cropperRef相关代码
})

// 裁剪器就绪回调
const onCropperReady = () => {
  // 移除cropperRef相关代码
}

// 工具函数
const getBase64 = imageProcessor.getBase64
</script>

<style lang="less" scoped>
.Takt-avatar-upload {
  .Takt-upload-list {
    :deep(.ant-upload-list-picture-card) {
      display: flex;
      flex-wrap: wrap;
      gap: 8px;

      .ant-upload-list-item {
        width: 104px;
        height: 104px;
        margin: 0;
        padding: 4px;
        border: 1px solid #d9d9d9;
        border-radius: 8px;
        
        &-info {
          &::before {
            left: 0;
          }
        }
        
        &-thumbnail {
          img {
            object-fit: cover;
            width: 100%;
            height: 100%;
          }
        }

        &-actions {
          .anticon-eye {
            color: #52c41a !important;
            &:hover {
              color: #73d13d !important;
            }
          }
          .anticon-delete {
            color: #ff4d4f !important;
            &:hover {
              color: #ff7875 !important;
            }
          }
        }

        &-uploading {
          padding: 8px;
          .ant-upload-list-item-progress {
            bottom: 14px;
            padding-inline: 0;
          }
        }
      }
    }

    :deep(.ant-upload.ant-upload-select) {
      width: 104px;
      height: 104px;
      margin: 0;
      border: 1px dashed #d9d9d9;
      border-radius: 8px;
      background-color: #fafafa;
      
      &:hover {
        border-color: #1890ff;
      }

      .ant-upload {
        padding: 16px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
      }
    }
  }

  .Takt-upload-tip {
    margin-top: 8px;
    color: rgba(0, 0, 0, 0.45);
  }
}
</style>

