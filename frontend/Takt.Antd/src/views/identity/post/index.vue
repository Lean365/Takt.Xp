<template>
  <div class="post-container">
    <!-- 搜索区域 -->
    <Takt-query
      v-model:show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
    </Takt-query>

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['identity:post:create']"
      :show-edit="true"
      :edit-permission="['identity:post:update']"
      :show-delete="true"
      :delete-permission="['identity:post:delete']"
      :show-import="true"
      :import-permission="['identity:post:import']"
      :show-export="true"
      :export-permission="['identity:post:export']"
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
      :columns="columns.filter((col: any) => col.key && columnSettings[col.key])"
      :data-source="tableData"
      :loading="loading"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      row-key="postId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
    >
      <template #bodyCell="{ column, record }">
        <!-- 状态 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.postStatus" />
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:post:update']"
            :show-delete="true"
            :delete-permission="['identity:post:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('identity.post.allocateUser')">
                <a-button
                  v-hasPermi="['identity:post:allocate']"
                  type="default"
                  size="small"
                  class="Takt-btn-user"
                  @click.stop="handleAssignUser(record)"
                >
                  <template #icon><team-outlined /></template>
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
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 岗位表单弹窗 -->
    <post-form
      v-model:visible="formVisible"
      :title="formTitle"
      :post-id="selectedPostId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="岗位导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的岗位信息字段,\n如岗位编码,岗位名称,排序号,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['identity:post:template']"
    />

    <!-- 分配用户弹窗 -->
    <post-allocate v-model:visible="userFormVisible" :post-id="selectedPostId!" @success="handleSuccess" />

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
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktPost, TaktPostQuery } from '@/types/identity/post'
import { getPostList, deletePost, batchDeletePost, exportPost, getTemplate, importPost } from '@/api/identity/post'
import PostForm from './components/PostForm.vue'
import PostAllocate from './components/PostAllocate.vue'
import { TeamOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'postCode',
    label: t('identity.post.fields.postCode.label'),
    type: 'input' as const,
    props: {
      placeholder: t('identity.post.fields.postCode.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'postName',
    label: t('identity.post.fields.postName.label'),
    type: 'input' as const,
    props: {
      placeholder: t('identity.post.fields.postName.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.post.fields.postStatus.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktPostQuery>({
  pageIndex: 1,
  pageSize: 10,
  postCode: undefined,
  postName: undefined,
  status: -1
})

// 查询区域显示状态
const showSearch = ref(true)

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktPost[]>([])

// 选中的行
const selectedRowKeys = ref<(string | number)[]>([])

// 分页相关
const total = ref(0)

// 表格列定义
const columns: TableColumnsType = [
  {
    title: 'ID',
    dataIndex: 'postId',
    key: 'postId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.postCode.label'),
    dataIndex: 'postCode',
    key: 'postCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.postName.label'),
    dataIndex: 'postName',
    key: 'postName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.postSort.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.postStatus.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('common.table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('common.table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('common.table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180,
    ellipsis: true
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
const selectedPostId = ref<string>()

// 分配用户弹窗
const userFormVisible = ref(false)

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

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('postColumnSettings')

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
  localStorage.setItem('postColumnSettings', JSON.stringify(settings))
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getPostList(queryParams.value)
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
    postCode: undefined,
    postName: undefined,
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
  formTitle.value = t('identity.post.dialog.create')
  selectedPostId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktPost) => {
  formTitle.value = t('identity.post.dialog.update')
  selectedPostId.value = record.postId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktPost) => {
  try {
    const res = await deletePost(record.postId)
    if (res.data) {
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
    item => String(item.postId) === String(selectedRowKeys.value[0])
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
    const results = await Promise.all(selectedRowKeys.value.map(id => deletePost(id as string)))
    const hasError = results.some(res => !res.data)
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
  selectedPostId.value = undefined
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

// 处理分配用户
const handleAssignUser = (record: Record<string, any>) => {
  try {
    console.log('分配用户, record:', record)
    if (!record.postId) {
      message.error('岗位ID不能为空')
      return
    }
    selectedPostId.value = record.postId
    console.log('设置selectedPostId:', selectedPostId.value)
    userFormVisible.value = true
  } catch (error: any) {
    console.error('分配用户失败:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error('分配用户失败')
    }
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
    const res = await importPost(file)
    console.log('导入响应数据:', res)
    console.log('res.data:', res.data)
    
    // res.data.data 包含 { success, fail } 对象
    const { success = 0, fail = 0 } = res.data.data
    
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
  fetchData()
}

// 处理下载模板
const handleDownloadTemplate = async () => {
  try {
    const res = await getTemplate()
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
    const res = await exportPost(queryParams.value)
    if (res.data) {
      message.success(t('common.export.success'))
    } else {
      message.error(t('common.export.failed'))
    }
  } catch (error: any) {
    console.error('导出失败:', error)
    if (error.response?.status === 500) {
      message.error(t('common.export.failed'))
    } else {
      message.error(error.response?.data?.msg || t('common.export.failed'))
    }
  }
}

// ==================== 模板方法 ====================
// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `岗位导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: TaktPost) => {
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

// ==================== 生命周期 ====================
onMounted(async () => {
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.post-container {
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  height: 100%;
  display: flex;
  flex-direction: column;

  .ant-table-wrapper {
    flex: 1;
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


