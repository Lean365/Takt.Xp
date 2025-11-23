//===================================================================
// 项目名: Takt.Frontend
// 文件名: plant.d.ts
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工厂管理类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult, TaktSelectOption } from '@/types/common'

/**
 * 工厂实体
 */
export interface TaktPlant extends TaktBaseEntity {
  /** 工厂ID */
  plantId: string
  /** 工厂编码 */
  plantCode: string
  /** 工厂名称 */
  plantName: string
  /** 工厂简称 */
  plantShortName?: string
  /** 工厂类型(1=生产工厂 2=销售工厂 3=采购工厂 4=物流中心 5=其他) */
  plantType: number
  /** 工厂类型名称 */
  plantTypeName?: string
  /** 工厂地址 */
  plantAddress?: string
  /** 工厂地址1 */
  plantAddress1?: string
  /** 工厂地址2 */
  plantAddress2?: string
  /** 工厂地址3 */
  plantAddress3?: string
  /** 邮政编码 */
  postalCode?: string
  /** 城市 */
  city?: string
  /** 省份 */
  province?: string
  /** 国家 */
  country?: string
  /** 联系电话 */
  phone?: string
  /** 传真 */
  fax?: string
  /** 邮箱 */
  email?: string
  /** 联系人 */
  contactPerson?: string
  /** 采购组织 */
  purchaseOrg?: string
  /** 销售组织 */
  salesOrg?: string
  /** 分销渠道 */
  distributionChannel?: string
  /** 产品组 */
  productGroup?: string
  /** 工厂日历 */
  factoryCalendar?: string
  /** 时区 */
  timeZone?: string
  /** 语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他) */
  languageCode: number
  /** 货币代码(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他) */
  currencyCode: number
  /** 是否启用批次管理(0=否 1=是) */
  isBatchManaged: number
  /** 是否启用序列号管理(0=否 1=是) */
  isSerialManaged: number
  /** 是否启用质量管理(0=否 1=是) */
  isQualityManaged: number
  /** 是否启用成本管理(0=否 1=是) */
  isCostManaged: number
  /** 是否启用库存管理(0=否 1=是) */
  isInventoryManaged: number
  /** 是否启用生产管理(0=否 1=是) */
  isProductionManaged: number
  /** 是否启用采购管理(0=否 1=是) */
  isPurchaseManaged: number
  /** 是否启用销售管理(0=否 1=是) */
  isSalesManaged: number
  /** 工厂启用日期 */
  plantStartDate?: string
  /** 工厂停用日期 */
  plantEndDate?: string
  /** 工厂描述 */
  plantDescription?: string
  /** 排序号 */
  orderNum: number
  /** 状态(0=停用 1=正常) */
  status: number
}

/**
 * 工厂查询条件
 */
export interface TaktPlantQuery extends TaktPagedQuery {
  /** 工厂编码 */
  plantCode?: string
  /** 工厂名称 */
  plantName?: string
  /** 工厂简称 */
  plantShortName?: string
  /** 工厂类型 */
  plantType?: number
  /** 工厂类型名称 */
  plantTypeName?: string
  /** 城市 */
  city?: string
  /** 省份 */
  province?: string
  /** 国家 */
  country?: string
  /** 状态 */
  status?: number
}

/**
 * 工厂创建请求
 */
export interface TaktPlantCreate {
  /** 工厂编码 */
  plantCode: string
  /** 工厂名称 */
  plantName: string
  /** 工厂简称 */
  plantShortName?: string
  /** 工厂类型 */
  plantType: number
  /** 工厂类型名称 */
  plantTypeName?: string
  /** 工厂地址 */
  plantAddress?: string
  /** 工厂地址1 */
  plantAddress1?: string
  /** 工厂地址2 */
  plantAddress2?: string
  /** 工厂地址3 */
  plantAddress3?: string
  /** 邮政编码 */
  postalCode?: string
  /** 城市 */
  city?: string
  /** 省份 */
  province?: string
  /** 国家 */
  country?: string
  /** 联系电话 */
  phone?: string
  /** 传真 */
  fax?: string
  /** 邮箱 */
  email?: string
  /** 联系人 */
  contactPerson?: string
  /** 采购组织 */
  purchaseOrg?: string
  /** 销售组织 */
  salesOrg?: string
  /** 分销渠道 */
  distributionChannel?: string
  /** 产品组 */
  productGroup?: string
  /** 工厂日历 */
  factoryCalendar?: string
  /** 时区 */
  timeZone?: string
  /** 语言代码 */
  languageCode: number
  /** 货币代码 */
  currencyCode: number
  /** 是否启用批次管理(0=否 1=是) */
  isBatchManaged: number
  /** 是否启用序列号管理(0=否 1=是) */
  isSerialManaged: number
  /** 是否启用质量管理(0=否 1=是) */
  isQualityManaged: number
  /** 是否启用成本管理(0=否 1=是) */
  isCostManaged: number
  /** 是否启用库存管理(0=否 1=是) */
  isInventoryManaged: number
  /** 是否启用生产管理(0=否 1=是) */
  isProductionManaged: number
  /** 是否启用采购管理(0=否 1=是) */
  isPurchaseManaged: number
  /** 是否启用销售管理(0=否 1=是) */
  isSalesManaged: number
  /** 工厂启用日期 */
  plantStartDate?: string
  /** 工厂停用日期 */
  plantEndDate?: string
  /** 工厂描述 */
  plantDescription?: string
  /** 排序号 */
  orderNum: number
  /** 状态 */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工厂更新请求
 */
export interface TaktPlantUpdate extends TaktPlantCreate {
  /** 工厂ID */
  plantId: string
}

/**
 * 工厂状态更新请求
 */
export interface TaktPlantStatus {
  /** 工厂ID */
  plantId: string
  /** 状态 */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 工厂分页结果
 */
export interface TaktPlantPageResult extends TaktPagedResult<TaktPlant> {}

