//===================================================================
// 项目名 : Lean.Takt
// 文件名称: dbDiffLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 数据库差异日志相关接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { TaktSqlDiffLog, TaktSqlDiffLogQuery } from '@/types/audit/sqlDiffLog'

/**
 * 获取数据库差异日志列表
 * @param query 查询参数
 * @returns 数据库差异日志列表
 */
export function getSqlDiffLogList(query: TaktSqlDiffLogQuery) {
  return request<TaktPagedResult<TaktSqlDiffLog>>({
    url: '/api/TaktSqlDiffLog/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取数据库差异日志详情
 * @param logId 日志ID
 * @returns 数据库差异日志详情
 */
export function getSqlDiffLog(logId: number) {
  return request<TaktSqlDiffLog>({
    url: `/api/TaktSqlDiffLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出数据库差异日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportSqllDiffLog(query: TaktSqlDiffLogQuery, sheetName: string = '数据库差异日志') {
  return request<Blob>({
    url: '/api/TaktSqlDiffLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空数据库差异日志
 * @returns 是否成功
 */
export function clearSqlDiffLog() {
  return request<boolean>({
    url: '/api/TaktSqlDiffLog/clear',
    method: 'delete'
  })
} 
