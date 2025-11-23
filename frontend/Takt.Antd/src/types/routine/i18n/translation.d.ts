//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktTranslation.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译相关类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 翻译对象
 */
export interface TaktTranslation extends TaktBaseEntity {
  /** 翻译ID */
  translationId: string
  /** 语言代码 */
  langCode: string
  /** 翻译键 */
  transKey: string
  /** 翻译值 */
  transValue: string
  /** 翻译类别（0是前端，1是后端） */
  transType: number
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  /** 备注 */
  remark?: string
}

/**
 * 翻译查询参数
 */
export interface TaktTranslationQuery extends TaktPagedQuery {
  /** 语言代码 */
  langCode?: string
  /** 翻译键 */
  transKey?: string
  /** 翻译值 */
  transValue?: string
  /** 翻译类别（0是前端，1是后端） */
  transType?: number
}

/**
 * 翻译创建参数
 */
export interface TaktTranslationCreate {
  /** 语言代码 */
  langCode: string
  /** 翻译键 */
  transKey: string
  /** 翻译值 */
  transValue: string
  /** 翻译类别（0是前端，1是后端） */
  transType: number
  /** 排序号 */
  orderNum: number
  /** 状态（0正常 1停用） */
  /** 备注 */
  remark?: string
}

/**
 * 翻译更新参数
 */
export interface TaktTranslationUpdate extends TaktTranslationCreate {
  /** 翻译ID */
  translationId: string
}


/**
 * 翻译语言信息
 */
export interface TaktTranslationLang {
  /** 翻译ID */
  translationId: string
  /** 语言代码 */
  langCode: string
  /** 翻译值 */
  transValue: string
  /** 状态（0正常 1停用） */
}

/**
 * 转置后的翻译数据
 */
export interface TaktTransposedData {
  /** 翻译键 */
  transKey: string
  /** 翻译类别（0是前端，1是后端） */
  transType: number
  /** 状态（0正常 1停用） */
  /** 备注 */
  remark: string
  /** 创建者 */
  createBy: string
  /** 创建时间 */
  createTime: string
  /** 更新者 */
  updateBy: string
  /** 更新时间 */
  updateTime: string
  /** 各语言翻译信息 */
  translations: Record<string, TaktTranslationLang>
}

/**
 * 翻译分页结果
 */
export type TaktTranslationPageResult = TaktPagedResult<TaktTranslation>

/**
 * 转置查询参数
 */
export interface TaktTransposedQueryDto extends TaktPagedQuery {
  /** 翻译键 */
  transKey?: string
  /** 翻译类别（0是前端，1是后端） */
  transType?: number
} 
