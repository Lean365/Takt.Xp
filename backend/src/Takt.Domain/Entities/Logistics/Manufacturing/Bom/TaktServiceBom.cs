#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAfterSalesBom.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 售后BOM实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 售后BOM实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 售后BOM管理，用于售后服务阶段
    /// </remarks>
    [SugarTable("Takt_logistics_bom_service", "服务BOM")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_product_code", nameof(ProductCode), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_service_type", nameof(ServiceType), OrderByType.Asc, false)]
    public class TaktServiceBom : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>产品编码</summary>
        [SugarColumn(ColumnName = "product_code", ColumnDescription = "产品编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>产品名称</summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>产品型号</summary>
        [SugarColumn(ColumnName = "product_model", ColumnDescription = "产品型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductModel { get; set; }

        /// <summary>售后版本</summary>
        [SugarColumn(ColumnName = "after_sales_version", ColumnDescription = "售后版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AfterSalesVersion { get; set; } = string.Empty;

        /// <summary>BOM版本</summary>
        [SugarColumn(ColumnName = "bom_version", ColumnDescription = "BOM版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BomVersion { get; set; } = string.Empty;

        /// <summary>层级</summary>
        [SugarColumn(ColumnName = "level", ColumnDescription = "层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int Level { get; set; } = 1;

        /// <summary>行项目</summary>
        [SugarColumn(ColumnName = "line_item", ColumnDescription = "行项目", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LineItem { get; set; } = string.Empty;

        /// <summary>物料编码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>物料名称</summary>
        [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialName { get; set; } = string.Empty;

        /// <summary>物料规格</summary>
        [SugarColumn(ColumnName = "material_specification", ColumnDescription = "物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialSpecification { get; set; }

        /// <summary>服务类型(1=维修 2=保养 3=更换 4=升级 5=其他)</summary>
        [SugarColumn(ColumnName = "service_type", ColumnDescription = "服务类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ServiceType { get; set; } = 1;

        /// <summary>服务数量</summary>
        [SugarColumn(ColumnName = "service_quantity", ColumnDescription = "服务数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ServiceQuantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>安装位置</summary>
        [SugarColumn(ColumnName = "installation_position", ColumnDescription = "安装位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InstallationPosition { get; set; }

        /// <summary>是否易损件</summary>
        [SugarColumn(ColumnName = "is_wear_part", ColumnDescription = "是否易损件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsWearPart { get; set; } = false;

        /// <summary>是否关键件</summary>
        [SugarColumn(ColumnName = "is_critical_part", ColumnDescription = "是否关键件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsCriticalPart { get; set; } = false;

        /// <summary>是否标准件</summary>
        [SugarColumn(ColumnName = "is_standard_part", ColumnDescription = "是否标准件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsStandardPart { get; set; } = false;

        /// <summary>是否外购件</summary>
        [SugarColumn(ColumnName = "is_purchased_part", ColumnDescription = "是否外购件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsPurchasedPart { get; set; } = false;

        /// <summary>是否自制件</summary>
        [SugarColumn(ColumnName = "is_manufactured_part", ColumnDescription = "是否自制件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsManufacturedPart { get; set; } = false;

        /// <summary>供应商编码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierCode { get; set; }

        /// <summary>供应商名称</summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName { get; set; }

        /// <summary>售后价格</summary>
        [SugarColumn(ColumnName = "after_sales_price", ColumnDescription = "售后价格", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal AfterSalesPrice { get; set; } = 0;

        /// <summary>保修期(月)</summary>
        [SugarColumn(ColumnName = "warranty_period", ColumnDescription = "保修期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int WarrantyPeriod { get; set; } = 0;

        /// <summary>更换周期(月)</summary>
        [SugarColumn(ColumnName = "replacement_cycle", ColumnDescription = "更换周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReplacementCycle { get; set; } = 0;

        /// <summary>使用寿命(小时)</summary>
        [SugarColumn(ColumnName = "service_life", ColumnDescription = "使用寿命", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ServiceLife { get; set; } = 0;

        /// <summary>故障率(%)</summary>
        [SugarColumn(ColumnName = "failure_rate", ColumnDescription = "故障率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FailureRate { get; set; } = 0;

        /// <summary>维修难度(1=简单 2=一般 3=困难 4=复杂)</summary>
        [SugarColumn(ColumnName = "repair_difficulty", ColumnDescription = "维修难度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int RepairDifficulty { get; set; } = 2;

        /// <summary>维修工时(小时)</summary>
        [SugarColumn(ColumnName = "repair_hours", ColumnDescription = "维修工时", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal RepairHours { get; set; } = 0;

        /// <summary>维修工具</summary>
        [SugarColumn(ColumnName = "repair_tools", ColumnDescription = "维修工具", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairTools { get; set; }

        /// <summary>维修技能要求</summary>
        [SugarColumn(ColumnName = "repair_skill_requirement", ColumnDescription = "维修技能要求", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairSkillRequirement { get; set; }

        /// <summary>备件库存</summary>
        [SugarColumn(ColumnName = "spare_parts_stock", ColumnDescription = "备件库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal SparePartsStock { get; set; } = 0;

        /// <summary>安全库存</summary>
        [SugarColumn(ColumnName = "safety_stock", ColumnDescription = "安全库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal SafetyStock { get; set; } = 0;

        /// <summary>采购提前期(天)</summary>
        [SugarColumn(ColumnName = "purchase_lead_time", ColumnDescription = "采购提前期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PurchaseLeadTime { get; set; } = 0;

        /// <summary>是否停产</summary>
        [SugarColumn(ColumnName = "is_discontinued", ColumnDescription = "是否停产", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsDiscontinued { get; set; } = false;

        /// <summary>替代件编码</summary>
        [SugarColumn(ColumnName = "alternative_part_code", ColumnDescription = "替代件编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AlternativePartCode { get; set; }

        /// <summary>替代件名称</summary>
        [SugarColumn(ColumnName = "alternative_part_name", ColumnDescription = "替代件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AlternativePartName { get; set; }

        /// <summary>技术文档编号</summary>
        [SugarColumn(ColumnName = "technical_doc_number", ColumnDescription = "技术文档编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalDocNumber { get; set; }

        /// <summary>售后工程师</summary>
        [SugarColumn(ColumnName = "after_sales_engineer", ColumnDescription = "售后工程师", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterSalesEngineer { get; set; }

        /// <summary>创建日期</summary>
        [SugarColumn(ColumnName = "create_date", ColumnDescription = "创建日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CreateDate { get; set; }

        /// <summary>售后备注</summary>
        [SugarColumn(ColumnName = "after_sales_remarks", ColumnDescription = "售后备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterSalesRemarks { get; set; }

        /// <summary>状态(0=草稿 1=生效 2=失效)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



