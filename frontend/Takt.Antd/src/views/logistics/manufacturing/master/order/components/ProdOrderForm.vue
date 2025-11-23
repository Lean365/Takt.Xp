//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ProdOrderForm.vue
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 生产工单表单组件
//===================================================================

<template>
  <Takt-modal
    :open="open"
    :title="title"
    width="800px"
    :footer="null"
    @cancel="handleCancel"
    destroy-on-close
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      layout="horizontal"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.plantCode.label')" name="plantCode">
            <a-input
              v-model:value="form.plantCode"
              :placeholder="t('logistics.manufacturing.master.order.fields.plantCode.placeholder')"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.prodOrderCode.label')" name="prodOrderCode">
            <a-input
              v-model:value="form.prodOrderCode"
              :placeholder="t('logistics.manufacturing.master.order.fields.prodOrderCode.placeholder')"
              :disabled="!!prodOrderId"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.prodOrderType.label')" name="prodOrderType">
            <a-input
              v-model:value="form.prodOrderType"
              :placeholder="t('logistics.manufacturing.master.order.fields.prodOrderType.placeholder')"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.materialCode.label')" name="materialCode">
            <a-input
              v-model:value="form.materialCode"
              :placeholder="t('logistics.manufacturing.master.order.fields.materialCode.placeholder')"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.prodOrderQty.label')" name="prodOrderQty">
            <a-input-number
              v-model:value="form.prodOrderQty"
              :min="0"
              :placeholder="t('logistics.manufacturing.master.order.fields.prodOrderQty.placeholder')"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.producedQty.label')" name="producedQty">
            <a-input-number
              v-model:value="form.producedQty"
              :min="0"
              :placeholder="t('logistics.manufacturing.master.order.fields.producedQty.placeholder')"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.unitOfMeasure.label')" name="unitOfMeasure">
            <a-input
              v-model:value="form.unitOfMeasure"
              :placeholder="t('logistics.manufacturing.master.order.fields.unitOfMeasure.placeholder')"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.Priority.label')" name="Priority">
            <a-input-number
              v-model:value="form.Priority"
              :min="1"
              :max="4"
              :placeholder="t('logistics.manufacturing.master.order.fields.Priority.placeholder')"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.workCenter.label')" name="workCenter">
            <a-input
              v-model:value="form.workCenter"
              :placeholder="t('logistics.manufacturing.master.order.fields.workCenter.placeholder')"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.serialNo.label')" name="serialNo">
            <a-input
              v-model:value="form.serialNo"
              :placeholder="t('logistics.manufacturing.master.order.fields.serialNo.placeholder')"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.prodBatch.label')" name="prodBatch">
            <a-input
              v-model:value="form.prodBatch"
              :placeholder="t('logistics.manufacturing.master.order.fields.prodBatch.placeholder')"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('logistics.manufacturing.master.order.fields.status.label')" name="status">
            <Takt-select
              v-model:value="form.status"
              dict-type="sys_normal_disable"
              :placeholder="t('logistics.manufacturing.master.order.fields.status.placeholder')"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item :label="t('logistics.manufacturing.master.order.fields.remarks.label')" name="remarks">
        <a-textarea
          v-model:value="form.remarks"
          :placeholder="t('logistics.manufacturing.master.order.fields.remarks.placeholder')"
          :rows="3"
        />
      </a-form-item>
    </a-form>

    <template #footer>
      <a-space>
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" :loading="loading" @click="handleSubmit">
          {{ t('common.button.confirm') }}
        </a-button>
      </a-space>
    </template>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, watch, nextTick, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktProdOrder, TaktProdOrderCreate, TaktProdOrderUpdate } from '@/types/logistics/manufacturing/master/prodOrder'
import { getProdOrderById, createProdOrder, updateProdOrder } from '@/api/logistics/manufacturing/master/prodOrder'

const { t } = useI18n()

