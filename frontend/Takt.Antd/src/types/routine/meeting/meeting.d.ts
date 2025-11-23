/** 会议相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 会议实体
 */
export interface TaktMeeting extends TaktBaseEntity {
  /** 会议ID */
  meetingId: number
  /** 会议标题 */
  meetingTitle: string
  /** 会议内容 */
  meetingContent: string
  /** 会议类型（0普通会议 1重要会议 2紧急会议） */
  meetingType: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
  /** 是否全天会议 */
  isAllDay: boolean
  /** 提前提醒分钟数 */
  remindMinutes?: number
  /** 会议地点 */
  location?: string
  /** 参会人员ID列表 */
  participantIds: number[]
  /** 参会人员名称列表 */
  participantNames: string[]
  /** 主持人ID */
  hostId: number
  /** 主持人名称 */
  hostName: string
  /** 会议纪要 */
  minutes?: string
  /** 是否需要签到 */
  needSignIn: boolean
  /** 开始时间 */
  startTime: string
  /** 结束时间 */
  endTime: string
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
  /** 会议链接 */
  meetingLink?: string
  /** 会议密码 */
  meetingPassword?: string
  /** 签到人数 */
  signInCount: number
  /** 实际参会人数 */
  actualParticipantCount: number
  /** 备注 */
  remark?: string
}

/**
 * 会议查询参数
 */
export interface TaktMeetingQuery extends TaktPagedQuery {
  /** 会议标题 */
  meetingTitle?: string
  /** 会议类型（0普通会议 1重要会议 2紧急会议） */
  meetingType?: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus?: number
  /** 会议地点 */
  location?: string
  /** 主持人ID */
  hostId?: number
  /** 主持人名称 */
  hostName?: string
  /** 参会人员ID */
  participantId?: number
  /** 参会人员名称 */
  participantName?: string
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority?: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency?: number
  /** 是否置顶（0否 1是） */
  isTop?: number
  /** 是否重要（0否 1是） */
  isImportant?: number
  /** 是否需要签到 */
  needSignIn?: boolean
  /** 开始时间范围 */
  startTimeRange?: [string, string]
  /** 结束时间范围 */
  endTimeRange?: [string, string]
  /** 创建时间范围 */
  createTimeRange?: [string, string]
}

/**
 * 会议创建参数
 */
export interface TaktMeetingCreate {
  /** 会议标题 */
  meetingTitle: string
  /** 会议内容 */
  meetingContent: string
  /** 会议类型（0普通会议 1重要会议 2紧急会议） */
  meetingType: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
  /** 是否全天会议 */
  isAllDay: boolean
  /** 提前提醒分钟数 */
  remindMinutes?: number
  /** 会议地点 */
  location?: string
  /** 参会人员ID列表 */
  participantIds: number[]
  /** 参会人员名称列表 */
  participantNames: string[]
  /** 主持人ID */
  hostId: number
  /** 主持人名称 */
  hostName: string
  /** 会议纪要 */
  minutes?: string
  /** 是否需要签到 */
  needSignIn: boolean
  /** 开始时间 */
  startTime: string
  /** 结束时间 */
  endTime: string
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
  /** 会议链接 */
  meetingLink?: string
  /** 会议密码 */
  meetingPassword?: string
  /** 备注 */
  remark?: string
}

/**
 * 会议更新参数
 */
export interface TaktMeetingUpdate extends TaktMeetingCreate {
  /** 会议ID */
  meetingId: number
}

/**
 * 会议状态更新参数
 */
export interface TaktMeetingStatus {
  /** 会议ID */
  meetingId: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
}

/**
 * 会议批量状态更新参数
 */
export interface TaktMeetingBatchStatus {
  /** 会议ID列表 */
  meetingIds: number[]
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
}

/**
 * 会议签到参数
 */
export interface TaktMeetingSignIn {
  /** 会议ID */
  meetingId: number
  /** 签到人ID */
  signInUserId: number
  /** 签到人名称 */
  signInUserName: string
  /** 签到时间 */
  signInTime?: string
  /** 签到地点 */
  signInLocation?: string
  /** 签到备注 */
  signInRemark?: string
}

/**
 * 会议签到记录
 */
export interface TaktMeetingSignInRecord {
  /** 签到记录ID */
  signInRecordId: number
  /** 会议ID */
  meetingId: number
  /** 签到人ID */
  signInUserId: number
  /** 签到人名称 */
  signInUserName: string
  /** 签到时间 */
  signInTime: string
  /** 签到地点 */
  signInLocation?: string
  /** 签到备注 */
  signInRemark?: string
  /** 创建时间 */
  createTime: string
}

/**
 * 会议参与人员
 */
export interface TaktMeetingParticipant {
  /** 参与记录ID */
  participantRecordId: number
  /** 会议ID */
  meetingId: number
  /** 参与人ID */
  participantId: number
  /** 参与人名称 */
  participantName: string
  /** 参与状态（0未确认 1已确认 2已拒绝 3已参加 4未参加） */
  participantStatus: number
  /** 确认时间 */
  confirmTime?: string
  /** 参与时间 */
  joinTime?: string
  /** 离开时间 */
  leaveTime?: string
  /** 参与备注 */
  participantRemark?: string
  /** 创建时间 */
  createTime: string
}

/**
 * 会议模板参数
 */
