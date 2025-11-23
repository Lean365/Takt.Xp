//===================================================================
// 项目名 : Lean.Takt
// 文件名 : DictTypeForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型表单组件
//===================================================================

<template>
  <Takt-modal
    :open="open"
    :title="title"
    width="600px"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item
        :label="t('core.dict.dictTypes.fields.dictCategory.label')"
        name="dictCategory"
      >
        <Takt-select
          v-model:value="formData.dictCategory"
          :placeholder="t('core.dict.dictTypes.fields.dictCategory.placeholder')"
          :disabled="!!dictTypeId"
          dict-type="sys_dict_category"
          type="radio"
          :show-all="false"
          @change="handleDictCategoryChange"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictTypes.fields.dictName.label')"
        name="dictName"
      >
        <a-input
          v-model:value="formData.dictName"
          :placeholder="t('core.dict.dictTypes.fields.dictName.placeholder')"
          :disabled="!!dictTypeId"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictTypes.fields.dictType.label')"
        name="dictType"
      >
        <a-input-group compact>
          <a-form-item name="dictTypeFirst" style="display:inline-block;margin-bottom:0;">
            <a-input
              v-model:value="dictTypeFirst"
              :placeholder="t('core.dict.dictTypes.fields.dictType.first')"
              :disabled="!!dictTypeId || formData.dictCategory === 1"
              style="width: 80px"
              @change="handleDictTypeChange"
            />
          </a-form-item>
          <span style="width: 36px; display: inline-block; text-align: center;">_</span>
          <a-form-item name="dictTypeSecond" style="display:inline-block;margin-bottom:0;">
            <a-input
              v-model:value="dictTypeSecond"
              :placeholder="t('core.dict.dictTypes.fields.dictType.second')"
              :disabled="!!dictTypeId"
              style="width: 80px"
              @change="handleDictTypeChange"
            />
          </a-form-item>
          <span style="width: 36px; display: inline-block; text-align: center;">_</span>
          <a-form-item name="dictTypeThird" style="display:inline-block;margin-bottom:0;">
            <a-input
              v-model:value="dictTypeThird"
              :placeholder="t('core.dict.dictTypes.fields.dictType.third')"
              :disabled="!!dictTypeId"
              style="width: 80px"
              @change="handleDictTypeChange"
            />
          </a-form-item>
        </a-input-group>
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictTypes.fields.isBuiltin.label')"
        name="isBuiltin"
      >
        <Takt-select
          v-model:value="formData.isBuiltin"
          :placeholder="t('core.dict.dictTypes.fields.isBuiltin.placeholder')"
          dict-type="sys_yes_no"
          type="radio"
          :show-all="false"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictTypes.fields.sqlScript.label')"
        name="sqlScript"
      >
        <a-textarea
          v-model:value="formData.sqlScript"
          :placeholder="t('core.dict.dictTypes.fields.sqlScript.placeholder')"
          :rows="4"
          :disabled="formData.dictCategory === 0"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictTypes.fields.orderNum.label')"
        name="orderNum"
      >
        <a-input-number
          v-model:value="formData.orderNum"
          :placeholder="t('core.dict.dictTypes.fields.orderNum.placeholder')"
          :min="0"
          :max="999"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictTypes.fields.status.label')"
        name="status"
      >
        <Takt-select
          v-model:value="formData.status"
          :placeholder="t('core.dict.dictTypes.fields.status.placeholder')"
          dict-type="sys_normal_disable"
          type="radio"
          :show-all="false"
        />
      </a-form-item>


      <a-form-item
        :label="t('core.dict.dictTypes.fields.remark.label')"
        name="remark"
      >
        <a-textarea
          v-model:value="formData.remark"
          :placeholder="t('core.dict.dictTypes.fields.remark.placeholder')"
          :rows="4"
          disabled
        />
      </a-form-item>
    </a-form>

    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel" :disabled="loading">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" @click="handleSubmit" :loading="loading">{{ t('common.button.submit') }}</a-button>
      </div>
    </template>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { SelectValue } from 'ant-design-vue/es/select'