// === Props 定义 ===
interface Props {
  open: boolean
  title: string
  prodOrderId?: number
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  title: '',
  prodOrderId: undefined
})

// === Emits 定义 ===
const emit = defineEmits<{
  'update:open': [value: boolean]
  'success': []
}>()

// === 状态定义 ===
const formRef = ref<FormInstance>()
const loading = ref(false)
const form = ref<TaktProdOrderCreate | TaktProdOrderUpdate>({
  plantCode: '',
  prodOrderCode: '',
  prodOrderType: '',
  materialCode: '',
  prodOrderQty: 0,
  producedQty: 0,
  unitOfMeasure: '',
  Priority: 2,
  serialNo: '',
  prodBatch: '',
  workCenter: '',
  status: 0,
  remarks: ''
})

// === 表单验证规则 ===
const rules: Record<string, any> = {
  plantCode: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.plantCode.validation.required'), trigger: 'blur' }
  ],
  prodOrderCode: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.prodOrderCode.validation.required'), trigger: 'blur' }
  ],
  prodOrderType: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.prodOrderType.validation.required'), trigger: 'blur' }
  ],
  materialCode: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.materialCode.validation.required'), trigger: 'blur' }
  ],
  prodOrderQty: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.prodOrderQty.validation.required'), trigger: 'blur' }
  ],
  unitOfMeasure: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.unitOfMeasure.validation.required'), trigger: 'blur' }
  ],
  Priority: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.Priority.validation.required'), trigger: 'blur' }
  ],
  workCenter: [
    { required: true, message: t('logistics.manufacturing.master.order.fields.workCenter.validation.required'), trigger: 'blur' }
  ]
}

// === 计算属性 ===
const isEdit = computed(() => !!props.prodOrderId)

// === 方法定义 ===
const resetForm = () => {
  form.value = {
    plantCode: '',
    prodOrderCode: '',
    prodOrderType: '',
    materialCode: '',
    prodOrderQty: 0,
    producedQty: 0,
    unitOfMeasure: '',
    Priority: 2,
    serialNo: '',
    prodBatch: '',
    workCenter: '',
    status: 0,
    remarks: ''
  }
  nextTick(() => {
    formRef.value?.clearValidate()
  })
}

const loadData = async () => {
  if (!props.prodOrderId) return
  
  try {
    const response = await getProdOrderById(props.prodOrderId)
    if (response.data?.data) {
      const data = response.data.data
      form.value = {
        prodOrderId: data.prodOrderId,
        plantCode: data.plantCode,
        prodOrderCode: data.prodOrderCode,
        prodOrderType: data.prodOrderType,
        materialCode: data.materialCode,
        prodOrderQty: data.prodOrderQty,
        producedQty: data.producedQty,
        unitOfMeasure: data.unitOfMeasure,
        Priority: data.Priority,
        serialNo: data.serialNo || '',
        prodBatch: data.prodBatch || '',
        workCenter: data.workCenter || '',
        status: data.status,
        remarks: data.remarks || ''
      }
    }
  } catch (error) {
    message.error(t('common.message.queryFailed'))
  }
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    if (isEdit.value) {
      await updateProdOrder(form.value as TaktProdOrderUpdate)
      message.success(t('common.message.updateSuccess'))
    } else {
      await createProdOrder(form.value as TaktProdOrderCreate)
      message.success(t('common.message.createSuccess'))
    }
    
    emit('success')
  } catch (error: any) {
    if (error?.errorFields) {
      message.error(t('common.message.validationFailed'))
    } else {
      message.error(isEdit.value ? t('common.message.updateFailed') : t('common.message.createFailed'))
    }
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  emit('update:open', false)
  resetForm()
}

// === 监听器 ===
watch(() => props.open, (newVal) => {
  if (newVal) {
    if (props.prodOrderId) {
      loadData()
    } else {
      resetForm()
    }
  }
})
</script>

<style lang="less" scoped>
:deep(.ant-form-item-label) {
  text-align: right;
}
</style>




