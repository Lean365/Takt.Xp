<!--
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: LogicFlow 流程设计器主组件（基于 OpenAuth.Net CreatedFlow，使用 LogicFlow 原生组件和插件）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
-->
<template>
  <div style="height: 100%;">
    <a-layout class="container">
      <!-- 左侧工具栏 -->
      <a-layout-sider width="200" theme="light" class="select-area" v-if="!isShowContent">
        <div ref="dndPanel" style="width: 100%; height: 100%; padding: 8px;">
          <a-space direction="vertical" style="width: 100%;">
            <!-- 基础节点 -->
            <a-divider orientation="left" style="margin: 8px 0;">基础节点</a-divider>
            <a-space direction="vertical" style="width: 100%;" size="small">
              <div v-for="(node, index) in commonNodes" :key="index" class="node-item"
                :data-type="getNodeType(node.type)">
                <component :is="getIconComponent(node.icon)" style="font-size: 16px; margin-right: 4px;" />
                {{ node.name }}
              </div>
            </a-space>

            <!-- 高级节点 -->
            <a-divider orientation="left" style="margin: 8px 0;">高级节点</a-divider>
            <a-space direction="vertical" style="width: 100%;" size="small">
              <div v-for="(node, index) in highNodes" :key="index" class="node-item"
                :data-type="getNodeType(node.type)">
                <component :is="getIconComponent(node.icon)" style="font-size: 16px; margin-right: 4px;" />
                {{ node.name }}
              </div>
            </a-space>

            <!-- 操作按钮 -->
            <a-divider orientation="left" style="margin: 8px 0;">操作</a-divider>
            <a-space direction="vertical" style="width: 100%;" size="small">
              <a-popconfirm title="确认要清空画布吗？" @confirm="clear">
                <template #icon>
                  <QuestionCircleOutlined />
                </template>
                <a-button block>
                  <template #icon>
                    <ReloadOutlined />
                  </template>
                  清空画布
                </a-button>
              </a-popconfirm>
              <a-button block @click="saveFlow">
                <template #icon>
                  <SaveOutlined />
                </template>
                保存流程
              </a-button>
            </a-space>
          </a-space>
        </div>
      </a-layout-sider>

      <!-- 中间画布区域 -->
      <a-layout-content style="padding: 0;">
        <div class="content">
          <!-- LogicFlow 画布容器 -->
          <div id="logicflow-container" ref="lfContainer" class="logicflow-container"></div>
        </div>
      </a-layout-content>

      <!-- 右侧属性面板 -->
      <a-layout-sider width="300" theme="light" class="attr-area" v-if="isShowAttr && !isShowContent">
        <flow-attr :lf="lf" :currentSelect="currentSelect" :formTemplate="formTemplate"></flow-attr>
      </a-layout-sider>
    </a-layout>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch, nextTick, onBeforeUnmount } from 'vue'
import LogicFlow from '@logicflow/core'
import '@logicflow/core/dist/style/index.css'
import { Menu, SelectionSelect, Control, MiniMap, Snapshot, DndPanel } from '@logicflow/extension'
import '@logicflow/extension/lib/style/index.css'
import { commonNodes, highNodes, nodeDefaultConfig } from './config/node-config'
import { logicFlowConfig, themeConfig } from './config/flow-config'
import { registerNodes } from './util/node-register'
import FlowAttr from './modules/FlowAttr.vue'
import { message } from 'ant-design-vue'
import type { LogicFlow as LogicFlowType } from '@logicflow/core'
import {
  PlayCircleOutlined,
  StopOutlined,
  ToolOutlined,
  ForkOutlined,
  MergeCellsOutlined,
  TeamOutlined,
  ReloadOutlined,
  QuestionCircleOutlined,
  SaveOutlined
} from '@ant-design/icons-vue'

interface Props {
  schemeContent?: string
  isEdit?: boolean
  formTemplate?: any[]
  isShowContent?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  schemeContent: '',
  isEdit: false,
  formTemplate: () => [],
  isShowContent: false
})

