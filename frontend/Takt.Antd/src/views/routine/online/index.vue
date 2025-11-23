<template>
  <div class="online-user-container">
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
      :show-delete="true"
      :delete-permission="['routine:online:list']"
      :show-export="true"
      :export-permission="['routine:online:export']"
      :disabled-delete="selectedRowKeys.length === 0"
      @refresh="fetchData"
      @delete="handleBatchDelete"
      @export="handleExport"
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
      :scroll="{ x: 800, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="deviceId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 在线状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'onlineStatus'">
          <a-tag :color="record.onlineStatus === 0 ? 'green' : 'red'">
            {{ record.onlineStatus === 0 ? t('signalr.online.status.online') : t('signalr.online.status.offline') }}
          </a-tag>
        </template>

        <!-- 设备类型列 -->
        <template v-if="column.dataIndex === 'deviceType'">
          <a-tag :color="getDeviceTypeColor(record.deviceType)">
            {{ getDeviceTypeLabel(record.deviceType) }}
          </a-tag>
        </template>

        <!-- 操作系统类型列 -->
        <template v-if="column.dataIndex === 'osType'">
          <a-tag :color="getOsTypeColor(record.osType)">
            {{ getOsTypeLabel(record.osType) }}
          </a-tag>
        </template>

        <!-- 浏览器类型列 -->
        <template v-if="column.dataIndex === 'browserType'">
          <a-tag :color="getBrowserTypeColor(record.browserType)">
            {{ getBrowserTypeLabel(record.browserType) }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-force="true"
            :force-permission="['routine:online:delete']"
            :show-send="true"
            :send-permission="['signalr:message:create']"
            size="small"
            @force="handleForceOffline(record)"
            @send="handleSendMessage(record)"
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

    <!-- 发送消息表单 -->
    <SendForm
      v-model:visible="sendFormVisible"
      :default-user-id="selectedUserId"
      @success="handleSendSuccess"
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
import { ref, onMounted, onUnmounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktSignalROnline, TaktSignalROnlineQuery } from '@/types/routine/signalr/signalROnline'
import type { TaktPagedQuery } from '@/types/common'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/userStore'
import { SettingOutlined, FullscreenOutlined, FullscreenExitOutlined } from '@ant-design/icons-vue'
import {
  getSignalROnlineList,
  forceSignalROnline,
  exportSignalROnline
} from '@/api/routine/signalr/signalROnline'
import { signalRService } from '@/utils/SignalR/service'
import SendForm from './components/SendForm.vue'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userId',
    label: t('signalr.online.fields.userId.label'),
    type: 'input' as const,
    placeholder: t('signalr.online.fields.userId.placeholder')
  },
  {
    name: 'startTime',
    label: t('signalr.online.fields.startTime.label'),
    type: 'date' as const,
    placeholder: t('signalr.online.fields.startTime.placeholder')
  },
  {
    name: 'endTime',
    label: t('signalr.online.fields.endTime.label'),
    type: 'date' as const,
    placeholder: t('signalr.online.fields.endTime.placeholder')
  },
  {
    name: 'onlineStatus',
    label: t('signalr.online.fields.onlineStatus.label'),
    type: 'select' as const,
    props: {
      options: [
        { label: t('signalr.online.fields.onlineStatus.options.online'), value: 0 },
        { label: t('signalr.online.fields.onlineStatus.options.offline'), value: 1 }
      ],
      showAll: true
    }
  }
]

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktSignalROnline[]>([])

