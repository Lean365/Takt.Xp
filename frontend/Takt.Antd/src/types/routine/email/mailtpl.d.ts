/** 邮件模板相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 邮件模板查询参数 */
export interface TaktMailTplQuery extends TaktPagedQuery {
  /** 模板名称 */
  mailTplName?: string
  /** 模板编码 */
  mailTplCode?: string
  /** 状态（0停用 1启用） */
  status?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 邮件模板数据传输对象 */
export interface TaktMailTpl extends TaktBaseEntity {
  /** ID */
  mailTplId: number
  /** 模板名称 */
  mailTplName: string
  /** 模板编码 */
  mailTplCode: string
  /** 主题 */
  mailTplSubject: string
  /** 内容 */
  mailTplBody: string
  /** 是否HTML */
  mailTplIsHtml: boolean
  /** 参数列表 */
  mailTplParameters?: string
  /** 状态（0停用1启用） */
  status: number
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
  /** 是否删除（0删除） */
  isDeleted: number
  /** 删除者 */
  deleteBy?: string
  /** 删除时间 */
  deleteTime?: Date
}

/** 邮件模板创建参数 */
export interface TaktMailTplCreate {
  /** 模板名称 */
  mailTplName: string
  /** 模板编码 */
  mailTplCode: string
  /** 主题 */
  mailTplSubject: string
  /** 内容 */
  mailTplBody: string
  /** 是否HTML */
  mailTplIsHtml: boolean
  /** 参数列表 */
  mailTplParameters?: string
  /** 状态（0停用1启用） */
  status: number
  /** 备注 */
  remark?: string
}

/** 邮件模板更新参数 */
export interface TaktMailTplUpdate extends TaktMailTplCreate {
  /** ID */
  mailTplId: number
}

/** 邮件模板删除参数 */
export interface TaktMailTplDelete {
  /** 主键ID */
  mailTplId: number
}

/** 邮件模板批量删除参数 */
export interface TaktMailTplBatchDelete {
  /** 主键ID列表 */
  mailTplIds: number[]
}

/** 邮件模板导入参数 */
export interface TaktMailTplImport {
  /** 模板名称 */
  mailTplName: string
  /** 模板编码 */
  mailTplCode: string
  /** 主题 */
  mailTplSubject: string
  /** 内容 */
  mailTplBody: string
  /** 是否HTML */
  mailTplIsHtml: boolean
  /** 参数列表 */
  mailTplParameters?: string
  /** 状态（0停用1启用） */
  status: number
  /** 备注 */
  remark?: string
}

/** 邮件模板导出参数 */
export interface TaktMailTplExport {
  /** 模板名称 */
  mailTplName: string
  /** 模板编码 */
  mailTplCode: string
  /** 主题 */
  mailTplSubject: string
  /** 内容 */
  mailTplBody: string
  /** 是否HTML */
  mailTplIsHtml: boolean
  /** 参数列表 */
  mailTplParameters?: string
  /** 状态（0停用1启用） */
  status: number
  /** 创建时间 */
  createTime: Date
  /** 备注 */
  remark?: string
}

/** 邮件模板导入模板参数 */
export interface TaktMailTplTemplate {
  /** 模板名称 */
  mailTplName: string
  /** 模板编码 */
  mailTplCode: string
  /** 主题 */
  mailTplSubject: string
  /** 内容 */
  mailTplBody: string
  /** 是否HTML */
  mailTplIsHtml: boolean
  /** 参数列表 */
  mailTplParameters?: string
  /** 状态（0停用1启用） */
  status: number
  /** 备注 */
  remark?: string
}

/** 邮件模板分页结果 */
export type TaktMailTplPagedResult = TaktPagedResult<TaktMailTpl>
