// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeePromotion.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeePromotion 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Employee
{
    /// <summary>
    /// 员工晋升实体
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 存储员工晋升申请信息
    /// 2. 支持晋升审批流程管理
    /// 3. 关联职位变更和薪资调整
    /// </remarks>
    [SugarTable("takt_humanresource_employee_promotion", "员工晋升表")]
    [SugarIndex("ix_employee_promotion_employee", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("ix_employee_promotion_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("ix_employee_promotion_type", nameof(PromotionType), OrderByType.Asc)]
    public class TaktEmployeePromotion : TaktBaseEntity
    {
        /// <summary>
        /// 晋升编号
        /// </summary>
        [SugarColumn(ColumnName = "promotion_no", ColumnDescription = "晋升编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
        public string PromotionNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工ID
        /// </summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 晋升类型(1=职位晋升 2=职级晋升 3=薪资晋升 4=综合晋升)
        /// </summary>
        [SugarColumn(ColumnName = "promotion_type", ColumnDescription = "晋升类型", ColumnDataType = "int", IsNullable = false)]
        public int PromotionType { get; set; }

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
        /// 原职级
        /// </summary>
        [SugarColumn(ColumnName = "original_level", ColumnDescription = "原职级", ColumnDataType = "int", IsNullable = true)]
        public int? OriginalLevel { get; set; }

        /// <summary>
        /// 新职级
        /// </summary>
        [SugarColumn(ColumnName = "new_level", ColumnDescription = "新职级", ColumnDataType = "int", IsNullable = true)]
        public int? NewLevel { get; set; }

        /// <summary>
        /// 原基本工资
        /// </summary>
        [SugarColumn(ColumnName = "original_base_salary", ColumnDescription = "原基本工资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? OriginalBaseSalary { get; set; }

        /// <summary>
        /// 新基本工资
        /// </summary>
        [SugarColumn(ColumnName = "new_base_salary", ColumnDescription = "新基本工资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? NewBaseSalary { get; set; }

        /// <summary>
        /// 薪资调整幅度(%)
        /// </summary>
        [SugarColumn(ColumnName = "salary_increase_rate", ColumnDescription = "薪资调整幅度", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? SalaryIncreaseRate { get; set; }

        /// <summary>
        /// 申请晋升日期
        /// </summary>
        [SugarColumn(ColumnName = "apply_date", ColumnDescription = "申请晋升日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 计划晋升日期
        /// </summary>
        [SugarColumn(ColumnName = "planned_promotion_date", ColumnDescription = "计划晋升日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime PlannedPromotionDate { get; set; }

        /// <summary>
        /// 实际晋升日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_promotion_date", ColumnDescription = "实际晋升日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualPromotionDate { get; set; }

        /// <summary>
        /// 晋升原因
        /// </summary>
        [SugarColumn(ColumnName = "promotion_reason", ColumnDescription = "晋升原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? PromotionReason { get; set; }

        /// <summary>
        /// 晋升依据
        /// </summary>
        [SugarColumn(ColumnName = "promotion_basis", ColumnDescription = "晋升依据", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
        public string? PromotionBasis { get; set; }

        /// <summary>
        /// 绩效表现
        /// </summary>
        [SugarColumn(ColumnName = "performance_evaluation", ColumnDescription = "绩效表现", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? PerformanceEvaluation { get; set; }

        /// <summary>
        /// 晋升状态(1=待审批 2=已批准 3=已拒绝 4=已晋升 5=已取消)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "晋升状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
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
        /// 拒绝原因
        /// </summary>
        [SugarColumn(ColumnName = "rejection_reason", ColumnDescription = "拒绝原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? RejectionReason { get; set; }

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


