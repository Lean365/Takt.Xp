import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

// #region 并行流程相关类型

/**
 * 并行分支配置
 */
export interface TaktParallelBranchConfig {
  /** 分支列表 */
  branches: TaktParallelBranch[]
  /** 汇聚策略(1:全部完成 2:任一完成 3:指定数量完成) */
  joinStrategy: number
  /** 汇聚数量(当JoinStrategy为3时使用) */
  joinCount?: number
  /** 超时时间(分钟) */
  timeoutMinutes?: number
}

/**
 * 并行分支
 */
export interface TaktParallelBranch {
  /** 分支名称 */
  branchName: string
  /** 分支键 */
  branchKey: string
  /** 流程定义ID */
  schemeId: string
  /** 开始节点ID */
  startNodeId: string
  /** 开始节点名称 */
  startNodeName: string
  /** 分支变量 */
  variables?: string
  /** 优先级 */
  priority: number
  /** 紧急程度 */
  urgency: number
}

/**
 * 并行分支状态
 */
export interface TaktParallelBranchStatus {
  /** 实例ID */
  instanceId: string
  /** 总分支数 */
  totalBranches: number
  /** 已完成分支数 */
  completedBranches: number
  /** 运行中分支数 */
  runningBranches: number
  /** 已暂停分支数 */
  suspendedBranches: number
  /** 已终止分支数 */
  terminatedBranches: number
  /** 是否全部完成 */
  isAllCompleted: boolean
  /** 完成百分比 */
  completionPercentage: number
}

/**
 * 并行分支信息
 */
export interface TaktParallelBranchInfo {
  /** 分支实例ID */
  branchInstanceId: string
  /** 分支名称 */
  branchName: string
  /** 状态 */
  status: number
  /** 当前节点名称 */
  currentNodeName?: string
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
  /** 进度 */
  progress: number
}

/**
 * 并行工作流信息
 */
export interface TaktParallelWorkflowInfo {
  /** 主实例ID */
  mainInstanceId: string
  /** 主实例标题 */
  mainInstanceTitle: string
  /** 主实例状态 */
  mainInstanceStatus: number
  /** 分支列表 */
  branches: TaktParallelBranchInfo[]
  /** 总分支数 */
  totalBranches: number
  /** 已完成分支数 */
  completedBranches: number
  /** 运行中分支数 */
  runningBranches: number
}

#endregion

/**
 * 工作流节点实体
 */
export interface TaktNode extends TaktBaseEntity {
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 节点类型 */
  nodeType: string
  /** 节点配置(JSON格式) */
  nodeConfig?: string
  /** 审批人类型(1:指定用户 2:角色 3:部门 4:动态) */
  approverType: number
  /** 审批人配置(JSON格式) */
  approverConfig?: string
}

/**
 * 工作流审批参数（兼容 OpenAuth.Net）
 */
export interface TaktWorkflowApprove {
  /** 工作流实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 操作类型(1:同意 2:拒绝 3:退回 4:转办 5:委托 6:加签 7:知会 8:撤回) */
  operType: number
  /** 操作意见 */
  operOpinion: string
  /** 操作数据(JSON格式) */
  operData?: string
  /** 目标用户ID(转办/委托时使用) */
  targetUserId?: string
  /** 驳回步骤，即驳回到的节点ID（退回时使用） */
  nodeRejectStep?: string
  /** 驳回类型（退回时使用）
   * null:使用节点配置的驳回类型
   * 0:前一步
   * 1:第一步
   * 2:指定节点，使用nodeRejectStep
   */
  nodeRejectType?: string
  /** 表单数据（如果该节点有可以修改的表单项时，会提交表单数据信息） */
  formData?: string
  /** 下一个节点执行权限类型（运行时指定执行者时使用）
   * RUNTIME_SPECIAL_ROLE:运行时指定角色
   * RUNTIME_SPECIAL_USER:运行时指定用户
   */
  nodeDesignateType?: string
  /** 下一个节点执行者（运行时指定执行者时使用）
   * 如果nodeDesignateType为RUNTIME_SPECIAL_ROLE，则该值为指定的角色ID数组
   * 如果nodeDesignateType为RUNTIME_SPECIAL_USER，则该值为指定的用户ID数组
   */
  nodeDesignates?: string[]
}

