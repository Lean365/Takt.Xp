import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 设备日志对象 - 与后端TaktDeviceLogDto.cs完全一致
 */
export interface TaktDeviceLog extends TaktBaseEntity {
  /** ID */
  deviceLogId: string
  /** 用户ID */
  userId: string
  /** 设备标识 */
  deviceId: string
  /** 设备类型（Desktop、Laptop、Mobile、Tablet等） */
  deviceType: string
  /** 设备令牌 */
  deviceToken?: string
  /** 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub） */
  loginType: number
  /** 登录来源（0Web 1App 2小程序 3其他） */
  loginSource: number
  /** 登录提供者（0系统 1微信 2钉钉 3企业微信） */
  loginProvider: number
  /** 提供者Key */
  providerKey: string
  /** 提供者显示名称 */
  providerDisplayName: string
  /** 网络类型（WIFI、移动数据、电信数据、联通数据、其他） */
  networkType: string
  /** 时区 */
  timeZone: string
  /** 语言 */
  language: string
  /** 是否VPN(否、是) */
  isVpn: string
  /** 是否代理（非代理、代理） */
  isProxy: string
  /** 首次登录时间 */
  firstLoginTime: string
  /** 首次登录IP */
  firstLoginIp: string
  /** 首次登录地点 */
  firstLoginLocation: string
  /** 首次登录设备ID */
  firstLoginDeviceId: string
  /** 首次登录设备类型（PC、Android、iOS、MacOS、Linux、其他） */
  firstLoginDeviceType: string
  /** 首次登录浏览器类型（Chrome、Firefox、Edge、Safari、IE、其他） */
  firstLoginBrowser: string
  /** 首次登录操作系统类型（Windows、Android、iOS、MacOS、Linux、其他） */
  firstLoginOs: string
  /** 最后登录时间 */
  lastLoginTime: string
  /** 最后登录IP */
  lastLoginIp: string
  /** 最后登录地点 */
  lastLoginLocation: string
  /** 最后登录设备ID */
  lastLoginDeviceId: string
  /** 最后登录设备类型（PC、Android、iOS、MacOS、Linux、其他） */
  lastLoginDeviceType: string
  /** 最后登录浏览器类型（Chrome、Firefox、Edge、Safari、IE、其他） */
  lastLoginBrowser: string
  /** 最后登录操作系统类型（Windows、Android、iOS、MacOS、Linux、其他） */
  lastLoginOs: string
  /** 最后在线时间 */
  lastOnlineTime: string
  /** 最后离线时间 */
  lastOfflineTime?: string
  /** 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"]) */
  todayOnlinePeriods?: string
  /** 总登录次数 */
  loginCount: number
  /** 连续登录天数 */
  continuousLoginDays: number
  /** 状态（0正常 1停用） */
  deviceStatus: number
  /** 备注 */
  remark?: string
  /** 创建者 */
  createBy: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy: string
  /** 更新时间 */
  updateTime: string
  /** 是否删除（0未删除 1已删除） */
  isDeleted: number
  /** 删除者 */
  deleteBy?: string
  /** 删除时间 */
  deleteTime?: string
}

/**
 * 设备日志查询参数 - 与后端TaktDeviceLogQueryDto完全一致
 */
export interface TaktDeviceLogQuery extends TaktPagedQuery {
  /** 用户ID */
  userId?: string
  /** 设备类型 */
  deviceType: string
  /** 登录类型 */
  loginType?: number
  /** 登录来源 */
  loginSource?: number
  /** 状态 */
  deviceStatus?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}


/**
 * 设备日志分页结果
 */
export type TaktDeviceLogPageResult = TaktPagedResult<TaktDeviceLog>

