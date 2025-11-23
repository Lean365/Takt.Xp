#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPcbaChangeoverTime.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA换型时间实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Pcba
{
    /// <summary>
    /// PCBA换型时间实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: PCBA换型时间管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_outputs_pcba_changeover_time", "PCBA换型时间")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_prod_date", nameof(ProdDate), OrderByType.Desc, false)]
    public class TaktPcbaChangeoverTime : TaktBaseEntity
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "prod_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// SMT切换次数
        /// </summary>
        [SugarColumn(ColumnName = "smt_switch_count", ColumnDescription = "SMT切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SmtSwitchCount { get; set; } = 0;

        /// <summary>
        /// SMT总切换时间
        /// </summary>
        [SugarColumn(ColumnName = "smt_total_switch_minutes", ColumnDescription = "SMT总切换时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal SmtTotalSwitchMinutes { get; set; } = 0;

        /// <summary>
        /// 自插切换次数
        /// </summary>
        [SugarColumn(ColumnName = "ai_switch_count", ColumnDescription = "自插切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AiSwitchCount { get; set; } = 0;

        /// <summary>
        /// 自插停机时间
        /// </summary>
        [SugarColumn(ColumnName = "ai_downtime_minutes", ColumnDescription = "自插停机时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal AiDowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 手插读SOP时间
        /// </summary>
        [SugarColumn(ColumnName = "mir_sop_read_minutes", ColumnDescription = "手插读SOP时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MirSopReadMinutes { get; set; } = 0;

        /// <summary>
        /// 手插人数
        /// </summary>
        [SugarColumn(ColumnName = "mir_person_count", ColumnDescription = "手插人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MirPersonCount { get; set; } = 0;

        /// <summary>
        /// 手插读SOP总时间
        /// </summary>
        [SugarColumn(ColumnName = "mir_sop_read_total_minutes", ColumnDescription = "手插读SOP总时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MirSopReadTotalMinutes { get; set; } = 0;

        /// <summary>
        /// 手插切换次数
        /// </summary>
        [SugarColumn(ColumnName = "mir_switch_count", ColumnDescription = "手插切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MirSwitchCount { get; set; } = 0;

        /// <summary>
        /// 手插切换时间
        /// </summary>
        [SugarColumn(ColumnName = "mir_switch_minutes", ColumnDescription = "手插切换时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MirSwitchMinutes { get; set; } = 0;

        /// <summary>
        /// 手插切换总时间
        /// </summary>
        [SugarColumn(ColumnName = "mir_switch_total_minutes", ColumnDescription = "手插切换总时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MirSwitchTotalMinutes { get; set; } = 0;

        /// <summary>
        /// 修正读SOP时间
        /// </summary>
        [SugarColumn(ColumnName = "repair_sop_read_minutes", ColumnDescription = "修正读SOP时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RepairSopReadMinutes { get; set; } = 0;

        /// <summary>
        /// 修正人数
        /// </summary>
        [SugarColumn(ColumnName = "repair_person_count", ColumnDescription = "修正人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RepairPersonCount { get; set; } = 0;

        /// <summary>
        /// 修正读SOP总时间
        /// </summary>
        [SugarColumn(ColumnName = "repair_sop_read_total_minutes", ColumnDescription = "修正读SOP总时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RepairSopReadTotalMinutes { get; set; } = 0;

        /// <summary>
        /// 修正切换次数
        /// </summary>
        [SugarColumn(ColumnName = "repair_switch_count", ColumnDescription = "修正切换次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RepairSwitchCount { get; set; } = 0;

        /// <summary>
        /// 修正切换时间
        /// </summary>
        [SugarColumn(ColumnName = "repair_switch_minutes", ColumnDescription = "修正切换时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RepairSwitchMinutes { get; set; } = 0;

        /// <summary>
        /// 修正切换总时间
        /// </summary>
        [SugarColumn(ColumnName = "repair_switch_total_minutes", ColumnDescription = "修正切换总时间", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RepairSwitchTotalMinutes { get; set; } = 0;
    }
} 



