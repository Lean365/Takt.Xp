<template>
  <Takt-modal
    :open="open"
    title="列定义"
    width="1000px"
    :destroyOnClose="true"
    :footer="null"
    @cancel="handleCancel"
  >
    <div class="gen-column-define">
      <!-- 查询区域 -->
      <Takt-query
        v-show="showSearch"
        :model="queryParams"
        :query-fields="queryFields"
        @search="handleQuery"
        @reset="handleReset"
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
        :show-template="true"
        :template-permission="['generator:tabledefine:import']"
        :disabled-edit="selectedRowKeys.length !== 1"
        :disabled-delete="selectedRowKeys.length === 0"
        @add="handleAdd"
        @edit="handleEditSelected"
        @delete="handleBatchDelete"
        @import="handleImport"
        @export="handleExport"
        @template="handleTemplate"
        @refresh="fetchData"
        @column-setting="handleColumnSetting"
        @toggle-search="toggleSearch"
        @toggle-fullscreen="toggleFullscreen"
      />

      <!-- 数据表格 -->
      <Takt-table
        :columns="columns"
        :data-source="dataSource"
        :loading="loading"
        row-key="genColumnDefineId"
        v-model:selectedRowKeys="selectedRowKeys"
        :pagination="false"
        :scroll="{ x: 'max-content' }"
        :row-selection="{
          type: 'checkbox',
          columnWidth: 60
        }"
        @change="handleTableChange"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.dataIndex === 'isPrimaryKey'">
            <a-switch 
              v-model:checked="record.isPrimaryKey" 
              :disabled="record.dbColumnType !== 'bigint' || !record.isEditing"
              @change="(checked) => handleSwitchChange(record, 'isPrimaryKey', checked ? 1 : 0)" 
            />
          </template>
          <template v-else-if="column.dataIndex === 'isRequired'">
            <a-switch 
              :checked="record.isRequired === 1"
              :disabled="record.dbColumnType === 'bigint' || record.dbColumnType === 'decimal' || record.dbColumnType === 'int' || !record.isEditing"
              @change="(checked) => handleSwitchChange(record, 'isRequired', checked ? 1 : 0)" 
            />
          </template>
          <template v-else-if="column.dataIndex === 'isIncrement'">
            <a-switch 
              v-model:checked="record.isIncrement" 
              :disabled="record.dbColumnType !== 'bigint' || !record.isEditing"
              @change="(checked) => handleSwitchChange(record, 'isIncrement', checked ? 1 : 0)" 
            />
          </template>
          <template v-else-if="column.dataIndex === 'dbColumnType'">
            <a-select
              v-model:value="record.dbColumnType"
              style="width: 120px"
              :disabled="!record.isEditing"
              @change="(value) => handleSelectChange(record, 'dbColumnType', value as string)"
            >
              <a-select-option value="bigint">bigint</a-select-option>
              <a-select-option value="char">char</a-select-option>
              <a-select-option value="datetime">datetime</a-select-option>
              <a-select-option value="decimal">decimal</a-select-option>
              <a-select-option value="int">int</a-select-option>
              <a-select-option value="nchar">nchar</a-select-option>
              <a-select-option value="nvarchar">nvarchar</a-select-option>
              <a-select-option value="text">text</a-select-option>
              <a-select-option value="varchar">varchar</a-select-option>
            </a-select>
          </template>
          <template v-else-if="column.dataIndex === 'columnLength'">
            <a-input-number
              v-model:value="record.columnLength"
              :min="0"
              :disabled="record.dbColumnType === 'int' || record.dbColumnType === 'bigint' || record.dbColumnType === 'datetime' || !record.isEditing"
              style="width: 100px"
              @change="(value) => handleNumberChange(record, 'columnLength', value as number)"
            />
          </template>
          <template v-else-if="column.dataIndex === 'decimalDigits'">
            <a-input-number
              v-model:value="record.decimalDigits"
              :min="0"
              :disabled="record.dbColumnType === 'int' || record.dbColumnType === 'bigint' || record.dbColumnType === 'varchar' || record.dbColumnType === 'nvarchar' || record.dbColumnType === 'char' || record.dbColumnType === 'nchar' || record.dbColumnType === 'text' || record.dbColumnType === 'datetime' || !record.isEditing"
              style="width: 100px"
              @change="(value) => handleNumberChange(record, 'decimalDigits', value as number)"
            />
          </template>
          <template v-else-if="column.dataIndex === 'orderNum'">
            <a-input-number
              v-model:value="record.orderNum"
              :min="0"
              :disabled="!record.isEditing"
              style="width: 100px"
              @change="(value) => handleNumberChange(record, 'orderNum', value as number)"
            />
          </template>
          <template v-else-if="column.dataIndex === 'columnName'">
            <a-form :model="record" :ref="el => setFormRef(record.genColumnDefineId, el)">
              <a-form-item
                :name="'columnName'"
                :rules="[
                  { required: true, message: '请输入字段名', trigger: 'blur' },
                  { min: 4, message: '字段名长度不能少于4个字符', trigger: 'blur' },
                  { max: 50, message: '字段名长度不能超过50个字符', trigger: 'blur' },
                  { 
                    validator: async (_rule: any, value: string) => {
                      if (!value) return;
                      const formattedValue = value.replace(/[^a-zA-Z0-9]/g, '');
                      if (formattedValue.length > 0) {
                        const firstChar = formattedValue.charAt(0).toUpperCase();
                        const restChars = formattedValue.slice(1);
                        const finalValue = firstChar + restChars;
                        if (finalValue !== value) {
                          throw new Error('字段名必须以大写字母开头，只能包含英文字母和数字');
                        }
                      }
                    },
                    trigger: 'blur'
                  }
                ]"
              >
                <a-input
                  v-model:value="record.columnName"
                  :disabled="!record.isEditing"
                  style="width: 120px"
                  @change="(e) => handleInputChange(record, 'columnName', e.target.value || '')"
                />
              </a-form-item>
            </a-form>
          </template>
          <template v-else-if="column.dataIndex === 'columnComment'">
            <a-form :model="record" :ref="el => setFormRef(record.genColumnDefineId + '_comment', el)">
              <a-form-item
                :name="'columnComment'"
                :rules="[
                  { required: true, message: '请输入字段说明', trigger: 'blur' },
                  { min: 2, message: '字段说明长度不能少于2个字符', trigger: 'blur' },
                  { max: 100, message: '字段说明长度不能超过100个字符', trigger: 'blur' }
                ]"
              >
                <a-input
                  v-model:value="record.columnComment"
                  :disabled="!record.isEditing"
                  style="width: 120px"
                  @change="(e) => handleInputChange(record, 'columnComment', e.target.value || '')"
                />
              </a-form-item>
            </a-form>
          </template>
          <template v-else-if="column.dataIndex === 'defaultValue'">
            <a-input
              v-model:value="record.defaultValue"
              :disabled="record.dbColumnType === 'varchar' || record.dbColumnType === 'nvarchar' || record.dbColumnType === 'char' || record.dbColumnType === 'nchar' || record.dbColumnType === 'text' || record.dbColumnType === 'datetime' || record.dbColumnType === 'int' || record.dbColumnType === 'bigint' || record.dbColumnType === 'decimal' || !record.isEditing"
              style="width: 120px"
              @change="(e) => handleInputChange(record, 'defaultValue', e.target.value || '')"
            />
          </template>
          <template v-else-if="column.key === 'action'">
            <Takt-operation
              :record="record"
              :show-save="record.isEditing"
              :save-permission="['generator:tabledefine:create']"
              :show-edit="!record.isEditing"
              :edit-permission="['generator:tabledefine:update']"
              :show-delete="true"
              :delete-permission="['generator:tabledefine:delete']"
              @save="handleSave(record)"
              @edit="handleEdit(record)"
              @delete="handleDelete"
            />
          </template>
        </template>
      </Takt-table>

      <!-- 分页 -->
      <Takt-pagination
        v-model:current="queryParams.pageIndex"
        v-model:pageSize="queryParams.pageSize"
        :total="pagination.total"
        :show-size-changer="true"
        :show-quick-jumper="true"
        :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
        @change="handlePageChange"
        @showSizeChange="handleSizeChange"
      />

      <!-- 表单弹窗 -->
      <gen-column-define-form
        v-model:open="formVisible"
        :title="formTitle"
        :table-id="props.tableId"
        :column-id="selectedColumnId"
        @success="handleSuccess"
      />

      <!-- 列设置抽屉 -->
      <a-drawer
        v-model:open="columnSettingVisible"
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
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, watch, shallowRef } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { message, Form } from 'ant-design-vue'
import { UploadOutlined } from '@ant-design/icons-vue'
import { useI18n } from 'vue-i18n'
import type { TaktGenColumnDefine, TaktGenColumnDefineCreate, TaktGenColumnDefineUpdate, TaktGenColumnDefineQuery } from '@/types/generator/genColumnDefine'
import type { TaktApiResponse } from '@/types/common'
import type { AxiosResponse } from 'axios'
import { getColumnDefineList, createColumnDefine, updateColumnDefine, deleteColumnDefine, batchDeleteColumnDefine, importColumnDefine, exportColumnDefine, getColumnDefineTemplate } from '@/api/generator/genTableDefine'
import type { QueryField } from '@/types/components/query'
import GenColumnDefineForm from './GenColumnDefineForm.vue'

