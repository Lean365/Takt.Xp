<template>
  <a-tag v-if="hasValue" :class="['Takt-dict-tag', tagClass]">{{ finalLabel }}</a-tag>
  <a-tag v-else color="default" class="Takt-dict-tag">{{ t('common.error.unknown') }}</a-tag>
</template>

<script lang="ts" setup>
import { computed, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { useDictStore } from '@/stores/dictStore'
import '@/assets/styles/components/dict-tag.less'

const { t } = useI18n()

interface Props {
  // 字典类型
  dictType: string
  // 字典值
  value: number | string
  // 是否使用国际化
  useI18n?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  useI18n: false
})

// 使用字典Hook
const dictStore = useDictStore()

// 判断是否有值
const hasValue = computed(() => {
  const result = props.value !== undefined && props.value !== null && String(props.value).trim() !== ''
  //console.log(`[TaktDictTag] 字典[${props.dictType}]值[${props.value}]是否有值:`, result)
  return result
})

// 计算最终显示的标签
const finalLabel = computed(() => {
  if (!hasValue.value) return ''
  const label = dictStore.getDictLabel(props.dictType, props.value)
  //console.log(`[TaktDictTag] 字典[${props.dictType}]值[${props.value}]获取到的标签:`, label)
  if (props.useI18n) {
    const transKey = dictStore.getDictTransKey(props.dictType, props.value)
    //console.log(`[TaktDictTag] 字典[${props.dictType}]值[${props.value}]国际化key:`, transKey)
    return transKey ? t(transKey) : label
  }
  return label
})

// 计算标签样式类
const tagClass = computed(() => {
  if (!hasValue.value) return ''
  const baseClass = dictStore.getDictClass(props.dictType, props.value)
  //console.log(`[TaktDictTag] 字典[${props.dictType}]值[${props.value}]获取到的样式类:`, baseClass)
  return baseClass ? `Takt-dict-tag-${baseClass}` : ''
})

// 监听字典类型和值的变化
watch(() => [props.dictType, props.value], ([newType, newValue]) => {
  // console.log(`[TaktDictTag] 字典类型或值发生变化:`, {
  //   dictType: newType,
  //   value: newValue,
  //   valueType: typeof newValue
  // })
}, { immediate: true })

onMounted(() => {
  if (props.dictType) {
    //console.log(`[TaktDictTag] 组件挂载，加载字典[${props.dictType}]`)
    dictStore.loadDict(props.dictType)
  }
})
</script> 
