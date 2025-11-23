<template>
  <div class="topic-participant-container">
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
      :add-permission="['routine:topic-participant:create']"
      :show-edit="true"
      :edit-permission="['routine:topic-participant:update']"
      :show-delete="true"
      :delete-permission="['routine:topic-participant:delete']"
      :show-import="true"
      :import-permission="['routine:topic-participant:import']"
      :show-export="true"
      :export-permission="['routine:topic-participant:export']"
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
      row-key="id"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 参与类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'participationType'">
          <Takt-dict-tag dict-type="topic_participation_type" :value="record.participationType" />
        </template>

        <!-- 参与状态列 -->
        <template v-if="column.dataIndex === 'participationStatus'">
          <Takt-dict-tag dict-type="topic_participation_status" :value="record.participationStatus" />
        </template>

        <!-- 贡献分数列 -->
        <template v-if="column.dataIndex === 'contributionScore'">
          <a-badge :count="record.contributionScore" :number-style="{ backgroundColor: '#52c41a' }" />
        </template>

        <!-- 最后活跃时间列 -->
        <template v-if="column.dataIndex === 'lastActiveTime'">
          {{ record.lastActiveTime ? formatDateTime(record.lastActiveTime) : '-' }}
        </template>

        <!-- 参与时间列 -->
        <template v-if="column.dataIndex === 'participationTime'">
          {{ record.participationTime ? formatDateTime(record.participationTime) : '-' }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:topic-participant:update']"
            :show-delete="true"
            :delete-permission="['routine:topic-participant:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          />
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

    <!-- 话题参与者表单对话框 -->
    <topic-participant-form
      v-model:visible="topicParticipantFormVisible"
      :title="formTitle"
      :participant-id="selectedParticipantId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="话题参与者导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的话题参与者信息字段,\n如话题ID,用户ID,用户名,参与类型,参与状态等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:topic-participant:template']"
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
        <div v-for="(col, index) in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h, nextTick, watch } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktNewsTopicParticipant, TaktNewsTopicParticipantQuery } from '@/types/routine/news/topic-participant'
import type { QueryField } from '@/types/components/query'
import {
  getNewsTopicParticipantList,
  deleteNewsTopicParticipant,
  batchDeleteNewsTopicParticipant,
  getNewsTopicParticipantById,
  createNewsTopicParticipant,
  updateNewsTopicParticipant
} from '@/api/routine/news/topic-participant'
import { formatDateTime } from '@/utils/format'
import TopicParticipantForm from './components/TopicParticipantForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询字段类型定义
type FieldType =
  | 'input'
  | 'select'
  | 'date'
  | 'dateRange'
  | 'number'
  | 'radio'
  | 'checkbox'
  | 'cascader'

// 表格列定义
interface TableColumn {
  title: string
  dataIndex?: string
  key: string
  width?: number
  ellipsis?: boolean
  fixed?: string
}

const columns: TableColumn[] = [
  {
    title: 'ID',
    dataIndex: 'id',
    key: 'id',
    width: 80,
    ellipsis: true
  },
  {
    title: '话题ID',
    dataIndex: 'topicId',
    key: 'topicId',
    width: 100,
    ellipsis: true
  },
  {
    title: '用户ID',
    dataIndex: 'userId',
    key: 'userId',
    width: 100,
    ellipsis: true
  },
  {
    title: '用户名',
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    ellipsis: true
  },
  {
    title: '用户头像',
    dataIndex: 'userAvatar',
    key: 'userAvatar',
    width: 100,
    ellipsis: true
  },
  {
    title: '参与类型',
    dataIndex: 'participationType',
    key: 'participationType',
    width: 100
  },
  {
    title: '参与状态',
    dataIndex: 'participationStatus',
    key: 'participationStatus',
    width: 100
  },
  {
    title: '参与时间',
    dataIndex: 'participationTime',
    key: 'participationTime',
    width: 180
  },
  {
    title: '最后活跃时间',
    dataIndex: 'lastActiveTime',
    key: 'lastActiveTime',
    width: 180
  },
  {
    title: '贡献分数',
    dataIndex: 'contributionScore',
    key: 'contributionScore',
    width: 100
  },
  {
    title: '内容数量',
    dataIndex: 'contentCount',
    key: 'contentCount',
    width: 100
  },
  {
    title: '评论数量',
    dataIndex: 'commentCount',
    key: 'commentCount',
    width: 100
  },
  {
    title: '点赞数量',
    dataIndex: 'likeCount',
    key: 'likeCount',
    width: 100
  },
  {
    title: '分享数量',
    dataIndex: 'shareCount',
    key: 'shareCount',
    width: 100
  },
  {
    title: '接收通知',
    dataIndex: 'receiveNotification',
    key: 'receiveNotification',
    width: 100
  },
  {
    title: '通知类型',
    dataIndex: 'notificationType',
    key: 'notificationType',
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
    title: t('table.columns.action'),
    key: 'action',
    width: 120,
    fixed: 'right'
  }
]

// 查询字段配置
const queryFields: QueryField[] = [
  {
    name: 'topicId',
    label: '话题ID',
    type: 'number',
    placeholder: '请输入话题ID'
  },
  {
    name: 'userId',
    label: '用户ID',
    type: 'number',
    placeholder: '请输入用户ID'
  },
  {
    name: 'userName',
    label: '用户名',
    type: 'input',
    placeholder: '请输入用户名'
  },
  {
    name: 'participationType',
    label: '参与类型',
    type: 'select',
    placeholder: '请选择参与类型',
    options: [
      { label: '创建者', value: 1 },
      { label: '管理员', value: 2 },
      { label: '普通成员', value: 3 },
      { label: '访客', value: 4 }
    ]
  },
  {
    name: 'participationStatus',
    label: '参与状态',
    type: 'select',
    placeholder: '请选择参与状态',
    options: [
      { label: '活跃', value: 1 },
      { label: '非活跃', value: 2 },
      { label: '被禁言', value: 3 }
    ]
  }
]

// 响应式数据
const loading = ref(false)
const tableData = ref<TaktNewsTopicParticipant[]>([])
const total = ref(0)
const selectedRowKeys = ref<(string | number)[]>([])
const selectedParticipantId = ref<number>()
const topicParticipantFormVisible = ref(false)
const formTitle = ref('')
const importVisible = ref(false)
const columnSettingVisible = ref(false)
const showSearch = ref(true)

// 查询参数
const queryParams = ref<TaktNewsTopicParticipantQuery>({
  pageIndex: 1,
  pageSize: 10
})

// 列设置
const defaultColumns = computed(() => columns)
const columnSettings = ref<Record<string, boolean>>({
  id: true,
  topicId: true,
  userId: true,
  userName: true,
  userAvatar: false,
  participationType: true,
  participationStatus: true,
  participationTime: true,
  lastActiveTime: true,
  contributionScore: true,
  contentCount: false,
  commentCount: false,
  likeCount: false,
  shareCount: false,
  receiveNotification: false,
  notificationType: false,
  remark: true,
  createBy: true,
  createTime: true,
  updateBy: false,
  updateTime: false,
  action: true
})

// 获取数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getNewsTopicParticipantList(queryParams.value)
    if (res.data?.data) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    }
  } catch (error) {
    message.error('获取数据失败')
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = (params: TaktNewsTopicParticipantQuery) => {
  queryParams.value = { ...queryParams.value, ...params, pageIndex: 1 }
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10
  }
  fetchData()
}

