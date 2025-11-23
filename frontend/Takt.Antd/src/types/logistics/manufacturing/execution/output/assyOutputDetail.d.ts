//===================================================================
// 项目名 : Lean.Takt
// 文件名 : assyMpOutputDetail.d.ts
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 组立生产明细类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 组立生产明细实体
 */
export interface TaktAssyOutputDetail extends TaktBaseEntity {
  /** 明细ID */
  assyOutputDetailId: number
  /** 组立日报ID */
  assyOutputId: number
  /** 生产时段 */
  timePeriod: string
  /** 实际生产数量 */
  prodActualQty: number
  /** 停线时间（分钟） */
  downtimeMinutes: number
  /** 停线原因 */
  downtimeReason?: string
  /** 停线说明 */
  downtimeDescription?: string
  /** 未达成原因 */
  unachievedReason?: string
  /** 未达成说明 */
  unachievedDescription?: string
  /** 投入工时（分钟） */
  inputMinutes: number
  /** 生产工时（分钟） */
  prodMinutes: number
  /** 实际工时（分钟） */
  actualMinutes: number
  /** 达成率（%） */
  achievementRate: number
}

/**
 * 组立生产明细查询参数
 */
export interface TaktAssyOutputDetailQuery extends TaktPagedQuery {
  /** 组立日报ID */
  assyOutputId?: number
  /** 生产时段 */
  timePeriod?: string
}

/**
 * 组立生产明细创建参数
 */
export interface TaktAssyOutputDetailCreate {
  /** 组立日报ID */
  assyOutputId: number
  /** 生产时段 */
  timePeriod: string
  /** 实际生产数量 */
  prodActualQty: number
  /** 停线时间（分钟） */
  downtimeMinutes: number
  /** 停线原因 */
  downtimeReason?: string
  /** 停线说明 */
  downtimeDescription?: string
  /** 未达成原因 */
  unachievedReason?: string
  /** 未达成说明 */
  unachievedDescription?: string
  /** 投入工时（分钟） */
  inputMinutes: number
  /** 生产工时（分钟） */
  prodMinutes: number
  /** 实际工时（分钟） */
  actualMinutes: number
  /** 达成率（%） */
  achievementRate: number
  /** 备注 */
  remark?: string

}

/**
 * 组立生产明细更新参数
 */
export interface TaktAssyOutputDetailUpdate extends TaktAssyOutputDetailCreate {
  /** 明细ID */
  assyOutputDetailId: number
}

/**
 * 组立生产明细导入参数
 */
export interface TaktAssyOutputDetailImport {
  /** 组立日报ID */
  assyOutputId: number
  /** 生产时段 */
  timePeriod: string
  /** 实际生产数量 */
  prodActualQty: number
  /** 停线时间（分钟） */
  downtimeMinutes: number
  /** 停线原因 */
  downtimeReason?: string
  /** 停线说明 */
  downtimeDescription?: string
  /** 未达成原因 */
  unachievedReason?: string
  /** 未达成说明 */
  unachievedDescription?: string
  /** 投入工时（分钟） */
  inputMinutes: number
  /** 生产工时（分钟） */
  prodMinutes: number
  /** 实际工时（分钟） */
  actualMinutes: number
  /** 达成率（%） */
  achievementRate: number
}

/**
 * 组立生产明细导出参数
 */
export interface TaktAssyOutputDetailExport {
  /** 组立日报ID */
  assyOutputId: number
  /** 生产时段 */
  timePeriod: string
  /** 实际生产数量 */
  prodActualQty: number
  /** 停线时间（分钟） */
  downtimeMinutes: number
  /** 停线原因 */
  downtimeReason?: string
  /** 停线说明 */
  downtimeDescription?: string
  /** 未达成原因 */
  unachievedReason?: string
  /** 未达成说明 */
  unachievedDescription?: string
  /** 投入工时（分钟） */
  inputMinutes: number
  /** 生产工时（分钟） */
  prodMinutes: number
  /** 实际工时（分钟） */
  actualMinutes: number
  /** 达成率（%） */
  achievementRate: number

}

/**
 * 组立生产明细模板
 */
export interface TaktAssyOutputDetailTemplate {
  /** 组立日报ID */
  assyOutputId: number
  /** 生产时段 */
  timePeriod: string
  /** 实际生产数量 */
  prodActualQty: number
  /** 停线时间（分钟） */
  downtimeMinutes: number
  /** 停线原因 */
  downtimeReason?: string
  /** 停线说明 */
  downtimeDescription?: string
  /** 未达成原因 */
  unachievedReason?: string
  /** 未达成说明 */
  unachievedDescription?: string
  /** 投入工时（分钟） */
  inputMinutes: number
  /** 生产工时（分钟） */
  prodMinutes: number
  /** 实际工时（分钟） */
  actualMinutes: number
  /** 达成率（%） */
  achievementRate: number
}

/**
 * 组立生产明细分页结果
 */
export interface TaktAssyOutputDetailPagedResult extends TaktPagedResult<TaktAssyOutputDetail> {}

