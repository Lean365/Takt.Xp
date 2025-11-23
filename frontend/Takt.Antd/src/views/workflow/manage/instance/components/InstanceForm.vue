<template>
  <Takt-modal
    v-model:open="visible"
    :title="title"
    :width="1800"
    :loading="loading"
    :fullscreen="isFullscreen"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-tabs v-model:activeKey="activeTab" class="form-tabs">
      <a-tab-pane key="1" :tab="t('workflow.instance.tabs.basic')">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 4 }"
          :wrapper-col="{ span: 20 }"
        >
          <a-form-item label="实例标题" name="instanceTitle">
            <a-input v-model:value="formState.instanceTitle" placeholder="请输入实例标题" />
          </a-form-item>
          <a-form-item label="流程定义" name="schemeId">
            <Takt-select 
              v-model:value="formState.schemeId" 
              :options="schemeOptions"
              placeholder="请选择流程定义" 
              style="width: 100%"
              show-search
              :filter-option="(input, option) => option.label?.toLowerCase().includes(input.toLowerCase())"
            />
          </a-form-item>
          <a-form-item label="业务键" name="businessKey">
            <a-input v-model:value="formState.businessKey" placeholder="请输入业务键" />
          </a-form-item>
          <a-form-item label="发起人ID" name="initiatorId">
            <Takt-select 
              v-model:value="formState.initiatorId" 
              :options="userOptions"
              placeholder="请选择发起人" 
              style="width: 100%"
              show-search
              :filter-option="(input, option) => option.label?.toLowerCase().includes(input.toLowerCase())"
            />
          </a-form-item>
          <a-form-item label="优先级" name="priority">
            <Takt-select 
              v-model:value="formState.priority" 
              dict-type="wf_instance_priority" 
              placeholder="请选择优先级" 
              style="width: 100%"
            />
          </a-form-item>
          <a-form-item label="紧急程度" name="urgency">
            <Takt-select 
              v-model:value="formState.urgency" 
              dict-type="wf_instance_urgency" 
              placeholder="请选择紧急程度" 
              style="width: 100%"
            />
          </a-form-item>
          <a-form-item label="当前节点" name="currentNodeId">
            <Takt-select 
              v-model:value="formState.currentNodeId" 
              :options="nodeOptions"
              placeholder="请选择当前节点" 
              style="width: 100%"
              show-search
              :filter-option="(input, option) => option.label?.toLowerCase().includes(input.toLowerCase())"
              @change="handleCurrentNodeChange"
            />
          </a-form-item>
          <a-form-item label="上一节点ID" name="previousNodeId">
            <a-input v-model:value="formState.previousNodeId" placeholder="自动计算" readonly />
          </a-form-item>
          <a-form-item label="上一节点名称" name="previousNodeName">
            <a-input v-model:value="formState.previousNodeName" placeholder="自动计算" readonly />
          </a-form-item>
          <a-form-item label="开始时间" name="startTime">
            <a-date-picker v-model:value="formState.startTime" placeholder="请选择开始时间" style="width: 100%" />
          </a-form-item>
          <a-form-item label="结束时间" name="endTime">
            <a-date-picker v-model:value="formState.endTime" placeholder="请选择结束时间" style="width: 100%" />
          </a-form-item>
          <a-form-item label="表单定义ID" name="formId">
            <Takt-select 
              v-model:value="formState.formId" 
              :options="formOptions"
              placeholder="请选择表单定义" 
              style="width: 100%"
              show-search
              :filter-option="(input, option) => option.label?.toLowerCase().includes(input.toLowerCase())"
            />
          </a-form-item>
          <a-form-item label="表单类型" name="formType">
            <Takt-select 
              v-model:value="formState.formType" 
              dict-type="wf_form_type" 
              placeholder="请选择表单类型" 
              style="width: 100%"
            />
          </a-form-item>
          <a-form-item label="状态" name="status">
            <Takt-select 
              v-model:value="formState.status" 
              dict-type="wf_instance_status" 
              placeholder="请选择状态"
              style="width: 100%" 
            />
          </a-form-item>
          <a-form-item label="备注" name="remark">
            <a-textarea v-model:value="formState.remark" placeholder="请输入备注" :rows="3" />
          </a-form-item>
        </a-form>
      </a-tab-pane>
      <a-tab-pane key="2" :tab="t('workflow.instance.tabs.designer')">
        <div class="workflow-config-section">
          <h4>流程配置</h4>
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
        <div class="variables-section">
          <h4>流程变量</h4>
          <a-textarea v-model:value="formState.variables" placeholder="请输入流程变量（JSON格式）" :rows="10" />
        </div>
        <div class="form-data-section">
          <h4>表单数据</h4>
          <div class="form-editor">
            <div v-if="formData && Object.keys(formData).length > 0">
              <Takt-form 
                :model-value="formConfig"
                :readonly="false"
              />
            </div>
            <div v-else class="no-form-data">
              <a-empty description="暂无表单数据" />
            </div>
          </div>
        </div>
      </a-tab-pane>
    </a-tabs>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TaktInstance, TaktInstanceCreate, TaktInstanceUpdate } from '@/types/workflow/instance'
