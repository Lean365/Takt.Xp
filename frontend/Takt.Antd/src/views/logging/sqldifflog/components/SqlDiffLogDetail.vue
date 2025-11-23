<template>
  <Takt-modal
    v-model:open="visible"
    title="数据差异详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions bordered :column="2">
      <a-descriptions-item label="差异类型">
        {{ diffInfo?.diffType }}
      </a-descriptions-item>
      <a-descriptions-item label="表名">
        {{ diffInfo?.tableName }}
      </a-descriptions-item>
      <a-descriptions-item label="业务名称">
        {{ diffInfo?.businessName }}
      </a-descriptions-item>
      <a-descriptions-item label="主键值">
        {{ diffInfo?.primaryKey }}
      </a-descriptions-item>
      <a-descriptions-item label="差异字段" :span="2">
        {{ diffInfo?.diffFields }}
      </a-descriptions-item>
      <a-descriptions-item label="执行SQL" :span="2">
        <pre>{{ diffInfo?.executeSql }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="SQL参数" :span="2">
        <pre>{{ diffInfo?.sqlParameters }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="变更前数据" :span="2">
        <pre>{{ diffInfo?.beforeData }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="变更后数据" :span="2">
        <pre>{{ diffInfo?.afterData }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="操作时间" :span="2">
        {{ diffInfo?.createTime }}
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps, defineExpose } from 'vue'
import type { TaktSqlDiffLogDto } from '@/types/audit/sqlDiffLog'

const props = defineProps<{
  diffInfo?: TaktSqlDiffLogDto
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
  width: 120px;
}

:deep(.ant-descriptions-item-content) {
  pre {
    margin: 0;
    white-space: pre-wrap;
    word-wrap: break-word;
    max-height: 300px;
    overflow-y: auto;
    background-color: #f5f5f5;
    padding: 8px;
    border-radius: 4px;
  }
}
</style> 
