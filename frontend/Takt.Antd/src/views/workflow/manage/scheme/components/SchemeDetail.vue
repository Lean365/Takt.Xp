<template>
  <Takt-modal
    v-model:open="visible"
    :title="`流程方案详情 - ${detail.schemeName}`"
    width="90%"
    :footer="null"
    :destroy-on-close="true"
    @cancel="handleCancel"
  >
    <a-tabs v-model:activeKey="activeKey" type="card">
      <!-- 基本信息Tab -->
      <a-tab-pane key="basic" tab="基本信息">
        <!-- 调试信息 -->
        <div style="background: #f0f0f0; padding: 10px; margin-bottom: 16px; font-size: 12px;">
          <div>调试信息:</div>
          <div>schemeId: {{ props.schemeId }}</div>
          <div>open: {{ props.open }}</div>
          <div>visible: {{ visible }}</div>
          <div>detail.schemeName: {{ detail.schemeName }}</div>
          <div>detail.schemeKey: {{ detail.schemeKey }}</div>
        </div>
        <a-descriptions :column="2" bordered>
          <a-descriptions-item label="方案名称">
            {{ detail.schemeName }}
          </a-descriptions-item>
          <a-descriptions-item label="方案标识">
            {{ detail.schemeKey }}
          </a-descriptions-item>
          <a-descriptions-item label="版本号">
            {{ detail.version }}
          </a-descriptions-item>
          <a-descriptions-item label="方案分类">
            <Takt-dict-tag :value="detail.schemeCategory" dict-type="workflow_scheme_category" />
          </a-descriptions-item>
          <a-descriptions-item label="状态">
            <Takt-dict-tag :value="detail.status" dict-type="wf_scheme_status" />
          </a-descriptions-item>
          <a-descriptions-item label="创建时间">
            {{ formatDateTime(detail.createTime) }}
          </a-descriptions-item>
          <a-descriptions-item label="更新时间">
            {{ formatDateTime(detail.updateTime) }}
          </a-descriptions-item>
        </a-descriptions>
        <a-descriptions :column="1" bordered style="margin-top: 16px;">
          <a-descriptions-item label="描述">
            {{ detail.notes || '无' }}
          </a-descriptions-item>
        </a-descriptions>
      </a-tab-pane>

      <!-- 表单信息Tab -->
      <a-tab-pane key="form" tab="表单信息">
        <a-card title="表单配置信息" class="form-info-card">
          <div v-if="loadingForm" class="form-loading">
            <a-spin tip="加载表单信息中..." />
          </div>
          <div v-else-if="formDetail" class="form-detail">
            <a-descriptions :column="2" bordered>
              <a-descriptions-item label="表单名称">
                {{ formDetail.formName }}
              </a-descriptions-item>
              <a-descriptions-item label="表单标识">
                {{ formDetail.formKey }}
              </a-descriptions-item>
              <a-descriptions-item label="表单分类">
                <Takt-dict-tag :value="formDetail.formCategory" dict-type="workflow_form_category" />
              </a-descriptions-item>
              <a-descriptions-item label="表单类型">
                <Takt-dict-tag :value="formDetail.formType" dict-type="wf_form_type" />
              </a-descriptions-item>
              <a-descriptions-item label="状态">
                <Takt-dict-tag :value="formDetail.status" dict-type="wf_form_status" />
              </a-descriptions-item>
              <a-descriptions-item label="创建时间">
                {{ formatDateTime(formDetail.createdTime) }}
              </a-descriptions-item>
            </a-descriptions>
            <a-descriptions :column="1" bordered style="margin-top: 16px;">
              <a-descriptions-item label="描述">
                {{ formDetail.description || '无' }}
              </a-descriptions-item>
            </a-descriptions>
            
            <!-- 表单字段列表 -->
            <div v-if="formFields.length > 0" class="form-fields-section">
              <h4>表单字段 ({{ formFields.length }}个)</h4>
              <a-table
                :columns="formColumns"
                :data-source="formFields"
                :pagination="false"
                size="small"
                class="field-table"
              />
            </div>
            
            <!-- 表单预览 -->
            <div v-if="formDetail.formConfig" class="form-preview-section">
              <h4>表单预览</h4>
              <!-- 调试信息 -->
              <div style="background: #f0f0f0; padding: 10px; margin-bottom: 16px; font-size: 12px;">
                <div>表单预览调试信息:</div>
                <div>formPreviewConfig存在: {{ !!formPreviewConfig }}</div>
                <div>formPreviewConfig.rule存在: {{ !!formPreviewConfig?.rule }}</div>
                <div>formPreviewConfig.rule长度: {{ formPreviewConfig?.rule?.length || 0 }}</div>
                <div>formPreviewData存在: {{ !!formPreviewData }}</div>
                <div>formPreviewData字段数量: {{ Object.keys(formPreviewData || {}).length }}</div>
                <div>formPreviewData内容: {{ JSON.stringify(formPreviewData) }}</div>
              </div>
              <div class="form-preview-wrapper">
                <div v-if="formPreviewConfig && formPreviewConfig.rule && formPreviewConfig.rule.length > 0">
                  <Takt-form 
                    :model-value="formPreviewConfig"
                    :readonly="true"
                  />
                </div>
                <div v-else class="no-form-preview">
                  <a-empty description="暂无表单预览数据" />
                </div>
              </div>
            </div>
          </div>
          <div v-else class="form-empty">
            <a-empty description="表单信息为空" />
          </div>
        </a-card>
      </a-tab-pane>

      <!-- 流程信息Tab -->
      <a-tab-pane key="flow" tab="流程信息">
        <a-card title="流程配置信息" class="flow-info-card">
          <div v-if="detail.schemeConfig" class="flow-detail">
            <!-- 流程图预览 -->
            <div class="flow-diagram-section">
              <h4>流程图预览</h4>
              <div class="flow-diagram">
                <Takt-flow 
                  v-model:value="schemeConfigObject" 
                  :width="1600" 
                  :height="800"
                  :readonly="true"
                />
              </div>
            </div>
          </div>
          <div v-else class="flow-empty">
            <a-empty description="流程配置为空或解析失败" />
          </div>
        </a-card>
      </a-tab-pane>
    </a-tabs>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch, nextTick, computed } from 'vue'
