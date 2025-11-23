<template>
  <a-modal
    :open="visible"
    :title="title"
    :confirm-loading="loading"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item
        :label="t('quartz.field.jobName')"
        name="jobName"
      >
        <a-input
          v-model:value="formState.quartzName"
          :placeholder="t('quartz.placeholder.jobName')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobGroup')"
        name="jobGroup"
      >
        <a-input
          v-model:value="formState.quartzGroupName"
          :placeholder="t('quartz.placeholder.jobGroup')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobClassName')"
        name="jobClassName"
      >
        <a-input
          v-model:value="formState.quartzClassName"
          :placeholder="t('quartz.placeholder.jobClassName')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobMethodName')"
        name="jobMethodName"
      >
        <a-input
          v-model:value="formState.quartzApiUrl"
          :placeholder="t('quartz.placeholder.jobMethodName')"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobCronExpression')"
        name="jobCronExpression"
      >
        <a-input
          v-model:value="formState.quartzCronExpression"
          :placeholder="t('quartz.placeholder.jobCronExpression')"
          allow-clear
        >
          <template #suffix>
            <a-tooltip :title="t('quartz.cronHelp')">
              <question-circle-outlined />
            </a-tooltip>
          </template>
        </a-input>
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobParams')"
        name="jobParams"
      >
        <a-textarea
          v-model:value="formState.quartzExecuteParams"
          :placeholder="t('quartz.placeholder.jobParams')"
          :auto-size="{ minRows: 3, maxRows: 5 }"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobDescription')"
        name="jobDescription"
      >
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('quartz.placeholder.jobDescription')"
          :auto-size="{ minRows: 3, maxRows: 5 }"
          allow-clear
        />
      </a-form-item>
      <a-form-item
        :label="t('quartz.field.jobStatus')"
        name="jobStatus"
      >
        <Takt-select
          v-model:value="formState.status"
          dict-type="quartz_job_status"
          type="radio"
          :placeholder="t('quartz.placeholder.jobStatus')"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { QuestionCircleOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktQuartzCreate, TaktQuartzUpdate } from '@/types/routine/quartz/quartz'
import {
  getQuartzJobDetail,
  createQuartzJob,
  updateQuartzJob
} from '@/api/routine/quartz/quartz'
import { message } from 'ant-design-vue'

const props = defineProps<{
  visible: boolean
  title: string
  jobId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单实例
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const formState = ref<Partial<TaktQuartzCreate>>({
  quartzName: '',
  quartzGroupName: '',
  quartzType: 0,
  quartzAssemblyName: '',
  quartzClassName: '',
  quartzApiUrl: '',
  quartzRequestMethod: '',
  quartzSql: '',
  quartzTriggerType: 0,
  quartzCronExpression: '',
  quartzInterval: 0,
  quartzExecuteParams: '',
  quartzConcurrent: 0,
  quartzStartTime: new Date(),
  quartzEndTime: new Date(),
  status: 0,
  remark: ''
})

// 表单验证规则
const rules = {
  jobName: [
    { required: true, message: t('quartz.rules.jobName.required') }
  ],
  jobGroup: [
    { required: true, message: t('quartz.rules.jobGroup.required') }
  ],
  jobClassName: [
    { required: true, message: t('quartz.rules.jobClassName.required') }
  ],
  jobMethodName: [
    { required: true, message: t('quartz.rules.jobMethodName.required') }
  ],
  jobCronExpression: [
    { required: true, message: t('quartz.rules.jobCronExpression.required') }
  ],
  jobStatus: [
    { required: true, message: t('quartz.rules.jobStatus.required') }
  ]
}

// 监听任务ID变化
watch(
  () => props.jobId,
  async (newVal) => {
    if (newVal) {
      loading.value = true
      try {
        const res = await getQuartzJobDetail(newVal)
        formState.value = {
          quartzName: res.data.data.quartzName,
          quartzGroupName: res.data.data.quartzGroupName,
          quartzType: res.data.data.quartzType,
          quartzAssemblyName: res.data.data.quartzAssemblyName,
          quartzClassName: res.data.data.quartzClassName,
          quartzApiUrl: res.data.data.quartzApiUrl,
          quartzRequestMethod: res.data.data.quartzRequestMethod,
          quartzSql: res.data.data.quartzSql,
          quartzTriggerType: res.data.data.quartzTriggerType,
          quartzCronExpression: res.data.data.quartzCronExpression,
          quartzInterval: res.data.data.quartzInterval,
          quartzExecuteParams: res.data.data.quartzExecuteParams,
          quartzConcurrent: res.data.data.quartzConcurrent,
          quartzStartTime: res.data.data.quartzStartTime,
          quartzEndTime: res.data.data.quartzEndTime,
          status: res.data.data.status,
          remark: res.data.data.remark
        }
      } catch (error) {
        console.error('获取定时任务详情失败:', error)
      } finally {
        loading.value = false
      }
    } else {
      formState.value = {
        quartzName: '',
        quartzGroupName: '',
        quartzType: 0,
        quartzAssemblyName: '',
        quartzClassName: '',
        quartzApiUrl: '',
        quartzRequestMethod: '',
        quartzSql: '',
        quartzTriggerType: 0,
        quartzCronExpression: '',
        quartzInterval: 0,
      }
    }
  },
  { immediate: true }
)

// 确定按钮点击
const handleOk = async () => {
  try {
    await formRef.value?.validateFields(['jobName', 'jobGroup', 'jobClassName', 'cronExpression', 'jobStatus', 'remark'], {
      validateMessages: {
        required: '${label}不能为空'
      }
    });
    // 表单验证通过，继续处理提交逻辑
    const formData = formState.value;
    if (props.jobId) {
      // 更新
      await updateQuartzJob(props.jobId, {
        ...formData,
        quartzId: props.jobId
      } as TaktQuartzUpdate);
      message.success(t('routine.quartz.result.updateSuccess'));
    } else {
      // 新增
      await createQuartzJob({
        ...formData,
        jobCreatorId: BigInt(0), // 这些值应该从用户信息中获取
        jobCreatorName: ''
      } as TaktQuartzCreate);
      message.success(t('routine.quartz.result.createSuccess'));
    }
    emit('success');
    emit('update:visible', false);
  } catch (error) {
    console.error('表单验证失败:', error);
  }
};

// 取消按钮点击
const handleCancel = () => {
  emit('update:visible', false)
}
</script> 
