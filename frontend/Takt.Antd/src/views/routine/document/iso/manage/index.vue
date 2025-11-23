<template>
  <div class="file-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['routine:file:create']"
      :show-edit="true"
      :edit-permission="['routine:file:update']"
      :show-delete="true"
      :delete-permission="['routine:file:delete']"
      :show-export="true"
      :export-permission="['routine:file:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDeleteConfirm"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="fileList"
      :columns="columns.filter((col: any) => columnSettings[col.key])"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: TaktFile) => String(record.fileId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 文件大小列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'fileSize'">
          {{ formatFileSize(record.fileSize) }}
        </template>
        <!-- 文件访问URL列 -->
        <template v-if="column.dataIndex === 'fileAccessUrl'">
          <a v-if="record.fileAccessUrl" :href="record.fileAccessUrl" target="_blank" rel="noopener noreferrer">
            {{ record.fileAccessUrl }}
          </a>
          <span v-else>-</span>
        </template>
        <!-- 创建时间列 -->
        <template v-if="column.dataIndex === 'createTime'">
          {{ formatDateTime(record.createTime) }}
        </template>
        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-view="true"
            :view-permission="['routine:file:query']"
            :show-edit="true"
            :edit-permission="['routine:file:update']"
            :show-delete="true"
            :delete-permission="['routine:file:delete']"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDeleteConfirm"
          />
        </template>
      </template>
    </Takt-table>

    <!-- 文件表单对话框 -->
    <file-form
      v-model:visible="formVisible"
      :title="formTitle"
      :file-id="selectedFileId"
      @success="handleSuccess"
    />

    <!-- 文件详情对话框 -->
    <file-detail
      v-model:open="detailVisible"
      :file-id="selectedFileId"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :open="columnSettingVisible"
      :title="t('common.columnSetting')"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getFileList, deleteFile, downloadFile, uploadFile, exportFileList, batchDeleteFile } from '@/api/routine/document/file'
import type { TaktFile, TaktFileQuery } from '@/types/routine/document/file'
import type { QueryField } from '@/types/components/query'
import FileForm from './components/FileForm.vue'
import FileDetail from './components/FileDetail.vue'
import { formatDateTime } from '@/utils/format'

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const showSearch = ref(true)
const fileList = ref<TaktFile[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedFileId = ref<number>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const columnSettingVisible = ref(false)
const total = ref(0)

// === 查询参数 ===
const queryParams = ref<TaktFileQuery>({
  pageIndex: 1,
  pageSize: 10,
  fileName: '',
  fileType: '',
  fileStatus: -1,
  startTime: undefined,
  endTime: undefined
})

// === 查询字段定义 ===
const queryFields = computed<QueryField[]>(() => [
  {
    name: 'fileName',
    label: t('routine.file.fileName'),
    type: 'input',
    placeholder: t('routine.file.placeholder.fileName')
  },
  {
    name: 'fileType',
    label: t('routine.file.fileType'),
    type: 'input',
    placeholder: t('routine.file.placeholder.fileType')
  },
  {
    name: 'fileStatus',
    label: t('routine.file.status'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    },
    placeholder: t('routine.file.placeholder.status')
  }
])

// === 表格列定义 ===
const defaultColumns = [
  {
    title: t('routine.file.fileId'),
    dataIndex: 'fileId',
    key: 'fileId',
    width: 100
  },
  {
    title: t('routine.file.fileOriginalName'),
    dataIndex: 'fileOriginalName',
    key: 'fileOriginalName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.file.fileExtension'),
    dataIndex: 'fileExtension',
    key: 'fileExtension',
    width: 100
  },
  {
    title: t('routine.file.fileName'),
    dataIndex: 'fileName',
    key: 'fileName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.file.filePath'),
    dataIndex: 'filePath',
    key: 'filePath',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.file.fileType'),
    dataIndex: 'fileType',
    key: 'fileType',
    width: 120
  },
  {
    title: t('routine.file.fileSize'),
    dataIndex: 'fileSize',
    key: 'fileSize',
    width: 120,
    customRender: ({ text }: { text: number }) => formatFileSize(text)
  },
  {
    title: t('routine.file.fileStorageType'),
    dataIndex: 'fileStorageType',
    key: 'fileStorageType',
    width: 120
  },
  {
    title: t('routine.file.fileStorageLocation'),
    dataIndex: 'fileStorageLocation',
    key: 'fileStorageLocation',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.file.fileAccessUrl'),
    dataIndex: 'fileAccessUrl',
    key: 'fileAccessUrl',
    width: 200,
    ellipsis: true,
    customRender: ({ text }: { text: string }) => {
      if (!text) return '-'
      return h('a', {
        href: text,
        target: '_blank',
        rel: 'noopener noreferrer'
      }, text)
    }
  },
  {
    title: t('routine.file.fileMd5'),
    dataIndex: 'fileMd5',
    key: 'fileMd5',
    width: 200,
    ellipsis: true
  },
  {
    title: t('routine.file.fileStatus'),
    dataIndex: 'fileStatus',
    key: 'fileStatus',
    width: 100,
    customRender: ({ text }: { text: number }) => text === 1 ? t('common.status.normal') : t('common.status.disable')
  },
  {
    title: t('routine.file.fileDownloadCount'),
    dataIndex: 'fileDownloadCount',
    key: 'fileDownloadCount',
    width: 120
  },
  {
    title: t('common.table.header.operation'),
    key: 'action',
    width: 180,
    fixed: 'right'
  }
]

