//===================================================================
// 项目名: Takt.Frontend
// 文件名: prodMaterial.d.ts
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 生产物料管理类型定义
//===================================================================

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult, TaktSelectOption } from '@/types/common'

/**
 * 生产物料实体
 */
export interface TaktProdMaterial extends TaktBaseEntity {
  /** 物料ID */
  materialId: number
  /** 工厂编码 */
  plantCode: string
  /** 物料编码 */
  materialCode: string
  /** 物料描述 */
  materialDescription?: string
  /** 移动平均价 */
  movingAveragePrice: number
  /** 价格单位 */
  priceUnit?: string
  /** 评估类 */
  valuationClass?: string
  /** 利润中心 */
  profitCenter?: string
  /** 最小起订量 */
  minimumOrderQuantity: number
  /** 舍入值 */
  roundingValue: number
  /** 计划交货时间 */
  plannedDeliveryTime: number
  /** 仓位 */
  storageBin?: string
  /** 采购组 */
  purchaseGroup?: string
  /** 外部采购仓储地点 */
  externalProcurementStorageLocation?: string
  /** 行业领域 */
  industrySector?: string
  /** 基本计量单位 */
  baseUnitOfMeasure?: string
  /** 物料类型 */
  materialType?: string
  /** 产品层次 */
  productHierarchy?: string
  /** 物料组 */
  materialGroup?: string
  /** 特殊采购类型 */
  specialProcurementType?: string
  /** 差异代码 */
  varianceCode?: string
  /** 制造商零件号 */
  manufacturerPartNumber?: string
  /** 制造商 */
  manufacturer?: string
  /** 货币 */
  currency?: string
  /** 价格控制 */
  priceControl?: string
  /** 生产仓储地点 */
  productionStorageLocation?: string
  /** 跨工厂物料状态（0=停用 1=正常） */
  crossPlantMaterialStatus: number
  /** 库存数量 */
  inventoryQuantity: number
  /** 状态（0=停用 1=正常） */
  status: number
}

/**
 * 生产物料查询参数
 */
export interface TaktProdMaterialQuery extends TaktPagedQuery {
  /** 工厂编码 */
  plantCode?: string
  /** 物料编码 */
  materialCode?: string
  /** 物料描述 */
  materialDescription?: string
  /** 物料类型 */
  materialType?: string
  /** 物料组 */
  materialGroup?: string
  /** 状态（0=停用 1=正常） */
  status?: number
}

/**
 * 生产物料创建参数
 */
export interface TaktProdMaterialCreate {
  /** 工厂编码 */
  plantCode: string
  /** 物料编码 */
  materialCode: string
  /** 物料描述 */
  materialDescription?: string
  /** 移动平均价 */
  movingAveragePrice: number
  /** 价格单位 */
  priceUnit?: string
  /** 评估类 */
  valuationClass?: string
  /** 利润中心 */
  profitCenter?: string
  /** 最小起订量 */
  minimumOrderQuantity: number
  /** 舍入值 */
  roundingValue: number
  /** 计划交货时间 */
  plannedDeliveryTime: number
  /** 仓位 */
  storageBin?: string
  /** 采购组 */
  purchaseGroup?: string
  /** 外部采购仓储地点 */
  externalProcurementStorageLocation?: string
  /** 行业领域 */
  industrySector?: string
  /** 基本计量单位 */
  baseUnitOfMeasure?: string
  /** 物料类型 */
  materialType?: string
  /** 产品层次 */
  productHierarchy?: string
  /** 物料组 */
  materialGroup?: string
  /** 特殊采购类型 */
  specialProcurementType?: string
  /** 差异代码 */
  varianceCode?: string
  /** 制造商零件号 */
  manufacturerPartNumber?: string
  /** 制造商 */
  manufacturer?: string
  /** 货币 */
  currency?: string
  /** 价格控制 */
  priceControl?: string
  /** 生产仓储地点 */
  productionStorageLocation?: string
  /** 跨工厂物料状态（0=停用 1=正常） */
  crossPlantMaterialStatus: number
  /** 库存数量 */
  inventoryQuantity: number
  /** 状态（0=停用 1=正常） */
  status: number
  /** 备注 */
  remark: string
}

/**
 * 生产物料更新参数
 */
export interface TaktProdMaterialUpdate extends TaktProdMaterialCreate {
  /** 物料ID */
  materialId: number
}

/**
 * 生产物料状态更新参数
 */
