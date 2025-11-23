<template>
  <div class="menu-container">
    <!-- 搜索区域 -->
    <Takt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['identity:menu:create']"
      :show-edit="true"
      :edit-permission="['identity:menu:update']"
      :show-delete="true"
      :delete-permission="['identity:menu:delete']"
      :show-import="true"
      :import-permission="['identity:menu:import']"
      :show-export="true"
      :export-permission="['identity:menu:export']"
      :disabled-edit="selectedKeys.length !== 1"
      :disabled-delete="selectedKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @export="handleExport"
      @refresh="getList"
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
      :data-source="list"
      :loading="loading"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      v-model:expanded-row-keys="expandedRowKeys"
      :indent-size="20"
      :children-column-name="'children'"
      row-key="menuId"
      size="middle"
      bordered
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
      :virtual="true"
      :lazy="true"
      :height="500"
      :item-size="54"
      :overscan="5"
      @expand="onExpand"
      @load-data="handleLoadData"
    >
      <template #bodyCell="{ column, record }">
        <!-- 菜单名称 -->
        <template v-if="column.dataIndex === 'menuName'">
          <span>
            <folder-outlined v-if="record.menuType === 0" />
            <menu-outlined v-else-if="record.menuType === 1" />
            <tool-outlined v-else />
            {{ record.menuName }}
          </span>
        </template>
        <!-- 图标 -->
        <template v-else-if="column.dataIndex === 'icon'">
          <span v-if="record.icon">
            <component :is="record.icon" />
            {{ record.icon }}
          </span>
        </template>
        <!-- 权限标识 -->
        <template v-else-if="column.dataIndex === 'perms'">
          <a-tag v-if="record.perms">{{ record.perms }}</a-tag>
        </template>
        <!-- 组件路径 -->
        <template v-else-if="column.dataIndex === 'component'">
          <a-tag v-if="record.component">{{ record.component }}</a-tag>
        </template>
        <!-- 状态 -->
        <template v-else-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:menu:update']"
            :show-delete="true"
            :delete-permission="['identity:menu:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </Takt-tree-table>

    <!-- 表单弹窗 -->
    <menu-tabs
      :visible="formVisible"
      :title="formTitle"
      :menu-id="formMenuId"
      :menu-type="formMenuType"
      @success="getList"
      @update:visible="formVisible = $event"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="菜单导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的菜单信息字段,\n如菜单名称,菜单类型,权限标识,组件路径,状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['identity:menu:template']"
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
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>

    <MenuType
      v-model:visible="menuTypeDialogVisible"
      :title="t('identity.menu.actions.selectType')"
      :ok-text="t('common.ok')"
      :cancel-text="t('common.cancel')"
      @ok="onMenuTypeOk"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import { FolderOutlined, MenuOutlined, ToolOutlined, ExpandOutlined } from '@ant-design/icons-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktMenu, TaktMenuQuery } from '@/types/identity/menu'
import {
  getMenuTree,
  deleteMenu,
  batchDeleteMenu,
  exportMenu,
  importMenu,
  getTemplate
} from '@/api/identity/menu'
import MenuTabs from './components/MenuTabs.vue'
import MenuType from './components/MenuType.vue'

const { t } = useI18n()

