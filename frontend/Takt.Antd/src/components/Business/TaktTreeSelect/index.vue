<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: TreeSelect/index.vue
创建日期: 2024-03-20
描述: 统一封装的树选择组件
功能特点:
1. 支持多种数据源（本地数据、远程数据、字典数据等）
2. 支持异步加载子节点
3. 支持单选和多选模式
4. 支持自定义字段映射
5. 支持搜索过滤
6. 支持虚拟滚动
7. 保持与ant-design-vue的API一致性
=================================================================== 
-->

<template>
  <a-tree-select
    v-model:value="innerValue"
    v-bind="treeSelectProps"
    :tree-data="treeData"
    :field-names="fieldNames"
    :loading="loading"
    @change="handleChange"
    @search="handleSearch"
    @select="handleSelect"
    @treeExpand="handleTreeExpand"
  >
    <template v-if="$slots.title" #title="titleProps">
      <slot name="title" v-bind="titleProps" />
    </template>
    <template v-if="$slots.switcherIcon" #switcherIcon="switcherProps">
      <slot name="switcherIcon" v-bind="switcherProps" />
    </template>
  </a-tree-select>
</template>

<script lang="ts" setup>
import type { TreeSelectProps } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { debounce } from 'lodash-es'
import type { TreeNode } from 'ant-design-vue/es/tree'
import type { DefaultOptionType } from 'ant-design-vue/es/select'
import type { TreeSelectProps as ATreeSelectProps } from 'ant-design-vue/es/tree-select'

const { t } = useI18n()
const attrs = useAttrs()
const loading = ref(false)
const keyword = ref('')

interface TreeFieldNames {
  children?: string
  label?: string
  value?: string
  key?: string
}

interface TreeNodeData {
  key?: string | number
  title?: string
  value?: string | number
  label?: string
  children?: TreeNodeData[]
  disabled?: boolean
  selectable?: boolean
  checkable?: boolean
  disableCheckbox?: boolean
  isLeaf?: boolean
  [key: string]: any
}

interface Props {
  value?: string | number | (string | number)[]
  treeData?: TreeNodeData[]
  fieldNames?: TreeFieldNames
  remote?: boolean
  remoteMethod?: (params: any) => Promise<TreeNodeData[]>
  showSearch?: boolean
  multiple?: boolean
  treeCheckable?: boolean
  treeDefaultExpandAll?: boolean
  treeDefaultExpandedKeys?: (string | number)[]
  treeNodeFilterProp?: string
  virtual?: boolean
  loading?: boolean
  placeholder?: string
  allowClear?: boolean
  maxTagCount?: number
  disabled?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  treeData: () => [],
  fieldNames: () => ({
    children: 'children',
    label: 'label',
    value: 'value'
  }),
  remote: false,
  showSearch: true,
  multiple: false,
  treeCheckable: false,
  treeDefaultExpandAll: false,
  virtual: true,
  placeholder: 'common.form.placeholder.select',
  allowClear: true,
  disabled: false
})

const emit = defineEmits<{
  'update:value': [value: string | number | (string | number)[]]
  'change': [value: string | number | (string | number)[], label: string | string[], extra: any]
  'select': [value: string | number, node: TreeNodeData, extra: any]
  'search': [value: string]
  'treeExpand': [expandedKeys: (string | number)[]]
}>()

// 内部值处理
const innerValue = computed({
  get: () => props.value,
  set: (val) => {
    if (val !== undefined) {
      emit('update:value', val)
    }
  }
})

// 树选择器属性
const treeSelectProps = computed(() => {
  return {
    ...attrs,
    showSearch: props.showSearch,
    multiple: props.multiple,
    treeCheckable: props.treeCheckable,
    treeDefaultExpandAll: props.treeDefaultExpandAll,
    treeDefaultExpandedKeys: props.treeDefaultExpandedKeys,
    treeNodeFilterProp: props.treeNodeFilterProp,
    virtual: props.virtual,
    loading: loading.value,
    placeholder: t(props.placeholder),
    allowClear: props.allowClear,
    maxTagCount: props.maxTagCount,
    disabled: props.disabled,
    filterTreeNode: props.showSearch ? handleFilterTreeNode : undefined,
    loadData: props.remote ? handleLoadData : undefined
  }
})

// 树数据
const treeData = ref<TreeNodeData[]>(props.treeData)

// 处理变更事件
const handleChange = (value: string | number | (string | number)[], label: string | string[], extra: any) => {
  emit('change', value, label, extra)
}

// 处理选择事件
const handleSelect: ATreeSelectProps['onSelect'] = (value: any, node: any) => {
  emit('select', value as string | number, node as unknown as TreeNodeData, { selected: true })
}

// 处理搜索事件
const handleSearch = props.showSearch ? debounce(async (value: string) => {
  if (!props.remote) return
  keyword.value = value
  emit('search', value)
  await loadRemoteData()
}, 300) : undefined

// 处理树节点展开事件
const handleTreeExpand = (expandedKeys: (string | number)[]) => {
  emit('treeExpand', expandedKeys)
}

// 处理节点过滤
const handleFilterTreeNode = (input: string, node: TreeNodeData) => {
  if (!input || !node[props.treeNodeFilterProp || 'title']) return true
  const nodeText = String(node[props.treeNodeFilterProp || 'title']).toLowerCase()
  return nodeText.includes(input.toLowerCase())
}

// 处理节点加载
const handleLoadData = async (node: TreeNodeData) => {
  if (!props.remote || !props.remoteMethod) return

  const { value: valueField, children: childrenField } = props.fieldNames
  if (node[childrenField || 'children'] && (node[childrenField || 'children'] as TreeNodeData[]).length > 0) return

  loading.value = true
  try {
    const data = await props.remoteMethod({
      parentId: node[valueField || 'value'],
      keyword: keyword.value
    })
    if (data && data.length > 0) {
      node[childrenField || 'children'] = data
    }
  } finally {
    loading.value = false
  }
}

// 加载远程数据
const loadRemoteData = async () => {
  if (!props.remote || !props.remoteMethod) return

  loading.value = true
  try {
    const data = await props.remoteMethod({
      keyword: keyword.value
    })
    treeData.value = data
  } finally {
    loading.value = false
  }
}

// 监听远程数据变化
watch(() => props.remote, async (newVal) => {
  if (newVal) {
    await loadRemoteData()
  }
}, { immediate: true })

// 监听本地数据变化
watch(() => props.treeData, (newVal) => {
  if (!props.remote) {
    treeData.value = newVal
  }
}, { immediate: true })
</script> 
