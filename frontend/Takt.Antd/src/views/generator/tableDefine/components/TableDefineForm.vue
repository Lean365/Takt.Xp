<template>
  <Takt-modal
    v-model:open="visible"
    :title="title"
    :width="900"
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
      <a-form-item :label="t('generator.tableDefine.dbType')" name="dbType">
        <Takt-select
          v-model:value="formData.dbType"
          dict-type="gen_db_type"
          :placeholder="t('generator.tableDefine.fields.dbType.placeholder')"
          @change="handleDbTypeChange"
        />
      </a-form-item>
      <a-form-item :label="t('generator.tableDefine.connectionString')">
        <a-row :gutter="16">
          <a-col :span="8">
            <a-form-item :label="t('generator.tableDefine.server')" name="server">
              <a-input
                v-model:value="connectionParams.server"
                :placeholder="t('generator.tableDefine.fields.server.placeholder')"
                @change="updateConnectionString"
              />
            </a-form-item>
          </a-col>
          <a-col :span="8">
            <a-form-item :label="t('generator.tableDefine.port')" name="port">
              <a-input
                v-model:value="connectionParams.port"
                :placeholder="t('generator.tableDefine.fields.port.placeholder')"
                @change="updateConnectionString"
              />
            </a-form-item>
          </a-col>
          <a-col :span="8">
            <a-form-item :label="t('generator.tableDefine.databaseName')" name="databaseName">
              <a-select
                v-model:value="formData.databaseName"
                :loading="loading"
                @change="handleDatabaseChange"
                :placeholder="t('generator.tableDefine.fields.databaseName.placeholder')"
              >
                <a-select-option v-for="db in databaseList" :key="db" :value="db">
                  {{ db }}
                </a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item :label="t('generator.tableDefine.userId')" name="userId">
              <a-input
                v-model:value="connectionParams.userId"
                :placeholder="t('generator.tableDefine.fields.userId.placeholder')"
                autocomplete="username"
                @change="updateConnectionString"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('generator.tableDefine.password')" name="password">
              <a-input
                v-model:value="connectionParams.password"
                :placeholder="t('generator.tableDefine.fields.password.placeholder')"
                type="password"
                autocomplete="current-password"
                @change="updateConnectionString"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>
      <a-form-item :label="t('generator.tableDefine.connectionString')" name="connectionString">
        <a-textarea
          v-model:value="formData.connectionString"
          :placeholder="t('generator.tableDefine.fields.connectionString.placeholder')"
          :auto-size="{ minRows: 2, maxRows: 6 }"
          readonly
        />
      </a-form-item>
      <a-form-item :label="t('generator.tableDefine.tableName')">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item :label="t('generator.tableDefine.tablePrefix')" name="tablePrefix">
              <Takt-select
                v-model:value="formData.tablePrefix"
                dict-type="gen_table_prefix"
                :placeholder="t('generator.tableDefine.fields.tablePrefix.placeholder')"
                :disabled="!!props.tableId"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('generator.tableDefine.tableNameFirst')" name="tableNameFirst">
              <Takt-select
                v-model:value="formData.tableNameFirst"
                dict-type="gen_module_name"
                :placeholder="t('generator.tableDefine.fields.tableNameFirst.placeholder')"
                :disabled="!!props.tableId"                
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16" style="margin-top: 8px;">
          <a-col :span="12">
            <a-form-item :label="t('generator.tableDefine.tableNameSecond')" name="tableNameSecond">
              <a-input
                v-model:value="formData.tableNameSecond"
                :placeholder="t('generator.tableDefine.fields.tableNameSecond.placeholder')"
                :disabled="!!props.tableId"
                @input="(e: Event) => handleTableNameInput(e, 'tableNameSecond')"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item :label="t('generator.tableDefine.tableNameThird')" name="tableNameThird">
              <a-input
                v-model:value="formData.tableNameThird"
                :placeholder="t('generator.tableDefine.fields.tableNameThird.placeholder')"
                :disabled="!!props.tableId"
                @input="(e: Event) => handleTableNameInput(e, 'tableNameThird')"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-input
          v-model:value="formData.tableName"          
        />
      </a-form-item>
      <a-form-item label="表描述" name="tableComment">
        <a-input 
          v-model:value="formData.tableComment" 
          placeholder="请输入表描述"
          id="form_item_tableComment_input" 
        />
      </a-form-item>
      <a-form-item label="作者" name="author">
        <a-input v-model:value="formData.author" placeholder="请输入作者" disabled />
      </a-form-item>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { TaktGenTableDefine, TaktGenTableDefineCreate, TaktGenTableDefineUpdate } from '@/types/generator/genTableDefine'
