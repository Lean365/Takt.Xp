<template>
  <Takt-modal v-model:open="visible" :title="title" :width="1800" :loading="loading" :fullscreen="isFullscreen"
    @update:open="handleVisibleChange" @ok="handleSubmit">
    <template #extra>
      <a-button type="text" @click="toggleFullscreen">
        <template #icon>
          <component :is="isFullscreen ? 'FullscreenExitOutlined' : 'FullscreenOutlined'" />
        </template>
      </a-button>
    </template>

    <!-- 步骤条 -->
    <a-steps :current="currentStep" class="form-steps">
      <a-step title="基本信息" description="填写工作流基本信息" />
      <a-step title="选择表单" description="选择关联的表单" />
      <a-step title="流程设计" description="设计工作流程图" />
    </a-steps>

    <!-- 步骤内容 -->
    <div class="step-content">
      <!-- 第一步：基本信息 -->
      <div v-show="currentStep === 0" class="step-panel">
        <a-form ref="formRef" :model="formState" :rules="rules" :label-col="{ span: 4 }" :wrapper-col="{ span: 20 }">
          <a-form-item label="流程定义键" name="schemeKey">
            <a-input v-model:value="formState.schemeKey" placeholder="请输入流程定义键" />
          </a-form-item>
          <a-form-item label="流程定义名称" name="schemeName">
            <a-input v-model:value="formState.schemeName" placeholder="请输入流程定义名称" />
          </a-form-item>
          <a-form-item label="流程分类" name="schemeCategory">
            <Takt-select v-model:value="formState.schemeCategory" dict-type="workflow_scheme_category" type="select"
              :show-all="false" :placeholder="t('generator.config.placeholder.workflowCategory')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="版本" name="version">
            <Takt-select v-model:value="formState.version" dict-type="workflow_version" type="select" :show-all="false"
              :placeholder="t('generator.config.placeholder.workflowVersion')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="状态" name="status">
            <Takt-select v-model:value="formState.status" dict-type="wf_scheme_status" type="select" :show-all="false"
              :placeholder="t('generator.config.placeholder.status')" style="width: 100%" />
          </a-form-item>
          <a-form-item label="注意事项" name="notes">
            <a-textarea v-model:value="formState.notes" placeholder="请输入注意事项" :rows="4" />
          </a-form-item>
          <a-form-item label="备注" name="remark">
            <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="4" />
          </a-form-item>
        </a-form>
      </div>

      <!-- 第二步：选择表单 -->
      <div v-show="currentStep === 1" class="step-panel">
        <!-- 编辑模式：显示只读表单信息 -->
        <div v-if="isEditMode" class="edit-mode-form-panel">
          <a-alert message="编辑模式" description="当前为编辑模式，表单为只读状态。如需修改表单，请前往表单管理页面进行修改。" type="info" show-icon
            :closable="false" style="margin-bottom: 16px;" />
          <div class="current-form-display">
            <h4>当前表单</h4>
            <div class="form-info">
              <p><strong>表单名称：</strong>{{ getCurrentFormName() }}</p>
              <p><strong>表单ID：</strong>{{ formState.formId }}</p>
            </div>
            <div class="form-preview">
              <h5>表单预览</h5>
              <div
                v-if="selectedFormConfig && formPreviewConfig && formPreviewConfig.rule && formPreviewConfig.rule.length > 0">
                <Takt-form :model-value="formPreviewConfig" :readonly="true" />
              </div>
              <div v-else class="no-form-preview">
                <a-empty description="暂无表单预览" />
              </div>
            </div>
          </div>
        </div>

        <!-- 新建模式：表单选择 -->
        <div v-else class="form-selection-container">
          <!-- 左侧表单列表 -->
          <div class="form-list-panel">
            <h4>可用表单列表</h4>
            <div class="form-list">
              <div v-for="form in formOptions" :key="form.value" class="form-item"
                :class="{ active: formState.formId === form.value }" @click="selectForm(form.value)">
                <div class="form-item-content">
                  <div class="form-name">{{ form.label }}</div>
                  <div class="form-id">ID: {{ form.value }}</div>
                </div>
              </div>
            </div>
          </div>

          <!-- 右侧表单预览 -->
          <div class="form-preview-panel">
            <div class="preview-header">
              <h4>表单预览</h4>
              <a-button v-if="formState.formId" type="primary" size="small" @click="confirmFormSelection">
                确认选择
              </a-button>
            </div>
            <div class="preview-content">
              <div v-if="!formState.formId" class="no-selection">
                <a-empty description="请从左侧选择一个表单进行预览" />
              </div>
              <div v-else-if="loadingForm" class="loading-form">
                <a-spin tip="加载表单配置中..." />
              </div>
              <div v-else class="form-preview-wrapper">
                <div
                  v-if="selectedFormConfig && formPreviewConfig && formPreviewConfig.rule && formPreviewConfig.rule.length > 0">
                  <Takt-form :model-value="formPreviewConfig" :readonly="true" />
                </div>
                <div v-else class="no-form-preview">
                  <a-empty description="暂无表单预览" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- 第三步：流程设计 -->
      <div v-show="currentStep === 2" class="step-panel">
        <div class="flow-canvas-wrapper">
          <Takt-jsplumb-flow :key="flowDesignerKey" v-model:value="schemeConfigObject" :width="1600" :height="800"
            :readonly="false" />
        </div>
      </div>
    </div>

    <!-- 步骤操作按钮 -->
    <template #footer>
      <div class="step-actions">
        <a-button v-if="currentStep > 0" @click="prevStep">
          上一步
        </a-button>
        <a-button v-if="currentStep < 2" type="primary" @click="nextStep">
          下一步
        </a-button>
        <a-button v-if="currentStep === 2" type="primary" @click="handleSubmit" :loading="loading">
          保存
        </a-button>
        <a-button @click="handleVisibleChange(false)">
          取消
        </a-button>
      </div>
    </template>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import { getSchemeById, createScheme, updateScheme } from '@/api/workflow/scheme'
