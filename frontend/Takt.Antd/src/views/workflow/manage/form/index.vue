<template>
  <div class="workflow-form-container">
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
      :add-permission="['workflow:manage:form:create']"
      :show-edit="true"
      :edit-permission="['workflow:manage:form:update']"
      :show-delete="true"
      :delete-permission="['workflow:manage:form:delete']"
      :show-import="true"
      :import-permission="['workflow:manage:form:import']"
      :show-export="true"
      :export-permission="['workflow:manage:form:export']"
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
      row-key="formId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 表单分类列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'formCategory'">
          <Takt-dict-tag dict-type="workflow_form_category" :value="record.formCategory" />
        </template>
      
        <!-- 表单类型列 -->
        <template v-if="column.dataIndex === 'formType'">
          <Takt-dict-tag dict-type="wf_form_type" :value="record.formType" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="wf_form_status" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['workflow:manage:form:update']"
            :show-delete="true"
            :delete-permission="['workflow:manage:form:delete']"
            :show-clone="true"
            :clone-permission="['workflow:manage:form:clone']"
            :show-design="true"
            :design-permission="['workflow:manage:form:design']"
            :show-preview="true"
            :preview-permission="['workflow:manage:form:preview']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @clone="handleClone"
            @design="handleDesign"
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

    <!-- 表单表单对话框 -->
    <form-form
      v-model:open="formFormVisible"
      :form-id="selectedFormId ?? undefined"
      :is-clone="isCloneMode"
      @success="handleFormSuccess"
    />

    <!-- 表单设计器对话框 -->
    <a-modal
      v-model:open="designerModalVisible"
      title="表单设计器"
      width="95%"
      :footer="null"
      :destroy-on-close="true"
    >
      <Takt-form
        v-if="designerModalVisible"
        :model-value="designerFormConfig"
        :readonly="false"
        :height="'600px'"
        @update:modelValue="handleDesignerSave"
      />
    </a-modal>

    <!-- 表单预览对话框 -->
    <a-modal
      v-model:open="previewModalVisible"
      title="表单预览"
      width="80%"
      :footer="null"
    >
      <div class="preview-container">
        <Takt-form
          v-if="previewFormConfig"
          :model-value="previewFormConfig"
          :readonly="true"
          :height="'500px'"
        />
      </div>
    </a-modal>

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleTemplate"
      template-file-name="表单导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的表单信息字段,\n如表单名称,表单键,表单分类,表单类型,版本,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['workflow:manage:form:template']"
    />

  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktForm, TaktFormQuery } from '@/types/workflow/form'
import { useDictStore } from '@/stores/dictStore'
import FormForm from './components/FormForm.vue'
import { getFormList, getFormById, deleteForm, batchDeleteForm, importForm, exportForm, getFormTemplate, updateForm } from '@/api/workflow/form'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'formName',
    label: '表单名称',
    type: 'input' as const,
    props: {
      placeholder: '请输入表单名称'
    }
  },
  {
    name: 'formCategory',
    label: '表单分类',
    type: 'select' as const,
    props: {
      placeholder: '请选择表单分类',
      dictType: 'workflow_form_category'
    }
  },
  {
    name: 'formType',
    label: '表单类型',
    type: 'select' as const,
    props: {
      placeholder: '请选择表单类型',
      dictType: 'wf_form_type'
    }
  },
  {
    name: 'status',
    label: '表单状态',
    type: 'select' as const,
    props: {
      placeholder: '请选择表单状态',
      dictType: 'wf_form_status'
    }
  }
]

// 查询参数
const queryParams = reactive<TaktFormQuery>({
  pageIndex: 1,
  pageSize: 10,
  formName: '',
  formCategory: undefined,
  formType: undefined,
  status: undefined
})

// 表格相关
const loading = ref(false)
const tableData = ref<TaktForm[]>([])
const selectedRowKeys = ref<string[]>([])
const total = ref(0)
const showSearch = ref(true)

// 表单对话框与设计器/预览
const formFormVisible = ref(false)
const designerModalVisible = ref(false)
const previewModalVisible = ref(false)
const selectedFormId = ref<string | null>(null)
const isCloneMode = ref(false)
const previewFormConfig = ref('')
const designerFormConfig = ref('')

// 导入相关
const importVisible = ref(false)

