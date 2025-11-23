<template>
  <div class="Takt-images-cropper">
    <a-modal
      :open="visible"
      @update:open="(val:any) => emit('update:visible', val)"
      :title="title"
      :maskClosable="false"
      :width="800"
      :footer="null"
      @cancel="handleCancel"
    >
      <div class="cropper-container">
        <!-- 裁剪区域 -->
        <div class="cropper-wrapper">          
          <cropper-canvas 
            ref="cropperCanvasRef" 
            background
            theme-color="#1890ff"
          >
            <cropper-image 
              ref="cropperImageRef" 
              :src="imageUrl"
              translatable
              rotatable
              scalable
              skewable
              crossorigin="anonymous"
              @transform="onImageTransform"
              @load="onImageLoad"
              @error="onImageError"
            />
            <cropper-handle action="move" plain />
            <cropper-selection 
              ref="cropperSelectionRef"
              initial-coverage="0.5"
              movable 
              resizable
              :aspect-ratio="getConfig('aspectRatio')"
              :min-width="getConfig('minCropWidth')"
              :min-height="getConfig('minCropHeight')"
              :max-width="getConfig('maxCropWidth')"
              :max-height="getConfig('maxCropHeight')"
              outlined
              @change="onSelectionChange"
            >
              <cropper-grid role="grid" covered />
              <cropper-crosshair centered />
              <cropper-handle action="move" theme-color="rgba(255, 255, 255, 0.35)" />
              <cropper-handle action="n-resize" />
              <cropper-handle action="e-resize" />
              <cropper-handle action="s-resize" />
              <cropper-handle action="w-resize" />
              <cropper-handle action="ne-resize" />
              <cropper-handle action="nw-resize" />
              <cropper-handle action="se-resize" />
              <cropper-handle action="sw-resize" />
            </cropper-selection>
          </cropper-canvas>
        </div>

        <!-- 工具栏 -->
        <div class="cropper-toolbar">
          <a-space>
            <a-button @click="handleRotate(-15)">
              <template #icon><RotateLeftOutlined /></template>
              向左旋转
            </a-button>
            <a-button @click="handleRotate(15)">
              <template #icon><RotateRightOutlined /></template>
              向右旋转
            </a-button>
            <a-button @click="handleReset">
              <template #icon><UndoOutlined /></template>
              重置
            </a-button>
          </a-space>
        </div>

        <!-- 底部按钮 -->
        <div class="cropper-footer">
          <a-space>
            <a-button @click="handleCancel">取消</a-button>
            <a-button type="primary" @click="handleConfirm">确定</a-button>
          </a-space>
        </div>
      </div>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount, nextTick, watch } from 'vue'
import { message } from 'ant-design-vue'
import {
  RotateLeftOutlined,
  RotateRightOutlined,
  UndoOutlined
} from '@ant-design/icons-vue'

// 定义组件属性
const props = defineProps<{
  visible: boolean
  title: string
  imageUrl: string
  aspectRatio?: number
  initialCropSize?: number
  minCropWidth?: number
  minCropHeight?: number
  maxCropWidth?: number
  maxCropHeight?: number
}>()

// 定义组件事件
const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success', result: { blob: Blob; data: any; dataUrl: string }): void
  (e: 'error', error: any): void
  (e: 'cancel'): void
}>()

// 默认值
const defaultProps = {
  aspectRatio: 1,
  initialCropSize: 0.5,  // 改为官方标准的 0.5
  minCropWidth: 100,
  minCropHeight: 100,
  maxCropWidth: 800,
  maxCropHeight: 800
}

// 获取配置值
const getConfig = (key: keyof typeof defaultProps) => {
  return props[key] ?? defaultProps[key]
}

// 组件引用
const cropperCanvasRef = ref<any>(null)
const cropperImageRef = ref<any>(null)
const cropperSelectionRef = ref<any>(null)

// 组件状态
const isReady = ref(false)
const componentsReady = ref({
  canvas: false,
  image: false,
  selection: false
})

