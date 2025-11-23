//===================================================================
// 项目名 : Lean.Takt
// 文件名 : assyMpOutput.d.ts
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 组立日报类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'
import type { TaktAssyOutputDetail } from './assyOutputDetail'

/**
 * 组立日报实体
 */
export interface TaktAssyOutput extends TaktBaseEntity {
  /** 组立日报ID */
  assyOutputId: number
  /** 工厂代码 */
  plantCode: string
  /** 生产类别 */
  prodCategory: string
  /** 生产日期 */
  prodDate: string
  /** 生产线 */
  prodLine: string
  /** 直接人员 */
  directLabor: number
  /** 间接人员 */
  indirectLabor: number
  /** 班次号 */
  shiftNo: number
  /** 生产订单类型 */
  prodOrderType?: string
  /** 生产订单号 */
  prodOrderCode: string
  /** 型号代码 */
  modelCode: string
  /** 物料代码 */
  materialCode: string
  /** 批次号 */
  batchNo?: string
  /** 订单数量 */
  prodOrderQty: number
  /** 标准工时 */
  stdMinutes: number
  /** 标准产能 */
  stdCapacity: number
  /** 状态 */
  status: number
  /** 生产明细列表 */
  assyOutputDetails?: TaktAssyOutputDetail[]
}

/**
 * 组立日报查询参数
 */
export interface TaktAssyOutputQuery extends TaktPagedQuery {
  /** 工厂代码 */
  plantCode?: string
  /** 生产类别 */
  prodCategory?: string
  /** 生产日期范围-开始日期 */
  startProdDate?: string
  /** 生产日期范围-结束日期 */
  endProdDate?: string
  /** 生产线 */
  prodLine?: string
  /** 生产订单类型 */
  prodOrderType?: string
  /** 生产工单号 */
  prodOrderCode?: string
  /** 机种 */
  modelCode?: string
  /** 物料编码 */
  materialCode?: string
  /** 批次 */
  batchNo?: string
  /** 状态 */
  status?: number
}

/**
 * 组立日报创建参数
 */
export interface TaktAssyOutputCreate {
  /** 工厂代码 */
  plantCode: string
  /** 生产类别 */
  prodCategory: string
  /** 生产日期 */
  prodDate: string
  /** 生产线 */
  prodLine: string
  /** 直接人员 */
  directLabor: number
  /** 间接人员 */
  indirectLabor: number
  /** 班次号 */
  shiftNo: number
  /** 生产订单类型 */
  prodOrderType?: string
  /** 生产订单号 */
  prodOrderCode: string
  /** 型号代码 */
  modelCode: string
  /** 物料代码 */
  materialCode: string
  /** 批次号 */
  batchNo?: string
  /** 订单数量 */
  prodOrderQty: number
  /** 标准工时 */
  stdMinutes: number
  /** 标准产能 */
  stdCapacity: number
  /** 状态 */
  status: number
  /** 组立明细列表 */
  assyOutputDetails?: TaktAssyOutputDetailCreate[]
}

/**
 * 组立日报更新参数
 */
export interface TaktAssyOutputUpdate extends TaktAssyOutputCreate {
  /** 组立日报ID */
  assyOutputId: number
}

/**
 * 组立日报导入参数
 */
export interface TaktAssyOutputImport {
  /** 工厂代码 */
  plantCode: string
  /** 生产类别 */
  prodCategory: string
  /** 生产日期 */
  prodDate: string
  /** 生产线 */
  prodLine: string
  /** 直接人员 */
  directLabor: number
  /** 间接人员 */
  indirectLabor: number
  /** 班次号 */
  shiftNo: number
  /** 生产订单类型 */
  prodOrderType?: string
  /** 生产订单号 */
  prodOrderCode: string
  /** 型号代码 */
  modelCode: string
  /** 物料代码 */
  materialCode: string
  /** 批次号 */
  batchNo?: string
  /** 订单数量 */
  prodOrderQty: number
  /** 标准工时 */
  stdMinutes: number
  /** 标准产能 */
  stdCapacity: number
  /** 状态 */
  status: number
}

/**
 * 组立日报导出参数
 */
export interface TaktAssyOutputExport {
  /** 工厂代码 */
  plantCode: string
  /** 生产类别 */
  prodCategory: string
  /** 生产日期 */
  prodDate: string
  /** 生产线 */
  prodLine: string
  /** 直接人员 */
  directLabor: number
  /** 间接人员 */
  indirectLabor: number
  /** 班次号 */
  shiftNo: number
  /** 生产订单类型 */
  prodOrderType?: string
  /** 生产订单号 */
  prodOrderCode: string
  /** 型号代码 */
  modelCode: string
  /** 物料代码 */
  materialCode: string
  /** 批次号 */
  batchNo?: string
  /** 订单数量 */
  prodOrderQty: number
  /** 标准工时 */
  stdMinutes: number
  /** 标准产能 */
  stdCapacity: number
  /** 状态 */
  status: number
}

/**
 * 组立日报导入模板
 */
export interface TaktAssyOutputTemplate {
  /** 工厂代码 */
  plantCode: string
  /** 生产类别 */
  prodCategory: string
  /** 生产日期 */
  prodDate: string
  /** 生产线 */
  prodLine: string
  /** 直接人员 */
  directLabor: number
  /** 间接人员 */
  indirectLabor: number
  /** 班次号 */
  shiftNo: number
  /** 生产订单类型 */
  prodOrderType?: string
  /** 生产订单号 */
  prodOrderCode: string
  /** 型号代码 */
  modelCode: string
  /** 物料代码 */
  materialCode: string
  /** 批次号 */
  batchNo?: string
  /** 订单数量 */
  prodOrderQty: number
  /** 标准工时 */
  stdMinutes: number
  /** 标准产能 */
  stdCapacity: number
  /** 状态 */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 组立日报分页结果
 */
export interface TaktAssyOutputPagedResult extends TaktPagedResult<TaktAssyOutput> {}

