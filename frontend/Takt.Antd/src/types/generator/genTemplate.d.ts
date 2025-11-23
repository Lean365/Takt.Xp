//===================================================================
// 项目名 : Lean.Takt
// 文件名 : genTemplate.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成模板类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 代码生成模板对象
 */
export interface TaktGenTemplate extends TaktBaseEntity {
  /** 模板ID */
  genTemplateId: string
  /** 模板名称 */
  templateName: string
  /** ORM框架类型（1：Entity Framework Core，2：Dapper，3：SqlSugar，4：MyBatis，5：Hibernate，6：SQLAlchemy，7：TypeORM，8：Prisma，9：Eloquent，10：其他） */
  templateOrmType: number
  /** 生成模板分类（1：后端代码，2：前端代码，3：SQL代码） */
  templateCodeType: number
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: number
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: number
  /** 版本号 */
  templateVersion: number
  /** 模板内容 */
  templateContent: string
  /** 文件名 */
  fileName: string
  /** 状态（0：停用，1：正常） */
  status: number

}

/**
 * 代码生成模板查询参数
 */
export interface TaktGenTemplateQuery extends TaktPagedQuery {
  /** 模板名称 */
  templateName?: string
  /** ORM框架类型 */
  templateOrmType?: number
  /** 生成模板分类 */
  templateCodeType?: number
  /** 模板分类 */
  templateCategory?: number
  /** 编程语言 */
  templateLanguage?: number
  /** 文件名 */
  fileName?: string
  /** 状态 */
  status?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 创建代码生成模板参数
 */
export interface TaktGenTemplateCreate {
  /** 模板名称 */
  templateName: string
  /** ORM框架类型（1：Entity Framework Core，2：Dapper，3：SqlSugar，4：MyBatis，5：Hibernate，6：SQLAlchemy，7：TypeORM，8：Prisma，9：Eloquent，10：其他） */
  templateOrmType: number
  /** 生成模板分类（1：后端代码，2：前端代码，3：SQL代码） */
  templateCodeType: number
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: number
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: number
  /** 版本号 */
  templateVersion: number
  /** 模板内容 */
  templateContent: string
  /** 文件名 */
  fileName: string
  /** 状态（0：停用，1：正常） */
  status: number
  /** 备注 */
  remark?: string

}

/**
 * 更新代码生成模板参数
 */
export interface TaktGenTemplateUpdate extends TaktGenTemplateCreate {
  /** 模板ID */
  genTemplateId: string
}

/**
 * 代码生成模板模板
 */
export interface TaktGenTemplateTemplate {
  /** 模板名称 */
  templateName: string
  /** ORM框架类型（1：Entity Framework Core，2：Dapper，3：SqlSugar，4：MyBatis，5：Hibernate，6：SQLAlchemy，7：TypeORM，8：Prisma，9：Eloquent，10：其他） */
  templateOrmType: number
  /** 生成模板分类（1：后端代码，2：前端代码，3：SQL代码） */
  templateCodeType: number
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: number
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: number
  /** 版本号 */
  templateVersion: number
  /** 模板内容 */
  templateContent: string
  /** 文件名 */
  fileName: string
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 代码生成模板导入参数
 */
export interface TaktGenTemplateImport {
  /** 模板名称 */
  templateName: string
  /** ORM框架类型（1：Entity Framework Core，2：Dapper，3：SqlSugar，4：MyBatis，5：Hibernate，6：SQLAlchemy，7：TypeORM，8：Prisma，9：Eloquent，10：其他） */
  templateOrmType: number
  /** 生成模板分类（1：后端代码，2：前端代码，3：SQL代码） */
  templateCodeType: number
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: number
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: number
  /** 版本号 */
  templateVersion: number
  /** 模板内容 */
  templateContent: string
  /** 文件名 */
  fileName: string
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 代码生成模板导出参数
 */
export interface TaktGenTemplateExport {
  /** 模板名称 */
  templateName: string
  /** ORM框架类型（1：Entity Framework Core，2：Dapper，3：SqlSugar，4：MyBatis，5：Hibernate，6：SQLAlchemy，7：TypeORM，8：Prisma，9：Eloquent，10：其他） */
  templateOrmType: number
  /** 生成模板分类（1：后端代码，2：前端代码，3：SQL代码） */
  templateCodeType: number
  /** 模板分类（1：实体类，2：控制器，3：服务层，4：数据访问层，5：视图，6：API，7：其他） */
  templateCategory: number
  /** 编程语言（1：C#，2：Java，3：Python，4：JavaScript，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：TypeScript） */
  templateLanguage: number
  /** 版本号 */
  templateVersion: number
  /** 模板内容 */
  templateContent: string
  /** 文件名 */
  fileName: string
  /** 状态（0：停用，1：正常） */
  status: number
}

/**
 * 代码生成模板分页结果
 */
export type TaktGenTemplatePageResult = TaktPagedResult<TaktGenTemplate> 
