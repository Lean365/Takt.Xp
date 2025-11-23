<template>
  <a-modal
    :open="visible"
    title="话题管理"
    :width="1000"
    @cancel="handleCancel"
    :footer="null"
  >
    <div class="topic-manage">
      <!-- 话题统计 -->
      <a-row :gutter="16" style="margin-bottom: 16px;">
        <a-col :span="6">
          <a-statistic title="关联话题数" :value="totalTopics" />
        </a-col>
        <a-col :span="6">
          <a-statistic title="活跃话题" :value="activeTopics" />
        </a-col>
        <a-col :span="6">
          <a-statistic title="热门话题" :value="hotTopics" />
        </a-col>
        <a-col :span="6">
          <a-statistic title="推荐话题" :value="recommendedTopics" />
        </a-col>
      </a-row>

      <!-- 话题关联操作 -->
      <div style="margin-bottom: 16px;">
        <a-space>
          <a-button type="primary" @click="handleAddTopic">
            <template #icon>
              <PlusOutlined />
            </template>
            关联话题
          </a-button>
          <a-button @click="handleAutoRelate">
            <template #icon>
              <LinkOutlined />
            </template>
            自动关联
          </a-button>
        </a-space>
      </div>

      <!-- 话题列表 -->
      <a-table
        :columns="columns"
        :data-source="topicList"
        :loading="loading"
        :pagination="pagination"
        row-key="id"
        @change="handleTableChange"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.dataIndex === 'topicIcon'">
            <a-image
              v-if="record.topicIcon"
              :src="record.topicIcon"
              :width="32"
              :height="32"
              style="border-radius: 4px; object-fit: cover"
            />
            <span v-else class="text-gray-400">无图标</span>
          </template>

          <template v-if="column.dataIndex === 'status'">
            <a-tag :color="getStatusColor(record.status)">
              {{ getStatusText(record.status) }}
            </a-tag>
          </template>

          <template v-if="column.dataIndex === 'topicIsHot'">
            <a-tag :color="record.topicIsHot === 1 ? 'red' : 'default'">
              {{ record.topicIsHot === 1 ? '热门' : '普通' }}
            </a-tag>
          </template>

          <template v-if="column.dataIndex === 'topicIsRecommend'">
            <a-tag :color="record.topicIsRecommend === 1 ? 'blue' : 'default'">
              {{ record.topicIsRecommend === 1 ? '推荐' : '普通' }}
            </a-tag>
          </template>

          <template v-if="column.dataIndex === 'topicIsTop'">
            <a-tag :color="record.topicIsTop === 1 ? 'orange' : 'default'">
              {{ record.topicIsTop === 1 ? '置顶' : '普通' }}
            </a-tag>
          </template>

          <template v-if="column.key === 'action'">
            <a-space>
              <a-button
                type="danger"
                size="small"
                @click="handleRemoveTopic(record)"
              >
                移除关联
              </a-button>
              <a-button
                type="default"
                size="small"
                @click="handleViewTopic(record)"
              >
                查看话题
              </a-button>
            </a-space>
          </template>
        </template>
      </a-table>
    </div>

    <!-- 关联话题对话框 -->
    <a-modal
      v-model:visible="addTopicVisible"
      title="关联话题"
      @ok="handleAddTopicConfirm"
      @cancel="addTopicVisible = false"
    >
      <a-form :model="addTopicForm" layout="vertical">
        <a-form-item label="选择话题">
          <a-select
            v-model:value="addTopicForm.topicId"
            placeholder="请选择要关联的话题"
            style="width: 100%"
            show-search
            :filter-option="filterTopicOption"
          >
            <a-select-option
              v-for="topic in availableTopics"
              :key="topic.topicId"
              :value="topic.topicId"
            >
              {{ topic.topicName }}
            </a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="关联权重">
          <a-input-number
            v-model:value="addTopicForm.relationWeight"
            :min="1"
            :max="10"
            style="width: 100%"
          />
        </a-form-item>
        <a-form-item label="关联备注">
          <a-textarea
            v-model:value="addTopicForm.relationRemark"
            placeholder="请输入关联备注"
            :rows="3"
          />
        </a-form-item>
      </a-form>
    </a-modal>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { PlusOutlined, LinkOutlined } from '@ant-design/icons-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { TaktNewsTopic } from '@/types/routine/news/topic'
import type { TaktNewsTopicRelation } from '@/types/routine/news/topic-relation'
import { 
  getTopicsByNewsId, 
  relateNewsToTopic,
  unrelateNewsFromTopic,
  autoRelateNewsToTopics
} from '@/api/routine/news/topic-relation'
import { getNewsTopicList } from '@/api/routine/news/topic'

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
const topicList = ref<TaktNewsTopicRelation[]>([])
const availableTopics = ref<TaktNewsTopic[]>([])
const totalTopics = ref(0)
const activeTopics = ref(0)
const hotTopics = ref(0)
const recommendedTopics = ref(0)

