// -----------------------------------------------------------------------------
// <copyright file="TaktExpense.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2024-12-01</date>
// <description>Expense 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 费用报销记录实体
/// </summary>
[SugarTable("Takt_accounting_controlling_expense", "费用报销记录表")]
[SugarIndex("ix_expense_no", nameof(ExpenseNo), OrderByType.Asc)]
[SugarIndex("ix_expense_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_expense_status", nameof(Status), OrderByType.Asc)]
public class TaktExpense : TaktBaseEntity
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
    /// 费用报销编号
    /// </summary>
    [SugarColumn(ColumnName = "expense_no", ColumnDescription = "费用报销编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string ExpenseNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 费用类型ID
    /// </summary>
    [SugarColumn(ColumnName = "expense_type_id", ColumnDescription = "费用类型ID", ColumnDataType = "bigint", IsNullable = false)]
    public long ExpenseTypeId { get; set; }

    /// <summary>
    /// 费用总金额
    /// </summary>
    [SugarColumn(ColumnName = "total_amount", ColumnDescription = "费用总金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// 费用日期
    /// </summary>
    [SugarColumn(ColumnName = "expense_date", ColumnDescription = "费用日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ExpenseDate { get; set; }

    /// <summary>
    /// 费用事由
    /// </summary>
    [SugarColumn(ColumnName = "expense_reason", ColumnDescription = "费用事由", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ExpenseReason { get; set; }

    /// <summary>
    /// 付款方式(1=现金 2=银行转账 3=信用卡 4=其他)
    /// </summary>
    [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PaymentMethod { get; set; }

    /// <summary>
    /// 费用状态(1=待审批 2=已批准 3=已拒绝 4=已取消)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "费用状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }

    /// <summary>
    /// 审批人ID
    /// </summary>
    [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? ApproverId { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    [SugarColumn(ColumnName = "approve_time", ColumnDescription = "审批时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApproveTime { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    [SugarColumn(ColumnName = "approve_comment", ColumnDescription = "审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ApproveComment { get; set; }

    /// <summary>
    /// 附件数量
    /// </summary>
    [SugarColumn(ColumnName = "attachment_count", ColumnDescription = "附件数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AttachmentCount { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "notes", ColumnDescription = "备注", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? Notes { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual HumanResource.Employee.TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
    public virtual HumanResource.Employee.TaktEmployee? Approver { get; set; }
}


