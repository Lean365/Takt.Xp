<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: query/index.vue
创建日期: 2024-03-20
描述: 统一封装的查询组件，提供表单查询、重置、展开收起等功能
=================================================================== 
-->

<template>

    <!-- 查询表单 -->
    <a-form
      ref="formRef"
      :model="formState"
      :label-col="labelCol"
      :wrapper-col="wrapperCol"
      :layout="layout"
      @finish="handleFinish"
      class="Takt-query"
      v-show="show"
    >
      <a-row :gutter="24">
        <!-- 查询字段 -->
        <template v-for="(field, index) in translatedQueryFields" :key="field.name">
          <a-col
            :span="field.span || defaultSpan"
            :xs="24"
            :sm="12"
            :md="field.span || defaultSpan"
            v-show="!collapsed || index < visibleFields"
          >
            <a-form-item
              :label="field.label"
              :name="field.name"
              :rules="field.rules"
              :colon="field.colon"
            >
              <!-- 默认插槽 -->
              <slot
                :name="field.name"
                v-bind="{ field, value: formState[field.name] }"
              >
                <!-- 输入框 -->
                <a-input
                  v-if="field.type === 'input'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder || t('components.query.input.placeholder')"
                  :allowClear="true"
                  :maxLength="field.maxLength"
                  :disabled="field.disabled"
                />

                <!-- 选择框 -->
                <Takt-select
                  v-else-if="field.type === 'select'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder || t('components.query.select.placeholder')"
                  v-bind="field.props"
                  :allow-clear="true"
                  :mode="field.mode"
                  :disabled="field.disabled"
                />

                <!-- 日期选择器 -->
                <a-date-picker
                  v-else-if="field.type === 'date'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder || t('components.query.date.placeholder')"
                  :format="field.format || 'YYYY-MM-DD'"
                  :allowClear="true"
                  :disabled="field.disabled"
                />

                <!-- 日期范围选择器 -->
                <a-range-picker
                  v-else-if="field.type === 'dateRange'"
                  v-model:value="formState[field.name]"
                  :placeholder="[
                    field.placeholder?.[0] || t('components.query.date.start'),
                    field.placeholder?.[1] || t('components.query.date.end')
                  ]"
                  :format="field.format || 'YYYY-MM-DD'"
                  :allowClear="true"
                  :disabled="field.disabled"
                />

                <!-- 数字输入框 -->
                <a-input-number
                  v-else-if="field.type === 'number'"
                  v-model:value="formState[field.name]"
                  :placeholder="field.placeholder"
                  :min="field.min"
                  :max="field.max"
                  :step="field.step"
                  :precision="field.precision"
                  :disabled="field.disabled"
                />

                <!-- 单选框组 -->
                <a-radio-group
                  v-else-if="field.type === 'radio'"
                  v-model:value="formState[field.name]"
                  :options="field.options"
                  :disabled="field.disabled"
                />

                <!-- 复选框组 -->
                <a-checkbox-group
                  v-else-if="field.type === 'checkbox'"
                  v-model:value="formState[field.name]"
                  :options="field.options"
                  :disabled="field.disabled"
                />

                <!-- 级联选择 -->
                <a-cascader
                  v-else-if="field.type === 'cascader'"
                  v-model:value="formState[field.name]"
                  :options="field.options"
                  :placeholder="field.placeholder || t('components.query.select.placeholder')"
                  :allowClear="true"
                  :disabled="field.disabled"
                />
              </slot>
            </a-form-item>
          </a-col>
        </template>

        <!-- 操作按钮 -->
        <a-col
          :span="defaultSpan"
          :xs="24"
          :sm="12"
          :md="defaultSpan"
          class="query-actions"
        >
          <a-space :size="buttonSpace">
            <!-- 搜索按钮 -->
            <a-button
              type="primary"
              html-type="submit"
              :loading="loading"
            >
              <template #icon><search-outlined /></template>
              {{ t('components.query.search') }}
            </a-button>
            
            <!-- 重置按钮 -->
            <a-button @click="handleReset">
              <template #icon><redo-outlined /></template>
              {{ t('components.query.reset') }}
            </a-button>
            
            <!-- 展开/收起按钮 -->
            <a-button
              v-if="showCollapse && queryFields.length > visibleFields"
              type="link"
              @click="toggleCollapse"
            >
              {{ collapsed ? t('components.query.expand') : t('components.query.collapse') }}
              <template #icon>
                <down-outlined :class="{ 'expanded': !collapsed }" />
              </template>
            </a-button>

            <!-- 额外的操作按钮 -->
            <slot name="actions"></slot>
          </a-space>
        </a-col>
      </a-row>
    </a-form>

</template>

