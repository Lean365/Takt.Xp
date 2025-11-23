#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktScrapAccidentCost.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 废弃事故成本子表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Quality.Cost
{
    /// <summary>
    /// 废弃事故成本子表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录废弃相关的成本明细，包括机种、部品、废弃费用、处理费用等
    /// </remarks>
    [SugarTable("Takt_logistics_quality_business_scrap_accident_cost", "废弃事故成本子表")]
    [SugarIndex("ix_quality_business_scrap_accident_cost", nameof(QualityBusinessId), OrderByType.Asc, nameof(CostDate), OrderByType.Desc, false)]
    public class TaktScrapAccidentCost : TaktBaseEntity
    {
        /// <summary>品质业务ID</summary>
        [SugarColumn(ColumnName = "quality_business_id", ColumnDescription = "品质业务ID", ColumnDataType = "bigint", IsNullable = false)]
        public long QualityBusinessId { get; set; }

        /// <summary>成本日期</summary>
        [SugarColumn(ColumnName = "cost_date", ColumnDescription = "成本日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime CostDate { get; set; } = DateTime.Today;

        /// <summary>机种</summary>
        [SugarColumn(ColumnName = "model_type", ColumnDescription = "机种", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModelType { get; set; }

        /// <summary>间接人员费率</summary>
        [SugarColumn(ColumnName = "indirect_personnel_rate", ColumnDescription = "间接人员费率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal IndirectPersonnelRate { get; set; } = 0;

        /// <summary>部品品号</summary>
        [SugarColumn(ColumnName = "part_number", ColumnDescription = "部品品号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartNumber { get; set; }

        /// <summary>部品品名</summary>
        [SugarColumn(ColumnName = "part_name", ColumnDescription = "部品品名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartName { get; set; }

        /// <summary>事故内容</summary>
        [SugarColumn(ColumnName = "accident_content", ColumnDescription = "事故内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccidentContent { get; set; }

        /// <summary>废弃费用</summary>
        [SugarColumn(ColumnName = "scrap_cost", ColumnDescription = "废弃费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ScrapCost { get; set; } = 0;

        /// <summary>废弃数量</summary>
        [SugarColumn(ColumnName = "scrap_quantity", ColumnDescription = "废弃数量", ColumnDataType = "decimal(15,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ScrapQuantity { get; set; } = 0;

        /// <summary>部品单价</summary>
        [SugarColumn(ColumnName = "part_unit_price", ColumnDescription = "部品单价", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PartUnitPrice { get; set; } = 0;

        /// <summary>废弃处理费用</summary>
        [SugarColumn(ColumnName = "scrap_processing_cost", ColumnDescription = "废弃处理费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ScrapProcessingCost { get; set; } = 0;

        /// <summary>运费</summary>
        [SugarColumn(ColumnName = "freight_cost", ColumnDescription = "运费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FreightCost { get; set; } = 0;

        /// <summary>其他费用</summary>
        [SugarColumn(ColumnName = "other_cost", ColumnDescription = "其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OtherCost { get; set; } = 0;

        /// <summary>处理作业时间(分)</summary>
        [SugarColumn(ColumnName = "processing_work_time_minutes", ColumnDescription = "处理作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessingWorkTimeMinutes { get; set; } = 0;

        /// <summary>关税</summary>
        [SugarColumn(ColumnName = "customs_duty", ColumnDescription = "关税", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CustomsDuty { get; set; } = 0;

        /// <summary>处理发生其他费用</summary>
        [SugarColumn(ColumnName = "processing_other_cost", ColumnDescription = "处理发生其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessingOtherCost { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



