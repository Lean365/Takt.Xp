<!--
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 流程画布区域组件（基于 OpenAuth.Net CreatedFlow，改为 Ant Design Vue）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
-->
<template>
  <div style="width: 100%; height: 100%; overflow: hidden; position: relative;" ref="flowContainer">
    <div v-if="isShowContent" class="states-box">
      <span v-for="item in states" :key="item.type" class="state-item">
        <span :class="item.className"></span>
        <p>{{ item.name }}</p>
      </span>
    </div>
    <draggable
      id="toDraggable"
      v-model="flowData.nodes"
      tag="div"
      v-bind="{ group: 'flow', animation: 200 }"
      @end="handleMoveEnd"
      @add="handleAddFormItem"
      class="flow-container"
      style="cursor: pointer;"
      :class="{
        zoomIn: currentTool.type == 'zoom-in',
        zoomOut: currentTool.type == 'zoom-out',
        canScale: container.scaleFlag,
        canDrag: container.dragFlag,
        canMultiple: rectangleMultiple.flag
      }"
      :style="{
        top: container.pos.top + 'px',
        left: container.pos.left + 'px',
        transform: 'scale(' + container.scale + ')',
        transformOrigin: container.scaleOrigin.left + 'px ' + container.scaleOrigin.top + 'px',
        'z-index': isDrag ? 11 : -2
      }"
    >
    </draggable>
    <div
      id="flowContainer"
      ref="flowContainers"
      class="flow-container"
      style="cursor: pointer;"
      :class="{
        grid: flowData.config.showGrid,
        zoomIn: currentTool.type == 'zoom-in',
        zoomOut: currentTool.type == 'zoom-out',
        canScale: container.scaleFlag,
        canDrag: container.dragFlag,
        canMultiple: rectangleMultiple.flag
      }"
      :style="{
        top: container.pos.top + 'px',
        left: container.pos.left + 'px',
        transform: 'scale(' + container.scale + ')',
        transformOrigin: container.scaleOrigin.left + 'px ' + container.scaleOrigin.top + 'px'
      }"
      @click.stop="containerHandler"
      @mousedown="mousedownHandler"
      @mousemove="mousemoveHandler"
      @mouseup="mouseupHandler"
      @wheel="scaleContainer"
      @contextmenu="showContainerContextMenu"
    >
      <template v-for="node in flowData.nodes" :key="node.id">
        <flow-node
          v-if="node && node.id"
          :node="node"
          :plumb="plumb"
          v-model:select="currentSelect"
          v-model:selectGroup="currentSelectGroup"
          :currentTool="currentTool"
          :isShowContent="isShowContent"
          @showNodeContextMenu="showNodeContextMenu"
          @isMultiple="isMultiple"
          @updateNodePos="updateNodePos"
          @alignForLine="alignForLine"
          @hideAlignLine="hideAlignLine"
        >
        </flow-node>
      </template>
    </div>
    <context-menu
      :contextMenuData="containerContextMenuData"
      @paste="paste"
      @selectAll="selectAll"
      @saveFlow="saveFlow"
      @verticaLeft="verticaLeft"
      @verticalCenter="verticalCenter"
      @verticalRight="verticalRight"
      @levelUp="levelUp"
      @levelCenter="levelCenter"
      @levelDown="levelDown"
    >
    </context-menu>
    <context-menu
      :contextMenuData="nodeContextMenuData"
      @copyNode="copyNode"
      @deleteNode="deleteNode"
    >
    </context-menu>
  </div>
</template>

<script setup>
import { ref, watch, computed, nextTick } from 'vue'
import draggable from 'vuedraggable'
import { flowConfig } from '../config/args-config'
import { ZFSN } from '../util/ZFSN'
import FlowNode from './FlowNode.vue'
import ContextMenu from '../components/ContextMenu.vue'
import { message } from 'ant-design-vue'

const props = defineProps({
  browserType: Number,
  flowData: Object,
  plumb: Object,
  select: Object,
  selectGroup: Array,
  currentTool: Object,
  isShowContent: Boolean,
  isDrag: Boolean
})

const emit = defineEmits(['update:select', 'update:selectGroup', 'findNodeConfig', 'selectTool', 'getShortcut', 'saveFlow'])

const flowContainer = ref(null)
const flowContainers = ref(null)
const currentSelect = ref(props.select || {})
const currentSelectGroup = ref(props.selectGroup || [])

