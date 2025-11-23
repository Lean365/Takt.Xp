<template>
  <div class="notification-item" @click="toggleReadStatus" :data-status="data.status">
    <div class="item-icon">
      <a-badge :dot="data.status === 'unread'" status="error" :offset="[-4, 4]">
        <template v-if="data.type === 'system'">
          <bell-outlined />
        </template>
        <template v-else-if="data.type === 'task'">
          <schedule-outlined />
        </template>
        <template v-else>
          <message-outlined />
        </template>
      </a-badge>
    </div>
    <div class="item-content">
      <div class="item-body">{{ truncatedContent }}</div>
      <div class="item-time">{{ formatTime(data.createTime) }}</div>
    </div>
    <div class="action-buttons">
      <a-tooltip :title="t('notification.detail')">
        <a-button type="text" @click.stop="toggleReadStatus">
          <template #icon>
            <eye-outlined />
          </template>
        </a-button>
      </a-tooltip>
      <a-tooltip :title="t('notification.delete')">
        <a-button type="text" @click.stop="$emit('delete', data.id)">
          <template #icon>
            <delete-outlined />
          </template>
        </a-button>
      </a-tooltip>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { BellOutlined, ScheduleOutlined, MessageOutlined, DeleteOutlined, EyeOutlined } from '@ant-design/icons-vue'
import type { NotificationItem } from '@/types/settings'
import { formatDistanceToNow } from 'date-fns'
import { zhCN } from 'date-fns/locale'

const { t } = useI18n()

// Props
interface Props {
  data: NotificationItem
}

const props = defineProps<Props>()

// Emits
const emit = defineEmits<{
  (e: 'read', id: string): void
  (e: 'unread', id: string): void
  (e: 'delete', id: string): void
}>()

// 计算属性：截断的内容
const truncatedContent = computed(() => {
  const content = props.data.content || ''
  return content.length > 40 ? content.slice(0, 40) + '...' : content
})

// 切换已读/未读状态
const toggleReadStatus = () => {
  if (props.data.status === 'unread') {
    emit('read', props.data.id)
  } else {
    emit('unread', props.data.id)
  }
}

// 格式化时间
const formatTime = (time: string) => {
  return formatDistanceToNow(new Date(time), { addSuffix: true, locale: zhCN })
}

// 格式化消息内容
const formatContent = (content: string) => {
  if (!content) return ''
  try {
    const data = JSON.parse(content)
    return data.content || data.Content || content
  } catch {
    return content
  }
}

// 格式化标题
const formatTitle = (title: string) => {
  try {
    const data = JSON.parse(title)
    // 优先使用标准标题字段
    if (data.Title || data.title) {
      return data.Title || data.title
    }
    // 根据消息类型生成标题
    if (data.type || data.messageType) {
      const type = data.type || data.messageType
      return t(`components.notification.types.${type}.label`) || '新消息'
    }
    // 如果是任务相关
    if (data.taskId || data.jobId) {
      return `任务通知 #${data.taskId || data.jobId}`
    }
    return '新消息'
  } catch {
    return title || '新消息'
  }
}
</script>

<style lang="less" scoped>
.notification-item {
  display: flex;
  align-items: center;
  padding: 4px 6px;
  margin: 0;
  cursor: pointer;
  transition: all 0.3s;
  height: 36px;
  box-sizing: border-box;
  width: 100%;
  position: relative;

  &:hover {
    background-color: var(--ant-color-bg-container-hover);
  }

  .item-icon {
    position: relative;
    flex-shrink: 0;
    width: 20px;
    height: 20px;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 50%;
    background-color: var(--ant-color-bg-container);
    color: var(--ant-color-primary);
    font-size: 14px;
    margin: 0;
  }

  .item-content {
    flex: 1;
    min-width: 0;
    padding-right: 64px;
    display: flex;
    flex-direction: column;
    margin: 0 8px;
    gap: 0;

    .item-body {
      color: var(--ant-color-text);
      font-size: 13px;
      line-height: 18px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
      max-width: 100%;
      font-weight: normal;

      [data-status="unread"] & {
        font-weight: 600;
      }
    }

    .item-time {
      color: var(--ant-color-text-secondary);
      font-size: 12px;
      line-height: 14px;
    }
  }

  .action-buttons {
    position: absolute;
    right: 6px;
    top: 50%;
    transform: translateY(-50%);
    display: flex;
    align-items: center;
    gap: 4px;
    opacity: 0;
    transition: opacity 0.2s;
    margin: 0;
    flex-shrink: 0;
    width: 56px;

    .ant-btn {
      width: 20px;
      height: 20px;
      padding: 0;
      font-size: 14px;
      display: flex;
      align-items: center;
      justify-content: center;
      border: none;
      color: rgba(0, 0, 0, 0.45);
      
      &:hover {
        color: rgba(0, 0, 0, 0.85);
        background-color: rgba(0, 0, 0, 0.06);
      }

      &:last-child:hover {
        color: var(--ant-color-error);
        background-color: var(--ant-color-error-bg);
      }

      .anticon {
        font-size: 14px;
        line-height: 1;
      }
    }
  }

  &:hover .action-buttons {
    opacity: 1;
  }
}

:deep(.ant-badge-dot) {
  box-shadow: 0 0 0 1px #fff;
  width: 6px;
  height: 6px;
}
</style> 