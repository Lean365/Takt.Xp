<template>
  <div class="oper-log">
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
      row-key="operLogId"
      :scroll="{ y: 400 }"
      @change="handleTableChange"
    >
      <!-- 自定义列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <a-tag :color="record.status === 0 ? 'success' : 'error'">
            {{ record.status === 0 ? '成功' : '失败' }}
          </a-tag>
        </template>
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </Takt-table>

    <!-- 详情组件 -->
    <oper-log-detail ref="detailRef" :oper-info="currentOper" />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktOperLogDto, TaktOperLogQueryDto } from '@/types/audit/operLog'
import { getOperLogList } from '@/api/audit/operLog'
import { useUserStore } from '@/stores/userStore'
import { useI18n } from 'vue-i18n'
import OperLogDetail from './OperLogDetail.vue'

const { t } = useI18n()
const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'tableName',
    label: '表名',
    type: 'input',
    placeholder: '请输入表名'
  },
  {
    name: 'operationType',
    label: '操作类型',
    type: 'input',
    placeholder: '请输入操作类型'
  },
  {
    name: 'status',
    label: '操作状态',
    type: 'select',
    placeholder: '请选择操作状态',
    options: [
      { label: '成功', value: 0 },
      { label: '失败', value: 1 }
    ]
  },
  {
    name: 'startTime',
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
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName',
    width: 150
  },
  {
    title: '操作类型',
    dataIndex: 'operationType',
    key: 'operationType',
    width: 120
  },
  {
    title: '请求方法',
    dataIndex: 'requestMethod',
    key: 'requestMethod',
    width: 100
  },
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130
  },
  {
    title: '操作地点',
    dataIndex: 'location',
    key: 'location',
    width: 150
  },
  {
    title: '操作状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '操作时间',
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
const tableData = ref<TaktOperLogDto[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentOper = ref<TaktOperLogDto>()

const queryParams = reactive<TaktOperLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  userName: userStore.userInfo?.userName,
  tableName: undefined,
  operationType: undefined,
  status: undefined,
  startTime: undefined,
  endTime: undefined
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getOperLogList(queryParams)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows.map((item: TaktOperLogDto) => ({
        ...item,
        errorMsg: item.errorMsg ?? ''
      }))
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取操作日志失败:', error)
    message.error('获取操作日志失败')
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
  queryParams.tableName = undefined
  queryParams.operationType = undefined
  queryParams.status = undefined
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
const handleViewDetail = (record: TaktOperLogDto) => {
  currentOper.value = record
  detailRef.value?.open()
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.oper-log {
  padding: 16px;
}
</style> 