const states = ref([
  {
    type: '4',
    name: '审核中',
    className: 'node-bg'
  },
  {
    type: '1',
    name: '通过',
    className: 'node-pass-bg'
  },
  {
    type: '2',
    name: '不通过',
    className: 'node-not-bg'
  },
  {
    type: '0',
    name: '驳回',
    className: 'node-back-bg'
  }
])

const container = ref({
  pos: {
    top: -3000,
    left: -3000
  },
  startPos: {
    startMove: false,
    x: 0,
    y: 0
  },
  endPos: {
    x: 0,
    y: 0
  },
  dragFlag: false,
  draging: false,
  scale: flowConfig.defaultStyle.containerScale.init,
  scaleFlag: false,
  scaleOrigin: {
    left: 0,
    top: 0
  },
  scaleShow: ZFSN.mul(flowConfig.defaultStyle.containerScale.init, 100),
  auxiliaryLine: {
    isOpen: flowConfig.defaultStyle.isOpenAuxiliaryLine,
    isShowXLine: false,
    isShowYLine: false,
    controlFnTimesFlag: true
  }
})

const auxiliaryLinePos = ref({
  left: 0,
  top: 0
})

const mouse = ref({
  position: {
    left: 0,
    top: 0
  },
  tempPos: {
    left: 0,
    top: 0
  }
})

const rectangleMultiple = ref({
  flag: false,
  multipling: false,
  position: {
    top: 0,
    left: 0
  },
  height: 0,
  width: 0
})

const containerContextMenuData = ref(flowConfig.contextMenu.container)
const nodeContextMenuData = ref(flowConfig.contextMenu.node)
const tempLinkId = ref('')
const clipboard = ref([])

// 保存当前选择
const saveCurrentSelect = (select) => {
  currentSelect.value = select
  emit('update:select', select)
}

// 监听 select 变化
watch(
  () => props.select,
  (val) => {
    if (val) {
      currentSelect.value = val
    }
  },
  { deep: true }
)

// 监听 selectGroup 变化
watch(
  () => props.selectGroup,
  (val) => {
    if (val) {
      currentSelectGroup.value = val
    }
  },
  { deep: true }
)

// 监听 currentSelect 变化
watch(
  currentSelect,
  () => {
    if (tempLinkId.value !== '') {
      const el = document.getElementById(tempLinkId.value)
      if (el && el.classList) {
        el.classList.remove('link-active')
      }
      tempLinkId.value = ''
    }
    if (currentSelect.value.type === 'sl') {
      tempLinkId.value = currentSelect.value.id
      const el = document.getElementById(currentSelect.value.id)
      if (el) {
        el.classList.add('link-active')
      }
    }
  },
  { deep: true }
)

// 监听 currentSelectGroup 变化
watch(
  currentSelectGroup,
  (val) => {
    emit('update:selectGroup', val)
    if (val.length <= 0 && props.plumb) {
      props.plumb.clearDragSelection()
    }
  },
  { deep: true }
)

const handleMoveEnd = ({ newIndex, oldIndex }) => {
  console.log(newIndex, oldIndex)
}

const handleAddFormItem = (evt) => {
  const flag =
    evt.originalEvent.target.className.indexOf('common-x-lane-node') >= 0 ||
    evt.originalEvent.target.className.indexOf('common-y-lane-node') >= 0
  mouse.value.position.left = flag
    ? evt.originalEvent.offsetX + evt.originalEvent.target.offsetLeft
    : evt.originalEvent.offsetX
  mouse.value.position.top = flag
    ? evt.originalEvent.offsetY + evt.originalEvent.target.offsetTop
    : evt.originalEvent.offsetY
  nextTick(() => {
    const newIndex = evt.newIndex
    const type = props.flowData.nodes[newIndex].type
    const belongto = props.flowData.nodes[newIndex].belongto

    emit('findNodeConfig', belongto, type, (node) => {
      if (!node) {
        message.error('未知的节点类型！')
        return
      }
      addNewNode(node, newIndex)
    })
  })
}

