import {request} from '@/utils/request'
import type { TaktQuartz, TaktQuartzQuery, TaktQuartzCreate, TaktQuartzUpdate } from '@/types/routine/quartz/quartz'
import type { TaktPagedResult } from '@/types/common'

/**
 * 获取定时任务列表
 * @param params 查询参数
 * @returns 定时任务列表
 */
export function getQuartzJobList(params: TaktQuartzQuery) {
  return request<TaktPagedResult<TaktQuartz>>({
    url: '/api/TaktQuartzJob/list',
    method: 'get',
    params
  })
}

/**
 * 获取定时任务详情
 * @param id 定时任务ID
 * @returns 定时任务详情
 */
export function getQuartzJobDetail(id: number | bigint) {
  return request<TaktQuartz>({
    url: `/api/TaktQuartzJob/${id}`,
    method: 'get'
  })
}

/**
 * 创建定时任务
 * @param data 定时任务数据
 * @returns 创建结果
 */
export function createQuartzJob(data: TaktQuartzCreate) {
  return request<TaktQuartz>({
    url: '/api/TaktQuartzJob',
    method: 'post',
    data
  })
}

/**
 * 更新定时任务
 * @param id 定时任务ID
 * @param data 定时任务数据
 * @returns 更新结果
 */
export function updateQuartzJob(id: number | bigint, data: TaktQuartzUpdate) {
  return request<TaktQuartz>({
    url: `/api/TaktQuartzJob/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除定时任务
 * @param id 定时任务ID
 * @returns 删除结果
 */
export function deleteQuartzJob(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktQuartzJob/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除定时任务
 * @param ids 定时任务ID列表
 * @returns 删除结果
 */
export function batchDeleteQuartzJob(ids: number[] | bigint[]) {
  return request<boolean>({
    url: '/api/TaktQuartzJob/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出定时任务列表
 * @param params 查询参数
 * @returns 导出结果
 */
export function exportQuartzJobList(params: TaktQuartzQuery) {
  return request<Blob>({
    url: '/api/TaktQuartzJob/export',
    method: 'get',
    params,
    responseType: 'blob'
  })
}

/**
 * 导入定时任务列表
 * @param file 文件
 * @returns 导入结果
 */
export function importQuartzJobList(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<boolean>({
    url: '/api/TaktQuartzJob/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 暂停定时任务
 * @param id 定时任务ID
 * @returns 暂停结果
 */
export function pauseQuartzJob(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktQuartzJob/pause/${id}`,
    method: 'put'
  })
}

/**
 * 恢复定时任务
 * @param id 定时任务ID
 * @returns 恢复结果
 */
export function resumeQuartzJob(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktQuartzJob/resume/${id}`,
    method: 'put'
  })
}

/**
 * 立即执行定时任务
 * @param id 定时任务ID
 * @returns 执行结果
 */
export function runQuartzJob(id: number | bigint) {
  return request<boolean>({
    url: `/api/TaktQuartzJob/run/${id}`,
    method: 'put'
  })
} 