// 监听 visible 变化，当弹窗打开时初始化裁剪器
watch(() => props.visible, async (visible) => {
  if (visible) {
    console.log('ℹ 裁剪器弹窗打开，图片URL:', props.imageUrl)
    
    // 预加载图片确保可用
    const img = new Image()
    img.onload = () => {
      console.log('✓ 图片预加载成功，尺寸:', img.width, 'x', img.height)
      initializeCropper()
    }
    img.onerror = (error) => {
      console.error('✗ 图片预加载失败:', error)
    }
    img.src = props.imageUrl
    
    await nextTick()
    // 延迟一点时间确保 DOM 完全渲染
    setTimeout(() => {
      if (!img.complete) {
        console.log('⠋ 图片仍在加载中，等待完成...')
      }
    }, 100)
  } else {
    destroyCropper()
  }
})

// 图片加载成功事件回调
const onImageLoad = () => {
  console.log('✓ 图片加载成功')
  
  // 检查图片是否真的加载到了 cropper-image 中
  const imageElement = cropperImageRef.value?.querySelector('img')
  if (imageElement) {
    console.log('ℹ cropper-image 中的图片元素:', imageElement)
    console.log('ℹ 图片尺寸:', imageElement.naturalWidth, 'x', imageElement.naturalHeight)
    console.log('✓ 图片是否完成加载:', imageElement.complete)
  } else {
    console.log('⚠ 未找到 cropper-image 中的图片元素')
  }
  
  // 图片加载成功后，标记为就绪
  if (!componentsReady.value.image) {
    componentsReady.value.image = true
    console.log('✓ 图片组件已标记为就绪')
    
    // 检查其他组件是否也准备就绪
    if (componentsReady.value.canvas && componentsReady.value.selection) {
      isReady.value = true
      console.log('✓ 所有组件已就绪，开始居中操作')
      centerImageAndSelection()
    } else {
      console.log('⠋ 等待其他组件就绪...', {
        canvas: componentsReady.value.canvas,
        image: componentsReady.value.image,
        selection: componentsReady.value.selection
      })
    }
  }
}

// 图片变换事件回调
const onImageTransform = (event: any) => {
  console.log('ℹ 图片变换事件:', event)
  // 当图片有变换时，标记为就绪
  if (!componentsReady.value.image) {
    componentsReady.value.image = true
    console.log('✓ 图片组件通过变换事件标记为就绪')
    
    if (componentsReady.value.canvas && componentsReady.value.selection) {
      isReady.value = true
      console.log('✓ 所有组件已就绪，开始居中操作')
      centerImageAndSelection()
    }
  }
}

// 裁剪框变化事件回调
const onSelectionChange = (event: any) => {
  console.log('ℹ 裁剪框变化事件:', event)
  // 当裁剪框有变化时，标记为就绪
  if (!componentsReady.value.selection) {
    componentsReady.value.selection = true
    console.log('✓ 裁剪框组件通过变化事件标记为就绪')
    
    if (componentsReady.value.canvas && componentsReady.value.image) {
      isReady.value = true
      console.log('✓ 所有组件已就绪，开始居中操作')
      centerImageAndSelection()
    }
  }
}

// 图片加载失败事件回调
const onImageError = (event: any) => {
  console.error('✗ 图片加载失败:', event)
  message.error('图片加载失败，请检查图片URL或网络连接')
}

// 初始化裁剪器
const initializeCropper = async () => {
  try {
    await nextTick()
    
    console.log('🚀 开始初始化裁剪器...')
    
    // 简化初始化逻辑，直接标记组件为就绪
    setTimeout(() => {
      componentsReady.value = {
        canvas: true,
        image: true,
        selection: true
      }
      isReady.value = true
      console.log('✅ 裁剪器初始化完成')
      centerImageAndSelection()
    }, 500)
    
  } catch (error) {
    console.error('❌ 初始化裁剪器失败:', error)
  }
}

// 居中图片和裁剪框
const centerImageAndSelection = () => {
  try {
    if (!isReady.value) {
      console.warn('⚠️  裁剪器未就绪，无法居中')
      return
    }

    console.log('🎯 开始居中图片和裁剪框...')
    
    // 获取组件引用
    const image = cropperImageRef.value
    const selection = cropperSelectionRef.value
    
    if (!image || !selection) {
      console.warn('⚠️  组件引用不完整，无法居中')
      return
    }

    // 居中图片 - 使用官方推荐的 contain 模式
    if (image.$center) {
      image.$center('contain')
      console.log('✅ 图片已居中（contain模式）')
    }
    
    // 居中裁剪框
    if (selection.$center) {
      selection.$center()
      console.log('✅ 裁剪框已居中')
    }
    
    console.log('🎯 居中操作完成')
    
  } catch (error) {
    console.error('❌ 居中操作失败:', error)
  }
}

