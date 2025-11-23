<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: Select/index.vue
创建日期: 2024-03-20
描述: 统一封装的选择组件
功能特点:
1. 支持多种选择类型（Select、Radio、Radio.Button、Checkbox、Switch、Rate等）
2. 处理数值类型的自动转换
3. 支持字典数据的自动加载和使用
4. 支持大数据虚拟滚动和远程搜索
5. 保持与ant-design-vue的API一致性
=================================================================== 
-->

<template>
  <component
    :is="controlComponent"
    v-model:value="innerValue"
    v-bind="controlProps"
    @change="handleChange"
    @search="handleSearch"
  >
    <template v-if="getEffectiveType() === 'radio-button'">
      <a-radio-button 
        v-for="option in displayOptions" 
        :key="getOptionKey(option.value)" 
        :value="option.value"
        :disabled="option.disabled"
      >
        {{ option.label }}
      </a-radio-button>
    </template>
    <template v-else-if="getEffectiveType() === 'checkbox'">
      <a-checkbox 
        v-for="option in displayOptions" 
        :key="getOptionKey(option.value)" 
        :value="option.value"
        :disabled="option.disabled"
      >
        {{ option.label }}
      </a-checkbox>
    </template>
  </component>
</template>

<script lang="ts" setup>
import type { SelectProps } from 'ant-design-vue'
import { SelectValue, DefaultOptionType } from 'ant-design-vue/es/select'
import { useDictStore } from '@/stores/dictStore'
import type { DictOption } from '@/stores/dictStore'
import type { TaktSelectOption } from '@/types/common'
import { debounce } from 'lodash-es'
import { useI18n } from 'vue-i18n'
const { t } = useI18n()
const attrs = useAttrs()
const loading = ref(false)
const keyword = ref('')
const page = ref(1)
type ControlType = 'select' | 'radio' | 'radio-button' | 'checkbox' | 'checkbox-group' | 'switch' | 'rate'

interface Props {
  value?: SelectValue
  options?: DefaultOptionType[]
  dictType?: string
  type?: ControlType
  checkedValue?: number | string
  uncheckedValue?: number | string
  count?: number
  remote?: boolean
  showSearch?: boolean
  maxTagCount?: number
  pageSize?: number
  mode?: 'multiple' | 'tags'
  placeholder?: string
  allowClear?: boolean
  loading?: boolean
  filterOption?: boolean | ((input: string, option: DefaultOptionType) => boolean)
  showAll?: boolean
  valueType?: 'string' | 'number'
}

const props = withDefaults(defineProps<Props>(), {
  options: () => [],
  type: 'select',
  checkedValue: 1,
  uncheckedValue: 0,
  count: 5,
  remote: false,
  showSearch: true,
  maxTagCount: 3,
  pageSize: 100,
  placeholder: 'common.form.placeholder.select',
  allowClear: true,
  loading: false,
  showAll: true,
  filterOption: (input: string, option: DefaultOptionType) => {
    return (option?.label as string)?.toLowerCase().includes(input.toLowerCase())
  },
  valueType: 'string'
})

const emit = defineEmits<{
  'update:value': [value: SelectValue]
  'change': [value: SelectValue, option: DefaultOptionType | DefaultOptionType[]]
}>()

// 加载字典数据
const dictStore = useDictStore()
const options = computed(() => props.dictType ? dictStore.getDictOptions(props.dictType) : [])

// 获取选项的key值
const getOptionKey = (value: any): string => {
  if (value === undefined || value === null) {
    return 'empty';
  }
  if (typeof value === 'number' && isNaN(value)) {
    return 'nan';
  }
  return String(value);
}

// 根据值查找对应的选项
const findOptionByValue = (value: any, options: DefaultOptionType[]): DefaultOptionType | undefined => {
  if (value === undefined || value === null) {
    return undefined;
  }
  // 用字符串比较，兼容类型
  return options.find(opt => String(opt.value) === String(value));
}

