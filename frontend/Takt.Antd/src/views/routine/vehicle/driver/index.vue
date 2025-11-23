<template>
  <div class="driver-container">
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
      :add-permission="['routine:driver:create']"
      :show-edit="true"
      :edit-permission="['routine:driver:update']"
      :show-delete="true"
      :delete-permission="['routine:driver:delete']"
      :show-import="true"
      :import-permission="['routine:driver:import']"
      :show-export="true"
      :export-permission="['routine:driver:export']"
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
      row-key="driverId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 驾驶员状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="driver_status" :value="record.status" />
        </template>

        <!-- 驾驶证类型列 -->
        <template v-if="column.dataIndex === 'licenseType'">
          <Takt-dict-tag dict-type="driver_license_type" :value="record.licenseType" />
        </template>

        <!-- 驾驶证状态列 -->
        <template v-if="column.dataIndex === 'licenseStatus'">
          <Takt-dict-tag dict-type="driver_license_status" :value="record.licenseStatus" />
        </template>

        <!-- 性别列 -->
        <template v-if="column.dataIndex === 'gender'">
          <Takt-dict-tag dict-type="sys_user_type" :value="record.gender" />
        </template>

        <!-- 驾驶证有效期列 -->
        <template v-if="column.dataIndex === 'licenseExpiryDate'">
          {{ record.licenseExpiryDate ? formatDateTime(record.licenseExpiryDate) : '-' }}
        </template>

        <!-- 出生日期列 -->
        <template v-if="column.dataIndex === 'birthDate'">
          {{ record.birthDate ? formatDateTime(record.birthDate) : '-' }}
        </template>

        <!-- 入职日期列 -->
        <template v-if="column.dataIndex === 'hireDate'">
          {{ record.hireDate ? formatDateTime(record.hireDate) : '-' }}
        </template>

        <!-- 离职日期列 -->
        <template v-if="column.dataIndex === 'resignDate'">
          {{ record.resignDate ? formatDateTime(record.resignDate) : '-' }}
        </template>

        <!-- 健康证有效期列 -->
        <template v-if="column.dataIndex === 'healthCertExpiry'">
          {{ record.healthCertExpiry ? formatDateTime(record.healthCertExpiry) : '-' }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:driver:update']"
            :show-delete="true"
            :delete-permission="['routine:driver:delete']"
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

    <!-- 驾驶员表单对话框 -->
    <driver-form
      v-model:visible="driverFormVisible"
      :title="formTitle"
      :driver-id="selectedDriverId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="驾驶员导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的驾驶员信息字段,\n如员工姓名,工号,驾驶证号码,驾驶证类型等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:driver:template']"
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

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h, nextTick, watch } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktDriver, TaktDriverQuery } from '@/types/routine/vehicle/driver'
import type { QueryField } from '@/types/components/query'
import {
  getDriverList,
  deleteDriver,
  batchDeleteDriver,
  exportDriver,
  importDriver,
  getDriverTemplate,
  getDriverById,
  createDriver,
  updateDriver
} from '@/api/routine/driver'
import { formatDateTime } from '@/utils/format'
import DriverForm from './components/DriverForm.vue'

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
    dataIndex: 'driverId',
    key: 'driverId',
    width: 80,
    ellipsis: true
  },
  {
    title: '员工姓名',
    dataIndex: 'employeeName',
    key: 'employeeName',
    width: 120,
    ellipsis: true
  },
  {
    title: '员工工号',
    dataIndex: 'employeeNo',
    key: 'employeeNo',
    width: 120,
    ellipsis: true
  },
  {
    title: '部门',
    dataIndex: 'department',
    key: 'department',
    width: 120,
    ellipsis: true
  },
  {
    title: '职位',
    dataIndex: 'position',
    key: 'position',
    width: 120,
    ellipsis: true
  },
  {
    title: '驾驶员状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '驾驶证号码',
    dataIndex: 'licenseNo',
    key: 'licenseNo',
    width: 150,
    ellipsis: true
  },
  {
    title: '驾驶证类型',
    dataIndex: 'licenseType',
    key: 'licenseType',
    width: 100
  },
  {
    title: '驾驶证发证机关',
    dataIndex: 'licenseAuthority',
    key: 'licenseAuthority',
    width: 150,
    ellipsis: true
  },
  {
    title: '驾驶证发证日期',
    dataIndex: 'licenseIssueDate',
    key: 'licenseIssueDate',
    width: 180
  },
  {
    title: '驾驶证有效期',
    dataIndex: 'licenseExpiryDate',
    key: 'licenseExpiryDate',
    width: 180
  },
  {
    title: '驾驶证状态',
    dataIndex: 'licenseStatus',
    key: 'licenseStatus',
    width: 100
  },
  {
    title: '驾驶证扣分',
    dataIndex: 'licensePoints',
    key: 'licensePoints',
    width: 100
  },
  {
    title: '身份证号码',
    dataIndex: 'idCardNo',
    key: 'idCardNo',
    width: 180,
    ellipsis: true
  },
  {
    title: '性别',
    dataIndex: 'gender',
    key: 'gender',
    width: 80
  },
  {
    title: '出生日期',
    dataIndex: 'birthDate',
    key: 'birthDate',
    width: 180
  },
  {
    title: '年龄',
    dataIndex: 'age',
    key: 'age',
    width: 80
  },
  {
    title: '联系电话',
    dataIndex: 'phone',
    key: 'phone',
    width: 120,
    ellipsis: true
  },
  {
    title: '紧急联系人',
    dataIndex: 'emergencyContact',
    key: 'emergencyContact',
    width: 120,
    ellipsis: true
  },
  {
    title: '紧急联系电话',
    dataIndex: 'emergencyPhone',
    key: 'emergencyPhone',
    width: 120,
    ellipsis: true
  },
  {
    title: '家庭地址',
    dataIndex: 'homeAddress',
    key: 'homeAddress',
    width: 200,
    ellipsis: true
  },
  {
    title: '入职日期',
    dataIndex: 'hireDate',
    key: 'hireDate',
    width: 180
  },
  {
    title: '离职日期',
    dataIndex: 'resignDate',
    key: 'resignDate',
    width: 180
  },
  {
    title: '驾龄（年）',
    dataIndex: 'drivingYears',
    key: 'drivingYears',
    width: 100
  },
  {
    title: '驾驶经验',
    dataIndex: 'drivingExperience',
    key: 'drivingExperience',
    width: 100
  },
  {
    title: '可驾驶车型',
    dataIndex: 'drivableVehicles',
    key: 'drivableVehicles',
    width: 150,
    ellipsis: true
  },
  {
    title: '驾驶技能评分',
    dataIndex: 'drivingSkillScore',
    key: 'drivingSkillScore',
    width: 120
  },
  {
    title: '安全驾驶评分',
    dataIndex: 'safetyScore',
    key: 'safetyScore',
    width: 120
  },
  {
    title: '服务态度评分',
    dataIndex: 'serviceScore',
    key: 'serviceScore',
    width: 120
  },
  {
    title: '综合评分',
    dataIndex: 'overallScore',
    key: 'overallScore',
    width: 100
  },
  {
    title: '事故记录次数',
    dataIndex: 'accidentCount',
    key: 'accidentCount',
    width: 120
  },
  {
    title: '违章记录次数',
    dataIndex: 'violationCount',
    key: 'violationCount',
    width: 120
  },
  {
    title: '投诉记录次数',
    dataIndex: 'complaintCount',
    key: 'complaintCount',
    width: 120
  },
  {
    title: '表扬记录次数',
    dataIndex: 'praiseCount',
    key: 'praiseCount',
    width: 120
  },
  {
    title: '是否专职司机',
    dataIndex: 'isFullTime',
    key: 'isFullTime',
    width: 120
  },
  {
    title: '是否可驾驶危险品车辆',
    dataIndex: 'canDriveHazardous',
    key: 'canDriveHazardous',
    width: 150
  },
  {
    title: '是否可驾驶大型车辆',
    dataIndex: 'canDriveLarge',
    key: 'canDriveLarge',
    width: 150
  },
  {
    title: '是否可驾驶客车',
    dataIndex: 'canDrivePassenger',
    key: 'canDrivePassenger',
    width: 150
  },
  {
    title: '工作区域',
    dataIndex: 'workArea',
    key: 'workArea',
    width: 150,
    ellipsis: true
  },
  {
    title: '工作时间',
    dataIndex: 'workSchedule',
    key: 'workSchedule',
    width: 100
  },
  {
    title: '基本工资',
    dataIndex: 'baseSalary',
    key: 'baseSalary',
    width: 120
  },
  {
    title: '绩效工资',
    dataIndex: 'performanceSalary',
    key: 'performanceSalary',
    width: 120
  },
  {
    title: '津贴',
    dataIndex: 'allowance',
    key: 'allowance',
    width: 100
  },
  {
    title: '总工资',
    dataIndex: 'totalSalary',
    key: 'totalSalary',
    width: 120
  },
  {
    title: '银行账户',
    dataIndex: 'bankAccount',
    key: 'bankAccount',
    width: 150,
    ellipsis: true
  },
  {
    title: '开户银行',
    dataIndex: 'bankName',
    key: 'bankName',
    width: 150,
    ellipsis: true
  },
  {
    title: '账户持有人',
    dataIndex: 'accountHolder',
    key: 'accountHolder',
    width: 120,
    ellipsis: true
  },
  {
    title: '健康证号码',
    dataIndex: 'healthCertNo',
    key: 'healthCertNo',
    width: 150,
    ellipsis: true
  },
  {
    title: '健康证有效期',
    dataIndex: 'healthCertExpiry',
    key: 'healthCertExpiry',
    width: 180
  },
  {
    title: '培训证书',
    dataIndex: 'trainingCertificates',
    key: 'trainingCertificates',
    width: 150,
    ellipsis: true
  },
  {
    title: '技能证书',
    dataIndex: 'skillCertificates',
    key: 'skillCertificates',
    width: 150,
    ellipsis: true
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
    name: 'employeeName',
    label: '员工姓名',
    type: 'input',
    placeholder: '请输入员工姓名'
  },
  {
    name: 'employeeNo',
    label: '员工工号',
    type: 'input',
    placeholder: '请输入员工工号'
  },
  {
    name: 'licenseNo',
    label: '驾驶证号码',
    type: 'input',
    placeholder: '请输入驾驶证号码'
  },
  {
    name: 'department',
    label: '部门',
    type: 'input',
    placeholder: '请输入部门'
  },
  {
    name: 'status',
    label: '驾驶员状态',
    type: 'select',
    placeholder: '请选择驾驶员状态',
    options: [
      { label: '在职', value: 0 },
      { label: '离职', value: 1 },
      { label: '休假', value: 2 },
      { label: '停职', value: 3 },
      { label: '其他', value: 4 }
    ]
  },
  {
    name: 'licenseType',
    label: '驾驶证类型',
    type: 'select',
    placeholder: '请选择驾驶证类型',
    options: [
      { label: 'A1', value: 0 },
      { label: 'A2', value: 1 },
      { label: 'A3', value: 2 },
      { label: 'B1', value: 3 },
      { label: 'B2', value: 4 },
      { label: 'C1', value: 5 },
      { label: 'C2', value: 6 },
      { label: 'D', value: 7 },
      { label: 'E', value: 8 },
      { label: 'F', value: 9 },
      { label: 'M', value: 10 },
      { label: 'N', value: 11 },
      { label: 'P', value: 12 }
    ]
  },
  {
    name: 'licenseStatus',
    label: '驾驶证状态',
    type: 'select',
    placeholder: '请选择驾驶证状态',
    options: [
      { label: '正常', value: 0 },
      { label: '即将到期', value: 1 },
      { label: '已过期', value: 2 },
      { label: '被吊销', value: 3 },
      { label: '被注销', value: 4 }
    ]
  },
  {
    name: 'gender',
    label: '性别',
    type: 'select',
    placeholder: '请选择性别',
    options: [
      { label: '男', value: 0 },
      { label: '女', value: 1 }
    ]
  },
  {
    name: 'isFullTime',
    label: '是否专职司机',
    type: 'select',
    placeholder: '请选择是否专职司机',
    options: [
      { label: '否', value: 0 },
      { label: '是', value: 1 }
    ]
  }
]

