<template>
  <a-modal
    v-model:open="visible"
    title="发起流程"
    width="90%"
    :footer="null"
    @cancel="handleCancel"
  >
    <div class="workflow-launch-container">
    <!-- 顶部标签页 -->
    <div class="launch-tabs">
      <a-tabs v-model:activeKey="activeTab" type="card">
        <a-tab-pane key="template" tab="选择模板">
          <div class="tab-content">
            <div class="template-selection">
              <div class="template-list">
                <div 
                  v-for="template in schemeOptions" 
                  :key="template.dictValue"
                  class="template-item"
                  :class="{ active: selectedSchemeId === template.dictValue }"
                  @click="selectTemplate(template)"
                >
                  <div class="template-name">{{ template.dictLabel }}</div>
                  <div class="template-desc">{{ template.extLabel || '暂无描述' }}</div>
                </div>
              </div>
            </div>
          </div>
        </a-tab-pane>
        
        <a-tab-pane key="form" tab="填写审批内容" :disabled="!selectedSchemeId">
          <div class="tab-content">
            <div class="form-container">
              <div class="form-header">
                <h3>{{ selectedSchemeName }}</h3>
                <p>{{ selectedSchemeDescription }}</p>
              </div>
              
              <div class="form-content">
                <Takt-form
                  v-if="formConfig"
                  :model-value="formConfig"
                  :readonly="false"
                  @submit="handleFormSubmit"
                  @change="handleFormChange"
                />
                <div v-else class="no-form">
                  <a-empty description="请先选择流程模板" />
                </div>
              </div>
            </div>
          </div>
        </a-tab-pane>
        
        <a-tab-pane key="flowchart" tab="全景流程图" :disabled="!selectedSchemeId">
          <div class="tab-content">
            <div class="flowchart-container">
              <div v-if="workflowConfig" class="flowchart-content">
                <Takt-flow 
                  v-model:value="workflowConfig" 
                  :width="800" 
                  :height="600"
                  :readonly="true"
                />
              </div>
              <div v-else class="no-flowchart">
                <a-empty description="暂无流程图数据" />
              </div>
            </div>
          </div>
        </a-tab-pane>
      </a-tabs>
    </div>

    <!-- 底部操作按钮 -->
    <div class="launch-actions">
      <a-space>
        <a-button 
          v-if="activeTab === 'template'"
          type="primary" 
          :disabled="!selectedSchemeId"
          @click="nextStep"
        >
          下一步
        </a-button>
        <a-button 
          v-if="activeTab === 'form'"
          type="primary" 
          :loading="submitting"
          @click="handleSubmit"
        >
          提交申请
        </a-button>
        <a-button 
          v-if="activeTab === 'form'"
          @click="saveDraft"
        >
          保存草稿
        </a-button>
        <a-button @click="handleCancel">
          取消
        </a-button>
      </a-space>
    </div>
    </div>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktSelectOption } from '@/types/common'
import type { TaktWorkflowStart } from '@/types/workflow/engine'
import { getSchemeOptions, getSchemeById } from '@/api/workflow/scheme'
import { getFormById } from '@/api/workflow/form'
import { startWorkflow } from '@/api/workflow/engine'
import { useUserStore } from '@/stores/userStore'

// 国际化
const { t } = useI18n()

// 用户状态
const userStore = useUserStore()

// Props
interface Props {
  visible: boolean
}

const props = defineProps<Props>()

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  'success': []
}>()

// 响应式数据
const activeTab = ref('template')
const selectedSchemeId = ref('')
const selectedSchemeName = ref('')
const selectedSchemeDescription = ref('')
const schemeOptions = ref<TaktSelectOption[]>([])
const formConfig = ref<string>('')
const workflowConfig = ref<any>(null)
const formData = ref<any>({})
const submitting = ref(false)

// 计算属性
const visible = computed({
  get: () => props.visible,
  set: (value) => emit('update:visible', value)
})

// 获取流程方案选项
const fetchSchemeOptions = async () => {
  try {
    const response = await getSchemeOptions()
    if (response.code === 200) {
      schemeOptions.value = response.data || []
    } else {
      message.error(response.msg || '获取流程模板失败')
    }
  } catch (error) {
    console.error('获取流程模板失败:', error)
    message.error('获取流程模板失败')
  }
}