// 选中的行
const selectedRowKeys = ref<string[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('signalr.online.fields.userId.label'),
    dataIndex: 'userId',
    key: 'userId',
    width: 100,
    ellipsis: true
  },
  {
    title: t('signalr.online.fields.userName.label'),
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('signalr.online.fields.nickName.label'),
    dataIndex: 'nickName',
    key: 'nickName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('signalr.online.fields.connectionId.label'),
    dataIndex: 'connectionId',
    key: 'connectionId',
    width: 200,
    ellipsis: true
  },
  {
    title: t('signalr.online.fields.deviceId.label'),
    dataIndex: 'deviceId',
    key: 'deviceId',
    width: 150,
    ellipsis: true
  },
  {
    title: t('signalr.online.fields.ipAddress.label'),
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 120
  },
  {
    title: t('signalr.online.fields.userAgent.label'),
    dataIndex: 'userAgent',
    key: 'userAgent',
    width: 200,
    ellipsis: true
  },
  {
    title: t('signalr.online.fields.lastActivity.label'),
    dataIndex: 'lastActivity',
    key: 'lastActivity',
    width: 150
  },
  {
    title: t('signalr.online.fields.onlineStatus.label'),
    dataIndex: 'onlineStatus',
    key: 'onlineStatus',
    width: 100
  },
  {
    title: t('signalr.online.fields.lastHeartbeat.label'),
    dataIndex: 'lastHeartbeat',
    key: 'lastHeartbeat',
    width: 150
  },
  {
    title: t('signalr.online.fields.totalOnlineMinutes.label'),
    dataIndex: 'totalOnlineMinutes',
    key: 'totalOnlineMinutes',
    width: 120
  },
  {
    title: t('signalr.online.fields.todayOnlineMinutes.label'),
    dataIndex: 'todayOnlineMinutes',
    key: 'todayOnlineMinutes',
    width: 120
  },
  {
    title: t('signalr.online.fields.remark.label'),
    dataIndex: 'remark',
    key: 'remark',
    width: 150,
    ellipsis: true
  },
  {
    title: t('common.action'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<TaktSignalROnlineQuery & TaktPagedQuery>({
  pageIndex: 1,
  pageSize: 10,
  userId: undefined,
  startTime: undefined,
  endTime: undefined,
  onlineStatus: undefined
})

// ==================== 表单相关 ====================
// 表单对话框
const sendFormVisible = ref(false)
const selectedUserId = ref<string>()

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 全屏状态
const isFullscreen = ref(false)

// ==================== 数据获取相关 ====================
// 数据获取
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getSignalROnlineList(queryParams.value)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取在线用户列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// ==================== 查询相关方法 ====================
// 处理查询
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
    startTime: undefined,
    endTime: undefined,
    onlineStatus: undefined
  }
  fetchData()
}

// ==================== 表格相关方法 ====================
// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 处理页面大小变化
const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  queryParams.value.pageIndex = 1
  fetchData()
}

// ==================== SignalR相关方法 ====================
// 处理用户上线
const handleUserOnline = (userId: string) => {
  console.log('用户上线:', userId)
  fetchData()
}

// 处理用户下线
const handleUserOffline = (userId: string) => {
  console.log('用户下线:', userId)
  fetchData()
}

// 处理强制下线事件
const handleForceOfflineEvent = async (msg: string) => {
  console.log('收到强制下线事件:', msg)
  message.info(msg)
  fetchData()
}

// ==================== 业务操作相关方法 ====================
// 强制用户下线
const handleForceOffline = async (record: TaktSignalROnline) => {
  try {
    await forceSignalROnline(record.deviceId)
    message.success(t('signalr.online.operation.forceOfflineSuccess'))
    fetchData()
  } catch (error) {
    console.error('强制下线失败:', error)
    message.error(t('signalr.online.operation.forceOfflineFailed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('signalr.online.operation.selectUsersFirst'))
    return
  }
  try {
    // selectedRowKeys 现在包含的是 deviceId
    await Promise.all(selectedRowKeys.value.map(deviceId => forceSignalROnline(deviceId)))
    message.success(t('signalr.online.operation.batchLogoutSuccess'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量强制下线失败:', error)
    message.error(t('signalr.online.operation.batchLogoutFailed'))
  }
}

// ==================== 其它功能方法 ====================
// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = async () => {
  if (!document.fullscreenElement) {
    await document.documentElement.requestFullscreen()
    isFullscreen.value = true
  } else {
    await document.exitFullscreen()
    isFullscreen.value = false
  }
}

// ==================== 消息相关方法 ====================
// 发送消息
const handleSendMessage = (record: TaktSignalROnline) => {
  selectedUserId.value = record.userId
  sendFormVisible.value = true
}

// 发送成功回调
const handleSendSuccess = () => {
  sendFormVisible.value = false
  selectedUserId.value = undefined
}

// 处理接收到的消息
const handleReceivedMessage = (message: any) => {
  console.log('收到消息:', message)
  // 可以在这里处理接收到的消息
}

// ==================== 导出相关方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportSignalROnline({
      ...queryParams.value
    }, '在线用户信息')
    
    // 动态获取文件名
    const disposition = res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
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
    
    // 如果没有从响应头获取到文件名，使用默认文件名
    if (!fileName) {
      const now = new Date()
      const timestamp = now.toISOString().slice(0, 19).replace(/[-:]/g, '').replace('T', '_')
      fileName = `在线用户信息_${timestamp}.xlsx`
    }
    
    // 创建下载链接
    const url = window.URL.createObjectURL(new Blob([res.data]))
    const link = document.createElement('a')
    link.href = url
    link.download = fileName
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    
    message.success(t('signalr.online.operation.exportSuccess'))
  } catch (error) {
    console.error('导出失败:', error)
    message.error(t('signalr.online.operation.exportFailed'))
  }
}

