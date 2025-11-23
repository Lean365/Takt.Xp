<!--
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 流程设计器主组件（基于 OpenAuth.Net CreatedFlow，改为 Ant Design Vue）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
-->
<template>
  <div style="height: 100%;">
    <a-layout class="container">
      <a-layout-sider width="130" theme="light" class="select-area" v-if="!isShowContent">
        <a-row>
          <a-checkbox v-model:checked="tag.checked0" class="tag">工具</a-checkbox>
          <div style="text-align: center;" v-if="tag.checked0">
            <a-button-group>
              <a-button v-for="(tool, index) in field.tools" :key="index" size="small"
                :type="currentTool.type == tool.type ? 'primary' : 'default'" @click="selectTool(tool.type)"
                style="font-size: 16px;width: 50px;">
                <template #icon>
                  <component :is="getIconComponent(tool.icon)" />
                </template>
              </a-button>
            </a-button-group>
          </div>
        </a-row>
        <a-row>
          <a-checkbox v-model:checked="tag.checked1" class="tag">基础节点</a-checkbox>
          <div style="text-align: center;" v-if="tag.checked1">
            <draggable tag="div" :list="field.commonNodes"
              v-bind="{ group: { name: 'flow', pull: 'clone', put: false }, sort: false }" @end="handleMoveEnd"
              @start="handleMoveStart" :move="handleMove">
              <template v-for="(commonNode, index) in field.commonNodes" :key="index">
                <div class="node-item" :type="commonNode.type" belongto="commonNodes">
                  <component :is="getIconComponent(commonNode.icon)" style="font-size: 16px;" />
                  {{ commonNode.name }}
                </div>
              </template>
            </draggable>
          </div>
        </a-row>
        <a-row>
          <a-checkbox v-model:checked="tag.checked3" class="tag">泳道节点</a-checkbox>
          <div style="text-align: center;">
            <draggable tag="div" :list="field.laneNodes" :grid="{ gutter: 8, column: 2 }" v-if="tag.checked3"
              v-bind="{ group: { name: 'flow', pull: 'clone', put: false }, sort: false }" @end="handleMoveEnd"
              @start="handleMoveStart" :move="handleMove">
              <div v-for="(laneNode, index) in field.laneNodes" :key="index" class="node-item" :type="laneNode.type"
                belongto="laneNodes">
                <component :is="getIconComponent(laneNode.icon)" style="font-size: 16px;" />
                {{ laneNode.name }}
              </div>
            </draggable>
          </div>
        </a-row>
        <a-row>
          <div style="text-align: center;">
            <a-popconfirm title="确认要重新绘制吗？" placement="bottom" @confirm="clear">
              <template #icon>
                <QuestionCircleOutlined />
              </template>
              <a-tooltip title="清空当前画布，重新绘制" placement="bottom">
                <a-button size="small">
                  <template #icon>
                    <ReloadOutlined />
                  </template>
                  重新绘制
                </a-button>
              </a-tooltip>
            </a-popconfirm>
          </div>
        </a-row>
      </a-layout-sider>
      <a-layout-content style="padding:0;">
        <a-layout style="height: 100%;">
          <a-layout-content class="content" style="padding: 0;">
            <flow-area v-if="loadFlowArea && flowData" ref="flowArea" :browserType="browserType" :flowData="flowData"
              v-model:select="currentSelect" v-model:selectGroup="currentSelectGroup" :plumb="plumb"
              :isShowContent="isShowContent" :currentTool="currentTool" :isDrag="isDrag"
              @findNodeConfig="findNodeConfig" @selectTool="selectTool" @getShortcut="getShortcut" @saveFlow="saveFlow">
            </flow-area>
            <context-menu :contextMenuData="linkContextMenuData" @deleteLink="deleteLink">
            </context-menu>
          </a-layout-content>
        </a-layout>
      </a-layout-content>
      <a-layout-sider width="300" theme="light" class="attr-area" @mousedown.stop="loseShortcut"
        v-if="isShowAttr && (!isShowContent)">
        <flow-attr :plumb="plumb" :flowData="flowData" :formTemplate="formTemplate"></flow-attr>
      </a-layout-sider>
    </a-layout>
    <a-modal :title="'流程设计图_' + flowData.attr.id + '.png'" centered :width="'90%'" :closable="flowPicture.closable"
      :maskClosable="flowPicture.maskClosable" v-model:open="flowPicture.modalVisible" okText="下载到本地" cancelText="取消"
      @ok="downLoadFlowPicture" @cancel="cancelDownLoadFlowPicture">
      <div style="text-align: center;">
        <img :src="flowPicture.url" />
      </div>
    </a-modal>
  </div>
