// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeBenefit.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeBenefit 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Benefit;

/// <summary>
/// 员工福利实体
/// </summary>
[SugarTable("takt_humanresource_employee_benefit", "员工福利表")]
[SugarIndex("ix_employee_benefit_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_employee_benefit_item", nameof(BenefitItemId), OrderByType.Asc)]
public class TaktEmployeeBenefit : TaktBaseEntity
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    [SugarColumn(ColumnName = "benefit_item_id", ColumnDescription = "福利项目ID", ColumnDataType = "bigint", IsNullable = false)]
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    [SugarColumn(ColumnName = "grant_date", ColumnDescription = "发放日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "amount", ColumnDescription = "金额", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? Amount { get; set; }


    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 福利项目
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(BenefitItemId))]
    public virtual TaktBenefitItem? BenefitItem { get; set; }
}



