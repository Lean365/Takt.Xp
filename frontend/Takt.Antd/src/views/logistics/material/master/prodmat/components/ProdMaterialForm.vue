<template>
  <a-modal
    :open="visible"
    :title="title"
    :width="1000"
    :confirm-loading="submitLoading"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :mask-closable="false"
    :destroy-on-close="true"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="formRules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
      layout="horizontal"
    >
      <a-tabs v-model:activeKey="activeTab" type="card">
        <!-- 基本信息 -->
        <a-tab-pane key="basic" tab="基本信息">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="工厂编码"
                name="plantCode"
              >
                <a-select
                  v-model:value="formData.plantCode"
                  placeholder="请选择工厂"
                  show-search
                  :filter-option="filterPlantOption"
                >
                  <a-select-option
                    v-for="plant in plantOptions"
                    :key="plant.dictValue"
                    :value="plant.dictValue"
                  >
                    {{ plant.dictLabel }}
                  </a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="物料编码"
                name="materialCode"
              >
                <a-input
                  v-model:value="formData.materialCode"
                  placeholder="请输入物料编码"
                  :disabled="!!prodMaterialId"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="物料描述"
                name="materialDescription"
              >
                <a-input
                  v-model:value="formData.materialDescription"
                  placeholder="请输入物料描述"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="物料类型"
                name="materialType"
              >
                <a-input
                  v-model:value="formData.materialType"
                  placeholder="请输入物料类型"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="物料组"
                name="materialGroup"
              >
                <a-input
                  v-model:value="formData.materialGroup"
                  placeholder="请输入物料组"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="基本计量单位"
                name="baseUnitOfMeasure"
              >
                <a-input
                  v-model:value="formData.baseUnitOfMeasure"
                  placeholder="请输入基本计量单位"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="产品层次"
                name="productHierarchy"
              >
                <a-input
                  v-model:value="formData.productHierarchy"
                  placeholder="请输入产品层次"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="状态"
                name="status"
              >
                <a-select
                  v-model:value="formData.status"
                  placeholder="请选择状态"
                >
                  <a-select-option :value="1">正常</a-select-option>
                  <a-select-option :value="0">停用</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <!-- 价格信息 -->
      <a-tab-pane key="price" tab="价格信息">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="移动平均价"
                name="movingAveragePrice"
              >
                <a-input-number
                  v-model:value="formData.movingAveragePrice"
                  placeholder="请输入移动平均价"
                  :min="0"
                  :precision="2"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="价格单位"
                name="priceUnit"
              >
                <a-input
                  v-model:value="formData.priceUnit"
                  placeholder="请输入价格单位"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="评估类"
                name="valuationClass"
              >
                <a-input
                  v-model:value="formData.valuationClass"
                  placeholder="请输入评估类"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="利润中心"
                name="profitCenter"
              >
                <a-input
                  v-model:value="formData.profitCenter"
                  placeholder="请输入利润中心"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="货币"
                name="currency"
              >
                <a-input
                  v-model:value="formData.currency"
                  placeholder="请输入货币"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="价格控制"
                name="priceControl"
              >
                <a-input
                  v-model:value="formData.priceControl"
                  placeholder="请输入价格控制"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <!-- 采购信息 -->
      <a-tab-pane key="procurement" tab="采购信息">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="最小起订量"
                name="minimumOrderQuantity"
              >
                <a-input-number
                  v-model:value="formData.minimumOrderQuantity"
                  placeholder="请输入最小起订量"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="舍入值"
                name="roundingValue"
              >
                <a-input-number
                  v-model:value="formData.roundingValue"
                  placeholder="请输入舍入值"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="计划交货时间"
                name="plannedDeliveryTime"
              >
                <a-input-number
                  v-model:value="formData.plannedDeliveryTime"
                  placeholder="请输入计划交货时间"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="采购组"
                name="purchaseGroup"
              >
                <a-input
                  v-model:value="formData.purchaseGroup"
                  placeholder="请输入采购组"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="特殊采购类型"
                name="specialProcurementType"
              >
                <a-input
                  v-model:value="formData.specialProcurementType"
                  placeholder="请输入特殊采购类型"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="外部采购仓储地点"
                name="externalProcurementStorageLocation"
              >
                <a-input
                  v-model:value="formData.externalProcurementStorageLocation"
                  placeholder="请输入外部采购仓储地点"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <!-- 仓储信息 -->
      <a-tab-pane key="storage" tab="仓储信息">
        <a-form
          ref="formRef"
          :model="formData"
          :rules="formRules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
        >
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="仓位"
                name="storageBin"
              >
                <a-input
                  v-model:value="formData.storageBin"
                  placeholder="请输入仓位"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="生产仓储地点"
                name="productionStorageLocation"
              >
                <a-input
                  v-model:value="formData.productionStorageLocation"
                  placeholder="请输入生产仓储地点"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="库存数量"
                name="inventoryQuantity"
              >
                <a-input-number
                  v-model:value="formData.inventoryQuantity"
                  placeholder="请输入库存数量"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="跨工厂物料状态"
                name="crossPlantMaterialStatus"
              >
                <a-select
                  v-model:value="formData.crossPlantMaterialStatus"
                  placeholder="请选择跨工厂物料状态"
                >
                  <a-select-option :value="1">正常</a-select-option>
                  <a-select-option :value="0">停用</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <!-- 分类信息 -->
      <a-tab-pane key="classification" tab="分类信息">
        <a-form
          ref="formRef"
          :model="formData"
          :rules="formRules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
        >
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="行业领域"
                name="industrySector"
              >
                <a-input
                  v-model:value="formData.industrySector"
                  placeholder="请输入行业领域"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="差异代码"
                name="varianceCode"
              >
                <a-input
                  v-model:value="formData.varianceCode"
                  placeholder="请输入差异代码"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <!-- 制造商信息 -->
      <a-tab-pane key="manufacturer" tab="制造商信息">
        <a-form
          ref="formRef"
          :model="formData"
          :rules="formRules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
        >
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item
                label="制造商"
                name="manufacturer"
              >
                <a-input
                  v-model:value="formData.manufacturer"
                  placeholder="请输入制造商"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item
                label="制造商零件号"
                name="manufacturerPartNumber"
              >
                <a-input
                  v-model:value="formData.manufacturerPartNumber"
                  placeholder="请输入制造商零件号"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>

      <!-- 其他信息 -->
      <a-tab-pane key="other" tab="其他信息">
        <a-form
          ref="formRef"
          :model="formData"
          :rules="formRules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 18 }"
          layout="horizontal"
        >
          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item
                label="备注"
                name="remark"
              >
                <a-textarea
                  v-model:value="formData.remark"
                  placeholder="请输入备注"
                  :rows="6"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-form>
      </a-tab-pane>
    </a-tabs>
  </a-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, watch, nextTick, onMounted } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktProdMaterial, TaktProdMaterialCreate, TaktProdMaterialUpdate } from '@/types/logistics/material/master/prodMaterial'
