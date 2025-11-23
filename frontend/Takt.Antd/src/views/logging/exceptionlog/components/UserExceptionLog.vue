<template>
  <div class="exception-log">
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
      :row-key="'exceptionLogId'"
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
    <exception-log-detail ref="detailRef" :exception-info="currentException" />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktExceptionLogDto, TaktExceptionLogQueryDto } from '@/types/audit/exceptionLog'
import { getExceptionLogList } from '@/api/audit/exceptionLog'
import { useUserStore } from '@/stores/userStore'
import ExceptionLogDetail from './ExceptionLogDetail.vue'

const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  { name: 'userName', label: '操作人员', type: 'input', placeholder: '请输入操作人员' },
  { name: 'method', label: '请求方法', type: 'input', placeholder: '请输入请求方法' },
  { name: 'exceptionType', label: '异常类型', type: 'input', placeholder: '请输入异常类型' },
  { name: 'startTime', label: '开始时间', type: 'date', placeholder: '请选择开始时间' },
  { name: 'endTime', label: '结束时间', type: 'date', placeholder: '请选择结束时间' }
]

// 表格列定义
const columns = [
  { title: '日志级别', dataIndex: 'logLevel', key: 'logLevel', width: 100 },
  { title: '操作人员', dataIndex: 'userName', key: 'userName', width: 120 },
  { title: '请求方法', dataIndex: 'method', key: 'method', width: 150 },
  { title: '异常类型', dataIndex: 'exceptionType', key: 'exceptionType', width: 150 },
  { title: '异常消息', dataIndex: 'exceptionMessage', key: 'exceptionMessage', width: 200, ellipsis: true },
  { title: 'IP地址', dataIndex: 'ipAddress', key: 'ipAddress', width: 130 },
  { title: '用户代理', dataIndex: 'userAgent', key: 'userAgent', width: 200, ellipsis: true },
  { title: '操作时间', dataIndex: 'createTime', key: 'createTime', width: 180 },
  { title: '操作', key: 'action', width: 80, fixed: 'right' }
]

// 状态定义
const loading = ref(false)
const tableData = ref<TaktExceptionLogDto[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentException = ref<TaktExceptionLogDto>()

const queryParams = reactive<TaktExceptionLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  userName: userStore.userInfo?.userName,
  method: undefined,
  exceptionType: undefined,
  startTime: undefined,
  endTime: undefined
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getExceptionLogList(queryParams)
    tableData.value = res.data.data.rows
    total.value = res.data.data.totalNum
  } catch (error) {
    console.error('获取异常日志失败:', error)
    message.error('获取异常日志失败')
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
  queryParams.userName = userStore.userInfo?.userName
  queryParams.method = undefined
  queryParams.exceptionType = undefined
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
const handleViewDetail = (record: TaktExceptionLogDto) => {
  currentException.value = record
  detailRef.value?.open()
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.exception-log {
  padding: 16px;
}
</style>
