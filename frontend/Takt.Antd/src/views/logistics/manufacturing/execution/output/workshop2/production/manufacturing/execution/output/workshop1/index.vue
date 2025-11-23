<template>
  <div class="assy-mp-output-container">
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
      :add-permission="['logistics:production:outputs:assy:create']"
      :show-edit="true"
      :edit-permission="['logistics:production:outputs:assy:update']"
      :show-delete="true"
      :delete-permission="['logistics:production:outputs:assy:delete']"
      :show-import="true"
      :import-permission="['logistics:production:outputs:assy:import']"
      :show-export="true"
      :export-permission="['logistics:production:outputs:assy:export']"
      :show-template="true"
      :template-permission="['logistics:production:outputs:assy:template']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @export="handleExport"
      @template="handleTemplate"
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
      :scroll="{ x: 1500 }"
      :default-height="594"
      row-key="assyMpOutputId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{ type: 'checkbox', columnWidth: 60 }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'shiftNo'">
          <Takt-dict-tag dict-type="shift_type" :value="record.shiftNo" />
        </template>
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['logistics:production:outputs:assy:update']"
            :show-delete="true"
            :delete-permission="['logistics:production:outputs:assy:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('assyMpOutput.view')">
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

    <!-- 组立日报表单对话框 -->
    <assy-mp-output-form
      v-model:visible="formVisible"
      :title="formTitle"
      :assy-mp-output-id="selectedId"
      @success="handleSuccess"
    />

    <!-- 查看详情对话框 -->
    <a-modal
      v-model:visible="viewVisible"
      :title="t('assyMpOutput.view')"
      width="800"
      :footer="null"
    >
      <div class="view-content">
        <a-descriptions :column="2" bordered>
          <a-descriptions-item :label="t('assyMpOutput.plantCode')">
            {{ viewData.plantCode }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.prodDate')">
            {{ viewData.prodDate }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.prodLine')">
            {{ viewData.prodLine }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.shiftNo')">
            <Takt-dict-tag dict-type="shift_type" :value="viewData.shiftNo" />
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.prodOrderCode')">
            {{ viewData.prodOrderCode }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.modelCode')">
            {{ viewData.modelCode }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.materialCode')">
            {{ viewData.materialCode }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.batchNo')">
            {{ viewData.batchNo || '-' }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.directLabor')">
            {{ viewData.directLabor }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.indirectLabor')">
            {{ viewData.indirectLabor }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.orderQty')">
            {{ viewData.orderQty }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.stdMinutes')">
            {{ viewData.stdMinutes }}
          </a-descriptions-item>
          <a-descriptions-item :label="t('assyMpOutput.stdCapacity')">
            {{ viewData.stdCapacity }}
          </a-descriptions-item>
        </a-descriptions>

        <!-- 明细数据 -->
        <div v-if="viewData.assyMpOutputDetails && viewData.assyMpOutputDetails.length > 0" class="detail-section">
          <h3>{{ t('assyMpOutput.details') }}</h3>
          <a-table
            :data-source="viewData.assyMpOutputDetails"
            :columns="detailColumns"
            :pagination="false"
            size="small"
            bordered
          />
        </div>
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
import type { TaktAssyMpOutput, TaktAssyMpOutputQuery } from '@/types/logistics/production/outputs/assy/assyMpOutput'
import type { QueryField } from '@/types/components/query'
import { getAssyMpOutputList, deleteAssyMpOutput, batchDeleteAssyMpOutput, exportAssyMpOutput, getAssyMpOutputTemplate, importAssyMpOutput } from '@/api/logistics/production/outputs/assy/assyMpOutput'
import AssyMpOutputForm from './components/AssyMpOutputForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询参数
const queryParams = ref<TaktAssyMpOutputQuery>({
  pageIndex: 1,
  pageSize: 10,
  plantCode: undefined,
  prodLine: undefined,
  prodOrderCode: undefined,
  modelCode: undefined,
  materialCode: undefined,
  shiftNo: undefined,
  startDate: undefined,
  endDate: undefined
})

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'plantCode',
    label: t('assyMpOutput.plantCode'),
    type: 'input' as const,
    placeholder: t('assyMpOutput.placeholder.plantCode')
  },
  {
    name: 'prodLine',
    label: t('assyMpOutput.prodLine'),
    type: 'input' as const,
    placeholder: t('assyMpOutput.placeholder.prodLine')
  },
  {
    name: 'prodOrderCode',
    label: t('assyMpOutput.prodOrderCode'),
    type: 'input' as const,
    placeholder: t('assyMpOutput.placeholder.prodOrderCode')
  },
  {
    name: 'modelCode',
    label: t('assyMpOutput.modelCode'),
    type: 'input' as const,
    placeholder: t('assyMpOutput.placeholder.modelCode')
  },
  {
    name: 'materialCode',
    label: t('assyMpOutput.materialCode'),
    type: 'input' as const,
    placeholder: t('assyMpOutput.placeholder.materialCode')
  },
  {
    name: 'shiftNo',
    label: t('assyMpOutput.shiftNo'),
    type: 'select' as const,
    props: { dictType: 'shift_type' },
    placeholder: t('assyMpOutput.placeholder.shiftNo')
  },
  {
    name: 'startDate',
    label: t('assyMpOutput.startDate'),
    type: 'date' as const,
    placeholder: t('assyMpOutput.placeholder.startDate')
  },
  {
    name: 'endDate',
    label: t('assyMpOutput.endDate'),
    type: 'date' as const,
    placeholder: t('assyMpOutput.placeholder.endDate')
  }
]

// 表格列配置
const columns = [
  { key: 'assyMpOutputId', title: t('assyMpOutput.assyMpOutputId'), dataIndex: 'assyMpOutputId', width: 100 },
  { key: 'plantCode', title: t('assyMpOutput.plantCode'), dataIndex: 'plantCode', width: 120 },
  { key: 'prodDate', title: t('assyMpOutput.prodDate'), dataIndex: 'prodDate', width: 120 },
  { key: 'prodLine', title: t('assyMpOutput.prodLine'), dataIndex: 'prodLine', width: 120 },
  { key: 'shiftNo', title: t('assyMpOutput.shiftNo'), dataIndex: 'shiftNo', width: 100 },
  { key: 'prodOrderCode', title: t('assyMpOutput.prodOrderCode'), dataIndex: 'prodOrderCode', width: 150 },
  { key: 'modelCode', title: t('assyMpOutput.modelCode'), dataIndex: 'modelCode', width: 120 },
  { key: 'materialCode', title: t('assyMpOutput.materialCode'), dataIndex: 'materialCode', width: 120 },
  { key: 'batchNo', title: t('assyMpOutput.batchNo'), dataIndex: 'batchNo', width: 120 },
  { key: 'directLabor', title: t('assyMpOutput.directLabor'), dataIndex: 'directLabor', width: 100 },
  { key: 'indirectLabor', title: t('assyMpOutput.indirectLabor'), dataIndex: 'indirectLabor', width: 100 },
  { key: 'orderQty', title: t('assyMpOutput.orderQty'), dataIndex: 'orderQty', width: 100 },
  { key: 'stdMinutes', title: t('assyMpOutput.stdMinutes'), dataIndex: 'stdMinutes', width: 100 },
  { key: 'stdCapacity', title: t('assyMpOutput.stdCapacity'), dataIndex: 'stdCapacity', width: 100 },
  { key: 'createTime', title: t('table.createTime'), dataIndex: 'createTime', width: 160 },
  { key: 'action', title: t('table.action'), dataIndex: 'action', width: 150, fixed: 'right' }
]

// 明细表格列配置
const detailColumns = [
  { key: 'timePeriod', title: t('assyMpOutputDetail.timePeriod'), dataIndex: 'timePeriod', width: 120 },
  { key: 'actualQty', title: t('assyMpOutputDetail.actualQty'), dataIndex: 'actualQty', width: 100 },
  { key: 'downtimeMinutes', title: t('assyMpOutputDetail.downtimeMinutes'), dataIndex: 'downtimeMinutes', width: 120 },
  { key: 'downtimeReason', title: t('assyMpOutputDetail.downtimeReason'), dataIndex: 'downtimeReason', width: 150 },
  { key: 'failureReason', title: t('assyMpOutputDetail.failureReason'), dataIndex: 'failureReason', width: 150 },
  { key: 'inputMinutes', title: t('assyMpOutputDetail.inputMinutes'), dataIndex: 'inputMinutes', width: 100 },
  { key: 'prodMinutes', title: t('assyMpOutputDetail.prodMinutes'), dataIndex: 'prodMinutes', width: 100 },
  { key: 'actualMinutes', title: t('assyMpOutputDetail.actualMinutes'), dataIndex: 'actualMinutes', width: 100 },
  { key: 'efficiencyRate', title: t('assyMpOutputDetail.efficiencyRate'), dataIndex: 'efficiencyRate', width: 100 }
]

// 响应式数据
const loading = ref(false)
const tableData = ref<TaktAssyMpOutput[]>([])
const total = ref(0)
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)
const formVisible = ref(false)
const formTitle = ref('')
const selectedId = ref<number | null>(null)
const viewVisible = ref(false)
const viewData = ref<TaktAssyMpOutput>({} as TaktAssyMpOutput)
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({
  assyMpOutputId: true,
  plantCode: true,
  prodDate: true,
  prodLine: true,
  shiftNo: true,
  prodOrderCode: true,
  modelCode: true,
  materialCode: true,
  batchNo: true,
  directLabor: true,
  indirectLabor: true,
  orderQty: true,
  stdMinutes: true,
  stdCapacity: true,
  createTime: true,
  action: true
})

// 获取数据
const fetchData = async () => {
  loading.value = true
  try {
    const response = await getAssyMpOutputList(queryParams.value)
    if (response.success) {
      tableData.value = response.data.rows
      total.value = response.data.totalNum
    } else {
      message.error(response.message || t('table.fetchError'))
    }
  } catch (error) {
    console.error('获取组立日报列表失败:', error)
    message.error(t('table.fetchError'))
  } finally {
    loading.value = false
  }
}

// 查询处理
const handleQuery = (params: any) => {
  queryParams.value = { ...queryParams.value, ...params, pageIndex: 1 }
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    plantCode: undefined,
    prodLine: undefined,
    prodOrderCode: undefined,
    modelCode: undefined,
    materialCode: undefined,
    shiftNo: undefined,
    startDate: undefined,
    endDate: undefined
  }
  fetchData()
}

// 分页处理
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

// 表格变化处理
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 行点击处理
const handleRowClick = (record: TaktAssyMpOutput) => {
  selectedRowKeys.value = [record.assyMpOutputId]
}

// 添加处理
const handleAdd = () => {
  selectedId.value = null
  formTitle.value = t('assyMpOutput.add')
  formVisible.value = true
}

// 编辑处理
const handleEdit = (record: TaktAssyMpOutput) => {
  selectedId.value = record.assyMpOutputId
  formTitle.value = t('assyMpOutput.edit')
  formVisible.value = true
}

// 编辑选中项
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => item.assyMpOutputId === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}

// 删除处理
const handleDelete = async (record: TaktAssyMpOutput) => {
  try {
    const response = await deleteAssyMpOutput(record.assyMpOutputId)
    if (response.success) {
      message.success(t('table.deleteSuccess'))
      fetchData()
    } else {
      message.error(response.message || t('table.deleteError'))
    }
  } catch (error) {
    console.error('删除组立日报失败:', error)
    message.error(t('table.deleteError'))
  }
}

// 批量删除处理
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('table.selectToDelete'))
    return
  }

  try {
    const response = await batchDeleteAssyMpOutput(selectedRowKeys.value as number[])
    if (response.success) {
      message.success(t('table.batchDeleteSuccess'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(response.message || t('table.batchDeleteError'))
    }
  } catch (error) {
    console.error('批量删除组立日报失败:', error)
    message.error(t('table.batchDeleteError'))
  }
}

// 查看详情处理
const handleView = async (record: TaktAssyMpOutput) => {
  try {
    const response = await getAssyMpOutputById(record.assyMpOutputId)
    if (response.success) {
      viewData.value = response.data
      viewVisible.value = true
    } else {
      message.error(response.message || t('table.fetchError'))
    }
  } catch (error) {
    console.error('获取组立日报详情失败:', error)
    message.error(t('table.fetchError'))
  }
}

// 导入处理
const handleImport = async (file: File) => {
  try {
    const response = await importAssyMpOutput(file)
    if (response.success) {
      message.success(t('table.importSuccess', { success: response.data.success, fail: response.data.fail }))
      fetchData()
    } else {
      message.error(response.message || t('table.importError'))
    }
  } catch (error) {
    console.error('导入组立日报失败:', error)
    message.error(t('table.importError'))
  }
}

// 导出处理
const handleExport = async () => {
  try {
    const blob = await exportAssyMpOutput(queryParams.value)
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `组立日报_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('table.exportSuccess'))
  } catch (error) {
    console.error('导出组立日报失败:', error)
    message.error(t('table.exportError'))
  }
}

// 模板下载处理
const handleTemplate = async () => {
  try {
    const blob = await getAssyMpOutputTemplate()
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = '组立日报导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('table.templateSuccess'))
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error(t('table.templateError'))
  }
}

// 表单成功处理
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 列设置处理
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

const handleColumnSettingChange = (checkedValues: CheckboxValueType[]) => {
  Object.keys(columnSettings.value).forEach(key => {
    columnSettings.value[key] = checkedValues.includes(key)
  })
}

// 切换搜索显示
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // 实现全屏切换逻辑
}

// 初始化
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.assy-mp-output-container {
  padding: 16px;
  background: #fff;
  border-radius: 6px;

  .view-content {
    .detail-section {
      margin-top: 16px;
      
      h3 {
        margin-bottom: 12px;
        color: #1890ff;
      }
    }
  }

  .column-setting-group {
    .column-setting-item {
      margin-bottom: 8px;
    }
  }
}
</style> 
