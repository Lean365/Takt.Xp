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
      <a-tabs>
        <!-- 基本信息 -->
        <a-tab-pane :key="'basicInfo'" :tab="t('accounting.financial.company.tabs.basicInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyCode.label')" name="companyCode">
                <a-input
                  v-model:value="form.companyCode"
                  :placeholder="t('accounting.financial.company.fields.companyCode.placeholder')"
                  :disabled="!!props.companyId"
                  show-count
                  :maxlength="8"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyName.label')" name="companyName">
                <a-input
                  v-model:value="form.companyName"
                  :placeholder="t('accounting.financial.company.fields.companyName.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyShortName.label')" name="companyShortName">
                <a-input
                  v-model:value="form.companyShortName"
                  :placeholder="t('accounting.financial.company.fields.companyShortName.placeholder')"
                  show-count
                  :maxlength="25"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyTaxNumber.label')" name="companyTaxNumber">
                <a-input
                  v-model:value="form.companyTaxNumber"
                  :placeholder="t('accounting.financial.company.fields.companyTaxNumber.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyNature.label')" name="companyNature">
                <Takt-select
                  v-model:value="form.companyNature"
                  dict-type="sys_enterprise_nature"
                  :placeholder="t('accounting.financial.company.fields.companyNature.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyIndustryType.label')" name="companyIndustryType">
                <Takt-select
                  v-model:value="form.companyIndustryType"
                  dict-type="sys_industry_type"
                  :placeholder="t('accounting.financial.company.fields.companyIndustryType.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyCurrency.label')" name="companyCurrency">
                <Takt-select
                  v-model:value="form.companyCurrency"
                  dict-type="sys_currency"
                  :placeholder="t('accounting.financial.company.fields.companyCurrency.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyLanguage.label')" name="companyLanguage">
                <Takt-select
                  v-model:value="form.companyLanguage"
                  dict-type="accounting_company_language"
                  :placeholder="t('accounting.financial.company.fields.companyLanguage.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyPhone.label')" name="companyPhone">
                <a-input
                  v-model:value="form.companyPhone"
                  :placeholder="t('accounting.financial.company.fields.companyPhone.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyEmail.label')" name="companyEmail">
                <a-input
                  v-model:value="form.companyEmail"
                  :placeholder="t('accounting.financial.company.fields.companyEmail.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyWebsite.label')" name="companyWebsite">
                <a-input
                  v-model:value="form.companyWebsite"
                  :placeholder="t('accounting.financial.company.fields.companyWebsite.placeholder')"
                  show-count
                  :maxlength="200"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyScale.label')" name="companyScale">
                <a-input
                  v-model:value="form.companyScale"
                  :placeholder="t('accounting.financial.company.fields.companyScale.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('accounting.financial.company.fields.companyAddress.label')" name="companyAddress">
                <a-textarea
                  v-model:value="form.companyAddress"
                  :placeholder="t('accounting.financial.company.fields.companyAddress.placeholder')"
                  :rows="3"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('accounting.financial.company.fields.remark.label')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('accounting.financial.company.fields.remark.placeholder')"
                  :rows="4"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 详细信息 -->
        <a-tab-pane :key="'detailInfo'" :tab="t('accounting.financial.company.tabs.detailInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyName1.label')" name="companyName1">
                <a-input
                  v-model:value="form.companyName1"
                  :placeholder="t('accounting.financial.company.fields.companyName1.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyName2.label')" name="companyName2">
                <a-input
                  v-model:value="form.companyName2"
                  :placeholder="t('accounting.financial.company.fields.companyName2.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyName3.label')" name="companyName3">
                <a-input
                  v-model:value="form.companyName3"
                  :placeholder="t('accounting.financial.company.fields.companyName3.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyLegalRepresentative.label')" name="companyLegalRepresentative">
                <a-input
                  v-model:value="form.companyLegalRepresentative"
                  :placeholder="t('accounting.financial.company.fields.companyLegalRepresentative.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyRegisteredCapital.label')" name="companyRegisteredCapital">
                <a-input-number
                  v-model:value="form.companyRegisteredCapital"
                  :placeholder="t('accounting.financial.company.fields.companyRegisteredCapital.placeholder')"
                  :min="0"
                  :precision="2"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyBusinessLicense.label')" name="companyBusinessLicense">
                <a-input
                  v-model:value="form.companyBusinessLicense"
                  :placeholder="t('accounting.financial.company.fields.companyBusinessLicense.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyOrganizationCode.label')" name="companyOrganizationCode">
                <a-input
                  v-model:value="form.companyOrganizationCode"
                  :placeholder="t('accounting.financial.company.fields.companyOrganizationCode.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyUnifiedCreditCode.label')" name="companyUnifiedCreditCode">
                <a-input
                  v-model:value="form.companyUnifiedCreditCode"
                  :placeholder="t('accounting.financial.company.fields.companyUnifiedCreditCode.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyCity.label')" name="companyCity">
                <a-input
                  v-model:value="form.companyCity"
                  :placeholder="t('accounting.financial.company.fields.companyCity.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyRegion.label')" name="companyRegion">
                <a-input
                  v-model:value="form.companyRegion"
                  :placeholder="t('accounting.financial.company.fields.companyRegion.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyPostalCode.label')" name="companyPostalCode">
                <a-input
                  v-model:value="form.companyPostalCode"
                  :placeholder="t('accounting.financial.company.fields.companyPostalCode.placeholder')"
                  show-count
                  :maxlength="10"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyCountry.label')" name="companyCountry">
                <a-input
                  v-model:value="form.companyCountry"
                  :placeholder="t('accounting.financial.company.fields.companyCountry.placeholder')"
                  show-count
                  :maxlength="3"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyFax.label')" name="companyFax">
                <a-input
                  v-model:value="form.companyFax"
                  :placeholder="t('accounting.financial.company.fields.companyFax.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.establishmentDate.label')" name="establishmentDate">
                <a-date-picker
                  v-model:value="form.establishmentDate"
                  :placeholder="t('accounting.financial.company.fields.establishmentDate.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.dissolutionDate.label')" name="dissolutionDate">
                <a-date-picker
                  v-model:value="form.dissolutionDate"
                  :placeholder="t('accounting.financial.company.fields.dissolutionDate.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.orderNum.label')" name="orderNum">
                <a-input-number
                  v-model:value="form.orderNum"
                  :placeholder="t('accounting.financial.company.fields.orderNum.placeholder')"
                  :min="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.status.label')" name="status">
                <Takt-select
                  v-model:value="form.status"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :placeholder="t('accounting.financial.company.fields.status.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 财务信息 -->
        <a-tab-pane :key="'financialInfo'" :tab="t('accounting.financial.company.tabs.financialInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyFiscalYearVariant.label')" name="companyFiscalYearVariant">
                <a-input
                  v-model:value="form.companyFiscalYearVariant"
                  :placeholder="t('accounting.financial.company.fields.companyFiscalYearVariant.placeholder')"
                  show-count
                  :maxlength="4"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyChartOfAccounts.label')" name="companyChartOfAccounts">
                <a-input
                  v-model:value="form.companyChartOfAccounts"
                  :placeholder="t('accounting.financial.company.fields.companyChartOfAccounts.placeholder')"
                  show-count
                  :maxlength="8"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyFinancialManagementArea.label')" name="companyFinancialManagementArea">
                <a-input
                  v-model:value="form.companyFinancialManagementArea"
                  :placeholder="t('accounting.financial.company.fields.companyFinancialManagementArea.placeholder')"
                  show-count
                  :maxlength="8"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyMaxExchangeRateDeviation.label')" name="companyMaxExchangeRateDeviation">
                <a-input-number
                  v-model:value="form.companyMaxExchangeRateDeviation"
                  :placeholder="t('accounting.financial.company.fields.companyMaxExchangeRateDeviation.placeholder')"
                  :min="0"
                  :max="100"
                  :precision="2"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyAllocationIdentifier.label')" name="companyAllocationIdentifier">
                <a-input
                  v-model:value="form.companyAllocationIdentifier"
                  :placeholder="t('accounting.financial.company.fields.companyAllocationIdentifier.placeholder')"
                  show-count
                  :maxlength="2"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyRelatedCompany.label')" name="companyRelatedCompany">
                <a-input
                  v-model:value="form.companyRelatedCompany"
                  :placeholder="t('accounting.financial.company.fields.companyRelatedCompany.placeholder')"
                  show-count
                  :maxlength="12"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('accounting.financial.company.fields.companyRelatedPlant.label')" name="companyRelatedPlant">
                <a-input
                  v-model:value="form.companyRelatedPlant"
                  :placeholder="t('accounting.financial.company.fields.companyRelatedPlant.placeholder')"
                  show-count
                  :maxlength="8"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('menu.Logistics.Manufacturing.change.input.uketsuke')" name="companyAddressNumber">
                <a-input
                  v-model:value="form.companyAddressNumber"
                  :placeholder="t('accounting.financial.company.fields.companyAddressNumber.placeholder')"
                  show-count
                  :maxlength="20"
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
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktCompany, TaktCompanyCreate, TaktCompanyUpdate } from '@/types/accounting/financial/company'
import { getByIdAsync, createCompany, updateCompany } from '@/api/accounting/financial/company'
import dayjs from 'dayjs'

