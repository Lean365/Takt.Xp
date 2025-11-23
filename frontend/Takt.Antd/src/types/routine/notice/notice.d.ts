/** 通知相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 通知实体
 */
export interface TaktNotice extends TaktBaseEntity {
  /** 通知ID */
  noticeId: number
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 通知状态（0未读 1已读） */
  noticeStatus: number
  /** 发送时间 */
  noticeSendTime?: string
  /** 阅读时间 */
  noticeReadTime?: string
  /** 接收人ID */
  noticeReceiverId: number
  /** 接收人名称 */
  noticeReceiverName: string
  /** 发送人ID */
  noticeSenderId: number
  /** 发送人名称 */
  noticeSenderName: string
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency: number
  /** 是否置顶（0否 1是） */
  isTop: number
  /** 是否重要（0否 1是） */
  isImportant: number
  /** 附件列表 */
  attachments?: string
  /** 相关链接 */
  relatedLinks?: string
  /** 过期时间 */
  expireTime?: string
  /** 阅读次数 */
  readCount: number
  /** 转发次数 */
  forwardCount: number
  /** 回复次数 */
  replyCount: number
}

/**
 * 通知查询参数
 */
export interface TaktNoticeQuery extends TaktPagedQuery {
  /** 通知标题 */
  noticeTitle?: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType?: number
  /** 通知状态（0未读 1已读） */
  noticeStatus?: number
  /** 发送人ID */
  noticeSenderId?: number
  /** 接收人ID */
  noticeReceiverId?: number
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority?: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency?: number
  /** 是否置顶（0否 1是） */
  isTop?: number
  /** 是否重要（0否 1是） */
  isImportant?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 发送时间范围 */
  sendTimeRange?: [string, string]
  /** 阅读时间范围 */
  readTimeRange?: [string, string]
}

/**
 * 通知创建参数
 */
export interface TaktNoticeCreate {
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 接收人ID列表 */
  receiverIds: number[]
  /** 接收人名称列表 */
  receiverNames: string[]
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency: number
  /** 是否置顶（0否 1是） */
  isTop: number
  /** 是否重要（0否 1是） */
  isImportant: number
  /** 附件列表 */
  attachments?: string
  /** 相关链接 */
  relatedLinks?: string
  /** 过期时间 */
  expireTime?: string
  /** 备注 */
  remark?: string
}

/**
 * 通知更新参数
 */
export interface TaktNoticeUpdate extends TaktNoticeCreate {
  /** 通知ID */
  noticeId: number
}

/**
 * 通知状态更新参数
 */
export interface TaktNoticeStatus {
  /** 通知ID */
  noticeId: number
  /** 通知状态（0未读 1已读） */
  noticeStatus: number
}

/**
 * 通知批量状态更新参数
 */
export interface TaktNoticeBatchStatus {
  /** 通知ID列表 */
  noticeIds: number[]
  /** 通知状态（0未读 1已读） */
  noticeStatus: number
}

/**
 * 通知发送参数
 */
export interface TaktNoticeSend {
  /** 通知ID */
  noticeId: number
  /** 发送时间 */
  sendTime?: string
  /** 发送人ID */
  senderId: number
  /** 发送人名称 */
  senderName: string
}

/**
 * 通知阅读参数
 */
export interface TaktNoticeRead {
  /** 通知ID */
  noticeId: number
  /** 阅读人ID */
  readerId: number
  /** 阅读人名称 */
  readerName: string
  /** 阅读时间 */
  readTime?: string
}

/**
 * 通知转发参数
 */
export interface TaktNoticeForward {
  /** 原通知ID */
  originalNoticeId: number
  /** 转发标题 */
  forwardTitle: string
  /** 转发内容 */
  forwardContent: string
  /** 接收人ID列表 */
  receiverIds: number[]
  /** 接收人名称列表 */
  receiverNames: string[]
  /** 转发人ID */
  forwarderId: number
  /** 转发人名称 */
  forwarderName: string
  /** 备注 */
  remark?: string
}

/**
 * 通知回复参数
 */
export interface TaktNoticeReply {
  /** 原通知ID */
  originalNoticeId: number
  /** 回复内容 */
  replyContent: string
  /** 回复人ID */
  replierId: number
  /** 回复人名称 */
  replierName: string
  /** 回复时间 */
  replyTime?: string
}

/**
 * 通知导入参数
 */