import { message } from 'ant-design-vue'
import { getSchemeById } from '@/api/workflow/scheme'
import { getFormById } from '@/api/workflow/form'
import { formatDateTime } from '@/utils/datetimeUtil'
import type { TaktScheme } from '@/types/workflow/scheme'

// Props
const props = defineProps<{
  schemeId?: string
  open?: boolean
}>()

// Emits
const emit = defineEmits<{
  'update:open': [value: boolean]
}>()

// 响应式数据
const visible = computed({
  get: () => props.open || false,
  set: (value: boolean) => emit('update:open', value)
})
const activeKey = ref('basic')
const loadingForm = ref(false)

// 方案详情
const detail = ref<TaktScheme>({
  schemeId: '',
  schemeKey: '',
  schemeName: '',
  schemeCategory: 1,
  version: '1.0',
  formId: '',
  schemeConfig: '',
  status: 0,
  description: '',
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0
} as TaktScheme)

const formDetail = ref<any>(null)
const formFields = ref<any[]>([])
const formPreviewData = ref<any>({})
const formPreviewConfig = ref<any>({})

// 计算属性：将schemeConfig字符串转换为对象供TaktFlow组件使用
const schemeConfigObject = computed({
  get: () => {
    console.log('[SchemeDetail] 计算属性被调用, detail.value.schemeConfig:', detail.value.schemeConfig)
    if (!detail.value.schemeConfig) {
      console.log('[SchemeDetail] schemeConfig为空，返回默认值')
      return { nodes: [], edges: [] }
    }
    try {
      const config = typeof detail.value.schemeConfig === 'string' 
        ? JSON.parse(detail.value.schemeConfig) 
        : detail.value.schemeConfig
      
      // 确保边数据有完整的锚点信息
      const processedConfig = {
        ...config,
        edges: config.edges.map((edge: any, index: number) => {
          const edgeConfig = {
            ...edge,
            id: edge.id || `edge_${index}`,
            source: edge.source,
            target: edge.target
          }
          
          // 设置锚点信息 - 优先使用数据中的锚点配置
          if (edge.sourceAnchor && edge.targetAnchor) {
            edgeConfig.sourceAnchor = edge.sourceAnchor
            edgeConfig.targetAnchor = edge.targetAnchor
          } else {
            // 如果没有锚点信息，设置默认值
            edgeConfig.sourceAnchor = 'right'
            edgeConfig.targetAnchor = 'left'
          }
          
          // 确保边数据格式与TaktFlow组件期望的格式一致
          // 移除可能存在的sourcePort和targetPort，使用锚点
          delete edgeConfig.sourcePort
          delete edgeConfig.targetPort
          
          return edgeConfig
        })
      }
      
      console.log('[SchemeDetail] 解析成功，结果:', processedConfig)
      return processedConfig
    } catch (error) {
      console.error('[SchemeDetail] 解析schemeConfig失败:', error)
      return { nodes: [], edges: [] }
    }
  },
  set: (value) => {
    // 只读模式，不需要setter
  }
})