const { t } = useI18n()

// Props
interface Props {
  visible: boolean
  title: string
  companyId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  companyId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  success: []
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 是否编辑模式
const isEdit = computed(() => !!props.companyId)

// 表单数据
const form = ref<TaktCompanyCreate | TaktCompanyUpdate>({
  companyCode: '',
  companyName: '',
  companyShortName: '',
  companyTaxNumber: '',
  companyNature: undefined,
  companyIndustryType: undefined,
  companyCurrency: 0,
  companyLanguage: 0,
  companyPhone: '',
  companyEmail: '',
  companyWebsite: '',
  companyScale: '',
  companyAddress: '',
  companyName1: '',
  companyName2: '',
  companyName3: '',
  companyLegalRepresentative: '',
  companyRegisteredCapital: undefined,
  companyBusinessLicense: '',
  companyOrganizationCode: '',
  companyUnifiedCreditCode: '',
  companyCity: '',
  companyRegion: '',
  companyPostalCode: '',
  companyCountry: '',
  companyFax: '',
  establishmentDate: undefined,
  dissolutionDate: undefined,
  orderNum: 0,
  status: 0,
  companyFiscalYearVariant: '',
  companyChartOfAccounts: '',
  companyFinancialManagementArea: '',
  companyMaxExchangeRateDeviation: undefined,
  companyAllocationIdentifier: '',
  companyRelatedCompany: '',
  companyRelatedPlant: '',
  companyAddressNumber: '',
  remark: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  companyCode: [
    { required: true, message: t('accounting.financial.company.fields.companyCode.required'), trigger: 'blur' }
  ],
  companyName: [
    { required: true, message: t('accounting.financial.company.fields.companyName.required'), trigger: 'blur' }
  ],
  companyCurrency: [
    { required: true, message: t('accounting.financial.company.fields.companyCurrency.required'), trigger: 'change' }
  ],
  companyLanguage: [
    { required: true, message: t('accounting.financial.company.fields.companyLanguage.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('accounting.financial.company.fields.status.required'), trigger: 'change' }
  ]
}

// 获取公司详情
const fetchCompanyDetail = async (companyId: string) => {
  try {
    loading.value = true
    const response = await getByIdAsync(companyId)
    form.value = { ...response.data }
    
    // 处理日期字段
    if (response.data.establishmentDate) {
      form.value.establishmentDate = dayjs(response.data.establishmentDate).format('YYYY-MM-DD')
    }
    if (response.data.dissolutionDate) {
      form.value.dissolutionDate = dayjs(response.data.dissolutionDate).format('YYYY-MM-DD')
    }
  } catch (error) {
    console.error('获取公司详情失败:', error)
    message.error(t('common.loadFailed'))
  } finally {
    loading.value = false
  }
}

// 重置表单到初始状态
const resetForm = () => {
  form.value = {
    companyCode: '',
    companyName: '',
    companyShortName: '',
    companyTaxNumber: '',
    companyNature: undefined,
    companyIndustryType: undefined,
    companyCurrency: 0,
    companyLanguage: 0,
    companyPhone: '',
    companyEmail: '',
    companyWebsite: '',
    companyScale: '',
    companyAddress: '',
    companyName1: '',
    companyName2: '',
    companyName3: '',
    companyLegalRepresentative: '',
    companyRegisteredCapital: undefined,
    companyBusinessLicense: '',
    companyOrganizationCode: '',
    companyUnifiedCreditCode: '',
    companyCity: '',
    companyRegion: '',
    companyPostalCode: '',
    companyCountry: '',
    companyFax: '',
    establishmentDate: undefined,
    dissolutionDate: undefined,
    orderNum: 0,
    status: 0,
    companyFiscalYearVariant: '',
    companyChartOfAccounts: '',
    companyFinancialManagementArea: '',
    companyMaxExchangeRateDeviation: undefined,
    companyAllocationIdentifier: '',
    companyRelatedCompany: '',
    companyRelatedPlant: '',
    companyAddressNumber: '',
    remark: ''
  }
  nextTick(() => {
    formRef.value?.resetFields()
  })
}

// 监听弹窗显示状态和公司ID的组合变化
watch([() => props.visible, () => props.companyId], ([newVisible, newCompanyId], [oldVisible, oldCompanyId]) => {
  if (newVisible && !oldVisible) {
    // 弹窗刚打开
    if (newCompanyId) {
      // 编辑模式，加载数据
      fetchCompanyDetail(newCompanyId)
    } else {
      // 新增模式，重置表单
      resetForm()
    }
  } else if (!newVisible && oldVisible) {
    // 弹窗关闭，重置表单
    resetForm()
  } else if (newVisible && oldVisible && newCompanyId !== oldCompanyId) {
    // 弹窗已打开，但公司ID发生变化（编辑切换到新增或新增切换到编辑）
    if (newCompanyId) {
      // 切换到编辑模式
      fetchCompanyDetail(newCompanyId)
    } else {
      // 切换到新增模式
      resetForm()
    }
  }
}, { immediate: true })

// 处理可见性变化
const handleVisibleChange = (visible: boolean) => {
  emit('update:visible', visible)
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    
    loading.value = true
    
    // 处理日期字段
    const submitData = { ...form.value }
    if (submitData.establishmentDate) {
      submitData.establishmentDate = dayjs(submitData.establishmentDate).format('YYYY-MM-DD')
    }
    if (submitData.dissolutionDate) {
      submitData.dissolutionDate = dayjs(submitData.dissolutionDate).format('YYYY-MM-DD')
    }
    
    if (isEdit.value) {
      await updateCompany(submitData as TaktCompanyUpdate)
      message.success(t('common.updateSuccess'))
    } else {
      await createCompany(submitData as TaktCompanyCreate)
      message.success(t('common.addSuccess'))
    }
    
    emit('success')
  } catch (error) {
    console.error('提交表单失败:', error)
    message.error(t('common.submitFailed'))
  } finally {
    loading.value = false
  }
}

// 取消表单
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}
</script>

<style scoped>
/* 样式可以根据需要添加 */
</style>
