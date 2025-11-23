// -----------------------------------------------------------------------------
// <copyright file="TaktPositionApplication.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>PositionApplication 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Recruitment
{
    /// <summary>
    /// 职位申请实体（内部职位调动申请）
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 存储员工内部职位调动申请信息
    /// 2. 支持职位调动审批流程
    /// 3. 关联员工、原职位、目标职位信息
    /// </remarks>
    [SugarTable("takt_humanresource_position_application", "职位申请表")]
    [SugarIndex("ix_position_application_code", nameof(ApplicationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_position_application_employee", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("ix_position_application_current_position", nameof(CurrentPositionId), OrderByType.Asc)]
    [SugarIndex("ix_position_application_target_position", nameof(TargetPositionId), OrderByType.Asc)]
    [SugarIndex("ix_position_application_status", nameof(ApplicationStatus), OrderByType.Asc)]
    [SugarIndex("ix_position_application_date", nameof(ApplicationDate), OrderByType.Desc)]
    public class TaktPositionApplication : TaktBaseEntity
    {
        /// <summary>
        /// 申请编号
        /// </summary>
        [SugarColumn(ColumnName = "application_code", ColumnDescription = "申请编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请员工ID
        /// </summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "申请员工ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 当前职位ID
        /// </summary>
        [SugarColumn(ColumnName = "current_position_id", ColumnDescription = "当前职位ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CurrentPositionId { get; set; }

        /// <summary>
        /// 当前部门ID
        /// </summary>
        [SugarColumn(ColumnName = "current_department_id", ColumnDescription = "当前部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CurrentDepartmentId { get; set; }

        /// <summary>
        /// 目标职位ID
        /// </summary>
        [SugarColumn(ColumnName = "target_position_id", ColumnDescription = "目标职位ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TargetPositionId { get; set; }

        /// <summary>
        /// 目标部门ID
        /// </summary>
        [SugarColumn(ColumnName = "target_department_id", ColumnDescription = "目标部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long TargetDepartmentId { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// 申请类型(1=平级调动 2=晋升 3=降级 4=轮岗)
        /// </summary>
        [SugarColumn(ColumnName = "application_type", ColumnDescription = "申请类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ApplicationType { get; set; }

        /// <summary>
        /// 申请状态(1=已提交 2=部门审批 3=人事审批 4=总经理审批 5=已批准 6=已拒绝 7=已取消)
        /// </summary>
        [SugarColumn(ColumnName = "application_status", ColumnDescription = "申请状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ApplicationStatus { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        [SugarColumn(ColumnName = "application_reason", ColumnDescription = "申请原因", ColumnDataType = "nvarchar", Length = 1000, IsNullable = false)]
        public string ApplicationReason { get; set; } = string.Empty;

        /// <summary>
        /// 个人优势
        /// </summary>
        [SugarColumn(ColumnName = "personal_advantages", ColumnDescription = "个人优势", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
        public string? PersonalAdvantages { get; set; }

        /// <summary>
        /// 工作规划
        /// </summary>
        [SugarColumn(ColumnName = "work_plan", ColumnDescription = "工作规划", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
        public string? WorkPlan { get; set; }

        /// <summary>
        /// 期望生效日期
        /// </summary>
        [SugarColumn(ColumnName = "expected_effective_date", ColumnDescription = "期望生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpectedEffectiveDate { get; set; }

        /// <summary>
        /// 部门审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "department_approver_id", ColumnDescription = "部门审批人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DepartmentApproverId { get; set; }

        /// <summary>
        /// 部门审批时间
        /// </summary>
        [SugarColumn(ColumnName = "department_approval_time", ColumnDescription = "部门审批时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DepartmentApprovalTime { get; set; }

        /// <summary>
        /// 部门审批结果(1=同意 2=拒绝 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "department_approval_result", ColumnDescription = "部门审批结果", ColumnDataType = "int", IsNullable = true)]
        public int? DepartmentApprovalResult { get; set; }

        /// <summary>
        /// 部门审批意见
        /// </summary>
        [SugarColumn(ColumnName = "department_approval_comment", ColumnDescription = "部门审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? DepartmentApprovalComment { get; set; }

        /// <summary>
        /// 人事审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "hr_approver_id", ColumnDescription = "人事审批人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? HrApproverId { get; set; }

        /// <summary>
        /// 人事审批时间
        /// </summary>
        [SugarColumn(ColumnName = "hr_approval_time", ColumnDescription = "人事审批时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? HrApprovalTime { get; set; }

        /// <summary>
        /// 人事审批结果(1=同意 2=拒绝 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "hr_approval_result", ColumnDescription = "人事审批结果", ColumnDataType = "int", IsNullable = true)]
        public int? HrApprovalResult { get; set; }

        /// <summary>
        /// 人事审批意见
        /// </summary>
        [SugarColumn(ColumnName = "hr_approval_comment", ColumnDescription = "人事审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? HrApprovalComment { get; set; }

        /// <summary>
        /// 总经理审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "general_manager_approver_id", ColumnDescription = "总经理审批人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? GeneralManagerApproverId { get; set; }

        /// <summary>
        /// 总经理审批时间
        /// </summary>
        [SugarColumn(ColumnName = "general_manager_approval_time", ColumnDescription = "总经理审批时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? GeneralManagerApprovalTime { get; set; }

        /// <summary>
        /// 总经理审批结果(1=同意 2=拒绝 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "general_manager_approval_result", ColumnDescription = "总经理审批结果", ColumnDataType = "int", IsNullable = true)]
        public int? GeneralManagerApprovalResult { get; set; }

        /// <summary>
        /// 总经理审批意见
        /// </summary>
        [SugarColumn(ColumnName = "general_manager_approval_comment", ColumnDescription = "总经理审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? GeneralManagerApprovalComment { get; set; }

        /// <summary>
        /// 最终审批结果(1=同意 2=拒绝 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "final_approval_result", ColumnDescription = "最终审批结果", ColumnDataType = "int", IsNullable = true)]
        public int? FinalApprovalResult { get; set; }

        /// <summary>
        /// 最终审批时间
        /// </summary>
        [SugarColumn(ColumnName = "final_approval_time", ColumnDescription = "最终审批时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FinalApprovalTime { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        [SugarColumn(ColumnName = "rejection_reason", ColumnDescription = "拒绝原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? RejectionReason { get; set; }

        /// <summary>
        /// 实际生效日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_effective_date", ColumnDescription = "实际生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEffectiveDate { get; set; }


        /// <summary>
        /// 申请员工信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
        public virtual Employee.TaktEmployee? Employee { get; set; }

        /// <summary>
        /// 当前职位信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CurrentPositionId))]
        public virtual Organization.TaktPosition? CurrentPosition { get; set; }

        /// <summary>
        /// 当前部门信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CurrentDepartmentId))]
        public virtual Organization.TaktDepartment? CurrentDepartment { get; set; }

        /// <summary>
        /// 目标职位信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TargetPositionId))]
        public virtual Organization.TaktPosition? TargetPosition { get; set; }

        /// <summary>
        /// 目标部门信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TargetDepartmentId))]
        public virtual Organization.TaktDepartment? TargetDepartment { get; set; }

        /// <summary>
        /// 部门审批人信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DepartmentApproverId))]
        public virtual Employee.TaktEmployee? DepartmentApprover { get; set; }

        /// <summary>
        /// 人事审批人信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(HrApproverId))]
        public virtual Employee.TaktEmployee? HrApprover { get; set; }

        /// <summary>
        /// 总经理审批人信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(GeneralManagerApproverId))]
        public virtual Employee.TaktEmployee? GeneralManagerApprover { get; set; }
    }
}