const emit = defineEmits<{
  (e: 'update:schemeContent', value: string): void
  (e: 'save', value: string): void
}>()

const lfContainer = ref<HTMLElement | null>(null)
const lf = ref<LogicFlowType | null>(null)
const currentSelect = ref<any>({})
const isShowAttr = ref(false)
const dndPanel = ref<HTMLElement | null>(null)

// 图标组件映射
const iconMap: Record<string, any> = {
  PlayCircleOutlined,
  StopOutlined,
  ToolOutlined,
  ForkOutlined,
  MergeCellsOutlined,
  TeamOutlined,
  ReloadOutlined,
  QuestionCircleOutlined,
  SaveOutlined
}

const getIconComponent = (iconName: string) => {
  return iconMap[iconName] || ToolOutlined
}

// 初始化 LogicFlow
const initLogicFlow = () => {
  if (!lfContainer.value) return

  const config = {
    ...logicFlowConfig,
    container: lfContainer.value
  }

  lf.value = new LogicFlow(config)

  // 注册自定义节点
  if (lf.value) {
    registerNodes(lf.value as LogicFlowType)
  }

  // 注册插件
  lf.value.use(Menu)
  lf.value.use(SelectionSelect)
  lf.value.use(Control)
  lf.value.use(MiniMap)
  lf.value.use(Snapshot)

  // 注册 DndPanel 插件（用于拖拽）
  if (dndPanel.value) {
    lf.value.use(DndPanel, {
      panelEl: dndPanel.value,
      dragText: '拖拽到画布',
      dropEdge: false
    })
  }

  // 设置主题（基于官方 API 文档）
  // @see https://logicflow.cn/api/theme
  lf.value.setTheme(themeConfig)

  // 事件监听（基于官方 API 文档）
  // @see https://logicflow.cn/api/event-center
  // 监听节点点击事件
  lf.value.on('node:click', ({ data }: any) => {
    currentSelect.value = data
    isShowAttr.value = true
  })

  // 监听边点击事件
  lf.value.on('edge:click', ({ data }: any) => {
    currentSelect.value = { ...data, type: 'edge' }
    isShowAttr.value = true
  })

  // 监听画布点击事件
  lf.value.on('blank:click', () => {
    currentSelect.value = {}
    isShowAttr.value = false
  })

  // 监听节点删除事件
  lf.value.on('node:delete', () => {
    currentSelect.value = {}
    isShowAttr.value = false
  })

  // 监听边删除事件
  lf.value.on('edge:delete', () => {
    currentSelect.value = {}
    isShowAttr.value = false
  })

  // 监听节点拖拽事件
  lf.value.on('node:drag', ({ data }: any) => {
    // 节点拖拽时的处理
    console.log('节点拖拽:', data)
  })

  // 监听连接创建事件
  lf.value.on('connection:not-allowed', ({ msg }: any) => {
    message.warning(msg || '连接不允许')
  })

  // 如果有传入的流程数据，加载它
  if (props.schemeContent) {
    loadFlowData(props.schemeContent)
  } else {
    // 渲染空画布
    lf.value.render({ nodes: [], edges: [] })
  }
}

// 获取节点类型（映射到 LogicFlow 注册的类型）
const getNodeType = (type: string): string => {
  const typeMap: Record<string, string> = {
    start: 'start',
    end: 'end',
    node: 'task',
    gateway: 'gateway',
    fork: 'fork',
    join: 'join',
    multiInstance: 'multiInstance'
  }
  return typeMap[type] || 'task'
}

// 加载流程数据
const loadFlowData = (content: string | object) => {
  try {
    const data = typeof content === 'string' ? JSON.parse(content) : content
    if (data && lf.value) {
      // 转换 OpenAuth.Net 格式到 LogicFlow 格式
      const lfData = convertToLogicFlowFormat(data)
      lf.value.render(lfData)
    }
  } catch (error) {
    console.error('加载流程数据失败', error)
    message.error('加载流程数据失败')
  }
}

