<template>
  <div class="mailtmpl-container">
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
      :add-permission="['routine:mailtmpl:create']"
      :show-edit="true"
      :edit-permission="['routine:mailtmpl:update']"
      :show-delete="true"
      :delete-permission="['routine:mailtmpl:delete']"
      :show-export="true"
      :export-permission="['routine:mailtmpl:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
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
      :columns="columns.filter(col => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 1000 }"
      :default-height="594"
      row-key="mailTplId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{ type: 'checkbox', columnWidth: 60 }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <template #bodyCell="{ column, record }">
        <!-- 模板类型列 -->
        <template v-if="column.dataIndex === 'mailTplCode'">
          <Takt-dict-tag dict-type="email_template_type" :value="record.mailTplCode" />
        </template>
        <!-- 模板状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="email_template_status" :value="record.status" />
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:mailtmpl:update']"
            :show-delete="true"
            :delete-permission="['routine:mailtmpl:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('mailtmpl.preview')">
                <a-button type="link" size="small" @click.stop="handlePreview(record)">
                  <template #icon><eye-outlined /></template>
                </a-button>
              </a-tooltip>
            </template>
          </Takt-operation>
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
      :show-total="(total: number) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 邮件模板表单对话框 -->
    <mailtmpl-form
      v-model:visible="formVisible"
      :title="formTitle"
      :template-id="selectedTemplateId"
      @success="handleSuccess"
    />

    <!-- 预览对话框 -->
    <a-modal
      v-model:open="previewVisible"
      :title="t('mailtmpl.preview')"
      width="700px"
      :footer="null"
    >
      <div class="preview-content">
        <h3>{{ previewData.mailTplSubject }}</h3>
        <div v-html="previewData.mailTplBody"></div>
      </div>
    </a-modal>

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
        <div v-for="col in columns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { CheckboxValueType } from 'ant-design-vue/es/checkbox/interface';
import { EyeOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktMailTpl, TaktMailTplQuery } from '@/types/routine/mailtpl'
import type { QueryField } from '@/types/components/query'
import { getMailTplList, deleteMailTpl } from '@/api/routine/mailtpl'
import MailtmplForm from './components/MailtmplForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询参数
const queryParams = ref<TaktMailTplQuery>({
  pageIndex: 1,
  pageSize: 10,
  mailTplName: undefined,
  mailTplCode: undefined,
  status: undefined
})

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'mailTplName',
    label: t('mailtmpl.templateName'),
    type: 'input' as const,
    placeholder: t('mailtmpl.placeholder.templateName')
  },
  {
    name: 'mailTplCode',
    label: t('mailtmpl.templateType'),
    type: 'select' as const,
    props: { dictType: 'email_template_type' },
    placeholder: t('mailtmpl.placeholder.templateType')
  },
  {
    name: 'status',
    label: t('mailtmpl.templateStatus'),
    type: 'select' as const,
    props: { dictType: 'email_template_status' },
    placeholder: t('mailtmpl.placeholder.templateStatus')
  }
];

// 表格列配置
const columns = [
  { key: 'mailTplId', title: 'ID', dataIndex: 'mailTplId', width: 80, ellipsis: true },
  { key: 'mailTplName', title: t('mailtmpl.templateName'), dataIndex: 'mailTplName', width: 180, ellipsis: true },
  { key: 'mailTplCode', title: t('mailtmpl.templateType'), dataIndex: 'mailTplCode', width: 120, ellipsis: true },
  { key: 'mailTplSubject', title: t('mailtmpl.templateSubject'), dataIndex: 'mailTplSubject', width: 200, ellipsis: true },
  { key: 'status', title: t('mailtmpl.templateStatus'), dataIndex: 'status', width: 100 },
  { key: 'createTime', title: t('mailtmpl.createTime'), dataIndex: 'createTime', width: 180, ellipsis: true },
  { key: 'action', title: t('common.operation'), fixed: 'right', width: 180 }
]

// 表格数据
const loading = ref(false)
const tableData = ref<TaktMailTpl[]>([])
const total = ref(0)
const selectedRowKeys = ref<string[]>([])
const selectedTemplateId = ref<string>()

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const previewVisible = ref(false)
const previewData = ref<TaktMailTpl>({} as TaktMailTpl)

// 列设置
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// 显示搜索条件
const showSearch = ref(true)

// 获取数据列表
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getMailTplList(queryParams.value)
    tableData.value = res.data.data.rows
    total.value = res.data.data.total
  } catch (error) {
    console.error('获取邮件模板列表失败:', error)
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    mailTplName: undefined,
    mailTplCode: undefined,
    status: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 页面大小变化
const handleSizeChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktMailTpl) => {
  selectedRowKeys.value = [String(record.mailTplId)]
}

// 新增
const handleAdd = () => {
  formTitle.value = t('mailtmpl.add')
  selectedTemplateId.value = undefined
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.pleaseSelectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.mailTplId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: TaktMailTpl) => {
  formTitle.value = t('mailtmpl.edit')
  selectedTemplateId.value = String(record.mailTplId)
  formVisible.value = true
}

// 删除
const handleDelete = async (record: TaktMailTpl) => {
  try {
    await deleteMailTpl(BigInt(record.mailTplId))
    message.success(t('mailtmpl.operation.success.delete'))
    fetchData()
  } catch (error) {
    console.error('删除邮件模板失败:', error)
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelectRecord'))
    return
  }
  try {
    // 这里只做单个删除，实际应为批量删除API
    await deleteMailTpl(BigInt(selectedRowKeys.value[0]))
    message.success(t('mailtmpl.operation.success.delete'))
    fetchData()
  } catch (error) {
    console.error('批量删除邮件模板失败:', error)
  }
}

// 导出
const handleExport = () => {
  // TODO: 实现导出功能
}

// 预览
const handlePreview = (record: TaktMailTpl) => {
  previewData.value = record
  previewVisible.value = true
}

// 表单成功回调
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: CheckboxValueType[]) => {
  const settings: Record<string, boolean> = {};
  columns.forEach(col => {
    settings[col.key] = checkedValues.map(String).includes(String(col.key));
  });
  columnSettings.value = settings;
};

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

// 初始化
onMounted(() => {
  fetchData()
  // 初始化列设置
  columns.forEach(col => {
    columnSettings.value[col.key] = true
  })
})
</script>

<style scoped>
.mailtmpl-container {
  padding: 24px;
}

.preview-content {
  padding: 20px;
  background-color: #f5f5f5;
  border-radius: 4px;
}

.preview-content h3 {
  margin-bottom: 20px;
  color: #333;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  display: flex;
  align-items: center;
}
</style> 