// ==================== 列设置相关方法 ====================
// 处理列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('onlineUserColumnSettings')

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
  localStorage.setItem('onlineUserColumnSettings', JSON.stringify(settings))
}

// ==================== 工具方法 ====================
// 处理行点击
const handleRowClick = (record: TaktSignalROnline) => {
  console.log('点击行:', record)
}

// 获取设备类型颜色
const getDeviceTypeColor = (type: number) => {
  const colors = {
    0: 'blue',    // 未知
    1: 'green',   // 手机
    2: 'orange',  // 平板
    3: 'purple',  // 电脑
    4: 'cyan'     // 其他
  }
  return colors[type as keyof typeof colors] || 'default'
}

// 获取设备类型标签
const getDeviceTypeLabel = (type: number) => {
  const labels = {
    0: '未知',
    1: '手机',
    2: '平板',
    3: '电脑',
    4: '其他'
  }
  return labels[type as keyof typeof labels] || '未知'
}

// 获取操作系统类型颜色
const getOsTypeColor = (type: number) => {
  const colors = {
    0: 'blue',    // 未知
    1: 'green',   // Windows
    2: 'orange',  // macOS
    3: 'purple',  // Linux
    4: 'cyan',    // Android
    5: 'red'      // iOS
  }
  return colors[type as keyof typeof colors] || 'default'
}

// 获取操作系统类型标签
const getOsTypeLabel = (type: number) => {
  const labels = {
    0: '未知',
    1: 'Windows',
    2: 'macOS',
    3: 'Linux',
    4: 'Android',
    5: 'iOS'
  }
  return labels[type as keyof typeof labels] || '未知'
}

// 获取浏览器类型颜色
const getBrowserTypeColor = (type: number) => {
  const colors = {
    0: 'blue',    // 未知
    1: 'green',   // Chrome
    2: 'orange',  // Firefox
    3: 'purple',  // Safari
    4: 'cyan',    // Edge
    5: 'red'      // 其他
  }
  return colors[type as keyof typeof colors] || 'default'
}

// 获取浏览器类型标签
const getBrowserTypeLabel = (type: number) => {
  const labels = {
    0: '未知',
    1: 'Chrome',
    2: 'Firefox',
    3: 'Safari',
    4: 'Edge',
    5: '其他'
  }
  return labels[type as keyof typeof labels] || '未知'
}

// ==================== 生命周期 ====================
// 生命周期钩子
onMounted(() => {
  console.log('[在线用户] 组件挂载，SignalR 连接状态:', signalRService.getConnectionState())
  initColumnSettings()
  fetchData()
  // 监听用户上下线事件
  signalRService.on('UserOnline', handleUserOnline)
  signalRService.on('UserOffline', handleUserOffline)
  // 监听强制下线事件
  signalRService.on('ForceOffline', handleForceOfflineEvent)
  // 监听SignalR消息
  signalRService.on('ReceiveMessage', handleReceivedMessage)
})

onUnmounted(() => {
  console.log('[在线用户] 组件卸载，SignalR 连接状态:', signalRService.getConnectionState())
  // 移除事件监听
  signalRService.off('UserOnline', handleUserOnline)
  signalRService.off('UserOffline', handleUserOffline)
  signalRService.off('ForceOffline', handleForceOfflineEvent)
  signalRService.off('ReceiveMessage', handleReceivedMessage)
})
</script>

<style scoped>
.online-user-container {
  padding: 20px;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  padding: 4px 0;
  
  &:last-child {
    border-bottom: none;
  }
}
</style>
