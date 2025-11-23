<template>
  <div class="login-log-container">
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
      :delete-permission="['audit:loginlog:delete']"
      :export-permission="['audit:loginlog:export']"
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
      :pagination="false"
      row-key="loginLogId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 登录状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <a-tag :color="record.status === '0' ? 'success' : 'error'">
            {{ record.status === '0' ? '成功' : '失败' }}
          </a-tag>
        </template>
        <template v-else-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-detail="true"
            :show-delete="true"
            :detail-permission="['audit:loginlog:detail']"
            :delete-permission="['audit:loginlog:delete']"
            size="small"
            @detail="handleViewDetail"
            @delete="handleDelete"
          />
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
    <!-- 登录详情对话框 -->
    <login-log-detail
      v-if="detailVisible"
      :login-info="currentLogin"
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
import { ref, reactive, onMounted } from 'vue'
import { message, Drawer, Checkbox, Button } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktLoginLogDto, TaktLoginLogQuery, TaktLoginLog } from '@/types/audit/loginLog'
import { getLoginLogList, exportLoginLog, clearLoginLog } from '@/api/audit/loginLog'
import { useI18n } from 'vue-i18n'
import LoginLogDetail from './components/LoginLogDetail.vue'

const { t } = useI18n()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: '用户名称',
    type: 'input',
    placeholder: '请输入用户名称'
  },
  {
    name: 'ipAddress',
    label: 'IP地址',
    type: 'input',
    placeholder: '请输入IP地址'
  },
  {
    name: 'loginSuccess',
    label: '登录状态',
    type: 'select',
    placeholder: '请选择登录状态',
    options: [
      { label: '成功', value: 1 },
      { label: '失败', value: 0 }
    ]
  },
  {
    name: 'loginType',
    label: '登录类型',
    type: 'select',
    placeholder: '请选择登录类型',
    options: [
      { label: '账号密码', value: 0 },
      { label: '手机验证码', value: 1 },
      { label: '邮箱验证码', value: 2 },
      { label: '第三方登录', value: 3 }
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
    title: '用户名称',
    dataIndex: 'userName',
    key: 'userName',
    width: 120
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
    title: '登录类型',
    dataIndex: 'loginType',
    key: 'loginType',
    width: 100
  },
  {
    title: '登录状态',
    dataIndex: 'loginSuccess',
    key: 'success',
    width: 100
  },
  {
    title: '登录来源',
    dataIndex: 'loginSource',
    key: 'loginSource',
    width: 100
  },
  {
    title: '消息',
    dataIndex: 'loginMessage',
    key: 'message',
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
const tableData = ref<TaktLoginLog[]>([])
const total = ref(0)
const queryParams = reactive<TaktLoginLogQuery>({
  pageIndex: 1,
  pageSize: 10,
  userName: undefined,
  ipAddress: undefined,
  loginSuccess: undefined,
  loginType: undefined,
  loginStatus: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const currentLogin = ref<TaktLoginLogDto>()
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

    const res = await getLoginLogList(queryParams)
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
  queryParams.ipAddress = undefined
  queryParams.loginSuccess = undefined
  queryParams.loginType = undefined
  queryParams.loginStatus = undefined
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

/** 导出登录日志 */
const handleExport = async () => {
  try {
    await exportLoginLog(queryParams)
    message.success('导出成功')
  } catch (error) {
    console.error('导出登录日志失败:', error)
    message.error('导出登录日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearLoginLog()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空登录日志失败:', error)
    message.error('清空登录日志失败')
  }
}

/** 查看详情 */
const handleViewDetail = (record: TaktLoginLogDto) => {
  currentLogin.value = record
  detailVisible.value = true
}

/** 处理删除 */
const handleDelete = async (record: TaktLoginLogDto) => {
  // 这里可根据实际API实现删除逻辑
  // 示例：await deleteLoginLog(record.loginLogId)
  // fetchData()
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
  localStorage.setItem('loginLogColumnSettings', JSON.stringify(settings))
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('loginLogColumnSettings')
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
.login-log-container {
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

