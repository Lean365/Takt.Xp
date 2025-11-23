<template>
  <div class="workflow-overview-container">
    <!-- 统计卡片 -->
    <div class="stats-cards">
      <a-row :gutter="16">
        <a-col :span="6">
          <a-card class="stat-card">
            <a-statistic
              :title="t('workflow.overview.totalInstances')"
              :value="stats.totalInstances"
              :value-style="{ color: '#1890ff' }"
            >
              <template #prefix>
                <FileTextOutlined />
              </template>
            </a-statistic>
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card class="stat-card">
            <a-statistic
              :title="t('workflow.overview.runningInstances')"
              :value="stats.runningInstances"
              :value-style="{ color: '#52c41a' }"
            >
              <template #prefix>
                <PlayCircleOutlined />
              </template>
            </a-statistic>
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card class="stat-card">
            <a-statistic
              :title="t('workflow.overview.completedInstances')"
              :value="stats.completedInstances"
              :value-style="{ color: '#722ed1' }"
            >
              <template #prefix>
                <CheckCircleOutlined />
              </template>
            </a-statistic>
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card class="stat-card">
            <a-statistic
              :title="t('workflow.overview.totalSchemes')"
              :value="stats.totalSchemes"
              :value-style="{ color: '#fa8c16' }"
            >
              <template #prefix>
                <SettingOutlined />
              </template>
            </a-statistic>
          </a-card>
        </a-col>
      </a-row>
    </div>

    <!-- 图表区域 -->
    <div class="charts-section">
      <a-row :gutter="16">
        <!-- 流程状态分布 -->
        <a-col :span="12">
          <a-card :title="t('workflow.overview.statusDistribution')" class="chart-card">
            <div ref="statusChartRef" class="chart-container"></div>
          </a-card>
        </a-col>
        
        <!-- 流程优先级分布 -->
        <a-col :span="12">
          <a-card :title="t('workflow.overview.priorityDistribution')" class="chart-card">
            <div ref="priorityChartRef" class="chart-container"></div>
          </a-card>
        </a-col>
      </a-row>
    </div>

    <!-- 最近流程 -->
    <div class="recent-section">
      <a-card :title="t('workflow.overview.recentInstances')" class="recent-card">
        <template #extra>
          <a-button type="link" @click="goToMyWorkflow">
            {{ t('workflow.overview.viewAll') }}
          </a-button>
        </template>
        
        <a-table
          :loading="recentLoading"
          :data-source="recentInstances"
          :columns="recentColumns"
          :pagination="false"
          :row-key="(record: TaktInstance) => String(record.instanceId)"
          size="small"
        >
          <!-- 状态列 -->
          <template #status="{ record }">
            <a-tag :color="getStatusColor(record.status)">
              {{ getStatusText(record.status) }}
            </a-tag>
          </template>

          <!-- 优先级列 -->
          <template #priority="{ record }">
            <a-tag :color="getPriorityColor(record.priority)">
              {{ getPriorityText(record.priority) }}
            </a-tag>
          </template>

          <!-- 操作列 -->
          <template #action="{ record }">
            <a-button type="link" size="small" @click="viewInstance(record)">
              {{ t('common.view') }}
            </a-button>
          </template>
        </a-table>
      </a-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { message } from 'ant-design-vue'
import { 
  FileTextOutlined, 
  PlayCircleOutlined, 
  CheckCircleOutlined, 
  SettingOutlined 
} from '@ant-design/icons-vue'
import * as echarts from 'echarts'
import type { TaktInstance } from '@/types/workflow/instance'
import { getMyInstances } from '@/api/workflow/instance'
import { getSchemeList } from '@/api/workflow/scheme'

// 国际化
const { t } = useI18n()

// 路由
const router = useRouter()

// 响应式数据
const recentLoading = ref(false)
const recentInstances = ref<TaktInstance[]>([])
const statusChartRef = ref<HTMLElement>()
const priorityChartRef = ref<HTMLElement>()

// 统计数据
const stats = reactive({
  totalInstances: 0,
  runningInstances: 0,
  completedInstances: 0,
  totalSchemes: 0
})

// 最近流程表格列
const recentColumns = [
  {
    key: 'instanceTitle',
    title: t('workflow.instance.title'),
    dataIndex: 'instanceTitle',
    ellipsis: true
  },
  {
    key: 'status',
    title: t('workflow.instance.status'),
    dataIndex: 'status',
    width: 100,
    slots: { customRender: 'status' }
  },
  {
    key: 'priority',
    title: t('workflow.instance.priority'),
    dataIndex: 'priority',
    width: 100,
    slots: { customRender: 'priority' }
  },
  {
    key: 'createTime',
    title: t('common.createTime'),
    dataIndex: 'createTime',
    width: 160
  },
  {
    key: 'action',
    title: t('common.action'),
    width: 80,
    slots: { customRender: 'action' }
  }
]

// 获取状态颜色
const getStatusColor = (status: number) => {
  const colors = {
    0: 'default',    // 草稿
    1: 'processing', // 运行中
    2: 'success',    // 已完成
    3: 'error'       // 已停用
  }
  return colors[status as keyof typeof colors] || 'default'
}

