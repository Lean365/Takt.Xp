import {request} from '@/utils/request'
import type { 
  TaktParallelBranchConfigDto,
  TaktParallelBranchDto,
  TaktParallelBranchStatusDto,
  TaktParallelBranchInfoDto,
  TaktParallelWorkflowInfoDto
} from '@/types/workflow/parallel'

// 创建并行分支
export function createParallelBranches(data: TaktParallelBranchConfigDto) {
  return request<TaktParallelBranchDto[]>({
    url: '/api/TaktEngine/parallel/branches',
    method: 'post',
    data
  })
}

// 获取并行分支状态
export function getParallelBranchStatus(instanceId: string, branchId: string) {
  return request<TaktParallelBranchStatusDto>({
    url: `/api/TaktEngine/parallel/branches/${instanceId}/${branchId}/status`,
    method: 'get'
  })
}

// 获取并行分支列表
export function getParallelBranches(instanceId: string) {
  return request<TaktParallelBranchInfoDto[]>({
    url: `/api/TaktEngine/parallel/branches/${instanceId}`,
    method: 'get'
  })
}

// 强制汇聚并行分支
export function forceJoinParallelBranches(instanceId: string, branchIds: string[]) {
  return request<boolean>({
    url: `/api/TaktEngine/parallel/branches/${instanceId}/force-join`,
    method: 'post',
    data: { branchIds }
  })
}

// 获取并行工作流信息
export function getParallelWorkflowInfo(instanceId: string) {
  return request<TaktParallelWorkflowInfoDto>({
    url: `/api/TaktEngine/parallel/workflow/${instanceId}`,
    method: 'get'
  })
}

// 暂停并行分支
export function pauseParallelBranch(instanceId: string, branchId: string) {
  return request<boolean>({
    url: `/api/TaktEngine/parallel/branches/${instanceId}/${branchId}/pause`,
    method: 'post'
  })
}

// 恢复并行分支
export function resumeParallelBranch(instanceId: string, branchId: string) {
  return request<boolean>({
    url: `/api/TaktEngine/parallel/branches/${instanceId}/${branchId}/resume`,
    method: 'post'
  })
}

// 终止并行分支
export function terminateParallelBranch(instanceId: string, branchId: string, reason: string) {
  return request<boolean>({
    url: `/api/TaktEngine/parallel/branches/${instanceId}/${branchId}/terminate`,
    method: 'post',
    data: { reason }
  })
}

