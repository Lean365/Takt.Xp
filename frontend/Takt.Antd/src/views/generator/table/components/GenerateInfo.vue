<template>
  <div class="generate-info">
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 18 }"
    >
      <!-- 基本信息 -->
      <a-form-item label="基本信息" name="basicInfo">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="模板类型" name="tplType">
              <Takt-select
                v-model:value="formData.tplType"
                dict-type="gen_tpl_type"
                type="radio"
                :show-all="false"
                placeholder="请选择模板类型"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="生成模板" name="tplCategory">
              <Takt-select
                v-model:value="formData.tplCategory"
                dict-type="gen_template_type"
                type="radio"
                :allow-clear="true"
                :show-all="false"
                placeholder="请选择生成模板"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="前端类型" name="frontendType">
              <Takt-select
                v-model:value="formData.frontTpl"
                dict-type="gen_frontend_type"
                type="radio"
                :show-all="false"
                placeholder="请选择前端类型"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="前端布局" name="frontStyle">
              <Takt-select
                v-model:value="formData.frontStyle"
                dict-type="gen_frontend_style"
                placeholder="请选择前端布局"
                type="radio"
                :show-all="false"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="按钮样式" name="buttonStyle">
              <Takt-select
                v-model:value="formData.btnStyle"
                dict-type="gen_button_style"
                type="radio"
                :show-all="false"
                placeholder="请选择按钮样式"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="生成方式" name="genMethod">
              <Takt-select
                v-model:value="formData.genMethod"
                dict-type="gen_type"
                type="radio"
                :show-all="false"
                placeholder="请选择生成方式"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="24">
            <a-form-item label="生成功能" name="options">
              <Takt-select
                v-model:value="selectedOptions"
                dict-type="gen_function"
                type="checkbox"
                placeholder="请选择生成功能"
                @change="(val: SelectValue) => selectedOptions = val as number[]"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>

      <!-- 树表配置 -->
      <a-form-item v-if="formData.tplCategory === 'tree'" label="树表配置" name="treeConfig">
        <a-row :gutter="16">
          <a-col :span="8">
            <a-form-item label="树编码字段" name="treeCode">
              <a-select
                v-model:value="formData.treeCode"
                placeholder="请选择树编码字段"
                :options="columnOptions"
              />
            </a-form-item>
          </a-col>
          <a-col :span="8">
            <a-form-item label="树父编码字段" name="treeParentCode">
              <a-select
                v-model:value="formData.treeParentCode"
                placeholder="请选择树父编码字段"
                :options="columnOptions"
              />
            </a-form-item>
          </a-col>
          <a-col :span="8">
            <a-form-item label="树名称字段" name="treeName">
              <a-select
                v-model:value="formData.treeName"
                placeholder="请选择树名称字段"
                :options="columnOptions"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>

      <!-- 主子表配置 -->
      <a-form-item v-if="formData.tplCategory === 'sub'" label="主子表配置" name="subConfig">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="关联子表" name="subTableName">
              <a-select
                v-model:value="formData.subTableName"
                placeholder="请选择关联子表"
                :options="tableOptions"
                @change="handleSubTableChange"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="子表关联外键" name="subTableFkName">
              <a-select
                v-model:value="formData.subTableFkName"
                placeholder="请选择子表关联外键"
                :options="subColumnOptions"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>

      <!-- 其他配置 -->
      <a-form-item label="其他配置" name="otherConfig">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="生成菜单" name="generateMenu">
              <a-switch
                :checked="formData.generateMenu === 1"
                @change="(checked) => formData.generateMenu = checked ? 1 : 0"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="排序" name="sortConfig">
              <a-row :gutter="16">
                <a-col :span="12">
                  <a-form-item name="sortField" no-style>
                    <a-input-number
                      v-model:value="formData.sortField"
                      :min="0"
                      style="width: 100%"
                      placeholder="排序字段"
                    />
                  </a-form-item>
                </a-col>
                <a-col :span="12">
                  <a-form-item name="sortType" no-style>
                    <a-select
                      v-model:value="formData.sortType"
                      style="width: 100%"
                      placeholder="排序方式"
                    >
                      <a-select-option value="asc">升序</a-select-option>
                      <a-select-option value="desc">降序</a-select-option>
                    </a-select>
                  </a-form-item>
                </a-col>
              </a-row>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="权限前缀" name="permsPrefix">
              <a-input v-model:value="formData.permsPrefix" placeholder="请输入权限前缀" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="基础命名空间" name="baseNamespace">
              <a-input v-model:value="formData.baseNamespace" placeholder="请输入基础命名空间" />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="生成路径" name="genPath" v-if="formData.genMethod === '0'">
              <a-input v-model:value="formData.genPath" placeholder="请输入生成路径" />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { CheckboxValueType } from 'ant-design-vue/es/checkbox/interface'