// 显示选项
const displayOptions = computed(() => {
  let opts: DefaultOptionType[] = []
  
  // 如果是 radio 类型且 showAll 为 true，先添加"全部"选项
  if (props.type === 'radio' && props.showAll) {
    opts.push({
      label: t('select.all'),
      value: -1,
      disabled: false,
      cssClass: '',
      listClass: '',
      extLabel: '',
      extValue: '',
      transKey: ''
    })
  }
  
  // 添加其他选项
  if (props.options && props.options.length > 0) {
    opts = [...opts, ...props.options.map(opt => ({
      ...opt,
      value: opt.value, // 保持原始类型
      key: getOptionKey(opt.value)
    }))]
  } else if (props.dictType) {
    opts = [...opts, ...options.value.map(opt => ({
      ...opt,
      value: opt.value, // 保持原始类型
      key: getOptionKey(opt.value)
    }))]
  }
  
  return opts
})

// 获取选项数量
const optionCount = computed(() => {
  let count = 0
  
  // 计算选项数量
  if (props.options && props.options.length > 0) {
    count = props.options.length
  } else if (props.dictType) {
    count = options.value.length
  }
  
  // 如果是 radio 类型且 showAll 为 true，需要加1（全部选项）
  if (props.type === 'radio' && props.showAll) {
    count += 1
  }
  
  return count
})

// 根据选项数量获取有效的组件类型
const getEffectiveType = (): ControlType => {
  const count = optionCount.value
  
  // 如果选项数量大于3，强制使用select
  if (count > 3) {
    return 'select'
  }
  
  // 如果选项数量小于等于3，且用户指定了radio类型，则使用radio
  if (count <= 3 && (props.type === 'radio' || props.type === 'radio-button')) {
    return props.type
  }
  
  // 其他情况使用select
  return 'select'
}

// 根据type确定使用哪个组件
const controlComponent = computed(() => {
  // 根据选项数量自动选择组件类型
  const effectiveType = getEffectiveType()
  
  //console.log('[TaktSelect] effectiveType:', effectiveType)
  switch (effectiveType) {
    case 'radio':
      return 'a-radio-group'
    case 'radio-button':
      return 'a-radio-group'
    case 'checkbox':
      return 'a-checkbox-group'
    case 'checkbox-group':
      return 'a-checkbox-group'
    case 'switch':
      return 'a-switch'
    case 'rate':
      return 'a-rate'
    default:
      return 'a-select'
  }
})

// 获取本地化的占位符文本
const localizedPlaceholder = computed(() => {
  return props.placeholder ? t(props.placeholder) : ''
})

// 根据控件类型设置不同的属性
const controlProps = computed(() => {
  const baseProps = { ...attrs }
  const effectiveType = getEffectiveType()
  //console.log('[TaktSelect] controlProps effectiveType:', effectiveType)
  
  switch (effectiveType) {
    case 'select':
      return {
        ...baseProps,
        options: displayOptions.value,
        loading: loading.value,
        showSearch: props.showSearch,
        filterOption: props.remote ? false : props.filterOption,
        maxTagCount: props.maxTagCount,
        mode: props.mode,
        placeholder: localizedPlaceholder.value,
        allowClear: props.allowClear,
        virtual: true,
        onPopupScroll: props.remote ? handlePopupScroll : undefined,
        onSearch: props.remote ? handleSearch : undefined
      }
    case 'radio':
      return {
        ...baseProps,
        optionType: 'default',
        options: displayOptions.value,
        placeholder: localizedPlaceholder.value,
        direction: 'horizontal',
        allowClear: props.allowClear,
        value: innerValue.value
      }
    case 'radio-button':
      return {
        ...baseProps,
        optionType: 'button',
        options: displayOptions.value,
        placeholder: localizedPlaceholder.value,
        buttonStyle: 'solid',
        direction: 'horizontal',
        allowClear: props.allowClear,
        value: innerValue.value
      }
    case 'checkbox':
    case 'checkbox-group':
      return {
        ...baseProps,
        options: displayOptions.value,
        placeholder: localizedPlaceholder.value
      }
    case 'switch':
      return {
        ...baseProps,
        checkedValue: props.checkedValue,
        unCheckedValue: props.uncheckedValue,
        checkedChildren: t('common.yesNo.yes'),
        unCheckedChildren: t('common.yesNo.no')
      }
    case 'rate':
      return {
        ...baseProps,
        count: props.count
      }
    default:
      return baseProps
  }
})

