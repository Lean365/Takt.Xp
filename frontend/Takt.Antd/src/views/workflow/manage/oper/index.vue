<template>
  <div class="workflow-oper-container">
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
      :delete-permission="['workflow:manage:oper:delete']"
      :show-export="true"
      :export-permission="['workflow:manage:oper:export']"
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
      :row-key="(record: TaktInstanceOper) => String(record.instanceOperId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 操作类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'operType'">
          <Takt-dict-tag dict-type="wf_instance_oper_type" :value="record.operType" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-delete="true"
            :delete-permission="['workflow:manage:oper:delete']"
            :show-view="true"
            :view-permission="['workflow:manage:oper:query']"
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

    <!-- 操作记录详情对话框 -->
    <instance-oper-detail
      v-model:open="detailVisible"
      :instance-oper-id="selectedOperId"
      @update:open="handleDetailClose"
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
import type { TaktInstanceOper, TaktInstanceOperQuery } from '@/types/workflow/oper'
import { useDictStore } from '@/stores/dictStore'
import { getInstanceOperList, deleteInstanceOper, batchDeleteInstanceOper, exportInstanceOper } from '@/api/workflow/oper'
import InstanceOperDetail from './components/InstanceOperDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'instanceId',
    label: t('workflow.oper.fields.instanceId'),
    type: 'input' as const
  },
  {
    name: 'nodeId',
    label: t('workflow.oper.fields.nodeId'),
    type: 'input' as const
  },
  {
    name: 'nodeName',
    label: t('workflow.oper.fields.nodeName'),
    type: 'input' as const
  },
  {
    name: 'operType',
    label: t('workflow.oper.fields.operType'),
    type: 'select' as const,
    props: {
      dictType: 'wf_instance_oper_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'operatorName',
    label: t('workflow.oper.fields.operatorName'),
    type: 'input' as const
  }
]

// 查询参数
const queryParams = ref<TaktInstanceOperQuery>({
  pageIndex: 1,
  pageSize: 10,
  instanceId: undefined,
  nodeId: '',
  nodeName: '',
  operType: -1,
  operatorName: ''
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktInstanceOper[]>([])

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
    dataIndex: 'instanceOperId',
    key: 'instanceOperId',
    width: 200
  },
  {
    title: t('workflow.oper.fields.instanceId'),
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 200
  },
  {
    title: t('workflow.oper.fields.nodeId'),
    dataIndex: 'nodeId',
    key: 'nodeId',
    width: 150
  },
  {
    title: t('workflow.oper.fields.nodeName'),
    dataIndex: 'nodeName',
    key: 'nodeName',
    width: 200
  },
  {
    title: t('workflow.oper.fields.operType'),
    dataIndex: 'operType',
    key: 'operType',
    width: 150
  },
  {
    title: t('workflow.oper.fields.operData'),
    dataIndex: 'operData',
    key: 'operData',
    width: 200,
    ellipsis: true,
    tooltip: true
  },
  {
    title: t('workflow.oper.fields.operComment'),
    dataIndex: 'operComment',
    key: 'operComment',
    width: 200,
    ellipsis: true
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
const selectedOperId = ref<string | undefined>(undefined)
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

    const res = await getInstanceOperList(queryParams.value)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows || []
      total.value = res.data.totalNum || 0
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取操作记录失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询
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
    instanceId: undefined,
    nodeId: '',
    nodeName: '',
    operType: -1,
    operatorName: ''
  }
  fetchData()
}

// 删除
const handleDelete = async (record: TaktInstanceOper) => {
  try {
    const res = await deleteInstanceOper(record.instanceOperId)
    if (res) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error('删除操作记录失败:', error)
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteInstanceOper(String(id))))
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
    const res = await exportInstanceOper({
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
      fileName = `工作流操作记录_${new Date().getTime()}.xlsx`
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
// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理行点击
const handleRowClick = (record: TaktInstanceOper) => {
  console.log('行点击:', record)
}

// 查看详情
const handleView = (record: TaktInstanceOper) => {
  selectedOperId.value = record.instanceOperId
  detailVisible.value = true
}

// 关闭详情
const handleDetailClose = (value: boolean) => {
  if (!value) {
    selectedOperId.value = undefined
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
    if (!col.key) return
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('workflowOperColumnSettings', JSON.stringify(settings))
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
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
  localStorage.removeItem('workflowOperColumnSettings')
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
  dictStore.loadDicts(['wf_instance_oper_type'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-oper-container {
  height: 100%;
  background-color: var(--ant-color-bg-container);
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  gap: 8px;
}
</style> 
