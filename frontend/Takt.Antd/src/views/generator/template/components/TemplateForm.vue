<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :confirm-loading="submitLoading"
    @update:visible="handleVisibleChange"
    @ok="submitForm"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item :label="t('generator.template.fields.templateName')" name="templateName">
        <a-input v-model:value="form.templateName" :placeholder="t('generator.template.placeholder.templateName')" allow-clear 
        @input="(e: any) => handleTemplateNameInput(e.target.value)" />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.templateOrmType')" name="templateOrmType">  
        <Takt-select v-model:value="form.templateOrmType" dict-type="gen_orm_framework" type="radio" 
        :show-all="false" :placeholder="t('generator.template.placeholder.templateOrmType')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.templateCodeType')" name="templateCodeType">  
        <Takt-select v-model:value="form.templateCodeType" dict-type="gen_code_type" type="radio" 
        :show-all="false" :placeholder="t('generator.template.placeholder.templateCodeType')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.templateCategory')" name="templateCategory">  
        <Takt-select v-model:value="form.templateCategory" dict-type="gen_template_category" type="radio" 
        :show-all="false" :placeholder="t('generator.template.placeholder.templateCategory')" style="width: 100%" @update:value="handleCategoryChange" />
      </a-form-item>

      <a-form-item :label="t('generator.template.fields.templateLanguage')" name="templateLanguage">  
        <Takt-select v-model:value="form.templateLanguage" dict-type="gen_programming_language" type="radio" 
        :show-all="false" :placeholder="t('generator.template.placeholder.templateLanguage')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.templateVersion')" name="templateVersion">  
        <a-input v-model:value="form.templateVersion" :placeholder="t('generator.template.placeholder.templateVersion')" allow-clear />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.fileName')" name="fileName">
        <a-input v-model:value="form.fileName" :placeholder="t('generator.template.placeholder.fileName')" allow-clear disabled />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.templateContent')" name="content">
        <a-textarea
          v-model:value="form.templateContent"
          :rows="15"
          :placeholder="t('generator.template.placeholder.templateContent')"
          allow-clear
        />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.status')" name="status">
        <Takt-select v-model:value="form.status" dict-type="sys_normal_disable" type="radio" 
        :show-all="false" :placeholder="t('generator.config.placeholder.genTplType')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.template.fields.remark')" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :rows="4"
          :placeholder="t('generator.template.placeholder.remark')"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TaktGenTemplate } from '@/types/generator/genTemplate'
import { getGenTemplate as getTemplate, createGenTemplate as createTemplate, updateGenTemplate as updateTemplate } from '@/api/generator/genTemplate'

