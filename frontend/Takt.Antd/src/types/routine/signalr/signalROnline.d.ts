import type { TaktPagedQuery, TaktPagedResult } from '../common'

/**
 * 在线用户信息 - 对应后端 TaktSignalROnlineDto
 */
export interface TaktSignalROnline {
  /** ID */
  signalRUserId: string
  /** 用户ID */
  userId: string
  /** 用户组ID */
  groupId: string
  /** 连接ID */
  connectionId: string
  /** 设备ID */
  deviceId: string
  /** 客户端IP */
  clientIp: string
  /** 用户代理 */
  userAgent: string
  /** 最后活动时间 */
  lastActivity: Date
  /** 在线状态（0=在线 1=离线） */
  onlineStatus: number
  /** 最后心跳时间 */
  lastHeartbeat: Date
  /** 总在线时长（分钟） */
  totalOnlineMinutes: number
  /** 今日在线时长（分钟） */
  todayOnlineMinutes: number
  /** 备注 */
  remark?: string
  /** 创建者 */
  createBy?: string
  /** 创建时间 */
  createTime: Date
  /** 更新者 */
  updateBy?: string
  /** 更新时间 */
  updateTime?: Date
  /** 是否删除（0未删除 1已删除） */
  isDeleted: number
  /** 删除者 */
  deleteBy?: string
  /** 删除时间 */
  deleteTime?: Date
}

/** 在线用户查询参数 - 对应后端 TaktSignalROnlineQueryDto */
export interface TaktSignalROnlineQuery extends TaktPagedQuery {
  /** 用户ID */
  userId?: string
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
  /** 在线状态（0=在线 1=离线） */
  onlineStatus?: number
}

/** 在线用户创建对象 - 对应后端 TaktSignalROnlineCreateDto */
export interface TaktSignalROnlineCreate {
  /** 用户ID */
  userId: string
  /** 用户组ID */
  groupId: string
  /** 连接ID */
  connectionId: string
  /** 设备ID */
  deviceId: string
  /** 客户端IP */
  clientIp: string
  /** 用户代理 */
  userAgent: string
  /** 在线状态（0=在线 1=离线） */
  onlineStatus: number
  /** 总在线时长（分钟） */
  totalOnlineMinutes: number
  /** 今日在线时长（分钟） */
  todayOnlineMinutes: number
}

/** 在线用户更新对象 - 对应后端 TaktSignalROnlineUpdateDto */
export interface TaktSignalROnlineUpdate extends TaktSignalROnlineCreate {
  /** ID */
  signalROnlineId: string
  /** 最后活动时间 */
  lastActivity: Date
  /** 最后心跳时间 */
  lastHeartbeat: Date
}

/** 在线用户状态更新对象 - 对应后端 TaktSignalROnlineStatusUpdateDto */
export interface TaktSignalROnlineStatusUpdate {
  /** ID */
  signalROnlineId: string
  /** 在线状态（0=在线 1=离线） */
  onlineStatus: number
  /** 最后活动时间 */
  lastActivity?: Date
  /** 最后心跳时间 */
  lastHeartbeat?: Date
  /** 总在线时长（分钟） */
  totalOnlineMinutes: number
  /** 今日在线时长（分钟） */
  todayOnlineMinutes: number
  /** 备注 */
  remark?: string
}