// 将数值类型统一处理用于内部显示
const innerValue = computed({
  get: () => {
    const opts = displayOptions.value;
    const value = props.value;
    
    console.log('[TaktSelect] get value:', value, 'mode:', props.mode, 'options:', opts);
    
    // 如果选项还没有加载完成，返回原始值
    if (opts.length === 0 && (props.options || props.dictType)) {
      return value;
    }
    
    // 对于multiple模式，确保返回数组
    if (props.mode === 'multiple' || props.mode === 'tags') {
      if (Array.isArray(value)) {
        return value;
      } else if (value === undefined || value === null) {
        return [];
      } else {
        // 如果不是数组，尝试转换为数组
        return [value];
      }
    }
    
    // 对于radio类型，如果值为undefined或null，返回-1（全部）
    if (props.type === 'radio' && (value === undefined || value === null)) {
      return -1;
    }
    
    // 对于其他类型，尝试查找匹配的选项
    const matchingOption = findOptionByValue(value, opts);
    
    // 如果找到匹配的选项，返回其值
    if (matchingOption) {
      return matchingOption.value;
    }
    
    // 如果没有找到匹配的选项，返回原始值
    return value;
  },
  set: (val: SelectValue) => {
    console.log('[TaktSelect] set value:', val);
    emit('update:value', val);
  }
})

// 处理change事件
const handleChange = (value: SelectValue, option: DefaultOptionType | DefaultOptionType[]) => {
  let convertedValue: any;
  const effectiveType = getEffectiveType()
  
  switch (effectiveType) {
    case 'switch':
      convertedValue = value ? props.checkedValue : props.uncheckedValue;
      break;
    case 'checkbox':
    case 'checkbox-group':
      convertedValue = Array.isArray(value) ? value : [value];
      break;
    case 'radio':
      // 对于radio类型，确保只返回单个值
      convertedValue = Array.isArray(value) ? value[0] : value;
      // 如果是"全部"选项，返回 -1
      if (convertedValue === 0) {
        convertedValue = -1;
      }
      // 如果是 undefined 或 null，返回 -1
      if (convertedValue === undefined || convertedValue === null) {
        convertedValue = -1;
      }
      break;
    case 'select':
      // 对于select类型，直接使用值
      convertedValue = value;
      break;
    default:
      convertedValue = value;
  }
  
  emit('change', convertedValue, option);
}

// 处理远程搜索
const handleSearch = props.type === 'select' ? debounce(async (value: string) => {
  if (!props.remote) return
  
  keyword.value = value
  page.value = 1
}, 300) : undefined

// 处理滚动加载
const handlePopupScroll = props.type === 'select' ? async (e: Event) => {
  if (!props.remote) return
  
  const target = e.target as HTMLElement
  if (
    !loading.value &&
    target.scrollTop + target.clientHeight >= target.scrollHeight - 20
  ) {
    page.value++
  }
} : undefined

// 监听dictType变化，自动加载字典数据
watch(() => props.dictType, async (newType) => {
  if (newType) {
    page.value = 1
    keyword.value = ''
  }
}, { immediate: true })

// 组件挂载时加载字典数据
onMounted(() => {
  if (props.dictType) {
    dictStore.loadDict(props.dictType)
  }
})
</script>