// 关联话题对话框
const addTopicVisible = ref(false)
const addTopicForm = reactive({
  topicId: undefined as number | undefined,
  relationWeight: 1,
  relationRemark: ''
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
    title: '话题图标',
    dataIndex: 'topicIcon',
    key: 'topicIcon',
    width: 80
  },
  {
    title: '话题名称',
    dataIndex: 'topicName',
    key: 'topicName',
    width: 150,
    ellipsis: true
  },
  {
    title: '话题描述',
    dataIndex: 'topicDescription',
    key: 'topicDescription',
    ellipsis: true
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '热门',
    dataIndex: 'topicIsHot',
    key: 'topicIsHot',
    width: 80
  },
  {
    title: '推荐',
    dataIndex: 'topicIsRecommend',
    key: 'topicIsRecommend',
    width: 80
  },
  {
    title: '置顶',
    dataIndex: 'topicIsTop',
    key: 'topicIsTop',
    width: 80
  },
  {
    title: '参与人数',
    dataIndex: 'topicParticipantCount',
    key: 'topicParticipantCount',
    width: 100
  },
  {
    title: '新闻数量',
    dataIndex: 'topicNewsCount',
    key: 'topicNewsCount',
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
      fetchTopics()
      fetchAvailableTopics()
    }
  }
)

// 获取话题列表
const fetchTopics = async () => {
  try {
    loading.value = true
    const res = await getTopicsByNewsId(props.newsId)
    
    if (res.data.code === 200) {
      topicList.value = res.data.data
      pagination.total = res.data.data.length
      
      // 计算统计数据
      calculateStats()
    } else {
      message.error(res.data.msg || '获取话题列表失败')
    }
  } catch (error) {
    console.error('[话题管理] 获取话题出错:', error)
    message.error('获取话题列表失败')
  } finally {
    loading.value = false
  }
}

// 获取可用话题列表
const fetchAvailableTopics = async () => {
  try {
    const res = await getNewsTopicList({ pageIndex: 1, pageSize: 100 })
    if (res.data.code === 200) {
      availableTopics.value = res.data.data.rows
    }
  } catch (error) {
    console.error('[话题管理] 获取可用话题出错:', error)
  }
}

// 计算统计数据
const calculateStats = () => {
  totalTopics.value = topicList.value.length
  activeTopics.value = 0 // 暂时设为0，需要从话题详情中获取
  hotTopics.value = 0 // 暂时设为0，需要从话题详情中获取
  recommendedTopics.value = 0 // 暂时设为0，需要从话题详情中获取
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1
  pagination.pageSize = pag.pageSize || 10
  fetchTopics()
}

// 关联话题
const handleAddTopic = () => {
  addTopicForm.topicId = undefined
  addTopicForm.relationWeight = 1
  addTopicForm.relationRemark = ''
  addTopicVisible.value = true
}

// 确认关联话题
const handleAddTopicConfirm = async () => {
  if (!addTopicForm.topicId) {
    message.warning('请选择要关联的话题')
    return
  }

  try {
    const res = await relateNewsToTopic(
      props.newsId,
      addTopicForm.topicId,
      1,
      addTopicForm.relationWeight,
      false
    )
    
    if (res.data.code === 200) {
      message.success('关联话题成功')
      addTopicVisible.value = false
      fetchTopics()
    } else {
      message.error(res.data.msg || '关联话题失败')
    }
  } catch (error) {
    console.error('[话题管理] 关联话题出错:', error)
    message.error('关联话题失败')
  }
}

// 自动关联
const handleAutoRelate = async () => {
  try {
    const res = await autoRelateNewsToTopics({ 
      newsId: props.newsId,
      newsTitle: '',
      newsContent: ''
    })
    if (res.data.code === 200) {
      message.success(`自动关联了 ${res.data.data} 个话题`)
      fetchTopics()
    } else {
      message.error(res.data.msg || '自动关联失败')
    }
  } catch (error) {
    console.error('[话题管理] 自动关联出错:', error)
    message.error('自动关联失败')
  }
}

// 移除话题关联
const handleRemoveTopic = async (record: TaktNewsTopic) => {
  try {
    const res = await unrelateNewsFromTopic(props.newsId, record.topicId)
    if (res.data.code === 200) {
      message.success('移除话题关联成功')
      fetchTopics()
    } else {
      message.error(res.data.msg || '移除话题关联失败')
    }
  } catch (error) {
    console.error('[话题管理] 移除话题关联出错:', error)
    message.error('移除话题关联失败')
  }
}

// 查看话题
const handleViewTopic = (record: TaktNewsTopic) => {
  // 实现查看话题逻辑
  console.log('查看话题:', record)
}

// 话题搜索过滤
const filterTopicOption = (input: string, option: any) => {
  return option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
}

// 获取状态颜色
const getStatusColor = (status: number) => {
  switch (status) {
    case 1: return 'green'
    case 2: return 'orange'
    case 3: return 'gray'
    default: return 'default'
  }
}

// 获取状态文本
const getStatusText = (status: number) => {
  switch (status) {
    case 1: return '活跃'
    case 2: return '非活跃'
    case 3: return '已归档'
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
    fetchTopics()
    fetchAvailableTopics()
  }
})
</script>

<style scoped>
.topic-manage {
  padding: 16px 0;
}

.text-gray-400 {
  color: #9ca3af;
}
</style> 
