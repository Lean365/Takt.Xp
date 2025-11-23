#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentDistribution.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档分发表
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO文档分发表
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文档的分发情况，以部门为单位进行分发
    /// 功能特点:
    /// 1. 以部门为单位分发ISO文档（符合ISO质量管理体系要求）
    /// 2. 记录分发份数和分发方式
    /// 3. 完整的接收确认流程
    /// 4. 分发状态跟踪和统计
    /// 注意: ISO文档分发必须按部门进行，不支持个人、角色、岗位分发
    /// </remarks>
    [SugarTable("Takt_routine_document_iso_distribution", "ISO文档分发表")]
    [SugarIndex("ix_iso_distribution_document_id", nameof(DocumentId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_distribution_dept_id", nameof(DistributedToDeptId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_distribution_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_distribution_date", nameof(DistributionDate), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_distribution_received_by", nameof(ReceivedBy), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_distribution_received_date", nameof(ReceiveDate), OrderByType.Desc, false)]
    [SugarIndex("ix_iso_distribution_confirm_by", nameof(ConfirmBy), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_distribution_confirm_date", nameof(ConfirmDate), OrderByType.Desc, false)]
    public class TaktIsoDocumentDistribution : TaktBaseEntity
    {
        /// <summary>
        /// ISO文档ID
        /// </summary>
        [SugarColumn(ColumnName = "document_id", ColumnDescription = "ISO文档ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本号", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 分发到的部门ID
        /// </summary>
        [SugarColumn(ColumnName = "distributed_to_dept_id", ColumnDescription = "分发到的部门ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long DistributedToDeptId { get; set; }

        /// <summary>
        /// 分发到的部门名称
        /// </summary>
        [SugarColumn(ColumnName = "distributed_to_dept_name", ColumnDescription = "分发到的部门名称", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string DistributedToDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 分发方式。0=系统通知，1=邮件，2=打印，3=电子文档，4=其他
        /// </summary>
        [SugarColumn(ColumnName = "distribution_method", ColumnDescription = "分发方式", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int DistributionMethod { get; set; }

        /// <summary>
        /// 分发份数
        /// </summary>
        [SugarColumn(ColumnName = "distribution_copies", ColumnDescription = "分发份数", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int DistributionCopies { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        [SugarColumn(ColumnName = "distribution_date", ColumnDescription = "分发日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime DistributionDate { get; set; }

        /// <summary>
        /// 分发人
        /// </summary>
        [SugarColumn(ColumnName = "distributor_by", ColumnDescription = "分发人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string DistributorBy { get; set; } = string.Empty;

        /// <summary>
        /// 状态。0=待分发，1=已分发，2=已接收，3=已确认，4=已拒绝，5=已过期
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        [SugarColumn(ColumnName = "receive_date", ColumnDescription = "接收日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [SugarColumn(ColumnName = "received_by", ColumnDescription = "接收人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReceivedBy { get; set; }

        /// <summary>
        /// 确认日期
        /// </summary>
        [SugarColumn(ColumnName = "confirm_date", ColumnDescription = "确认日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        [SugarColumn(ColumnName = "confirm_by", ColumnDescription = "确认人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmBy { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        [SugarColumn(ColumnName = "expire_date", ColumnDescription = "过期日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 分发说明
        /// </summary>
        [SugarColumn(ColumnName = "distribution_note", ColumnDescription = "分发说明", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DistributionNote { get; set; }

        /// <summary>
        /// 接收说明
        /// </summary>
        [SugarColumn(ColumnName = "receive_note", ColumnDescription = "接收说明", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReceiveNote { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        [SugarColumn(ColumnName = "reject_reason", ColumnDescription = "拒绝原因", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RejectReason { get; set; }

        /// <summary>
        /// 是否强制分发
        /// </summary>
        [SugarColumn(ColumnName = "is_forced", ColumnDescription = "是否强制分发", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsForced { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        [SugarColumn(ColumnName = "is_read", ColumnDescription = "是否已读", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsRead { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ReadCount { get; set; }

        /// <summary>
        /// 最后阅读时间
        /// </summary>
        [SugarColumn(ColumnName = "last_read_time", ColumnDescription = "最后阅读时间", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? LastReadTime { get; set; }

        /// <summary>
        /// 关联的ISO文档
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(DocumentId))]
        public TaktIsoDocument? Document { get; set; }

        /// <summary>
        /// 关联的分发部门
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(DistributedToDeptId))]
        public TaktDept? DistributedToDepartment { get; set; }
    }
}




