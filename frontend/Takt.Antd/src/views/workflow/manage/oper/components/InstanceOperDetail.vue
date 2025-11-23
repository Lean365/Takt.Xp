<template>
  <Takt-modal
    v-model:open="visible"
    :title="t('workflow.oper.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-oper-detail">
      <!-- 基本信息 -->
      <a-descriptions :column="2" bordered>
        <a-descriptions-item :label="t('workflow.oper.fields.instanceOperId')">
          {{ detail.instanceOperId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.instanceId')">
          {{ detail.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.nodeId')">
          {{ detail.nodeId || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.nodeName')">
          {{ detail.nodeName || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.operType')">
          <Takt-dict-tag dict-type="wf_instance_oper_type" :value="detail.operType" />
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.operatorId')">
          {{ detail.operatorId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.operatorName')">
          {{ detail.operatorName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.operOpinion')" :span="2">
          {{ detail.operOpinion || '无' }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.operData')" :span="2">
          <div v-if="detail.operData" class="oper-data-container">
            <a-button 
              type="link" 
              size="small" 
              @click="toggleJsonView"
              class="toggle-btn"
            >
              {{ showFormattedJson ? '显示原始数据' : '格式化显示' }}
            </a-button>
            <pre v-if="showFormattedJson" class="json-formatted">{{ formattedOperData }}</pre>
            <pre v-else class="json-raw">{{ detail.operData }}</pre>
          </div>
          <span v-else>无</span>
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.oper.fields.remark')" :span="2">
          {{ detail.remark || '无' }}
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
import type { TaktInstanceOper } from '@/types/workflow/oper'
import { getInstanceOperById } from '@/api/workflow/oper'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  instanceOperId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const detail = ref<TaktInstanceOper>({
  instanceOperId: '0',
  instanceId: '0',
  nodeId: '',
  nodeName: '',
  operType: 1,
  operatorId: '0',
  operatorName: '',
  operOpinion: '',
  operData: '',
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: ''
} as TaktInstanceOper)

const showFormattedJson = ref(true)

// 格式化操作数据
const formattedOperData = computed(() => {
  if (!detail.value.operData) return ''
  
  try {
    const parsed = JSON.parse(detail.value.operData)
    return JSON.stringify(parsed, null, 2)
  } catch (error) {
    // 如果不是有效的JSON，返回原始数据
    return detail.value.operData
  }
})

// 切换JSON显示方式
const toggleJsonView = () => {
  showFormattedJson.value = !showFormattedJson.value
}

// 获取操作记录详情
const fetchData = async () => {
  if (!props.instanceOperId) {
    return
  }
  try {
    const res = await getInstanceOperById(props.instanceOperId)
    if (res.data) {
      detail.value = res.data
    }
  } catch (error) {
    console.error('获取操作记录详情失败:', error)
  }
}

// 监听instanceOperId变化
watch(() => props.instanceOperId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })
</script>

<style lang="less" scoped>
.workflow-oper-detail {
  .time-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }

  .oper-data-container {
    position: relative;
    
    .toggle-btn {
      position: absolute;
      top: -8px;
      right: 0;
      z-index: 1;
      padding: 0 8px;
      height: 24px;
      line-height: 22px;
      font-size: 12px;
    }
  }

  .json-formatted,
  .json-raw {
    margin: 0;
    padding: 12px;
    background-color: #f8f9fa;
    border: 1px solid #e9ecef;
    border-radius: 6px;
    white-space: pre-wrap;
    word-wrap: break-word;
    font-family: 'Monaco', 'Menlo', 'Ubuntu Mono', monospace;
    font-size: 12px;
    line-height: 1.4;
    max-height: 300px;
    overflow-y: auto;
  }

  .json-formatted {
    color: #2c3e50;
  }

  .json-raw {
    color: #495057;
  }
}
</style> 
