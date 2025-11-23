<template>
  <div class="role-container">
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
      :add-permission="['identity:role:create']"
      :show-edit="true"
      :edit-permission="['identity:role:update']"
      :show-delete="true"
      :delete-permission="['identity:role:delete']"
      :show-import="true"
      :import-permission="['identity:role:import']"
      :show-export="true"
      :export-permission="['identity:role:export']"
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
      row-key="roleId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <a-switch
            :checked="record.roleStatus === 0"
            checked-children="正常"
            un-checked-children="停用"
            @change="(val: any) => handleStatusChange(record, !!val)"
            :loading="record.roleStatusLoading"
            :disabled="isSystemAdminRole(record)"
          />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="!isSystemAdminRole(record)"
            :edit-permission="['identity:role:update']"
            :show-delete="!isSystemAdminRole(record)"
            :delete-permission="['identity:role:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template v-if="!isSystemAdminRole(record)" #extra>
              <a-tooltip :title="t('identity.role.actions.authorize')">
                <a-button
                  v-hasPermi="['identity:role:allocate']"
                  :disabled="isSystemAdminRole(record)"
                  type="default"
                  size="small"
                  class="Takt-btn-menu"
                  @click.stop="handleAuthorize(record)"
                >
                  <template #icon><menu-outlined /></template>
                </a-button>
              </a-tooltip>
              <a-tooltip :title="t('identity.user.allocateDept')">
                <a-button
                  v-hasPermi="['identity:role:allocate']"
                  :disabled="isSystemAdminRole(record)"
                  type="default"
                  size="small"
                  class="Takt-btn-dept"
                  @click.stop="handleDeptAuthorize(record)"
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 角色表单弹窗 -->
    <role-form
      v-model:visible="formVisible"
      :title="formTitle"
      :role-id="selectedRoleId"
      @success="handleSuccess"
    />

    <!-- 菜单分配对话框 -->
    <menu-allocate
      v-model:visible="menuAllocateVisible"
      :role-id="selectedRoleId"
      @success="handleSuccess"
    />

    <!-- 部门分配对话框 -->
    <dept-allocate
      v-model:visible="deptAllocateVisible"
      :role-id="selectedRoleId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="角色导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的角色信息字段,\n如角色名称,角色标识,排序号,数据范围,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['identity:role:template']"
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
import { ref, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktRole, TaktRoleQuery } from '@/types/identity/role'
import { useDictStore } from '@/stores/dictStore'
import { getRoleList, deleteRole, exportRole, updateRoleStatus, getTemplate, importRole } from '@/api/identity/role'
import RoleForm from './components/RoleForm.vue'
import MenuAllocate from './components/MenuAllocate.vue'
import DeptAllocate from './components/DeptAllocate.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 判断是否为系统管理员角色
const isSystemAdminRole = (role: TaktRole) => {
  return role.roleKey === 'system_admin'
}

// ==================== CRUD 相关 ====================
// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'roleName',
    label: t('identity.role.fields.roleName.label'),
    type: 'input' as const
  },
  {
    name: 'roleKey',
    label: t('identity.role.fields.roleKey.label'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('identity.role.fields.roleStatus.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktRoleQuery>({
  pageIndex: 1,
  pageSize: 10,
  roleName: '',
  roleKey: '',
  roleStatus: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktRole[]>([])

// 选中的行
const selectedRowKeys = ref<string[]>([])

// 分页相关
const total = ref(0)

// 显示搜索
const showSearch = ref(true)

// 表格列定义
const columns = [
  {
    title: t('identity.role.fields.roleName.label'),
    dataIndex: 'roleName',
    key: 'roleName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.roleKey.label'),
    dataIndex: 'roleKey',
    key: 'roleKey',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.roleSort.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('identity.role.fields.dataScope.label'),
    dataIndex: 'dataScope',
    key: 'dataScope',
    width: 120
  },
  {
    title: t('identity.role.fields.roleStatus.label'),
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
const selectedRoleId = ref<string>()

// 菜单分配弹窗
const menuAllocateVisible = ref(false)
// 部门分配弹窗
const deptAllocateVisible = ref(false)



// ==================== 导入相关 ====================
// 导入弹窗显示状态
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
  localStorage.removeItem('roleColumnSettings')

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
  localStorage.setItem('roleColumnSettings', JSON.stringify(settings))
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

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getRoleList(queryParams.value)
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
    roleName: '',
    roleKey: '',
    roleStatus: -1
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
  formTitle.value = t('common.actions.add')
  selectedRoleId.value = undefined
  formVisible.value = true
}

// 处理成功
const handleSuccess = () => {
  formVisible.value = false
  selectedRoleId.value = undefined
  fetchData()
}

// 处理编辑
const handleEdit = (record: TaktRole) => {
  formTitle.value = t('common.actions.edit')
  selectedRoleId.value = record.roleId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktRole) => {
  try {
    const res = await deleteRole(record.roleId)
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

// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importRole(file)
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
const handleDownloadTemplate = async (): Promise<Blob> => {
  try {
    const res = await getTemplate()
    return res.data
  } catch (error: any) {
    console.error('下载模板失败:', error)
    throw error
  }
}


// 处理导出
const handleExport = () => {
  message.info(t('common.developing'))
}



// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(item => item.roleId === selectedRowKeys.value[0])
  if (record) {
    formTitle.value = t('common.actions.edit')
    selectedRoleId.value = record.roleId
    formVisible.value = true
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteRole(id.toString())))
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

// 处理授权
const handleAuthorize = (record: TaktRole) => {
  if (isSystemAdminRole(record)) return
  formTitle.value = t('identity.role.actions.authorize')
  selectedRoleId.value = record.roleId
  menuAllocateVisible.value = true
}

// 处理状态变化
const handleStatusChange = async (record: TaktRole, checked: boolean) => {
  // @ts-ignore
  record.roleStatusLoading = true
  try {
    const newStatus = checked ? 0 : 1
    const res = await updateRoleStatus({ roleId: record.roleId.toString(), roleStatus: newStatus })
    if (res.data) {
      record.roleStatus = newStatus
      message.success('状态更新成功')
    } else {
      message.error('状态更新失败')
    }
  } catch (error) {
    message.error('状态更新失败')
  }
  // @ts-ignore
  record.roleStatusLoading = false
}

// 处理行点击
const handleRowClick = (record: TaktRole) => {
  console.log('行点击:', record)
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

// 处理部门授权
const handleDeptAuthorize = (record: TaktRole) => {
  if (isSystemAdminRole(record)) return
  formTitle.value = t('identity.role.actions.deptAuthorize')
  selectedRoleId.value = record.roleId
  deptAllocateVisible.value = true
}





onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.role-container {
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