/**
 * 工作流启动参数
 */
export interface TaktWorkflowStart {
  /** 流程定义ID */
  schemeId: string
  /** 实例标题 */
  instanceTitle: string
  /** 业务键 */
  businessKey?: string
  /** 发起人ID */
  initiatorId: string
  /** 流程变量(JSON格式) */
  variables?: string
}

/**
 * 工作流实例状态参数
 */
export interface TaktInstanceStatus {
  /** 工作流实例ID */
  instanceId: string
  /** 状态(0:草稿 1:运行中 2:已完成 3:已暂停 4:已终止) */
  status: number
}

/**
 * 工作流转换查询参数
 */
export interface TaktTransitionQuery extends TaktPagedQuery {
  /** 实例ID */
  instanceId?: string
  /** 开始节点ID */
  startNodeId?: string
  /** 目标节点ID */
  toNodeId?: string
  /** 状态 */
  status?: number
}

/**
 * 工作流节点查询参数
 */
export interface TaktNodeQuery extends TaktPagedQuery {
  /** 节点名称 */
  nodeName?: string
  /** 节点类型 */
  nodeType?: string
  /** 审批人类型 */
  approverType?: number
}



/**
 * 工作流节点创建参数
 */
export interface TaktNodeCreate {
  /** 节点名称 */
  nodeName: string
  /** 节点类型 */
  nodeType: string
  /** 节点配置(JSON格式) */
  nodeConfig?: string
  /** 审批人类型(1:指定用户 2:角色 3:部门 4:动态) */
  approverType: number
  /** 审批人配置(JSON格式) */
  approverConfig?: string
}

/**
 * 工作流节点更新参数
 */
export interface TaktNodeUpdate extends TaktNodeCreate {
  /** 节点ID */
  nodeId: string
}


/**
 * 工作流节点分页结果
 */
export interface TaktNodePagedResult extends TaktPagedResult<TaktNode> {}

/**
 * 工作流拒绝参数
 */
export interface TaktWorkflowReject {
  /** 工作流实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 拒绝原因 */
  rejectReason: string
  /** 拒绝数据(JSON格式) */
  rejectData?: string
}

/**
 * 工作流转换参数
 */
export interface TaktWorkflowTransit {
  /** 工作流实例ID */
  instanceId: string
  /** 源节点ID */
  fromNodeId: string
  /** 目标节点ID */
  toNodeId: string
  /** 转换原因 */
  transitReason: string
  /** 转换数据(JSON格式) */
  transitData?: string
}

/**
 * 条件评估参数
 */
export interface TaktConditionEvaluate {
  /** 条件表达式 */
  expression: string
  /** 工作流变量 */
  variables: Record<string, any>
}

/**
 * 条件评估结果
 */
export interface TaktConditionEvaluateResult {
  /** 条件表达式 */
  expression: string
  /** 评估结果 */
  result: boolean
  /** 错误信息 */
  errorMessage: string
}

/**
 * 工作流任务
 */
export interface TaktWorkflowTask {
  /** 任务ID */
  taskId: number
  /** 工作流实例ID */
  instanceId: string
  /** 实例名称 */
  instanceName: string
  /** 流程定义ID */
  schemeId: string
  /** 流程定义名称 */
  schemeName: string
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 节点类型 */
  nodeType: string
  /** 发起人姓名 */
  startUserName: string
  /** 当前处理人姓名 */
  currentUserName: string
  /** 状态 */
  status: string
  /** 创建时间 */
  createTime: string
  /** 更新时间 */
  updateTime: string
}

/**
 * 待办任务查询参数
 */
