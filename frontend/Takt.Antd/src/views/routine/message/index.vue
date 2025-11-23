<template>
  <div class="message-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['routine:message:create']"
      :show-edit="true"
      :edit-permission="['routine:message:create']"
      :show-delete="true"
      :delete-permission="['routine:message:delete']"
      :show-export="true"
      :export-permission="['routine:message:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleSendMessage"
      @edit="handleSendTestMessage"
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
      :scroll="{ x: 1200, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      :row-key="(record: TaktSignalRMessage) => record.signalRMessageId.toString()"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 消息类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'messageType'">
          <a-tag :color="getMessageTypeColor(record.messageType)">
            {{ getMessageTypeText(record.messageType) }}
          </a-tag>
        </template>

        <!-- 消息状态列 -->
        <template v-if="column.dataIndex === 'isRead'">
          <a-tag :color="record.isRead === 0 ? 'orange' : 'green'">
            {{ record.isRead === 0 ? '未读' : '已读' }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:message:create']"
            :show-delete="true"
            :delete-permission="['routine:message:delete']"
            size="small"
            @edit="handleEdit"
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

    <!-- 发送消息表单 -->
    <SendForm
      v-model:visible="sendFormVisible"
      @success="handleSendSuccess"
    />

    <!-- 消息详情表单 -->
    <MessageDetailForm
      v-model:visible="detailFormVisible"
      :message-data="selectedMessage"
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

<script lang="ts" setup>
import { ref, reactive, onMounted, onUnmounted, computed, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktSignalRMessage } from '@/types/routine/signalr/signalRMessage'
import { getSignalRMessageList, deleteSignalRMessage, exportSignalRMessage } from '@/api/routine/signalr/signalRMessage'
import { signalRService } from '@/utils/SignalR/service'
import { useUserStore, type UserInfoResponse } from '@/stores/userStore'
import SendForm from './components/SendForm.vue'
import MessageDetailForm from './components/MessageDetailForm.vue'

const { t } = useI18n()
const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'senderId',
    label: '发送者ID',
    type: 'input' as const,
    placeholder: '请输入发送者ID'
  },
  {
    name: 'receiverId',
    label: '接收者ID',
    type: 'input' as const,
    placeholder: '请输入接收者ID'
  },
  {
    name: 'messageType',
    label: '消息类型',
    type: 'select' as const,
    placeholder: '请选择消息类型',
    props: {
      options: [
        { label: '系统消息', value: 1 },
        { label: '邮件通知', value: 2 },
        { label: '通知状态', value: 3 },
        { label: '任务状态', value: 4 },
        { label: '个人通知', value: 5 },
        { label: '系统广播', value: 6 }
      ],
      showAll: true
    }
  },
  {
    name: 'isRead',
    label: '消息状态',
    type: 'select' as const,
    placeholder: '请选择消息状态',
    props: {
      options: [
        { label: '未读', value: 0 },
        { label: '已读', value: 1 }
      ],
      showAll: true
    }
  }
]

// 表格列定义
const columns: TableColumnsType = [
  {
    title: '消息ID',
    dataIndex: 'signalRMessageId',
    key: 'signalRMessageId',
    width: 120,
    ellipsis: true
  },
  {
    title: '发送者ID',
    dataIndex: 'senderId',
    key: 'senderId',
    width: 120,
    ellipsis: true
  },
  {
    title: '接收者ID',
    dataIndex: 'receiverId',
    key: 'receiverId',
    width: 120,
    ellipsis: true
  },
  {
    title: '消息类型',
    dataIndex: 'messageType',
    key: 'messageType',
    width: 120
  },
  {
    title: '消息内容',
    dataIndex: 'content',
    key: 'content',
    ellipsis: true
  },
  {
    title: '消息状态',
    dataIndex: 'isRead',
    key: 'isRead',
    width: 100
  },
  {
    title: '发送时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '读取时间',
    dataIndex: 'readTime',
    key: 'readTime',
    width: 180
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: '创建人',
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: '更新人',
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: '更新时间',
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<TaktSignalRMessage[]>([])
const total = ref(0)
const queryParams = reactive({
  pageIndex: 1,
  pageSize: 10,
  senderId: undefined,
  receiverId: undefined,
  messageType: undefined,
  isRead: undefined
})
const selectedRowKeys = ref<string[]>([])
const showSearch = ref(true)
const sendFormVisible = ref(false)
const detailFormVisible = ref(false)
const selectedMessage = ref<TaktSignalRMessage>()

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 全屏状态
const isFullscreen = ref(false)

// 生命周期钩子
onMounted(async () => {
  // 获取用户信息
  if (!userStore.userInfo) {
    try {
      await userStore.getUserInfo()
      console.log('用户信息获取成功:', userStore.userInfo)
    } catch (error) {
      console.error('获取用户信息失败:', error)
      message.error('获取用户信息失败，请重新登录')
      return
    }
  }

  initColumnSettings()
  fetchData()
  // 监听新消息事件
  signalRService.on('ReceiveMessage', handleNewMessage)
})

onUnmounted(() => {
  // 移除事件监听
  signalRService.off('ReceiveMessage', handleNewMessage)
})

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams
    })

    const res = await getSignalRMessageList(queryParams)
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
  queryParams.senderId = undefined
  queryParams.receiverId = undefined
  queryParams.messageType = undefined
  queryParams.isRead = undefined
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
  queryParams.pageIndex = 1
  fetchData()
}

