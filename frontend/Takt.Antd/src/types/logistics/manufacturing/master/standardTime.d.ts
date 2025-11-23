/** 标准工时相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery } from '@/types/common'

/** 标准工时 */
export interface TaktStandardTime extends TaktBaseEntity {
  /** 标准工时ID */
  standardTimeId: number
  /** 物料编码 */
  materialCode: string
  /** 工作中心 */
  workCenter: string
  /** 标准工时(分钟) */
  standardMinutes: number
  /** 标准点数 */
  standardShorts: number
  /** 时间单位 */
  timeUnit: string
  /** 点数单位 */
  shortsUnit: string
  /** 生效日期 */
  effectiveDate?: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 标准工时查询条件 */
export interface TaktStandardTimeQuery extends TaktPagedQuery {
  /** 物料编码 */
  materialCode?: string
  /** 工作中心 */
  workCenter?: string
  /** 状态 */
  status?: number
  /** 开始日期 */
  startDate?: string
  /** 结束日期 */
  endDate?: string
}

/** 创建标准工时 */
export interface TaktStandardTimeCreate {
  /** 物料编码 */
  materialCode: string
  /** 工作中心 */
  workCenter: string
  /** 标准工时(分钟) */
  standardMinutes: number
  /** 标准点数 */
  standardShorts: number
  /** 时间单位 */
  timeUnit: string
  /** 点数单位 */
  shortsUnit: string
  /** 生效日期 */
  effectiveDate?: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 更新标准工时 */
export interface TaktStandardTimeUpdate {
  /** 标准工时ID */
  standardTimeId: number
  /** 物料编码 */
  materialCode: string
  /** 工作中心 */
  workCenter: string
  /** 标准工时(分钟) */
  standardMinutes: number
  /** 标准点数 */
  standardShorts: number
  /** 时间单位 */
  timeUnit: string
  /** 点数单位 */
  shortsUnit: string
  /** 生效日期 */
  effectiveDate?: string
  /** 失效日期 */
  expiryDate?: string
  /** 状态 */
  status: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 标准工时状态更新 */
export interface TaktStandardTimeStatus {
  /** 标准工时ID */
  standardTimeId: number
  /** 状态 */
  status: number
}

/** 标准工时导入 */
export interface TaktStandardTimeImport {
  /** 物料编码 */
  materialCode: string
  /** 工作中心 */
  workCenter: string
  /** 标准工时(分钟) */
  standardMinutes: number
  /** 标准点数 */
  standardShorts: number
  /** 时间单位 */
  timeUnit: string
  /** 点数单位 */
  shortsUnit: string
  /** 生效日期 */
  effectiveDate?: string
  /** 失效日期 */
  expiryDate?: string
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}

/** 标准工时导出 */
export interface TaktStandardTimeExport {
  /** 标准工时ID */
  standardTimeId: number
  /** 物料编码 */
  materialCode: string
  /** 工作中心 */
  workCenter: string
  /** 标准工时(分钟) */
  standardMinutes: number
  /** 标准点数 */
  standardShorts: number
  /** 时间单位 */
  timeUnit: string
  /** 点数单位 */
  shortsUnit: string
  /** 生效日期 */
  effectiveDate?: string
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

/** 标准工时模板 */
export interface TaktStandardTimeTemplate {
  /** 物料编码 */
  materialCode: string
  /** 工作中心 */
  workCenter: string
  /** 标准工时(分钟) */
  standardMinutes: number
  /** 标准点数 */
  standardShorts: number
  /** 时间单位 */
  timeUnit: string
  /** 点数单位 */
  shortsUnit: string
  /** 生效日期 */
  effectiveDate?: string
  /** 失效日期 */
  expiryDate?: string
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remarks?: string
}


