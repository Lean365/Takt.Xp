import type { TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 消息类型 */
export enum MessageType {
  /** 系统消息 */
  System = 'System',
  /** 邮件通知 */
  Email = 'Email',
  /** 通知状态 */
  Notification = 'Notification',
  /** 任务状态 */
  Task = 'Task',
  /** 个人通知 */
  Personal = 'Personal',
  /** 系统广播 */
  Broadcast = 'Broadcast'
}

/** 在线消息数据传输对象 - 对应后端 TaktSignalRMessageDto */
export interface TaktSignalRMessage {
  /** SignalR消息ID */
  signalRMessageId: string
  /** 发送者ID */
  senderId: string
  /** 接收者ID */
  receiverId: string
  /** 消息类型 */
  messageType: number
  /** 消息内容 */
  content: string
  /** 是否已读（0未读 1已读） */
  isRead: number
  /** 阅读时间 */
  readTime?: Date
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

/** 在线消息查询参数 - 对应后端 TaktSignalMessageQueryDto */
export interface TaktSignalRMessageQueryParams extends TaktPagedQuery {
  /** 发送者ID */
  senderId?: string
  /** 接收者ID */
  receiverId?: string
  /** 消息类型 */
  messageType?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
  /** 是否已读（0未读 1已读） */
  isRead?: number
}

/** 在线消息创建对象 - 对应后端 TaktSignalMessageCreateDto */
export interface TaktSignalRMessageCreateDto {
  /** 发送者ID */
  senderId: string
  /** 接收者ID */
  receiverId: string
  /** 消息类型 */
  messageType: number
  /** 消息内容 */
  content: string
  /** 是否已读（0未读 1已读） */
  isRead: number
}

/** 在线消息更新对象 - 对应后端 TaktSignalMessageUpdateDto */
export interface TaktSignalRMessageUpdateDto extends TaktSignalRMessageCreateDto {
  /** 阅读时间 */
  readTime: Date
}

/** 在线消息导出对象 - 对应后端 TaktSignalMessageExportDto */
export interface TaktSignalRMessageExportDto {
  /** 发送者ID */
  senderId: string
  /** 接收者ID */
  receiverId: string
  /** 消息类型 */
  messageType: number
  /** 消息内容 */
  content: string
  /** 是否已读（0未读 1已读） */
  isRead: number
  /** 阅读时间 */
  readTime?: Date
}

/** 在线消息分页结果 */
export type TaktSignalRMessagePageResult = TaktPagedResult<TaktSignalRMessage> 
