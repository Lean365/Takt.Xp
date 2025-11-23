//===================================================================
// 项目名 : Takt.Xp
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 标准工时管理页面
//===================================================================

<template>
  <div class="standard-time-container">
    <!-- 查询区域 -->
    <Takt-query
      v-model:show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['logistics:production:master:standardtime:create']"
      :show-edit="true"
      :edit-permission="['logistics:production:master:standardtime:update']"
      :show-delete="true"
      :delete-permission="['logistics:production:master:standardtime:delete']"
      :show-import="true"
      :import-permission="['logistics:production:master:standardtime:import']"
      :show-export="true"
      :export-permission="['logistics:production:master:standardtime:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
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
      :row-key="(record: TaktStandardTime) => String(record.standardTimeId)"
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
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['logistics:production:master:standardtime:query']"
            :show-edit="true"
            :edit-permission="['logistics:production:master:standardtime:update']"
            :show-delete="true"
            :delete-permission="['logistics:production:master:standardtime:delete']"
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

    <!-- 标准工时表单对话框 -->
    <standard-time-form
      v-model:open="formVisible"
      :title="formTitle"
      :standard-time-id="selectedStandardTimeId"
      @success="handleSuccess"
    />

    <!-- 标准工时详情对话框 -->
    <standard-time-detail
      v-model:open="detailVisible"
      :standard-time-id="selectedStandardTimeId"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { TaktStandardTime, TaktStandardTimeQuery } from '@/types/logistics/manufacturing/master/standardTime'
import type { QueryField } from '@/types/components/query'
import {
  getStandardTimeList,
  getStandardTimeById,
  createStandardTime,
  updateStandardTime,
  deleteStandardTime,
  batchDeleteStandardTime,
  importStandardTime,
  exportStandardTime,
  getStandardTimeTemplate
} from '@/api/logistics/manufacturing/master/standardTime'
import StandardTimeForm from './components/StandardTimeForm.vue'
import StandardTimeDetail from './components/StandardTimeDetail.vue'

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const showSearch = ref(false)
const tableData = ref<TaktStandardTime[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedStandardTimeId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const columnSettingVisible = ref(false)
const total = ref(0)

// === 查询参数 ===
const queryParams = ref<TaktStandardTimeQuery>({
  pageIndex: 1,
  pageSize: 10,
  materialCode: '',
  workCenter: '',
  status: -1,
  createBy: '',
  createTime: '',
  isDeleted: 0
})

// === 查询字段定义 ===
const queryFields: QueryField[] = [
  {
    name: 'materialCode',
    label: t('logistics.standardTime.fields.materialCode.label'),
    placeholder: t('logistics.standardTime.fields.materialCode.placeholder'),
    type: 'input' as const
  },
  {
    name: 'workCenter',
    label: t('logistics.standardTime.fields.workCenter.label'),
    placeholder: t('logistics.standardTime.fields.workCenter.placeholder'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('logistics.standardTime.fields.status.label'),
    placeholder: t('logistics.standardTime.fields.status.placeholder'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// === 表格列定义 ===
const defaultColumns = [
  {
    title: t('logistics.standardTime.table.columns.materialCode'),
    dataIndex: 'materialCode',
    key: 'materialCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.standardTime.table.columns.workCenter'),
    dataIndex: 'workCenter',
    key: 'workCenter',
    width: 120
  },
  {
    title: t('logistics.standardTime.table.columns.standardMinutes'),
    dataIndex: 'standardMinutes',
    key: 'standardMinutes',
    width: 120
  },
  {
    title: t('logistics.standardTime.table.columns.standardShorts'),
    dataIndex: 'standardShorts',
    key: 'standardShorts',
    width: 100
  },
  {
    title: t('logistics.standardTime.table.columns.timeUnit'),
    dataIndex: 'timeUnit',
    key: 'timeUnit',
    width: 100
  },
  {
    title: t('logistics.standardTime.table.columns.shortsUnit'),
    dataIndex: 'shortsUnit',
    key: 'shortsUnit',
    width: 100
  },
  {
    title: t('logistics.standardTime.table.columns.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('table.columns.action'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// === 列设置状态 ===
const columnSettings = ref<Record<string, boolean>>({
  materialCode: true,
  workCenter: true,
  standardMinutes: true,
  standardShorts: true,
  timeUnit: true,
  shortsUnit: true,
  status: true,
  createTime: true,
  action: true
})

// === 计算属性 ===
const visibleColumns = computed(() => 
  defaultColumns.filter(col => col.key && columnSettings.value[col.key])
)

// === 方法定义 ===
const fetchData = async () => {
  loading.value = true
  try {
    const response = await getStandardTimeList(queryParams.value)
    tableData.value = response.data.data.rows
    total.value = response.data.data.totalNum
  } catch (error) {
    message.error('获取数据失败')
  } finally {
    loading.value = false
  }
}

const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    materialCode: '',
    workCenter: '',
    status: -1,
    createBy: '',
    createTime: '',
    isDeleted: 0
  }
  fetchData()
}

const handleAdd = () => {
  selectedStandardTimeId.value = undefined
  formTitle.value = '新增标准工时'
  formVisible.value = true
}

const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => String(item.standardTimeId) === selectedRowKeys.value[0])
    if (record) {
      selectedStandardTimeId.value = record.standardTimeId
      formTitle.value = '编辑标准工时'
      formVisible.value = true
    }
  }
}

const handleEdit = (record: TaktStandardTime) => {
  selectedStandardTimeId.value = record.standardTimeId
  formTitle.value = '编辑标准工时'
  formVisible.value = true
}

const handleView = (record: TaktStandardTime) => {
  selectedStandardTimeId.value = record.standardTimeId
  detailVisible.value = true
}

const handleDelete = async (record: TaktStandardTime) => {
  try {
    await deleteStandardTime(record.standardTimeId)
    message.success('删除成功')
    fetchData()
  } catch (error) {
    message.error('删除失败')
  }
}

const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的数据')
    return
  }
  
  try {
    const ids = selectedRowKeys.value.map(key => parseInt(key))
    await batchDeleteStandardTime(ids)
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

const handleImport = () => {
  // 导入功能实现
  message.info('导入功能待实现')
}

const handleTemplate = () => {
  // 下载模板功能实现
  message.info('模板下载功能待实现')
}

const handleExport = () => {
  // 导出功能实现
  message.info('导出功能待实现')
}

const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

const handleTableChange = (pagination: TablePaginationConfig) => {
  if (pagination.current) {
    queryParams.value.pageIndex = pagination.current
  }
  if (pagination.pageSize) {
    queryParams.value.pageSize = pagination.pageSize
  }
  fetchData()
}

const handleRowClick = (record: TaktStandardTime) => {
  // 行点击处理
}

const handlePageChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = 1
  queryParams.value.pageSize = size
  fetchData()
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnSettingChange = (checkedValues: any[]) => {
  Object.keys(columnSettings.value).forEach(key => {
    columnSettings.value[key] = checkedValues.includes(key)
  })
}

const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

const toggleFullscreen = () => {
  // 全屏功能实现
}

// === 生命周期 ===
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.standard-time-container {
  padding: 16px;
  background-color: var(--ant-color-bg-layout);
  min-height: calc(100vh - 64px);

  .column-setting-group {
    .column-setting-item {
      margin-bottom: 8px;
    }
  }
}
</style>


