<template>
  <div class="monitor-container">
    <a-card :bordered="false">
      <template #title>{{ t('dashboard.monitor.title') }}</template>
      <a-row :gutter="24" style="margin-bottom: 24px;">
        <a-col :span="6">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.monitor.cpu')" :value="cpuUsage + '%'" />
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.monitor.memory')" :value="memoryUsage + '%'" />
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.monitor.disk')" :value="diskUsage + '%'" />
          </a-card>
        </a-col>
        <a-col :span="6">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.monitor.onlineUsers')" :value="onlineUsers" />
          </a-card>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="12">
          <a-card :bordered="false">
            <template #title>{{ t('dashboard.monitor.cpuTrend') }}</template>
            <div ref="cpuChartRef" style="height:240px;" />
          </a-card>
        </a-col>
        <a-col :span="12">
          <a-card :bordered="false">
            <template #title>{{ t('dashboard.monitor.memoryTrend') }}</template>
            <div ref="memoryChartRef" style="height:240px;" />
          </a-card>
        </a-col>
      </a-row>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import * as echarts from 'echarts'
const { t } = useI18n()

const cpuUsage = ref(32)
const memoryUsage = ref(58)
const diskUsage = ref(71)
const onlineUsers = ref(102)

const cpuChartRef = ref<HTMLDivElement | null>(null)
const memoryChartRef = ref<HTMLDivElement | null>(null)

onMounted(() => {
  if (cpuChartRef.value) {
    const chart = echarts.init(cpuChartRef.value)
    chart.setOption({
      tooltip: { trigger: 'axis' },
      xAxis: { type: 'category', data: ['10:00','10:05','10:10','10:15','10:20','10:25','10:30'] },
      yAxis: { type: 'value', max: 100 },
      series: [{ name: t('dashboard.monitor.cpu'), type: 'line', data: [30, 32, 35, 40, 38, 36, 32] }]
    })
  }
  if (memoryChartRef.value) {
    const chart = echarts.init(memoryChartRef.value)
    chart.setOption({
      tooltip: { trigger: 'axis' },
      xAxis: { type: 'category', data: ['10:00','10:05','10:10','10:15','10:20','10:25','10:30'] },
      yAxis: { type: 'value', max: 100 },
      series: [{ name: t('dashboard.monitor.memory'), type: 'line', data: [55, 56, 57, 59, 60, 58, 58] }]
    })
  }
})
</script>

<style lang="less" scoped>
.monitor-container {
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
}
</style> 