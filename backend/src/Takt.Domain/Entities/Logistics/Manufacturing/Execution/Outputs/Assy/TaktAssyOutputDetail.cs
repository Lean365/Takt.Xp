#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 组立生产明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立生产明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 组立生产管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_outputs_assy_detail", "组立生产明细表")]
    [SugarIndex("ix_assy_output_id", nameof(AssyOutputId), OrderByType.Asc, false)]
    public class TaktAssyOutputDetail : TaktBaseEntity
    {
        /// <summary>
        /// 组立日报ID
        /// </summary>
        [SugarColumn(ColumnName = "assy_output_id", ColumnDescription = "组立日报ID", ColumnDataType = "bigint", IsNullable = false)]
        public long AssyOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        [SugarColumn(ColumnName = "time_period", ColumnDescription = "生产时段", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 实际生产数量
        /// </summary>
        [SugarColumn(ColumnName = "prod_actual_qty", ColumnDescription = "实际生产数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "downtime_minutes", ColumnDescription = "停线时间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        [SugarColumn(ColumnName = "downtime_reason", ColumnDescription = "停线原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        [SugarColumn(ColumnName = "downtime_description", ColumnDescription = "停线说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        [SugarColumn(ColumnName = "unachieved_reason", ColumnDescription = "未达成原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UnachievedReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        [SugarColumn(ColumnName = "unachieved_description", ColumnDescription = "未达成说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UnachievedDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "prod_minutes", ColumnDescription = "生产工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        [SugarColumn(ColumnName = "achievement_rate", ColumnDescription = "达成率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal AchievementRate { get; set; } = 0;

        /// <summary>
        /// 组立日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(AssyOutputId))]
        public TaktAssyOutput? AssyOutput { get; set; }
    }
} 



