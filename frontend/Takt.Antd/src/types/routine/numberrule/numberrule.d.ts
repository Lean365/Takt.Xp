//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktNumberRule.ts
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : v1.0.0
// 描述    : 编号规则类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 编号规则实体
 */
export interface TaktNumberRule extends TaktBaseEntity {
  /** 编号规则ID */
  numberRuleId: string
  /** 公司代码 */
  companyCode: string
  /** 部门代码 */
  deptCode: string
  /** 编号规则代码 */
  numberRuleCode: string
  /** 编号规则简称 */
  numberRuleShortCode: string
  /** 编号规则名称 */
  numberRuleName: string
  /** 编号规则类型(1:日常事务 2:人力资源 3:财务核算 4:后勤管理 5:生产管理 6:工作流) */
  numberRuleType: number
  /** 编号规则描述 */
  numberRuleDescription: string
  /** 编号前缀 */
  numberPrefix: string
  /** 编号后缀 */
  numberSuffix: string
  /** 修订号(用于标识文档的修订版本，自然数序号，如1、2、3等) */
  revisionNumber: number
  /** 日期格式 */
  dateFormat: string
  /** 序号长度 */
  sequenceLength: number
  /** 序号起始值 */
  sequenceStart: number
  /** 序号步长 */
  sequenceStep: number
  /** 当前序号 */
  currentSequence: number
  /** 序号重置规则(1:不重置 2:按年重置 3:按月重置 4:按日重置) */
  sequenceResetRule: number
  /** 编号格式模板 */
  numberFormatTemplate: string
  /** 编号示例 */
  numberExample: string
  /** 是否包含公司代码(0:否 1:是) */
  includeCompanyCode: number
  /** 是否包含部门代码(0:否 1:是) */
  includeDepartmentCode: number
  /** 是否包含修订号(0:否 1:是) */
  includeRevisionNumber: number
  /** 是否包含年份(0:否 1:是) */
  includeYear: number
  /** 是否包含月份(0:否 1:是) */
  includeMonth: number
  /** 是否包含日期(0:否 1:是) */
  includeDay: number
  /** 是否包含小时(0:否 1:是) */
  includeHour: number
  /** 是否包含分钟(0:否 1:是) */
  includeMinute: number
  /** 是否包含秒(0:否 1:是) */
  includeSecond: number
  /** 是否包含毫秒(0:否 1:是) */
  includeMillisecond: number
  /** 是否包含随机数(0:否 1:是) */
  includeRandom: number
  /** 随机数长度 */
  randomLength: number
  /** 是否包含校验位(0:否 1:是) */
  includeCheckDigit: number
  /** 校验位算法(1:模10 2:模11) */
  checkDigitAlgorithm: number
  /** 是否允许重复(0:否 1:是) */
  allowDuplicate: number
  /** 重复检查范围(1:全局 2:按年 3:按月 4:按日) */
  duplicateCheckScope: number
  /** 排序号 */
  orderNum: number
  /** 最后使用时间 */
  lastUsedTime?: string
  /** 使用次数 */
  usageCount: number
  /** 状态(0:正常 1:停用) */
  status: number
}

/**
 * 编号规则查询参数
 */
export interface TaktNumberRuleQuery extends TaktPagedQuery {
  /** 公司代码 */
  companyCode?: string
  /** 部门代码 */
  deptCode?: string
  /** 编号规则代码 */
  numberRuleCode?: string
  /** 编号规则简称 */
  numberRuleShortCode?: string
  /** 编号规则名称 */
  numberRuleName?: string
  /** 编号规则类型 */
  numberRuleType?: number
  /** 状态 */
  status: number
}

/**
 * 编号规则创建参数
 */
export interface TaktNumberRuleCreate {
  /** 公司代码 */
  companyCode: string
  /** 部门代码 */
  deptCode: string
  /** 编号规则代码 */
  numberRuleCode: string
  /** 编号规则简称 */
  numberRuleShortCode: string
  /** 编号规则名称 */
  numberRuleName: string
  /** 编号规则类型 */
  numberRuleType: number
  /** 编号规则描述 */
  numberRuleDescription: string
  /** 编号前缀 */
  numberPrefix: string
  /** 编号后缀 */
  numberSuffix: string
  /** 修订号 */
  revisionNumber: number
  /** 日期格式 */
  dateFormat: string
  /** 序号长度 */
  sequenceLength: number
  /** 序号起始值 */
  sequenceStart: number
  /** 序号步长 */
  sequenceStep: number
  /** 当前序号 */
  currentSequence: number
  /** 序号重置规则 */
  sequenceResetRule: number
  /** 编号格式模板 */
  numberFormatTemplate: string
  /** 编号示例 */
  numberExample: string
  /** 是否包含公司代码 */
  includeCompanyCode: number
  /** 是否包含部门代码 */
  includeDepartmentCode: number
  /** 是否包含修订号 */
  includeRevisionNumber: number
  /** 是否包含年份 */
  includeYear: number
  /** 是否包含月份 */
  includeMonth: number
  /** 是否包含日期 */
  includeDay: number
  /** 是否包含小时 */
  includeHour: number
  /** 是否包含分钟 */
  includeMinute: number
  /** 是否包含秒 */
  includeSecond: number
  /** 是否包含毫秒 */
  includeMillisecond: number
  /** 是否包含随机数 */
  includeRandom: number
  /** 随机数长度 */
  randomLength: number
  /** 是否包含校验位 */
  includeCheckDigit: number
  /** 校验位算法 */
  checkDigitAlgorithm: number
  /** 是否允许重复 */
  allowDuplicate: number
  /** 重复检查范围 */
  duplicateCheckScope: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
  /** 状态 */
  status: number
}

/**
 * 编号规则更新参数
 */
export interface TaktNumberRuleUpdate extends TaktNumberRuleCreate {
  /** 编号规则ID */
  numberRuleId: string
}

/**
 * 批量删除编号规则参数
 */
export interface TaktNumberRuleBatchDelete {
  /** 编号规则ID数组 */
  numberRuleIds: string[]
}

/**
 * 更新编号规则状态参数
 */
export interface TaktNumberRuleStatusUpdate {
  /** 编号规则ID */
  numberRuleId: string
  /** 状态 */
  status: number
}


/**
 * 编号规则分页结果
 */
export type TaktNumberRulePageResult = TaktPagedResult<TaktNumberRule>