// 选择流程模板
const selectTemplate = async (template: TaktSelectOption) => {
  selectedSchemeId.value = template.dictValue as string
  selectedSchemeName.value = template.dictLabel
  selectedSchemeDescription.value = template.extLabel || ''
  
  // 获取流程方案详情
  try {
    const response = await getSchemeById(template.dictValue as string)
    if (response.code === 200) {
      const scheme = response.data
      
      // 获取关联的表单配置
      if (scheme.formId) {
        const formResponse = await getFormById(scheme.formId)
        if (formResponse.code === 200) {
          formConfig.value = formResponse.data.formConfig || ''
        }
      }
      
      // 获取工作流配置
      if (scheme.schemeConfig) {
        try {
          workflowConfig.value = JSON.parse(scheme.schemeConfig)
        } catch (error) {
          console.error('解析工作流配置失败:', error)
        }
      }
    }
  } catch (error) {
    console.error('获取流程方案详情失败:', error)
    message.error('获取流程方案详情失败')
  }
}

// 下一步
const nextStep = () => {
  if (!selectedSchemeId.value) {
    message.warning('请先选择流程模板')
    return
  }
  activeTab.value = 'form'
}

// 表单提交
const handleFormSubmit = (data: any) => {
  formData.value = data
  handleSubmit()
}

// 表单变化
const handleFormChange = (data: any) => {
  formData.value = data
}

// 提交申请
const handleSubmit = async () => {
  if (!selectedSchemeId.value) {
    message.warning('请先选择流程模板')
    return
  }
  
  try {
    submitting.value = true
    
    const workflowStart: TaktWorkflowStart = {
      schemeId: selectedSchemeId.value,
      instanceTitle: selectedSchemeName.value,
      businessKey: `WF_${Date.now()}`,
      initiatorId: userStore.userInfo?.userId || '',
      variables: JSON.stringify(formData.value)
    }
    
    const response = await startWorkflow(workflowStart)
    if (response.code === 200) {
      message.success('流程发起成功')
      emit('success')
      handleCancel()
    } else {
      message.error(response.msg || '流程发起失败')
    }
  } catch (error) {
    console.error('流程发起失败:', error)
    message.error('流程发起失败')
  } finally {
    submitting.value = false
  }
}

// 保存草稿
const saveDraft = () => {
  message.info('草稿保存功能待实现')
}

// 取消
const handleCancel = () => {
  visible.value = false
  resetForm()
}

// 重置表单
const resetForm = () => {
  activeTab.value = 'template'
  selectedSchemeId.value = ''
  selectedSchemeName.value = ''
  selectedSchemeDescription.value = ''
  formConfig.value = ''
  workflowConfig.value = null
  formData.value = {}
}

// 组件挂载时获取数据
onMounted(() => {
  fetchSchemeOptions()
})
</script>

<style scoped>
.workflow-launch-container {
  height: 100vh;
  display: flex;
  flex-direction: column;
  background: #f5f5f5;
}

.launch-tabs {
  flex: 1;
  padding: 16px;
  overflow: hidden;
}

.tab-content {
  height: 100%;
  overflow: auto;
}

.template-selection {
  height: 100%;
}

.template-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 16px;
  padding: 16px 0;
}

.template-item {
  padding: 16px;
  background: white;
  border: 2px solid #e8e8e8;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s;
}

.template-item:hover {
  border-color: #1890ff;
  box-shadow: 0 2px 8px rgba(24, 144, 255, 0.2);
}

.template-item.active {
  border-color: #1890ff;
  background: #f0f8ff;
}

.template-name {
  font-size: 16px;
  font-weight: 500;
  color: #262626;
  margin-bottom: 8px;
}

.template-desc {
  font-size: 14px;
  color: #8c8c8c;
}

.form-container {
  height: 100%;
  display: flex;
  flex-direction: column;
}

.form-header {
  padding: 16px;
  background: white;
  border-bottom: 1px solid #e8e8e8;
  margin-bottom: 16px;
}

.form-header h3 {
  margin: 0 0 8px 0;
  font-size: 18px;
  color: #262626;
}

.form-header p {
  margin: 0;
  color: #8c8c8c;
}

.form-content {
  flex: 1;
  padding: 0 16px;
  overflow: auto;
}

.no-form,
.no-flowchart {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 400px;
}

.flowchart-container {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.flowchart-content {
  background: white;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.launch-actions {
  padding: 16px;
  background: white;
  border-top: 1px solid #e8e8e8;
  text-align: right;
}
</style>

