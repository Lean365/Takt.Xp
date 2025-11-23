<template>
  <a-modal
    :open="visible"
    title="分配菜单权限"
    :confirm-loading="confirmLoading"
    @update:visible="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-tree
      v-model:checkedKeys="checkedKeys"
      v-model:expandedKeys="expandedKeys"
      :tree-data="treeData"
      :field-names="{
        children: 'children',
        title: 'title',
        key: 'key'
      }"
      checkable
      :default-expand-all="false"
      @expand="handleExpand"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TreeDataItem } from 'ant-design-vue/es/tree'
import type { EventDataNode } from 'ant-design-vue/es/tree'
import type { TaktMenu } from '@/types/identity/menu'
import { getMenuTree } from '@/api/identity/menu'
import { getRoleMenus, assignRoleMenus } from '@/api/identity/role'

interface Props {
  visible: boolean
  roleId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  roleId: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 菜单树数据
const menuTree = ref<TaktMenu[]>([])
// 转换后的树形数据
const treeData = ref<TreeDataItem[]>([])
// 选中的菜单ID
const checkedKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)
// 展开的节点keys
const expandedKeys = ref<string[]>([])

// 转换树形数据
const transformTreeData = (menus: TaktMenu[]): TreeDataItem[] => {
  return menus.map(menu => ({
    key: String(menu.menuId),
    title: menu.menuName,
    children: menu.children ? transformTreeData(menu.children) : undefined,
    disabled: menu.status === 1
  }))
}

// 处理展开/收缩事件
const handleExpand = (expandedKeys: (string | number)[], info: { node: EventDataNode; expanded: boolean; nativeEvent: MouseEvent }) => {
  console.log('展开的节点:', expandedKeys)
}

// 加载菜单树数据
const loadMenuTree = async () => {
  try {
    const res = await getMenuTree()
    if (res.data) {
      menuTree.value = res.data
      treeData.value = transformTreeData(res.data)
      // 只展开前两项
      expandedKeys.value = treeData.value.slice(0, 2).map(node => node.key as string)
    } else {
      message.error('加载菜单树失败')
    }
  } catch (err) {
    console.error('加载菜单树失败:', err)
    message.error('加载菜单树失败')
  }
}

// 获取所有节点的key
const getAllKeys = (nodes: TreeDataItem[]): string[] => {
  const keys: string[] = []
  const traverse = (node: TreeDataItem) => {
    keys.push(node.key as string)
    if (node.children) {
      node.children.forEach(traverse)
    }
  }
  nodes.forEach(traverse)
  return keys
}

// 加载角色菜单
const loadRoleMenus = async () => {
  if (!props.roleId) return
  try {
    const res = await getRoleMenus(props.roleId)
    if (res.data) {
      checkedKeys.value = res.data.map((item: any) => String(item.menuId))
    } else {
      message.error('加载角色菜单失败')
    }
  } catch (err) {
    console.error('加载角色菜单失败:', err)
    message.error('加载角色菜单失败')
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.roleId) return
  try {
    confirmLoading.value = true
    await assignRoleMenus(props.roleId, checkedKeys.value)
    message.success('分配成功')
    emit('success')
    emit('update:visible', false)
  } catch (err) {
    console.error('分配菜单失败:', err)
  } finally {
    confirmLoading.value = false
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loadMenuTree()
      loadRoleMenus()
    } else {
      checkedKeys.value = []
    }
  }
)
</script> 
