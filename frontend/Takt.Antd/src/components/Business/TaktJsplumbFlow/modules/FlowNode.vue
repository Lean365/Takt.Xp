<!--
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 流程节点组件（基于 OpenAuth.Net CreatedFlow，改为 Ant Design Vue）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
-->
<template>
  <a-tooltip :title="contentHtml ? undefined : '点击关闭 tooltip 功能'" :overlayInnerStyle="{ textAlign: 'left' }">
    <template v-if="contentHtml" #title>
      <div v-html="contentHtml"></div>
    </template>
    <div v-if="node.type == 'start'" :id="node.id" class="common-circle-node node-start-bg node-start"
      :class="{ active: isActive() }" :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <PlayCircleOutlined />
      {{ node.name }}
    </div>

    <div v-else-if="node.type == 'end'" :id="node.id" class="common-circle-node node-end"
      :class="{ active: isActive() }" :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <StopOutlined />
      {{ node.name }}
    </div>

    <div v-else-if="node.type === 'node'" :id="node.id" class="common-rectangle-node flex-row" :class="stateClass()"
      :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <component :is="getIconComponent(node.icon || node.defaultIcon || 'ToolOutlined')" />
      <span class="flex-item">{{ node.name }}</span>
    </div>

    <div v-else-if="node.type === 'fork'" :id="node.id" class="common-rectangle-node flex-row" :class="stateClass()"
      :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <ForkOutlined />
      <span class="flex-item">{{ node.name }}</span>
    </div>

    <div v-else-if="node.type === 'join'" :id="node.id" class="common-rectangle-node flex-row" :class="stateClass()"
      :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <MergeCellsOutlined />
      <span class="flex-item">{{ node.name }}</span>
    </div>

    <div v-else-if="node.type == 'multiInstance'" :id="node.id" class="common-rectangle-node flex-row"
      :class="stateClass()" :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <TeamOutlined />
      <span class="flex-item">{{ node.name }}</span>
    </div>

    <div v-else-if="node.type == 'common'" :id="node.id" class="common-rectangle-node flex-row" :class="stateClass()"
      :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        cursor:
          currentTool.type == 'drag'
            ? 'move'
            : currentTool.type == 'connection'
              ? 'crosshair'
              : currentTool.type == 'zoom-in'
                ? 'zoom-in'
                : currentTool.type == 'zoom-out'
                  ? 'zoom-out'
                  : 'default'
      }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <component :is="getIconComponent(node.icon || node.defaultIcon)" />
      <span class="flex-item">{{ node.name }}</span>
    </div>

    <div v-else-if="node.type == 'freedom'" :id="node.id" class="common-rectangle-node" :class="stateClass()" :style="{
      top: node.top + 'px',
      left: node.left + 'px',
      cursor:
        currentTool.type == 'drag'
          ? 'move'
          : currentTool.type == 'connection'
            ? 'crosshair'
            : currentTool.type == 'zoom-in'
              ? 'zoom-in'
              : currentTool.type == 'zoom-out'
                ? 'zoom-out'
                : 'default'
    }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <ReloadOutlined />
      {{ node.name }}
    </div>

    <div v-else-if="node.type == 'event'" :id="node.id" class="common-circle-node" :class="stateClass()" :style="{
      top: node.top + 'px',
      left: node.left + 'px',
      cursor:
        currentTool.type == 'drag'
          ? 'move'
          : currentTool.type == 'connection'
            ? 'crosshair'
            : currentTool.type == 'zoom-in'
              ? 'zoom-in'
              : currentTool.type == 'zoom-out'
                ? 'zoom-out'
                : 'default'
    }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      {{ node.name }}
    </div>

    <div v-else-if="node.type == 'gateway'" :id="node.id" class="common-diamond-node" :class="stateClass()" :style="{
      top: node.top + 'px',
      left: node.left + 'px',
      cursor:
        currentTool.type == 'drag'
          ? 'move'
          : currentTool.type == 'connection'
            ? 'crosshair'
            : currentTool.type == 'zoom'
              ? 'zoom-in'
              : currentTool.type == 'zoom-out'
                ? 'zoom-out'
                : 'default'
    }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
    </div>

    <div v-else-if="node.type == 'child-flow'" :id="node.id" class="common-rectangle-node" :class="stateClass()" :style="{
      top: node.top + 'px',
      left: node.left + 'px',
      cursor:
        currentTool.type == 'drag'
          ? 'move'
          : currentTool.type == 'connection'
            ? 'crosshair'
            : currentTool.type == 'zoom-in'
              ? 'zoom-in'
              : currentTool.type == 'zoom-out'
                ? 'zoom-out'
                : 'default'
    }" @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
      <ApiOutlined class="node-icon" />
      {{ node.name }}
    </div>

    <!-- 泳道节点暂时简化处理，不使用 vue-draggable-resizable-gorkys -->
    <div v-else-if="node.type == 'x-lane'" :id="node.id" class="common-x-lane-node" :class="{ laneActive: isActive() }"
      :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        height: node.height + 'px',
        width: node.width + 'px',
        cursor: currentTool.type == 'zoom-in' ? 'zoom-in' : currentTool.type == 'zoom-out' ? 'zoom-out' : 'default'
      }">
      <div class="lane-text-div" :style="{ top: 0, left: 0, cursor: currentTool.type == 'drag' ? 'move' : 'default' }"
        @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
        <span class="lane-text">{{ node.name }}</span>
      </div>
    </div>

    <div v-else-if="node.type == 'y-lane'" :id="node.id" class="common-y-lane-node" :class="{ laneActive: isActive() }"
      :style="{
        top: node.top + 'px',
        left: node.left + 'px',
        height: node.height + 'px',
        width: node.width + 'px',
        cursor: currentTool.type == 'zoom-in' ? 'zoom-in' : currentTool.type == 'zoom-out' ? 'zoom-out' : 'default'
      }">
      <div class="lane-text-div" :style="{ cursor: currentTool.type == 'drag' ? 'move' : 'default' }"
        @click.stop="selectNode" @contextmenu.stop="showNodeContextMenu">
        <span class="lane-text">{{ node.name }}</span>
      </div>
    </div>

    <div v-else></div>
  </a-tooltip>
