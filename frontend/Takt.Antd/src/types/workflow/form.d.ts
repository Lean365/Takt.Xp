import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 表单实体
 */
export interface TaktForm extends TaktBaseEntity {
  /** 表单ID */
  formId: string
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他) */
  formCategory: number
  /** 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他) */
  formType: number
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 业务实体表名 */
  businessTableName?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 表单查询参数
 */
export interface TaktFormQuery extends TaktPagedQuery {
  /** 表单键 */
  formKey?: string
  /** 表单名称 */
  formName?: string
  /** 表单分类 */
  formCategory?: number
  /** 表单类型 */
  formType?: number
  /** 状态 */
  status?: number
}

/**
 * 表单创建参数
 */
export interface TaktFormCreate {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他) */
  formCategory: number
  /** 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他) */
  formType: number
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 业务实体表名 */
  businessTableName?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 表单更新参数
 */
export interface TaktFormUpdate extends TaktFormCreate {
  /** 表单ID */
  formId: string
}

/**
 * 表单状态更新参数
 */
export interface TaktFormStatus {
  /** 表单ID */
  formId: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 表单模板参数
 */
export interface TaktFormTemplate {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他) */
  formCategory: number
  /** 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他) */
  formType: number
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 业务实体表名 */
  businessTableName?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 表单导入参数
 */
export interface TaktFormImport {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他) */
  formCategory: number
  /** 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他) */
  formType: number
  /** 表单版本 */
  version: string
  /** 表单配置(JSON格式) */
  formConfig: string
  /** 业务实体表名 */
  businessTableName?: string
  /** 注意事项 */
  notes?: string
  /** 状态(0:草稿 1:已发布 2:已完成 3:已停用) */
  status: number
}

/**
 * 表单导出参数
 */
export interface TaktFormExport {
  /** 表单键 */
  formKey: string
  /** 表单名称 */
  formName: string
  /** 表单分类 */
  formCategory: string
  /** 表单类型 */
  formType: string
  /** 表单版本 */
  version: string
  /** 表单配置 */
  formConfig: string
  /** 业务实体表名 */
  businessTableName: string
  /** 注意事项 */
  notes: string
  /** 状态 */
  status: string
  /** 创建时间 */
  createTime: string
}

/**
 * 表单分页结果
 */
export interface TaktFormPagedResult extends TaktPagedResult<TaktForm> {}


