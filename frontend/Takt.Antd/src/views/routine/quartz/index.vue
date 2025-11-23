<template>
  <div class="quartz-container">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item :label="t('quartz.jobName')">
          <a-input
            v-model:value="queryParams.jobName"
            :placeholder="t('quartz.placeholder.jobName')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('quartz.jobGroup')">
          <a-input
            v-model:value="queryParams.jobGroup"
            :placeholder="t('quartz.placeholder.jobGroup')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('quartz.jobStatus')">
          <Takt-select
            v-model:value="queryParams.jobStatus"
            dict-type="quartz_job_status"
            type="radio"
            :placeholder="t('quartz.placeholder.jobStatus')"
            allow-clear
          />
        </a-form-item>
      </template>
    </Takt-query>

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-add="true"
      :add-permission="['routine:quartz:create']"
      :show-edit="true"
      :edit-permission="['routine:quartz:update']"
      :show-delete="true"
      :delete-permission="['routine:quartz:delete']"
      :show-export="true"
      :export-permission="['routine:quartz:export']"
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
      :row-key="(record: TaktQuartzJobDto) => String(record.jobId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'jobStatus'">
          <Takt-dict-tag dict-type="quartz_job_status" :value="record.jobStatus" />
        </template>
        <template v-else-if="column.key === 'action'">
          <Takt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:quartz:update']"
            :show-delete="true"
            :delete-permission="['routine:quartz:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('quartz.execute')">
                <a-button
                  type="link"
                  size="small"
                  @click.stop="handleExecute(record)"
                >
                  <template #icon><play-circle-outlined /></template>
                </a-button>
              </a-tooltip>
              <a-tooltip :title="record.jobStatus === 0 ? t('quartz.resume') : t('quartz.pause')">
                <a-button
                  type="link"
                  size="small"
                  @click.stop="handleToggleStatus(record)"
                >
                  <template #icon>
                    <pause-circle-outlined v-if="record.jobStatus === 1" />
                    <play-circle-outlined v-else />
                  </template>
                </a-button>
              </a-tooltip>
              <a-tooltip :title="t('quartz.log')">
                <a-button
                  type="link"
                  size="small"
                  @click.stop="handleViewLog(record)"
                >
                  <template #icon><file-text-outlined /></template>
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

    <!-- 定时任务表单对话框 -->
    <quartz-form
      v-model:visible="formVisible"
      :title="formTitle"
      :job-id="selectedJobId"
      @success="handleSuccess"
    />

    <!-- 日志对话框 -->
    <a-modal
      v-model:open="logVisible"
      :title="t('quartz.log')"
      :footer="null"
      width="800px"
    >
      <template v-if="logData">
        <div class="log-info">
          <p>{{ t('quartz.field.jobExecuteTime') }}: {{ logData.jobExecuteTime }}</p>
          <p>{{ t('quartz.field.jobExecuteResult') }}: {{ logData.jobExecuteResult }}</p>
          <p v-if="logData.jobExecuteError">{{ t('quartz.field.jobExecuteError') }}: {{ logData.jobExecuteError }}</p>
        </div>
      </template>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  PlayCircleOutlined,
  PauseCircleOutlined,
  FileTextOutlined
} from '@ant-design/icons-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { TaktQuartzJobDto, TaktQuartzJobQueryDto } from '@/types/routine/quartz'
import type { TaktApiResponse, TaktPagedResult } from '@/types/common'
import type { QueryField } from '@/types/components/query'
import {
  getQuartzJobList,
  getQuartzJobDetail,
  deleteQuartzJob,
  batchDeleteQuartzJob,
  exportQuartzJobList,
  runQuartzJob,
  pauseQuartzJob,
  resumeQuartzJob
} from '@/api/routine/quartz'
import QuartzForm from './components/QuartzForm.vue'
import { message } from 'ant-design-vue'

const { t } = useI18n()

// 查询参数
const queryParams = ref<TaktQuartzJobQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  jobName: undefined,
  jobGroup: undefined,
  jobStatus: undefined
})

// 查询字段配置
const queryFields = computed(() => [
  {
    name: 'jobName',
    label: t('quartz.jobName'),
    type: 'input',
    placeholder: t('quartz.placeholder.jobName')
  },
  {
    name: 'jobGroup',
    label: t('quartz.jobGroup'),
    type: 'input',
    placeholder: t('quartz.placeholder.jobGroup')
  },
  {
    name: 'jobStatus',
    label: t('quartz.jobStatus'),
    type: 'select',
    props: {
      dictType: 'quartz_job_status'
    },
    placeholder: t('quartz.placeholder.jobStatus')
  }
] as QueryField[])

