#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMaterial.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 集团级物料主数据实体类 (基于SAP MM物料管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Material.Master
{
    /// <summary>
    /// 集团级物料主数据实体类 (基于SAP MM物料管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_material_gen", "通用物料")]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, true)]
    [SugarIndex("ix_material_type", nameof(MaterialType), OrderByType.Asc, false)]
    [SugarIndex("ix_material_group", nameof(MaterialGroup), OrderByType.Asc, false)]
    public class TaktGeneralMaterial : TaktBaseEntity
    {
        /// <summary>
        /// 集团代码
        /// </summary>
        [SugarColumn(ColumnName = "group_code", ColumnDescription = "集团代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string GroupCode { get; set; } = string.Empty;

        // ==================== 基础信息 ====================

        /// <summary>
        /// 物料号
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料号", Length = 40, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料描述
        /// </summary>
        [SugarColumn(ColumnName = "material_description", ColumnDescription = "物料描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialDescription { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        [SugarColumn(ColumnName = "material_type", ColumnDescription = "物料类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialType { get; set; }

        /// <summary>
        /// 行业领域
        /// </summary>
        [SugarColumn(ColumnName = "industry_sector", ColumnDescription = "行业领域", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IndustrySector { get; set; }

        /// <summary>
        /// 物料组
        /// </summary>
        [SugarColumn(ColumnName = "material_group", ColumnDescription = "物料组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialGroup { get; set; }

        /// <summary>
        /// 旧物料号
        /// </summary>
        [SugarColumn(ColumnName = "old_material_number", ColumnDescription = "旧物料号", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialNumber { get; set; }

        // ==================== 状态管理 ====================

        /// <summary>
        /// 维护全部物料状态
        /// </summary>
        [SugarColumn(ColumnName = "maintain_all_material_status", ColumnDescription = "维护全部物料状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MaintainAllMaterialStatus { get; set; } = 1;

        /// <summary>
        /// 维护状态
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_status", ColumnDescription = "维护状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MaintenanceStatus { get; set; } = 1;

        /// <summary>
        /// 在客户级标记要删除的物料
        /// </summary>
        [SugarColumn(ColumnName = "marked_for_deletion_at_client_level", ColumnDescription = "在客户级标记要删除的物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MarkedForDeletionAtClientLevel { get; set; } = 0;

        /// <summary>
        /// 跨工厂物料状态
        /// </summary>
        [SugarColumn(ColumnName = "cross_plant_material_status", ColumnDescription = "跨工厂物料状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CrossPlantMaterialStatus { get; set; }

        /// <summary>
        /// 跨分销链物料状态
        /// </summary>
        [SugarColumn(ColumnName = "cross_distribution_chain_material_status", ColumnDescription = "跨分销链物料状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CrossDistributionChainMaterialStatus { get; set; }

        /// <summary>
        /// 从跨工厂物料状态有效起的日期
        /// </summary>
        [SugarColumn(ColumnName = "cross_plant_status_valid_from", ColumnDescription = "从跨工厂物料状态有效起的日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CrossPlantStatusValidFrom { get; set; }

        /// <summary>
        /// 从跨分销链物料状态有效起的日期
        /// </summary>
        [SugarColumn(ColumnName = "cross_distribution_status_valid_from", ColumnDescription = "从跨分销链物料状态有效起的日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CrossDistributionStatusValidFrom { get; set; }

        // ==================== 计量单位 ====================

        /// <summary>
        /// 基本计量单位
        /// </summary>
        [SugarColumn(ColumnName = "base_unit_of_measure", ColumnDescription = "基本计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BaseUnitOfMeasure { get; set; }

        /// <summary>
        /// 采购订单的计量单位
        /// </summary>
        [SugarColumn(ColumnName = "purchase_order_unit_of_measure", ColumnDescription = "采购订单的计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrderUnitOfMeasure { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        [SugarColumn(ColumnName = "weight_unit", ColumnDescription = "重量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WeightUnit { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        [SugarColumn(ColumnName = "volume_unit", ColumnDescription = "体积单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VolumeUnit { get; set; }

        /// <summary>
        /// 长度/宽度/高度的尺寸单位
        /// </summary>
        [SugarColumn(ColumnName = "dimension_unit", ColumnDescription = "长度/宽度/高度的尺寸单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DimensionUnit { get; set; }

        /// <summary>
        /// 加权的单位(允许打包加权)
        /// </summary>
        [SugarColumn(ColumnName = "weighted_unit_for_packing", ColumnDescription = "加权的单位(允许打包加权)", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WeightedUnitForPacking { get; set; }

        /// <summary>
        /// 体积单位(允许打包体积)
        /// </summary>
        [SugarColumn(ColumnName = "volume_unit_for_packing", ColumnDescription = "体积单位(允许打包体积)", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VolumeUnitForPacking { get; set; }

        /// <summary>
        /// 内容单位
        /// </summary>
        [SugarColumn(ColumnName = "content_unit", ColumnDescription = "内容单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContentUnit { get; set; }

        /// <summary>
        /// 比较价格单位
        /// </summary>
        [SugarColumn(ColumnName = "comparison_price_unit", ColumnDescription = "比较价格单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ComparisonPriceUnit { get; set; }

        /// <summary>
        /// 计量单位车辆空间优化
        /// </summary>
        [SugarColumn(ColumnName = "unit_of_measure_vso", ColumnDescription = "计量单位车辆空间优化", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UnitOfMeasureVso { get; set; }

        /// <summary>
        /// 最大包装长/宽/高的计量单位
        /// </summary>
        [SugarColumn(ColumnName = "max_package_dimension_unit", ColumnDescription = "最大包装长/宽/高的计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaxPackageDimensionUnit { get; set; }

        /// <summary>
        /// 隔离期间的时间单位
        /// </summary>
        [SugarColumn(ColumnName = "quarantine_period_unit", ColumnDescription = "隔离期间的时间单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QuarantinePeriodUnit { get; set; }

        /// <summary>
        /// EWM CW: 后勤计量单位
        /// </summary>
        [SugarColumn(ColumnName = "ewm_cw_logistics_unit", ColumnDescription = "EWM CW: 后勤计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EwmCwLogisticsUnit { get; set; }

        // ==================== 文档管理 ====================

        /// <summary>
        /// 文档号码(无文档管理系统)
        /// </summary>
        [SugarColumn(ColumnName = "document_number_no_dms", ColumnDescription = "文档号码(无文档管理系统)", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentNumberNoDms { get; set; }

        /// <summary>
        /// 凭证类型(无凭证管理系统)
        /// </summary>
        [SugarColumn(ColumnName = "document_type_no_dms", ColumnDescription = "凭证类型(无凭证管理系统)", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentTypeNoDms { get; set; }

        /// <summary>
        /// 文档版本（无文档管理系统）
        /// </summary>
        [SugarColumn(ColumnName = "document_version_no_dms", ColumnDescription = "文档版本（无文档管理系统）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentVersionNoDms { get; set; }

        /// <summary>
        /// 文件的页面大小（不包括文件管理系统）
        /// </summary>
        [SugarColumn(ColumnName = "file_page_size_no_dms", ColumnDescription = "文件的页面大小（不包括文件管理系统）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FilePageSizeNoDms { get; set; }

        /// <summary>
        /// 文档变更号(无文档管理系统)
        /// </summary>
        [SugarColumn(ColumnName = "document_change_number_no_dms", ColumnDescription = "文档变更号(无文档管理系统)", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentChangeNumberNoDms { get; set; }

        /// <summary>
        /// 凭证的页号(不带凭证管理系统)
        /// </summary>
        [SugarColumn(ColumnName = "document_page_number_no_dms", ColumnDescription = "凭证的页号(不带凭证管理系统)", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentPageNumberNoDms { get; set; }

        /// <summary>
        /// 页数(没有凭证管理系统)
        /// </summary>
        [SugarColumn(ColumnName = "page_count_no_dms", ColumnDescription = "页数(没有凭证管理系统)", ColumnDataType = "int", IsNullable = true)]
        public int? PageCountNoDms { get; set; }

        // ==================== 生产信息 ====================

        /// <summary>
        /// 生产/检验备忘录
        /// </summary>
        [SugarColumn(ColumnName = "production_inspection_memo", ColumnDescription = "生产/检验备忘录", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionInspectionMemo { get; set; }

        /// <summary>
        /// 生产备忘录的页格式
        /// </summary>
        [SugarColumn(ColumnName = "production_memo_page_format", ColumnDescription = "生产备忘录的页格式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionMemoPageFormat { get; set; }

        /// <summary>
        /// 大小/量纲
        /// </summary>
        [SugarColumn(ColumnName = "size_dimension", ColumnDescription = "大小/量纲", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SizeDimension { get; set; }

        /// <summary>
        /// 基本物料
        /// </summary>
        [SugarColumn(ColumnName = "basic_material", ColumnDescription = "基本物料", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BasicMaterial { get; set; }

        /// <summary>
        /// 行业标准描述（例如 ANSI 或 ISO）
        /// </summary>
        [SugarColumn(ColumnName = "industry_standard_description", ColumnDescription = "行业标准描述（例如 ANSI 或 ISO）", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IndustryStandardDescription { get; set; }

        /// <summary>
        /// 实验室/设计室
        /// </summary>
        [SugarColumn(ColumnName = "laboratory_design_office", ColumnDescription = "实验室/设计室", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LaboratoryDesignOffice { get; set; }

        /// <summary>
        /// 采购价值代码
        /// </summary>
        [SugarColumn(ColumnName = "purchasing_value_code", ColumnDescription = "采购价值代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchasingValueCode { get; set; }

        // ==================== 物理属性 ====================

        /// <summary>
        /// 毛重
        /// </summary>
        [SugarColumn(ColumnName = "gross_weight", ColumnDescription = "毛重", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal GrossWeight { get; set; } = 0;

        /// <summary>
        /// 净重
        /// </summary>
        [SugarColumn(ColumnName = "net_weight", ColumnDescription = "净重", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal NetWeight { get; set; } = 0;

        /// <summary>
        /// 业务量
        /// </summary>
        [SugarColumn(ColumnName = "volume", ColumnDescription = "业务量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Volume { get; set; } = 0;

        /// <summary>
        /// 长度
        /// </summary>
        [SugarColumn(ColumnName = "length", ColumnDescription = "长度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Length { get; set; } = 0;

        /// <summary>
        /// 宽度
        /// </summary>
        [SugarColumn(ColumnName = "width", ColumnDescription = "宽度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Width { get; set; } = 0;

        /// <summary>
        /// 高度
        /// </summary>
        [SugarColumn(ColumnName = "height", ColumnDescription = "高度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal Height { get; set; } = 0;

        /// <summary>
        /// 允许的包装重量
        /// </summary>
        [SugarColumn(ColumnName = "allowed_packaging_weight", ColumnDescription = "允许的包装重量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal AllowedPackagingWeight { get; set; } = 0;

        /// <summary>
        /// 允许的包装体积
        /// </summary>
        [SugarColumn(ColumnName = "allowed_packaging_volume", ColumnDescription = "允许的包装体积", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal AllowedPackagingVolume { get; set; } = 0;

        /// <summary>
        /// 净内容
        /// </summary>
        [SugarColumn(ColumnName = "net_contents", ColumnDescription = "净内容", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal NetContents { get; set; } = 0;

        /// <summary>
        /// 毛内容
        /// </summary>
        [SugarColumn(ColumnName = "gross_contents", ColumnDescription = "毛内容", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal GrossContents { get; set; } = 0;

        /// <summary>
        /// 可变皮重
        /// </summary>
        [SugarColumn(ColumnName = "variable_tare_weight", ColumnDescription = "可变皮重", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal VariableTareWeight { get; set; } = 0;

        /// <summary>
        /// 包装物料的最大允许容量
        /// </summary>
        [SugarColumn(ColumnName = "max_allowed_capacity_packaging_material", ColumnDescription = "包装物料的最大允许容量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxAllowedCapacityPackagingMaterial { get; set; } = 0;

        /// <summary>
        /// 处理单位的最大容量容差
        /// </summary>
        [SugarColumn(ColumnName = "max_capacity_tolerance_handling_unit", ColumnDescription = "处理单位的最大容量容差", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxCapacityToleranceHandlingUnit { get; set; } = 0;

        /// <summary>
        /// 包装物料的最大包长度
        /// </summary>
        [SugarColumn(ColumnName = "max_package_length", ColumnDescription = "包装物料的最大包长度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxPackageLength { get; set; } = 0;

        /// <summary>
        /// 包装物料的最大包宽度
        /// </summary>
        [SugarColumn(ColumnName = "max_package_width", ColumnDescription = "包装物料的最大包宽度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxPackageWidth { get; set; } = 0;

        /// <summary>
        /// 包装物料的最大包高度
        /// </summary>
        [SugarColumn(ColumnName = "max_package_height", ColumnDescription = "包装物料的最大包高度", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxPackageHeight { get; set; } = 0;

        /// <summary>
        /// 处理单位的超重量容差
        /// </summary>
        [SugarColumn(ColumnName = "overweight_tolerance_handling_unit", ColumnDescription = "处理单位的超重量容差", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal OverweightToleranceHandlingUnit { get; set; } = 0;

        /// <summary>
        /// 处理单位的超量冗差
        /// </summary>
        [SugarColumn(ColumnName = "excess_volume_tolerance_handling_unit", ColumnDescription = "处理单位的超量冗差", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ExcessVolumeToleranceHandlingUnit { get; set; } = 0;

        // ==================== 存储条件 ====================

        /// <summary>
        /// 容器需求
        /// </summary>
        [SugarColumn(ColumnName = "container_requirements", ColumnDescription = "容器需求", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContainerRequirements { get; set; }

        /// <summary>
        /// 存储条件
        /// </summary>
        [SugarColumn(ColumnName = "storage_conditions", ColumnDescription = "存储条件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StorageConditions { get; set; }

        /// <summary>
        /// 温度条件标识
        /// </summary>
        [SugarColumn(ColumnName = "temperature_conditions_indicator", ColumnDescription = "温度条件标识", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TemperatureConditionsIndicator { get; set; }

        /// <summary>
        /// 仓库存储条件
        /// </summary>
        [SugarColumn(ColumnName = "warehouse_storage_conditions", ColumnDescription = "仓库存储条件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WarehouseStorageConditions { get; set; }

        // ==================== 产品层次和分类 ====================

        /// <summary>
        /// 低层代码
        /// </summary>
        [SugarColumn(ColumnName = "low_level_code", ColumnDescription = "低层代码", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LowLevelCode { get; set; } = 0;

        /// <summary>
        /// 产品层次
        /// </summary>
        [SugarColumn(ColumnName = "product_hierarchy", ColumnDescription = "产品层次", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductHierarchy { get; set; }

        /// <summary>
        /// 产品组
        /// </summary>
        [SugarColumn(ColumnName = "product_group", ColumnDescription = "产品组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductGroup { get; set; }

        /// <summary>
        /// 竞争者
        /// </summary>
        [SugarColumn(ColumnName = "competitor", ColumnDescription = "竞争者", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Competitor { get; set; }

        /// <summary>
        /// 最大层次 (按体积)
        /// </summary>
        [SugarColumn(ColumnName = "maximum_level_by_volume", ColumnDescription = "最大层次 (按体积)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaximumLevelByVolume { get; set; } = 0;

        /// <summary>
        /// 堆栈因子
        /// </summary>
        [SugarColumn(ColumnName = "stacking_factor", ColumnDescription = "堆栈因子", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal StackingFactor { get; set; } = 0;

        /// <summary>
        /// 物料组: 包装物料
        /// </summary>
        [SugarColumn(ColumnName = "material_group_packaging_materials", ColumnDescription = "物料组: 包装物料", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialGroupPackagingMaterials { get; set; }

        /// <summary>
        /// 权限组
        /// </summary>
        [SugarColumn(ColumnName = "authorization_group", ColumnDescription = "权限组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AuthorizationGroup { get; set; }

        // ==================== 日期管理 ====================

        /// <summary>
        /// 开始生效日期
        /// </summary>
        [SugarColumn(ColumnName = "valid_from_date", ColumnDescription = "开始生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ValidFromDate { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>
        [SugarColumn(ColumnName = "deletion_date", ColumnDescription = "删除日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DeletionDate { get; set; }

        /// <summary>
        /// 季度年
        /// </summary>
        [SugarColumn(ColumnName = "quarter_year", ColumnDescription = "季度年", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QuarterYear { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        [SugarColumn(ColumnName = "expiration_date", ColumnDescription = "过期日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpirationDate { get; set; }

        // ==================== 编码和标识 ====================

        /// <summary>
        /// 运输组
        /// </summary>
        [SugarColumn(ColumnName = "transportation_group", ColumnDescription = "运输组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TransportationGroup { get; set; }

        /// <summary>
        /// 危险物料号
        /// </summary>
        [SugarColumn(ColumnName = "hazardous_material_number", ColumnDescription = "危险物料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HazardousMaterialNumber { get; set; }

        /// <summary>
        /// 欧洲物品号 (EAN) - 已过时的 !!!!!
        /// </summary>
        [SugarColumn(ColumnName = "european_article_number_ean_obsolete", ColumnDescription = "欧洲物品号 (EAN) - 已过时的 !!!!!", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EuropeanArticleNumberEanObsolete { get; set; }

        /// <summary>
        /// 国际文件号(EAN/UPC)
        /// </summary>
        [SugarColumn(ColumnName = "international_article_number_ean_upc", ColumnDescription = "国际文件号(EAN/UPC)", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternationalArticleNumberEanUpc { get; set; }

        /// <summary>
        /// 国际商品编码的类别 (EAN)
        /// </summary>
        [SugarColumn(ColumnName = "ean_category", ColumnDescription = "国际商品编码的类别 (EAN)", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EanCategory { get; set; }

        /// <summary>
        /// 全球贸易项目编号变式
        /// </summary>
        [SugarColumn(ColumnName = "global_trade_item_number_variant", ColumnDescription = "全球贸易项目编号变式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? GlobalTradeItemNumberVariant { get; set; }

        // ==================== 数量管理 ====================

        /// <summary>
        /// 数量: 待打印的 GR/GI 单数量
        /// </summary>
        [SugarColumn(ColumnName = "quantity_to_be_printed", ColumnDescription = "数量: 待打印的 GR/GI 单数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal QuantityToBePrinted { get; set; } = 0;

        /// <summary>
        /// 数量转换方法
        /// </summary>
        [SugarColumn(ColumnName = "quantity_conversion_method", ColumnDescription = "数量转换方法", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QuantityConversionMethod { get; set; }

        // ==================== 采购相关 ====================

        /// <summary>
        /// 采购规则
        /// </summary>
        [SugarColumn(ColumnName = "procurement_rule", ColumnDescription = "采购规则", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcurementRule { get; set; }

        /// <summary>
        /// 货源
        /// </summary>
        [SugarColumn(ColumnName = "source_of_supply", ColumnDescription = "货源", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SourceOfSupply { get; set; }

        /// <summary>
        /// 季节类别
        /// </summary>
        [SugarColumn(ColumnName = "season_category", ColumnDescription = "季节类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SeasonCategory { get; set; }

        // ==================== 标签和包装 ====================

        /// <summary>
        /// 标签类型
        /// </summary>
        [SugarColumn(ColumnName = "label_type", ColumnDescription = "标签类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LabelType { get; set; }

        /// <summary>
        /// 标签格式
        /// </summary>
        [SugarColumn(ColumnName = "label_form", ColumnDescription = "标签格式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LabelForm { get; set; }

        /// <summary>
        /// 包装物料类型
        /// </summary>
        [SugarColumn(ColumnName = "packaging_material_type", ColumnDescription = "包装物料类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PackagingMaterialType { get; set; }

        /// <summary>
        /// Packaging Code
        /// </summary>
        [SugarColumn(ColumnName = "packaging_code", ColumnDescription = "Packaging Code", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PackagingCode { get; set; }

        /// <summary>
        /// 危险品的包装状态
        /// </summary>
        [SugarColumn(ColumnName = "hazardous_goods_packaging_status", ColumnDescription = "危险品的包装状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HazardousGoodsPackagingStatus { get; set; }

        // ==================== 标志和指示符 ====================

        /// <summary>
        /// 取消激活的
        /// </summary>
        [SugarColumn(ColumnName = "deactivated", ColumnDescription = "取消激活的", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Deactivated { get; set; } = 0;

        /// <summary>
        /// 库存转移净改变成本核算
        /// </summary>
        [SugarColumn(ColumnName = "stock_transfer_net_change_costing", ColumnDescription = "库存转移净改变成本核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StockTransferNetChangeCosting { get; set; } = 0;

        /// <summary>
        /// CAD 标识
        /// </summary>
        [SugarColumn(ColumnName = "cad_indicator", ColumnDescription = "CAD 标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CadIndicator { get; set; } = 0;

        /// <summary>
        /// 激活采购中的 QM
        /// </summary>
        [SugarColumn(ColumnName = "qm_in_procurement_is_active", ColumnDescription = "激活采购中的 QM", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int QmInProcurementIsActive { get; set; } = 0;

        /// <summary>
        /// 可变采购订单单位活动
        /// </summary>
        [SugarColumn(ColumnName = "variable_purchase_order_unit_active", ColumnDescription = "可变采购订单单位活动", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int VariablePurchaseOrderUnitActive { get; set; } = 0;

        /// <summary>
        /// 标识: 修改水平已分配到物料
        /// </summary>
        [SugarColumn(ColumnName = "change_level_assigned_to_material", ColumnDescription = "标识: 修改水平已分配到物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ChangeLevelAssignedToMaterial { get; set; } = 0;

        /// <summary>
        /// 可配置的物料
        /// </summary>
        [SugarColumn(ColumnName = "configurable_material", ColumnDescription = "可配置的物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConfigurableMaterial { get; set; } = 0;

        /// <summary>
        /// 批次管理需求的标识
        /// </summary>
        [SugarColumn(ColumnName = "batch_management_requirement_indicator", ColumnDescription = "批次管理需求的标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BatchManagementRequirementIndicator { get; set; } = 0;

        /// <summary>
        /// 空白物料清单
        /// </summary>
        [SugarColumn(ColumnName = "blank_bom", ColumnDescription = "空白物料清单", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BlankBom { get; set; } = 0;

        /// <summary>
        /// 一般可配置物料
        /// </summary>
        [SugarColumn(ColumnName = "general_configurable_material", ColumnDescription = "一般可配置物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int GeneralConfigurableMaterial { get; set; } = 0;

        /// <summary>
        /// 指示符 : 物料是联产品
        /// </summary>
        [SugarColumn(ColumnName = "material_is_co_product", ColumnDescription = "指示符 : 物料是联产品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialIsCoProduct { get; set; } = 0;

        /// <summary>
        /// 标记：物料有一后续物料
        /// </summary>
        [SugarColumn(ColumnName = "material_has_follow_up_material", ColumnDescription = "标记：物料有一后续物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialHasFollowUpMaterial { get; set; } = 0;

        /// <summary>
        /// 环境相关
        /// </summary>
        [SugarColumn(ColumnName = "environmentally_relevant", ColumnDescription = "环境相关", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int EnvironmentallyRelevant { get; set; } = 0;

        /// <summary>
        /// 标识符:高粘性的
        /// </summary>
        [SugarColumn(ColumnName = "highly_viscous_indicator", ColumnDescription = "标识符:高粘性的", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int HighlyViscousIndicator { get; set; } = 0;

        /// <summary>
        /// 标识符:固体/液体
        /// </summary>
        [SugarColumn(ColumnName = "solid_liquid_indicator", ColumnDescription = "标识符:固体/液体", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SolidLiquidIndicator { get; set; } = 0;

        /// <summary>
        /// 包装物料是密闭包装的
        /// </summary>
        [SugarColumn(ColumnName = "packaging_material_is_closed", ColumnDescription = "包装物料是密闭包装的", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PackagingMaterialIsClosed { get; set; } = 0;

        /// <summary>
        /// 指示符：需要批准的批量记录
        /// </summary>
        [SugarColumn(ColumnName = "approved_batch_record_required", ColumnDescription = "指示符：需要批准的批量记录", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovedBatchRecordRequired { get; set; } = 0;

        /// <summary>
        /// 标识：印刷在包装上的产品成份
        /// </summary>
        [SugarColumn(ColumnName = "product_composition_printed_on_packaging", ColumnDescription = "标识：印刷在包装上的产品成份", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProductCompositionPrintedOnPackaging { get; set; } = 0;

        /// <summary>
        /// 含后勤变量的一般物料
        /// </summary>
        [SugarColumn(ColumnName = "generic_material_with_logistics_variants", ColumnDescription = "含后勤变量的一般物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int GenericMaterialWithLogisticsVariants { get; set; } = 0;

        /// <summary>
        /// 物料被锁定
        /// </summary>
        [SugarColumn(ColumnName = "material_is_locked", ColumnDescription = "物料被锁定", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialIsLocked { get; set; } = 0;

        /// <summary>
        /// 与配置管理相关
        /// </summary>
        [SugarColumn(ColumnName = "configuration_management_related", ColumnDescription = "与配置管理相关", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConfigurationManagementRelated { get; set; } = 0;

        /// <summary>
        /// 标识：全局数据同步相关
        /// </summary>
        [SugarColumn(ColumnName = "global_data_sync_relevant", ColumnDescription = "标识：全局数据同步相关", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int GlobalDataSyncRelevant { get; set; } = 0;

        /// <summary>
        /// 原始接受
        /// </summary>
        [SugarColumn(ColumnName = "original_acceptance", ColumnDescription = "原始接受", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OriginalAcceptance { get; set; } = 0;

        /// <summary>
        /// 可偷窃的
        /// </summary>
        [SugarColumn(ColumnName = "pilferable", ColumnDescription = "可偷窃的", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Pilferable { get; set; } = 0;

        /// <summary>
        /// 与危险物料相关
        /// </summary>
        [SugarColumn(ColumnName = "hazardous_material_related", ColumnDescription = "与危险物料相关", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int HazardousMaterialRelated { get; set; } = 0;

        /// <summary>
        /// EWM CW: 物料是称重物料
        /// </summary>
        [SugarColumn(ColumnName = "ewm_cw_material_is_catch_weight", ColumnDescription = "EWM CW: 物料是称重物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int EwmCwMaterialIsCatchWeight { get; set; } = 0;

        /// <summary>
        /// 知识产权标识（CRM 产品）
        /// </summary>
        [SugarColumn(ColumnName = "intellectual_property_indicator", ColumnDescription = "知识产权标识（CRM 产品）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IntellectualPropertyIndicator { get; set; } = 0;

        /// <summary>
        /// 允许的变式价格（针对生产物料主数据）
        /// </summary>
        [SugarColumn(ColumnName = "variant_price_allowed", ColumnDescription = "允许的变式价格（针对生产物料主数据）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int VariantPriceAllowed { get; set; } = 0;

        /// <summary>
        /// 中间
        /// </summary>
        [SugarColumn(ColumnName = "intermediate", ColumnDescription = "中间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Intermediate { get; set; } = 0;

        /// <summary>
        /// 实物商品
        /// </summary>
        [SugarColumn(ColumnName = "physical_goods", ColumnDescription = "实物商品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PhysicalGoods { get; set; } = 0;

        /// <summary>
        /// 标识：包含动物源的非纺织部分
        /// </summary>
        [SugarColumn(ColumnName = "contains_animal_non_textile_parts", ColumnDescription = "标识：包含动物源的非纺织部分", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContainsAnimalNonTextileParts { get; set; } = 0;

        /// <summary>
        /// 标识：新纺织组分功能
        /// </summary>
        [SugarColumn(ColumnName = "new_textile_component_function", ColumnDescription = "标识：新纺织组分功能", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NewTextileComponentFunction { get; set; } = 0;

        /// <summary>
        /// 分段相关
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_relevant", ColumnDescription = "分段相关", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SegmentationRelevant { get; set; } = 0;

        /// <summary>
        /// 标识：季节使用
        /// </summary>
        [SugarColumn(ColumnName = "seasonal_usage", ColumnDescription = "标识：季节使用", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SeasonalUsage { get; set; } = 0;

        /// <summary>
        /// 标识：在库存管理中激活季节
        /// </summary>
        [SugarColumn(ColumnName = "season_activated_in_inventory_management", ColumnDescription = "标识：在库存管理中激活季节", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SeasonActivatedInInventoryManagement { get; set; } = 0;

        /// <summary>
        /// 特征值转换的物料转换标识
        /// </summary>
        [SugarColumn(ColumnName = "material_conversion_id_for_characteristic_value_conversion", ColumnDescription = "特征值转换的物料转换标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialConversionIdForCharacteristicValueConversion { get; set; } = 0;

        /// <summary>
        /// 可以倾斜物料 (车辆空间优化)
        /// </summary>
        [SugarColumn(ColumnName = "material_can_be_tilted_vso", ColumnDescription = "可以倾斜物料 (车辆空间优化)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialCanBeTiltedVso { get; set; } = 0;

        /// <summary>
        /// 不允许堆栈 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "stacking_not_allowed_vso", ColumnDescription = "不允许堆栈 (VSO)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StackingNotAllowedVso { get; set; } = 0;

        /// <summary>
        /// 底层 (车辆空间最优化)
        /// </summary>
        [SugarColumn(ColumnName = "bottom_layer_vso", ColumnDescription = "底层 (车辆空间最优化)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BottomLayerVso { get; set; } = 0;

        /// <summary>
        /// 顶层 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "top_layer_vso", ColumnDescription = "顶层 (VSO)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TopLayerVso { get; set; } = 0;

        /// <summary>
        /// 无包装物料的装载 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "loading_without_packaging_material_vso", ColumnDescription = "无包装物料的装载 (VSO)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoadingWithoutPackagingMaterialVso { get; set; } = 0;

        // ==================== 代码和参数文件 ====================

        /// <summary>
        /// 价格标记类别
        /// </summary>
        [SugarColumn(ColumnName = "price_marking_category", ColumnDescription = "价格标记类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PriceMarkingCategory { get; set; }

        /// <summary>
        /// 外部物料组
        /// </summary>
        [SugarColumn(ColumnName = "external_material_group", ColumnDescription = "外部物料组", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExternalMaterialGroup { get; set; }

        /// <summary>
        /// 物料类别
        /// </summary>
        [SugarColumn(ColumnName = "material_category", ColumnDescription = "物料类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialCategory { get; set; }

        /// <summary>
        /// 定价参考物料
        /// </summary>
        [SugarColumn(ColumnName = "pricing_reference_material", ColumnDescription = "定价参考物料", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PricingReferenceMaterial { get; set; }

        /// <summary>
        /// 物料从税收分类
        /// </summary>
        [SugarColumn(ColumnName = "material_tax_classification", ColumnDescription = "物料从税收分类", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialTaxClassification { get; set; }

        /// <summary>
        /// 类别参数文件
        /// </summary>
        [SugarColumn(ColumnName = "category_profile", ColumnDescription = "类别参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CategoryProfile { get; set; }

        /// <summary>
        /// 产品分配确定程序
        /// </summary>
        [SugarColumn(ColumnName = "product_allocation_determination_procedure", ColumnDescription = "产品分配确定程序", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductAllocationDeterminationProcedure { get; set; }

        /// <summary>
        /// 变式的定价参数文件
        /// </summary>
        [SugarColumn(ColumnName = "variant_pricing_profile", ColumnDescription = "变式的定价参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VariantPricingProfile { get; set; }

        /// <summary>
        /// 针对物料的折扣类型
        /// </summary>
        [SugarColumn(ColumnName = "discount_type_for_material", ColumnDescription = "针对物料的折扣类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DiscountTypeForMaterial { get; set; }

        /// <summary>
        /// 制造商零件编号
        /// </summary>
        [SugarColumn(ColumnName = "manufacturer_part_number", ColumnDescription = "制造商零件编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>
        /// 制造商编号
        /// </summary>
        [SugarColumn(ColumnName = "manufacturer_number", ColumnDescription = "制造商编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManufacturerNumber { get; set; }

        /// <summary>
        /// 公司自己 (内部) 的盘点管理物料编号
        /// </summary>
        [SugarColumn(ColumnName = "internal_inventory_management_material_number", ColumnDescription = "公司自己 (内部) 的盘点管理物料编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalInventoryManagementMaterialNumber { get; set; }

        /// <summary>
        /// 制造商部件参数文件
        /// </summary>
        [SugarColumn(ColumnName = "manufacturer_part_profile", ColumnDescription = "制造商部件参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManufacturerPartProfile { get; set; }

        /// <summary>
        /// 计量单位使用
        /// </summary>
        [SugarColumn(ColumnName = "unit_of_measure_usage", ColumnDescription = "计量单位使用", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UnitOfMeasureUsage { get; set; }

        /// <summary>
        /// 在季节内展开
        /// </summary>
        [SugarColumn(ColumnName = "expand_within_season", ColumnDescription = "在季节内展开", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpandWithinSeason { get; set; }

        /// <summary>
        /// 危险货物标识参数文件
        /// </summary>
        [SugarColumn(ColumnName = "dangerous_goods_indicator_profile", ColumnDescription = "危险货物标识参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DangerousGoodsIndicatorProfile { get; set; }

        /// <summary>
        /// 序列号的清晰的级别
        /// </summary>
        [SugarColumn(ColumnName = "serial_number_level", ColumnDescription = "序列号的清晰的级别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNumberLevel { get; set; }

        /// <summary>
        /// 指定有效参数值/覆盖更改编号
        /// </summary>
        [SugarColumn(ColumnName = "specified_valid_parameter_value_override_change_number", ColumnDescription = "指定有效参数值/覆盖更改编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecifiedValidParameterValueOverrideChangeNumber { get; set; }

        /// <summary>
        /// 物料完成的水平
        /// </summary>
        [SugarColumn(ColumnName = "material_completion_level", ColumnDescription = "物料完成的水平", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialCompletionLevel { get; set; }

        /// <summary>
        /// 货架寿命到期日的期间标识
        /// </summary>
        [SugarColumn(ColumnName = "shelf_life_expiration_date_period_indicator", ColumnDescription = "货架寿命到期日的期间标识", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ShelfLifeExpirationDatePeriodIndicator { get; set; }

        /// <summary>
        /// 货架寿命到期日期计算舍入规则
        /// </summary>
        [SugarColumn(ColumnName = "shelf_life_expiration_date_rounding_rule", ColumnDescription = "货架寿命到期日期计算舍入规则", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ShelfLifeExpirationDateRoundingRule { get; set; }

        /// <summary>
        /// 普通项目类别组
        /// </summary>
        [SugarColumn(ColumnName = "generic_item_category_group", ColumnDescription = "普通项目类别组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? GenericItemCategoryGroup { get; set; }

        /// <summary>
        /// 分类清单类型
        /// </summary>
        [SugarColumn(ColumnName = "assortment_list_type", ColumnDescription = "分类清单类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssortmentListType { get; set; }

        /// <summary>
        /// 在预包装物料中的一般物料的物料编号
        /// </summary>
        [SugarColumn(ColumnName = "generic_material_number_in_prepack", ColumnDescription = "在预包装物料中的一般物料的物料编号", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? GenericMaterialNumberInPrepack { get; set; }

        /// <summary>
        /// 以相同方式包装的物料的参考物料
        /// </summary>
        [SugarColumn(ColumnName = "reference_material_for_materials_packed_in_same_way", ColumnDescription = "以相同方式包装的物料的参考物料", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReferenceMaterialForMaterialsPackedInSameWay { get; set; }

        /// <summary>
        /// 标准处理单位类型
        /// </summary>
        [SugarColumn(ColumnName = "standard_handling_unit_type", ColumnDescription = "标准处理单位类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StandardHandlingUnitType { get; set; }

        /// <summary>
        /// 仓库物料组
        /// </summary>
        [SugarColumn(ColumnName = "warehouse_material_group", ColumnDescription = "仓库物料组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WarehouseMaterialGroup { get; set; }

        /// <summary>
        /// 处理标识
        /// </summary>
        [SugarColumn(ColumnName = "handling_indicator", ColumnDescription = "处理标识", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HandlingIndicator { get; set; }

        /// <summary>
        /// 处理单位类型
        /// </summary>
        [SugarColumn(ColumnName = "handling_unit_type", ColumnDescription = "处理单位类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HandlingUnitType { get; set; }

        // ==================== 时间和期间 ====================

        /// <summary>
        /// 最短剩余货架寿命
        /// </summary>
        [SugarColumn(ColumnName = "minimum_remaining_shelf_life", ColumnDescription = "最短剩余货架寿命", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MinimumRemainingShelfLife { get; set; } = 0;

        /// <summary>
        /// 总货架寿命
        /// </summary>
        [SugarColumn(ColumnName = "total_shelf_life", ColumnDescription = "总货架寿命", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TotalShelfLife { get; set; } = 0;

        /// <summary>
        /// 仓储百分比
        /// </summary>
        [SugarColumn(ColumnName = "storage_percentage", ColumnDescription = "仓储百分比", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StoragePercentage { get; set; } = 0;

        /// <summary>
        /// 隔离期间
        /// </summary>
        [SugarColumn(ColumnName = "quarantine_period", ColumnDescription = "隔离期间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int QuarantinePeriod { get; set; } = 0;

        // ==================== 地理和运输 ====================

        /// <summary>
        /// 物料原产地国家
        /// </summary>
        [SugarColumn(ColumnName = "country_of_origin", ColumnDescription = "物料原产地国家", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CountryOfOrigin { get; set; }

        /// <summary>
        /// 物料运输组
        /// </summary>
        [SugarColumn(ColumnName = "material_transportation_group", ColumnDescription = "物料运输组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialTransportationGroup { get; set; }

        // ==================== 质量管理 ====================

        /// <summary>
        /// 质量检查组
        /// </summary>
        [SugarColumn(ColumnName = "quality_inspection_group", ColumnDescription = "质量检查组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityInspectionGroup { get; set; }

        /// <summary>
        /// 序列号参数文件
        /// </summary>
        [SugarColumn(ColumnName = "serial_number_profile", ColumnDescription = "序列号参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNumberProfile { get; set; }

        /// <summary>
        /// 物料条件管理
        /// </summary>
        [SugarColumn(ColumnName = "material_condition_management", ColumnDescription = "物料条件管理", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialConditionManagement { get; set; }

        // ==================== 特殊字段 ====================

        /// <summary>
        /// 表格名
        /// </summary>
        [SugarColumn(ColumnName = "table_name", ColumnDescription = "表格名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TableName { get; set; }

        /// <summary>
        /// EWM CW: 输入 CW 数量的称重参数文件
        /// </summary>
        [SugarColumn(ColumnName = "ewm_cw_catch_weight_profile", ColumnDescription = "EWM CW: 输入 CW 数量的称重参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EwmCwCatchWeightProfile { get; set; }

        /// <summary>
        /// EWM-CW: EWM 的称重容差组
        /// </summary>
        [SugarColumn(ColumnName = "ewm_cw_weight_tolerance_group", ColumnDescription = "EWM-CW: EWM 的称重容差组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EwmCwWeightToleranceGroup { get; set; }

        /// <summary>
        /// 调整参数文件
        /// </summary>
        [SugarColumn(ColumnName = "adjustment_profile", ColumnDescription = "调整参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AdjustmentProfile { get; set; }

        /// <summary>
        /// 分段结构
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_structure", ColumnDescription = "分段结构", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationStructure { get; set; }

        /// <summary>
        /// 分段策略
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_strategy", ColumnDescription = "分段策略", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationStrategy { get; set; }

        /// <summary>
        /// 分段生产物料主数据状态
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_material_master_status", ColumnDescription = "分段生产物料主数据状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationMaterialMasterStatus { get; set; }

        /// <summary>
        /// 分段策略范围
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_strategy_scope", ColumnDescription = "分段策略范围", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationStrategyScope { get; set; }

        /// <summary>
        /// ANP Code
        /// </summary>
        [SugarColumn(ColumnName = "anp_code", ColumnDescription = "ANP Code", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AnpCode { get; set; }

        /// <summary>
        /// Fashion 信息字段：1
        /// </summary>
        [SugarColumn(ColumnName = "fashion_info_field_1", ColumnDescription = "Fashion 信息字段：1", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FashionInfoField1 { get; set; }

        /// <summary>
        /// Fashion 信息字段：2
        /// </summary>
        [SugarColumn(ColumnName = "fashion_info_field_2", ColumnDescription = "Fashion 信息字段：2", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FashionInfoField2 { get; set; }

        /// <summary>
        /// Fashion 信息字段：3
        /// </summary>
        [SugarColumn(ColumnName = "fashion_info_field_3", ColumnDescription = "Fashion 信息字段：3", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FashionInfoField3 { get; set; }

        /// <summary>
        /// 装载单位
        /// </summary>
        [SugarColumn(ColumnName = "loading_unit", ColumnDescription = "装载单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoadingUnit { get; set; }

        /// <summary>
        /// 装载单位组：IS 饮料
        /// </summary>
        [SugarColumn(ColumnName = "loading_unit_group_is_beverage", ColumnDescription = "装载单位组：IS 饮料", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoadingUnitGroupIsBeverage { get; set; }

        /// <summary>
        /// 物料关系的结构类别
        /// </summary>
        [SugarColumn(ColumnName = "structural_category_material_relationship", ColumnDescription = "物料关系的结构类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StructuralCategoryMaterialRelationship { get; set; }

        /// <summary>
        /// 容差类型标识
        /// </summary>
        [SugarColumn(ColumnName = "tolerance_type_indicator", ColumnDescription = "容差类型标识", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ToleranceTypeIndicator { get; set; }

        /// <summary>
        /// 计算组
        /// </summary>
        [SugarColumn(ColumnName = "calculation_group", ColumnDescription = "计算组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CalculationGroup { get; set; }

        /// <summary>
        /// DSD 分组
        /// </summary>
        [SugarColumn(ColumnName = "dsd_grouping", ColumnDescription = "DSD 分组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DsdGrouping { get; set; }

        /// <summary>
        /// 堆栈因子 (车辆空间优化)
        /// </summary>
        [SugarColumn(ColumnName = "stacking_factor_vso", ColumnDescription = "堆栈因子 (车辆空间优化)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal StackingFactorVso { get; set; } = 0;

        /// <summary>
        /// 包装物料的允许悬挂深度 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "allowed_overhang_depth_packaging_material_vso", ColumnDescription = "包装物料的允许悬挂深度 (VSO)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal AllowedOverhangDepthPackagingMaterialVso { get; set; } = 0;

        /// <summary>
        /// 装运物料的允许悬挂宽度 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "allowed_overhang_width_shipping_material_vso", ColumnDescription = "装运物料的允许悬挂宽度 (VSO)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal AllowedOverhangWidthShippingMaterialVso { get; set; } = 0;

        /// <summary>
        /// 包装物料的最大叠放高度 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "max_stacking_height_packaging_material_vso", ColumnDescription = "包装物料的最大叠放高度 (VSO)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxStackingHeightPackagingMaterialVso { get; set; } = 0;

        /// <summary>
        /// 包装物料的最小叠放高度 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "min_stacking_height_packaging_material_vso", ColumnDescription = "包装物料的最小叠放高度 (VSO)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinStackingHeightPackagingMaterialVso { get; set; } = 0;

        /// <summary>
        /// 超出最大叠放高度的容差 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "tolerance_exceeding_max_stacking_height_vso", ColumnDescription = "超出最大叠放高度的容差 (VSO)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ToleranceExceedingMaxStackingHeightVso { get; set; } = 0;

        /// <summary>
        /// 每个已结清 PKM 的物料编号(VSO)
        /// </summary>
        [SugarColumn(ColumnName = "material_number_per_cleared_pkm_vso", ColumnDescription = "每个已结清 PKM 的物料编号(VSO)", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialNumberPerClearedPkmVso { get; set; }

        /// <summary>
        /// 请求的已清包装物料 (VSO)
        /// </summary>
        [SugarColumn(ColumnName = "requested_cleared_packaging_material_vso", ColumnDescription = "请求的已清包装物料 (VSO)", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RequestedClearedPackagingMaterialVso { get; set; }

        /// <summary>
        /// 返回代码
        /// </summary>
        [SugarColumn(ColumnName = "return_code", ColumnDescription = "返回代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReturnCode { get; set; }

        /// <summary>
        /// 后勤等级退货
        /// </summary>
        [SugarColumn(ColumnName = "logistics_level_return", ColumnDescription = "后勤等级退货", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LogisticsLevelReturn { get; set; }

        /// <summary>
        /// NATO 项目标识编号
        /// </summary>
        [SugarColumn(ColumnName = "nato_item_identification_number", ColumnDescription = "NATO 项目标识编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NatoItemIdentificationNumber { get; set; }

        /// <summary>
        /// 完全互换组
        /// </summary>
        [SugarColumn(ColumnName = "complete_interchangeability_group", ColumnDescription = "完全互换组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompleteInterchangeabilityGroup { get; set; }

        /// <summary>
        /// 替换链编号
        /// </summary>
        [SugarColumn(ColumnName = "follow_up_chain_number", ColumnDescription = "替换链编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FollowUpChainNumber { get; set; }

        /// <summary>
        /// 创建状态 - 季节性采购
        /// </summary>
        [SugarColumn(ColumnName = "creation_status_seasonal_procurement", ColumnDescription = "创建状态 - 季节性采购", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CreationStatusSeasonalProcurement { get; set; }

        // ==================== 特性和变式 ====================

        /// <summary>
        /// 颜色特性的内部特性编号
        /// </summary>
        [SugarColumn(ColumnName = "internal_characteristic_number_color", ColumnDescription = "颜色特性的内部特性编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalCharacteristicNumberColor { get; set; }

        /// <summary>
        /// 主尺寸的特性的内部特性编号
        /// </summary>
        [SugarColumn(ColumnName = "internal_characteristic_number_main_size", ColumnDescription = "主尺寸的特性的内部特性编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalCharacteristicNumberMainSize { get; set; }

        /// <summary>
        /// 次尺寸的特性的内部特性编号
        /// </summary>
        [SugarColumn(ColumnName = "internal_characteristic_number_secondary_size", ColumnDescription = "次尺寸的特性的内部特性编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalCharacteristicNumberSecondarySize { get; set; }

        /// <summary>
        /// 变式颜色的特性值
        /// </summary>
        [SugarColumn(ColumnName = "variant_color_characteristic_value", ColumnDescription = "变式颜色的特性值", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VariantColorCharacteristicValue { get; set; }

        /// <summary>
        /// 变式主尺寸的特性值
        /// </summary>
        [SugarColumn(ColumnName = "variant_main_size_characteristic_value", ColumnDescription = "变式主尺寸的特性值", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VariantMainSizeCharacteristicValue { get; set; }

        /// <summary>
        /// 变式的次尺寸的特性值
        /// </summary>
        [SugarColumn(ColumnName = "variant_secondary_size_characteristic_value", ColumnDescription = "变式的次尺寸的特性值", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VariantSecondarySizeCharacteristicValue { get; set; }

        /// <summary>
        /// 评估目的的特性值
        /// </summary>
        [SugarColumn(ColumnName = "characteristic_value_for_evaluation_purposes", ColumnDescription = "评估目的的特性值", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CharacteristicValueForEvaluationPurposes { get; set; }

        // ==================== 纺织和时装 ====================

        /// <summary>
        /// 护理代码（例如洗涤代码, 烫平衣服代码, 等等)
        /// </summary>
        [SugarColumn(ColumnName = "care_code", ColumnDescription = "护理代码（例如洗涤代码, 烫平衣服代码, 等等)", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CareCode { get; set; }

        /// <summary>
        /// 商标
        /// </summary>
        [SugarColumn(ColumnName = "brand", ColumnDescription = "商标", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Brand { get; set; }

        /// <summary>
        /// 纺织品的光纤代码（组件1）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_code_component_1", ColumnDescription = "纺织品的光纤代码（组件1）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiberCodeComponent1 { get; set; }

        /// <summary>
        /// 光纤的百分比共享（组件1）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_percentage_component_1", ColumnDescription = "光纤的百分比共享（组件1）", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FiberPercentageComponent1 { get; set; } = 0;

        /// <summary>
        /// 纺织品的光纤代码（组件2）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_code_component_2", ColumnDescription = "纺织品的光纤代码（组件2）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiberCodeComponent2 { get; set; }

        /// <summary>
        /// 光纤的百分比共享（组件2）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_percentage_component_2", ColumnDescription = "光纤的百分比共享（组件2）", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FiberPercentageComponent2 { get; set; } = 0;

        /// <summary>
        /// 纺织品的光纤代码（组件3）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_code_component_3", ColumnDescription = "纺织品的光纤代码（组件3）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiberCodeComponent3 { get; set; }

        /// <summary>
        /// 光纤的百分比共享（组件3）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_percentage_component_3", ColumnDescription = "光纤的百分比共享（组件3）", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FiberPercentageComponent3 { get; set; } = 0;

        /// <summary>
        /// 纺织品的光纤代码（组件4）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_code_component_4", ColumnDescription = "纺织品的光纤代码（组件4）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiberCodeComponent4 { get; set; }

        /// <summary>
        /// 光纤的百分比共享（组件4）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_percentage_component_4", ColumnDescription = "光纤的百分比共享（组件4）", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FiberPercentageComponent4 { get; set; } = 0;

        /// <summary>
        /// 纺织品的光纤代码（组件5）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_code_component_5", ColumnDescription = "纺织品的光纤代码（组件5）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiberCodeComponent5 { get; set; }

        /// <summary>
        /// 光纤的百分比共享（组件5）
        /// </summary>
        [SugarColumn(ColumnName = "fiber_percentage_component_5", ColumnDescription = "光纤的百分比共享（组件5）", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FiberPercentageComponent5 { get; set; } = 0;

        /// <summary>
        /// 时装等级
        /// </summary>
        [SugarColumn(ColumnName = "fashion_grade", ColumnDescription = "时装等级", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FashionGrade { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
}




