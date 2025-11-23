//===================================================================
// 项目名 : Lean.Takt
// 文件名称: loginLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 登录日志相关接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { 
  TaktLoginLog, 
  TaktLoginLogQuery,
} from '@/types/audit/loginLog'

/**
 * 获取登录日志列表
 * @param params 查询参数
 * @returns 登录日志列表
 */
export function getLoginLogList(query: TaktLoginLogQuery) {
  return request<TaktPagedResult<TaktLoginLog>>({
    url: '/api/TaktLoginLog/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取登录日志详情
 * @param id 日志ID
 * @returns 登录日志详情
 */
export function getLoginLog(id: number) {
  return request<TaktLoginLog>({
    url: `/api/TaktLoginLog/${id}`,
    method: 'get'
  })
}


/**
 * 删除登录日志
 * @param id 日志ID
 * @returns 删除结果
 */
export function deleteLoginLog(id: number) {
  return request<boolean>({
    url: `/api/TaktLoginLog/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除登录日志
 * @param ids 日志ID数组
 * @returns 删除结果
 */
export function batchDeleteLoginLog(ids: number[]) {
  return request<boolean>({
    url: '/api/TaktLoginLog/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 清空登录日志
 * @returns 清空结果
 */
export function clearLoginLog() {
  return request<boolean>({
    url: '/api/TaktLoginLog/clear',
    method: 'delete'
  })
}

/**
 * 导出登录日志
 * @param query 查询参数
 * @returns Excel文件
 */
export function exportLoginLog(query: TaktLoginLogQuery) {
  return request<Blob>({
    url: '/api/TaktLoginLog/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}


