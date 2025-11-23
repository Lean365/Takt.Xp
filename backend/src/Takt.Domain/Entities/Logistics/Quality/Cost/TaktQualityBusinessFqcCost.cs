#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktQualityBusinessFqcCost.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 品质业务FQC成本子表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Quality.Cost
{
    /// <summary>
    /// 品质业务FQC成本子表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录品质业务FQC（出荷检查）相关的成本明细，包括出荷检查、信赖性评价、顾客品质要求对应等业务费用
    /// </remarks>
    [SugarTable("Takt_logistics_quality_business_fqc_cost", "品质业务FQC成本子表")]
    [SugarIndex("ix_quality_business_fqc_cost", nameof(QualityBusinessId), OrderByType.Asc, nameof(CostDate), OrderByType.Desc, false)]
    public class TaktQualityBusinessFqcCost : TaktBaseEntity
    {
        /// <summary>品质业务ID</summary>
        [SugarColumn(ColumnName = "quality_business_id", ColumnDescription = "品质业务ID", ColumnDataType = "bigint", IsNullable = false)]
        public long QualityBusinessId { get; set; }

        /// <summary>成本日期</summary>
        [SugarColumn(ColumnName = "cost_date", ColumnDescription = "成本日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime CostDate { get; set; } = DateTime.Today;

        /// <summary>直接人员费率</summary>
        [SugarColumn(ColumnName = "direct_personnel_rate", ColumnDescription = "直接人员费率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DirectPersonnelRate { get; set; } = 0;

        /// <summary>出荷检查业务费用</summary>
        [SugarColumn(ColumnName = "shipping_inspection_cost", ColumnDescription = "出荷检查业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ShippingInspectionCost { get; set; } = 0;

        /// <summary>检查时间(分)</summary>
        [SugarColumn(ColumnName = "inspection_time_minutes", ColumnDescription = "检查时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionTimeMinutes { get; set; } = 0;

        /// <summary>检查其他费用</summary>
        [SugarColumn(ColumnName = "inspection_other_cost", ColumnDescription = "检查其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionOtherCost { get; set; } = 0;

        /// <summary>检查备注</summary>
        [SugarColumn(ColumnName = "inspection_remark", ColumnDescription = "检查备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InspectionRemark { get; set; }

        /// <summary>信赖性评价・ORT业务费用</summary>
        [SugarColumn(ColumnName = "reliability_evaluation_ort_cost", ColumnDescription = "信赖性评价・ORT业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ReliabilityEvaluationOrtCost { get; set; } = 0;

        /// <summary>评价作业时间(分)</summary>
        [SugarColumn(ColumnName = "evaluation_work_time_minutes", ColumnDescription = "评价作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EvaluationWorkTimeMinutes { get; set; } = 0;

        /// <summary>评价其他费用</summary>
        [SugarColumn(ColumnName = "evaluation_other_cost", ColumnDescription = "评价其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EvaluationOtherCost { get; set; } = 0;

        /// <summary>评价备注</summary>
        [SugarColumn(ColumnName = "evaluation_remark", ColumnDescription = "评价备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EvaluationRemark { get; set; }

        /// <summary>顾客品质要求对应业务费用</summary>
        [SugarColumn(ColumnName = "customer_quality_requirement_cost", ColumnDescription = "顾客品质要求对应业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CustomerQualityRequirementCost { get; set; } = 0;

        /// <summary>评价作业时间(分)</summary>
        [SugarColumn(ColumnName = "customer_quality_work_time_minutes", ColumnDescription = "评价作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CustomerQualityWorkTimeMinutes { get; set; } = 0;

        /// <summary>评价其他费用</summary>
        [SugarColumn(ColumnName = "customer_quality_other_cost", ColumnDescription = "评价其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CustomerQualityOtherCost { get; set; } = 0;

        /// <summary>评价备注</summary>
        [SugarColumn(ColumnName = "customer_quality_remark", ColumnDescription = "评价备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerQualityRemark { get; set; }

        /// <summary>其他通常业务费用</summary>
        [SugarColumn(ColumnName = "other_normal_business_cost", ColumnDescription = "其他通常业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OtherNormalBusinessCost { get; set; } = 0;

        /// <summary>通常业务作业时间(分)</summary>
        [SugarColumn(ColumnName = "normal_business_work_time_minutes", ColumnDescription = "通常业务作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal NormalBusinessWorkTimeMinutes { get; set; } = 0;

        /// <summary>通常业务其他费用</summary>
        [SugarColumn(ColumnName = "normal_business_other_cost", ColumnDescription = "通常业务其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal NormalBusinessOtherCost { get; set; } = 0;

        /// <summary>通常业务其他备注</summary>
        [SugarColumn(ColumnName = "normal_business_other_remark", ColumnDescription = "通常业务其他备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NormalBusinessOtherRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



