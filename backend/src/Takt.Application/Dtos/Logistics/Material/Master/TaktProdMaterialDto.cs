//===================================================================
// 项目名: Takt.Application
// 文件名: TaktProdMaterialDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 生产级物料主数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Material.Master
{
    /// <summary>
    /// 生产级物料主数据基础DTO
    /// </summary>
    public class TaktProdMaterialDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            PriceUnit = string.Empty;
            ValuationClass = string.Empty;
            ProfitCenter = string.Empty;
            StorageBin = string.Empty;
            PurchaseGroup = string.Empty;
            ExternalProcurementStorageLocation = string.Empty;
            IndustrySector = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            MaterialType = string.Empty;
            ProductHierarchy = string.Empty;
            MaterialGroup = string.Empty;
            SpecialProcurementType = string.Empty;
            VarianceCode = string.Empty;
            ManufacturerPartNumber = string.Empty;
            Manufacturer = string.Empty;
            Currency = string.Empty;
            PriceControl = string.Empty;
            ProductionStorageLocation = string.Empty;
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long MaterialId { get; set; }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 移动平均价
        /// </summary>
        public decimal MovingAveragePrice { get; set; } = 0;

        /// <summary>
        /// 价格单位
        /// </summary>
        public string? PriceUnit { get; set; }

        /// <summary>
        /// 评估类
        /// </summary>
        public string? ValuationClass { get; set; }

        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }

        /// <summary>
        /// 最小起订量
        /// </summary>
        public decimal MinimumOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 舍入值
        /// </summary>
        public decimal RoundingValue { get; set; } = 0;

        /// <summary>
        /// 计划交货时间
        /// </summary>
        public decimal PlannedDeliveryTime { get; set; } = 0;

        /// <summary>
        /// 仓位
        /// </summary>
        public string? StorageBin { get; set; }

        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 外部采购仓储地点
        /// </summary>
        public string? ExternalProcurementStorageLocation { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 产品层次
        /// </summary>
        public string? ProductHierarchy { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 采购类型
        /// </summary>
        public int? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类
        /// </summary>
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 散装物料
        /// </summary>
        public int IsBulkMaterial { get; set; } = 0;

        /// <summary>
        /// 自制生产天数
        /// </summary>
        public decimal InHouseProductionDays { get; set; } = 0;

        /// <summary>
        /// 过账到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } = 0;

        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceCode { get; set; }

        /// <summary>
        /// 批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 制造商零件编号
        /// </summary>
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// 价格控制
        /// </summary>
        public string? PriceControl { get; set; }

        /// <summary>
        /// 生产仓储地点
        /// </summary>
        public string? ProductionStorageLocation { get; set; }

        /// <summary>
        /// 跨工厂物料状态
        /// </summary>
        public int CrossPlantMaterialStatus { get; set; } = 1;

        /// <summary>
        /// 库存
        /// </summary>
        public decimal InventoryQuantity { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 生产级物料主数据查询DTO
    /// </summary>
    public class TaktProdMaterialQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialQueryDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            MaterialGroup = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 生产级物料主数据创建DTO
    /// </summary>
    public class TaktProdMaterialCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialCreateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            PriceUnit = string.Empty;
            ValuationClass = string.Empty;
            ProfitCenter = string.Empty;
            StorageBin = string.Empty;
            PurchaseGroup = string.Empty;
            ExternalProcurementStorageLocation = string.Empty;
            IndustrySector = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            MaterialType = string.Empty;
            ProductHierarchy = string.Empty;
            MaterialGroup = string.Empty;
            SpecialProcurementType = string.Empty;
            VarianceCode = string.Empty;
            ManufacturerPartNumber = string.Empty;
            Manufacturer = string.Empty;
            Currency = string.Empty;
            PriceControl = string.Empty;
            ProductionStorageLocation = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 移动平均价
        /// </summary>
        public decimal MovingAveragePrice { get; set; } = 0;

        /// <summary>
        /// 价格单位
        /// </summary>
        public string? PriceUnit { get; set; }

        /// <summary>
        /// 评估类
        /// </summary>
        public string? ValuationClass { get; set; }

        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }

        /// <summary>
        /// 最小起订量
        /// </summary>
        public decimal MinimumOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 舍入值
        /// </summary>
        public decimal RoundingValue { get; set; } = 0;

        /// <summary>
        /// 计划交货时间
        /// </summary>
        public decimal PlannedDeliveryTime { get; set; } = 0;

        /// <summary>
        /// 仓位
        /// </summary>
        public string? StorageBin { get; set; }

        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 外部采购仓储地点
        /// </summary>
        public string? ExternalProcurementStorageLocation { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 产品层次
        /// </summary>
        public string? ProductHierarchy { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 采购类型
        /// </summary>
        public int? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类
        /// </summary>
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 散装物料
        /// </summary>
        public int IsBulkMaterial { get; set; } = 0;

        /// <summary>
        /// 自制生产天数
        /// </summary>
        public decimal InHouseProductionDays { get; set; } = 0;

        /// <summary>
        /// 过账到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } = 0;

        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceCode { get; set; }

        /// <summary>
        /// 批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 制造商零件编号
        /// </summary>
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// 价格控制
        /// </summary>
        public string? PriceControl { get; set; }

        /// <summary>
        /// 生产仓储地点
        /// </summary>
        public string? ProductionStorageLocation { get; set; }

        /// <summary>
        /// 跨工厂物料状态
        /// </summary>
        public int CrossPlantMaterialStatus { get; set; } = 1;

        /// <summary>
        /// 库存
        /// </summary>
        public decimal InventoryQuantity { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 生产级物料主数据更新DTO
    /// </summary>
    public class TaktProdMaterialUpdateDto : TaktProdMaterialCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long MaterialId { get; set; }
    }

    /// <summary>
    /// 生产级物料主数据导入DTO
    /// </summary>
    public class TaktProdMaterialImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialImportDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            PriceUnit = string.Empty;
            ValuationClass = string.Empty;
            ProfitCenter = string.Empty;
            StorageBin = string.Empty;
            PurchaseGroup = string.Empty;
            ExternalProcurementStorageLocation = string.Empty;
            IndustrySector = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            MaterialType = string.Empty;
            ProductHierarchy = string.Empty;
            MaterialGroup = string.Empty;
            SpecialProcurementType = string.Empty;
            VarianceCode = string.Empty;
            ManufacturerPartNumber = string.Empty;
            Manufacturer = string.Empty;
            Currency = string.Empty;
            PriceControl = string.Empty;
            ProductionStorageLocation = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 移动平均价
        /// </summary>
        public decimal MovingAveragePrice { get; set; } = 0;

        /// <summary>
        /// 价格单位
        /// </summary>
        public string? PriceUnit { get; set; }

        /// <summary>
        /// 评估类
        /// </summary>
        public string? ValuationClass { get; set; }

        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }

        /// <summary>
        /// 最小起订量
        /// </summary>
        public decimal MinimumOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 舍入值
        /// </summary>
        public decimal RoundingValue { get; set; } = 0;

        /// <summary>
        /// 计划交货时间
        /// </summary>
        public decimal PlannedDeliveryTime { get; set; } = 0;

        /// <summary>
        /// 仓位
        /// </summary>
        public string? StorageBin { get; set; }

        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 外部采购仓储地点
        /// </summary>
        public string? ExternalProcurementStorageLocation { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 产品层次
        /// </summary>
        public string? ProductHierarchy { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 采购类型
        /// </summary>
        public int? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类
        /// </summary>
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 散装物料
        /// </summary>
        public int IsBulkMaterial { get; set; } = 0;

        /// <summary>
        /// 自制生产天数
        /// </summary>
        public decimal InHouseProductionDays { get; set; } = 0;

        /// <summary>
        /// 过账到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } = 0;

        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceCode { get; set; }

        /// <summary>
        /// 批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 制造商零件编号
        /// </summary>
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// 价格控制
        /// </summary>
        public string? PriceControl { get; set; }

        /// <summary>
        /// 生产仓储地点
        /// </summary>
        public string? ProductionStorageLocation { get; set; }

        /// <summary>
        /// 跨工厂物料状态
        /// </summary>
        public int CrossPlantMaterialStatus { get; set; } = 1;

        /// <summary>
        /// 库存
        /// </summary>
        public decimal InventoryQuantity { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 生产级物料主数据导出DTO
    /// </summary>
    public class TaktProdMaterialExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialExportDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            MaterialType = string.Empty;
            MaterialGroup = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            Status = 1;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 1;
    }

    /// <summary>
    /// 生产级物料主数据模板DTO
    /// </summary>
    public class TaktProdMaterialTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialTemplateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            MaterialDescription = string.Empty;
            PriceUnit = string.Empty;
            ValuationClass = string.Empty;
            ProfitCenter = string.Empty;
            StorageBin = string.Empty;
            PurchaseGroup = string.Empty;
            ExternalProcurementStorageLocation = string.Empty;
            IndustrySector = string.Empty;
            BaseUnitOfMeasure = string.Empty;
            MaterialType = string.Empty;
            ProductHierarchy = string.Empty;
            MaterialGroup = string.Empty;
            SpecialProcurementType = string.Empty;
            VarianceCode = string.Empty;
            ManufacturerPartNumber = string.Empty;
            Manufacturer = string.Empty;
            Currency = string.Empty;
            PriceControl = string.Empty;
            ProductionStorageLocation = string.Empty;
        }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 移动平均价
        /// </summary>
        public decimal MovingAveragePrice { get; set; } = 0;

        /// <summary>
        /// 价格单位
        /// </summary>
        public string? PriceUnit { get; set; }

        /// <summary>
        /// 评估类
        /// </summary>
        public string? ValuationClass { get; set; }

        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }

        /// <summary>
        /// 最小起订量
        /// </summary>
        public decimal MinimumOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 舍入值
        /// </summary>
        public decimal RoundingValue { get; set; } = 0;

        /// <summary>
        /// 计划交货时间
        /// </summary>
        public decimal PlannedDeliveryTime { get; set; } = 0;

        /// <summary>
        /// 仓位
        /// </summary>
        public string? StorageBin { get; set; }

        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 外部采购仓储地点
        /// </summary>
        public string? ExternalProcurementStorageLocation { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 基本计量单位
        /// </summary>
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public string? MaterialType { get; set; }

        /// <summary>
        /// 产品层次
        /// </summary>
        public string? ProductHierarchy { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 采购类型
        /// </summary>
        public int? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类
        /// </summary>
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 散装物料
        /// </summary>
        public int IsBulkMaterial { get; set; } = 0;

        /// <summary>
        /// 自制生产天数
        /// </summary>
        public decimal InHouseProductionDays { get; set; } = 0;

        /// <summary>
        /// 过账到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } = 0;

        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceCode { get; set; }

        /// <summary>
        /// 批次管理
        /// </summary>
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 制造商零件编号
        /// </summary>
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 货币
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// 价格控制
        /// </summary>
        public string? PriceControl { get; set; }

        /// <summary>
        /// 生产仓储地点
        /// </summary>
        public string? ProductionStorageLocation { get; set; }

        /// <summary>
        /// 跨工厂物料状态
        /// </summary>
        public int CrossPlantMaterialStatus { get; set; } = 1;

        /// <summary>
        /// 库存
        /// </summary>
        public decimal InventoryQuantity { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 生产级物料主数据状态更新DTO
    /// </summary>
    public class TaktProdMaterialStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdMaterialStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long MaterialId { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}


