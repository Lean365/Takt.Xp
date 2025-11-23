<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :width="1000"
    :footer="null"
    @update:open="handleVisibleChange"
  >
    <div v-if="loading" class="loading-container">
      <a-spin size="large" />
    </div>
    <div v-else-if="data" class="detail-container">
      <a-tabs>
        <!-- 基本信息 -->
        <a-tab-pane :key="'basicInfo'" :tab="t('routine.core.numberrule.tabs.basicInfo')">
          <a-descriptions :column="2" bordered>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberRuleId.label')">
              {{ data.numberRuleId }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberRuleName.label')">
              {{ data.numberRuleName }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberRuleCode.label')">
              {{ data.numberRuleCode }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberRuleType.label')">
              <Takt-dict-tag dict-type="routine_number_rule_type" :value="data.numberRuleType" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.status.label')">
              <Takt-dict-tag dict-type="sys_normal_disable" :value="data.status" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberRuleDescription.label')" :span="2">
              {{ data.numberRuleDescription || '-' }}
            </a-descriptions-item>
          </a-descriptions>
        </a-tab-pane>

        <!-- 编号配置 -->
        <a-tab-pane :key="'numberConfig'" :tab="t('routine.core.numberrule.tabs.numberConfig')">
          <a-descriptions :column="2" bordered>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberPrefix.label')">
              {{ data.numberPrefix || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberSuffix.label')">
              {{ data.numberSuffix || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.dateFormat.label')">
              {{ data.dateFormat }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.sequenceLength.label')">
              {{ data.sequenceLength }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.sequenceStart.label')">
              {{ data.sequenceStart }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.sequenceStep.label')">
              {{ data.sequenceStep }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.currentSequence.label')">
              {{ data.currentSequence }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.sequenceResetRule.label')">
              <Takt-dict-tag dict-type="routine_sequence_reset_rule" :value="data.sequenceResetRule" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberFormatTemplate.label')" :span="2">
              {{ data.numberFormatTemplate || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.numberExample.label')" :span="2">
              {{ data.numberExample || '-' }}
            </a-descriptions-item>
          </a-descriptions>
        </a-tab-pane>

        <!-- 高级配置 -->
        <a-tab-pane :key="'advancedConfig'" :tab="t('routine.core.numberrule.tabs.advancedConfig')">
          <a-descriptions :column="2" bordered>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeCompanyCode.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeCompanyCode" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeDepartmentCode.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeDepartmentCode" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeYear.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeYear" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeMonth.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeMonth" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeDay.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeDay" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeHour.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeHour" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeMinute.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeMinute" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeSecond.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeSecond" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeMillisecond.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeMillisecond" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeRandom.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeRandom" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.randomLength.label')">
              {{ data.randomLength }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.includeCheckDigit.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.includeCheckDigit" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.checkDigitAlgorithm.label')">
              <Takt-dict-tag dict-type="routine_check_digit_algorithm" :value="data.checkDigitAlgorithm" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.allowDuplicate.label')">
              <Takt-dict-tag dict-type="sys_yes_no" :value="data.allowDuplicate" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.duplicateCheckScope.label')">
              <Takt-dict-tag dict-type="routine_duplicate_check_scope" :value="data.duplicateCheckScope" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.orderNum.label')">
              {{ data.orderNum }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.lastUsedTime.label')">
              {{ data.lastUsedTime ? new Date(data.lastUsedTime).toLocaleString() : '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('routine.core.numberrule.fields.usageCount.label')">
              {{ data.usageCount }}
            </a-descriptions-item>
          </a-descriptions>
        </a-tab-pane>

        <!-- 其他信息 -->
        <a-tab-pane :key="'otherInfo'" :tab="t('routine.core.numberrule.tabs.otherInfo')">
          <a-descriptions :column="2" bordered>
            <a-descriptions-item :label="t('table.columns.remark')" :span="2">
              {{ data.remark || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('table.columns.createBy')">
              {{ data.createBy || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('table.columns.createTime')">
              {{ data.createTime ? new Date(data.createTime).toLocaleString() : '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('table.columns.updateBy')">
              {{ data.updateBy || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('table.columns.updateTime')">
              {{ data.updateTime ? new Date(data.updateTime).toLocaleString() : '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('table.columns.deleteBy')">
              {{ data.deleteBy || '-' }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('table.columns.deleteTime')">
              {{ data.deleteTime ? new Date(data.deleteTime).toLocaleString() : '-' }}
            </a-descriptions-item>
          </a-descriptions>
        </a-tab-pane>
      </a-tabs>
    </div>
    <div v-else class="error-container">
      <a-result
        status="error"
        :title="t('common.load.failed')"
        :sub-title="t('common.load.failed.desc')"
      >
        <template #extra>
          <a-button type="primary" @click="loadData">
            {{ t('common.retry') }}
          </a-button>
        </template>
      </a-result>
    </div>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktNumberRule } from '@/types/routine/numberrule/numberrule'
import { useDictStore } from '@/stores/dictStore'
import { getByIdAsync } from '@/api/routine/numberrule/numberrule'

const { t } = useI18n()
const dictStore = useDictStore()

// Props
interface Props {
  visible: boolean
  numberRuleId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  numberRuleId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [visible: boolean]
}>()

// 数据
const data = ref<TaktNumberRule | null>(null)
const loading = ref(false)

// 计算属性
const title = computed(() => {
  return data.value ? `${t('routine.core.numberrule.detail.title')} - ${data.value.numberRuleName}` : t('routine.core.numberrule.detail.title')
})

// 监听visible变化
watch(() => props.visible, (newVal) => {
  if (newVal && props.numberRuleId) {
    loadData()
  }
})

// 加载数据
const loadData = async () => {
  if (!props.numberRuleId) return
  
  try {
    loading.value = true
    const res = await getByIdAsync(props.numberRuleId)
    data.value = res.data
  } catch (error) {
    console.error('加载编号规则详情失败:', error)
    message.error(t('common.load.failed'))
    data.value = null
  } finally {
    loading.value = false
  }
}

// 处理可见性变化
const handleVisibleChange = (visible: boolean) => {
  emit('update:visible', visible)
}
</script>

<style lang="less" scoped>
.loading-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 200px;
}

.detail-container {
  .ant-descriptions {
    margin-bottom: 16px;
  }
}

.error-container {
  padding: 20px 0;
}
</style>

