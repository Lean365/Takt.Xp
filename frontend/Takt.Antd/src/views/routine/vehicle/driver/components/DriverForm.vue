<template>
  <a-modal
    v-model:open="visible"
    :title="title"
    width="1000px"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirm-loading="loading"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      layout="vertical"
      :label-col="{ span: 24 }"
      :wrapper-col="{ span: 24 }"
    >
      <!-- 基本信息 -->
      <a-divider>基本信息</a-divider>
      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="员工姓名" name="employeeName">
            <a-input
              v-model:value="formData.employeeName"
              placeholder="请输入员工姓名"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="员工工号" name="employeeNo">
            <a-input
              v-model:value="formData.employeeNo"
              placeholder="请输入员工工号"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="部门" name="department">
            <a-input
              v-model:value="formData.department"
              placeholder="请输入部门"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="职位" name="position">
            <a-input
              v-model:value="formData.position"
              placeholder="请输入职位"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾驶员状态" name="status">
            <a-select
              v-model:value="formData.status"
              placeholder="请选择驾驶员状态"
              :disabled="isView"
            >
              <a-select-option :value="0">在职</a-select-option>
              <a-select-option :value="1">离职</a-select-option>
              <a-select-option :value="2">休假</a-select-option>
              <a-select-option :value="3">停职</a-select-option>
              <a-select-option :value="4">其他</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="性别" name="gender">
            <a-select
              v-model:value="formData.gender"
              placeholder="请选择性别"
              :disabled="isView"
            >
              <a-select-option :value="0">男</a-select-option>
              <a-select-option :value="1">女</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="出生日期" name="birthDate">
            <a-date-picker
              v-model:value="formData.birthDate"
              placeholder="请选择出生日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="年龄" name="age">
            <a-input-number
              v-model:value="formData.age"
              placeholder="请输入年龄"
              style="width: 100%"
              :min="0"
              :max="150"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="联系电话" name="phone">
            <a-input
              v-model:value="formData.phone"
              placeholder="请输入联系电话"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="身份证号码" name="idCardNo">
            <a-input
              v-model:value="formData.idCardNo"
              placeholder="请输入身份证号码"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="入职日期" name="hireDate">
            <a-date-picker
              v-model:value="formData.hireDate"
              placeholder="请选择入职日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="离职日期" name="resignDate">
            <a-date-picker
              v-model:value="formData.resignDate"
              placeholder="请选择离职日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="紧急联系人" name="emergencyContact">
            <a-input
              v-model:value="formData.emergencyContact"
              placeholder="请输入紧急联系人"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="紧急联系电话" name="emergencyPhone">
            <a-input
              v-model:value="formData.emergencyPhone"
              placeholder="请输入紧急联系电话"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="家庭地址" name="homeAddress">
        <a-textarea
          v-model:value="formData.homeAddress"
          placeholder="请输入家庭地址"
          :rows="2"
          :disabled="isView"
        />
      </a-form-item>

      <!-- 驾驶证信息 -->
      <a-divider>驾驶证信息</a-divider>
      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="驾驶证号码" name="licenseNo">
            <a-input
              v-model:value="formData.licenseNo"
              placeholder="请输入驾驶证号码"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾驶证类型" name="licenseType">
            <a-select
              v-model:value="formData.licenseType"
              placeholder="请选择驾驶证类型"
              :disabled="isView"
            >
              <a-select-option :value="0">A1</a-select-option>
              <a-select-option :value="1">A2</a-select-option>
              <a-select-option :value="2">A3</a-select-option>
              <a-select-option :value="3">B1</a-select-option>
              <a-select-option :value="4">B2</a-select-option>
              <a-select-option :value="5">C1</a-select-option>
              <a-select-option :value="6">C2</a-select-option>
              <a-select-option :value="7">D</a-select-option>
              <a-select-option :value="8">E</a-select-option>
              <a-select-option :value="9">F</a-select-option>
              <a-select-option :value="10">M</a-select-option>
              <a-select-option :value="11">N</a-select-option>
              <a-select-option :value="12">P</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾驶证状态" name="licenseStatus">
            <a-select
              v-model:value="formData.licenseStatus"
              placeholder="请选择驾驶证状态"
              :disabled="isView"
            >
              <a-select-option :value="0">正常</a-select-option>
              <a-select-option :value="1">即将到期</a-select-option>
              <a-select-option :value="2">已过期</a-select-option>
              <a-select-option :value="3">被吊销</a-select-option>
              <a-select-option :value="4">被注销</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="驾驶证发证机关" name="licenseAuthority">
            <a-input
              v-model:value="formData.licenseAuthority"
              placeholder="请输入驾驶证发证机关"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾驶证发证日期" name="licenseIssueDate">
            <a-date-picker
              v-model:value="formData.licenseIssueDate"
              placeholder="请选择驾驶证发证日期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾驶证有效期" name="licenseExpiryDate">
            <a-date-picker
              v-model:value="formData.licenseExpiryDate"
              placeholder="请选择驾驶证有效期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="驾驶证扣分" name="licensePoints">
            <a-input-number
              v-model:value="formData.licensePoints"
              placeholder="请输入驾驶证扣分"
              style="width: 100%"
              :min="0"
              :max="12"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾龄（年）" name="drivingYears">
            <a-input-number
              v-model:value="formData.drivingYears"
              placeholder="请输入驾龄"
              style="width: 100%"
              :min="0"
              :max="50"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="驾驶经验" name="drivingExperience">
            <a-select
              v-model:value="formData.drivingExperience"
              placeholder="请选择驾驶经验"
              :disabled="isView"
            >
              <a-select-option :value="0">新手</a-select-option>
              <a-select-option :value="1">一般</a-select-option>
              <a-select-option :value="2">熟练</a-select-option>
              <a-select-option :value="3">专家</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="可驾驶车型" name="drivableVehicles">
        <a-textarea
          v-model:value="formData.drivableVehicles"
          placeholder="请输入可驾驶车型"
          :rows="2"
          :disabled="isView"
        />
      </a-form-item>

      <!-- 评分信息 -->
      <a-divider>评分信息</a-divider>
      <a-row :gutter="16">
        <a-col :span="6">
          <a-form-item label="驾驶技能评分" name="drivingSkillScore">
            <a-input-number
              v-model:value="formData.drivingSkillScore"
              placeholder="请输入驾驶技能评分"
              style="width: 100%"
              :min="0"
              :max="10"
              :precision="1"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="安全驾驶评分" name="safetyScore">
            <a-input-number
              v-model:value="formData.safetyScore"
              placeholder="请输入安全驾驶评分"
              style="width: 100%"
              :min="0"
              :max="10"
              :precision="1"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="服务态度评分" name="serviceScore">
            <a-input-number
              v-model:value="formData.serviceScore"
              placeholder="请输入服务态度评分"
              style="width: 100%"
              :min="0"
              :max="10"
              :precision="1"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="综合评分" name="overallScore">
            <a-input-number
              v-model:value="formData.overallScore"
              placeholder="请输入综合评分"
              style="width: 100%"
              :min="0"
              :max="10"
              :precision="1"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <!-- 记录信息 -->
      <a-divider>记录信息</a-divider>
      <a-row :gutter="16">
        <a-col :span="6">
          <a-form-item label="事故记录次数" name="accidentCount">
            <a-input-number
              v-model:value="formData.accidentCount"
              placeholder="请输入事故记录次数"
              style="width: 100%"
              :min="0"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="违章记录次数" name="violationCount">
            <a-input-number
              v-model:value="formData.violationCount"
              placeholder="请输入违章记录次数"
              style="width: 100%"
              :min="0"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="投诉记录次数" name="complaintCount">
            <a-input-number
              v-model:value="formData.complaintCount"
              placeholder="请输入投诉记录次数"
              style="width: 100%"
              :min="0"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="表扬记录次数" name="praiseCount">
            <a-input-number
              v-model:value="formData.praiseCount"
              placeholder="请输入表扬记录次数"
              style="width: 100%"
              :min="0"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <!-- 工作信息 -->
      <a-divider>工作信息</a-divider>
      <a-row :gutter="16">
        <a-col :span="6">
          <a-form-item label="是否专职司机" name="isFullTime">
            <a-switch
              v-model:checked="formData.isFullTime"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="可驾驶危险品车辆" name="canDriveHazardous">
            <a-switch
              v-model:checked="formData.canDriveHazardous"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="可驾驶大型车辆" name="canDriveLarge">
            <a-switch
              v-model:checked="formData.canDriveLarge"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="可驾驶客车" name="canDrivePassenger">
            <a-switch
              v-model:checked="formData.canDrivePassenger"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="工作区域" name="workArea">
            <a-input
              v-model:value="formData.workArea"
              placeholder="请输入工作区域"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="工作时间" name="workSchedule">
            <a-select
              v-model:value="formData.workSchedule"
              placeholder="请选择工作时间"
              :disabled="isView"
            >
              <a-select-option :value="0">白班</a-select-option>
              <a-select-option :value="1">夜班</a-select-option>
              <a-select-option :value="2">全天</a-select-option>
              <a-select-option :value="3">灵活</a-select-option>
            </a-select>
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="健康证号码" name="healthCertNo">
            <a-input
              v-model:value="formData.healthCertNo"
              placeholder="请输入健康证号码"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="健康证有效期" name="healthCertExpiry">
            <a-date-picker
              v-model:value="formData.healthCertExpiry"
              placeholder="请选择健康证有效期"
              style="width: 100%"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="培训证书" name="trainingCertificates">
            <a-input
              v-model:value="formData.trainingCertificates"
              placeholder="请输入培训证书"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="技能证书" name="skillCertificates">
            <a-input
              v-model:value="formData.skillCertificates"
              placeholder="请输入技能证书"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <!-- 薪资信息 -->
      <a-divider>薪资信息</a-divider>
      <a-row :gutter="16">
        <a-col :span="6">
          <a-form-item label="基本工资" name="baseSalary">
            <a-input-number
              v-model:value="formData.baseSalary"
              placeholder="请输入基本工资"
              style="width: 100%"
              :min="0"
              :precision="2"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="绩效工资" name="performanceSalary">
            <a-input-number
              v-model:value="formData.performanceSalary"
              placeholder="请输入绩效工资"
              style="width: 100%"
              :min="0"
              :precision="2"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="津贴" name="allowance">
            <a-input-number
              v-model:value="formData.allowance"
              placeholder="请输入津贴"
              style="width: 100%"
              :min="0"
              :precision="2"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="6">
          <a-form-item label="总工资" name="totalSalary">
            <a-input-number
              v-model:value="formData.totalSalary"
              placeholder="请输入总工资"
              style="width: 100%"
              :min="0"
              :precision="2"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="8">
          <a-form-item label="银行账户" name="bankAccount">
            <a-input
              v-model:value="formData.bankAccount"
              placeholder="请输入银行账户"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="开户银行" name="bankName">
            <a-input
              v-model:value="formData.bankName"
              placeholder="请输入开户银行"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
        <a-col :span="8">
          <a-form-item label="账户持有人" name="accountHolder">
            <a-input
              v-model:value="formData.accountHolder"
              placeholder="请输入账户持有人"
              :disabled="isView"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          placeholder="请输入备注"
          :rows="3"
          :disabled="isView"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import {
  getDriverById,
  createDriver,
  updateDriver
} from '@/api/routine/driver'
import type {
  TaktDriver,
  TaktDriverCreate,
  TaktDriverUpdate
} from '@/types/routine/vehicle/driver'

