#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyDefectDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 组立不良明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 组立不良明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 组立不良明细管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_defects_assy_detail", "组立不良明细表")]
    [SugarIndex("ix_assy_defect_id", nameof(AssyDefectId), OrderByType.Asc, false)]
    public class TaktAssyDefectDetail : TaktBaseEntity
    {
        /// <summary>
        /// 组立不良ID
        /// </summary>
        [SugarColumn(ColumnName = "assy_defect_id", ColumnDescription = "组立不良ID", ColumnDataType = "bigint", IsNullable = false)]
        public long AssyDefectId { get; set; }

        /// <summary>
        /// 不良区分
        /// </summary>
        [SugarColumn(ColumnName = "defect_category", ColumnDescription = "不良区分", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectCategory { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_qty", ColumnDescription = "不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 累计不良
        /// </summary>
        [SugarColumn(ColumnName = "cumulative_defect_qty", ColumnDescription = "累计不良", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal CumulativeDefectQty { get; set; } = 0;

        /// <summary>
        /// 不良症状
        /// </summary>
        [SugarColumn(ColumnName = "defect_symptom", ColumnDescription = "不良症状", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectSymptom { get; set; }

        /// <summary>
        /// 不良个所
        /// </summary>
        [SugarColumn(ColumnName = "defect_location", ColumnDescription = "不良个所", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectLocation { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        [SugarColumn(ColumnName = "defect_reason", ColumnDescription = "不良原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectReason { get; set; }

        /// <summary>
        /// 修理员
        /// </summary>
        [SugarColumn(ColumnName = "repair_operator", ColumnDescription = "修理员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairOperator { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 组立不良日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(AssyDefectId))]
        public TaktAssyDefect? AssyDefect { get; set; }
    }
} 



