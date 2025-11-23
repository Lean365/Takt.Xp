<template>
  <div class="my-workflow-container">
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
      :show-export="true"
      :export-permission="['workflow:manage:instance:export']"
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
    >
      <!-- 状态列 -->
      <template #status="{ record }">
        <a-tag :color="getStatusColor(record.status)">
          {{ getStatusText(record.status) }}
        </a-tag>
      </template>

      <!-- 优先级列 -->
      <template #priority="{ record }">
        <a-tag :color="getPriorityColor(record.priority)">
          {{ getPriorityText(record.priority) }}
        </a-tag>
      </template>

      <!-- 紧急程度列 -->
      <template #urgency="{ record }">
        <a-tag :color="getUrgencyColor(record.urgency)">
          {{ getUrgencyText(record.urgency) }}
        </a-tag>
      </template>

      <!-- 操作列 -->
      <template #action="{ record }">
        <Takt-operation
          :show-view="true"
          :show-edit="true"
          :show-delete="true"
          :view-permission="['workflow:manage:instance:query']"
          :edit-permission="['workflow:manage:instance:update']"
          :delete-permission="['workflow:manage:instance:delete']"
          @view="handleView(record)"
          @edit="handleEdit(record)"
          @delete="handleDelete(record)"
        />
      </template>
    </Takt-table>

    <!-- 分页 -->
    <Takt-pagination
      v-model:current="pagination.current"
      v-model:page-size="pagination.pageSize"
      :total="pagination.total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', `共 ${total} 条记录`)"
      @change="handlePageChange"
      @show-size-change="handlePageSizeChange"
    />

    <!-- 详情对话框 -->
    <InstanceDetail
      v-model:visible="detailVisible"
      :instance-id="currentInstanceId"
      @refresh="fetchData"
    />


    <!-- 流程发起对话框 -->
    <WorkflowLaunch
      v-model:visible="launchVisible"
      @success="handleLaunchSuccess"
    />

  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed, h } from 'vue'
