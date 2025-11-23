#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContractPayment.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 合同付款实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Contract
{
    /// <summary>
    /// 合同付款实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录合同付款的相关信息，包括付款信息、付款计划、付款记录等
    /// </remarks>
    [SugarTable("Takt_routine_contract_payment", "合同付款表")]
    [SugarIndex("ix_contract_payment_code", nameof(ContractPaymentCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_contract_payment", nameof(CompanyCode), OrderByType.Asc, nameof(ContractPaymentCode), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_payment_status", nameof(ContractPaymentStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_payment_type", nameof(ContractPaymentType), OrderByType.Asc, false)]
    public class TaktContractPayment : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>合同付款编号</summary>
        [SugarColumn(ColumnName = "contract_payment_code", ColumnDescription = "合同付款编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractPaymentCode { get; set; } = string.Empty;

        /// <summary>付款类型(1=预付款 2=进度款 3=验收款 4=质保金 5=其他付款)</summary>
        [SugarColumn(ColumnName = "contract_payment_type", ColumnDescription = "付款类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractPaymentType { get; set; } = 1;

        /// <summary>付款标题</summary>
        [SugarColumn(ColumnName = "payment_title", ColumnDescription = "付款标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PaymentTitle { get; set; } = string.Empty;

        /// <summary>付款描述</summary>
        [SugarColumn(ColumnName = "payment_description", ColumnDescription = "付款描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentDescription { get; set; }

        /// <summary>关联合同编号</summary>
        [SugarColumn(ColumnName = "related_contract_code", ColumnDescription = "关联合同编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedContractCode { get; set; } = string.Empty;

        /// <summary>关联合同名称</summary>
        [SugarColumn(ColumnName = "related_contract_name", ColumnDescription = "关联合同名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedContractName { get; set; } = string.Empty;

        /// <summary>合同总金额</summary>
        [SugarColumn(ColumnName = "contract_total_amount", ColumnDescription = "合同总金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ContractTotalAmount { get; set; }

        /// <summary>付款金额</summary>
        [SugarColumn(ColumnName = "payment_amount", ColumnDescription = "付款金额", ColumnDataType = "decimal(15,2)", IsNullable = false)]
        public decimal PaymentAmount { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>付款比例(%)</summary>
        [SugarColumn(ColumnName = "payment_percentage", ColumnDescription = "付款比例(%)", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? PaymentPercentage { get; set; }

        /// <summary>付款条件</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentTerms { get; set; }

        /// <summary>付款方式(1=银行转账 2=现金 3=支票 4=汇票 5=其他)</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PaymentMethod { get; set; } = 1;

        /// <summary>收款方</summary>
        [SugarColumn(ColumnName = "payee", ColumnDescription = "收款方", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Payee { get; set; }

        /// <summary>收款方类型(1=客户 2=供应商 3=合作伙伴 4=其他)</summary>
        [SugarColumn(ColumnName = "payee_type", ColumnDescription = "收款方类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PayeeType { get; set; } = 1;

        /// <summary>收款方银行</summary>
        [SugarColumn(ColumnName = "payee_bank", ColumnDescription = "收款方银行", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PayeeBank { get; set; }

        /// <summary>收款方账号</summary>
        [SugarColumn(ColumnName = "payee_account", ColumnDescription = "收款方账号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PayeeAccount { get; set; }

        /// <summary>收款方开户行</summary>
        [SugarColumn(ColumnName = "payee_bank_branch", ColumnDescription = "收款方开户行", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PayeeBankBranch { get; set; }

        /// <summary>计划付款日期</summary>
        [SugarColumn(ColumnName = "planned_payment_date", ColumnDescription = "计划付款日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedPaymentDate { get; set; }

        /// <summary>实际付款日期</summary>
        [SugarColumn(ColumnName = "actual_payment_date", ColumnDescription = "实际付款日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualPaymentDate { get; set; }

        /// <summary>付款申请日期</summary>
        [SugarColumn(ColumnName = "payment_application_date", ColumnDescription = "付款申请日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PaymentApplicationDate { get; set; } = DateTime.Now;

        /// <summary>付款优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "payment_priority", ColumnDescription = "付款优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int PaymentPriority { get; set; } = 2;

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>付款部门</summary>
        [SugarColumn(ColumnName = "payment_department", ColumnDescription = "付款部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentDepartment { get; set; }

        /// <summary>付款申请人</summary>
        [SugarColumn(ColumnName = "payment_applicant", ColumnDescription = "付款申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentApplicant { get; set; }

        /// <summary>付款申请人电话</summary>
        [SugarColumn(ColumnName = "payment_applicant_phone", ColumnDescription = "付款申请人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentApplicantPhone { get; set; }

        /// <summary>付款申请人邮箱</summary>
        [SugarColumn(ColumnName = "payment_applicant_email", ColumnDescription = "付款申请人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentApplicantEmail { get; set; }

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

        /// <summary>财务审核人</summary>
        [SugarColumn(ColumnName = "finance_reviewer", ColumnDescription = "财务审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FinanceReviewer { get; set; }

        /// <summary>财务审核日期</summary>
        [SugarColumn(ColumnName = "finance_review_date", ColumnDescription = "财务审核日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FinanceReviewDate { get; set; }

        /// <summary>财务审核意见</summary>
        [SugarColumn(ColumnName = "finance_review_comment", ColumnDescription = "财务审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FinanceReviewComment { get; set; }

        /// <summary>付款执行人</summary>
        [SugarColumn(ColumnName = "payment_executor", ColumnDescription = "付款执行人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentExecutor { get; set; }

        /// <summary>付款执行日期</summary>
        [SugarColumn(ColumnName = "payment_execution_date", ColumnDescription = "付款执行日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PaymentExecutionDate { get; set; }

        /// <summary>付款凭证号</summary>
        [SugarColumn(ColumnName = "payment_voucher_number", ColumnDescription = "付款凭证号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentVoucherNumber { get; set; }

        /// <summary>付款凭证</summary>
        [SugarColumn(ColumnName = "payment_voucher", ColumnDescription = "付款凭证", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentVoucher { get; set; }

        /// <summary>付款备注</summary>
        [SugarColumn(ColumnName = "payment_remark", ColumnDescription = "付款备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentRemark { get; set; }

        /// <summary>合同付款状态(0=草稿 1=待审核 2=已审核 3=已批准 4=财务审核中 5=已付款 6=已拒绝 7=已取消)</summary>
        [SugarColumn(ColumnName = "contract_payment_status", ColumnDescription = "合同付款状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContractPaymentStatus { get; set; } = 0;

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
        [SugarColumn(ColumnName = "contract_payment_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractPaymentRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



