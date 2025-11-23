<template>
  <a-modal
    :open="open"
    title="生产明细详情"
    :width="800"
    @cancel="handleCancel"
  >
    <div v-if="detailData" class="detail-content">
      <a-descriptions :column="2" bordered>
        <a-descriptions-item label="明细ID">
          {{ detailData.assyOutputDetailId }}
        </a-descriptions-item>
        <a-descriptions-item label="组立日报ID">
          {{ detailData.assyOutputId }}
        </a-descriptions-item>
        <a-descriptions-item label="生产时段">
          {{ detailData.timePeriod }}
        </a-descriptions-item>
        <a-descriptions-item label="实际生产数量">
          {{ detailData.prodActualQty }}
        </a-descriptions-item>
        <a-descriptions-item label="停线时间(分钟)">
          {{ detailData.downtimeMinutes }}
        </a-descriptions-item>
        <a-descriptions-item label="停线原因">
          {{ detailData.downtimeReason || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="停线说明">
          {{ detailData.downtimeDescription || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="未达成原因">
          {{ detailData.unachievedReason || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="未达成说明">
          {{ detailData.unachievedDescription || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="投入工时(分钟)">
          {{ detailData.inputMinutes }}
        </a-descriptions-item>
        <a-descriptions-item label="生产工时(分钟)">
          {{ detailData.prodMinutes }}
        </a-descriptions-item>
        <a-descriptions-item label="实际工时(分钟)">
          {{ detailData.actualMinutes }}
        </a-descriptions-item>
        <a-descriptions-item label="达成率(%)">
          {{ detailData.achievementRate }}
        </a-descriptions-item>
        <a-descriptions-item label="创建人">
          {{ detailData.createBy }}
        </a-descriptions-item>
        <a-descriptions-item label="创建时间">
          {{ detailData.createTime }}
        </a-descriptions-item>
        <a-descriptions-item label="备注" :span="2">
          {{ detailData.remark || '-' }}
        </a-descriptions-item>
      </a-descriptions>
    </div>
    
    <template #footer>
      <a-button @click="handleCancel">关闭</a-button>
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { TaktAssyOutputDetail } from '@/types/logistics/manufacturing/execution/output/assyOutputDetail'
import { getAssyOutputDetail } from '@/api/logistics/manufacturing/execution/output/assyOutputDetail'

interface Props {
  open: boolean
  detailId?: number
}

interface Emits {
  (e: 'update:open', value: boolean): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const detailData = ref<TaktAssyOutputDetail | null>(null)

// 监听ID变化，加载详情
watch(() => props.detailId, (newId) => {
  if (newId && props.open) {
    loadDetail(newId)
  }
})

// 监听弹窗状态
watch(() => props.open, (newOpen) => {
  if (newOpen && props.detailId) {
    loadDetail(props.detailId)
  }
})

// 加载详情
const loadDetail = async (id: number) => {
  try {
    const res = await getAssyOutputDetail(id)
    if (res.data.code === 200) {
      detailData.value = res.data.data
    } else {
      console.error('获取详情失败:', res.data.msg)
    }
  } catch (error) {
    console.error('获取详情失败:', error)
  }
}

// 关闭弹窗
const handleCancel = () => {
  emit('update:open', false)
  detailData.value = null
}
</script>

<style lang="less" scoped>
.detail-content {
  // 样式可以根据需要添加
}
</style>

