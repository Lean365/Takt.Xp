//===================================================================
// 项目名 : Lean.Takt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 组立日报管理主页面 - 通用主子表架构
//===================================================================

<template>
  <div class="app-container">
    <!-- 主表搜索区域 -->
    <Takt-query
      v-model:show="showSearch"
      :loading="loading"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 主表操作按钮区域 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['logistics:manufacturing:execution:output:workshop1:create']"
      :show-edit="true"
      :edit-permission="['logistics:manufacturing:execution:output:workshop1:update']"
      :show-delete="true"
      :delete-permission="['logistics:manufacturing:execution:output:workshop1:delete']"
      :show-import="true"
      :import-permission="['logistics:manufacturing:execution:output:workshop1:import']"
      :show-export="true"
      :export-permission="['logistics:manufacturing:execution:output:workshop1:export']"
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

    <!-- 主表数据区域 -->
    <Takt-table
      :columns="columns"
      :data-source="dataList"
      :loading="loading"
      :pagination="false"
      :row-selection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :row-key="(record: TaktAssyOutput) => String(record.assyOutputId)"
      @row-click="handleRowClick"
    >
      <template #bodyCell="{ column, record }">
        <!-- 状态列 -->
        <template v-if="column.key === 'status'">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <!-- 班次列 -->
        <template v-else-if="column.key === 'shiftNo'">
          <Takt-dict-tag dict-type="shift_type" :value="record.shiftNo" />
        </template>
        <!-- 操作列 -->
        <template v-else-if="column.key === 'operation'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['logistics:manufacturing:execution:output:workshop1:query']"
            :show-edit="true"
            :edit-permission="['logistics:manufacturing:execution:output:workshop1:update']"
            :show-delete="true"
            :delete-permission="['logistics:manufacturing:execution:output:workshop1:delete']"
            size="small"
            @view="handleDetail"
            @edit="handleUpdate"
            @delete="handleDelete"
          />
        </template>
      </template>
    </Takt-table>

    <!-- 主表分页组件 -->
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

    <!-- 子表数据区域 - 当选中主表记录时显示 -->
    <div v-if="selectedRecord" class="detail-section">
      <div class="detail-header">
        <span class="title">{{ t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.title') }}</span>
        <a-button type="primary" class="ml-4" @click="handleAddSub">
          <template #icon><PlusOutlined /></template>
          {{ t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.table.add') }}
        </a-button>
      </div>
      
      <Takt-table
        :columns="subColumns"
        :data-source="subDataList"
        :loading="subLoading"
        :pagination="false"
        :row-key="(record: TaktAssyOutputDetail) => String(record.assyOutputDetailId)"
      >
        <template #bodyCell="{ column, record }">
          <!-- 状态列 -->
          <template v-if="column.key === 'status'">
            <Takt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
          </template>
          <!-- 操作列 -->
          <template v-else-if="column.key === 'operation'">
            <Takt-operation
              :record="record"
              :show-view="true"
              :view-permission="['logistics:manufacturing:execution:output:workshop1:query']"
              :show-edit="true"
              :edit-permission="['logistics:manufacturing:execution:output:workshop1:update']"
              :show-delete="true"
              :delete-permission="['logistics:manufacturing:execution:output:workshop1:delete']"
              size="small"
              @view="handleDetailSub"
              @edit="handleUpdateSub"
              @delete="handleDeleteSub"
            />
          </template>
        </template>
      </Takt-table>

      <!-- 子表分页组件 -->
      <Takt-pagination
        v-model:current="subQueryParams.pageIndex"
        v-model:pageSize="subQueryParams.pageSize"
        :total="subTotal"
        :show-size-changer="true"
        :show-quick-jumper="true"
        :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
        @change="handleSubPageChange"
        @showSizeChange="handleSubSizeChange"
      />
    </div>

    <!-- 主表表单对话框 -->
    <assy-output-form
      v-model:open="open"
      :title="title"
      :assy-output-id="form.assyOutputId"
      @success="handleSuccess"
      @cancel="cancel"
    />

    <!-- 子表表单对话框 -->
    <assy-output-detail-form
      v-model:open="subOpen"
      :title="subTitle"
      :assy-output-id="selectedRecord?.assyOutputId || 0"
      :detail-id="subForm.assyOutputDetailId"
      @success="handleSubSuccess"
      @cancel="cancelSub"
    />

    <!-- 主表详情对话框 -->
    <assy-output-detail
      v-model:open="detailOpen"
      :assy-output-id="selectedRecord?.assyOutputId"
    />

    <!-- 子表详情对话框 -->
    <assy-output-detail-detail
      v-model:open="subDetailOpen"
      :detail-id="subForm.assyOutputDetailId"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      title="列设置"
      placement="right"
      width="300"
      @close="columnSettingVisible = false"
    >
      <div class="drawer-header-actions">
        <a-button type="primary" size="small" @click="resetColumnSettings">
          重置为默认
        </a-button>
      </div>
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
import { ref, reactive, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { TaktAssyOutput, TaktAssyOutputQuery } from '@/types/logistics/manufacturing/execution/output/assyOutput'
import type { TaktAssyOutputDetail, TaktAssyOutputDetailQuery } from '@/types/logistics/manufacturing/execution/output/assyOutputDetail'
import {
  getAssyOutputList,
  getAssyOutput,
  createAssyOutput,
  updateAssyOutput,
  deleteAssyOutput,
  batchDeleteAssyOutput,
  importAssyOutput,
  exportAssyOutput,
  getTemplate
} from '@/api/logistics/manufacturing/execution/output/assyOutput'
import {
  getAssyOutputDetailList,
  getAssyOutputDetail,
  deleteAssyOutputDetail
} from '@/api/logistics/manufacturing/execution/output/assyOutputDetail'

// 导入组件
import AssyOutputForm from './components/AssyOutputForm.vue'
import AssyOutputDetailForm from './components/AssyOutputDetailForm.vue'
import AssyOutputDetail from './components/AssyOutputDetail.vue'
import AssyOutputDetailDetail from './components/AssyOutputDetailDetail.vue'

const { t } = useI18n()

// === 状态定义 ===
// 遮罩层
const loading = ref(false)
const subLoading = ref(false)
const submitLoading = ref(false)
const subSubmitLoading = ref(false)

// 选中数组
const selectedRowKeys = ref<number[]>([])
const selectedRecord = ref<TaktAssyOutput | null>(null)

// 显示搜索条件
const showSearch = ref(true)

// 总条数
const total = ref(0)
const subTotal = ref(0)

// 主表表格数据
const dataList = ref<TaktAssyOutput[]>([])
// 子表表格数据
const subDataList = ref<TaktAssyOutputDetail[]>([])

// 弹出层标题
const title = ref('')
const subTitle = ref('')

// 是否显示弹出层
const open = ref(false)
const subOpen = ref(false)
const detailOpen = ref(false)
const subDetailOpen = ref(false)

// 查询参数
const queryParams = reactive<TaktAssyOutputQuery>({
  pageIndex: 1,
  pageSize: 10,
  plantCode: '',
  startProdDate: '',
  endProdDate: '',
  prodLine: '',
  prodOrderType: '',
  prodOrderCode: '',
  modelCode: '',
  materialCode: '',
  batchNo: '',
  status: undefined
})

// 子表查询参数
const subQueryParams = reactive<TaktAssyOutputDetailQuery>({
  pageIndex: 1,
  pageSize: 10,
  assyOutputId: 0,
  timePeriod: ''
})

// 表单参数
const form = reactive<Partial<TaktAssyOutput>>({
  assyOutputId: undefined,
  plantCode: '',
  prodDate: '',
  prodLine: '',
  shiftNo: 1,
  prodOrderType: '',
  prodOrderCode: '',
  modelCode: '',
  materialCode: '',
  batchNo: '',
  directLabor: 0,
  indirectLabor: 0,
  prodOrderQty: 0,
  stdMinutes: 0,
  stdCapacity: 0,
  status: 0,
  remark: ''
})

// 子表表单参数
const subForm = reactive<Partial<TaktAssyOutputDetail>>({
  assyOutputDetailId: undefined,
  assyOutputId: 0,
  timePeriod: '',
  prodActualQty: 0,
  downtimeMinutes: 0,
  downtimeReason: '',
  downtimeDescription: '',
  unachievedReason: '',
  unachievedDescription: '',
  inputMinutes: 0,
  prodMinutes: 0,
  actualMinutes: 0,
  achievementRate: 0,
  remark: ''
})

// 分页参数已移除，使用 Takt-pagination 组件

// === 查询字段定义 ===
const queryFields = [
  {
    type: 'input' as const,
    name: 'plantCode',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.plantCode.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.plantCode.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'dateRange' as const,
    name: 'prodDate',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodDate.label'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'prodLine',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodLine.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodLine.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'prodOrderType',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderType.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderType.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'prodOrderCode',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderCode.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderCode.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'modelCode',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.modelCode.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.modelCode.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'materialCode',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.materialCode.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.materialCode.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'input' as const,
    name: 'batchNo',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.batchNo.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.batchNo.placeholder'),
    props: {
      allowClear: true
    }
  },
  {
    type: 'select' as const,
    name: 'status',
    label: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.status.label'),
    placeholder: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.status.placeholder'),
    props: {
      allowClear: true
    },
    options: [
      { label: '启用', value: 1 },
      { label: '禁用', value: 0 }
    ]
  }
]

// === 表格列定义 ===
// 主表列定义
const defaultColumns = [
  {
    title:'ID',
    dataIndex: 'assyOutputId',
    key: 'assyOutputId',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.plantCode.label'),
    dataIndex: 'plantCode',
    key: 'plantCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodDate.label'),
    dataIndex: 'prodDate',
    key: 'prodDate',
    width: 120
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodLine.label'),
    dataIndex: 'prodLine',
    key: 'prodLine',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.shiftNo.label'),
    dataIndex: 'shiftNo',
    key: 'shiftNo',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderType.label'),
    dataIndex: 'prodOrderType',
    key: 'prodOrderType',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderCode.label'),
    dataIndex: 'prodOrderCode',
    key: 'prodOrderCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.modelCode.label'),
    dataIndex: 'modelCode',
    key: 'modelCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.materialCode.label'),
    dataIndex: 'materialCode',
    key: 'materialCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.batchNo.label'),
    dataIndex: 'batchNo',
    key: 'batchNo',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.prodOrderQty.label'),
    dataIndex: 'prodOrderQty',
    key: 'prodOrderQty',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.directLabor.label'),
    dataIndex: 'directLabor',
    key: 'directLabor',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.indirectLabor.label'),
    dataIndex: 'indirectLabor',
    key: 'indirectLabor',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.stdMinutes.label'),
    dataIndex: 'stdMinutes',
    key: 'stdMinutes',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutput.fields.stdCapacity.label'),
    dataIndex: 'stdCapacity',
    key: 'stdCapacity',
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

// 子表列定义
const subColumns = [
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.timePeriod.label'),
    dataIndex: 'timePeriod',
    key: 'timePeriod',
    width: 120
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.prodActualQty.label'),
    dataIndex: 'prodActualQty',
    key: 'prodActualQty',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.downtimeMinutes.label'),
    dataIndex: 'downtimeMinutes',
    key: 'downtimeMinutes',
    width: 120
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.downtimeReason.label'),
    dataIndex: 'downtimeReason',
    key: 'downtimeReason',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.downtimeDescription.label'),
    dataIndex: 'downtimeDescription',
    key: 'downtimeDescription',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.unachievedReason.label'),
    dataIndex: 'unachievedReason',
    key: 'unachievedReason',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.unachievedDescription.label'),
    dataIndex: 'unachievedDescription',
    key: 'unachievedDescription',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.inputMinutes.label'),
    dataIndex: 'inputMinutes',
    key: 'inputMinutes',
    width: 120
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.prodMinutes.label'),
    dataIndex: 'prodMinutes',
    key: 'prodMinutes',
    width: 120
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.actualMinutes.label'),
    dataIndex: 'actualMinutes',
    key: 'actualMinutes',
    width: 100
  },
  {
    title: t('logistics.manufacturing.outputs.assy.assyMpOutputDetail.fields.achievementRate.label'),
    dataIndex: 'achievementRate',
    key: 'achievementRate',
    width: 100
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
    width: 160
  },
  {
    title: t('table.columns.operation'),
    key: 'operation',
    width: 200,
    fixed: 'right'
  }
]

// 列设置相关
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})
const columns = computed(() => defaultColumns.filter(col => columnSettings.value[col.key]))

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('assyMpOutputColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
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
  localStorage.setItem('assyMpOutputColumnSettings', JSON.stringify(settings))
}

