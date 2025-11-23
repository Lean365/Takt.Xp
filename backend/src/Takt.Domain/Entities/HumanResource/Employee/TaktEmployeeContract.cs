// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeContract.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeContract 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Employee;

/// <summary>
/// 员工合同实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-23
/// 功能说明:
/// 1. 存储员工合同信息
/// 2. 支持合同生命周期管理
/// 3. 关联员工和合同类型
/// </remarks>
[SugarTable("takt_humanresource_employee_contract", "员工合同表")]
[SugarIndex("ix_employee_contract_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_employee_contract_type", nameof(ContractTypeId), OrderByType.Asc)]
[SugarIndex("ix_employee_contract_status", nameof(Status), OrderByType.Asc)]
public class TaktEmployeeContract : TaktBaseEntity
{
    /// <summary>
    /// 合同编号
    /// </summary>
    [SugarColumn(ColumnName = "contract_no", ColumnDescription = "合同编号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ContractNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 合同类型ID
    /// </summary>
    [SugarColumn(ColumnName = "contract_type_id", ColumnDescription = "合同类型ID", ColumnDataType = "bigint", IsNullable = false)]
    public long ContractTypeId { get; set; }

    /// <summary>
    /// 合同开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "合同开始日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 合同结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "合同结束日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 试用期开始日期
    /// </summary>
    [SugarColumn(ColumnName = "probation_start_date", ColumnDescription = "试用期开始日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ProbationStartDate { get; set; }

    /// <summary>
    /// 试用期结束日期
    /// </summary>
    [SugarColumn(ColumnName = "probation_end_date", ColumnDescription = "试用期结束日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ProbationEndDate { get; set; }

    /// <summary>
    /// 试用期月数
    /// </summary>
    [SugarColumn(ColumnName = "probation_months", ColumnDescription = "试用期月数", ColumnDataType = "int", IsNullable = true)]
    public int? ProbationMonths { get; set; }

    /// <summary>
    /// 合同状态(1=生效 2=试用期 3=到期 4=终止 5=续签)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "合同状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }

    /// <summary>
    /// 基本工资
    /// </summary>
    [SugarColumn(ColumnName = "base_salary", ColumnDescription = "基本工资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? BaseSalary { get; set; }

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
    /// 合同文件路径
    /// </summary>
    [SugarColumn(ColumnName = "contract_file", ColumnDescription = "合同文件路径", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ContractFile { get; set; }

    /// <summary>
    /// 合同备注
    /// </summary>
    [SugarColumn(ColumnName = "contract_remark", ColumnDescription = "合同备注", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ContractRemark { get; set; }

    /// <summary>
    /// 终止原因
    /// </summary>
    [SugarColumn(ColumnName = "termination_reason", ColumnDescription = "终止原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? TerminationReason { get; set; }

    /// <summary>
    /// 终止日期
    /// </summary>
    [SugarColumn(ColumnName = "termination_date", ColumnDescription = "终止日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? TerminationDate { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual TaktEmployee? Employee { get; set; }

}