const addNewNode = (node, index) => {
  let x = mouse.value.position.left
  let y = mouse.value.position.top
  const nodePos = computeNodePos(x, y)
  x = nodePos.left
  y = nodePos.top

  const newNode = Object.assign({}, node)
  newNode.id = newNode.type + '-' + ZFSN.getId()
  newNode.height = 50
  if (newNode.type === 'start' || newNode.type === 'end' || newNode.type === 'event' || newNode.type === 'gateway') {
    newNode.left = x - 25
    newNode.width = 50
  } else {
    newNode.left = x - 60
    newNode.width = 120
  }
  newNode.top = y - 25
  if (newNode.type === 'x-lane') {
    newNode.height = 200
    newNode.width = 600
  } else if (newNode.type === 'y-lane') {
    newNode.height = 600
    newNode.width = 200
  }
  props.flowData.nodes[index] = { ...newNode }
  emit('selectTool', 'drag')
}

const computeNodePos = (x, y) => {
  const pxx = flowConfig.defaultStyle.alignGridPX[0]
  const pxy = flowConfig.defaultStyle.alignGridPX[1]
  if (x % pxx) x = pxx - (x % pxx) + x
  if (y % pxy) y = pxy - (y % pxy) + y
  return {
    left: x,
    top: y
  }
}

const mousedownHandler = (e) => {
  const event = window.event || e
  container.value.startPos = {
    startMove: true,
    x: event.clientX,
    y: event.clientY,
    l: flowContainers.value.offsetLeft,
    t: flowContainers.value.offsetTop
  }
  if (event.button === 0) {
    if (container.value.dragFlag) {
      mouse.value.tempPos = { ...mouse.value.position }
      container.value.draging = true
    }

    currentSelectGroup.value = []
    if (rectangleMultiple.value.flag) {
      mouse.value.tempPos = { ...mouse.value.position }
      rectangleMultiple.value.multipling = true
    }
  }
}

const mousemoveHandler = (e) => {
  const event = window.event || e

  if (container.value.startPos.startMove) {
    container.value.endPos = {
      x: event.clientX,
      y: event.clientY
    }

    const YM = container.value.endPos.y - (container.value.startPos.y - container.value.startPos.t)
    const XM = container.value.endPos.x - (container.value.startPos.x - container.value.startPos.l)
    const yFlag = Math.abs(container.value.endPos.y - container.value.startPos.y) >= 20
    const xFlag = Math.abs(container.value.endPos.x - container.value.startPos.x) >= 20
    container.value.pos = {
      top: yFlag ? YM : container.value.pos.top,
      left: xFlag ? XM : container.value.pos.left
    }
  }
  if (event.target.id === 'flowContainer') {
    mouse.value.position = {
      left: event.offsetX,
      top: event.offsetY
    }
  } else {
    const cn = event.target.className
    const tn = event.target.tagName
    if (cn !== 'lane-text' && cn !== 'lane-text-div' && tn !== 'svg' && tn !== 'path' && tn !== 'I') {
      mouse.value.position.left = event.target.offsetLeft + event.offsetX
      mouse.value.position.top = event.target.offsetTop + event.offsetY
    }
  }
  if (container.value.draging) {
    let nTop = container.value.pos.top + (mouse.value.position.top - mouse.value.tempPos.top)
    let nLeft = container.value.pos.left + (mouse.value.position.left - mouse.value.tempPos.left)
    if (nTop >= 0) nTop = 0
    if (nLeft >= 0) nLeft = 0
    container.value.pos = {
      top: nTop,
      left: nLeft
    }
  }
  if (rectangleMultiple.value.multipling) {
    let h = mouse.value.position.top - mouse.value.tempPos.top
    let w = mouse.value.position.left - mouse.value.tempPos.left
    let t = mouse.value.tempPos.top
    let l = mouse.value.tempPos.left
    if (h >= 0 && w < 0) {
      w = -w
      l -= w
    } else if (h < 0 && w >= 0) {
      h = -h
      t -= h
    } else if (h < 0 && w < 0) {
      h = -h
      w = -w
      t -= h
      l -= w
    }
    rectangleMultiple.value.height = h
    rectangleMultiple.value.width = w
    rectangleMultiple.value.position.top = t
    rectangleMultiple.value.position.left = l
  }
}

const mouseupHandler = () => {
  container.value.startPos.startMove = false

  if (container.value.draging) container.value.draging = false
  if (rectangleMultiple.value.multipling) {
    judgeSelectedNode()
    rectangleMultiple.value.multipling = false
    rectangleMultiple.value.width = 0
    rectangleMultiple.value.height = 0
  }
}