// 响应式数据
const loading = ref(false)
const tableData = ref<TaktDriver[]>([])
const total = ref(0)
const selectedRowKeys = ref<(string | number)[]>([])
const selectedDriverId = ref<number>()
const driverFormVisible = ref(false)
const formTitle = ref('')
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const showSearch = ref(true)

// 查询参数
const queryParams = ref<TaktDriverQuery>({
  pageIndex: 1,
  pageSize: 10
})

// 列设置
const defaultColumns = computed(() => columns)
const columnSettings = ref<Record<string, boolean>>({
  driverId: true,
  employeeName: true,
  employeeNo: true,
  department: true,
  position: false,
  status: true,
  licenseNo: true,
  licenseType: true,
  licenseAuthority: false,
  licenseIssueDate: false,
  licenseExpiryDate: true,
  licenseStatus: true,
  licensePoints: true,
  idCardNo: false,
  gender: true,
  birthDate: false,
  age: true,
  phone: true,
  emergencyContact: false,
  emergencyPhone: false,
  homeAddress: false,
  hireDate: false,
  resignDate: false,
  drivingYears: true,
  drivingExperience: true,
  drivableVehicles: false,
  drivingSkillScore: false,
  safetyScore: false,
  serviceScore: false,
  overallScore: true,
  accidentCount: true,
  violationCount: true,
  complaintCount: false,
  praiseCount: false,
  isFullTime: true,
  canDriveHazardous: false,
  canDriveLarge: false,
  canDrivePassenger: false,
  workArea: false,
  workSchedule: false,
  baseSalary: false,
  performanceSalary: false,
  allowance: false,
  totalSalary: false,
  bankAccount: false,
  bankName: false,
  accountHolder: false,
  healthCertNo: false,
  healthCertExpiry: false,
  trainingCertificates: false,
  skillCertificates: false,
  remark: true,
  createBy: true,
  createTime: true,
  updateBy: false,
  updateTime: false,
  action: true
})

