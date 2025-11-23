<template>
  <a-modal
    :open="visible"
    title="导入表"
    width="800px"
    :confirm-loading="loading"
    @ok="handleBatchImport"
    @cancel="handleCancel"
  >
    <a-form layout="inline" :model="formState" class="mb-4">
      <a-form-item label="数据库">
        <a-select
          v-model:value="formState.databaseName"
          style="width: 200px"
          placeholder="请选择数据库"
          :loading="loading"
          @change="handleDatabaseChange"
        >
          <a-select-option v-for="db in databaseList" :key="db" :value="db">
            {{ db }}
          </a-select-option>
        </a-select>
      </a-form-item>
    </a-form>
    <Takt-table
      :columns="columns"
      :data-source="dbTableList"
      :loading="tableLoading"
      :row-key="(record: TaktGenTable) => record.genTableId"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60,
        onChange: (selectedRowKeys: any[], selectedRows: any[]) => handleSelectionChange(selectedRowKeys as string[], selectedRows as TaktGenTable[])
      }"
      :scroll="{ y: 400 }"
      :pagination="false"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['generator:table:query']"
            :show-import="true"
            :import-permission="['generator:table:import']"
            @view="handleView"
            @import="handleSingleImport"
          />
        </template>
      </template>
    </Takt-table>
    <!-- 分页组件 -->
    <Takt-pagination
      v-model:current="pagination.current"
      v-model:page-size="pagination.pageSize"
      :total="pagination.total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      @change="handlePageChange"
      @show-size-change="handleSizeChange"
    />
  </a-modal>

  <!-- 列信息弹窗 -->
  <ImportColumn
    v-model:visible="columnVisible"
    :database-name="formState.databaseName"
    :table="currentTable"
  />
</template>

<script lang="ts" setup>
import { ref, watch, reactive, h } from 'vue'
import { message } from 'ant-design-vue'
import type { TaktGenTable } from '@/types/generator/genTable'
import { getDatabasesByDb, getTablesByDb, importTable, getTableColumnsByDb, importTableAndColumns } from '@/api/generator/genTable'
import type { SelectValue } from 'ant-design-vue/es/select'
import type { DefaultOptionType } from 'ant-design-vue/es/select'
import type { TableInfoDto } from '@/types/generator/genTable'
import ImportColumn from './ImportColumn.vue'