</template>

<script setup>
import { ref, onMounted, watch, computed, h } from 'vue'
import Draggable from 'vuedraggable'
import FlowArea from './modules/FlowArea.vue'
import { jsPlumb } from 'jsplumb'
import { tools, commonNodes, highNodes, laneNodes } from './config/basic-node-config'
import { flowConfig } from './config/args-config'
import { ZFSN } from './util/ZFSN'
import FlowAttr from './modules/FlowAttr.vue'
import ContextMenu from './components/ContextMenu.vue'
import { message } from 'ant-design-vue'
import {
  DragOutlined,
  ShareAltOutlined,
  PlayCircleOutlined,
  StopOutlined,
  ToolOutlined,
  ForkOutlined,
  MergeCellsOutlined,
  TeamOutlined,
  ApiOutlined,
  ColumnWidthOutlined,
  ColumnHeightOutlined,
  ReloadOutlined,
  QuestionCircleOutlined
} from '@ant-design/icons-vue'

const props = defineProps({
  schemeContent: String,
  isEdit: Boolean,
  formTemplate: Array,
  isShowContent: Boolean
})

const emit = defineEmits(['update:schemeContent'])

// 状态管理（使用 Pinia 或本地状态）
const currentSelect = ref({})
const currentSelectGroup = ref([])

const isDrag = ref(false)
const loadFlowArea = ref(false)
const isShowPopover = ref(false)
const isShowAttr = ref(false)
const tag = ref({
  checked0: true,
  checked1: true,
  checked2: true,
  checked3: true
})
const browserType = ref(3)
const plumb = ref(null)
const field = ref({
  tools: tools,
  commonNodes: commonNodes,
  highNodes: highNodes,
  laneNodes: laneNodes
})
const flowData = ref({
  nodes: [],
  lines: [],
  attr: {
    id: ''
  },
  config: {
    showGrid: true,
    showGridText: '隐藏网格',
    showGridIcon: 'EyeOutlined'
  },
  status: flowConfig.flowStatus.CREATE,
  remarks: []
})
const currentTool = ref({
  type: 'drag',
  icon: 'DragOutlined',
  name: '拖拽'
})
const activeShortcut = ref(true)
const linkContextMenuData = ref(flowConfig.contextMenu.sl)
const flowPicture = ref({
  url: '',
  modalVisible: false,
  closable: false,
  maskClosable: false
})

// 图标组件映射
const iconMap = {
  DragOutlined,
  ShareAltOutlined,
  PlayCircleOutlined,
  StopOutlined,
  ToolOutlined,
  ForkOutlined,
  MergeCellsOutlined,
  TeamOutlined,
  ApiOutlined,
  ColumnWidthOutlined,
  ColumnHeightOutlined,
  ReloadOutlined,
  QuestionCircleOutlined
}

const getIconComponent = (iconName) => {
  return iconMap[iconName] || ToolOutlined
}

// 保存当前选择（简化版，实际应该使用 Pinia）
const saveCurrentSelect = (select) => {
  currentSelect.value = select
}

// 监听 currentSelect 变化
watch(currentSelect, () => {
  const list = currentSelect.value.type === 'sl' ? flowData.value.lines : flowData.value.nodes
  const index = list.findIndex(item => item.id === currentSelect.value.id)
  if (index >= 0) {
    list[index] = currentSelect.value
    isShowAttr.value = true
  } else {
    isShowAttr.value = false
  }
}, { deep: true })

// 监听 schemeContent 变化
watch(() => props.schemeContent, (val) => {
  if (val && props.isEdit) {
    flowData.value = groupSchemeContent()
    flowData.value.status = flowConfig.flowStatus.MODIFY
    if (plumb.value) {
      plumb.value.deleteEveryConnection()
    }
    initJsPlumb()
    loadFlow()
  }
})

