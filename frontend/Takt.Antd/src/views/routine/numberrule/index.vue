//===================================================================
// 项目名 : Lean.Takt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 编号规则管理页面
//===================================================================

<template>
  <div class="numberrule-container">
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
      :add-permission="['routine:numberrule:create']"
      :show-edit="true"
      :edit-permission="['routine:numberrule:update']"
      :show-delete="true"
      :delete-permission="['routine:numberrule:delete']"
      :show-import="true"
      :import-permission="['routine:numberrule:import']"
      :show-export="true"
      :export-permission="['routine:numberrule:export']"
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
      :row-key="(record: TaktNumberRule) => String(record.numberRuleId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 规则类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'numberRuleType'">
          <Takt-dict-tag dict-type="routine_number_rule_type" :value="record.numberRuleType" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['routine:numberrule:query']"
            :show-edit="true"
            :edit-permission="['routine:numberrule:update']"
            :show-delete="true"
            :delete-permission="['routine:numberrule:delete']"
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

    <!-- 编号规则表单对话框 -->
    <number-rule-form
      v-model:visible="formVisible"
      :title="formTitle"
      :number-rule-id="selectedNumberRuleId"
      @success="handleSuccess"
    />

    <!-- 编号规则详情对话框 -->
    <number-rule-detail
      v-model:visible="detailVisible"
      :number-rule-id="selectedNumberRuleId"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="编号规则导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的编号规则信息字段,\n如规则名称,规则代码,规则类型,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:numberrule:template']"
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
import type { TaktNumberRule, TaktNumberRuleQuery } from '@/types/routine/numberrule/numberrule'
import { useDictStore } from '@/stores/dictStore'
import {
  getNumberRuleList,
  deleteNumberRule,
  batchDeleteNumberRule,
  createNumberRule,
  updateNumberRule,
  importNumberRule,
  exportNumberRule,
  getNumberRuleTemplate,
} from '@/api/routine/numberrule/numberrule'
import NumberRuleForm from './components/NumberRuleForm.vue'
import NumberRuleDetail from './components/NumberRuleDetail.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'numberRuleName',
    label: t('routine.core.numberrule.fields.numberRuleName.label'),
    type: 'input' as const,
  },
  {
    name: 'numberRuleCode',
    label: t('routine.core.numberrule.fields.numberRuleCode.label'),
    type: 'input' as const
  },
  {
    name: 'numberRuleType',
    label: t('routine.core.numberrule.fields.numberRuleType.label'),
    type: 'select' as const,
    props: {
      dictType: 'routine_number_rule_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: t('routine.core.numberrule.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktNumberRuleQuery>({
  pageIndex: 1,
  pageSize: 10,
  numberRuleName: '',
  numberRuleCode: '',
  numberRuleType: -1,
  status: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktNumberRule[]>([])

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
    dataIndex: 'numberRuleId',
    key: 'numberRuleId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('routine.core.numberrule.fields.numberRuleName.label'),
    dataIndex: 'numberRuleName',
    key: 'numberRuleName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.core.numberrule.fields.numberRuleCode.label'),
    dataIndex: 'numberRuleCode',
    key: 'numberRuleCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('routine.core.numberrule.fields.numberRuleType.label'),
    dataIndex: 'numberRuleType',
    key: 'numberRuleType',
    width: 120
  },
  {
    title: t('routine.core.numberrule.fields.numberRuleDescription.label'),
    dataIndex: 'numberRuleDescription',
    key: 'numberRuleDescription',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.core.numberrule.fields.numberPrefix.label'),
    dataIndex: 'numberPrefix',
    key: 'numberPrefix',
    width: 100,
    ellipsis: true
  },
  {
    title: t('routine.core.numberrule.fields.numberSuffix.label'),
    dataIndex: 'numberSuffix',
    key: 'numberSuffix',
    width: 100,
    ellipsis: true
  },
  {
    title: t('routine.core.numberrule.fields.dateFormat.label'),
    dataIndex: 'dateFormat',
    key: 'dateFormat',
    width: 120
  },
  {
    title: t('routine.core.numberrule.fields.sequenceLength.label'),
    dataIndex: 'sequenceLength',
    key: 'sequenceLength',
    width: 100
  },
  {
    title: t('routine.core.numberrule.fields.status.label'),
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

// ==================== 表单相关 ====================
// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedNumberRuleId = ref<string>()

// 详情对话框
const detailVisible = ref(false)

// ==================== 导入相关 ====================
// 导入对话框
const importVisible = ref(false)

// ==================== 其它功能 ====================
// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('numberruleColumnSettings')

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
  localStorage.setItem('numberruleColumnSettings', JSON.stringify(settings))
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getNumberRuleList(queryParams.value)
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
    numberRuleName: '',
    numberRuleCode: '',
    numberRuleType: -1,
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

// 处理行点击
const handleRowClick = (record: TaktNumberRule) => {
  console.log('行点击:', record)
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('common.actions.add')
  selectedNumberRuleId.value = undefined
  formVisible.value = true
}

// 处理查看
const handleView = (record: TaktNumberRule) => {
  selectedNumberRuleId.value = String(record.numberRuleId)
  detailVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktNumberRule) => {
  formTitle.value = t('common.actions.edit')
  selectedNumberRuleId.value = String(record.numberRuleId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktNumberRule) => {
  try {
    const res = await deleteNumberRule(String(record.numberRuleId))
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
    item => String(item.numberRuleId) === String(selectedRowKeys.value[0])
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteNumberRule(String(id))))
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
  selectedNumberRuleId.value = undefined
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

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// ==================== 导入方法 ====================
// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importNumberRule(file)
    console.log('导入响应数据:', res)
    
    // res 包含 { success, fail } 对象
    const { success = 0, fail = 0 } = (res.data as any)?.data || {}
    
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

// ==================== 模板方法 ====================
// 处理下载模板
const handleDownloadTemplate = async () => {
  try {
    const res = await getNumberRuleTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    throw error
  }
}

// ==================== 导出方法 ====================
// 处理导出
const handleExport = async () => {
  try {
    const res = await exportNumberRule({
      ...queryParams.value
    })
    // 生成文件名
    const fileName = `编号规则_${new Date().getTime()}.xlsx`
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

// ==================== 生命周期 ====================
onMounted(async () => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable', 'routine_number_rule_type'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.numberrule-container {
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



