<template>
  <div class="workflow-instance-container">
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
      :add-permission="['workflow:manage:instance:create']"
      :show-edit="true"
      :edit-permission="['workflow:manage:instance:update']"
      :show-delete="true"
      :delete-permission="['workflow:manage:instance:delete']"
      :show-import="true"
      :import-permission="['workflow:manage:instance:import']"
      :show-export="true"
      :export-permission="['workflow:manage:instance:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
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
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: TaktInstance) => String(record.instanceId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="wf_instance_status" :value="record.status" />
        </template>

        <!-- 优先级列 -->
        <template v-if="column.dataIndex === 'priority'">
          <Takt-dict-tag dict-type="wf_instance_priority" :value="record.priority" />
        </template>

        <!-- 紧急程度列 -->
        <template v-if="column.dataIndex === 'urgency'">
          <Takt-dict-tag dict-type="wf_instance_urgency" :value="record.urgency" />
        </template>

        <!-- 表单类型列 -->
        <template v-if="column.dataIndex === 'formType'">
          <Takt-dict-tag dict-type="wf_form_type" :value="record.formType" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :button-order="getInstanceButtonOrder(record.status)"
            :show-edit="record.status === 0"
            :edit-permission="['workflow:manage:instance:update']"
            :show-delete="record.status === 0"
            :delete-permission="['workflow:manage:instance:delete']"
            :show-view="true"
            :view-permission="['workflow:manage:instance:query']"
            :show-start="record.status === 0"
            :start-permission="['workflow:manage:instance:start']"
            :show-suspend="record.status === 1"
            :suspend-permission="['workflow:manage:instance:suspend']"
            :show-resume="record.status === 2"
            :resume-permission="['workflow:manage:instance:resume']"
            :show-submit="record.status === 1 && canSubmit(record)"
            :submit-permission="['workflow:manage:instance:submit']"
            :show-withdraw="record.status === 1 && canWithdraw(record)"
            :withdraw-permission="['workflow:manage:instance:withdraw']"
            :show-terminate="record.status >= 1 && record.status <= 2"
            :terminate-permission="['workflow:manage:instance:terminate']"
            :show-transfer="record.status === 1 && canTransfer(record)"
            :transfer-permission="['workflow:manage:instance:transfer']"
            :show-delegate="record.status === 1 && canDelegate(record)"
            :delegate-permission="['workflow:manage:instance:delegate']"
            :show-return="record.status === 1 && canReturn(record)"
            :return-permission="['workflow:manage:instance:return']"
            :show-urge="record.status === 1"
            :urge-permission="['workflow:manage:instance:urge']"
            :show-progress="record.status >= 1"
            :progress-permission="['workflow:manage:instance:progress']"
            :show-history="record.status >= 1"
            :history-permission="['workflow:manage:instance:history']"
            :show-addsign="record.status === 1 && canAddsign(record)"
            :addsign-permission="['workflow:manage:instance:addsign']"
            :show-subsign="record.status === 1 && canSubsign(record)"
            :subsign-permission="['workflow:manage:instance:subsign']"
            :show-approve="record.status === 1 && canApprove(record)"
            :approve-permission="['workflow:manage:instance:approve']"
            :show-reject="record.status === 1 && canReject(record)"
            :reject-permission="['workflow:manage:instance:reject']"
            :show-claim="record.status === 1 && canClaim(record)"
            :claim-permission="['workflow:manage:instance:claim']"
            :show-release="record.status === 1 && canRelease(record)"
            :release-permission="['workflow:manage:instance:release']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @view="handleView"
            @start="handleStart"
            @suspend="handleSuspend"
            @resume="handleResume"
            @submit="handleSubmit"
            @withdraw="handleWithdraw"
            @terminate="handleTerminate"
            @transfer="handleTransfer"
            @delegate="handleDelegate"
            @return="handleReturn"
            @urge="handleUrge"
            @progress="handleProgress"
            @history="handleHistory"
            @addsign="handleAddsign"
            @subsign="handleSubsign"
            @approve="handleApprove"
            @reject="handleReject"
            @claim="handleClaim"
            @release="handleRelease"
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

    <!-- 新增/编辑表单 -->
    <instance-form
      v-model:open="formVisible"
      :title="formTitle"
      :instance-id="selectedInstanceId"
      @success="handleSuccess"
    />

    <!-- 查看详情 -->
    <instance-detail
      v-model:open="detailVisible"
      :instance-id="selectedInstanceId"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleTemplate"
      template-file-name="工作流实例导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的工作流实例信息字段,\n如实例标题,流程定义ID,业务键,发起人ID,优先级,紧急程度等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['workflow:manage:instance:template']"
    />

    <!-- 终止对话框 -->
    <a-modal
      v-model:open="terminateVisible"
      title="终止工作流实例"
      @ok="handleTerminateSubmit"
      @cancel="handleTerminateCancel"
    >
      <a-form :model="terminateForm" :rules="terminateRules">
        <a-form-item label="终止原因" name="reason">
          <a-textarea v-model:value="terminateForm.reason" placeholder="请输入终止原因" :rows="4" />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      :title="t('common.columnSetting')"
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
import { ref, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktInstance, TaktInstanceQuery } from '@/types/workflow/instance'
import { useDictStore } from '@/stores/dictStore'
import { useUserStore } from '@/stores/userStore'
import { useRouter } from 'vue-router'
import {
  getInstanceList,
  getInstanceById,
  createInstance,
  updateInstance,
  deleteInstance,
  batchDeleteInstance,
  importInstance,
  exportInstance,
  getInstanceTemplate
} from '@/api/workflow/instance'
import InstanceForm from './components/InstanceForm.vue'
import InstanceDetail from './components/InstanceDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const userStore = useUserStore()
const router = useRouter()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'instanceTitle',
    label: t('workflow.instance.fields.instanceTitle'),
    type: 'input' as const
  },
  {
    name: 'businessKey',
    label: t('workflow.instance.fields.businessKey'),
    type: 'input' as const
  },
  {
    name: 'schemeId',
    label: t('workflow.instance.fields.schemeId'),
    type: 'input' as const
  },
  {
    name: 'initiatorId',
    label: t('workflow.instance.fields.initiatorId'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('workflow.instance.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'wf_instance_status',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'priority',
    label: t('workflow.instance.fields.priority'),
    type: 'select' as const,
    props: {
      dictType: 'wf_instance_priority',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'urgency',
    label: t('workflow.instance.fields.urgency'),
    type: 'select' as const,
    props: {
      dictType: 'wf_instance_urgency',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'timeRange',
    label: t('workflow.instance.fields.timeRange'),
    type: 'dateRange' as const,
    props: {
      placeholder: ['开始时间', '结束时间'],
      showTime: true,
      format: 'YYYY-MM-DD HH:mm:ss'
    }
  }
]

// 查询参数
const queryParams = ref<TaktInstanceQuery>({
  pageIndex: 1,
  pageSize: 10,
  instanceTitle: '',
  businessKey: '',
  status: -1,
  priority: -1,
  urgency: -1,
  startTime: undefined,
  endTime: undefined
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktInstance[]>([])

// 选中的行
const selectedRowKeys = ref<(string | number)[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns = [
  {
    title: t('table.columns.id'),
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 200
  },
  {
    title: t('workflow.instance.fields.instanceTitle'),
    dataIndex: 'instanceTitle',
    key: 'instanceTitle',
    width: 200
  },
  {
    title: t('workflow.instance.fields.businessKey'),
    dataIndex: 'businessKey',
    key: 'businessKey',
    width: 200
  },
  {
    title: t('workflow.instance.fields.schemeId'),
    dataIndex: 'schemeId',
    key: 'schemeId',
    width: 150
  },
  {
    title: t('workflow.instance.fields.currentNodeId'),
    dataIndex: 'currentNodeId',
    key: 'currentNodeId',
    width: 200
  },
  {
    title: t('workflow.instance.fields.currentNodeName'),
    dataIndex: 'currentNodeName',
    key: 'currentNodeName',
    width: 200
  },
  {
    title: t('workflow.instance.fields.initiatorId'),
    dataIndex: 'initiatorId',
    key: 'initiatorId',
    width: 150
  },
  {
    title: t('workflow.instance.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 150
  },
  {
    title: t('workflow.instance.fields.priority'),
    dataIndex: 'priority',
    key: 'priority',
    width: 100
  },
  {
    title: t('workflow.instance.fields.urgency'),
    dataIndex: 'urgency',
    key: 'urgency',
    width: 100
  },
  {
    title: t('workflow.instance.fields.formType'),
    dataIndex: 'formType',
    key: 'formType',
    width: 120
  },
  {
    title: t('workflow.instance.fields.startTime'),
    dataIndex: 'startTime',
    key: 'startTime',
    width: 180
  },
  {
    title: t('workflow.instance.fields.endTime'),
    dataIndex: 'endTime',
    key: 'endTime',
    width: 180
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// ==================== 表单相关 ====================
// 弹窗控制相关
const formVisible = ref(false)
const formTitle = ref('')
const selectedInstanceId = ref<string | undefined>(undefined)
const detailVisible = ref(false)

// ==================== 业务操作相关 ====================
// 终止相关
const terminateVisible = ref(false)
const terminateForm = ref({
  id: '0',
  reason: ''
})
const terminateRules = {
  reason: [
    { type: 'array' as const, required: true, message: t('workflow.instance.fields.terminateReason.required'), trigger: 'blur' as const }
  ]
}

// ==================== 导入相关 ====================
const importVisible = ref(false)

// ==================== 导出相关 ====================

// ==================== 模板相关 ====================

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// ==================== 数据获取方法 ====================
// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getInstanceList(queryParams.value)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows || []
      total.value = res.data.totalNum || 0
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取工作流实例列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    // 处理时间范围查询
    if (values.timeRange && values.timeRange.length === 2) {
      queryParams.value.startTime = values.timeRange[0]
      queryParams.value.endTime = values.timeRange[1]
      delete values.timeRange
    }
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
    instanceTitle: '',
    businessKey: '',
    status: -1,
    priority: -1,
    urgency: -1,
    startTime: undefined,
    endTime: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// ==================== 业务操作方法 ====================
// 处理删除
const handleDelete = async (record: TaktInstance) => {
  try {
    const res = await deleteInstance(record.instanceId)
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

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(
    item => String(item.instanceId) === String(selectedRowKeys.value[0])
  )
  if (record) {
    handleEdit(record)
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteInstance(String(id))))
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

// ==================== 导入方法 ====================
// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importInstance(file)
    console.log('导入响应数据:', res)
    console.log('res.data:', res.data)
    
    // res.data 包含 { success, fail } 对象
    const { success = 0, fail = 0 } = res.data
    
    console.log('解析后的数据:', { success, fail })
    
    return {
      code: 200,
      msg: '导入成功',
      data: {
        success,
        fail
      }
    }
  } catch (error: any) {
    console.error('导入失败:', error)
    throw error
  }
}

// 处理导入成功
const handleImportSuccess = () => {
  //message.success(t('common.import.success'))
  fetchData()
}

// ==================== 导出方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportInstance({
      ...queryParams.value
    })
    // 动态获取文件名
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
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
      fileName = `工作流实例_${new Date().getTime()}.xlsx`
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

// ==================== 模板方法 ====================
// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getInstanceTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
    throw error
  }
}

// ==================== 其它功能 ====================
// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('workflowInstanceColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前10列（不包含操作列）
  const firstTenColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 10)

  // 设置前10列为true
  firstTenColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('workflowInstanceColumnSettings', JSON.stringify(settings))
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 处理行点击
const handleRowClick = (record: TaktInstance) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: TaktInstance) => {
  selectedInstanceId.value = record.instanceId
  detailVisible.value = true
}

// 处理新增
const handleAdd = () => {
  selectedInstanceId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktInstance) => {
  selectedInstanceId.value = record.instanceId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedInstanceId.value = undefined
  fetchData()
}

// 处理提交
const handleSubmit = async (record: TaktInstance) => {
  try {
    // 这里需要调用工作流引擎的提交接口
    message.success(t('workflow.instance.submit.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.submit.failed'))
  }
}

// 处理撤回
const handleWithdraw = async (record: TaktInstance) => {
  try {
    // 这里需要调用工作流引擎的撤回接口
    message.success(t('workflow.instance.withdraw.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.withdraw.failed'))
  }
}

// 处理启动
const handleStart = async (record: TaktInstance) => {
  try {
    // 这里需要调用工作流引擎的启动接口
    message.success(t('workflow.instance.start.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.start.failed'))
  }
}

// 处理暂停
const handleSuspend = async (record: TaktInstance) => {
  try {
    // 这里需要调用工作流引擎的暂停接口
    message.success(t('workflow.instance.suspend.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.suspend.failed'))
  }
}

// 处理恢复
const handleResume = async (record: TaktInstance) => {
  try {
    // 这里需要调用工作流引擎的恢复接口
    message.success(t('workflow.instance.resume.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.resume.failed'))
  }
}

// 处理终止
const handleTerminate = (record: TaktInstance) => {
  terminateForm.value.id = record.instanceId
  terminateVisible.value = true
}

// 处理终止提交
const handleTerminateSubmit = async () => {
  if (!terminateForm.value.reason) {
    message.warning(t('workflow.instance.fields.terminateReason.required'))
    return
  }

  try {
    // 这里需要调用工作流引擎的终止接口
    message.success(t('workflow.instance.terminate.success'))
    terminateVisible.value = false
    terminateForm.value = {
      id: '0',
      reason: ''
    }
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('workflow.instance.terminate.failed'))
  }
}

// 处理终止取消
const handleTerminateCancel = () => {
  terminateVisible.value = false
  terminateForm.value = {
    id: '0',
    reason: ''
  }
}

// 处理转办流程实例
const handleTransfer = (record: TaktInstance) => {
  console.log('转办工作流实例:', record)
  // TODO: 打开转办对话框
  message.info('转办流程功能待实现')
}

// 处理委托流程实例
const handleDelegate = (record: TaktInstance) => {
  console.log('委托工作流实例:', record)
  // TODO: 打开委托对话框
  message.info('委托流程功能待实现')
}

// 处理退回流程实例
const handleReturn = (record: TaktInstance) => {
  console.log('退回工作流实例:', record)
  // TODO: 打开退回对话框
  message.info('退回流程功能待实现')
}

// 处理催办流程实例
const handleUrge = (record: TaktInstance) => {
  console.log('催办工作流实例:', record)
  // TODO: 发送催办通知
  message.info('催办流程功能待实现')
}

// 处理查看流程进度
const handleProgress = (record: TaktInstance) => {
  console.log('查看工作流实例进度:', record)
  // TODO: 打开进度查看页面
  message.info('查看进度功能待实现')
}

// 处理查看流程历史
const handleHistory = (record: TaktInstance) => {
  console.log('查看工作流实例历史:', record)
  // TODO: 打开历史记录页面
  message.info('查看历史功能待实现')
}

// 处理加签操作
const handleAddsign = (record: TaktInstance) => {
  console.log('加签工作流实例:', record)
  // TODO: 打开加签对话框
  message.info('加签功能待实现')
}

// 处理减签操作
const handleSubsign = (record: TaktInstance) => {
  console.log('减签工作流实例:', record)
  // TODO: 打开减签对话框
  message.info('减签功能待实现')
}

// 处理审批操作
const handleApprove = (record: TaktInstance) => {
  console.log('审批工作流实例:', record)
  // TODO: 打开审批对话框
  message.info('审批功能待实现')
}

// 处理拒绝操作
const handleReject = (record: TaktInstance) => {
  console.log('拒绝工作流实例:', record)
  // TODO: 打开拒绝对话框
  message.info('拒绝功能待实现')
}

// 处理签收操作
const handleClaim = (record: TaktInstance) => {
  console.log('签收工作流实例:', record)
  // TODO: 执行签收逻辑
  message.info('签收功能待实现')
}

// 处理释放操作
const handleRelease = (record: TaktInstance) => {
  console.log('释放工作流实例:', record)
  // TODO: 执行释放逻辑
  message.info('释放功能待实现')
}

// 根据流程实例状态获取按钮显示顺序
const getInstanceButtonOrder = (status: number) => {
  switch (status) {
    case 0: // 草稿状态
      return ['start', 'edit', 'delete', 'view']
    case 1: // 运行中状态
      return ['approve', 'reject', 'submit', 'withdraw', 'claim', 'release', 'transfer', 'delegate', 'return', 'urge', 'addsign', 'subsign', 'suspend', 'terminate', 'progress', 'history', 'view']
    case 2: // 暂停状态
      return ['resume', 'terminate', 'progress', 'history', 'view']
    case 3: // 已完成状态
      return ['progress', 'history', 'view']
    case 4: // 已终止状态
      return ['progress', 'history', 'view']
    default:
      return ['view']
  }
}

// === 业务逻辑判断函数 ===
// 判断是否可以提交
const canSubmit = (record: TaktInstance) => {
  // 只有发起人或者当前审批人可以提交
  return record.initiatorId === userStore.userInfo?.userId || 
         isCurrentApprover(record)
}

// 判断是否可以撤回
const canWithdraw = (record: TaktInstance) => {
  // 只有发起人且在流程开始阶段可以撤回
  return record.initiatorId === userStore.userInfo?.userId && 
         record.status === 1
}

// 判断是否可以转办
const canTransfer = (record: TaktInstance) => {
  // 当前审批人可以转办
  return isCurrentApprover(record)
}

// 判断是否可以委托
const canDelegate = (record: TaktInstance) => {
  // 当前审批人可以委托
  return isCurrentApprover(record)
}

// 判断是否可以退回
const canReturn = (record: TaktInstance) => {
  // 当前审批人可以退回
  return isCurrentApprover(record)
}

// 判断是否可以加签
const canAddsign = (record: TaktInstance) => {
  // 当前审批人可以加签
  return isCurrentApprover(record)
}

// 判断是否可以减签
const canSubsign = (record: TaktInstance) => {
  // 当前审批人可以减签
  return isCurrentApprover(record)
}

// 判断是否可以审批
const canApprove = (record: TaktInstance) => {
  // 当前审批人可以审批
  return isCurrentApprover(record)
}

// 判断是否可以拒绝
const canReject = (record: TaktInstance) => {
  // 当前审批人可以拒绝
  return isCurrentApprover(record)
}

// 判断是否可以签收
const canClaim = (record: TaktInstance) => {
  // 待签收的流程实例可以签收
  return record.status === 1 && !isCurrentApprover(record)
}

// 判断是否可以释放
const canRelease = (record: TaktInstance) => {
  // 已签收的流程实例可以释放
  return isCurrentApprover(record)
}

// 判断是否是当前审批人
const isCurrentApprover = (record: TaktInstance) => {
  // TODO: 这里需要根据实际的审批人逻辑来判断
  // 可以从 record.CurrentApproverId 或者通过其他方式获取当前审批人
  return false // 临时返回 false，需要根据实际业务逻辑实现
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

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['wf_instance_status', 'wf_instance_priority', 'wf_instance_urgency'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-instance-container {
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
