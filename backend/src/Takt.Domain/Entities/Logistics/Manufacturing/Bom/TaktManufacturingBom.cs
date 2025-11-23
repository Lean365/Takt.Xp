#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProductionBom.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 生产BOM实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 生产BOM实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 生产BOM管理，用于生产制造阶段
    /// </remarks>
    [SugarTable("Takt_logistics_bom_manufacturing", "制造BOM")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_product_code", nameof(ProductCode), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_work_center", nameof(WorkCenter), OrderByType.Asc, false)]
    public class TaktManufacturingBom : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>产品编码</summary>
        [SugarColumn(ColumnName = "product_code", ColumnDescription = "产品编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>产品名称</summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductName { get; set; } = string.Empty;

        /// <summary>生产版本</summary>
        [SugarColumn(ColumnName = "production_version", ColumnDescription = "生产版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductionVersion { get; set; } = string.Empty;

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

        /// <summary>生产数量</summary>
        [SugarColumn(ColumnName = "production_quantity", ColumnDescription = "生产数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProductionQuantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>损耗率(%)</summary>
        [SugarColumn(ColumnName = "scrap_rate", ColumnDescription = "损耗率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ScrapRate { get; set; } = 0;

        /// <summary>实际需求数量</summary>
        [SugarColumn(ColumnName = "actual_required_quantity", ColumnDescription = "实际需求数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualRequiredQuantity { get; set; } = 0;

        /// <summary>工作中心</summary>
        [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkCenter { get; set; }

        /// <summary>工序号</summary>
        [SugarColumn(ColumnName = "operation_number", ColumnDescription = "工序号", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperationNumber { get; set; }

        /// <summary>工序描述</summary>
        [SugarColumn(ColumnName = "operation_description", ColumnDescription = "工序描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperationDescription { get; set; }

        /// <summary>标准工时(分钟)</summary>
        [SugarColumn(ColumnName = "standard_time", ColumnDescription = "标准工时", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StandardTime { get; set; } = 0;

        /// <summary>准备工时(分钟)</summary>
        [SugarColumn(ColumnName = "setup_time", ColumnDescription = "准备工时", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal SetupTime { get; set; } = 0;

        /// <summary>供应商编码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierCode { get; set; }

        /// <summary>供应商名称</summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName { get; set; }

        /// <summary>采购单价</summary>
        [SugarColumn(ColumnName = "purchase_price", ColumnDescription = "采购单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PurchasePrice { get; set; } = 0;

        /// <summary>自制成本</summary>
        [SugarColumn(ColumnName = "manufacturing_cost", ColumnDescription = "自制成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ManufacturingCost { get; set; } = 0;

        /// <summary>是否关键件</summary>
        [SugarColumn(ColumnName = "is_critical_part", ColumnDescription = "是否关键件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsCriticalPart { get; set; } = false;

        /// <summary>是否外购件</summary>
        [SugarColumn(ColumnName = "is_purchased_part", ColumnDescription = "是否外购件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsPurchasedPart { get; set; } = false;

        /// <summary>是否自制件</summary>
        [SugarColumn(ColumnName = "is_manufactured_part", ColumnDescription = "是否自制件", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsManufacturedPart { get; set; } = false;

        /// <summary>是否散装物料</summary>
        [SugarColumn(ColumnName = "is_bulk_material", ColumnDescription = "是否散装物料", ColumnDataType = "bit", IsNullable = false, DefaultValue = "0")]
        public bool IsBulkMaterial { get; set; } = false;

        /// <summary>最小库存</summary>
        [SugarColumn(ColumnName = "min_stock", ColumnDescription = "最小库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinStock { get; set; } = 0;

        /// <summary>最大库存</summary>
        [SugarColumn(ColumnName = "max_stock", ColumnDescription = "最大库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaxStock { get; set; } = 0;

        /// <summary>安全库存</summary>
        [SugarColumn(ColumnName = "safety_stock", ColumnDescription = "安全库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal SafetyStock { get; set; } = 0;

        /// <summary>采购提前期(天)</summary>
        [SugarColumn(ColumnName = "purchase_lead_time", ColumnDescription = "采购提前期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PurchaseLeadTime { get; set; } = 0;

        /// <summary>生产提前期(天)</summary>
        [SugarColumn(ColumnName = "production_lead_time", ColumnDescription = "生产提前期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProductionLeadTime { get; set; } = 0;

        /// <summary>生产工程师</summary>
        [SugarColumn(ColumnName = "production_engineer", ColumnDescription = "生产工程师", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionEngineer { get; set; }

        /// <summary>生产日期</summary>
        [SugarColumn(ColumnName = "production_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProductionDate { get; set; }

        /// <summary>生产备注</summary>
        [SugarColumn(ColumnName = "production_remarks", ColumnDescription = "生产备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionRemarks { get; set; }

        /// <summary>状态(0=草稿 1=生效 2=失效)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



