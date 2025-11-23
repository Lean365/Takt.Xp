<template>
  <div class="news-container">
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
      :add-permission="['routine:news:hot:create']"
      :show-edit="true"
      :edit-permission="['routine:news:hot:update']"
      :show-delete="true"
      :delete-permission="['routine:news:hot:delete']"
      :show-import="true"
      :import-permission="['routine:news:hot:import']"
      :show-export="true"
      :export-permission="['routine:news:hot:export']"
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
      :columns="columns.filter((col: TableColumn) => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="newsId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 新闻封面列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'newsCover'">
          <a-image
            v-if="record.newsCover"
            :src="record.newsCover"
            :width="60"
            :height="40"
            :preview="{
              src: record.newsCover
            }"
            style="object-fit: cover; border-radius: 4px"
          />
          <span v-else class="text-gray-400">无封面</span>
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <Takt-dict-tag dict-type="news_status" :value="record.status" />
        </template>

        <!-- 置顶状态列 -->
        <template v-if="column.dataIndex === 'isTop'">
          <a-tag :color="record.isTop === 1 ? 'red' : 'default'">
            {{ record.isTop === 1 ? '置顶' : '普通' }}
          </a-tag>
        </template>

        <!-- 推荐状态列 -->
        <template v-if="column.dataIndex === 'isRecommend'">
          <a-tag :color="record.isRecommend === 1 ? 'blue' : 'default'">
            {{ record.isRecommend === 1 ? '推荐' : '普通' }}
          </a-tag>
        </template>

        <!-- 热门状态列 -->
        <template v-if="column.dataIndex === 'isHot'">
          <a-tag :color="record.isHot === 1 ? 'orange' : 'default'">
            {{ record.isHot === 1 ? '热门' : '普通' }}
          </a-tag>
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:news:hot:update']"
            :show-delete="true"
            :delete-permission="['routine:news:hot:delete']"
            :show-view="true"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @view="handleView"
          >
            <template #extra>
              <a-dropdown>
                <a-button>
                  {{ t('common.actions.more') }}
                  <DownOutlined />
                </a-button>
                <template #overlay>
                  <a-menu>
                    <a-tooltip title="查看评论">
                      <a-button
                        v-hasPermi="['routine:news:comment:list']"
                        type="default"
                        size="small"
                        class="Takt-btn-comment"
                        @click.stop="handleViewComments(record)"
                      >
                        <template #icon><message-outlined /></template>
                      </a-button>
                    </a-tooltip>
                    <a-tooltip title="查看点赞">
                      <a-button
                        v-hasPermi="['routine:news:like:list']"
                        type="default"
                        size="small"
                        class="Takt-btn-like"
                        @click.stop="handleViewLikes(record)"
                      >
                        <template #icon><heart-outlined /></template>
                      </a-button>
                    </a-tooltip>
                    <a-tooltip title="话题管理">
                      <a-button
                        v-hasPermi="['routine:news:topic:list']"
                        type="default"
                        size="small"
                        class="Takt-btn-topic"
                        @click.stop="handleManageTopics(record)"
                      >
                        <template #icon><tag-outlined /></template>
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

    <!-- 新闻表单对话框 -->
    <news-form
      v-model:open="newsFormVisible"
      :title="formTitle"
      :news-id="selectedNewsId"
      @success="handleSuccess"
    />

    <!-- 评论管理对话框 -->
    <comment-manage
      v-if="commentDialogVisible && selectedNewsId !== undefined"
      v-model:open="commentDialogVisible"
      :news-id="selectedNewsId"
      @success="handleSuccess"
    />

    <!-- 点赞管理对话框 -->
    <like-manage
      v-if="likeDialogVisible && selectedNewsId !== undefined"
      v-model:open="likeDialogVisible"
      :news-id="selectedNewsId"
      @success="handleSuccess"
    />

    <!-- 话题管理对话框 -->
    <topic-manage
      v-if="topicDialogVisible && selectedNewsId !== undefined"
      v-model:open="topicDialogVisible"
      :news-id="selectedNewsId"
      @success="handleSuccess"
    />

    <!-- 导入对话框 -->
    <Takt-import-dialog
      v-model:open="importVisible"
      :upload-method="handleImportUpload"
      :template-method="handleDownloadTemplate"
      template-file-name="新闻导入模板.xlsx"
      :tips="'请确保Excel文件包含必要的新闻信息字段,\n如新闻标题,新闻内容,新闻分类,作者,发布部门等信息'"
      @success="handleImportSuccess"
      :show-template="true"
      :template-permission="['routine:news:template']"
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
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h, nextTick, watch } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dictStore'
import type { TaktNews, TaktNewsQuery } from '@/types/routine/news/news'
import type { QueryField } from '@/types/components/query'
import { 
  DownOutlined, 
  MessageOutlined, 
  HeartOutlined,
  TagOutlined
} from '@ant-design/icons-vue'
import {
  getNewsList,
  deleteNews,
  exportNews,
  importNews,
  getNewsTemplate,
  getNewsById,
  createNews,
  updateNews,
  updateNewsStatus,
  batchDeleteNews
} from '@/api/routine/news/news'
import NewsForm from './components/NewsForm.vue'
import CommentManage from './components/CommentManage.vue'
import LikeManage from './components/LikeManage.vue'
import TopicManage from './components/TopicManage.vue'

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
    dataIndex: 'newsId',
    key: 'newsId',
    width: 80,
    ellipsis: true
  },
  {
    title: '新闻封面',
    dataIndex: 'newsCover',
    key: 'newsCover',
    width: 100
  },
  {
    title: '新闻标题',
    dataIndex: 'newsTitle',
    key: 'newsTitle',
    width: 200,
    ellipsis: true
  },
  {
    title: '新闻副标题',
    dataIndex: 'newsSubtitle',
    key: 'newsSubtitle',
    width: 150,
    ellipsis: true
  },
  {
    title: '新闻分类',
    dataIndex: 'newsCategory',
    key: 'newsCategory',
    width: 120,
    ellipsis: true
  },
  {
    title: '作者',
    dataIndex: 'authorName',
    key: 'authorName',
    width: 120,
    ellipsis: true
  },
  {
    title: '发布部门',
    dataIndex: 'publishDepartment',
    key: 'publishDepartment',
    width: 120,
    ellipsis: true
  },
  {
    title: '发布时间',
    dataIndex: 'publishTime',
    key: 'publishTime',
    width: 180
  },
  {
    title: '阅读数',
    dataIndex: 'readCount',
    key: 'readCount',
    width: 100
  },
  {
    title: '点赞数',
    dataIndex: 'likeCount',
    key: 'likeCount',
    width: 100
  },
  {
    title: '评论数',
    dataIndex: 'commentCount',
    key: 'commentCount',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '置顶',
    dataIndex: 'isTop',
    key: 'isTop',
    width: 80
  },
  {
    title: '推荐',
    dataIndex: 'isRecommend',
    key: 'isRecommend',
    width: 80
  },
  {
    title: '热门',
    dataIndex: 'isHot',
    key: 'isHot',
    width: 80
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: '创建人',
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: '更新人',
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: '更新时间',
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<TaktNewsQuery>({
  pageIndex: 1,
  pageSize: 10,
  newsTitle: '',
  newsContent: '',
  newsCategory: '',
  newsTags: '',
  newsKeywords: '',
  status: undefined,
  isTop: undefined,
  isRecommend: undefined,
  isHot: undefined,
  authorId: undefined,
  authorName: '',
  publishDepartment: '',
  startTime: undefined,
  endTime: undefined
})

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'newsTitle',
    label: '新闻标题',
    type: 'input' as const
  },
  {
    name: 'newsCategory',
    label: '新闻分类',
    type: 'input' as const
  },
  {
    name: 'authorName',
    label: '作者',
    type: 'input' as const
  },
  {
    name: 'publishDepartment',
    label: '发布部门',
    type: 'input' as const
  },
  {
    name: 'status',
    label: '状态',
    type: 'select' as const,
    props: {
      dictType: 'news_status',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'isTop',
    label: '置顶状态',
    type: 'select' as const,
    props: {
      options: [
        { label: '全部', value: undefined },
        { label: '置顶', value: 1 },
        { label: '普通', value: 0 }
      ]
    }
  },
  {
    name: 'isRecommend',
    label: '推荐状态',
    type: 'select' as const,
    props: {
      options: [
        { label: '全部', value: undefined },
        { label: '推荐', value: 1 },
        { label: '普通', value: 0 }
      ]
    }
  },
  {
    name: 'isHot',
    label: '热门状态',
    type: 'select' as const,
    props: {
      options: [
        { label: '全部', value: undefined },
        { label: '热门', value: 1 },
        { label: '普通', value: 0 }
      ]
    }
  },
  {
    name: 'publishTime',
    label: '发布时间',
    type: 'dateRange' as const
  }
]

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<TaktNews[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const newsFormVisible = ref(false)
const formTitle = ref('')
const selectedNewsId = ref<number>()

// 评论管理弹窗
const commentDialogVisible = ref(false)

// 点赞管理弹窗
const likeDialogVisible = ref(false)

// 话题管理弹窗
const topicDialogVisible = ref(false)

// 导入对话框
const importVisible = ref(false)

// 列设置
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({
  newsId: true,
  newsCover: true,
  newsTitle: true,
  newsSubtitle: true,
  newsCategory: true,
  authorName: true,
  publishDepartment: true,
  publishTime: true,
  readCount: true,
  likeCount: true,
  commentCount: true,
  status: true,
  isTop: true,
  isRecommend: true,
  isHot: true,
  remark: true,
  createBy: true,
  createTime: true,
  updateBy: true,
  updateTime: true,
  action: true
})

// 默认列设置
const defaultColumns = computed(() => columns)

// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    const res = await getNewsList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.total
    } else {
      message.error(res.data.msg || '获取新闻列表失败')
    }
  } catch (error) {
    console.error('[新闻管理] 获取数据出错:', error)
    message.error('获取新闻列表失败')
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  Object.assign(queryParams.value, {
    pageIndex: 1,
    pageSize: 10,
    newsTitle: '',
    newsContent: '',
    newsCategory: '',
    newsTags: '',
    newsKeywords: '',
    status: undefined,
    isTop: undefined,
    isRecommend: undefined,
    isHot: undefined,
    authorId: undefined,
    authorName: '',
    publishDepartment: '',
    startTime: undefined,
    endTime: undefined
  })
  fetchData()
}

