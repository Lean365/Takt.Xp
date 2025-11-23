//===================================================================
// 项目名 : Takt.Xp
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 生产工单管理页面
//===================================================================

<template>
  <div class="prod-order-container">
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
      :add-permission="['logistics:manufacturing:master:order:create']"
      :show-edit="true"
      :edit-permission="['logistics:manufacturing:master:order:update']"
      :show-delete="true"
      :delete-permission="['logistics:manufacturing:master:order:delete']"
      :show-import="true"
      :import-permission="['logistics:manufacturing:master:order:import']"
      :show-export="true"
      :export-permission="['logistics:manufacturing:master:order:export']"
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
      :row-key="(record: TaktProdOrder) => String(record.prodOrderId)"
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

        <!-- 工单类型列 -->
        <template v-if="column.dataIndex === 'prodOrderType'">
          <Takt-dict-tag dict-type="prod_order_type" :value="record.prodOrderType" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['logistics:manufacturing:mes:master:order:query']"
            :show-edit="true"
            :edit-permission="['logistics:manufacturing:master:order:update']"
            :show-delete="true"
            :delete-permission="['logistics:manufacturing:master:order:delete']"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 生产工单表单对话框 -->
    <prod-order-form
      v-model:open="formVisible"
      :title="formTitle"
      :prod-order-id="selectedProdOrderId"
      @success="handleSuccess"
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
import type { TaktProdOrder, TaktProdOrderQuery } from '@/types/logistics/manufacturing/master/prodOrder'
import type { QueryField } from '@/types/components/query'
import {
  getProdOrderList,
  getProdOrderById,
  createProdOrder,
  updateProdOrder,
  deleteProdOrder,
  batchDeleteProdOrder,
  importProdOrder,
  exportProdOrder,
  getProdOrderTemplate
} from '@/api/logistics/manufacturing/master/prodOrder'
import ProdOrderForm from './components/ProdOrderForm.vue'


const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const showSearch = ref(false)
const tableData = ref<TaktProdOrder[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedProdOrderId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')

const columnSettingVisible = ref(false)
const total = ref(0)

// === 查询参数 ===
const queryParams = ref<TaktProdOrderQuery>({
  pageIndex: 1,
  pageSize: 10,
  plantCode: '',
  prodOrderType: '',
  prodOrderCode: '',
  materialCode: '',
  prodBatch: '',
  status: -1,
  startDate: undefined,
  endDate: undefined
})

// === 查询字段定义 ===
const queryFields: QueryField[] = [
  {
    name: 'plantCode',
    label: t('logistics.manufacturing.master.order.fields.plantCode.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.plantCode.placeholder'),
    type: 'input' as const
  },
  {
    name: 'prodOrderType',
    label: t('logistics.manufacturing.master.order.fields.prodOrderType.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.prodOrderType.placeholder'),
    type: 'select' as const,
    props: {
      dictType: 'prod_order_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'prodOrderCode',
    label: t('logistics.manufacturing.master.order.fields.prodOrderCode.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.prodOrderCode.placeholder'),
    type: 'input' as const
  },
  {
    name: 'materialCode',
    label: t('logistics.manufacturing.master.order.fields.materialCode.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.materialCode.placeholder'),
    type: 'input' as const
  },
  {
    name: 'prodBatch',
    label: t('logistics.manufacturing.master.order.fields.prodBatch.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.prodBatch.placeholder'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('logistics.manufacturing.master.order.fields.status.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.status.placeholder'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'dateRange',
    label: t('logistics.manufacturing.master.order.fields.dateRange.label'),
    placeholder: t('logistics.manufacturing.master.order.fields.dateRange.placeholder'),
    type: 'dateRange' as const,
    props: {
      format: 'YYYY-MM-DD',
      valueFormat: 'YYYY-MM-DD'
    }
  }
]

// === 表格列定义 ===
const defaultColumns = [
  {
    title: t('logistics.manufacturing.master.order.fields.plantCode.label'),
    dataIndex: 'plantCode',
    key: 'plantCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.master.order.fields.prodOrderCode.label'),
    dataIndex: 'prodOrderCode',
    key: 'prodOrderCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.master.order.fields.prodOrderType.label'),
    dataIndex: 'prodOrderType',
    key: 'prodOrderType',
    width: 120
  },
  {
    title: t('logistics.manufacturing.master.order.fields.materialCode.label'),
    dataIndex: 'materialCode',
    key: 'materialCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.master.order.fields.prodBatch.label'),
    dataIndex: 'prodBatch',
    key: 'prodBatch',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.master.order.fields.prodOrderQty.label'),
    dataIndex: 'prodOrderQty',
    key: 'prodOrderQty',
    width: 100
  },
  {
    title: t('logistics.manufacturing.master.order.fields.workCenter.label'),
    dataIndex: 'workCenter',
    key: 'workCenter',
    width: 120
  },
  {
    title: t('logistics.manufacturing.master.order.fields.status.label'),
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
  plantCode: true,
  prodOrderCode: true,
  prodOrderType: true,
  materialCode: true,
  prodBatch: true,
  prodOrderQty: true,
  workCenter: true,
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
    // 处理日期范围参数
    const params = { ...queryParams.value }
    if (params.dateRange && Array.isArray(params.dateRange) && params.dateRange.length === 2) {
      params.startDate = params.dateRange[0]
      params.endDate = params.dateRange[1]
      delete params.dateRange
    }
    
    const response = await getProdOrderList(params)
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
    plantCode: '',
    prodOrderType: '',
    prodOrderCode: '',
    materialCode: '',
    prodBatch: '',
    status: -1,
    startDate: undefined,
    endDate: undefined
  }
  fetchData()
}

const handleAdd = () => {
  selectedProdOrderId.value = undefined
  formTitle.value = '新增生产工单'
  formVisible.value = true
}

const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => String(item.prodOrderId) === selectedRowKeys.value[0])
    if (record) {
      selectedProdOrderId.value = record.prodOrderId
      formTitle.value = '编辑生产工单'
      formVisible.value = true
    }
  }
}

const handleEdit = (record: TaktProdOrder) => {
  selectedProdOrderId.value = record.prodOrderId
  formTitle.value = '编辑生产工单'
  formVisible.value = true
}



const handleDelete = async (record: TaktProdOrder) => {
  try {
    await deleteProdOrder(record.prodOrderId)
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
    await batchDeleteProdOrder(ids)
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

const handleRowClick = (record: TaktProdOrder) => {
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
.prod-order-container {
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