import { getFormById } from '@/api/workflow/form'
import type { TaktScheme, TaktSchemeCreate, TaktSchemeUpdate } from '@/types/workflow/scheme'




const props = defineProps<{
  open: boolean
  title: string
  schemeId?: string
  isClone?: boolean
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

// 判断是否为编辑模式
const isEditMode = computed(() => !!props.schemeId)

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)
const formOptions = ref<{ label: string; value: string }[]>([])
const currentStep = ref(0)
const isFullscreen = ref(false)
const loadingForm = ref(false)
const selectedFormConfig = ref('')
const formPreviewData = ref<any>({})
const formPreviewConfig = ref<any>({})
const flowDesignerKey = ref(0) // 用于强制流程设计器重新渲染

// 重置表单
const resetForm = () => {
  formState.schemeKey = ''
  formState.schemeName = ''
  formState.schemeCategory = 1
  formState.version = '1.0'
  formState.formId = '0'
  formState.schemeConfig = ''
  formState.status = 0
  formState.notes = ''
  formState.remark = ''
  currentStep.value = 0
  selectedFormConfig.value = ''
  formPreviewData.value = {}
  formPreviewConfig.value = {}
  formRef.value?.resetFields()
  // 强制流程设计器重新渲染
  flowDesignerKey.value++
}

const formState = reactive<TaktSchemeCreate & { status: number }>({
  schemeKey: '',
  schemeName: '',
  schemeCategory: 1,
  version: '1.0',
  schemeConfig: '',
  formId: '0',
  status: 0,
  notes: '',
  remark: ''
})

const rules: Record<string, any[]> = {
  schemeKey: [
    { required: true, message: '请输入流程定义键', trigger: 'blur' }
  ],
  schemeName: [
    { required: true, message: '请输入流程定义名称', trigger: 'blur' }
  ],
  schemeCategory: [
    { required: true, message: '请选择流程分类', trigger: 'change' }
  ],
  status: [
    { required: true, message: '请选择状态', trigger: 'change' }
  ],
  notes: [
    { required: true, message: '请输入注意事项', trigger: 'blur' }
  ]
}

