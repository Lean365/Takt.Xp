<template>
  <div class="field-info">
    <a-table
      :columns="columns"
      :data-source="formData.columns"
      :pagination="false"
      size="small"
      bordered
    >
      <template #bodyCell="{ column, record }">
        <!-- 字段名 -->
        <template v-if="column.dataIndex === 'columnName'">
          <span>{{ record.columnName }}</span>
        </template>

        <!-- 字段类型 -->
        <template v-if="column.dataIndex === 'dbColumnType'">
          <span>{{ record.dbColumnType }}</span>
        </template>

        <!-- C#类型 -->
        <template v-if="column.dataIndex === 'csharpType'">
          <span>{{ record.csharpType }}</span>
        </template>

        <!-- 字段注释 -->
        <template v-if="column.dataIndex === 'columnComment'">
          <a-input
            v-model:value="record.columnComment"
            placeholder="请输入字段注释"
            @change="handleColumnChange"
          />
        </template>

        <!-- 插入 -->
        <template v-if="column.dataIndex === 'isInsert'">
          <a-checkbox
            :checked="record.isInsert === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isInsert')"
          />
        </template>

        <!-- 编辑 -->
        <template v-if="column.dataIndex === 'isEdit'">
          <a-checkbox
            :checked="record.isEdit === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isEdit')"
          />
        </template>

        <!-- 列表 -->
        <template v-if="column.dataIndex === 'isList'">
          <a-checkbox
            :checked="record.isList === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isList')"
          />
        </template>

        <!-- 查询 -->
        <template v-if="column.dataIndex === 'isQuery'">
          <a-checkbox
            :checked="record.isQuery === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isQuery')"
          />
        </template>

        <!-- 查询方式 -->
        <template v-if="column.dataIndex === 'queryType'">
          <a-select
            v-model:value="record.queryType"
            style="width: 100%"
            @change="handleColumnChange"
          >
            <a-select-option value="EQ">等于</a-select-option>
            <a-select-option value="NE">不等于</a-select-option>
            <a-select-option value="GT">大于</a-select-option>
            <a-select-option value="GE">大于等于</a-select-option>
            <a-select-option value="LT">小于</a-select-option>
            <a-select-option value="LE">小于等于</a-select-option>
            <a-select-option value="LIKE">模糊查询</a-select-option>
            <a-select-option value="BETWEEN">范围查询</a-select-option>
          </a-select>
        </template>

        <!-- 显示类型 -->
        <template v-if="column.dataIndex === 'displayType'">
          <a-select
            v-model:value="record.displayType"
            style="width: 100%"
            @change="handleColumnChange"
          >
            <a-select-option value="input">文本框</a-select-option>
            <a-select-option value="textarea">文本域</a-select-option>
            <a-select-option value="select">下拉框</a-select-option>
            <a-select-option value="radio">单选框</a-select-option>
            <a-select-option value="checkbox">复选框</a-select-option>
            <a-select-option value="datetime">日期控件</a-select-option>
            <a-select-option value="imageUpload">图片上传</a-select-option>
            <a-select-option value="fileUpload">文件上传</a-select-option>
            <a-select-option value="editor">富文本编辑器</a-select-option>
          </a-select>
        </template>

        <!-- 字典类型 -->
        <template v-if="column.dataIndex === 'dictType'">
          <Takt-select
            v-model:value="record.dictType"
            dict-type="sys_dict_type"
            placeholder="请选择字典类型"
            @change="handleColumnChange"
          />
        </template>

        <!-- 必填 -->
        <template v-if="column.dataIndex === 'isRequired'">
          <a-checkbox
            :checked="record.isRequired === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isRequired')"
          />
        </template>

        <!-- 主键 -->
        <template v-if="column.dataIndex === 'isPrimaryKey'">
          <a-checkbox
            :checked="record.isPrimaryKey === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isPrimaryKey')"
          />
        </template>

        <!-- 自增 -->
        <template v-if="column.dataIndex === 'isIncrement'">
          <a-checkbox
            :checked="record.isIncrement === 1"
            @change="(e) => handleCheckboxChange(e, record, 'isIncrement')"
          />
        </template>

        <!-- 排序 -->
        <template v-if="column.dataIndex === 'orderNum'">
          <a-input-number
            v-model:value="record.orderNum"
            :min="0"
            style="width: 100%"
            @change="handleColumnChange"
          />
        </template>
      </template>
    </a-table>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import type { TaktGenTable } from '@/types/generator/genTable'
import type { TaktGenColumn } from '@/types/generator/genColumn'
import { getColumns } from '@/api/generator/genTable'

const props = defineProps<{
  modelValue: TaktGenTable
  id?: string
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: TaktGenTable): void
}>()

