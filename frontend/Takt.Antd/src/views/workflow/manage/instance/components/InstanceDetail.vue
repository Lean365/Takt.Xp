<template>
  <Takt-modal
    v-model:open="visible"
    :title="t('workflow.instance.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-instance-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.instance.fields.instanceTitle')">
          {{ detail.instanceTitle }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.schemeId')">
          {{ detail.schemeId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.businessKey')">
          {{ detail.businessKey || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.initiatorId')">
          {{ detail.initiatorId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.currentNodeId')">
          {{ detail.currentNodeId || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.currentNodeName')">
          {{ detail.currentNodeName || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.previousNodeId')">
          {{ detail.previousNodeId || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.previousNodeName')">
          {{ detail.previousNodeName || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.priority')">
          <Takt-dict-tag dict-type="wf_instance_priority" :value="detail.priority" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.urgency')">
          <Takt-dict-tag dict-type="wf_instance_urgency" :value="detail.urgency" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.startTime')">
          {{ detail.startTime || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.endTime')">
          {{ detail.endTime || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.formId')">
          {{ detail.formId || '-' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.formType')">
          <Takt-dict-tag dict-type="wf_form_type" :value="detail.formType" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.status')">
          <Takt-dict-tag dict-type="wf_instance_status" :value="detail.status" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.instance.fields.remark')">
          {{ detail.remark || '-' }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 节点信息 -->
      <div class="node-info">
        <h3>{{ t('workflow.instance.node.title') }}</h3>
        <a-table
          :columns="nodeColumns"
          :data-source="[]"
          :pagination="false"
          row-key="id"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'nodeType'">
              <Takt-dict-tag dict-type="wf_node_type" :value="record.nodeType" />
            </template>
            <template v-if="column.key === 'assigneeType'">
              <Takt-dict-tag dict-type="wf_approver_type" :value="record.assigneeType" />
            </template>
            <template v-if="column.key === 'formType'">
              <Takt-dict-tag dict-type="workflow_form_type" :value="record.formType" />
            </template>
            <template v-if="column.key === 'action'">
              <a-space>
                <span>简化后的工作流不包含节点操作功能</span>
              </a-space>
            </template>
          </template>
        </a-table>
      </div>

      <!-- 工作流图形 -->
      <div class="workflow-diagram">
        <h3>工作流程图</h3>
        <div class="workflow-canvas">
          <div v-if="workflowConfig && (workflowConfig.nodes?.length > 0 || workflowConfig.edges?.length > 0)">
            <Takt-flow 
              v-model:value="workflowConfig" 
              :width="1600" 
              :height="600"
              :readonly="true"
            />
          </div>
          <div v-else class="no-workflow-data">
            <a-empty description="暂无工作流配置" />
          </div>
        </div>
      </div>

      <!-- 表单信息 -->
      <div class="form-info">
        <h3>{{ t('workflow.instance.form.title') }}</h3>
        <a-table
          :columns="formColumns"
          :data-source="formFields"
          :pagination="false"
          row-key="id"
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'fieldType'">
              <Takt-dict-tag dict-type="wf_form_field_type" :value="record.fieldType" />
            </template>
            <template v-if="column.key === 'required'">
              <a-tag :color="record.required ? 'red' : 'green'">
                {{ record.required ? t('common.yes') : t('common.no') }}
              </a-tag>
            </template>
          </template>
        </a-table>
      </div>

      <!-- 流程变量 -->
      <div class="variables-info" v-if="detail.variables">
        <h3>流程变量</h3>
        <a-textarea :value="detail.variables" :rows="6" readonly />
      </div>

      <!-- 表单数据 -->
      <div class="form-data-info" v-if="detail.formData">
        <h3>表单数据</h3>
        <div class="form-display">
          <div v-if="formData && Object.keys(formData).length > 0">
            <Takt-form 
              :model-value="formConfig"
              :readonly="true"
            />
          </div>
          <div v-else class="no-form-data">
            <a-empty description="暂无表单数据" />
          </div>
        </div>
      </div>
    </div>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { TaktInstance } from '@/types/workflow/instance'
import { getInstanceById } from '@/api/workflow/instance'
import { getSchemeById } from '@/api/workflow/scheme'
import { message } from 'ant-design-vue'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  instanceId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<TaktInstance>({
  instanceId: '0',
  schemeId: '0',
  instanceTitle: '',
  schemeConfig: '',
  businessKey: '',
  initiatorId: '0',
  currentNodeId: '',
  currentNodeName: '',
  previousNodeId: '',
  previousNodeName: '',
  priority: 2,
  urgency: 1,
  startTime: '',
  endTime: '',
  variables: '',
  formId: '',
  formType: 1,
  formData: '',
  status: 0,
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0
} as TaktInstance)

const formFields = ref<any[]>([])
const schemeConfigData = ref<string>('')
const formData = ref<any>({})
const formConfig = ref<any>({})

// 计算属性：将schemeConfig字符串转换为对象供TaktFlow组件使用
const workflowConfig = computed({
  get: () => {
    if (!schemeConfigData.value) {
      return { nodes: [], edges: [] }
    }
    try {
      return typeof schemeConfigData.value === 'string' 
        ? JSON.parse(schemeConfigData.value) 
        : schemeConfigData.value
    } catch (error) {
      console.error('解析schemeConfig失败:', error)
      return { nodes: [], edges: [] }
    }
  },
  set: (value) => {
    // 只读模式，不需要setter
  }
})

// 节点表格列定义
const nodeColumns = [
  {
    title: t('workflow.node.fields.nodeName.label'),
    dataIndex: 'nodeName',
    key: 'nodeName'
  },
  {
    title: t('workflow.node.fields.nodeType.label'),
    dataIndex: 'nodeType',
    key: 'nodeType'
  },
  {
    title: t('workflow.node.fields.assigneeType.label'),
    dataIndex: 'assigneeType',
    key: 'assigneeType'
  },
  {
    title: t('workflow.node.fields.formType.label'),
    dataIndex: 'formType',
    key: 'formType'
  },
  {
    title: t('common.action'),
    key: 'action',
    width: 200,
    fixed: 'right' as const
  }
]

// 表单表格列定义
const formColumns = [
  {
    title: t('workflow.form.fields.fieldName.label'),
    dataIndex: 'fieldName',
    key: 'fieldName'
  },
  {
    title: t('workflow.form.fields.fieldType.label'),
    dataIndex: 'fieldType',
    key: 'fieldType'
  },
  {
    title: t('workflow.form.fields.required.label'),
    dataIndex: 'required',
    key: 'required'
  },
  {
    title: t('workflow.form.fields.remark.label'),
    dataIndex: 'remark',
    key: 'remark'
  }
]

// 解析表单数据
const parseFormData = () => {
  if (!detail.value.formData) {
    formData.value = {}
    formConfig.value = {}
    return
  }
  
  try {
    const parsedData = typeof detail.value.formData === 'string' 
      ? JSON.parse(detail.value.formData) 
      : detail.value.formData
    
    console.log('[InstanceDetail] 解析表单数据:', parsedData)
    
    // 分离表单数据和表单配置
    if (parsedData.formData) {
      formData.value = parsedData.formData
    } else {
      formData.value = parsedData
    }
    
    if (parsedData.formConfig) {
      formConfig.value = parsedData.formConfig
    } else {
      // 如果没有表单配置，创建一个简单的配置
      formConfig.value = {
        rule: Object.keys(formData.value).map(key => ({
          type: 'input',
          field: key,
          title: key,
          props: {
            placeholder: `请输入${key}`
          }
        })),
        option: {
          labelWidth: 120,
          labelPosition: 'right'
        }
      }
    }
    
    console.log('[InstanceDetail] 解析后的表单数据:', formData.value)
    console.log('[InstanceDetail] 解析后的表单配置:', formConfig.value)
  } catch (error) {
    console.error('[InstanceDetail] 解析表单数据失败:', error)
    formData.value = {}
    formConfig.value = {}
  }
}

// 获取工作流实例详情
const fetchData = async () => {
  if (!props.instanceId) {
    return
  }
  try {
    const res = await getInstanceById(props.instanceId)
    if (res.data) {
      detail.value = res.data
      
      // 解析表单数据
      parseFormData()
      
      // 获取工作流配置
      if (detail.value.schemeId) {
        await loadWorkflowConfig(detail.value.schemeId)
      }
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取工作流实例详情失败:', error)
    message.error(t('common.failed'))
  }
}

// 加载工作流配置
const loadWorkflowConfig = async (schemeId: string) => {
  if (!schemeId || schemeId === '0') {
    schemeConfigData.value = ''
    return
  }
  
  try {
    const res = await getSchemeById(schemeId)
    if (res.data && res.data.schemeConfig) {
      console.log('[InstanceDetail] 获取工作流配置:', res.data.schemeConfig)
      schemeConfigData.value = res.data.schemeConfig
      console.log('[InstanceDetail] 工作流配置设置成功')
    } else {
      schemeConfigData.value = ''
    }
  } catch (error) {
    console.error('[InstanceDetail] 获取工作流配置失败:', error)
    schemeConfigData.value = ''
  }
}

// 监听instanceId变化
watch(() => props.instanceId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })

// 简化后的工作流不包含task功能，这些操作方法已不再需要
</script>

<style lang="less" scoped>
.workflow-instance-detail {
  .node-info,
  .form-info,
  .workflow-diagram,
  .variables-info,
  .form-data-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }
  
  .workflow-diagram {
    .workflow-canvas {
      border: 1px solid #d9d9d9;
      border-radius: 6px;
      overflow: hidden;
    }
  }

  .variables-info,
  .form-data-info {
    h3 {
      color: #1890ff;
      font-weight: 500;
    }
  }
  
  .form-display {
    border: 1px solid #d9d9d9;
    border-radius: 6px;
    padding: 16px;
    background-color: #fafafa;
  }
}
</style> 
