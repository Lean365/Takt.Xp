//===================================================================
// 项目名 : Lean.Takt
// 文件名称: OperLogDetail.vue
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 操作日志详情组件
//===================================================================

<template>
  <Takt-modal
    v-model:visible="visible"
    title="操作日志详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item label="操作模块" :span="2">
        {{ operInfo?.tableName }}
      </a-descriptions-item>
      <a-descriptions-item label="请求方式">
        {{ operInfo?.requestMethod }}
      </a-descriptions-item>
      <a-descriptions-item label="操作类型">
        {{ operInfo?.operationType }}
      </a-descriptions-item>
      <a-descriptions-item label="操作人员">
        {{ operInfo?.userName }}
      </a-descriptions-item>
      <a-descriptions-item label="操作时间">
        {{ operInfo?.createTime }}
      </a-descriptions-item>
      <a-descriptions-item label="请求地址" :span="2">
        {{ operInfo?.requestMethod }}
      </a-descriptions-item>
      <a-descriptions-item label="操作地点" :span="2">
        {{ operInfo?.location }}
      </a-descriptions-item>
      <a-descriptions-item label="请求参数" :span="2">
        {{ operInfo?.requestParam }}
      </a-descriptions-item>
      <a-descriptions-item label="业务主键" :span="2">
        {{ operInfo?.businessKey }}
      </a-descriptions-item>
      <a-descriptions-item label="操作状态">
        <a-tag :color="operInfo?.status === 0 ? 'success' : 'error'">
          {{ operInfo?.status === 0 ? '正常' : '异常' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="日志级别">
        {{ operInfo?.logLevel }}
      </a-descriptions-item>
      <a-descriptions-item v-if="operInfo?.errorMsg" label="错误消息" :span="2">
        {{ operInfo?.errorMsg }}
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps } from 'vue'
import type { TaktOperLogDto } from '@/types/audit/operLog'

defineProps<{
  operInfo?: TaktOperLogDto
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

defineExpose({
  open,
  close
})
</script>

<style lang="less" scoped>
:deep(.ant-descriptions-item-label) {
  width: 100px;
  text-align: right;
  background-color: #fafafa;
}
</style> 
