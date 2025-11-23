<template>
  <div class="device-log-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="false"
      :show-edit="false"
      :show-delete="true"
      :delete-permission="['audit:devicelog:delete']"
      :show-import="false"
      :show-export="true"
      :export-permission="['audit:devicelog:export']"
      :disabled-delete="selectedRowKeys.length === 0"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="fetchData"
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
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="deviceLogId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 设备类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'deviceType'">
          <a-tag :color="getDeviceTypeColor(record.deviceType)">
            {{ getDeviceTypeText(record.deviceType) }}
          </a-tag>
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <a-tag :color="record.deviceStatus === 0 ? 'success' : 'error'">
            {{ record.deviceStatus === 0 ? '正常' : '停用' }}
          </a-tag>
        </template>

        <!-- VPN状态列 -->
        <template v-if="column.dataIndex === 'isVpn'">
          <a-tag :color="record.isVpn === 1 ? 'orange' : 'default'">
            {{ record.isVpn === 1 ? 'VPN' : '直连' }}
          </a-tag>
        </template>

        <!-- 代理状态列 -->
        <template v-if="column.dataIndex === 'isProxy'">
          <a-tag :color="record.isProxy === 1 ? 'orange' : 'default'">
            {{ record.isProxy === 1 ? '代理' : '直连' }}
          </a-tag>
        </template>

        <!-- 登录类型列 -->
        <template v-if="column.dataIndex === 'loginType'">
          <Takt-dict-tag dict-type="audit_login_type" :value="record.loginType" />
        </template>

        <!-- 登录来源列 -->
        <template v-if="column.dataIndex === 'loginSource'">
          <Takt-dict-tag dict-type="audit_login_source" :value="record.loginSource" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-detail="true"
            :detail-permission="['audit:devicelog:detail']"
            :show-edit="false"
            :show-delete="true"
            :delete-permission="['audit:devicelog:delete']"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 设备日志详情对话框 -->
    <device-log-detail
      v-if="detailVisible"
      :device-log="currentDeviceLog"
      @close="detailVisible = false"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
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

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktDeviceLog, TaktDeviceLogQuery } from '@/types/audit/deviceLog'
import { useDictStore } from '@/stores/dictStore'
import {
  getDeviceLogList,
  deleteDeviceLog,
  batchDeleteDeviceLog,
  exportDeviceLog
} from '@/api/audit/deviceLog'
import DeviceLogDetail from './components/DeviceLogDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== CRUD 相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userId',
    label: '用户ID',
    type: 'input' as const,
  },
  {
    name: 'deviceType',
    label: '设备类型',
    type: 'select' as const,
    props: {
      dictType: 'audit_device_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'loginType',
    label: '登录类型',
    type: 'select' as const,
    props: {
      dictType: 'audit_login_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'loginSource',
    label: '登录来源',
    type: 'select' as const,
    props: {
      dictType: 'audit_login_source',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: '状态',
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'startTime',
    label: '开始时间',
    type: 'date' as const,
  },
  {
    name: 'endTime',
    label: '结束时间',
    type: 'date' as const,
  }
]

// 表格列定义
const columns: TableColumnsType = [
  {
    title: 'ID',
    dataIndex: 'deviceLogId',
    key: 'deviceLogId',
    width: 80,
    ellipsis: true
  },
  {
    title: '用户ID',
    dataIndex: 'userId',
    key: 'userId',
    width: 100,
    ellipsis: true
  },
  {
    title: '设备标识',
    dataIndex: 'deviceId',
    key: 'deviceId',
    width: 150,
    ellipsis: true
  },
  {
    title: '设备类型',
    dataIndex: 'deviceType',
    key: 'deviceType',
    width: 120
  },
  {
    title: '设备令牌',
    dataIndex: 'deviceToken',
    key: 'deviceToken',
    width: 150,
    ellipsis: true
  },
  {
    title: '提供者',
    dataIndex: 'providerDisplayName',
    key: 'providerDisplayName',
    width: 120,
    ellipsis: true
  },
  {
    title: '网络类型',
    dataIndex: 'networkType',
    key: 'networkType',
    width: 100,
    ellipsis: true
  },
  {
    title: '时区',
    dataIndex: 'timeZone',
    key: 'timeZone',
    width: 100,
    ellipsis: true
  },
  {
    title: '语言',
    dataIndex: 'language',
    key: 'language',
    width: 80,
    ellipsis: true
  },
  {
    title: 'VPN',
    dataIndex: 'isVpn',
    key: 'isVpn',
    width: 80
  },
  {
    title: '代理',
    dataIndex: 'isProxy',
    key: 'isProxy',
    width: 80
  },
  {
    title: '登录类型',
    dataIndex: 'loginType',
    key: 'loginType',
    width: 100
  },
  {
    title: '登录来源',
    dataIndex: 'loginSource',
    key: 'loginSource',
    width: 100
  },
  {
    title: '登录提供者',
    dataIndex: 'loginProvider',
    key: 'loginProvider',
    width: 120,
    ellipsis: true
  },
  {
    title: '首次登录时间',
    dataIndex: 'firstLoginTime',
    key: 'firstLoginTime',
    width: 150,
    ellipsis: true
  },
  {
    title: '首次登录IP',
    dataIndex: 'firstLoginIp',
    key: 'firstLoginIp',
    width: 120,
    ellipsis: true
  },
  {
    title: '首次登录地点',
    dataIndex: 'firstLoginLocation',
    key: 'firstLoginLocation',
    width: 120,
    ellipsis: true
  },
  {
    title: '最后登录时间',
    dataIndex: 'lastLoginTime',
    key: 'lastLoginTime',
    width: 150,
    ellipsis: true
  },
  {
    title: '最后登录IP',
    dataIndex: 'lastLoginIp',
    key: 'lastLoginIp',
    width: 120,
    ellipsis: true
  },
  {
    title: '最后登录地点',
    dataIndex: 'lastLoginLocation',
    key: 'lastLoginLocation',
    width: 120,
    ellipsis: true
  },
  {
    title: '最后在线时间',
    dataIndex: 'lastOnlineTime',
    key: 'lastOnlineTime',
    width: 150,
    ellipsis: true
  },
  {
    title: '最后离线时间',
    dataIndex: 'lastOfflineTime',
    key: 'lastOfflineTime',
    width: 150,
    ellipsis: true
  },
  {
    title: '登录次数',
    dataIndex: 'loginCount',
    key: 'loginCount',
    width: 100
  },
  {
    title: '连续登录天数',
    dataIndex: 'continuousLoginDays',
    key: 'continuousLoginDays',
    width: 120
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('table.columns.operation'),
    key: 'action',
    width: 120,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<TaktDeviceLogQuery>({
  pageIndex: 1,
  pageSize: 10,
  userId: undefined,
  deviceType: '',
  loginType: undefined,
  loginSource: undefined,
  status: -1,
  startTime: '',
  endTime: ''
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<TaktDeviceLog[]>([])
const selectedRowKeys = ref<string[]>([])
const showSearch = ref(true)

// 详情对话框
const detailVisible = ref(false)
const currentDeviceLog = ref<TaktDeviceLog | null>(null)

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// ==================== CRUD 方法 ====================
// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getDeviceLogList(queryParams.value)
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

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    userId: undefined,
    deviceType: '',
    loginType: undefined,
    loginSource: undefined,
    status: -1,
    startTime: '',
    endTime: ''
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理删除
const handleDelete = async (record: TaktDeviceLog) => {
  try {
    const res = await deleteDeviceLog(record.deviceLogId)
    if (res) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteDeviceLog(id)))
    const hasError = results.some(res => !res)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 分页处理
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  fetchData()
}

// 查看详情
const handleViewDetail = (record: TaktDeviceLog) => {
  currentDeviceLog.value = record
  detailVisible.value = true
}

// ==================== 导出方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportDeviceLog({
      ...queryParams.value
    })
    // 动态获取文件名
    console.log('res.headers', res.headers)
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    console.log('disposition', disposition)
    let fileName = ''
    if (disposition) {
      // 优先匹配 filename*（带中文）
      let match = disposition.match(/filename\*=UTF-8''([^;]+)/)
      if (match && match[1]) {
        fileName = decodeURIComponent(match[1])
      } else {
        // 再匹配 filename
        match = disposition.match(/filename="?([^";]+)"?/)
        if (match && match[1]) {
          fileName = match[1]
        }
      }
    }
    if (!fileName) {
      fileName = `设备日志数据_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('common.export.failed'))
  }
}

// ==================== 其它方法 ====================
// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('deviceLogColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key!, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    if (!col.key) return
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('deviceLogColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: TaktDeviceLog) => {
  console.log('行点击:', record)
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 获取设备类型颜色
const getDeviceTypeColor = (deviceType: string) => {
  switch (deviceType) {
    case 'Desktop': return 'blue'
    case 'Mobile': return 'green'
    case 'Tablet': return 'orange'
    default: return 'default'
  }
}

// 获取设备类型文本
const getDeviceTypeText = (deviceType: string) => {
  switch (deviceType) {
    case 'Desktop': return 'PC'
    case 'Mobile': return '移动设备'
    case 'Tablet': return '平板'
    default: return deviceType
  }
}

// ==================== 生命周期 ====================
onMounted(async () => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable', 'audit_device_type', 'audit_login_type', 'audit_login_source'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.device-log-container {
  height: 100%;
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