// ==================== 查询相关 ====================
// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'menuName',
    label: t('identity.menu.fields.menuName.label'),
    placeholder: t('identity.menu.fields.menuName.placeholder'),
    type: 'input',
    props: {
      allowClear: true
    }
  },
  {
    name: 'menuType',
    label: t('identity.menu.fields.menuType.label'),
    placeholder: t('identity.menu.fields.menuType.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_menu_type',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'visible',
    label: t('identity.menu.fields.visible.label'),
    placeholder: t('identity.menu.fields.visible.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_is_visible',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'isExternal',
    label: t('identity.menu.fields.isExternal.label'),
    placeholder: t('identity.menu.fields.isExternal.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'isCache',
    label: t('identity.menu.fields.isCache.label'),
    placeholder: t('identity.menu.fields.isCache.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.menu.fields.status.label'),
    placeholder: t('identity.menu.fields.status.placeholder'),
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
const queryParams = ref<TaktMenuQuery>({
  pageIndex: 1,
  pageSize: 10,
  menuName: '',
  status: -1
})

// ==================== 表格相关 ====================
// 加载状态
const loading = ref(false)

// 菜单列表数据
const list = ref<TaktMenu[]>([])

// 选中的菜单ID
const selectedKeys = ref<(string | number)[]>([])

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.menu.fields.menuName.label'),
    dataIndex: 'menuName',
    key: 'menuName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.icon.label'),
    dataIndex: 'icon',
    key: 'icon',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.perms.label'),
    dataIndex: 'perms',
    key: 'perms',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.path.label'),
    dataIndex: 'path',
    key: 'path',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.component.label'),
    dataIndex: 'component',
    key: 'component',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.queryParams.label'),
    dataIndex: 'queryParams',
    key: 'queryParams',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.isExternal.label'),
    dataIndex: 'isExternal',
    key: 'isExternal',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.isCache.label'),
    dataIndex: 'isCache',
    key: 'isCache',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.menuType.label'),
    dataIndex: 'menuType',
    key: 'menuType',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.visible.label'),
    dataIndex: 'visible',
    key: 'visible',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100,
    ellipsis: true
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

// ==================== 表单相关 ====================
// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的菜单ID
const formMenuId = ref<string>()

// 菜单类型
const formMenuType = ref<number>(0)

// 菜单类型选择弹窗
const menuTypeDialogVisible = ref(false)

// ==================== 导入相关 ====================
// 导入对话框
const importVisible = ref(false)

// ==================== 树形表格相关 ====================
// 树形表格引用
const treeTableRef = ref()

// 展开状态
const isExpanded = ref(false)

// 展开的行键
const expandedRowKeys = ref<(string | number)[]>([])

// ==================== 列设置相关 ====================
// 列设置抽屉显示状态
const columnSettingVisible = ref(false)

// 默认列定义
const defaultColumns = [
  {
    title: t('identity.menu.fields.menuName.label'),
    dataIndex: 'menuName',
    key: 'menuName',
    width: 220
  },
  {
    title: t('identity.menu.fields.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('identity.menu.fields.path.label'),
    dataIndex: 'path',
    key: 'path',
    width: 200
  },
  {
    title: t('identity.menu.fields.component.label'),
    dataIndex: 'component',
    key: 'component',
    width: 150
  },
  {
    title: t('identity.menu.fields.perms.label'),
    dataIndex: 'perms',
    key: 'perms',
    width: 150
  },
  {
    title: t('identity.menu.fields.menuType.label'),
    dataIndex: 'menuType',
    key: 'menuType',
    width: 100
  },
  {
    title: t('identity.menu.fields.status.label'),
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

// 列设置状态
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  localStorage.removeItem('menuColumnSettings')
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))
  // 默认显示前7列（不含操作列）
  const firstColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 7)
  firstColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('menuColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// ==================== 业务操作方法 ====================
// 获取菜单列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getMenuTree(queryParams.value as any)
    if (res.data) {
      list.value = res.data
      // 如果当前是展开状态，只展开第一层
      if (isExpanded.value) {
        expandedRowKeys.value = list.value.map(item => item.menuId)
      } else {
        expandedRowKeys.value = []
      }
    } else {
      message.error(t('common.failed'))
    }
  } catch (error: any) {
    console.error('[菜单管理] 获取菜单列表出错:', error)
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

// 查询方法
const handleQuery = (values?: TaktMenuQuery) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  getList()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    menuName: '',
    status: -1
  }
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: TaktMenu[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  menuTypeDialogVisible.value = true
}

// 菜单类型选择确认
function onMenuTypeOk(type: number) {
  formMenuType.value = type
  formTitle.value =
    type === 0
      ? t('identity.menu.actions.addDirectory')
      : type === 1
        ? t('identity.menu.actions.addMenu')
        : t('identity.menu.actions.addButton')
  formMenuId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: TaktMenu) => {
  formTitle.value = t('identity.menu.actions.edit')
  formMenuId.value = record.menuId
  formMenuType.value = record.menuType
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deleteMenu(record.menuId)
    if (res.data) {
      message.success(t('common.delete.success'))
      getList()
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
        const res = await batchDeleteMenu(selectedKeys.value.map(id => String(id)))
        if (res.data) {
          message.success(t('common.delete.success'))
          selectedKeys.value = []
          getList()
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

// 处理编辑选中记录
const handleEditSelected = () => {
  if (selectedKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }
  const record = list.value.find(item => String(item.menuId) === String(selectedKeys.value[0]))
  if (record) {
    handleEdit(record)
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
    const res = await importMenu(file)
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
  getList()
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
    const { data } = await exportMenu(queryParams.value as any)
    const blob = new Blob([data], { type: 'application/vnd.ms-excel' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `menu_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error(error)
    message.error(t('common.export.failed'))
  }
}

// ==================== 模板方法 ====================
// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `菜单导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// ==================== 树形表格方法 ====================
// 处理展开/收缩事件
const onExpand = async (expanded: boolean, record: TaktMenu) => {
  if (expanded && !record.children) {
    try {
      loading.value = true
      const res = await getMenuTree({
        ...queryParams.value,
        parentId: record.menuId
      } as any)
      if (res.data) {
        record.children = res.data
      } else {
        message.error(t('common.failed'))
      }
    } catch (error: any) {
      console.error('[菜单管理] 加载子节点数据出错:', error)
      message.error(t('common.failed'))
    } finally {
      loading.value = false
    }
  }
  isExpanded.value = expandedRowKeys.value.length > 0
}

// 切换展开/收缩
const toggleExpand = () => {
  isExpanded.value = !isExpanded.value
  if (isExpanded.value) {
    // 只展开第一层
    expandedRowKeys.value = list.value.map(item => item.menuId)
  } else {
    expandedRowKeys.value = []
  }
}

// 处理懒加载数据
const handleLoadData = async (record: TaktMenu) => {
  try {
    loading.value = true
    const res = await getMenuTree({
      ...queryParams.value,
      parentId: record.menuId
    } as any)
    if (res.data) {
      // 更新子节点数据
      record.children = res.data
    } else {
      message.error(t('common.failed'))
    }
  } catch (error: any) {
    console.error('[菜单管理] 加载子节点数据出错:', error)
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

// ==================== 其他功能方法 ====================
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
  getList()
})
</script>

<style lang="less" scoped>
.menu-container {
  height: 100%;
  padding: 16px;
  background-color: #f0f2f5;
  display: flex;
  flex-direction: column;

  :deep(.ant-table-wrapper) {
    flex: 1;
    margin-top: 16px;
    background-color: var(--ant-color-bg-container);

    .ant-spin-nested-loading {
      height: 100%;

      .ant-spin-container {
        height: 100%;
        display: flex;
        flex-direction: column;

        .ant-table {
          flex: 1;
          overflow: hidden;

          .ant-table-container {
            height: 100%;
          }
        }
      }
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

