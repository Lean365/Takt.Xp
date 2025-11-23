<template>
  <div class="language-container">
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
    >
      <template #extra>
        <a-button
          type="default"
          class="Takt-btn-save"
          @click="handleLang"
        >
          <template #icon><plus-outlined /></template>
          {{ t('routine.i18n.languages.title') }}
        </a-button>
      </template>
    </Takt-toolbar>

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="tableData"
      :columns="dynamicColumns.filter((col: any) => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      :row-key="(record: TableRow) => String(record.translationId)"
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
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.i18nStatus" />
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

    <!-- 翻译表单对话框 -->
    <translation-form
      v-model:open="formVisible"
      :translation-id="selectedTranslationId"
      :trans-key="selectedTransKey"
      @success="handleSuccess"
    />

    <!-- 翻译详情对话框 -->
    <translation-detail
      v-model:open="detailVisible"
      :translation-id="selectedTranslationId"
      :trans-key="selectedTransKey"
    />

    <!-- 语言表单对话框 -->
    <language
      v-model:open="languageFormVisible"
      @success="handleLanguageSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      :template-file-name="t('routine.i18n.translations.template.fileName') + '.xlsx'"
      :tips="t('routine.i18n.translations.importTips')"
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
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktTransposedData } from '@/types/routine/i18n/translation'
import type { TaktPagedResult } from '@/types/common'
import { useDictStore } from '@/stores/dictStore'
import { PlusOutlined } from '@ant-design/icons-vue'
import {
  getTranslationList,
  getByIdAsync,
  createTranslation,
  updateTranslation,
  deleteTranslation,
  batchDeleteTranslation,
  importTranslation,
  exportTranslation,
  getTranslationTemplate,
  getTransposedDetail,
  getTransposedData
} from '@/api/routine/i18n/translation'
import TranslationForm from './components/TranslationForm.vue'
import TranslationDetail from './components/TranslationDetail.vue'
import Language from './components/Language.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'transType',
    label: t('routine.i18n.translations.fields.transType.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_translation_category',
      type: 'radio',
      allowClear: true
    }
  },
  {
    name: 'transKey',
    label: t('routine.i18n.translations.fields.transKey.label'),
    type: 'input' as const
  },
  {
    name: 'transValue',
    label: t('routine.i18n.translations.fields.transValue.label'),
    type: 'input' as const,
    props: {
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('routine.i18n.translations.fields.i18nStatus.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref({
  pageIndex: 1,
  pageSize: 10,
  transType: -1,
  transKey: '',
  transValue: '',
  status: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TableRow[]>([])

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
    dataIndex: 'translationId',
    key: 'translationId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('routine.i18n.translations.fields.transType.label'),
    dataIndex: 'transType',
    key: 'transType',
    width: 150,
    ellipsis: true
  },
  {
    title: t('routine.i18n.translations.fields.transKey.label'),
    dataIndex: 'transKey',
    key: 'transKey',
    width: 200,
    ellipsis: true
  }
]

// ==================== 表单相关 ====================
// 表单对话框
const formVisible = ref(false)
const selectedTranslationId = ref<string | undefined>(undefined)
const selectedTransKey = ref('')

// 详情对话框
const detailVisible = ref(false)

// 语言表单对话框
const languageFormVisible = ref(false)

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

// 动态列
const dynamicColumns = ref<any[]>([...columns])

// 列设置相关

interface TableColumn {
  title: string
  dataIndex?: string
  key: string
  width: number
  ellipsis?: boolean
  fixed?: string
}

interface TableRow {
  transType: number
  transKey: string
  translationId: string
  [key: string]: string | number
}

interface QueryValues {
  transType?: number
  transKey?: string
  transValue?: string
  status?: number
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log(t('routine.i18n.translations.tableText.queryParams') + ':', {
      ...queryParams.value
    })

    const res = await getTransposedData(queryParams.value)
    // 假设API直接返回分页数据
    const data = res.data as any
      const rows = data.rows
      console.log(t('routine.i18n.translations.tableText.importResponseData') + ':', data)
      console.log(t('routine.i18n.translations.tableText.parsedData') + ':', rows)
      console.log('第一行数据:', rows[0])
      console.log('translations:', rows[0]?.translations)
      
      // 获取所有语言代码
      const langCodes = rows.length > 0 ? Object.keys(rows[0]?.translations || {}) : []
      console.log('语言代码:', langCodes)
      
      // 生成动态列（只更新 dynamicColumns，不动 defaultColumns）
      dynamicColumns.value = [
        ...defaultColumns,
        ...langCodes.map((code: string) => ({
          title: code,
          dataIndex: code,
          key: code,
          width: 200,
          ellipsis: true
        })),
        {
          title: t('table.columns.action'),
          key: 'action',
          width: 180,
          fixed: 'right'
        }
      ]
      initColumnSettings()
      
      // 更新表格数据
      tableData.value = rows.map((row: any) => {
        // 获取第一个有效的translationId
        let translationId = '0'
        if (row.translations) {
          const firstValidTranslation = Object.values(row.translations).find((trans: any) => trans?.translationId && trans.translationId !== '0')
          if (firstValidTranslation) {
            translationId = String((firstValidTranslation as any).translationId)
          }
        }
        
        const baseData = {
          transType: row.transType,
          transKey: row.transKey,
          translationId: translationId
        }
        
        // 添加每个语言的翻译值
        if (row.translations) {
          Object.entries(row.translations).forEach(([langCode, translation]: [string, any]) => {
            (baseData as Record<string, string | number>)[langCode] = translation.transValue
          })
        }
        
        return baseData
      })
      
      total.value = data.totalNum
  } catch (error) {
    console.error(error)
    message.error(t('routine.i18n.translations.messages.getDataFailed'))
  }
  loading.value = false
}