export interface TaktWorkflowTodoQuery {
  /** 页码 */
  pageIndex: number
  /** 页大小 */
  pageSize: number
  /** 关键词 */
  keyword?: string
  /** 流程定义ID */
  schemeId?: string
  /** 节点ID */
  nodeId?: string
  /** 开始时间 */
  startTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 已办任务查询参数
 */
export interface TaktWorkflowDoneQuery {
  /** 页码 */
  pageIndex: number
  /** 页大小 */
  pageSize: number
  /** 关键词 */
  keyword?: string
  /** 流程定义ID */
  schemeId?: string
  /** 节点ID */
  nodeId?: string
  /** 操作类型 */
  operType?: string
}

/**
 * 工作流任务实体
 */
export interface TaktTask extends TaktBaseEntity {
  /** 任务ID */
  taskId: string
  /** 任务名称 */
  taskName: string
  /** 任务状态 0:待处理 1:处理中 2:已完成 3:已拒绝 4:已转办 */
  taskState: number
  /** 优先级 0:低 1:中 2:高 3:紧急 */
  priority: number
  /** 流程实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 处理人ID */
  assigneeId: string
  /** 处理人姓名 */
  assigneeName: string
  /** 处理时间 */
  processTime?: string
  /** 完成时间 */
  finishTime?: string
  /** 处理意见 */
  processOpinion?: string
  /** 任务描述 */
  description?: string
  /** 任务类型 */
  taskType?: number
  /** 任务来源 */
  taskSource?: string
  /** 任务参数 */
  taskParams?: string
  /** 任务结果 */
  taskResult?: string
  /** 任务备注 */
  remark?: string
}

/**
 * 工作流任务查询参数
 */
export interface TaktTaskQuery {
  /** 页码 */
  pageIndex: number
  /** 页大小 */
  pageSize: number
  /** 任务状态 */
  taskState?: number
  /** 优先级 */
  priority?: number
  /** 任务名称 */
  taskName?: string
  /** 流程实例ID */
  instanceId?: string
  /** 节点名称 */
  nodeName?: string
  /** 处理人ID */
  assigneeId?: string
  /** 处理人姓名 */
  assigneeName?: string
  /** 处理时间开始 */
  processTimeStart?: string
  /** 处理时间结束 */
  processTimeEnd?: string
  /** 创建时间开始 */
  createTimeStart?: string
  /** 创建时间结束 */
  createTimeEnd?: string
  /** 任务类型 */
  taskType?: number
  /** 任务来源 */
  taskSource?: string
}

/**
 * 工作流任务创建参数
 */
export interface TaktTaskCreate {
  /** 任务名称 */
  taskName: string
  /** 流程实例ID */
  instanceId: string
  /** 节点ID */
  nodeId: string
  /** 节点名称 */
  nodeName: string
  /** 处理人ID */
  assigneeId: string
  /** 处理人姓名 */
  assigneeName: string
  /** 优先级 */
  priority?: number
  /** 任务描述 */
  description?: string
  /** 任务类型 */
  taskType?: number
  /** 任务来源 */
  taskSource?: string
  /** 任务参数 */
  taskParams?: string
}

/**
 * 工作流任务更新参数
 */
export interface TaktTaskUpdate {
  /** 任务ID */
  taskId: string
  /** 任务名称 */
  taskName?: string
  /** 任务状态 */
  taskState?: number
  /** 优先级 */
  priority?: number
  /** 处理人ID */
  assigneeId?: string
  /** 处理人姓名 */
  assigneeName?: string
  /** 处理时间 */
  processTime?: string
  /** 完成时间 */
  finishTime?: string
  /** 处理意见 */
  processOpinion?: string
  /** 任务描述 */
  description?: string
  /** 任务参数 */
  taskParams?: string
  /** 任务结果 */
  taskResult?: string
  /** 任务备注 */
  remark?: string
}

/**
 * 工作流任务处理参数
 */
export interface TaktTaskProcess {
  /** 任务ID */
  taskId: string
  /** 处理动作 1:同意 2:拒绝 3:转办 4:退回 */
  action: number
  /** 处理意见 */
  opinion?: string
  /** 转办人ID */
  transferUserId?: string
  /** 转办人姓名 */
  transferUserName?: string
  /** 处理参数 */
  processParams?: string
}

/**
 * 工作流任务批量处理参数
 */
export interface TaktTaskBatchProcess {
  /** 任务ID列表 */
  taskIds: string[]
  /** 处理动作 */
  action: number
  /** 处理意见 */
  opinion?: string
  /** 转办人ID */
  transferUserId?: string
  /** 转办人姓名 */
  transferUserName?: string
  /** 处理参数 */
  processParams?: string
} 
