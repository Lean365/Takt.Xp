<template>
  <div class="quartz-log">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :model="queryParams"
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
      row-key="quartzLogId"
      :height="540"
      @change="handleTableChange"
    >
      <!-- 自定义列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'logStatus'">
          <a-tag :color="record.logStatus === 1 ? 'success' : 'error'">
            {{ record.logStatus === 1 ? '成功' : '失败' }}
          </a-tag>
        </template>
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </Takt-table>

    <!-- 详情组件 -->
    <quartz-log-detail ref="detailRef" :quartz-info="currentQuartz" />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktQuartzLogDto, TaktQuartzLogQueryDto } from '@/types/audit/quartzLog'
import { getQuartzLogList } from '@/api/audit/quartzLog'
import { useUserStore } from '@/stores/userStore'
import { useI18n } from 'vue-i18n'
import QuartzLogDetail from './QuartzLogDetail.vue'

const { t } = useI18n()
const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'logTaskName',
    label: '任务名称',
    type: 'input',
    placeholder: '请输入任务名称'
  },
  {
    name: 'logGroupName',
    label: '任务组',
    type: 'input',
    placeholder: '请输入任务组'
  },
  {
    name: 'logStatus',
    label: '执行状态',
    type: 'select',
    placeholder: '请选择执行状态',
    options: [
      { label: '成功', value: 1 },
      { label: '失败', value: 0 }
    ]
  },
  {
    name: 'beginTime',
    label: '开始时间',
    type: 'date',
    placeholder: '请选择开始时间'
  },
  {
    name: 'endTime',
    label: '结束时间',
    type: 'date',
    placeholder: '请选择结束时间'
  }
]

// 表格列定义
const columns = [
  {
    title: '任务名称',
    dataIndex: 'logTaskName',
    key: 'logTaskName',
    width: 150
  },
  {
    title: '任务组',
    dataIndex: 'logGroupName',
    key: 'logGroupName',
    width: 120
  },
  {
    title: '调用目标',
    dataIndex: 'logExecuteParams',
    key: 'logExecuteParams',
    width: 200,
    ellipsis: true
  },
  {
    title: '执行状态',
    dataIndex: 'logStatus',
    key: 'logStatus',
    width: 100
  },
  {
    title: '执行耗时',
    dataIndex: 'logExecuteDuration',
    key: 'logExecuteDuration',
    width: 100,
    customRender: ({ text }: { text: number }) => `${text}ms`
  },
  {
    title: '执行时间',
    dataIndex: 'logExecuteTime',
    key: 'logExecuteTime',
    width: 180
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 80,
    fixed: 'right'
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<TaktQuartzLogDto[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentQuartz = ref<TaktQuartzLogDto>()

const queryParams = reactive<TaktQuartzLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  logTaskName: undefined,
  logGroupName: undefined,
  logStatus: undefined,
  beginTime: undefined,
  endTime: undefined
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getQuartzLogList(queryParams)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取任务日志失败:', error)
    message.error('获取任务日志失败')
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
  queryParams.logTaskName = undefined
  queryParams.logGroupName = undefined
  queryParams.logStatus = undefined
  queryParams.beginTime = undefined
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
const handleViewDetail = (record: TaktQuartzLogDto) => {
  currentQuartz.value = record
  detailRef.value?.open()
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.quartz-log {
  padding: 16px;
  height: 100%;
  
  :deep(.Takt-table-container) {
    height: 540px;
  }
}
</style> 