import { message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/userStore'
import type { TaktInstance, TaktInstanceQuery } from '@/types/workflow/instance'
import type { TaktPagedResult } from '@/types/common'
import type { QueryField } from '@/types/components/query'
import { 
  getMyInstances, 
  deleteInstance, 
  batchDeleteInstance, 
  exportInstance
} from '@/api/workflow/instance'
import InstanceDetail from './components/InstanceDetail.vue'
import WorkflowLaunch from './components/WorkflowLaunch.vue'

// 国际化
const { t } = useI18n()

// 用户状态
const userStore = useUserStore()

// 响应式数据
const loading = ref(false)
const tableData = ref<TaktInstance[]>([])
const selectedRowKeys = ref<string[]>([])
const showSearch = ref(true)
const detailVisible = ref(false)
const launchVisible = ref(false)
const currentInstanceId = ref<string>('')

// 分页数据
const pagination = reactive({
  current: 1,
  pageSize: 10,
  total: 0
})

// 查询参数
const queryParams = reactive<TaktInstanceQuery>({
  instanceTitle: '',
  businessKey: '',
  status: undefined,
  priority: undefined,
  urgency: undefined,
  startTime: '',
  endTime: '',
  pageIndex: 1,
  pageSize: 10
})

// 查询字段配置
const queryFields = computed((): QueryField[] => [
  {
    name: 'instanceTitle',
    label: t('workflow.instance.title'),
    type: 'input',
    placeholder: t('workflow.instance.titlePlaceholder')
  },
  {
    name: 'businessKey',
    label: t('workflow.instance.businessKey'),
    type: 'input',
    placeholder: t('workflow.instance.businessKeyPlaceholder')
  },
  {
    name: 'status',
    label: t('workflow.instance.status'),
    type: 'select',
    placeholder: t('workflow.instance.statusPlaceholder'),
    options: [
      { label: t('workflow.instance.status.draft'), value: 0 },
      { label: t('workflow.instance.status.running'), value: 1 },
      { label: t('workflow.instance.status.completed'), value: 2 },
      { label: t('workflow.instance.status.stopped'), value: 3 }
    ]
  },
  {
    name: 'priority',
    label: t('workflow.instance.priority'),
    type: 'select',
    placeholder: t('workflow.instance.priorityPlaceholder'),
    options: [
      { label: t('workflow.instance.priority.low'), value: 1 },
      { label: t('workflow.instance.priority.normal'), value: 2 },
      { label: t('workflow.instance.priority.high'), value: 3 },
      { label: t('workflow.instance.priority.urgent'), value: 4 },
      { label: t('workflow.instance.priority.critical'), value: 5 }
    ]
  },
  {
    name: 'urgency',
    label: t('workflow.instance.urgency'),
    type: 'select',
    placeholder: t('workflow.instance.urgencyPlaceholder'),
    options: [
      { label: t('workflow.instance.urgency.normal'), value: 1 },
      { label: t('workflow.instance.urgency.urgent'), value: 2 },
      { label: t('workflow.instance.urgency.critical'), value: 3 }
    ]
  },
  {
    name: 'timeRange',
    label: t('workflow.instance.timeRange'),
    type: 'dateRange',
    placeholder: t('workflow.instance.timeRangePlaceholder')
  }
])

// 表格列配置
const columns = computed(() => [
  {
    key: 'instanceTitle',
    title: t('workflow.instance.title'),
    dataIndex: 'instanceTitle',
    width: 200,
    ellipsis: true
  },
  {
    key: 'businessKey',
    title: t('workflow.instance.businessKey'),
    dataIndex: 'businessKey',
    width: 150,
    ellipsis: true
  },
  {
    key: 'currentNodeName',
    title: t('workflow.instance.currentNode'),
    dataIndex: 'currentNodeName',
    width: 150,
    ellipsis: true
  },
  {
    key: 'status',
    title: t('workflow.instance.status'),
    dataIndex: 'status',
    width: 100,
    slots: { customRender: 'status' }
  },
  {
    key: 'priority',
    title: t('workflow.instance.priority'),
    dataIndex: 'priority',
    width: 100,
    slots: { customRender: 'priority' }
  },
  {
    key: 'urgency',
    title: t('workflow.instance.urgency'),
    dataIndex: 'urgency',
    width: 100,
    slots: { customRender: 'urgency' }
  },
  {
    key: 'startTime',
    title: t('workflow.instance.startTime'),
    dataIndex: 'startTime',
    width: 160
  },
  {
    key: 'endTime',
    title: t('workflow.instance.endTime'),
    dataIndex: 'endTime',
    width: 160
  },
  {
    key: 'createTime',
    title: t('common.createTime'),
    dataIndex: 'createTime',
    width: 160
  },
  {
    key: 'action',
    title: t('common.action'),
    width: 120,
    fixed: 'right',
    slots: { customRender: 'action' }
  }
])

// 列设置
const columnSettings = ref<Record<string, boolean>>({
  instanceTitle: true,
  businessKey: true,
  currentNodeName: true,
  status: true,
  priority: true,
  urgency: true,
  startTime: true,
  endTime: true,
  createTime: true,
  action: true
})

// 获取状态颜色
const getStatusColor = (status: number) => {
  const colors = {
    0: 'default',    // 草稿
    1: 'processing', // 运行中
    2: 'success',    // 已完成
    3: 'error'       // 已停用
  }
  return colors[status as keyof typeof colors] || 'default'
}

// 获取状态文本
const getStatusText = (status: number) => {
  const texts = {
    0: t('workflow.instance.status.draft'),
    1: t('workflow.instance.status.running'),
    2: t('workflow.instance.status.completed'),
    3: t('workflow.instance.status.stopped')
  }
  return texts[status as keyof typeof texts] || t('common.unknown')
}

// 获取优先级颜色
const getPriorityColor = (priority: number) => {
  const colors = {
    1: 'default',    // 低
    2: 'blue',       // 普通
    3: 'orange',     // 高
    4: 'red',        // 紧急
    5: 'purple'      // 特急
  }
  return colors[priority as keyof typeof colors] || 'default'
}

// 获取优先级文本
const getPriorityText = (priority: number) => {
  const texts = {
    1: t('workflow.instance.priority.low'),
    2: t('workflow.instance.priority.normal'),
    3: t('workflow.instance.priority.high'),
    4: t('workflow.instance.priority.urgent'),
    5: t('workflow.instance.priority.critical')
  }
  return texts[priority as keyof typeof texts] || t('common.unknown')
}

// 获取紧急程度颜色
const getUrgencyColor = (urgency: number) => {
  const colors = {
    1: 'default',    // 普通
    2: 'orange',     // 加急
    3: 'red'         // 特急
  }
  return colors[urgency as keyof typeof colors] || 'default'
}

// 获取紧急程度文本
const getUrgencyText = (urgency: number) => {
  const texts = {
    1: t('workflow.instance.urgency.normal'),
    2: t('workflow.instance.urgency.urgent'),
    3: t('workflow.instance.urgency.critical')
  }
  return texts[urgency as keyof typeof texts] || t('common.unknown')
}

// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    const params = {
      ...queryParams,
      pageNum: pagination.current,
      pageSize: pagination.pageSize
    }
    
    const response = await getMyInstances(params)
    if (response.code === 200) {
      tableData.value = response.data.rows || []
      pagination.total = response.data.totalNum || 0
    } else {
      message.error(response.msg || t('common.loadFailed'))
    }
  } catch (error) {
    console.error('获取我的流程实例失败:', error)
    message.error(t('common.loadFailed'))
  } finally {
    loading.value = false
  }
}