// 表格列定义
const columns = [
  {
    title: t('quartz.field.jobId'),
    dataIndex: 'jobId',
    width: 80
  },
  {
    title: t('quartz.field.jobName'),
    dataIndex: 'jobName',
    width: 150
  },
  {
    title: t('quartz.field.jobGroup'),
    dataIndex: 'jobGroup',
    width: 120
  },
  {
    title: t('quartz.field.jobStatus'),
    dataIndex: 'jobStatus',
    width: 100
  },
  {
    title: t('quartz.field.jobClassName'),
    dataIndex: 'jobClassName',
    width: 200
  },
  {
    title: t('quartz.field.jobMethodName'),
    dataIndex: 'jobMethodName',
    width: 120
  },
  {
    title: t('quartz.field.jobCronExpression'),
    dataIndex: 'jobCronExpression',
    width: 150
  },
  {
    title: t('quartz.field.jobExecuteTime'),
    dataIndex: 'jobExecuteTime',
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
const dataList = ref<TaktQuartzJobDto[]>([])
const loading = ref(false)
const total = ref(0)

// 选择行
const selectedRowKeys = ref<string[]>([])

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedJobId = ref<number>()

// 日志对话框
const logVisible = ref(false)
const logData = ref<TaktQuartzJobDto>()

// 显示搜索条件
const showSearch = ref(true)

// 获取数据列表
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getQuartzJobList(queryParams.value)
    dataList.value = res.data.data.rows
    total.value = res.data.data.total
  } catch (error) {
    console.error('获取定时任务列表失败:', error)
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
    jobName: undefined,
    jobGroup: undefined,
    jobStatus: undefined
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
const handleRowClick = (record: TaktQuartzJobDto) => {
  selectedJobId.value = Number(record.jobId)
}

// 新增按钮操作
const handleAdd = () => {
  selectedJobId.value = undefined
  formTitle.value = t('quartz.add')
  formVisible.value = true
}

// 修改按钮操作
const handleEdit = (record: TaktQuartzJobDto) => {
  selectedJobId.value = Number(record.jobId)
  formTitle.value = t('quartz.edit')
  formVisible.value = true
}

// 修改选中按钮操作
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.pleaseSelectOneRecord'))
    return
  }
  const record = dataList.value.find(item => String(item.jobId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 删除按钮操作
const handleDelete = async (record: TaktQuartzJobDto) => {
  try {
    await deleteQuartzJob(record.jobId)
    message.success(t('quartz.operation.success.delete'))
    fetchData()
  } catch (error) {
    console.error('删除定时任务失败:', error)
  }
}

// 批量删除按钮操作
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelectRecord'))
    return
  }
  try {
    await batchDeleteQuartzJob(selectedRowKeys.value.map(id => Number(id)))
    message.success(t('quartz.operation.success.batchDelete'))
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('批量删除定时任务失败:', error)
  }
}

// 导出按钮操作
const handleExport = async () => {
  try {
    await exportQuartzJobList(queryParams.value)
    message.success(t('quartz.operation.success.export'))
  } catch (error) {
    console.error('导出定时任务失败:', error)
  }
}

// 执行按钮操作
const handleExecute = async (record: TaktQuartzJobDto) => {
  try {
    await runQuartzJob(record.jobId)
    message.success(t('quartz.operation.success.execute'))
  } catch (error) {
    console.error('执行定时任务失败:', error)
  }
}

// 切换状态按钮操作
const handleToggleStatus = async (record: TaktQuartzJobDto) => {
  try {
    if (record.jobStatus === 0) {
      await resumeQuartzJob(record.jobId)
      message.success(t('quartz.operation.success.resume'))
    } else {
      await pauseQuartzJob(record.jobId)
      message.success(t('quartz.operation.success.pause'))
    }
    fetchData()
  } catch (error) {
    console.error('切换定时任务状态失败:', error)
  }
}

// 查看日志按钮操作
const handleViewLog = async (record: TaktQuartzJobDto) => {
  try {
    const res = await getQuartzJobDetail(record.jobId)
    logData.value = res.data.data
    logVisible.value = true
  } catch (error) {
    console.error('获取定时任务日志失败:', error)
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
.quartz-container {
  padding: 24px;

  .log-info {
    padding: 16px;
    background-color: #fafafa;
    border: 1px solid #f0f0f0;
    border-radius: 4px;

    p {
      margin-bottom: 8px;
      &:last-child {
        margin-bottom: 0;
      }
    }
  }
}
</style>

