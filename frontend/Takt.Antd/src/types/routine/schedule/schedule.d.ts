/** 日程相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 日程查询参数 */
export interface TaktScheduleQuery extends TaktPagedQuery {
  title?: string
  status?: number
  startTime?: string
  endTime?: string
  [key: string]: any
}

/** 日程实体 */
export interface TaktSchedule extends TaktBaseEntity {
  scheduleId: number
  title: string
  content?: string
  location?: string
  startTime: string
  endTime: string
  status: number
  [key: string]: any
}

/** 日程创建参数 */
export interface TaktScheduleCreate {
  title: string
  content?: string
  location?: string
  startTime: string
  endTime: string
  status: number
  [key: string]: any
}

/** 日程更新参数 */
export interface TaktScheduleUpdate extends TaktScheduleCreate {
  scheduleId: number
}

/** 日程批量删除参数 */
export interface TaktScheduleBatchDelete {
  scheduleIds: number[]
}

/** 日程分页结果 */
export type TaktSchedulePagedResult = TaktPagedResult<TaktSchedule> 
