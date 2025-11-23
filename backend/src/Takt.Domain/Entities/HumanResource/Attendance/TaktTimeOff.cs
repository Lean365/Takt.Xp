// -----------------------------------------------------------------------------
// <copyright file="TaktTimeOff.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>TimeOff 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Attendance;

/// <summary>
/// 调休实体
/// </summary>
[SugarTable("takt_humanresource_time_off", "调休表")]
[SugarIndex("ix_time_off_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_time_off_status", nameof(Status), OrderByType.Asc)]
[SugarIndex("ix_time_off_scope", nameof(ApplyScope), OrderByType.Asc)]
public class TaktTimeOff : TaktBaseEntity
{
    /// <summary>
    /// 调休编号
    /// </summary>
    [SugarColumn(ColumnName = "time_off_no", ColumnDescription = "调休编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string TimeOffNo { get; set; } = string.Empty;

    /// <summary>
    /// 调休名称
    /// </summary>
    [SugarColumn(ColumnName = "time_off_name", ColumnDescription = "调休名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string TimeOffName { get; set; } = string.Empty;

    /// <summary>
    /// 调休类型(1=加班调休 2=节假日调休 3=周末调休 4=其他调休)
    /// </summary>
    [SugarColumn(ColumnName = "time_off_type", ColumnDescription = "调休类型", ColumnDataType = "int", IsNullable = false)]
    public int TimeOffType { get; set; }

    /// <summary>
    /// 适用范围(1=全体员工 2=指定部门 3=指定员工)
    /// </summary>
    [SugarColumn(ColumnName = "apply_scope", ColumnDescription = "适用范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int ApplyScope { get; set; }

    /// <summary>
    /// 员工ID(当适用范围为指定员工时使用)
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 适用部门ID列表(逗号分隔，当适用范围为指定部门时使用)
    /// </summary>
    [SugarColumn(ColumnName = "apply_departments", ColumnDescription = "适用部门ID列表", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ApplyDepartments { get; set; }

    /// <summary>
    /// 适用员工ID列表(逗号分隔，当适用范围为指定员工时使用)
    /// </summary>
    [SugarColumn(ColumnName = "apply_employees", ColumnDescription = "适用员工ID列表", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ApplyEmployees { get; set; }

    /// <summary>
    /// 调休开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "调休开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 调休结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "调休结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 调休时长(小时)
    /// </summary>
    [SugarColumn(ColumnName = "time_off_hours", ColumnDescription = "调休时长", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal TimeOffHours { get; set; }

    /// <summary>
    /// 调休原因
    /// </summary>
    [SugarColumn(ColumnName = "time_off_reason", ColumnDescription = "调休原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? TimeOffReason { get; set; }

    /// <summary>
    /// 调休状态(1=待审批 2=已批准 3=已拒绝 4=已生效 5=已过期 6=已取消)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "调休状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
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
    /// 关联加班记录ID
    /// </summary>
    [SugarColumn(ColumnName = "overtime_id", ColumnDescription = "关联加班记录ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? OvertimeId { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    [SugarColumn(ColumnName = "expire_time", ColumnDescription = "过期时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ExpireTime { get; set; }

    /// <summary>
    /// 发布人ID
    /// </summary>
    [SugarColumn(ColumnName = "publisher_id", ColumnDescription = "发布人ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? PublisherId { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    [SugarColumn(ColumnName = "publish_time", ColumnDescription = "发布时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PublishTime { get; set; }


    /// <summary>
    /// 员工信息(当适用范围为指定员工时)
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
    public virtual Employee.TaktEmployee? Approver { get; set; }

    /// <summary>
    /// 发布人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(PublisherId))]
    public virtual Employee.TaktEmployee? Publisher { get; set; }

    /// <summary>
    /// 关联加班记录
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(OvertimeId))]
    public virtual TaktOvertime? Overtime { get; set; }
}



