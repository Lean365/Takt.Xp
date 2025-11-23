<template>
  <div class="dept-container">
    <!-- 搜索区域 -->
    <Takt-query
      v-model:show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
    </Takt-query>

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['identity:dept:create']"
      :show-edit="true"
      :edit-permission="['identity:dept:update']"
      :show-delete="true"
      :delete-permission="['identity:dept:delete']"
      :show-import="true"
      :import-permission="['identity:dept:import']"
      :show-export="true"
      :export-permission="['identity:dept:export']"
      :disabled-edit="selectedKeys.length !== 1"
      :disabled-delete="selectedKeys.length === 0"
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
      <!-- 自定义按钮 -->
      <template #extra>
        <a-button @click="toggleExpand" class="Takt-btn-expand">
          <template #icon><expand-outlined /></template>
          {{ isExpanded ? '收缩' : '展开' }}
        </a-button>
      </template>
    </Takt-toolbar>

    <!-- 数据表格 -->
    <Takt-tree-table
      ref="treeTableRef"
      :columns="columns.filter((col: any) => col.key && columnSettings[col.key])"
      :data-source="deptList"
      :loading="loading"
      :pagination="false"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      v-model:expanded-row-keys="expandedRowKeys"
      :indent-size="20"
      :children-column-name="'children'"
      row-key="deptId"
      size="middle"
      bordered
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :virtual="true"
      :lazy="false"
      :resizable="true"
      @expand="onExpand"
    >
      <template #bodyCell="{ column, record }">
        <!-- 部门名称 -->
        <template v-if="column.dataIndex === 'deptName'">
          <span>
            <folder-outlined />
            {{ record.deptName }}
          </span>
        </template>
        <!-- 状态 -->
        <template v-else-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.deptStatus" />
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:dept:update']"
            :show-delete="true"
            :delete-permission="['identity:dept:delete']"
            :show-assign-user="true"
            :assign-user-permission="['identity:dept:allocate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @assign-user="handleAssignUser"
          />
        </template>
      </template>
    </Takt-tree-table>

    <!-- 部门表单弹窗 -->
    <dept-form
      v-model:visible="formVisible"
      :title="formTitle"
      :deptId="formDeptId"
      @success="fetchData"
    />

    <!-- 分配用户弹窗 -->
    <dept-allocate
      v-model:visible="userFormVisible"
      :deptId="selectedDeptId!"
      @success="fetchData"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleTemplate"
      template-file-name="部门导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的部门信息字段,\n如部门名称,父部门名称,显示顺序,负责人,联系电话,邮箱,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['identity:dept:template']"
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
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import { FolderOutlined, ExpandOutlined, TeamOutlined } from '@ant-design/icons-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktDept, TaktDeptQuery } from '@/types/identity/dept'
import { getDeptTree, deleteDept, batchDeleteDept, exportDept, importDept, getTemplate } from '@/api/identity/dept'
import DeptForm from './components/DeptForm.vue'
import DeptAllocate from './components/DeptAllocate.vue'


const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'deptName',
    label: t('identity.dept.fields.deptName.label'),
    placeholder: t('identity.dept.fields.deptName.placeholder'),
    type: 'input',
    props: {
     
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.dept.fields.deptStatus.label'),
    placeholder: t('identity.dept.fields.deptStatus.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktDeptQuery>({
  pageIndex: 1,
  pageSize: 10,
  deptName: undefined,
  status: -1
})

// 加载状态
const loading = ref(false)

// 部门列表数据
const deptList = ref<TaktDept[]>([])

// 选中的部门ID
const selectedKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的部门ID
const formDeptId = ref<string>()

// 分配用户弹窗显示状态
const userFormVisible = ref(false)

// 当前选中的部门ID（用于分配用户）
const selectedDeptId = ref<string>()

// 导入弹窗显示状态
const importVisible = ref(false)

