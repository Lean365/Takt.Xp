//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktNumberRule.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 编号规则相关接口
//===================================================================

import { request } from '@/utils/request'
import type {
  TaktNumberRule,
  TaktNumberRuleQuery,
  TaktNumberRuleCreate,
  TaktNumberRuleUpdate,
  TaktNumberRuleStatusUpdate,
  TaktNumberRulePageResult
} from '@/types/routine/core/numberrule'

/**
 * 获取编号规则分页列表
 * @param params 查询参数
 * @returns 编号规则分页列表
 */
export const getNumberRuleList = (params: TaktNumberRuleQuery) => {
  return request<TaktNumberRulePageResult>({
    url: '/api/TaktNumberingRules/list',
    method: 'get',
    params
  })
}

/**
 * 获取编号规则详情
 * @param numberRuleId 编号规则ID
 * @returns 编号规则详情
 */
export const getByIdAsync = (numberRuleId: string) => {
  return request<TaktNumberRule>({
    url: `/api/TaktNumberingRules/${numberRuleId}`,
    method: 'get'
  })
}

/**
 * 创建编号规则
 * @param data 创建参数
 * @returns 编号规则ID
 */
export const createNumberRule = (data: TaktNumberRuleCreate) => {
  return request<number>({
    url: '/api/TaktNumberingRules',
    method: 'post',
    data
  })
}

/**
 * 更新编号规则
 * @param data 更新参数
 * @returns 是否成功
 */
export const updateNumberRule = (data: TaktNumberRuleUpdate) => {
  return request<boolean>({
    url: '/api/TaktNumberingRules',
    method: 'put',
    data
  })
}

/**
 * 删除编号规则
 * @param numberRuleId 编号规则ID
 * @returns 是否成功
 */
export const deleteNumberRule = (numberRuleId: string) => {
  return request<boolean>({
    url: `/api/TaktNumberingRules/${numberRuleId}`,
    method: 'delete'
  })
}

/**
 * 批量删除编号规则
 * @param numberRuleIds 编号规则ID列表
 * @returns 是否成功
 */
export const batchDeleteNumberRule = (numberRuleIds: string[]) => {
  return request<boolean>({
    url: '/api/TaktNumberingRules/batch',
    method: 'delete',
    data: numberRuleIds
  })
}

/**
 * 更新编号规则状态
 * @param data 状态更新参数
 * @returns 是否成功
 */
export const updateNumberRuleStatus = (data: TaktNumberRuleStatusUpdate) => {
  return request<boolean>({
    url: '/api/TaktNumberingRules/status',
    method: 'put',
    data
  })
}

/**
 * 生成编号
 * @param ruleCode 规则代码
 * @returns 生成的编号
 */
export const generateNumber = (ruleCode: string) => {
  return request<string>({
    url: '/api/TaktNumberingRules/generate',
    method: 'get',
    params: { ruleCode }
  })
}

/**
 * 验证编号规则
 * @param data 验证参数
 * @returns 验证结果
 */
export const validateNumberRule = (data: TaktNumberRuleCreate) => {
  return request<boolean>({
    url: '/api/TaktNumberingRules/validate',
    method: 'post',
    data
  })
}

/**
 * 导入编号规则数据
 * @param file 文件
 * @returns 导入结果
 */
export const importNumberRule = (file: File) => {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ code: number; msg: string; data: { success: number; fail: number } }>({
    url: '/api/TaktNumberingRules/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出编号规则数据
 * @param query 查询参数
 * @returns 导出文件
 */
export const exportNumberRule = (query: TaktNumberRuleQuery) => {
  return request<Blob>({
    url: '/api/TaktNumberingRules/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取编号规则导入模板
 * @returns 模板文件
 */
export const getNumberRuleTemplate = () => {
  return request<Blob>({
    url: '/api/TaktNumberingRules/template',
    method: 'get',
    responseType: 'blob'
  })
}
