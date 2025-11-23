#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContractChange.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 合同变更实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Contract
{
    /// <summary>
    /// 合同变更实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录合同变更的相关信息，包括变更信息、变更内容、审批流程等
    /// </remarks>
    [SugarTable("Takt_routine_contract_change", "合同变更表")]
    [SugarIndex("ix_contract_change_code", nameof(ContractChangeCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_contract_change", nameof(CompanyCode), OrderByType.Asc, nameof(ContractChangeCode), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_change_status", nameof(ContractChangeStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_change_type", nameof(ContractChangeType), OrderByType.Asc, false)]
    public class TaktContractChange : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>合同变更编号</summary>
        [SugarColumn(ColumnName = "contract_change_code", ColumnDescription = "合同变更编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractChangeCode { get; set; } = string.Empty;

        /// <summary>变更类型(1=合同金额变更 2=合同期限变更 3=合同条款变更 4=合同方变更 5=其他变更)</summary>
        [SugarColumn(ColumnName = "contract_change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractChangeType { get; set; } = 1;

        /// <summary>变更标题</summary>
        [SugarColumn(ColumnName = "change_title", ColumnDescription = "变更标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeTitle { get; set; } = string.Empty;

        /// <summary>变更描述</summary>
        [SugarColumn(ColumnName = "change_description", ColumnDescription = "变更描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeDescription { get; set; }

        /// <summary>变更原因</summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>原合同编号</summary>
        [SugarColumn(ColumnName = "original_contract_code", ColumnDescription = "原合同编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OriginalContractCode { get; set; } = string.Empty;

        /// <summary>原合同名称</summary>
        [SugarColumn(ColumnName = "original_contract_name", ColumnDescription = "原合同名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OriginalContractName { get; set; } = string.Empty;

        /// <summary>原合同金额</summary>
        [SugarColumn(ColumnName = "original_contract_amount", ColumnDescription = "原合同金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? OriginalContractAmount { get; set; }

        /// <summary>原合同开始日期</summary>
        [SugarColumn(ColumnName = "original_contract_start_date", ColumnDescription = "原合同开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OriginalContractStartDate { get; set; }

        /// <summary>原合同结束日期</summary>
        [SugarColumn(ColumnName = "original_contract_end_date", ColumnDescription = "原合同结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OriginalContractEndDate { get; set; }

        /// <summary>原合同期限(月)</summary>
        [SugarColumn(ColumnName = "original_contract_duration", ColumnDescription = "原合同期限(月)", ColumnDataType = "int", IsNullable = true)]
        public int? OriginalContractDuration { get; set; }

        /// <summary>原合同条款</summary>
        [SugarColumn(ColumnName = "original_contract_terms", ColumnDescription = "原合同条款", Length = 4000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OriginalContractTerms { get; set; }

        /// <summary>新合同金额</summary>
        [SugarColumn(ColumnName = "new_contract_amount", ColumnDescription = "新合同金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? NewContractAmount { get; set; }

        /// <summary>新合同开始日期</summary>
        [SugarColumn(ColumnName = "new_contract_start_date", ColumnDescription = "新合同开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewContractStartDate { get; set; }

        /// <summary>新合同结束日期</summary>
        [SugarColumn(ColumnName = "new_contract_end_date", ColumnDescription = "新合同结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewContractEndDate { get; set; }

        /// <summary>新合同期限(月)</summary>
        [SugarColumn(ColumnName = "new_contract_duration", ColumnDescription = "新合同期限(月)", ColumnDataType = "int", IsNullable = true)]
        public int? NewContractDuration { get; set; }

        /// <summary>新合同条款</summary>
        [SugarColumn(ColumnName = "new_contract_terms", ColumnDescription = "新合同条款", Length = 4000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewContractTerms { get; set; }

        /// <summary>变更金额</summary>
        [SugarColumn(ColumnName = "change_amount", ColumnDescription = "变更金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ChangeAmount { get; set; }

        /// <summary>变更期限(月)</summary>
        [SugarColumn(ColumnName = "change_duration", ColumnDescription = "变更期限(月)", ColumnDataType = "int", IsNullable = true)]
        public int? ChangeDuration { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>变更影响分析</summary>
        [SugarColumn(ColumnName = "change_impact_analysis", ColumnDescription = "变更影响分析", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeImpactAnalysis { get; set; }

        /// <summary>风险评估</summary>
        [SugarColumn(ColumnName = "risk_assessment", ColumnDescription = "风险评估", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskAssessment { get; set; }

        /// <summary>变更优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "change_priority", ColumnDescription = "变更优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ChangePriority { get; set; } = 2;

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>变更部门</summary>
        [SugarColumn(ColumnName = "change_department", ColumnDescription = "变更部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeDepartment { get; set; }

        /// <summary>变更申请人</summary>
        [SugarColumn(ColumnName = "change_applicant", ColumnDescription = "变更申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeApplicant { get; set; }

        /// <summary>变更申请人电话</summary>
        [SugarColumn(ColumnName = "change_applicant_phone", ColumnDescription = "变更申请人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeApplicantPhone { get; set; }

        /// <summary>变更申请人邮箱</summary>
        [SugarColumn(ColumnName = "change_applicant_email", ColumnDescription = "变更申请人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeApplicantEmail { get; set; }

        /// <summary>变更申请日期</summary>
        [SugarColumn(ColumnName = "change_application_date", ColumnDescription = "变更申请日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeApplicationDate { get; set; } = DateTime.Now;

        /// <summary>期望生效日期</summary>
        [SugarColumn(ColumnName = "expected_effective_date", ColumnDescription = "期望生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpectedEffectiveDate { get; set; }

        /// <summary>审核人</summary>
        [SugarColumn(ColumnName = "reviewer", ColumnDescription = "审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Reviewer { get; set; }

        /// <summary>审核日期</summary>
        [SugarColumn(ColumnName = "review_date", ColumnDescription = "审核日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReviewDate { get; set; }

        /// <summary>审核意见</summary>
        [SugarColumn(ColumnName = "review_comment", ColumnDescription = "审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReviewComment { get; set; }

        /// <summary>批准人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "批准人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>批准日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "批准日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>批准意见</summary>
        [SugarColumn(ColumnName = "approval_comment", ColumnDescription = "批准意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalComment { get; set; }

        /// <summary>执行人</summary>
        [SugarColumn(ColumnName = "executor", ColumnDescription = "执行人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Executor { get; set; }

        /// <summary>执行日期</summary>
        [SugarColumn(ColumnName = "execution_date", ColumnDescription = "执行日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExecutionDate { get; set; }

        /// <summary>执行结果</summary>
        [SugarColumn(ColumnName = "execution_result", ColumnDescription = "执行结果", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExecutionResult { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>合同变更状态(0=草稿 1=待审核 2=已审核 3=已批准 4=执行中 5=已完成 6=已拒绝 7=已取消)</summary>
        [SugarColumn(ColumnName = "contract_change_status", ColumnDescription = "合同变更状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContractChangeStatus { get; set; } = 0;

        /// <summary>拒绝原因</summary>
        [SugarColumn(ColumnName = "rejection_reason", ColumnDescription = "拒绝原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RejectionReason { get; set; }

        /// <summary>取消原因</summary>
        [SugarColumn(ColumnName = "cancellation_reason", ColumnDescription = "取消原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CancellationReason { get; set; }

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "contract_change_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractChangeRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



