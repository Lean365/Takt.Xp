<template>
  <div class="Takt-server-monitor">
   
    <a-card :bordered="false" class="monitor-card">
      <template #title>
        <div class="card-title">
          <span>{{ t('audit.server.title') }}</span>
          <a-button type="primary" @click="refreshData" class="refresh-btn">
            <template #icon><ReloadOutlined /></template>
            {{ t('audit.server.refresh') }}
          </a-button>
        </div>
      </template>

      <a-tabs v-model:activeKey="activeTab">
        <!-- 资源使用 -->
        <a-tab-pane :key="'resource'" :tab="t('audit.server.resource.title')">
          <div class="tab-content">
            <a-row :gutter="16">
              <!-- CPU使用率 -->
              <a-col :span="8">
                <div class="resource-usage-item">
                  <div class="usage-header">
                    <span class="usage-name">{{ t('audit.server.resource.cpu') }}</span>
                  </div>
                  <a-progress
                    type="dashboard"
                    :percent="Number((serverInfo?.cpuUsage || 0).toFixed(1))"
                    :stroke-color="getUsageColor(serverInfo?.cpuUsage || 0)"
                    :format="percent => `${percent}%`"
                  />
                </div>
              </a-col>
              
              <!-- 内存使用率 -->
              <a-col :span="8">
                <div class="resource-usage-item">
                  <div class="usage-header">
                    <span class="usage-name">{{ t('audit.server.resource.memory') }}</span>
                  </div>
                  <a-progress
                    type="dashboard"
                    :percent="Number((serverInfo?.memoryUsage || 0).toFixed(1))"
                    :stroke-color="getUsageColor(serverInfo?.memoryUsage || 0)"
                    :format="percent => `${percent}%`"
                  />
                  <div class="usage-detail">
                    {{ formatSize(serverInfo?.usedMemory || 0) }} / {{ formatSize(serverInfo?.totalMemory || 0) }}
                  </div>
                </div>
              </a-col>

              <!-- 磁盘使用情况 -->
              <a-col :span="8" v-for="disk in (serverInfo?.diskInfos || [])" :key="disk.driveName">
                <div class="resource-usage-item">
                  <div class="usage-header">
                    <span class="usage-name">{{ disk.driveName }}</span>
                  </div>
                  <a-progress
                    type="dashboard"
                    :percent="Number(((disk.usedSpace || 0) / (disk.totalSpace || 1) * 100).toFixed(1))"
                    :stroke-color="getUsageColor((disk.usedSpace || 0) / (disk.totalSpace || 1) * 100)"
                    :format="percent => `${percent}%`"
                  />
                  <div class="usage-detail">
                    {{ formatSize(disk.usedSpace || 0) }} / {{ formatSize(disk.totalSpace || 0) }}
                  </div>
                </div>
              </a-col>
            </a-row>
          </div>
        </a-tab-pane>

        <!-- 系统信息 -->
        <a-tab-pane :key="'system'" :tab="t('audit.server.system.title')">
          <div class="tab-content">
            <a-descriptions :column="1">
              <a-descriptions-item :label="t('audit.server.system.os')">
                {{ serverInfo?.osName || '-' }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.system.architecture')">
                {{ serverInfo?.osArchitecture || '-' }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.system.version')">
                {{ serverInfo?.osVersion || '-' }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.system.processor.name')">
                {{ serverInfo?.processorName || '-' }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.system.processor.count')">
                {{ serverInfo?.processorCount || 0 }} {{ t('audit.server.system.processor.unit') }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.system.startup.time')">
                {{ serverInfo?.systemStartTime ? formatDateTime(serverInfo.systemStartTime) : '-' }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.system.startup.uptime')">
                {{ formatUptime(serverInfo?.systemUptime || 0) }}
              </a-descriptions-item>
            </a-descriptions>
          </div>
        </a-tab-pane>

        <!-- .NET运行时信息 -->
        <a-tab-pane :key="'dotnet'" :tab="t('audit.server.dotnet.title')">
          <div class="tab-content">
            <a-descriptions :column="1">
              <a-descriptions-item :label="t('audit.server.dotnet.runtime.version')">
                <span class="version-text">.NET</span>
                <span class="version-number">{{ (serverInfo?.dotNetRuntimeVersion || '').replace('.NET ', '') }}</span>
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.dotnet.clr.version')">
                {{ serverInfo?.clrVersion || '-' }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('audit.server.dotnet.runtime.directory')" :span="2">
                <a-tooltip :title="serverInfo?.dotNetRuntimeDirectory || ''">
                  <span class="ellipsis-text">{{ serverInfo?.dotNetRuntimeDirectory || '-' }}</span>
                </a-tooltip>
              </a-descriptions-item>
            </a-descriptions>
          </div>
        </a-tab-pane>

        <!-- 网络信息 -->
        <a-tab-pane :key="'network'" :tab="t('audit.server.network.title')">
          <div class="tab-content">
            <a-table
              :columns="networkColumns"
              :data-source="networkInfo"
              :pagination="false"
              :loading="loading"
            >
              <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'sendRate'">
                  {{ formatSpeed(record.sendRate) }}
                </template>
                <template v-if="column.key === 'receiveRate'">
                  {{ formatSpeed(record.receiveRate) }}
                </template>
              </template>
            </a-table>
          </div>
        </a-tab-pane>
      </a-tabs>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { ReloadOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import type { TaktServerMonitorDto, TaktNetworkDto } from '@/types/realtime/serverMonitor'
import type { TaktApiResponse } from '@/types/common'
import type { AxiosResponse } from 'axios'
import { getServerInfo, getNetworkInfo } from '@/api/audit/serverMonitor'
import { formatDateTime } from '@/utils/format'
import * as echarts from 'echarts'

// 获取国际化函数
const { t } = useI18n()

// 图表引用
const cpuChartRef = ref<HTMLElement>()
const memoryChartRef = ref<HTMLElement>()

// 图表实例
let cpuChart: echarts.ECharts | null = null
let memoryChart: echarts.ECharts | null = null

// 数据
const serverInfo = ref<TaktServerMonitorDto>({
  cpuUsage: 0,
  totalMemory: 0,
  usedMemory: 0,
  memoryUsage: 0,
  diskInfos: [],
  osName: '',
  osArchitecture: '',
  osVersion: '',
  processorName: '',
  processorCount: 0,
  systemStartTime: new Date(),
  systemUptime: 0,
  dotNetRuntimeVersion: '',
  clrVersion: '',
  dotNetRuntimeDirectory: ''
})

const networkInfo = ref<TaktNetworkDto[]>([])
const loading = ref(false)

// 添加当前激活的标签页
const activeTab = ref('resource')

// 初始化图表
const initCharts = () => {
  if (cpuChartRef.value) {
    cpuChart = echarts.init(cpuChartRef.value)
  }
  if (memoryChartRef.value) {
    memoryChart = echarts.init(memoryChartRef.value)
  }
}

// 获取使用率颜色
const getUsageColor = (usage: number) => {
  if (usage >= 90) return '#ff4d4f'
  if (usage >= 70) return '#faad14'
  return '#52c41a'
}

// 获取使用率状态
const getUsageStatus = (usage: number): 'normal' | 'exception' | 'success' | 'active' => {
  if (usage >= 90) return 'exception'
  if (usage >= 70) return 'normal'
  return 'success'
}

// 更新图表数据
const updateCharts = () => {
  // CPU使用率图表
  cpuChart?.setOption({
    title: {
      text: 'CPU使用率',
      left: 'center'
    },
    tooltip: {
      formatter: '{b}: {c}%'
    },
    series: [
      {
        type: 'gauge',
        startAngle: 180,
        endAngle: 0,
        min: 0,
        max: 100,
        splitNumber: 10,
        itemStyle: {
          color: getUsageColor(serverInfo.value.cpuUsage)
        },
        progress: {
          show: true,
          roundCap: true,
          width: 18
        },
        pointer: {
          show: false
        },
        axisLine: {
          roundCap: true,
          lineStyle: {
            width: 18
          }
        },
        axisTick: {
          show: false
        },
        splitLine: {
          show: false
        },
        axisLabel: {
          show: false
        },
        title: {
          fontSize: 14
        },
        detail: {
          width: 50,
          height: 14,
          fontSize: 14,
          color: 'inherit',
          formatter: '{value}%'
        },
        data: [{
          value: serverInfo.value.cpuUsage,
          name: 'CPU'
        }]
      }
    ]
  })

  // 内存使用率图表
  memoryChart?.setOption({
    title: {
      text: '内存使用率',
      subtext: `${formatSize(serverInfo.value.usedMemory)}/${formatSize(serverInfo.value.totalMemory)}`,
      left: 'center'
    },
    tooltip: {
      trigger: 'item',
      formatter: '{b}: {c} ({d}%)'
    },
    series: [
      {
        type: 'pie',
        radius: ['60%', '80%'],
        avoidLabelOverlap: false,
        itemStyle: {
          borderRadius: 10,
          borderColor: '#fff',
          borderWidth: 2
        },
        label: {
          show: false
        },
        emphasis: {
          label: {
            show: false
          }
        },
        data: [
          { 
            value: serverInfo.value.usedMemory,
            name: '已用内存',
            itemStyle: { color: getUsageColor(serverInfo.value.memoryUsage) }
          },
          { 
            value: serverInfo.value.totalMemory - serverInfo.value.usedMemory,
            name: '可用内存',
            itemStyle: { color: '#E1E1E1' }
          }
        ]
      }
    ]
  })
}

// 表格列定义
const networkColumns = [
  {
    title: t('audit.server.network.adapter'),
    dataIndex: 'adapterName',
    key: 'adapterName'
  },
  {
    title: t('audit.server.network.mac'),
    dataIndex: 'macAddress',
    key: 'macAddress'
  },
  {
    title: t('audit.server.network.ip.address'),
    dataIndex: 'ipAddress',
    key: 'ipAddress'
  },
  {
    title: t('audit.server.network.ip.location'),
    dataIndex: 'ipLocation',
    key: 'ipLocation'
  },
  {
    title: t('audit.server.network.rate.send'),
    dataIndex: 'sendRate',
    key: 'sendRate'
  },
  {
    title: t('audit.server.network.rate.receive'),
    dataIndex: 'receiveRate',
    key: 'receiveRate'
  }
]

// 格式化运行时间
const formatUptime = (days: number) => {
  if (!days) return '0天'
  const totalHours = days * 24
  const d = Math.floor(days)
  const h = Math.floor(totalHours % 24)
  return d > 0 ? `${d}天${h}小时` : `${h}小时`
}

// 格式化大小
const formatSize = (bytes: number) => {
  if (!bytes) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB', 'TB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return `${(bytes / Math.pow(k, i)).toFixed(2)} ${sizes[i]}`
}

// 格式化速率
const formatSpeed = (kbps: number) => {
  if (kbps < 1024) {
    return `${kbps.toFixed(2)} KB/s`
  } else {
    return `${(kbps / 1024).toFixed(2)} MB/s`
  }
}

// 刷新数据
const refreshData = async () => {
  try {
    loading.value = true
    const [serverRes, networkRes] = await Promise.all([
      getServerInfo(),
      getNetworkInfo()
    ])
    
    if (serverRes.data.code === 200 && serverRes.data.data) {
      serverInfo.value = {
        ...serverInfo.value,
        ...serverRes.data.data
      }
    }
    if (networkRes.data.code === 200 && networkRes.data.data) {
      networkInfo.value = networkRes.data.data
    }
    updateCharts()
    message.success(t('audit.server.refreshResult.success'))
  } catch (error) {
    message.error(t('audit.server.refreshResult.failed'))
  } finally {
    loading.value = false
  }
}

// 页面加载时初始化
onMounted(() => {
  initCharts()
  refreshData()

  // 监听窗口大小变化，重绘图表
  window.addEventListener('resize', () => {
    cpuChart?.resize()
    memoryChart?.resize()
  })
})

// 页面卸载时销毁图表
onUnmounted(() => {
  cpuChart?.dispose()
  memoryChart?.dispose()
  window.removeEventListener('resize', () => {
    cpuChart?.resize()
    memoryChart?.resize()
  })
})
</script>

<style lang="less" scoped>
.Takt-server-monitor {
  height: 100%;
  
  .monitor-card {
    height: 100%;
    display: flex;
    flex-direction: column;

    :deep(.ant-card-body) {
      flex: 1;
      overflow: hidden;
      padding: 0;
      
      .ant-tabs {
        height: 100%;
        display: flex;
        flex-direction: column;

        .ant-tabs-content {
          flex: 1;
          overflow: auto;
        }
      }
    }
  }

  .tab-content {
    padding: 24px;
  }

  .card-title {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
  }

  .refresh-btn {
    margin-left: auto;
  }

  .resource-usage-item {
    text-align: center;
    padding: 16px;
    background-color: #fafafa;
    border-radius: 4px;
    margin-bottom: 16px;
    
    .usage-header {
      margin-bottom: 16px;
      
      .usage-name {
        font-size: 16px;
        font-weight: 500;
        color: rgba(0, 0, 0, 0.85);
      }
    }

    .usage-detail {
      margin-top: 8px;
      color: rgba(0, 0, 0, 0.45);
      font-size: 12px;
    }

    :deep(.ant-progress) {
      margin: 0 auto;
    }
  }

  .ellipsis-text {
    display: inline-block;
    max-width: 100%;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  .version-text {
    color: rgba(0, 0, 0, 0.45);
    margin-right: 4px;
  }

  .version-number {
    color: #1890ff;
    font-weight: 500;
    background-color: #e6f7ff;
    padding: 2px 8px;
    border-radius: 4px;
    border: 1px solid #91d5ff;
  }
}
</style>
