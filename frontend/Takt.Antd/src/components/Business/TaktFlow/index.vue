<template>
  <div class="flow-root">
    <div class="flow-canvas-wrap">
      <div v-if="!readonly" class="node-palette">
        <a-tooltip title="开始" placement="right">
          <div 
            class="palette-item" 
            :class="{ 'disabled': nodeStatus.hasStart }"
            @mousedown="e => { console.log('[流程设计器] 开始节点mousedown事件'); startDrag('start', e) }"
          >
            <span class="palette-icon start-icon"></span>
          </div>
        </a-tooltip>
        <a-tooltip title="任务" placement="right">
          <div class="palette-item" @mousedown="e => { console.log('[流程设计器] 任务节点mousedown事件'); startDrag('task', e) }">
            <span class="palette-icon task-icon"></span>
          </div>
        </a-tooltip>
        <a-tooltip title="网关" placement="right">
          <div class="palette-item" @mousedown="e => { console.log('[流程设计器] 网关节点mousedown事件'); startDrag('gateway', e) }">
            <span class="palette-icon gateway-icon"></span>
          </div>
        </a-tooltip>
        <a-tooltip title="结束" placement="right">
          <div 
            class="palette-item" 
            :class="{ 'disabled': nodeStatus.hasEnd }"
            @mousedown="e => { console.log('[流程设计器] 结束节点mousedown事件'); startDrag('end', e) }"
          >
            <span class="palette-icon end-icon"></span>
          </div>
        </a-tooltip>
      </div>
      <div id="container" class="flow-canvas"></div>
      <NodePropertyPanel
        v-if="!readonly"
        :visible="propertyPanelVisible"
        :node="selectedNode"
        :onSave="handleNodePropertySave"
        :onCancel="handleNodePropertyCancel"
      />
      <div v-if="!readonly" class="canvas-ops-panel">
        <a-tooltip title="导入" placement="left">
          <button class="ops-btn" @click="handleImport">
            <span class="ops-icon import-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="导出" placement="left">
          <button class="ops-btn" @click="handleExport">
            <span class="ops-icon export-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="放大" placement="left">
          <button class="ops-btn" @click="handleZoomIn">
            <span class="ops-icon zoom-in-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="缩小" placement="left">
          <button class="ops-btn" @click="handleZoomOut">
            <span class="ops-icon zoom-out-icon"></span>
          </button>
        </a-tooltip>
        <a-tooltip title="重置" placement="left">
          <button class="ops-btn" @click="handleReset">
            <span class="ops-icon reset-icon"></span>
          </button>
        </a-tooltip>
      </div>
      <div
        v-if="contextMenu.visible && !readonly"
        class="context-menu"
        :style="{ left: contextMenu.x + 'px', top: contextMenu.y + 'px' }"
        @contextmenu.prevent
      >
        <template v-if="contextMenu.type === 'edge'">
          <div class="context-menu-item" @click="deleteEdge">删除</div>
        </template>
        <template v-else-if="contextMenu.type === 'node'">
          <div class="context-menu-item" @click="showNodeProps">属性</div>
          <div class="context-menu-item" @click="deleteNode">删除</div>
        </template>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, nextTick, watch } from 'vue'
import { Graph } from '@antv/x6'
import { Dnd } from '@antv/x6-plugin-dnd'
import NodePropertyPanel from './components/NodePropertyPanel.vue'
import { message } from 'ant-design-vue'

// Props 和 Emits 定义
const props = defineProps({
  value: { type: Object, default: () => ({}) },
  width: { type: Number, default: 1600 },
  height: { type: Number, default: 800 },
  readonly: { type: Boolean, default: false }
})

const emit = defineEmits(['update:value'])

const container = ref(null)
let graph = null
let dnd = null

// 属性面板相关
const propertyPanelVisible = ref(false)
const selectedNode = ref(null)

// 右键菜单相关
const contextMenu = ref({
  visible: false,
  x: 0,
  y: 0,
  type: '', // 'node' | 'edge'
  target: null
})

// 节点状态管理
const nodeStatus = ref({
  hasStart: false,
  hasEnd: false
})

// 内部流程数据
const localConfig = ref(props.value || {})

// 监听父传入的 value，变化时同步到本地
watch(() => props.value, (val) => {
  console.log('[流程设计器] 检测到value变化:', val)
  if (val && JSON.stringify(val) !== JSON.stringify(localConfig.value)) {
    console.log('[流程设计器] value内容发生变化，更新本地配置')
    localConfig.value = val
    // 如果有初始数据，加载到画布
    if (graph && val.nodes && val.edges) {
      console.log('[流程设计器] 重新加载工作流数据')
      loadWorkflowData(val)
    } else {
      console.log('[流程设计器] 数据不完整，无法加载:', { 
        hasGraph: !!graph, 
        hasNodes: !!(val && val.nodes), 
        hasEdges: !!(val && val.edges) 
      })
    }
  } else {
    console.log('[流程设计器] value未发生变化或为空')
  }
}, { deep: true })