// 切换搜索显示
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏
const toggleFullscreen = () => {
  // 实现全屏切换逻辑
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktNews) => {
  selectedRowKeys.value = [record.newsId]
}

// 分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 页大小变化
const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  queryParams.value.pageIndex = 1
  fetchData()
}

// 新增
const handleAdd = () => {
  formTitle.value = '新增新闻'
  selectedNewsId.value = undefined
  newsFormVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length === 1) {
    const record = tableData.value.find(item => item.newsId === selectedRowKeys.value[0])
    if (record) {
      handleEdit(record)
    }
  }
}

// 编辑
const handleEdit = (record: TaktNews) => {
  formTitle.value = '编辑新闻'
  selectedNewsId.value = record.newsId
  newsFormVisible.value = true
}

// 查看
const handleView = (record: TaktNews) => {
  formTitle.value = '查看新闻'
  selectedNewsId.value = record.newsId
  newsFormVisible.value = true
}

// 删除
const handleDelete = async (record: TaktNews) => {
  try {
    const res = await deleteNews(record.newsId)
    if (res.data.code === 200) {
      message.success('删除成功')
      fetchData()
    } else {
      message.error(res.data.msg || '删除失败')
    }
  } catch (error) {
    console.error('[新闻管理] 删除出错:', error)
    message.error('删除失败')
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要删除的新闻')
    return
  }
  
  try {
    const res = await batchDeleteNews(selectedRowKeys.value as number[])
    if (res.data.code === 200) {
      message.success('批量删除成功')
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.data.msg || '批量删除失败')
    }
  } catch (error) {
    console.error('[新闻管理] 批量删除出错:', error)
    message.error('批量删除失败')
  }
}