// 列设置
const columnSettings = ref<Record<string, boolean>>({})
const columns = computed(() => {
  return defaultColumns.filter(col => columnSettings.value[col.key])
})

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    console.log('查询参数:', queryParams.value)
    const res = await getFileList(queryParams.value)
    console.log('API响应:', res)
    if (res.data.code === 200) {
      fileList.value = res.data.data.rows
      total.value = res.data.data.totalNum
      console.log('处理后的数据:', fileList.value)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载文件列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleSearch = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    fileName: '',
    fileType: '',
    fileStatus: -1,
    startTime: undefined,
    endTime: undefined
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  if (pag.current) {
    queryParams.value.pageIndex = pag.current
  }
  if (pag.pageSize) {
    queryParams.value.pageSize = pag.pageSize
  }
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktFile) => {
  selectedFileId.value = record.fileId
}

// 格式化文件大小
const formatFileSize = (size: number) => {
  if (size < 1024) {
    return size + ' B'
  } else if (size < 1024 * 1024) {
    return (size / 1024).toFixed(2) + ' KB'
  } else if (size < 1024 * 1024 * 1024) {
    return (size / (1024 * 1024)).toFixed(2) + ' MB'
  } else {
    return (size / (1024 * 1024 * 1024)).toFixed(2) + ' GB'
  }
}

// 下载文件
const handleDownload = async (id: number) => {
  try {
    const response = await downloadFile(id)
    const blob = new Blob([response.data], { type: response.headers['content-type'] })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    const file = fileList.value.find(f => f.fileId === id)
    link.download = file?.fileOriginalName || 'file'
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('下载文件失败:', error)
    message.error(t('common.failed'))
  }
}

// 删除文件
const handleDelete = async (record: TaktFile) => {
  try {
    const res = await deleteFile(record.fileId)
    if (res.data.code === 200) {
      message.success(t('common.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('删除文件失败:', error)
    message.error(t('common.failed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.message.selectRecords'))
    return
  }
  try {
    const res = await batchDeleteFile(selectedRowKeys.value.map(id => Number(id)))
    if (res.data.code === 200) {
      message.success(t('common.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('批量删除文件失败:', error)
    message.error(t('common.failed'))
  }
}

// 删除确认
const handleDeleteConfirm = (record: TaktFile) => {
  Modal.confirm({
    title: t('common.message.confirmDelete'),
    content: t('common.message.deleteConfirm', { name: record.fileOriginalName }),
    okText: t('common.ok'),
    cancelText: t('common.cancel'),
    onOk: () => handleDelete(record)
  })
}

// 批量删除确认
const handleBatchDeleteConfirm = () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.message.selectRecords'))
    return
  }
  Modal.confirm({
    title: t('common.message.confirmDelete'),
    content: t('common.message.batchDeleteConfirm', { count: selectedRowKeys.value.length }),
    okText: t('common.ok'),
    cancelText: t('common.cancel'),
    onOk: handleBatchDelete
  })
}

// 新增
const handleAdd = () => {
  selectedFileId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = fileList.value.find(item => String(item.fileId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: TaktFile) => {
  selectedFileId.value = record.fileId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 查看
const handleView = (record: TaktFile) => {
  selectedFileId.value = record.fileId
  detailVisible.value = true
}

// 导出
const handleExport = async () => {
  try {
    loading.value = true
    const response = await exportFileList(queryParams.value)
    const blob = new Blob([response.data], { type: response.headers['content-type'] })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `文件列表_${formatDateTime(new Date(), 'yyyyMMddHHmmss')}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.message.exportSuccess'))
  } catch (error) {
    console.error('导出文件失败:', error)
    message.error(t('common.message.exportFailed'))
  } finally {
    loading.value = false
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedValues.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('fileColumnSettings', JSON.stringify(settings))
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  selectedFileId.value = undefined
  selectedRowKeys.value = []
  fetchData()
}

// === 生命周期 ===
onMounted(() => {
  // 初始化列设置
  defaultColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })
  // 加载数据
  fetchData()
})
</script>

<style lang="less" scoped>
.file-container {
  padding: 16px;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 4px 0;
}
</style>