// 获取状态文本
const getStatusText = (status: number) => {
  const texts = {
    0: t('workflow.instance.status.draft'),
    1: t('workflow.instance.status.running'),
    2: t('workflow.instance.status.completed'),
    3: t('workflow.instance.status.stopped')
  }
  return texts[status as keyof typeof texts] || t('common.unknown')
}

// 获取优先级颜色
const getPriorityColor = (priority: number) => {
  const colors = {
    1: 'default',    // 低
    2: 'blue',       // 普通
    3: 'orange',     // 高
    4: 'red',        // 紧急
    5: 'purple'      // 特急
  }
  return colors[priority as keyof typeof colors] || 'default'
}

// 获取优先级文本
const getPriorityText = (priority: number) => {
  const texts = {
    1: t('workflow.instance.priority.low'),
    2: t('workflow.instance.priority.normal'),
    3: t('workflow.instance.priority.high'),
    4: t('workflow.instance.priority.urgent'),
    5: t('workflow.instance.priority.critical')
  }
  return texts[priority as keyof typeof texts] || t('common.unknown')
}

// 获取统计数据
const fetchStats = async () => {
  try {
    // 获取我的流程实例
    const instanceResponse = await getMyInstances({ pageNum: 1, pageSize: 1000 })
    if (instanceResponse.code === 200) {
      const instances = instanceResponse.data.rows || []
      stats.totalInstances = instances.length
      stats.runningInstances = instances.filter(item => item.status === 1).length
      stats.completedInstances = instances.filter(item => item.status === 2).length
    }

    // 获取流程定义
    const schemeResponse = await getSchemeList({ pageNum: 1, pageSize: 1000 })
    if (schemeResponse.code === 200) {
      stats.totalSchemes = schemeResponse.data.total || 0
    }
  } catch (error) {
    console.error('获取统计数据失败:', error)
  }
}

// 获取最近流程
const fetchRecentInstances = async () => {
  try {
    recentLoading.value = true
    const response = await getMyInstances({ 
      pageNum: 1, 
      pageSize: 10,
      orderBy: 'createTime',
      orderDirection: 'desc'
    })
    if (response.code === 200) {
      recentInstances.value = response.data.rows || []
    }
  } catch (error) {
    console.error('获取最近流程失败:', error)
  } finally {
    recentLoading.value = false
  }
}

// 初始化状态分布图表
const initStatusChart = () => {
  if (!statusChartRef.value) return
  
  const chart = echarts.init(statusChartRef.value)
  const option = {
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: t('workflow.overview.statusDistribution'),
        type: 'pie',
        radius: '50%',
        data: [
          { value: stats.runningInstances, name: t('workflow.instance.status.running') },
          { value: stats.completedInstances, name: t('workflow.instance.status.completed') },
          { value: stats.totalInstances - stats.runningInstances - stats.completedInstances, name: t('workflow.instance.status.other') }
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  }
  chart.setOption(option)
  
  // 响应式调整
  window.addEventListener('resize', () => {
    chart.resize()
  })
}

// 初始化优先级分布图表
const initPriorityChart = () => {
  if (!priorityChartRef.value) return
  
  const chart = echarts.init(priorityChartRef.value)
  const option = {
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: t('workflow.overview.priorityDistribution'),
        type: 'pie',
        radius: '50%',
        data: [
          { value: 10, name: t('workflow.instance.priority.low') },
          { value: 20, name: t('workflow.instance.priority.normal') },
          { value: 15, name: t('workflow.instance.priority.high') },
          { value: 8, name: t('workflow.instance.priority.urgent') },
          { value: 5, name: t('workflow.instance.priority.critical') }
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  }
  chart.setOption(option)
  
  // 响应式调整
  window.addEventListener('resize', () => {
    chart.resize()
  })
}

// 查看流程实例
const viewInstance = (record: TaktInstance) => {
  // 这里可以跳转到详情页面或打开详情对话框
  message.info(`查看流程实例: ${record.instanceTitle}`)
}

// 跳转到我的流程
const goToMyWorkflow = () => {
  router.push('/workflow/my')
}

// 组件挂载时初始化
onMounted(async () => {
  await fetchStats()
  await fetchRecentInstances()
  
  await nextTick()
  initStatusChart()
  initPriorityChart()
})
</script>

<style scoped>
.workflow-overview-container {
  padding: 16px;
  background: #f0f2f5;
  min-height: 100vh;
}

.stats-cards {
  margin-bottom: 16px;
}

.stat-card {
  text-align: center;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.charts-section {
  margin-bottom: 16px;
}

.chart-card {
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.chart-container {
  height: 300px;
  width: 100%;
}

.recent-section {
  margin-bottom: 16px;
}

.recent-card {
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

:deep(.ant-card-head) {
  border-bottom: 1px solid #f0f0f0;
}

:deep(.ant-card-body) {
  padding: 16px;
}
</style>

