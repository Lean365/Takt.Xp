﻿// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeResignation.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeResignation 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Employee;

/// <summary>
/// 员工离职实体
/// </summary>
[SugarTable("takt_humanresource_employee_resignation", "员工离职表")]
[SugarIndex("ix_employee_resignation_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_employee_resignation_status", nameof(Status), OrderByType.Asc)]
public class TaktEmployeeResignation : TaktBaseEntity
{
    /// <summary>
    /// 离职编号
    /// </summary>
    [SugarColumn(ColumnName = "resignation_no", ColumnDescription = "离职编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string ResignationNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 离职类型(1=主动离职 2=被动离职 3=退休 4=合同到期)
    /// </summary>
    [SugarColumn(ColumnName = "resignation_type", ColumnDescription = "离职类型", ColumnDataType = "int", IsNullable = false)]
    public int ResignationType { get; set; }

    /// <summary>
    /// 申请离职日期
    /// </summary>
    [SugarColumn(ColumnName = "apply_date", ColumnDescription = "申请离职日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ApplyDate { get; set; }

    /// <summary>
    /// 计划离职日期
    /// </summary>
    [SugarColumn(ColumnName = "planned_leave_date", ColumnDescription = "计划离职日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime PlannedLeaveDate { get; set; }

    /// <summary>
    /// 实际离职日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_leave_date", ColumnDescription = "实际离职日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ActualLeaveDate { get; set; }

    /// <summary>
    /// 离职原因
    /// </summary>
    [SugarColumn(ColumnName = "resignation_reason", ColumnDescription = "离职原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ResignationReason { get; set; }

    /// <summary>
    /// 离职状态(1=待审批 2=已批准 3=已拒绝 4=已离职)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "离职状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
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
    /// 工作交接情况
    /// </summary>
    [SugarColumn(ColumnName = "handover_situation", ColumnDescription = "工作交接情况", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? HandoverSituation { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
    public virtual TaktEmployee? Approver { get; set; }
}



