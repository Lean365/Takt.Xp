<template>
  <a-modal
    :open="visible"
    :title="t('generator.table.import.column.title')"
    width="1000px"
    :confirm-loading="loading"
    @cancel="handleCancel"
    :footer="null"
  >
    <Takt-table
      :columns="columns"
      :data-source="columnList"
      :loading="loading"
      :scroll="{ y: 400 }"
      :pagination="false"
    >
      <!-- 数据类型列 -->
      <template #dataType="{ record }">
        <a-tag>{{ record.dataType }}</a-tag>
      </template>
      <!-- 是否主键列 -->
      <template #isPrimaryKey="{ record }">
        <a-tag v-if="record.isPrimaryKey" color="blue">{{ t('common.yes') }}</a-tag>
        <a-tag v-else color="default">{{ t('common.no') }}</a-tag>
      </template>
      <!-- 是否可空列 -->
      <template #isNullable="{ record }">
        <a-tag v-if="record.isNullable" color="orange">{{ t('common.yes') }}</a-tag>
        <a-tag v-else color="green">{{ t('common.no') }}</a-tag>
      </template>
    </Takt-table>
    <!-- 分页组件 -->
    <Takt-pagination
      v-model:current="pagination.current"
      v-model:page-size="pagination.pageSize"
      :total="pagination.total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      @change="handlePageChange"
      @show-size-change="handleSizeChange"
    />
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch, reactive } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import type { TaktGenTable } from '@/types/generator/genTable'
import type { TaktGenColumn } from '@/types/generator/genColumn'
import { getTableColumnsByDb } from '@/api/generator/genTable'


const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  databaseName: string
  table: TaktGenTable | null
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
}>()

// 表格列定义
const columns = [
  { title: t('generator.table.import.column.tableName'), dataIndex: 'tableName', key: 'tableName', width: 120 },
  { title: t('generator.table.import.column.tableId'), dataIndex: 'tableId', key: 'tableId', width: 80 },
  { title: t('generator.table.import.column.columnName'), dataIndex: 'dbColumnName', key: 'dbColumnName', width: 150 },
  { title: t('generator.table.import.column.propertyName'), dataIndex: 'propertyName', key: 'propertyName', width: 120 },
  { title: t('generator.table.import.column.dataType'), dataIndex: 'dataType', key: 'dataType', width: 120 },
  { title: t('generator.table.import.column.oracleDataType'), dataIndex: 'oracleDataType', key: 'oracleDataType', width: 120 },
  { title: t('generator.table.import.column.propertyType'), dataIndex: 'propertyType', key: 'propertyType', width: 120 },
  { title: t('generator.table.import.column.length'), dataIndex: 'length', key: 'length', width: 80 },
  { title: t('generator.table.import.column.columnDescription'), dataIndex: 'columnDescription', key: 'columnDescription', width: 180 },
  { title: t('generator.table.import.column.defaultValue'), dataIndex: 'defaultValue', key: 'defaultValue', width: 120 },
  { title: t('generator.table.import.column.isNullable'), dataIndex: 'isNullable', key: 'isNullable', width: 80 },
  { title: t('generator.table.import.column.isIdentity'), dataIndex: 'isIdentity', key: 'isIdentity', width: 80 },
  { title: t('generator.table.import.column.isPrimarykey'), dataIndex: 'isPrimarykey', key: 'isPrimarykey', width: 80 },
  { title: t('generator.table.import.column.value'), dataIndex: 'value', key: 'value', width: 100 },
  { title: t('generator.table.import.column.decimalDigits'), dataIndex: 'decimalDigits', key: 'decimalDigits', width: 100 },
  { title: t('generator.table.import.column.scale'), dataIndex: 'scale', key: 'scale', width: 80 },
  { title: t('generator.table.import.column.isArray'), dataIndex: 'isArray', key: 'isArray', width: 80 },
  { title: t('generator.table.import.column.isJson'), dataIndex: 'isJson', key: 'isJson', width: 80 },
  { title: t('generator.table.import.column.isUnsigned'), dataIndex: 'isUnsigned', key: 'isUnsigned', width: 80 },
  { title: t('generator.table.import.column.createTableFieldSort'), dataIndex: 'createTableFieldSort', key: 'createTableFieldSort', width: 120 },
  { title: t('generator.table.import.column.insertServerTime'), dataIndex: 'insertServerTime', key: 'insertServerTime', width: 120 },
  { title: t('generator.table.import.column.insertSql'), dataIndex: 'insertSql', key: 'insertSql', width: 120 },
  { title: t('generator.table.import.column.updateServerTime'), dataIndex: 'updateServerTime', key: 'updateServerTime', width: 120 },
  { title: t('generator.table.import.column.updateSql'), dataIndex: 'updateSql', key: 'updateSql', width: 120 },
  { title: t('generator.table.import.column.sqlParameterDbType'), dataIndex: 'sqlParameterDbType', key: 'sqlParameterDbType', width: 120 },
]

// 列信息列表
const columnList = ref<any[]>([])
// 加载状态
const loading = ref(false)

// 分页配置
const pagination = reactive({
  current: 1,
  pageSize: 10,
  total: 0
})

// 监听弹窗显示状态
watch(
  () => props.visible,
  async (newVal) => {
    if (newVal && props.databaseName && props.table) {
      await fetchColumnList()
    } else {
      // 关闭弹窗时重置状态
      handleCancel()
    }
  }
)

/** 获取表列信息 */
const fetchColumnList = async () => {
  if (!props.databaseName || !props.table) {
    message.warning(t('generator.table.message.selectDatabase'))
    return
  }

  loading.value = true
  try {
    console.log('开始获取表列信息，数据库:', props.databaseName, '表:', props.table.tableName)
    const res = await getTableColumnsByDb(props.databaseName, props.table.tableName)
    if (res.data) {
      // 更新总数
      pagination.total = res.data.length
      
      // 计算分页数据
      const start = (pagination.current - 1) * pagination.pageSize
      const end = start + pagination.pageSize
      columnList.value = res.data.slice(start, end)
    } else {
      columnList.value = []
      pagination.total = 0
      message.warning(t('generator.table.message.noColumnInfo'))
    }
  } catch (error) {
    console.error('获取表列信息失败:', error)
    message.error(t('generator.table.message.getColumnInfoFailed'))
    columnList.value = []
    pagination.total = 0
  } finally {
    loading.value = false
  }
}

/** 页码改变事件 */
const handlePageChange = (page: number) => {
  pagination.current = page
  fetchColumnList()
}

/** 每页条数改变事件 */
const handleSizeChange = (current: number, size: number) => {
  pagination.current = current
  pagination.pageSize = size
  fetchColumnList()
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:visible', false)
  // 重置状态
  columnList.value = []
  loading.value = false
  pagination.current = 1
  pagination.pageSize = 10
  pagination.total = 0
}
</script>

<style lang="less" scoped>
:deep(.ant-tag) {
  margin: 0;
}
</style> 
