#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSopChange.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : SOP变更记录表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Sop
{
    /// <summary>
    /// SOP变更记录表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 描述: SOP变更管理
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_sop_change", "SOP变更记录表")]
    [SugarIndex("ix_change_id", nameof(ChangeId), OrderByType.Asc, true)]
    [SugarIndex("ix_sop_id", nameof(SopId), OrderByType.Asc, false)]
    public class TaktSopChange : TaktBaseEntity
    {
        /// <summary>
        /// 变更ID
        /// </summary>
        [SugarColumn(ColumnName = "change_id", ColumnDescription = "变更ID", Length = 20, ColumnDataType = "varchar", IsNullable = false, IsPrimaryKey = true)]
        public string ChangeId { get; set; } = string.Empty;

        /// <summary>
        /// SOP编号
        /// </summary>
        [SugarColumn(ColumnName = "sop_id", ColumnDescription = "SOP编号", Length = 20, ColumnDataType = "varchar", IsNullable = false)]
        public string SopId { get; set; } = string.Empty;

        /// <summary>
        /// 旧版本号
        /// </summary>
        [SugarColumn(ColumnName = "old_version", ColumnDescription = "旧版本号", Length = 10, ColumnDataType = "varchar", IsNullable = false)]
        public string OldVersion { get; set; } = string.Empty;

        /// <summary>
        /// 新版本号
        /// </summary>
        [SugarColumn(ColumnName = "new_version", ColumnDescription = "新版本号", Length = 10, ColumnDataType = "varchar", IsNullable = false)]
        public string NewVersion { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 状态(0=草稿 1=发布)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 申请人
        /// </summary>
        [SugarColumn(ColumnName = "requester", ColumnDescription = "申请人", Length = 20, ColumnDataType = "varchar", IsNullable = false)]
        public string Requester { get; set; } = string.Empty;

        /// <summary>
        /// 申请时间
        /// </summary>
        [SugarColumn(ColumnName = "request_time", ColumnDescription = "申请时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime RequestTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 批准人
        /// </summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "批准人", Length = 20, ColumnDataType = "varchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>
        /// 批准时间
        /// </summary>
        [SugarColumn(ColumnName = "approve_time", ColumnDescription = "批准时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApproveTime { get; set; }

        // ==================== 导航属性 ====================

        /// <summary>
        /// 关联的SOP主表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public TaktSopHead? SopHead { get; set; }
    }
}




