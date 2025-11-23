//===================================================================
// 项目名 : Lean.Takt
// 文件名 : genTableDefine.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述   : 代码生成表定义类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'
import type { TaktGenColumnDefine } from './genColumnDefine'

/**
 * 代码生成表定义
 */
export interface TaktGenTableDefine extends TaktBaseEntity {
  /** 表ID */
  genTableDefineId: string
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: TaktGenColumnDefine[]
}

/**
 * 代码生成表定义查询对象
 */
export interface TaktGenTableDefineQuery extends TaktPagedQuery {
  /** 表名 */
  tableName?: string
  /** 表描述 */
  tableComment?: string
  /** 日期范围 */
  dateRange?: [string, string]
}

/**
 * 代码生成表定义分页结果
 */
export interface TaktGenTableDefinePageResult extends TaktPagedResult<TaktGenTableDefine> {}

/**
 * 代码生成表定义创建对象
 */
export interface TaktGenTableDefineCreate {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表前缀 */
  tablePrefix: string
  /** 表名第一部分（模块名称） */
  tableNameFirst: string
  /** 表名第二部分 */
  tableNameSecond: string
  /** 表名第三部分 */
  tableNameThird: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: TaktGenColumnDefine[]
}

/**
 * 代码生成表定义更新对象
 */
export interface TaktGenTableDefineUpdate extends TaktGenTableDefineCreate {
  /** 表ID */
  genTableDefineId: string
}

/**
 * 代码生成表定义导入对象
 */
export interface TaktGenTableDefineImport {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: TaktGenColumnDefine[]
}

/**
 * 代码生成表定义导出对象
 */
export interface TaktGenTableDefineExport {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: TaktGenColumnDefine[]
}

/**
 * 代码生成表定义模板对象
 */
export interface TaktGenTableDefineTemplate {
  /** 数据库类型（0：SqlServer，1：MySQL，2：PostgreSQL，3：Oracle，4：SQLite） */
  dbType: number
  /** 连接字符串 */
  connectionString: string
  /** 数据库名称 */
  databaseName: string
  /** 表名 */
  tableName: string
  /** 表描述 */
  tableComment: string
  /** 作者 */
  author: string
  /** 字段定义列表 */
  columns: TaktGenColumnDefine[]
} 
