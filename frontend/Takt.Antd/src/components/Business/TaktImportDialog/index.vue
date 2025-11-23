<!--
===================================================================
项目名 : Lean.Takt
文件名 : ImportDialog/index.vue
创建者 : Claude
创建时间: 2024-03-27
版本号 : v1.0.0
描述    : 通用导入对话框组件

使用示例:
<Takt-import-dialog
  v-model:open="importVisible"
  :upload-method="handleImportUpload"
  :template-method="handleDownloadTemplate"
  template-file-name="用户导入模板.xlsx"
  :tips="'请确保Excel文件包含必要的用户信息字段,\n如用户名,昵称,邮箱,手机号,性别,用户类型,状态'"
  :show-template="true"
  :template-permission="['identity:user:import:template']"
  @success="handleImportSuccess"
/>

换行支持:
1. 使用 \n 换行: "第一行\n第二行"
2. 使用 \\n 转义换行: "第一行\\n第二行"  
3. 使用 <br> 标签: "第一行<br>第二行"
4. 使用 <br/> 标签: "第一行<br/>第二行"
===================================================================
-->

<template>
  <a-modal
    :open="visible"
    :title="t('common.import.title')"
    :width="700"
    :footer="null"
    :maskClosable="false"
    :draggable="true"
    :confirm-loading="loading"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
      <!-- 文件上传 -->
      <a-form-item :label="t('common.import.file')">
        <a-upload-dragger
          :accept="accept"
          :show-upload-list="true"
          :before-upload="handleBeforeUpload"
          :customRequest="handleCustomRequest"
          :name="'file'"
          :maxCount="1"
          @change="handleChange"
        >
          <p class="ant-upload-drag-icon">
            <upload-outlined />
          </p>
          <p class="ant-upload-text">{{ t('common.import.dragText') }}</p>
          <p class="ant-upload-hint">{{ t('common.import.dragHint') }}</p>
        </a-upload-dragger>
      </a-form-item>

      <!-- 下载模板 -->
      <a-form-item :label="t('common.import.template')" v-if="templateFileName">
        <div v-if="props.showTemplate" v-hasPermi="props.templatePermission">
          <a-button 
            class="Takt-btn-import"
            size="small"
            :loading="loading"
            @click="handleDownloadTemplate"
          >
            <template #icon>
              <download-outlined />
            </template>
            {{ t('common.import.download') }}
          </a-button>
        </div>
      </a-form-item>

      <!-- 导入说明 -->
      <a-form-item :label="t('common.import.note')" v-if="tips || sheetName">
        <a-alert
          v-if="tips"
          type="info"
          show-icon
        >
          <template #message>
            <div v-html="formatTips(tips)"></div>
          </template>
        </a-alert>
        <div class="mt-2" v-if="sheetName">
          <a-alert
            :message="t('common.import.sheetName', { sheetName })"
            type="warning"
            show-icon
          />
        </div>
        <!-- 导入限制 -->
        <div class="mt-2">
          <a-alert
            :message="t('common.import.limits.title')"
            type="warning"
            show-icon
          >
            <template #description>
              <div>
                <div>{{ t('common.import.limits.fileCount', { count: 1 }) }}</div>
                <div>{{ t('common.import.limits.fileSize', { size: props.maxSize }) }}</div>
                <div>{{ t('common.import.limits.recordCount', { count: props.maxRecords }) }}</div>
                <div>{{ t('common.import.limits.fileFormat') }}</div>
              </div>
            </template>
          </a-alert>
        </div>
      </a-form-item>
    </a-form>

    <!-- 导入结果 -->
    <template v-if="importResult">
      <a-divider />
      <a-descriptions :column="1">
        <a-descriptions-item :label="t('common.import.total')">
          {{ importResult.total }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.success')">
          {{ importResult.success }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.failed')">
          {{ importResult.failed }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.message')" v-if="importResult.message">
          {{ importResult.message }}
        </a-descriptions-item>
      </a-descriptions>
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { UploadOutlined, DownloadOutlined } from '@ant-design/icons-vue'
import { getToken } from '@/utils/authUtil'
import type { UploadChangeParam } from 'ant-design-vue'
import request from '@/utils/request'

interface ImportResult {
  total: number
  success: number
  failed: number
  message?: string
}

interface Props {
  open: boolean
  /** 上传方法 */
  uploadMethod: (file: File, onUploadProgress?: (progressEvent: any) => void) => Promise<any>
  /** 模板下载方法 */
  templateMethod: () => Promise<Blob>
  /** 模板文件名 */
  templateFileName?: string
  /** 接受的文件类型 */
  accept?: string
  /** Excel工作表名称 */
  sheetName?: string
  /** 文件大小限制(MB) */
  maxSize?: number
  /** 记录数限制 */
  maxRecords?: number
  /** 导入说明 */
  tips?: string
  /** 是否自动关闭 */
  autoClose?: boolean
  /** 是否显示模板下载按钮 */
  showTemplate?: boolean
  /** 模板下载权限数组，为空数组时不显示下载按钮 */
  templatePermission?: string[]
}

const props = withDefaults(defineProps<Props>(), {
  accept: '.xlsx',
  maxSize: 10,
  maxRecords: 1000,
  autoClose: true,
  showTemplate: true,
  templatePermission: () => []
})

const emit = defineEmits(['update:open'])

const visible = computed(() => props.open)

const { t } = useI18n()

// 加载状态
const loading = ref(false)

// 导入结果
const importResult = ref<ImportResult>()

// 上传文件
const uploadFile = ref<File>()

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:open', val)
  if (!val) {
    uploadFile.value = undefined
    importResult.value = undefined
  }
}

// 上传状态改变
const handleChange = (info: UploadChangeParam) => {
  if (info.file.status === 'uploading') {
    loading.value = true
  } else if (info.file.status === 'done') {
    loading.value = false
    const response = info.file.response
    if (response.code === 200) {
      const success = response.data.success || 0
      const fail = response.data.fail || 0
      
      importResult.value = {
        total: success + fail,
        success,
        failed: fail,
        message: response.msg
      }
      
      // 检查记录数限制
      if (success + fail > props.maxRecords) {
        message.warning(t('common.import.recordLimit', { max: props.maxRecords, actual: success + fail }))
      }
      
      console.log('importResult.value:', importResult.value.success)
      // 根据导入结果显示不同的提示信息
      if (success > 0 && fail === 0) {
        message.success(t('common.import.allSuccess', { count: success }))
      } else if (success > 0 && fail > 0) {
        message.warning(t('common.import.partialSuccess', { success, fail }))
      } else if (success === 0 && fail > 0) {
        message.error(t('common.import.allFailed', { count: fail }))
      } else {
        message.info(t('common.import.noData'))
      }
      
      if (props.autoClose) {
        handleVisibleChange(false)
      }
    } else {
      message.error(response.msg || t('common.import.importFailed'))
    }
  } else if (info.file.status === 'error') {
    loading.value = false
    message.error(info.file.response?.msg || t('common.import.importFailed'))
  }
}

// 自定义上传请求
const handleCustomRequest = async (options: any) => {
  const { file, onProgress, onSuccess, onError } = options
  
  try {
    // 设置上传进度
    const onUploadProgress = (progressEvent: any) => {
      if (progressEvent.total) {
        const percent = Math.round((progressEvent.loaded * 100) / progressEvent.total)
        onProgress({ percent })
      }
    }

    console.log('[上传文件] 开始上传:', {
      name: file.name,
      size: file.size,
      type: file.type
    })

    // 使用props传入的uploadMethod处理文件上传
    const response = await props.uploadMethod(file, onUploadProgress)
    
    console.log('[上传文件] 上传成功:', response)
    
    // 处理响应
    if (response?.code === 200) {
      onSuccess(response)
    } else {
      onError(new Error(response?.msg || t('common.import.importFailed')))
    }
  } catch (error: any) {
    console.error('[上传文件] 上传失败:', error)
    onError(error)
  }
}

// 上传前校验
const handleBeforeUpload = (file: File) => {
  // 检查文件对象和内容
  if (!file || file.size === 0) {
    message.error(t('common.import.empty'))
    return false
  }

  // 检查文件类型
  const isExcel = file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  if (!isExcel) {
    message.error(t('common.import.format'))
    return false
  }

  // 检查文件大小
  const isLtMaxSize = file.size / 1024 / 1024 < props.maxSize
  if (!isLtMaxSize) {
    message.error(t('common.import.size', { size: props.maxSize }))
    return false
  }

  return true
}

// 下载模板
const handleDownloadTemplate = async () => {
  try {
    loading.value = true
    const blob = await props.templateMethod()
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = props.templateFileName || t('common.import.templateFileName', { time: new Date().getTime() })
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.message.downloadSuccess'))
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error(t('common.message.downloadFailed'))
  } finally {
    loading.value = false
  }
}

// 提交
const handleSubmit = async () => {
  if (!uploadFile.value) {
    message.warning(t('common.import.select'))
    return
  }
  
  try {
    loading.value = true
    console.log('开始提交文件:', {
      name: uploadFile.value.name,
      size: uploadFile.value.size,
      type: uploadFile.value.type
    })
    
    // 使用props传入的uploadMethod处理文件上传
    const response = await props.uploadMethod(uploadFile.value)
    console.log('提交成功:', response)
    
    // 处理响应
    if (response.code === 200) {
      const success = response.data.success || 0
      const fail = response.data.fail || 0
      
      importResult.value = {
        total: success + fail,
        success,
        failed: fail,
        message: response.msg
      }
      
      // 检查记录数限制
      if (success + fail > props.maxRecords) {
        message.warning(t('common.import.recordLimit', { max: props.maxRecords, actual: success + fail }))
      }
      
      // 根据导入结果显示不同的提示信息
      if (success > 0 && fail === 0) {
        message.success(t('common.import.allSuccess', { count: success }))
      } else if (success > 0 && fail > 0) {
        message.warning(t('common.import.partialSuccess', { success, fail }))
      } else if (success === 0 && fail > 0) {
        message.error(t('common.import.allFailed', { count: fail }))
      } else {
        message.info(t('common.import.noData'))
      }
      
      if (props.autoClose) {
        handleVisibleChange(false)
      }
    } else {
      message.error(response.msg || t('common.import.importFailed'))
    }
  } catch (error: any) {
    console.error('提交失败:', error)
    message.error(error.response?.data?.msg || error.message || t('common.import.importFailed'))
  } finally {
    loading.value = false
  }
}

// 格式化导入说明
const formatTips = (tips: string | undefined) => {
  if (!tips) return ''
  
  // 处理多种换行方式
  return tips
    .replace(/\\n/g, '<br>')  // 处理转义的换行符
    .replace(/\n/g, '<br>')   // 处理普通换行符
    .replace(/<br\s*\/?>/gi, '<br>')  // 统一br标签格式
}
</script>

<style lang="less" scoped>
:deep(.ant-upload-list) {
  max-height: 300px;
  overflow-y: auto;
}

:deep(.ant-descriptions-item-label) {
  width: 100px;
}

// 确保表单标签有足够的宽度
:deep(.ant-form-item-label) {
  min-width: 140px;
  text-align: right;
  padding-right: 12px;
  flex-shrink: 0; // 防止标签被压缩
  
  // 对于长标签文本，确保不被截断
  .ant-form-item-required {
    white-space: nowrap;
    overflow: visible; // 允许文本溢出显示
    text-overflow: clip; // 不显示省略号
  }
  
  // 确保标签文本完整显示
  label {
    white-space: nowrap;
    overflow: visible;
    text-overflow: clip;
    font-weight: 500;
  }
}

// 确保表单内容区域有足够的空间
:deep(.ant-form-item-control) {
  flex: 1;
  min-width: 0; // 允许内容区域收缩
  margin-left: 0; // 移除默认的左边距
}

// 优化表单行布局
:deep(.ant-form-item) {
  margin-bottom: 24px;
  
  &:last-child {
    margin-bottom: 0;
  }
}

// 优化上传区域样式
:deep(.ant-upload-dragger) {
  width: 100%;
  min-height: 120px;
}

// 优化警告框样式
:deep(.ant-alert) {
  margin-bottom: 8px;
  
  &:last-child {
    margin-bottom: 0;
  }
  
  // 确保tips中的换行正确显示
  .ant-alert-message {
    white-space: pre-line;
    line-height: 1.6;
    
    // 支持HTML内容的换行
    div {
      white-space: pre-line;
      line-height: 1.6;
    }
  }
}

// 优化下载模板按钮样式
:deep(.ant-btn) {
  display: inline-flex;
  align-items: center;
  gap: 4px;
  
  .anticon {
    font-size: 14px;
  }
}

// 确保模态框内容有足够的空间
:deep(.ant-modal-body) {
  padding: 24px;
  max-height: 70vh;
  overflow-y: auto;
}

// 优化描述列表样式
:deep(.ant-descriptions) {
  .ant-descriptions-item-label {
    font-weight: 500;
    color: rgba(0, 0, 0, 0.85);
  }
  
  .ant-descriptions-item-content {
    color: rgba(0, 0, 0, 0.65);
  }
}

// 响应式设计：在小屏幕上调整布局
@media (max-width: 768px) {
  :deep(.ant-form-item-label) {
    min-width: 100px;
    text-align: left;
    padding-right: 0;
    padding-bottom: 4px;
  }
  
  :deep(.ant-form-item-control) {
    margin-left: 0;
  }
}

// 支持从右到左的语言（如阿拉伯语）
[dir="rtl"] {
  :deep(.ant-form-item-label) {
    text-align: right;
    padding-right: 0;
    padding-left: 12px;
  }
  
  :deep(.ant-form-item-control) {
    margin-right: 0;
    margin-left: 0;
  }
  
  :deep(.ant-upload-dragger) {
    text-align: right;
  }
  
  :deep(.ant-alert) {
    text-align: right;
  }
}
</style> 
