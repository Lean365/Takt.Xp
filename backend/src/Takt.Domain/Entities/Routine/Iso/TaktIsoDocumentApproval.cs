#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentApproval.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档审批表
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO文档审批表
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 专门处理ISO文档的审批流程，与申请流程分离
    /// 功能特点:
    /// 1. 支持多级审批流程（部门审核、管理者代表批准等）
    /// 2. 区分指定审批人和实际审批人
    /// 3. 支持审批意见和审批结果记录
    /// 4. 完整的审批时间跟踪
    /// 注意: 此表只处理审批流程，不包含申请信息
    /// </remarks>
    [SugarTable("Takt_routine_document_iso_approval", "ISO文档审批表")]
    [SugarIndex("ix_iso_approval_document_id", nameof(DocumentId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_approval_step", nameof(Step), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_approval_step_type", nameof(ApprovalStep), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_approval_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_approval_approver_by", nameof(ApproverBy), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_approval_result", nameof(ApprovalResult), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_approval_date", nameof(ApprovalDate), OrderByType.Desc, false)]
    public class TaktIsoDocumentApproval : TaktBaseEntity
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
        /// 审批步骤
        /// </summary>
        [SugarColumn(ColumnName = "step", ColumnDescription = "审批步骤", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Step { get; set; }

        /// <summary>
        /// 审批环节。1=部门审核，2=管理者代表批准，3=质量经理审批，4=总经理审批，5=其他
        /// </summary>
        [SugarColumn(ColumnName = "approval_step", ColumnDescription = "审批环节", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ApprovalStep { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        [SugarColumn(ColumnName = "step_name", ColumnDescription = "审批步骤名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        [SugarColumn(ColumnName = "approver_by", ColumnDescription = "审批人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ApproverBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人部门
        /// </summary>
        [SugarColumn(ColumnName = "approver_dept", ColumnDescription = "审批人部门", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApproverDept { get; set; }


        /// <summary>
        /// 状态。0=待审批，1=已审批，2=已拒绝，3=已跳过，4=已撤回
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=驳回，3=跳过，4=撤回
        /// </summary>
        [SugarColumn(ColumnName = "approval_result", ColumnDescription = "审批结果", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_comments", ColumnDescription = "审批意见", Length = 2000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 提交日期
        /// </summary>
        [SugarColumn(ColumnName = "submit_date", ColumnDescription = "提交日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime SubmitDate { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [SugarColumn(ColumnName = "submitter_by", ColumnDescription = "提交人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string SubmitterBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否必须审批
        /// </summary>
        [SugarColumn(ColumnName = "is_required", ColumnDescription = "是否必须审批", IsNullable = false, DefaultValue = "1", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否并行审批
        /// </summary>
        [SugarColumn(ColumnName = "is_parallel", ColumnDescription = "是否并行审批", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsParallel { get; set; }

        /// <summary>
        /// 审批时限（小时）
        /// </summary>
        [SugarColumn(ColumnName = "time_limit_hours", ColumnDescription = "审批时限（小时）", IsNullable = true, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? TimeLimitHours { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        [SugarColumn(ColumnName = "is_timeout", ColumnDescription = "是否超时", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsTimeout { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        [SugarColumn(ColumnName = "timeout_date", ColumnDescription = "超时时间", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? TimeoutDate { get; set; }

        /// <summary>
        /// 前一步骤ID
        /// </summary>
        [SugarColumn(ColumnName = "previous_step_id", ColumnDescription = "前一步骤ID", IsNullable = true, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? PreviousStepId { get; set; }

        /// <summary>
        /// 下一步骤ID
        /// </summary>
        [SugarColumn(ColumnName = "next_step_id", ColumnDescription = "下一步骤ID", IsNullable = true, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? NextStepId { get; set; }

        /// <summary>
        /// 审批附件
        /// </summary>
        [SugarColumn(ColumnName = "approval_attachment", ColumnDescription = "审批附件", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalAttachment { get; set; }

        /// <summary>
        /// 关联的ISO文档
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(DocumentId))]
        public TaktIsoDocument? Document { get; set; }
    }
}