</template>

<script setup>
import { ref, computed, watch, onMounted } from 'vue'
import { flowConfig } from '../config/args-config'
import {
  PlayCircleOutlined,
  StopOutlined,
  ToolOutlined,
  ForkOutlined,
  MergeCellsOutlined,
  TeamOutlined,
  ReloadOutlined,
  ApiOutlined
} from '@ant-design/icons-vue'

const props = defineProps({
  select: Object,
  selectGroup: Array,
  node: Object,
  plumb: Object,
  currentTool: Object,
  isShowContent: Boolean
})

const emit = defineEmits(['update:select', 'update:selectGroup', 'showNodeContextMenu', 'isMultiple', 'updateNodePos', 'alignForLine', 'hideAlignLine'])

const contentHtml = ref('')
const currentSelect = ref(props.select || {})
const currentSelectGroup = ref(props.selectGroup || [])

const setInfo = ref({
  NodeRejectType: 0,
  NodeConfluenceType: '',
  NodeDesignate: '',
  NodeDesignateData: {
    users: [],
    roles: [],
    orgs: [],
    Texts: ''
  }
})

// 图标组件映射
const iconMap = {
  PlayCircleOutlined,
  StopOutlined,
  ToolOutlined,
  ForkOutlined,
  MergeCellsOutlined,
  TeamOutlined,
  ReloadOutlined,
  ApiOutlined
}

const getIconComponent = (iconName) => {
  return iconMap[iconName] || ToolOutlined
}

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

// 监听 currentSelectGroup 变化
watch(
  currentSelectGroup,
  (val) => {
    emit('update:selectGroup', val)
  },
  { deep: true }
)