const props = defineProps<{
  visible: boolean
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 表格列定义
const columns = [
  {
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName'
  },
  {
    title: '表描述',
    dataIndex: 'tableComment',
    key: 'tableComment'
  },
  {
    title: '操作',
    key: 'action',
    width: 100
  }
]

// 数据库列表
const databaseList = ref<string[]>([])
// 数据库表列表
const dbTableList = ref<TaktGenTable[]>([])
// 选中的表
const selectedTables = ref<TaktGenTable[]>([])
// 选中的行键值
const selectedRowKeys = ref<string[]>([])
// 表单状态
const formState = reactive({
  databaseName: ''
})
// 加载状态
const loading = ref(false)
const tableLoading = ref(false)

// 表选项
const tableOptions = ref<TableInfoDto[]>([])

// 分页配置
const pagination = reactive({
  current: 1,
  pageSize: 10,
  total: 0
})

// 列信息弹窗控制
const columnVisible = ref(false)
const currentTable = ref<TaktGenTable | null>(null)

// 监听弹窗显示状态
watch(
  () => props.visible,
  async (newVal) => {
    if (newVal) {
      await fetchDatabaseList()
    } else {
      // 关闭弹窗时重置状态
      handleCancel()
    }
  }
)

/** 获取数据库列表 */
const fetchDatabaseList = async () => {
  loading.value = true
  try {
    const res = await getDatabasesByDb()
    databaseList.value = res.data || []
  } catch (error) {
    console.error('获取数据库列表失败:', error)
    message.error('获取数据库列表失败')
  } finally {
    loading.value = false
  }
}

/** 获取表列表 */
const fetchTableList = async (databaseName: string) => {
  try {
    console.log('开始获取表列表，数据库名称:', databaseName)
    const res = await getTablesByDb(databaseName)
    console.log('获取表列表完整响应:', res)
    console.log('响应数据类型:', typeof res)
    console.log('响应数据结构:', JSON.stringify(res, null, 2))
    
    if (!res?.data) {
      throw new Error('返回数据格式错误')
    }
    
    // 检查返回的数据结构
    const tableData = res.data
    console.log('原始表数据:', JSON.stringify(tableData, null, 2))
    console.log('表数据类型:', typeof tableData)
    console.log('表数据是否为数组:', Array.isArray(tableData))
    
    if (!Array.isArray(tableData)) {
      throw new Error('返回的数据不是数组格式')
    }
    
    // 过滤掉无效数据
    const validTables = tableData.filter(item => item && item.tableName && item.tableComment)
    console.log('有效表数据:', JSON.stringify(validTables, null, 2))
    
    if (validTables.length === 0) {
      throw new Error('返回的表数据为空，请检查数据库连接和权限')
    }
    
    // 更新总数
    pagination.total = validTables.length
    
    // 计算分页数据
    const start = (pagination.current - 1) * pagination.pageSize
    const end = start + pagination.pageSize
    const pageData = validTables.slice(start, end)
    
    // 直接使用返回的数据结构
    tableOptions.value = pageData.map(item => ({
      name: item.tableName,
      description: item.tableComment,
      tableType: 'BASE TABLE'
    }))
    
    // 更新表格数据
    dbTableList.value = pageData.map((item) => {
      // 使用 tableName 的哈希值作为 genTableId
      const hash = item.tableName.split('').reduce((acc: number, char: string) => {
        return ((acc << 5) - acc) + char.charCodeAt(0) | 0
      }, 0)
      
      return {
        genTableId: String(Math.abs(hash)),
        databaseName: databaseName,
        tableName: item.tableName,
        tableComment: item.tableComment,
        createBy: '',
        createTime: new Date().toISOString(),
        isDeleted: 0,
        entityClassName: '',
        entityNamespace: '',
        baseNamespace: '',
        dtoType: '',
        dtoNamespace: '',
        dtoClassName: '',
        serviceNamespace: '',
        iServiceClassName: '',
        serviceClassName: '',
        iRepositoryNamespace: '',
        iRepositoryClassName: '',
        repositoryNamespace: '',
        repositoryClassName: '',
        controllerNamespace: '',
        controllerClassName: '',
        tplType: '0',
        tplCategory: 'crud',
        treeCode: '',
        treeName: '',
        treeParentCode: '',
        moduleName: '',
        businessName: '',
        functionName: '',
        author: '',
        genFunction: '1,2,3,4,5,6,7,8',
        isSqlDiff: 0,
        isSnowflakeId: 1,
        isRepository: 0,
        genMethod: '0',
        genPath: '',
        parentMenuId: '',
        sortType: 'asc',
        sortField: '',
        permsPrefix: '',
        generateMenu: 1,
        frontTpl: 1,
        btnStyle: 1,
        colNum: 24,
        status: 1,
        frontStyle: 1,
        options: {
          enableLog: 1,
          useSnowflakeId: 1,
          generateRepo: 0,
          checkedBtn: [1, 2, 3, 4, 5, 6, 7, 8],
          isSqlDiff: 0,
          isSnowflakeId: 1,
          isRepository: 0,
          crudGroup: []
        },
      }
    })
    
    console.log('更新后的表格数据:', dbTableList.value)
  } catch (error: any) {
    console.error('获取表列表失败:', error)
    message.error(error?.message || '获取表列表失败')
    dbTableList.value = []
    tableOptions.value = []
    pagination.total = 0
  }
}

/** 数据库选择变更事件 */
const handleDatabaseChange = async (value: SelectValue, option: DefaultOptionType | DefaultOptionType[]) => {
  if (typeof value !== 'string') return
  if (!value) return
  
  // 清空已选择的表
  selectedTables.value = []
  tableLoading.value = true
  
  try {
    await fetchTableList(value)
  } catch (error) {
    console.error('获取数据表列表失败:', error)
    message.error('获取数据表列表失败')
    dbTableList.value = []
  } finally {
    tableLoading.value = false
  }
}

/** 表格选择框事件 */
const handleSelectionChange = (selectedRowKeys: string[], selection: TaktGenTable[]) => {
  selectedTables.value = selection
}

/** 查看按钮点击事件 */
const handleView = async (record: TaktGenTable) => {
  if (!formState.databaseName) {
    message.warning('请先选择数据库')
    return
  }

  currentTable.value = record
  columnVisible.value = true
}

/** 单表导入按钮点击事件 */
const handleSingleImport = async (record: TaktGenTable) => {
  if (!formState.databaseName) {
    message.warning('请先选择数据库')
    return
  }

  if (!record.tableName) {
    message.warning('表名不能为空')
    return
  }

  loading.value = true
  try {
    console.log('开始导入表:', { databaseName: formState.databaseName, tableName: record.tableName })
    const res = await importTableAndColumns(formState.databaseName, record.tableName)
    console.log('导入响应:', res)
    
    if (res.code === 200) {
      message.success('导入成功')
      emit('success')
      handleCancel()
    } else {
      message.error(res.msg || '导入失败')
    }
  } catch (error: any) {
    console.error('导入失败:', error)
    if (error.code === 'ECONNABORTED') {
      message.error('导入超时，请稍后重试')
    } else {
      message.error(error?.response?.data?.msg || error?.message || '导入失败')
    }
  } finally {
    loading.value = false
  }
}

/** 批量导入按钮点击事件 */
const handleBatchImport = async () => {
  if (!formState.databaseName) {
    message.warning('请先选择数据库')
    return
  }
  
  if (selectedTables.value.length === 0) {
    message.warning('请选择要导入的表')
    return
  }

  loading.value = true
  try {
    const tableNames = selectedTables.value.map(table => table.tableName)
    await importTable({ tableNames })
    message.success('导入成功')
    emit('success')
    handleCancel()
  } catch (error) {
    console.error('导入失败:', error)
    message.error('导入失败')
  } finally {
    loading.value = false
  }
}

/** 导出按钮点击事件 */
const handleExport = (record: TaktGenTable) => {
  console.log('导出记录:', record)
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:visible', false)
  // 重置表单
  formState.databaseName = ''
  dbTableList.value = []
  selectedTables.value = []
  loading.value = false
  tableLoading.value = false
}

/** 编辑按钮点击事件 */
const handleEdit = (record: TaktGenTable) => {
  console.log('编辑记录:', record)
}

/** 删除按钮点击事件 */
const handleDelete = (record: TaktGenTable) => {
  console.log('删除记录:', record)
}

/** 页码改变事件 */
const handlePageChange = (page: number) => {
  pagination.current = page
  // 重新加载数据
  if (formState.databaseName) {
    fetchTableList(formState.databaseName)
  }
}

/** 每页条数改变事件 */
const handleSizeChange = (current: number, size: number) => {
  pagination.current = current
  pagination.pageSize = size
  // 重新加载数据
  if (formState.databaseName) {
    fetchTableList(formState.databaseName)
  }
}
</script>

<style lang="less" scoped>
.dialog-footer {
  text-align: right;
  padding-top: 16px;
}

.mb-4 {
  margin-bottom: 16px;
}
</style> 
