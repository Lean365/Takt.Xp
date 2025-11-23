#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectBudgetExpense.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目预算与费用实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目预算与费用实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目预算与费用的相关信息，包括预算信息、费用明细、成本控制等
    /// </remarks>
    [SugarTable("Takt_routine_project_budget_expense", "项目预算与费用表")]
    [SugarIndex("ix_project_budget_expense_code", nameof(ProjectBudgetExpenseCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_budget_expense", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectBudgetExpenseCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_budget_expense_status", nameof(ProjectBudgetExpenseStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_budget_expense_type", nameof(ProjectBudgetExpenseType), OrderByType.Asc, false)]
    public class TaktProjectBudgetExpense : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目预算与费用编号</summary>
        [SugarColumn(ColumnName = "project_budget_expense_code", ColumnDescription = "项目预算与费用编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectBudgetExpenseCode { get; set; } = string.Empty;

        /// <summary>预算或费用名称</summary>
        [SugarColumn(ColumnName = "budget_expense_name", ColumnDescription = "预算或费用名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetExpenseName { get; set; } = string.Empty;

        /// <summary>预算与费用类型(1=人工费用 2=材料费用 3=设备费用 4=外包费用 5=差旅费用 6=办公费用 7=其他费用)</summary>
        [SugarColumn(ColumnName = "project_budget_expense_type", ColumnDescription = "预算与费用类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectBudgetExpenseType { get; set; } = 1;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>关联任务编号</summary>
        [SugarColumn(ColumnName = "related_task_code", ColumnDescription = "关联任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskCode { get; set; }

        /// <summary>关联任务名称</summary>
        [SugarColumn(ColumnName = "related_task_name", ColumnDescription = "关联任务名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskName { get; set; }

        /// <summary>预算或费用描述</summary>
        [SugarColumn(ColumnName = "budget_expense_description", ColumnDescription = "预算或费用描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetExpenseDescription { get; set; }

        /// <summary>预算金额</summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? BudgetAmount { get; set; }

        /// <summary>实际金额</summary>
        [SugarColumn(ColumnName = "actual_amount", ColumnDescription = "实际金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualAmount { get; set; }

        /// <summary>差异金额</summary>
        [SugarColumn(ColumnName = "variance_amount", ColumnDescription = "差异金额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal VarianceAmount { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>汇率</summary>
        [SugarColumn(ColumnName = "exchange_rate", ColumnDescription = "汇率", ColumnDataType = "decimal(10,4)", IsNullable = false, DefaultValue = "1")]
        public decimal ExchangeRate { get; set; } = 1;

        /// <summary>预算周期(1=月度 2=季度 3=年度 4=项目周期)</summary>
        [SugarColumn(ColumnName = "budget_period", ColumnDescription = "预算周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int BudgetPeriod { get; set; } = 1;

        /// <summary>预算开始日期</summary>
        [SugarColumn(ColumnName = "budget_start_date", ColumnDescription = "预算开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? BudgetStartDate { get; set; }

        /// <summary>预算结束日期</summary>
        [SugarColumn(ColumnName = "budget_end_date", ColumnDescription = "预算结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? BudgetEndDate { get; set; }

        /// <summary>费用发生日期</summary>
        [SugarColumn(ColumnName = "expense_date", ColumnDescription = "费用发生日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpenseDate { get; set; }

        /// <summary>费用发生时间</summary>
        [SugarColumn(ColumnName = "expense_time", ColumnDescription = "费用发生时间", ColumnDataType = "time", IsNullable = true)]
        public TimeSpan? ExpenseTime { get; set; }

        /// <summary>费用发生地点</summary>
        [SugarColumn(ColumnName = "expense_location", ColumnDescription = "费用发生地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseLocation { get; set; }

        /// <summary>费用类别</summary>
        [SugarColumn(ColumnName = "expense_category", ColumnDescription = "费用类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseCategory { get; set; }

        /// <summary>费用子类别</summary>
        [SugarColumn(ColumnName = "expense_subcategory", ColumnDescription = "费用子类别", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseSubcategory { get; set; }

        /// <summary>费用项目</summary>
        [SugarColumn(ColumnName = "expense_item", ColumnDescription = "费用项目", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseItem { get; set; }

        /// <summary>费用数量</summary>
        [SugarColumn(ColumnName = "expense_quantity", ColumnDescription = "费用数量", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ExpenseQuantity { get; set; }

        /// <summary>费用单位</summary>
        [SugarColumn(ColumnName = "expense_unit", ColumnDescription = "费用单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseUnit { get; set; }

        /// <summary>费用单价</summary>
        [SugarColumn(ColumnName = "expense_unit_price", ColumnDescription = "费用单价", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ExpenseUnitPrice { get; set; }

        /// <summary>费用税率(%)</summary>
        [SugarColumn(ColumnName = "expense_tax_rate", ColumnDescription = "费用税率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ExpenseTaxRate { get; set; } = 0;

        /// <summary>费用税额</summary>
        [SugarColumn(ColumnName = "expense_tax_amount", ColumnDescription = "费用税额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ExpenseTaxAmount { get; set; } = 0;

        /// <summary>费用不含税金额</summary>
        [SugarColumn(ColumnName = "expense_amount_excluding_tax", ColumnDescription = "费用不含税金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ExpenseAmountExcludingTax { get; set; }

        /// <summary>费用含税金额</summary>
        [SugarColumn(ColumnName = "expense_amount_including_tax", ColumnDescription = "费用含税金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ExpenseAmountIncludingTax { get; set; }

        /// <summary>供应商名称</summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName { get; set; }

        /// <summary>供应商编号</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierCode { get; set; }

        /// <summary>供应商联系人</summary>
        [SugarColumn(ColumnName = "supplier_contact", ColumnDescription = "供应商联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierContact { get; set; }

        /// <summary>供应商联系电话</summary>
        [SugarColumn(ColumnName = "supplier_phone", ColumnDescription = "供应商联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierPhone { get; set; }

        /// <summary>发票号码</summary>
        [SugarColumn(ColumnName = "invoice_number", ColumnDescription = "发票号码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InvoiceNumber { get; set; }

        /// <summary>发票日期</summary>
        [SugarColumn(ColumnName = "invoice_date", ColumnDescription = "发票日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? InvoiceDate { get; set; }

        /// <summary>付款方式(1=现金 2=银行转账 3=支票 4=信用卡 5=其他)</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int PaymentMethod { get; set; } = 2;

        /// <summary>付款状态(0=未付款 1=部分付款 2=已付款 3=已退款)</summary>
        [SugarColumn(ColumnName = "payment_status", ColumnDescription = "付款状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PaymentStatus { get; set; } = 0;

        /// <summary>付款日期</summary>
        [SugarColumn(ColumnName = "payment_date", ColumnDescription = "付款日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PaymentDate { get; set; }

        /// <summary>费用申请人</summary>
        [SugarColumn(ColumnName = "expense_applicant", ColumnDescription = "费用申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseApplicant { get; set; }

        /// <summary>费用申请日期</summary>
        [SugarColumn(ColumnName = "expense_application_date", ColumnDescription = "费用申请日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExpenseApplicationDate { get; set; }

        /// <summary>费用审批人</summary>
        [SugarColumn(ColumnName = "expense_approver", ColumnDescription = "费用审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseApprover { get; set; }

        /// <summary>费用审批日期</summary>
        [SugarColumn(ColumnName = "expense_approval_date", ColumnDescription = "费用审批日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExpenseApprovalDate { get; set; }

        /// <summary>费用审批意见</summary>
        [SugarColumn(ColumnName = "expense_approval_comment", ColumnDescription = "费用审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseApprovalComment { get; set; }

        /// <summary>费用报销人</summary>
        [SugarColumn(ColumnName = "expense_reimburser", ColumnDescription = "费用报销人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseReimburser { get; set; }

        /// <summary>费用报销日期</summary>
        [SugarColumn(ColumnName = "expense_reimbursement_date", ColumnDescription = "费用报销日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExpenseReimbursementDate { get; set; }

        /// <summary>成本中心</summary>
        [SugarColumn(ColumnName = "cost_center", ColumnDescription = "成本中心", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenter { get; set; }

        /// <summary>成本科目</summary>
        [SugarColumn(ColumnName = "cost_account", ColumnDescription = "成本科目", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostAccount { get; set; }

        /// <summary>是否可报销(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_reimbursable", ColumnDescription = "是否可报销", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsReimbursable { get; set; } = 1;

        /// <summary>是否已报销(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_reimbursed", ColumnDescription = "是否已报销", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsReimbursed { get; set; } = 0;

        /// <summary>相关附件</summary>
        [SugarColumn(ColumnName = "related_attachments", ColumnDescription = "相关附件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedAttachments { get; set; }

        /// <summary>项目预算与费用状态(0=草稿 1=待审批 2=已审批 3=已执行 4=已关闭 5=已取消)</summary>
        [SugarColumn(ColumnName = "project_budget_expense_status", ColumnDescription = "项目预算与费用状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectBudgetExpenseStatus { get; set; } = 0;

        /// <summary>预算与费用备注</summary>
        [SugarColumn(ColumnName = "budget_expense_remark", ColumnDescription = "预算与费用备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetExpenseRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