import type { TaktSelectOption } from '@/types/common'
import { getProdMaterialById, createProdMaterial, updateProdMaterial } from '@/api/logistics/material/master/prodMaterial'
import { getPlantOptions } from '@/api/logistics/material/master/plant'

const { t } = useI18n()

// Props
interface Props {
  visible: boolean
  title: string
  prodMaterialId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  prodMaterialId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  success: []
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 当前激活的tab
const activeTab = ref('basic')

// 表单数据
const formData = ref<TaktProdMaterialCreate>({
  plantCode: '',
  materialCode: '',
  materialDescription: '',
  movingAveragePrice: 0,
  priceUnit: '',
  valuationClass: '',
  profitCenter: '',
  minimumOrderQuantity: 0,
  roundingValue: 0,
  plannedDeliveryTime: 0,
  storageBin: '',
  purchaseGroup: '',
  externalProcurementStorageLocation: '',
  industrySector: '',
  baseUnitOfMeasure: '',
  materialType: '',
  productHierarchy: '',
  materialGroup: '',
  specialProcurementType: '',
  varianceCode: '',
  manufacturerPartNumber: '',
  manufacturer: '',
  currency: '',
  priceControl: '',
  productionStorageLocation: '',
  crossPlantMaterialStatus: 1,
  inventoryQuantity: 0,
  status: 1,
  remark: ''
})

// 表单验证规则
const formRules: Record<string, any> = {
  plantCode: [
    { required: true, message: '请选择工厂', trigger: 'change' }
  ],
  materialCode: [
    { required: true, message: '请输入物料编码', trigger: 'blur' }
  ],
  materialDescription: [
    { required: true, message: '请输入物料描述', trigger: 'blur' }
  ],
  movingAveragePrice: [
    { required: true, message: '请输入移动平均价', trigger: 'blur' }
  ],
  priceUnit: [
    { required: true, message: '请输入价格单位', trigger: 'blur' }
  ],
  valuationClass: [
    { required: true, message: '请输入评估类', trigger: 'blur' }
  ],
  profitCenter: [
    { required: true, message: '请输入利润中心', trigger: 'blur' }
  ],
  minimumOrderQuantity: [
    { required: true, message: '请输入最小起订量', trigger: 'blur' }
  ],
  roundingValue: [
    { required: true, message: '请输入舍入值', trigger: 'blur' }
  ],
  plannedDeliveryTime: [
    { required: true, message: '请输入计划交货时间', trigger: 'blur' }
  ],
  baseUnitOfMeasure: [
    { required: true, message: '请输入基本计量单位', trigger: 'blur' }
  ],
  materialType: [
    { required: true, message: '请输入物料类型', trigger: 'blur' }
  ],
  materialGroup: [
    { required: true, message: '请输入物料组', trigger: 'blur' }
  ]
}

// 提交加载状态
const submitLoading = ref(false)

// 是否为编辑模式
const isEdit = computed(() => !!props.prodMaterialId)

// 工厂选项
const plantOptions = ref<TaktSelectOption[]>([])

// 监听visible变化，重置表单
watch(
  () => props.visible,
  (visible) => {
    if (visible) {
      if (isEdit.value && props.prodMaterialId) {
        loadProdMaterialData(props.prodMaterialId)
      } else {
        resetForm()
      }
      // 重置到第一个tab
      activeTab.value = 'basic'
    }
  }
)

// 加载生产物料数据
const loadProdMaterialData = async (id: number) => {
  try {
    const res = await getProdMaterialById(id)
    const prodMaterial = res.data
    formData.value = {
      plantCode: prodMaterial.plantCode,
      materialCode: prodMaterial.materialCode,
      materialDescription: prodMaterial.materialDescription || '',
      movingAveragePrice: prodMaterial.movingAveragePrice,
      priceUnit: prodMaterial.priceUnit || '',
      valuationClass: prodMaterial.valuationClass || '',
      profitCenter: prodMaterial.profitCenter || '',
      minimumOrderQuantity: prodMaterial.minimumOrderQuantity,
      roundingValue: prodMaterial.roundingValue,
      plannedDeliveryTime: prodMaterial.plannedDeliveryTime,
      storageBin: prodMaterial.storageBin || '',
      purchaseGroup: prodMaterial.purchaseGroup || '',
      externalProcurementStorageLocation: prodMaterial.externalProcurementStorageLocation || '',
      industrySector: prodMaterial.industrySector || '',
      baseUnitOfMeasure: prodMaterial.baseUnitOfMeasure || '',
      materialType: prodMaterial.materialType || '',
      productHierarchy: prodMaterial.productHierarchy || '',
      materialGroup: prodMaterial.materialGroup || '',
      specialProcurementType: prodMaterial.specialProcurementType || '',
      varianceCode: prodMaterial.varianceCode || '',
      manufacturerPartNumber: prodMaterial.manufacturerPartNumber || '',
      manufacturer: prodMaterial.manufacturer || '',
      currency: prodMaterial.currency || '',
      priceControl: prodMaterial.priceControl || '',
      productionStorageLocation: prodMaterial.productionStorageLocation || '',
      crossPlantMaterialStatus: prodMaterial.crossPlantMaterialStatus,
      inventoryQuantity: prodMaterial.inventoryQuantity,
      status: prodMaterial.status,
      remark: prodMaterial.remark || ''
    }
  } catch (error) {
    console.error(error)
    message.error('加载失败')
  }
}

// 重置表单
const resetForm = () => {
  formData.value = {
    plantCode: '',
    materialCode: '',
    materialDescription: '',
    movingAveragePrice: 0,
    priceUnit: '',
    valuationClass: '',
    profitCenter: '',
    minimumOrderQuantity: 0,
    roundingValue: 0,
    plannedDeliveryTime: 0,
    storageBin: '',
    purchaseGroup: '',
    externalProcurementStorageLocation: '',
    industrySector: '',
    baseUnitOfMeasure: '',
    materialType: '',
    productHierarchy: '',
    materialGroup: '',
    specialProcurementType: '',
    varianceCode: '',
    manufacturerPartNumber: '',
    manufacturer: '',
    currency: '',
    priceControl: '',
    productionStorageLocation: '',
    crossPlantMaterialStatus: 1,
    inventoryQuantity: 0,
    status: 1,
    remark: ''
  }
  formRef.value?.clearValidate()
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    submitLoading.value = true

    if (isEdit.value && props.prodMaterialId) {
      // 更新
      const updateData: TaktProdMaterialUpdate = {
        materialId: props.prodMaterialId,
        ...formData.value
      }
      await updateProdMaterial(updateData)
      message.success('更新成功')
      emit('success')
    } else {
      // 新增
      await createProdMaterial(formData.value)
      message.success('创建成功')
      emit('success')
    }
  } catch (error) {
    console.error(error)
    message.error('操作失败')
  } finally {
    submitLoading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 过滤工厂选项
const filterPlantOption = (input: string, option: any) => {
  return option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
}

// 加载工厂选项
const loadPlantOptions = async () => {
  try {
    const res = await getPlantOptions()
    plantOptions.value = res.data
  } catch (error) {
    console.error('加载工厂选项失败:', error)
  }
}

onMounted(() => {
  loadPlantOptions()
})
</script>

<style lang="less" scoped>
:deep(.ant-form-item-label) {
  text-align: right;
}

:deep(.ant-tabs-content-holder) {
  padding: 16px 0;
}

:deep(.ant-tabs-tab) {
  margin-right: 8px;
}
</style>

