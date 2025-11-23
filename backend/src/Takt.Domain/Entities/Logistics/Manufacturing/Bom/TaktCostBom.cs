#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCostBom.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 成本BOM实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 成本BOM实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 成本BOM管理，用于成本核算和定价
    /// </remarks>
    [SugarTable("Takt_logistics_bom_cost", "成本BOM")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_product_code", nameof(ProductCode), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_cost_version", nameof(CostVersion), OrderByType.Asc, false)]
    public class TaktCostBom : TaktBaseEntity
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

        /// <summary>成本版本</summary>
        [SugarColumn(ColumnName = "cost_version", ColumnDescription = "成本版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CostVersion { get; set; } = string.Empty;

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

        /// <summary>成本数量</summary>
        [SugarColumn(ColumnName = "cost_quantity", ColumnDescription = "成本数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal CostQuantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>标准成本</summary>
        [SugarColumn(ColumnName = "standard_cost", ColumnDescription = "标准成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StandardCost { get; set; } = 0;

        /// <summary>实际成本</summary>
        [SugarColumn(ColumnName = "actual_cost", ColumnDescription = "实际成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualCost { get; set; } = 0;

        /// <summary>计划成本</summary>
        [SugarColumn(ColumnName = "planned_cost", ColumnDescription = "计划成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedCost { get; set; } = 0;

        /// <summary>目标成本</summary>
        [SugarColumn(ColumnName = "target_cost", ColumnDescription = "目标成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TargetCost { get; set; } = 0;

        /// <summary>采购成本</summary>
        [SugarColumn(ColumnName = "purchase_cost", ColumnDescription = "采购成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PurchaseCost { get; set; } = 0;

        /// <summary>制造成本</summary>
        [SugarColumn(ColumnName = "manufacturing_cost", ColumnDescription = "制造成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ManufacturingCost { get; set; } = 0;

        /// <summary>人工成本</summary>
        [SugarColumn(ColumnName = "labor_cost", ColumnDescription = "人工成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal LaborCost { get; set; } = 0;

        /// <summary>制造费用</summary>
        [SugarColumn(ColumnName = "overhead_cost", ColumnDescription = "制造费用", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OverheadCost { get; set; } = 0;

        /// <summary>运输成本</summary>
        [SugarColumn(ColumnName = "transportation_cost", ColumnDescription = "运输成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TransportationCost { get; set; } = 0;

        /// <summary>包装成本</summary>
        [SugarColumn(ColumnName = "packaging_cost", ColumnDescription = "包装成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PackagingCost { get; set; } = 0;

        /// <summary>质量成本</summary>
        [SugarColumn(ColumnName = "quality_cost", ColumnDescription = "质量成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal QualityCost { get; set; } = 0;

        /// <summary>库存成本</summary>
        [SugarColumn(ColumnName = "inventory_cost", ColumnDescription = "库存成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InventoryCost { get; set; } = 0;

        /// <summary>总成本</summary>
        [SugarColumn(ColumnName = "total_cost", ColumnDescription = "总成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalCost { get; set; } = 0;

        /// <summary>成本差异</summary>
        [SugarColumn(ColumnName = "cost_variance", ColumnDescription = "成本差异", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CostVariance { get; set; } = 0;

        /// <summary>成本差异率(%)</summary>
        [SugarColumn(ColumnName = "cost_variance_rate", ColumnDescription = "成本差异率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CostVarianceRate { get; set; } = 0;

        /// <summary>币种</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>汇率</summary>
        [SugarColumn(ColumnName = "exchange_rate", ColumnDescription = "汇率", ColumnDataType = "decimal(10,4)", IsNullable = false, DefaultValue = "1")]
        public decimal ExchangeRate { get; set; } = 1;

        /// <summary>成本中心</summary>
        [SugarColumn(ColumnName = "cost_center", ColumnDescription = "成本中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenter { get; set; }

        /// <summary>成本类型(1=直接材料 2=直接人工 3=制造费用 4=间接费用 5=其他)</summary>
        [SugarColumn(ColumnName = "cost_type", ColumnDescription = "成本类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CostType { get; set; } = 1;

        /// <summary>成本分类(1=固定成本 2=变动成本 3=半变动成本)</summary>
        [SugarColumn(ColumnName = "cost_category", ColumnDescription = "成本分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int CostCategory { get; set; } = 2;

        /// <summary>是否关键成本</summary>
        [SugarColumn(ColumnName = "is_critical_cost", ColumnDescription = "是否关键成本", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsCriticalCost { get; set; } = false;

        /// <summary>成本核算方法(1=标准成本法 2=实际成本法 3=计划成本法)</summary>
        [SugarColumn(ColumnName = "costing_method", ColumnDescription = "成本核算方法", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CostingMethod { get; set; } = 1;

        /// <summary>成本更新频率(1=实时 2=日 3=周 4=月 5=季度)</summary>
        [SugarColumn(ColumnName = "cost_update_frequency", ColumnDescription = "成本更新频率", ColumnDataType = "int", IsNullable = false, DefaultValue = "4")]
        public int CostUpdateFrequency { get; set; } = 4;

        /// <summary>最后成本更新日期</summary>
        [SugarColumn(ColumnName = "last_cost_update_date", ColumnDescription = "最后成本更新日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LastCostUpdateDate { get; set; }

        /// <summary>成本工程师</summary>
        [SugarColumn(ColumnName = "cost_engineer", ColumnDescription = "成本工程师", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostEngineer { get; set; }

        /// <summary>成本审核人</summary>
        [SugarColumn(ColumnName = "cost_reviewer", ColumnDescription = "成本审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostReviewer { get; set; }

        /// <summary>成本批准人</summary>
        [SugarColumn(ColumnName = "cost_approver", ColumnDescription = "成本批准人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostApprover { get; set; }

        /// <summary>成本备注</summary>
        [SugarColumn(ColumnName = "cost_remarks", ColumnDescription = "成本备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostRemarks { get; set; }

        /// <summary>状态(0=草稿 1=生效 2=失效)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



