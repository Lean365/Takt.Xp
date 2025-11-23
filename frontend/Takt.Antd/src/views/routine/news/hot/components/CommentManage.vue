<template>
  <a-modal
    :open="visible"
    title="评论管理"
    :width="1000"
    @cancel="handleCancel"
    :footer="null"
  >
    <div class="comment-manage">
      <!-- 评论统计 -->
      <a-row :gutter="16" style="margin-bottom: 16px;">
        <a-col :span="6">
          <a-statistic title="总评论数" :value="totalComments" />
        </a-col>
        <a-col :span="6">
          <a-statistic title="待审核" :value="pendingComments" />
        </a-col>
        <a-col :span="6">
          <a-statistic title="已通过" :value="approvedComments" />
        </a-col>
        <a-col :span="6">
          <a-statistic title="已拒绝" :value="rejectedComments" />
        </a-col>
      </a-row>

      <!-- 评论列表 -->
      <a-table
        :columns="columns"
        :data-source="commentList"
        :loading="loading"
        :pagination="pagination"
        row-key="id"
        @change="handleTableChange"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.dataIndex === 'commentContent'">
            <div class="comment-content">
              <div class="comment-text">{{ record.commentContent }}</div>
              <div class="comment-info">
                <span class="comment-user">{{ record.commentUserName }}</span>
                <span class="comment-time">{{ formatTime(record.createTime) }}</span>
              </div>
            </div>
          </template>

          <template v-if="column.dataIndex === 'commentStatus'">
            <a-tag :color="getStatusColor(record.commentStatus)">
              {{ getStatusText(record.commentStatus) }}
            </a-tag>
          </template>

          <template v-if="column.key === 'action'">
            <a-space>
              <a-button
                v-if="record.commentStatus === 0"
                type="primary"
                size="small"
                @click="handleApprove(record)"
              >
                通过
              </a-button>
              <a-button
                v-if="record.commentStatus === 0"
                type="danger"
                size="small"
                @click="handleReject(record)"
              >
                拒绝
              </a-button>
              <a-button
                type="default"
                size="small"
                @click="handleViewDetail(record)"
              >
                详情
              </a-button>
            </a-space>
          </template>
        </template>
      </a-table>
    </div>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { TaktNewsComment } from '@/types/routine/news/comment'
import { 
  getCommentsByNewsId, 
  approveNewsComment, 
  rejectNewsComment 
} from '@/api/routine/news/comment'

interface Props {
  visible: boolean
  newsId: number
}

interface Emits {
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const loading = ref(false)
const commentList = ref<TaktNewsComment[]>([])
const totalComments = ref(0)
const pendingComments = ref(0)
const approvedComments = ref(0)
const rejectedComments = ref(0)

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
    title: '评论内容',
    dataIndex: 'commentContent',
    key: 'commentContent',
    ellipsis: true
  },
  {
    title: '状态',
    dataIndex: 'commentStatus',
    key: 'commentStatus',
    width: 100
  },
  {
    title: '点赞数',
    dataIndex: 'likeCount',
    key: 'likeCount',
    width: 100
  },
  {
    title: '回复数',
    dataIndex: 'replyCount',
    key: 'replyCount',
    width: 100
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right' as const
  }
]

// 监听visible变化
watch(
  () => props.visible,
  (visible) => {
    if (visible && props.newsId) {
      fetchComments()
    }
  }
)

// 获取评论列表
const fetchComments = async () => {
  try {
    loading.value = true
    const res = await getCommentsByNewsId(props.newsId, pagination.current || 1)
    
    if (res.data.code === 200) {
      commentList.value = res.data.data.rows
      pagination.total = res.data.data.total
      
      // 计算统计数据
      calculateStats()
    } else {
      message.error(res.data.msg || '获取评论列表失败')
    }
  } catch (error) {
    console.error('[评论管理] 获取评论出错:', error)
    message.error('获取评论列表失败')
  } finally {
    loading.value = false
  }
}

// 计算统计数据
const calculateStats = () => {
  totalComments.value = commentList.value.length
  pendingComments.value = commentList.value.filter(c => c.commentStatus === 0).length
  approvedComments.value = commentList.value.filter(c => c.commentStatus === 1).length
  rejectedComments.value = commentList.value.filter(c => c.commentStatus === 2).length
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1
  pagination.pageSize = pag.pageSize || 10
  fetchComments()
}

// 通过评论
const handleApprove = async (record: TaktNewsComment) => {
  try {
    const res = await approveNewsComment(record.commentId)
    if (res.data.code === 200) {
      message.success('审核通过成功')
      fetchComments()
    } else {
      message.error(res.data.msg || '审核通过失败')
    }
  } catch (error) {
    console.error('[评论管理] 审核通过出错:', error)
    message.error('审核通过失败')
  }
}

// 拒绝评论
const handleReject = async (record: TaktNewsComment) => {
  try {
    const res = await rejectNewsComment(record.commentId)
    if (res.data.code === 200) {
      message.success('审核拒绝成功')
      fetchComments()
    } else {
      message.error(res.data.msg || '审核拒绝失败')
    }
  } catch (error) {
    console.error('[评论管理] 审核拒绝出错:', error)
    message.error('审核拒绝失败')
  }
}

// 查看详情
const handleViewDetail = (record: TaktNewsComment) => {
  // 实现查看详情逻辑
  console.log('查看评论详情:', record)
}

// 格式化时间
const formatTime = (time: string | Date) => {
  if (!time) return ''
  return new Date(time).toLocaleString()
}

// 获取状态颜色
const getStatusColor = (status: number) => {
  switch (status) {
    case 0: return 'orange'
    case 1: return 'green'
    case 2: return 'red'
    default: return 'default'
  }
}

// 获取状态文本
const getStatusText = (status: number) => {
  switch (status) {
    case 0: return '待审核'
    case 1: return '已通过'
    case 2: return '已拒绝'
    default: return '未知'
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 初始化
onMounted(() => {
  if (props.visible && props.newsId) {
    fetchComments()
  }
})
</script>

<style scoped>
.comment-manage {
  padding: 16px 0;
}

.comment-content {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.comment-text {
  font-size: 14px;
  line-height: 1.5;
}

.comment-info {
  display: flex;
  gap: 16px;
  font-size: 12px;
  color: #666;
}

.comment-user {
  font-weight: 500;
}

.comment-time {
  color: #999;
}
</style> 