const registerNode = () => {
  if (!props.node.id || props.isShowContent) {
    return
  }
  props.plumb.draggable(props.node.id, {
    containment: 'parent',
    handle: function (e, el) {
      const possibles = el.parentNode.querySelectorAll(
        '.common-circle-node,.common-rectangle-node,.common-diamond-node,.lane-text-div'
      )
      for (let i = 0; i < possibles.length; i++) {
        if (possibles[i] === el || e.target.className === 'lane-text') return true
      }
      return false
    },
    grid: flowConfig.defaultStyle.alignGridPX,
    drag: function (e) {
      if (props.node.type === 'x-lane' || props.node.type === 'y-lane') {
        props.node.left = e.pos[0]
        props.node.top = e.pos[1]
      }
      if (flowConfig.defaultStyle.isOpenAuxiliaryLine) {
        emit('alignForLine', e)
      }
    },
    stop: function (e) {
      if (props.node.type !== 'x-lane' && props.node.type !== 'y-lane') {
        props.node.left = e.pos[0]
        props.node.top = e.pos[1]
      }
      if (currentSelectGroup.value.length > 1) {
        emit('updateNodePos')
      }
      emit('hideAlignLine')
    }
  })
  const currentSelectNode = { ...props.node, setInfo: props.node.setInfo || setInfo.value }
  saveCurrentSelect(currentSelectNode)
  currentSelectGroup.value = []
}

const selectNode = () => {
  if (currentSelectGroup.value.length > 0) {
    return
  }
  const currentSelectNode = Object.assign({}, props.node)
  saveCurrentSelect(currentSelectNode)
  emit('isMultiple', (flag) => {
    if (!flag) {
      currentSelectGroup.value = []
    } else {
      const f = currentSelectGroup.value.filter((s) => s.id === props.node.id)
      if (f.length <= 0) {
        props.plumb.addToDragSelection(props.node.id)
        currentSelectGroup.value.push(props.node)
      }
    }
  })
}

const showNodeContextMenu = (e) => {
  emit('showNodeContextMenu', e)
  selectNode()
}

const stateClass = () => {
  let classname = ''
  if (isActive()) {
    classname += 'active'
  }
  if (
    props.node.setInfo !== undefined &&
    props.node.setInfo !== null &&
    props.node.setInfo.Taged !== undefined &&
    props.node.setInfo.Taged !== null
  ) {
    if (props.node.setInfo.Taged === 2) {
      classname += ' node-not-bg'
    } else if (props.node.setInfo.Taged === 1) {
      classname += ' node-pass-bg'
    } else {
      classname += ' node-back-bg'
    }
  }
  return classname
}

const isActive = () => {
  if (currentSelect.value.id === props.node.id) return true
  const f = currentSelectGroup.value.filter((n) => n.id === props.node.id)
  if (f.length > 0) return true
  return false
}

const isDisabled = () => {
  let flag = true
  if (
    props.node.setInfo !== undefined &&
    props.node.setInfo !== null &&
    props.node.setInfo.Taged !== undefined &&
    props.node.setInfo.Taged !== null
  ) {
    let tips = '<div style="text-align:left">'
    const tagname = { '1': '通过', '2': '不通过', '3': '驳回' }
    tips += '<p>处理人：' + props.node.setInfo.UserName + '</p>'
    tips += '<p>结果：' + tagname[props.node.setInfo.Taged] + '</p>'
    tips += '<p>处理时间：' + props.node.setInfo.TagedTime + '</p>'
    tips += '<p>备注：' + props.node.setInfo.Description + '</p></div>'
    contentHtml.value = tips
    flag = false
  }
  return flag
}

onMounted(() => {
  if (!props.isShowContent) {
    registerNode()
  }
})
</script>

<style lang="scss">
@import '../style/flow-node.scss';

.node-loading-bg {
  background: #5bc0de;
  color: #fff;
}

.node-not-bg {
  background: #d9534f;
  color: #fff;
}

.node-pass-bg {
  background: #5cb85c;
  color: #fff;
}

.node-back-bg {
  background: #f0ad4e;
  color: #fff;
}
</style>