import type { TaktSelectOption } from '@/types/common'
import { getInstanceById, createInstance, updateInstance } from '@/api/workflow/instance'
import { getSchemeById, getSchemeOptions } from '@/api/workflow/scheme'
import { getFormById, getFormOptions } from '@/api/workflow/form'
import { getUserOptions } from '@/api/identity/user'
import { useDictStore } from '@/stores/dictStore'


const props = defineProps<{
  open: boolean
  title: string
  instanceId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const { t } = useI18n()
const dictStore = useDictStore()
const formRef = ref<FormInstance>()
const loading = ref(false)
const formOptions = ref<{label: string, value: string | number}[]>([])
const schemeOptions = ref<{label: string, value: string | number}[]>([])
const userOptions = ref<{label: string, value: string | number}[]>([])
const nodeOptions = ref<{label: string, value: string | number}[]>([])
const activeTab = ref('1')
const isFullscreen = ref(false)
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
    // 防止无限循环：只有当值真正改变时才更新
    const newValue = JSON.stringify(value)
    if (newValue !== schemeConfigData.value) {
      schemeConfigData.value = newValue
    }
  }
})

// 解析表单数据
const parseFormData = () => {
  if (!formState.formData) {
    formData.value = {}
    formConfig.value = {}
    return
  }
  
  try {
    const parsedData = typeof formState.formData === 'string' 
      ? JSON.parse(formState.formData) 
      : formState.formData
    
    console.log('[InstanceForm] 解析表单数据:', parsedData)
    
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
    
    console.log('[InstanceForm] 解析后的表单数据:', formData.value)
    console.log('[InstanceForm] 解析后的表单配置:', formConfig.value)
  } catch (error) {
    console.error('[InstanceForm] 解析表单数据失败:', error)
    formData.value = {}
    formConfig.value = {}
  }
}

// 更新表单数据到formState
const updateFormDataToState = () => {
  try {
    formState.formData = JSON.stringify({
      formData: formData.value,
      formConfig: formConfig.value
    })
  } catch (error) {
    console.error('更新表单数据失败:', error)
  }
}

// 重置表单
const resetForm = () => {
  formState.schemeId = '0'
  formState.instanceTitle = ''
  formState.businessKey = ''
  formState.initiatorId = '0'
  formState.currentNodeId = ''
  formState.currentNodeName = ''
  formState.previousNodeId = ''
  formState.previousNodeName = ''
  formState.priority = 2
  formState.urgency = 1
  formState.startTime = ''
  formState.endTime = ''
  formState.variables = ''
  formState.formId = ''
  formState.formType = 1
  formState.formData = ''
  formState.status = 0
  formState.remark = ''
  formData.value = {}
  formConfig.value = {}
  nodeOptions.value = []
  formRef.value?.resetFields()
}

