<template>
  <a-modal
    :open="visible"
    :title="t('identity.user.allocateRole')"
    :confirm-loading="confirmLoading"
    @update:open="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-transfer
      v-model:targetKeys="targetKeys"
      :data-source="roleList"
      :titles="[t('identity.user.roleAllocate.unallocated'), t('identity.user.roleAllocate.allocated')]"
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
import type { TaktRole } from '@/types/identity/role'
import { getRoleOptions } from '@/api/identity/role'
import { getUserRoles, assignUserRoles } from '@/api/identity/user'

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

// 角色列表
const roleList = ref<TransferItem[]>([])
// 已选角色
const targetKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 加载角色列表
const loadRoleList = async () => {
  try {
    const res = await getRoleOptions()
    if (res.data) {
      roleList.value = res.data.map(role => ({
        key: role.dictValue.toString(),
        title: role.dictLabel,
        description: ''
      }))
    }
  } catch (err) {
    console.error(t('identity.user.roleAllocate.loadRoleListFailed'), err)
  }
}

// 加载用户角色
const loadUserRoles = async () => {
  if (!props.userId) return
  try {
    const res = await getUserRoles(props.userId)
    if (res.data) {
      targetKeys.value = res.data.map(role => role.roleId.toString())
    }
  } catch (err) {
    console.error(t('identity.user.roleAllocate.loadUserRolesFailed'), err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.userId) return
  try {
    const res = await assignUserRoles(
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
    if (val) {
      loadRoleList()
      loadUserRoles()
    } else {
      targetKeys.value = []
    }
  }
)
</script> 
