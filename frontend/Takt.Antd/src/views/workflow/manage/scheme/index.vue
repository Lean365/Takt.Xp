<template>
  <div class="workflow-scheme-container">
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
      :add-permission="['workflow:manage:scheme:create']"
      :show-edit="true"
      :edit-permission="['workflow:manage:scheme:update']"
      :show-delete="true"
      :delete-permission="['workflow:manage:scheme:delete']"
      :show-import="true"
      :import-permission="['workflow:manage:scheme:import']"
      :show-export="true"
      :export-permission="['workflow:manage:scheme:export']"
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
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: TaktScheme) => String(record.schemeId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 流程分类列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'schemeCategory'">
          <Takt-dict-tag dict-type="workflow_scheme_category" :value="record.schemeCategory" />
        </template>
      
        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="wf_scheme_status" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :button-order="getSchemeButtonOrder(record.status)"
            :show-edit="record.status === 0"
            :edit-permission="['workflow:manage:scheme:update']"
            :show-delete="record.status === 0"
            :delete-permission="['workflow:manage:scheme:delete']"
            :show-view="true"
            :view-permission="['workflow:manage:scheme:query']"
            :show-clone="record.status >= 1"
            :clone-permission="['workflow:manage:scheme:clone']"
            :show-publish="record.status === 0"
            :publish-permission="['workflow:manage:scheme:publish']"
            :show-validate="record.status === 1"
            :validate-permission="['workflow:manage:scheme:validate']"
            :show-pause="record.status === 1"
            :pause-permission="['workflow:manage:scheme:disable']"
            :show-resume="record.status === 2"
            :resume-permission="['workflow:manage:scheme:enable']"
            :show-design="record.status === 0"
            :design-permission="['workflow:manage:scheme:design']"
            :show-config="record.status >= 1"
            :config-permission="['workflow:manage:scheme:config']"
            :show-version="record.status >= 1"
            :version-permission="['workflow:manage:scheme:version']"
            :show-preview="record.status >= 1"
            :preview-permission="['workflow:manage:scheme:preview']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @view="handleView"
            @clone="handleClone"
            @publish="handlePublish"
            @validate="handleValidate"
            @pause="handlePause"
            @resume="handleResume"
            @design="handleDesign"
            @config="handleConfig"
            @version="handleVersion"
            @preview="handlePreview"
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

    <!-- 工作流定义表单对话框 -->
    <scheme-form
      v-model:open="formVisible"
      :title="formTitle"
      :scheme-id="selectedSchemeId"
      :is-clone="isCloneMode"
      @success="handleSuccess"
    />

    <!-- 工作流定义详情对话框 -->
    <scheme-detail
      v-model:open="detailVisible"
      :scheme-id="selectedSchemeId"
      @update:open="handleDetailClose"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleTemplate"
      template-file-name="工作流定义导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的工作流定义信息字段,\n如流程定义键,流程定义名称,流程分类,版本,流程定义配置等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['workflow:manage:scheme:template']"
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
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktScheme, TaktSchemeQuery } from '@/types/workflow/scheme'
import { useDictStore } from '@/stores/dictStore'
import { useRouter } from 'vue-router'
import {
  getSchemeList,
  getSchemeById,
  createScheme,
  updateScheme,
  deleteScheme,
  batchDeleteScheme,
  importScheme,
  exportScheme,
  getSchemeTemplate
} from '@/api/workflow/scheme'
import SchemeForm from './components/SchemeForm.vue'
import SchemeDetail from './components/SchemeDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()
const router = useRouter()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'schemeKey',
    label: t('workflow.scheme.fields.schemeKey'),
    type: 'input' as const
  },
  {
    name: 'schemeName',
    label: t('workflow.scheme.fields.schemeName'),
    type: 'input' as const
  },
  {
    name: 'schemeCategory',
    label: t('workflow.scheme.fields.schemeCategory'),
    type: 'select' as const,
    props: {
      dictType: 'workflow_scheme_category',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: t('workflow.scheme.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'wf_scheme_status',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktSchemeQuery>({
  pageIndex: 1,
  pageSize: 10,
  schemeKey: '',
  schemeName: '',
  schemeCategory: -1,
  status: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktScheme[]>([])

// 选中的行
const selectedRowKeys = ref<(string | number)[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns = [
  {
    title: t('table.columns.id'),
    dataIndex: 'schemeId',
    key: 'schemeId',
    width: 200
  },
  {
    title: t('workflow.scheme.fields.schemeKey'),
    dataIndex: 'schemeKey',
    key: 'schemeKey',
    width: 200
  },
  {
    title: t('workflow.scheme.fields.schemeName'),
    dataIndex: 'schemeName',
    key: 'schemeName',
    width: 200
  },
  {
    title: t('workflow.scheme.fields.schemeCategory'),
    dataIndex: 'schemeCategory',
    key: 'schemeCategory',
    width: 150
  },
  {
    title: t('workflow.scheme.fields.version'),
    dataIndex: 'version',
    key: 'version',
    width: 150
  },
  {
    title: t('workflow.scheme.fields.formId'),
    dataIndex: 'formId',
    key: 'formId',
    width: 150,
    ellipsis: true,
    tooltip: true
  },
  {
    title: t('workflow.scheme.fields.schemeConfig'),
    dataIndex: 'schemeConfig',
    key: 'schemeConfig',
    width: 150,
    ellipsis: true,
    tooltip: true
  },
  {
    title: t('workflow.scheme.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'notes',
    key: 'notes',
    width: 120,
    ellipsis: true
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
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// ==================== 表单相关 ====================
// 弹窗控制相关
const formVisible = ref(false)
const formTitle = ref('')
const selectedSchemeId = ref<string | undefined>(undefined)
const isCloneMode = ref(false)
const detailVisible = ref(false)

// ==================== 导入相关 ====================
const importVisible = ref(false)

// ==================== 导出相关 ====================

// ==================== 模板相关 ====================

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})
const visibleColumns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// ==================== 数据获取方法 ====================
// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getSchemeList(queryParams.value)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows || []
      total.value = res.data.totalNum || 0
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取工作流定义列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
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
    schemeKey: '',
    schemeName: '',
    schemeCategory: -1,
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

// ==================== 业务操作方法 ====================
// 处理删除
const handleDelete = async (record: TaktScheme) => {
  try {
    const res = await deleteScheme(record.schemeId)
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
    item => String(item.schemeId) === String(selectedRowKeys.value[0])
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteScheme(String(id))))
    const hasError = results.some(res => !res)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// ==================== 导入方法 ====================
// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importScheme(file)
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

// 处理导入成功
const handleImportSuccess = () => {
  //message.success(t('common.import.success'))
  fetchData()
}

// ==================== 导出方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportScheme({
      ...queryParams.value
    })
    // 动态获取文件名
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
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
      fileName = `工作流定义_${new Date().getTime()}.xlsx`
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
const handleTemplate = async () => {
  try {
    const res = await getSchemeTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
    throw error
  }
}

// ==================== 其它功能 ====================
// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('workflowSchemeColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前10列（不包含操作列）
  const firstTenColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 10)

  // 设置前10列为true
  firstTenColumns.forEach(col => {
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
  localStorage.setItem('workflowSchemeColumnSettings', JSON.stringify(settings))
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 处理行点击
const handleRowClick = (record: TaktScheme) => {
  console.log('行点击:', record)
}

// 处理查看
const handleView = (record: TaktScheme) => {
  selectedSchemeId.value = record.schemeId
  detailVisible.value = true
}

// 处理新增
const handleAdd = () => {
  selectedSchemeId.value = undefined
  formTitle.value = t('common.title.create')
  isCloneMode.value = false // 重置克隆模式
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktScheme) => {
  selectedSchemeId.value = record.schemeId
  formTitle.value = t('common.title.edit')
  isCloneMode.value = false // 重置克隆模式
  formVisible.value = true
}

// 处理克隆
const handleClone = (record: TaktScheme) => {
  selectedSchemeId.value = record.schemeId // 传递源记录ID用于克隆
  formTitle.value = t('common.title.clone')
  isCloneMode.value = true // 设置为克隆模式
  formVisible.value = true
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedSchemeId.value = undefined
  isCloneMode.value = false // 重置克隆模式
  fetchData()
}

// 处理发布流程
const handlePublish = (record: TaktScheme) => {
  console.log('发布工作流定义:', record)
  // TODO: 实现发布逻辑，将状态从草稿(0)改为已发布(1)
  message.info('发布流程功能待实现')
}

// 处理停用流程
const handlePause = (record: TaktScheme) => {
  console.log('停用工作流定义:', record)
  // TODO: 实现停用逻辑，将状态从已发布(1)改为已停用(2)
  message.info('停用流程功能待实现')
}

// 处理启用流程
const handleResume = (record: TaktScheme) => {
  console.log('启用工作流定义:', record)
  // TODO: 实现启用逻辑，将状态从已停用(2)改为已发布(1)
  message.info('启用流程功能待实现')
}

// 处理验证流程
const handleValidate = (record: TaktScheme) => {
  console.log('验证工作流定义:', record)
  // TODO: 执行流程验证逻辑
  message.info('验证流程功能待实现')
}

// 处理设计流程
const handleDesign = (record: TaktScheme) => {
  console.log('设计工作流定义:', record)
  // TODO: 打开流程设计器
  message.info('设计流程功能待实现')
}

// 处理配置流程
const handleConfig = (record: TaktScheme) => {
  console.log('配置工作流定义:', record)
  // TODO: 打开流程配置页面
  message.info('配置流程功能待实现')
}

// 处理版本管理
const handleVersion = (record: TaktScheme) => {
  console.log('版本管理工作流定义:', record)
  // TODO: 打开版本管理页面
  message.info('版本管理功能待实现')
}

// 处理预览流程
const handlePreview = (record: TaktScheme) => {
  console.log('预览工作流定义:', record)
  // TODO: 打开流程预览页面
  message.info('预览流程功能待实现')
}

// 根据流程定义状态获取按钮显示顺序
const getSchemeButtonOrder = (status: number) => {
  switch (status) {
    case 0: // 草稿状态
      return ['design', 'edit', 'publish', 'clone', 'delete', 'view']
    case 1: // 已发布状态
      return ['config', 'validate', 'pause', 'version', 'preview', 'clone', 'view']
    case 2: // 已停用状态
      return ['resume', 'version', 'preview', 'clone', 'view']
    default:
      return ['view']
  }
}

// 关闭详情
const handleDetailClose = (value: boolean) => {
  if (!value) {
    selectedSchemeId.value = undefined
  }
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

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['wf_scheme_category', 'wf_scheme_status'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-scheme-container {
  height: 100%;
  background-color: var(--ant-color-bg-container);
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.column-setting-item {
  display: flex;
  align-items: center;
  gap: 8px;
}
</style> 
