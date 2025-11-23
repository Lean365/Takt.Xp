// -----------------------------------------------------------------------------
// <copyright file="TaktAttendance.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Attendance 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Attendance;

/// <summary>
/// 考勤记录实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-23
/// 功能说明:
/// 1. 存储员工考勤记录
/// 2. 支持多种考勤方式
/// 3. 关联员工和考勤规则
/// </remarks>
[SugarTable("takt_humanresource_attendance", "考勤记录表")]
[SugarIndex("ix_attendance_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_attendance_date", nameof(AttendanceDate), OrderByType.Asc)]
[SugarIndex("ix_attendance_type", nameof(AttendanceType), OrderByType.Asc)]
public class TaktAttendance : TaktBaseEntity
{
    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 考勤日期
    /// </summary>
    [SugarColumn(ColumnName = "attendance_date", ColumnDescription = "考勤日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime AttendanceDate { get; set; }

    /// <summary>
    /// 考勤类型(1=上班 2=下班 3=外出 4=出差 5=请假 6=加班)
    /// </summary>
    [SugarColumn(ColumnName = "attendance_type", ColumnDescription = "考勤类型", ColumnDataType = "int", IsNullable = false)]
    public int AttendanceType { get; set; }

    /// <summary>
    /// 签到时间
    /// </summary>
    [SugarColumn(ColumnName = "check_in_time", ColumnDescription = "签到时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? CheckInTime { get; set; }

    /// <summary>
    /// 签退时间
    /// </summary>
    [SugarColumn(ColumnName = "check_out_time", ColumnDescription = "签退时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? CheckOutTime { get; set; }

    /// <summary>
    /// 工作时长(小时)
    /// </summary>
    [SugarColumn(ColumnName = "work_hours", ColumnDescription = "工作时长", ColumnDataType = "decimal(5,2)", IsNullable = true)]
    public decimal? WorkHours { get; set; }

    /// <summary>
    /// 加班时长(小时)
    /// </summary>
    [SugarColumn(ColumnName = "overtime_hours", ColumnDescription = "加班时长", ColumnDataType = "decimal(5,2)", IsNullable = true)]
    public decimal? OvertimeHours { get; set; }

    /// <summary>
    /// 迟到时长(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "late_minutes", ColumnDescription = "迟到时长", ColumnDataType = "int", IsNullable = true)]
    public int? LateMinutes { get; set; }

    /// <summary>
    /// 早退时长(分钟)
    /// </summary>
    [SugarColumn(ColumnName = "early_leave_minutes", ColumnDescription = "早退时长", ColumnDataType = "int", IsNullable = true)]
    public int? EarlyLeaveMinutes { get; set; }

    /// <summary>
    /// 缺勤时长(小时)
    /// </summary>
    [SugarColumn(ColumnName = "absent_hours", ColumnDescription = "缺勤时长", ColumnDataType = "decimal(5,2)", IsNullable = true)]
    public decimal? AbsentHours { get; set; }

    /// <summary>
    /// 考勤状态(1=正常 2=迟到 3=早退 4=缺勤 5=请假 6=出差 7=加班)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "考勤状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }

    /// <summary>
    /// 考勤地点
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "考勤地点", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Location { get; set; }

    /// <summary>
    /// 考勤设备
    /// </summary>
    [SugarColumn(ColumnName = "device", ColumnDescription = "考勤设备", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Device { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }
}


