<template>
  <div class="user-container">
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
      :add-permission="['identity:user:create']"
      :show-edit="true"
      :edit-permission="['identity:user:update']"
      :show-delete="true"
      :delete-permission="['identity:user:delete']"
      :show-import="true"
      :import-permission="['identity:user:import']"
      :show-export="true"
      :export-permission="['identity:user:export']"
      :disabled-edit="selectedRowKeys.length !== 1 || (selectedRowKeys.length === 1 && selectedUsers.length > 0 && isAdminUser(selectedUsers[0]))"
      :disabled-delete="selectedRowKeys.length === 0 || hasSelectedAdminUsers"
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
      row-key="userId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 头像列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'avatar'">
          <a-image
            :src="getAvatarUrl(record.avatar)"
            :width="32"
            :height="32"
            :preview="{
              src: getAvatarUrl(record.avatar)
            }"
            style="border-radius: 50%; object-fit: cover"
          />
        </template>

        <!-- 用户类型列 -->
        <template v-if="column.dataIndex === 'userType'">
          <Takt-dict-tag dict-type="identity_user_type" :value="record.userType" />
        </template>

        <!-- 性别列 -->
        <template v-if="column.dataIndex === 'gender'">
          <Takt-dict-tag dict-type="identity_user_gender" :value="record.gender" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <a-switch
            v-hasPermi="['identity:user:update']"
            :checked="record.userStatus === 0"
            :checked-children="dictStore.getDictLabel('sys_normal_disable', 0)"
            :un-checked-children="dictStore.getDictLabel('sys_normal_disable', 1)"
            @change="(val: any) => handleStatusChange(record, !!val)"
            :loading="record.userStatusLoading"
            :disabled="isAdminUser(record)"
          />
        </template>

        <!-- 锁定时间列 -->
        <template v-if="column.dataIndex === 'lockEndTime'">
          {{ record.lockEndTime ? new Date(record.lockEndTime).toLocaleString() : t('identity.user.table.notLocked') }}
        </template>

        <!-- 锁定原因列 -->
        <template v-if="column.dataIndex === 'lockReason'">
          {{ record.lockReason || t('identity.user.table.none') }}
        </template>

        <!-- 锁定状态列 -->
        <template v-if="column.dataIndex === 'isLock'">
          <a-switch
            v-hasPermi="['identity:user:update']"
            :checked="record.isLock === 0"
            :checked-children="dictStore.getDictLabel('identity_user_locked', 0)"
            :un-checked-children="dictStore.getDictLabel('identity_user_locked', 1)"
            @change="(val: any) => handleLockStatusChange(record, !!val)"
            :loading="record.lockStatusLoading"
            :disabled="isAdminUser(record)"
          />
        </template>

        <!-- 错误次数列 -->
        <template v-if="column.dataIndex === 'errorLimit'">
          {{ record.errorLimit || t('identity.user.table.none') }}
        </template>

        <!-- 登录次数列 -->
        <template v-if="column.dataIndex === 'loginCount'">
          {{ record.loginCount || t('identity.user.table.none') }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="!isAdminUser(record)"
            :edit-permission="['identity:user:update']"
            :show-delete="!isAdminUser(record)"
            :delete-permission="['identity:user:delete']"
            :show-authorize="!isAdminUser(record)"
            :authorize-permission="['identity:user:allocate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @authorize="handleAuthorize"
          >
            <template v-if="!isAdminUser(record)" #extra>
              <a-dropdown>
                <a-button>
                  {{ t('common.actions.more') }}
                  <DownOutlined />
                </a-button>
                <template #overlay>
                  <a-menu>
                    <a-tooltip
                      v-if="!isAdminUser(record)"
                      :title="t('identity.user.resetPwd')"
                    >
                      <a-button
                        v-hasPermi="['identity:user:resetpwd']"
                        type="default"
                        size="small"
                        class="Takt-btn-reset"
                        @click.stop="handleResetPassword(record)"
                      >
                        <template #icon><key-outlined /></template>
                      </a-button>
                    </a-tooltip>
                    <a-tooltip
                      v-if="!isAdminUser(record)"
                      :title="t('identity.user.allocateDept')"
                    >
                      <a-button
                        v-hasPermi="['identity:user:allocate']"
                        type="default"
                        size="small"
                        class="Takt-btn-dept"
                        @click.stop="handleAllocateDept(record)"
                      >
                        <template #icon><team-outlined /></template>
                      </a-button>
                    </a-tooltip>
                    <a-tooltip
                      v-if="!isAdminUser(record)"
                      :title="t('identity.user.allocatePost')"
                    >
                      <a-button
                        v-hasPermi="['identity:user:allocate']"
                        type="default"
                        size="small"
                        class="Takt-btn-post"
                        @click.stop="handleAllocatePost(record)"
                      >
                        <template #icon><solution-outlined /></template>
                      </a-button>
                    </a-tooltip>
                  </a-menu>
                </template>
              </a-dropdown>
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

    <!-- 用户表单对话框 -->
    <user-form
      v-model:visible="userFormVisible"
      :title="formTitle"
      :user-id="selectedUserId"
      @success="handleSuccess"
    />

    <!-- 重置密码表单对话框 -->
    <reset-pwd-form
      v-model:visible="resetPwdVisible"
      :user-id="selectedUserId!"
      @success="handleResetPwdSuccess"
    />

    <!-- 角色分配对话框 -->
    <role-allocate
      v-model:visible="roleDialogVisible"
      :user-id="selectedUserId!"
      @success="handleSuccess"
    />

    <!-- 部门分配对话框 -->
    <dept-allocate
      v-if="deptDialogVisible && selectedUserId !== undefined"
      v-model:visible="deptDialogVisible"
      :user-id="selectedUserId"
      @success="handleSuccess"
    />

    <!-- 岗位分配对话框 -->
    <post-allocate
      v-model:visible="postDialogVisible"
      :user-id="selectedUserId!"
      @success="handleSuccess"
    />


    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleTemplate"
      :template-file-name="t('identity.user.template.fileName') + '.xlsx'"
      :tips="t('identity.user.importTips')"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['identity:user:template']"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      :title="t('common.actions.columnSetting')"
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
import { ref, onMounted, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktUser, TaktUserQuery } from '@/types/identity/user'
import { useDictStore } from '@/stores/dictStore'
import {
  KeyOutlined, 
  TeamOutlined, 
  SolutionOutlined,
  ApartmentOutlined
} from '@ant-design/icons-vue'
import {
  getUserList,
  deleteUser,
  exportUser,
  importUser,
  getTemplate,
  updateUserStatus,
  resetPassword,
  createUser,
  updateUser,
} from '@/api/identity/user'
import UserForm from './components/UserForm.vue'
import ResetPwdForm from './components/ResetPwdForm.vue'
import RoleAllocate from './components/RoleAllocate.vue'
import DeptAllocate from './components/DeptAllocate.vue'
import PostAllocate from './components/PostAllocate.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 判断是否为管理用户（系统用户、管理员或admin用户）
const isAdminUser = (user: TaktUser) => {
  return user.userName === 'admin' || user.userType === 0 || user.userType === 2
}

// 计算选中的用户
const selectedUsers = computed(() => {
  return tableData.value.filter(item =>
    selectedRowKeys.value.includes(String(item.userId))
  )
})

// 计算是否包含管理用户
const hasSelectedAdminUsers = computed(() => {
  return selectedUsers.value.some(user => isAdminUser(user))
})

// ==================== 查询相关 ====================
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: () => t('identity.user.fields.userName.label'),
    type: 'input' as const,
  },
  {
    name: 'nickName',
    label: () => t('identity.user.fields.nickName.label'),
    type: 'input' as const
  },
  {
    name: 'phoneNumber',
    label: () => t('identity.user.fields.phoneNumber.label'),
    type: 'input' as const
  },
  {
    name: 'email',
    label: () => t('identity.user.fields.email.label'),
    type: 'input' as const
  },
  {
    name: 'userType',
    label: () => t('identity.user.fields.userType.label'),
    type: 'select' as const,
    props: {
      dictType: 'identity_user_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'gender',
    label: () => t('identity.user.fields.gender.label'),
    type: 'select' as const,
    props: {
      dictType: 'identity_user_gender',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: () => t('identity.user.fields.userStatus.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<TaktUserQuery>({
  pageIndex: 1,
  pageSize: 10,
  userName: '',
  nickName: '',
  phoneNumber: '',
  email: '',
  userType: -1,
  gender: -1,
  userStatus: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 表格数据
const tableData = ref<TaktUser[]>([])

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
    dataIndex: 'userId',
    key: 'userId',
    width: 80,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.userName.label'),
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.nickName.label'),
    dataIndex: 'nickName',
    key: 'nickName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.englishName.label'),
    dataIndex: 'englishName',
    key: 'englishName',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.userType.label'),
    dataIndex: 'userType',
    key: 'userType',
    width: 100
  },
  {
    title: () => t('identity.user.fields.email.label'),
    dataIndex: 'email',
    key: 'email',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.phoneNumber.label'),
    dataIndex: 'phoneNumber',
    key: 'phoneNumber',
    width: 120,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.gender.label'),
    dataIndex: 'gender',
    key: 'gender',
    width: 80
  },
  {
    title: () => t('identity.user.fields.avatar.label'),
    dataIndex: 'avatar',
    key: 'avatar',
    width: 100,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.userStatus.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: () => t('identity.user.fields.lockEndTime.label'),
    dataIndex: 'lockEndTime',
    key: 'lockEndTime',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.lockReason.label'),
    dataIndex: 'lockReason',
    key: 'lockReason',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('identity.user.fields.isLock.label'),
    dataIndex: 'isLock',
    key: 'isLock',
    width: 100
  },
  {
    title: () => t('identity.user.fields.errorLimit.label'),
    dataIndex: 'errorLimit',
    key: 'errorLimit',
    width: 120
  },
  {
    title: () => t('identity.user.fields.loginCount.label'),
    dataIndex: 'loginCount',
    key: 'loginCount',
    width: 120
  },
  {
    title: () => t('identity.user.fields.lastPasswordChangeTime.label'),
    dataIndex: 'lastPasswordChangeTime',
    key: 'lastPasswordChangeTime',
    width: 180,
    ellipsis: true
  },
  {
    title: () => t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: () => t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: () => t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: () => t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: () => t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: () => t('table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: () => t('table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: () => t('table.columns.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// ==================== 表单相关 ====================
// 获取头像完整URL
const getAvatarUrl = (avatar: string | null) => {
  const baseUrl = 'https://localhost:50081'
  return baseUrl + (avatar || '/avatar/default.gif')
}

// 表单对话框
const userFormVisible = ref(false)
const formTitle = ref('')
const selectedUserId = ref<string>()

// 重置密码表单
const resetPwdVisible = ref(false)

// 角色分配弹窗
const roleDialogVisible = ref(false)

// 部门分配弹窗
const deptDialogVisible = ref(false)

// 岗位分配弹窗
const postDialogVisible = ref(false)


// ==================== 业务操作相关 ====================
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
  localStorage.removeItem('userColumnSettings')

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
  localStorage.setItem('userColumnSettings', JSON.stringify(settings))
}



// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log(t('identity.user.table.queryParams'), {
      ...queryParams.value
    })

    const res = await getUserList(queryParams.value)
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
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    userName: '',
    nickName: '',
    phoneNumber: '',
    email: '',
    gender: -1,
    userStatus: -1,
    userType: -1,
    deptId: undefined
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
  selectedUserId.value = undefined
  userFormVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktUser) => {
  formTitle.value = t('common.actions.edit')
  selectedUserId.value = record.userId
  userFormVisible.value = true
}

// 处理删除
const handleDelete = async (record: TaktUser) => {
  if (isAdminUser(record)) {
      message.warning(t('identity.user.messages.cannotDeleteAdmin'))
    return
  }
  try {
    const res = await deleteUser(record.userId)
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
    item => String(item.userId) === String(selectedRowKeys.value[0])
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

  // 判断是否包含管理用户
  console.log('selectedUsers:', selectedUsers.value)
  if (hasSelectedAdminUsers.value) {
      message.warning(t('identity.user.messages.cannotDeleteAdmin'))
    return
  }

  // 只有不包含 admin 时才会执行下面的删除逻辑
  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteUser(id)))
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
  userFormVisible.value = false
  roleDialogVisible.value = false
  deptDialogVisible.value = false
  postDialogVisible.value = false
  selectedUserId.value = undefined
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

// 处理状态变化
const handleStatusChange = async (record: TaktUser, checked: boolean) => {
  // @ts-ignore
  record.userStatusLoading = true
  try {
    const newStatus = checked ? 0 : 1
    const res = await updateUserStatus({ userId: record.userId, userStatus: newStatus })
    if (res) {
      record.userStatus = newStatus
      message.success(t('identity.user.messages.statusUpdateSuccess'))
    } else {
      message.error(t('identity.user.messages.statusUpdateFailed'))
    }
  } catch (error) {
    message.error(t('identity.user.messages.statusUpdateFailed'))
  }
  // @ts-ignore
  record.userStatusLoading = false
}

// 处理锁定状态变化
const handleLockStatusChange = async (record: TaktUser, checked: boolean) => {
  // @ts-ignore
  record.lockStatusLoading = true
  try {
    const newLockStatus = checked ? 0 : 1
    // 使用updateUser API更新锁定状态
    const updateData = {
      userId: record.userId,
      userName: record.userName,
      nickName: record.nickName || '',
      userType: record.userType,
      password: '', // 不修改密码
      realName: record.realName || '',
      fullName: record.fullName || '',
      englishName: record.englishName || '',
      email: record.email || '',
      phoneNumber: record.phoneNumber || '',
      gender: record.gender,
      avatar: record.avatar || '',
      userStatus: record.userStatus,
      roleIds: [], // 保持原有角色
      postIds: [], // 保持原有岗位
      deptIds: [], // 保持原有部门
      remark: record.remark || ''
    }
    
    const res = await updateUser(updateData)
    if (res) {
      record.isLock = newLockStatus
      message.success(t('identity.user.messages.lockStatusUpdateSuccess'))
    } else {
      message.error(t('identity.user.messages.lockStatusUpdateFailed'))
    }
  } catch (error) {
    console.error(t('identity.user.messages.lockStatusUpdateFailed'), error)
    message.error(t('identity.user.messages.lockStatusUpdateFailed'))
  }
  // @ts-ignore
  record.lockStatusLoading = false
}

// 处理重置密码
const handleResetPassword = (record: TaktUser) => {
  // 这里假设有resetPassword API，传递userId和默认密码
  resetPassword({ userId: record.userId, newPassword: 'Takt@123852' })
    .then(res => {
      if (res) {
        message.success(t('identity.user.messages.resetPasswordSuccess'))
      } else {
        message.error(t('common.failed'))
      }
    })
    .catch(error => {
      console.error(error)
      message.error(t('common.failed'))
    })
}

// 处理重置密码成功
const handleResetPwdSuccess = () => {
  resetPwdVisible.value = false
  message.success(t('identity.user.messages.resetPasswordSuccess'))
}

// 处理授权
const handleAuthorize = (record: TaktUser) => {
  selectedUserId.value = record.userId
  roleDialogVisible.value = true
}

// 处理分配部门
const handleAllocateDept = (record: TaktUser) => {
  selectedUserId.value = record.userId
  deptDialogVisible.value = true
}

// 处理分配岗位
const handleAllocatePost = (record: TaktUser) => {
  selectedUserId.value = record.userId
  postDialogVisible.value = true
}


// ==================== 导入方法 ====================
// 处理导入
const handleImport = () => {
  importVisible.value = true
}

// 处理导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importUser(file, 'User')
    console.log(t('identity.user.table.importResponseData'), res)
    console.log('res.data:', res.data)
    
    // res.data 包含 { success, fail } 对象
    const { success = 0, fail = 0 } = res.data
    
    console.log(t('identity.user.table.parsedData'), { success, fail })
    
    return {
      code: 200,
      msg: t('identity.user.messages.importSuccess'),
      data: {
        success,
        fail
      }
    }
  } catch (error: any) {
    console.error(t('identity.user.messages.importFailed'), error)
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
    const res = await exportUser({
      ...queryParams.value
    }, 'User', t('identity.user.export.fileName'))
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
        fileName = `${t('identity.user.export.fileName')}_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error(t('identity.user.table.exportFailed'), error)
    message.error(error.message || t('common.export.failed'))
  }
}

// ==================== 模板方法 ====================
// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate('User', t('identity.user.template.fileName'))
    return res.data
  } catch (error: any) {
    console.error(t('identity.user.table.downloadTemplateFailed'), error)
    throw error
  }
}



// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理行点击
const handleRowClick = (record: TaktUser) => {
  console.log(t('identity.user.table.rowClicked'), record)
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log(t('identity.user.table.toggleFullscreenState'), isFullscreen)
}

// ==================== 生命周期 ====================
onMounted(async () => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable', 'identity_user_gender', 'identity_user_type', 'identity_user_locked'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})

// 监听语言变化，强制重新渲染
watch(
  () => t('identity.user.fields.userName.label'),
  () => {
    // 语言变化时，强制重新渲染
    console.log('语言变化，重新渲染用户管理页面')
  }
)
</script>

<style lang="less" scoped>
.user-container {
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
