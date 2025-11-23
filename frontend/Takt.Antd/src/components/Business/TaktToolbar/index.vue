<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: Toolbar/index.vue
创建日期: 2024-03-20
描述: 统一封装的工具栏组件，提供常用的操作按钮和工具按钮
=================================================================== 
-->

<template>
  <div class="Takt-toolbar">
    <!-- 左侧按钮组 -->
    <div class="toolbar-left">
      <a-space :size="4">
        <div v-if="showAdd" v-hasPermi="addPermission">
          <a-tooltip :title="t('common.actions.add')">
            <a-button @click="handleAdd" class="Takt-btn-add">
              <template #icon><plus-outlined /></template>
              {{ t('common.actions.add') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showEdit" v-hasPermi="editPermission">
          <a-tooltip :title="t('common.actions.edit')">
            <a-button 
              @click="handleEdit" 
              class="Takt-btn-edit"
              :disabled="disabledEdit"
            >
              <template #icon><edit-outlined /></template>
              {{ t('common.actions.edit') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showDelete" v-hasPermi="deletePermission">
          <a-tooltip :title="t('common.actions.delete')">
            <a-button 
              @click="handleDelete" 
              class="Takt-btn-delete"
              :disabled="disabledDelete"
            >
              <template #icon><delete-outlined /></template>
              {{ t('common.actions.delete') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showImport" v-hasPermi="importPermission">
          <a-tooltip :title="t('common.actions.import')">
            <a-button @click="handleImport" class="Takt-btn-import">
              <template #icon><import-outlined /></template>
              {{ t('common.actions.import') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showExport" v-hasPermi="exportPermission">
          <a-tooltip :title="t('common.actions.export')">
            <a-button @click="handleExport" class="Takt-btn-export">
              <template #icon><export-outlined /></template>
              {{ t('common.actions.export') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showAudit" v-hasPermi="auditPermission">
          <a-tooltip :title="t('common.actions.audit')">
            <a-button @click="handleAudit" class="Takt-btn-audit">
              <template #icon><audit-outlined /></template>
              {{ t('common.actions.audit') }}
            </a-button>
          </a-tooltip>
        </div>
        <div v-if="showRevoke" v-hasPermi="revokePermission">
          <a-tooltip :title="t('common.actions.revoke')">
            <a-button @click="handleRevoke" class="Takt-btn-revoke">
              <template #icon><rollback-outlined /></template>
              {{ t('common.actions.revoke') }}
            </a-button>
          </a-tooltip>
        </div>
        <!-- 自定义按钮插槽 -->
        <slot name="extra"></slot>
      </a-space>
    </div>

    <!-- 右侧按钮组 -->
    <div class="toolbar-right">
      <a-space :size="4">
        <a-tooltip :title="t('common.actions.refresh')">
          <a-button @click="handleRefresh" class="Takt-btn-refresh">
            <template #icon><reload-outlined /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip :title="t('table.config.columnSetting')">
          <a-button @click="handleColumnSetting" class="Takt-btn-column-setting">
            <template #icon><setting-outlined /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip :title="t('common.actions.search')">
          <a-button @click="toggleSearch" class="Takt-btn-search">
            <template #icon><search-outlined /></template>
          </a-button>
        </a-tooltip>
        <a-tooltip :title="isFullscreen ? t('header.fullscreen.exit') : t('header.fullscreen.enter')">
          <a-button @click="toggleFullscreen" class="Takt-btn-fullscreen">
            <template #icon>
              <fullscreen-outlined v-if="!isFullscreen" />
              <fullscreen-exit-outlined v-else />
            </template>
          </a-button>
        </a-tooltip>
        <!-- 自定义工具插槽 -->
        <slot v-if="false" name="buttons"></slot>
      </a-space>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  ImportOutlined,
  ExportOutlined,
  AuditOutlined,
  RollbackOutlined,
  ReloadOutlined,
  SettingOutlined,
  SearchOutlined,
  FullscreenOutlined,
  FullscreenExitOutlined
} from '@ant-design/icons-vue'
import { useUserStore } from '@/stores/userStore'

const { t } = useI18n()

const userStore = useUserStore()

// === 类型定义 ===
interface Props {
  showAdd?: boolean // 是否显示新增按钮
  addPermission?: string[] // 新增按钮权限
  showEdit?: boolean // 是否显示编辑按钮
  editPermission?: string[] // 编辑按钮权限
  showDelete?: boolean // 是否显示删除按钮
  deletePermission?: string[] // 删除按钮权限
  showImport?: boolean // 是否显示导入按钮
  importPermission?: string[] // 导入按钮权限
  showExport?: boolean // 是否显示导出按钮
  exportPermission?: string[] // 导出按钮权限
  showAudit?: boolean // 是否显示审核按钮
  auditPermission?: string[] // 审核按钮权限
  showRevoke?: boolean // 是否显示撤销按钮
  revokePermission?: string[] // 撤销按钮权限
  disabledEdit?: boolean // 是否禁用编辑按钮
  disabledDelete?: boolean // 是否禁用删除按钮
  showSearch?: boolean // 是否显示搜索栏
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  showAdd: false,
  addPermission: () => [],
  showEdit: false,
  editPermission: () => [],
  showDelete: false,
  deletePermission: () => [],
  showImport: false,
  importPermission: () => [],
  showExport: false,
  exportPermission: () => [],
  showAudit: false,
  auditPermission: () => [],
  showRevoke: false,
  revokePermission: () => [],
  disabledEdit: false,
  disabledDelete: false,
  showSearch: true
})

// === 事件定义 ===
const emit = defineEmits([
  'add',
  'edit',
  'delete',
  'import',
  'export',
  'audit',
  'revoke',
  'refresh',
  'column-setting',
  'toggle-search',
  'toggle-fullscreen'
])

// === 状态管理 ===
const isFullscreen = ref(false)

// === 全屏相关 ===
const toggleFullscreen = async () => {
  try {
    if (!document.fullscreenElement) {
      await document.documentElement.requestFullscreen()
      isFullscreen.value = true
    } else {
      await document.exitFullscreen()
      isFullscreen.value = false
    }
    emit('toggle-fullscreen', isFullscreen.value)
  } catch (err) {
    console.error('全屏切换失败:', err)
  }
}

// 监听全屏变化
onMounted(() => {
  document.addEventListener('fullscreenchange', handleFullscreenChange)
  console.log('[工具栏] 组件挂载:', {
    用户权限: userStore.permissions,
    新增权限: props.addPermission,
    编辑权限: props.editPermission,
    删除权限: props.deletePermission
  })
})

onUnmounted(() => {
  document.removeEventListener('fullscreenchange', handleFullscreenChange)
})

const handleFullscreenChange = () => {
  isFullscreen.value = !!document.fullscreenElement
}

// === 搜索栏显示状态 ===
const searchVisible = ref(false)

const toggleSearch = () => {
  searchVisible.value = !searchVisible.value
  emit('toggle-search', searchVisible.value)
}

// === 事件处理 ===
const handleAdd = () => emit('add')
const handleEdit = () => emit('edit')
const handleDelete = () => emit('delete')
const handleImport = () => emit('import')
const handleExport = () => emit('export')
const handleAudit = () => emit('audit')
const handleRevoke = () => emit('revoke')
const handleRefresh = () => emit('refresh')
const handleColumnSetting = () => emit('column-setting')
</script>

<style lang="less">
@import '@/assets/styles/components/button.less';
@import '@/assets/styles/components/dropdown.less';

.Takt-toolbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 4px 6px;
  margin-bottom: 4px;
  //background-color: #fff;
  border-radius: 2px;

  // 左侧按钮组
  .toolbar-left {
    display: inline-flex;
    align-items: center;

    .ant-space {
      display: inline-flex;
      flex-wrap: nowrap;
      white-space: nowrap;
      gap: 4px;
    }
  }

  // 右侧按钮组
  .toolbar-right {
    margin-left: auto;

    .ant-space {
      display: inline-flex;
      flex-wrap: nowrap;
      white-space: nowrap;
      gap: 4px;
    }
  }

  // 修复按钮组最后一个按钮的右边框
  .ant-space .ant-btn:last-child {
    margin-right: 0;
  }
}
</style> 
