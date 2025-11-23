<template>
  <a-modal
    :open="open"
    :title="title"
    width="1300px"
    @update:open="handleClose"
    @ok="handleSubmit"
    :confirm-loading="loading"
  >
    <a-tabs v-model:activeKey="activeKey">
      <a-tab-pane key="basic" :tab="t('generator.table.tab.basic')">
        <basic-info v-model="formData" :id="props.id" />
      </a-tab-pane>
      <a-tab-pane key="generate" :tab="t('generator.table.tab.generate')">
        <generate-info v-model="formData" :id="props.id" />
      </a-tab-pane>
      <a-tab-pane key="field" :tab="t('generator.table.tab.field')">
        <field-info v-model="formData" :id="props.id" />
      </a-tab-pane>
    </a-tabs>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { TaktGenTable } from '@/types/generator/genTable'
import { getTable, createTable, updateTable } from '@/api/generator/genTable'
import BasicInfo from './BasicInfo.vue'
import GenerateInfo from './GenerateInfo.vue'
import FieldInfo from './FieldInfo.vue'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  title: string
  id?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const formRef = ref<FormInstance>()
const loading = ref(false)

// 表单数据
const formData = ref<TaktGenTable>({
  genTableId: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  deleteBy: '',
  deleteTime: '',
  isDeleted: 0,
  remark: '',
  databaseName: '',
  tableName: '',
  tableComment: '',
  baseNamespace: '',
  tplType: '0',
  tplCategory: 'crud',
  subTableName: undefined,
  subTableFkName: undefined,
  treeCode: '',
  treeName: '',
  treeParentCode: '',
  moduleName: '',
  businessName: '',
  functionName: '',
  author: '',
  genMethod: '0',
  genPath: '',
  parentMenuId: '',
  sortType: 'asc',
  sortField: '',
  permsPrefix: '',
  generateMenu: 1,
  frontTpl: 1,
  btnStyle: 1,
  frontStyle: 24,
  status: 1,
  entityClassName: '',
  entityNamespace: '',
  dtoType: [],
  dtoNamespace: '',
  dtoClassName: '',
  serviceNamespace: '',
  iServiceClassName: '',
  serviceClassName: '',
  iRepositoryNamespace: '',
  iRepositoryClassName: '',
  repositoryNamespace: '',
  repositoryClassName: '',
  controllerNamespace: '',
  controllerClassName: '',
  genFunction: '1,2,3,4,5,6,7',
  isSqlDiff: 1,
  isSnowflakeId: 1,
  isRepository: 1,
  columns: [],
  subTable: undefined
})

// 表单校验规则
const rules = {
  tableName: [{ required: true, message: t('generator.table.required.name') }],
  tableComment: [{ required: true, message: t('generator.table.required.comment') }],
  baseNamespace: [{ required: true, message: t('generator.table.required.baseNamespace') }],
  moduleName: [{ required: true, message: t('generator.table.required.moduleName') }],
  businessName: [{ required: true, message: t('generator.table.required.businessName') }],
  functionName: [{ required: true, message: t('generator.table.required.functionName') }],
  author: [{ required: true, message: t('generator.table.required.author') }],
  genMode: [{ required: true, message: t('generator.table.required.genMode') }],
  genPath: [{ required: true, message: t('generator.table.required.genPath') }]
}

// 当前激活的标签页
const activeKey = ref('basic')

// 监听表格ID变化
watch(() => props.id, async (newVal) => {
  console.log('TableForm - id 变化:', newVal, '类型:', typeof newVal)
  console.log('TableForm - 当前 props:', props)
  console.log('TableForm - 当前 formData:', formData.value)
  
  if (!newVal || typeof newVal !== 'string') {
    console.log('TableForm - id 无效，等待有效值')
    formData.value = {
      genTableId: '',
      createBy: '',
      createTime: '',
      updateBy: '',
      updateTime: '',
      deleteBy: '',
      deleteTime: '',
      isDeleted: 0,
      remark: '',
      databaseName: '',
      tableName: '',
      tableComment: '',
      baseNamespace: '',
      tplType: '0',
      tplCategory: 'crud',
      subTableName: undefined,
      subTableFkName: undefined,
      treeCode: '',
      treeName: '',
      treeParentCode: '',
      moduleName: '',
      businessName: '',
      functionName: '',
      author: '',
      genMethod: '0',
      genPath: '',
      parentMenuId: '',
      sortType: 'asc',
      sortField: '',
      permsPrefix: '',
      generateMenu: 1,
      frontTpl: 1,
      btnStyle: 1,
      frontStyle: 24,
      status: 1,
      entityClassName: '',
      entityNamespace: '',
      dtoType: [],
      dtoNamespace: '',
      dtoClassName: '',
      serviceNamespace: '',
      iServiceClassName: '',
      serviceClassName: '',
      iRepositoryNamespace: '',
      iRepositoryClassName: '',
      repositoryNamespace: '',
      repositoryClassName: '',
      controllerNamespace: '',
      controllerClassName: '',
      genFunction: '1,2,3,4,5,6,7',
      isSqlDiff: 1,
      isSnowflakeId: 1,
      isRepository: 1,
      columns: [],
      subTable: undefined
    } as TaktGenTable
    return
  }

  try {
    console.log('TableForm - 开始获取表信息，id:', newVal)
    const res = await getTable(newVal)
    console.log('TableForm - 获取表信息响应:', res)
    
    if (!res || !res.data) {
      console.error('TableForm - 获取表信息响应无效:', res)
      return
    }

    if (res.code !== 200) {
      console.error('TableForm - 获取表信息失败:', res.msg)
      return
    }

    if (!res.data) {
      console.error('TableForm - 获取表信息数据为空')
      return
    }

    // 确保所有必需字段都有值，并设置正确的tableId
    formData.value = {
      ...res.data,
      genTableId: res.data.genTableId || '',
      genPath: res.data.genPath || '',
      columns: res.data.columns?.map((column: any) => ({
        ...column,
        tableId: res.data.genTableId // 确保每个列都设置了正确的tableId
      })) || []
    } as TaktGenTable
    console.log('TableForm - 设置表单数据:', formData.value)
  } catch (error) {
    console.error('TableForm - 获取表信息失败:', error)
  }
}, { immediate: true })

