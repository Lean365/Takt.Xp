import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 工作流定义实体
 */
export interface TaktScheme extends TaktBaseEntity {
  /** 工作流定义ID */
  schemeId: string
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: number
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流定义查询参数
 */
export interface TaktSchemeQuery extends TaktPagedQuery {
  /** 流程定义键 */
  schemeKey?: string
  /** 流程定义名称 */
  schemeName?: string
  /** 流程分类 */
  schemeCategory?: number
  /** 状态 */
  status?: number
}

/**
 * 工作流定义创建参数
 */
export interface TaktSchemeCreate {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: number
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: string
  /** 注意事项 */
  notes?: string
  /** 备注 */
  remark?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流定义更新参数
 */
export interface TaktSchemeUpdate extends TaktSchemeCreate {
  /** 工作流定义ID */
  schemeId: string
}

/**
 * 工作流定义状态更新参数
 */
export interface TaktSchemeStatus {
  /** 工作流定义ID */
  schemeId: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流定义模板参数
 */
export interface TaktSchemeTemplate {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: number
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流定义导入参数
 */
export interface TaktSchemeImport {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: number
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流定义导出参数
 */
export interface TaktSchemeExport {
  /** 流程定义键 */
  schemeKey: string
  /** 流程定义名称 */
  schemeName: string
  /** 流程分类(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他) */
  schemeCategory: string
  /** 流程定义版本 */
  version: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 表单定义ID */
  formId: string
  /** 注意事项 */
  notes: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流定义分页结果
 */
export interface TaktSchemePagedResult extends TaktPagedResult<TaktScheme> {}
