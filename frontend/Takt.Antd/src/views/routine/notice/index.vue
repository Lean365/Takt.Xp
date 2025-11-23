<template>
  <div class="notice-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item :label="t('notice.noticeTitle')">
          <a-input
            v-model:value="queryParams.noticeTitle"
            :placeholder="t('notice.placeholder.noticeTitle')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('notice.noticeType')">
          <Takt-select
            v-model:value="queryParams.noticeType"
            dict-type="notice_type"
            type="radio"
            :placeholder="t('notice.placeholder.noticeType')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('notice.noticeStatus')">
          <Takt-select
            v-model:value="queryParams.noticeStatus"
            dict-type="notice_status"
            type="radio"
            :placeholder="t('notice.placeholder.noticeStatus')"
            allow-clear
          />
        </a-form-item>
      </template>
    </Takt-query>

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['routine:notice:create']"
      :show-edit="true"
      :edit-permission="['routine:notice:update']"
      :show-delete="true"
      :delete-permission="['routine:notice:delete']"
      :show-export="true"
      :export-permission="['routine:notice:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="fetchData"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="dataList"
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: TaktNoticeDto) => String(record.noticeId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'noticeType'">
          <Takt-dict-tag dict-type="notice_type" :value="record.noticeType" />
        </template>
        <template v-else-if="column.dataIndex === 'noticeStatus'">
          <Takt-dict-tag dict-type="notice_status" :value="record.noticeStatus" />
        </template>
        <template v-else-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:notice:update']"
            :show-delete="true"
            :delete-permission="['routine:notice:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('notice.preview')">
                <a-button
                  type="link"
                  size="small"
                  @click.stop="handlePreview(record)"
                >
                  <template #icon><eye-outlined /></template>
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('common.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 通知表单对话框 -->
    <notice-form
      v-model:visible="formVisible"
      :title="formTitle"
      :notice-id="selectedNoticeId"
      @success="handleSuccess"
    />

    <!-- 预览对话框 -->
    <a-modal
      v-model:open="previewVisible"
      :title="t('notice.preview')"
      :footer="null"
      width="800px"
    >
      <template v-if="previewData">
        <h2>{{ previewData.noticeTitle }}</h2>
        <div class="notice-info">
          <span>{{ t('notice.field.noticeType') }}: <Takt-dict-tag dict-type="notice_type" :value="previewData.noticeType" /></span>
          <span>{{ t('notice.field.noticeSendTime') }}: {{ previewData.noticeSendTime }}</span>
        </div>
        <div class="notice-content" v-html="previewData.noticeContent"></div>
      </template>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { EyeOutlined } from '@ant-design/icons-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { TaktNoticeDto, TaktNoticeQueryDto } from '@/types/routine/notice'
import type { TaktApiResponse, TaktPagedResult } from '@/types/common'
import type { QueryField } from '@/types/components/query'
import {
  getNoticeList,
  getNoticeDetail,
  deleteNotice,
  batchDeleteNotice,
  exportNoticeList
} from '@/api/routine/notice'
import NoticeForm from './components/NoticeForm.vue'
import { message } from 'ant-design-vue'

const { t } = useI18n()

// 查询参数
const queryParams = ref<TaktNoticeQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  noticeTitle: undefined,
  noticeType: undefined,
  noticeStatus: undefined
})

// 查询字段配置
const queryFields = computed(() => [
  {
    name: 'noticeTitle',
    label: t('notice.noticeTitle'),
    type: 'input',
    placeholder: t('notice.placeholder.noticeTitle')
  },
  {
    name: 'noticeType',
    label: t('notice.noticeType'),
    type: 'select',
    props: {
      dictType: 'notice_type'
    },
    placeholder: t('notice.placeholder.noticeType')
  },
  {
    name: 'noticeStatus',
    label: t('notice.noticeStatus'),
    type: 'select',
    props: {
      dictType: 'notice_status'
    },
    placeholder: t('notice.placeholder.noticeStatus')
  }
] as QueryField[])