import type { TaktDictType, TaktDictTypeCreate, TaktDictTypeUpdate } from '@/types/routine/dict/dictType'
import { getByIdAsync, createDictType, updateDictType } from '@/api/routine/dict/dictType'
import { useUserStore } from '@/stores/userStore'

const props = defineProps({
  open: {
    type: Boolean,
    default: false
  },
  title: {
    type: String,
    default: ''
  },
  dictTypeId: {
    type: String,
    default: undefined
  }
})

const emit = defineEmits(['update:open', 'success'])

const { t } = useI18n()
const userStore = useUserStore()

// === 状态定义 ===
const loading = ref(false)
const formRef = ref<FormInstance>()
const dictTypeFirst = ref('')
const dictTypeSecond = ref('')
const dictTypeThird = ref('')

// === 表单数据 ===
const formData = reactive<TaktDictTypeCreate>({
  dictName: '',
  dictType: '',
  dictCategory: 0,
  isBuiltin: 0,
  sqlScript: '',
  orderNum: 0,
  status: 0,
  remark: ''
})

// === 表单验证规则 ===
const rules: Record<string, Rule[]> = {
  dictName: [
    { required: true, message: t('core.dict.dictTypes.fields.dictName.validate.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('core.dict.dictTypes.fields.dictName.validate.length'), trigger: 'blur' }
  ],
  dictType: [
    { required: true, message: t('core.dict.dictTypes.fields.dictType.validate.required') }
  ],
  status: [
    { required: true, message: t('core.dict.dictTypes.fields.status.validate.required'), trigger: 'change' }
  ],
  sqlScript: [
    { 
      required: true, 
      message: t('core.dict.dictTypes.fields.sqlScript.validate.required'), 
      trigger: 'blur',
      validator: (rule: any, value: string) => {
        if (formData.dictCategory === 1 && !value) {
          return Promise.reject(t('core.dict.dictTypes.fields.sqlScript.validate.required'))
        }
        return Promise.resolve()
      }
    }
  ]
}

// === 验证方法 ===
// 检查是否包含特殊字符
const hasSpecialChars = (value: string) => {
  if (!value) return false
  return /[.,;:'"`~!@#$%^&*()_+\-=\[\]{};\\|<>/?]/.test(value)
}

// 检查是否包含重叠字符（如 aa、aaa、bb、bbb 等）
const hasOverlappingChars = (value: string) => {
  if (!value) return false
  return /(.)\1+/.test(value)
}

// 检查两个字符串之间是否有三个或以上重复字符
const hasRepeatedCharsBetween = (str1: string, str2: string) => {
  if (!str1 || !str2) return false
  const chars1 = str1.split('')
  const chars2 = str2.split('')
  
  for (const char1 of chars1) {
    let count = 0
    for (const char2 of chars2) {
      if (char1 === char2) {
        count++
        if (count >= 3) return true
      }
    }
  }
  return false
}

// 验证第一部分（3位小写字母）
const validateDictTypeFirst = (value: string) => {
  if (!value) return true
  const regex = /^[a-z]{3}$/
  if (!regex.test(value)) return false
  if (hasOverlappingChars(value)) return false
  if (hasSpecialChars(value)) return false
  if (hasRepeatedCharsBetween(value, dictTypeSecond.value)) return false
  if (hasRepeatedCharsBetween(value, dictTypeThird.value)) return false
  return true
}

// 验证第二部分（3-10位小写字母）
const validateDictTypeSecond = (value: string) => {
  if (!value) return true
  const regex = /^[a-z]{3,10}$/
  if (!regex.test(value)) return false
  if (hasOverlappingChars(value)) return false
  if (hasSpecialChars(value)) return false
  if (hasRepeatedCharsBetween(value, dictTypeFirst.value)) return false
  if (hasRepeatedCharsBetween(value, dictTypeThird.value)) return false
  return true
}

// 验证第三部分（3-10位，小写字母开头，可包含数字）
const validateDictTypeThird = (value: string) => {
  if (!value) return true
  const regex = /^[a-z][a-z0-9]{2,9}$/
  if (!regex.test(value)) return false
  if (hasOverlappingChars(value)) return false
  if (hasSpecialChars(value)) return false
  if (hasRepeatedCharsBetween(value, dictTypeFirst.value)) return false
  if (hasRepeatedCharsBetween(value, dictTypeSecond.value)) return false
  return true
}

// === 方法定义 ===
// 处理字典类型变化
const handleDictTypeChange = () => {
  formData.dictType = `${dictTypeFirst.value}_${dictTypeSecond.value}_${dictTypeThird.value}`
}

// 验证字典类型
const validateDictType = () => {
  if (!formData.dictType) {
    message.error(t('core.dict.dictTypes.fields.dictType.validate.required'))
    return false
  }

  const parts = formData.dictType.split('_')
  if (parts.length !== 3) {
    message.error(t('core.dict.dictTypes.fields.dictType.validate.format'))
    return false
  }

  const [first, second, third] = parts
  if (!validateDictTypeFirst(first)) {
    message.error(t('core.dict.dictTypes.fields.dictType.validate.first'))
    return false
  }
  if (!validateDictTypeSecond(second)) {
    message.error(t('core.dict.dictTypes.fields.dictType.validate.second'))
    return false
  }
  if (!validateDictTypeThird(third)) {
    message.error(t('core.dict.dictTypes.fields.dictType.validate.third'))
    return false
  }
  return true
}

// 处理字典分类变化
const handleDictCategoryChange = (value: SelectValue) => {
  if (value === 1) {
    // SQL字典类型
    dictTypeFirst.value = 'sql'
    formData.sqlScript = ''
  } else {
    // 普通字典类型
    if (dictTypeFirst.value === 'sql') {
      dictTypeFirst.value = ''
    }
    formData.sqlScript = ''
  }
  handleDictTypeChange()
}

// 监听字典分类变化
watch(() => formData.dictCategory, (newValue) => {
  if (newValue === 1) {
    // SQL字典类型
    dictTypeFirst.value = 'sql'
    formData.sqlScript = ''
  } else {
    // 普通字典类型
    if (dictTypeFirst.value === 'sql') {
      dictTypeFirst.value = ''
    }
    formData.sqlScript = ''
  }
  handleDictTypeChange()
})

// 获取字典类型详情
const getDictType = async (id: string) => {
  try {
    loading.value = true
    const res = await getByIdAsync(String(id))
    if (res.data) {
      const data = res.data
      // 解析字典类型
      const parts = (data.dictType || '').split('_')
      if (parts.length === 3) {
        dictTypeFirst.value = parts[0] || ''
        dictTypeSecond.value = parts[1] || ''
        dictTypeThird.value = parts[2] || ''
      }
      Object.assign(formData, data)
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取字典类型详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 提交表单
const handleSubmit = async () => {
  try {
    // 先验证其他字段
    await formRef.value?.validate()
    
    // 再验证字典类型
    if (!validateDictType()) {
      return
    }

    loading.value = true

    let res
    if (props.dictTypeId) {
      // 更新
      const data: TaktDictTypeUpdate = {
        ...formData,
        dictTypeId: props.dictTypeId
      }
      res = await updateDictType(data)
    } else {
      // 新增
      res = await createDictType(formData)
    }

    if (res) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
    } else {
      message.error(t('common.message.saveFailed'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error(t('common.message.saveFailed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:open', false)
}

// 重置表单
const resetForm = () => {
  formRef.value?.resetFields()
  dictTypeFirst.value = ''
  dictTypeSecond.value = ''
  dictTypeThird.value = ''
  Object.assign(formData, {
    dictName: '',
    dictType: '',
    dictCategory: 0,
    isBuiltin: 0,
    sqlScript: '',
    orderNum: 0,
    status: 0,
    remark: ''
  })
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.open, (val) => {
  if (val) {
    if (props.dictTypeId) {
      getDictType(props.dictTypeId)
    } else {
      resetForm()
    }
  }
})

// 监听 dictName 和 dictType 变化，自动更新 remark
watch([() => formData.dictName, () => formData.dictType], ([newDictName, newDictType]) => {
  if (newDictName && newDictType) {
    formData.remark = `${newDictName}（${newDictType}）字典`
  }
})

// === 生命周期 ===
onMounted(() => {
  if (props.open && props.dictTypeId) {
    getDictType(props.dictTypeId)
  }
})
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 
