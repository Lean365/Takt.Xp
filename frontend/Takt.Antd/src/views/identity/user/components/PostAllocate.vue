<template>
  <a-modal
    :open="visible"
    :title="t('identity.user.allocatePost')"
    :confirm-loading="confirmLoading"
    @update:open="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-transfer
      v-model:targetKeys="targetKeys"
      :data-source="postList"
      :titles="[t('identity.user.postAllocate.unallocated'), t('identity.user.postAllocate.allocated')]"
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
import type { TaktPost } from '@/types/identity/post'
import { getPostOptions } from '@/api/identity/post'
import { getUserPosts, assignUserPosts } from '@/api/identity/user'
import { useI18n } from 'vue-i18n'

interface Props {
  visible: boolean
  userId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  userId: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 岗位列表
const postList = ref<TransferItem[]>([])
// 已选岗位
const targetKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 加载岗位列表
const loadPostList = async () => {
  try {
    const res = await getPostOptions()
    if (res.data) {
      postList.value = res.data.map(post => ({
        key: post.dictValue.toString(),
        title: post.dictLabel,
        description: ''
      }))
    }
  } catch (err) {
    console.error(t('identity.user.postAllocate.loadPostListFailed'), err)
  }
}

// 加载用户岗位
const loadUserPosts = async () => {
  console.log(t('identity.user.postAllocate.startLoadUserPosts'), 'userId:', props.userId)
  if (!props.userId) {
    console.error('userId is undefined')
    return
  }
  try {
    const res = await getUserPosts(props.userId)
    console.log(t('identity.user.postAllocate.userPostsApiResponse'), res)
    if (res.data) {
      targetKeys.value = res.data.map(post => post.postId.toString())
      console.log(t('identity.user.postAllocate.setSelectedPosts'), targetKeys.value)
    }
  } catch (err) {
    console.error(t('identity.user.postAllocate.loadUserPostsFailed'), err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.userId) return
  try {
    const res = await assignUserPosts(
      props.userId,
      targetKeys.value.map(key => key)
    )
    if (res.data) {
      message.success(t('common.success'))
      emit('success')
      targetKeys.value = []
      emit('update:visible', false)
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    console.log('PostAllocate visible changed:', val, 'userId:', props.userId)
    if (val) {
      loadPostList()
      loadUserPosts()
    } else {
      targetKeys.value = []
    }
  }
)
</script> 

