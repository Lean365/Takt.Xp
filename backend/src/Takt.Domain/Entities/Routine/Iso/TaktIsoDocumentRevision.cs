#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentRevision.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档修订历史表
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO文档修订历史表
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文档的修订历史，包括版本号、修订内容、修订原因等
    /// </remarks>
    [SugarTable("Takt_routine_document_iso_revision", "ISO文档修订表")]
    [SugarIndex("ix_iso_revision_document_id", nameof(DocumentId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_revision_version", nameof(Version), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_revision_status", nameof(Status), OrderByType.Asc, false)]
    public class TaktIsoDocumentRevision : TaktBaseEntity
    {
        /// <summary>
        /// ISO文档ID
        /// </summary>
        [SugarColumn(ColumnName = "document_id", ColumnDescription = "ISO文档ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long DocumentId { get; set; }

        /// <summary>
        /// 修订版本号
        /// </summary>
        [SugarColumn(ColumnName = "version", ColumnDescription = "版本号", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 修订日期
        /// </summary>
        [SugarColumn(ColumnName = "revision_date", ColumnDescription = "修订日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime RevisionDate { get; set; }

        /// <summary>
        /// 修订内容
        /// </summary>
        [SugarColumn(ColumnName = "revision_content", ColumnDescription = "修订内容", ColumnDataType = "text", IsNullable = true, IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RevisionContent { get; set; }

        /// <summary>
        /// 修订原因
        /// </summary>
        [SugarColumn(ColumnName = "revision_reason", ColumnDescription = "修订原因", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RevisionReason { get; set; }

        /// <summary>
        /// 修订人
        /// </summary>
        [SugarColumn(ColumnName = "revision_by", ColumnDescription = "修订人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string RevisionBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        [SugarColumn(ColumnName = "approval_by", ColumnDescription = "审批人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalBy { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_comment", ColumnDescription = "审批意见", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 状态。0=草稿，1=待审批，2=已审批，3=已发布，4=已作废
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 是否当前版本
        /// </summary>
        [SugarColumn(ColumnName = "is_current_version", ColumnDescription = "是否当前版本", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsCurrentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? EffectiveDate { get; set; }

    }
}