// Props
interface Props {
  visible: boolean
  title: string
  driverId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  title: '',
  driverId: undefined
})

// Emits
const emit = defineEmits<{
  'update:visible': [value: boolean]
  success: []
}>()

// 响应式数据
const loading = ref(false)
const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<TaktDriverCreate & { driverId?: number }>({
  employeeId: 0,
  employeeName: '',
  employeeNo: '',
  department: '',
  position: '',
  status: 0,
  licenseNo: '',
  licenseType: 5,
  licenseAuthority: '',
  licenseIssueDate: undefined,
  licenseExpiryDate: new Date(),
  licenseStatus: 0,
  licensePoints: 0,
  licenseImages: '',
  idCardNo: '',
  gender: 0,
  birthDate: undefined,
  age: undefined,
  phone: '',
  emergencyContact: '',
  emergencyPhone: '',
  homeAddress: '',
  hireDate: undefined,
  resignDate: undefined,
  drivingYears: undefined,
  drivingExperience: 1,
  drivableVehicles: '',
  drivingSkillScore: undefined,
  safetyScore: undefined,
  serviceScore: undefined,
  overallScore: undefined,
  accidentCount: 0,
  violationCount: 0,
  complaintCount: 0,
  praiseCount: 0,
  isFullTime: 0,
  canDriveHazardous: 0,
  canDriveLarge: 0,
  canDrivePassenger: 0,
  workArea: '',
  workSchedule: 0,
  baseSalary: undefined,
  performanceSalary: undefined,
  allowance: undefined,
  totalSalary: undefined,
  bankAccount: '',
  bankName: '',
  accountHolder: '',
  healthCertNo: '',
  healthCertExpiry: undefined,
  healthCertImages: '',
  trainingCertificates: '',
  skillCertificates: ''
})

