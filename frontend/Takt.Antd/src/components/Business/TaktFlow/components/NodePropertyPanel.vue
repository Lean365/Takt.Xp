<template>
  <a-drawer
    :open="visible"
    title="节点属性"
    placement="right"
    width="480"
    @close="onCancel"
    destroyOnClose
  >
    <a-form :model="form" :rules="rules" ref="formRef" layout="vertical">
      <a-form-item label="节点ID" name="nodeId">
        <a-input v-model:value="form.nodeId" disabled />
      </a-form-item>
      <a-form-item label="节点名称" name="nodeName" required>
        <a-input v-model:value="form.nodeName" placeholder="请输入节点名称" />
      </a-form-item>
      <a-form-item label="节点类型" name="nodeType" required>
        <Takt-select
          v-model:value="form.nodeType"
          dict-type="workflow_node_type"
          placeholder="请选择节点类型"
          type="radio"
          :show-all="false"
        />
      </a-form-item>
      <a-form-item label="父节点ID" name="parentNodeId">
        <a-input-number v-model:value="form.parentNodeId" style="width: 100%" />
      </a-form-item>
      <a-form-item label="节点配置" name="nodeConfig">
        <div class="node-config-container">
          <a-tabs v-model:activeKey="activeConfigTab">
            <a-tab-pane key="simple" tab="简单配置">
              <div class="key-value-list">
                <div v-for="(item, index) in configItems" :key="index" class="key-value-item">
                  <a-input 
                    v-model:value="item.key" 
                    placeholder="配置键" 
                    style="width: 40%; margin-right: 8px;"
                    @change="updateNodeConfig"
                  />
                  <a-input 
                    v-model:value="item.value" 
                    placeholder="配置值" 
                    style="width: 40%; margin-right: 8px;"
                    @change="updateNodeConfig"
                  />
                  <a-button 
                    type="text" 
                    danger 
                    @click="removeConfigItem(index)"
                    style="width: 10%"
                  >
                    <template #icon><DeleteOutlined /></template>
                  </a-button>
                </div>
                <a-button 
                  type="dashed" 
                  block 
                  @click="addConfigItem"
                  style="margin-top: 8px;"
                >
                  <PlusOutlined /> 添加配置项
                </a-button>
              </div>
            </a-tab-pane>
            <a-tab-pane key="json" tab="JSON编辑">
              <a-textarea 
                v-model:value="jsonConfig" 
                placeholder="请输入JSON格式的节点配置"
                :rows="8"
                @change="updateFromJson"
              />
            </a-tab-pane>
          </a-tabs>
        </div>
      </a-form-item>
      <a-form-item label="排序号" name="orderNum">
        <a-input-number v-model:value="form.orderNum" style="width: 100%" />
      </a-form-item>
      <a-form-item label="状态" name="status" required>
        <Takt-select
          v-model:value="form.status"
          dict-type="workflow_node_status"
          placeholder="请选择状态"
          type="radio"
          :show-all="false"
        />
      </a-form-item>
      <a-form-item label="开始时间" name="startTime">
        <a-date-picker v-model:value="form.startTime" show-time style="width: 100%" />
      </a-form-item>
      <a-form-item label="结束时间" name="endTime">
        <a-date-picker v-model:value="form.endTime" show-time style="width: 100%" />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-input v-model:value="form.remark" placeholder="请输入备注" />
      </a-form-item>
    </a-form>
    <template #footer>
      <div style="text-align:right;">
        <a-button style="margin-right: 8px;" @click="onCancel">取消</a-button>
        <a-button type="primary" @click="handleOk">保存</a-button>
      </div>
    </template>
  </a-drawer>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits, onMounted, computed } from 'vue'