const groupSchemeContent = () => {
  const obj = Object.assign({}, JSON.parse(props.schemeContent))
  if (!obj.attr || !obj.attr.id) {
    const { lines, nodes } = obj
    nodes.length > 0 && nodes.forEach(item => {
      item.setInfo = item.setInfo || {
        NodeRejectType: 0,
        NodeConfluenceType: '',
        NodeDesignate: '',
        NodeDesignateData: {
          users: [],
          roles: [],
          Texts: ''
        }
      }
    })
    lines.length > 0 && lines.forEach(item => {
      item.label = item.label || item.name
      item.cls = {
        linkType: 'Flowchart',
        linkColor: '#2a2929',
        linkThickness: 2
      }
    })
    obj.attr = {
      id: obj.id || ''
    }
    obj.config = {
      showGrid: true,
      showGridText: '隐藏网格',
      showGridIcon: 'EyeOutlined'
    }
  }
  return obj
}

const handleMoveEnd = () => {
  isDrag.value = false
}

const handleMoveStart = ({ oldIndex }) => {
  console.log('oldIndex', oldIndex)
  isDrag.value = true
}

const handleMove = () => {
  return true
}

const getBrowserType = () => {
  const userAgent = navigator.userAgent
  const isOpera = userAgent.indexOf('Opera') > -1
  if (isOpera) {
    return 1
  }
  if (userAgent.indexOf('Firefox') > -1) {
    return 2
  }
  if (userAgent.indexOf('Chrome') > -1) {
    return 3
  }
  if (userAgent.indexOf('Safari') > -1) {
    return 4
  }
  if (userAgent.indexOf('compatible') > -1 && userAgent.indexOf('MSIE') > -1 && !isOpera) {
    alert('IE浏览器支持性较差，推荐使用Firefox或Chrome')
    return 5
  }
  if (userAgent.indexOf('Trident') > -1) {
    alert('Edge浏览器支持性较差，推荐使用Firefox或Chrome')
    return 6
  }
}

const dealCompatibility = () => {
  browserType.value = getBrowserType()
  if (browserType.value === 2) {
    flowConfig.shortcut.scaleContainer = {
      code: 16,
      codeName: 'SHIFT(chrome下为ALT)',
      shortcutName: '画布缩放'
    }
  }
}

const initJsPlumb = () => {
  plumb.value = jsPlumb.getInstance(flowConfig.jsPlumbInsConfig)
  loadFlowArea.value = true
  plumb.value.ready(() => {
    plumb.value.bind('beforeDrop', function (info) {
      const from = info.sourceId
      const to = info.targetId

      if (from === to) return false
      const filter = flowData.value.lines.filter(link => (link.from === from && link.to === to))
      if (filter.length > 0) {
        message.error('同方向的两节点连线只能有一条！')
        return false
      }
      return true
    })
    let conunt = 0
    plumb.value.bind('connection', (conn) => {
      const connObj = conn.connection.canvas
      const o = {}
      let id = ''
      let label
      if (flowData.value.status === flowConfig.flowStatus.CREATE || flowData.value.status === flowConfig.flowStatus.MODIFY) {
        id = 'link-' + ZFSN.getId()
        label = ''
      } else if (flowData.value.status === flowConfig.flowStatus.LOADING) {
        const l = flowData.value.lines[conunt]
        id = l.id
        label = l.label
        conunt++
      }
      connObj.id = id
      o.type = 'sl'
      o.id = id
      o.from = conn.sourceId
      o.to = conn.targetId
      o.label = label
      o.cls = {
        linkType: flowConfig.jsPlumbInsConfig.Connector[0],
        linkColor: flowConfig.jsPlumbInsConfig.PaintStyle.stroke,
        linkThickness: flowConfig.jsPlumbInsConfig.PaintStyle.strokeWidth
      }
      const linkElement = document.getElementById(id)
      if (linkElement) {
        linkElement.addEventListener('contextmenu', function (e) {
          showLinkContextMenu(e)
          const currentSelectLink = flowData.value.lines.filter(l => l.id === id)[0]
          saveCurrentSelect(currentSelectLink)
        })
        linkElement.addEventListener('click', function (e) {
          const event = window.event || e
          event.stopPropagation()
          const currentSelectLink = flowData.value.lines.filter(l => l.id === id)[0]
          const Compares = [{
            FieldName: '',
            Operation: '',
            Value: ''
          }]
          currentSelectLink.Compares = currentSelectLink.Compares || Compares
          saveCurrentSelect(currentSelectLink)
        })
      }
      if (flowData.value.status !== flowConfig.flowStatus.LOADING) flowData.value.lines.push(o)
    })

    plumb.value.importDefaults({
      ConnectionsDetachable: flowConfig.jsPlumbConfig.conn.isDetachable
    })
  })
}