// === 方法定义 ===
// 获取主表数据
const fetchData = async () => {
  try {
    loading.value = true
    const res = await getAssyOutputList(queryParams)
    if (res.data.code === 200) {
      dataList.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载主表数据失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 获取子表数据
const fetchSubData = async () => {
  if (!selectedRecord.value) return
  
  try {
    subLoading.value = true
    const queryParams = {
      ...subQueryParams,
      assyOutputId: selectedRecord.value.assyOutputId
    }
    
    // 调用子表API获取数据
    const res = await getAssyOutputDetailList(queryParams)
    if (res.data.code === 200) {
      subDataList.value = res.data.data.rows
      subTotal.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载子表数据失败:', error)
    message.error(t('common.failed'))
  } finally {
    subLoading.value = false
  }
}

// 查询
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams, values)
  }
  queryParams.pageIndex = 1
  fetchData()
}

// 重置
const resetQuery = () => {
  Object.assign(queryParams, {
    pageIndex: 1,
    pageSize: 10,
    plantCode: '',
    startProdDate: '',
    endProdDate: '',
    prodLine: '',
    prodOrderType: '',
    prodOrderCode: '',
    modelCode: '',
    materialCode: '',
    batchNo: '',
    status: undefined
  })
  fetchData()
}

// 新增主表
const handleAdd = () => {
  resetForm()
  title.value = t('common.title.create')
  open.value = true
}

// 编辑选中主表
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = dataList.value.find(item => item.assyOutputId === selectedRowKeys.value[0])
  if (record) {
    handleUpdate(record)
  }
}

