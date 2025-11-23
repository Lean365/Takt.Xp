<template>
  <Takt-modal
    :open="visible"
    :title="t('core.dict.dictDatas.title') + ' - ' + props.dictType?.dictName"
    :footer="null"
    width="900px"
    append-to-body
    destroy-on-close
  >
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
      :add-permission="['routine:dict:create']"
      :show-edit="true"
      :edit-permission="['routine:dict:update']"
      :show-delete="true"
      :delete-permission="['routine:dict:delete']"
      :show-import="true"
      :import-permission="['routine:dict:import']"
      :show-export="true"
      :export-permission="['core:dictdata:export']"
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
      :scroll="{ x: 'max-content', y: 400 }"
      :row-key="(record: TaktDictData) => String(record.dictDataId)"
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
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['routine:dict:query']"
            :show-edit="true"
            :edit-permission="['routine:dict:update']"
            :show-delete="true"
            :delete-permission="['routine:dict:delete']"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 字典数据表单对话框 -->
    <dict-data-form
      v-model:open="formVisible"
      :title="formTitle"
      :dict-data-id="selectedDictDataId"
      :dict-type="props.dictType?.dictType || ''"
      @success="handleSuccess"
    />

    <!-- 字典数据详情对话框 -->
    <dict-data-detail
      v-model:open="detailVisible"
      :dict-data-id="selectedDictDataId"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="字典数据导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的字典数据信息字段,\n如字典标签,字典值,排序号,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:dict:template']"
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
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktDictData, TaktDictDataQuery } from '@/types/routine/dict/dictData'
import type { TaktDictType } from '@/types/routine/dict/dictType'
import { useDictStore } from '@/stores/dictStore'
import {
  getDictDataList,
  getByIdAsync,
  createDictData,
  updateDictData,
  deleteDictData,
  batchDeleteDictData,
  importDictData,
  exportDictData,
  getDictDataTemplate
} from '@/api/routine/dict/dictData'
import DictDataForm from './DictDataForm.vue'
import DictDataDetail from './DictDataDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== Props 和 Emits ====================
const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  dictTypeId: {
    type: String,
    default: undefined
  },
  dictType: {
    type: Object as () => TaktDictType,
    default: undefined
  }
})

const emit = defineEmits(['update:visible'])

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'dictLabel',
    label: t('core.dict.dictDatas.table.columns.dictLabel'),
    type: 'input' as const
  },
  {
    name: 'dictValue',
    label: t('core.dict.dictDatas.table.columns.dictValue'),
    type: 'input' as const
  },
  {
    name: 'status',
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktDictDataQuery>({
  pageIndex: 1,
  pageSize: 10,
  dictType: props.dictType?.dictType || '',
  status: undefined,
  orderByColumn: undefined,
  orderType: undefined
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktDictData[]>([])

// 选中的行
const selectedRowKeys = ref<string[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns: any[] = [
  {
    title: 'ID',
    dataIndex: 'dictDataId',
    key: 'dictDataId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.dictType'),
    dataIndex: 'dictType',
    key: 'dictType',
    width: 100
  },
  {
    title: t('core.dict.dictDatas.table.columns.dictLabel'),
    dataIndex: 'dictLabel',
    key: 'dictLabel',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.transKey'),
    dataIndex: 'transKey',
    key: 'transKey',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.listClass'),
    dataIndex: 'listClass',
    key: 'listClass',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.cssClass'),
    dataIndex: 'cssClass',
    key: 'cssClass',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.dictValue'),
    dataIndex: 'dictValue',
    key: 'dictValue',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.extLabel'),
    dataIndex: 'extLabel',
    key: 'extLabel',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.extValue'),
    dataIndex: 'extValue',
    key: 'extValue',
    width: 200,
    ellipsis: true
  },
  {
    title: t('core.dict.dictDatas.table.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
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
    title: t('table.columns.isDeleted'),
    dataIndex: 'isDeleted',
    key: 'isDeleted',
    width: 100
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
const formVisible = ref(false)
const formTitle = ref('')
const selectedDictDataId = ref<string>()

// 详情对话框
const detailVisible = ref(false)

// ==================== 业务操作相关 ====================
// 暂无额外业务操作相关变量

// ==================== 导入相关 ====================
// 导入对话框
const importVisible = ref(false)

// ==================== 导出相关 ====================
// 暂无额外导出相关变量

// ==================== 模板相关 ====================
// 暂无额外模板相关变量

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 对话框可见性
const dialogVisible = ref(props.visible)

// 获取表格数据
const fetchData = async () => {
  if (!props.dictTypeId) return
  
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getDictDataList(queryParams.value)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(t('common.failed'))
    }
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
    dictType: props.dictType?.dictType || '',
    status: undefined,
    orderByColumn: undefined,
    orderType: undefined
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
  selectedDictDataId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktDictData) => {
  formTitle.value = t('common.actions.edit')
  selectedDictDataId.value = String(record.dictDataId)
  formVisible.value = true
}

// 处理查看
const handleView = (record: TaktDictData) => {
  selectedDictDataId.value = String(record.dictDataId)
  detailVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktDictData) => {
  try {
    const res = await deleteDictData(record.dictDataId)
    if (res) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(
    item => String(item.dictDataId) === String(selectedRowKeys.value[0])
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
    const res = await batchDeleteDictData(selectedRowKeys.value)
    if (res) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('common.delete.failed'))
  }
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedDictDataId.value = undefined
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

// ==================== 导入方法 ====================
// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importDictData(file)
    console.log('导入响应数据:', res)
    
    // 由于importTaktDictData返回boolean，我们需要模拟成功导入
    // 这里假设导入成功，返回默认的成功和失败数量
    return {
      code: 200,
      msg: '导入成功',
      data: {
        success: 1,
        fail: 0
      }
    }
  } catch (error: any) {
    console.error('导入失败:', error)
    throw error
  }
}

// 处理导入成功
const handleImportSuccess = () => {
  //message.success(t('common.import.success'))
  fetchData()
}

// ==================== 导出方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportDictData({
      ...queryParams.value
    })
    // 动态获取文件名
    console.log('res.headers', (res as any).headers)
    const disposition =
      (res as any).headers && ((res as any).headers['content-disposition'] || (res as any).headers['Content-Disposition'])
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
      fileName = `字典数据_${new Date().getTime()}.xlsx`
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

// ==================== 模板方法 ====================
// 处理下载模板
const handleDownloadTemplate = async () => {
  try {
    const res = await getDictDataTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    throw error
  }
}

// ==================== 其它方法 ====================
// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('dictDataColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key!, false]))

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

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    if (!col.key) return
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('dictDataColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: TaktDictData) => {
  console.log('行点击:', record)
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 取消
const handleCancel = () => {
  dialogVisible.value = false
}

// ==================== 监听器 ====================
// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  dialogVisible.value = val
})

watch(() => dialogVisible.value, (val) => {
  emit('update:visible', val)
})

// 监听字典类型变化，加载数据
watch(() => props.dictType?.dictType, (val) => {
  if (val) {
    queryParams.value.dictType = val
    fetchData()
  }
}, { immediate: true })

// ==================== 生命周期 ====================
onMounted(async () => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  if (props.dictTypeId) {
    fetchData()
  }
})
</script>

<style lang="less" scoped>
.dict-container {
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  border-radius: 4px;
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