// 表单数据
const formData = reactive<TaktGenTable>({
  ...props.modelValue,
  columns: []
})


// 表格列定义
const columns = [
  {
    title: '字段名',
    dataIndex: 'columnName',
    key: 'columnName',
    width: 120
  },
  {
    title: '字段类型',
    dataIndex: 'dbColumnType',
    key: 'dbColumnType',
    width: 120
  },
  {
    title: 'C#类型',
    dataIndex: 'csharpType',
    key: 'csharpType',
    width: 120
  },
  {
    title: '字段注释',
    dataIndex: 'columnComment',
    key: 'columnComment',
    width: 120
  },
  {
    title: '插入',
    dataIndex: 'isInsert',
    key: 'isInsert',
    width: 60
  },
  {
    title: '编辑',
    dataIndex: 'isEdit',
    key: 'isEdit',
    width: 60
  },
  {
    title: '列表',
    dataIndex: 'isList',
    key: 'isList',
    width: 60
  },
  {
    title: '查询',
    dataIndex: 'isQuery',
    key: 'isQuery',
    width: 60
  },
  {
    title: '查询方式',
    dataIndex: 'queryType',
    key: 'queryType',
    width: 120
  },
  {
    title: '显示类型',
    dataIndex: 'displayType',
    key: 'displayType',
    width: 120
  },
  {
    title: '字典类型',
    dataIndex: 'dictType',
    key: 'dictType',
    width: 120
  },
  {
    title: '必填',
    dataIndex: 'isRequired',
    key: 'isRequired',
    width: 60
  },
  {
    title: '主键',
    dataIndex: 'isPrimaryKey',
    key: 'isPrimaryKey',
    width: 60
  },
  {
    title: '自增',
    dataIndex: 'isIncrement',
    key: 'isIncrement',
    width: 60
  },
  {
    title: '排序',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 60
  }
]


// 获取表列信息
const fetchTableColumns = async (tableId: string) => {
  console.log('fetchTableColumns 被调用，tableId:', tableId)
  if (!tableId) {
    console.log('fetchTableColumns: tableId无效')
    return
  }

  try {
    console.log('开始获取表列信息，tableId:', tableId)
    const res = await getColumns(tableId)
    console.log('获取表列信息响应:', res)
    
    if (res?.code === 200 && res?.data) {
      const columns = res.data.map((col: any) => ({
        ...col,
        tableId,
        isInsert: Number(col.isInsert) || 0,
        isEdit: Number(col.isEdit) || 0,
        isList: Number(col.isList) || 0,
        isQuery: Number(col.isQuery) || 0,
        isRequired: Number(col.isRequired) || 0,
        isPrimaryKey: Number(col.isPrimaryKey) || 0,
        isIncrement: Number(col.isIncrement) || 0,
        orderNum: Number(col.orderNum) || 0,
        queryType: col.queryType || 'EQ',
        displayType: col.displayType || 'input',
        dictType: col.dictType || ''
      }))
      
      console.log('处理后的列数据:', columns)
      formData.columns = columns
      emit('update:modelValue', { ...formData })
    } else {
      console.log('获取表列信息失败或数据为空:', res)
      formData.columns = []
    }
  } catch (error) {
    console.error('获取表列信息失败:', error)
    formData.columns = []
  }
}

// 监听 id 变化
watch(
  () => props.id,
  (newVal) => {
    console.log('id变化监听触发，新值:', newVal, '类型:', typeof newVal)
    if (newVal && typeof newVal === 'string') {
      console.log('准备调用fetchTableColumns')
      fetchTableColumns(newVal)
    } else {
      console.log('id无效，清空列数据')
      formData.columns = []
    }
  },
  { immediate: true }
)

// 监听表单数据变化
watch(
  () => props.modelValue,
  (newVal) => {
    console.log('modelValue变化:', newVal)
    if (newVal) {
      Object.assign(formData, {
        ...newVal,
        columns: newVal.columns || []
      })
    }
  },
  { deep: true }
)

// 处理列变化
const handleColumnChange = () => {
  console.log('列数据变化')
  emit('update:modelValue', { ...formData })
}

// 处理复选框变化
const handleCheckboxChange = (e: any, record: any, field: string) => {
  console.log('复选框变化:', field, e.target.checked)
  record[field] = e.target.checked ? 1 : 0
  emit('update:modelValue', { ...formData })
}

// 初始化
onMounted(() => {
  console.log('组件挂载，当前props:', props)
  if (props.id && typeof props.id === 'string') {
    console.log('组件挂载时调用fetchTableColumns')
    fetchTableColumns(props.id)
  }
})
</script>

<style lang="less" scoped>
.field-info {
  padding: 24px;
}
</style> 