const listenShortcut = () => {
  document.onkeydown = function (e) {
    const event = window.event || e

    if (!activeShortcut.value) return
    const key = event.keyCode

    switch (key) {
      case flowConfig.shortcut.multiple.code:
        // flowArea.value.rectangleMultiple.flag = true
        break
      case flowConfig.shortcut.dragContainer.code:
        // flowArea.value.container.dragFlag = true
        break
      case flowConfig.shortcut.scaleContainer.code:
        // flowArea.value.container.scaleFlag = true
        break
      case flowConfig.shortcut.dragTool.code:
        selectTool('drag')
        break
      case flowConfig.shortcut.connTool.code:
        selectTool('connection')
        break
      case flowConfig.shortcut.zoomInTool.code:
        selectTool('zoom-in')
        break
      case flowConfig.shortcut.zoomOutTool.code:
        selectTool('zoom-out')
        break
      case 37:
        moveNode('left')
        break
      case 38:
        moveNode('up')
        break
      case 39:
        moveNode('right')
        break
      case 40:
        moveNode('down')
        break
      default:
        break
    }

    if (event.ctrlKey) {
      if (event.altKey) {
        switch (key) {
          case flowConfig.shortcut.settingModal.code:
            // setting()
            break
          case flowConfig.shortcut.testModal.code:
            // openTest()
            break
        }
      }
    }
  }
  document.onkeyup = function (e) {
    const event = window.event || e

    const key = event.keyCode
    if (key === flowConfig.shortcut.dragContainer.code) {
      // flowArea.value.container.dragFlag = false
    } else if (key === flowConfig.shortcut.scaleContainer.code) {
      event.preventDefault()
      // flowArea.value.container.scaleFlag = false
    } else if (key === flowConfig.shortcut.multiple.code) {
      // flowArea.value.rectangleMultiple.flag = false
    }
  }
}

const loadFlow = () => {
  flowData.value.status = flowConfig.flowStatus.LOADING
  plumb.value.ready(() => {
    const lines = Object.assign([], flowData.value.lines)
    setTimeout(() => {
      lines.forEach((link) => {
        const conn = plumb.value.connect({
          source: link.from,
          target: link.to,
          anchor: flowConfig.jsPlumbConfig.anchor.default,
          connector: [
            link.cls.linkType,
            {
              gap: 5,
              cornerRadius: 8,
              alwaysRespectStubs: true
            }
          ],
          paintStyle: {
            stroke: link.cls.linkColor,
            strokeWidth: link.cls.linkThickness
          }
        })
        if (link.label !== '') {
          conn.setLabel({
            label: link.label,
            cssClass: 'linkLabel'
          })
        }
      })
      saveCurrentSelect({})
      currentSelectGroup.value = []
      flowData.value.status = flowConfig.flowStatus.MODIFY
    }, 0)
  })
}

const findNodeConfig = (belongto, type, callback) => {
  let node = null
  switch (belongto) {
    case 'commonNodes':
      node = commonNodes.filter(n => n.type === type)
      break
    case 'highNodes':
      node = highNodes.filter(n => n.type === type)
      break
    case 'laneNodes':
      node = laneNodes.filter(n => n.type === type)
      break
  }
  if (node && node.length >= 0) node = node[0]
  callback(node)
}

const selectTool = (type) => {
  if (currentTool.value.type === type) {
    return
  }
  const tool = tools.filter(t => t.type === type)
  if (tool && tool.length >= 0) currentTool.value = tool[0]

  switch (type) {
    case 'drag':
      changeToDrag()
      break
    case 'connection':
      changeToConnection()
      break
    case 'zoom-in':
      changeToZoomIn()
      break
    case 'zoom-out':
      changeToZoomOut()
      break
  }
}

const changeToDrag = () => {
  setTimeout(() => {
    flowData.value.nodes.forEach(function (node) {
      const f = plumb.value.toggleDraggable(node.id)
      if (!f) {
        plumb.value.toggleDraggable(node.id)
      }
      if (node.type !== 'x-lane' && node.type !== 'y-lane') {
        plumb.value.unmakeSource(node.id)
        plumb.value.unmakeTarget(node.id)
      }
    })
  }, 0)
}