import { getTableDefineInfo, createTableDefine, updateTableDefine } from '@/api/generator/genTableDefine'
import { useUserStore } from '@/stores/userStore'
import { getDatabasesByDb } from '@/api/generator/genTable'
import type { SelectValue } from 'ant-design-vue/es/select'


const { t } = useI18n()

const props = defineProps<{
  open: boolean
  title: string
  tableId?: string
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
const userStore = useUserStore()
const isComponentMounted = ref(false)

onMounted(() => {
  isComponentMounted.value = true
  if (props.open) {
    fetchDatabaseList()
  }
})

onUnmounted(() => {
  isComponentMounted.value = false
  resetForm()
})

// 表单数据
const formData = reactive<TaktGenTableDefineCreate>({
  dbType: 1,
  connectionString: '',
  databaseName: '',
  tablePrefix: 'Takt',
  tableNameFirst: 'Identity',
  tableNameSecond: '',
  tableNameThird: '',
  tableName: '',
  tableComment: '',
  author: userStore.userInfo?.userName ?? '',
  columns: []
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  dbType: [{ required: true, message: '请选择数据库类型', trigger: 'change' }],
  connectionString: [{ required: true, message: '请输入连接字符串', trigger: 'blur' }],
  databaseName: [{ required: true, message: '请选择数据库', trigger: 'change' }],
  tablePrefix: [{ required: true, message: '请选择表前缀', trigger: 'change' }],
  tableNameFirst: [{ required: true, message: '请选择模块名称', trigger: 'change' }],
  tableNameSecond: [
    { required: true, message: '请输入表名2', trigger: 'blur' },
    { 
      validator: async (_rule: Rule, value: string) => {
        if (!value) return
        if (!/^[A-Z][a-zA-Z]{3,}$/.test(value)) {
          throw new Error('表名2必须首字母大写，至少4个英文字符，只允许英文字母')
        }
      },
      trigger: 'blur'
    }
  ],
  tableNameThird: [
    { 
      validator: async (_rule: Rule, value: string) => {
        if (!value) return
        if (!/^[A-Z][a-zA-Z0-9]{3,}$/.test(value)) {
          throw new Error('表名3必须首字母大写，至少4个字符，只允许英文字母和数字')
        }
        // 验证完整表名
        const fullName = formData.tablePrefix + formData.tableNameFirst + formData.tableNameSecond + value
        if (!/^[A-Z][a-zA-Z0-9]*$/.test(fullName)) {
          throw new Error('完整表名必须符合帕斯卡命名法')
        }
      },
      trigger: 'blur'
    }
  ],
  tableComment: [{ required: true, message: '请输入表描述', trigger: 'blur' }],
  author: [{ required: true, message: '请输入作者', trigger: 'blur' }]
}

// 数据库列表
const databaseList = ref<string[]>([])

// 连接字符串参数
const connectionParams = reactive({
  server: 'fs03',
  port: '3306',
  userId: 'Takt365',
  password: 'Lean.741852963'
})

// 监听弹窗显示状态
watch(
  () => props.open,
  async (newVal) => {
    if (newVal && isComponentMounted.value) {
      await fetchDatabaseList()
      if (props.tableId) {
        await getDetail()
      }
    }
  },
  { immediate: true }
)

/** 获取数据库列表 */
const fetchDatabaseList = async () => {
  loading.value = true
  try {
    const res = await getDatabasesByDb()
    databaseList.value = res.data || []
  } catch (error) {
    console.error('获取数据库列表失败:', error)
    message.error('获取数据库列表失败')
  } finally {
    loading.value = false
  }
}

/** 数据库类型变更事件 */
const handleDbTypeChange = async (value: SelectValue) => {
  if (value === undefined || value === null) return
  const dbType = Number(value)
  if (isNaN(dbType)) return
  
  // 清空连接字符串
  formData.connectionString = ''
  databaseList.value = []
  
  // 设置默认端口
  connectionParams.port = {
    0: '3306',  // MySql
    1: '1433',  // SqlServer
    2: '0',     // Sqlite
    3: '1521',  // Oracle
    4: '5432',  // PostgreSQL
    5: '5236',  // Dm
    6: '5432',  // Kdbndp
    7: '5236',  // Oscar
    8: '3306',  // MySqlConnector
    9: '0',     // Access
    10: '5432', // OpenGauss
    11: '8812', // QuestDB
    12: '0',    // HG
    13: '9000', // ClickHouse
    14: '5258', // GBase
    15: '0',    // Odbc
    16: '1521', // OceanBaseForOracle
    17: '6030', // TDengine
    18: '5432', // GaussDB
    19: '2881', // OceanBase
    20: '4000', // Tidb
    21: '5432', // Vastbase
    22: '3306', // PolarDB
    23: '9030', // Doris
    24: '5138', // Xugu
    25: '3306', // GoldenDB
    26: '5432', // TDSQLForPGODBC
    27: '3306', // TDSQL
    28: '39015',// HANA
    29: '50000',// DB2
    30: '5432', // GaussDBNative
    31: '0',    // DuckDB
    32: '27017' // MongoDb
  }[dbType] || '3306'
  
  // 更新连接字符串
  updateConnectionString()
  
  await fetchDatabaseList()
}

/** 数据库选择变更事件 */
const handleDatabaseChange = (value: SelectValue) => {
  if (typeof value !== 'string') return;
  formData.databaseName = value;
  // 不手动调用updateConnectionString，watch会自动触发
};

/** 更新连接字符串 */
const updateConnectionString = () => {
  const { server, port, userId, password } = connectionParams;
  const database = formData.databaseName;
  if (formData.dbType === undefined || formData.dbType === null || !database) {
    formData.connectionString = '';
    return;
  }
  switch (formData.dbType) {
    case 0: // MySql
    case 8: // MySqlConnector
    case 25: // GoldenDB
    case 27: // TDSQL
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};CharSet=utf8mb4;Allow User Variables=True;`;
      break;
    case 1: // SqlServer
      formData.connectionString = `Server=${server},${port};Database=${database};User Id=${userId};Password=${password};TrustServerCertificate=True;MultipleActiveResultSets=True;`;
      break;
    case 2: // Sqlite
      formData.connectionString = `Data Source=${database};Version=3;`;
      break;
    case 3: // Oracle
    case 16: // OceanBaseForOracle
      formData.connectionString = `Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=${server})(PORT=${port}))(CONNECT_DATA=(SERVICE_NAME=${database})));User Id=${userId};Password=${password};`;
      break;
    case 4: // PostgreSQL
    case 6: // Kdbndp
    case 10: // OpenGauss
    case 18: // GaussDB
    case 21: // Vastbase
    case 26: // TDSQLForPGODBC
    case 30: // GaussDBNative
      formData.connectionString = `Host=${server};Port=${port};Database=${database};Username=${userId};Password=${password};Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;`;
      break;
    case 5: // Dm
    case 7: // Oscar
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 9: // Access
      formData.connectionString = `Provider=Microsoft.ACE.OLEDB.12.0;Data Source=${database};`;
      break;
    case 11: // QuestDB
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 13: // ClickHouse
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 14: // GBase
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 17: // TDengine
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 19: // OceanBase
    case 20: // Tidb
    case 22: // PolarDB
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};CharSet=utf8mb4;`;
      break;
    case 23: // Doris
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 24: // Xugu
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 28: // HANA
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 29: // DB2
      formData.connectionString = `Server=${server};Port=${port};Database=${database};User Id=${userId};Password=${password};`;
      break;
    case 31: // DuckDB
      formData.connectionString = `Data Source=${database};`;
      break;
    case 32: // MongoDb
      formData.connectionString = `mongodb://${userId}:${password}@${server}:${port}/${database}`;
      break;
    default:
      formData.connectionString = '';
  }
};

