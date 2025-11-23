import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 工作流实例实体
 */
export interface TaktInstance extends TaktBaseEntity {
  /** 工作流实例ID */
  instanceId: string
  /** 实例标题 */
  instanceTitle: string
  /** 流程定义ID */
  schemeId: string
  /** 流程定义配置(JSON格式) */
  schemeConfig?: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: string
  /** 当前节点ID */
  currentNodeId?: string
  /** 当前节点名称 */
  currentNodeName?: string
  /** 上一节点ID */
  previousNodeId?: string
  /** 上一节点名称 */
  previousNodeName?: string
  /** 优先级(1:低 2:普通 3:高 4:紧急 5:特急) */
  priority: number
  /** 紧急程度(1:普通 2:加急 3:特急) */
  urgency: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 流程变量(JSON格式) */
  variables?: string
  /** 表单定义ID */
  formId?: string
  /** 表单类型 */
  formType: number
  /** 表单数据(JSON格式) */
  formData?: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流实例查询参数
 */
export interface TaktInstanceQuery extends TaktPagedQuery {
  /** 实例标题 */
  instanceTitle?: string
  /** 业务键 */
  businessKey?: string
  /** 状态 */
  status?: number
  /** 优先级 */
  priority?: number
  /** 紧急程度 */
  urgency?: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 工作流实例创建参数
 */
export interface TaktInstanceCreate {
  /** 实例标题 */
  instanceTitle: string
  /** 流程定义ID */
  schemeId: string
  /** 流程定义配置(JSON格式) */
  schemeConfig?: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: string
  /** 当前节点ID */
  currentNodeId?: string
  /** 当前节点名称 */
  currentNodeName?: string
  /** 上一节点ID */
  previousNodeId?: string
  /** 上一节点名称 */
  previousNodeName?: string
  /** 优先级(1:低 2:普通 3:高 4:紧急 5:特急) */
  priority: number
  /** 紧急程度(1:普通 2:加急 3:特急) */
  urgency: number
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 流程变量(JSON格式) */
  variables?: string
  /** 表单定义ID */
  formId?: string
  /** 表单类型 */
  formType: number
  /** 表单数据(JSON格式) */
  formData?: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工作流实例更新参数
 */
export interface TaktInstanceUpdate extends TaktInstanceCreate {
  /** 工作流实例ID */
  instanceId: string
}

/**
 * 工作流实例状态更新参数
 */
export interface TaktInstanceStatus {
  /** 工作流实例ID */
  instanceId: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流实例启动参数
 */
export interface TaktInstanceStart {
  /** 流程定义ID */
  schemeId: string
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 流程变量(JSON格式) */
  variables?: string
}


/**
 * 工作流实例导出参数
 */
export interface TaktInstanceExport {
  /** 实例标题 */
  instanceTitle: string
  /** 流程定义ID */
  schemeId: string
  /** 流程定义配置(JSON格式) */
  schemeConfig: string
  /** 业务键 */
  businessKey: string
  /** 发起人ID */
  initiatorId: string
  /** 当前节点ID */
  currentNodeId: string
  /** 当前节点名称 */
  currentNodeName: string
  /** 上一节点ID */
  previousNodeId: string
  /** 上一节点名称 */
  previousNodeName: string
  /** 优先级(1:低 2:普通 3:高 4:紧急 5:特急) */
  priority: string
  /** 紧急程度(1:普通 2:加急 3:特急) */
  urgency: string
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 流程变量(JSON格式) */
  variables: string
  /** 表单定义ID */
  formId: string
  /** 表单类型 */
  formType: string
  /** 表单数据(JSON格式) */
  formData: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流实例分页结果
 */
export interface TaktInstancePagedResult extends TaktPagedResult<TaktInstance> {}
