<template>
  <Takt-modal
    v-model:open="visible"
    :title="t('workflow.trans.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-trans-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.trans.fields.instanceTransId')">
          {{ detail.instanceTransId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.instanceId')">
          {{ detail.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.startNodeId')">
          {{ detail.startNodeId || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.startNodeName')">
          {{ detail.startNodeName || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.startNodeType')">
          <Takt-dict-tag dict-type="wf_node_type" :value="detail.startNodeType" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.toNodeId')">
          {{ detail.toNodeId || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.toNodeName')">
          {{ detail.toNodeName || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.toNodeType')">
          <Takt-dict-tag dict-type="wf_node_type" :value="detail.toNodeType" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.status')">
          <Takt-dict-tag dict-type="wf_instance_trans_status" :value="detail.status" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.trans.fields.transTime')" :span="2">
          {{ detail.transTime || '无' }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 时间信息 -->
      <div class="time-info">
        <h3>{{ t('common.timeInfo') }}</h3>
        <a-descriptions :column="2" bordered>
          <a-descriptions-item :label="t('common.createBy')">
            {{ detail.createBy || '无' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('common.createTime')">
            {{ detail.createTime || '无' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('common.updateBy')">
            {{ detail.updateBy || '无' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('common.updateTime')">
            {{ detail.updateTime || '无' }}
          </a-descriptions-item>
        </a-descriptions>
      </div>
    </div>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { TaktInstanceTrans } from '@/types/workflow/trans'
import { getInstanceTransById } from '@/api/workflow/trans'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  instanceTransId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<TaktInstanceTrans>({
  instanceTransId: '0',
  instanceId: '0',
  startNodeId: '',
  startNodeType: 1,
  startNodeName: '',
  toNodeId: '',
  toNodeType: 1,
  toNodeName: '',
  status: 1,
  transTime: '',
  isDeleted: 0,
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: ''
} as TaktInstanceTrans)

// 获取转换记录详情
const fetchData = async () => {
  if (!props.instanceTransId) {
    return
  }
  try {
    const response = await getInstanceTransById(props.instanceTransId)
    if (response.data) {
      detail.value = response.data
    }
  } catch (error) {
    console.error('获取转换记录详情失败:', error)
  }
}

// 监听instanceTransId变化
watch(() => props.instanceTransId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })
</script>

<style lang="less" scoped>
.workflow-trans-detail {
  .time-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }
}
</style> 
