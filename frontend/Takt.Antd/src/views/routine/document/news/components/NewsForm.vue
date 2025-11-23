<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :confirm-loading="loading"
    :width="800"
    @update:visible="handleVisibleChange"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item name="title" label="新闻标题">
            <a-input
              v-model:value="formData.title"
              placeholder="请输入新闻标题"
              allow-clear
              show-count
              :maxlength="100"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="author" label="作者">
            <a-input
              v-model:value="formData.author"
              placeholder="请输入作者"
              allow-clear
              :maxlength="50"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="publishTime" label="发布时间">
            <a-date-picker
              v-model:value="formData.publishTime"
              placeholder="请选择发布时间"
              show-time
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="content" label="新闻内容">
            <a-textarea
              v-model:value="formData.content"
              placeholder="请输入新闻内容"
              :rows="6"
              show-count
              :maxlength="2000"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="status" label="状态">
            <Takt-select
              v-model:value="formData.status"
              dict-type="sys_normal_disable"
              placeholder="请选择状态"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="remark" label="备注">
            <a-textarea
              v-model:value="formData.remark"
              placeholder="请输入备注"
              :rows="3"
              :maxlength="500"
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import dayjs from 'dayjs'

const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  title: string
  newsId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const formData = ref({
  title: '',
  author: '',
  publishTime: dayjs(),
  content: '',
  status: 0,
  remark: ''
})

// 校验规则
const rules: Record<string, Rule[]> = {
  title: [
    { required: true, message: '请输入新闻标题', trigger: 'blur' },
    { max: 100, message: '新闻标题不能超过100个字符' }
  ],
  author: [
    { required: true, message: '请输入作者', trigger: 'blur' },
    { max: 50, message: '作者不能超过50个字符' }
  ],
  content: [
    { required: true, message: '请输入新闻内容', trigger: 'blur' },
    { max: 2000, message: '新闻内容不能超过2000个字符' }
  ],
  status: [
    { required: true, message: '请选择状态', trigger: 'change' }
  ]
}

// 重置表单
const resetForm = () => {
  formData.value = {
    title: '',
    author: '',
    publishTime: dayjs(),
    content: '',
    status: 0,
    remark: ''
  }
  formRef.value?.resetFields()
}

// 监听新闻ID变化，加载或重置表单
watch(
  () => props.newsId,
  async (newVal: number | undefined) => {
    if (newVal) {
      try {
        // TODO: 调用API获取新闻详情
        // const res = await getNews(newVal)
        // if (res.data.code === 200) {
        //   const data = res.data.data
        //   if (data.publishTime) {
        //     data.publishTime = dayjs(data.publishTime)
        //   }
        //   formData.value = data
        // } else {
        //   message.error(res.data.msg || t('common.failed'))
        // }
        
        // 模拟数据
        formData.value = {
          title: '示例新闻标题',
          author: '管理员',
          publishTime: dayjs(),
          content: '这是示例新闻内容...',
          status: 0,
          remark: '示例备注'
        }
      } catch (error) {
        console.error('[新闻管理] 获取新闻详情出错:', error)
        message.error(t('common.failed'))
      }
    } else {
      resetForm()
    }
  },
  { immediate: true }
)

// 弹窗显示/关闭
const handleVisibleChange = (val: boolean) => {
  if (!val) {
    resetForm()
  }
  emit('update:visible', val)
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 提交
const handleOk = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    const data = { ...formData.value }
    if (data.publishTime) {
      data.publishTime = dayjs(data.publishTime).format('YYYY-MM-DD HH:mm:ss')
    }
    
    // TODO: 调用API保存新闻
    // let res
    // if (!props.newsId) {
    //   res = await createNews(data)
    // } else {
    //   res = await updateNews({ ...data, id: props.newsId })
    // }
    // 
    // if (res.data.code === 200) {
    //   message.success(props.newsId ? t('common.update.success') : t('common.create.success'))
    //   emit('success')
    //   emit('update:visible', false)
    // } else {
    //   message.error(res.data.msg || (props.newsId ? t('common.update.failed') : t('common.create.failed')))
    // }
    
    // 模拟成功
    message.success(props.newsId ? '更新成功' : '创建成功')
    emit('success')
    emit('update:visible', false)
  } catch (error) {
    console.error('[新闻管理] 提交表单出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}
</script> 
