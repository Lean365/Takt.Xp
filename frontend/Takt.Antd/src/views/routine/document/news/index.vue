<template>
  <div class="news-management">
    <a-card title="新闻管理" :bordered="false">
      <template #extra>
        <a-space>
          <a-button type="primary" @click="handleAdd">
            <template #icon>
              <PlusOutlined />
            </template>
            新增新闻
          </a-button>
          <a-button @click="handleRefresh">
            <template #icon>
              <ReloadOutlined />
            </template>
            刷新
          </a-button>
        </a-space>
      </template>

      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams" @finish="handleQuery" style="margin-bottom: 16px;">
        <a-form-item name="title" label="新闻标题">
          <a-input
            v-model:value="queryParams.title"
            placeholder="请输入新闻标题"
            allow-clear
            style="width: 200px"
          />
        </a-form-item>
        <a-form-item name="status" label="状态">
          <Takt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            placeholder="请选择状态"
            style="width: 120px"
            allow-clear
          />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" html-type="submit">
              <template #icon>
                <SearchOutlined />
              </template>
              查询
            </a-button>
            <a-button @click="handleReset">
              <template #icon>
                <RedoOutlined />
              </template>
              重置
            </a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="list"
        :loading="loading"
        :pagination="pagination"
        row-key="id"
        @change="handleTableChange"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'status'">
            <Takt-tag :value="record.status" dict-type="sys_normal_disable" />
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a @click="handleView(record)">查看</a>
              <a @click="handleEdit(record)">编辑</a>
              <a-popconfirm
                title="确定要删除这条新闻吗？"
                @confirm="handleDelete(record)"
              >
                <a class="text-danger">删除</a>
              </a-popconfirm>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 新增/编辑弹窗 -->
    <news-form
      v-model:visible="formVisible"
      :title="formTitle"
      :news-id="formNewsId"
      @success="handleSuccess"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { PlusOutlined, ReloadOutlined, SearchOutlined, RedoOutlined } from '@ant-design/icons-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import NewsForm from './components/NewsForm.vue'

// 定义新闻数据类型
interface NewsItem {
  id: number
  title: string
  author: string
  publishTime: string
  status: number
}

// 响应式数据
const loading = ref(false)
const list = ref<NewsItem[]>([])
const formVisible = ref(false)
const formTitle = ref('')
const formNewsId = ref<number>()

// 查询参数
const queryParams = reactive({
  title: '',
  status: undefined
})

// 分页配置
const pagination = reactive<TablePaginationConfig>({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true,
  showTotal: (total, range) => `第 ${range[0]}-${range[1]} 条/共 ${total} 条`
})

// 表格列定义
const columns = [
  {
    title: '新闻标题',
    dataIndex: 'title',
    key: 'title',
    ellipsis: true
  },
  {
    title: '作者',
    dataIndex: 'author',
    key: 'author',
    width: 120
  },
  {
    title: '发布时间',
    dataIndex: 'publishTime',
    key: 'publishTime',
    width: 180
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right' as const
  }
]

// 获取列表数据
const getList = async () => {
  try {
    loading.value = true
    // TODO: 调用API获取新闻列表
    // const res = await getNewsList({
    //   ...queryParams,
    //   pageNum: pagination.current,
    //   pageSize: pagination.pageSize
    // })
    // if (res.data.code === 200) {
    //   list.value = res.data.data.list
    //   pagination.total = res.data.data.total
    // }
    
    // 模拟数据
    list.value = [
      {
        id: 1,
        title: '公司年度总结大会通知',
        author: '管理员',
        publishTime: '2024-01-15 10:00:00',
        status: 0
      },
      {
        id: 2,
        title: '新员工入职培训安排',
        author: '人事部',
        publishTime: '2024-01-14 14:30:00',
        status: 0
      }
    ]
    pagination.total = 2
  } catch (error) {
    console.error('[新闻管理] 获取列表出错:', error)
    message.error('获取新闻列表失败')
  } finally {
    loading.value = false
  }
}

// 查询
const handleQuery = () => {
  pagination.current = 1
  getList()
}

// 重置
const handleReset = () => {
  Object.assign(queryParams, {
    title: '',
    status: undefined
  })
  pagination.current = 1
  getList()
}

// 刷新
const handleRefresh = () => {
  getList()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1
  pagination.pageSize = pag.pageSize || 10
  getList()
}

// 新增
const handleAdd = () => {
  formTitle.value = '新增新闻'
  formNewsId.value = undefined
  formVisible.value = true
}

// 编辑
const handleEdit = (record: any) => {
  formTitle.value = '编辑新闻'
  formNewsId.value = record.id
  formVisible.value = true
}

// 查看
const handleView = (record: any) => {
  formTitle.value = '查看新闻'
  formNewsId.value = record.id
  formVisible.value = true
}

// 删除
const handleDelete = async (record: any) => {
  try {
    // TODO: 调用删除API
    // const res = await deleteNews(record.id)
    // if (res.data.code === 200) {
    //   message.success('删除成功')
    //   getList()
    // }
    message.success('删除成功')
    getList()
  } catch (error) {
    console.error('[新闻管理] 删除出错:', error)
    message.error('删除失败')
  }
}

// 表单提交成功
const handleSuccess = () => {
  getList()
}

// 初始化
onMounted(() => {
  getList()
})
</script>

<style scoped>
.news-management {
  padding: 16px;
}

.text-danger {
  color: #ff4d4f;
}

.text-danger:hover {
  color: #ff7875;
}
</style> 