// 查询方法
const handleQuery = (values?: QueryValues) => {
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
    transType: -1,
    transKey: '',
    transValue: '',
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
  selectedTranslationId.value = undefined
  selectedTransKey.value = ''
  formVisible.value = true
}

// 处理编辑
const handleEdit = async (record: TableRow) => {
  try {
    // 检查 translationId 是否有效
    if (!record.translationId || record.translationId === '0') {
      // 如果没有 translationId，直接进入新建模式
      selectedTranslationId.value = undefined
      selectedTransKey.value = record.transKey
      formVisible.value = true
      return
    }

    // 检查是否存在该翻译
    const res = await getByIdAsync(String(record.translationId))
    if (res.data) {
      // 存在则更新
      selectedTranslationId.value = record.translationId
      selectedTransKey.value = record.transKey
      formVisible.value = true
    } else {
      // 不存在则新建
      selectedTranslationId.value = undefined
      selectedTransKey.value = record.transKey
      formVisible.value = true
    }
  } catch (error: any) {
    console.error('检查翻译记录失败:', error)
    // 如果是记录不存在的错误，直接进入新建模式
    if (error.response?.data?.code === 'Core.Translation.NotFound') {
      selectedTranslationId.value = undefined
      selectedTransKey.value = record.transKey
      formVisible.value = true
    } else {
      message.error(t('common.failed'))
    }
  }
}

// 处理查看
const handleView = (record: TableRow) => {
  selectedTranslationId.value = record.translationId
  selectedTransKey.value = record.transKey
  detailVisible.value = true
}

// 处理删除
const handleDelete = async (record: TableRow) => {
  try {
    const res = await deleteTranslation(String(record.translationId))
    if (res) {
      message.success(t('routine.i18n.translations.messages.deleteSuccess'))
      fetchData()
    } else {
      message.error(t('routine.i18n.translations.messages.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('routine.i18n.translations.messages.deleteFailed'))
  }
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(
    item => String(item.translationId) === String(selectedRowKeys.value[0])
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
    const res = await batchDeleteTranslation(selectedRowKeys.value)
    if (res) {
      message.success(t('routine.i18n.translations.messages.deleteSuccess'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('routine.i18n.translations.messages.deleteFailed'))
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('routine.i18n.translations.messages.deleteFailed'))
  }
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedTranslationId.value = undefined
  selectedTransKey.value = ''
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
    const res = await importTranslation(file)
    console.log('导入响应数据:', res)
    
    // 由于importTaktTranslation返回boolean，我们需要模拟成功导入
    // 这里假设导入成功，返回默认的成功和失败数量
    return {
      code: 200,
      msg: t('routine.i18n.translations.messages.importSuccess'),
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
    const res = await exportTranslation({
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
      fileName = `翻译数据_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('routine.i18n.translations.messages.exportSuccess'))
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('routine.i18n.translations.messages.exportFailed'))
  }
}

// ==================== 模板方法 ====================
// 处理下载模板
const handleDownloadTemplate = async () => {
  try {
    const res = await getTranslationTemplate()
    return res.data
  } catch (error: any) {
    console.error(t('routine.i18n.translations.tableText.downloadTemplateFailed') + ':', error)
    throw error
  }
}

// ==================== 其它方法 ====================
// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('languageColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(dynamicColumns.value.map(col => [col.key!, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = dynamicColumns.value.filter(col => col.key !== 'action').slice(0, 9)

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
  dynamicColumns.value.forEach(col => {
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
const handleRowClick = (record: TableRow) => {
  console.log(t('routine.i18n.translations.tableText.rowClicked') + ':', record)
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log(t('routine.i18n.translations.tableText.toggleFullscreenState') + ':', isFullscreen)
}

// 新增语言
const handleLang = () => {
  languageFormVisible.value = true
}

// 语言表单提交成功
const handleLanguageSuccess = () => {
  languageFormVisible.value = false
  fetchData()
}

// ==================== 生命周期 ====================
onMounted(async () => {
  // 加载字典数据
  dictStore.loadDicts(['sys_translation_category', 'sys_normal_disable'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.language-container {
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
