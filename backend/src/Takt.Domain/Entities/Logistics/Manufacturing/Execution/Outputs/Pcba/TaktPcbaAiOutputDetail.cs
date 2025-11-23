#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPcbaEppAiDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA自插明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Pcba
{
    /// <summary>
    /// PCBA自插明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: PCBA自插管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_outputs_pcba_ai_detail", "PCBA自插明细")]
    [SugarIndex("ix_pcba_ai_output_id", nameof(PcbaAiOutputId), OrderByType.Asc, false)]
    public class TaktPcbaAiOutputDetail : TaktBaseEntity
    {
        /// <summary>
        /// PCBA自插EPP输出ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_ai_output_id", ColumnDescription = "PCBA自插输出ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaAiOutputId { get; set; }

        /// <summary>
        /// PCB板别
        /// </summary>
        [SugarColumn(ColumnName = "pcb_board_type", ColumnDescription = "PCB板别", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PcbBoardType { get; set; } = string.Empty;

        /// <summary>
        /// 面板别
        /// </summary>
        [SugarColumn(ColumnName = "panel_side", ColumnDescription = "面板别", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PanelSide { get; set; } = string.Empty;

        /// <summary>
        /// 生产时间
        /// </summary>
        [SugarColumn(ColumnName = "prod_time", ColumnDescription = "生产时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ProdTime { get; set; }

        /// <summary>
        /// 批次数
        /// </summary>
        [SugarColumn(ColumnName = "batch_qty", ColumnDescription = "批次数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal BatchQty { get; set; } = 0;

        /// <summary>
        /// 当日完成数
        /// </summary>
        [SugarColumn(ColumnName = "daily_completed_qty", ColumnDescription = "当日完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal DailyCompletedQty { get; set; } = 0;

        /// <summary>
        /// 总完成数
        /// </summary>
        [SugarColumn(ColumnName = "total_completed_qty", ColumnDescription = "累计完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal TotalCompletedQty { get; set; } = 0;

        /// <summary>
        /// 完成状态
        /// </summary>
        [SugarColumn(ColumnName = "completion_status", ColumnDescription = "完成状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompletionStatus { get; set; } = string.Empty;

        /// <summary>
        /// 停线时间
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
        /// 序列号
        /// </summary>
        [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SerialNo { get; set; } = string.Empty;

        /// <summary>
        /// 投入工时
        /// </summary>
        [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时
        /// </summary>
        [SugarColumn(ColumnName = "actual_minutes", ColumnDescription = "实际工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 累计工时
        /// </summary>
        [SugarColumn(ColumnName = "cumulative_minutes", ColumnDescription = "累计工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal CumulativeMinutes { get; set; } = 0;

        /// <summary>
        /// 总工时
        /// </summary>
        [SugarColumn(ColumnName = "total_minutes", ColumnDescription = "总工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal TotalMinutes { get; set; } = 0;

        /// <summary>
        /// 切换次数
        /// </summary>
        [SugarColumn(ColumnName = "switch_count", ColumnDescription = "切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SwitchCount { get; set; } = 0;

        /// <summary>
        /// 切换时间
        /// </summary>
        [SugarColumn(ColumnName = "switch_minutes", ColumnDescription = "切换时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal SwitchMinutes { get; set; } = 0;

        /// <summary>
        /// 切停机时间
        /// </summary>
        [SugarColumn(ColumnName = "switch_downtime_minutes", ColumnDescription = "切停机时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal SwitchDowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 修工时
        /// </summary>
        [SugarColumn(ColumnName = "repair_minutes", ColumnDescription = "修工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RepairMinutes { get; set; } = 0;

        /// <summary>
        /// 不良台数
        /// </summary>
        [SugarColumn(ColumnName = "defect_count", ColumnDescription = "不良台数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DefectCount { get; set; } = 0;

        /// <summary>
        /// 实际点数
        /// </summary>
        [SugarColumn(ColumnName = "actual_shorts", ColumnDescription = "实际点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActualShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        [SugarColumn(ColumnName = "points_unit", ColumnDescription = "点数单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "SHORT")]
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 达成率(%)
        /// </summary>
        [SugarColumn(ColumnName = "achievement_rate", ColumnDescription = "达成率", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal AchievementRate { get; set; } = 0;

        /// <summary>
        /// PCBA自插
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaAiOutputId))]
        public TaktPcbaAiOutput? PcbaAiOutput { get; set; }
    }
}



