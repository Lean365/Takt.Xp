import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** SQL差异日志记录 */
export interface TaktSqlDiffLog extends TaktBaseEntity {
  sqlDiffLogId: number
  diffType: string
  tableName: string
  businessName?: string
  primaryKey?: string
  beforeData?: string
  afterData?: string
  diffFields?: string
  executeSql?: string
  sqlParameters?: string
}

/** SQL差异日志查询参数 */
export interface TaktSqlDiffLogQuery extends TaktPagedQuery {
  diffType?: string
  tableName?: string
  businessName?: string
  primaryKey?: string
  startTime?: string
  endTime?: string
}



/** SQL差异日志分页结果 */
export type TaktSqlDiffLogPageResult = TaktPagedResult<TaktSqlDiffLog> 
