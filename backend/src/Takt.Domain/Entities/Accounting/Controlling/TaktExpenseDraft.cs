#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExpenseDraft.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 费用签拟单实体类 (基于SAP CO费用管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;

namespace Takt.Domain.Entities.Accounting.Controlling
{
    /// <summary>
    /// 费用签拟单实体类 (基于SAP CO费用管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP CO-OM-CCA 成本中心会计
    /// </remarks>
    [SugarTable("Takt_accounting_controlling_expense_draft", "费用签拟单")]
    [SugarIndex("ix_expense_draft_code", nameof(DocNo), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktExpenseDraft : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 单据编号
        /// </summary>
        [SugarColumn(ColumnName = "doc_no", ColumnDescription = "单据编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocNo { get; set; } = string.Empty;

        /// <summary>
        /// 申请人ID
        /// </summary>
        [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "申请人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ApplicantId { get; set; }

        /// <summary>
        /// 申请人编码
        /// </summary>
        [SugarColumn(ColumnName = "applicant_code", ColumnDescription = "申请人编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ApplicantCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请人姓名
        /// </summary>
        [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "申请人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ApplicantName { get; set; } = string.Empty;

        /// <summary>
        /// 申请部门ID
        /// </summary>
        [SugarColumn(ColumnName = "department_id", ColumnDescription = "申请部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 申请部门编码
        /// </summary>
        [SugarColumn(ColumnName = "department_code", ColumnDescription = "申请部门编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请部门名称
        /// </summary>
        [SugarColumn(ColumnName = "department_name", ColumnDescription = "申请部门名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 申请日期
        /// </summary>
        [SugarColumn(ColumnName = "apply_date", ColumnDescription = "申请日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 费用类型
        /// </summary>
        [SugarColumn(ColumnName = "expense_type", ColumnDescription = "费用类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ExpenseType { get; set; } = string.Empty;

        /// <summary>
        /// 费用子类型
        /// </summary>
        [SugarColumn(ColumnName = "expense_sub_type", ColumnDescription = "费用子类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExpenseSubType { get; set; }

        /// <summary>
        /// 费用金额
        /// </summary>
        [SugarColumn(ColumnName = "expense_amount", ColumnDescription = "费用金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal ExpenseAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// 汇率
        /// </summary>
        [SugarColumn(ColumnName = "exchange_rate", ColumnDescription = "汇率", ColumnDataType = "decimal(10,4)", IsNullable = true)]
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// 本币金额
        /// </summary>
        [SugarColumn(ColumnName = "local_amount", ColumnDescription = "本币金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? LocalAmount { get; set; }

        /// <summary>
        /// 用途说明
        /// </summary>
        [SugarColumn(ColumnName = "purpose", ColumnDescription = "用途说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Purpose { get; set; }

        /// <summary>
        /// 成本中心ID
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_id", ColumnDescription = "成本中心ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? CostCenterId { get; set; }

        /// <summary>
        /// 成本中心编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterCode { get; set; }

        /// <summary>
        /// 成本中心名称
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_name", ColumnDescription = "成本中心名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterName { get; set; }

        /// <summary>
        /// 成本要素ID
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_id", ColumnDescription = "成本要素ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? CostElementId { get; set; }

        /// <summary>
        /// 成本要素编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_code", ColumnDescription = "成本要素编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementCode { get; set; }

        /// <summary>
        /// 成本要素名称
        /// </summary>
        [SugarColumn(ColumnName = "cost_element_name", ColumnDescription = "成本要素名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostElementName { get; set; }

        /// <summary>
        /// 预算类型
        /// </summary>
        [SugarColumn(ColumnName = "budget_type", ColumnDescription = "预算类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetType { get; set; }

        /// <summary>
        /// 预算编号
        /// </summary>
        [SugarColumn(ColumnName = "budget_no", ColumnDescription = "预算编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetNo { get; set; }

        /// <summary>
        /// 预算ID
        /// </summary>
        [SugarColumn(ColumnName = "budget_id", ColumnDescription = "预算ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? BudgetId { get; set; }

        /// <summary>
        /// 预算金额
        /// </summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? BudgetAmount { get; set; }

        /// <summary>
        /// 已用预算金额
        /// </summary>
        [SugarColumn(ColumnName = "used_budget_amount", ColumnDescription = "已用预算金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? UsedBudgetAmount { get; set; }

        /// <summary>
        /// 剩余预算金额
        /// </summary>
        [SugarColumn(ColumnName = "remaining_budget_amount", ColumnDescription = "剩余预算金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? RemainingBudgetAmount { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        [SugarColumn(ColumnName = "contract_number", ColumnDescription = "合同编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractNumber { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [SugarColumn(ColumnName = "invoice_number", ColumnDescription = "发票号码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        /// 预计付款日期
        /// </summary>
        [SugarColumn(ColumnName = "expected_payment_date", ColumnDescription = "预计付款日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpectedPaymentDate { get; set; }

        /// <summary>
        /// 实际付款日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_payment_date", ColumnDescription = "实际付款日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualPaymentDate { get; set; }

        /// <summary>
        /// 付款状态(0=未付款 1=已付款 2=部分付款)
        /// </summary>
        [SugarColumn(ColumnName = "payment_status", ColumnDescription = "付款状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PaymentStatus { get; set; }

        /// <summary>
        /// 审批状态(0=草稿 1=已提交 2=已审批 3=已拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovalStatus { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 



