<template>
  <a-modal
    :open="open"
    :title="title"
    :width="800"
    :confirm-loading="submitLoading"
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
          <a-form-item label="生产时段" name="timePeriod">
            <a-input v-model:value="form.timePeriod" placeholder="请输入生产时段" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="实际生产数量" name="prodActualQty">
            <a-input-number
              v-model:value="form.prodActualQty"
              style="width: 100%"
              placeholder="请输入实际生产数量"
              :min="0"
              :precision="3"
            />
          </a-form-item>
        </a-col>
      </a-row>
      
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="停线时间(分钟)" name="downtimeMinutes">
            <a-input-number
              v-model:value="form.downtimeMinutes"
              style="width: 100%"
              placeholder="请输入停线时间"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="停线原因" name="downtimeReason">
            <a-input v-model:value="form.downtimeReason" placeholder="请输入停线原因" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="停线说明" name="downtimeDescription">
            <a-input v-model:value="form.downtimeDescription" placeholder="请输入停线说明" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="未达成原因" name="unachievedReason">
            <a-input v-model:value="form.unachievedReason" placeholder="请输入未达成原因" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="未达成说明" name="unachievedDescription">
            <a-input v-model:value="form.unachievedDescription" placeholder="请输入未达成说明" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="投入工时(分钟)" name="inputMinutes">
            <a-input-number
              v-model:value="form.inputMinutes"
              style="width: 100%"
              placeholder="请输入投入工时"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="生产工时(分钟)" name="prodMinutes">
            <a-input-number
              v-model:value="form.prodMinutes"
              style="width: 100%"
              placeholder="请输入生产工时"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="实际工时(分钟)" name="actualMinutes">
            <a-input-number
              v-model:value="form.actualMinutes"
              style="width: 100%"
              placeholder="请输入实际工时"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="达成率(%)" name="achievementRate">
            <a-input-number
              v-model:value="form.achievementRate"
              style="width: 100%"
              placeholder="请输入达成率"
              :min="0"
              :max="100"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :rows="3"
          placeholder="请输入备注信息"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktAssyOutputDetail } from '@/types/logistics/manufacturing/execution/output/assyOutputDetail'
import {
  createAssyOutputDetail,
  updateAssyOutputDetail
} from '@/api/logistics/manufacturing/execution/output/assyOutputDetail'

interface Props {
  open: boolean
  title: string
  assyOutputId: number
  assyOutputDetailId?: number
}

interface Emits {
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
  (e: 'cancel'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const formRef = ref<FormInstance>()
const submitLoading = ref(false)

const form = reactive<Partial<TaktAssyOutputDetail>>({
  assyOutputDetailId: undefined,
  assyOutputId: props.assyOutputId,
  timePeriod: '',
  prodActualQty: 0,
  downtimeMinutes: 0,
  downtimeReason: '',
  downtimeDescription: '',
  unachievedReason: '',
  unachievedDescription: '',
  inputMinutes: 0,
  prodMinutes: 0,
  actualMinutes: 0,
  achievementRate: 0,
  remark: ''
})

const rules: Record<string, any> = {
  timePeriod: [{ required: true, message: '请输入生产时段', trigger: 'blur' }],
  prodActualQty: [{ required: true, message: '请输入实际生产数量', trigger: 'blur' }],
  downtimeMinutes: [{ required: true, message: '请输入停线时间', trigger: 'blur' }],
  inputMinutes: [{ required: true, message: '请输入投入工时', trigger: 'blur' }],
  prodMinutes: [{ required: true, message: '请输入生产工时', trigger: 'blur' }],
  actualMinutes: [{ required: true, message: '请输入实际工时', trigger: 'blur' }],
  achievementRate: [{ required: true, message: '请输入达成率', trigger: 'blur' }]
}

// 监听assyMpOutputId变化
watch(() => props.assyOutputId, (newVal) => {
  form.assyOutputId = newVal
})

// 监听弹窗打开状态
watch(() => props.open, (newVal) => {
  if (newVal) {
    resetForm()
  }
})

// 重置表单
const resetForm = () => {
  form.assyOutputDetailId = undefined
  form.assyOutputId = props.assyOutputId
  form.timePeriod = ''
  form.prodActualQty = 0
  form.downtimeMinutes = 0
  form.downtimeReason = ''
  form.downtimeDescription = ''
  form.unachievedReason = ''
  form.unachievedDescription = ''
  form.inputMinutes = 0
  form.prodMinutes = 0
  form.actualMinutes = 0
  form.achievementRate = 0
  form.remark = ''
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    submitLoading.value = true

    if (form.assyOutputDetailId) {
      // 更新
      const res = await updateAssyOutputDetail(form as TaktAssyOutputDetail)
      if (res.data.code === 200) {
        message.success('更新成功')
        emit('success')
      } else {
        message.error(res.data.msg || '更新失败')
      }
    } else {
      // 新增
      const res = await createAssyOutputDetail(form as TaktAssyOutputDetail)
      if (res.data.code === 200) {
        message.success('创建成功')
        emit('success')
      } else {
        message.error(res.data.msg || '创建失败')
      }
    }
  } catch (error) {
    console.error('提交失败:', error)
    message.error('提交失败')
  } finally {
    submitLoading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:open', false)
  emit('cancel')
}
</script>