export interface TaktProdMaterialStatus {
  /** 物料ID */
  materialId: number
  /** 状态（0=停用 1=正常） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 生产物料导入参数
 */
export interface TaktProdMaterialImport {
  /** 工厂编码 */
  plantCode: string
  /** 物料编码 */
  materialCode: string
  /** 物料描述 */
  materialDescription: string
  /** 移动平均价 */
  movingAveragePrice: number
  /** 价格单位 */
  priceUnit: string
  /** 评估类 */
  valuationClass: string
  /** 利润中心 */
  profitCenter: string
  /** 最小起订量 */
  minimumOrderQuantity: number
  /** 舍入值 */
  roundingValue: number
  /** 计划交货时间 */
  plannedDeliveryTime: number
  /** 仓位 */
  storageBin: string
  /** 采购组 */
  purchaseGroup: string
  /** 外部采购仓储地点 */
  externalProcurementStorageLocation: string
  /** 行业领域 */
  industrySector: string
  /** 基本计量单位 */
  baseUnitOfMeasure: string
  /** 物料类型 */
  materialType: string
  /** 产品层次 */
  productHierarchy: string
  /** 物料组 */
  materialGroup: string
  /** 特殊采购类型 */
  specialProcurementType: string
  /** 差异代码 */
  varianceCode: string
  /** 制造商零件号 */
  manufacturerPartNumber: string
  /** 制造商 */
  manufacturer: string
  /** 货币 */
  currency: string
  /** 价格控制 */
  priceControl: string
  /** 生产仓储地点 */
  productionStorageLocation: string
  /** 跨工厂物料状态（0=停用 1=正常） */
  crossPlantMaterialStatus: number
  /** 库存数量 */
  inventoryQuantity: number
  /** 状态（0=停用 1=正常） */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 生产物料导出参数
 */
export interface TaktProdMaterialExport {
  /** 工厂编码 */
  plantCode: string
  /** 物料编码 */
  materialCode: string
  /** 物料描述 */
  materialDescription: string
  /** 移动平均价 */
  movingAveragePrice: number
  /** 价格单位 */
  priceUnit: string
  /** 评估类 */
  valuationClass: string
  /** 利润中心 */
  profitCenter: string
  /** 最小起订量 */
  minimumOrderQuantity: number
  /** 舍入值 */
  roundingValue: number
  /** 计划交货时间 */
  plannedDeliveryTime: number
  /** 仓位 */
  storageBin: string
  /** 采购组 */
  purchaseGroup: string
  /** 外部采购仓储地点 */
  externalProcurementStorageLocation: string
  /** 行业领域 */
  industrySector: string
  /** 基本计量单位 */
  baseUnitOfMeasure: string
  /** 物料类型 */
  materialType: string
  /** 产品层次 */
  productHierarchy: string
  /** 物料组 */
  materialGroup: string
  /** 特殊采购类型 */
  specialProcurementType: string
  /** 差异代码 */
  varianceCode: string
  /** 制造商零件号 */
  manufacturerPartNumber: string
  /** 制造商 */
  manufacturer: string
  /** 货币 */
  currency: string
  /** 价格控制 */
  priceControl: string
  /** 生产仓储地点 */
  productionStorageLocation: string
  /** 跨工厂物料状态 */
  crossPlantMaterialStatus: string
  /** 库存数量 */
  inventoryQuantity: number
  /** 状态 */
  status: number
  /** 创建时间 */
  createTime: string
}

/**
 * 生产物料模板参数
 */
export interface TaktProdMaterialTemplate {
  /** 工厂编码 */
  plantCode: string
  /** 物料编码 */
  materialCode: string
  /** 物料描述 */
  materialDescription: string
  /** 移动平均价 */
  movingAveragePrice: number
  /** 价格单位 */
  priceUnit: string
  /** 评估类 */
  valuationClass: string
  /** 利润中心 */
  profitCenter: string
  /** 最小起订量 */
  minimumOrderQuantity: number
  /** 舍入值 */
  roundingValue: number
  /** 计划交货时间 */
  plannedDeliveryTime: number
  /** 仓位 */
  storageBin: string
  /** 采购组 */
  purchaseGroup: string
  /** 外部采购仓储地点 */
  externalProcurementStorageLocation: string
  /** 行业领域 */
  industrySector: string
  /** 基本计量单位 */
  baseUnitOfMeasure: string
  /** 物料类型 */
  materialType: string
  /** 产品层次 */
  productHierarchy: string
  /** 物料组 */
  materialGroup: string
  /** 特殊采购类型 */
  specialProcurementType: string
  /** 差异代码 */
  varianceCode: string
  /** 制造商零件号 */
  manufacturerPartNumber: string
  /** 制造商 */
  manufacturer: string
  /** 货币 */
  currency: string
  /** 价格控制 */
  priceControl: string
  /** 生产仓储地点 */
  productionStorageLocation: string
  /** 跨工厂物料状态（0=停用 1=正常） */
  crossPlantMaterialStatus: number
  /** 库存数量 */
  inventoryQuantity: number
  /** 状态（0=停用 1=正常） */
  status: number
  /** 备注 */
  remark: string
}

/**
 * 生产物料分页结果
 */
export interface TaktProdMaterialPagedResult extends TaktPagedResult<TaktProdMaterial> {}