const changeToConnection = () => {
  flowData.value.nodes.forEach(function (node) {
    const f = plumb.value.toggleDraggable(node.id)
    if (f) {
      plumb.value.toggleDraggable(node.id)
    }
    if (node.type !== 'x-lane' && node.type !== 'y-lane') {
      plumb.value.makeSource(node.id, flowConfig.jsPlumbConfig.makeSourceConfig)
      plumb.value.makeTarget(node.id, flowConfig.jsPlumbConfig.makeTargetConfig)
    }
  })

  saveCurrentSelect({})
  currentSelectGroup.value = []
}

const changeToZoomIn = () => {
  console.log('切换到放大工具')
}

const changeToZoomOut = () => {
  console.log('切换到缩小工具')
}

const checkFlow = () => {
  const nodes = flowData.value.nodes

  if (nodes.length <= 0) {
    message.error('流程图中无任何节点！')
    return false
  }
  return true
}

const saveFlow = () => {
  const flowObj = Object.assign({}, flowData.value)

  if (!checkFlow()) return
  flowObj.status = flowConfig.flowStatus.SAVE
  const d = JSON.stringify(flowObj)
  message.success('保存流程成功！请查看控制台。')
  return d
}

const downLoadFlowPicture = () => {
  const alink = document.createElement('a')
  const alinkId = 'alink-' + ZFSN.getId()
  alink.id = alinkId
  alink.ref = alinkId
  alink.href = flowPicture.value.url
  alink.download = '流程设计图_' + flowData.value.attr.id + '.png'
  alink.click()
}

const cancelDownLoadFlowPicture = () => {
  flowPicture.value.url = ''
  flowPicture.value.modalVisible = false
}

const clear = () => {
  flowData.value.nodes.forEach(function (node) {
    plumb.value.remove(node.id)
  })
  saveCurrentSelect({})
  currentSelectGroup.value = []
  flowData.value.nodes = []
  flowData.value.lines = []
  flowData.value.remarks = []
  isShowPopover.value = false
}

const showLinkContextMenu = (e) => {
  const event = window.event || e
  event.preventDefault()
  event.stopPropagation()
  const x = event.clientX
  const y = event.clientY
  linkContextMenuData.value.axis = { x, y }
}

const deleteLink = () => {
  const from = currentSelect.value.from
  const to = currentSelect.value.to
  plumb.value.deleteConnection(plumb.value.getConnections({
    source: from,
    target: to
  })[0])
  const { lines } = flowData.value
  lines.splice(lines.findIndex(link => (link.from === from && link.to === to)), 1)
  flowData.value.lines = Object.assign([], lines)
  saveCurrentSelect({})
}

const loseShortcut = () => {
  activeShortcut.value = false
}

const getShortcut = () => {
  activeShortcut.value = true
}

const moveNode = (type) => {
  let m = flowConfig.defaultStyle.movePx
  let isX = true
  switch (type) {
    case 'left':
      m = -m
      break
    case 'up':
      m = -m
      isX = false
      break
    case 'right':
      break
    case 'down':
      isX = false
  }

  if (currentSelectGroup.value.length > 0) {
    currentSelectGroup.value.forEach(function (node) {
      if (isX) {
        node.left += m
      } else {
        node.top += m
      }
    })
    plumb.value.repaintEverything()
  } else if (currentSelect.value.id) {
    const currentSelectNode = { ...currentSelect.value }
    if (isX) {
      currentSelectNode.left += m
    } else {
      currentSelectNode.top += m
    }
    saveCurrentSelect(currentSelectNode)
    plumb.value.repaintEverything()
  }
}

onMounted(() => {
  dealCompatibility()
  if (props.schemeContent) {
    flowData.value = groupSchemeContent()
    flowData.value.status = flowConfig.flowStatus.MODIFY
    initJsPlumb()
    loadFlow()
  } else {
    initJsPlumb()
    loadFlow()
  }
  listenShortcut()
})
</script>

<style lang="scss">
@import './style/flow-designer.scss';

.tag {
  margin: 6px 0;
  font-size: 12px;
  color: #1890ff;
}
</style>