// 表单验证规则
const rules = {
  employeeName: [{ required: true, message: '请输入员工姓名', type: 'string' }],
  employeeNo: [{ required: true, message: '请输入员工工号', type: 'string' }],
  status: [{ required: true, message: '请选择驾驶员状态', type: 'number' }],
  licenseNo: [{ required: true, message: '请输入驾驶证号码', type: 'string' }],
  licenseType: [{ required: true, message: '请选择驾驶证类型', type: 'number' }],
  licenseExpiryDate: [{ required: true, message: '请选择驾驶证有效期', type: 'object' }],
  licenseStatus: [{ required: true, message: '请选择驾驶证状态', type: 'number' }],
  gender: [{ required: true, message: '请选择性别', type: 'number' }],
  drivingExperience: [{ required: true, message: '请选择驾驶经验', type: 'number' }],
  isFullTime: [{ required: true, message: '请选择是否专职司机', type: 'number' }],
  canDriveHazardous: [{ required: true, message: '请选择是否可驾驶危险品车辆', type: 'number' }],
  canDriveLarge: [{ required: true, message: '请选择是否可驾驶大型车辆', type: 'number' }],
  canDrivePassenger: [{ required: true, message: '请选择是否可驾驶客车', type: 'number' }],
  workSchedule: [{ required: true, message: '请选择工作时间', type: 'number' }]
}