// 查看评论
const handleViewComments = (record: TaktNews) => {
  selectedNewsId.value = record.newsId
  commentDialogVisible.value = true
}

// 查看点赞
const handleViewLikes = (record: TaktNews) => {
  selectedNewsId.value = record.newsId
  likeDialogVisible.value = true
}

// 管理话题
const handleManageTopics = (record: TaktNews) => {
  selectedNewsId.value = record.newsId
  topicDialogVisible.value = true
}

// 导入
const handleImport = () => {
  importVisible.value = true
}

// 导入上传
const handleImportUpload = async (file: File) => {
  try {
    const res = await importNews(file)
    return res.data
  } catch (error) {
    console.error('[新闻管理] 导入出错:', error)
    throw error
  }
}

// 导入成功
const handleImportSuccess = () => {
  message.success('导入成功')
  fetchData()
}

// 导出
const handleExport = async () => {
  try {
    const res = await exportNews(queryParams.value)
    const blob = new Blob([res.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `新闻数据_${new Date().toISOString().slice(0, 10)}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('[新闻管理] 导出出错:', error)
    message.error('导出失败')
  }
}

// 下载模板
const handleDownloadTemplate = async () => {
  try {
    const res = await getNewsTemplate()
    const blob = new Blob([res.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = '新闻导入模板.xlsx'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error('[新闻管理] 下载模板出错:', error)
    message.error('下载模板失败')
  }
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: string[]) => {
  const newSettings: Record<string, boolean> = {}
  defaultColumns.value.forEach(col => {
    newSettings[col.key] = checkedValues.includes(col.key)
  })
  columnSettings.value = newSettings
}

// 表单提交成功
const handleSuccess = () => {
  newsFormVisible.value = false
  fetchData()
}

// 初始化
onMounted(() => {
  fetchData()
})
</script>

<style scoped>
.news-container {
  padding: 16px;
}

.text-gray-400 {
  color: #9ca3af;
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

.Takt-btn-comment,
.Takt-btn-like,
.Takt-btn-topic {
  margin-left: 4px;
}
</style> 
