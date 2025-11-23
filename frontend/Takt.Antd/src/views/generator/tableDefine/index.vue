<template>
  <div class="table-define-container">
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
      :add-permission="['generator:tabledefine:create']"
      :show-edit="true"
      :edit-permission="['generator:tabledefine:update']"
      :show-delete="true"
      :delete-permission="['generator:tabledefine:delete']"
      :show-import="true"
      :import-permission="['generator:tabledefine:import']"
      :show-export="true"
      :export-permission="['generator:tabledefine:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      :disabled-sync="selectedRowKeys.length !== 1"
      :disabled-initialize="selectedRowKeys.length !== 1"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @initialize="handleInitialize"
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
      :scroll="{ x: 1000, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="genTableDefineId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 操作列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['generator:tabledefine:query']"
            :show-edit="true"
            :edit-permission="['generator:tabledefine:update']"
            :show-delete="true"
            :delete-permission="['generator:tabledefine:delete']"
            :show-sync="true"
            :sync-permission="['generator:tabledefine:sync']"
            :show-initialize="true"
            :initialize-permission="['generator:tabledefine:initialize']"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
            @sync="handleSync"
            @initialize="handleInitialize"
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

    <!-- 表定义表单 -->
    <table-define-form
      v-model:open="formVisible"
      :title="formTitle"
      :table-id="selectedTableDefineId"
      @success="handleSuccess"
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
import { ref, onMounted, computed, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktGenTableDefine, TaktGenTableDefineQuery } from '@/types/generator/genTableDefine'
import { 
  getTableDefineList,
  deleteTableDefine,
  syncTableDefine,
  initializeTableDefine
} from '@/api/generator/genTableDefine'
import TableDefineForm from './components/TableDefineForm.vue'

const { t } = useI18n()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'tableName',
    label: () => t('generator.tableDefine.fields.tableName'),
    type: 'input' as const
  },
  {
    name: 'tableComment',
    label: () => t('generator.tableDefine.fields.tableComment'),
    type: 'input' as const
  },
  {
    name: 'className',
    label: () => t('generator.tableDefine.fields.className'),
    type: 'input' as const
  }
]

// 查询参数
const queryParams = ref<TaktGenTableDefineQuery>({
  pageIndex: 1,
  pageSize: 10,
  tableName: '',
  tableComment: ''
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktGenTableDefine[]>([])

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
    dataIndex: 'genTableDefineId',
    key: 'genTableDefineId',
    width: 80,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.tableName'),
    dataIndex: 'tableName',
    key: 'tableName',
    width: 150,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.tableComment'),
    dataIndex: 'tableComment',
    key: 'tableComment',
    width: 200,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.className'),
    dataIndex: 'className',
    key: 'className',
    width: 150,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.packageName'),
    dataIndex: 'packageName',
    key: 'packageName',
    width: 200,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.moduleName'),
    dataIndex: 'moduleName',
    key: 'moduleName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.businessName'),
    dataIndex: 'businessName',
    key: 'businessName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.functionName'),
    dataIndex: 'functionName',
    key: 'functionName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.author'),
    dataIndex: 'author',
    key: 'author',
    width: 100,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('generator.tableDefine.fields.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('table.columns.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 200,
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
const selectedTableDefineId = ref<string>()
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

    const res = await getTableDefineList(queryParams.value)
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
    tableName: '',
    tableComment: ''
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
  selectedTableDefineId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktGenTableDefine) => {
  formTitle.value = t('common.actions.edit')
  selectedTableDefineId.value = record.genTableDefineId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktGenTableDefine) => {
  if (!record.genTableDefineId) {
    message.error('无效的表定义ID')
    return
  }
  try {
    const res = await deleteTableDefine(record.genTableDefineId)
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
    item => item.genTableDefineId === selectedRowKeys.value[0]!
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteTableDefine(id)))
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
  selectedTableDefineId.value = undefined
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
  localStorage.setItem('tableDefineColumnSettings', JSON.stringify(settings))
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
    message.info('导出功能待实现')
  } catch (error: any) {
    console.error(error)
    message.error(t('common.export.failed'))
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    message.info('模板下载功能待实现')
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 处理导入
const handleImport = () => {
  message.info('导入功能待实现')
}

// 查看详情
const handleView = (record: TaktGenTableDefine) => {
  selectedTableDefineId.value = record.genTableDefineId
  // TODO: 实现查看详情功能
}

// 行点击处理
const handleRowClick = (record: TaktGenTableDefine) => {
  selectedTableDefineId.value = record.genTableDefineId
}

// 同步表定义
const handleSync = async (record: TaktGenTableDefine) => {
  try {
    await syncTableDefine(record.genTableDefineId)
    message.success(t('common.sync.success'))
    fetchData()
  } catch (error) {
    console.error('同步表定义失败:', error)
    message.error(t('common.sync.failed'))
  }
}

// 初始化表定义
const handleInitialize = async (record: TaktGenTableDefine) => {
  try {
    await initializeTableDefine({
      dbType: record.dbType,
      connectionString: record.connectionString,
      databaseName: record.databaseName,
      tablePrefix: '',
      tableNameFirst: '',
      tableNameSecond: '',
      tableNameThird: '',
      tableName: record.tableName,
      tableComment: record.tableComment,
      author: record.author,
      columns: record.columns || []
    })
    message.success(t('common.initialize.success'))
    fetchData()
  } catch (error) {
    console.error('初始化表定义失败:', error)
    message.error(t('common.initialize.failed'))
  }
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.table-define-container {
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
