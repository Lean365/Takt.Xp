<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="600"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('identity.post.fields.postCode.label')" name="postCode">
            <a-input
              v-model:value="form.postCode"
              :placeholder="t('identity.post.fields.postCode.placeholder')"
              :disabled="!!props.postId"
              show-count
              :maxlength="64"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('identity.post.fields.postName.label')" name="postName">
            <a-input
              v-model:value="form.postName"
              :placeholder="t('identity.post.fields.postName.placeholder')"
              show-count
              :maxlength="50"
              allow-clear
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('identity.post.fields.postSort.label')" name="orderNum">
            <a-input-number
              v-model:value="form.orderNum"
              :placeholder="t('identity.post.fields.postSort.placeholder')"
              style="width: 100%"
              :min="0"
              :max="9999"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('identity.post.fields.postStatus.label')" name="postStatus">
            <Takt-select
              v-model:value="form.postStatus"
              dict-type="sys_normal_disable"
              type="radio"
              :placeholder="t('identity.post.fields.postStatus.placeholder')"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item :label="t('identity.post.fields.remark.label')" name="remark">
            <a-textarea
              v-model:value="form.remark"
              :placeholder="t('identity.post.fields.remark.placeholder')"
              :rows="4"
              show-count
              :maxlength="500"
              allow-clear
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TaktPost } from '@/types/identity/post'
import { getByIdAsync, createPost, updatePost } from '@/api/identity/post'

const props = defineProps<{
  visible: boolean
  title: string
  postId?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单校验规则
const rules: Record<string, Rule[]> = {
  postCode: [
    { required: true, message: t('identity.post.fields.postCode.validation.required') },
    { max: 64, message: t('identity.post.fields.postCode.validation.maxLength') }
  ],
  postName: [
    { required: true, message: t('identity.post.fields.postName.validation.required') },
    { max: 50, message: t('identity.post.fields.postName.validation.maxLength') }
  ],
  orderNum: [
    { required: true, message: t('identity.post.fields.postSort.validation.required') },
    { type: 'number', message: t('identity.post.fields.postSort.validation.type') }
  ],
  postStatus: [{ required: true, message: t('identity.post.fields.postStatus.validation.required') }],
  remark: [{ max: 500, message: t('identity.post.fields.remark.validation.maxLength') }]
}

// 表单数据
const form = ref<Partial<TaktPost>>({
  postId: undefined,
  postCode: '',
  postName: '',
  orderNum: 0,
  status: 0,
  remark: ''
})

// 监听岗位ID变化
watch(
  () => props.postId,
  async (newVal: string | undefined) => {
    if (newVal) {
      try {
        loading.value = true
        const res = await getByIdAsync(newVal)
        if (res.data) {
          const { postId, ...rest } = res.data
          form.value = rest
        } else {
          message.error(t('common.failed'))
        }
      } catch (error) {
        console.error('[岗位管理] 获取岗位详情出错:', error)
        message.error(t('common.failed'))
      } finally {
        loading.value = false
      }
    } else {
      resetForm()
    }
  }
)

// 重置表单
const resetForm = () => {
  form.value = {
    postId: undefined,
    postCode: '',
    postName: '',
    orderNum: 0,
    status: 0,
    remark: ''
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
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    if (props.postId) {
      const updateData: TaktPost = {
        ...form.value,
        postId: props.postId
      } as TaktPost
      const res = await updatePost(updateData)
      if (res.data) {
        message.success(t('common.update.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        message.error(t('common.update.failed'))
      }
    } else {
      const res = await createPost(form.value as TaktPost)
      if (res.data) {
        message.success(t('common.create.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        message.error(t('common.create.failed'))
      }
    }
  } catch (error) {
    console.error('[岗位管理] 提交表单出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

defineExpose({
  resetForm
})
</script> 
