#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOfficeSupplyWithdraw.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 办公用品领用申请主表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.OfficeSupply
{
    /// <summary>
    /// 办公用品领用申请主表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录办公用品领用申请的主表信息，包括申请人、申请原因、审批状态等
    /// </remarks>
    [SugarTable("Takt_routine_office_supply_withdraw", "办公用品领用申请")]
    [SugarIndex("ix_office_supply_withdraw_code", nameof(RequestCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_office_supply_withdraw", nameof(CompanyCode), OrderByType.Asc, nameof(RequestCode), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_withdraw_applicant", nameof(ApplicantId), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_withdraw_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_office_supply_withdraw_create_time", nameof(CreateTime), OrderByType.Desc, false)]
    public class TaktOfficeSupplyWithdraw : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>领用申请编号</summary>
        [SugarColumn(ColumnName = "request_code", ColumnDescription = "领用申请编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RequestCode { get; set; } = string.Empty;

        /// <summary>申请人ID</summary>
        [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ApplicantId { get; set; }

        /// <summary>申请人姓名</summary>
        [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ApplicantName { get; set; } = string.Empty;

        /// <summary>申请部门</summary>
        [SugarColumn(ColumnName = "department", ColumnDescription = "申请部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Department { get; set; }

        /// <summary>领用原因</summary>
        [SugarColumn(ColumnName = "requisition_reason", ColumnDescription = "领用原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RequisitionReason { get; set; }

        /// <summary>紧急程度(1=普通 2=紧急 3=特急)</summary>
        [SugarColumn(ColumnName = "urgency_level", ColumnDescription = "紧急程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int UrgencyLevel { get; set; } = 1;

        /// <summary>预计总金额</summary>
        [SugarColumn(ColumnName = "estimated_total_amount", ColumnDescription = "预计总金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EstimatedTotalAmount { get; set; } = 0;

        /// <summary>申请状态(0=待审批 1=已审批 2=已拒绝 3=已发放 4=已取消)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "申请状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>审批人ID</summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ApproverId { get; set; }

        /// <summary>审批人姓名</summary>
        [SugarColumn(ColumnName = "approver_name", ColumnDescription = "审批人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApproverName { get; set; }

        /// <summary>审批时间</summary>
        [SugarColumn(ColumnName = "approve_time", ColumnDescription = "审批时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApproveTime { get; set; }

        /// <summary>审批意见</summary>
        [SugarColumn(ColumnName = "approve_remark", ColumnDescription = "审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApproveRemark { get; set; }

        /// <summary>发放人ID</summary>
        [SugarColumn(ColumnName = "issuer_id", ColumnDescription = "发放人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? IssuerId { get; set; }

        /// <summary>发放人姓名</summary>
        [SugarColumn(ColumnName = "issuer_name", ColumnDescription = "发放人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssuerName { get; set; }

        /// <summary>发放时间</summary>
        [SugarColumn(ColumnName = "issue_time", ColumnDescription = "发放时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? IssueTime { get; set; }

        /// <summary>发放备注</summary>
        [SugarColumn(ColumnName = "issue_remark", ColumnDescription = "发放备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueRemark { get; set; }

        /// <summary>预计发放时间</summary>
        [SugarColumn(ColumnName = "expected_issue_time", ColumnDescription = "预计发放时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExpectedIssueTime { get; set; }


    }
}



