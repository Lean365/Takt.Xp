// -----------------------------------------------------------------------------
// <copyright file="TaktPerformance.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Performance 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Performance;

/// <summary>
/// 绩效考核实体
/// </summary>
[SugarTable("takt_humanresource_performance", "绩效考核表")]
[SugarIndex("ix_performance_employee", nameof(EmployeeId), OrderByType.Asc)]
public class TaktPerformance : TaktBaseEntity
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 考核周期(如2024Q1)
    /// </summary>
    [SugarColumn(ColumnName = "period", ColumnDescription = "考核周期", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string Period { get; set; } = string.Empty;

    /// <summary>
    /// 绩效分数
    /// </summary>
    [SugarColumn(ColumnName = "score", ColumnDescription = "绩效分数", ColumnDataType = "decimal(5,2)", IsNullable = true)]
    public decimal? Score { get; set; }

    /// <summary>
    /// 绩效等级
    /// </summary>
    [SugarColumn(ColumnName = "grade", ColumnDescription = "绩效等级", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Grade { get; set; }

    /// <summary>
    /// 评语
    /// </summary>
    [SugarColumn(ColumnName = "comment", ColumnDescription = "评语", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Comment { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }
}



