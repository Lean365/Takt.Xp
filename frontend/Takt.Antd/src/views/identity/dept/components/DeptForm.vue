<template>
  <a-modal
    :open="visible"
    :title="title"
    :mask-closable="false"
    @ok="submitForm"
    @cancel="handleCancel"
    :confirm-loading="submitLoading"
    @update:visible="handleVisibleChange"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      label-align="right"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item :label="t('identity.dept.fields.parentId.label')" name="parentId">
        <dept-tree-select v-model:value="form.parentId" :exclude="deptId" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.fields.deptName.label')" name="deptName">
        <a-input v-model:value="form.deptName" :placeholder="t('identity.dept.fields.deptName.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.fields.orderNum.label')" name="orderNum">
        <a-input-number
          v-model:value="form.orderNum"
          :placeholder="t('identity.dept.fields.orderNum.placeholder')"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item :label="t('identity.dept.fields.leader.label')" name="leader">
        <a-input v-model:value="form.leader" :placeholder="t('identity.dept.fields.leader.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.fields.phone.label')" name="phone">
        <a-input v-model:value="form.phone" :placeholder="t('identity.dept.fields.phone.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.fields.email.label')" name="email">
        <a-input v-model:value="form.email" :placeholder="t('identity.dept.fields.email.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.fields.deptStatus.label')" name="deptStatus">
        <Takt-select
          v-model:value="form.deptStatus"
          dict-type="sys_normal_disable"
          type="radio"
          :show-all="false"
          :placeholder="t('identity.dept.fields.deptStatus.placeholder')"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TaktDept, TaktDeptCreate, TaktDeptUpdate } from '@/types/identity/dept'
import { getByIdAsync, createDept, updateDept } from '@/api/identity/dept'
import DeptTreeSelect from './DeptTreeSelect.vue'

const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  title: string
  deptId?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 提交按钮loading
const submitLoading = ref(false)

// 表单校验规则
const rules: Record<string, Rule[]> = {
  parentId: [{ required: true, message: t('identity.dept.rules.parentId'), trigger: 'change' }],
  deptName: [{ required: true, message: t('identity.dept.rules.deptName'), trigger: 'blur' }],
  orderNum: [{ required: true, message: t('identity.dept.rules.orderNum'), trigger: 'blur' }],
  email: [{ type: 'email', message: t('identity.dept.rules.email'), trigger: 'blur' }],
  phone: [{ pattern: /^1[3-9]\d{9}$/, message: t('identity.dept.rules.phone'), trigger: 'blur' }]
}

// 表单数据
const form = ref<TaktDeptCreate>({
  deptName: '',
  parentId: '0',
  orderNum: 0,
  leader: '',
  phone: '',
  email: '',
  status: 0,
  userCount: 0
})

// 监听部门ID变化
watch(
  () => props.deptId,
  async (newVal: string | undefined) => {
    if (newVal) {
      try {
        const res = await getByIdAsync(String(newVal))
        Object.assign(form.value, res.data)
      } catch (error) {
        console.error('[部门管理] 获取部门详情出错:', error)
        message.error(t('common.failed'))
      }
    } else {
      resetForm()
    }
  }
)

// 重置表单
const resetForm = () => {
  form.value = {
    deptName: '',
    parentId: '0',
    orderNum: 0,
    leader: '',
    phone: '',
    email: '',
    status: 0,
    userCount: 0
  }
  formRef.value?.resetFields()
}

// 处理弹窗显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    resetForm()
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 提交表单
const submitForm = async () => {
  try {
    await formRef.value?.validate()
    submitLoading.value = true

    if (props.deptId) {
      const updateData: TaktDeptUpdate = {
        ...form.value,
        deptId: props.deptId
      }
      const res = await updateDept(updateData)
      if (res.data) {
        message.success(t('common.update.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        message.error(t('common.update.failed'))
      }
    } else {
      const res = await createDept(form.value)
      if (typeof res.data === 'number' && res.data > 0) {
        message.success(t('common.create.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        message.error(t('common.create.failed'))
      }
    }
  } catch (error) {
    console.error('[部门管理] 提交表单出错:', error)
    message.error(t('common.failed'))
  } finally {
    submitLoading.value = false
  }
}

defineExpose({
  resetForm
})
</script> 
