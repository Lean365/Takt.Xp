/** 生产工单相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 生产工单实体 */
export interface TaktProdOrder extends TaktBaseEntity {
  // 基础字段 - 严格按照TaktProdOrder.cs的顺序
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  changeLogs?: TaktProdOrderChangeLog[]
  [key: string]: any
}

/** 生产工单查询参数 */
export interface TaktProdOrderQuery extends TaktPagedQuery {
  plantCode?: string
  prodOrderType?: string
  prodOrderCode?: string
  materialCode?: string
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  status?: number
  startDate?: string
  endDate?: string
  [key: string]: any
}

/** 生产工单创建参数 */
export interface TaktProdOrderCreate {
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  changeLogs?: TaktProdOrderChangeLogCreate[]
  [key: string]: any
}

/** 生产工单更新参数 */
export interface TaktProdOrderUpdate extends TaktProdOrderCreate {
  prodOrderId: number
}

/** 生产工单导入参数 */
export interface TaktProdOrderImport {
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  [key: string]: any
}

/** 生产工单导出参数 */
export interface TaktProdOrderExport {
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  createTime: string
  [key: string]: any
}

/** 生产工单模板参数 */
export interface TaktProdOrderTemplate {
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  [key: string]: any
}

/** 生产工单状态更新参数 */
export interface TaktProdOrderStatus {
  id: number
  status: number
}

/** 生产工单变更记录 */
export interface TaktProdOrderChangeLog extends TaktBaseEntity {
  prodOrderChangeLogId: number
  prodOrderId: number
  // 业务字段 - 按照TaktProdOrder.cs的顺序
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  // 变更记录特有字段
  changeType: number
  changeDate: string
  changeUser: string
  changeReason?: string
  beforeValue?: string
  afterValue?: string
  changeField?: string
  beforeFieldValue?: string
  afterFieldValue?: string
  [key: string]: any
}

/** 生产工单变更记录创建参数 */
export interface TaktProdOrderChangeLogCreate {
  prodOrderId: number
  // 业务字段 - 按照TaktProdOrder.cs的顺序
  plantCode: string
  prodOrderType: string
  prodOrderCode: string
  materialCode: string
  prodOrderQty: number
  producedQty: number
  unitOfMeasure: string
  actualStartDate?: string
  actualEndDate?: string
  Priority: number
  workCenter?: string
  prodLine?: string
  prodBatch?: string
  serialNo?: string
  routingCode?: string
  status: number
  // 变更记录特有字段
  changeType: number
  changeDate: string
  changeUser: string
  changeReason?: string
  beforeValue?: string
  afterValue?: string
  changeField?: string
  beforeFieldValue?: string
  afterFieldValue?: string
  [key: string]: any
}

/** 生产工单分页结果 */
export type TaktProdOrderPagedResult = TaktPagedResult<TaktProdOrder>


