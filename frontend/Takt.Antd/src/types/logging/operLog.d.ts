//===================================================================
// 项目名 : Lean.Takt
// 文件名称: operLog.d.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 操作日志类型定义（顺序规范）
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 操作日志记录
 */
export interface TaktOperLog extends TaktBaseEntity {
  /** 日志ID */
  operLogId: number
  /** 日志级别 */
  logLevel: number
  /** 用户ID */
  userId: number
  /** 用户名 */
  userName: string
  /** 操作类型 */
  operationType: string
  /** 表名 */
  tableName: string
  /** 业务主键 */
  businessKey: string
  /** 请求方法 */
  requestMethod: string
  /** 请求参数 */
  requestParam?: string
  /** IP地址 */
  ipAddress: string
  /** 操作地点 */
  location?: string
  /** 操作状态（0正常 1异常） */
  status: number
  /** 错误消息 */
  errorMsg?: string
}

/**
 * 操作日志查询参数
 */
export interface TaktOperLogQuery extends TaktPagedQuery {
  /** 用户名 */
  userName?: string
  /** 操作类型 */
  operationType?: string
  /** 表名 */
  tableName?: string
  /** IP地址 */
  ipAddress?: string
  /** 操作状态 */
  status?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}



/**
 * 操作日志分页结果
 */
export type TaktOperLogPageResult = TaktPagedResult<TaktOperLog> 
