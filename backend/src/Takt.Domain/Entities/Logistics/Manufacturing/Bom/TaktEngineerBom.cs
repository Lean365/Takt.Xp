#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTechnicalBom.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 技术BOM实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 技术BOM实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 技术BOM管理，用于产品设计和开发阶段
    /// </remarks>
    [SugarTable("Takt_logistics_bom_engineer_bom", "工程BOM")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_product_code", nameof(ProductCode), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_design_version", nameof(DesignVersion), OrderByType.Asc, false)]
    public class TaktEngineerBom : TaktBaseEntity
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

        /// <summary>设计版本</summary>
        [SugarColumn(ColumnName = "design_version", ColumnDescription = "设计版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DesignVersion { get; set; } = string.Empty;

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

        /// <summary>技术参数</summary>
        [SugarColumn(ColumnName = "technical_parameters", ColumnDescription = "技术参数", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalParameters { get; set; }

        /// <summary>设计数量</summary>
        [SugarColumn(ColumnName = "design_quantity", ColumnDescription = "设计数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DesignQuantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>安装位置</summary>
        [SugarColumn(ColumnName = "installation_position", ColumnDescription = "安装位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InstallationPosition { get; set; }

        /// <summary>装配顺序</summary>
        [SugarColumn(ColumnName = "assembly_sequence", ColumnDescription = "装配顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AssemblySequence { get; set; } = 0;

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

        /// <summary>设计图纸编号</summary>
        [SugarColumn(ColumnName = "drawing_number", ColumnDescription = "设计图纸编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DrawingNumber { get; set; }

        /// <summary>设计工程师</summary>
        [SugarColumn(ColumnName = "design_engineer", ColumnDescription = "设计工程师", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DesignEngineer { get; set; }

        /// <summary>设计日期</summary>
        [SugarColumn(ColumnName = "design_date", ColumnDescription = "设计日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DesignDate { get; set; }

        /// <summary>审核工程师</summary>
        [SugarColumn(ColumnName = "review_engineer", ColumnDescription = "审核工程师", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReviewEngineer { get; set; }

        /// <summary>审核日期</summary>
        [SugarColumn(ColumnName = "review_date", ColumnDescription = "审核日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ReviewDate { get; set; }

        /// <summary>批准工程师</summary>
        [SugarColumn(ColumnName = "approve_engineer", ColumnDescription = "批准工程师", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApproveEngineer { get; set; }

        /// <summary>批准日期</summary>
        [SugarColumn(ColumnName = "approve_date", ColumnDescription = "批准日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ApproveDate { get; set; }

        /// <summary>技术备注</summary>
        [SugarColumn(ColumnName = "technical_remarks", ColumnDescription = "技术备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalRemarks { get; set; }

        /// <summary>状态(0=草稿 1=设计中 2=审核中 3=已批准 4=已发布 5=已作废)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



