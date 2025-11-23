<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: table/index.vue
创建日期: 2024-03-20
描述: 统一封装的表格组件，提供数据展示、分页、排序、筛选、行选择等功能
功能特点:
1. 支持数据展示和分页
2. 支持行选择（单选/多选）
3. 支持表格排序和筛选
4. 支持虚拟滚动优化性能
5. 支持自定义列配置
6. 支持表格大小和边框样式设置
=================================================================== 
-->

<template>
  <div class="Takt-table-container">
    <!-- 表格主体 - 基于 Ant Design Vue 的 Table 组件封装 -->
    <a-table
      :columns="translatedColumns"
      :data-source="dataSource"
      :loading="loading"
      :pagination="paginationConfig"
      :row-selection="rowSelectionConfig"
      :scroll="scrollConfig"
      :bordered="bordered"
      :size="size"
      :virtual="enableVirtual"
      :row-height="rowHeight"
      :row-key="rowKey"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 动态插槽渲染 - 支持自定义列内容 -->
      <template #[item]="data" v-for="item in Object.keys($slots)">
        <slot :name="item" v-bind="data"></slot>
      </template>
    </a-table>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import i18n from '@/locales'
import type { TableProps, TablePaginationConfig } from 'ant-design-vue'

const { t } = useI18n()

// 强制更新触发器
const forceUpdate = ref(0)

// === 类型定义 ===
interface Props {
  // 表格列配置 - 定义表格的列结构，包括标题、数据字段、宽度等
  columns?: any[]
  
  // 表格数据源 - 要展示的数据数组
  dataSource?: any[]
  
  // 加载状态 - 控制表格的加载中状态显示
  loading?: boolean
  
  // 分页配置 - 设置分页器的参数，如每页条数、是否显示快速跳转等
  pagination?: false | TablePaginationConfig
  
  // 行选择配置 - 设置行选择的类型（单选/多选）和选择事件回调
  rowSelection?: TableProps['rowSelection']
  
  // 滚动配置 - 设置表格的水平和垂直滚动
  scroll?: TableProps['scroll']
  
  // 是否显示边框 - 控制表格单元格的边框显示
  bordered?: boolean
  
  // 表格大小 - 设置表格的尺寸，可选值：small/middle/large
  size?: 'small' | 'middle' | 'large'
  
  // 是否启用虚拟滚动 - 大数据量时优化性能
  enableVirtual?: boolean
  
  // 行高 - 虚拟滚动时的行高设置
  rowHeight?: number
  
  // 启用虚拟滚动的数据量阈值 - 超过该值时自动启用虚拟滚动
  threshold?: number
  
  // 行数据的唯一标识字段 - 用于选择功能的键值
  rowKey?: string | ((record: any) => string)
  
  // 当前选中行的 key 数组
  selectedRowKeys?: (string | number)[]
  
  // 表格默认高度 - 用于设置表格的默认显示高度
  defaultHeight?: number | string
}

// === 属性定义 - 设置属性的默认值 ===
const props = withDefaults(defineProps<Props>(), {
  columns: () => [],
  dataSource: () => [],
  loading: false,
  pagination: false,
  bordered: false,
  size: 'middle',
  enableVirtual: false,
  rowHeight: 54,
  threshold: 100,
  rowKey: 'id',
  selectedRowKeys: () => [],
  defaultHeight: 'auto'
})

// === 事件定义 - 声明组件对外暴露的事件 ===
const emit = defineEmits<{
  // 排序、筛选变化时触发
  'change': [filters: any, sorter: any]
  // 行点击事件
  'row-click': [record: any, index: number, event: Event]
  // 选中项发生变化时触发
  'update:selectedRowKeys': [(string | number)[]]
}>()

// === 计算属性 ===
// 处理列的翻译 - 使表格列标题能够响应语言变化
const translatedColumns = computed(() => {
  if (!props.columns || props.columns.length === 0) return []
  
  return props.columns.map(column => {
    // 如果列标题是函数，则调用函数获取标题
    if (typeof column.title === 'function') {
      return {
        ...column,
        title: column.title()
      }
    }
    // 否则直接返回原列配置
    return column
  })
})

// 行选择配置 - 处理行选择的相关配置和事件
const rowSelectionConfig = computed(() => {
  if (!props.rowSelection) return undefined
  
  const baseConfig = props.rowSelection
  const config = {
    ...baseConfig,
    selectedRowKeys: props.selectedRowKeys,
    onChange: (selectedKeys: (string | number)[], selectedRows: any[]) => {
      emit('update:selectedRowKeys', selectedKeys)
      baseConfig.onChange?.(selectedKeys, selectedRows)
    }
  }
  console.log('【TaktTable】rowSelectionConfig:', config)
  return config
})

// 分页配置 - 处理分页器的显示和功能
const paginationConfig = computed<false | TablePaginationConfig>(() => {
  return false
})

// 表格高度 - 根据传入的scroll.y或defaultHeight计算
const tableHeight = computed(() => {
  const height = props.scroll?.y || props.defaultHeight
  if (!height || height === 'auto') return 'auto'
  return typeof height === 'number' ? `${height}px` : height
})

// 滚动配置 - 处理表格的滚动行为
const scrollConfig = computed(() => {
  const defaultScroll = { 
    x: '100%',
    y: props.defaultHeight
  }
  if (!props.scroll) return defaultScroll

  return {
    ...defaultScroll,
    ...props.scroll
  }
})

// === 事件处理 ===
// 表格变化事件处理 - 处理排序、筛选等变化
const handleTableChange = (filters: any, sorter: any) => {
  emit('change', filters, sorter)
}

// 行点击事件处理
const handleRowClick = (record: any, index: number, event: Event) => {
  emit('row-click', record, index, event)
}

// === 虚拟滚动配置 ===
// 是否启用虚拟滚动 - 根据数据量自动判断
const enableVirtual = computed(() => {
  return props.enableVirtual || props.dataSource.length > props.threshold
})
</script>

<style lang="less">
.Takt-table-container {
  width: 100%;

  .ant-table {
    // 设置表格容器高度
    .ant-table-container {
      .ant-table-body {
        height: v-bind(tableHeight) !important;
        overflow-y: auto;
        overflow-x: auto;
        
        // 自定义滚动条样式
        &::-webkit-scrollbar {
          width: 6px;
          height: 6px;
        }
        
        &::-webkit-scrollbar-thumb {
          background: rgba(0, 0, 0, 0.2);
          border-radius: 3px;
        }
        
        &::-webkit-scrollbar-track {
          background: rgba(0, 0, 0, 0.1);
        }
      }
    }
  }
}
</style> 
