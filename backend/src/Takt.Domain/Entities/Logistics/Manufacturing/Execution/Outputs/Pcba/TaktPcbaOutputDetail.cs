#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPcbaDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Pcba
{
    /// <summary>
    /// PCBA明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: PCBA生产管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_outputs_pcba_detail", "PCBA生产明细")]
    [SugarIndex("ix_pcba_output_id", nameof(PcbaOutputId), OrderByType.Asc, false)]
    [SugarIndex("ix_time_period", nameof(TimePeriod), OrderByType.Asc, false)]
    public class TaktPcbaOutputDetail : TaktBaseEntity
    {
        /// <summary>
        /// PCBA日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_output_id", ColumnDescription = "PCBA ECP产出ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        [SugarColumn(ColumnName = "time_period", ColumnDescription = "生产时段", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 班组
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班组", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 板别
        /// </summary>
        [SugarColumn(ColumnName = "pcb_board_type", ColumnDescription = "PCB板别", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PcbBoardType { get; set; } = string.Empty;

        /// <summary>
        /// 面板别
        /// </summary>
        [SugarColumn(ColumnName = "panel_side", ColumnDescription = "面板别", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PanelSide { get; set; } = string.Empty;

        /// <summary>
        /// 批次数量
        /// </summary>
        [SugarColumn(ColumnName = "batch_qty", ColumnDescription = "批次数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal BatchQty { get; set; } = 0;

        /// <summary>
        /// 当日完成数
        /// </summary>
        [SugarColumn(ColumnName = "daily_completed_qty", ColumnDescription = "当日完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal DailyCompletedQty { get; set; } = 0;

        /// <summary>
        /// 累计完成数
        /// </summary>
        [SugarColumn(ColumnName = "total_completed_qty", ColumnDescription = "累计完成数", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal TotalCompletedQty { get; set; } = 0;

        /// <summary>
        /// 完成状态
        /// </summary>
        [SugarColumn(ColumnName = "completed_status", ColumnDescription = "完成状态", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompletedStatus { get; set; } = string.Empty;

        /// <summary>
        /// 序列号
        /// </summary>
        [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SerialNo { get; set; } = string.Empty;

        /// <summary>
        /// 不良台数
        /// </summary>
        [SugarColumn(ColumnName = "defect_count", ColumnDescription = "不良台数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DefectCount { get; set; } = 0;

        /// <summary>
        /// 投入工数
        /// </summary>
        [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal InputMinutes { get; set; } = 0;  

        /// <summary>
        /// 修工数
        /// </summary>
        [SugarColumn(ColumnName = "repair_minutes", ColumnDescription = "修工数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RepairMinutes { get; set; } = 0;

        /// <summary>
        /// 切换次数
        /// </summary>
        [SugarColumn(ColumnName = "switch_count", ColumnDescription = "切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SwitchCount { get; set; } = 0;

        /// <summary>
        /// 切换时间
        /// </summary>
        [SugarColumn(ColumnName = "switch_time", ColumnDescription = "切换时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal SwitchTime { get; set; } = 0;

        /// <summary>
        /// 切停机时间
        /// </summary>
        [SugarColumn(ColumnName = "stop_time", ColumnDescription = "切停机时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal StopTime { get; set; } = 0;

        /// <summary>
        /// 总工数
        /// </summary>
        [SugarColumn(ColumnName = "total_minutes", ColumnDescription = "总工数", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal TotalMinutes { get; set; } = 0;

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
        /// PCBA日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaOutputId))]
        public TaktPcbaOutput? PcbaOutput { get; set; }
    }
} 



