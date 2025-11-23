import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 工作流实例流转历史实体
 */
export interface TaktInstanceTrans extends TaktBaseEntity {
  /** 工作流实例流转历史ID */
  instanceTransId: string
  /** 工作流实例ID */
  instanceId: string
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: number
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: number
  /** 目标节点名称 */
  toNodeName: string
  /** 转化时间 */
  transTime: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流实例流转历史查询参数
 */
export interface TaktInstanceTransQuery extends TaktPagedQuery {
  /** 开始节点名称 */
  startNodeName?: string
  /** 开始节点类型 */
  startNodeType?: number
  /** 目标节点名称 */
  toNodeName?: string
  /** 目标节点类型 */
  toNodeType?: number
  /** 状态 */
  status?: number
  /** 创建人（用于查询我的待办/已办） */
  createBy?: string
  /** 转换开始时间 */
  transTimeStart?: string
  /** 转换结束时间 */
  transTimeEnd?: string
}



/**
 * 工作流实例流转历史导出参数
 */
export interface TaktInstanceTransExport {
  /** 工作流实例ID */
  instanceId: string
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点类型 */
  startNodeType: string
  /** 开始节点名称 */
  startNodeName: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点类型 */
  toNodeType: string
  /** 目标节点名称 */
  toNodeName: string
  /** 转化时间 */
  transTime: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流实例流转历史分页结果
 */
export interface TaktInstanceTransPagedResult extends TaktPagedResult<TaktInstanceTrans> {}
