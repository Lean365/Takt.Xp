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
          <a-form-item label="工厂代码" name="plantCode">
            <Takt-select
              v-model:value="form.plantCode"
              dict-type="plant_code"
              :placeholder="'请选择工厂代码'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="生产日期" name="prodDate">
            <a-date-picker
              v-model:value="form.prodDate"
              style="width: 100%"
              placeholder="请选择生产日期"
            />
          </a-form-item>
        </a-col>
      </a-row>
      
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="生产线" name="prodLine">
            <Takt-select
              v-model:value="form.prodLine"
              dict-type="prod_line"
              :placeholder="'请选择生产线'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="班次" name="shiftNo">
            <Takt-select
              v-model:value="form.shiftNo"
              dict-type="shift_type"
              :placeholder="'请选择班次'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="生产订单类型" name="prodOrderType">
            <a-input v-model:value="form.prodOrderType" placeholder="请输入生产订单类型" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="生产订单号" name="prodOrderCode">
            <Takt-select
              v-model:value="form.prodOrderCode"
              dict-type="prod_order_code"
              :placeholder="'请选择生产订单号'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="型号代码" name="modelCode">
            <a-input v-model:value="form.modelCode" placeholder="请输入型号代码" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="状态" name="status">
            <Takt-select
              v-model:value="form.status"
              dict-type="sys_normal_disable"
              :placeholder="'请选择状态'"
              :show-all="false"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="物料代码" name="materialCode">
            <a-input v-model:value="form.materialCode" placeholder="请输入物料代码" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="批次号" name="batchNo">
            <a-input v-model:value="form.batchNo" placeholder="请输入批次号" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="直接人员" name="directLabor">
            <a-input-number
              v-model:value="form.directLabor"
              style="width: 100%"
              placeholder="请输入直接人员数量"
              :min="0"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="间接人员" name="indirectLabor">
            <a-input-number
              v-model:value="form.indirectLabor"
              style="width: 100%"
              placeholder="请输入间接人员数量"
              :min="0"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="订单数量" name="prodOrderQty">
            <a-input-number
              v-model:value="form.prodOrderQty"
              style="width: 100%"
              placeholder="请输入订单数量"
              :min="0"
              :precision="3"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="标准工时(分钟)" name="stdMinutes">
            <a-input-number
              v-model:value="form.stdMinutes"
              style="width: 100%"
              placeholder="请输入标准工时"
              :min="0"
              :precision="2"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="标准产能" name="stdCapacity">
            <a-input-number
              v-model:value="form.stdCapacity"
              style="width: 100%"
              placeholder="请输入标准产能"
              :min="0"
              :precision="3"
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

    <!-- 从表数据区域 -->
    <div class="detail-section">
      <div class="detail-header">
        <span class="title">生产明细</span>
        <a-button type="primary" size="small" @click="handleAddDetail">
          <template #icon><PlusOutlined /></template>
          添加明细
        </a-button>
      </div>
      
             <a-table
         :columns="detailColumns"
         :data-source="detailList"
         :pagination="false"
         size="small"
         :row-key="(record: DetailItem) => record.tempId || record.assyOutputDetailId"
       >
         <template #bodyCell="{ column, record, index }">
                       <template v-if="column.key === 'timePeriod'">
              <div v-if="(record as DetailItem).editing">
                <Takt-select
                  v-model:value="(record as DetailItem).timePeriod"
                  dict-type="time_period"
                  size="small"
                  :placeholder="'请选择生产时段'"
                  :show-all="false"
                />
              </div>
              <span v-else>{{ (record as DetailItem).timePeriod }}</span>
            </template>
           
           <template v-else-if="column.key === 'prodActualQty'">
             <div v-if="record.editing">
               <a-input-number 
                 v-model:value="record.prodActualQty" 
                 size="small" 
                 style="width: 100%"
                 placeholder="请输入实际生产数量"
                 :min="0"
                 :precision="3"
               />
             </div>
             <span v-else>{{ record.prodActualQty }}</span>
           </template>
           
                       <template v-else-if="column.key === 'downtimeMinutes'">
              <div v-if="(record as DetailItem).editing">
                <a-input-number 
                  v-model:value="(record as DetailItem).downtimeMinutes" 
                  size="small" 
                  style="width: 100%"
                  placeholder="请输入停线时间"
                  :min="0"
                  :precision="2"
                />
              </div>
              <span v-else>{{ (record as DetailItem).downtimeMinutes }}</span>
            </template>
            
            <template v-else-if="column.key === 'downtimeReason'">
              <div v-if="(record as DetailItem).editing">
                <Takt-select
                  v-model:value="(record as DetailItem).downtimeReason"
                  dict-type="downtime_reason"
                  size="small"
                  mode="multiple"
                  :placeholder="'请选择停线原因'"
                  :show-all="false"
                />
              </div>
              <span v-else>{{ (record as DetailItem).downtimeReason }}</span>
            </template>
            
            <template v-else-if="column.key === 'downtimeDescription'">
              <div v-if="(record as DetailItem).editing">
                <a-input 
                  v-model:value="(record as DetailItem).downtimeDescription" 
                  size="small" 
                  placeholder="请输入停线说明"
                />
              </div>
              <span v-else>{{ (record as DetailItem).downtimeDescription }}</span>
            </template>
            
            <template v-else-if="column.key === 'unachievedReason'">
              <div v-if="(record as DetailItem).editing">
                <Takt-select
                  v-model:value="(record as DetailItem).unachievedReason"
                  dict-type="unachieved_reason"
                  size="small"
                  mode="multiple"
                  :placeholder="'请选择未达成原因'"
                  :show-all="false"
                />
              </div>
              <span v-else>{{ (record as DetailItem).unachievedReason }}</span>
            </template>
            
            <template v-else-if="column.key === 'unachievedDescription'">
              <div v-if="(record as DetailItem).editing">
                <a-input 
                  v-model:value="(record as DetailItem).unachievedDescription" 
                  size="small" 
                  placeholder="请输入未达成说明"
                />
              </div>
              <span v-else>{{ (record as DetailItem).unachievedDescription }}</span>
            </template>
            
                        <template v-else-if="column.key === 'inputMinutes'">
              <div v-if="(record as DetailItem).editing">
                <a-input-number 
                  v-model:value="(record as DetailItem).inputMinutes" 
                  size="small" 
                  style="width: 100%"
                  placeholder="请输入投入工时"
                  :min="0"
                  :precision="2"
                />
              </div>
              <span v-else>{{ (record as DetailItem).inputMinutes }}</span>
            </template>
            
            <template v-else-if="column.key === 'prodMinutes'">
              <div v-if="(record as DetailItem).editing">
                <a-input-number 
                  v-model:value="(record as DetailItem).prodMinutes" 
                  size="small" 
                  style="width: 100%"
                  placeholder="请输入生产工时"
                  :min="0"
                  :precision="2"
                />
              </div>
              <span v-else>{{ (record as DetailItem).prodMinutes }}</span>
            </template>
            
            <template v-else-if="column.key === 'actualMinutes'">
              <div v-if="(record as DetailItem).editing">
                <a-input-number 
                  v-model:value="(record as DetailItem).actualMinutes" 
                  size="small" 
                  style="width: 100%"
                  placeholder="请输入实际工时"
                  :min="0"
                  :precision="2"
                />
              </div>
              <span v-else>{{ (record as DetailItem).actualMinutes }}</span>
            </template>
            
            <template v-else-if="column.key === 'achievementRate'">
              <div v-if="(record as DetailItem).editing">
                <a-input-number 
                  v-model:value="(record as DetailItem).achievementRate" 
                  size="small" 
                  style="width: 100%"
                  placeholder="请输入达成率"
                  :min="0"
                  :max="100"
                  :precision="2"
                />
              </div>
              <span v-else>{{ (record as DetailItem).achievementRate }}</span>
            </template>
            
            <template v-else-if="column.key === 'action'">
              <div v-if="(record as DetailItem).editing">
                <a-button type="link" size="small" @click="handleSaveDetail(index)">保存</a-button>
                <a-button type="link" size="small" @click="handleCancelEditDetail(index)">取消</a-button>
              </div>
              <div v-else>
                <a-button type="link" size="small" @click="handleEditDetail(index)">编辑</a-button>
                <a-button type="link" size="small" danger @click="handleDeleteDetail(index)">删除</a-button>
              </div>
            </template>
         </template>
       </a-table>
    </div>

    
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktAssyOutput } from '@/types/logistics/manufacturing/execution/output/assyOutput'
import type { TaktAssyOutputDetail } from '@/types/logistics/manufacturing/execution/output/assyOutputDetail'
import {
  createAssyOutput,
  updateAssyOutput
} from '@/api/logistics/manufacturing/execution/output/assyOutput'
import { getDictDataByType } from '@/api/routine/core/dictData'

