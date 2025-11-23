<!--
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 右键菜单组件（替代 vue-contextmenu）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
-->
<template>
  <div v-if="contextMenuData && contextMenuData.axis && contextMenuData.axis.x && contextMenuData.axis.y"
    class="context-menu" :style="{ left: contextMenuData.axis.x + 'px', top: contextMenuData.axis.y + 'px' }"
    @click.stop @contextmenu.prevent>
    <a-menu mode="vertical" :selectable="false">
      <template v-for="(item, index) in contextMenuData.menulists" :key="index">
        <a-sub-menu v-if="item.children && item.children.length > 0" :title="item.btnName">
          <a-menu-item v-for="(child, childIndex) in item.children" :key="childIndex"
            @click="handleMenuClick(child.fnHandler)">
            {{ child.btnName }}
          </a-menu-item>
        </a-sub-menu>
        <a-menu-item v-else @click="handleMenuClick(item.fnHandler)">
          {{ item.btnName }}
        </a-menu-item>
      </template>
    </a-menu>
  </div>
</template>

<script setup>
import { watch } from 'vue'

const props = defineProps({
  contextMenuData: {
    type: Object,
    default: () => ({})
  }
})

const emit = defineEmits(['deleteLink', 'paste', 'selectAll', 'saveFlow', 'verticaLeft', 'verticalCenter', 'verticalRight', 'levelUp', 'levelCenter', 'levelDown', 'copyNode', 'deleteNode'])

const handleMenuClick = (handler) => {
  emit(handler)
  // 点击后隐藏菜单
  if (props.contextMenuData && props.contextMenuData.axis) {
    props.contextMenuData.axis.x = null
    props.contextMenuData.axis.y = null
  }
}

// 点击外部区域关闭菜单
watch(() => props.contextMenuData?.axis, () => {
  if (props.contextMenuData?.axis?.x && props.contextMenuData?.axis?.y) {
    const closeMenu = (e) => {
      if (!e.target.closest('.context-menu')) {
        if (props.contextMenuData && props.contextMenuData.axis) {
          props.contextMenuData.axis.x = null
          props.contextMenuData.axis.y = null
        }
        document.removeEventListener('click', closeMenu)
      }
    }
    setTimeout(() => {
      document.addEventListener('click', closeMenu)
    }, 0)
  }
}, { deep: true })
</script>

<style scoped>
.context-menu {
  position: fixed;
  z-index: 9999;
  background: #fff;
  border: 1px solid #e6e6e6;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
  min-width: 120px;
}
</style>