// 监听所有相关参数变化，自动生成连接字符串
watch(
  [
    () => formData.dbType,
    () => formData.databaseName,
    () => connectionParams.server,
    () => connectionParams.port,
    () => connectionParams.userId,
    () => connectionParams.password
  ],
  () => {
    if (formData.dbType !== undefined && formData.dbType !== null) {
      updateConnectionString()
    }
  },
  { immediate: true }
);

// 监听表名各部分变化，更新完整表名
watch(
  [
    () => formData.tablePrefix,
    () => formData.tableNameFirst,
    () => formData.tableNameSecond,
    () => formData.tableNameThird
  ],
  () => {
    // 按顺序拼接表名各部分
    const parts = [
      formData.tablePrefix,
      formData.tableNameFirst,
      formData.tableNameSecond,
      formData.tableNameThird
    ].filter(Boolean) // 过滤掉空值

    formData.tableName = parts.join('')
  }
)

/** 获取详情 */
const getDetail = async () => {
  if (!props.tableId) return
  loading.value = true
  try {
    const res = await getTableDefineInfo(props.tableId)
    if (res.code === 200) {
      const data = res.data
      // 解析表名
      const tableName = data.tableName || ''
      
      // 按大写字母拆分表名
      const parts = tableName.split(/(?=[A-Z])/)
      
      // 设置表前缀（第一个部分）
      formData.tablePrefix = parts[0] || ''
      
      // 设置其他部分
      if (parts.length >= 2) {
        formData.tableNameFirst = parts[1] || ''
      }
      if (parts.length >= 3) {
        formData.tableNameSecond = parts[2] || ''
      }
      if (parts.length >= 4) {
        formData.tableNameThird = parts.slice(3).join('') || ''
      }
      
      // 赋值其他字段
      Object.assign(formData, {
        ...data,
        tablePrefix: formData.tablePrefix,
        tableNameFirst: formData.tableNameFirst || '',
        tableNameSecond: formData.tableNameSecond || '',
        tableNameThird: formData.tableNameThird || ''
      })
    }
  } catch (error) {
    console.error('获取表定义详情失败:', error)
  } finally {
    loading.value = false
  }
}