export interface TaktNoticeImport {
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 接收人名称 */
  receiverName: string
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency: number
  /** 是否置顶（0否 1是） */
  isTop: number
  /** 是否重要（0否 1是） */
  isImportant: number
  /** 过期时间 */
  expireTime?: string
  /** 备注 */
  remark?: string
}

/**
 * 通知导出参数
 */
export interface TaktNoticeExport {
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型 */
  noticeType: string
  /** 通知状态 */
  noticeStatus: string
  /** 发送人名称 */
  senderName: string
  /** 接收人名称 */
  receiverName: string
  /** 优先级 */
  priority: string
  /** 紧急程度 */
  urgency: string
  /** 是否置顶 */
  isTop: string
  /** 是否重要 */
  isImportant: string
  /** 发送时间 */
  sendTime: string
  /** 阅读时间 */
  readTime: string
  /** 阅读次数 */
  readCount: number
  /** 创建时间 */
  createTime: string
}

/**
 * 通知模板参数
 */
export interface TaktNoticeTemplate {
  /** 模板ID */
  templateId: number
  /** 模板名称 */
  templateName: string
  /** 模板标题 */
  templateTitle: string
  /** 模板内容 */
  templateContent: string
  /** 模板类型（1系统通知 2业务通知） */
  templateType: number
  /** 模板状态（0禁用 1启用） */
  templateStatus: number
  /** 模板描述 */
  templateDescription?: string
  /** 创建者 */
  createBy?: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
}

/**
 * 通知统计信息
 */
export interface TaktNoticeStatistics {
  /** 总通知数 */
  totalNotices: number
  /** 未读通知数 */
  unreadNotices: number
  /** 已读通知数 */
  readNotices: number
  /** 今日发送通知数 */
  todaySentNotices: number
  /** 本月发送通知数 */
  monthSentNotices: number
  /** 通知类型分布 */
  typeDistribution: {
    /** 系统通知数 */
    system: number
    /** 业务通知数 */
    business: number
  }
  /** 通知状态分布 */
  statusDistribution: {
    /** 未读通知数 */
    unread: number
    /** 已读通知数 */
    read: number
  }
  /** 优先级分布 */
  priorityDistribution: {
    /** 低优先级数 */
    low: number
    /** 普通优先级数 */
    normal: number
    /** 高优先级数 */
    high: number
    /** 紧急优先级数 */
    urgent: number
    /** 特急优先级数 */
    critical: number
  }
}

/**
 * 通知分页结果
 */
export type TaktNoticePagedResult = TaktPagedResult<TaktNotice>

/**
 * 通知DTO
 */
export interface TaktNoticeDto {
  /** 通知ID */
  noticeId: number
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 通知状态（0未读 1已读） */
  noticeStatus: number
  /** 发送时间 */
  noticeSendTime?: string
  /** 阅读时间 */
  noticeReadTime?: string
  /** 接收人ID */
  noticeReceiverId: number
  /** 接收人名称 */
  noticeReceiverName: string
  /** 发送人ID */
  noticeSenderId: number
  /** 发送人名称 */
  noticeSenderName: string
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency: number
  /** 是否置顶（0否 1是） */
  isTop: number
  /** 是否重要（0否 1是） */
  isImportant: number
  /** 附件列表 */
  attachments?: string
  /** 相关链接 */
  relatedLinks?: string
  /** 过期时间 */
  expireTime?: string
  /** 阅读次数 */
  readCount: number
  /** 转发次数 */
  forwardCount: number
  /** 回复次数 */
  replyCount: number
  /** 备注 */
  remark?: string
  /** 创建者 */
  createBy?: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
  /** 是否删除（0未删除 1已删除） */
  isDeleted: number
  /** 删除者 */
  deleteBy?: string
  /** 删除时间 */
  deleteTime?: string
}

/**
 * 通知表单类型
 */
export interface TaktNoticeForm {
  /** 通知标题 */
  noticeTitle: string
  /** 通知内容 */
  noticeContent: string
  /** 通知类型（1系统通知 2业务通知） */
  noticeType: number
  /** 接收人ID列表 */
  receiverIds: number[]
  /** 接收人名称列表 */
  receiverNames: string[]
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency: number
  /** 是否置顶（0否 1是） */
  isTop: number
  /** 是否重要（0否 1是） */
  isImportant: number
  /** 附件列表 */
  attachments?: string
  /** 相关链接 */
  relatedLinks?: string
  /** 过期时间 */
  expireTime?: string
  /** 备注 */
  remark?: string
} 