// 检查节点状态
const checkNodeStatus = () => {
  if (!graph) return
  
  const nodes = graph.getNodes()
  nodeStatus.value.hasStart = nodes.some(node => 
    node.shape === 'circle' && node.attr('body/fill') === '#52c41a'
  )
  nodeStatus.value.hasEnd = nodes.some(node => 
    node.shape === 'circle' && node.attr('body/fill') === '#ff4d4f'
  )
}

const nodeTemplates = {
  start: {
    shape: 'circle',
    width: 60,
    height: 60,
    attrs: {
      body: {
        r: 30,
        fill: '#52c41a',
        stroke: '#389e0d',
        strokeWidth: 2
      },
      label: {
        text: '开始',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        out: {
          position: 'right',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#52c41a',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'out1', group: 'out' }
      ]
    }
  },
  task: {
    shape: 'rect',
    width: 100,
    height: 40,
    attrs: {
      body: {
        fill: '#1890ff',
        stroke: '#096dd9',
        strokeWidth: 2,
        rx: 8,
        ry: 8
      },
      label: {
        text: '任务',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        top: {
          position: 'top',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        right: {
          position: 'right',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        bottom: {
          position: 'bottom',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        left: {
          position: 'left',
          attrs: {
            circle: {
              r: 6,
              magnet: true,
              stroke: '#1890ff',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'top', group: 'top' },
        { id: 'right', group: 'right' },
        { id: 'bottom', group: 'bottom' },
        { id: 'left', group: 'left' }
      ]
    }
  },
  gateway: {
    shape: 'polygon',
    width: 80,
    height: 80,
    attrs: {
      body: {
        refPoints: '0,10 10,0 20,10 10,20',
        fill: '#faad14',
        stroke: '#d48806',
        strokeWidth: 2
      },
      label: {
        text: '网关',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        top: {
          position: 'top',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        right: {
          position: 'right',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        bottom: {
          position: 'bottom',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        },
        left: {
          position: 'left',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#faad14',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'top', group: 'top' },
        { id: 'right', group: 'right' },
        { id: 'bottom', group: 'bottom' },
        { id: 'left', group: 'left' }
      ]
    }
  },
  end: {
    shape: 'circle',
    width: 60,
    height: 60,
    attrs: {
      body: {
        r: 30,
        fill: '#ff4d4f',
        stroke: '#cf1322',
        strokeWidth: 2
      },
      label: {
        text: '结束',
        fill: '#fff',
        fontSize: 14,
        fontWeight: 'bold'
      }
    },
    ports: {
      groups: {
        in: {
          position: 'left',
          attrs: {
            circle: {
              r: 4,
              magnet: true,
              stroke: '#ff4d4f',
              strokeWidth: 2,
              fill: '#fff'
            }
          },
          markup: [
            { tagName: 'circle', selector: 'circle' }
          ]
        }
      },
      items: [
        { id: 'end-in', group: 'in' }
      ]
    }
  }
}

function startDrag(type, e) {
  console.log('[流程设计器] 开始拖拽:', type, 'readonly:', props.readonly, 'graph:', !!graph, 'dnd:', !!dnd)
  
  if (!graph || props.readonly) {
    console.log('[流程设计器] 拖拽被阻止: graph不存在或只读模式')
    return
  }

  // 唯一性校验：开始和结束节点只能有一个
  if (type === 'start' || type === 'end') {
    const exist = graph.getNodes().some(node => {
      // 根据节点形状判断类型
      if (type === 'start') {
        // 开始节点是绿色圆形
        return node.shape === 'circle' && 
               node.attr('body/fill') === '#52c41a'
      } else if (type === 'end') {
        // 结束节点是红色圆形
        return node.shape === 'circle' && 
               node.attr('body/fill') === '#ff4d4f'
      }
      return false
    })
    if (exist) {
      message.warning(`只能有一个${type === 'start' ? '开始' : '结束'}节点`)
      return
    }
  }

  const config = nodeTemplates[type]
  if (!config) {
    console.log('[流程设计器] 节点模板不存在:', type)
    return
  }
  
  const node = graph.createNode(config)
  console.log('[流程设计器] 创建节点:', node.id)
  
  // 如果Dnd插件存在，使用拖拽；否则直接添加到画布
  if (dnd) {
    console.log('[流程设计器] 使用Dnd拖拽')
  dnd.start(node, e)
  } else {
    console.log('[流程设计器] Dnd插件不存在，直接添加到画布')
    // 直接添加到画布中心位置
    const container = graph.container
    const rect = container.getBoundingClientRect()
    const x = e.clientX - rect.left
    const y = e.clientY - rect.top
    console.log('[流程设计器] 计算的位置:', { x, y, clientX: e.clientX, clientY: e.clientY, rect })
    node.position(x, y)
    graph.addNode(node)
  }
  
  // 更新节点状态
  checkNodeStatus()
}

function handleImport() {
  if (props.readonly) return
  
  // 创建文件输入元素
  const input = document.createElement('input')
  input.type = 'file'
  input.accept = '.json'
  input.style.display = 'none'
  
  input.onchange = (e) => {
    const file = e.target.files[0]
    if (!file) return
    
    const reader = new FileReader()
    reader.onload = (event) => {
      try {
        const data = JSON.parse(event.target.result)
        
        // 清空现有图形
        graph.clearCells()
        
        // 导入节点
        if (data.nodes && Array.isArray(data.nodes)) {
          data.nodes.forEach(nodeData => {
            const node = graph.addNode({
              id: nodeData.id,
              shape: nodeData.shape,
              x: nodeData.x,
              y: nodeData.y,
              width: nodeData.width,
              height: nodeData.height,
              attrs: nodeData.attrs,
              ports: nodeData.ports,
              data: nodeData.data || {}
            })
          })
        }
        
        // 导入边
        if (data.edges && Array.isArray(data.edges)) {
          data.edges.forEach(edgeData => {
            const edgeConfig = {
              id: edgeData.id,
              source: edgeData.source,
              target: edgeData.target,
              sourcePort: edgeData.sourcePort,
              targetPort: edgeData.targetPort,
              attrs: edgeData.attrs || {}
            }
            
            // 设置锚点信息 - 优先使用数据中的锚点配置
            if (edgeData.sourceAnchor) {
              edgeConfig.sourceAnchor = edgeData.sourceAnchor
            }
            if (edgeData.targetAnchor) {
              edgeConfig.targetAnchor = edgeData.targetAnchor
            }
            
            // 确保每个边都有完整的锚点配置
            // 如果没有源锚点且没有源端口，使用默认锚点
            if (!edgeConfig.sourceAnchor && !edgeData.sourcePort) {
              edgeConfig.sourceAnchor = 'right'
            }
            // 如果没有目标锚点且没有目标端口，使用默认锚点
            if (!edgeConfig.targetAnchor && !edgeData.targetPort) {
              edgeConfig.targetAnchor = 'left'
            }
            
            // 最终检查：确保锚点配置完整，防止X6报错
            if (!edgeConfig.sourceAnchor) {
              edgeConfig.sourceAnchor = 'right'
            }
            if (!edgeConfig.targetAnchor) {
              edgeConfig.targetAnchor = 'left'
            }
            
            try {
              graph.addEdge(edgeConfig)
            } catch (error) {
              console.error('导入边失败:', edgeConfig, error)
              // 如果添加失败，尝试使用默认配置
              const fallbackConfig = {
                id: edgeData.id,
                source: edgeData.source,
                target: edgeData.target,
                sourceAnchor: 'right',
                targetAnchor: 'left',
                attrs: edgeData.attrs || {}
              }
              graph.addEdge(fallbackConfig)
            }
          })
        }
        
        message.success('流程导入成功')
        console.log('[流程设计器] 导入数据:', data)
      } catch (error) {
        console.error('[流程设计器] 导入失败:', error)
        message.error('导入失败：文件格式错误')
      }
    }
    
    reader.readAsText(file)
  }
  
  document.body.appendChild(input)
  input.click()
  document.body.removeChild(input)
}

function handleExport() {
  if (!graph) {
    message.warning('没有可导出的数据')
    return
  }
  
  // 检查是否有节点
  const nodes = graph.getNodes()
  if (nodes.length === 0) {
    message.warning('空白流程不能导出')
    return
  }
  
  try {
    // 获取所有节点
    const nodeData = nodes.map(node => {
      try {
        return {
      id: node.id,
      shape: node.shape,
          x: node.getPosition ? node.getPosition().x : 0,
          y: node.getPosition ? node.getPosition().y : 0,
          width: node.getSize ? node.getSize().width : 100,
          height: node.getSize ? node.getSize().height : 40,
          attrs: node.getAttrs ? node.getAttrs() : {},
          ports: node.getPorts ? node.getPorts() : {},
          data: node.getData ? node.getData() : {}
        }
      } catch (error) {
        console.error('[流程设计器] 导出节点数据失败:', node, error)
        return {
          id: node.id,
          shape: node.shape || 'rect',
          x: 0,
          y: 0,
          width: 100,
          height: 40,
          attrs: {},
          ports: {},
          data: {}
        }
      }
    })
    
    // 获取所有边
    const edges = graph.getEdges().map(edge => {
      try {
        return {
      id: edge.id,
      source: edge.getSource(),
      target: edge.getTarget(),
          sourcePort: edge.getSourcePortId ? edge.getSourcePortId() : null,
          targetPort: edge.getTargetPortId ? edge.getTargetPortId() : null,
          sourceAnchor: edge.getSourceAnchor ? edge.getSourceAnchor() : null,
          targetAnchor: edge.getTargetAnchor ? edge.getTargetAnchor() : null,
          attrs: edge.getAttrs ? edge.getAttrs() : {}
        }
      } catch (error) {
        console.error('[流程设计器] 导出边数据失败:', edge, error)
        return {
          id: edge.id,
          source: edge.getSource ? edge.getSource() : null,
          target: edge.getTarget ? edge.getTarget() : null,
          sourcePort: null,
          targetPort: null,
          sourceAnchor: null,
          targetAnchor: null,
          attrs: {}
        }
      }
    })
    
    // 构建导出数据
    const exportData = {
      version: '1.0',
      timestamp: new Date().toISOString(),
      nodes: nodeData,
      edges,
      metadata: {
        nodeCount: nodeData.length,
        edgeCount: edges.length,
        exportTime: new Date().toLocaleString()
      }
    }
    
    // 创建下载链接
    const dataStr = JSON.stringify(exportData, null, 2)
    const dataBlob = new Blob([dataStr], { type: 'application/json' })
    const url = URL.createObjectURL(dataBlob)
    
    const link = document.createElement('a')
    link.href = url
    link.download = `workflow_${new Date().getTime()}.json`
    link.style.display = 'none'
    
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    
    URL.revokeObjectURL(url)
    
    message.success('流程导出成功')
    console.log('[流程设计器] 导出数据:', exportData)
  } catch (error) {
    console.error('[流程设计器] 导出失败:', error)
    message.error('导出失败：' + error.message)
  }
}

function handleZoomIn() {
  if (graph && !props.readonly) graph.zoom(0.1)
}
function handleZoomOut() {
  if (graph && !props.readonly) graph.zoom(-0.1)
}
function handleReset() {
  if (graph && !props.readonly) {
    // 清空画布
    graph.clearCells()
    // 重置缩放
    graph.zoomTo(1)
    graph.centerContent()
    // 重新创建默认流程
    createDefaultWorkflow()
    message.success('已重置为默认流程')
  }
}

function deleteEdge() {
  if (props.readonly) return
  if (contextMenu.value.target && graph) {
    graph.removeEdge(contextMenu.value.target)
  }
  contextMenu.value.visible = false
  updateWorkflowConfig()
}
function deleteNode() {
  if (props.readonly) return
  if (contextMenu.value.target && graph) {
    graph.removeNode(contextMenu.value.target)
    // 更新节点状态
    checkNodeStatus()
  }
  contextMenu.value.visible = false
  updateWorkflowConfig()
}
function showNodeProps() {
  if (props.readonly) return
  const node = contextMenu.value.target
  selectedNode.value = {
    nodeId: node.id,
    nodeName: node.attr('label/text'),
    ...node.getData()
  }
  propertyPanelVisible.value = true
  contextMenu.value.visible = false
}

function closeContextMenu() {
  contextMenu.value.visible = false
}

function handleNodePropertySave(data) {
  if (props.readonly) return
  if (graph && data && data.nodeId) {
    const node = graph.getCellById(data.nodeId)
    if (node) {
      node.setData({ ...data })
      if (data.nodeName) {
        node.attr('label/text', data.nodeName)
      }
    }
  }
  propertyPanelVisible.value = false
  updateWorkflowConfig()
}
function handleNodePropertyCancel() {
  propertyPanelVisible.value = false
}

function createDefaultWorkflow() {
  // 创建开始节点
  const startNode = graph.addNode({
    ...nodeTemplates.start,
    x: 100,
    y: 100
  })

  // 创建第一个任务节点
  const taskNode1 = graph.addNode({
    ...nodeTemplates.task,
    x: 250,
    y: 100
  })

  // 创建第一个网关节点
  const gatewayNode1 = graph.addNode({
    ...nodeTemplates.gateway,
    x: 400,
    y: 100
  })

  // 创建第二个任务节点
  const taskNode2 = graph.addNode({
    ...nodeTemplates.task,
    x: 550,
    y: 100
  })

  // 创建第二个网关节点
  const gatewayNode2 = graph.addNode({
    ...nodeTemplates.gateway,
    x: 700,
    y: 100
  })

  // 创建结束节点
  const endNode = graph.addNode({
    ...nodeTemplates.end,
    x: 850,
    y: 100
  })

  // 创建边：开始 → 任务1 → 网关1 → 任务2 → 网关2 → 结束
  try {
  graph.addEdge({
    source: startNode.id,
    target: taskNode1.id,
    sourcePort: 'out1',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: taskNode1.id,
    target: gatewayNode1.id,
    sourcePort: 'right',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: gatewayNode1.id,
    target: taskNode2.id,
    sourcePort: 'right',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: taskNode2.id,
    target: gatewayNode2.id,
    sourcePort: 'right',
    targetPort: 'left'
  })
  
  graph.addEdge({
    source: gatewayNode2.id,
    target: endNode.id,
    sourcePort: 'right',
    targetPort: 'end-in'
  })

  // 添加驳回路径：任务1 → 结束（驳回）
  graph.addEdge({
    source: taskNode1.id,
    target: endNode.id,
    sourcePort: 'bottom',
    targetPort: 'end-in'
  })
  } catch (error) {
    console.error('创建默认流程边失败:', error)
    // 如果端口连接失败，尝试使用锚点连接
    try {
      graph.addEdge({
        source: startNode.id,
        target: taskNode1.id,
        sourceAnchor: 'right',
        targetAnchor: 'left'
      })
      
      graph.addEdge({
        source: taskNode1.id,
        target: gatewayNode1.id,
        sourceAnchor: 'right',
        targetAnchor: 'left'
      })
      
      graph.addEdge({
        source: gatewayNode1.id,
        target: taskNode2.id,
        sourceAnchor: 'right',
        targetAnchor: 'left'
      })
      
      graph.addEdge({
        source: taskNode2.id,
        target: gatewayNode2.id,
        sourceAnchor: 'right',
        targetAnchor: 'left'
      })
      
      graph.addEdge({
        source: gatewayNode2.id,
        target: endNode.id,
        sourceAnchor: 'right',
        targetAnchor: 'left'
      })

      // 添加驳回路径：任务1 → 结束（驳回）
      graph.addEdge({
        source: taskNode1.id,
        target: endNode.id,
        sourceAnchor: 'bottom',
        targetAnchor: 'left'
      })
    } catch (fallbackError) {
      console.error('创建默认流程边失败（fallback）:', fallbackError)
    }
  }

  // 更新节点状态
  checkNodeStatus()
}

// 导出当前流程图数据
const exportCurrentWorkflowData = () => {
  if (!graph) return {}
  
  const nodes = graph.getNodes()
  const edges = graph.getEdges()
  
  // 导出简洁的节点数据，只保留必要信息
  const nodeData = nodes.map(node => {
    try {
      const position = node.getPosition ? node.getPosition() : { x: 0, y: 0 }
      const nodeData = node.getData ? node.getData() : {}
      
      return {
        id: node.id,
        type: nodeData.type || 'task', // 使用data中的type，默认为task
        x: position.x,
        y: position.y,
        data: {
          nodeName: nodeData.nodeName || node.id
        }
      }
    } catch (error) {
      console.error('[流程设计器] 导出节点数据失败:', node, error)
      return {
        id: node.id,
        type: 'task',
        x: 0,
        y: 0,
        data: {
          nodeName: node.id
        }
      }
    }
  })
  
  // 导出简洁的边数据，只保留必要信息
  const edgeData = edges.map(edge => {
    try {
      const source = edge.getSource()
      const target = edge.getTarget()
      
      return {
        id: edge.id,
        source: typeof source === 'object' ? source.cell : source,
        target: typeof target === 'object' ? target.cell : target,
        sourceAnchor: edge.getSourceAnchor ? edge.getSourceAnchor() : 'right',
        targetAnchor: edge.getTargetAnchor ? edge.getTargetAnchor() : 'left'
      }
    } catch (error) {
      console.error('[流程设计器] 导出边数据失败:', edge, error)
      return {
        id: edge.id,
        source: null,
        target: null,
        sourceAnchor: 'right',
        targetAnchor: 'left'
      }
    }
  })
  
  return {
    nodes: nodeData,
    edges: edgeData
  }
}

// 加载流程图数据到画布
const loadWorkflowData = (data) => {
  if (!graph || !data) return
  
  // 清空现有图形
  graph.clearCells()
  
  // 导入节点
  if (data.nodes && Array.isArray(data.nodes)) {
    console.log('[流程设计器] 开始加载节点:', data.nodes.length)
    data.nodes.forEach(nodeData => {
      // 使用模板配置，但覆盖标签文本
      const template = nodeTemplates[nodeData.type] || nodeTemplates.task
      const nodeConfig = {
        ...template,
        id: nodeData.id,
        x: nodeData.x || 100,
        y: nodeData.y || 100,
        data: nodeData.data || {}
      }
      
      // 如果节点数据中有自定义名称，覆盖模板中的标签
      if (nodeData.data && nodeData.data.nodeName) {
        nodeConfig.attrs = {
          ...nodeConfig.attrs,
          label: {
            ...nodeConfig.attrs.label,
            text: nodeData.data.nodeName
          }
        }
      }
      console.log('[流程设计器] 加载节点配置:', nodeConfig)
      graph.addNode(nodeConfig)
    })
  }
  
  // 导入边
  if (data.edges && Array.isArray(data.edges)) {
    data.edges.forEach(edgeData => {
      const edgeConfig = {
        id: edgeData.id,
        source: edgeData.source,
        target: edgeData.target,
        attrs: edgeData.attrs || {}
      }
      
      // 如果有标签信息，添加到边配置中
      if (edgeData.labels) {
        edgeConfig.labels = edgeData.labels
      }
      
      // 设置源端口和目标端口
      if (edgeData.sourcePort) {
        edgeConfig.sourcePort = edgeData.sourcePort
      }
      if (edgeData.targetPort) {
        edgeConfig.targetPort = edgeData.targetPort
      }
      
      // 设置锚点信息 - 这是X6必需的
      // 优先使用数据中的锚点配置
      if (edgeData.sourceAnchor) {
        edgeConfig.sourceAnchor = edgeData.sourceAnchor
      }
      if (edgeData.targetAnchor) {
        edgeConfig.targetAnchor = edgeData.targetAnchor
      }
      
      // 确保每个边都有完整的锚点配置
      // 如果没有源锚点且没有源端口，使用默认锚点
      if (!edgeConfig.sourceAnchor && !edgeData.sourcePort) {
        edgeConfig.sourceAnchor = 'right'
      }
      // 如果没有目标锚点且没有目标端口，使用默认锚点
      if (!edgeConfig.targetAnchor && !edgeData.targetPort) {
        edgeConfig.targetAnchor = 'left'
      }
      
      // 最终检查：确保锚点配置完整，防止X6报错
      if (!edgeConfig.sourceAnchor) {
        edgeConfig.sourceAnchor = 'right'
      }
      if (!edgeConfig.targetAnchor) {
        edgeConfig.targetAnchor = 'left'
      }
      
      console.log('[流程设计器] 加载边配置:', {
        id: edgeConfig.id,
        source: edgeConfig.source,
        target: edgeConfig.target,
        sourcePort: edgeConfig.sourcePort,
        targetPort: edgeConfig.targetPort,
        sourceAnchor: edgeConfig.sourceAnchor,
        targetAnchor: edgeConfig.targetAnchor,
        attrs: edgeConfig.attrs
      })
      
      try {
        graph.addEdge(edgeConfig)
      } catch (error) {
        console.error('[流程设计器] 添加边失败:', edgeConfig, error)
        // 如果添加失败，尝试使用默认配置
        const fallbackConfig = {
          id: edgeData.id,
          source: edgeData.source,
          target: edgeData.target,
          sourceAnchor: 'right',
          targetAnchor: 'left',
          attrs: edgeData.attrs || {}
        }
        try {
          graph.addEdge(fallbackConfig)
        } catch (fallbackError) {
          console.error('[流程设计器] 添加边失败（fallback）:', fallbackConfig, fallbackError)
        }
      }
    })
  }
  
  // 更新节点状态
  checkNodeStatus()
}

// 更新工作流配置并同步到父组件
const updateWorkflowConfig = () => {
  try {
  const data = exportCurrentWorkflowData()
  localConfig.value = data
  emit('update:value', data)
  } catch (error) {
    console.error('[流程设计器] 更新工作流配置失败:', error)
  }
}

onMounted(() => {
  console.log('[流程设计器] 开始初始化Graph, readonly:', props.readonly)
  
  graph = new Graph({
    container: document.getElementById('container'),
    autoResize: true,
    background: { color: '#f5f6fa' },
    grid: { size: 10, visible: true, type: 'dot' },
    panning: !props.readonly,
    mousewheel: !props.readonly,
    connecting: {
      sourceAnchor: 'right',
      targetAnchor: 'left',
      connectionPoint: 'boundary',
      allowBlank: false,
      allowLoop: false,
      highlight: !props.readonly,
      connector: 'rounded',
      router: {
        name: 'manhattan',
        args: {
          padding: 10
        }
      },
      enabled: !props.readonly
    },
    // 确保节点可以移动
    interacting: {
      nodeMovable: !props.readonly,
      edgeMovable: !props.readonly,
      edgeLabelMovable: !props.readonly,
      magnetConnectable: !props.readonly,
      magnetAdsorbed: !props.readonly
    },
    // 确保拖拽功能正常
    embedding: {
      enabled: false
    },
    // 确保选择功能正常
    selecting: {
      enabled: !props.readonly,
      multiple: true,
      rubberband: true,
      movable: true,
      showNodeSelectionBox: true
    }
  })
  
  console.log('[流程设计器] Graph初始化完成:', !!graph)
  
  // 初始化拖拽插件
  dnd = new Dnd({ target: graph })
  console.log('[流程设计器] Dnd插件初始化完成:', !!dnd)
  
  // 添加拖拽事件监听器用于调试
  if (dnd) {
    dnd.on('drag:start', (args) => {
      console.log('[流程设计器] 拖拽开始:', args)
    })
    dnd.on('drag:move', (args) => {
      console.log('[流程设计器] 拖拽移动:', args)
    })
    dnd.on('drag:end', (args) => {
      console.log('[流程设计器] 拖拽结束:', args)
    })
  }
  
  // 添加节点移动事件监听器
  graph.on('node:moved', (args) => {
    console.log('[流程设计器] 节点移动:', args)
  })
  
  // 添加节点添加事件监听器
  graph.on('node:added', (args) => {
    console.log('[流程设计器] 节点添加:', args)
  })
  
  // 添加拖拽相关事件监听器
  graph.on('node:dragstart', (args) => {
    console.log('[流程设计器] 节点拖拽开始:', args)
  })
  
  graph.on('node:drag', (args) => {
    console.log('[流程设计器] 节点拖拽中:', args)
  })
  
  graph.on('node:dragend', (args) => {
    console.log('[流程设计器] 节点拖拽结束:', args)
  })

  // 初始化节点状态
  checkNodeStatus()

  // 监听节点变化
  graph.on('node:added', () => {
    checkNodeStatus()
    // 延迟更新配置，避免频繁调用
    if (!props.readonly) {
      setTimeout(() => updateWorkflowConfig(), 100)
    }
  })
  
  graph.on('node:removed', () => {
    checkNodeStatus()
    // 延迟更新配置，避免频繁调用
    if (!props.readonly) {
      setTimeout(() => updateWorkflowConfig(), 100)
    }
  })

  // 监听边变化
  graph.on('edge:connected', () => {
    if (!props.readonly) {
      setTimeout(() => updateWorkflowConfig(), 100)
    }
  })

  graph.on('edge:disconnected', () => {
    if (!props.readonly) {
      setTimeout(() => updateWorkflowConfig(), 100)
    }
  })

  // 监听节点移动 - 使用防抖
  let moveTimeout = null
  graph.on('node:moved', () => {
    if (!props.readonly) {
      if (moveTimeout) {
        clearTimeout(moveTimeout)
      }
      moveTimeout = setTimeout(() => updateWorkflowConfig(), 200)
    }
  })

  // 如果有初始数据，加载到画布
  if (props.value && props.value.nodes && props.value.edges) {
    console.log('[流程设计器] 检测到初始数据，加载工作流:', props.value)
    loadWorkflowData(props.value)
  } else if (!props.readonly) {
    // 只在非只读模式下且没有初始数据时创建默认流程
    console.log('[流程设计器] 没有初始数据，创建默认流程')
    createDefaultWorkflow()
  } else {
    console.log('[流程设计器] 只读模式且没有初始数据，不创建任何内容')
  }

  // 右键连接线弹菜单
  if (!props.readonly) {
  graph.on('edge:contextmenu', ({ edge, e }) => {
    e.preventDefault()
    contextMenu.value = {
      visible: true,
      x: e.clientX,
      y: e.clientY,
      type: 'edge',
      target: edge
    }
  })
  // 右键节点弹菜单
  graph.on('node:contextmenu', ({ node, e }) => {
    e.preventDefault()
    contextMenu.value = {
      visible: true,
      x: e.clientX,
      y: e.clientY,
      type: 'node',
      target: node
    }
  })
  // 点击其它区域关闭菜单
  document.addEventListener('click', closeContextMenu)
  }
})
</script>

<style scoped>
.flow-root {
  width: v-bind('props.readonly ? props.width + "px" : "100vw"');
  height: v-bind('props.readonly ? props.height + "px" : "100vh"');
  display: flex;
  align-items: stretch;
  justify-content: flex-start;
  background: #f5f6fa;
}
.flow-canvas-wrap {
  flex: 1;
  min-width: v-bind('props.readonly ? "400px" : "400px"');
  min-height: v-bind('props.readonly ? "300px" : "400px"');
  max-width: v-bind('props.readonly ? "100%" : "100vw"');
  max-height: v-bind('props.readonly ? "100%" : "100vh"');
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
}
.flow-canvas {
  width: 100%;
  height: 100%;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 12px rgba(0,0,0,0.08);
  display: block;
}
.node-palette {
  position: absolute;
  top: 64px;
  left: 24px;
  display: flex;
  flex-direction: column;
  gap: 12px;
  z-index: 10;
}
.palette-item {
  width: 48px;
  height: 48px;
  background: #f0f5ff;
  border: 1px solid #e6e6e6;
  border-radius: 12px;
  color: #1890ff;
  font-size: 14px;
  cursor: pointer;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
  box-sizing: border-box;
}
.palette-item:hover {
  background: #e6f7ff;
  border-color: #91d5ff;
}
.palette-item.disabled {
  opacity: 0.5;
  cursor: not-allowed;
  background: #f5f5f5;
  border-color: #d9d9d9;
}
.palette-item.disabled:hover {
  background: #f5f5f5;
  border-color: #d9d9d9;
  transform: none;
}
.canvas-ops-panel {
  position: absolute;
  top: 64px;
  right: 24px;
  display: flex;
  flex-direction: column;
  gap: 12px;
  z-index: 10;
}
.ops-btn {
  width: 48px;
  height: 48px;
  background: #f0f5ff;
  border: 1px solid #e6e6e6;
  border-radius: 8px;
  color: #1890ff;
  font-size: 32px;
  cursor: pointer;
  transition: all 0.2s;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0;
}
.ops-btn:hover {
  background: #e6f7ff;
  border-color: #91d5ff;
  transform: translateY(-1px);
  box-shadow: 0 2px 8px rgba(24, 144, 255, 0.2);
}
.palette-icon {
  width: 32px;
  height: 32px;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}
.start-icon {
  background: #52c41a;
  border-radius: 50%;
  width: 32px;
  height: 32px;
}
.task-icon {
  background: #1890ff;
  border-radius: 8px;
  width: 32px;
  height: 20px;
  margin: 6px 0;
}
.gateway-icon {
  width: 32px;
  height: 32px;
  background: #faad14;
  clip-path: polygon(50% 0%, 100% 50%, 50% 100%, 0% 50%);
}
.end-icon {
  background: #ff4d4f;
  border-radius: 50%;
  width: 32px;
  height: 32px;
  position: relative;
}
.end-icon::after {
  content: '';
  display: block;
  width: 16px;
  height: 3px;
  background: #fff;
  position: absolute;
  left: 8px;
  top: 14.5px;
  border-radius: 1.5px;
}
.ops-icon {
  width: 32px;
  height: 32px;
  display: inline-block;
  background-size: contain;
  background-repeat: no-repeat;
  background-position: center;
}
.import-icon {
  background-color: #52c41a;
  border-radius: 4px;
  position: relative;
}
.import-icon::before {
  content: '↓';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 16px;
  font-weight: bold;
}
.export-icon {
  background-color: #1890ff;
  border-radius: 4px;
  position: relative;
}
.export-icon::before {
  content: '↑';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 16px;
  font-weight: bold;
}
.zoom-in-icon {
  background-color: #faad14;
  border-radius: 50%;
  position: relative;
}
.zoom-in-icon::before {
  content: '+';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 18px;
  font-weight: bold;
}
.zoom-out-icon {
  background-color: #722ed1;
  border-radius: 50%;
  position: relative;
}
.zoom-out-icon::before {
  content: '−';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 18px;
  font-weight: bold;
}
.reset-icon {
  background-color: #ff4d4f;
  border-radius: 4px;
  position: relative;
}
.reset-icon::before {
  content: '↺';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  color: #fff;
  font-size: 16px;
  font-weight: bold;
}
.context-menu {
  position: fixed;
  z-index: 9999;
  background: #fff;
  border: 1px solid #e6e6e6;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.15);
  min-width: 100px;
  padding: 4px 0;
}
.context-menu-item {
  padding: 8px 16px;
  cursor: pointer;
}
.context-menu-item:hover {
  background: #f5f5f5;
}
</style>