// 表单表格列定义
const formColumns = [
  {
    title: '字段名称',
    dataIndex: 'fieldName',
    key: 'fieldName'
  },
  {
    title: '字段类型',
    dataIndex: 'fieldType',
    key: 'fieldType'
  },
  {
    title: '是否必填',
    dataIndex: 'required',
    key: 'required'
  },
  {
    title: '默认值',
    dataIndex: 'defaultValue',
    key: 'defaultValue'
  },
  {
    title: '字段描述',
    dataIndex: 'description',
    key: 'description'
  }
]


// 加载表单详情
const loadFormDetail = async (formId: string) => {
  if (!formId) return
  
  console.log('[SchemeDetail] 开始加载表单详情, formId:', formId)
  loadingForm.value = true
  try {
    const res = await getFormById(formId)
    console.log('[SchemeDetail] 获取表单详情响应:', res)
    if (res.data) {
      formDetail.value = res.data
      console.log('[SchemeDetail] 表单详情数据:', formDetail.value)
      
      // 解析表单字段和预览配置
      if (formDetail.value.formConfig) {
        console.log('[SchemeDetail] 开始解析表单配置:', formDetail.value.formConfig)
        try {
          const config = JSON.parse(formDetail.value.formConfig)
          
          // 解析表单字段
          if (config.fields && Array.isArray(config.fields)) {
            formFields.value = config.fields.map((field: any) => ({
              fieldName: field.name || field.fieldName,
              fieldType: field.type || field.fieldType,
              required: field.required || false,
              defaultValue: field.defaultValue || '',
              description: field.description || ''
            }))
            console.log('[SchemeDetail] 表单字段解析成功:', formFields.value)
          }
          
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
                // 尝试多种可能的默认值来源
                let defaultValue = field.props?.defaultValue || 
                                 field.defaultValue || 
                                 field.value || 
                                 ''
                
                // 如果默认值为空，根据字段类型生成合适的预览值
                if (!defaultValue) {
                  switch (field.type) {
                    case 'input':
                      defaultValue = `示例${field.title || field.field}`
                      break
                    case 'select':
                      // 如果有选项，使用第一个选项的值
                      if (field.options && field.options.length > 0) {
                        defaultValue = field.options[0].value
                      } else {
                        defaultValue = '请选择'
                      }
                      break
                    case 'number':
                      defaultValue = 0
                      break
                    case 'datePicker':
                      defaultValue = new Date().toISOString().split('T')[0]
                      break
                    case 'textarea':
                      defaultValue = `这是${field.title || field.field}的示例内容`
                      break
                    case 'upload':
                      defaultValue = []
                      break
                    default:
                      defaultValue = `示例${field.title || field.field}`
                  }
                }
                
                previewData[field.field] = defaultValue
              }
            })
            formPreviewData.value = previewData
            
            console.log('[SchemeDetail] 表单预览配置解析成功:', formPreviewConfig.value)
            console.log('[SchemeDetail] 表单预览数据:', formPreviewData.value)
            console.log('[SchemeDetail] 表单预览数据字段数量:', Object.keys(formPreviewData.value).length)
            console.log('[SchemeDetail] 表单预览配置规则数量:', formPreviewConfig.value.rule?.length || 0)
          } else {
            // 如果没有rule配置，尝试其他可能的结构
            console.log('[SchemeDetail] 未找到rule配置，尝试其他结构:', config)
            
            // 尝试处理fields结构
            if (config.fields && Array.isArray(config.fields)) {
              const previewData: any = {}
              config.fields.forEach((field: any) => {
                const fieldName = field.name || field.fieldName || field.field
                if (fieldName) {
                  let defaultValue = field.defaultValue || field.value || ''
                  
                  // 如果默认值为空，根据字段类型生成合适的预览值
                  if (!defaultValue) {
                    switch (field.type) {
                      case 'input':
                        defaultValue = `示例${field.title || field.label || fieldName}`
                        break
                      case 'select':
                        if (field.options && field.options.length > 0) {
                          defaultValue = field.options[0].value
                        } else {
                          defaultValue = '请选择'
                        }
                        break
                      case 'number':
                        defaultValue = 0
                        break
                      case 'datePicker':
                        defaultValue = new Date().toISOString().split('T')[0]
                        break
                      case 'textarea':
                        defaultValue = `这是${field.title || field.label || fieldName}的示例内容`
                        break
                      case 'upload':
                        defaultValue = []
                        break
                      default:
                        defaultValue = `示例${field.title || field.label || fieldName}`
                    }
                  }
                  
                  previewData[fieldName] = defaultValue
                }
              })
              formPreviewData.value = previewData
              
              // 生成基本的表单配置
              formPreviewConfig.value = {
                rule: config.fields.map((field: any) => ({
                  type: field.type || 'input',
                  field: field.name || field.fieldName || field.field,
                  title: field.title || field.label || field.name || field.fieldName,
                  props: {
                    placeholder: field.placeholder || `请输入${field.title || field.label || field.name}`,
                    ...field.props
                  }
                })),
                option: config.option || {
                  labelWidth: 120,
                  labelPosition: 'right'
                }
              }
              
              console.log('[SchemeDetail] 使用fields结构生成预览配置:', formPreviewConfig.value)
              console.log('[SchemeDetail] 使用fields结构生成预览数据:', formPreviewData.value)
            }
          }
        } catch (error) {
          console.error('[SchemeDetail] 解析表单配置失败:', error)
        }
      }
    }
  } catch (error) {
    console.error('加载表单详情失败:', error)
    message.error('加载表单详情失败')
  } finally {
    loadingForm.value = false
  }
}

