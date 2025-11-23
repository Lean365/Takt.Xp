<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="1000"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.plantCode.label')" name="plantCode">
            <a-input
              v-model:value="form.plantCode"
              :placeholder="t('assyMpOutput.fields.plantCode.placeholder')"
              show-count
              :maxlength="50"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.prodDate.label')" name="prodDate">
            <a-date-picker
              v-model:value="form.prodDate"
              :placeholder="t('assyMpOutput.fields.prodDate.placeholder')"
              style="width: 100%"
              format="YYYY-MM-DD"
              value-format="YYYY-MM-DD"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.prodLine.label')" name="prodLine">
            <a-input
              v-model:value="form.prodLine"
              :placeholder="t('assyMpOutput.fields.prodLine.placeholder')"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.shiftNo.label')" name="shiftNo">
            <Takt-select
              v-model:value="form.shiftNo"
              dict-type="shift_type"
              :placeholder="t('assyMpOutput.fields.shiftNo.placeholder')"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.directLabor.label')" name="directLabor">
            <a-input-number
              v-model:value="form.directLabor"
              :placeholder="t('assyMpOutput.fields.directLabor.placeholder')"
              style="width: 100%"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.indirectLabor.label')" name="indirectLabor">
            <a-input-number
              v-model:value="form.indirectLabor"
              :placeholder="t('assyMpOutput.fields.indirectLabor.placeholder')"
              style="width: 100%"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.prodOrderCode.label')" name="prodOrderCode">
            <a-input
              v-model:value="form.prodOrderCode"
              :placeholder="t('assyMpOutput.fields.prodOrderCode.placeholder')"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.modelCode.label')" name="modelCode">
            <a-input
              v-model:value="form.modelCode"
              :placeholder="t('assyMpOutput.fields.modelCode.placeholder')"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.materialCode.label')" name="materialCode">
            <a-input
              v-model:value="form.materialCode"
              :placeholder="t('assyMpOutput.fields.materialCode.placeholder')"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.batchNo.label')" name="batchNo">
            <a-input
              v-model:value="form.batchNo"
              :placeholder="t('assyMpOutput.fields.batchNo.placeholder')"
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.orderQty.label')" name="orderQty">
            <a-input-number
              v-model:value="form.orderQty"
              :placeholder="t('assyMpOutput.fields.orderQty.placeholder')"
              style="width: 100%"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.stdMinutes.label')" name="stdMinutes">
            <a-input-number
              v-model:value="form.stdMinutes"
              :placeholder="t('assyMpOutput.fields.stdMinutes.placeholder')"
              style="width: 100%"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('assyMpOutput.fields.stdCapacity.label')" name="stdCapacity">
            <a-input-number
              v-model:value="form.stdCapacity"
              :placeholder="t('assyMpOutput.fields.stdCapacity.placeholder')"
              style="width: 100%"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item :label="t('assyMpOutput.fields.remarks.label')" name="remarks">
            <a-textarea
              v-model:value="form.remarks"
              :placeholder="t('assyMpOutput.fields.remarks.placeholder')"
              :rows="3"
              show-count
              :maxlength="500"
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { createAssyMpOutput, updateAssyMpOutput, getAssyMpOutputById } from '@/api/logistics/manufacturing/outputs/assy/assyMpOutput'
import type { TaktAssyMpOutput, TaktAssyMpOutputCreate, TaktAssyMpOutputUpdate } from '@/types/logistics/manufacturing/outputs/assy/assyMpOutput'

const { t } = useI18n()

interface Props {
  visible: boolean
  title: string
  assyMpOutputId?: number | bigint
}