// 表格列定义
const columns = [
  {
    title: t('notice.field.noticeId'),
    dataIndex: 'noticeId',
    width: 80
  },
  {
    title: t('notice.field.noticeTitle'),
    dataIndex: 'noticeTitle',
    width: 200
  },
  {
    title: t('notice.field.noticeType'),
    dataIndex: 'noticeType',
    width: 120
  },
  {
    title: t('notice.field.noticeStatus'),
    dataIndex: 'noticeStatus',
    width: 120
  },
  {
    title: t('notice.field.noticeSendTime'),
    dataIndex: 'noticeSendTime',
    width: 180
  },
  {
    title: t('common.field.action'),
    key: 'action',
    width: 200,
    fixed: 'right' as const
  }
]

// 数据列表
const dataList = ref<TaktNoticeDto[]>([])
const loading = ref(false)
const total = ref(0)

// 选择行
const selectedRowKeys = ref<string[]>([])

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedNoticeId = ref<number>()

// 预览对话框
const previewVisible = ref(false)
const previewData = ref<TaktNoticeDto>()

// 显示搜索条件
const showSearch = ref(true)

// 获取数据列表
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getNoticeList(queryParams.value)
    dataList.value = res.data.data.rows
    total.value = res.data.data.total
  } catch (error) {
    console.error('获取通知列表失败:', error)
  } finally {
    loading.value = false
  }
}

// 搜索按钮操作
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置按钮操作
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    noticeTitle: undefined,
    noticeType: undefined,
    noticeStatus: undefined
  }
  handleQuery()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current || 1
  queryParams.value.pageSize = pagination.pageSize || 10
  fetchData()
}

// 行点击
const handleRowClick = (record: TaktNoticeDto) => {
  selectedNoticeId.value = Number(record.noticeId)
}

// 新增按钮操作
const handleAdd = () => {
  selectedNoticeId.value = undefined
  formTitle.value = t('notice.add')
  formVisible.value = true
}

// 修改按钮操作
const handleEdit = (record: TaktNoticeDto) => {
  selectedNoticeId.value = Number(record.noticeId)
  formTitle.value = t('notice.edit')
  formVisible.value = true
}

// 修改选中按钮操作
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.pleaseSelectOneRecord'))
    return
  }
  const record = dataList.value.find(item => String(item.noticeId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 删除按钮操作
const handleDelete = async (record: TaktNoticeDto) => {
  try {
    await deleteNotice(record.noticeId)
    message.success(t('notice.operation.success.delete'))
    fetchData()
  } catch (error) {
    console.error('删除通知失败:', error)
  }
}

// 批量删除按钮操作
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelectRecord'))
    return
  }
  try {
    await batchDeleteNotice(selectedRowKeys.value.map(id => Number(id)))
    message.success(t('notice.operation.success.batchDelete'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除通知失败:', error)
  }
}

// 导出按钮操作
const handleExport = async () => {
  try {
    await exportNoticeList(queryParams.value)
    message.success(t('notice.operation.success.export'))
  } catch (error) {
    console.error('导出通知失败:', error)
  }
}

// 预览按钮操作
const handlePreview = async (record: TaktNoticeDto) => {
  try {
    const res = await getNoticeDetail(record.noticeId)
    previewData.value = res.data.data
    previewVisible.value = true
  } catch (error) {
    console.error('获取通知详情失败:', error)
  }
}

// 表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 切换搜索显示
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏显示
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

// 分页变化
const handlePageChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

// 每页条数变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchData()
}

onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.notice-container {
  padding: 24px;

  .notice-info {
    margin: 16px 0;
    color: #666;

    span {
      margin-right: 24px;
    }
  }

  .notice-content {
    margin-top: 16px;
    padding: 16px;
    background-color: #fafafa;
    border: 1px solid #f0f0f0;
    border-radius: 4px;
  }
}
</style>

