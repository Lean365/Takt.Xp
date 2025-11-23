<template>
  <Takt-modal
    v-model:open="visible"
    title="我的数据差异日志"
    :width="1200"
    :footer="null"
  >
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-refresh="true"
      :show-search="true"
      @refresh="fetchData"
      @toggle-search="toggleSearch"
    />

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="'sqlDiffLogId'"
      :scroll="{ y: 400 }"
      @change="handleTableChange"
    >
      <!-- 自定义列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </Takt-table>

    <!-- 详情组件 -->
    <db-diff-log-detail ref="detailRef" :diff-info="currentDiff" />
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, defineExpose } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktSqlDiffLogDto, TaktSqlDiffLogQueryDto } from '@/types/audit/sqlDiffLog'
import { getSqlDiffLogList } from '@/api/audit/sqlDiffLog'
import { useUserStore } from '@/stores/userStore'
import DbDiffLogDetail from './SqlDiffLogDetail.vue'

const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  { name: 'diffType', label: '差异类型', type: 'input', placeholder: '请输入差异类型' },
  { name: 'tableName', label: '表名', type: 'input', placeholder: '请输入表名' },
  { name: 'businessName', label: '业务名称', type: 'input', placeholder: '请输入业务名称' },
  { name: 'primaryKey', label: '主键值', type: 'input', placeholder: '请输入主键值' },
  { name: 'startTime', label: '开始时间', type: 'date', placeholder: '请选择开始时间' },
  { name: 'endTime', label: '结束时间', type: 'date', placeholder: '请选择结束时间' }
]

// 表格列定义
const columns = [
  { title: '差异类型', dataIndex: 'diffType', key: 'diffType', width: 120 },
  { title: '表名', dataIndex: 'tableName', key: 'tableName', width: 150 },
  { title: '业务名称', dataIndex: 'businessName', key: 'businessName', width: 150 },
  { title: '主键值', dataIndex: 'primaryKey', key: 'primaryKey', width: 120 },
  { title: '变更前数据', dataIndex: 'beforeData', key: 'beforeData', width: 200, ellipsis: true },
  { title: '变更后数据', dataIndex: 'afterData', key: 'afterData', width: 200, ellipsis: true },
  { title: '差异字段', dataIndex: 'diffFields', key: 'diffFields', width: 200, ellipsis: true },
  { title: '执行SQL', dataIndex: 'executeSql', key: 'executeSql', width: 200, ellipsis: true },
  { title: 'SQL参数', dataIndex: 'sqlParameters', key: 'sqlParameters', width: 200, ellipsis: true },
  { title: '操作时间', dataIndex: 'createTime', key: 'createTime', width: 180 }
]

// 状态定义
const visible = ref(false)
const loading = ref(false)
const tableData = ref<TaktSqlDiffLogDto[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentDiff = ref<TaktSqlDiffLogDto>()

const queryParams = reactive<TaktSqlDiffLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  diffType: undefined,
  tableName: undefined,
  businessName: undefined,
  primaryKey: undefined,
  startTime: undefined,
  endTime: undefined
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getSqlDiffLogList(queryParams)
    tableData.value = res.data.data.rows
    total.value = res.data.data.totalNum
  } catch (error) {
    console.error('获取数据差异日志失败:', error)
    message.error('获取数据差异日志失败')
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.diffType = undefined
  queryParams.tableName = undefined
  queryParams.businessName = undefined
  queryParams.primaryKey = undefined
  queryParams.startTime = undefined
  queryParams.endTime = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 查看详情 */
const handleViewDetail = (record: TaktSqlDiffLogDto) => {
  currentDiff.value = record
  detailRef.value?.open()
}

/** 打开弹窗 */
const open = () => {
  visible.value = true
  fetchData()
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
:deep(.ant-modal-body) {
  padding: 16px;
  max-height: 800px;
  overflow-y: auto;
}
</style> 
