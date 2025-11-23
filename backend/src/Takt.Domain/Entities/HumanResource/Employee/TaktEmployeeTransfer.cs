﻿// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeTransfer.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeTransfer 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Employee;

/// <summary>
/// 员工调岗实体
/// </summary>
[SugarTable("takt_humanresource_employee_transfer", "员工调岗表")]
[SugarIndex("ix_employee_transfer_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_employee_transfer_status", nameof(Status), OrderByType.Asc)]
public class TaktEmployeeTransfer : TaktBaseEntity
{
    /// <summary>
    /// 调岗编号
    /// </summary>
    [SugarColumn(ColumnName = "transfer_no", ColumnDescription = "调岗编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string TransferNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 调岗类型(1=部门调动 2=职位调动 3=工作地点调动)
    /// </summary>
    [SugarColumn(ColumnName = "transfer_type", ColumnDescription = "调岗类型", ColumnDataType = "int", IsNullable = false)]
    public int TransferType { get; set; }

    /// <summary>
    /// 原部门ID
    /// </summary>
    [SugarColumn(ColumnName = "original_department_id", ColumnDescription = "原部门ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? OriginalDepartmentId { get; set; }

    /// <summary>
    /// 新部门ID
    /// </summary>
    [SugarColumn(ColumnName = "new_department_id", ColumnDescription = "新部门ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? NewDepartmentId { get; set; }

    /// <summary>
    /// 原职位ID
    /// </summary>
    [SugarColumn(ColumnName = "original_position_id", ColumnDescription = "原职位ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? OriginalPositionId { get; set; }

    /// <summary>
    /// 新职位ID
    /// </summary>
    [SugarColumn(ColumnName = "new_position_id", ColumnDescription = "新职位ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? NewPositionId { get; set; }

    /// <summary>
    /// 原工作地点
    /// </summary>
    [SugarColumn(ColumnName = "original_work_location", ColumnDescription = "原工作地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OriginalWorkLocation { get; set; }

    /// <summary>
    /// 新工作地点
    /// </summary>
    [SugarColumn(ColumnName = "new_work_location", ColumnDescription = "新工作地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? NewWorkLocation { get; set; }

    /// <summary>
    /// 申请调岗日期
    /// </summary>
    [SugarColumn(ColumnName = "apply_date", ColumnDescription = "申请调岗日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime ApplyDate { get; set; }

    /// <summary>
    /// 计划调岗日期
    /// </summary>
    [SugarColumn(ColumnName = "planned_transfer_date", ColumnDescription = "计划调岗日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime PlannedTransferDate { get; set; }

    /// <summary>
    /// 实际调岗日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_transfer_date", ColumnDescription = "实际调岗日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? ActualTransferDate { get; set; }

    /// <summary>
    /// 调岗原因
    /// </summary>
    [SugarColumn(ColumnName = "transfer_reason", ColumnDescription = "调岗原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? TransferReason { get; set; }

    /// <summary>
    /// 调岗状态(1=待审批 2=已批准 3=已拒绝 4=已调岗)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "调岗状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
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
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 原部门
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(OriginalDepartmentId))]
    public virtual Organization.TaktDepartment? OriginalDepartment { get; set; }

    /// <summary>
    /// 新部门
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(NewDepartmentId))]
    public virtual Organization.TaktDepartment? NewDepartment { get; set; }

    /// <summary>
    /// 原职位
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(OriginalPositionId))]
    public virtual Organization.TaktPosition? OriginalPosition { get; set; }

    /// <summary>
    /// 新职位
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(NewPositionId))]
    public virtual Organization.TaktPosition? NewPosition { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
    public virtual TaktEmployee? Approver { get; set; }
}



