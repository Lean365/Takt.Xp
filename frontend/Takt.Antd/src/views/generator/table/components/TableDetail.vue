<template>
  <a-modal
    :open="props.open"
    :title="t('generator.table.detail.title')"
    :width="1000"
    :footer="null"
    @update:open="emit('update:open', $event)"
  >
    <!-- 表基本信息 -->
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('generator.table.name')">
        {{ tableInfo?.tableName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.comment')">
        {{ tableInfo?.tableComment }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.baseNamespace')">
        {{ tableInfo?.baseNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.tplCategory')">
        {{ tableInfo?.tplCategory }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.subTableName')">
        {{ tableInfo?.subTableName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.subTableFkName')">
        {{ tableInfo?.subTableFkName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.treeCode')">
        {{ tableInfo?.treeCode }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.treeName')">
        {{ tableInfo?.treeName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.treeParentCode')">
        {{ tableInfo?.treeParentCode }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.moduleName')">
        {{ tableInfo?.moduleName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.businessName')">
        {{ tableInfo?.businessName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.functionName')">
        {{ tableInfo?.functionName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.author')">
        {{ tableInfo?.author }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.genMethod')">
        {{ tableInfo?.genMethod }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.genPath')">
        {{ tableInfo?.genPath }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.parentMenuId')">
        {{ tableInfo?.parentMenuId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.sortType')">
        {{ tableInfo?.sortType }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.sortField')">
        {{ tableInfo?.sortField }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.permsPrefix')">
        {{ tableInfo?.permsPrefix }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.generateMenu')">
        {{ tableInfo?.generateMenu }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.frontTpl')">
        {{ tableInfo?.frontTpl }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.btnStyle')">
        {{ tableInfo?.btnStyle }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.frontStyle')">
        {{ tableInfo?.frontStyle }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.status')">
        {{ tableInfo?.status }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.entityClassName')">
        {{ tableInfo?.entityClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.entityNamespace')">
        {{ tableInfo?.entityNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.dtoType')">
        {{ Array.isArray(tableInfo?.dtoType) ? tableInfo.dtoType.join(', ') : '' }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.dtoNamespace')">
        {{ tableInfo?.dtoNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.dtoClassName')">
        {{ tableInfo?.dtoClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.serviceNamespace')">
        {{ tableInfo?.serviceNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.iServiceClassName')">
        {{ tableInfo?.iServiceClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.serviceClassName')">
        {{ tableInfo?.serviceClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.iRepositoryNamespace')">
        {{ tableInfo?.iRepositoryNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.iRepositoryClassName')">
        {{ tableInfo?.iRepositoryClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.repositoryNamespace')">
        {{ tableInfo?.repositoryNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.repositoryClassName')">
        {{ tableInfo?.repositoryClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.controllerNamespace')">
        {{ tableInfo?.controllerNamespace }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.controllerClassName')">
        {{ tableInfo?.controllerClassName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.remark')">
        {{ tableInfo?.remark }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.isSqlDiff')">
        {{ tableInfo?.isSqlDiff }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.isSnowflakeId')">
        {{ tableInfo?.isSnowflakeId }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.isRepository')">
        {{ tableInfo?.isRepository }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.genFunction')">
        {{ tableInfo?.genFunction }}
      </a-descriptions-item>
    </a-descriptions>

    <!-- 列信息表格 -->
    <a-divider>{{ t('generator.table.detail.columnInfo') }}</a-divider>
    <div v-if="columnList.length === 0" class="empty-tip">
      <a-empty :description="t('generator.table.detail.noColumns')" />
    </div>
    <Takt-table
      v-else
      :columns="columns"
      :data-source="columnList"
      :pagination="false"
      :scroll="{ x: 1000 }"
      :row-key="(record) => record.columnName"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'isRequired'">
          <a-tag :color="record.isRequired ? 'red' : 'green'">
            {{ record.isRequired ? t('generator.table.detail.yes') : t('generator.table.detail.no') }}
          </a-tag>
        </template>
        <template v-if="column.key === 'isInsert'">
          <a-tag :color="record.isInsert ? 'blue' : 'default'">
            {{ record.isInsert ? t('generator.table.detail.yes') : t('generator.table.detail.no') }}
          </a-tag>
        </template>
        <template v-if="column.key === 'isEdit'">
          <a-tag :color="record.isEdit ? 'blue' : 'default'">
            {{ record.isEdit ? t('generator.table.detail.yes') : t('generator.table.detail.no') }}
          </a-tag>
        </template>
        <template v-if="column.key === 'isList'">
          <a-tag :color="record.isList ? 'blue' : 'default'">
            {{ record.isList ? t('generator.table.detail.yes') : t('generator.table.detail.no') }}
          </a-tag>
        </template>
        <template v-if="column.key === 'isQuery'">
          <a-tag :color="record.isQuery ? 'blue' : 'default'">
            {{ record.isQuery ? t('generator.table.detail.yes') : t('generator.table.detail.no') }}
          </a-tag>
        </template>
      </template>
    </Takt-table>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { TaktGenTable } from '@/types/generator/genTable'
import type { TaktGenColumn } from '@/types/generator/genColumn'
import { getTable, getColumns } from '@/api/generator/genTable'
import { message } from 'ant-design-vue'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  tableId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

// 表信息
const tableInfo = ref<TaktGenTable>()
// 列信息
const columnList = ref<TaktGenColumn[]>([])

// 表格列定义
const columns = [
  {
    title: t('generator.table.detail.columnName'),
    dataIndex: 'columnName',
    key: 'columnName',
    width: 150
  },
  {
    title: t('generator.table.detail.columnComment'),
    dataIndex: 'columnComment',
    key: 'columnComment',
    width: 150
  },
  {
    title: t('generator.table.detail.columnType'),
    dataIndex: 'dbColumnType',
    key: 'dbColumnType',
    width: 100
  },
  {
    title: t('generator.table.detail.javaType'),
    dataIndex: 'csharpType',
    key: 'csharpType',
    width: 100
  },
  {
    title: t('generator.table.detail.javaField'),
    dataIndex: 'csharpField',
    key: 'csharpField',
    width: 100
  },
  {
    title: t('generator.table.detail.isRequired'),
    dataIndex: 'isRequired',
    key: 'isRequired',
    width: 80
  },
  {
    title: t('generator.table.detail.isInsert'),
    dataIndex: 'isInsert',
    key: 'isInsert',
    width: 80
  },
  {
    title: t('generator.table.detail.isEdit'),
    dataIndex: 'isEdit',
    key: 'isEdit',
    width: 80
  },
  {
    title: t('generator.table.detail.isList'),
    dataIndex: 'isList',
    key: 'isList',
    width: 80
  },
  {
    title: t('generator.table.detail.isQuery'),
    dataIndex: 'isQuery',
    key: 'isQuery',
    width: 80
  },
  {
    title: t('generator.table.detail.queryType'),
    dataIndex: 'queryType',
    key: 'queryType',
    width: 100
  },
  {
    title: t('generator.table.detail.htmlType'),
    dataIndex: 'displayType',
    key: 'displayType',
    width: 100
  },
  {
    title: t('generator.table.detail.dictType'),
    dataIndex: 'dictType',
    key: 'dictType',
    width: 100
  }
]

// 监听 tableId 变化
watch(
  () => props.tableId,
  async (newId) => {
    console.log('TableDetail - tableId 变化:', newId)
    if (!newId || typeof newId !== 'number' || isNaN(newId)) {
      console.log('TableDetail - tableId 未设置或无效，等待有效值')
      tableInfo.value = undefined
      columnList.value = []
      return
    }

    // 如果对话框未打开，不加载数据
    if (!props.open) {
      console.log('TableDetail - 对话框未打开，等待对话框打开')
      return
    }

    await loadTableData(newId)
  }
)

// 监听对话框打开状态
watch(
  () => props.open,
  async (newVal) => {
    console.log('TableDetail - 对话框状态变化:', newVal)
    if (newVal && props.tableId) {
      // 如果对话框打开且有 tableId，加载数据
      console.log('TableDetail - 对话框打开，加载数据')
      await loadTableData(props.tableId)
    } else {
      // 如果对话框关闭，清空数据
      console.log('TableDetail - 对话框关闭，清空数据')
      tableInfo.value = undefined
      columnList.value = []
    }
  }
)

// 加载表数据
async function loadTableData(id: string) {
  try {
    console.log('TableDetail - 开始获取表信息，id:', id)
    // 获取表信息
    const res = await getTable(id)
    console.log('TableDetail - 获取表信息响应:', res)
    
    if (!res || !res.data) {
      console.error('TableDetail - 获取表信息响应无效:', res)
      message.error(t('generator.table.detail.loadFailed'))
      return
    }

    if (res.code !== 200) {
      console.error('TableDetail - 获取表信息失败:', res.msg)
      message.error(res.msg || t('common.failed'))
      return
    }

    if (!res.data) {
      console.error('TableDetail - 获取表信息数据为空')
      message.error(t('common.failed'))
      return
    }

    // 设置表信息
    tableInfo.value = {
      genTableId: res.data.genTableId || '',
      createBy: res.data.createBy || '',
      createTime: res.data.createTime || '',
      updateBy: res.data.updateBy || '',
      updateTime: res.data.updateTime || '',
      deleteBy: res.data.deleteBy || '',
      deleteTime: res.data.deleteTime || '',
      isDeleted: res.data.isDeleted || 0,
      remark: res.data.remark || '',
      databaseName: res.data.databaseName || '',
      tableName: res.data.tableName || '',
      tableComment: res.data.tableComment || '',
      baseNamespace: res.data.baseNamespace || '',
      tplType: res.data.tplType || '0',
      tplCategory: res.data.tplCategory || 'crud',
      subTableName: res.data.subTableName || undefined,
      subTableFkName: res.data.subTableFkName || undefined,
      treeCode: res.data.treeCode || '',
      treeName: res.data.treeName || '',
      treeParentCode: res.data.treeParentCode || '',
      moduleName: res.data.moduleName || '',
      businessName: res.data.businessName || '',
      functionName: res.data.functionName || '',
      author: res.data.author || '',
      genMethod: res.data.genMethod || '0',
      genPath: res.data.genPath || '',
      parentMenuId: res.data.parentMenuId || '',
      sortType: res.data.sortType || 'asc',
      sortField: res.data.sortField || '',
      permsPrefix: res.data.permsPrefix || '',
      generateMenu: res.data.generateMenu || 1,
      frontTpl: res.data.frontTpl || 1,
      btnStyle: res.data.btnStyle || 1,
      frontStyle: res.data.frontStyle || 24,
      status: res.data.status || 1,
      entityClassName: res.data.entityClassName || '',
      entityNamespace: res.data.entityNamespace || '',
      dtoType: Array.isArray(res.data.dtoType) ? res.data.dtoType : [],
      dtoNamespace: res.data.dtoNamespace || '',
      dtoClassName: res.data.dtoClassName || '',
      serviceNamespace: res.data.serviceNamespace || '',
      iServiceClassName: res.data.iServiceClassName || '',
      serviceClassName: res.data.serviceClassName || '',
      iRepositoryNamespace: res.data.iRepositoryNamespace || '',
      iRepositoryClassName: res.data.iRepositoryClassName || '',
      repositoryNamespace: res.data.repositoryNamespace || '',
      repositoryClassName: res.data.repositoryClassName || '',
      controllerNamespace: res.data.controllerNamespace || '',
      controllerClassName: res.data.controllerClassName || '',
      genFunction: res.data.genFunction || '1,2,3,4,5,6,7',
      isSqlDiff: res.data.isSqlDiff || 1,
      isSnowflakeId: res.data.isSnowflakeId || 1,
      isRepository: res.data.isRepository || 1,
      columns: res.data.columns || [],
      subTable: res.data.subTable || undefined
    }
    console.log('TableDetail - 设置表信息:', tableInfo.value)

    // 获取列信息
    try {
      console.log('TableDetail - 开始获取列信息，表ID:', id)
      const columnRes = await getColumns(id)
      console.log('TableDetail - 获取列信息原始响应:', columnRes)
      
      if (!columnRes || !columnRes.data) {
        console.error('TableDetail - 获取列信息响应无效:', columnRes)
        message.error(t('generator.table.detail.loadColumnsFailed'))
        return
      }

      if (columnRes.code !== 200) {
        console.error('TableDetail - 获取列信息失败:', columnRes.msg)
        message.error(columnRes.msg || t('common.failed'))
        return
      }

      console.log('TableDetail - 列信息响应数据:', columnRes.data)
      
      if (!columnRes.data || !Array.isArray(columnRes.data)) {
        console.error('TableDetail - 列信息数据格式无效:', columnRes.data)
        message.error(t('generator.table.detail.invalidColumnData'))
        columnList.value = []
        return
      }

      if (columnRes.data.length === 0) {
        console.log('TableDetail - 列信息数据为空数组')
        message.info(t('generator.table.detail.noColumns'))
        columnList.value = []
        return
      }

      // 处理列数据
      columnList.value = columnRes.data.map((column: TaktGenColumn): TaktGenColumn => {
        console.log('TableDetail - 处理列数据:', column)
        return {
          genColumnId: column.genColumnId || '',
          createBy: column.createBy || '',
          createTime: column.createTime || '',
          isDeleted: column.isDeleted || 0,
          updateBy: column.updateBy || '',
          updateTime: column.updateTime || '',
          deleteBy: column.deleteBy || '',
          deleteTime: column.deleteTime || '',
          remark: column.remark || '',
          tableId: column.tableId || id, // 使用传入的id作为tableId
          columnName: column.columnName || '',
          columnComment: column.columnComment || '',
          dbColumnType: column.dbColumnType || '',
          csharpType: column.csharpType || '',
          csharpColumn: column.csharpColumn || '',
          csharpLength: column.csharpLength || 0,
          csharpDecimalDigits: column.csharpDecimalDigits || 0,
          csharpField: column.csharpField || '',
          isIncrement: column.isIncrement || 0,
          isPrimaryKey: column.isPrimaryKey || 0,
          isRequired: column.isRequired || 0,
          isInsert: column.isInsert || 0,
          isEdit: column.isEdit || 0,
          isList: column.isList || 0,
          isQuery: column.isQuery || 0,
          queryType: column.queryType || 'EQ',
          isSort: column.isSort || 0,
          isExport: column.isExport || 0,
          displayType: column.displayType || 'input',
          dictType: column.dictType || '',
          orderNum: column.orderNum || 0

        }
      })
      console.log('TableDetail - 处理后的列数据:', columnList.value)
    } catch (error) {
      console.error('TableDetail - 获取列信息出错:', error)
      message.error(t('generator.table.detail.loadColumnsFailed'))
      columnList.value = []
    }
  } catch (error) {
    console.error('TableDetail - 获取表信息出错:', error)
    message.error(t('common.failed'))
    tableInfo.value = undefined
    columnList.value = []
  }
}
</script>

<style lang="less" scoped>
:deep(.ant-descriptions-item-label) {
  width: 120px;
  font-weight: bold;
}

:deep(.ant-divider) {
  margin: 16px 0;
}

:deep(.ant-table) {
  margin-top: 16px;
}

:deep(.ant-tag) {
  margin: 0;
}

.empty-tip {
  padding: 32px 0;
  text-align: center;
}
</style> 
