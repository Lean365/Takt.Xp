/** 稼动率相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery } from '@/types/common'

/** 稼动率 */
export interface TaktOperationRate extends TaktBaseEntity {
  /** 稼动率ID */
  operationRateId: number
  /** 工厂代码 */
  plantCode: string
  /** 稼动率类型 */
  operationType: string
  /** 稼动率 */
  operationRate: number
  /** 生效日期 */
  effectiveDate: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 稼动率查询条件 */
export interface TaktOperationRateQuery extends TaktPagedQuery {
  /** 工厂代码 */
  plantCode?: string
  /** 稼动率类型 */
  operationType?: string
  /** 状态 */
  status?: number
  /** 开始日期 */
  startDate?: string
  /** 结束日期 */
  endDate?: string
}

/** 创建稼动率 */
export interface TaktOperationRateCreate {
  /** 工厂代码 */
  plantCode: string
  /** 稼动率类型 */
  operationType: string
  /** 稼动率 */
  operationRate: number
  /** 生效日期 */
  effectiveDate: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 更新稼动率 */
export interface TaktOperationRateUpdate {
  /** 稼动率ID */
  operationRateId: number
  /** 工厂代码 */
  plantCode: string
  /** 稼动率类型 */
  operationType: string
  /** 稼动率 */
  operationRate: number
  /** 生效日期 */
  effectiveDate: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 稼动率状态更新 */
export interface TaktOperationRateStatus {
  /** 稼动率ID */
  operationRateId: number
  /** 状态 */
  status: number
}

/** 稼动率导入 */
export interface TaktOperationRateImport {
  /** 工厂代码 */
  plantCode: string
  /** 稼动率类型 */
  operationType: string
  /** 稼动率 */
  operationRate: number
  /** 生效日期 */
  effectiveDate: string
  /** 失效日期 */
  expiryDate?: string
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 稼动率导出 */
export interface TaktOperationRateExport {
  /** 稼动率ID */
  operationRateId: number
  /** 工厂代码 */
  plantCode: string
  /** 稼动率类型 */
  operationType: string
  /** 稼动率 */
  operationRate: number
  /** 生效日期 */
  effectiveDate: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
  /** 创建时间 */
  createTime: string
  /** 创建人 */
  createBy: string
  /** 更新时间 */
  updateTime?: string
  /** 更新人 */
  updateBy?: string
}

/** 稼动率模板 */
export interface TaktOperationRateTemplate {
  /** 工厂代码 */
  plantCode: string
  /** 稼动率类型 */
  operationType: string
  /** 稼动率 */
  operationRate: number
  /** 生效日期 */
  effectiveDate: string
  /** 失效日期 */
  expiryDate?: string
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

