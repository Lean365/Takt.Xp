/** 驾驶员相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 驾驶员状态枚举 */
export enum TaktDriverStatus {
  OnDuty = 0,      // 在职
  Resigned = 1,    // 离职
  Vacation = 2,    // 休假
  Suspended = 3,   // 停职
  Other = 4        // 其他
}

/** 驾驶证类型枚举 */
export enum TaktLicenseType {
  A1 = 0,          // A1
  A2 = 1,          // A2
  A3 = 2,          // A3
  B1 = 3,          // B1
  B2 = 4,          // B2
  C1 = 5,          // C1
  C2 = 6,          // C2
  D = 7,           // D
  E = 8,           // E
  F = 9,           // F
  M = 10,          // M
  N = 11,          // N
  P = 12           // P
}

/** 驾驶证状态枚举 */
export enum TaktLicenseStatus {
  Normal = 0,      // 正常
  Expiring = 1,    // 即将到期
  Expired = 2,     // 已过期
  Revoked = 3,     // 被吊销
  Cancelled = 4    // 被注销
}

/** 性别枚举 */
export enum TaktGender {
  Male = 0,        // 男
  Female = 1       // 女
}

/** 驾驶经验枚举 */
export enum TaktDrivingExperience {
  Beginner = 0,    // 新手
  Normal = 1,      // 一般
  Skilled = 2,     // 熟练
  Expert = 3       // 专家
}

/** 工作时间枚举 */
export enum TaktWorkSchedule {
  DayShift = 0,    // 白班
  NightShift = 1,  // 夜班
  FullTime = 2,    // 全天
  Flexible = 3     // 灵活
}

/** 驾驶员查询参数 */
export interface TaktDriverQuery extends TaktPagedQuery {
  employeeName?: string
  employeeNo?: string
  licenseNo?: string
  department?: string
  status?: TaktDriverStatus
  licenseType?: TaktLicenseType
  licenseStatus?: TaktLicenseStatus
  gender?: TaktGender
  isFullTime?: number
}

/** 驾驶员数据传输对象 */
export interface TaktDriver extends TaktBaseEntity {
  driverId: number
  employeeId: number
  employeeName: string
  employeeNo: string
  department?: string
  position?: string
  status: TaktDriverStatus
  licenseNo: string
  licenseType: TaktLicenseType
  licenseAuthority?: string
  licenseIssueDate?: Date
  licenseExpiryDate: Date
  licenseStatus: TaktLicenseStatus
  licensePoints: number
  licenseImages?: string
  idCardNo?: string
  gender: TaktGender
  birthDate?: Date
  age?: number
  phone?: string
  emergencyContact?: string
  emergencyPhone?: string
  homeAddress?: string
  hireDate?: Date
  resignDate?: Date
  drivingYears?: number
  drivingExperience: TaktDrivingExperience
  drivableVehicles?: string
  drivingSkillScore?: number
  safetyScore?: number
  serviceScore?: number
  overallScore?: number
  accidentCount: number
  violationCount: number
  complaintCount: number
  praiseCount: number
  isFullTime: number
  canDriveHazardous: number
  canDriveLarge: number
  canDrivePassenger: number
  workArea?: string
  workSchedule: TaktWorkSchedule
  baseSalary?: number
  performanceSalary?: number
  allowance?: number
  totalSalary?: number
  bankAccount?: string
  bankName?: string
  accountHolder?: string
  healthCertNo?: string
  healthCertExpiry?: Date
  healthCertImages?: string
  trainingCertificates?: string
  skillCertificates?: string
}

/** 驾驶员创建参数 */
export interface TaktDriverCreate {
  employeeId: number
  employeeName: string
  employeeNo: string
  department?: string
  position?: string
  status: TaktDriverStatus
  licenseNo: string
  licenseType: TaktLicenseType
  licenseAuthority?: string
  licenseIssueDate?: Date
  licenseExpiryDate: Date
  licenseStatus: TaktLicenseStatus
  licensePoints: number
  licenseImages?: string
  idCardNo?: string
  gender: TaktGender
  birthDate?: Date
  age?: number
  phone?: string
  emergencyContact?: string
  emergencyPhone?: string
  homeAddress?: string
  hireDate?: Date
  resignDate?: Date
  drivingYears?: number
  drivingExperience: TaktDrivingExperience
  drivableVehicles?: string
  drivingSkillScore?: number
  safetyScore?: number
  serviceScore?: number
  overallScore?: number
  accidentCount: number
  violationCount: number
  complaintCount: number
  praiseCount: number
  isFullTime: number
  canDriveHazardous: number
  canDriveLarge: number
  canDrivePassenger: number
  workArea?: string
  workSchedule: TaktWorkSchedule
  baseSalary?: number
  performanceSalary?: number
  allowance?: number
  totalSalary?: number
  bankAccount?: string
  bankName?: string
  accountHolder?: string
  healthCertNo?: string
  healthCertExpiry?: Date
  healthCertImages?: string
  trainingCertificates?: string
  skillCertificates?: string
}

/** 驾驶员更新参数 */
export interface TaktDriverUpdate extends TaktDriverCreate {
  driverId: number
}

