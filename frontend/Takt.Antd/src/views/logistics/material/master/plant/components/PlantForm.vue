<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
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
      <a-tabs>
        <!-- 基本信息 -->
        <a-tab-pane :key="'basicInfo'" :tab="t('logistics.material.master.plant.tabs.basicInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.plantCode.label')" name="plantCode">
                <a-input
                  v-model:value="form.plantCode"
                  :placeholder="t('logistics.material.master.plant.fields.plantCode.placeholder')"
                  :disabled="!!props.plantId"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.plantName.label')" name="plantName">
                <a-input
                  v-model:value="form.plantName"
                  :placeholder="t('logistics.material.master.plant.fields.plantName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.plantShortName.label')" name="plantShortName">
                <a-input
                  v-model:value="form.plantShortName"
                  :placeholder="t('logistics.material.master.plant.fields.plantShortName.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.plantType.label')" name="plantType">
                <Takt-select
                  v-model:value="form.plantType"
                  dict-type="logistics_plant_type"
                  :placeholder="t('logistics.material.master.plant.fields.plantType.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.plantTypeName.label')" name="plantTypeName">
                <a-input
                  v-model:value="form.plantTypeName"
                  :placeholder="t('logistics.material.master.plant.fields.plantTypeName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.status.label')" name="status">
                <Takt-select
                  v-model:value="form.status"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.status.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.plantAddress.label')" name="plantAddress">
                <a-input
                  v-model:value="form.plantAddress"
                  :placeholder="t('logistics.material.master.plant.fields.plantAddress.placeholder')"
                  show-count
                  :maxlength="200"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.contactPerson.label')" name="contactPerson">
                <a-input
                  v-model:value="form.contactPerson"
                  :placeholder="t('logistics.material.master.plant.fields.contactPerson.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.phone.label')" name="phone">
                <a-input
                  v-model:value="form.phone"
                  :placeholder="t('logistics.material.master.plant.fields.phone.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.email.label')" name="email">
                <a-input
                  v-model:value="form.email"
                  :placeholder="t('logistics.material.master.plant.fields.email.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('logistics.material.master.plant.fields.remark.label')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('logistics.material.master.plant.fields.remark.placeholder')"
                  :rows="4"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 管理配置 -->
        <a-tab-pane :key="'management'" :tab="t('logistics.material.master.plant.tabs.management')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isBatchManaged.label')" name="isBatchManaged">
                <Takt-select
                  v-model:value="form.isBatchManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isBatchManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isSerialManaged.label')" name="isSerialManaged">
                <Takt-select
                  v-model:value="form.isSerialManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isSerialManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isQualityManaged.label')" name="isQualityManaged">
                <Takt-select
                  v-model:value="form.isQualityManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isQualityManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isCostManaged.label')" name="isCostManaged">
                <Takt-select
                  v-model:value="form.isCostManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isCostManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isInventoryManaged.label')" name="isInventoryManaged">
                <Takt-select
                  v-model:value="form.isInventoryManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isInventoryManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isProductionManaged.label')" name="isProductionManaged">
                <Takt-select
                  v-model:value="form.isProductionManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isProductionManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isPurchaseManaged.label')" name="isPurchaseManaged">
                <Takt-select
                  v-model:value="form.isPurchaseManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isPurchaseManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('logistics.material.master.plant.fields.isSalesManaged.label')" name="isSalesManaged">
                <Takt-select
                  v-model:value="form.isSalesManaged"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('logistics.material.master.plant.fields.isSalesManaged.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>
      </a-tabs>
    </a-form>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktPlant, TaktPlantCreate, TaktPlantUpdate } from '@/types/logistics/material/master/plant'
import { getByIdAsync, createPlant, updatePlant } from '@/api/logistics/material/master/plant'

interface Props {
  visible: boolean
  title: string
  plantId?: string
}

interface Emits {
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()
const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const form = ref<any>({
  plantCode: '',
  plantName: '',
  plantShortName: '',
  plantType: 1,
  plantTypeName: '',
  status: 0,
  plantAddress: '',
  contactPerson: '',
  phone: '',
  email: '',
  remark: '',
  isBatchManaged: 0,
  isSerialManaged: 0,
  isQualityManaged: 0,
  isCostManaged: 0,
  isInventoryManaged: 0,
  isProductionManaged: 0,
  isPurchaseManaged: 0,
  isSalesManaged: 0
})

// 表单验证规则
const rules = computed(() => ({
  plantCode: [
    { required: true, message: t('logistics.material.master.plant.fields.plantCode.required'), trigger: 'blur' },
    { min: 1, max: 50, message: t('logistics.material.master.plant.fields.plantCode.length'), trigger: 'blur' }
  ],
  plantName: [
    { required: true, message: t('logistics.material.master.plant.fields.plantName.required'), trigger: 'blur' },
    { min: 1, max: 100, message: t('logistics.material.master.plant.fields.plantName.length'), trigger: 'blur' }
  ],
  plantType: [
    { required: true, message: t('logistics.material.master.plant.fields.plantType.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('logistics.material.master.plant.fields.status.required'), trigger: 'change' }
  ],
  email: [
    { type: 'email', message: t('logistics.material.master.plant.fields.email.format'), trigger: 'blur' }
  ],
  phone: [
    { pattern: /^1[3-9]\d{9}$/, message: t('logistics.material.master.plant.fields.phone.format'), trigger: 'blur' }
  ]
} as any))

// 监听visible变化，加载数据
watch(() => props.visible, async (newVal) => {
  if (newVal && props.plantId) {
    await loadData()
  } else if (newVal && !props.plantId) {
    resetForm()
  }
})

// 加载数据
const loadData = async () => {
  if (!props.plantId) return
  
  try {
    loading.value = true
    const response = await getByIdAsync(props.plantId)
    if (response.data) {
      form.value = { ...response.data }
    }
  } catch (error) {
    console.error('加载工厂数据失败:', error)
    message.error('加载工厂数据失败')
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  form.value = {
    plantCode: '',
    plantName: '',
    plantShortName: '',
    plantType: 1,
    plantTypeName: '',
    status: 0,
    plantAddress: '',
    contactPerson: '',
    phone: '',
    email: '',
    remark: '',
    isBatchManaged: 0,
    isSerialManaged: 0,
    isQualityManaged: 0,
    isCostManaged: 0,
    isInventoryManaged: 0,
    isProductionManaged: 0,
    isPurchaseManaged: 0,
    isSalesManaged: 0
  }
  nextTick(() => {
    formRef.value?.clearValidate()
  })
}

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  emit('update:visible', value)
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 处理提交
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    if (props.plantId) {
      // 更新
      await updatePlant(form.value as TaktPlantUpdate)
      message.success('更新工厂成功')
    } else {
      // 创建
      await createPlant(form.value as TaktPlantCreate)
      message.success('创建工厂成功')
    }

    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error('提交失败:', error)
    if (error instanceof Error) {
      message.error(error.message)
    } else {
      message.error('操作失败')
    }
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.ant-form-item {
  margin-bottom: 16px;
}
</style>
