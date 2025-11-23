//===================================================================
// 项目名 : Lean.Takt
// 文件名 : column.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成列定义类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 代码生成列定义实体
 */
export interface TaktGenColumn extends TaktBaseEntity {
  /** 表ID */
  tableId: string
  /** 列ID */
  genColumnId: string
  /** 列名 */
  columnName: string
  /** 列注释 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** C#类型 */
  csharpType: string
  /** C#列名（首字母大写） */
  csharpColumn: string
  /** C#长度（字符串长度、数值类型的整数位数） */
  csharpLength: number
  /** C#小数位数（decimal等数值类型的小数位数） */
  csharpDecimalDigits: number
  /** C#字段名（首字母小写） */
  csharpField: string
  /** 是否自增（1是） */
  isIncrement: number
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否为新增字段（1是） */
  isInsert: number
  /** 是否编辑字段（1是） */
  isEdit: number
  /** 是否列表字段（1是） */
  isList: number
  /** 是否查询字段（1是） */
  isQuery: number
  /** 查询方式（等于、不等于、大于、小于、范围） */
  queryType: string
  /** 是否排序字段（1是） */
  isSort: number
  /** 是否导出字段（1是） */
  isExport: number
  /** 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件） */
  displayType: string
  /** 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段） */
  dictType: string
  /** 排序 */
  orderNum: number
}

/**
 * 代码生成列定义查询参数
 */
export interface TaktGenColumnQuery extends TaktPagedQuery {
  /** 表ID */
  tableId?: number
  /** 列名 */
  columnName?: string
  /** 列注释 */
  columnComment?: string
}

/**
 * 代码生成列定义创建参数
 */
export interface TaktGenColumnCreate {
  /** 表ID */
  tableId: string
  /** 列名 */
  columnName: string
  /** 列注释 */
  columnComment: string
  /** 数据库列类型 */
  dbColumnType: string
  /** C#类型 */
  csharpType: string
  /** C#列名（首字母大写） */
  csharpColumn: string
  /** C#长度（字符串长度、数值类型的整数位数） */
  csharpLength: number
  /** C#小数位数（decimal等数值类型的小数位数） */
  csharpDecimalDigits: number
  /** C#字段名（首字母小写） */
  csharpField: string
  /** 是否自增（1是） */
  isIncrement: number
  /** 是否主键（1是） */
  isPrimaryKey: number
  /** 是否必填（1是） */
  isRequired: number
  /** 是否为新增字段（1是） */
  isInsert: number
  /** 是否编辑字段（1是） */
  isEdit: number
  /** 是否列表字段（1是） */
  isList: number
  /** 是否查询字段（1是） */
  isQuery: number
  /** 查询方式（等于、不等于、大于、小于、范围） */
  queryType: string
  /** 是否排序字段（1是） */
  isSort: number
  /** 是否导出字段（1是） */
  isExport: number
  /** 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件） */
  displayType: string
  /** 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段） */
  dictType: string
  /** 排序 */
  orderNum: number

}

/**
 * 代码生成列定义更新参数
 */
export interface TaktGenColumnUpdate extends TaktGenColumnCreate {
  /** 列ID */
  genColumnId: string
}

/**
 * 代码生成列定义导入参数
 */
export interface TaktGenColumnImport extends TaktGenColumnCreate {}

/**
 * 代码生成列定义导出参数
 */
export interface TaktGenColumnExport extends TaktGenColumnCreate {}

/**
 * 代码生成列定义分页结果
 */
export type TaktGenColumnPageResult = TaktPagedResult<TaktGenColumn>

/**
 * 数据库表列信息
 */
export interface TaktGenTableColumnInfo {
  /** 表ID */
  tableId: string
  /** 列ID */
  genColumnId: string
  /** 列名 */
  dbColumnName: string
  /** 数据类型 */
  dataType: string
  /** 长度 */
  length: number
  /** 小数位数 */
  decimalDigits: number
  /** 精度 */
  scale: number
  /** 是否可空 */
  isNullable: boolean
  /** 是否自增 */
  isIdentity: boolean
  /** 是否主键 */
  isPrimarykey: boolean
  /** 默认值 */
  defaultValue: string
  /** 列描述 */
  columnDescription: string
  /** Oracle类型 */
  oracleDataType: string
  /** 属性名 */
  propertyName: string
  /** 属性类型 */
  propertyType: string
  /** 值 */
  value?: any
  /** 是否数组 */
  isArray: boolean
  /** 是否Json */
  isJson: boolean
  /** 建表字段排序 */
  createTableFieldSort: number
  /** 插入SQL */
  insertSql: string
  /** 更新SQL */
  updateSql: string
} 