export interface TaktMeetingTemplate {
  /** 模板ID */
  templateId: number
  /** 模板名称 */
  templateName: string
  /** 模板标题 */
  templateTitle: string
  /** 模板内容 */
  templateContent: string
  /** 模板类型（0普通会议 1重要会议 2紧急会议） */
  templateType: number
  /** 模板状态（0禁用 1启用） */
  templateStatus: number
  /** 模板描述 */
  templateDescription?: string
  /** 默认会议地点 */
  defaultLocation?: string
  /** 默认提前提醒分钟数 */
  defaultRemindMinutes?: number
  /** 默认是否需要签到 */
  defaultNeedSignIn: boolean
  /** 默认优先级 */
  defaultPriority: number
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
 * 会议导入参数
 */
export interface TaktMeetingImport {
  /** 会议标题 */
  meetingTitle: string
  /** 会议内容 */
  meetingContent: string
  /** 会议类型（0普通会议 1重要会议 2紧急会议） */
  meetingType: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
  /** 是否全天会议 */
  isAllDay: boolean
  /** 提前提醒分钟数 */
  remindMinutes: number
  /** 会议地点 */
  location: string
  /** 参会人员名称 */
  participantNames: string
  /** 主持人名称 */
  hostName: string
  /** 会议纪要 */
  minutes: string
  /** 是否需要签到 */
  needSignIn: boolean
  /** 开始时间 */
  startTime: string
  /** 结束时间 */
  endTime: string
  /** 优先级（1低 2普通 3高 4紧急 5特急） */
  priority: number
  /** 紧急程度（1普通 2加急 3特急） */
  urgency: number
  /** 是否置顶（0否 1是） */
  isTop: number
  /** 是否重要（0否 1是） */
  isImportant: number
  /** 备注 */
  remark?: string
}

/**
 * 会议导出参数
 */
export interface TaktMeetingExport {
  /** 会议标题 */
  meetingTitle: string
  /** 会议内容 */
  meetingContent: string
  /** 会议类型 */
  meetingType: string
  /** 会议状态 */
  meetingStatus: string
  /** 是否全天会议 */
  isAllDay: string
  /** 提前提醒分钟数 */
  remindMinutes: number
  /** 会议地点 */
  location: string
  /** 参会人员 */
  participantNames: string
  /** 主持人 */
  hostName: string
  /** 会议纪要 */
  minutes: string
  /** 是否需要签到 */
  needSignIn: string
  /** 开始时间 */
  startTime: string
  /** 结束时间 */
  endTime: string
  /** 优先级 */
  priority: string
  /** 紧急程度 */
  urgency: string
  /** 是否置顶 */
  isTop: string
  /** 是否重要 */
  isImportant: string
  /** 签到人数 */
  signInCount: number
  /** 实际参会人数 */
  actualParticipantCount: number
  /** 创建时间 */
  createTime: string
}

/**
 * 会议统计信息
 */
export interface TaktMeetingStatistics {
  /** 总会议数 */
  totalMeetings: number
  /** 未开始会议数 */
  notStartedMeetings: number
  /** 进行中会议数 */
  ongoingMeetings: number
  /** 已结束会议数 */
  endedMeetings: number
  /** 已取消会议数 */
  cancelledMeetings: number
  /** 今日会议数 */
  todayMeetings: number
  /** 本周会议数 */
  weekMeetings: number
  /** 本月会议数 */
  monthMeetings: number
  /** 会议类型分布 */
  typeDistribution: {
    /** 普通会议数 */
    normal: number
    /** 重要会议数 */
    important: number
    /** 紧急会议数 */
    urgent: number
  }
  /** 会议状态分布 */
  statusDistribution: {
    /** 未开始会议数 */
    notStarted: number
    /** 进行中会议数 */
    ongoing: number
    /** 已结束会议数 */
    ended: number
    /** 已取消会议数 */
    cancelled: number
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
 * 会议分页结果
 */
export type TaktMeetingPagedResult = TaktPagedResult<TaktMeeting>

/**
 * 会议DTO
 */
export interface TaktMeetingDto {
  /** 会议ID */
  meetingId: number
  /** 会议标题 */
  meetingTitle: string
  /** 会议内容 */
  meetingContent: string
  /** 会议类型（0普通会议 1重要会议 2紧急会议） */
  meetingType: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
  /** 是否全天会议 */
  isAllDay: boolean
  /** 提前提醒分钟数 */
  remindMinutes?: number
  /** 会议地点 */
  location?: string
  /** 参会人员ID列表 */
  participantIds: number[]
  /** 参会人员名称列表 */
  participantNames: string[]
  /** 主持人ID */
  hostId: number
  /** 主持人名称 */
  hostName: string
  /** 会议纪要 */
  minutes?: string
  /** 是否需要签到 */
  needSignIn: boolean
  /** 开始时间 */
  startTime: string
  /** 结束时间 */
  endTime: string
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
  /** 会议链接 */
  meetingLink?: string
  /** 会议密码 */
  meetingPassword?: string
  /** 签到人数 */
  signInCount: number
  /** 实际参会人数 */
  actualParticipantCount: number
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
 * 会议表单类型
 */
export interface TaktMeetingForm {
  /** 会议标题 */
  meetingTitle: string
  /** 会议内容 */
  meetingContent: string
  /** 会议类型（0普通会议 1重要会议 2紧急会议） */
  meetingType: number
  /** 会议状态（0未开始 1进行中 2已结束 3已取消） */
  meetingStatus: number
  /** 是否全天会议 */
  isAllDay: boolean
  /** 提前提醒分钟数 */
  remindMinutes?: number
  /** 会议地点 */
  location?: string
  /** 参会人员ID列表 */
  participantIds: number[]
  /** 参会人员名称列表 */
  participantNames: string[]
  /** 主持人ID */
  hostId: number
  /** 主持人名称 */
  hostName: string
  /** 会议纪要 */
  minutes?: string
  /** 是否需要签到 */
  needSignIn: boolean
  /** 开始时间 */
  startTime: string
  /** 结束时间 */
  endTime: string
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
  /** 会议链接 */
  meetingLink?: string
  /** 会议密码 */
  meetingPassword?: string
  /** 备注 */
  remark?: string
} 