<script lang="ts" setup>
import { ref, reactive, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import i18n from '@/locales'
import type { FormInstance } from 'ant-design-vue'
import {
  SearchOutlined,
  RedoOutlined,
  DownOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()

// 强制更新触发器
const forceUpdate = ref(0)

// === 类型定义 ===
type FieldType = 'input' | 'select' | 'date' | 'dateRange' | 'number' | 'radio' | 'checkbox' | 'cascader'

interface QueryField {
  name: string // 字段名
  label: string | (() => string) // 字段标签，支持函数形式
  type: FieldType // 字段类型
  span?: number // 栅格宽度
  placeholder?: string // 占位文本
  rules?: any[] // 验证规则
  options?: { label: string; value: any }[] // 选择项
  format?: string // 日期格式
  min?: number // 最小值
  max?: number // 最大值
  step?: number // 步长
  precision?: number // 精度
  maxLength?: number // 最大长度
  disabled?: boolean // 是否禁用
  colon?: boolean // 是否显示冒号
  mode?: 'multiple' | 'tags' // 选择模式
  props?: any // Additional props for Takt-select
}

interface Props {
  queryFields: QueryField[] // 查询字段配置
  defaultSpan?: number // 默认栅格宽度
  visibleFields?: number // 默认显示字段数
  showCollapse?: boolean // 是否显示展开/收起按钮
  loading?: boolean // 加载状态
  layout?: 'horizontal' | 'vertical' | 'inline' // 表单布局
  labelCol?: object // 标签布局
  wrapperCol?: object // 输入框布局
  buttonSpace?: number // 按钮间距
  show?: boolean // 控制显示/隐藏
  model?: Record<string, any> // 表单模型
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  queryFields: () => [],
  defaultSpan: 6,
  visibleFields: 3,
  showCollapse: true,
  loading: false,
  layout: 'horizontal',
  labelCol: () => ({ span: 8 }),
  wrapperCol: () => ({ span: 16 }),
  buttonSpace: 8,
  show: false
})

// === 事件定义 ===
const emit = defineEmits(['search', 'reset', 'update:show'])

// === 表单相关 ===
const formRef = ref<FormInstance>()
const formState = reactive<Record<string, any>>({})
const collapsed = ref(true)

// === 计算属性 ===
// 处理查询字段的翻译 - 使查询字段标签能够响应语言变化
const translatedQueryFields = computed(() => {
  if (!props.queryFields || props.queryFields.length === 0) return []
  
  return props.queryFields.map(field => {
    // 如果字段标签是函数，则调用函数获取标签
    if (typeof field.label === 'function') {
      return {
        ...field,
        label: field.label()
      }
    }
    // 否则直接返回原字段配置
    return field
  })
})

// === 工具函数：根据字段类型返回默认值 ===
function getDefaultValue(field: QueryField) {
  if (field.type === 'input' || field.type === 'date' || field.type === 'dateRange' || field.type === 'cascader') {
    return ''
  }
  if (field.type === 'select' || field.type === 'radio' || field.type === 'checkbox' || field.type === 'number') {
    return -1
  }
  return ''
}

// === 初始化表单状态 ===
const initFormState = () => {
  translatedQueryFields.value.forEach((field) => {
    if (field.type === 'dateRange') {
      formState[field.name] = []
    } else {
      formState[field.name] = undefined
    }
  })
}

// === 初始化表单 ===
initFormState()

// === 监听语言变化，重新初始化表单状态 ===
watch(
  () => i18n.global.locale.value,
  () => {
    initFormState()
  }
)

// === 监听 model 变化，更新表单状态 ===
watch(
  () => props.model,
  (newModel) => {
    if (newModel) {
      translatedQueryFields.value.forEach((field) => {
        if (field.type === 'dateRange') {
          formState[field.name] = newModel[field.name] || []
        } else {
          formState[field.name] = newModel[field.name]
        }
      })
    }
  },
  { immediate: true, deep: true }
)

// === 事件处理 ===
const handleFinish = (values: any) => {
  // 过滤掉 undefined 值
  const result: Record<string, any> = {}
  Object.entries(values).forEach(([key, value]) => {
    if (value !== undefined) {
      result[key] = value
    }
  })
  emit('search', result)
}

const handleReset = () => {
  props.queryFields.forEach(field => {
    formState[field.name] = getDefaultValue(field)
  })
  formRef.value?.resetFields()
  emit('reset')
}

const toggleCollapse = () => {
  collapsed.value = !collapsed.value
}

// 暴露方法给父组件
defineExpose({
  toggleCollapse,
  handleReset,
  handleFinish
})
</script>

<style lang="less" scoped>
.Takt-query {
  padding: 24px;
  margin-bottom: 16px;

  .query-actions {
    display: flex;
    justify-content: flex-end;
    align-items: flex-start;
  }

  :deep(.expanded) {
    transform: rotate(180deg);
  }
}
</style> 
