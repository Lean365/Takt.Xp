<template>
  <a-modal
    :open="visible"
    title="点赞管理"
    :width="800"
    @cancel="handleCancel"
    :footer="null"
  >
    <div class="like-manage">
      <!-- 点赞统计 -->
      <a-row :gutter="16" style="margin-bottom: 16px;">
        <a-col :span="8">
          <a-statistic title="总点赞数" :value="totalLikes" />
        </a-col>
        <a-col :span="8">
          <a-statistic title="今日点赞" :value="todayLikes" />
        </a-col>
        <a-col :span="8">
          <a-statistic title="本周点赞" :value="weekLikes" />
        </a-col>
      </a-row>

      <!-- 点赞列表 -->
      <a-table
        :columns="columns"
        :data-source="likeList"
        :loading="loading"
        :pagination="pagination"
        row-key="id"
        @change="handleTableChange"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.dataIndex === 'userAvatar'">
            <a-image
              :src="record.userAvatar || '/avatar/default.gif'"
              :width="32"
              :height="32"
              style="border-radius: 50%; object-fit: cover"
            />
          </template>

          <template v-if="column.dataIndex === 'likeTime'">
            {{ formatTime(record.likeTime) }}
          </template>

          <template v-if="column.key === 'action'">
            <a-space>
              <a-button
                danger
                size="small"
                @click="handleRemoveLike(record)"
              >
                移除点赞
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
import type { TaktNewsLike } from '@/types/routine/news/like'
import { getLikedUsersByNewsId, deleteNewsLike } from '@/api/routine/news/like'

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
const likeList = ref<TaktNewsLike[]>([])
const totalLikes = ref(0)
const todayLikes = ref(0)
const weekLikes = ref(0)

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
    title: '用户头像',
    dataIndex: 'userAvatar',
    key: 'userAvatar',
    width: 80
  },
  {
    title: '用户名',
    dataIndex: 'userName',
    key: 'userName',
    width: 120
  },
  {
    title: '用户IP',
    dataIndex: 'userIp',
    key: 'userIp',
    width: 120
  },
  {
    title: '点赞时间',
    dataIndex: 'likeTime',
    key: 'likeTime',
    width: 180
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark',
    ellipsis: true
  },
  {
    title: '操作',
    key: 'action',
    width: 120,
    fixed: 'right' as const
  }
]

// 监听visible变化
watch(
  () => props.visible,
  (visible) => {
    if (visible && props.newsId) {
      fetchLikes()
    }
  }
)

// 获取点赞列表
const fetchLikes = async () => {
  try {
    loading.value = true
    const res = await getLikedUsersByNewsId({
      newsId: props.newsId,
      pageIndex: pagination.current,
      pageSize: pagination.pageSize
    })
    
    if (res.code === 200) {
      likeList.value = res.data.rows
      pagination.total = res.data.totalNum
      
      // 计算统计数据
      calculateStats()
    } else {
      message.error(res.msg || '获取点赞列表失败')
    }
  } catch (error) {
    console.error('[点赞管理] 获取点赞出错:', error)
    message.error('获取点赞列表失败')
  } finally {
    loading.value = false
  }
}

// 计算统计数据
const calculateStats = () => {
  totalLikes.value = likeList.value.length
  
  const now = new Date()
  const today = new Date(now.getFullYear(), now.getMonth(), now.getDate())
  const weekAgo = new Date(today.getTime() - 7 * 24 * 60 * 60 * 1000)
  
  todayLikes.value = likeList.value.filter(like => 
    new Date(like.likeTime) >= today
  ).length
  
  weekLikes.value = likeList.value.filter(like => 
    new Date(like.likeTime) >= weekAgo
  ).length
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1
  pagination.pageSize = pag.pageSize || 10
  fetchLikes()
}

// 移除点赞
const handleRemoveLike = async (record: any) => {
  try {
    const res = await deleteNewsLike(record.id)
    if (res) {
      message.success('移除点赞成功')
      fetchLikes()
    } else {
      message.error('移除点赞失败')
    }
  } catch (error) {
    console.error('[点赞管理] 移除点赞出错:', error)
    message.error('移除点赞失败')
  }
}

// 格式化时间
const formatTime = (time: string | Date) => {
  if (!time) return ''
  return new Date(time).toLocaleString()
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 初始化
onMounted(() => {
  if (props.visible && props.newsId) {
    fetchLikes()
  }
})
</script>

<style scoped>
.like-manage {
  padding: 16px 0;
}
</style> 
