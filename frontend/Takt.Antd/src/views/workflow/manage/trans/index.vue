<template>
  <div class="workflow-trans-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-delete="true"
      :delete-permission="['workflow:manage:trans:delete']"
      :show-export="true"
      :export-permission="['workflow:manage:trans:export']"
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
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: TaktInstanceTrans) => String(record.instanceTransId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 流转类型列 -->
      <template #bodyCell="{ column, record }">
        <!-- 开始节点类型列 -->
        <template v-if="column.dataIndex === 'startNodeType'">
          <Takt-dict-tag dict-type="wf_node_type" :value="record.startNodeType" />
        </template>

        <!-- 目标节点类型列 -->
        <template v-if="column.dataIndex === 'toNodeType'">
          <Takt-dict-tag dict-type="wf_node_type" :value="record.toNodeType" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="wf_instance_trans_status" :value="record.status" />
        </template>
      

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-delete="true"
            :delete-permission="['workflow:manage:trans:delete']"
            :show-view="true"
            :view-permission="['workflow:manage:trans:query']"
            size="small"
            @delete="handleDelete"
            @view="handleView"
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

    <!-- 查看详情 -->
    <instance-trans-detail
      v-model:open="detailVisible"
      :instance-trans-id="selectedTransId"
    />

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
import type { TaktInstanceTrans, TaktInstanceTransQuery } from '@/types/workflow/trans'
import { useDictStore } from '@/stores/dictStore'
import { getInstanceTransList, deleteInstanceTrans, batchDeleteInstanceTrans, exportInstanceTrans } from '@/api/workflow/trans'
import InstanceTransDetail from './components/InstanceTransDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'startNodeName',
    label: t('workflow.trans.fields.startNodeName'),
    type: 'input' as const
  },
  {
    name: 'toNodeName',
    label: t('workflow.trans.fields.toNodeName'),
    type: 'input' as const
  },
  {
    name: 'startNodeType',
    label: t('workflow.trans.fields.startNodeType'),
    type: 'select' as const,
    props: {
      placeholder: '请选择开始节点类型',
      dictType: 'wf_node_type'
    }
  },
  {
    name: 'toNodeType',
    label: t('workflow.trans.fields.toNodeType'),
    type: 'select' as const,
    props: {
      placeholder: '请选择目标节点类型',
      dictType: 'wf_node_type'
    }
  },
  {
    name: 'status',
    label: t('workflow.trans.fields.status'),
    type: 'select' as const,
    props: {
      placeholder: '请选择状态',
      dictType: 'wf_instance_trans_status'
    }
  },
  {
    name: 'transTimeRange',
    label: t('workflow.trans.fields.transTime'),
    type: 'dateRange' as const,
    props: {
      placeholder: ['开始时间', '结束时间'],
      showTime: true,
      format: 'YYYY-MM-DD HH:mm:ss'
    }
  },
]

// 查询参数
const queryParams = ref<TaktInstanceTransQuery>({
  pageIndex: 1,
  pageSize: 10,
  startNodeName: '',
  toNodeName: '',
  startNodeType: undefined,
  toNodeType: undefined,
  status: undefined,
  transTimeStart: undefined,
  transTimeEnd: undefined
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktInstanceTrans[]>([])

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
    dataIndex: 'instanceTransId',
    key: 'instanceTransId',
    width: 200
  },
  {
    title: t('workflow.trans.fields.instanceId'),
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 200
  },
  {
    title: t('workflow.trans.fields.startNodeId'),
    dataIndex: 'startNodeId',
    key: 'startNodeId',
    width: 150
  },
  {
    title: t('workflow.trans.fields.startNodeName'),
    dataIndex: 'startNodeName',
    key: 'startNodeName',
    width: 200
  },
  {
    title: t('workflow.trans.fields.startNodeType'),
    dataIndex: 'startNodeType',
    key: 'startNodeType',
    width: 150
  },
  {
    title: t('workflow.trans.fields.toNodeId'),
    dataIndex: 'toNodeId',
    key: 'toNodeId',
    width: 150
  },
  {
    title: t('workflow.trans.fields.toNodeName'),
    dataIndex: 'toNodeName',
    key: 'toNodeName',
    width: 200
  },
  {
    title: t('workflow.trans.fields.toNodeType'),
    dataIndex: 'toNodeType',
    key: 'toNodeType',
    width: 150
  },
  {
    title: t('workflow.trans.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 150
  },
  {
    title: t('workflow.trans.fields.transTime'),
    dataIndex: 'transTime',
    key: 'transTime',
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

// 对话框相关
const selectedTransId = ref<string | undefined>(undefined)
const detailVisible = ref(false)

// 列设置相关
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})
const defaultColumns = columns

// 可见列
const visibleColumns = computed(() => {
  return columns.filter(col => columnSettings.value[col.key])
})

// ==================== 数据获取方法 ====================
// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getInstanceTransList(queryParams.value)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows || []
      total.value = res.data.totalNum || 0
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取流转历史失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    // 处理时间范围查询
    if (values.transTimeRange && values.transTimeRange.length === 2) {
      queryParams.value.transTimeStart = values.transTimeRange[0]
      queryParams.value.transTimeEnd = values.transTimeRange[1]
      delete values.transTimeRange
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
    startNodeName: '',
    toNodeName: '',
    startNodeType: undefined,
    toNodeType: undefined,
    status: undefined,
    transTimeStart: undefined,
    transTimeEnd: undefined
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
const handleDelete = async (record: TaktInstanceTrans) => {
  try {
    const res = await deleteInstanceTrans(record.instanceTransId)
    if (res) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error('删除流转历史失败:', error)
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteInstanceTrans(String(id))))
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

// ==================== 导出方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportInstanceTrans({
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
      fileName = `工作流流转历史_${new Date().getTime()}.xlsx`
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

// ==================== 其它功能 ====================
// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    if (!col.key) return
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('workflowTransColumnSettings', JSON.stringify(settings))
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
const handleRowClick = (record: TaktInstanceTrans) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: TaktInstanceTrans) => {
  selectedTransId.value = record.instanceTransId
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

// 初始化列设置
const initColumnSettings = () => {
  localStorage.removeItem('workflowTransColumnSettings')
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key!, false]))
  const firstColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)
  firstColumns.forEach(col => {
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })
  columnSettings.value['action'] = true
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['wf_instance_trans_type', 'wf_instance_trans_result'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-trans-container {
  height: 100%;
  background-color: var(--ant-color-bg-container);
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  gap: 8px;
}
</style> 