// 获取数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getDriverList(queryParams.value)
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
const handleQuery = (params: TaktDriverQuery) => {
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
const handleRowClick = (record: TaktDriver) => {
  selectedDriverId.value = record.driverId
}

// 新增
const handleAdd = () => {
  formTitle.value = '新增驾驶员'
  selectedDriverId.value = undefined
  driverFormVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => item.driverId === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}

// 编辑
const handleEdit = (record: TaktDriver) => {
  formTitle.value = '编辑驾驶员'
  selectedDriverId.value = record.driverId
  driverFormVisible.value = true
}

// 删除
const handleDelete = async (record: TaktDriver) => {
  try {
    await deleteDriver(record.driverId)
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
    await batchDeleteDriver({ driverIds: selectedRowKeys.value as number[] })
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

// 表单成功回调
const handleSuccess = () => {
  driverFormVisible.value = false
  fetchData()
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入上传
const handleImportUpload = async (file: File) => {
  // 这里需要实现导入逻辑
  return true
}

// 导入成功
const handleImportSuccess = () => {
  importVisible.value = false
  fetchData()
}

// 下载模板
const handleDownloadTemplate = async () => {
  // 这里需要实现下载模板逻辑
  message.info('下载模板功能待实现')
}

// 导出
const handleExport = async () => {
  // 这里需要实现导出逻辑
  message.info('导出功能待实现')
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
.driver-container {
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
