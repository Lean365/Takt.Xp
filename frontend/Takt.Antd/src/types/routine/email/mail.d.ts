/** 邮件相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 邮件实体
 */
export interface TaktMail extends TaktBaseEntity {
  /** 邮件ID */
  mailId: number
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: boolean
  /** 抄送 */
  mailCc?: string
  /** 附件 */
  mailAttachments?: string
  /** 发送状态（0失败 1成功） */
  mailStatus: number
  /** 发送时间 */
  mailSendTime?: string
  /** 备注 */
  remark?: string
}

/**
 * 邮件查询参数
 */
export interface TaktMailQuery extends TaktPagedQuery {
  /** 收件人 */
  mailTo?: string
  /** 主题 */
  mailSubject?: string
  /** 发送状态（0失败 1成功） */
  mailStatus?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 邮件创建参数
 */
export interface TaktMailCreate {
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: number
  /** 抄送 */
  mailCc?: string
  /** 附件 */
  mailAttachments?: string
  /** 发送状态（0失败 1成功） */
  mailStatus: number
  /** 备注 */
  remark?: string
}

/**
 * 邮件更新参数
 */
export interface TaktMailUpdate extends TaktMailCreate {
  /** 邮件ID */
  mailId: number
}

/**
 * 邮件状态更新参数
 */
export interface TaktMailStatus {
  /** 邮件ID */
  mailId: number
  /** 发送状态（0失败 1成功） */
  mailStatus: number
}

/**
 * 邮件发送参数
 */
export interface TaktMailSend {
  /** 邮件ID */
  mailId?: number
  /** 发件人 */
  mailFrom?: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml?: boolean
  /** 抄送 */
  mailCc?: string
  /** 附件 */
  mailAttachments?: string
}

/**
 * 邮件导入参数
 */
export interface TaktMailImport {
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: number
  /** 抄送 */
  mailCc: string
  /** 附件 */
  mailAttachments: string
  /** 发送状态（0失败 1成功） */
  mailStatus: number
  /** 备注 */
  remark?: string
}

/**
 * 邮件导出参数
 */
export interface TaktMailExport {
  /** 发件人 */
  mailFrom: string
  /** 收件人 */
  mailTo: string
  /** 主题 */
  mailSubject: string
  /** 内容 */
  mailBody: string
  /** 是否HTML */
  mailIsHtml: number
  /** 抄送 */
  mailCc: string
  /** 附件 */
  mailAttachments: string
  /** 发送状态 */
  mailStatus: number
  /** 发送时间 */
  mailSendTime: string
  /** 创建时间 */
  createTime: string
}

/**
 * 邮件分页结果
 */
export interface TaktMailPagedResult extends TaktPagedResult<TaktMail> {} 
