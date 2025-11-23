//===================================================================
// 项目名 : Lean.Takt
// 文件名称: quartzLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 任务日志相关接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { TaktQuartzLog, TaktQuartzLogQuery } from '@/types/audit/quartzLog'

/**
 * 获取任务日志列表
 * @param query 查询参数
 * @returns 任务日志列表
 */
export function getQuartzLogList(query: TaktQuartzLogQuery) {
  return request<TaktPagedResult<TaktQuartzLog>>({
    url: '/api/TaktQuartzLog/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取任务日志详情
 * @param logId 日志ID
 * @returns 任务日志详情
 */
export function getQuartzLog(logId: number) {
  return request<TaktQuartzLog>({
    url: `/api/TaktQuartzLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出任务日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportQuartzLog(query: TaktQuartzLogQuery, sheetName: string = '任务日志') {
  return request<Blob>({
    url: '/api/TaktQuartzLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空任务日志
 * @returns 是否成功
 */
export function clearQuartzLog() {
  return request<boolean>({
    url: '/api/TaktQuartzLog/clear',
    method: 'delete'
  })
} 
