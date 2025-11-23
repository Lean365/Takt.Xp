//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktLanguage.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言相关类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 语言实体
 */
export interface TaktLanguage extends TaktBaseEntity {
  /** 语言ID */
  languageId: string
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 语言图标 */
  langIcon?: string
  /** 排序号 */
  orderNum: number
  /** 系统内置(0=否,1=是) */
  isBuiltin: number
  /** 是否默认 */
  isDefault: number
  /** 状态(0=正常/启用,1=停用/禁用) */
  i18nStatus: number
}

/**
 * 语言查询参数
 */
export interface TaktLanguageQuery extends TaktPagedQuery {
  /** 语言代码 */
  langCode?: string
  /** 语言名称 */
  langName?: string
  /** 系统内置(0=否,1=是) */
  isBuiltin?: number
  /** 是否默认 */
  isDefault?: number
  /** 状态 */
  i18nStatus?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 创建语言参数
 */
export interface TaktLanguageCreate {
  /** 语言代码 */
  langCode: string
  /** 语言名称 */
  langName: string
  /** 语言图标 */
  langIcon?: string
  /** 排序号 */
  orderNum: number
  /** 系统内置(0=否,1=是) */
  isBuiltin: number
  /** 是否默认 */
  isDefault: number
  /** 状态(0=禁用,1=启用) */
  i18nStatus: number
  /** 备注 */
  remark?: string
}

/**
 * 更新语言参数
 */
export interface TaktLanguageUpdate extends TaktLanguageCreate {
  /** 语言ID */
  languageId: string
}

/**
 * 语言状态更新参数
 */
export interface TaktLanguageStatus {
  /** 语言ID */
  languageId: string
  /** 状态(0=禁用,1=启用) */
  i18nStatus: number
}

/**
 * 语言分页结果
 */
export type TaktLanguagePageResult = TaktPagedResult<TaktLanguage> 