const judgeSelectedNode = () => {
  const ay = rectangleMultiple.value.position.top
  const ax = rectangleMultiple.value.position.left
  const by = ay + rectangleMultiple.value.height
  const bx = ax + rectangleMultiple.value.width

  const nodes = props.flowData.nodes
  nodes.forEach(function (node) {
    if (node.top >= ay && node.left >= ax && node.top <= by && node.left <= bx) {
      props.plumb.addToDragSelection(node.id)
      currentSelectGroup.value.push(node)
    }
  })
}

const scaleContainer = (e) => {
  e.preventDefault()
  const event = window.event || e

  if (container.value.scaleFlag) {
    if (props.browserType === 2) {
      if (event.detail < 0) {
        enlargeContainer()
      } else {
        narrowContainer()
      }
    } else {
      if (event.deltaY < 0) {
        enlargeContainer()
      } else if (container.value.scale) {
        narrowContainer()
      }
    }
  } else {
    if (event.wheelDelta > 0) {
      enlargeContainer()
    } else {
      narrowContainer()
    }
  }
}

const enlargeContainer = () => {
  container.value.scaleOrigin.left = mouse.value.position.left
  container.value.scaleOrigin.top = mouse.value.position.top
  const newScale = ZFSN.add(container.value.scale, flowConfig.defaultStyle.containerScale.onceEnlarge)
  if (newScale <= flowConfig.defaultStyle.containerScale.max) {
    container.value.scale = newScale
    container.value.scaleShow = ZFSN.mul(container.value.scale, 100)
    props.plumb.setZoom(container.value.scale)
  }
}

const narrowContainer = () => {
  container.value.scaleOrigin.left = mouse.value.position.left
  container.value.scaleOrigin.top = mouse.value.position.top
  const newScale = ZFSN.sub(container.value.scale, flowConfig.defaultStyle.containerScale.onceNarrow)
  if (newScale >= flowConfig.defaultStyle.containerScale.min) {
    container.value.scale = newScale
    container.value.scaleShow = ZFSN.mul(container.value.scale, 100)
    props.plumb.setZoom(container.value.scale)
  }
}

const showContainerContextMenu = (e) => {
  const event = window.event || e

  event.preventDefault()
  selectContainer()
  const x = event.clientX
  const y = event.clientY
  containerContextMenuData.value.axis = { x, y }
}

const showNodeContextMenu = (e) => {
  const event = window.event || e

  event.preventDefault()
  const x = event.clientX
  const y = event.clientY
  nodeContextMenuData.value.axis = { x, y }
}

const paste = () => {
  let dis = 0
  clipboard.value.forEach(function (node) {
    const newNode = Object.assign({}, node)
    newNode.id = newNode.type + '-' + ZFSN.getId()
    const nodePos = computeNodePos(mouse.value.position.left + dis, mouse.value.position.top + dis)
    newNode.left = nodePos.left
    newNode.top = nodePos.top
    dis += 20
    props.flowData.nodes.push(newNode)
  })
}

const selectAll = () => {
  props.flowData.nodes.forEach(function (node) {
    props.plumb.addToDragSelection(node.id)
    currentSelectGroup.value.push(node)
  })
}

const saveFlow = () => {
  emit('saveFlow')
}

const checkAlign = () => {
  if (currentSelectGroup.value.length < 2) {
    message.error('请选择至少两个节点！')
    return false
  }
  return true
}

const verticaLeft = () => {
  if (!checkAlign()) return
  const nodes = props.flowData.nodes
  const selectGroup = currentSelectGroup.value
  const baseX = selectGroup[0].left
  let baseY = selectGroup[0].top
  for (let i = 1; i < selectGroup.length; i++) {
    baseY = baseY + selectGroup[i - 1].height + flowConfig.defaultStyle.alignSpacing.vertical
    const f = nodes.filter((n) => n.id === selectGroup[i].id)[0]
    f.tx = baseX
    f.ty = baseY
    props.plumb.animate(
      selectGroup[i].id,
      { top: baseY, left: baseX },
      {
        duration: flowConfig.defaultStyle.alignDuration,
        complete: function () {
          f.left = f.tx
          f.top = f.ty
        }
      }
    )
  }
}