// 表格列定义
const columns = [
  {
    title: t('identity.dept.fields.deptName.label'),
    dataIndex: 'deptName',
    key: 'deptName',
    width: 200,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('identity.dept.fields.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('identity.dept.fields.deptStatus.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('identity.dept.fields.leader.label'),
    dataIndex: 'leader',
    key: 'leader',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('identity.dept.fields.phone.label'),
    dataIndex: 'phone',
    key: 'phone',
    width: 120,
    ellipsis: true,
    resizable: true
  },
  {
    title: t('identity.dept.fields.email.label'),
    dataIndex: 'email',
    key: 'email',
    width: 180,
    ellipsis: true,
    resizable: true
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
    fixed: 'right' as const
  }
]

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('deptColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 总数
const total = ref(0)

// 处理分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 处理每页条数变化
const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  fetchData()
}

// 获取部门列表
const fetchData = async () => {
  try {
    loading.value = true
    const res = await getDeptTree(queryParams.value)
    console.log('[部门管理] 前端接收到的数据:', res.data)
    deptList.value = res.data
    total.value = res.data.length
    // 如果当前是展开状态，更新展开的节点
    if (isExpanded.value) {
      expandedRowKeys.value = getAllKeys(deptList.value)
    } else {
      expandedRowKeys.value = []
    }
  } catch (error: any) {
    console.error('[部门管理] 获取部门列表出错:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error(t('common.failed'))
    }
  } finally {
    loading.value = false
  }
}

// 处理查询
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  fetchData()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    deptName: undefined,
    status: -1
  }
  fetchData()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: TaktDept[]) => {
  selectedKeys.value = keys
}



// 处理分配用户
const handleAssignUser = (record: Record<string, any>) => {
  selectedDeptId.value = record.deptId
  userFormVisible.value = true
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.dept.dialog.create')
  formDeptId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const deptId = selectedKeys.value[0]
  formTitle.value = t('identity.dept.dialog.update')
  formDeptId.value = String(deptId)
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.dept.dialog.update')
  formDeptId.value = String(record.deptId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deleteDept(record.deptId)
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

// 处理批量删除
const handleBatchDelete = () => {
  Modal.confirm({
    title: t('common.delete.confirm'),
    content: t('common.delete.message', { count: selectedKeys.value.length }),
    async onOk() {
      try {
        const res = await batchDeleteDept(selectedKeys.value.map(id => String(id)))
        if (res.data) {
          message.success(t('common.delete.success'))
          selectedKeys.value = []
          fetchData()
        } else {
          message.error(t('common.delete.failed'))
        }
      } catch (error) {
        console.error(error)
        message.error(t('common.delete.failed'))
      }
    }
  })
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
  localStorage.setItem('deptColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportDept({
      ...queryParams.value
      //sheetName: '部门信息'
    })
    // 动态获取文件名
    console.log('res.headers', res.headers)
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
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
      fileName = `部门数据_${new Date().getTime()}.xlsx`
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

const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

const toggleFullscreen = () => {
  // 处理全屏切换
  console.log('全屏切换')
}

const treeTableRef = ref()
const isExpanded = ref(false)
const expandedRowKeys = ref<(string | number)[]>([])

// 递归获取所有节点的ID
const getAllKeys = (data: TaktDept[]): (string | number)[] => {
  return data.flatMap(item => [
    item.deptId,
    ...(item.children ? getAllKeys(item.children) : [])
  ])
}

// 处理展开/收缩事件
const onExpand = (expanded: boolean, record: TaktDept) => {
  isExpanded.value = expandedRowKeys.value.length > 0
}

// 切换展开/收缩
const toggleExpand = () => {
  isExpanded.value = !isExpanded.value
  if (isExpanded.value) {
    treeTableRef.value?.expandAll()
  } else {
    treeTableRef.value?.collapseAll()
  }
}



// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importDept(file)
    console.log('导入响应数据:', res)
    console.log('res.data:', res.data)
    
    // 直接使用res.data，它已经是{ success: number; fail: number }格式
    const { success = 0, fail = 0 } = res.data || {}
    
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

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    throw error
  }
}

onMounted(() => {
  initColumnSettings()
  fetchData()
})

// 将方法暴露给模板
defineExpose({
  onExpand
})
</script>

<style lang="less" scoped>
.dept-container {
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

