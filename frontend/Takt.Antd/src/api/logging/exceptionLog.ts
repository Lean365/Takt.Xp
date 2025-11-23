//===================================================================
// 项目名 : Lean.Takt
// 文件名称: exceptionLog.ts
// 创建者  : Claude
// 创建时间: 2024-03-27
// 版本号  : v1.0.0
// 描述    : 异常日志相关接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { TaktExceptionLog, TaktExceptionLogQuery } from '@/types/audit/exceptionLog'

/**
 * 获取异常日志列表
 * @param query 查询参数
 * @returns 异常日志列表
 */
export function getExceptionLogList(query: TaktExceptionLogQuery) {
  return request<TaktPagedResult<TaktExceptionLog>>({
    url: '/api/TaktExceptionLog/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取异常日志详情
 * @param query 查询参数
 * @returns 异常日志详情
 */
export function getExceptionLog(logId: number) {
  return request<TaktExceptionLog>({
    url: `/api/TaktExceptionLog/${logId}`,
    method: 'get'
  })
}

/**
 * 导出异常日志数据
 * @param query 查询条件
 * @param sheetName 工作表名称
 * @returns Excel文件
 */
export function exportExceptionLog(query: TaktExceptionLogQuery, sheetName: string = '异常日志') {
  return request<Blob>({
    url: '/api/TaktExceptionLog/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

/**
 * 清空异常日志
 * @returns 是否成功
 */
export function clearExceptionLog() {
  return request<boolean>({
    url: '/api/TaktExceptionLog/clear',
    method: 'delete'
  })
}

