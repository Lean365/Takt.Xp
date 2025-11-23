// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeePromotionHistory.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeePromotionHistory 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Employee
{
    /// <summary>
    /// 员工晋升历史记录实体
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 记录员工晋升历史信息
    /// 2. 支持晋升轨迹追踪
    /// 3. 为晋升决策提供历史依据
    /// </remarks>
    [SugarTable("takt_humanresource_employee_promotion_history", "员工晋升历史表")]
    [SugarIndex("ix_employee_promotion_history_employee", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("ix_employee_promotion_history_date", nameof(PromotionDate), OrderByType.Desc)]
    [SugarIndex("ix_employee_promotion_history_type", nameof(PromotionType), OrderByType.Asc)]
    public class TaktEmployeePromotionHistory : TaktBaseEntity
    {
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
        /// 晋升日期
        /// </summary>
        [SugarColumn(ColumnName = "promotion_date", ColumnDescription = "晋升日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime PromotionDate { get; set; }

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
        /// 原职位名称
        /// </summary>
        [SugarColumn(ColumnName = "original_position_name", ColumnDescription = "原职位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? OriginalPositionName { get; set; }

        /// <summary>
        /// 新职位名称
        /// </summary>
        [SugarColumn(ColumnName = "new_position_name", ColumnDescription = "新职位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? NewPositionName { get; set; }

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