// 计算属性：将schemeConfig字符串转换为对象供TaktFlow组件使用
const schemeConfigObject = computed({
  get: () => {
    console.log('[SchemeForm] 计算属性get被调用, formState.schemeConfig:', formState.schemeConfig)
    if (!formState.schemeConfig) {
      console.log('[SchemeForm] schemeConfig为空，返回默认值')
      return { nodes: [], edges: [] }
    }
    try {
      const result = typeof formState.schemeConfig === 'string'
        ? JSON.parse(formState.schemeConfig)
        : formState.schemeConfig
      console.log('[SchemeForm] 解析成功，结果:', result)
      return result
    } catch (error) {
      console.error('[SchemeForm] 解析schemeConfig失败:', error)
      return { nodes: [], edges: [] }
    }
  },
  set: (value) => {
    console.log('[SchemeForm] 计算属性set被调用, 接收到的值:', value)

    // 现在Takt-flow组件已经导出简洁格式，直接使用
    const workflowData = {
      nodes: value?.nodes || [],
      edges: value?.edges || []
    }

    // 防止无限循环：只有当值真正改变时才更新
    const newValue = JSON.stringify(workflowData)
    console.log('[SchemeForm] 工作流数据:', workflowData)
    console.log('[SchemeForm] 序列化后的值:', newValue)
    console.log('[SchemeForm] 当前formState.schemeConfig:', formState.schemeConfig)

    if (newValue !== formState.schemeConfig) {
      console.log('[SchemeForm] 值发生变化，更新schemeConfig')
      formState.schemeConfig = newValue
      console.log('[SchemeForm] 更新后的formState.schemeConfig:', formState.schemeConfig)
    } else {
      console.log('[SchemeForm] 值未改变，跳过更新')
    }
  }
})

