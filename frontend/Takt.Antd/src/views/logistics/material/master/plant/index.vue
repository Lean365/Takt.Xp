<template>
  <div class="plant-container">
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
      :add-permission="['logistics:material:master:plant:create']"
      :show-edit="true"
      :edit-permission="['logistics:material:master:plant:update']"
      :show-delete="true"
      :delete-permission="['logistics:material:master:plant:delete']"
      :show-import="true"
      :import-permission="['logistics:material:master:plant:import']"
      :show-export="true"
      :export-permission="['logistics:material:master:plant:export']"
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
      row-key="plantId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 工厂类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'plantType'">
          <Takt-dict-tag dict-type="logistics_plant_type" :value="record.plantType" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <a-switch
            v-hasPermi="['logistics:material:master:plant:update']"
            :checked="record.status === 0"
            :checked-children="dictStore.getDictLabel('sys_normal_disable', 0)"
            :un-checked-children="dictStore.getDictLabel('sys_normal_disable', 1)"
            @change="(val: any) => handleStatusChange(record, !!val)"
            :loading="record.statusLoading"
          />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['logistics:material:master:plant:update']"
            :show-delete="true"
            :delete-permission="['logistics:material:master:plant:delete']"
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

    <!-- 工厂表单对话框 -->
    <plant-form
      v-model:visible="plantFormVisible"
      :title="formTitle"
      :plant-id="selectedPlantId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleTemplate"
      template-file-name="工厂导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的工厂信息字段,\n如工厂编码,工厂名称,工厂类型,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['logistics:material:master:plant:template']"
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
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktPlant, TaktPlantQuery } from '@/types/logistics/material/master/plant'
import { useDictStore } from '@/stores/dictStore'
import {
  getPlantList,
  deletePlant,
  exportPlant,
  importPlant,
  getTemplate,
  createPlant,
  updatePlant,
  updatePlantStatus
} from '@/api/logistics/material/master/plant'
import PlantForm from './components/PlantForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'plantCode',
    label: t('logistics.material.master.plant.fields.plantCode.label'),
    type: 'input' as const,
  },
  {
    name: 'plantName',
    label: t('logistics.material.master.plant.fields.plantName.label'),
    type: 'input' as const
  },
  {
    name: 'plantType',
    label: t('logistics.material.master.plant.fields.plantType.label'),
    type: 'select' as const,
    props: {
      dictType: 'logistics_plant_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: t('logistics.material.master.plant.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktPlantQuery>({
  pageIndex: 1,
  pageSize: 10,
  plantCode: '',
  plantName: '',
  plantType: -1,
  status: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktPlant[]>([])

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
    dataIndex: 'plantId',
    key: 'plantId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.plantCode.label'),
    dataIndex: 'plantCode',
    key: 'plantCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.plantName.label'),
    dataIndex: 'plantName',
    key: 'plantName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.plantShortName.label'),
    dataIndex: 'plantShortName',
    key: 'plantShortName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.plantType.label'),
    dataIndex: 'plantType',
    key: 'plantType',
    width: 120
  },
  {
    title: t('logistics.material.master.plant.fields.email.label'),
    dataIndex: 'email',
    key: 'email',
    width: 180,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.phone.label'),
    dataIndex: 'phone',
    key: 'phone',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.contactPerson.label'),
    dataIndex: 'contactPerson',
    key: 'contactPerson',
    width: 120,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.plantAddress.label'),
    dataIndex: 'plantAddress',
    key: 'plantAddress',
    width: 200,
    ellipsis: true
  },
  {
    title: t('logistics.material.master.plant.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
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

// ==================== 表单相关 ====================
// 表单对话框
const plantFormVisible = ref(false)
const formTitle = ref('')
const selectedPlantId = ref<string>()

// 导入对话框
const importVisible = ref(false)

// ==================== 业务操作相关 ====================

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('plantColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getPlantList(queryParams.value)
    console.log('res:', res)
    tableData.value = res.data.rows
    total.value = res.data.totalNum
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 查询方法
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
    plantCode: '',
    plantName: '',
    plantType: undefined,
    status: -1
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('common.actions.add')
  selectedPlantId.value = undefined
  plantFormVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktPlant) => {
  formTitle.value = t('common.actions.edit')
  selectedPlantId.value = record.plantId
  plantFormVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktPlant) => {
  try {
    await deletePlant(record.plantId)
    message.success(t('common.delete.success'))
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// ==================== 导出相关 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportPlant({
      ...queryParams.value
    })
    // 动态获取文件名
    console.log('res.headers', res.headers)
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    console.log('disposition', disposition)
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
      fileName = `工厂数据_${new Date().getTime()}.xlsx`
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

// 处理表单提交成功
const handleSuccess = () => {
  plantFormVisible.value = false
  fetchData()
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(
    item => String(item.plantId) === String(selectedRowKeys.value[0])
  )
  if (record) {
    handleEdit(record)
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    await Promise.all(selectedRowKeys.value.map(id => deletePlant(String(id))))
    message.success(t('common.delete.success'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else if (col.key) {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('plantColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: TaktPlant) => {
  console.log('行点击:', record)
}

// ==================== 导入相关 ====================
// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importPlant(file)
    console.log('导入响应数据:', res)
    console.log('res.data:', res.data)
    
    // res.data 包含 { success, fail } 对象
    const { success = 0, fail = 0 } = res.data
    
    console.log('解析后的数据:', { success, fail })
    
    return {
      code: 200,
      msg: '导入成功',
      data: {
        success,
        fail
      }
    }
  } catch (error: any) {
    console.error('导入失败:', error)
    throw error
  }
}

// ==================== 模板相关 ====================
// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    throw error
  }
}

// 处理导入成功
const handleImportSuccess = () => {
  fetchData()
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

// 处理状态变化
const handleStatusChange = async (record: TaktPlant, checked: boolean) => {
  // @ts-ignore
  record.statusLoading = true
  try {
    const newStatus = checked ? 0 : 1
    await updatePlantStatus({ plantId: record.plantId, status: newStatus })
    record.status = newStatus
    message.success('状态更新成功')
  } catch (error) {
    message.error('状态更新失败')
  }
  // @ts-ignore
  record.statusLoading = false
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable', 'logistics_plant_type'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.plant-container {
  height: 100%;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  &:last-child {
    border-bottom: none;
  }
}
</style>