// 监听表单数据变化
watch(
  () => formData.value,
  (newVal) => {
    console.log('TableForm - 表单数据变化:', newVal)
  },
  { deep: true }
)

// 关闭对话框
const handleClose = () => {
  emit('update:open', false)
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    const api = props.id ? updateTable : createTable
    const data = {
      genTableId: formData.value.genTableId,
      databaseName: formData.value.databaseName,
      tableName: formData.value.tableName,
      tableComment: formData.value.tableComment,
      baseNamespace: formData.value.baseNamespace,
      tplType: formData.value.tplType,
      tplCategory: formData.value.tplCategory,
      subTableName: formData.value.subTableName,
      subTableFkName: formData.value.subTableFkName,
      treeCode: formData.value.treeCode,
      treeName: formData.value.treeName,
      treeParentCode: formData.value.treeParentCode,
      moduleName: formData.value.moduleName,
      businessName: formData.value.businessName,
      functionName: formData.value.functionName,
      author: formData.value.author,
      genMethod: formData.value.genMethod,
      genPath: formData.value.genPath,
      parentMenuId: formData.value.parentMenuId,
      sortType: formData.value.sortType,
      sortField: formData.value.sortField,
      permsPrefix: formData.value.permsPrefix,
      generateMenu: formData.value.generateMenu,
      frontTpl: formData.value.frontTpl,
      btnStyle: formData.value.btnStyle,
      frontStyle: formData.value.frontStyle,
      status: formData.value.status,
      entityClassName: formData.value.entityClassName,
      entityNamespace: formData.value.entityNamespace,
      dtoType: formData.value.dtoType,
      dtoNamespace: formData.value.dtoNamespace,
      dtoClassName: formData.value.dtoClassName,
      serviceNamespace: formData.value.serviceNamespace,
      iServiceClassName: formData.value.iServiceClassName,
      serviceClassName: formData.value.serviceClassName,
      iRepositoryNamespace: formData.value.iRepositoryNamespace,
      iRepositoryClassName: formData.value.iRepositoryClassName,
      repositoryNamespace: formData.value.repositoryNamespace,
      repositoryClassName: formData.value.repositoryClassName,
      controllerNamespace: formData.value.controllerNamespace,
      controllerClassName: formData.value.controllerClassName,
      genFunction: formData.value.genFunction,
      isSqlDiff: formData.value.isSqlDiff,
      isSnowflakeId: formData.value.isSnowflakeId,
      isRepository: formData.value.isRepository,
      columns: formData.value.columns?.map(column => ({
        ...column,
        tableId: formData.value.genTableId
      })),
      subTable: formData.value.subTable,
      createBy: formData.value.createBy || '',
      createTime: formData.value.createTime || '',
      isDeleted: formData.value.isDeleted || 0
    }
    const res = await api(data)
    
    if (res.code === 200) {
      emit('success')
      handleClose()
    }
  } catch (error) {
    console.error('提交失败:', error)
  } finally {
    loading.value = false
  }
}

const resetForm = () => {
  formData.value = {
    genTableId: '',
    createBy: '',
    createTime: '',
    updateBy: '',
    updateTime: '',
    deleteBy: '',
    deleteTime: '',
    isDeleted: 0,
    remark: '',
    databaseName: '',
    tableName: '',
    tableComment: '',
    baseNamespace: '',
    tplType: '0',
    tplCategory: 'crud',
    subTableName: undefined,
    subTableFkName: undefined,
    treeCode: '',
    treeName: '',
    treeParentCode: '',
    moduleName: '',
    businessName: '',
    functionName: '',
    author: '',
    genMethod: '0',
    genPath: '',
    parentMenuId: '',
    sortType: 'asc',
    sortField: '',
    permsPrefix: '',
    generateMenu: 1,
    frontTpl: 1,
    btnStyle: 1,
    frontStyle: 24,
    status: 1,
    entityClassName: '',
    entityNamespace: '',
    dtoType: [],
    dtoNamespace: '',
    dtoClassName: '',
    serviceNamespace: '',
    iServiceClassName: '',
    serviceClassName: '',
    iRepositoryNamespace: '',
    iRepositoryClassName: '',
    repositoryNamespace: '',
    repositoryClassName: '',
    controllerNamespace: '',
    controllerClassName: '',
    genFunction: '1,2,3,4,5,6,7',
    isSqlDiff: 1,
    isSnowflakeId: 1,
    isRepository: 1,
    columns: [],
    subTable: undefined
  }
}
</script>

<style lang="less" scoped>
:deep(.ant-form-item-label) {
  font-weight: bold;
}
</style>