// 旋转
const handleRotate = (degree: number) => {
  try {
    if (!isReady.value || !cropperImageRef.value) {
      message.warning('裁剪器未就绪，请稍后再试')
      return
    }

    if (cropperImageRef.value.$rotate) {
      cropperImageRef.value.$rotate(degree)
      console.log(`图片已旋转 ${degree} 度`)
    } else {
      console.warn('旋转方法不可用')
      message.warning('旋转功能不可用')
    }
  } catch (error) {
    console.error('旋转失败:', error)
    message.error('旋转失败，请重试')
  }
}

// 重置
const handleReset = () => {
  try {
    if (!isReady.value) {
      message.warning('裁剪器未就绪，请稍后再试')
      return
    }

    console.log('开始重置裁剪器...')
    
    // 重置图片
    if (cropperImageRef.value) {
      if (cropperImageRef.value.$reset) {
        cropperImageRef.value.$reset()
        console.log('图片已重置')
      }
      if (cropperImageRef.value.$center) {
        cropperImageRef.value.$center('contain')
        console.log('图片已居中（contain模式）')
      }
    }
    
    // 重置裁剪框
    if (cropperSelectionRef.value) {
      if (cropperSelectionRef.value.$reset) {
        cropperSelectionRef.value.$reset()
        console.log('裁剪框已重置')
      }
      if (cropperSelectionRef.value.$center) {
        cropperSelectionRef.value.$center()
        console.log('裁剪框已居中')
      }
    }
    
    console.log('重置操作完成')
    message.success('已重置到初始状态')
    
  } catch (error) {
    console.error('重置失败:', error)
    message.error('重置失败，请重试')
  }
}

// 确认裁剪
const handleConfirm = async () => {
  try {
    if (!isReady.value || !cropperSelectionRef.value) {
      console.error('裁剪器未就绪')
      message.error('裁剪器未就绪，请稍后再试')
      return
    }

    const canvas = await cropperSelectionRef.value.$toCanvas()
    if (!canvas) {
      console.error('无法生成画布')
      message.error('裁剪失败，无法生成画布')
      return
    }

    canvas.toBlob((blob: Blob | null) => {
      if (!blob) {
        console.error('无法生成 Blob 数据')
        message.error('裁剪失败，无法生成图片数据')
        return
      }

      emit('success', {
        blob,
        data: null,
        dataUrl: canvas.toDataURL('image/jpeg', 0.9)
      })
      emit('update:visible', false)
    }, 'image/jpeg', 0.9)
  } catch (error) {
    console.error('裁剪失败:', error)
    message.error('裁剪失败，请重试')
    emit('error', error)
  }
}

// 取消
const handleCancel = () => {
  emit('cancel')
  emit('update:visible', false)
}

// 销毁裁剪器
const destroyCropper = () => {
  // Web Components 会自动清理，无需手动销毁
  isReady.value = false
  componentsReady.value = {
    canvas: false,
    image: false,
    selection: false
  }
  console.log('裁剪器状态已重置')
}

// 组件挂载后
onMounted(async () => {
  console.log('Cropper 组件挂载完成')
  
  // 检查 Web Components 是否正确注册
  await nextTick()
  
  console.log('检查 Web Components 注册状态:')
  console.log('cropper-canvas 已定义:', customElements.get('cropper-canvas'))
  console.log('cropper-image 已定义:', customElements.get('cropper-image'))
  console.log('cropper-selection 已定义:', customElements.get('cropper-selection'))
})

// 组件卸载前清理
onBeforeUnmount(() => {
  console.log('Cropper 组件即将卸载')
  destroyCropper()
})
</script>

<style scoped>
.Takt-images-cropper {
  position: relative;
}

.cropper-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.cropper-wrapper {
  position: relative;
  width: 100%;
  height: 600px;
  overflow: hidden;
  border: 1px solid #d9d9d9;
  border-radius: 6px;
  background-color: #f0f0f0;
}

/* Web Components 样式 */
:deep(cropper-canvas) {
  width: 100%;
  height: 100%;
}

:deep(cropper-image) {
  width: 100%;
  height: 100%;
  user-select: none;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
}

/* 移除所有自定义样式，使用官方默认样式 */

.cropper-toolbar {
  display: flex;
  justify-content: center;
  gap: 8px;
}

.cropper-footer {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  margin-top: 16px;
}
</style>

