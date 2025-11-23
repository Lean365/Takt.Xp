/** 定时任务相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 定时任务查询参数 */
export interface TaktQuartzQuery extends TaktPagedQuery {
  /** 任务名称 */
  quartzName?: string
  /** 任务组名 */
  quartzGroupName?: string
  /** 任务类型（1.程序集 2.网络请求 3.SQL语句） */
  quartzType?: number
  /** 触发器类型（0.simple 1.cron） */
  quartzTriggerType?: number
  /** 状态（0停用 1启用） */
  status?: number
  /** 开始时间 */
  startTime?: Date
  /** 结束时间 */
  endTime?: Date
}

/** 定时任务数据传输对象 */
export interface TaktQuartz extends TaktBaseEntity {
  /** ID */
  quartzId: number
  /** 任务名称 */
  quartzName: string
  /** 任务组名 */
  quartzGroupName: string
  /** 任务类型（1.程序集 2.网络请求 3.SQL语句） */
  quartzType: number
  /** 程序集名称 */
  quartzAssemblyName: string
  /** 任务类名 */
  quartzClassName: string
  /** API执行地址 */
  quartzApiUrl?: string
  /** 网络请求方式 */
  quartzRequestMethod?: string
  /** SQL语句 */
  quartzSql?: string
  /** 触发器类型（0.simple 1.cron） */
  quartzTriggerType: number
  /** Cron表达式 */
  quartzCronExpression: string
  /** 执行间隔时间（单位秒） */
  quartzInterval: number
  /** 执行参数 */
  quartzExecuteParams?: string
  /** 是否并发执行 */
  quartzConcurrent: number
  /** 开始时间 */
  quartzStartTime?: Date
  /** 结束时间 */
  quartzEndTime?: Date
  /** 最近执行时间 */
  quartzLastRunTime?: Date
  /** 下次执行时间 */
  quartzNextRunTime?: Date
  /** 执行次数 */
  quartzExecuteCount: number
  /** 状态（0停用 1启用） */
  status: number
}

/** 定时任务创建参数 */
export interface TaktQuartzCreate {
  /** 任务名称 */
  quartzName: string
  /** 任务组名 */
  quartzGroupName: string
  /** 任务类型（1.程序集 2.网络请求 3.SQL语句） */
  quartzType: number
  /** 程序集名称 */
  quartzAssemblyName: string
  /** 任务类名 */
  quartzClassName: string
  /** API执行地址 */
  quartzApiUrl?: string
  /** 网络请求方式 */
  quartzRequestMethod?: string
  /** SQL语句 */
  quartzSql?: string
  /** 触发器类型（0.simple 1.cron） */
  quartzTriggerType: number
  /** Cron表达式 */
  quartzCronExpression: string
  /** 执行间隔时间（单位秒） */
  quartzInterval: number
  /** 执行参数 */
  quartzExecuteParams?: string
  /** 是否并发执行 */
  quartzConcurrent: number
  /** 开始时间 */
  quartzStartTime?: Date
  /** 结束时间 */
  quartzEndTime?: Date
  /** 状态（0停用 1启用） */
  status: number
  /** 备注 */
  remark?: string
}

/** 定时任务更新参数 */
export interface TaktQuartzUpdate extends TaktQuartzCreate {
  /** ID */
  quartzId: number
}

/** 定时任务删除参数 */
export interface TaktQuartzDelete {
  /** ID */
  quartzId: number
}

/** 定时任务批量删除参数 */
export interface TaktQuartzBatchDelete {
  /** ID列表 */
  quartzIds: number[]
}

/** 定时任务导入参数 */
export interface TaktQuartzImport {
  /** 任务名称 */
  quartzName: string
  /** 任务组名 */
  quartzGroupName: string
  /** 任务类型（1.程序集 2.网络请求 3.SQL语句） */
  quartzType: number
  /** 程序集名称 */
  quartzAssemblyName: string
  /** 任务类名 */
  quartzClassName: string
  /** API执行地址 */
  quartzApiUrl?: string
  /** 网络请求方式 */
  quartzRequestMethod?: string
  /** SQL语句 */
  quartzSql?: string
  /** 触发器类型（0.simple 1.cron） */
  quartzTriggerType: number
  /** Cron表达式 */
  quartzCronExpression: string
  /** 执行间隔时间（单位秒） */
  quartzInterval: number
  /** 执行参数 */
  quartzExecuteParams?: string
  /** 是否并发执行 */
  quartzConcurrent: number
  /** 开始时间 */
  quartzStartTime?: Date
  /** 结束时间 */
  quartzEndTime?: Date
  /** 状态（0停用 1启用） */
  status: number
  /** 备注 */
  remark?: string
}

/** 定时任务导出参数 */
export interface TaktQuartzExport {
  /** 任务名称 */
  quartzName: string
  /** 任务组名 */
  quartzGroupName: string
  /** 任务类型（1.程序集 2.网络请求 3.SQL语句） */
  quartzType: number
  /** 程序集名称 */
  quartzAssemblyName: string
  /** 任务类名 */
  quartzClassName: string
  /** API执行地址 */
  quartzApiUrl?: string
  /** 网络请求方式 */
  quartzRequestMethod?: string
  /** SQL语句 */
  quartzSql?: string
  /** 触发器类型（0.simple 1.cron） */
  quartzTriggerType: number
  /** Cron表达式 */
  quartzCronExpression: string
  /** 执行间隔时间（单位秒） */
  quartzInterval: number
  /** 执行参数 */
  quartzExecuteParams?: string
  /** 是否并发执行 */
  quartzConcurrent: number
  /** 开始时间 */
  quartzStartTime?: Date
  /** 结束时间 */
  quartzEndTime?: Date
  /** 最近执行时间 */
  quartzLastRunTime?: Date
  /** 下次执行时间 */
  quartzNextRunTime?: Date
  /** 执行次数 */
  quartzExecuteCount: number
  /** 状态（0停用 1启用） */
  status: number
  /** 创建时间 */
  createTime: Date
  /** 备注 */
  remark?: string
}

/** 定时任务导入模板参数 */
export interface TaktQuartzTemplate {
  /** 任务名称 */
  quartzName: string
  /** 任务组名 */
  quartzGroupName: string
  /** 任务类型（1.程序集 2.网络请求 3.SQL语句） */
  quartzType: number
  /** 程序集名称 */
  quartzAssemblyName: string
  /** 任务类名 */
  quartzClassName: string
  /** API执行地址 */
  quartzApiUrl?: string
  /** 网络请求方式 */
  quartzRequestMethod?: string
  /** SQL语句 */
  quartzSql?: string
  /** 触发器类型（0.simple 1.cron） */
  quartzTriggerType: number
  /** Cron表达式 */
  quartzCronExpression: string
  /** 执行间隔时间（单位秒） */
  quartzInterval: number
  /** 执行参数 */
  quartzExecuteParams?: string
  /** 是否并发执行 */
  quartzConcurrent: number
  /** 开始时间 */
  quartzStartTime?: Date
  /** 结束时间 */
  quartzEndTime?: Date
  /** 状态（0停用 1启用） */
  status: number
  /** 备注 */
  remark?: string
}

/** 定时任务分页结果 */
export type TaktQuartzPagedResult = TaktPagedResult<TaktQuartz> 
