// -----------------------------------------------------------------------------
// <copyright file="TaktPersonalTax.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>PersonalTax 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Salary;

/// <summary>
/// 个税实体
/// </summary>
[SugarTable("takt_humanresource_personal_tax", "个税表")]
[SugarIndex("ix_personal_tax_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_personal_tax_month", nameof(TaxMonth), OrderByType.Asc)]
public class TaktPersonalTax : TaktBaseEntity
{
    /// <summary>
    /// 个税编号
    /// </summary>
    [SugarColumn(ColumnName = "tax_no", ColumnDescription = "个税编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string TaxNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 个税年月(如202406)
    /// </summary>
    [SugarColumn(ColumnName = "tax_month", ColumnDescription = "个税年月", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
    public string TaxMonth { get; set; } = string.Empty;

    /// <summary>
    /// 应纳税所得额
    /// </summary>
    [SugarColumn(ColumnName = "taxable_income", ColumnDescription = "应纳税所得额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal TaxableIncome { get; set; }

    /// <summary>
    /// 适用税率(%)
    /// </summary>
    [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "适用税率", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal TaxRate { get; set; }

    /// <summary>
    /// 速算扣除数
    /// </summary>
    [SugarColumn(ColumnName = "quick_deduction", ColumnDescription = "速算扣除数", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal QuickDeduction { get; set; }

    /// <summary>
    /// 应纳税额
    /// </summary>
    [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "应纳税额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// 已预缴税额
    /// </summary>
    [SugarColumn(ColumnName = "prepaid_tax", ColumnDescription = "已预缴税额", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? PrepaidTax { get; set; }

    /// <summary>
    /// 应补(退)税额
    /// </summary>
    [SugarColumn(ColumnName = "tax_difference", ColumnDescription = "应补(退)税额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal TaxDifference { get; set; }

    /// <summary>
    /// 工资薪金所得
    /// </summary>
    [SugarColumn(ColumnName = "salary_income", ColumnDescription = "工资薪金所得", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal SalaryIncome { get; set; }

    /// <summary>
    /// 专项扣除(社保公积金)
    /// </summary>
    [SugarColumn(ColumnName = "special_deduction", ColumnDescription = "专项扣除", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal SpecialDeduction { get; set; }

    /// <summary>
    /// 专项附加扣除
    /// </summary>
    [SugarColumn(ColumnName = "additional_deduction", ColumnDescription = "专项附加扣除", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? AdditionalDeduction { get; set; }

    /// <summary>
    /// 其他扣除
    /// </summary>
    [SugarColumn(ColumnName = "other_deduction", ColumnDescription = "其他扣除", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? OtherDeduction { get; set; }

    /// <summary>
    /// 累计应纳税所得额
    /// </summary>
    [SugarColumn(ColumnName = "cumulative_taxable_income", ColumnDescription = "累计应纳税所得额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal CumulativeTaxableIncome { get; set; }

    /// <summary>
    /// 累计应纳税额
    /// </summary>
    [SugarColumn(ColumnName = "cumulative_tax_amount", ColumnDescription = "累计应纳税额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal CumulativeTaxAmount { get; set; }

    /// <summary>
    /// 累计已预缴税额
    /// </summary>
    [SugarColumn(ColumnName = "cumulative_prepaid_tax", ColumnDescription = "累计已预缴税额", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? CumulativePrepaidTax { get; set; }

    /// <summary>
    /// 个税状态(1=未申报 2=已申报 3=已缴纳 4=已退税)
    /// </summary>
    [SugarColumn(ColumnName = "tax_status", ColumnDescription = "个税状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TaxStatus { get; set; }

    /// <summary>
    /// 申报时间
    /// </summary>
    [SugarColumn(ColumnName = "declare_time", ColumnDescription = "申报时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DeclareTime { get; set; }

    /// <summary>
    /// 缴纳时间
    /// </summary>
    [SugarColumn(ColumnName = "payment_time", ColumnDescription = "缴纳时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PaymentTime { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }
}


