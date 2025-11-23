#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSopProcess.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : SOP工序流程表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Sop
{
    /// <summary>
    /// SOP工序流程表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 描述: 管理产品的工序流程，支持电子看板显示
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_sop_process", "SOP工序流程表")]
    [SugarIndex("ix_process_sequence", nameof(ProcessSequence), OrderByType.Asc, false)]
    [SugarIndex("ix_station_id", nameof(StationId), OrderByType.Asc, false)]
    public class TaktSopProcess : TaktBaseEntity
    {
        /// <summary>
        /// 工序序号
        /// </summary>
        [SugarColumn(ColumnName = "process_sequence", ColumnDescription = "工序序号", ColumnDataType = "int", IsNullable = false)]
        public int ProcessSequence { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        [SugarColumn(ColumnName = "process_name", ColumnDescription = "工序名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProcessName { get; set; } = string.Empty;

        /// <summary>
        /// 工位ID
        /// </summary>
        [SugarColumn(ColumnName = "station_id", ColumnDescription = "工位ID", Length = 15, ColumnDataType = "varchar", IsNullable = false)]
        public string StationId { get; set; } = string.Empty;

        /// <summary>
        /// SOP编号
        /// </summary>
        [SugarColumn(ColumnName = "sop_id", ColumnDescription = "SOP编号", Length = 20, ColumnDataType = "varchar", IsNullable = false)]
        public string SopId { get; set; } = string.Empty;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "standard_time_minutes", ColumnDescription = "标准工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StandardTimeMinutes { get; set; } = 0;

        /// <summary>
        /// 是否关键工序
        /// </summary>
        [SugarColumn(ColumnName = "is_critical", ColumnDescription = "是否关键工序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCritical { get; set; } = 0;

        /// <summary>
        /// 状态(0=草稿 1=发布)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        // ==================== 导航属性 ====================

        /// <summary>
        /// 关联的SOP主表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public TaktSopHead? SopHead { get; set; }

        /// <summary>
        /// 关联的工位信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public TaktWorkStation? WorkStation { get; set; }
    }
}




