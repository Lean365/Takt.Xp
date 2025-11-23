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
      :label-col="{ span: 7 }"
      :wrapper-col="{ span: 15 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('schedule.title')" name="title">
            <a-input v-model:value="form.title" :placeholder="t('schedule.placeholder.title')" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('schedule.location')" name="location">
            <a-input v-model:value="form.location" :placeholder="t('schedule.placeholder.location')" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item :label="t('schedule.content')" name="content">
            <a-input v-model:value="form.content" :placeholder="t('schedule.placeholder.content')" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('schedule.startTime')" name="startTime">
            <a-date-picker v-model:value="form.startTime" show-time style="width: 100%" :placeholder="t('schedule.placeholder.startTime')" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('schedule.endTime')" name="endTime">
            <a-date-picker v-model:value="form.endTime" show-time style="width: 100%" :placeholder="t('schedule.placeholder.endTime')" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item :label="t('schedule.priority')" name="priority">
            <Takt-select v-model:value="form.priority" dict-type="schedule_priority" :placeholder="t('schedule.placeholder.priority')" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('schedule.status')" name="status">
            <Takt-select v-model:value="form.status" dict-type="schedule_status" :placeholder="t('schedule.placeholder.status')" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item :label="t('table.columns.remark')" name="remark">
            <a-textarea v-model:value="form.remark" :placeholder="t('table.columns.remark')" :rows="3" />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
// import { getSchedule, createSchedule, updateSchedule } from '@/api/routine/schedule'

const props = defineProps<{
  visible: boolean
  title: string
  scheduleId?: number
}>()
const emit = defineEmits(['update:open', 'success'])
const { t } = useI18n()

const loading = ref(false)
const formRef = ref()
const form = ref({
  title: '',
  content: '',
  location: '',
  startTime: '',
  endTime: '',
  priority: 0,
  status: 0,
  remark: ''
})

const rules = {
  title: [
    { required: true, message: t('schedule.validate.title'), trigger: 'blur', type: 'string' }
  ],
  startTime: [
    { required: true, message: t('schedule.validate.startTime'), trigger: 'change', type: 'object' }
  ],
  endTime: [
    { required: true, message: t('schedule.validate.endTime'), trigger: 'change', type: 'object' }
  ]
} as Record<string, any>

// 获取日程信息
const getInfo = async (scheduleId: number) => {
  try {
    // const res = await getSchedule(scheduleId)
    // if (res.data.code === 200) {
    //   Object.assign(form.value, res.data.data)
    // } else {
    //   message.error(res.data.msg || t('common.failed'))
    // }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 弹窗显示变化时，重置或加载数据
watch(() => props.visible, async (val) => {
  if (val) {
    if (props.scheduleId) {
      // 编辑，加载数据
      await getInfo(props.scheduleId)
    } else {
      // 新增，重置表单
      nextTick(() => {
        formRef.value?.resetFields()
      })
      form.value = {
        title: '',
        content: '',
        location: '',
        startTime: '',
        endTime: '',
        priority: 0,
        status: 0,
        remark: ''
      }
    }
  }
})

const handleVisibleChange = (val: boolean) => {
  emit('update:open', val)
}

const handleSubmit = async () => {
  await formRef.value.validate()
  loading.value = true
  try {
    if (props.scheduleId) {
      // await updateSchedule({ ...form.value, scheduleId: props.scheduleId })
      message.success(t('common.updateSuccess'))
    } else {
      // await createSchedule(form.value)
      message.success(t('common.addSuccess'))
    }
    emit('update:open', false)
    emit('success')
  } catch (error) {
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  emit('update:open', false)
}
</script>