const props = defineProps<{
  open: boolean
  tableId: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const { t } = useI18n()

/** 查询字段 */
const queryFields: QueryField[] = [
  {
    name: 'columnName',
    label: '字段名',
    type: 'input',
    placeholder: '请输入字段名'
  },
  {
    name: 'columnComment',
    label: '字段说明',
    type: 'input',
    placeholder: '请输入字段说明'
  }
]

// 表格列定义
const columns = [
  {
    title: '字段名',
    dataIndex: 'columnName',
    key: 'columnName',
    width: '15%',
    ellipsis: true
  },
  {
    title: '字段说明',
    dataIndex: 'columnComment',
    key: 'columnComment',
    width: '15%',
    ellipsis: true
  },
  {
    title: '数据库列类型',
    dataIndex: 'dbColumnType',
    key: 'dbColumnType',
    width: '12%',
    ellipsis: true
  },
  {
    title: '是否主键',
    dataIndex: 'isPrimaryKey',
    key: 'isPrimaryKey',
    width: '8%',
    ellipsis: true,
    customRender: ({ text }: { text: number }) => text === 1 ? '是' : '否'
  },
  {
    title: '是否必填',
    dataIndex: 'isRequired',
    key: 'isRequired',
    width: '8%',
    ellipsis: true,
    customRender: ({ text }: { text: number }) => text === 1 ? '是' : '否'
  },
  {
    title: '是否自增',
    dataIndex: 'isIncrement',
    key: 'isIncrement',
    width: '8%',
    ellipsis: true,
    customRender: ({ text }: { text: number }) => text === 1 ? '是' : '否'
  },
  {
    title: '列长度',
    dataIndex: 'columnLength',
    key: 'columnLength',
    width: '8%',
    ellipsis: true
  },
  {
    title: '小数位数',
    dataIndex: 'decimalDigits',
    key: 'decimalDigits',
    width: '8%',
    ellipsis: true
  },
  {
    title: '默认值',
    dataIndex: 'defaultValue',
    key: 'defaultValue',
    width: '10%',
    ellipsis: true
  },
  {
    title: '排序',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: '8%',
    ellipsis: true
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 状态定义
const loading = ref(false)
const dataSource = ref<TaktGenColumnDefine[]>([])
const selectedRowKeys = ref<string[]>([])
const showSearch = ref(true)
const queryParams = ref<TaktGenColumnDefineQuery>({
  pageIndex: 1,
  pageSize: 10,
  tableId: props.tableId,
  columnName: '',
  columnComment: ''
})
const pagination = reactive({
  total: 0,
  current: 1,
  pageSize: 10
})

// 表单相关
const formVisible = ref(false)
const formTitle = ref('')
const selectedColumnId = ref<string>()

// 表单校验规则
const rules: Record<string, Rule[]> = {
  columnName: [
    { required: true, message: '请输入字段名', trigger: 'blur' },
    { min: 4, message: '字段名长度不能少于4个字符', trigger: 'blur' },
    { max: 50, message: '字段名长度不能超过50个字符', trigger: 'blur' },
    { 
      validator: async (_rule: any, value: string) => {
        if (!value) return;
        // 检查是否以大写字母开头
        if (!/^[A-Z]/.test(value)) {
          throw new Error('字段名必须以大写字母开头');
        }
        // 检查是否只包含字母和数字
        if (!/^[A-Z][a-zA-Z0-9]*$/.test(value)) {
          throw new Error('字段名只能包含英文字母和数字');
        }
      }
    }
  ],
  columnComment: [
    { required: true, message: '请输入字段说明', trigger: 'blur' },
    { min: 2, message: '字段说明长度不能少于2个字符', trigger: 'blur' },
    { max: 100, message: '字段说明长度不能超过100个字符', trigger: 'blur' }
  ],
  dbColumnType: [
    { required: true, message: '请选择数据库列类型', trigger: 'change' },
    { 
      validator: (rule: any, value: string) => {
        const validTypes = ['bigint', 'char', 'datetime', 'decimal', 'int', 'nchar', 'nvarchar', 'text', 'varchar']
        if (!validTypes.includes(value)) {
          return Promise.reject('无效的数据库列类型')
        }
        return Promise.resolve()
      },
      trigger: 'change'
    }
  ],
  columnLength: [
    { required: true, message: '请输入列长度', trigger: 'blur' },
    { type: 'number', message: '列长度必须为数字', trigger: 'blur' }
  ],
  decimalDigits: [
    { required: true, message: '请输入小数位数', trigger: 'blur' },
    { type: 'number', message: '小数位数必须为数字', trigger: 'blur' }
  ],
  orderNum: [
    { required: true, message: '请输入排序号', trigger: 'blur' },
    { type: 'number', message: '排序号必须为数字', trigger: 'blur' }
  ]
}

// 导入相关
const importVisible = ref(false)
const importLoading = ref(false)
const fileList = ref<any[]>([])

// 列设置相关
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// 替换formRefs为shallowRef对象，确保只存储FormInstance类型
const formRefs = reactive<{ [key: string]: FormInstance | null }>({})

function setFormRef(key: string, el: any) {
  // 只存储FormInstance类型
  formRefs[key] = el && typeof el.validateFields === 'function' ? el as FormInstance : null;
}

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('genColumnDefineColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(columns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = columns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  columns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('genColumnDefineColumnSettings', JSON.stringify(settings))
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

// 监听 tableId 变化
watch(() => props.tableId, (newTableId) => {
  if (newTableId) {
    queryParams.value.tableId = newTableId
    fetchData()
  }
})

/** 获取表格数据 */
const fetchData = async () => {
  if (!props.tableId || props.tableId === '') {
    //message.warning('请先选择表')
    return
  }
  loading.value = true
  try {
    console.log('开始获取列定义数据，tableId:', props.tableId)
    const res = await getColumnDefineList(queryParams.value)
    console.log('列定义数据响应:', res)
    
    if (res.code === 200) {
      // 添加空值检查
      if (res.data) {
        dataSource.value = (res.data.rows || []).map((item: any) => ({
          ...item,
          isEditing: false // 设置初始状态为非编辑
        }))
        pagination.total = res.data.totalNum || 0
      } else {
        dataSource.value = []
        pagination.total = 0
        console.log('列定义数据为空')
      }
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取列定义数据失败:', error)
    message.error(t('common.failed'))
    dataSource.value = []
    pagination.total = 0
  } finally {
    loading.value = false
  }
}

// 搜索按钮操作
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置按钮操作
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    tableId: props.tableId,
    columnName: '',
    columnComment: ''
  }
  fetchData()
}

// 表格变化事件
const handleTableChange = (page: number, pageSize: number) => {
  pagination.current = page
  pagination.pageSize = pageSize
  fetchData()
}

// 新增按钮操作
const handleAdd = async () => {
  try {
    const newColumn: TaktGenColumnDefine = {
      genColumnDefineId: '',
      tableId: props.tableId,
      columnName: '',
      columnComment: '',
      dbColumnType: 'nvarchar',
      isPrimaryKey: 0,
      isRequired: 0,
      isIncrement: 0,
      columnLength: 64,
      decimalDigits: 0,
      defaultValue: '',
      orderNum: dataSource.value.length + 1,
      createBy: '',
      createTime: new Date().toISOString(),
      isDeleted: 0,
      isEditing: true // 新增时设置为编辑状态
    }
    
    // 直接添加到数据源中，进入编辑状态
    dataSource.value = [newColumn, ...dataSource.value]
    message.success('请编辑新列的信息')
  } catch (error) {
    console.error('新增失败:', error)
    message.error('新增失败')
  }
}

// 编辑选中行
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning('请选择一条记录')
    return
  }
  const record = dataSource.value.find(item => item.genColumnDefineId === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑按钮操作
const handleEdit = (record: TaktGenColumnDefine) => {
  record.isEditing = true // 设置为编辑状态
}

// 删除按钮操作
const handleDelete = async (record: TaktGenColumnDefine) => {
  try {
    if (record.genColumnDefineId === '') {
      // 如果是新增的记录，直接从数据源中移除
      const index = dataSource.value.findIndex(item => item === record)
      if (index > -1) {
        dataSource.value.splice(index, 1)
        message.success('删除成功')
      }
    } else {
      // 如果是已保存的记录，调用删除API
      const res = await deleteColumnDefine(record.genColumnDefineId)
      if (res) {
        message.success('删除成功')
        fetchData() // 重新加载数据
      }
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error('删除失败')
  }
}

// 批量删除按钮操作
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的数据')
    return
  }
  try {
    await batchDeleteColumnDefine(selectedRowKeys.value)
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除失败:', error)
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
      const res = await importColumnDefine(file)
      const { success = 0, fail = 0 } = (res as any).Data || {}
      console.log(
        'fail:',
        (res as any).Data?.fail,
        'success:',
        (res as any).Data?.success
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

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportColumnDefine(props.tableId)
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `列定义_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('common.export.failed'))
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getColumnDefineTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `列定义导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 表单提交成功回调
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 处理选择框变化
const handleSelectChange = (record: TaktGenColumnDefine, field: string, value: string) => {
  if (field === 'dbColumnType') {
    const validTypes = ['bigint', 'char', 'datetime', 'decimal', 'int', 'nchar', 'nvarchar', 'text', 'varchar']
    console.log('当前选择的数据库列类型:', value)
    console.log('有效的数据库列类型列表:', validTypes)
    console.log('类型是否有效:', validTypes.includes(value))
    
    if (!validTypes.includes(value)) {
      message.error('无效的数据库列类型')
      return
    }
  }
  
  (record as any)[field] = value
  
  // 当数据库列类型改变时，根据类型设置默认值
  if (field === 'dbColumnType') {
    console.log('设置数据库列类型后的记录:', record)
    // 如果类型不是bigint，则禁用主键和自增
    if (value !== 'bigint') {
      record.isPrimaryKey = 0
      record.isIncrement = 0
    }
    
    // 设置bigint、decimal、int类型为必填
    if (value === 'bigint' || value === 'decimal' || value === 'int') {
      record.isRequired = 1
    } else {
      record.isRequired = 0
    }
    
    switch (value) {
      case 'decimal':
        // decimal类型设置默认长度为18，精度为5，默认值为0
        record.columnLength = 18
        record.decimalDigits = 5
        record.defaultValue = '0'
        break
      case 'varchar':
      case 'nvarchar':
        // varchar和nvarchar类型默认长度为64
        record.columnLength = 64
        record.decimalDigits = 0
        record.defaultValue = ''
        break
      case 'char':
      case 'nchar':
        // char和nchar类型默认长度为255
        record.columnLength = 255
        record.decimalDigits = 0
        record.defaultValue = ''
        break
      case 'text':
        // text类型默认长度为-1（max）
        record.columnLength = -1
        record.decimalDigits = 0
        record.defaultValue = ''
        break
      case 'int':
      case 'bigint':
        // 这些类型设置列长度、小数位数和默认值为0
        record.columnLength = 0
        record.decimalDigits = 0
        record.defaultValue = '0'
        break
      case 'datetime':
        // datetime类型设置列长度和小数位数为0，默认值为Now
        record.columnLength = 0
        record.decimalDigits = 0
        record.defaultValue = 'Now'
        break
    }
  }
  
  validateField(record, field)
}

// 处理开关变化
const handleSwitchChange = (record: TaktGenColumnDefine, field: string, value: number) => {
  (record as any)[field] = value
  validateField(record, field)
}

// 处理数字输入框变化
const handleNumberChange = (record: TaktGenColumnDefine, field: string, value: number) => {
  (record as any)[field] = value
  validateField(record, field)
}

// 处理文本输入框变化
const handleInputChange = (record: TaktGenColumnDefine, field: string, value: string) => {
  if (field === 'columnName') {
    // 转换为帕斯卡命名法
    value = value.replace(/[^a-zA-Z0-9]/g, '') // 移除非字母数字字符
    if (value.length > 0) {
      value = value.charAt(0).toUpperCase() + value.slice(1) // 首字母大写
    }
    // 直接更新记录的值
    record.columnName = value;

    console.log('转换后的字段名:', value)
  } else {
    (record as any)[field] = value;
  }
}

// 验证单个字段
const validateField = (record: TaktGenColumnDefine, field: string) => {
  const fieldRules = rules[field]
  if (!fieldRules) return true

  const value = (record as any)[field]
  let isValid = true
  let errorMessage = ''

  for (const rule of fieldRules) {
    if (rule.required && !value) {
      isValid = false
      errorMessage = rule.message as string
      break
    }
    if (rule.type === 'number' && typeof value !== 'number') {
      isValid = false
      errorMessage = rule.message as string
      break
    }
    if (rule.min && value && value.length < rule.min) {
      isValid = false
      errorMessage = rule.message as string
      break
    }
    if (rule.max && value && value.length > rule.max) {
      isValid = false
      errorMessage = rule.message as string
      break
    }
    if (rule.pattern && !rule.pattern.test(value)) {
      isValid = false
      errorMessage = rule.message as string
      break
    }
  }

  if (!isValid) {
    message.error(errorMessage)
    return false
  }
  return true
}

// 处理保存按钮操作
const handleSave = async (record: TaktGenColumnDefine) => {
  try {
    // 使用每行的formRef进行校验
    await formRefs[record.genColumnDefineId]?.validateFields?.();
    await formRefs[record.genColumnDefineId + '_comment']?.validateFields?.();
    if (record.genColumnDefineId === '') {
      // 如果是新列，创建
      const res = await createColumnDefine({
        tableId: record.tableId,
        columnName: record.columnName,
        columnComment: record.columnComment,
        dbColumnType: record.dbColumnType,
        isPrimaryKey: record.isPrimaryKey,
        isRequired: record.isRequired,
        isIncrement: record.isIncrement,
        columnLength: record.columnLength,
        decimalDigits: record.decimalDigits,
        defaultValue: record.defaultValue,
        orderNum: record.orderNum
      })
      if (res.code === 200) {
        Object.assign(record, res.data)
        record.isEditing = false // 设置为非编辑状态
        message.success('保存成功')
      } else {
        message.error(res.msg || '保存失败')
      }
    } else {
      // 更新现有列
      const res = await updateColumnDefine({
        ...record
      })
      if (res.code === 200) {
        record.isEditing = false // 设置为非编辑状态
        message.success('更新成功')
      } else {
        message.error(res.msg || '更新失败')
      }
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:open', false)
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

// 初始化
onMounted(() => {
  fetchData()
  initColumnSettings()
})
</script>

<style lang="less" scoped>
.gen-column-define {
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;

  :deep(.ant-table-wrapper) {
    flex: 1;
    overflow: auto;
    height: 800px;
  }

  :deep(.ant-table) {
    width: 100%;
  }

  :deep(.ant-table-body) {
    height: 800px;
  }

  :deep(.ant-table-cell) {
    padding: 8px 16px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
  }

  :deep(.ant-input),
  :deep(.ant-select),
  :deep(.ant-input-number) {
    width: 100%;
    min-width: 100px;
  }

  :deep(.ant-form-item) {
    margin-bottom: 16px;
  }

  :deep(.ant-space) {
    width: 100%;
  }

  :deep(.ant-space-item) {
    flex: 1;
  }

  // 添加响应式样式
  :deep(.ant-table-cell) {
    .ant-input,
    .ant-select,
    .ant-input-number {
      width: 100%;
      min-width: 100px;
      max-width: 100%;
    }
  }
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

