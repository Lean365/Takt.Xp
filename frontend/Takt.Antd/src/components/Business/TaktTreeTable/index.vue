<template>
  <div class="Takt-tree-table">
    <a-table
      :columns="columns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="pagination"
      :row-selection="rowSelection"
      :expanded-row-keys="expandedRowKeys"
      :indent-size="indentSize"
      :children-column-name="childrenColumnName"
      :row-key="rowKey"
      :size="size"
      :bordered="bordered"
      :scroll="scroll"
      :virtual="true"
      :scrollToFirstRowOnChange="true"
      :row-class-name="getRowClassName"
      :resizable="resizable"
      @expand="onExpand"
      @change="onChange"
    >
      <template v-for="slot in Object.keys($slots)" #[slot]="data">
        <slot :name="slot" v-bind="data"></slot>
      </template>
    </a-table>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import type { TableProps } from 'ant-design-vue'

interface Props {
  // 表格列配置
  columns: TableProps['columns']
  // 数据源
  dataSource: any[]
  // 加载状态
  loading?: boolean
  // 分页配置
  pagination?: TableProps['pagination']
  // 行选择配置
  rowSelection?: TableProps['rowSelection']
  // 展开的行
  expandedRowKeys?: (string | number)[]
  // 缩进大小
  indentSize?: number
  // 子节点字段名
  childrenColumnName?: string
  // 行键名
  rowKey?: string | ((record: any) => string | number)
  // 表格大小
  size?: 'small' | 'middle' | 'large'
  // 是否显示边框
  bordered?: boolean
  // 滚动配置
  scroll?: { x?: number | string | true; y?: number | string }
  // 是否启用虚拟滚动
  virtual?: boolean
  // 每页显示条数
  pageSize?: number
  // 是否启用懒加载
  lazy?: boolean
  // 是否允许调整列宽
  resizable?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  loading: false,
  pagination: false,
  indentSize: 20,
  childrenColumnName: 'children',
  rowKey: 'id',
  size: 'middle',
  bordered: true,
  virtual: true,
  pageSize: 100,
  lazy: true,
  resizable: false,
  scroll: () => ({ x: 1200, y: 'calc(100vh - 300px)' })
})

const emit = defineEmits<{
  (e: 'update:expandedRowKeys', keys: (string | number)[]): void
  (e: 'expand', expanded: boolean, record: any): void
  (e: 'change', pagination: any, filters: any, sorter: any): void
  (e: 'loadData', record: any): void
}>()

// 展开的行
const expandedRowKeys = ref<(string | number)[]>(props.expandedRowKeys || [])

// 监听展开行变化
watch(() => props.expandedRowKeys, (val) => {
  if (val) {
    expandedRowKeys.value = val
  }
})

// 处理展开/收缩事件
const onExpand = async (expanded: boolean, record: any) => {
  if (expanded) {
    // 如果是懒加载模式且没有子节点数据，则触发加载
    if (props.lazy && (!record[props.childrenColumnName] || record[props.childrenColumnName].length === 0)) {
      emit('loadData', record)
    }
    expandedRowKeys.value = [...expandedRowKeys.value, record[props.rowKey as string]]
  } else {
    expandedRowKeys.value = expandedRowKeys.value.filter(key => key !== record[props.rowKey as string])
  }
  emit('update:expandedRowKeys', expandedRowKeys.value)
  emit('expand', expanded, record)
}

// 处理表格变化事件
const onChange = (pagination: any, filters: any, sorter: any) => {
  emit('change', pagination, filters, sorter)
}

// 递归获取所有节点的键值
const getAllKeys = (data: any[]): (string | number)[] => {
  return data.flatMap(item => [
    item[props.rowKey as string],
    ...(item[props.childrenColumnName] ? getAllKeys(item[props.childrenColumnName]) : [])
  ])
}

// 展开所有节点
const expandAll = () => {
  expandedRowKeys.value = getAllKeys(props.dataSource)
  emit('update:expandedRowKeys', expandedRowKeys.value)
}

// 收缩所有节点
const collapseAll = () => {
  expandedRowKeys.value = []
  emit('update:expandedRowKeys', expandedRowKeys.value)
}

// 获取行类名
const getRowClassName = (record: any) => {
  return record[props.childrenColumnName]?.length > 0 ? 'has-children' : ''
}

// 暴露方法
defineExpose({
  expandAll,
  collapseAll,
  getAllKeys
})
</script>

<style lang="less" scoped>
.Takt-tree-table {
  width: 100%;
  height: 100%;

  :deep(.ant-table-row) {
    &.has-children {
      cursor: pointer;
    }
  }

  :deep(.ant-table-row-expand-icon) {
    cursor: pointer;
  }

  :deep(.ant-table-row-level-1) {
    background-color: var(--ant-color-bg-1);
  }

  :deep(.ant-table-row-level-2) {
    background-color: var(--ant-color-bg-2);
  }

  :deep(.ant-table-row-level-3) {
    background-color: var(--ant-color-bg-3);
  }
}
</style> 
