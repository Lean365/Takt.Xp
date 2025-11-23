<template>
  <a-modal
    v-model:open="visible"
    :title="title"
    width="800px"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirm-loading="loading"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      layout="vertical"
      :label-col="{ span: 24 }"
      :wrapper-col="{ span: 24 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="车牌号" name="plateNumber">
            <a-input
              v-model:value="formData.plateNumber"
              placeholder="请输入车牌号"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="车辆类型" name="vehicleType">
            <a-select
              v-model:value="formData.vehicleType"
              placeholder="请选择车辆类型"
              :disabled="isView"
            >
              <a-select-option :value="0">轿车</a-select-option>
              <a-select-option :value="1">SUV</a-select-option>
              <a-select-option :value="2">商务车</a-select-option>
              <a-select-option :value="3">面包车</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="车辆状态" name="status">
            <a-select
              v-model:value="formData.status"
              placeholder="请选择车辆状态"
              :disabled="isView"
            >
              <a-select-option :value="0">空闲</a-select-option>
              <a-select-option :value="1">使用中</a-select-option>
              <a-select-option :value="2">维修中</a-select-option>
              <a-select-option :value="3">已报废</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="品牌" name="brand">
            <a-input
              v-model:value="formData.brand"
              placeholder="请输入品牌"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="型号" name="model">
            <a-input
              v-model:value="formData.model"
              placeholder="请输入型号"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="颜色" name="color">
            <a-input
              v-model:value="formData.color"
              placeholder="请输入颜色"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="座位数" name="seatCount">
            <a-input-number
              v-model:value="formData.seatCount"
              placeholder="请输入座位数"
              style="width: 100%"
              :min="1"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="购买日期" name="purchaseDate">
            <a-date-picker
              v-model:value="formData.purchaseDate"
              placeholder="请选择购买日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="购买价格" name="purchasePrice">
            <a-input-number
              v-model:value="formData.purchasePrice"
              placeholder="请输入购买价格"
              style="width: 100%"
              :min="0"
              :precision="2"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="当前里程数" name="currentMileage">
            <a-input-number
              v-model:value="formData.currentMileage"
              placeholder="请输入当前里程数"
              style="width: 100%"
              :min="0"
              :precision="2"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="保险到期日期" name="insuranceExpiryDate">
            <a-date-picker
              v-model:value="formData.insuranceExpiryDate"
              placeholder="请选择保险到期日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="年检到期日期" name="inspectionExpiryDate">
            <a-date-picker
              v-model:value="formData.inspectionExpiryDate"
              placeholder="请选择年检到期日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          placeholder="请输入备注"
          :rows="3"
          :disabled="isView"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import {
  getVehicleById,
  createVehicle,
  updateVehicle
} from '@/api/routine/vehicle'
import type {
  TaktVehicle,
  TaktVehicleCreate,
  TaktVehicleUpdate
} from '@/types/routine/vehicle/vehicle'

// Props
interface Props {
  visible: boolean
  title: string
  vehicleId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  vehicleId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  success: []
}>()

// 响应式数据
const loading = ref(false)
const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<TaktVehicleCreate & { vehicleId?: number }>({
  plateNumber: '',
  vehicleType: 0,
  status: 0,
  brand: '',
  model: '',
  color: '',
  seatCount: 5,
  purchaseDate: undefined,
  purchasePrice: undefined,
  currentMileage: 0,
  insuranceExpiryDate: undefined,
  inspectionExpiryDate: undefined
})

// 表单验证规则
const rules = {
  plateNumber: [{ required: true, message: '请输入车牌号', type: 'string' }],
  vehicleType: [{ required: true, message: '请选择车辆类型', type: 'number' }],
  status: [{ required: true, message: '请选择车辆状态', type: 'number' }],
  seatCount: [{ required: true, message: '请输入座位数', type: 'number' }],
  currentMileage: [{ required: true, message: '请输入当前里程数', type: 'number' }]
}

// 计算属性
const isView = computed(() => props.title.includes('查看'))

// 监听visible变化
watch(
  () => props.visible,
  (visible) => {
    if (visible && props.vehicleId) {
      getDetail()
    } else if (visible && !props.vehicleId) {
      resetForm()
    }
  }
)

// 获取详情
const getDetail = async () => {
  if (!props.vehicleId) return
  
  loading.value = true
  try {
    const res = await getVehicleById(props.vehicleId)
    if (res.data?.data) {
      const data = res.data.data
      Object.assign(formData, {
        vehicleId: data.vehicleId,
        plateNumber: data.plateNumber,
        vehicleType: data.vehicleType,
        status: data.status,
        brand: data.brand,
        model: data.model,
        color: data.color,
        seatCount: data.seatCount,
        purchaseDate: data.purchaseDate ? new Date(data.purchaseDate) : undefined,
        purchasePrice: data.purchasePrice,
        currentMileage: data.currentMileage,
        insuranceExpiryDate: data.insuranceExpiryDate ? new Date(data.insuranceExpiryDate) : undefined,
        inspectionExpiryDate: data.inspectionExpiryDate ? new Date(data.inspectionExpiryDate) : undefined
      })
    }
  } catch (error) {
    message.error('获取详情失败')
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(formData, {
    vehicleId: undefined,
    plateNumber: '',
    vehicleType: 0,
    status: 0,
    brand: '',
    model: '',
    color: '',
    seatCount: 5,
    purchaseDate: undefined,
    purchasePrice: undefined,
    currentMileage: 0,
    insuranceExpiryDate: undefined,
    inspectionExpiryDate: undefined
  })
  formRef.value?.clearValidate()
}

// 提交表单
const handleSubmit = async () => {
  if (isView.value) {
    handleCancel()
    return
  }

  try {
    await formRef.value?.validate()
    loading.value = true

    if (props.vehicleId) {
      // 更新
      const updateData: TaktVehicleUpdate = {
        vehicleId: props.vehicleId,
        plateNumber: formData.plateNumber,
        vehicleType: formData.vehicleType,
        status: formData.status,
        brand: formData.brand,
        model: formData.model,
        color: formData.color,
        seatCount: formData.seatCount,
        purchaseDate: formData.purchaseDate,
        purchasePrice: formData.purchasePrice,
        currentMileage: formData.currentMileage,
        insuranceExpiryDate: formData.insuranceExpiryDate,
        inspectionExpiryDate: formData.inspectionExpiryDate
      }
      await updateVehicle(updateData)
      message.success('更新成功')
    } else {
      // 新增
      const createData: TaktVehicleCreate = {
        plateNumber: formData.plateNumber,
        vehicleType: formData.vehicleType,
        status: formData.status,
        brand: formData.brand,
        model: formData.model,
        color: formData.color,
        seatCount: formData.seatCount,
        purchaseDate: formData.purchaseDate,
        purchasePrice: formData.purchasePrice,
        currentMileage: formData.currentMileage,
        insuranceExpiryDate: formData.insuranceExpiryDate,
        inspectionExpiryDate: formData.inspectionExpiryDate
      }
      await createVehicle(createData)
      message.success('创建成功')
    }

    emit('success')
  } catch (error) {
    console.error('表单提交失败:', error)
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}
</script>

<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 16px;
}
</style> 
