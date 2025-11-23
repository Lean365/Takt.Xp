// -----------------------------------------------------------------------------
// <copyright file="TaktOvertime.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Overtime 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Attendance;

/// <summary>
/// 加班记录实体
/// </summary>
[SugarTable("takt_humanresource_overtime", "加班记录表")]
[SugarIndex("ix_overtime_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_overtime_status", nameof(Status), OrderByType.Asc)]
public class TaktOvertime : TaktBaseEntity
{
    /// <summary>
    /// 加班编号
    /// </summary>
    [SugarColumn(ColumnName = "overtime_no", ColumnDescription = "加班编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string OvertimeNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 加班类型(1=工作日加班 2=休息日加班 3=节假日加班)
    /// </summary>
    [SugarColumn(ColumnName = "overtime_type", ColumnDescription = "加班类型", ColumnDataType = "int", IsNullable = false)]
    public int OvertimeType { get; set; }

    /// <summary>
    /// 加班开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "加班开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 加班结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "加班结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 加班时长(小时)
    /// </summary>
    [SugarColumn(ColumnName = "overtime_hours", ColumnDescription = "加班时长", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal OvertimeHours { get; set; }

    /// <summary>
    /// 加班原因
    /// </summary>
    [SugarColumn(ColumnName = "overtime_reason", ColumnDescription = "加班原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? OvertimeReason { get; set; }

    /// <summary>
    /// 加班状态(1=待审批 2=已批准 3=已拒绝 4=已取消)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "加班状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
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
    /// 加班补偿方式(1=调休 2=加班费 3=调休+加班费)
    /// </summary>
    [SugarColumn(ColumnName = "compensation_type", ColumnDescription = "加班补偿方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int CompensationType { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
    public virtual Employee.TaktEmployee? Approver { get; set; }
}