const props = defineProps<{
  visible: boolean
  title: string
  templateId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 提交按钮loading
const submitLoading = ref(false)

// 表单校验规则
const rules: Record<string, Rule[]> = {
  templateName: [
    { required: true, message: t('generator.template.placeholder.validation.required', { field: t('generator.template.fields.templateName') }) },
    { min: 4, message: t('generator.template.placeholder.validation.minLength', { field: t('generator.template.fields.templateName'), min: 4 }) },
    { max: 50, message: t('generator.template.placeholder.validation.length', { field: t('generator.template.fields.templateName'), length: 50 }) },
    { 
      pattern: /^[A-Z][a-zA-Z]*$/, 
      message: t('generator.template.placeholder.validation.pascalCase', { field: t('generator.template.fields.templateName') })
    }
  ],
  fileName: [
    { required: true, message: t('generator.template.placeholder.validation.required', { field: t('generator.template.fields.fileName') }) },
    { max: 100, message: t('generator.template.placeholder.validation.length', { field: t('generator.template.fields.fileName'), length: 100 }) }
  ],
  content: [
    { required: true, message: t('generator.template.placeholder.validation.required', { field: t('generator.template.fields.templateContent') }) }
  ],
  remark: [
    { max: 500, message: t('generator.template.placeholder.validation.length', { field: t('generator.template.fields.remark'), length: 500 }) }
  ]
}

// 表单数据
const form = ref<Partial<TaktGenTemplate>>({
  /** 模板名称 */
  templateName: '',
  /** 模板类型（1：后端代码，2：前端代码，3：SQL代码） */
  templateOrmType: 1,
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCodeType: 1,
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: 1,
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: 1,
  /** 版本号 */
  templateVersion: 1,
  /** 模板内容 */
  templateContent: '',
  /** 文件名 */
  fileName: '',
  /** 状态（0：停用，1：正常） */
  status: 0
})

// 重置表单
const resetForm = () => {
  form.value = {
  /** 模板名称 */
  templateName: '',
  /** 模板类型（1：后端代码，2：前端代码，3：SQL代码） */
  templateOrmType: 1,
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCodeType: 1,
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: 1,
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: 1,
  /** 版本号 */
  templateVersion: 1,
  /** 模板内容 */
  templateContent: '',
  /** 文件名 */
  fileName: '',
  /** 状态（0：停用，1：正常） */
  status: 0
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

    if (props.templateId) {
      console.log('[模板管理] 开始更新模板, id:', props.templateId)
      console.log('[模板管理] 更新数据:', form.value)
      
      const updateData: TaktGenTemplate = {
        ...form.value,
        id: props.templateId
      } as TaktGenTemplate
      
      console.log('[模板管理] 发送更新请求:', updateData)
      const res = await updateTemplate(updateData)
      console.log('[模板管理] 更新响应:', res)

      if (res.data.code === 200) {
        message.success(t('common.update.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        console.error('[模板管理] 更新失败:', res.data.msg)
        message.error(res.data.msg || t('common.update.failed'))
      }
    } else {
      console.log('[模板管理] 开始创建模板')
      console.log('[模板管理] 创建数据:', form.value)
      
      const res = await createTemplate(form.value as TaktGenTemplate)
      console.log('[模板管理] 创建响应:', res)

      if (res.data.code === 200) {
        message.success(t('common.create.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        console.error('[模板管理] 创建失败:', res.data.msg)
        message.error(res.data.msg || t('common.create.failed'))
      }
    }
  } catch (error) {
    console.error('[模板管理] 提交表单出错:', error)
    message.error('操作失败：' + (error instanceof Error ? error.message : '未知错误'))
  } finally {
    submitLoading.value = false
  }
}

// 监听模板ID变化
watch(
  () => props.templateId,
  async (newVal: number | undefined) => {
    console.log('[模板管理] templateId changed:', newVal)
    if (newVal) {
      try {
        console.log('[模板管理] 开始获取模板详情, id:', newVal)
        const res = await getTemplate(newVal)
        console.log('[模板管理] 获取模板详情响应:', res)
        
        // 检查响应结构
        if (!res.data) {
          console.error('[模板管理] 响应数据为空')
          message.error('获取数据失败：响应数据为空')
          return
        }

        // 检查响应状态码
        if (res.data.code !== 200) {
          console.error('[模板管理] 响应状态码错误:', res.data.code, res.data.msg)
          message.error(res.data.msg || '获取数据失败')
          return
        }

        // 检查数据
        if (!res.data.data) {
          console.error('[模板管理] 响应数据为空')
          message.error('获取数据失败：数据为空')
          return
        }

        console.log('[模板管理] 设置表单数据:', res.data.data)
        form.value = res.data.data
      } catch (error) {
        console.error('[模板管理] 获取模板详情出错:', error)
        message.error('获取数据失败：' + (error instanceof Error ? error.message : '未知错误'))
      }
    } else {
      console.log('[模板管理] 重置表单')
      resetForm()
    }
  },
  { immediate: true }
)

// 监听弹窗显示状态
watch(
  () => props.visible,
  (newVal: boolean) => {
    console.log('[模板管理] visible changed:', newVal)
    if (!newVal) {
      resetForm()
    }
  }
)

// 处理模板分类变化
const handleCategoryChange = (value: any) => {
  console.log('[模板管理] 模板分类变化:', value)
  if (!form.value.templateName) {
    message.warning(t('generator.template.messages.enterNameFirst'))
    return
  }

  // 根据模板分类生成文件名
  const fileNameMap: Record<number, string> = {
    1: '{{className}}.cs', // 实体类
    2: '{{className}}Dto.cs', // DTO
    3: 'I{{className}}Service.cs', // 服务接口
    4: '{{className}}Service.cs', // 服务实现
    5: 'I{{className}}Repository.cs', // 仓储接口
    6: '{{className}}Repository.cs', // 仓储实现
    7: '{{className}}.ts', // API
    8: '{{className}}.d.ts', // 类型
    9: 'index.vue', // 视图
    10: 'components/{{className}}Form.vue', // 表单组件
    11: '{{className}}Trans.sql', // 翻译SQL
    12: '{{className}}Menu.sql', // 菜单SQL
    13: '{{className}}Controller.cs' // 控制器
  }

  // 直接使用模板文件名，不进行变量替换
  form.value.fileName = fileNameMap[value] || ''
}

// 处理模板名称输入
const handleTemplateNameInput = (value: string) => {
  if (!value) return
  
  // 只允许字母
  const lettersOnly = value.replace(/[^a-zA-Z]/g, '')
  
  // 首字母大写
  const pascalName = lettersOnly.charAt(0).toUpperCase() + lettersOnly.slice(1)
  
  // 更新表单值
  form.value.templateName = pascalName
  
  // 如果已选择模板分类，更新文件名
  if (form.value.templateCategory) {
    handleCategoryChange(form.value.templateCategory)
  }
}

// 监听模板名称变化
watch(
  () => form.value.templateName,
  (newVal) => {
    if (newVal && form.value.templateCategory) {
      handleCategoryChange(form.value.templateCategory)
    }
  }
)

defineExpose({
  resetForm
})

onMounted(() => {
  const initData = async () => {
    if (props.templateId) {
      const res = await getTemplate(props.templateId)
      if (res.data.code === 200) {
        form.value = res.data.data
      }
    }
  }
  initData()
})
</script> 
