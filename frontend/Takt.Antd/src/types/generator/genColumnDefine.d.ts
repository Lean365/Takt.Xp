//===================================================================
// 项目名 : Lean.Takt
// 文件名 : genColumnDefine.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述   : 代码生成字段定义类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery } from '@/types/common'

/**
 * 代码生成字段定义
 */
export interface TaktGenColumnDefine extends TaktBaseEntity {
  /** 字段ID */
  genColumnDefineId: string
  /** 表ID */
  tableId: string
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
  /** 是否处于编辑状态 */
  isEditing?: boolean
}

/**
 * 代码生成字段定义查询对象
 */
export interface TaktGenColumnDefineQuery extends TaktPagedQuery {
  /** 表ID */
  tableId?: string
  /** 字段名 */
  columnName?: string
  /** 字段说明 */
  columnComment?: string
}

/**
 * 代码生成字段定义分页结果
 */
export interface TaktGenColumnDefinePageResult {
  /** 总记录数 */
  total: number
  /** 列表数据 */
  rows: TaktGenColumnDefine[]
}

/**
 * 代码生成字段定义创建对象
 */
export interface TaktGenColumnDefineCreate {
  /** 表ID */
  tableId: string
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义更新对象
 */
export interface TaktGenColumnDefineUpdate extends TaktGenColumnDefineCreate {
  /** 字段ID */
  genColumnDefineId: string
}

/**
 * 代码生成字段定义导入对象
 */
export interface TaktGenColumnDefineImport {
  /** 表ID */
  tableId: string
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义导出对象
 */
export interface TaktGenColumnDefineExport {
  /** 表ID */
  tableId: string
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成字段定义模板对象
 */
export interface TaktGenColumnDefineTemplate {
  /** 表ID */
  tableId: string
  /** 字段名 */
  columnName: string
  /** 字段说明 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否自增（1是） */
  isIncrement: number
  /** 列长度 */
  columnLength: number
  /** 小数位数 */
  decimalDigits: number
  /** 默认值 */
  defaultValue?: string
  /** 排序 */
  orderNum: number
} 
