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
import { getUserDepts, assignUserDepts } from '@/api/identity/dept'
import { useI18n } from 'vue-i18n'

interface Props {
  visible: boolean
  deptId?: string
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  deptId: undefined
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

// 加载部门用户
const loadDeptUsers = async () => {
  if (!props.deptId) return
  try {
    console.log('开始加载部门用户, deptId:', props.deptId)
    const res = await getUserDepts(props.deptId, {})
    console.log('部门用户API响应:', res)
    if (res.data && res.data.items) {
      // API返回的是分页结果，需要访问items属性
      targetKeys.value = res.data.items.map((user: any) => user.userId.toString())
      console.log('已选用户ID列表:', targetKeys.value)
    }
  } catch (err) {
    console.error('加载部门用户失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.deptId) return
  try {
    const res = await assignUserDepts(
      props.deptId,
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
    console.log('DeptAllocate visible changed:', val, 'deptId:', props.deptId)
    if (val) {
      loadUserList()
      loadDeptUsers()
    } else {
      targetKeys.value = []
    }
  }
)
</script>

<style lang="less" scoped>
</style>

