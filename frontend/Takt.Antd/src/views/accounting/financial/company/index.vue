<template>
  <div class="company-container">
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
      :add-permission="['accounting:financial:company:create']"
      :show-edit="true"
      :edit-permission="['accounting:financial:company:update']"
      :show-delete="true"
      :delete-permission="['accounting:financial:company:delete']"
      :show-import="true"
      :import-permission="['accounting:financial:company:import']"
      :show-export="true"
      :export-permission="['accounting:financial:company:export']"
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
      row-key="companyId"
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
          <a-switch
            v-hasPermi="['accounting:financial:company:update']"
            :checked="record.status === 0"
            :checked-children="dictStore.getDictLabel('sys_normal_disable', 0)"
            :un-checked-children="dictStore.getDictLabel('sys_normal_disable', 1)"
            @change="(val: any) => handleStatusChange(record, !!val)"
            :loading="record.statusLoading"
          />
        </template>

        <!-- 公司性质列 -->
        <template v-if="column.dataIndex === 'companyNature'">
          <Takt-dict-tag dict-type="sys_enterprise_nature" :value="record.companyNature" />
        </template>

        <!-- 公司行业类型列 -->
        <template v-if="column.dataIndex === 'companyIndustryType'">
          <Takt-dict-tag dict-type="sys_industry_type" :value="record.companyIndustryType" />
        </template>

        <!-- 公司币种列 -->
        <template v-if="column.dataIndex === 'companyCurrency'">
          <Takt-dict-tag dict-type="sys_currency" :value="record.companyCurrency" />
        </template>

        <!-- 公司语言列 -->
        <template v-if="column.dataIndex === 'companyLanguage'">
          <Takt-dict-tag dict-type="accounting_company_language" :value="record.companyLanguage" />
        </template>

        <!-- 成立日期列 -->
        <template v-if="column.dataIndex === 'establishmentDate'">
          <span v-if="record.establishmentDate">{{ record.establishmentDate }}</span>
          <span v-else>-</span>
        </template>

        <!-- 注销日期列 -->
        <template v-if="column.dataIndex === 'dissolutionDate'">
          <span v-if="record.dissolutionDate">{{ record.dissolutionDate }}</span>
          <span v-else>-</span>
        </template>

        <!-- 注册资本列 -->
        <template v-if="column.dataIndex === 'companyRegisteredCapital'">
          <span v-if="record.companyRegisteredCapital">{{ record.companyRegisteredCapital.toLocaleString() }}</span>
          <span v-else>-</span>
        </template>

        <!-- 最大汇率偏差列 -->
        <template v-if="column.dataIndex === 'companyMaxExchangeRateDeviation'">
          <span v-if="record.companyMaxExchangeRateDeviation">{{ record.companyMaxExchangeRateDeviation }}%</span>
          <span v-else>-</span>
        </template>

        <!-- 长文本字段处理 - 公司地址 -->
        <template v-if="column.dataIndex === 'companyAddress'">
          <a-tooltip :title="record.companyAddress" placement="topLeft">
            <div class="text-ellipsis" :style="{ maxWidth: '180px' }">
              {{ record.companyAddress || '-' }}
            </div>
          </a-tooltip>
        </template>

        <!-- 长文本字段处理 - 公司邮箱 -->
        <template v-if="column.dataIndex === 'companyEmail'">
          <a-tooltip :title="record.companyEmail" placement="topLeft">
            <div class="text-ellipsis" :style="{ maxWidth: '180px' }">
              {{ record.companyEmail || '-' }}
            </div>
          </a-tooltip>
        </template>

        <!-- 长文本字段处理 - 公司网站 -->
        <template v-if="column.dataIndex === 'companyWebsite'">
          <a-tooltip :title="record.companyWebsite" placement="topLeft">
            <div class="text-ellipsis" :style="{ maxWidth: '180px' }">
              <a v-if="record.companyWebsite" :href="record.companyWebsite" target="_blank" class="text-blue-500 hover:text-blue-700">
                {{ record.companyWebsite }}
              </a>
              <span v-else>-</span>
            </div>
          </a-tooltip>
        </template>

        <!-- 长文本字段处理 - 公司名称 -->
        <template v-if="column.dataIndex === 'companyName'">
          <a-tooltip :title="record.companyName" placement="topLeft">
            <div class="text-ellipsis font-medium" :style="{ maxWidth: '180px' }">
              {{ record.companyName || '-' }}
            </div>
          </a-tooltip>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['accounting:financial:company:update']"
            :show-delete="true"
            :delete-permission="['accounting:financial:company:delete']"
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

    <!-- 公司表单对话框 -->
    <company-form
      v-model:visible="companyFormVisible"
      :title="formTitle"
      :company-id="selectedCompanyId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="公司导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的公司信息字段,\n如公司代码,公司名称,公司简称,公司性质,行业类型,币种,语言等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['accounting:financial:company:template']"
    />

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