/** 驾驶员删除参数 */
export interface TaktDriverDelete {
  driverId: number
}

/** 驾驶员批量删除参数 */
export interface TaktDriverBatchDelete {
  driverIds: number[]
}

/** 驾驶员导入参数 */
export interface TaktDriverImport {
  employeeId: number
  employeeName: string
  employeeNo: string
  department?: string
  position?: string
  status: TaktDriverStatus
  licenseNo: string
  licenseType: TaktLicenseType
  licenseAuthority?: string
  licenseIssueDate?: Date
  licenseExpiryDate: Date
  licenseStatus: TaktLicenseStatus
  licensePoints: number
  licenseImages?: string
  idCardNo?: string
  gender: TaktGender
  birthDate?: Date
  age?: number
  phone?: string
  emergencyContact?: string
  emergencyPhone?: string
  homeAddress?: string
  hireDate?: Date
  resignDate?: Date
  drivingYears?: number
  drivingExperience: TaktDrivingExperience
  drivableVehicles?: string
  drivingSkillScore?: number
  safetyScore?: number
  serviceScore?: number
  overallScore?: number
  accidentCount: number
  violationCount: number
  complaintCount: number
  praiseCount: number
  isFullTime: number
  canDriveHazardous: number
  canDriveLarge: number
  canDrivePassenger: number
  workArea?: string
  workSchedule: TaktWorkSchedule
  baseSalary?: number
  performanceSalary?: number
  allowance?: number
  totalSalary?: number
  bankAccount?: string
  bankName?: string
  accountHolder?: string
  healthCertNo?: string
  healthCertExpiry?: Date
  healthCertImages?: string
  trainingCertificates?: string
  skillCertificates?: string
}

/** 驾驶员导出参数 */
export interface TaktDriverExport {
  employeeName: string
  employeeNo: string
  department?: string
  position?: string
  status: TaktDriverStatus
  licenseNo: string
  licenseType: TaktLicenseType
  licenseAuthority?: string
  licenseIssueDate?: Date
  licenseExpiryDate: Date
  licenseStatus: TaktLicenseStatus
  licensePoints: number
  idCardNo?: string
  gender: TaktGender
  birthDate?: Date
  age?: number
  phone?: string
  emergencyContact?: string
  emergencyPhone?: string
  homeAddress?: string
  hireDate?: Date
  resignDate?: Date
  drivingYears?: number
  drivingExperience: TaktDrivingExperience
  drivableVehicles?: string
  drivingSkillScore?: number
  safetyScore?: number
  serviceScore?: number
  overallScore?: number
  accidentCount: number
  violationCount: number
  complaintCount: number
  praiseCount: number
  isFullTime: number
  canDriveHazardous: number
  canDriveLarge: number
  canDrivePassenger: number
  workArea?: string
  workSchedule: TaktWorkSchedule
  baseSalary?: number
  performanceSalary?: number
  allowance?: number
  totalSalary?: number
  bankAccount?: string
  bankName?: string
  accountHolder?: string
  healthCertNo?: string
  healthCertExpiry?: Date
  trainingCertificates?: string
  skillCertificates?: string
  createTime: Date
}

/** 驾驶员导入模板参数 */
export interface TaktDriverTemplate {
  employeeId: number
  employeeName: string
  employeeNo: string
  department?: string
  position?: string
  status: TaktDriverStatus
  licenseNo: string
  licenseType: TaktLicenseType
  licenseAuthority?: string
  licenseIssueDate?: Date
  licenseExpiryDate: Date
  licenseStatus: TaktLicenseStatus
  licensePoints: number
  idCardNo?: string
  gender: TaktGender
  birthDate?: Date
  age?: number
  phone?: string
  emergencyContact?: string
  emergencyPhone?: string
  homeAddress?: string
  hireDate?: Date
  resignDate?: Date
  drivingYears?: number
  drivingExperience: TaktDrivingExperience
  drivableVehicles?: string
  drivingSkillScore?: number
  safetyScore?: number
  serviceScore?: number
  overallScore?: number
  accidentCount: number
  violationCount: number
  complaintCount: number
  praiseCount: number
  isFullTime: number
  canDriveHazardous: number
  canDriveLarge: number
  canDrivePassenger: number
  workArea?: string
  workSchedule: TaktWorkSchedule
  baseSalary?: number
  performanceSalary?: number
  allowance?: number
  totalSalary?: number
  bankAccount?: string
  bankName?: string
  accountHolder?: string
  healthCertNo?: string
  healthCertExpiry?: Date
  trainingCertificates?: string
  skillCertificates?: string
}

/** 驾驶员分页结果 */
export type TaktDriverPagedResult = TaktPagedResult<TaktDriver>

/** 驾驶员统计信息 */
export interface TaktDriverStatistics {
  totalDrivers: number
  onDutyDrivers: number
  resignedDrivers: number
  vacationDrivers: number
  suspendedDrivers: number
  otherDrivers: number
  expiringLicenses: number
  expiredLicenses: number
  revokedLicenses: number
  averageScore: number
  totalAccidents: number
  totalViolations: number
} 
