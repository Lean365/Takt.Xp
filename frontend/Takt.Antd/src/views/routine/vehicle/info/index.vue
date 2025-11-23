<template>
  <div class="vehicle-container">
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
      :add-permission="['routine:vehicle:create']"
      :show-edit="true"
      :edit-permission="['routine:vehicle:update']"
      :show-delete="true"
      :delete-permission="['routine:vehicle:delete']"
      :show-import="true"
      :import-permission="['routine:vehicle:import']"
      :show-export="true"
      :export-permission="['routine:vehicle:export']"
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
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="vehicleId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 车辆类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'vehicleType'">
          <Takt-dict-tag dict-type="vehicle_type" :value="record.vehicleType" />
        </template>

        <!-- 车辆状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="vehicle_status" :value="record.status" />
        </template>

        <!-- 购买价格列 -->
        <template v-if="column.dataIndex === 'purchasePrice'">
          {{ record.purchasePrice ? `¥${record.purchasePrice.toLocaleString()}` : '-' }}
        </template>

        <!-- 当前里程数列 -->
        <template v-if="column.dataIndex === 'currentMileage'">
          {{ record.currentMileage ? `${record.currentMileage.toLocaleString()} km` : '-' }}
        </template>

        <!-- 保险到期日期列 -->
        <template v-if="column.dataIndex === 'insuranceExpiryDate'">
          {{ record.insuranceExpiryDate ? formatDate(record.insuranceExpiryDate) : '-' }}
        </template>

        <!-- 年检到期日期列 -->
        <template v-if="column.dataIndex === 'inspectionExpiryDate'">
          {{ record.inspectionExpiryDate ? formatDate(record.inspectionExpiryDate) : '-' }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:vehicle:update']"
            :show-delete="true"
            :delete-permission="['routine:vehicle:delete']"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 用车表单对话框 -->
    <vehicle-form
      v-model:visible="vehicleFormVisible"
      :title="formTitle"
      :vehicle-id="selectedVehicleId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="用车导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的用车信息字段,\n如车牌号,车辆类型,状态,品牌,型号,颜色,座位数等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:vehicle:template']"
    />

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
        <div v-for="(col, index) in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h, nextTick, watch } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktVehicle, TaktVehicleQuery } from '@/types/routine/vehicle/vehicle'
import type { QueryField } from '@/types/components/query'
import {
  getVehicleList,
  deleteVehicle,
  batchDeleteVehicle,
  exportVehicle,
  importVehicle,
  getVehicleTemplate,
  getVehicleById,
  createVehicle,
  updateVehicle
} from '@/api/routine/vehicle'
import VehicleForm from './components/VehicleForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询字段类型定义
type FieldType =
  | 'input'
  | 'select'
  | 'date'
  | 'dateRange'
  | 'number'
  | 'radio'
  | 'checkbox'
  | 'cascader'

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
    title: 'ID',
    dataIndex: 'vehicleId',
    key: 'vehicleId',
    width: 80,
    ellipsis: true
  },
  {
    title: '车牌号',
    dataIndex: 'plateNumber',
    key: 'plateNumber',
    width: 120,
    ellipsis: true
  },
  {
    title: '车辆类型',
    dataIndex: 'vehicleType',
    key: 'vehicleType',
    width: 100
  },
  {
    title: '车辆状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '品牌',
    dataIndex: 'brand',
    key: 'brand',
    width: 120,
    ellipsis: true
  },
  {
    title: '型号',
    dataIndex: 'model',
    key: 'model',
    width: 120,
    ellipsis: true
  },
  {
    title: '颜色',
    dataIndex: 'color',
    key: 'color',
    width: 80
  },
  {
    title: '座位数',
    dataIndex: 'seatCount',
    key: 'seatCount',
    width: 80
  },
  {
    title: '购买日期',
    dataIndex: 'purchaseDate',
    key: 'purchaseDate',
    width: 120
  },
  {
    title: '购买价格',
    dataIndex: 'purchasePrice',
    key: 'purchasePrice',
    width: 120
  },
  {
    title: '当前里程数',
    dataIndex: 'currentMileage',
    key: 'currentMileage',
    width: 120
  },
  {
    title: '保险到期日期',
    dataIndex: 'insuranceExpiryDate',
    key: 'insuranceExpiryDate',
    width: 150
  },
  {
    title: '年检到期日期',
    dataIndex: 'inspectionExpiryDate',
    key: 'inspectionExpiryDate',
    width: 150
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('table.columns.action'),
    key: 'action',
    width: 120,
    fixed: 'right'
  }
]

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'plateNumber',
    label: '车牌号',
    type: 'input',
    placeholder: '请输入车牌号'
  },
  {
    name: 'vehicleType',
    label: '车辆类型',
    type: 'select',
    placeholder: '请选择车辆类型',
    options: [
      { label: '轿车', value: 0 },
      { label: 'SUV', value: 1 },
      { label: '商务车', value: 2 },
      { label: '面包车', value: 3 }
    ]
  },
  {
    name: 'status',
    label: '车辆状态',
    type: 'select',
    placeholder: '请选择车辆状态',
    options: [
      { label: '空闲', value: 0 },
      { label: '使用中', value: 1 },
      { label: '维修中', value: 2 },
      { label: '已报废', value: 3 }
    ]
  },
  {
    name: 'brand',
    label: '品牌',
    type: 'input',
    placeholder: '请输入品牌'
  },
  {
    name: 'model',
    label: '型号',
    type: 'input',
    placeholder: '请输入型号'
  },
  {
    name: 'color',
    label: '颜色',
    type: 'input',
    placeholder: '请输入颜色'
  }
]

