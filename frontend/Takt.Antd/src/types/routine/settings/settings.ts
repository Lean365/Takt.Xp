//===================================================================
// 项目名 : Lean.Takt
// 文件名 : generalSettings.d.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 通用设置类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 系统设置实体
 */
export interface TaktGeneralSettings extends TaktBaseEntity {
  /** 设置ID */
  settingsId: string
  /** 设置名称 */
  settingsName: string
  /** 设置键名 */
  settingsKey: string
  /** 设置键值 */
  settingsValue: string
  /** 设置类别（0是前端，1是后端） */
  settingsType: number
  /** 系统内置（0否 1是） */
  isBuiltin: number
  /** 排序号 */
  orderNum: number

  /** 状态（0正常 1停用） */
  status: number
  /** 是否加密（0否 1是） */
  isEncrypted: number
}

/**
 * 系统设置查询参数
 */
export interface TaktGeneralSettingsQuery extends TaktPagedQuery {
  /** 设置名称 */
  settingsName?: string
  /** 设置键名 */
  settingsKey?: string
  /** 设置键值 */
  settingsValue?: string
  /** 设置类别（0是前端，1是后端） */
  settingsType?: number
  /** 系统内置（0否 1是） */
  isBuiltin: number
  /** 状态（0正常 1停用） */
  status: number
  /** 是否加密（0否 1是） */
  isEncrypted: number
}

/**
 * 系统设置创建参数
 */
export interface TaktGeneralSettingsCreate {
  /** 设置名称 */
  settingsName: string
  /** 设置键名 */
  settingsKey: string
  /** 设置键值 */
  settingsValue: string
  /** 设置类别（0是前端，1是后端） */
  settingsType: number
  /** 系统内置 */
  isBuiltin: number
  /** 排序号 */
  orderNum: number
  /** 备注 */
  remark?: string
  /** 状态（0正常 1停用） */
  status: number
  /** 是否加密（0否 1是） */
  isEncrypted: number 
}

/**
 * 系统设置更新参数
 */
export interface TaktGeneralSettingsUpdate extends TaktGeneralSettingsCreate {
  /** 设置ID */
  settingsId: string
}

/**
 * 批量删除系统设置参数
 */
export interface TaktGeneralSettingsBatchDelete {
  /** 设置ID数组 */
  settingsIds: number[]
}

/**
 * 更新系统设置状态参数
 */
export interface TaktGeneralSettingsStatusUpdate {
  /** 设置ID */
  settingsId: string
  /** 状态（0正常 1停用） */
  status: number
}


/**
 * 系统设置项
 */
export interface TaktGeneralSettingsItem {
  /** 设置ID */
  settingsId: string
  /** 设置名称 */
  settingsName: string
  /** 设置键 */
  settingsKey: string
  /** 设置值 */
  settingsValue: string
  /** 设置类别（0是前端，1是后端） */
  settingsType: number
  /** 是否系统设置 */
  isBuiltin: number
  /** 状态（0正常 1停用） */
  status: number
  /** 是否加密（0否 1是） */
  isEncrypted: number
  /** 备注 */
  remark?: string
  /** 创建时间 */
  createTime?: string
  /** 更新时间 */
  updateTime?: string
}

/**
 * 系统设置分页结果
 */
export type TaktGeneralSettingsPageResult = TaktPagedResult<TaktGeneralSettings>


