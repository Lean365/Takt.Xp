<template>
  <div class="db-diff-log-container">
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
      :delete-permission="['audit:sqldifflog:delete']"
      :export-permission="['audit:sqldifflog:export']"
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
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      row-key="diffLogId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 变更类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'changeType'">
          <a-tag :color="getChangeTypeColor(record.changeType)">
            {{ record.changeType }}
          </a-tag>
        </template>
      </template>
    </Takt-table>

    <!-- 差异详情对话框 -->
    <user-sql-diff-log v-model:open="detailVisible" :diff-log="currentDiff" />

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
        <div v-for="col in columns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktSqlDiffLogDto, TaktSqlDiffLogQueryDto } from '@/types/audit/sqlDiffLog'
import { getSqlDiffLogList, exportSqllDiffLog, clearSqlDiffLog } from '@/api/audit/sqlDiffLog'
import { useI18n } from 'vue-i18n'
import UserSqlDiffLog from './components/UserSqlDiffLog.vue'

const { t } = useI18n()

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
  {
    title: 'SQL参数',
    dataIndex: 'sqlParameters',
    key: 'sqlParameters',
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

const visibleColumns = ref(columns.map(col => col.key))

// 状态定义
const loading = ref(false)
const tableData = ref<TaktSqlDiffLogDto[]>([])
const total = ref(0)
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
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentDiff = ref<TaktSqlDiffLogDto | null>(null)
const columnSettings = ref<Record<string, boolean>>({})
const columnSettingVisible = ref(false)

/** 获取变更类型颜色 */
const getChangeTypeColor = (type: string) => {
  const colors: { [key: string]: string } = {
    CREATE_TABLE: 'green',
    DROP_TABLE: 'red',
    ADD_COLUMN: 'blue',
    MODIFY_COLUMN: 'orange',
    DROP_COLUMN: 'red'
  }
  return colors[type] || 'default'
}

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getSqlDiffLogList(queryParams)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取数据库差异日志失败:', error)
    message.error('获取数据库差异日志失败')
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

/** 导出数据库差异日志 */
const handleExport = async () => {
  try {
    await exportSqllDiffLog(queryParams, '数据库差异日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出数据库差异日志失败:', error)
    message.error('导出数据库差异日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearSqlDiffLog()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空数据库差异日志失败:', error)
    message.error('清空数据库差异日志失败')
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
/** 查看详情 */
const handleViewDetail = (record: TaktSqlDiffLogDto) => {
  currentDiff.value = record
  detailVisible.value = true
}

/** 处理列设置 */
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnSettingChange = (checkedKeys: (string | number | boolean)[]) => {
  const settings: Record<string, boolean> = {}
  columns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedKeys.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('sqlDiffLogColumnSettings', JSON.stringify(settings))
}

const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('sqlDiffLogColumnSettings')
  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(columns.map(col => [col.key, false]))
  // 获取前6列（不包含操作列）
  const firstColumns = columns.filter(col => col.key !== 'action').slice(0, 6)
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
</script>

<style lang="less" scoped>
.sql-diff-log-container {
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

