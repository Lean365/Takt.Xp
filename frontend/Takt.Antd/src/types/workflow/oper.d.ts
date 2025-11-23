import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 工作流实例操作记录实体
 */
export interface TaktInstanceOper extends TaktBaseEntity {
  /** 工作流实例操作记录ID */
  instanceOperId: string
  /** 工作流实例ID */
  instanceId: string
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回) */
  operType: number
  /** 操作人ID */
  operatorId: string
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: number
}

/**
 * 工作流实例操作记录查询参数
 */
export interface TaktInstanceOperQuery extends TaktPagedQuery {
  /** 工作流实例ID */
  instanceId?: string
  /** 节点ID */
  nodeId?: string
  /** 节点名称 */
  nodeName?: string
  /** 操作类型 */
  operType?: number
  /** 操作人ID */
  operatorId?: string
  /** 操作人姓名 */
  operatorName?: string
  /** 状态 */
  status?: number
}


/**
 * 工作流审批参数
 */
export interface TaktInstanceApprove {
  /** 工作流实例ID */
  instanceId: string
  /** 节点ID */
  nodeId?: string
  /** 操作类型(2:审批 3:驳回 4:转办) */
  operType: number
  /** 操作意见 */
  operOpinion?: string
  /** 操作数据(JSON格式) */
  operData?: string
}


/**
 * 工作流实例操作记录导出参数
 */
export interface TaktInstanceOperExport {
  /** 工作流实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 操作类型 */
  operType: string
  /** 操作人ID */
  operatorId: string
  /** 操作人姓名 */
  operatorName: string
  /** 操作意见 */
  operOpinion: string
  /** 操作数据(JSON格式) */
  operData: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已停用) */
  status: string
  /** 创建时间 */
  createTime: string
}

/**
 * 工作流实例操作记录分页结果
 */
export interface TaktInstanceOperPagedResult extends TaktPagedResult<TaktInstanceOper> {}
