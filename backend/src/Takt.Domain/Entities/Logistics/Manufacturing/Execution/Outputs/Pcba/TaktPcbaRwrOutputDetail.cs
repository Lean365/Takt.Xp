#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPcbaEppMirDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA修正明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Pcba
{
    /// <summary>
    /// PCBA修正明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: PCBA修正明细管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_outputs_pcba_rwr_detail", "PCBA修正明细")]
    [SugarIndex("ix_pcba_rwr_output_id", nameof(PcbaRwrOutputId), OrderByType.Asc, false)]
    public class TaktPcbaRwrOutputDetail : TaktBaseEntity
    {
        /// <summary>
        /// PCBA修正ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_rwr_output_id", ColumnDescription = "PCBA修正ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaRwrOutputId { get; set; }

        /// <summary>
        /// 实际生产数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_epp_qty", ColumnDescription = "实际数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 1, IsNullable = false, DefaultValue = "0")]
        public decimal ActualEppQty { get; set; } = 0;

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
        /// PCBA手插
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaRwrOutputId))]
        public TaktPcbaRwrOutput? PcbaRwrOutput { get; set; }
    }
}



