<template>
  <a-modal
    :open="visible"
    title="分配数据权限"
    :confirm-loading="confirmLoading"
    @update:visible="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-tree
      v-model:checkedKeys="checkedKeys"
      :tree-data="treeData"
      :field-names="{
        children: 'children',
        title: 'title',
        key: 'key'
      }"
      checkable
      :default-expand-all="true"
      :expanded-keys="expandedKeys"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TreeDataItem } from 'ant-design-vue/es/tree'
import type { TaktDept } from '@/types/identity/dept'
import { getDeptTree } from '@/api/identity/dept'
import { getRoleDepts, assignRoleDepts } from '@/api/identity/role'

// 展开的节点keys
const expandedKeys = ref<string[]>([])

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

// 部门树数据
const deptTree = ref<TaktDept[]>([])
// 转换后的树形数据
const treeData = ref<TreeDataItem[]>([])
// 选中的部门ID
const checkedKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 转换树形数据
const transformTreeData = (depts: TaktDept[]): TreeDataItem[] => {
  return depts.map(dept => ({
    key: String(dept.deptId || '0'),
    title: dept.deptName,
    children: dept.children ? transformTreeData(dept.children) : undefined,
    disabled: dept.status === 1
  }))
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

// 加载部门树数据
const loadDeptTree = async () => {
  try {
    const res = await getDeptTree({ pageIndex: 1, pageSize: 1000 })
    if (res.data) {
      deptTree.value = res.data
      treeData.value = transformTreeData(res.data)
      // 设置所有节点为展开状态
      expandedKeys.value = getAllKeys(treeData.value)
    } else {
      message.error('加载部门树失败')
    }
  } catch (err) {
    console.error('加载部门树失败:', err)
    message.error('加载部门树失败')
  }
}

// 加载角色部门
const loadRoleDepts = async () => {
  if (!props.roleId) return
  try {
    const res = await getRoleDepts(props.roleId)
    if (res.data) {
      checkedKeys.value = res.data.map((item: any) => String(item.deptId))
    } else {
      message.error('加载角色部门失败')
    }
  } catch (err) {
    console.error('加载角色部门失败:', err)
    message.error('加载角色部门失败')
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.roleId) return
  try {
    confirmLoading.value = true
    await assignRoleDepts(props.roleId, checkedKeys.value)
    message.success('分配成功')
    emit('success')
    emit('update:visible', false)
  } catch (err) {
    console.error('分配部门失败:', err)
  } finally {
    confirmLoading.value = false
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  async (val) => {
    if (val) {
      await loadDeptTree()
      await loadRoleDepts()
    } else {
      checkedKeys.value = []
    }
  }
)
</script> 