interface Props {
  open: boolean
  title: string
  assyMpOutputId?: number
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

// 字典数据状态
const dictData = ref<{
  plantCode: any[]
  prodLine: any[]
  status: any[]
  timePeriod: any[]
  downtimeReason: any[]
  unachievedReason: any[]
}>({
  plantCode: [],
  prodLine: [],
  status: [],
  timePeriod: [],
  downtimeReason: [],
  unachievedReason: []
})

// 从表相关状态
type DetailItem = Omit<TaktAssyOutputDetail, 'downtimeReason' | 'unachievedReason'> & { 
  tempId?: string; 
  editing?: boolean;
  downtimeReason: string | string[];
  unachievedReason: string | string[];
}
const detailList = ref<DetailItem[]>([])

const form = reactive<Partial<TaktAssyOutput>>({
  assyOutputId: undefined,
  plantCode: '',
  prodDate: '',
  prodLine: '',
  shiftNo: 1,
  prodOrderType: '',
  prodOrderCode: '',
  modelCode: '',
  materialCode: '',
  batchNo: '',
  directLabor: 0,
  indirectLabor: 0,
  prodOrderQty: 0,
  stdMinutes: 0,
  stdCapacity: 0,
  status: 0,
  remark: ''
})

// 从表列定义
const detailColumns = [
  { title: '生产时段', dataIndex: 'timePeriod', key: 'timePeriod', width: 120 },
  { title: '实际生产数量', dataIndex: 'prodActualQty', key: 'prodActualQty', width: 120 },
  { title: '停线时间(分钟)', dataIndex: 'downtimeMinutes', key: 'downtimeMinutes', width: 120 },
  { title: '停线原因', dataIndex: 'downtimeReason', key: 'downtimeReason', width: 120 },
  { title: '停线说明', dataIndex: 'downtimeDescription', key: 'downtimeDescription', width: 120 },
  { title: '未达成原因', dataIndex: 'unachievedReason', key: 'unachievedReason', width: 120 },
  { title: '未达成说明', dataIndex: 'unachievedDescription', key: 'unachievedDescription', width: 120 },
  { title: '投入工时(分钟)', dataIndex: 'inputMinutes', key: 'inputMinutes', width: 120 },
  { title: '生产工时(分钟)', dataIndex: 'prodMinutes', key: 'prodMinutes', width: 120 },
  { title: '实际工时(分钟)', dataIndex: 'actualMinutes', key: 'actualMinutes', width: 120 },
  { title: '达成率(%)', dataIndex: 'achievementRate', key: 'achievementRate', width: 100 },
  { title: '操作', key: 'action', width: 120 }
]

const rules: any = {
  plantCode: [{ required: true, message: '请输入工厂代码', trigger: 'blur' }],
  prodDate: [{ required: true, message: '请选择生产日期', trigger: 'change' }],
  prodLine: [{ required: true, message: '请输入生产线', trigger: 'blur' }],
  shiftNo: [{ required: true, message: '请选择班次', trigger: 'change' }],
  prodOrderType: [{ required: false, message: '请输入生产订单类型', trigger: 'blur' }],
  status: [{ required: true, message: '请选择状态', trigger: 'change' }]
}

// 加载字典数据
const loadDictData = async () => {
  try {
    const [plantCodeRes, prodLineRes, statusRes, timePeriodRes, downtimeReasonRes, unachievedReasonRes] = await Promise.all([
      getDictDataByType('plant_code'),
      getDictDataByType('prod_line'),
      getDictDataByType('sys_normal_disable'),
      getDictDataByType('time_period'),
      getDictDataByType('downtime_reason'),
      getDictDataByType('unachieved_reason')
    ])
    
    if (plantCodeRes.data.code === 200) dictData.value.plantCode = plantCodeRes.data.data
    if (prodLineRes.data.code === 200) dictData.value.prodLine = prodLineRes.data.data
    if (statusRes.data.code === 200) dictData.value.status = statusRes.data.data
    if (timePeriodRes.data.code === 200) dictData.value.timePeriod = timePeriodRes.data.data
    if (downtimeReasonRes.data.code === 200) dictData.value.downtimeReason = downtimeReasonRes.data.data
    if (unachievedReasonRes.data.code === 200) dictData.value.unachievedReason = unachievedReasonRes.data.data
  } catch (error) {
    console.error('加载字典数据失败:', error)
  }
}

// 监听ID变化，加载详情
watch(() => props.assyMpOutputId, (newId) => {
  if (newId) {
    // 编辑模式，加载详情
    loadDetail(newId)
  } else {
    // 新增模式，重置表单
    resetForm()
  }
})

// 组件挂载时加载字典数据
onMounted(() => {
  loadDictData()
})

// 加载详情
const loadDetail = async (id: number) => {
  try {
    // TODO: 实现获取详情的API调用
    // const res = await getTaktAssyMpOutput(id)
    // if (res.data.code === 200) {
    //   Object.assign(form, res.data.data)
    // }
  } catch (error) {
    console.error('获取详情失败:', error)
    message.error('获取详情失败')
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(form, {
    assyMpOutputId: undefined,
    plantCode: '',
    prodDate: '',
    prodLine: '',
    shiftNo: 1,
    prodOrderType: '',
    prodOrderCode: '',
    modelCode: '',
    materialCode: '',
    batchNo: '',
    directLabor: 0,
    indirectLabor: 0,
    prodOrderQty: 0,
    stdMinutes: 0,
    stdCapacity: 0,
    status: 0,
    remark: ''
  })
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    submitLoading.value = true
    
    if (form.assyOutputId) {
      // 更新
      const res = await updateAssyOutput(form as TaktAssyOutput)
      if (res.data.code === 200) {
        message.success('更新成功')
        emit('success')
      } else {
        message.error(res.data.msg || '更新失败')
      }
    } else {
      // 新增
      const res = await createAssyOutput(form as TaktAssyOutput)
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

// 从表相关方法
const handleAddDetail = () => {
  const newDetail: DetailItem = {
    assyOutputDetailId: 0,
    assyOutputId: 0,
    timePeriod: '',
    prodActualQty: 0,
    downtimeMinutes: 0,
    downtimeReason: [],
    downtimeDescription: '',
    unachievedReason: [],
    unachievedDescription: '',
    inputMinutes: 0,
    prodMinutes: 0,
    actualMinutes: 0,
    achievementRate: 0,
    createBy: '',
    createTime: '',
    updateBy: '',
    updateTime: '',
    isDeleted: 0,
    deleteBy: '',
    deleteTime: '',
    tempId: `temp_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`,
    editing: true
  }
  detailList.value.push(newDetail)
}

const handleEditDetail = (index: number) => {
  detailList.value[index].editing = true
}

const handleSaveDetail = (index: number) => {
  const record = detailList.value[index]
  // 验证必填字段
  if (!record.timePeriod || !record.prodActualQty || !record.downtimeMinutes || 
      !record.inputMinutes || !record.prodMinutes || !record.actualMinutes || !record.achievementRate) {
    message.error('请填写所有必填字段')
    return
  }
  record.editing = false
  message.success('保存成功')
}

const handleCancelEditDetail = (index: number) => {
  const record = detailList.value[index]
  if (record.tempId) {
    // 新增的记录，直接删除
    detailList.value.splice(index, 1)
  } else {
    // 编辑的记录，恢复原值
    record.editing = false
    // TODO: 这里可以恢复原始值
  }
}

const handleDeleteDetail = (index: number) => {
  detailList.value.splice(index, 1)
}
</script>

<style lang="less" scoped>
.detail-section {
  margin-top: 16px;
  padding: 16px;
  background-color: var(--ant-color-bg-container);
  border: 1px solid var(--ant-color-split);
  border-radius: 4px;
}

.detail-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 16px;
  padding-bottom: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  .title {
    font-size: 14px;
    font-weight: 500;
    color: var(--ant-color-text);
  }
}
</style>

