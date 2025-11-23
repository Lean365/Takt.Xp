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
        <a-tab-pane :key="'basicInfo'" :tab="t('routine.core.numberrule.tabs.basicInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.companyCode.label')" name="companyCode">
                <a-input
                  v-model:value="form.companyCode"
                  :placeholder="t('routine.core.numberrule.fields.companyCode.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.numberRuleName.label')" name="numberRuleName">
                <a-input
                  v-model:value="form.numberRuleName"
                  :placeholder="t('routine.core.numberrule.fields.numberRuleName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.numberRuleCode.label')" name="numberRuleCode">
                <a-input
                  v-model:value="form.numberRuleCode"
                  :placeholder="t('routine.core.numberrule.fields.numberRuleCode.placeholder')"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.deptCode.label')" name="deptCode">
                <a-input
                  v-model:value="form.deptCode"
                  :placeholder="t('routine.core.numberrule.fields.deptCode.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.numberRuleShortCode.label')" name="numberRuleShortCode">
                <a-input
                  v-model:value="form.numberRuleShortCode"
                  :placeholder="t('routine.core.numberrule.fields.numberRuleShortCode.placeholder')"
                  show-count
                  :maxlength="4"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.numberRuleType.label')" name="numberRuleType">
                <Takt-select
                  v-model:value="form.numberRuleType"
                  dict-type="routine_number_rule_type"
                  :placeholder="t('routine.core.numberrule.fields.numberRuleType.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.status.label')" name="status">
                <Takt-select
                  v-model:value="form.status"
                  dict-type="sys_normal_disable"
                  :placeholder="t('routine.core.numberrule.fields.status.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <!-- 占位列，保持布局平衡 -->
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('routine.core.numberrule.fields.numberRuleDescription.label')" name="numberRuleDescription">
                <a-textarea
                  v-model:value="form.numberRuleDescription"
                  :placeholder="t('routine.core.numberrule.fields.numberRuleDescription.placeholder')"
                  :rows="3"
                  show-count
                  :maxlength="500"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 编号配置 -->
        <a-tab-pane :key="'numberConfig'" :tab="t('routine.core.numberrule.tabs.numberConfig')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.numberPrefix.label')" name="numberPrefix">
                <a-input
                  v-model:value="form.numberPrefix"
                  :placeholder="t('routine.core.numberrule.fields.numberPrefix.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.numberSuffix.label')" name="numberSuffix">
                <a-input
                  v-model:value="form.numberSuffix"
                  :placeholder="t('routine.core.numberrule.fields.numberSuffix.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.dateFormat.label')" name="dateFormat">
                <a-select
                  v-model:value="form.dateFormat"
                  :placeholder="t('routine.core.numberrule.fields.dateFormat.placeholder')"
                >
                  <a-select-option value="yyyy">yyyy</a-select-option>
                  <a-select-option value="yyyyMM">yyyyMM</a-select-option>
                  <a-select-option value="yyyyMMdd">yyyyMMdd</a-select-option>
                  <a-select-option value="yyyyMMddHH">yyyyMMddHH</a-select-option>
                  <a-select-option value="yyyyMMddHHmm">yyyyMMddHHmm</a-select-option>
                  <a-select-option value="yyyyMMddHHmmss">yyyyMMddHHmmss</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.sequenceLength.label')" name="sequenceLength">
                <a-input-number
                  v-model:value="form.sequenceLength"
                  :min="1"
                  :max="10"
                  :placeholder="t('routine.core.numberrule.fields.sequenceLength.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.sequenceStart.label')" name="sequenceStart">
                <a-input-number
                  v-model:value="form.sequenceStart"
                  :min="1"
                  :placeholder="t('routine.core.numberrule.fields.sequenceStart.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.sequenceStep.label')" name="sequenceStep">
                <a-input-number
                  v-model:value="form.sequenceStep"
                  :min="1"
                  :placeholder="t('routine.core.numberrule.fields.sequenceStep.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.currentSequence.label')" name="currentSequence">
                <a-input-number
                  v-model:value="form.currentSequence"
                  :min="0"
                  :placeholder="t('routine.core.numberrule.fields.currentSequence.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.sequenceResetRule.label')" name="sequenceResetRule">
                <Takt-select
                  v-model:value="form.sequenceResetRule"
                  dict-type="routine_sequence_reset_rule"
                  :placeholder="t('routine.core.numberrule.fields.sequenceResetRule.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 高级配置 -->
        <a-tab-pane :key="'advancedConfig'" :tab="t('routine.core.numberrule.tabs.advancedConfig')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.includeCompanyCode.label')" name="includeCompanyCode">
                <Takt-select
                  v-model:value="form.includeCompanyCode"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.includeCompanyCode.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.includeDepartmentCode.label')" name="includeDepartmentCode">
                <Takt-select
                  v-model:value="form.includeDepartmentCode"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.includeDepartmentCode.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.includeRevisionNumber.label')" name="includeRevisionNumber">
                <Takt-select
                  v-model:value="form.includeRevisionNumber"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.includeRevisionNumber.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.includeYear.label')" name="includeYear">
                <Takt-select
                  v-model:value="form.includeYear"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.includeYear.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.includeMonth.label')" name="includeMonth">
                <Takt-select
                  v-model:value="form.includeMonth"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.includeMonth.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.includeDay.label')" name="includeDay">
                <Takt-select
                  v-model:value="form.includeDay"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.includeDay.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.allowDuplicate.label')" name="allowDuplicate">
                <Takt-select
                  v-model:value="form.allowDuplicate"
                  dict-type="sys_yes_no"
                  :placeholder="t('routine.core.numberrule.fields.allowDuplicate.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.duplicateCheckScope.label')" name="duplicateCheckScope">
                <Takt-select
                  v-model:value="form.duplicateCheckScope"
                  dict-type="routine_duplicate_check_scope"
                  :placeholder="t('routine.core.numberrule.fields.duplicateCheckScope.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('routine.core.numberrule.fields.orderNum.label')" name="orderNum">
                <a-input-number
                  v-model:value="form.orderNum"
                  :min="0"
                  :placeholder="t('routine.core.numberrule.fields.orderNum.placeholder')"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 其他信息 -->
        <a-tab-pane :key="'otherInfo'" :tab="t('routine.core.numberrule.tabs.otherInfo')">
          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('table.columns.remark')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('table.columns.remark')"
                  :rows="4"
                  show-count
                  :maxlength="500"
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
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { TaktNumberRule, TaktNumberRuleCreate, TaktNumberRuleUpdate } from '@/types/routine/numberrule/numberrule'
import { useDictStore } from '@/stores/dictStore'
import {
  getByIdAsync,
  createNumberRule,
  updateNumberRule,
} from '@/api/routine/numberrule/numberrule'