// 转换 OpenAuth.Net 格式到 LogicFlow 格式
const convertToLogicFlowFormat = (data: any) => {
  const nodes = (data.nodes || []).map((node: any) => {
    const nodeType = getNodeType(node.type)
    return {
      id: node.id,
      type: nodeType,
      x: node.left || node.x || 100,
      y: node.top || node.y || 100,
      text: node.name || node.text || '',
      properties: {
        ...(nodeDefaultConfig[node.type]?.properties || nodeDefaultConfig.node.properties),
        ...node.setInfo,
        originalType: node.type
      }
    }
  })

  const edges = (data.lines || []).map((line: any) => {
    return {
      id: line.id,
      type: 'polyline',
      sourceNodeId: line.from,
      targetNodeId: line.to,
      text: line.label || line.name || '',
      properties: {
        ...line.Compares,
        originalType: 'line'
      }
    }
  })

  return { nodes, edges }
}

// 转换 LogicFlow 格式到 OpenAuth.Net 格式
const convertToOpenAuthFormat = () => {
  if (!lf.value) return null

  const graphData = lf.value.getGraphData() as any
  const nodes = (graphData.nodes || []).map((node: any) => {
    const originalType = node.properties?.originalType || node.type
    return {
      id: node.id,
      type: originalType,
      name: node.text?.value || node.text || '',
      left: node.x,
      top: node.y,
      width: node.width || 120,
      height: node.height || 60,
      setInfo: {
        NodeRejectType: node.properties?.NodeRejectType || 0,
        NodeConfluenceType: node.properties?.NodeConfluenceType || '',
        NodeDesignate: node.properties?.NodeDesignate || '',
        NodeDesignateData: node.properties?.NodeDesignateData || {
          users: [],
          roles: [],
          Texts: ''
        }
      }
    }
  })

  const lines = (graphData.edges || []).map((edge: any) => {
    return {
      id: edge.id,
      from: edge.sourceNodeId,
      to: edge.targetNodeId,
      label: edge.text?.value || edge.text || '',
      name: edge.text?.value || edge.text || '',
      Compares: edge.properties?.Compares || []
    }
  })

  return {
    nodes,
    lines,
    attr: {
      id: graphData.id || 'flow-' + Date.now()
    },
    config: {
      showGrid: true,
      showGridText: '隐藏网格',
      showGridIcon: 'EyeOutlined'
    },
    status: 'save',
    remarks: []
  }
}

// 清空画布
const clear = () => {
  if (lf.value) {
    lf.value.clearData()
    currentSelect.value = {}
    isShowAttr.value = false
    message.success('画布已清空')
  }
}

// 保存流程
const saveFlow = () => {
  if (!lf.value) return

  const data = convertToOpenAuthFormat()
  if (data && data.nodes.length > 0) {
    const jsonData = JSON.stringify(data)
    message.success('保存流程成功！')
    emit('save', jsonData)
    return jsonData
  } else {
    message.error('流程图中无任何节点！')
    return null
  }
}

// 监听 schemeContent 变化
watch(
  () => props.schemeContent,
  (val: string | undefined) => {
    if (val && props.isEdit && lf.value) {
      loadFlowData(val)
    }
  }
)

onMounted(() => {
  nextTick(() => {
    initLogicFlow()
  })
})

onBeforeUnmount(() => {
  if (lf.value) {
    lf.value = null
  }
})

// 暴露方法给父组件
defineExpose({
  saveFlow,
  clear,
  getGraphData: () => lf.value?.getGraphData(),
  getGraphRawData: () => convertToOpenAuthFormat()
})
</script>

<style lang="scss">
@import './style/flow-designer.scss';

.logicflow-container {
  width: 100%;
  height: 100%;
  position: relative;
}

.node-item {
  background: #f4f6fc;
  height: 32px;
  line-height: 32px;
  padding: 0 8px;
  text-align: left;
  cursor: move;
  border-radius: 4px;
  display: flex;
  align-items: center;
  transition: all 0.3s;

  &:hover {
    color: #1890ff;
    outline: 1px dashed #1890ff;
    background: #e6f7ff;
  }
}
</style>
