//===================================================================
// 项目名 : Lean.Takt
// 文件名称: operLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 操作日志相关接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { TaktOperLog, TaktOperLogQuery } from '@/types/audit/operLog'

/**
 * 获取操作日志列表
 * @param query 查询参数
 * @returns 操作日志列表
 */
export function getOperLogList(query: TaktOperLogQuery) {
  return request<TaktPagedResult<TaktOperLog>>({
    url: '/api/TaktOperLog/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取操作日志详情
 * @param logId 日志ID
 * @returns 操作日志详情
 */
export function getOperLog(logId: number) {
  return request<TaktOperLog>({
    url: `/api/TaktOperLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出操作日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportOperLog(query: TaktOperLogQuery, sheetName: string = '操作日志') {
  return request<Blob>({
    url: '/api/TaktOperLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空操作日志
 * @returns 是否成功
 */
export function clearOperLog() {
  return request<boolean>({
    url: '/api/TaktOperLog/clear',
    method: 'delete'
  })
}

