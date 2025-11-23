<template>
  <a-modal
    :open="visible"
    title="分配用户"
    :confirm-loading="confirmLoading"
    @update:open="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-transfer
      v-model:targetKeys="targetKeys"
      :data-source="userList"
      :titles="['未分配', '已分配']"
      :render="(item) => item.title"
      :disabled="false"
      :default-expand-all="true"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TransferItem } from 'ant-design-vue/es/transfer'
import type { TaktUser } from '@/types/identity/user'
import { getUserOptions } from '@/api/identity/user'
import { getUserPosts, assignUserPosts } from '@/api/identity/post'
import { useI18n } from 'vue-i18n'

interface Props {
  visible: boolean
  postId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  postId: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const i18n = useI18n()

// 用户列表
const userList = ref<TransferItem[]>([])
// 已选用户
const targetKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 加载用户列表
const loadUserList = async () => {
  try {
    console.log('开始加载用户列表')
    const res = await getUserOptions()
    console.log('用户选项API响应:', res)
    if (res.data) {
      // 获取所有用户选项
      userList.value = res.data.map((user: any) => ({
        key: user.dictValue.toString(),
        title: user.dictLabel,
        description: ''
      }))
      console.log('用户列表数据:', userList.value)
    }
  } catch (err) {
    console.error('加载用户列表失败:', err)
  }
}

// 加载岗位用户
const loadPostUsers = async () => {
  if (!props.postId) return
  try {
    console.log('开始加载岗位用户, postId:', props.postId)
    const res = await getUserPosts(props.postId)
    console.log('岗位用户API响应:', res)
    if (res.data) {
      // API返回的是用户数组，所有用户都是已分配的
      targetKeys.value = res.data.map((user: any) => user.userId.toString())
      console.log('已选用户ID列表:', targetKeys.value)
    }
  } catch (err) {
    console.error('加载岗位用户失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.postId) return
  try {
    const res = await assignUserPosts(
      props.postId,
      targetKeys.value.map(key => key)
    )
    if (res.data) {
      message.success(i18n.t('common.success'))
      emit('success')
      targetKeys.value = []
      emit('update:visible', false)
    } else {
      message.error(i18n.t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(i18n.t('common.failed'))
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loadUserList()
      loadPostUsers()
    } else {
      targetKeys.value = []
    }
  }
)
</script>

<style lang="less" scoped>
</style> 
