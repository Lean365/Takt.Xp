//===================================================================
// 项目名 : Lean.Takt
// 文件名 : config.d.ts
// 创建者 : Claude
// 创建时间: 2024-04-24
// 版本号 : v1.0.0
// 描述    : 代码生成配置类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 代码生成配置实体
 */
export interface TaktGenConfig extends TaktBaseEntity {
  /** 配置ID */
  genConfigId: string
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 项目名称 */
  projectName: string
  /** 模块名 */
  moduleName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成方式 */
  genMethod: number
  /** 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用TaktGenTemplate表中的模板） */
  genTplType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态（0正常 1停用） */
  status: number
}

/**
 * 代码生成配置查询参数
 */
export interface TaktGenConfigQuery extends TaktPagedQuery {
  /** 配置名称 */
  genConfigName?: string

  /** 项目名称 */
  projectName?: string
  /** 模块名 */
  moduleName?: string
  /** 包名 */
  businessName?: string
  /** 功能名 */
  functionName?: string
  /** 生成方式 */
  genMethod?: number
  /** 状态 */
  status?: number
  /** 日期范围 */
  dateRange?: [string, string]
}

/**
 * 代码生成配置创建参数
 */
export interface TaktGenConfigCreate {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 项目名称 */
  projectName: string
  /** 模块名 */
  moduleName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成方式 */
  genMethod: number
  /** 模板类型 */
  genTplType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态 */
  status: number
}

/**
 * 代码生成配置更新参数
 */
export interface TaktGenConfigUpdate extends TaktGenConfigCreate {
  /** 配置ID */
  genConfigId: string
}

/**
 * 代码生成配置模板
 */
export interface TaktGenConfigTemplate {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 项目名称 */
  projectName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成方式 */
  genMethod: string
  /** 模板类型 */
  genTplType: string
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options: string
  /** 状态 */
  status: string
}

/**
 * 代码生成配置导入参数
 */
export interface TaktGenConfigImport {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 项目名称 */
  projectName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成方式 */
  genMethod: number
  /** 模板类型 */
  genTplType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态 */
  status: number
}

/**
 * 代码生成配置导出参数
 */
export interface TaktGenConfigExport {
  /** 配置名称 */
  genConfigName: string
  /** 作者 */
  author: string
  /** 模块名 */
  moduleName: string
  /** 项目名称 */
  projectName: string
  /** 业务名 */
  businessName: string
  /** 功能名 */
  functionName: string
  /** 生成方式 */
  genMethod: number
  /** 模板类型 */
  genTplType: number
  /** 生成路径 */
  genPath: string
  /** 选项配置 */
  options?: string
  /** 状态 */
  status: number
  /** 创建时间 */
  createTime: string
}

/**
 * 代码生成配置分页结果
 */
export type TaktGenConfigPageResult = TaktPagedResult<TaktGenConfig> 