const { t } = useI18n()
const dictStore = useDictStore()

// Props
interface Props {
  visible: boolean
  title: string
  numberRuleId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  numberRuleId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [visible: boolean]
  'success': []
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const form = reactive<TaktNumberRuleCreate>({
  companyCode: '',
  deptCode: '',
  numberRuleCode: '',
  numberRuleShortCode: '',
  numberRuleName: '',
  numberRuleType: 1,
  numberRuleDescription: '',
  numberPrefix: '',
  numberSuffix: '',
  revisionNumber: 1,
  dateFormat: 'yyyyMMdd',
  sequenceLength: 4,
  sequenceStart: 1,
  sequenceStep: 1,
  currentSequence: 0,
  sequenceResetRule: 1,
  numberFormatTemplate: '',
  numberExample: '',
  includeCompanyCode: 0,
  includeDepartmentCode: 0,
  includeRevisionNumber: 0,
  includeYear: 1,
  includeMonth: 0,
  includeDay: 0,
  includeHour: 0,
  includeMinute: 0,
  includeSecond: 0,
  includeMillisecond: 0,
  includeRandom: 0,
  randomLength: 0,
  includeCheckDigit: 0,
  checkDigitAlgorithm: 1,
  allowDuplicate: 0,
  duplicateCheckScope: 1,
  orderNum: 0,
  remark: '',
  status: 0
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  companyCode: [
    { required: true, message: t('routine.core.numberrule.fields.companyCode.required'), trigger: 'blur' },
    { min: 1, max: 20, message: t('routine.core.numberrule.fields.companyCode.length'), trigger: 'blur' }
  ],
  numberRuleName: [
    { required: true, message: t('routine.core.numberrule.fields.numberRuleName.required'), trigger: 'blur' },
    { min: 1, max: 100, message: t('routine.core.numberrule.fields.numberRuleName.length'), trigger: 'blur' }
  ],
  numberRuleCode: [
    { required: true, message: t('routine.core.numberrule.fields.numberRuleCode.required'), trigger: 'blur' },
    { min: 1, max: 50, message: t('routine.core.numberrule.fields.numberRuleCode.length'), trigger: 'blur' }
  ],
  deptCode: [
    { required: true, message: t('routine.core.numberrule.fields.deptCode.required'), trigger: 'blur' },
    { min: 1, max: 20, message: t('routine.core.numberrule.fields.deptCode.length'), trigger: 'blur' }
  ],
  numberRuleShortCode: [
    { required: true, message: t('routine.core.numberrule.fields.numberRuleShortCode.required'), trigger: 'blur' },
    { min: 1, max: 4, message: t('routine.core.numberrule.fields.numberRuleShortCode.length'), trigger: 'blur' }
  ],
  numberRuleType: [
    { required: true, message: t('routine.core.numberrule.fields.numberRuleType.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('routine.core.numberrule.fields.status.required'), trigger: 'change' }
  ],
  dateFormat: [
    { required: true, message: t('routine.core.numberrule.fields.dateFormat.required'), trigger: 'change' }
  ],
  sequenceLength: [
    { required: true, message: t('routine.core.numberrule.fields.sequenceLength.required'), trigger: 'blur' }
  ],
  sequenceStart: [
    { required: true, message: t('routine.core.numberrule.fields.sequenceStart.required'), trigger: 'blur' }
  ],
  sequenceStep: [
    { required: true, message: t('routine.core.numberrule.fields.sequenceStep.required'), trigger: 'blur' }
  ]
}

// 计算属性
const isEdit = computed(() => !!props.numberRuleId)

// 监听visible变化
watch(() => props.visible, (newVal) => {
  if (newVal) {
    if (props.numberRuleId) {
      loadData()
    } else {
      resetForm()
    }
  }
})

// 加载数据
const loadData = async () => {
  if (!props.numberRuleId) return
  
  try {
    loading.value = true
    const res = await getByIdAsync(props.numberRuleId)
    // 更新时只赋值创建所需的字段，排除ID和其他系统字段
    const { numberRuleId, createTime, updateTime, createBy, updateBy, ...formData } = res.data
    Object.assign(form, formData)
  } catch (error) {
    console.error('加载编号规则详情失败:', error)
    message.error(t('common.load.failed'))
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(form, {
    companyCode: '',
    deptCode: '',
    numberRuleCode: '',
    numberRuleShortCode: '',
    numberRuleName: '',
    numberRuleType: 1,
    numberRuleDescription: '',
    numberPrefix: '',
    numberSuffix: '',
    revisionNumber: 1,
    dateFormat: 'yyyyMMdd',
    sequenceLength: 4,
    sequenceStart: 1,
    sequenceStep: 1,
    currentSequence: 0,
    sequenceResetRule: 1,
    numberFormatTemplate: '',
    numberExample: '',
    includeCompanyCode: 0,
    includeDepartmentCode: 0,
    includeRevisionNumber: 0,
    includeYear: 1,
    includeMonth: 0,
    includeDay: 0,
    includeHour: 0,
    includeMinute: 0,
    includeSecond: 0,
    includeMillisecond: 0,
    includeRandom: 0,
    randomLength: 0,
    includeCheckDigit: 0,
    checkDigitAlgorithm: 1,
    allowDuplicate: 0,
    duplicateCheckScope: 1,
    orderNum: 0,
    remark: '',
    status: 0
  })
  formRef.value?.clearValidate()
}

// 处理可见性变化
const handleVisibleChange = (visible: boolean) => {
  emit('update:visible', visible)
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

    if (isEdit.value) {
      // 更新操作：构造更新数据
      const updateData: TaktNumberRuleUpdate = {
        numberRuleId: props.numberRuleId!,
        ...form
      }
      await updateNumberRule(updateData)
      message.success(t('common.update.success'))
    } else {
      // 新增操作：直接使用表单数据
      await createNumberRule(form)
      message.success(t('common.create.success'))
    }

    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error('提交失败:', error)
    if (error instanceof Error) {
      message.error(error.message)
    } else {
      message.error(t('common.submit.failed'))
    }
  } finally {
    loading.value = false
  }
}
</script>

<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 16px;
}

.ant-tabs-content-holder {
  padding-top: 16px;
}
</style>

