//===================================================================
// 项目名 : Lean.Takt
// 文件名 : dictData.d.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 字典数据实体
 */
export interface TaktDictData extends TaktBaseEntity {
  /** 字典数据ID */
  dictDataId: string
  /** 字典类型 */
  dictType: string
  /** 字典标签 */
  dictLabel: string
  /** 字典键值 */
  dictValue: string
  /** 排序号 */
  orderNum: number
  /** 样式属性 */
  cssClass?: string
  /** 表格回显样式 */
  listClass?: string
  /** 是否默认（0否 1是） */
  isDefault: number
  /** 状态（0正常 1停用） */
  /** 扩展标签 */
  extLabel?: string
  /** 扩展值 */
  extValue?: string
  /** 翻译键 */
  transKey?: string
}

/**
 * 字典数据查询参数
 */
export interface TaktDictDataQuery extends TaktPagedQuery {
  /** 字典类型 */
  dictType: string
  /** 字典标签 */
  dictLabel?: string
  /** 字典键值 */
  dictValue?: string
  /** 状态（0正常 1停用） */
}

/**
 * 字典数据创建参数
 */
export interface TaktDictDataCreate {
    /** 字典类型 */
    dictType: string
    /** 字典标签 */
    dictLabel: string
    /** 字典键值 */
    dictValue: string
    /** 扩展标签 */
    extLabel?: string
    /** 扩展值 */
    extValue?: string
    /** 翻译键 */
    transKey?: string
    /** 排序号 */
    orderNum: number
    /** 样式属性 */
    cssClass?: string
    /** 表格回显样式 */
    listClass?: string
    /** 状态（0正常 1停用） */
    /** 备注 */
    remark?: string
}

/**
 * 字典数据更新参数
 */
export interface TaktDictDataUpdate extends TaktDictDataCreate {
  /** 字典数据ID */
  dictDataId: string
}

/**
 * 字典数据状态更新参数
 */
export interface TaktDictDataStatus {
  /** 字典数据ID */
  dictDataId: string
  /** 状态（0正常 1停用） */
}

/**
 * 字典数据选项
 */
export interface DictOption {
  /** 标签 */
  label: string
  /** 值 */
  value: number
  /** CSS类名 */
  cssClass?: number
  /** 列表类名 */
  listClass?: number
  /** 扩展标签 */
  extLabel?: string
  /** 扩展值 */
  extValue?: string
  /** 翻译键 */
  transKey?: string
}

/**
 * 字典数据分页结果
 */
export type TaktDictDataPageResult = TaktPagedResult<TaktDictData> 
