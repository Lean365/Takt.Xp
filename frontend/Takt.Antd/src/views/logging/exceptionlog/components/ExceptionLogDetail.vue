<template>
  <Takt-modal
    v-model:open="visible"
    title="异常详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions bordered :column="2">
      <a-descriptions-item label="日志级别">{{ exceptionInfo?.logLevel }}</a-descriptions-item>
      <a-descriptions-item label="操作人员">{{ exceptionInfo?.userName }}</a-descriptions-item>
      <a-descriptions-item label="异常类型">{{ exceptionInfo?.exceptionType }}</a-descriptions-item>
      <a-descriptions-item label="请求方法">{{ exceptionInfo?.method }}</a-descriptions-item>
      <a-descriptions-item label="参数" :span="2"><pre>{{ exceptionInfo?.parameters }}</pre></a-descriptions-item>
      <a-descriptions-item label="异常消息" :span="2"><pre>{{ exceptionInfo?.exceptionMessage }}</pre></a-descriptions-item>
      <a-descriptions-item label="堆栈跟踪" :span="2"><pre>{{ exceptionInfo?.stackTrace }}</pre></a-descriptions-item>
      <a-descriptions-item label="IP地址">{{ exceptionInfo?.ipAddress }}</a-descriptions-item>
      <a-descriptions-item label="用户代理">{{ exceptionInfo?.userAgent }}</a-descriptions-item>
      <a-descriptions-item label="操作时间">{{ exceptionInfo?.createTime }}</a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps, defineExpose } from 'vue'
import type { TaktExceptionLogDto } from '@/types/audit/exceptionLog'

const props = defineProps<{
  exceptionInfo?: TaktExceptionLogDto
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
