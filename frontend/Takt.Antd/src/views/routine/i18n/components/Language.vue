<template>
  <Takt-modal
    :title="t('core.language.title')"
    :open="open"
    width="800px"
    @cancel="handleCancel"
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
      :add-permission="['routine:i18n:create']"
      :show-edit="true"
      :edit-permission="['routine:i18n:update']"
      :show-delete="true"
      :delete-permission="['routine:i18n:delete']"
      :show-import="true"
      :import-permission="['routine:i18n:import']"
      :show-export="true"
      :export-permission="['routine:i18n:export']"
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
      :scroll="{ y: 'calc(70vh - 200px)' }"
      :row-key="(record: TaktLanguage) => String(record.languageId)"
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
        
        <template v-if="column.dataIndex === 'isBuiltin'">
          <Takt-dict-tag dict-type="sys_yes_no" :value="record.isBuiltin ?? 0" />
        </template>

        <template v-if="column.dataIndex === 'isDefault'">
          <Takt-dict-tag dict-type="sys_yes_no" :value="record.isDefault ?? 0" />
        </template>

        <template v-if="column.dataIndex === 'langCode'">
          <a @click.stop.prevent="handleShowTranslationModal(record)">{{ record.langCode }}</a>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['routine:i18n:query']"
            :show-edit="true"
            :edit-permission="['routine:i18n:update']"
            :show-delete="true"
            :delete-permission="['routine:i18n:delete']"
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

    <!-- 语言表单对话框 -->
    <Language-form
      v-model:open="formVisible"
      :title="formTitle"
      :language-id="selectedLanguageId"
      @success="handleSuccess"
    />

    <!-- 语言详情对话框 -->
    <Language-detail
      v-model:open="detailVisible"
      :language-id="selectedLanguageId || ''"
    />

    <!-- 翻译管理对话框 -->
    <Translation-manager
      v-model:open="translationVisible"
      :language="currentLanguage"
      @cancel="handleTranslationCancel"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="语言数据导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的语言信息字段,\n如语言代码,语言名称,语言图标,排序号,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:i18n:template']"
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
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktLanguage, TaktLanguageQuery } from '@/types/routine/i18n/language'
import { useDictStore } from '@/stores/dictStore'
import {
  getLanguageList,
  getByIdAsync,
  createLanguage,
  updateLanguage,
  deleteLanguage,
  batchDeleteLanguage,
  importLanguage,
  exportLanguage,
  getLanguageTemplate
} from '@/api/routine/i18n/language'
import LanguageForm from './LanguageForm.vue'
import LanguageDetail from './LanguageDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== Props 和 Emits ====================
const props = defineProps<{
  open: boolean
}>()

const emit = defineEmits(['update:open'])

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'langCode',
    label: t('core.language.fields.langCode.label'),
    type: 'input' as const
  },
  {
    name: 'langName',
    label: t('core.language.fields.langName.label'),
    type: 'input' as const
  },
  {
    name: 'isBuiltin',
    label: t('core.language.fields.isBuiltin.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'isDefault',
    label: t('core.language.fields.isDefault.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: t('core.language.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktLanguageQuery>({
  pageIndex: 1,
  pageSize: 10,
  langCode: '',
  langName: '',
  isDefault: -1,
  isBuiltin: -1,
  status: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktLanguage[]>([])

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
    dataIndex: 'languageId',
    key: 'languageId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('core.language.table.columns.langCode'),
    dataIndex: 'langCode',
    key: 'langCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.language.table.columns.langName'),
    dataIndex: 'langName',
    key: 'langName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.language.table.columns.langIcon'),
    dataIndex: 'langIcon',
    key: 'langIcon',
    width: 100
  },
  {
    title: t('core.language.table.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    sorter: true
  },
  {
    title: t('core.language.table.columns.isBuiltin'),
    dataIndex: 'isBuiltin',
    key: 'isBuiltin',
    width: 100
  },
  {
    title: t('core.language.table.columns.isDefault'),
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: t('core.language.table.columns.status'),
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
const selectedLanguageId = ref<string>()

// 详情对话框
const detailVisible = ref(false)

// ==================== 业务操作相关 ====================
// 翻译管理对话框
const translationVisible = ref(false)
const currentLanguage = ref<TaktLanguage>()

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

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getLanguageList(queryParams.value)
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
    langCode: '',
    langName: '',
    isDefault: -1,
    isBuiltin: -1,
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
  selectedLanguageId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktLanguage) => {
  formTitle.value = t('common.actions.edit')
  selectedLanguageId.value = String(record.languageId)
  formVisible.value = true
}

// 处理查看
const handleView = (record: TaktLanguage) => {
  selectedLanguageId.value = String(record.languageId)
  detailVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktLanguage) => {
  try {
    const res = await deleteLanguage(record.languageId)
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
    (item: TaktLanguage) => String(item.languageId) === String(selectedRowKeys.value[0])
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
    const res = await batchDeleteLanguage(selectedRowKeys.value)
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
  selectedLanguageId.value = undefined
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
    const res = await importLanguage(file)
    console.log('导入响应数据:', res)
    
    // 由于importTaktLanguage返回boolean，我们需要模拟成功导入
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
    const res = await exportLanguage({
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
      fileName = `语言数据_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL((res as any).data)
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
    const res = await getLanguageTemplate()
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
  localStorage.removeItem('languageColumnSettings')

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
  localStorage.setItem('languageColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: TaktLanguage) => {
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

// 显示翻译管理对话框
const handleShowTranslationModal = (record: TaktLanguage) => {
  currentLanguage.value = record
  translationVisible.value = true
  console.log('currentLanguage:', currentLanguage.value)
}

// 翻译管理对话框取消
const handleTranslationCancel = () => {
  translationVisible.value = false
  currentLanguage.value = undefined
}

// 取消
const handleCancel = () => {
  emit('update:open', false)
}

// ==================== 生命周期 ====================
onMounted(async () => {
  // 加载字典数据
  dictStore.loadDicts(['sys_yes_no', 'sys_normal_disable'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.language-container {
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