interface Emits {
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const formRef = ref<FormInstance>()
const loading = ref(false)

const form = reactive<TaktAssyMpOutputCreate>({
  plantCode: '',
  prodDate: '',
  prodLine: '',
  directLabor: 0,
  indirectLabor: 0,
  shiftNo: 1,
  prodOrderCode: '',
  modelCode: '',
  materialCode: '',
  batchNo: '',
  orderQty: 0,
  stdMinutes: 0,
  stdCapacity: 0,
  remarks: ''
})

const rules = {
  plantCode: [
    { required: true, message: t('assyMpOutput.fields.plantCode.required'), trigger: 'blur' }
  ],
  prodDate: [
    { required: true, message: t('assyMpOutput.fields.prodDate.required'), trigger: 'change' }
  ],
  prodLine: [
    { required: true, message: t('assyMpOutput.fields.prodLine.required'), trigger: 'blur' }
  ],
  shiftNo: [
    { required: true, message: t('assyMpOutput.fields.shiftNo.required'), trigger: 'change' }
  ],
  directLabor: [
    { required: true, message: t('assyMpOutput.fields.directLabor.required'), trigger: 'blur' }
  ],
  indirectLabor: [
    { required: true, message: t('assyMpOutput.fields.indirectLabor.required'), trigger: 'blur' }
  ],
  prodOrderCode: [
    { required: true, message: t('assyMpOutput.fields.prodOrderCode.required'), trigger: 'blur' }
  ],
  modelCode: [
    { required: true, message: t('assyMpOutput.fields.modelCode.required'), trigger: 'blur' }
  ],
  materialCode: [
    { required: true, message: t('assyMpOutput.fields.materialCode.required'), trigger: 'blur' }
  ],
  orderQty: [
    { required: true, message: t('assyMpOutput.fields.orderQty.required'), trigger: 'blur' }
  ],
  stdMinutes: [
    { required: true, message: t('assyMpOutput.fields.stdMinutes.required'), trigger: 'blur' }
  ],
  stdCapacity: [
    { required: true, message: t('assyMpOutput.fields.stdCapacity.required'), trigger: 'blur' }
  ]
}

const resetForm = () => {
  Object.assign(form, {
    plantCode: '',
    prodDate: '',
    prodLine: '',
    directLabor: 0,
    indirectLabor: 0,
    shiftNo: 1,
    prodOrderCode: '',
    modelCode: '',
    materialCode: '',
    batchNo: '',
    orderQty: 0,
    stdMinutes: 0,
    stdCapacity: 0,
    remarks: ''
  })
  formRef.value?.clearValidate()
}

const loadData = async () => {
  if (!props.assyMpOutputId) {
    resetForm()
    return
  }

  loading.value = true
  try {
    const response = await getAssyMpOutputById(props.assyMpOutputId)
    if (response.data.code === 200) {
      const data = response.data.data
      Object.assign(form, {
        plantCode: data.plantCode,
        prodDate: data.prodDate,
        prodLine: data.prodLine,
        directLabor: data.directLabor,
        indirectLabor: data.indirectLabor,
        shiftNo: data.shiftNo,
        prodOrderCode: data.prodOrderCode,
        modelCode: data.modelCode,
        materialCode: data.materialCode,
        batchNo: data.batchNo,
        orderQty: data.orderQty,
        stdMinutes: data.stdMinutes,
        stdCapacity: data.stdCapacity,
        remarks: data.remarks
      })
    }
  } catch (error) {
    console.error('加载数据失败:', error)
    message.error(t('common.message.loadFailed'))
  } finally {
    loading.value = false
  }
}

const handleVisibleChange = (visible: boolean) => {
  emit('update:visible', visible)
  if (visible) {
    nextTick(() => {
      loadData()
    })
  }
}

const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    if (props.assyMpOutputId) {
      // 更新
      const updateData: TaktAssyMpOutputUpdate = {
        ...form,
        assyMpOutputId: props.assyMpOutputId
      }
      const response = await updateAssyMpOutput(updateData)
      if (response.data.code === 200) {
        message.success(t('common.message.updateSuccess'))
        emit('success')
        emit('update:visible', false)
      }
    } else {
      // 创建
      const response = await createAssyMpOutput(form)
      if (response.data.code === 200) {
        message.success(t('common.message.createSuccess'))
        emit('success')
        emit('update:visible', false)
      }
    }
  } catch (error) {
    console.error('提交失败:', error)
    message.error(t('common.message.submitFailed'))
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  emit('update:visible', false)
}

watch(() => props.visible, (visible) => {
  if (visible) {
    nextTick(() => {
      loadData()
    })
  }
})
</script>

<style scoped>
.assy-mp-output-form {
  padding: 16px;
}
</style>

