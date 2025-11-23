#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPcbaRepairDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA改修明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// PCBA改修明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: PCBA改修明细管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_defects_pcba_repair_detail", "PCBA改修明细表")]
    [SugarIndex("ix_pcba_repair_id", nameof(PcbaRepairId), OrderByType.Asc, false)]
    public class TaktPcbaRepairDetail : TaktBaseEntity
    {
        /// <summary>
        /// PCBA改修日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_repair_id", ColumnDescription = "PCBA改修ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaRepairId { get; set; }


        /// <summary>
        /// PCBA板别
        /// </summary>
        [SugarColumn(ColumnName = "pcba_board_type", ColumnDescription = "PCBA板别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PcbaBoardType { get; set; }

        /// <summary>
        /// 生产实绩
        /// </summary>
        [SugarColumn(ColumnName = "prod_actual_qty", ColumnDescription = "生产实绩", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdLine { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [SugarColumn(ColumnName = "card_no", ColumnDescription = "卡号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CardNo { get; set; }

        /// <summary>
        /// 不良症状
        /// </summary>
        [SugarColumn(ColumnName = "defect_symptom", ColumnDescription = "不良症状", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectSymptom { get; set; }

        /// <summary>
        /// 检出工程
        /// </summary>
        [SugarColumn(ColumnName = "defect_engineering", ColumnDescription = "检出工程", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectEngineering { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        [SugarColumn(ColumnName = "defect_reason", ColumnDescription = "不良原因", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectReason { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_qty", ColumnDescription = "不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 责任归属
        /// </summary>
        [SugarColumn(ColumnName = "defect_responsibility", ColumnDescription = "责任归属", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectResponsibility { get; set; }

        /// <summary>
        /// 不良性质
        /// </summary>
        [SugarColumn(ColumnName = "defect_nature", ColumnDescription = "不良性质", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectNature { get; set; }

        /// <summary>
        /// 修理员
        /// </summary>
        [SugarColumn(ColumnName = "repair_operator", ColumnDescription = "修理员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairOperator { get; set; }

        /// <summary>
        /// PCBA改修日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaRepairId))]
        public TaktPcbaRepair? PcbaRepair { get; set; }
    }
} 