import type { TaktNode } from '@/types/workflow/node'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { SelectValue } from 'ant-design-vue/es/select'
import { DeleteOutlined, PlusOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dictStore'

const props = defineProps<{
  visible: boolean
  node: Partial<TaktNode> | null
  onSave: (data: Partial<TaktNode>) => void
  onCancel: () => void
}>()
const emit = defineEmits(['update:visible'])

const formRef = ref<FormInstance>()
const form = ref<Partial<TaktNode>>({})
const dictStore = useDictStore()

const rules: Record<string, Rule[]> = {
  nodeName: [{ required: true, message: '请输入节点名称', trigger: 'blur' }],
  nodeType: [{ required: true, message: '请选择节点类型', trigger: 'change' }],
  status: [{ required: true, message: '请选择状态', trigger: 'change' }]
}

const activeConfigTab = ref('simple')
const configItems = ref<{ key: string; value: string }[]>([])
const jsonConfig = ref('')

// 初始化字典数据
onMounted(() => {
  dictStore.loadDicts(['workflow_node_type', 'workflow_node_status'])
})

// 根据节点类型设置默认配置
const setDefaultConfigByNodeType = () => {
  if (!form.value.nodeType) return
  
  let defaultConfig = {}
  
  switch (form.value.nodeType) {
    case 1: // 开始节点
      defaultConfig = {
        type: 'start',
        name: '开始节点',
        description: '流程开始节点',
        autoStart: true
      }
      break
    case 2: // 任务节点
      defaultConfig = {
        type: 'task',
        name: '任务节点',
        description: '用户任务节点',
        assigneeType: 'user',
        formType: 'dynamic'
      }
      break
    case 3: // 网关节点
      defaultConfig = {
        type: 'gateway',
        name: '网关节点',
        description: '条件分支节点',
        gatewayType: 'exclusive'
      }
      break
    case 4: // 结束节点
      defaultConfig = {
        type: 'end',
        name: '结束节点',
        description: '流程结束节点',
        autoComplete: true
      }
      break
  }
  
  // 如果当前没有配置或配置为空，则设置默认配置
  if (!form.value.nodeConfig || form.value.nodeConfig === '{}') {
    form.value.nodeConfig = JSON.stringify(defaultConfig, null, 2)
    jsonConfig.value = JSON.stringify(defaultConfig, null, 2)
    configItems.value = Object.entries(defaultConfig).map(([key, value]) => ({
      key,
      value: String(value)
    }))
  }
}

// 初始化配置项
const initConfigItems = () => {
  if (form.value.nodeConfig) {
    try {
      const config = JSON.parse(form.value.nodeConfig)
      configItems.value = Object.entries(config).map(([key, value]) => ({
        key,
        value: String(value)
      }))
      jsonConfig.value = JSON.stringify(config, null, 2)
    } catch (error) {
      // 如果解析失败，清空配置
      configItems.value = []
      jsonConfig.value = ''
    }
  } else {
    configItems.value = []
    jsonConfig.value = ''
  }
}

const handleOk = () => {
  formRef.value?.validate().then(() => {
    props.onSave({ ...form.value })
  })
}

const addConfigItem = () => {
  configItems.value.push({ key: '', value: '' })
  updateNodeConfig()
}

const removeConfigItem = (index: number) => {
  configItems.value.splice(index, 1)
  updateNodeConfig()
}

const updateNodeConfig = () => {
  // 过滤掉空的键值对
  const validItems = configItems.value.filter(item => item.key.trim() !== '')
  const config = validItems.reduce((acc, item) => {
    acc[item.key.trim()] = item.value.trim()
    return acc
  }, {} as Record<string, string>)
  
  form.value.nodeConfig = JSON.stringify(config)
  jsonConfig.value = JSON.stringify(config, null, 2)
}

const updateFromJson = () => {
  try {
    const config = JSON.parse(jsonConfig.value)
    form.value.nodeConfig = JSON.stringify(config)
    
    // 更新简单配置视图
    configItems.value = Object.entries(config).map(([key, value]) => ({
      key,
      value: String(value)
    }))
  } catch (error) {
    // JSON格式错误时不更新
    console.warn('JSON格式错误:', error)
  }
}

// 监听表单数据变化，初始化配置
watch(() => props.node, (val) => {
  if (val) {
    form.value = { ...val }
    // 自动赋值父节点ID
    if (!form.value.parentNodeId && val.parentNodeId) {
      form.value.parentNodeId = val.parentNodeId
    }
    // 节点类型映射
    const nodeTypeDict = dictStore.getDictOptions('workflow_node_type')
    const nodeTypeMap: Record<number, number> = {
      1: 1,
      2: 2,
      3: 3,
      4: 4,
      5: 5,
      6: 6,
    }
    form.value.nodeType = nodeTypeMap[val.nodeType as number] || val.nodeType
    // 状态映射
    const statusDict = dictStore.getDictOptions('workflow_node_status')
    const draftStatus = statusDict.find(item => item.label === '草稿')?.value || statusDict[0]?.value
    form.value.status = draftStatus as number
    initConfigItems()
    setDefaultConfigByNodeType()
  }
}, { immediate: true })

watch(() => props.visible, (val) => {
  if (val && props.node) {
    form.value = { ...props.node }
    // 自动赋值父节点ID
    if (!form.value.parentNodeId && props.node.parentNodeId) {
      form.value.parentNodeId = props.node.parentNodeId
    }
    // 节点类型映射
    const nodeTypeDict = dictStore.getDictOptions('workflow_node_type')
    const nodeTypeMap: Record<number, number> = {
      1: 1,
      2: 2,
      3: 3,
      4: 4,
      5: 5,
      6: 6,
    }
    form.value.nodeType = nodeTypeMap[props.node.nodeType as number] || props.node.nodeType
    // 状态映射
    const statusDict = dictStore.getDictOptions('workflow_node_status')
    const draftStatus = statusDict.find(item => item.label === '草稿')?.value || statusDict[0]?.value
    form.value.status = draftStatus as number
    initConfigItems()
    setDefaultConfigByNodeType()
  }
})
</script>

<style scoped>
.node-config-container {
  border: 1px solid #d9d9d9;
  border-radius: 6px;
  padding: 16px;
}

.key-value-list {
  max-height: 300px;
  overflow-y: auto;
}

.key-value-item {
  display: flex;
  align-items: center;
  margin-bottom: 8px;
  gap: 8px;
}

.key-value-item:last-child {
  margin-bottom: 0;
}

.key-value-item .ant-input {
  flex: 1;
}

.key-value-item .ant-btn {
  flex-shrink: 0;
}
</style> 