const verticalCenter = () => {
  if (!checkAlign()) return
  const nodes = props.flowData.nodes
  const selectGroup = currentSelectGroup.value
  let baseX = selectGroup[0].left
  let baseY = selectGroup[0].top
  const firstX = baseX
  for (let i = 1; i < selectGroup.length; i++) {
    baseY = baseY + selectGroup[i - 1].height + flowConfig.defaultStyle.alignSpacing.vertical
    baseX = firstX + ZFSN.div(selectGroup[0].width, 2) - ZFSN.div(selectGroup[i].width, 2)
    const f = nodes.filter((n) => n.id === selectGroup[i].id)[0]
    f.tx = baseX
    f.ty = baseY
    props.plumb.animate(
      selectGroup[i].id,
      { top: baseY, left: baseX },
      {
        duration: flowConfig.defaultStyle.alignDuration,
        complete: function () {
          f.left = f.tx
          f.top = f.ty
        }
      }
    )
  }
}

const verticalRight = () => {
  if (!checkAlign()) return
  const nodes = props.flowData.nodes
  const selectGroup = currentSelectGroup.value
  let baseX = selectGroup[0].left
  let baseY = selectGroup[0].top
  const firstX = baseX
  for (let i = 1; i < selectGroup.length; i++) {
    baseY = baseY + selectGroup[i - 1].height + flowConfig.defaultStyle.alignSpacing.vertical
    baseX = firstX + selectGroup[0].width - selectGroup[i].width
    const f = nodes.filter((n) => n.id === selectGroup[i].id)[0]
    f.tx = baseX
    f.ty = baseY
    props.plumb.animate(
      selectGroup[i].id,
      { top: baseY, left: baseX },
      {
        duration: flowConfig.defaultStyle.alignDuration,
        complete: function () {
          f.left = f.tx
          f.top = f.ty
        }
      }
    )
  }
}

const levelUp = () => {
  if (!checkAlign()) return
  const nodes = props.flowData.nodes
  const selectGroup = currentSelectGroup.value
  let baseX = selectGroup[0].left
  const baseY = selectGroup[0].top
  for (let i = 1; i < selectGroup.length; i++) {
    baseX = baseX + selectGroup[i - 1].width + flowConfig.defaultStyle.alignSpacing.level
    const f = nodes.filter((n) => n.id === selectGroup[i].id)[0]
    f.tx = baseX
    f.ty = baseY
    props.plumb.animate(
      selectGroup[i].id,
      { top: baseY, left: baseX },
      {
        duration: flowConfig.defaultStyle.alignDuration,
        complete: function () {
          f.left = f.tx
          f.top = f.ty
        }
      }
    )
  }
}

const levelCenter = () => {
  if (!checkAlign()) return
  const nodes = props.flowData.nodes
  const selectGroup = currentSelectGroup.value
  let baseX = selectGroup[0].left
  let baseY = selectGroup[0].top
  const firstY = baseY
  for (let i = 1; i < selectGroup.length; i++) {
    baseY = firstY + ZFSN.div(selectGroup[0].height, 2) - ZFSN.div(selectGroup[i].height, 2)
    baseX = baseX + selectGroup[i - 1].width + flowConfig.defaultStyle.alignSpacing.level
    const f = nodes.filter((n) => n.id === selectGroup[i].id)[0]
    f.tx = baseX
    f.ty = baseY
    props.plumb.animate(
      selectGroup[i].id,
      { top: baseY, left: baseX },
      {
        duration: flowConfig.defaultStyle.alignDuration,
        complete: function () {
          f.left = f.tx
          f.top = f.ty
        }
      }
    )
  }
}

const levelDown = () => {
  if (!checkAlign()) return
  const nodes = props.flowData.nodes
  const selectGroup = currentSelectGroup.value
  let baseX = selectGroup[0].left
  let baseY = selectGroup[0].top
  const firstY = baseY
  for (let i = 1; i < selectGroup.length; i++) {
    baseY = firstY + selectGroup[0].height - selectGroup[i].height
    baseX = baseX + selectGroup[i - 1].width + flowConfig.defaultStyle.alignSpacing.level
    const f = nodes.filter((n) => n.id === selectGroup[i].id)[0]
    f.tx = baseX
    f.ty = baseY
    props.plumb.animate(
      selectGroup[i].id,
      { top: baseY, left: baseX },
      {
        duration: flowConfig.defaultStyle.alignDuration,
        complete: function () {
          f.left = f.tx
          f.top = f.ty
        }
      }
    )
  }
}

