// -----------------------------------------------------------------------------
// <copyright file="TaktLeave.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Leave 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Leave;

/// <summary>
/// 请假记录实体
/// </summary>
[SugarTable("takt_humanresource_leave", "请假记录表")]
[SugarIndex("ix_leave_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_leave_type", nameof(LeaveTypeId), OrderByType.Asc)]
[SugarIndex("ix_leave_status", nameof(Status), OrderByType.Asc)]
public class TaktLeave : TaktBaseEntity
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 请假编号
    /// </summary>
    [SugarColumn(ColumnName = "leave_no", ColumnDescription = "请假编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string LeaveNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 请假类型ID
    /// </summary>
    [SugarColumn(ColumnName = "leave_type_id", ColumnDescription = "请假类型ID", ColumnDataType = "bigint", IsNullable = false)]
    public long LeaveTypeId { get; set; }

    /// <summary>
    /// 请假开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "请假开始时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 请假结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "请假结束时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 请假天数
    /// </summary>
    [SugarColumn(ColumnName = "leave_days", ColumnDescription = "请假天数", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal LeaveDays { get; set; }

    /// <summary>
    /// 请假原因
    /// </summary>
    [SugarColumn(ColumnName = "leave_reason", ColumnDescription = "请假原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? LeaveReason { get; set; }

    /// <summary>
    /// 请假状态(1=待审批 2=已批准 3=已拒绝 4=已取消)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "请假状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
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
    public virtual Employee.TaktEmployee? Employee { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
    public virtual Employee.TaktEmployee? Approver { get; set; }
}



