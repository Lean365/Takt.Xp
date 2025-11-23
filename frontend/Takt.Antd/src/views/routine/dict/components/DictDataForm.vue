//===================================================================
// 项目名 : Lean.Takt
// 文件名 : DictDataForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据表单组件
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
        :label="t('core.dict.dictDatas.fields.dictType.label')"
        name="dictType"
      >
        <a-input
          v-model:value="formData.dictType"
          :placeholder="t('core.dict.dictDatas.fields.dictType.placeholder')"
          disabled
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.dictLabel.label')"
        name="dictLabel"
      >
        <a-input
          v-model:value="formData.dictLabel"
          :placeholder="t('core.dict.dictDatas.fields.dictLabel.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.dictValue.label')"
        name="dictValue"
      >
        <a-input
          v-model:value="formData.dictValue"
          :placeholder="t('core.dict.dictDatas.fields.dictValue.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.orderNum.label')"
        name="orderNum"
      >
        <a-input-number
          v-model:value="formData.orderNum"
          :placeholder="t('core.dict.dictDatas.fields.orderNum.placeholder')"
          :min="0"
          :max="999"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.cssClass.label')"
        name="cssClass"
      >
        <a-input
          v-model:value="formData.cssClass"
          :placeholder="t('core.dict.dictDatas.fields.cssClass.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.listClass.label')"
        name="listClass"
      >
        <a-input
          v-model:value="formData.listClass"
          :placeholder="t('core.dict.dictDatas.fields.listClass.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.status.label')"
        name="status"
      >
        <a-radio-group v-model:value="formData.status">
          <a-radio :value="0">{{ t('core.dict.dictDatas.fields.status.normal') }}</a-radio>
          <a-radio :value="1">{{ t('core.dict.dictDatas.fields.status.disable') }}</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.extLabel.label')"
        name="extLabel"
      >
        <a-input
          v-model:value="formData.extLabel"
          :placeholder="t('core.dict.dictDatas.fields.extLabel.placeholder')"
        />
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.extValue.label')"
        name="extValue"
      >
        <a-input
          v-model:value="formData.extValue"
          :placeholder="t('core.dict.dictDatas.fields.extValue.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('core.dict.dictDatas.fields.transKey.label')">
        <a-input-group compact>
          <a-form-item name="transKeyFirst" style="display:inline-block;margin-bottom:0;">
            <a-input v-model:value="transKeyFirst" style="width:80px" :placeholder="t('core.dict.dictDatas.fields.transKey.first')" />
          </a-form-item>
          <span style="width:36px;display:inline-block;text-align:center;">_</span>
          <a-form-item name="transKeySecond" style="display:inline-block;margin-bottom:0;">
            <a-input v-model:value="transKeySecond" style="width:80px" :placeholder="t('core.dict.dictDatas.fields.transKey.second')" />
          </a-form-item>
          <span style="width:36px;display:inline-block;text-align:center;">_</span>
          <a-form-item name="transKeyThird" style="display:inline-block;margin-bottom:0;">
            <a-input v-model:value="transKeyThird" style="width:80px" :placeholder="t('core.dict.dictDatas.fields.transKey.third')" />
          </a-form-item>
        </a-input-group>
      </a-form-item>

      <a-form-item
        :label="t('core.dict.dictDatas.fields.remark.label')"
        name="remark"
      >
        <a-textarea
          v-model:value="formData.remark"
          :placeholder="t('core.dict.dictDatas.fields.remark.placeholder')"
          :rows="4"
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
import type { TaktDictData, TaktDictDataCreate, TaktDictDataUpdate } from '@/types/routine/dict/dictData'
import { getByIdAsync, createDictData, updateDictData } from '@/api/routine/dict/dictData'

const props = defineProps({
  open: {
    type: Boolean,
    default: false
  },
  title: {
    type: String,
    default: ''
  },
  dictType: {
    type: String,
    required: true
  },
  dictDataId: {
    type: String,
    default: undefined
  }
})

const emit = defineEmits(['update:open', 'success'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const formRef = ref<FormInstance>()
const transKeyFirst = ref('')
const transKeySecond = ref('')
const transKeyThird = ref('')

// === 表单数据 ===
const formData = reactive<TaktDictDataCreate>({
  dictType: props.dictType,
  dictLabel: '',
  dictValue: '',
  orderNum: 0,
  cssClass: '',
  listClass: '',
  status: 0,
  extLabel: '',
  extValue: '',
  transKey: '',
  remark: ''
})

// === 表单验证规则 ===
const rules: Record<string, Rule[]> = {
  dictLabel: [
    { required: true, message: t('admin.dict.dictLabel.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('admin.dict.dictLabel.length'), trigger: 'blur' }
  ],
  dictValue: [
    { required: true, message: t('admin.dict.dictValue.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('admin.dict.dictValue.length'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, message: t('admin.dict.orderNum.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('admin.dict.status.required'), trigger: 'change' }
  ]
}

// === 方法定义 ===
// 获取字典数据详情
const getDictData = async (id: string) => {
  try {
    loading.value = true
    const res = await getByIdAsync(id)
    if (res.code === 200) {
      Object.assign(formData, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取字典数据详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    let res
    if (props.dictDataId) {
      // 更新
      const data: TaktDictDataUpdate = {
        ...formData,
        dictDataId: props.dictDataId
      }
      res = await updateDictData(data)
    } else {
      // 新增
      res = await createDictData(formData)
    }

    if (res.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
    } else {
      message.error(res.msg || t('common.message.saveFailed'))
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
  Object.assign(formData, {
    dictType: props.dictType,
    dictLabel: '',
    dictValue: '',
    orderNum: 0,
    cssClass: '',
    listClass: '',
    status: 0,
    extLabel: '',
    extValue: '',
    transKey: '',
    remark: ''
  })
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.open, (val) => {
  if (val) {
    if (props.dictDataId) {
      getDictData(props.dictDataId)
    } else {
      resetForm()
    }
  }
})

// === 生命周期 ===
onMounted(() => {
  if (props.open && props.dictDataId) {
    getDictData(props.dictDataId)
  }
})

// 监听transKey输入变化
watch([transKeyFirst, transKeySecond, transKeyThird], ([first, second, third]) => {
  formData.transKey = [first, second, third].filter(Boolean).join('_')
})
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 
