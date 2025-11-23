import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 定时任务日志记录 */
export interface TaktQuartzLog extends TaktBaseEntity {
  quartzLogId: number
  logTaskId: number
  logTaskName: string
  logGroupName: string
  logExecuteTime: string
  logExecuteDuration: number
  logExecuteParams: string
  logStatus: number
  logErrorInfo: string
  logExecuteIp: string
  logExecuteHost: string
  createTime: string
}

/** 定时任务日志查询参数 */
export interface TaktQuartzLogQuery extends TaktPagedQuery {
  logTaskName?: string
  logGroupName?: string
  logStatus?: number
  beginTime?: string
  endTime?: string
}



/** 定时任务日志分页结果 */
export type TaktQuartzLogPageResult = TaktPagedResult<TaktQuartzLog> 
