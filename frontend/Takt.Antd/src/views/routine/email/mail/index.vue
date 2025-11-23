<template>
  <div class="mail-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item label="收件人">
          <a-input
            v-model:value="queryParams.mailTo"
            placeholder="请输入收件人"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="主题">
          <a-input
            v-model:value="queryParams.mailSubject"
            placeholder="请输入邮件主题"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item label="发送状态">
          <Takt-select
            v-model:value="queryParams.mailStatus"
            dict-type="sys_yes_no"
            type="radio"
            placeholder="请选择发送状态"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="发送时间">
          <a-range-picker
            v-model:value="dateRange"
            :show-time="true"
            format="YYYY-MM-DD HH:mm:ss"
            value-format="YYYY-MM-DD HH:mm:ss"
            @change="handleDateRangeChange"
          />
        </a-form-item>
      </template>
    </Takt-query>

    <!-- 操作按钮 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['routine:mail:add']"
      :show-edit="true"
      :edit-permission="['routine:mail:edit']"
      :show-delete="true"
      :delete-permission="['routine:mail:delete']"
      :show-export="true"
      :export-permission="['routine:mail:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
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
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: TaktMail) => String(record.mailId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 发送状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'mailStatus'">
          <Takt-dict-tag dict-type="sys_yes_no" :value="record.mailStatus" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:mail:edit']"
            :show-delete="true"
            :delete-permission="['routine:mail:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <!-- 发送按钮 -->
            <template #extra>
              <a-tooltip title="发送邮件">
                <a-button
                  v-hasPermi="['routine:mail:send']"
                  type="link"
                  size="small"
                  @click.stop="handleSend(record)"
                >
                  <template #icon><send-outlined /></template>
                </a-button>
              </a-tooltip>
            </template>
          </Takt-operation>
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

    <!-- 邮件表单对话框 -->
    <mail-form
      v-model:open="formVisible"
      :title="formTitle"
      :mail-id="selectedMailId"
      @success="handleSuccess"
    />

    <!-- 发送邮件对话框 -->
    <send-mail-form
      v-model:open="sendMailVisible"
      :mail-id="selectedMailId"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>

    <!-- 邮件详情对话框 -->
    <mail-detail
      v-model:open="detailVisible"
      :mail-id="selectedMailId"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktMail, TaktMailQuery } from '@/types/routine/email/mail'
import type { QueryField } from '@/types/components/query'
import { 
  getMailList, 
  deleteMail, 
  batchDeleteMail, 
  exportMailList 
} from '@/api/routine/email/mail'
import { 
  PlusOutlined, 
  EditOutlined, 
  DeleteOutlined, 
  ExportOutlined, 
  ImportOutlined, 
  DownloadOutlined, 
  SettingOutlined,
  SendOutlined
} from '@ant-design/icons-vue'

// 组件导入
import MailForm from './components/MailForm.vue'
import SendMailForm from './components/SendMailForm.vue'
import MailDetail from './components/MailDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 日期范围
const dateRange = ref<[string, string] | undefined>(undefined)

// 表格列定义
interface TableColumn {
  title: string
  dataIndex?: string
  key: string
  width?: number
  ellipsis?: boolean
  fixed?: string
}

const columns: TableColumn[] = [
  {
    title: '收件人',
    dataIndex: 'mailTo',
    key: 'mailTo',
    width: 180,
    ellipsis: true
  },
  {
    title: '主题',
    dataIndex: 'mailSubject',
    key: 'mailSubject',
    width: 200,
    ellipsis: true
  },
  {
    title: '发送状态',
    dataIndex: 'mailStatus',
    key: 'mailStatus',
    width: 100
  },
  {
    title: '发送时间',
    dataIndex: 'mailSendTime',
    key: 'mailSendTime',
    width: 180,
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<TaktMailQuery>({
  pageIndex: 1,
  pageSize: 10,
  mailTo: '',
  mailSubject: '',
  mailStatus: undefined,
  startTime: undefined,
  endTime: undefined
})

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'mailTo',
    label: '收件人',
    type: 'input' as const
  },
  {
    name: 'mailSubject',
    label: '主题',
    type: 'input' as const
  },
  {
    name: 'mailStatus',
    label: '发送状态',
    type: 'select' as const,
    props: {
      dictType: 'sys_yes_no',
      type: 'radio'
    }
  }
]

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<TaktMail[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedMailId = ref<number | bigint>()

// 发送邮件表单
const sendMailVisible = ref(false)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns

// 初始化列设置
const initColumnSettings = () => {
  const savedSettings = localStorage.getItem('mailColumnSettings')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
  }
}
const columnSettings = ref<Record<string, boolean>>({})

// 获取表格数据
const fetchData = async () => {
  try {
    const res = await getMailList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || '获取数据失败')
    }
  } catch (error) {
    console.error(error)
    message.error('获取数据失败')
  }
}

// 查询方法
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    mailTo: '',
    mailSubject: '',
    mailStatus: undefined,
    startTime: undefined,
    endTime: undefined
  }
  dateRange.value = undefined
  fetchData()
}

// 处理日期范围变化
const handleDateRangeChange = (_: any, dateStrings: [string, string]) => {
  if (dateStrings && dateStrings.length === 2) {
    queryParams.value.startTime = new Date(dateStrings[0])
    queryParams.value.endTime = new Date(dateStrings[1])
  } else {
    queryParams.value.startTime = undefined
    queryParams.value.endTime = undefined
  }
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = '新增邮件'
  selectedMailId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktMail) => {
  formTitle.value = '编辑邮件'
  selectedMailId.value = record.mailId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktMail) => {
  try {
    const res = await deleteMail(record.mailId)
    if (res.data.code === 200) {
      message.success('删除成功')
      fetchData()
    } else {
      message.error(res.data.msg || '删除失败')
    }
  } catch (error) {
    console.error(error)
    message.error('删除失败')
  }
}

// 处理批量删除
const handleBatchDelete = async () => {
  try {
    const mailIds = selectedRowKeys.value.map(id => Number(id))
    const res = await batchDeleteMail(mailIds)
    if (res.data.code === 200) {
      message.success('删除成功')
      fetchData()
    } else {
      message.error(res.data.msg || '删除失败')
    }
  } catch (error) {
    console.error(error)
    message.error('删除失败')
  }
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportMailList(queryParams.value)
    const blob = new Blob([res.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = '邮件数据.xlsx'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error(error)
    message.error('导出失败')
  }
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 处理发送邮件
const handleSend = (record: TaktMail) => {
  selectedMailId.value = record.mailId
  sendMailVisible.value = true
}

// 处理发送邮件成功
const handleSendSuccess = () => {
  sendMailVisible.value = false
  message.success('发送成功')
  fetchData()
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  
  const record = tableData.value.find(item => String(item.mailId) === String(selectedRowKeys.value[0]))
  if (record) {
    handleEdit(record)
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedValue.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('mailColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: TaktMail) => {
  selectedMailId.value = record.mailId
  detailVisible.value = true
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

// 详情对话框
const detailVisible = ref(false)

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['sys_yes_no'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.mail-container {
  height: 100%;
  background-color: var(--ant-color-bg-container);
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
