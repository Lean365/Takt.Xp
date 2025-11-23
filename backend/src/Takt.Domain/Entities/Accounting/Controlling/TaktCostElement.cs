#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCostElement.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 成本要素实体类 (基于SAP CO成本要素管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Controlling
{
    /// <summary>
    /// 成本要素实体类 (基于SAP CO成本要素管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP CO-OM-CEL 成本要素会计
    /// </remarks>
    [SugarTable("Takt_accounting_controlling_cost_element", "成本要素表")]
    [SugarIndex("ix_cost_element_code", nameof(CostElementCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktCostElement : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 成本要素编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_code", ColumnDescription = "成本要素编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CostElementCode { get; set; } = string.Empty;

        /// <summary>
        /// 成本要素名称
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_name", ColumnDescription = "成本要素名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CostElementName { get; set; } = string.Empty;

        /// <summary>
        /// 成本要素简称
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_short_name", ColumnDescription = "成本要素简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementShortName { get; set; }

        /// <summary>
        /// 成本要素类型
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_type", ColumnDescription = "成本要素类型", ColumnDataType = "int", IsNullable = false)]
        public int CostElementType { get; set; }

        /// <summary>
        /// 上级成本要素ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级成本要素ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 上级成本要素
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktCostElement? Parent { get; set; }

        /// <summary>
        /// 成本要素分类
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_category", ColumnDescription = "成本要素分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CostElementCategory { get; set; } = string.Empty;

        /// <summary>
        /// 成本要素子分类
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_sub_category", ColumnDescription = "成本要素子分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementSubCategory { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "计量单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Unit { get; set; }

        /// <summary>
        /// 成本要素描述
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_description", ColumnDescription = "成本要素描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementDescription { get; set; }

        /// <summary>
        /// 成本要素用途
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_purpose", ColumnDescription = "成本要素用途", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementPurpose { get; set; }

        /// <summary>
        /// 成本要素性质
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_nature", ColumnDescription = "成本要素性质", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementNature { get; set; }

        /// <summary>
        /// 是否允许分配
        /// </summary>
        [SugarColumn(ColumnName = "allow_allocation", ColumnDescription = "是否允许分配", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowAllocation { get; set; } = 1;

        /// <summary>
        /// 是否允许分摊
        /// </summary>
        [SugarColumn(ColumnName = "allow_distribution", ColumnDescription = "是否允许分摊", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowDistribution { get; set; } = 1;

        /// <summary>
        /// 是否允许结转
        /// </summary>
        [SugarColumn(ColumnName = "allow_carry_forward", ColumnDescription = "是否允许结转", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowCarryForward { get; set; } = 1;

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = false, DefaultValue = "9999-12-31")]
        public DateTime? ExpiryDate { get; set; } = new DateTime(9999, 12, 31);

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
}



