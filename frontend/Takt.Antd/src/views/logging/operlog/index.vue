<template>
  <div class="oper-log-container">
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
      :show-export="true"
      :show-delete="true"
      :delete-permission="['audit:operlog:delete']"
      :export-permission="['audit:operlog:export']"
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
      row-key="operId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <!-- 操作状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <a-tag :color="record.status === 1 ? 'success' : 'error'">
            {{ record.status === 1 ? '成功' : '失败' }}
          </a-tag>
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

    <!-- 操作详情对话框 -->
    <oper-log-detail v-if="detailVisible" :oper-info="currentOper" @close="detailVisible = false" />

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
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktOperLogDto, TaktOperLogQueryDto } from '@/types/audit/operLog'
import { getOperLogList, exportOperLog, clearOperLog } from '@/api/audit/operLog'
import { ref, reactive, onMounted, h } from 'vue'
import OperLogDetail from './components/OperLogDetail.vue'


const { t } = useI18n()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: '操作人员',
    type: 'input',
    placeholder: '请输入操作人员'
  },
  {
    name: 'module',
    label: '所属模块',
    type: 'input',
    placeholder: '请输入所属模块'
  },
  {
    name: 'operation',
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
      { label: '成功', value: 1 },
      { label: '失败', value: 0 }
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
    title: '操作模块',
    dataIndex: 'module',
    key: 'module',
    width: 120,
    ellipsis: true
  },
  {
    title: '操作类型',
    dataIndex: 'operation',
    key: 'operation',
    width: 120,
    ellipsis: true
  },
  {
    title: '操作人员',
    dataIndex: 'userName',
    key: 'userName',
    width: 120
  },
  {
    title: '操作地址',
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
    title: '耗时(毫秒)',
    dataIndex: 'elapsed',
    key: 'elapsed',
    width: 100
  },
  {
    title: '请求方法',
    dataIndex: 'method',
    key: 'method',
    width: 150,
    ellipsis: true
  },
  {
    title: '请求参数',
    dataIndex: 'parameters',
    key: 'parameters',
    width: 200,
    ellipsis: true
  },
  {
    title: '返回结果',
    dataIndex: 'result',
    key: 'result',
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
const tableData = ref<TaktOperLogDto[]>([])
const total = ref(0)
const queryParams = reactive<TaktOperLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  userName: undefined,
  tableName: undefined,
  operationType: undefined,
  status: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentOper = ref<TaktOperLogDto>()
const visibleColumns = ref(columns.map(col => col.key))
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const columnSettingVisible = ref(false)

/** 处理列设置 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnSettingChange = (checkedKeys: (string | number | boolean)[]) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedKeys.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('operLogColumnSettings', JSON.stringify(settings))
}

const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('operLogColumnSettings')
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

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams
    })

    const res = await getOperLogList(queryParams)
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
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.userName = undefined
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

/** 分页处理 */
const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.pageSize = size
  fetchData()
}

/** 导出操作日志 */
const handleExport = async () => {
  try {
    await exportOperLog(queryParams)
    message.success('导出成功')
  } catch (error) {
    console.error('导出操作日志失败:', error)
    message.error('导出操作日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearOperLog()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空操作日志失败:', error)
    message.error('清空操作日志失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}
// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}
/** 查看操作详情 */
const handleViewDetail = (record: TaktOperLogDto) => {
  currentOper.value = record
  detailVisible.value = true
}

// 初始化加载数据
fetchData()
</script>

<style lang="less" scoped>
.oper-log-container {
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

