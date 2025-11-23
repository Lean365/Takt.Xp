#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentObsolete.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档作废表
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO文档作废表
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文档的作废和回收情况，包括作废原因、回收状态、处理方式等
    /// 功能特点:
    /// 1. 作废流程管理 - 记录文档的作废过程
    /// 2. 申请信息 - 包含作废申请的相关字段（申请编号、申请人、申请原因等）
    /// 3. 审批信息 - 包含审批流程的相关字段（审批结果、审批意见等）
    /// 4. 回收管理 - 记录文档回收、销毁、归档等后续处理
    /// 注意: 申请和审批字段在同一表中，但通过注释明确区分
    /// </remarks>
    [SugarTable("Takt_routine_document_iso_obsolete", "ISO文档作废表")]
    [SugarIndex("ix_iso_obsolete_document_id", nameof(DocumentId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_obsolete_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_obsolete_type", nameof(ObsoleteType), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_obsolete_date", nameof(ObsoleteDate), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_obsolete_application_code", nameof(ApplicationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_iso_obsolete_applicant_by", nameof(ApplicantBy), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_obsolete_application_status", nameof(ApplicationStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_obsolete_application_date", nameof(ApplicationDate), OrderByType.Desc, false)]
    [SugarIndex("ix_iso_obsolete_approval_result", nameof(ApprovalResult), OrderByType.Asc, false)]
    public class TaktIsoDocumentObsolete : TaktBaseEntity
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
        /// 作废类型。0=自然作废，1=强制作废，2=替代作废，3=错误作废
        /// </summary>
        [SugarColumn(ColumnName = "obsolete_type", ColumnDescription = "作废类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ObsoleteType { get; set; }

        /// <summary>
        /// 作废原因
        /// </summary>
        [SugarColumn(ColumnName = "obsolete_reason", ColumnDescription = "作废原因", Length = 1000, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ObsoleteReason { get; set; } = string.Empty;

        /// <summary>
        /// 作废日期
        /// </summary>
        [SugarColumn(ColumnName = "obsolete_date", ColumnDescription = "作废日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime ObsoleteDate { get; set; }

        /// <summary>
        /// 作废人
        /// </summary>
        [SugarColumn(ColumnName = "obsolete_by", ColumnDescription = "作废人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ObsoleteBy { get; set; } = string.Empty;

        /// <summary>
        /// 替代文档ID
        /// </summary>
        [SugarColumn(ColumnName = "replacement_document_id", ColumnDescription = "替代文档ID", IsNullable = true, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ReplacementDocumentId { get; set; }

        /// <summary>
        /// 替代文档版本号
        /// </summary>
        [SugarColumn(ColumnName = "replacement_document_version", ColumnDescription = "替代文档版本号", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReplacementDocumentVersion { get; set; }

        /// <summary>
        /// 状态。0=已作废，1=待回收，2=已回收，3=已销毁，4=已归档
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 回收日期
        /// </summary>
        [SugarColumn(ColumnName = "recycle_date", ColumnDescription = "回收日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? RecycleDate { get; set; }

        /// <summary>
        /// 回收人
        /// </summary>
        [SugarColumn(ColumnName = "recycle_by", ColumnDescription = "回收人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RecycleBy { get; set; }

        /// <summary>
        /// 回收方式。0=电子回收，1=纸质回收，2=混合回收
        /// </summary>
        [SugarColumn(ColumnName = "recycle_method", ColumnDescription = "回收方式", IsNullable = true, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? RecycleMethod { get; set; }

        /// <summary>
        /// 回收说明
        /// </summary>
        [SugarColumn(ColumnName = "recycle_note", ColumnDescription = "回收说明", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RecycleNote { get; set; }

        /// <summary>
        /// 销毁日期
        /// </summary>
        [SugarColumn(ColumnName = "destroy_date", ColumnDescription = "销毁日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? DestroyDate { get; set; }

        /// <summary>
        /// 销毁人
        /// </summary>
        [SugarColumn(ColumnName = "destroy_by", ColumnDescription = "销毁人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DestroyBy { get; set; }

        /// <summary>
        /// 销毁方式。0=电子销毁，1=物理销毁，2=混合销毁
        /// </summary>
        [SugarColumn(ColumnName = "destroy_method", ColumnDescription = "销毁方式", IsNullable = true, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? DestroyMethod { get; set; }

        /// <summary>
        /// 销毁说明
        /// </summary>
        [SugarColumn(ColumnName = "destroy_note", ColumnDescription = "销毁说明", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DestroyNote { get; set; }

        /// <summary>
        /// 归档日期
        /// </summary>
        [SugarColumn(ColumnName = "archive_date", ColumnDescription = "归档日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ArchiveDate { get; set; }

        /// <summary>
        /// 归档人
        /// </summary>
        [SugarColumn(ColumnName = "archive_by", ColumnDescription = "归档人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ArchiveBy { get; set; }

        /// <summary>
        /// 归档位置
        /// </summary>
        [SugarColumn(ColumnName = "archive_location", ColumnDescription = "归档位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ArchiveLocation { get; set; }

        /// <summary>
        /// 归档说明
        /// </summary>
        [SugarColumn(ColumnName = "archive_note", ColumnDescription = "归档说明", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ArchiveNote { get; set; }

        /// <summary>
        /// 是否通知相关人员
        /// </summary>
        [SugarColumn(ColumnName = "is_notified", ColumnDescription = "是否通知相关人员", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        [SugarColumn(ColumnName = "notify_date", ColumnDescription = "通知日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NotifyDate { get; set; }

        // ==================== 申请相关字段 ====================

        /// <summary>
        /// 申请编号
        /// </summary>
        [SugarColumn(ColumnName = "application_code", ColumnDescription = "申请编号", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请人
        /// </summary>
        [SugarColumn(ColumnName = "applicant_by", ColumnDescription = "申请人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ApplicantBy { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门
        /// </summary>
        [SugarColumn(ColumnName = "applicant_dept", ColumnDescription = "申请人部门", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApplicantDept { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        [SugarColumn(ColumnName = "application_reason", ColumnDescription = "申请原因", Length = 1000, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ApplicationReason { get; set; } = string.Empty;

        /// <summary>
        /// 申请状态。0=待审批，1=审批中，2=已批准，3=已拒绝，4=已撤回
        /// </summary>
        [SugarColumn(ColumnName = "application_status", ColumnDescription = "申请状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ApplicationStatus { get; set; }

        // ==================== 审批相关字段 ====================

        /// <summary>
        /// 审批开始日期
        /// </summary>
        [SugarColumn(ColumnName = "approval_start_date", ColumnDescription = "审批开始日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalStartDate { get; set; }

        /// <summary>
        /// 审批完成日期
        /// </summary>
        [SugarColumn(ColumnName = "approval_complete_date", ColumnDescription = "审批完成日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalCompleteDate { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=拒绝
        /// </summary>
        [SugarColumn(ColumnName = "approval_result", ColumnDescription = "审批结果", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_comment", ColumnDescription = "审批意见", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        [SugarColumn(ColumnName = "approver_by", ColumnDescription = "审批人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        [SugarColumn(ColumnName = "approver_dept", ColumnDescription = "审批人部门", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 申请附件
        /// </summary>
        [SugarColumn(ColumnName = "application_attachment", ColumnDescription = "申请附件", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApplicationAttachment { get; set; }

        /// <summary>
        /// 审批记录列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(TaktIsoDocumentApproval.DocumentId))]
        public List<TaktIsoDocumentApproval>? ApprovalRecords { get; set; }

    }
}