// 查询处理
const handleQuery = (params: any) => {
  Object.assign(queryParams, params)
  pagination.current = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  Object.assign(queryParams, {
    instanceTitle: '',
    businessKey: '',
    status: undefined,
    priority: undefined,
    urgency: undefined,
    startTime: '',
    endTime: '',
    pageNum: 1,
    pageSize: 10
  })
  pagination.current = 1
  fetchData()
}

// 表格变化处理
const handleTableChange = (pagination: any) => {
  // 这里可以处理表格排序、筛选等变化
}

// 分页变化处理
const handlePageChange = (page: number) => {
  pagination.current = page
  fetchData()
}

// 页面大小变化处理
const handlePageSizeChange = (current: number, size: number) => {
  pagination.current = current
  pagination.pageSize = size
  fetchData()
}

// 新增处理
const handleAdd = () => {
  launchVisible.value = true
}

// 流程发起成功处理
const handleLaunchSuccess = () => {
  fetchData()
}

// 编辑处理
const handleEdit = (record: TaktInstance) => {
  currentInstanceId.value = record.instanceId
  detailVisible.value = true
}

// 编辑选中处理
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => item.instanceId === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}

// 查看处理
const handleView = (record: TaktInstance) => {
  currentInstanceId.value = record.instanceId
  detailVisible.value = true
}

// 删除处理
const handleDelete = async (record: TaktInstance) => {
  try {
    await deleteInstance(record.instanceId)
    message.success(t('common.deleteSuccess'))
    fetchData()
  } catch (error) {
    console.error('删除流程实例失败:', error)
    message.error(t('common.deleteFailed'))
  }
}

// 批量删除处理
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelect'))
    return
  }
  
  try {
    await batchDeleteInstance(selectedRowKeys.value)
    message.success(t('common.batchDeleteSuccess'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除流程实例失败:', error)
    message.error(t('common.batchDeleteFailed'))
  }
}


// 导出处理
const handleExport = async () => {
  try {
    const response = await exportInstance(queryParams)
    const blob = new Blob([response.data], { 
      type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' 
    })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `我的流程实例_${new Date().toISOString().slice(0, 10)}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.exportSuccess'))
  } catch (error) {
    console.error('导出流程实例失败:', error)
    message.error(t('common.exportFailed'))
  }
}

// 列设置处理
const handleColumnSetting = () => {
  // 这里可以实现列设置功能
  message.info('列设置功能待实现')
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // 这里可以实现全屏功能
  message.info('全屏功能待实现')
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.my-workflow-container {
  padding: 16px;
  background: #fff;
  border-radius: 6px;
}
</style>