import type { SelectValue } from 'ant-design-vue/es/select'
import type { TaktGenTable } from '@/types/generator/genTable'
import type { TaktMenu } from '@/types/identity/menu'
import { getMenuList } from '@/api/identity/menu'
import { getTable, getTableList } from '@/api/generator/genTable'
import {
  SearchOutlined,
  EyeOutlined,
  PlusOutlined,
  EditOutlined,
  ImportOutlined,
  DeleteOutlined,
  ExportOutlined,
  PrinterOutlined
} from '@ant-design/icons-vue'

// 组件属性定义
interface Props {
  modelValue: TaktGenTable
  id?: string
}

// 组件事件定义
interface Emits {
  (e: 'update:modelValue', value: TaktGenTable): void
  (e: 'submit'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const formRef = ref<FormInstance>()

// 选中的选项
const selectedOptions = ref<number[]>([])

// 树表选项
const treeOptions = ref<number[]>([])

// 主子表选项
const subOptions = ref<number[]>([])

// 添加新的响应式变量
const columnOptions = ref<{ label: string; value: string }[]>([])
const tableOptions = ref<{ label: string; value: string }[]>([])
const subColumnOptions = ref<{ label: string; value: string }[]>([])
const menuOptions = ref<TaktMenu[]>([])

// 表单校验规则
const rules: Record<string, Rule[]> = {
  baseNamespace: [{ required: true, message: '请输入命名空间前缀', trigger: 'blur' }],
  moduleName: [{ required: true, message: '请输入生成模块', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入生成业务名', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入生成功能名', trigger: 'blur' }],
  tplCategory: [{ required: true, message: '请选择生成类型', trigger: 'change' }]
}

// 5.3 默认选项
const defaultOptions = {
  isSqlDiff: 1,
  isSnowflakeId: 1,
  isRepository: 0,
  crudGroup: [1, 2, 3, 4, 5, 6, 7, 8] // 默认选中所有基础操作
}

// 表单数据
const formData = reactive<TaktGenTable>({
  genTableId: '',
  databaseName: '',
  tableName: '',
  tableComment: '',
  subTableName: '',
  subTableFkName: '',
  treeCode: '',
  treeName: '',
  treeParentCode: '',
  tplType: '0',
  tplCategory: 'crud',
  baseNamespace: '',
  entityNamespace: '',
  entityClassName: '',
  dtoNamespace: '',
  dtoClassName: '',
  dtoType: '',
  serviceNamespace: '',
  iServiceClassName: '',
  serviceClassName: '',
  iRepositoryNamespace: '',
  iRepositoryClassName: '',
  repositoryNamespace: '',
  repositoryClassName: '',
  controllerNamespace: '',
  controllerClassName: '',
  moduleName: '',
  businessName: '',
  functionName: '',
  author: '',
  genMethod: '0',
  genPath: '/',
  parentMenuId: '',
  generateMenu: 1,
  sortType: 'asc',
  sortField: '',
  permsPrefix: '',
  frontTpl: 2,
  frontStyle: 24,
  btnStyle: 1,
  status: 0,
  columns: [],
  subTable: undefined,
  genFunction: '',
  isSqlDiff: 1,
  isSnowflakeId: 1,
  isRepository: 0,
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  deleteBy: '',
  deleteTime: '',
  isDeleted: 0,
  remark: ''
})

// 监听表单数据变化
watch(
  () => props.modelValue,
  (newVal) => {
    if (newVal) {
      Object.assign(formData, newVal)
      // 设置选中的功能
      selectedOptions.value = defaultOptions.crudGroup
    }
  },
  { deep: true, immediate: true }
)

// 7.2 监听生成类型变化
watch(() => formData.tplCategory, (newVal) => {
  // 更新选中的选项
  if (newVal === 'tree') {
    selectedOptions.value = [5, 6, 7] // 树表操作：详情、预览、打印
  } else if (newVal === 'sub') {
    selectedOptions.value = [8] // 主子表操作
  } else {
    selectedOptions.value = [1, 2, 3, 4, 5, 6, 7, 8] // 基础操作：新增、修改、删除、导入、模板、导出、详情、预览、打印
  }
  // 更新表单选项
  // formData.options = {
  //   ...defaultOptions,
  //   crudGroup: selectedOptions.value
  // }
  emit('update:modelValue', formData)
})

// 获取菜单列表
const getMenus = async () => {
  try {
    const res = await getMenuList({
      pageIndex: 1,
      pageSize: 100
    })
    if (res.data?.rows) {
      menuOptions.value = res.data.rows
    }
  } catch (error) {
    console.error('获取菜单列表失败:', error)
  }
}

// 处理子表变化
const handleSubTableChange = async (value: SelectValue) => {
  if (value) {
    try {
      const res = await getTable(String(value))
      if (res.data?.columns) {
        subColumnOptions.value = res.data.columns.map((col: any) => ({
          label: col.columnName,
          value: col.columnName
        }))
      }
    } catch (error) {
      console.error('获取子表信息失败:', error)
    }
  } else {
    subColumnOptions.value = []
  }
}

// 获取表列表
const getTables = async () => {
  try {
    const res = await getTableList({
      pageIndex: 1,
      pageSize: 100
    })
    if (res.data?.rows) {
      tableOptions.value = res.data.rows.map((table: any) => ({
        label: table.tableName,
        value: table.tableName
      }))
    }
  } catch (error) {
    console.error('获取表列表失败:', error)
  }
}

// 获取列信息
const getColumns = async () => {
  if (props.id) {
    try {
      const res = await getTable(props.id)
      if (res.data?.columns) {
        columnOptions.value = res.data.columns.map((col: any) => ({
          label: col.columnName,
          value: col.columnName
        }))
      }
    } catch (error) {
      console.error('获取列信息失败:', error)
    }
  }
}

// 初始化
onMounted(async () => {
  console.log('GenerateInfo - 组件挂载，当前数据:', formData)
  await getMenus()
  await getTables()
  await getColumns()
  
  // 如果有 id，获取详细信息
  if (props.id) {
    try {
      const res = await getTable(props.id)
      if (res.data) {
        const tableData = res.data
        Object.assign(formData, {
          ...tableData
        })
        // 更新选中的选项
        selectedOptions.value = [1, 2, 3, 4]
        // 更新树表选项
        treeOptions.value = selectedOptions.value.filter(opt => 
          [5, 6, 7].includes(opt)
        )
        // 更新主子表选项
        subOptions.value = selectedOptions.value.filter(opt => 
          [8].includes(opt)
        )
      }
    } catch (error) {
      console.error('获取表信息失败:', error)
    }
  }
})

// 提交表单
const handleSubmit = () => {
  emit('submit')
}
</script>

<style lang="less" scoped>
.generate-info {
  padding: 24px;
  max-height: 600px;
  overflow-y: auto;

  :deep(.ant-form-item) {
    margin-bottom: 16px;
  }

  :deep(.ant-form-item-label) {
    font-weight: bold;
  }

  .options-container {
    background-color: #fafafa;
    padding: 16px;
    border-radius: 4px;
    border: 1px solid #f0f0f0;

    :deep(.ant-checkbox-wrapper) {
      margin-right: 0;
      width: 100%;
    }
  }
}
</style> 
