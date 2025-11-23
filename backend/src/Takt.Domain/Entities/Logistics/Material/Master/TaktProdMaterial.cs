#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMpMaterial.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 生产级物料主数据实体类 (基于SAP MM物料管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Material.Master
{
    /// <summary>
    /// 生产级物料主数据实体类 (基于SAP MM物料管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_material_mp", "生产物料")]
    [SugarIndex("ix_mp_material", nameof(MaterialCode), OrderByType.Asc, true)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    public class TaktProdMaterial : TaktBaseEntity
    {
        /// <summary>
        /// 工厂编码
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂编码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        [SugarColumn(ColumnName = "material_description", ColumnDescription = "物料描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 移动平均价
        /// </summary>
        [SugarColumn(ColumnName = "moving_average_price", ColumnDescription = "移动平均价", ColumnDataType = "decimal(18,4)", IsNullable = false, DefaultValue = "0")]
        public decimal MovingAveragePrice { get; set; } = 0;

        /// <summary>
        /// 价格单位
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "price_unit", ColumnDescription = "价格单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PriceUnit { get; set; }

        /// <summary>
        /// 评估类
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "valuation_class", ColumnDescription = "评估类", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ValuationClass { get; set; }

        /// <summary>
        /// 利润中心
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "profit_center", ColumnDescription = "利润中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenter { get; set; }

        /// <summary>
        /// 最小起订量
        /// </summary>
        [SugarColumn(ColumnName = "minimum_order_quantity", ColumnDescription = "最小起订量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinimumOrderQuantity { get; set; } = 0;

        /// <summary>
        /// 舍入值
        /// </summary>
        [SugarColumn(ColumnName = "rounding_value", ColumnDescription = "舍入值", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal RoundingValue { get; set; } = 0;

        /// <summary>
        /// 计划交货时间
        /// </summary>
        [SugarColumn(ColumnName = "planned_delivery_time", ColumnDescription = "计划交货时间", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedDeliveryTime { get; set; } = 0;

        /// <summary>
        /// 仓位
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "storage_bin", ColumnDescription = "仓位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StorageBin { get; set; }

        /// <summary>
        /// 采购组
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "purchase_group", ColumnDescription = "采购组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseGroup { get; set; }

        /// <summary>
        /// 外部采购仓储地点
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "external_procurement_storage_location", ColumnDescription = "外部采购仓储地点", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExternalProcurementStorageLocation { get; set; }

        /// <summary>
        /// 行业领域
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "industry_sector", ColumnDescription = "行业领域", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 基本计量单位
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "base_unit_of_measure", ColumnDescription = "基本计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 物料类型
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "material_type", ColumnDescription = "物料类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialType { get; set; }

        /// <summary>
        /// 产品层次
        /// </summary>
        [SugarColumn(ColumnName = "product_hierarchy", ColumnDescription = "产品层次", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductHierarchy { get; set; }

        /// <summary>
        /// 物料组
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "material_group", ColumnDescription = "物料组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 采购类型
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "procurement_type", ColumnDescription = "采购类型", ColumnDataType = "int", IsNullable = true)]
        public int? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "special_procurement_type", ColumnDescription = "特殊采购类", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 散装物料
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_bulk_material", ColumnDescription = "散装物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsBulkMaterial { get; set; } = 0;

        /// <summary>
        /// 自制生产天数
        /// </summary>
        [SugarColumn(ColumnName = "in_house_production_days", ColumnDescription = "自制生产天数", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal InHouseProductionDays { get; set; } = 0;

        /// <summary>
        /// 过账到检验库存
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "post_to_inspection_stock", ColumnDescription = "过账到检验库存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PostToInspectionStock { get; set; } = 0;

        /// <summary>
        /// 差异码
        /// </summary>
        [SugarColumn(ColumnName = "variance_code", ColumnDescription = "差异码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VarianceCode { get; set; }

        /// <summary>
        /// 批次管理
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "is_batch_managed", ColumnDescription = "批次管理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsBatchManaged { get; set; } = 0;

        /// <summary>
        /// 制造商零件编号
        /// </summary>
        [SugarColumn(ColumnName = "manufacturer_part_number", ColumnDescription = "制造商零件编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "制造商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 货币
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "货币", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Currency { get; set; }

        /// <summary>
        /// 价格控制
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "price_control", ColumnDescription = "价格控制", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PriceControl { get; set; }

        /// <summary>
        /// 生产仓储地点
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "production_storage_location", ColumnDescription = "生产仓储地点", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionStorageLocation { get; set; }

        /// <summary>
        /// 跨工厂物料状态
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "cross_plant_material_status", ColumnDescription = "跨工厂物料状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CrossPlantMaterialStatus { get; set; } = 1;

        /// <summary>
        /// 库存
        /// </summary>
        [SugarColumn(ColumnName = "inventory_quantity", ColumnDescription = "库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal InventoryQuantity { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// 需要使用字典
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }

    }
}




