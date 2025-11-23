<template>
  <a-modal
    :open="open"
    title="组立日报详情"
    :width="900"
    @cancel="handleCancel"
  >
    <div v-if="detailData" class="detail-content">
      <a-descriptions :column="2" bordered>
        <a-descriptions-item label="组立日报ID">
          {{ detailData.assyOutputId }}
        </a-descriptions-item>
        <a-descriptions-item label="工厂代码">
          {{ detailData.plantCode }}
        </a-descriptions-item>
        <a-descriptions-item label="生产类别">
          {{ detailData.prodCategory }}
        </a-descriptions-item>
        <a-descriptions-item label="生产日期">
          {{ detailData.prodDate }}
        </a-descriptions-item>
        <a-descriptions-item label="生产线">
          {{ detailData.prodLine }}
        </a-descriptions-item>
        <a-descriptions-item label="班次">
          <Takt-dict-tag dict-type="shift_type" :value="detailData.shiftNo" />
        </a-descriptions-item>
        <a-descriptions-item label="生产订单类型">
          {{ detailData.prodOrderType || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="生产订单号">
          {{ detailData.prodOrderCode }}
        </a-descriptions-item>
        <a-descriptions-item label="型号代码">
          {{ detailData.modelCode }}
        </a-descriptions-item>
        <a-descriptions-item label="物料代码">
          {{ detailData.materialCode }}
        </a-descriptions-item>
        <a-descriptions-item label="批次号">
          {{ detailData.batchNo || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="直接人工">
          {{ detailData.directLabor }}
        </a-descriptions-item>
        <a-descriptions-item label="间接人工">
          {{ detailData.indirectLabor }}
        </a-descriptions-item>
        <a-descriptions-item label="订单数量">
          {{ detailData.prodOrderQty }}
        </a-descriptions-item>
        <a-descriptions-item label="标准工时(分钟)">
          {{ detailData.stdMinutes }}
        </a-descriptions-item>
        <a-descriptions-item label="标准产能">
          {{ detailData.stdCapacity }}
        </a-descriptions-item>
        <a-descriptions-item label="状态">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="detailData.status" />
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

      <!-- 明细列表 -->
      <div v-if="detailData.assyOutputDetails && detailData.assyOutputDetails.length > 0" class="detail-list">
        <h3 class="detail-title">生产明细列表</h3>
        <a-table
          :columns="detailColumns"
          :data-source="detailData.assyOutputDetails"
          :pagination="false"
          size="small"
          bordered
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.key === 'status'">
              <Takt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
            </template>
          </template>
        </a-table>
      </div>
    </div>
    
    <template #footer>
      <a-button @click="handleCancel">关闭</a-button>
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import type { TaktAssyOutput } from '@/types/logistics/manufacturing/execution/output/assyOutput'
import type { TaktAssyOutputDetail } from '@/types/logistics/manufacturing/execution/output/assyOutputDetail'
import { getAssyOutput } from '@/api/logistics/manufacturing/execution/output/assyOutput'

interface Props {
  open: boolean
  assyOutputId?: number
}

interface Emits {
  (e: 'update:open', value: boolean): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const detailData = ref<TaktAssyOutput | null>(null)

// 明细表格列定义
const detailColumns = [
  {
    title: '生产时段',
    dataIndex: 'timePeriod',
    key: 'timePeriod',
    width: 120
  },
  {
    title: '实际生产数量',
    dataIndex: 'prodActualQty',
    key: 'prodActualQty',
    width: 120
  },
  {
    title: '停线时间(分钟)',
    dataIndex: 'downtimeMinutes',
    key: 'downtimeMinutes',
    width: 120
  },
  {
    title: '停线原因',
    dataIndex: 'downtimeReason',
    key: 'downtimeReason',
    width: 150,
    ellipsis: true
  },
  {
    title: '投入工时(分钟)',
    dataIndex: 'inputMinutes',
    key: 'inputMinutes',
    width: 120
  },
  {
    title: '生产工时(分钟)',
    dataIndex: 'prodMinutes',
    key: 'prodMinutes',
    width: 120
  },
  {
    title: '实际工时(分钟)',
    dataIndex: 'actualMinutes',
    key: 'actualMinutes',
    width: 120
  },
  {
    title: '达成率(%)',
    dataIndex: 'achievementRate',
    key: 'achievementRate',
    width: 100
  }
]

// 监听ID变化，加载详情
watch(() => props.assyOutputId, (newId) => {
  if (newId && props.open) {
    loadDetail(newId)
  }
})

// 监听弹窗状态
watch(() => props.open, (newOpen) => {
  if (newOpen && props.assyOutputId) {
    loadDetail(props.assyOutputId)
  }
})

// 加载详情
const loadDetail = async (id: number) => {
  try {
    const res = await getAssyOutput(id)
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
  .detail-list {
    margin-top: 24px;
    
    .detail-title {
      margin-bottom: 16px;
      font-size: 16px;
      font-weight: 500;
      color: var(--ant-color-text);
    }
  }
}
</style>

