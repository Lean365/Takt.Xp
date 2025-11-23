<template>
  <Takt-modal
    v-model:open="visible"
    :title="title"
    :width="800"
    :loading="loading"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="字段名" name="columnName">
        <a-input v-model:value="formData.columnName" placeholder="请输入字段名" />
      </a-form-item>
      <a-form-item label="字段说明" name="columnComment">
        <a-input v-model:value="formData.columnComment" placeholder="请输入字段说明" />
      </a-form-item>
      <a-form-item label="数据库列类型" name="dbColumnType">
        <a-select v-model:value="formData.dbColumnType" placeholder="请选择数据库列类型">
          <a-select-option value="varchar">varchar</a-select-option>
          <a-select-option value="char">char</a-select-option>
          <a-select-option value="text">text</a-select-option>
          <a-select-option value="int">int</a-select-option>
          <a-select-option value="bigint">bigint</a-select-option>
          <a-select-option value="decimal">decimal</a-select-option>
          <a-select-option value="datetime">datetime</a-select-option>
          <a-select-option value="date">date</a-select-option>
          <a-select-option value="time">time</a-select-option>
          <a-select-option value="bit">bit</a-select-option>
          <a-select-option value="float">float</a-select-option>
          <a-select-option value="double">double</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="是否主键" name="isPrimaryKey">
        <a-switch v-model:checked="formData.isPrimaryKey" />
      </a-form-item>
      <a-form-item label="是否必填" name="isRequired">
        <a-switch v-model:checked="formData.isRequired" />
      </a-form-item>
      <a-form-item label="是否自增" name="isIncrement">
        <a-switch v-model:checked="formData.isIncrement" />
      </a-form-item>
      <a-form-item label="列长度" name="columnLength">
        <a-input-number v-model:value="formData.columnLength" :min="0" />
      </a-form-item>
      <a-form-item label="小数位数" name="decimalDigits">
        <a-input-number v-model:value="formData.decimalDigits" :min="0" />
      </a-form-item>
      <a-form-item label="默认值" name="defaultValue">
        <a-input v-model:value="formData.defaultValue" placeholder="请输入默认值" />
      </a-form-item>
      <a-form-item label="排序" name="orderNum">
        <a-input-number v-model:value="formData.orderNum" :min="0" />
      </a-form-item>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TaktGenColumnDefine, TaktGenColumnDefineCreate, TaktGenColumnDefineUpdate } from '@/types/generator/genColumnDefine'
import { getColumnDefineList, createColumnDefine, updateColumnDefine } from '@/api/generator/genTableDefine'

const props = defineProps<{
  open: boolean
  title: string
  tableId: string
  columnId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单数据
const formData = reactive<TaktGenColumnDefineCreate>({
  tableId: props.tableId,
  columnName: '',
  columnComment: '',
  dbColumnType: '',
  isPrimaryKey: 0,
  isRequired: 0,
  isIncrement: 0,
  columnLength: 0,
  decimalDigits: 0,
  defaultValue: '',
  orderNum: 0
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  columnName: [{ required: true, message: '请输入字段名', trigger: 'blur' }],
  columnComment: [{ required: true, message: '请输入字段说明', trigger: 'blur' }],
  dbColumnType: [{ required: true, message: '请选择数据库列类型', trigger: 'change' }]
}

// 监听visible变化
watch(
  () => props.open,
  (val) => {
    if (val) {
      if (props.columnId) {
        getDetail()
      } else {
        resetForm()
      }
    }
  }
)

/** 获取详情 */
const getDetail = async () => {
  if (!props.columnId) return
  loading.value = true
  try {
    const res = await getColumnDefineList({
      tableId: props.tableId,
      columnName: props.columnId.toString(),
      pageIndex: 1,
      pageSize: 1
    })
    if (res.data?.rows?.length > 0) {
      Object.assign(formData, res.data.rows[0])
    }
  } finally {
    loading.value = false
  }
}

/** 重置表单 */
const resetForm = () => {
  formRef.value?.resetFields()
  Object.assign(formData, {
    tableId: props.tableId,
    columnName: '',
    columnComment: '',
    dbColumnType: '',
    isPrimaryKey: 0,
    isRequired: 0,
    isIncrement: 0,
    columnLength: 0,
    decimalDigits: 0,
    defaultValue: '',
    orderNum: 0
  })
}

/** 提交表单 */
const handleSubmit = async () => {
  if (!formRef.value) return
  await formRef.value.validate()
  loading.value = true
  try {
    if (props.columnId) {
      await updateColumnDefine({
        ...formData,
        genColumnDefineId: props.columnId
      })
    } else {
      await createColumnDefine(formData)
    }
    emit('success')
  } finally {
    loading.value = false
  }
}

/** 取消 */
const handleCancel = () => {
  emit('update:open', false)
  resetForm()
}
</script> 
