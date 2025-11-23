import {request} from '@/utils/request'
import type { 
  TaktWorkflowStart, 
  TaktWorkflowApprove, 
  TaktWorkflowReject,
  TaktWorkflowTransit,
  TaktConditionEvaluate,
  TaktConditionEvaluateResult,
  TaktWorkflowTodoQuery,
  TaktWorkflowDoneQuery,
  TaktWorkflowTask,
  TaktInstanceStatus, 
  TaktNode,
  TaktTransitionQuery
} from '@/types/workflow/engine'
import type { TaktInstanceTrans } from '@/types/workflow/trans'
import type { TaktInstanceOper } from '@/types/workflow/oper'
import type { TaktPagedResult } from '@/types/common'

// 启动工作流实例
export function startWorkflow(data: TaktWorkflowStart) {
  return request<number>({
    url: '/api/TaktEngine/start',
    method: 'post',
    data
  })
}

/**
 * 暂停工作流
 */
export function suspendWorkflow(instanceId: string, reason: string = '手动暂停') {
  return request<boolean>({
    url: `/api/TaktEngine/${instanceId}/suspend`,
    method: 'post',
    params: { reason }
  })
}

/**
 * 恢复工作流
 */
export function resumeWorkflow(instanceId: string) {
  return request<boolean>({
    url: `/api/TaktEngine/${instanceId}/resume`,
    method: 'post'
  })
}

/**
 * 终止工作流
 */
export function terminateWorkflow(instanceId: string, reason: string) {
  return request<boolean>({
    url: `/api/TaktEngine/${instanceId}/terminate`,
    method: 'post',
    params: { reason }
  })
}

// 审批工作流（兼容 OpenAuth.Net 的 verification 接口）
export function approveWorkflow(data: TaktWorkflowApprove) {
  return request<boolean>({
    url: '/api/TaktEngine/approve',
    method: 'post',
    data
  })
}

// 工作流审批（OpenAuth.Net 风格的接口，兼容 verification）
export function verifyWorkflow(data: TaktWorkflowApprove) {
  return request<boolean>({
    url: '/api/TaktEngine/approve',
    method: 'post',
    data
  })
}

// 撤回工作流（OpenAuth.Net 风格的接口）
export function recallWorkflow(instanceId: string) {
  return request<boolean>({
    url: `/api/TaktEngine/${instanceId}/withdraw`,
    method: 'post'
  })
}

/**
 * 获取工作流状态
 */
export function getWorkflowStatus(instanceId: string) {
  return request<TaktInstanceStatus>({
    url: `/api/TaktEngine/${instanceId}/status`,
    method: 'get'
  })
}

/**
 * 获取可用转换
 */
export function getAvailableTransitions(instanceId: string) {
  return request<TaktInstanceTrans[]>({
    url: `/api/TaktEngine/${instanceId}/transitions`,
    method: 'get'
  })
}

/**
 * 获取当前节点
 */
export function getCurrentNode(instanceId: string) {
  return request<TaktNode>({
    url: `/api/TaktEngine/${instanceId}/current-node`,
    method: 'get'
  })
}

/**
 * 获取工作流变量
 */
export function getWorkflowVariables(instanceId: string) {
  return request<Record<string, any>>({
    url: `/api/TaktEngine/${instanceId}/variables`,
    method: 'get'
  })
}

/**
 * 设置工作流变量
 */
export function setWorkflowVariables(instanceId: string, variables: Record<string, any>) {
  return request<boolean>({
    url: `/api/TaktEngine/${instanceId}/variables`,
    method: 'put',
    data: variables
  })
}

/**
 * 获取工作流历史
 */
export function getWorkflowHistory(instanceId: string) {
  return request<TaktInstanceTrans[]>({
    url: `/api/TaktEngine/${instanceId}/history`,
    method: 'get'
  })
}

/**
 * 获取工作流操作
 */
export function getWorkflowOperations(instanceId: string) {
  return request<TaktInstanceOper[]>({
    url: `/api/TaktEngine/${instanceId}/operations`,
    method: 'get'
  })
}

// 转换历史相关API

// 获取转换历史列表
export function getTransitionList(params: TaktTransitionQuery) {
  return request<TaktPagedResult<TaktInstanceTrans>>({
    url: '/api/TaktEngine/transition/list',
    method: 'get',
    params
  })
}

// 获取转换历史详情
export function getTransition(transitionId: string) {
  return request<TaktInstanceTrans>({
    url: `/api/TaktEngine/transition/${transitionId}`,
    method: 'get'
  })
}

// 拒绝工作流
export function rejectWorkflow(data: TaktWorkflowReject) {
  return request<boolean>({
    url: '/api/TaktEngine/reject',
    method: 'post',
    data
  })
}

// 手动触发节点转换
export function transitWorkflow(data: TaktWorkflowTransit) {
  return request<boolean>({
    url: '/api/TaktEngine/transit',
    method: 'post',
    data
  })
}

// 评估条件表达式
export function evaluateCondition(data: TaktConditionEvaluate) {
  return request<TaktConditionEvaluateResult>({
    url: '/api/TaktEngine/evaluate-condition',
    method: 'post',
    data
  })
}

// 获取待办任务列表
export function getTodoTasks(userId: string, query: TaktWorkflowTodoQuery) {
  return request<TaktWorkflowTask[]>({
    url: `/api/TaktEngine/todo/${userId}`,
    method: 'get',
    params: query
  })
}

// 获取已办任务列表
export function getDoneTasks(userId: string, query: TaktWorkflowDoneQuery) {
  return request<TaktWorkflowTask[]>({
    url: `/api/TaktEngine/done/${userId}`,
    method: 'get',
    params: query
  })
} 
