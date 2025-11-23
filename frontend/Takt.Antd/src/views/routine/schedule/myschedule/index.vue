<template>
  <div class="schedule-container">
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
      :add-permission="['routine:schedule:create']"
      :show-edit="true"
      :edit-permission="['routine:schedule:update']"
      :show-delete="true"
      :delete-permission="['routine:schedule:delete']"
      :show-export="true"
      :export-permission="['routine:schedule:export']"
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
      :columns="columns.filter(col => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 1200 }"
      :default-height="594"
      row-key="scheduleId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{ type: 'checkbox', columnWidth: 60 }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'priority'">
          <Takt-dict-tag dict-type="schedule_priority" :value="record.priority" />
        </template>
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="schedule_status" :value="record.status" />
        </template>
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:schedule:update']"
            :show-delete="true"
            :delete-permission="['routine:schedule:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('schedule.view')">
                <a-button type="link" size="small" @click.stop="handleView(record)">
                  <template #icon><eye-outlined /></template>
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
      :show-total="(total: number) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 日程表单对话框 -->
    <schedule-form
      v-model:visible="formVisible"
      :title="formTitle"
      :schedule-id="selectedId"
      @success="handleSuccess"
    />

    <!-- 查看详情对话框 -->
    <a-modal
      v-model:open="viewVisible"
      :title="t('schedule.view')"
      width="600"
      :footer="null"
    >
      <div class="view-content">
        <a-descriptions :column="1" bordered>
          <a-descriptions-item :label="t('schedule.title')">
            {{ viewData.title }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('schedule.content')">
            {{ viewData.content }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('schedule.location')">
            {{ viewData.location }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('schedule.startTime')">
            {{ viewData.startTime }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('schedule.endTime')">
            {{ viewData.endTime }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('schedule.priority')">
            <Takt-dict-tag dict-type="schedule_priority" :value="viewData.priority" />
          </a-descriptions-item>
          <a-descriptions-item :label="t('schedule.status')">
            <Takt-dict-tag dict-type="schedule_status" :value="viewData.status" />
          </a-descriptions-item>
        </a-descriptions>
      </div>
    </a-modal>

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
        <div v-for="col in columns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, onMounted, h } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { CheckboxValueType } from 'ant-design-vue/es/checkbox/interface'
import { EyeOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktSchedule, TaktScheduleQuery } from '@/types/routine/schedule'
import type { QueryField } from '@/types/components/query'
import { getScheduleList, deleteSchedule, batchDeleteSchedule, exportSchedule } from '@/api/routine/schedule'
import ScheduleForm from './components/ScheduleForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询参数
const queryParams = ref<TaktScheduleQuery>({
  pageIndex: 1,
  pageSize: 10,
  title: undefined,
  status: undefined,
  priority: undefined,
  startTime: undefined,
  endTime: undefined
})

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'title',
    label: t('schedule.title'),
    type: 'input' as const,
    placeholder: t('schedule.placeholder.title')
  },
  {
    name: 'status',
    label: t('schedule.status'),
    type: 'select' as const,
    props: { dictType: 'schedule_status' },
    placeholder: t('schedule.placeholder.status')
  },
  {
    name: 'priority',
    label: t('schedule.priority'),
    type: 'select' as const,
    props: { dictType: 'schedule_priority' },
    placeholder: t('schedule.placeholder.priority')
  },
  {
    name: 'startTime',
    label: t('schedule.startTime'),
    type: 'date' as const,
    placeholder: t('schedule.placeholder.startTime')
  },
  {
    name: 'endTime',
    label: t('schedule.endTime'),
    type: 'date' as const,
    placeholder: t('schedule.placeholder.endTime')
  }
]

// 表格列配置
const columns = [
  { key: 'scheduleId', title: 'ID', dataIndex: 'scheduleId', width: 80, ellipsis: true },
  { key: 'title', title: t('schedule.title'), dataIndex: 'title', width: 180, ellipsis: true },
  { key: 'content', title: t('schedule.content'), dataIndex: 'content', width: 200, ellipsis: true },
  { key: 'location', title: t('schedule.location'), dataIndex: 'location', width: 150, ellipsis: true },
  { key: 'startTime', title: t('schedule.startTime'), dataIndex: 'startTime', width: 120 },
  { key: 'endTime', title: t('schedule.endTime'), dataIndex: 'endTime', width: 120 },
  { key: 'priority', title: t('schedule.priority'), dataIndex: 'priority', width: 100 },
  { key: 'status', title: t('schedule.status'), dataIndex: 'status', width: 100 },
  { key: 'remark', title: t('table.columns.remark'), dataIndex: 'remark', width: 120 },
  { key: 'createBy', title: t('table.columns.createBy'), dataIndex: 'createBy', width: 120 },
  { key: 'createTime', title: t('table.columns.createTime'), dataIndex: 'createTime', width: 180 },
  { key: 'updateBy', title: t('table.columns.updateBy'), dataIndex: 'updateBy', width: 120 },
  { key: 'updateTime', title: t('table.columns.updateTime'), dataIndex: 'updateTime', width: 180 },
  { key: 'action', title: t('table.columns.action'), fixed: 'right', width: 180 }
]

// 表格数据
const tableData = ref<TaktSchedule[]>([])
const loading = ref(false)
const total = ref(0)
const selectedRowKeys = ref<string[]>([])

// 列设置
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// 搜索显示控制
const showSearch = ref(true)

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedId = ref<number>()
// 查看详情对话框
const viewVisible = ref(false)
const viewData = ref<TaktSchedule>({} as TaktSchedule)

// 获取数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getScheduleList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.total
    }
  } catch (error) {
    console.error('获取日程列表失败:', error)
    message.error(t('common.fetchError'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    title: undefined,
    status: undefined,
    priority: undefined,
    startTime: undefined,
    endTime: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 页面大小变化
const handleSizeChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktSchedule) => {
  selectedRowKeys.value = [String(record.scheduleId)]
}

// 新增
const handleAdd = () => {
  formTitle.value = t('schedule.add')
  selectedId.value = undefined
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.pleaseSelectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.scheduleId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: TaktSchedule) => {
  formTitle.value = t('schedule.edit')
  selectedId.value = record.scheduleId
  formVisible.value = true
}

// 查看
const handleView = (record: TaktSchedule) => {
  viewData.value = record
  viewVisible.value = true
}

// 删除
const handleDelete = async (record: TaktSchedule) => {
  try {
    await deleteSchedule(record.scheduleId)
    message.success(t('common.deleteSuccess'))
    fetchData()
  } catch (error) {
    console.error('删除日程失败:', error)
    message.error(t('common.deleteError'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelectRecord'))
    return
  }
  try {
    const ids = selectedRowKeys.value.map(key => Number(key))
    await batchDeleteSchedule(ids)
    message.success(t('common.batchDeleteSuccess'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除日程失败:', error)
    message.error(t('common.batchDeleteError'))
  }
}

// 导出
const handleExport = async () => {
  try {
    const res = await exportSchedule(queryParams.value)
    const blob = new Blob([res.data])
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = '日程数据.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('common.exportSuccess'))
  } catch (error) {
    console.error('导出日程失败:', error)
    message.error(t('common.exportError'))
  }
}

// 表单成功回调
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  columns.forEach(col => {
    settings[col.key] = checkedValues.map(String).includes(String(col.key))
  })
  columnSettings.value = settings
  localStorage.setItem('scheduleColumnSettings', JSON.stringify(settings))
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

// 初始化
onMounted(() => {
  fetchData()
  // 初始化列设置
  columns.forEach(col => {
    columnSettings.value[col.key] = true
  })
})
</script>

<style lang="less" scoped>
.schedule-container {
  padding: 16px;
  background: var(--ant-color-bg-container);
  border-radius: 6px;
}

.view-content {
  padding: 16px;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  display: flex;
  align-items: center;
}
</style> 
