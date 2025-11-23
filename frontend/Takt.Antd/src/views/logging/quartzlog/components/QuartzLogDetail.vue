<template>
  <Takt-modal
    v-model:open="visible"
    title="任务日志详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions bordered :column="2">
      <a-descriptions-item label="任务名称" :span="2">
        {{ quartzInfo?.logTaskName }}
      </a-descriptions-item>
      <a-descriptions-item label="任务组">
        {{ quartzInfo?.logGroupName }}
      </a-descriptions-item>
      <a-descriptions-item label="执行机器">
        {{ quartzInfo?.logExecuteHost }}
      </a-descriptions-item>
      <a-descriptions-item label="执行状态">
        <a-tag :color="quartzInfo?.logStatus === 1 ? 'success' : 'error'">
          {{ quartzInfo?.logStatus === 1 ? '成功' : '失败' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="执行耗时">
        {{ quartzInfo?.logExecuteDuration }}ms
      </a-descriptions-item>
      <a-descriptions-item label="执行时间">
        {{ quartzInfo?.logExecuteTime }}
      </a-descriptions-item>
      <a-descriptions-item label="创建时间">
        {{ quartzInfo?.createTime }}
      </a-descriptions-item>
      <a-descriptions-item label="执行参数" :span="2">
        <pre>{{ quartzInfo?.logExecuteParams }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="错误信息" :span="2" v-if="quartzInfo?.logStatus === 0">
        <pre>{{ quartzInfo?.logErrorInfo }}</pre>
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps, defineExpose } from 'vue'
import type { TaktQuartzLogDto } from '@/types/audit/quartzLog'

const props = defineProps<{
  quartzInfo?: TaktQuartzLogDto
}>()

const visible = ref(false)

/** 打开弹窗 */
const open = () => {
  visible.value = true
}

/** 关闭弹窗 */
const close = () => {
  visible.value = false
}

// 暴露方法给父组件
defineExpose({
  open,
  close
})
</script>

<style lang="less" scoped>
:deep(.ant-descriptions-item-label) {
  width: 100px;
}

:deep(.ant-descriptions-item-content) {
  pre {
    margin: 0;
    white-space: pre-wrap;
    word-wrap: break-word;
    max-height: 300px;
    overflow-y: auto;
  }
}
</style> 
