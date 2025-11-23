<template>
  <a-modal
    v-model:open="visible"
    :title="title"
    width="600px"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirm-loading="loading"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      layout="vertical"
      :label-col="{ span: 24 }"
      :wrapper-col="{ span: 24 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="话题ID" name="topicId">
            <a-input-number
              v-model:value="formData.topicId"
              placeholder="请输入话题ID"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="用户ID" name="userId">
            <a-input-number
              v-model:value="formData.userId"
              placeholder="请输入用户ID"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="用户姓名" name="userName">
            <a-input
              v-model:value="formData.userName"
              placeholder="请输入用户姓名"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="用户头像" name="userAvatar">
            <a-input
              v-model:value="formData.userAvatar"
              placeholder="请输入用户头像URL"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="参与类型" name="participationType">
            <a-select
              v-model:value="formData.participationType"
              placeholder="请选择参与类型"
              :disabled="isView"
            >
              <a-select-option :value="1">创建者</a-select-option>
              <a-select-option :value="2">管理员</a-select-option>
              <a-select-option :value="3">普通成员</a-select-option>
              <a-select-option :value="4">访客</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="参与状态" name="participationStatus">
            <a-select
              v-model:value="formData.participationStatus"
              placeholder="请选择参与状态"
              :disabled="isView"
            >
              <a-select-option :value="1">活跃</a-select-option>
              <a-select-option :value="2">非活跃</a-select-option>
              <a-select-option :value="3">被禁言</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="排序号" name="orderNum">
            <a-input-number
              v-model:value="formData.orderNum"
              placeholder="请输入排序号"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="通知类型" name="notificationType">
            <a-select
              v-model:value="formData.notificationType"
              placeholder="请选择通知类型"
              :disabled="isView"
            >
              <a-select-option :value="1">全部通知</a-select-option>
              <a-select-option :value="2">重要通知</a-select-option>
              <a-select-option :value="3">不通知</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="是否接收通知" name="receiveNotification">
            <a-switch
              :checked="formData.receiveNotification === 1"
              :disabled="isView"
              @change="(checked: boolean) => formData.receiveNotification = checked ? 1 : 0"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="参与备注" name="participationRemark">
        <a-textarea
          v-model:value="formData.participationRemark"
          placeholder="请输入参与备注"
          :rows="3"
          :disabled="isView"
        />
      </a-form-item>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          placeholder="请输入备注"
          :rows="3"
          :disabled="isView"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import {
  getNewsTopicParticipantById,
  createNewsTopicParticipant,
  updateNewsTopicParticipant
} from '@/api/routine/news/topic-participant'
import type {
  TaktNewsTopicParticipant,
  TaktNewsTopicParticipantCreate,
  TaktNewsTopicParticipantUpdate
} from '@/types/routine/news/topic-participant'

// Props
interface Props {
  visible: boolean
  title: string
  participantId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  participantId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  success: []
}>()

// 响应式数据
const loading = ref(false)
const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<TaktNewsTopicParticipantCreate & { id?: number }>({
  topicId: 0,
  userId: 0,
  userName: '',
  userAvatar: '',
  participationType: 3,
  participationStatus: 1,
  participationRemark: '',
  receiveNotification: 1,
  notificationType: 1,
  orderNum: 0,
  remark: ''
})

// 表单验证规则
const rules = {
  topicId: [{ required: true, message: '请输入话题ID', type: 'number' }],
  userId: [{ required: true, message: '请输入用户ID', type: 'number' }],
  userName: [{ required: true, message: '请输入用户姓名', type: 'string' }],
  participationType: [{ required: true, message: '请选择参与类型', type: 'number' }],
  participationStatus: [{ required: true, message: '请选择参与状态', type: 'number' }]
}

// 计算属性
const isView = computed(() => props.title.includes('查看'))

// 监听visible变化
watch(
  () => props.visible,
  (visible) => {
    if (visible && props.participantId) {
      getDetail()
    } else if (visible && !props.participantId) {
      resetForm()
    }
  }
)

// 获取详情
const getDetail = async () => {
  if (!props.participantId) return
  
  loading.value = true
  try {
    const res = await getNewsTopicParticipantById(props.participantId)
    if (res.data?.data) {
      const data = res.data.data
      Object.assign(formData, {
        id: data.id,
        topicId: data.topicId,
        userId: data.userId,
        userName: data.userName,
        userAvatar: data.userAvatar,
        participationType: data.participationType,
        participationStatus: data.participationStatus,
        participationRemark: data.participationRemark,
        receiveNotification: data.receiveNotification,
        notificationType: data.notificationType,
        orderNum: data.orderNum,
        remark: data.remark
      })
    }
  } catch (error) {
    message.error('获取详情失败')
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(formData, {
    id: undefined,
    topicId: 0,
    userId: 0,
    userName: '',
    userAvatar: '',
    participationType: 3,
    participationStatus: 1,
    participationRemark: '',
    receiveNotification: 1,
    notificationType: 1,
    orderNum: 0,
    remark: ''
  })
  formRef.value?.clearValidate()
}

// 提交表单
const handleSubmit = async () => {
  if (isView.value) {
    handleCancel()
    return
  }

  try {
    await formRef.value?.validate()
    loading.value = true

    if (props.participantId) {
      // 更新
      const updateData: TaktNewsTopicParticipantUpdate = {
        id: props.participantId,
        topicId: formData.topicId,
        userId: formData.userId,
        userName: formData.userName,
        userAvatar: formData.userAvatar,
        participationType: formData.participationType,
        participationStatus: formData.participationStatus,
        participationRemark: formData.participationRemark,
        receiveNotification: formData.receiveNotification,
        notificationType: formData.notificationType,
        orderNum: formData.orderNum,
        remark: formData.remark
      }
      await updateNewsTopicParticipant(updateData)
      message.success('更新成功')
    } else {
      // 新增
      const createData: TaktNewsTopicParticipantCreate = {
        topicId: formData.topicId,
        userId: formData.userId,
        userName: formData.userName,
        userAvatar: formData.userAvatar,
        participationType: formData.participationType,
        participationStatus: formData.participationStatus,
        participationRemark: formData.participationRemark,
        receiveNotification: formData.receiveNotification,
        notificationType: formData.notificationType,
        orderNum: formData.orderNum,
        remark: formData.remark
      }
      await createNewsTopicParticipant(createData)
      message.success('创建成功')
    }

    emit('success')
  } catch (error) {
    console.error('表单提交失败:', error)
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}
</script>

<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 16px;
}
</style> 