// 响应式数据
const loading = ref(false)
const tableData = ref<TaktVehicle[]>([])
const total = ref(0)
const selectedRowKeys = ref<(string | number)[]>([])
const selectedVehicleId = ref<number>()
const vehicleFormVisible = ref(false)
const formTitle = ref('')
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const showSearch = ref(true)

// 查询参数
const queryParams = ref<TaktVehicleQuery>({
  pageIndex: 1,
  pageSize: 10
})

// 列设置
const defaultColumns = computed(() => columns)
const columnSettings = ref<Record<string, boolean>>({
  vehicleId: true,
  plateNumber: true,
  vehicleType: true,
  status: true,
  brand: true,
  model: true,
  color: true,
  seatCount: true,
  purchaseDate: true,
  purchasePrice: true,
  currentMileage: true,
  insuranceExpiryDate: true,
  inspectionExpiryDate: true,
  remark: true,
  createBy: true,
  createTime: true,
  updateBy: false,
  updateTime: false,
  action: true
})

// 格式化日期
const formatDate = (date: string | Date) => {
  if (!date) return '-'
  return new Date(date).toLocaleDateString()
}

// 获取数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getVehicleList(queryParams.value)
    if (res.data?.data) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    }
  } catch (error) {
    message.error('获取数据失败')
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = (params: TaktVehicleQuery) => {
  queryParams.value = { ...queryParams.value, ...params, pageIndex: 1 }
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10
  }
  fetchData()
}

// 分页变化
const handlePageChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

// 页面大小变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = 1
  queryParams.value.pageSize = size
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktVehicle) => {
  selectedVehicleId.value = record.vehicleId
}

// 新增
const handleAdd = () => {
  formTitle.value = '新增用车'
  selectedVehicleId.value = undefined
  vehicleFormVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => item.vehicleId === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}

// 编辑
const handleEdit = (record: TaktVehicle) => {
  formTitle.value = '编辑用车'
  selectedVehicleId.value = record.vehicleId
  vehicleFormVisible.value = true
}

// 删除
const handleDelete = async (record: TaktVehicle) => {
  try {
    await deleteVehicle(record.vehicleId)
    message.success('删除成功')
    fetchData()
  } catch (error) {
    message.error('删除失败')
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的数据')
    return
  }
  
  try {
    await batchDeleteVehicle({ vehicleIds: selectedRowKeys.value as number[] })
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

// 表单成功回调
const handleSuccess = () => {
  vehicleFormVisible.value = false
  fetchData()
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入上传
const handleImportUpload = async (file: File) => {
  try {
    await importVehicle(file)
    return true
  } catch (error) {
    return false
  }
}

// 导入成功
const handleImportSuccess = () => {
  importVisible.value = false
  fetchData()
}

// 下载模板
const handleDownloadTemplate = async () => {
  try {
    const response = await getVehicleTemplate()
    const blob = response.data
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = '用车导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    message.error('下载模板失败')
  }
}

// 导出
const handleExport = async () => {
  try {
    const response = await exportVehicle(queryParams.value)
    const blob = response.data
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = '用车数据.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    message.error('导出失败')
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: string[]) => {
  const newSettings: Record<string, boolean> = {}
  Object.keys(columnSettings.value).forEach(key => {
    newSettings[key] = checkedValues.includes(key)
  })
  columnSettings.value = newSettings
}

// 切换搜索
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
.vehicle-container {
  padding: 16px;
  background: #f0f2f5;
  min-height: 100vh;
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
