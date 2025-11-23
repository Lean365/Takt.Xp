//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktFlagIcon/index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 国旗图标选择组件
//===================================================================

<template>
  <a-select
    v-model:value="selectedCountry"
    show-search
    :filter-option="filterOption"
    :options="options"
    :field-names="{ label: 'label', value: 'value' }"
    :placeholder="placeholder"
    @change="handleChange"
    @blur="handleBlur"
    class="ant-flag-select"
  >
    <template #option="{ code, name, nameEn, nameShort }">
      <div class="ant-flag-option">
        <span :class="['fi', `fi-${code.toLowerCase()}`]"></span>
        <span class="country-name">{{ nameEn }} ({{ name }}) [{{ nameShort }}]</span>
      </div>
    </template>
    <template #suffixIcon>
      <span v-if="selectedCountry" :class="['fi', `fi-${selectedCountry.toLowerCase()}`]"></span>
    </template>
    <template #notFoundContent>
      <a-empty :image="simpleImage" description="未找到匹配的国家" />
    </template>
  </a-select>
</template>

<script lang="ts" setup>
import { ref, computed, watch } from 'vue'
import { Empty } from 'ant-design-vue'
import type { SelectValue, DefaultOptionType } from 'ant-design-vue/es/select'
import 'flag-icons/css/flag-icons.min.css'
import { useI18n } from 'vue-i18n'
import countries from 'i18n-iso-countries'
import languages from '@cospired/i18n-iso-languages'
import zh from 'i18n-iso-countries/langs/zh.json'
import en from 'i18n-iso-countries/langs/en.json'
import zhLang from '@cospired/i18n-iso-languages/langs/zh.json'
import enLang from '@cospired/i18n-iso-languages/langs/en.json'
import countryData from 'flag-icons/country.json'

// 注册语言
countries.registerLocale(zh)
countries.registerLocale(en)
languages.registerLocale(zhLang)
languages.registerLocale(enLang)

const { t, locale } = useI18n()

const props = withDefaults(defineProps<{
  modelValue?: string
  placeholder?: string
}>(), {
  modelValue: '',
  placeholder: '请选择图标'
})

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void
  (e: 'change', value: { code: string; name: string }): void
  (e: 'blur'): void
}>()

const simpleImage = Empty.PRESENTED_IMAGE_SIMPLE

interface Country {
  code: string
  name: string
  nameEn: string
  nameShort: string
  langName: string
}

interface CountryInfo {
  code: string
  name: string
  flag_1x1: string
  flag_4x3: string
  iso: boolean
  capital?: string
  continent?: string
}

const countryDataTyped = countryData as CountryInfo[]

// 获取所有国家的ISO代码
const countryCodes = countries.getAlpha2Codes()

// 国家数据
const countriesList = ref<Country[]>(
  Object.keys(countryCodes).map(code => {
    const country = countryDataTyped.find(c => c.code.toLowerCase() === code.toLowerCase())
    const langCode = code.toLowerCase()
    const langName = languages.getName(langCode, locale.value)
    return {
      code,
      name: countries.getName(code, 'zh') || code,
      nameEn: countries.getName(code, 'en') || code,
      nameShort: country?.name || code,
      langName: langName || ''
    }
  })
)

// 选中国家
const selectedCountry = ref(props.modelValue)

// 监听语言变化
watch(locale, (newLocale) => {
  countriesList.value = countriesList.value.map(country => ({
    ...country,
    name: countries.getName(country.code, 'zh') || country.name,
    nameEn: countries.getName(country.code, 'en') || country.nameEn
  }))
})

// 转换为Ant Select需要的options格式
const options = computed(() => {
  return countriesList.value.map(country => ({
    value: country.code,
    label: `${country.nameEn} (${country.name}) [${country.nameShort}] ${country.langName ? `- ${country.langName}` : ''}`,
    code: country.code,
    name: country.name,
    nameEn: country.nameEn,
    nameShort: country.nameShort,
    langName: country.langName
  }))
})

// 自定义筛选函数
const filterOption = (input: string, option: DefaultOptionType) => {
  const searchText = input.toLowerCase()
  return (
    (option.name as string).toLowerCase().includes(searchText) ||
    (option.nameEn as string).toLowerCase().includes(searchText) ||
    (option.nameShort as string).toLowerCase().includes(searchText) ||
    (option.code as string).toLowerCase().includes(searchText) ||
    (option.langName as string).toLowerCase().includes(searchText)
  )
}

// 处理选择变化
const handleChange = (value: SelectValue, option: DefaultOptionType) => {
  if (typeof value === 'string') {
    emit('update:modelValue', value)
    emit('change', {
      code: value,
      name: option.name as string
    })
  }
}

// 处理失焦事件
const handleBlur = () => {
  emit('blur')
}

// 监听外部modelValue变化
watch(() => props.modelValue, (newVal) => {
  if (newVal !== selectedCountry.value) {
    selectedCountry.value = newVal
  }
}, { immediate: true })
</script>

<style lang="less" scoped>
/* 国旗样式 */
.fi {
  width: 20px;
  height: 15px;
  margin-right: 8px;
  border: 1px solid #eee;
  display: inline-block;
  vertical-align: middle;
}

/* 选项样式 */
.ant-flag-option {
  display: flex;
  align-items: center;
  padding: 8px 12px;
}

.country-name {
  margin-left: 8px;
}

/* 选择框内国旗样式 */
:deep(.ant-flag-select .ant-select-selection-item) {
  display: flex;
  align-items: center;
}

:deep(.ant-flag-select .ant-select-selection-item .fi) {
  margin-right: 8px;
}

/* 后缀图标样式 */
:deep(.ant-flag-select .ant-select-suffix) {
  display: flex;
  align-items: center;
}

:deep(.ant-flag-select .ant-select-suffix .fi) {
  margin: 0;
}
</style>

