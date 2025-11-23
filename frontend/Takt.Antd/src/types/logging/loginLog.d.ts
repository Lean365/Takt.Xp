import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'
import type { TaktDeviceLog } from './deviceLog'

/** 登录日志数据 */
export interface TaktLoginLog extends TaktBaseEntity {
  /** 登录日志ID */
  loginLogId: string
  /** 用户ID */
  userId: string
  /** 用户名 */
  userName: string
  /** 登录类型 */
  loginType: number
  /** 登录状态 */
  loginStatus: number
  /** 登录来源 */
  loginSource: number
  /** 登录时间 */
  loginTime: string
  /** 登录IP */
  loginIp: string
  /** 登录位置 */
  loginLocation: string
  /** 设备ID */
  deviceId?: string
  /** 设备类型 */
  deviceType?: string
  /** 用户代理 */
  userAgent: string
  /** 消息 */
  loginMessage?: string
  /** 设备日志 */
  deviceLog?: TaktDeviceLog
  /** 创建时间 */
  createTime: string
}

/** 登录日志查询参数 */
export interface TaktLoginLogQuery extends TaktPagedQuery {
  /** 用户名 */
  userName?: string
  /** 设备类型 */
  deviceType?: string
  /** 登录状态 */
  loginStatus?: number
  /** 登录类型 */
  loginType?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}


/** 登录日志分页结果 */
export type TaktLoginLogPageResult = TaktPagedResult<TaktLoginLog> 