// 分页变化
const handlePageChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

// 页面大小变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = 1
  queryParams.value.pageSize = size
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktNewsTopicParticipant) => {
  selectedParticipantId.value = record.id
}

// 新增
const handleAdd = () => {
  formTitle.value = '新增话题参与者'
  selectedParticipantId.value = undefined
  topicParticipantFormVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => item.id === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}

// 编辑
const handleEdit = (record: TaktNewsTopicParticipant) => {
  formTitle.value = '编辑话题参与者'
  selectedParticipantId.value = record.id
  topicParticipantFormVisible.value = true
}

// 删除
const handleDelete = async (record: TaktNewsTopicParticipant) => {
  try {
    await deleteNewsTopicParticipant(record.id)
    message.success('删除成功')
    fetchData()
  } catch (error) {
    message.error('删除失败')
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的数据')
    return
  }
  
  try {
    await batchDeleteNewsTopicParticipant(selectedRowKeys.value as number[])
    message.success('批量删除成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    message.error('批量删除失败')
  }
}

// 表单成功回调
const handleSuccess = () => {
  topicParticipantFormVisible.value = false
  fetchData()
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入上传
const handleImportUpload = async (file: File) => {
  // 这里需要实现导入逻辑
  return true
}

// 导入成功
const handleImportSuccess = () => {
  importVisible.value = false
  fetchData()
}

// 下载模板
const handleDownloadTemplate = async () => {
  // 这里需要实现下载模板逻辑
  message.info('下载模板功能待实现')
}

// 导出
const handleExport = async () => {
  // 这里需要实现导出逻辑
  message.info('导出功能待实现')
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: string[]) => {
  const newSettings: Record<string, boolean> = {}
  Object.keys(columnSettings.value).forEach(key => {
    newSettings[key] = checkedValues.includes(key)
  })
  columnSettings.value = newSettings
}

// 切换搜索
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // 实现全屏切换逻辑
}

// 初始化
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.topic-participant-container {
  padding: 16px;
  background: #f0f2f5;
  min-height: 100vh;
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
