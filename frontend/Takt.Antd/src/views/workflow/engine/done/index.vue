<template>
  <div class="workflow-done-container">
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
      :show-column-setting="true"
      :show-export="true"
      :export-permission="['workflow:manage:oper:export']"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @export="handleExport"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    >
    </Takt-toolbar>

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="tableData"
      :columns="visibleColumns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: TaktInstanceOper) => String(record.instanceOperId)"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 操作类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'operType'">
          <a-tag :color="getOperTypeColor(record.operType)">
            {{ getOperTypeText(record.operType) }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :show-edit="true"
            :show-delete="true"
            :view-permission="['workflow:manage:oper:query']"
            :edit-permission="['workflow:manage:oper:edit']"
            :delete-permission="['workflow:manage:oper:delete']"
            size="small"
            @view="handleView"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 操作记录详情对话框 -->
    <a-modal
      v-model:open="detailVisible"
      title="操作记录详情"
      width="800px"
      :footer="null"
    >
      <a-descriptions :column="2" bordered>
        <a-descriptions-item label="操作记录ID">
          {{ currentRecord?.instanceOperId }}
        </a-descriptions-item>
        <a-descriptions-item label="操作类型">
          <a-tag :color="getOperTypeColor(currentRecord?.operType)">
            {{ getOperTypeText(currentRecord?.operType) }}
          </a-tag>
        </a-descriptions-item>
        <a-descriptions-item label="实例ID">
          {{ currentRecord?.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item label="节点名称">
          {{ currentRecord?.nodeName || '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="操作人">
          {{ currentRecord?.operatorName }}
        </a-descriptions-item>
        <a-descriptions-item label="创建时间">
          {{ currentRecord?.createTime ? formatDateTime(currentRecord.createTime) : '-' }}
        </a-descriptions-item>
        <a-descriptions-item label="操作意见" :span="2">
          {{ currentRecord?.operOpinion || '无' }}
        </a-descriptions-item>
        <a-descriptions-item label="操作数据" :span="2">
          <pre v-if="currentRecord?.operData" class="oper-data">{{ formatOperData(currentRecord.operData) }}</pre>
          <span v-else>无</span>
        </a-descriptions-item>
        <a-descriptions-item label="备注" :span="2">
          {{ currentRecord?.remark || '无' }}
        </a-descriptions-item>
      </a-descriptions>
    </a-modal>

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
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/userStore'
import type { TaktInstanceOper, TaktInstanceOperQuery } from '@/types/workflow/oper'
import type { TaktWorkflowTask, TaktWorkflowDoneQuery } from '@/types/workflow/engine'
import type { QueryField } from '@/types/components/query'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getInstanceOperList } from '@/api/workflow/oper'
import { getDoneTasks } from '@/api/workflow/engine'
import { formatDateTime } from '@/utils/formatUtil'

const { t } = useI18n()
const router = useRouter()

// 表格列定义
const columns = [
  {
    title: '操作记录ID',
    dataIndex: 'instanceOperId',
    key: 'instanceOperId',
    width: 120
  },
  {
    title: '实例ID',
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 120
  },
  {
    title: '操作类型',
    dataIndex: 'operType',
    key: 'operType',
    width: 100
  },
  {
    title: '节点名称',
    dataIndex: 'nodeName',
    key: 'nodeName',
    width: 150
  },
  {
    title: '操作人',
    dataIndex: 'operatorName',
    key: 'operatorName',
    width: 120
  },
  {
    title: '操作意见',
    dataIndex: 'operOpinion',
    key: 'operOpinion',
    width: 200,
    ellipsis: true
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 160
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right' as const,
    align: 'center' as const
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'operType',
    label: '操作类型',
    type: 'select' as const,
    props: {
      options: [
        { label: '提交', value: 1 },
        { label: '审批', value: 2 },
        { label: '驳回', value: 3 },
        { label: '转办', value: 4 },
        { label: '终止', value: 5 },
        { label: '撤回', value: 6 }
      ],
      placeholder: '请选择操作类型',
      allowClear: true
    }
  },
  {
    name: 'instanceId',
    label: '实例ID',
    type: 'input' as const,
    props: {
      placeholder: '请输入实例ID',
      allowClear: true
    }
  },
  {
    name: 'nodeName',
    label: '节点名称',
    type: 'input' as const,
    props: {
      placeholder: '请输入节点名称',
      allowClear: true
    }
  },
  {
    name: 'createTimeRange',
    label: '创建时间',
    type: 'dateRange' as const,
    props: {
      placeholder: ['开始时间', '结束时间'],
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktInstanceOperQuery>({
  pageIndex: 1,
  pageSize: 10,
  operType: undefined,
  instanceId: undefined,
  nodeName: undefined
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<TaktInstanceOper[]>([])
const showSearch = ref(true)

// 详情对话框
const detailVisible = ref(false)
const currentRecord = ref<TaktInstanceOper | null>(null)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    // 使用新的工作流任务API
    const userStore = useUserStore()
    const userId = userStore.userInfo?.userId?.toString() || ''
    
    if (userId) {
      const doneQuery: TaktWorkflowDoneQuery = {
        pageIndex: queryParams.value.pageIndex,
        pageSize: queryParams.value.pageSize,
        keyword: queryParams.value.nodeName,
        schemeId: undefined,
        nodeId: undefined,
        operType: queryParams.value.operType?.toString()
      }
      
      const res = await getDoneTasks(userId, doneQuery)
      if (res.data) {
        // 转换为TaktInstanceOper格式以保持兼容性
        tableData.value = res.data.map((task: TaktWorkflowTask) => ({
          instanceOperId: task.taskId.toString(),
          instanceId: task.instanceId,
          nodeId: task.nodeId,
          nodeName: task.nodeName,
          operType: 1, // 默认操作类型
          operatorId: (task as any).assigneeId?.toString() || '',
          operatorName: (task as any).assigneeName || '',
          operOpinion: '',
          operData: '',
          operTime: task.updateTime,
          createTime: task.createTime,
          updateTime: task.updateTime,
          createBy: (task as any).createBy || '',
          isDeleted: 0
        } as TaktInstanceOper))
        total.value = res.data.length
      }
    } else {
      // 回退到原有API
      const res = await getInstanceOperList(queryParams.value)
      if (res.data) {
        tableData.value = res.data.rows || []
        total.value = res.data.totalNum || 0
      }
    }
  } catch (error) {
    console.error('获取已办操作记录失败:', error)
    message.error('获取已办操作记录失败')
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    // 处理日期范围
    if (values.createTimeRange && values.createTimeRange.length === 2) {
      values.createTimeStart = values.createTimeRange[0]
      values.createTimeEnd = values.createTimeRange[1]
      delete values.createTimeRange
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
    operType: undefined,
    instanceId: undefined,
    nodeName: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 查看详情
const handleView = (record: TaktInstanceOper) => {
  currentRecord.value = record
  detailVisible.value = true
}

// 编辑
const handleEdit = (record: TaktInstanceOper) => {
  message.info('编辑功能待实现')
  console.log('编辑操作记录:', record)
}

// 删除
const handleDelete = (record: TaktInstanceOper) => {
  message.info('删除功能待实现')
  console.log('删除操作记录:', record)
}

// 导出数据
const handleExport = () => {
  message.info('导出功能待实现')
  console.log('导出操作记录数据')
}

// 获取操作类型颜色
const getOperTypeColor = (operType?: number) => {
  switch (operType) {
    case 1:
      return 'blue'
    case 2:
      return 'green'
    case 3:
      return 'red'
    case 4:
      return 'orange'
    case 5:
      return 'purple'
    case 6:
      return 'cyan'
    default:
      return 'default'
  }
}

// 获取操作类型文本
const getOperTypeText = (operType?: number) => {
  switch (operType) {
    case 1:
      return '提交'
    case 2:
      return '审批'
    case 3:
      return '驳回'
    case 4:
      return '转办'
    case 5:
      return '终止'
    case 6:
      return '撤回'
    default:
      return '未知'
  }
}

// 格式化操作数据
const formatOperData = (operData: string) => {
  try {
    const data = JSON.parse(operData)
    return JSON.stringify(data, null, 2)
  } catch {
    return operData
  }
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 初始化列设置
const initColumnSettings = () => {
  // 初始化所有列为true
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
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
}

// 处理行点击
const handleRowClick = (record: TaktInstanceOper) => {
  console.log('行点击:', record)
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

// 在组件挂载时初始化
onMounted(() => {
  initColumnSettings()
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-done-container {
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

.oper-data {
  background-color: #f5f5f5;
  padding: 8px;
  border-radius: 4px;
  font-size: 12px;
  line-height: 1.4;
  max-height: 200px;
  overflow-y: auto;
  white-space: pre-wrap;
  word-wrap: break-word;
}
</style> 