// 编辑主表
const handleUpdate = (record: TaktAssyOutput) => {
  resetForm()
  const id = record.assyOutputId
  if (id) {
    getDetail(id)
  }
  title.value = t('common.title.edit')
  open.value = true
}

// 查看主表详情
const handleDetail = (record: TaktAssyOutput) => {
  selectedRecord.value = record
  detailOpen.value = true
}

// 删除主表
const handleDelete = async (record: TaktAssyOutput) => {
  const ids = [record.assyOutputId]
  await handleBatchDelete(ids)
}

// 批量删除主表
const handleBatchDelete = async (ids?: number[]) => {
  const deleteIds = ids || selectedRowKeys.value
  if (deleteIds.length === 0) {
    message.warning(t('common.message.selectRecord'))
    return
  }
  
  try {
    const res = await batchDeleteAssyOutput(deleteIds)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      selectedRowKeys.value = []
      selectedRecord.value = null
      fetchData()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 新增子表
const handleAddSub = () => {
  resetSubForm()
  subTitle.value = t('common.title.create')
  subOpen.value = true
}

// 编辑子表
const handleUpdateSub = (record: TaktAssyOutputDetail) => {
  resetSubForm()
  const id = record.assyOutputDetailId
  if (id) {
    getSubDetail(id)
  }
  subTitle.value = t('common.title.edit')
  subOpen.value = true
}

// 查看子表详情
const handleDetailSub = (record: TaktAssyOutputDetail) => {
  subForm.assyOutputDetailId = record.assyOutputDetailId
  subDetailOpen.value = true
}

// 删除子表
const handleDeleteSub = async (record: TaktAssyOutputDetail) => {
  try {
    const res = await deleteAssyOutputDetail(record.assyOutputDetailId)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchSubData()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 导入
const handleImport = () => {
  // TODO: 实现导入功能
  message.info('导入功能开发中')
}

// 下载模板
const handleTemplate = () => {
  // TODO: 实现下载模板功能
  message.info('下载模板功能开发中')
}

// 导出
const handleExport = () => {
  // TODO: 实现导出功能
  message.info('导出功能开发中')
}

// 获取主表详情
const getDetail = async (id: number) => {
  try {
    const res = await getAssyOutput(id)
    if (res.data.code === 200) {
      Object.assign(form, res.data.data)
    } else {
      message.error(res.data.msg || t('common.message.queryFailed'))
    }
  } catch (error) {
    console.error('获取详情失败:', error)
    message.error(t('common.message.queryFailed'))
  }
}

// 获取子表详情
const getSubDetail = async (id: number) => {
  try {
    const res = await getAssyOutputDetail(id)
    if (res.data.code === 200) {
      Object.assign(subForm, res.data.data)
    } else {
      message.error(res.data.msg || t('common.message.queryFailed'))
    }
  } catch (error) {
    console.error('获取子表详情失败:', error)
    message.error(t('common.message.queryFailed'))
  }
}

// 重置主表表单
const resetForm = () => {
  Object.assign(form, {
    assyOutputId: undefined,
    plantCode: '',
    prodDate: '',
    prodLine: '',
    shiftNo: 1,
    prodOrderCode: '',
    modelCode: '',
    materialCode: '',
    batchNo: '',
    directLabor: 0,
    indirectLabor: 0,
    prodOrderQty: 0,
    stdMinutes: 0,
    stdCapacity: 0,
    remark: ''
  })
}

// 重置子表表单
const resetSubForm = () => {
  Object.assign(subForm, {
    assyOutputDetailId: undefined,
    assyOutputId: selectedRecord.value?.assyOutputId || 0,
    timePeriod: '',
    prodActualQty: 0,
    downtimeMinutes: 0,
    downtimeReason: '',
    downtimeDescription: '',
    unachievedReason: '',
    unachievedDescription: '',
    inputMinutes: 0,
    prodMinutes: 0,
    actualMinutes: 0,
    achievementRate: 0,
    remark: ''
  })
}

// 主表表单提交成功
const handleSuccess = () => {
  open.value = false
  fetchData()
}

// 子表表单提交成功
const handleSubSuccess = () => {
  subOpen.value = false
  fetchSubData()
}

// 取消主表表单
const cancel = () => {
  open.value = false
  resetForm()
}

// 取消子表表单
const cancelSub = () => {
  subOpen.value = false
  resetSubForm()
}

// 行选择变化
const onSelectChange = (keys: (string | number)[], selectedRows: any[]) => {
  selectedRowKeys.value = keys.map(key => Number(key))
}

// 行点击事件
const handleRowClick = (record: TaktAssyOutput) => {
  selectedRecord.value = record
  fetchSubData()
}

// 主表分页处理
const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.pageSize = size
  fetchData()
}

// 子表分页处理
const handleSubPageChange = (page: number) => {
  subQueryParams.pageIndex = page
  fetchSubData()
}

const handleSubSizeChange = (size: number) => {
  subQueryParams.pageSize = size
  fetchSubData()
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 重置列设置为默认
const resetColumnSettings = () => {
  // 清除localStorage
  localStorage.removeItem('assyMpOutputColumnSettings')
  
  // 重新初始化列设置
  initColumnSettings()
  
  message.success('列设置已重置为默认')
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// === 生命周期 ===
onMounted(() => {
  initColumnSettings()
  fetchData()
})
</script>

<style lang="less" scoped>
.app-container {
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  border-radius: 4px;
}

.detail-section {
  margin-top: 16px;
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  border: 1px solid var(--ant-color-split);
  border-radius: 4px;
}

.detail-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 16px;
  padding-bottom: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  .title {
    font-size: 16px;
    font-weight: 500;
    color: var(--ant-color-text);
  }
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.drawer-header-actions {
  margin-bottom: 16px;
  padding-bottom: 8px;
  border-bottom: 1px solid var(--ant-color-split);
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  &:last-child {
    border-bottom: none;
  }
}
</style> 