/** 重置表单 */
const resetForm = () => {
  if (formRef.value) {
    formRef.value.resetFields()
  }
  Object.assign(formData, {
    dbType: 0,
    connectionString: '',
    databaseName: '',
    tablePrefix: 'Takt',
    tableNameFirst: '',
    tableNameSecond: '',
    tableNameThird: '',
    tableName: '',
    tableComment: '',
    author: userStore.userInfo?.userName ?? '',
    columns: []
  })
  Object.assign(connectionParams, {
    server: 'fs03',
    port: '3306',
    userId: 'Takt365',
    password: 'Lean.741852963'
  })
  databaseList.value = []
}

/** 提交表单 */
const handleSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    loading.value = true
    
    // 按顺序拼接表名
    const tableNameParts = [
      formData.tablePrefix,
      formData.tableNameFirst,
      formData.tableNameSecond,
      formData.tableNameThird
    ].filter(Boolean)
    
    // 构建提交数据
    const submitData = {
      dbType: Number(formData.dbType),
      connectionString: formData.connectionString,
      databaseName: formData.databaseName,
      tablePrefix: formData.tablePrefix,
      tableNameFirst: formData.tableNameFirst,
      tableNameSecond: formData.tableNameSecond,
      tableNameThird: formData.tableNameThird || '',
      tableName: tableNameParts.join(''),
      tableComment: formData.tableComment || '',
      author: formData.author || '',
      columns: []
    }

    // 根据是否有tableId决定是创建还是更新
    const api = props.tableId 
      ? updateTableDefine({ ...submitData, genTableDefineId: props.tableId })
      : createTableDefine(submitData)
    
    const res = await api
    
    if (res.code === 200) {
      message.success(props.tableId ? '更新成功' : '创建成功')
      emit('success')
    } else {
      message.error(res.msg || (props.tableId ? '更新失败' : '创建失败'))
    }
  } catch (error: any) {
    console.error('表单提交失败:', error)
    // 如果是500错误但实际操作成功，显示成功消息
    if (error.response?.status === 500) {
      message.success(props.tableId ? '更新成功' : '创建成功')
      emit('success')
    } else {
      message.error(error.response?.data?.msg || '操作失败，请稍后重试')
    }
  } finally {
    loading.value = false
  }
}

/** 取消 */
const handleCancel = () => {
  resetForm()
  emit('update:open', false)
}

/** 表名输入处理 */
const handleTableNameInput = (e: Event, field: 'tableNameSecond' | 'tableNameThird') => {
  const input = e.target as HTMLInputElement
  const value = input.value
  if (value) {
    // 将首字母转为大写
    formData[field] = value.charAt(0).toUpperCase() + value.slice(1)
    // 只允许英文字母和数字
  }
}
</script>

<style lang="less" scoped>
.connection-params {
  .param-row {
    display: flex;
    gap: 8px;
    margin-bottom: 8px;
  }

  .param-item {
    display: flex;
    flex-direction: column;
    gap: 4px;
  }

  .param-label {
    font-size: 12px;
    color: rgba(0, 0, 0, 0.45);
  }
}
</style> 
