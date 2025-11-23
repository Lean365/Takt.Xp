<!-- 
//===================================================================
// 项目名 : Lean.Takt
// 文件名 : index.vue  
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 统一封装的通用分页组件
//===================================================================
-->

<template>
  <div class="Takt-pagination" :class="[align, { 'Takt-pagination-mini': small }]">
    <a-pagination
      v-model:current="currentPage"
      v-model:pageSize="pageSize"
      :total="total"
      :show-total="computedShowTotal"
      :show-size-changer="showSizeChanger"
      :show-quick-jumper="showQuickJumper"
      :disabled="disabled"
      :page-size-options="pageSizeOptions"
      :size="size"
      :simple="simple"
      :responsive="responsive"
      :hide-on-single-page="hideOnSinglePage"
      :show-less-items="showLessItems"
      :item-render="itemRender"
      :default-current="defaultCurrent"
      :default-page-size="defaultPageSize"
      @change="handleChange"
      @showSizeChange="handleSizeChange"
      @jumpTo="handleJumpTo"
      role="navigation"
      aria-label="分页导航"
    >
      <template #itemRender="{ page, type, originalElement }" v-if="$slots.itemRender">
        <slot name="itemRender" :page="page" :type="type" :original="originalElement" />
      </template>
      <template #buildOptionText="{ value }" v-if="$slots.buildOptionText">
        <slot name="buildOptionText" :value="value" />
      </template>
      <template #total="range" v-if="$slots.total">
        <slot name="total" v-bind="range" />
      </template>
    </a-pagination>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { VNode } from 'vue'

/**
 * 分页组件属性接口
 */
interface TaktPaginationProps {
  /** 当前页码 */
  current?: number;
  /** 每页条数 */
  pageSize?: number;
  /** 数据总数 */
  total: number;
  /** 是否显示快速跳转 */
  showQuickJumper?: boolean;
  /** 是否显示每页条数选择器 */
  showSizeChanger?: boolean;
  /** 是否显示总数 */
  showTotal?: (total: number, range: [number, number]) => VNode;
  /** 是否禁用 */
  disabled?: boolean;
  /** 每页条数选项 */
  pageSizeOptions?: string[];
  /** 组件大小 */
  size?: 'small' | 'default';
  /** 是否使用简单模式 */
  simple?: boolean;
  /** 对齐方式 */
  align?: 'left' | 'center' | 'right';
  /** 是否显示较少页面内容 */
  small?: boolean;
  /** 用于自定义页码的结构 */
  itemRender?: (opt: { page: number; type: 'page' | 'prev' | 'next' | 'jump-prev' | 'jump-next'; originalElement: any }) => any;
  /** 当添加该属性时，显示为简单分页 */
  responsive?: boolean;
  /** 指定每页可以显示多少条 */
  defaultPageSize?: number;
  /** 指定默认的当前页数 */
  defaultCurrent?: number;
  /** 只有一页时是否隐藏分页器 */
  hideOnSinglePage?: boolean;
  /** 当为「small」时，是否显示较少页面内容 */
  showLessItems?: boolean;
  /** 主题模式 */
  theme?: 'light' | 'dark';
}

const props = withDefaults(defineProps<TaktPaginationProps>(), {
  current: 1,
  pageSize: 10,
  showQuickJumper: true,
  showSizeChanger: true,
  showTotal: undefined,
  disabled: false,
  pageSizeOptions: () => ['10', '20', '50', '100'],
  size: 'default',
  simple: false,
  align: 'right',
  small: false,
  itemRender: undefined,
  responsive: false,
  defaultPageSize: 10,
  defaultCurrent: 1,
  hideOnSinglePage: false,
  showLessItems: false,
  theme: 'light'
})

/**
 * 组件事件
 */
const emit = defineEmits<{
  /**
   * 更新当前页码事件
   * @param page 新的页码
   */
  (e: 'update:current', page: number): void;
  /**
   * 更新每页条数事件
   * @param size 新的每页条数
   */
  (e: 'update:pageSize', size: number): void;
  /**
   * 分页变更事件
   * @param page 新的页码
   * @param pageSize 新的每页条数
   */
  (e: 'change', page: number, pageSize: number): void;
  /**
   * 页码改变的回调
   * @param page 改变后的页码
   * @param pageSize 每页条数
   */
  (e: 'pageChange', page: number, pageSize: number): void;
  /**
   * 每页条数改变的回调
   * @param size 改变后的每页条数
   * @param current 当前页码
   */
  (e: 'sizeChange', size: number, current: number): void;
  /**
   * 快速跳转时的回调
   * @param page 跳转的页码
   */
  (e: 'jumpTo', page: number): void;
}>()

// 使用vue-i18n
const { t } = useI18n()

// 当前页码
const currentPage = ref(props.current)
const pageSize = ref(props.pageSize)

// 计算主题类名
const themeClass = computed(() => props.theme === 'dark' ? 'dark' : '')

// 计算显示总数的函数
const computedShowTotal = computed(() => {
  if (props.showTotal) {
    return props.showTotal
  }
  
  // 默认的显示总数函数，使用翻译
  return (total: number, range: [number, number]) => {
    return t('pagination.total', { total })
  }
})

// 监听props变化
watch(() => props.current, (val) => {
  currentPage.value = val
})

watch(() => props.pageSize, (val) => {
  pageSize.value = val
})

/**
 * 页码改变事件处理
 * @param page 新的页码
 * @param size 新的每页条数
 */
const handleChange = (page: number, size: number) => {
  emit('update:current', page)
  emit('update:pageSize', size)
  emit('change', page, size)
  emit('pageChange', page, size)
}

/**
 * 每页条数改变事件处理
 * @param current 当前页码
 * @param size 新的每页条数
 */
const handleSizeChange = (current: number, size: number) => {
  emit('update:pageSize', size)
  emit('change', current, size)
  emit('sizeChange', size, current)
}

/**
 * 快速跳转事件处理
 * @param page 跳转的页码
 */
const handleJumpTo = (page: number) => {
  emit('jumpTo', page)
}
</script>

<style lang="less" scoped>
.Takt-pagination {
  margin: 4px 0;
  padding: 0;
  display: flex;

  &.left {
    justify-content: flex-start;
  }

  &.center {
    justify-content: center;
  }

  &.right {
    justify-content: flex-end;
  }

  &.Takt-pagination-mini {
    :deep(.ant-pagination-options) {
      margin-left: 8px;
    }
  }
}
</style> 
