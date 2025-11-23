<template>
  <div class="login-log">
    <!-- 查询区域 -->
    <Takt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <Takt-toolbar
      :show-refresh="true"
      :show-search="true"
      @refresh="fetchData"
      @toggle-search="toggleSearch"
    />

    <!-- 数据表格 -->
    <Takt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      row-key="loginLogId"
      :scroll="{ y: 840 }"
      @change="handleTableChange"
    >
      <!-- 登录状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'success'">
          <a-tag :color="record.success ? 'success' : 'error'">
            {{ record.success ? '成功' : '失败' }}
          </a-tag>
        </template>
        <template v-if="column.key === 'loginType'">
          {{ getLoginTypeName(record.loginType) }}
        </template>
        <template v-if="column.key === 'action'">
          <a @click="handleViewDetail(record)">详情</a>
        </template>
      </template>
    </Takt-table>

    <!-- 详情组件 -->
    <login-log-detail ref="detailRef" :login-info="currentLogin" />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { TaktLoginLog, TaktLoginLogQuery, TaktLoginLogDto } from '@/types/audit/loginLog'
import { getLoginLogList } from '@/api/audit/loginLog'
import { useUserStore } from '@/stores/userStore'
import { useI18n } from 'vue-i18n'
import LoginLogDetail from './LoginLogDetail.vue'

const { t } = useI18n()
const userStore = useUserStore()

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'ipAddress',
    label: 'IP地址',
    type: 'input',
    placeholder: '请输入IP地址'
  },
  {
    name: 'success',
    label: '登录状态',
    type: 'select',
    placeholder: '请选择登录状态',
    options: [
      { label: '成功', value: 1 },
      { label: '失败', value: 0 }
    ]
  },
  {
    name: 'loginType',
    label: '登录类型',
    type: 'select',
    placeholder: '请选择登录类型',
    options: [
      { label: '账号密码', value: 0 },
      { label: '手机验证码', value: 1 },
      { label: '邮箱验证码', value: 2 },
      { label: '第三方登录', value: 3 }
    ]
  },
  {
    name: 'startTime',
    label: '开始时间',
    type: 'date',
    placeholder: '请选择开始时间'
  },
  {
    name: 'endTime',
    label: '结束时间',
    type: 'date',
    placeholder: '请选择结束时间'
  }
]

// 表格列定义
const columns = [
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130
  },
  {
    title: '登录地点',
    dataIndex: ['deviceInfo', 'location'],
    key: 'location',
    width: 150
  },
  {
    title: '浏览器',
    key: 'browser',
    width: 120,
    customRender: ({ record }: { record: TaktLoginLog }) => {
      const type = record.deviceInfo?.browserType
      const version = record.deviceInfo?.browserVersion
      const browserName = type === 0 ? 'Chrome' :
                         type === 1 ? 'Firefox' :
                         type === 2 ? 'Edge' :
                         type === 3 ? 'Safari' :
                         type === 4 ? 'IE' : '其他'
      return `${browserName} ${version || ''}`
    }
  },
  {
    title: '操作系统',
    key: 'os',
    width: 120,
    customRender: ({ record }: { record: TaktLoginLog }) => {
      const type = record.deviceInfo?.osType
      const version = record.deviceInfo?.osVersion
      const osName = type === 0 ? 'Windows' :
                    type === 1 ? 'Android' :
                    type === 2 ? 'iOS' :
                    type === 3 ? 'MacOS' :
                    type === 4 ? 'Linux' : '其他'
      return `${osName} ${version || ''}`
    }
  },
  {
    title: '登录状态',
    dataIndex: 'success',
    key: 'success',
    width: 100
  },
  {
    title: '登录类型',
    dataIndex: 'loginType',
    key: 'loginType',
    width: 100
  },
  {
    title: '登录来源',
    dataIndex: 'loginSource',
    key: 'loginSource',
    width: 100
  },
  {
    title: '消息',
    dataIndex: 'loginMessage',
    key: 'loginMessage',
    width: 200,
    ellipsis: true
  },
  {
    title: '登录时间',
    dataIndex: 'loginTime',
    key: 'loginTime',
    width: 180
  },
  {
    title: '操作',
    key: 'action',
    width: 80,
    fixed: 'right'
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<TaktLoginLog[]>([])
const total = ref(0)
const showSearch = ref(true)
const detailRef = ref()
const currentLogin = ref<TaktLoginLogDto>()

const queryParams = reactive<TaktLoginLogQuery>({
  pageIndex: 1,
  pageSize: 10,
  orderByColumn: undefined,
  orderType: undefined,
  userName: userStore.userInfo?.userName,
  ipAddress: undefined,
  loginSuccess: undefined,
  loginType: undefined,
  startTime: undefined,
  endTime: undefined
})

/** 获取登录类型名称 */
const getLoginTypeName = (type?: number) => {
  const types: { [key: number]: string } = {
    0: '账号密码',
    1: '手机验证码',
    2: '邮箱验证码',
    3: '第三方登录'
  }
  return types[type || 0] || '未知'
}

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getLoginLogList(queryParams)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取登录日志失败:', error)
    message.error('获取登录日志失败')
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.ipAddress = undefined
  queryParams.loginSuccess = undefined
  queryParams.loginType = undefined
  queryParams.startTime = undefined
  queryParams.endTime = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

/** 查看详情 */
const handleViewDetail = (record: TaktLoginLog) => {
  currentLogin.value = {
    ...record,
    loginMessage: record.loginMessage ?? ''
  }
  detailRef.value?.open()
}

// 组件挂载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.login-log {
  padding: 16px;
}
</style>
