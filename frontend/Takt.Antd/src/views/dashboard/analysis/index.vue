<template>
  <div class="analysis-container">
    <a-card :bordered="false">
      <template #title>{{ t('dashboard.analysis.title') }}</template>
      <a-row :gutter="24" style="margin-bottom: 24px;">
        <a-col :span="8">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.analysis.userCount')" :value="12345" />
          </a-card>
        </a-col>
        <a-col :span="8">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.analysis.visitCount')" :value="67890" />
          </a-card>
        </a-col>
        <a-col :span="8">
          <a-card :bordered="false">
            <a-statistic :title="t('dashboard.analysis.activeCount')" :value="2345" />
          </a-card>
        </a-col>
      </a-row>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-card :bordered="false">
            <template #title>{{ t('dashboard.analysis.userTrend') }}</template>
            <div ref="chartRef" style="height:300px;" />
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

const chartRef = ref<HTMLDivElement | null>(null)

onMounted(() => {
  if (chartRef.value) {
    const chart = echarts.init(chartRef.value)
    chart.setOption({
      tooltip: { trigger: 'axis' },
      xAxis: {
        type: 'category',
        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
      },
      yAxis: { type: 'value' },
      series: [
        {
          name: t('dashboard.analysis.visits'),
          type: 'line',
          data: [120, 200, 150, 80, 70, 110, 130]
        }
      ]
    })
  }
})
</script>

<style lang="less" scoped>
.analysis-container {
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
}
</style> 