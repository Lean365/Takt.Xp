<template>
  <div class="config-container">
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
      :add-permission="['generator:config:create']"
      :show-edit="true"
      :edit-permission="['generator:config:update']"
      :show-delete="true"
      :delete-permission="['generator:config:delete']"
      :show-export="true"
      :show-import="true"
      :export-permission="['generator:config:export']"
      :import-permission="['generator:config:import']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @import="handleImport"
      @template="handleTemplate"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <Takt-table
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :data-source="tableData"
      :loading="loading"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      row-key="genConfigId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <!-- 生成方式列 -->
        <template v-if="column.dataIndex === 'genMethod'">
          <Takt-dict-tag :value="record.genMethod" dict-type="gen_method" />
        </template>
        <!-- 模板类型列 -->
        <template v-if="column.dataIndex === 'genTplType'">
          <Takt-dict-tag :value="record.genTplType" dict-type="gen_tpl_type" />
        </template>
        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag :value="record.status" dict-type="sys_normal_disable" />
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['generator:config:update']"
            :delete-permission="['generator:config:delete']"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 新增/编辑表单 -->
    <config-form
      v-model:visible="formVisible"
      :title="formTitle"
      :config-id="selectedConfigId ? Number(selectedConfigId) : undefined"
      @success="handleSuccess"
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
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktGenConfig, TaktGenConfigQuery } from '@/types/generator/genConfig'
import { 
  getGenConfigList,
  deleteGenConfig,
  batchDeleteGenConfig,
  exportGenConfig,
  downloadTemplate,
  importGenConfig
} from '@/api/generator/genConfig'
import ConfigForm from './components/ConfigForm.vue'

const { t } = useI18n()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'genConfigName',
    label: () => t('generator.config.fields.genConfigName'),
    type: 'input' as const
  },
  {
    name: 'projectName',
    label: () => t('generator.config.fields.projectName'),
    type: 'input' as const
  },
  {
    name: 'moduleName',
    label: () => t('generator.config.fields.moduleName'),
    type: 'input' as const
  },
  {
    name: 'businessName',
    label: () => t('generator.config.fields.businessName'),
    type: 'input' as const
  },
  {
    name: 'functionName',
    label: () => t('generator.config.fields.functionName'),
    type: 'input' as const
  },
  {
    name: 'genMethod',
    label: () => t('generator.config.fields.genMethod'),
    type: 'select' as const,
    props: {
      dictType: 'gen_method',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: () => t('generator.config.fields.status'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'dateRange',
    label: () => t('generator.config.fields.dateRange'),
    type: 'dateRange' as const
  }
]

// 查询参数
const queryParams = ref<TaktGenConfigQuery>({
  pageIndex: 1,
  pageSize: 10,
  genConfigName: '',
  moduleName: '',
  projectName: '',
  businessName: '',
  functionName: '',
  genMethod: -1,
  status: -1,
  dateRange: undefined
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktGenConfig[]>([])

// 选中的行
const selectedRowKeys = ref<string[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns: TableColumnsType = [
  {
    title: () => t('generator.config.fields.genConfigName'),
    dataIndex: 'genConfigName',
    key: 'genConfigName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.author'),
    dataIndex: 'author',
    key: 'author',
    width: 100,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.projectName'),
    dataIndex: 'projectName',
    key: 'projectName',
    width: 150,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.moduleName'),
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.businessName'),
    dataIndex: 'businessName',
    key: 'businessName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.functionName'),
    dataIndex: 'functionName',
    key: 'functionName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.genMethod'),
    dataIndex: 'genMethod',
    key: 'genMethod',
    width: 100
  },
  {
    title: () => t('generator.config.fields.genTplType'),
    dataIndex: 'genTplType',
    key: 'genTplType',
    width: 150
  },
  {
    title: () => t('generator.config.fields.genPath'),
    dataIndex: 'genPath',
    key: 'genPath',
    width: 200,
    ellipsis: true
  },
  {
    title: () => t('generator.config.fields.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: () => t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// 默认列设置
const defaultColumns = columns.map(col => ({
  key: col.key,
  title: col.title
}))

// 列设置状态
const columnSettings = ref<Record<string, boolean>>(
  defaultColumns.reduce((acc, col) => {
    if (col.key) {
      acc[col.key] = true
    }
    return acc
  }, {} as Record<string, boolean>)
)

// 表单相关
const selectedConfigId = ref<string>()
const formVisible = ref(false)
const formTitle = ref('')
const columnSettingVisible = ref(false)

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getGenConfigList(queryParams.value)
    console.log('res:', res)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
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
  selectedRowKeys.value = []
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    genConfigName: '',
    moduleName: '',
    projectName: '',
    businessName: '',
    functionName: '',
    genMethod: -1,
    status: -1,
    dateRange: undefined
  }
  selectedRowKeys.value = []
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
  selectedConfigId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktGenConfig) => {
  formTitle.value = t('common.actions.edit')
  selectedConfigId.value = record.genConfigId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktGenConfig) => {
  if (!record.genConfigId) {
    message.error('无效的配置ID')
    return
  }
  try {
    const res = await deleteGenConfig(record.genConfigId)
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
    item => item.genConfigId === selectedRowKeys.value[0]!
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteGenConfig(id)))
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

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedConfigId.value = undefined
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

// 列设置变更
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
  localStorage.setItem('configColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索区域显示状态
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏显示状态
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportGenConfig(queryParams.value)
    const blob = new Blob([res.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `生成配置_${new Date().toISOString().slice(0, 10)}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error(error)
    if (error.response?.status === 500) {
      message.error(t('common.export.failed'))
    } else {
      message.error(error.response?.data?.msg || t('common.export.failed'))
    }
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await downloadTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `配置导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 处理导入
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const res = await importGenConfig(file)
      const { success = 0, fail = 0 } = (res.data as any).Data || {}
      console.log(
        'fail:',
        (res.data as any).Data?.fail,
        'success:',
        (res.data as any).Data?.success
      )

      if (success > 0 && fail === 0) {
        message.success(`导入成功${success}条，全部成功！`)
      } else if (success > 0 && fail > 0) {
        message.warning(`导入成功${success}条，失败${fail}条`)
      } else if (success === 0 && fail > 0) {
        message.error(`全部导入失败，共${fail}条`)
      } else {
        message.info('未读取到任何数据')
      }
      if (success > 0) fetchData()
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.config-container {
  padding: 16px;
}
.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}
.column-setting-item {
  padding: 4px 0;
}
</style>