// 计算属性
const isView = computed(() => props.title.includes('查看'))

// 监听visible变化
watch(
  () => props.visible,
  (visible) => {
    if (visible && props.driverId) {
      getDetail()
    } else if (visible && !props.driverId) {
      resetForm()
    }
  }
)

// 获取详情
const getDetail = async () => {
  if (!props.driverId) return

  loading.value = true
  try {
    const res = await getDriverById(props.driverId)
    if (res.data?.data) {
      const data = res.data.data
      Object.assign(formData, {
        driverId: data.driverId,
        employeeId: data.employeeId,
        employeeName: data.employeeName,
        employeeNo: data.employeeNo,
        department: data.department,
        position: data.position,
        status: data.status,
        licenseNo: data.licenseNo,
        licenseType: data.licenseType,
        licenseAuthority: data.licenseAuthority,
        licenseIssueDate: data.licenseIssueDate ? new Date(data.licenseIssueDate) : undefined,
        licenseExpiryDate: data.licenseExpiryDate ? new Date(data.licenseExpiryDate) : new Date(),
        licenseStatus: data.licenseStatus,
        licensePoints: data.licensePoints,
        licenseImages: data.licenseImages,
        idCardNo: data.idCardNo,
        gender: data.gender,
        birthDate: data.birthDate ? new Date(data.birthDate) : undefined,
        age: data.age,
        phone: data.phone,
        emergencyContact: data.emergencyContact,
        emergencyPhone: data.emergencyPhone,
        homeAddress: data.homeAddress,
        hireDate: data.hireDate ? new Date(data.hireDate) : undefined,
        resignDate: data.resignDate ? new Date(data.resignDate) : undefined,
        drivingYears: data.drivingYears,
        drivingExperience: data.drivingExperience,
        drivableVehicles: data.drivableVehicles,
        drivingSkillScore: data.drivingSkillScore,
        safetyScore: data.safetyScore,
        serviceScore: data.serviceScore,
        overallScore: data.overallScore,
        accidentCount: data.accidentCount,
        violationCount: data.violationCount,
        complaintCount: data.complaintCount,
        praiseCount: data.praiseCount,
        isFullTime: data.isFullTime,
        canDriveHazardous: data.canDriveHazardous,
        canDriveLarge: data.canDriveLarge,
        canDrivePassenger: data.canDrivePassenger,
        workArea: data.workArea,
        workSchedule: data.workSchedule,
        baseSalary: data.baseSalary,
        performanceSalary: data.performanceSalary,
        allowance: data.allowance,
        totalSalary: data.totalSalary,
        bankAccount: data.bankAccount,
        bankName: data.bankName,
        accountHolder: data.accountHolder,
        healthCertNo: data.healthCertNo,
        healthCertExpiry: data.healthCertExpiry ? new Date(data.healthCertExpiry) : undefined,
        healthCertImages: data.healthCertImages,
        trainingCertificates: data.trainingCertificates,
        skillCertificates: data.skillCertificates
      })
    }
  } catch (error) {
    message.error('获取详情失败')
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(formData, {
    driverId: undefined,
    employeeId: 0,
    employeeName: '',
    employeeNo: '',
    department: '',
    position: '',
    status: 0,
    licenseNo: '',
    licenseType: 5,
    licenseAuthority: '',
    licenseIssueDate: undefined,
    licenseExpiryDate: new Date(),
    licenseStatus: 0,
    licensePoints: 0,
    licenseImages: '',
    idCardNo: '',
    gender: 0,
    birthDate: undefined,
    age: undefined,
    phone: '',
    emergencyContact: '',
    emergencyPhone: '',
    homeAddress: '',
    hireDate: undefined,
    resignDate: undefined,
    drivingYears: undefined,
    drivingExperience: 1,
    drivableVehicles: '',
    drivingSkillScore: undefined,
    safetyScore: undefined,
    serviceScore: undefined,
    overallScore: undefined,
    accidentCount: 0,
    violationCount: 0,
    complaintCount: 0,
    praiseCount: 0,
    isFullTime: 0,
    canDriveHazardous: 0,
    canDriveLarge: 0,
    canDrivePassenger: 0,
    workArea: '',
    workSchedule: 0,
    baseSalary: undefined,
    performanceSalary: undefined,
    allowance: undefined,
    totalSalary: undefined,
    bankAccount: '',
    bankName: '',
    accountHolder: '',
    healthCertNo: '',
    healthCertExpiry: undefined,
    healthCertImages: '',
    trainingCertificates: '',
    skillCertificates: ''
  })
  formRef.value?.clearValidate()
}

// 提交表单
const handleSubmit = async () => {
  if (isView.value) {
    handleCancel()
    return
  }

  try {
    await formRef.value?.validate()
    loading.value = true

    if (props.driverId) {
      // 更新
      const updateData: TaktDriverUpdate = {
        driverId: props.driverId,
        employeeId: formData.employeeId,
        employeeName: formData.employeeName,
        employeeNo: formData.employeeNo,
        department: formData.department,
        position: formData.position,
        status: formData.status,
        licenseNo: formData.licenseNo,
        licenseType: formData.licenseType,
        licenseAuthority: formData.licenseAuthority,
        licenseIssueDate: formData.licenseIssueDate,
        licenseExpiryDate: formData.licenseExpiryDate,
        licenseStatus: formData.licenseStatus,
        licensePoints: formData.licensePoints,
        licenseImages: formData.licenseImages,
        idCardNo: formData.idCardNo,
        gender: formData.gender,
        birthDate: formData.birthDate,
        age: formData.age,
        phone: formData.phone,
        emergencyContact: formData.emergencyContact,
        emergencyPhone: formData.emergencyPhone,
        homeAddress: formData.homeAddress,
        hireDate: formData.hireDate,
        resignDate: formData.resignDate,
        drivingYears: formData.drivingYears,
        drivingExperience: formData.drivingExperience,
        drivableVehicles: formData.drivableVehicles,
        drivingSkillScore: formData.drivingSkillScore,
        safetyScore: formData.safetyScore,
        serviceScore: formData.serviceScore,
        overallScore: formData.overallScore,
        accidentCount: formData.accidentCount,
        violationCount: formData.violationCount,
        complaintCount: formData.complaintCount,
        praiseCount: formData.praiseCount,
        isFullTime: formData.isFullTime,
        canDriveHazardous: formData.canDriveHazardous,
        canDriveLarge: formData.canDriveLarge,
        canDrivePassenger: formData.canDrivePassenger,
        workArea: formData.workArea,
        workSchedule: formData.workSchedule,
        baseSalary: formData.baseSalary,
        performanceSalary: formData.performanceSalary,
        allowance: formData.allowance,
        totalSalary: formData.totalSalary,
        bankAccount: formData.bankAccount,
        bankName: formData.bankName,
        accountHolder: formData.accountHolder,
        healthCertNo: formData.healthCertNo,
        healthCertExpiry: formData.healthCertExpiry,
        healthCertImages: formData.healthCertImages,
        trainingCertificates: formData.trainingCertificates,
        skillCertificates: formData.skillCertificates
      }
      await updateDriver(updateData)
      message.success('更新成功')
    } else {
      // 新增
      const createData: TaktDriverCreate = {
        employeeId: formData.employeeId,
        employeeName: formData.employeeName,
        employeeNo: formData.employeeNo,
        department: formData.department,
        position: formData.position,
        status: formData.status,
        licenseNo: formData.licenseNo,
        licenseType: formData.licenseType,
        licenseAuthority: formData.licenseAuthority,
        licenseIssueDate: formData.licenseIssueDate,
        licenseExpiryDate: formData.licenseExpiryDate,
        licenseStatus: formData.licenseStatus,
        licensePoints: formData.licensePoints,
        licenseImages: formData.licenseImages,
        idCardNo: formData.idCardNo,
        gender: formData.gender,
        birthDate: formData.birthDate,
        age: formData.age,
        phone: formData.phone,
        emergencyContact: formData.emergencyContact,
        emergencyPhone: formData.emergencyPhone,
        homeAddress: formData.homeAddress,
        hireDate: formData.hireDate,
        resignDate: formData.resignDate,
        drivingYears: formData.drivingYears,
        drivingExperience: formData.drivingExperience,
        drivableVehicles: formData.drivableVehicles,
        drivingSkillScore: formData.drivingSkillScore,
        safetyScore: formData.safetyScore,
        serviceScore: formData.serviceScore,
        overallScore: formData.overallScore,
        accidentCount: formData.accidentCount,
        violationCount: formData.violationCount,
        complaintCount: formData.complaintCount,
        praiseCount: formData.praiseCount,
        isFullTime: formData.isFullTime,
        canDriveHazardous: formData.canDriveHazardous,
        canDriveLarge: formData.canDriveLarge,
        canDrivePassenger: formData.canDrivePassenger,
        workArea: formData.workArea,
        workSchedule: formData.workSchedule,
        baseSalary: formData.baseSalary,
        performanceSalary: formData.performanceSalary,
        allowance: formData.allowance,
        totalSalary: formData.totalSalary,
        bankAccount: formData.bankAccount,
        bankName: formData.bankName,
        accountHolder: formData.accountHolder,
        healthCertNo: formData.healthCertNo,
        healthCertExpiry: formData.healthCertExpiry,
        healthCertImages: formData.healthCertImages,
        trainingCertificates: formData.trainingCertificates,
        skillCertificates: formData.skillCertificates
      }
      await createDriver(createData)
      message.success('创建成功')
    }

    emit('success')
  } catch (error) {
    console.error('表单提交失败:', error)
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}
</script>

<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 16px;
}
</style> 
