// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeSalary.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeSalary 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Salary;

/// <summary>
/// 员工薪资实体
/// </summary>
[SugarTable("takt_humanresource_employee_salary", "员工薪资表")]
[SugarIndex("ix_employee_salary_employee", nameof(EmployeeId), OrderByType.Asc)]
public class TaktEmployeeSalary : TaktBaseEntity
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 薪资年月(如202406)
    /// </summary>
    [SugarColumn(ColumnName = "salary_month", ColumnDescription = "薪资年月", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
    public string SalaryMonth { get; set; } = string.Empty;

    /// <summary>
    /// 基本工资
    /// </summary>
    [SugarColumn(ColumnName = "base_salary", ColumnDescription = "基本工资", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal BaseSalary { get; set; }

    /// <summary>
    /// 岗位工资
    /// </summary>
    [SugarColumn(ColumnName = "position_salary", ColumnDescription = "岗位工资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? PositionSalary { get; set; }

    /// <summary>
    /// 绩效工资
    /// </summary>
    [SugarColumn(ColumnName = "performance_salary", ColumnDescription = "绩效工资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? PerformanceSalary { get; set; }

    /// <summary>
    /// 津贴/补贴
    /// </summary>
    [SugarColumn(ColumnName = "allowance", ColumnDescription = "津贴/补贴", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? Allowance { get; set; }

    /// <summary>
    /// 奖金
    /// </summary>
    [SugarColumn(ColumnName = "bonus", ColumnDescription = "奖金", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? Bonus { get; set; }

    /// <summary>
    /// 扣款
    /// </summary>
    [SugarColumn(ColumnName = "deduction", ColumnDescription = "扣款", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? Deduction { get; set; }

    /// <summary>
    /// 实发工资
    /// </summary>
    [SugarColumn(ColumnName = "actual_salary", ColumnDescription = "实发工资", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal ActualSalary { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }
}



