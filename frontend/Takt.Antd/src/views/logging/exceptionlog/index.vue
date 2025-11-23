<template>
  <div class="exception-log-container">
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
      :show-export="true"
      :show-delete="true"
      :delete-permission="['audit:exceptionlog:delete']"
      :export-permission="['audit:exceptionlog:export']"
      :disabled-delete="selectedRowKeys.length === 0"
      @refresh="fetchData"
      @export="handleExport"
      @delete="handleBatchDelete"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns.filter((col: any) => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 1200, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="exceptionLogId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <!-- 异常类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'exceptionType'">
          <a-tag color="error">{{ record.exceptionType }}</a-tag>
        </template>
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </Takt-table>

    <!-- 分页组件 -->
    <Takt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 异常详情对话框 -->
    <exception-log-detail
      v-if="detailVisible"
      :exception-info="currentException"
      @close="detailVisible = false"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :visible="columnSettingVisible"
      title="列设置"
      placement="right"
      width="300"
      @close="columnSettingVisible = false"
    >
      <a-checkbox-group
        :value="Object.keys(columnSettings).filter(key => columnSettings[key])"
        @change="handleColumnSettingChange"
        class="column-setting-group"
      >
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, h } from 'vue'
import { message, Drawer, Checkbox, Button } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktExceptionLogDto, TaktExceptionLogQueryDto } from '@/types/audit/exceptionLog'
import {
  getExceptionLogList,
  exportExceptionLog,
  clearExceptionLog
} from '@/api/audit/exceptionLog'
import { useI18n } from 'vue-i18n'
import ExceptionLogDetail from './components/ExceptionLogDetail.vue'


const { t } = useI18n()

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
  {
    title: '日志级别',
    dataIndex: 'logLevel',
    key: 'logLevel',
    width: 100
  },
  {
    title: '操作人员',
    dataIndex: 'userName',
    key: 'userName',
    width: 120
  },
  {
    title: '请求方法',
    dataIndex: 'method',
    key: 'method',
    width: 150
  },
  {
    title: '异常类型',
    dataIndex: 'exceptionType',
    key: 'exceptionType',
    width: 150
  },
  {
    title: '异常消息',
    dataIndex: 'exceptionMessage',
    key: 'exceptionMessage',
    width: 200,
    ellipsis: true
  },
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130
  },
  {
    title: '用户代理',
    dataIndex: 'userAgent',
    key: 'userAgent',
    width: 200,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: t('identity.user.table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('identity.user.table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('identity.user.table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('identity.user.table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('identity.user.table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: t('identity.user.table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: t('common.table.header.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    ellipsis: true
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<TaktExceptionLogDto[]>([])
const total = ref(0)
const queryParams = reactive<TaktExceptionLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  userName: undefined,
  method: undefined,
  exceptionType: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentException = ref<TaktExceptionLogDto>()
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// ==================== CRUD 方法 ====================
/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams
    })

    const res = await getExceptionLogList(queryParams)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

/** 搜索按钮操作 */
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams, values)
  }
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.userName = undefined
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

/** 分页处理 */
const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.pageSize = size
  fetchData()
}

/** 导出异常日志 */
const handleExport = async () => {
  try {
    await exportExceptionLog(queryParams, '异常日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出异常日志失败:', error)
    message.error('导出异常日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearExceptionLog()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空异常日志失败:', error)
    message.error('清空异常日志失败')
  }
}

/** 查看异常详情 */
const handleViewDetail = (record: TaktExceptionLogDto) => {
  currentException.value = record
  detailVisible.value = true
}

// ==================== 其他功能 ====================
/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 切换全屏 */
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

/** 处理列设置 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnSettingChange = (checkedKeys: (string | number | boolean)[]) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedKeys.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('exceptionLogColumnSettings', JSON.stringify(settings))
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('exceptionLogColumnSettings')
  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))
  // 获取前6列（不包含操作列）
  const firstColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 6)
  // 设置前6列为true
  firstColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })
  // 确保操作列显示
  columnSettings.value['action'] = true
}

onMounted(() => {
  initColumnSettings()
})

// 初始化加载数据
fetchData()
</script>

<style lang="less" scoped>
.exception-log-container {
  padding: 16px;
  background-color: #fff;
  height: 100%;
  display: flex;
  flex-direction: column;

  .ant-table-wrapper {
    flex: 1;
  }
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  &:last-child {
    border-bottom: none;
  }
}
</style>