<script setup lang="ts">
import { ref, onMounted, h, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktCompany, TaktCompanyQuery } from '@/types/accounting/financial/company'
import { useDictStore } from '@/stores/dictStore'
import {
  getCompanyList,
  deleteCompany,
  exportCompany,
  importCompany,
  getTemplate,
  updateCompanyStatus,
} from '@/api/accounting/financial/company'
import CompanyForm from './components/CompanyForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'companyCode',
    label: t('accounting.financial.company.fields.companyCode.label'),
    type: 'input' as const,
  },
  {
    name: 'companyName',
    label: t('accounting.financial.company.fields.companyName.label'),
    type: 'input' as const
  },
  {
    name: 'companyShortName',
    label: t('accounting.financial.company.fields.companyShortName.label'),
    type: 'input' as const
  },
  {
    name: 'companyTaxNumber',
    label: t('accounting.financial.company.fields.companyTaxNumber.label'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('accounting.financial.company.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'companyNature',
    label: t('accounting.financial.company.fields.companyNature.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_enterprise_nature',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'companyIndustryType',
    label: t('accounting.financial.company.fields.companyIndustryType.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_industry_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'companyCurrency',
    label: t('accounting.financial.company.fields.companyCurrency.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_currency',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'companyLanguage',
    label: t('accounting.financial.company.fields.companyLanguage.label'),
    type: 'select' as const,
    props: {
      dictType: 'accounting_company_language',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktCompanyQuery>({
  pageIndex: 1,
  pageSize: 10,
  companyCode: '',
  companyName: '',
  companyShortName: '',
  companyTaxNumber: '',
  status: -1,
  companyNature: -1,
  companyIndustryType: -1,
  companyCurrency: -1,
  companyLanguage: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktCompany[]>([])

// 选中的行
const selectedRowKeys = ref<string[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns: TableColumnsType = [
  {
    title: 'ID',
    dataIndex: 'companyId',
    key: 'companyId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyCode.label'),
    dataIndex: 'companyCode',
    key: 'companyCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyName.label'),
    dataIndex: 'companyName',
    key: 'companyName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyName1.label'),
    dataIndex: 'companyName1',
    key: 'companyName1',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyName2.label'),
    dataIndex: 'companyName2',
    key: 'companyName2',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyName3.label'),
    dataIndex: 'companyName3',
    key: 'companyName3',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyShortName.label'),
    dataIndex: 'companyShortName',
    key: 'companyShortName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyAddress.label'),
    dataIndex: 'companyAddress',
    key: 'companyAddress',
    width: 200,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyAddress1.label'),
    dataIndex: 'companyAddress1',
    key: 'companyAddress1',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyAddress2.label'),
    dataIndex: 'companyAddress2',
    key: 'companyAddress2',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyAddress3.label'),
    dataIndex: 'companyAddress3',
    key: 'companyAddress3',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyCity.label'),
    dataIndex: 'companyCity',
    key: 'companyCity',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyRegion.label'),
    dataIndex: 'companyRegion',
    key: 'companyRegion',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyPostalCode.label'),
    dataIndex: 'companyPostalCode',
    key: 'companyPostalCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyCountry.label'),
    dataIndex: 'companyCountry',
    key: 'companyCountry',
    width: 100,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyPhone.label'),
    dataIndex: 'companyPhone',
    key: 'companyPhone',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyFax.label'),
    dataIndex: 'companyFax',
    key: 'companyFax',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyEmail.label'),
    dataIndex: 'companyEmail',
    key: 'companyEmail',
    width: 200,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyWebsite.label'),
    dataIndex: 'companyWebsite',
    key: 'companyWebsite',
    width: 200,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyLegalRepresentative.label'),
    dataIndex: 'companyLegalRepresentative',
    key: 'companyLegalRepresentative',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyRegisteredCapital.label'),
    dataIndex: 'companyRegisteredCapital',
    key: 'companyRegisteredCapital',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.establishmentDate.label'),
    dataIndex: 'establishmentDate',
    key: 'establishmentDate',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.dissolutionDate.label'),
    dataIndex: 'dissolutionDate',
    key: 'dissolutionDate',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyBusinessLicense.label'),
    dataIndex: 'companyBusinessLicense',
    key: 'companyBusinessLicense',
    width: 180,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyTaxNumber.label'),
    dataIndex: 'companyTaxNumber',
    key: 'companyTaxNumber',
    width: 180,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyOrganizationCode.label'),
    dataIndex: 'companyOrganizationCode',
    key: 'companyOrganizationCode',
    width: 180,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyUnifiedCreditCode.label'),
    dataIndex: 'companyUnifiedCreditCode',
    key: 'companyUnifiedCreditCode',
    width: 180,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyIndustryType.label'),
    dataIndex: 'companyIndustryType',
    key: 'companyIndustryType',
    width: 120
  },
  {
    title: t('accounting.financial.company.fields.companyScale.label'),
    dataIndex: 'companyScale',
    key: 'companyScale',
    width: 120,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyNature.label'),
    dataIndex: 'companyNature',
    key: 'companyNature',
    width: 120
  },
  {
    title: t('accounting.financial.company.fields.companyCurrency.label'),
    dataIndex: 'companyCurrency',
    key: 'companyCurrency',
    width: 100
  },
  {
    title: t('accounting.financial.company.fields.companyLanguage.label'),
    dataIndex: 'companyLanguage',
    key: 'companyLanguage',
    width: 100
  },
  {
    title: t('accounting.financial.company.fields.companyFiscalYearVariant.label'),
    dataIndex: 'companyFiscalYearVariant',
    key: 'companyFiscalYearVariant',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyChartOfAccounts.label'),
    dataIndex: 'companyChartOfAccounts',
    key: 'companyChartOfAccounts',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyFinancialManagementArea.label'),
    dataIndex: 'companyFinancialManagementArea',
    key: 'companyFinancialManagementArea',
    width: 180,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyMaxExchangeRateDeviation.label'),
    dataIndex: 'companyMaxExchangeRateDeviation',
    key: 'companyMaxExchangeRateDeviation',
    width: 180,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyAllocationIdentifier.label'),
    dataIndex: 'companyAllocationIdentifier',
    key: 'companyAllocationIdentifier',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyRelatedCompany.label'),
    dataIndex: 'companyRelatedCompany',
    key: 'companyRelatedCompany',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyRelatedPlant.label'),
    dataIndex: 'companyRelatedPlant',
    key: 'companyRelatedPlant',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.companyAddressNumber.label'),
    dataIndex: 'companyAddressNumber',
    key: 'companyAddressNumber',
    width: 150,
    ellipsis: true
  },
  {
    title: t('accounting.financial.company.fields.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('accounting.financial.company.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
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
    title: t('table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: t('table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: t('table.columns.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 默认列配置
const defaultColumns = columns

// 列设置
const columnSettings = ref<Record<string, boolean>>({
  companyId: true,
  companyCode: true,
  companyName: true,
  companyName1: false,
  companyName2: false,
  companyName3: false,
  companyShortName: true,
  companyAddress: false,
  companyAddress1: false,
  companyAddress2: false,
  companyAddress3: false,
  companyCity: false,
  companyRegion: false,
  companyPostalCode: false,
  companyCountry: false,
  companyPhone: true,
  companyFax: false,
  companyEmail: true,
  companyWebsite: false,
  companyLegalRepresentative: false,
  companyRegisteredCapital: false,
  establishmentDate: false,
  dissolutionDate: false,
  companyBusinessLicense: false,
  companyTaxNumber: true,
  companyOrganizationCode: false,
  companyUnifiedCreditCode: false,
  companyIndustryType: true,
  companyScale: false,
  companyNature: true,
  companyCurrency: true,
  companyLanguage: true,
  companyFiscalYearVariant: false,
  companyChartOfAccounts: false,
  companyFinancialManagementArea: false,
  companyMaxExchangeRateDeviation: false,
  companyAllocationIdentifier: false,
  companyRelatedCompany: false,
  companyRelatedPlant: false,
  companyAddressNumber: false,
  orderNum: false,
  status: true,
  remark: true,
  createBy: true,
  createTime: true,
  updateBy: true,
  updateTime: true,
  deleteBy: true,
  deleteTime: true,
  action: true
})

// ==================== 表单相关 ====================
// 表单可见性
const companyFormVisible = ref(false)

// 选中的公司ID
const selectedCompanyId = ref<string>()

// 表单标题
const formTitle = computed(() => selectedCompanyId.value ? t('common.edit') : t('common.add'))

// ==================== 导入相关 ====================
// 导入对话框可见性
const importVisible = ref(false)

// ==================== 列设置相关 ====================
// 列设置抽屉可见性
const columnSettingVisible = ref(false)

// ==================== 方法 ====================
// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    const response = await getCompanyList(queryParams.value)
    tableData.value = response.data.rows
    total.value = response.data.totalNum
  } catch (error) {
    console.error('获取公司列表失败:', error)
    message.error(t('common.loadFailed'))
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
    companyCode: '',
    companyName: '',
    companyShortName: '',
    companyTaxNumber: '',
    status: -1,
    companyNature: -1,
    companyIndustryType: -1,
    companyCurrency: -1,
    companyLanguage: -1
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: any) => {
  queryParams.value.pageIndex = pagination.current
  queryParams.value.pageSize = pagination.pageSize
  fetchData()
}

// 分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 页面大小变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktCompany) => {
  selectedRowKeys.value = [record.companyId]
}

// 新增
const handleAdd = () => {
  selectedCompanyId.value = undefined
  companyFormVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    handleEdit({ companyId: selectedRowKeys.value[0] } as TaktCompany)
  }
}

// 编辑
const handleEdit = (record: TaktCompany) => {
  selectedCompanyId.value = record.companyId
  companyFormVisible.value = true
}

// 删除
const handleDelete = async (record: TaktCompany) => {
  try {
    await deleteCompany(record.companyId)
    message.success(t('common.deleteSuccess'))
    fetchData()
  } catch (error) {
    console.error('删除公司失败:', error)
    message.error(t('common.deleteFailed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }
  
  try {
    await Promise.all(selectedRowKeys.value.map(id => deleteCompany(id)))
    message.success(t('common.deleteSuccess'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除公司失败:', error)
    message.error(t('common.deleteFailed'))
  }
}

// 状态变更
const handleStatusChange = async (record: TaktCompany, enabled: boolean) => {
  try {
    record.statusLoading = true
    await updateCompanyStatus({
      companyId: record.companyId,
      status: enabled ? 0 : 1
    })
    record.status = enabled ? 0 : 1
    message.success(t('common.updateSuccess'))
  } catch (error) {
    console.error('更新公司状态失败:', error)
    message.error(t('common.updateFailed'))
  } finally {
    record.statusLoading = false
  }
}

// 表单成功回调
const handleSuccess = () => {
  companyFormVisible.value = false
  selectedCompanyId.value = undefined
  fetchData()
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入上传
const handleImportUpload = async (file: File) => {
  const response = await importCompany(file)
  return response.data
}

// 下载模板
const handleDownloadTemplate = async () => {
  const response = await getTemplate()
  return response.data
}

// 导入成功
const handleImportSuccess = () => {
  importVisible.value = false
  fetchData()
}

// 导出
const handleExport = async () => {
  try {
    const response = await exportCompany(queryParams.value)
    const blob = response.data
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `公司数据_${new Date().toISOString().slice(0, 10)}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
    message.success(t('common.exportSuccess'))
  } catch (error) {
    console.error('导出公司失败:', error)
    message.error(t('common.exportFailed'))
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: any[]) => {
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

// 组件挂载
onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.company-container {
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

/* 文本溢出处理样式 */
.text-ellipsis {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  word-break: break-all;
}

/* 表格单元格样式优化 */
:deep(.ant-table-tbody > tr > td) {
  padding: 8px 12px;
  vertical-align: top;
}

/* 长文本字段特殊处理 */
:deep(.ant-table-tbody > tr > td .text-ellipsis) {
  line-height: 1.4;
  max-height: 2.8em; /* 最多显示2行 */
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  white-space: normal;
  word-break: break-word;
}

/* 单行文本字段 */
:deep(.ant-table-tbody > tr > td .text-ellipsis.single-line) {
  max-height: 1.4em;
  -webkit-line-clamp: 1;
  line-clamp: 1;
  white-space: nowrap;
}

/* 链接样式 */
:deep(.ant-table-tbody > tr > td a) {
  text-decoration: none;
  transition: color 0.3s;
}

:deep(.ant-table-tbody > tr > td a:hover) {
  text-decoration: underline;
}

/* 工具提示样式优化 */
:deep(.ant-tooltip-inner) {
  max-width: 300px;
  word-break: break-word;
  white-space: pre-wrap;
}
</style>
