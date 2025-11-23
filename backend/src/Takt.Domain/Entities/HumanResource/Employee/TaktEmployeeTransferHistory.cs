// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeTransferHistory.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeTransferHistory 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Employee
{
    /// <summary>
    /// 员工调岗历史记录实体
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 记录员工调岗历史信息
    /// 2. 支持调岗轨迹追踪
    /// 3. 为调岗决策提供历史依据
    /// </remarks>
    [SugarTable("takt_humanresource_employee_transfer_history", "员工调岗历史表")]
    [SugarIndex("ix_employee_transfer_history_employee", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("ix_employee_transfer_history_date", nameof(TransferDate), OrderByType.Desc)]
    [SugarIndex("ix_employee_transfer_history_type", nameof(TransferType), OrderByType.Asc)]
    public class TaktEmployeeTransferHistory : TaktBaseEntity
    {
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
        /// 调岗日期
        /// </summary>
        [SugarColumn(ColumnName = "transfer_date", ColumnDescription = "调岗日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime TransferDate { get; set; }

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
        /// 调岗原因
        /// </summary>
        [SugarColumn(ColumnName = "transfer_reason", ColumnDescription = "调岗原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? TransferReason { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        [SugarColumn(ColumnName = "approver_name", ColumnDescription = "审批人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string? ApproverName { get; set; }

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
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 员工信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
        public virtual TaktEmployee? Employee { get; set; }

        /// <summary>
        /// 原部门信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(OriginalDepartmentId))]
        public virtual Organization.TaktDepartment? OriginalDepartment { get; set; }

        /// <summary>
        /// 新部门信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NewDepartmentId))]
        public virtual Organization.TaktDepartment? NewDepartment { get; set; }

        /// <summary>
        /// 原职位信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(OriginalPositionId))]
        public virtual Organization.TaktPosition? OriginalPosition { get; set; }

        /// <summary>
        /// 新职位信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NewPositionId))]
        public virtual Organization.TaktPosition? NewPosition { get; set; }

        /// <summary>
        /// 审批人信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
        public virtual TaktEmployee? Approver { get; set; }
    }
} 