// 表单状态
const formState = reactive<TaktInstanceCreate>({
  schemeId: '0',
  instanceTitle: '',
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
  remark: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  instanceTitle: [
    { required: true, message: t('workflow.instance.fields.instanceTitle.required'), trigger: 'blur' }
  ],
  schemeId: [
    { required: true, message: t('workflow.instance.fields.schemeId.required'), trigger: 'change' }
  ],
  initiatorId: [
    { required: true, message: t('workflow.instance.fields.initiatorId.required'), trigger: 'change' }
  ]
}

// 获取实例详情
const getDetail = async (id: string) => {
  if (!id) {
    return
  }
  try {
    const res = await getInstanceById(id)
    if (res.data) {
      const data = res.data
      formState.schemeId = data.schemeId
      formState.instanceTitle = data.instanceTitle || ''
      formState.businessKey = data.businessKey || ''
      formState.initiatorId = data.initiatorId
      formState.currentNodeId = data.currentNodeId || ''
      formState.currentNodeName = data.currentNodeName || ''
      formState.previousNodeId = data.previousNodeId || ''
      formState.previousNodeName = data.previousNodeName || ''
      formState.priority = data.priority
      formState.urgency = data.urgency
      formState.startTime = data.startTime || ''
      formState.endTime = data.endTime || ''
      formState.variables = data.variables || ''
      formState.formId = data.formId || ''
      formState.formType = data.formType
      formState.formData = data.formData || ''
      formState.status = data.status
      formState.remark = data.remark || ''
      
      // 解析表单数据
      parseFormData()
      
      // 获取工作流配置
      if (data.schemeId) {
        await loadWorkflowConfig(data.schemeId)
        
        // 如果有当前节点ID，自动处理上一节点
        if (data.currentNodeId) {
          updatePreviousNode(data.currentNodeId)
        }
      }
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error(error)
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
      console.log('[InstanceForm] 获取工作流配置:', res.data.schemeConfig)
      schemeConfigData.value = res.data.schemeConfig
      console.log('[InstanceForm] 工作流配置设置成功')
    } else {
      schemeConfigData.value = ''
    }
  } catch (error) {
    console.error('[InstanceForm] 获取工作流配置失败:', error)
    schemeConfigData.value = ''
  }
}

// 加载表单配置
const loadFormConfig = async (formId: string) => {
  if (!formId || formId === '0') {
    formConfig.value = {}
    return
  }
  
  try {
    const res = await getFormById(formId)
    if (res.data && res.data.formConfig) {
      formConfig.value = JSON.parse(res.data.formConfig)
      console.log('[InstanceForm] 表单配置加载成功:', formConfig.value)
    } else {
      formConfig.value = {}
    }
  } catch (error) {
    console.error('[InstanceForm] 加载表单配置失败:', error)
    formConfig.value = {}
  }
}

// 加载节点选项
const loadNodeOptions = async (schemeConfig: string) => {
  if (!schemeConfig) {
    nodeOptions.value = []
    return
  }
  
  try {
    const config = JSON.parse(schemeConfig)
    if (config.nodes && Array.isArray(config.nodes)) {
      nodeOptions.value = config.nodes.map((node: any) => {
        // 节点名称可能存储在 data.nodeName 或直接是 name 字段
        const nodeName = node.data?.nodeName || node.name || node.id
        return {
          label: `${nodeName} (${node.type})`,
          value: node.id
        }
      })
      console.log('[InstanceForm] 节点选项加载成功:', nodeOptions.value)
    } else {
      nodeOptions.value = []
    }
  } catch (error) {
    console.error('[InstanceForm] 加载节点选项失败:', error)
    nodeOptions.value = []
  }
}

// 处理当前节点变化
const handleCurrentNodeChange = (value: any) => {
  const nodeId = String(value)
  if (nodeId && nodeId !== '0') {
    const selectedNode = nodeOptions.value.find(node => node.value === nodeId)
    if (selectedNode) {
      formState.currentNodeName = selectedNode.label.split(' (')[0] // 提取节点名称
    }
    
    // 自动处理上一节点
    updatePreviousNode(nodeId)
  } else {
    formState.currentNodeName = ''
    formState.previousNodeId = ''
    formState.previousNodeName = ''
  }
}

// 根据当前节点自动更新上一节点
const updatePreviousNode = (currentNodeId: string) => {
  if (!schemeConfigData.value) {
    return
  }
  
  try {
    const config = JSON.parse(schemeConfigData.value)
    if (config.edges && Array.isArray(config.edges)) {
      // 查找指向当前节点的边
      const incomingEdges = config.edges.filter((edge: any) => edge.target === currentNodeId)
      
      if (incomingEdges.length > 0) {
        // 取第一个入边的源节点作为上一节点
        const previousEdge = incomingEdges[0]
        const previousNode = config.nodes.find((node: any) => node.id === previousEdge.source)
        
        if (previousNode) {
          const previousNodeName = previousNode.data?.nodeName || previousNode.name || previousNode.id
          formState.previousNodeId = previousNode.id
          formState.previousNodeName = previousNodeName
          console.log('[InstanceForm] 自动设置上一节点:', previousNodeName)
        }
      } else {
        // 没有入边，可能是开始节点
        formState.previousNodeId = ''
        formState.previousNodeName = ''
        console.log('[InstanceForm] 当前节点没有上一节点（可能是开始节点）')
      }
    }
  } catch (error) {
    console.error('[InstanceForm] 更新上一节点失败:', error)
  }
}

// 监听表单打开状态
watch(() => props.open, (newVal) => {
  if (newVal) {
    // 表单打开时加载选项数据
    loadOptions()
  }
}, { immediate: true })

// 监听工作流实例ID变化
watch(() => props.instanceId, (newVal) => {
  if (newVal) {
    getDetail(newVal)
  } else {
    resetForm()
  }
}, { immediate: true })

// 监听流程定义变化，自动关联表单和加载节点选项
watch(() => formState.schemeId, async (newSchemeId) => {
  if (newSchemeId && newSchemeId !== '0') {
    try {
      // 获取流程定义详情
      const schemeRes = await getSchemeById(newSchemeId)
      if (schemeRes.data) {
        // 自动设置关联的表单
        if (schemeRes.data.formId) {
          formState.formId = schemeRes.data.formId
          // 加载表单配置
          await loadFormConfig(schemeRes.data.formId)
        }
        
        // 加载节点选项
        await loadNodeOptions(schemeRes.data.schemeConfig)
      }
    } catch (error) {
      console.error('加载流程定义关联数据失败:', error)
    }
  } else {
    // 清空节点选项
    nodeOptions.value = []
  }
})

// 切换全屏
const toggleFullscreen = () => {
  isFullscreen.value = !isFullscreen.value
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    // 验证数据一致性
    if (!validateDataConsistency()) {
      return
    }
    
    // 更新表单数据到formState
    updateFormDataToState()
    
    const data: TaktInstanceCreate = {
      schemeId: formState.schemeId,
      instanceTitle: formState.instanceTitle,
      businessKey: formState.businessKey,
      initiatorId: formState.initiatorId,
      currentNodeId: formState.currentNodeId,
      currentNodeName: formState.currentNodeName,
      previousNodeId: formState.previousNodeId,
      previousNodeName: formState.previousNodeName,
      priority: formState.priority,
      urgency: formState.urgency,
      startTime: formState.startTime,
      endTime: formState.endTime,
      variables: formState.variables,
      formId: formState.formId,
      formType: formState.formType,
      formData: formState.formData,
      status: formState.status,
      remark: formState.remark
    }
    let res
    if (props.instanceId) {
      const updateData: TaktInstanceUpdate = {
        ...data,
        instanceId: props.instanceId
      }
      res = await updateInstance(updateData)
    } else {
      res = await createInstance(data)
    }
    if (res) {
      message.success(t('common.success'))
      resetForm()
      emit('success')
      handleVisibleChange(false)
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 加载选项数据
const loadOptions = async () => {
  try {
    const [schemeRes, formRes, userRes] = await Promise.all([
      getSchemeOptions(),
      getFormOptions(),
      getUserOptions()
    ])
    
    // 通用转换函数：从 TaktSelectOption 转换为 { label, value } 格式（与UserForm.vue保持一致）
    const convertToOption = (item: any) => ({
      label: item.dictLabel,
      value: item.dictValue
    })
    
    schemeOptions.value = (schemeRes.data || []).map(convertToOption)
    formOptions.value = (formRes.data || []).map(convertToOption)
    userOptions.value = (userRes.data || []).map(convertToOption)
  } catch (error) {
    console.error('加载选项数据失败:', error)
    message.error('加载选项数据失败')
  }
}

// 验证数据一致性
const validateDataConsistency = (): boolean => {
  // 验证流程定义和表单的关联性
  if (formState.schemeId && formState.schemeId !== '0' && formState.formId && formState.formId !== '0') {
    // 这里可以添加更复杂的验证逻辑
    // 例如：验证表单类型是否与流程定义兼容
    console.log('[InstanceForm] 数据一致性验证通过')
    return true
  }
  
  // 如果流程定义已选择但表单未选择，给出提示
  if (formState.schemeId && formState.schemeId !== '0' && (!formState.formId || formState.formId === '0')) {
    message.warning('请选择关联的表单定义')
    return false
  }
  
  return true
}

// 取消
const handleVisibleChange = (value: boolean) => {
  emit('update:open', value)
  if (!value) {
    resetForm()
  }
}
</script>

<style lang="less" scoped>
.form-tabs {
  :deep(.ant-tabs-content) {
    height: calc(100vh - 200px);
    overflow-y: auto;
  }
}

.workflow-config-section {
  margin-bottom: 24px;
  
  h4 {
    margin-bottom: 16px;
    color: #1890ff;
    font-weight: 500;
  }
  
  .workflow-canvas {
    border: 1px solid #d9d9d9;
    border-radius: 6px;
    overflow: hidden;
  }
}

.variables-section,
.form-data-section {
  margin-bottom: 24px;
  
  h4 {
    margin-bottom: 16px;
    color: #1890ff;
    font-weight: 500;
  }
}

.form-editor {
  border: 1px solid #d9d9d9;
  border-radius: 6px;
  padding: 16px;
  background-color: #fafafa;
}
</style> 