// 表格列配置
const columns = [
  {
    title: '表单ID',
    dataIndex: 'formId',
    key: 'formId',
    width: 100,
    fixed: 'left'
  },
  {
    title: '表单名称',
    dataIndex: 'formName',
    key: 'formName',
    width: 200,
    ellipsis: true
  },
  {
    title: '表单键',
    dataIndex: 'formKey',
    key: 'formKey',
    width: 150
  },
  {
    title: '表单分类',
    dataIndex: 'formCategory',
    key: 'formCategory',
    width: 120
  },
  {
    title: '表单类型',
    dataIndex: 'formType',
    key: 'formType',
    width: 120
  },
  {
    title: '版本',
    dataIndex: 'version',
    key: 'version',
    width: 80
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right'
  }
]

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

const initColumnSettings = () => {
  localStorage.removeItem('workflowFormColumnSettings')
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key!, false]))
  const firstColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)
  firstColumns.forEach(col => {
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })
  columnSettings.value['action'] = true
}

// ==================== 数据获取方法 ====================
// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams
    })

    const res = await getFormList(queryParams)
    console.log('res:', res)
    if (res.data) {
      tableData.value = res.data.rows || []
      total.value = res.data.totalNum || 0
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取表单列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams, values)
  }
  queryParams.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  Object.assign(queryParams, {
    pageIndex: 1,
    pageSize: 10,
    formName: '',
    formCategory: undefined,
    formType: undefined
  })
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current ?? 1
  queryParams.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// ==================== 业务操作方法 ====================
// 处理删除
const handleDelete = async (record: TaktForm) => {
  try {
    const res = await deleteForm(record.formId)
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
    item => String(item.formId) === String(selectedRowKeys.value[0])
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteForm(String(id))))
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
    const res = await importForm(file)
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
    const res = await exportForm(queryParams)
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
      fileName = `表单数据_${new Date().getTime()}.xlsx`
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
    const res = await getFormTemplate()
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

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    if (!col.key) return
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('workflowFormColumnSettings', JSON.stringify(settings))
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
const handleRowClick = (record: TaktForm) => {
  console.log('行点击:', record)
}

// 处理新增
const handleAdd = () => {
  selectedFormId.value = null
  isCloneMode.value = false // 重置克隆模式
  formFormVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktForm) => {
  selectedFormId.value = record.formId
  isCloneMode.value = false // 重置克隆模式
  formFormVisible.value = true
}

// 处理克隆
const handleClone = (record: TaktForm) => {
  selectedFormId.value = record.formId // 传递源记录ID用于克隆
  isCloneMode.value = true // 设置为克隆模式
  formFormVisible.value = true
}

// 处理表单提交成功
const handleFormSuccess = () => {
  formFormVisible.value = false
  selectedFormId.value = null
  isCloneMode.value = false // 重置克隆模式
  fetchData()
}

// 处理设计
const handleDesign = async (record: TaktForm) => {
  try {
    selectedFormId.value = record.formId
    const res = await getFormById(record.formId)
    if (res.data) {
      designerFormConfig.value = res.data.formConfig || ''
      designerModalVisible.value = true
    }
  } catch (error) {
    console.error('加载表单配置失败:', error)
    message.error('加载表单配置失败')
  }
}

// 处理预览
const handlePreview = async (record: TaktForm) => {
  try {
    const res = await getFormById(record.formId)
    if (res.data) {
      previewFormConfig.value = res.data.formConfig
      previewModalVisible.value = true
    }
  } catch (error) {
    console.error('加载表单配置失败:', error)
    message.error('加载表单配置失败')
  }
}

// 处理设计器保存
const handleDesignerSave = async (formConfig: string) => {
  try {
    if (!selectedFormId.value) {
      message.error('表单ID不能为空')
      return
    }
    
    // 更新表单配置
    const res = await updateForm({
      formId: selectedFormId.value,
      formConfig: formConfig
    } as any)
    
    if (res) {
      message.success('表单设计保存成功')
      designerModalVisible.value = false
      designerFormConfig.value = ''
      fetchData()
    } else {
      message.error('保存失败')
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  }
}

// 分页处理
const handlePageChange = (page: number) => {
  queryParams.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.pageSize = size
  fetchData()
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['wf_form_category', 'wf_form_type', 'wf_form_status'])
  // 加载表格数据
  initColumnSettings()
  fetchData()
})
</script>

<style lang="less" scoped>
.workflow-form-container {
  height: 100%;
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
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

.preview-container {
  .Takt-form {
    border: 1px solid #d9d9d9;
    border-radius: 4px;
  }
}
</style>