const copyNode = () => {
  clipboard.value = []
  if (currentSelectGroup.value.length > 0) {
    clipboard.value = Object.assign([], currentSelectGroup.value)
  } else if (currentSelect.value.id) {
    clipboard.value.push(currentSelect.value)
  }
}

const deleteNode = () => {
  const nodes = props.flowData.nodes
  const lines = props.flowData.lines
  props.flowData.lines = lines.filter(
    (link) => link.from !== currentSelect.value.id && link.to !== currentSelect.value.id
  )
  props.plumb.deleteConnectionsForElement(currentSelect.value.id)
  const inx = nodes.findIndex((node) => node.id === currentSelect.value.id)
  nodes.splice(inx, 1)
  selectContainer()
}

const containerHandler = () => {
  selectContainer()
  const toolType = props.currentTool.type
  if (toolType === 'zoom-in') {
    enlargeContainer()
  } else if (toolType === 'zoom-out') {
    narrowContainer()
  }
}

const selectContainer = () => {
  saveCurrentSelect({})
  emit('getShortcut')
}

const isMultiple = (callback) => {
  callback(rectangleMultiple.value.flag)
}

const updateNodePos = () => {
  const nodes = props.flowData.nodes
  currentSelectGroup.value.forEach(function (node) {
    const el = document.getElementById(node.id)
    if (el) {
      const l = parseInt(el.style.left)
      const t = parseInt(el.style.top)
      const f = nodes.filter((n) => n.id === node.id)[0]
      if (f) {
        f.left = l
        f.top = t
      }
    }
  })
}

const alignForLine = (e) => {
  if (currentSelectGroup.value.length > 1) return
  if (container.value.auxiliaryLine.controlFnTimesFlag) {
    const elId = e.el.id
    const nodes = props.flowData.nodes
    nodes.forEach(function (node) {
      if (elId !== node.id) {
        const dis = flowConfig.defaultStyle.showAuxiliaryLineDistance
        const elPos = e.pos
        const elH = e.el.offsetHeight
        const elW = e.el.offsetWidth
        const disX = elPos[0] - node.left
        const disY = elPos[1] - node.top
        if ((disX >= -dis && disX <= dis) || (disX + elW >= -dis && disX + elW <= dis)) {
          container.value.auxiliaryLine.isShowYLine = true
          auxiliaryLinePos.value.left = node.left + container.value.pos.left
          const nodeMidPointX = node.left + node.width / 2
          if (nodeMidPointX === elPos[0] + elW / 2) {
            auxiliaryLinePos.value.left = nodeMidPointX + container.value.pos.left
          }
        }
        if ((disY >= -dis && disY <= dis) || (disY + elH >= -dis && disY + elH <= dis)) {
          container.value.auxiliaryLine.isShowXLine = true
          auxiliaryLinePos.value.top = node.top + container.value.pos.top
          const nodeMidPointY = node.top + node.height / 2
          if (nodeMidPointY === elPos[1] + elH / 2) {
            auxiliaryLinePos.value.top = nodeMidPointY + container.value.pos.left
          }
        }
      }
    })
    container.value.auxiliaryLine.controlFnTimesFlag = false
    setTimeout(function () {
      container.value.auxiliaryLine.controlFnTimesFlag = true
    }, 200)
  }
}

const hideAlignLine = () => {
  if (container.value.auxiliaryLine.isOpen) {
    container.value.auxiliaryLine.isShowXLine = false
    container.value.auxiliaryLine.isShowYLine = false
  }
}

// 暴露给父组件的方法
defineExpose({
  container,
  rectangleMultiple
})
</script>

<style lang="scss">
@import '../style/flow-area.scss';
.sortable-chosen.ghost {
  display: none;
}
.states-box {
  position: absolute;
  top: 10px;
  right: 10px;
  z-index: 999;
  .state-item {
    display: inline-block;
    margin-left: 10px;
    text-align: center;
    > p {
      margin: 0;
      font-size: 12px;
    }
    > span {
      display: inline-block;
      width: 15px;
      height: 15px;
      border-radius: 2px;
      box-sizing: border-box;
      &.node-bg {
        border: 1px solid #777;
        background: #f4f6fc;
      }
      &.node-not-bg {
        background: #d9534f;
        color: #fff;
      }
      &.node-pass-bg {
        background: #5cb85c;
        color: #fff;
      }
      &.node-back-bg {
        background: #f0ad4e;
        color: #fff;
      }
    }
  }
}
</style>