// 下一步
const nextStep = async () => {
  try {
    if (currentStep.value === 0) {
      await formRef.value?.validate()
    } else if (currentStep.value === 1) {
      // 检查是否选择了表单
      if (!formState.formId) {
        message.error('请选择一个表单')
        return
      }
    }
    currentStep.value++
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

// 上一步
const prevStep = () => {
  currentStep.value--
}

// 获取工作流定义详情
const getDetail = async (id: string) => {
  if (!id) {
    console.log('[SchemeForm] 没有schemeId，跳过加载')
    return
  }

  console.log('[SchemeForm] 开始加载工作流详情, schemeId:', id)
  try {
    const res = await getSchemeById(id)
    if (res.data) {
      const data = res.data
      console.log('[SchemeForm] 获取到工作流数据:', data)

      // 先重置表单，再设置新数据
      resetForm()

      formState.schemeKey = data.schemeKey || ''
      formState.schemeName = data.schemeName || ''
      formState.schemeCategory = data.schemeCategory || 1
      formState.version = data.version || '1.0'
      formState.formId = data.formId || '0'
      formState.schemeConfig = data.schemeConfig || ''
      formState.status = data.status || 0
      formState.notes = data.notes || ''
      formState.remark = data.remark || ''

      console.log('[SchemeForm] 工作流数据设置完成:', formState)

      // 如果有表单ID，加载表单配置
      if (data.formId && data.formId !== '0') {
        console.log('[SchemeForm] 开始加载表单配置, formId:', data.formId)
        await loadFormConfig(data.formId)
      }
    } else {
      console.error('[SchemeForm] 获取工作流数据失败')
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('[SchemeForm] 获取工作流数据异常:', error)
    message.error(t('common.failed'))
  }
}

// 获取工作流定义详情用于克隆
const getDetailForClone = async (id: string) => {
  if (!id) {
    console.log('[SchemeForm] 没有schemeId，跳过加载')
    return
  }

  console.log('[SchemeForm] 开始加载工作流详情用于克隆, schemeId:', id)
  try {
    const res = await getSchemeById(id)
    if (res.data) {
      const data = res.data
      console.log('[SchemeForm] 获取到工作流数据:', data)

      // 先重置表单，再设置新数据
      resetForm()

      // 克隆时清空关键字段，让用户重新输入
      formState.schemeKey = (data.schemeKey || '') + '_copy' // 添加副本标识
      formState.schemeName = (data.schemeName || '') + ' - 副本' // 添加副本标识
      formState.schemeCategory = data.schemeCategory || 1
      formState.version = '1.0' // 重置版本
      formState.formId = data.formId || '0'
      formState.schemeConfig = data.schemeConfig || ''
      formState.status = 0 // 重置状态为草稿
      formState.notes = data.notes || ''
      formState.remark = data.remark || ''

      console.log('[SchemeForm] 克隆数据设置完成:', formState)

      // 如果有表单ID，加载表单配置
      if (data.formId && data.formId !== '0') {
        console.log('[SchemeForm] 开始加载表单配置, formId:', data.formId)
        await loadFormConfig(data.formId)
      }
    } else {
      console.error('[SchemeForm] 获取工作流数据失败')
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('[SchemeForm] 获取工作流数据异常:', error)
    message.error(t('common.failed'))
  }
}

// 获取表单选项
const fetchFormOptions = async () => {
  try {
    // 暂时使用模拟数据，后续需要实现表单选项API
    formOptions.value = [
      { label: '请假申请表', value: '1' },
      { label: '报销申请表', value: '2' },
      { label: '采购申请表', value: '3' }
    ]
  } catch (error) {
    console.error('获取表单选项失败:', error)
    message.error(t('common.failed'))
  }
}

// 监听表单打开状态
watch(() => props.open, (newVal) => {
  if (newVal) {
    // 对话框打开时，先重置表单
    resetForm()

    fetchFormOptions()
    currentStep.value = 0

    // 然后根据情况加载数据
    if (props.schemeId) {
      if (props.isClone) {
        // 克隆模式：加载源数据但清空ID
        getDetailForClone(props.schemeId)
      } else {
        // 编辑模式：正常加载数据
        getDetail(props.schemeId)
      }
    }
    // 如果没有schemeId，表单已经通过resetForm()重置了
  }
}, { immediate: true })

// 监听工作流定义ID变化
watch(() => props.schemeId, (newVal, oldVal) => {
  // 只有在表单打开时才处理ID变化
  if (props.open) {
    if (newVal) {
      if (props.isClone) {
        // 克隆模式：加载源数据但清空ID
        getDetailForClone(newVal)
      } else {
        // 编辑模式：正常加载数据
        getDetail(newVal)
      }
    } else if (oldVal && !newVal) {
      // 当schemeId从有值变为undefined时，说明切换到了创建模式，需要重置表单
      resetForm()
    }
  }
})

// 监听 schemeConfig 变化
watch(() => formState.schemeConfig, (newVal) => {
  console.log('schemeConfig 发生变化:', newVal)
}, { deep: true })

// 获取当前表单名称
const getCurrentFormName = () => {
  const form = formOptions.value.find(f => f.value === formState.formId)
  return form ? form.label : `表单 ${formState.formId}`
}

// 切换全屏
const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
}

// 提交表单
const handleSubmit = async () => {
  try {
    loading.value = true
    console.log('[SchemeForm] 开始提交表单')
    console.log('[SchemeForm] 提交前的 formState:', formState)
    console.log('[SchemeForm] 提交前的 schemeConfig:', formState.schemeConfig)
    console.log('[SchemeForm] 提交前的 schemeConfigObject:', schemeConfigObject.value)

    // 保证 schemeConfig 为字符串
    let schemeConfigStr = formState.schemeConfig
    if (typeof schemeConfigStr !== 'string') {
      schemeConfigStr = JSON.stringify(schemeConfigStr)
    }
    console.log('[SchemeForm] 转换后的 schemeConfig:', schemeConfigStr)

    const data: TaktSchemeCreate = {
      schemeKey: formState.schemeKey,
      schemeName: formState.schemeName,
      schemeCategory: formState.schemeCategory,
      version: formState.version,
      formId: formState.formId,
      schemeConfig: schemeConfigStr,
      status: formState.status,
      notes: formState.notes,
      remark: formState.remark
    }
    console.log('最终提交的数据:', data)

    let res
    if (props.schemeId && !props.isClone) {
      // 只有在编辑模式（非克隆）时才更新
      const updateData: TaktSchemeUpdate = {
        ...data,
        schemeId: props.schemeId
      }
      res = await updateScheme(updateData)
    } else {
      // 新建模式或克隆模式都创建新记录
      res = await createScheme(data)
    }
    // 检查返回结果
    if (res.code === 200) {
      message.success(t('common.success'))
      resetForm()
      emit('success')
      handleVisibleChange(false)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
  if (!value) {
    resetForm()
  }
}

// 选择表单
const selectForm = async (formId: string) => {
  formState.formId = formId
  await loadFormConfig(formId)
}

// 加载表单配置
const loadFormConfig = async (formId: string) => {
  if (!formId) return

  loadingForm.value = true
  try {
    // 调用API获取表单配置
    const res = await getFormById(formId)
    if (res.data) {
      const formData = res.data
      console.log('表单数据:', formData)

      // 确保formConfig存在且格式正确
      if (formData.formConfig) {
        try {
          const config = JSON.parse(formData.formConfig)
          console.log('解析后的配置:', config)
          selectedFormConfig.value = formData.formConfig

          // 解析表单预览配置
          if (config.rule && Array.isArray(config.rule)) {
            formPreviewConfig.value = {
              rule: config.rule,
              option: config.option || {
                labelWidth: 120,
                labelPosition: 'right'
              }
            }

            // 生成预览数据（使用默认值）
            const previewData: any = {}
            config.rule.forEach((field: any) => {
              if (field.field) {
                previewData[field.field] = field.props?.defaultValue || ''
              }
            })
            formPreviewData.value = previewData

            console.log('[SchemeForm] 表单预览配置解析成功:', formPreviewConfig.value)
            console.log('[SchemeForm] 表单预览数据:', formPreviewData.value)
          }
        } catch (parseError) {
          console.error('解析表单配置失败:', parseError)
          // 如果解析失败，直接使用原始字符串
          selectedFormConfig.value = formData.formConfig
          formPreviewData.value = {}
          formPreviewConfig.value = {}
        }
      } else {
        console.warn('表单配置为空')
        selectedFormConfig.value = ''
        formPreviewData.value = {}
        formPreviewConfig.value = {}
      }
    } else {
      message.error('获取表单配置失败')
    }
  } catch (error) {
    console.error('获取表单配置失败:', error)
    message.error('获取表单配置失败')
  } finally {
    loadingForm.value = false
  }
}

// 确认表单选择
const confirmFormSelection = () => {
  if (formState.formId) {
    message.success('表单选择已确认')
  }
}
</script>

<style lang="less" scoped>
.form-steps {
  margin-bottom: 24px;
}

.step-content {
  min-height: 400px;
}

.step-panel {
  padding: 24px 0;
}

.form-preview {
  margin-top: 24px;
  padding: 16px;
  background: #f5f5f5;
  border-radius: 6px;

  h4 {
    margin-bottom: 12px;
    color: #333;
  }

  .preview-content {
    color: #666;
  }
}

.step-actions {
  display: flex;
  justify-content: space-between;
  align-items: center;

  .ant-btn {
    margin-left: 8px;
  }
}

.flow-canvas-wrapper {
  width: 100%;
  min-width: 1200px;
  height: 80vh;
  min-height: 600px;
  background: #fff;
  box-sizing: border-box;
  border: 1px solid #e1e5e9;
  border-radius: 4px;
  overflow: hidden;
  display: flex;
  align-items: stretch;
  justify-content: flex-start;
}

.form-selection-container {
  display: flex;
  gap: 16px;
  height: 600px;
}

.form-list-panel {
  flex: 0 0 33.33%;
  padding: 12px;
  background: #fff;
  border-radius: 6px;
  box-sizing: border-box;
  border: 1px solid #e1e5e9;
  display: flex;
  flex-direction: column;

  h4 {
    margin-bottom: 8px;
    color: #333;
    font-size: 14px;
  }
}

.form-list {
  display: flex;
  flex-direction: column;
  gap: 4px;
  flex: 1;
  overflow-y: auto;
}

.form-item {
  display: flex;
  align-items: center;
  padding: 8px 10px;
  background: #f5f5f5;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.2s ease;
  border: 1px solid transparent;

  &:hover {
    background: #e6f7ff;
    border-color: #91d5ff;
  }

  &.active {
    background: #e6f7ff;
    border-color: #1890ff;
  }

  .form-item-content {
    flex: 1;

    .form-name {
      font-weight: 500;
      color: #333;
      margin-bottom: 2px;
      font-size: 13px;
      line-height: 1.2;
    }

    .form-id {
      font-size: 11px;
      color: #666;
      line-height: 1.2;
    }

    .form-actions {
      margin-top: 8px;
    }
  }


  .edit-mode-form-panel {
    padding: 20px;
  }

  .current-form-display {
    h4 {
      color: #333;
      margin-bottom: 16px;
    }

    .form-info {
      background: #f5f5f5;
      padding: 16px;
      border-radius: 6px;
      margin-bottom: 20px;

      p {
        margin: 8px 0;
        color: #666;
      }
    }

    .form-preview {
      h5 {
        color: #333;
        margin-bottom: 12px;
      }
    }
  }
}

.form-preview-panel {
  flex: 0 0 66.67%;
  padding: 12px;
  background: #fff;
  border-radius: 6px;
  box-sizing: border-box;
  border: 1px solid #e1e5e9;
  display: flex;
  flex-direction: column;

  .preview-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 8px;

    h4 {
      color: #333;
      font-size: 14px;
    }

    .preview-actions {
      display: flex;
      gap: 8px;
    }
  }
}

.preview-content {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 0;

  .no-selection {
    text-align: center;
  }

  .loading-form {
    text-align: center;
    padding: 40px;
  }

  .form-preview-wrapper {
    width: 100%;
    height: 100%;
    padding: 8px;
    background: #fff;
    border-radius: 4px;
    box-sizing: border-box;
    border: 1px solid #e1e5e9;
    overflow: auto;
  }
}
</style>
