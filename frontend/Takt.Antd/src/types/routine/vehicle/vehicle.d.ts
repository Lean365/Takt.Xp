/** 用车相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 车辆类型枚举 */
export enum TaktVehicleType {
  Sedan = 0,      // 轿车
  SUV = 1,        // SUV
  Business = 2,   // 商务车
  Van = 3         // 面包车
}

/** 车辆状态枚举 */
export enum TaktVehicleStatus {
  Idle = 0,       // 空闲
  InUse = 1,      // 使用中
  Maintenance = 2, // 维修中
  Scrapped = 3    // 已报废
}

/** 用车查询参数 */
export interface TaktVehicleQuery extends TaktPagedQuery {
  plateNumber?: string
  vehicleType?: TaktVehicleType
  status?: TaktVehicleStatus
  brand?: string
  model?: string
  color?: string
}

/** 用车数据传输对象 */
export interface TaktVehicle extends TaktBaseEntity {
  vehicleId: number
  plateNumber: string
  vehicleType: TaktVehicleType
  status: TaktVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车创建参数 */
export interface TaktVehicleCreate {
  plateNumber: string
  vehicleType: TaktVehicleType
  status: TaktVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车更新参数 */
export interface TaktVehicleUpdate extends TaktVehicleCreate {
  vehicleId: number
}

/** 用车删除参数 */
export interface TaktVehicleDelete {
  vehicleId: number
}

/** 用车批量删除参数 */
export interface TaktVehicleBatchDelete {
  vehicleIds: number[]
}

/** 用车导入参数 */
export interface TaktVehicleImport {
  plateNumber: string
  vehicleType: TaktVehicleType
  status: TaktVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车导出参数 */
export interface TaktVehicleExport {
  plateNumber: string
  vehicleType: TaktVehicleType
  status: TaktVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
  createTime: Date
}

/** 用车导入模板参数 */
export interface TaktVehicleTemplate {
  plateNumber: string
  vehicleType: TaktVehicleType
  status: TaktVehicleStatus
  brand?: string
  model?: string
  color?: string
  seatCount: number
  purchaseDate?: Date
  purchasePrice?: number
  currentMileage: number
  insuranceExpiryDate?: Date
  inspectionExpiryDate?: Date
}

/** 用车分页结果 */
export type TaktVehiclePagedResult = TaktPagedResult<TaktVehicle>

/** 用车统计信息 */
export interface TaktVehicleStatistics {
  totalVehicles: number
  idleVehicles: number
  inUseVehicles: number
  maintenanceVehicles: number
  scrappedVehicles: number
  totalMileage: number
  averageMileage: number
} 
