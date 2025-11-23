#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPcbaInspectionDailyDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : PCBA检查明细实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// PCBA检查明细实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: PCBA检查明细管理
    /// </remarks>
    [SugarTable("Takt_logistics_execution_defects_pcba_inspection_detail", "PCBA检查明细表")]
    [SugarIndex("ix_pcba_inspection_id", nameof(PcbaInspectionId), OrderByType.Asc, false)]
    public class TaktPcbaInspectionDetail : TaktBaseEntity
    {
        /// <summary>
        /// PCBA检查日报ID
        /// </summary>
        [SugarColumn(ColumnName = "pcba_inspection_id", ColumnDescription = "PCBA检查ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PcbaInspectionId { get; set; }

        /// <summary>
        /// PCBA板别
        /// </summary>
        [SugarColumn(ColumnName = "pcba_board_type", ColumnDescription = "PCBA板别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PcbaBoardType { get; set; }

        /// <summary>
        /// 目视线别
        /// </summary>
        [SugarColumn(ColumnName = "visual_inspection_line", ColumnDescription = "目视线别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VisualInspectionLine { get; set; }

        /// <summary>
        /// AOI线别
        /// </summary>
        [SugarColumn(ColumnName = "aoi_line", ColumnDescription = "AOI线别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AoiLine { get; set; }

        /// <summary>
        /// B面实装日期
        /// </summary>
        [SugarColumn(ColumnName = "b_side_assembly_date", ColumnDescription = "B面实装日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? BSideAssemblyDate { get; set; }

        /// <summary>
        /// T面实装日期
        /// </summary>
        [SugarColumn(ColumnName = "t_side_assembly_date", ColumnDescription = "T面实装日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? TSideAssemblyDate { get; set; }

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public string? ShiftNo { get; set; }

        /// <summary>
        /// 检查员
        /// </summary>
        [SugarColumn(ColumnName = "inspector_name", ColumnDescription = "检查员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InspectorName { get; set; }

        /// <summary>
        /// 当日完成数量
        /// </summary>
        [SugarColumn(ColumnName = "daily_completed_qty", ColumnDescription = "当日完成数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyCompletedQty { get; set; } = 0;

        /// <summary>
        /// 检查数量
        /// </summary>
        [SugarColumn(ColumnName = "inspection_qty", ColumnDescription = "检查数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionQty { get; set; } = 0;

        /// <summary>
        /// 检查状态
        /// </summary>
        [SugarColumn(ColumnName = "inspection_status", ColumnDescription = "检查状态", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InspectionStatus { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdLine { get; set; }

        /// <summary>
        /// 检查工数
        /// </summary>
        [SugarColumn(ColumnName = "inspection_work_hours", ColumnDescription = "检查工数", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionWorkHours { get; set; } = 0;

        /// <summary>
        /// AOI工数
        /// </summary>
        [SugarColumn(ColumnName = "aoi_work_hours", ColumnDescription = "AOI工数", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal AoiWorkHours { get; set; } = 0;

        /// <summary>
        /// 不良数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_qty", ColumnDescription = "不良数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 手贴
        /// </summary>
        [SugarColumn(ColumnName = "hand_placement", ColumnDescription = "手贴", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HandPlacement { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        [SugarColumn(ColumnName = "serial_number", ColumnDescription = "流水号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNumber { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnName = "content", ColumnDescription = "内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Content { get; set; }

        /// <summary>
        /// 不良个所
        /// </summary>
        [SugarColumn(ColumnName = "defect_location", ColumnDescription = "不良个所", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectLocation { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// PCBA检查日报
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PcbaInspectionId))]
        public TaktPcbaInspection? PcbaInspection { get; set; }
    }
} 



