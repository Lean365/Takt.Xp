<template>
  <div class="Takt-image-upload">
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
        <div v-if="fileList.length < maxCount">
          <plus-outlined />
          <div style="margin-top: 8px">上传图片（点击或拖拽）</div>
        </div>
      </a-upload>
    </div>

    <a-image
      v-if="previewImage"
      :style="{ display: 'none' }"
      :src="previewImage"
      :preview="{
        visible: previewVisible,
        onVisibleChange: (visible: boolean) => {
          previewVisible = visible
        }
      }"
    />

    <!-- 图片裁剪 -->
    <Takt-images-cropper
      v-model:visible="cropperVisible"
      :image-url="previewImage"
      :title="cropperTitle"
      @success="handleCropperSuccess"
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
import type { UploadChangeParam, UploadProps, UploadFile } from 'ant-design-vue'
import type { UploadRequestOption } from 'ant-design-vue/es/vc-upload/interface'
import fileUploader, { ChunkInfo } from '@/utils/uploadUtil'
import imageProcessor from '@/utils/imageUtil'
import { PropType } from 'vue'
import { getToken } from '@/utils/auth'
import TaktImagesCropper from './TaktImagesCropper.vue'

// 注册组件
const components = {
  TaktImagesCropper
}

// 配置参数
const props = defineProps({
  uploadUrl: {
    type: String,
    required: true
  },
  savePath: {
    type: String,
    default: 'uploads/images'
  },
  fileName: {
    type: String,
    default: ''
  },
  maxCount: {
    type: Number,
    default: 8
  },
  maxSize: {
    type: Number,
    default: 50
  },
  accept: {
    type: String,
    default: '.jpg,.jpeg,.png,.gif'
  },
  fileTypes: {
    type: Array as PropType<string[]>,
    default: () => ['image/jpeg', 'image/png', 'image/gif']
  },
  nameStrategy: {
    type: String as PropType<'original' | 'random' | 'custom'>,
    default: 'random'
  },
  nameTemplate: {
    type: String,
    default: '{random}{ext}'
  },
  chunkSize: {
    type: Number,
    default: 2 * 1024 * 1024
  },
  compress: {
    type: Object,
    default: () => ({
      quality: 0.8,
      maxWidth: 1920,
      maxHeight: 1080
    })
  },
  crop: {
    type: Object,
    default: () => ({
      aspect: 16 / 9,
      width: 800,
      height: 450
    })
  }
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
const cropperTitle = ref('图片裁剪')
const cropperOptions = ref({
  aspectRatio: props.crop.aspect,
  viewMode: 2,
  dragMode: 'crop',
  autoCropArea: 1,
  restore: false,
  modal: true,
  guides: true,
  highlight: true,
  cropBoxMovable: true,
  cropBoxResizable: true,
  toggleDragModeOnDblclick: false,
  responsive: true,
  checkCrossOrigin: false,
  checkOrientation: true,
  background: true,
  center: true,
  zoomOnWheel: true,
  wheelZoomRatio: 0.1,
  zoomOnTouch: true,
  movable: true,
  rotatable: true,
  scalable: true,
  zoomable: true,
  autoCrop: true,
  minCropBoxWidth: 200,
  minCropBoxHeight: 200
})

// 计算属性
const headers = computed(() => ({
  Authorization: `Bearer ${getToken()}`
}))

// 格式化进度
const progressFormat = (percent?: number) => {
  if (percent === undefined) return ''
  return percent === 100 ? '上传完成' : `${percent}%`
}

// 处理预览
const handlePreview = async (file: UploadFile) => {
  if (!file.url && !file.preview) {
    file.preview = await getBase64(file.originFileObj as File)
  }
  previewImage.value = file.url || file.preview || ''
  previewVisible.value = true
  previewTitle.value = file.name || ''
}

const handlePreviewCancel = () => {
  previewVisible.value = false
  previewTitle.value = ''
}

// 上传前处理
const handleBeforeUpload = async (file: File) => {
  // 检查文件类型
  if (!props.fileTypes.includes(file.type)) {
    message.error(`不支持的图片格式，请上传${props.accept}格式的图片`)
    return Upload.LIST_IGNORE
  }

  // 检查数量限制
  if (fileList.value.length >= props.maxCount) {
    message.error(`最多只能上传${props.maxCount}张图片`)
    return Upload.LIST_IGNORE
  }

  // 如果文件大小小于等于 maxSize，直接上传
  if (file.size <= props.maxSize * 1024 * 1024) {
    emit('file-selected', file)
    return false // 阻止默认上传
  }

  // 否则弹出裁剪框
  currentFile.value = file
  previewImage.value = await getBase64(file)
  cropperVisible.value = true

  return false // 阻止默认上传
}

// 裁剪成功
const handleCropperSuccess = async (result: any) => {
  try {
    const { blob, dataUrl } = result
    // 生成 base64 预览
    const preview = await getBase64(blob)
    // 计算 fileMd5
    const fileMd5 = await fileUploader.calculateFileMD5(blob)
    // 替换 fileList 中当前图片
    const index = fileList.value.findIndex(f => f.uid === (currentFile.value as any).uid)
    if (index !== -1) {
      fileList.value[index].originFileObj = blob as any
      fileList.value[index].preview = preview
      fileList.value[index].thumbUrl = preview
      fileList.value[index].name = currentFile.value!.name
    }
    cropperVisible.value = false
    message.success('裁剪成功')
    emit('file-selected', blob)
  } catch (error) {
    message.error('图片处理失败')
  }
}

const handleCropperCancel = () => {
  cropperVisible.value = false
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

// 处理上传状态改变
const handleChange = (info: UploadChangeParam) => {
  // 如果文件被忽略，不做任何处理
  if (info.file.status === 'error' && info.file.response === Upload.LIST_IGNORE) {
    return
  }
  
  const file = info.file
  fileList.value = info.fileList
  if ((file.status === 'done' || file.status === 'success') && file.originFileObj) {
    emit('file-selected', file.originFileObj)
  }
}

// 定义事件
const emit = defineEmits<{
  (e: 'file-selected', file: File): void
}>()

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

// 监听裁剪弹窗
watch(cropperVisible, async (visible) => {
  if (visible) {
    await nextTick()
  } else {
    previewImage.value = ''
  }
})

// 工具函数
const getBase64 = imageProcessor.getBase64

// 处理上传状态改变
const handleRemove = (file: any) => {
  const index = fileList.value.indexOf(file)
  const newFileList = fileList.value.slice()
  newFileList.splice(index, 1)
  fileList.value = newFileList
}
</script>

<style lang="less" scoped>
.Takt-image-upload {
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

  .Takt-cropper-container {
    .Takt-cropper-wrap {
      height: 400px;
      background-color: #f0f0f0;
      overflow: hidden;
      position: relative;
      
      img {
        max-width: 100%;
        max-height: 100%;
      }
    }

    .Takt-cropper-toolbar {
      margin-top: 16px;
      display: flex;
      justify-content: center;
    }
  }

  .Takt-upload-tip {
    margin-top: 8px;
    color: rgba(0, 0, 0, 0.45);
  }
}
</style>