/** 新消息事件 */
const handleNewMessage = () => {
  fetchData()
}

/** 编辑消息 */
const handleEdit = (record: TaktSignalRMessage) => {
  selectedMessage.value = record
  detailFormVisible.value = true
}

/** 删除消息 */
const handleDelete = async (record: TaktSignalRMessage) => {
  try {
    await deleteSignalRMessage(record.signalRMessageId)
    message.success('删除成功')
    fetchData()
  } catch (error) {
    message.error('删除失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的消息')
    return
  }
  try {
    await Promise.all(selectedRowKeys.value.map(id => deleteSignalRMessage(id)))
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

/** 切换全屏 */
const toggleFullscreen = async () => {
  try {
    if (!document.fullscreenElement) {
      await document.documentElement.requestFullscreen()
      isFullscreen.value = true
    } else {
      await document.exitFullscreen()
      isFullscreen.value = false
    }
  } catch (error) {
    console.error('全屏切换失败:', error)
  }
}



const handleSendMessage = () => {
  sendFormVisible.value = true
}

const handleSendTestMessage = async () => {
  const userInfo = userStore.userInfo as UserInfoResponse
  console.log('当前用户信息:', userInfo)
  
  if (!userInfo) {
    message.warning('用户信息未获取到，请刷新页面重试')
    return
  }

  if (!userInfo.userId) {
    message.warning('用户ID未获取到，请重新登录')
    return
  }

  try {
    const testContent = `测试消息 ${new Date().toLocaleString()}`
    console.log('发送测试消息:', {
      userId: userInfo.userId,
      userName: userInfo.userName,
      content: testContent
    })
    
    await signalRService.sendMessage({
      userId: userInfo.userId.toString(),
      content: testContent
    })
    message.success('测试消息发送成功')
  } catch (error) {
    console.error('发送测试消息失败:', error)
    message.error('发送测试消息失败')
  }
}

/** 发送消息成功 */
const handleSendSuccess = () => {
  message.success('消息发送成功')
  sendFormVisible.value = false
  fetchData()
}

/** 处理导出 */
const handleExport = async () => {
  try {
    const res = await exportSignalRMessage({
      ...queryParams
    })
    
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
    if (!fileName) {
      fileName = `消息数据_${new Date().getTime()}.xlsx`
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

// 处理列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('messageColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key!, false]))

  // 获取前10列（不包含操作列）
  const firstTenColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 10)

  // 设置前10列为true
  firstTenColumns.forEach(col => {
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
  localStorage.setItem('messageColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: TaktSignalRMessage) => {
  console.log('行点击:', record)
}

/** 获取消息类型颜色 */
const getMessageTypeColor = (type: number) => {
  const colors: Record<number, string> = {
    1: 'blue',   // 系统消息
    2: 'green',  // 邮件通知
    3: 'orange', // 通知状态
    4: 'purple', // 任务状态
    5: 'cyan',   // 个人通知
    6: 'red'     // 系统广播
  }
  return colors[type] || 'default'
}

/** 获取消息类型文本 */
const getMessageTypeText = (type: number) => {
  const texts: Record<number, string> = {
    1: '系统消息',
    2: '邮件通知',
    3: '通知状态',
    4: '任务状态',
    5: '个人通知',
    6: '系统广播'
  }
  return texts[type] || type.toString()
}
</script>

<style lang="less" scoped>
.message-container {
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