// 加载方案详情
const loadSchemeDetail = async () => {
  console.log('[SchemeDetail] 开始加载方案详情, schemeId:', props.schemeId)
  if (!props.schemeId) {
    console.log('[SchemeDetail] schemeId为空，跳过加载')
    return
  }
  
  try {
    const res = await getSchemeById(props.schemeId)
    console.log('[SchemeDetail] 获取方案详情响应:', res)
    if (res.data) {
      detail.value = res.data
      console.log('[SchemeDetail] 方案详情数据:', detail.value)
      console.log('[SchemeDetail] 方案名称:', detail.value.schemeName)
      console.log('[SchemeDetail] 方案标识:', detail.value.schemeKey)
      console.log('[SchemeDetail] 工作流配置:', detail.value.schemeConfig)
      
      // 加载表单详情
      if (detail.value.formId) {
        console.log('[SchemeDetail] 开始加载表单详情, formId:', detail.value.formId)
        await loadFormDetail(detail.value.formId)
      }
      
      // 工作流配置通过计算属性自动处理，无需手动解析
    } else {
      console.log('[SchemeDetail] 响应数据为空')
    }
  } catch (error) {
    console.error('[SchemeDetail] 加载方案详情失败:', error)
    message.error('加载方案详情失败')
  }
}

// 取消
const handleCancel = () => {
  visible.value = false
}

// 监听visible变化
watch(visible, (newVal) => {
  console.log('[SchemeDetail] visible变化:', newVal, 'props.open:', props.open, 'props.schemeId:', props.schemeId)
  if (newVal) {
    nextTick(() => {
      console.log('[SchemeDetail] 准备加载数据')
      loadSchemeDetail()
    })
  }
})

// 监听props.open变化
watch(() => props.open, (newVal) => {
  console.log('[SchemeDetail] props.open变化:', newVal)
})

// 监听props.schemeId变化
watch(() => props.schemeId, (newVal) => {
  console.log('[SchemeDetail] props.schemeId变化:', newVal)
})

// 暴露方法
defineExpose({
  open: () => {
    visible.value = true
  }
})
</script>

<style scoped>
.form-info-card,
.flow-info-card {
  margin-bottom: 16px;
}

.form-loading,
.flow-loading {
  text-align: center;
  padding: 40px 0;
}

.form-detail,
.flow-detail {
  padding: 16px 0;
}

.form-fields-section,
.form-preview-section {
  margin-top: 24px;
}

.form-fields-section h4,
.form-preview-section h4 {
  margin-bottom: 16px;
  color: #1890ff;
}

.field-table {
  margin-top: 16px;
}

.flow-diagram-section {
  margin-top: 24px;
}

.flow-diagram-section h4 {
  margin-bottom: 16px;
  color: #1890ff;
}

.flow-diagram {
  border: 1px solid #d9d9d9;
  border-radius: 6px;
  overflow: hidden;
}

.form-empty,
.flow-empty {
  text-align: center;
  padding: 40px 0;
}

.form-preview-wrapper {
  border: 1px solid #d9d9d9;
  border-radius: 6px;
  padding: 16px;
  background-color: #fafafa;
}

.no-form-preview {
  text-align: center;
  padding: 20px 0;
}
</style>
