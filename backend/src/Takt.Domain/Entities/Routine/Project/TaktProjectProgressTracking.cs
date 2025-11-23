#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectProgressTracking.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目进度跟踪实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目进度跟踪实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目进度跟踪的相关信息，包括进度信息、里程碑、偏差等
    /// </remarks>
    [SugarTable("Takt_routine_project_progress_tracking", "项目进度跟踪表")]
    [SugarIndex("ix_project_progress_tracking_code", nameof(ProjectProgressTrackingCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_progress_tracking", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectProgressTrackingCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_progress_tracking_status", nameof(ProjectProgressTrackingStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_progress_tracking_type", nameof(ProjectProgressTrackingType), OrderByType.Asc, false)]
    public class TaktProjectProgressTracking : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目进度跟踪编号</summary>
        [SugarColumn(ColumnName = "project_progress_tracking_code", ColumnDescription = "项目进度跟踪编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectProgressTrackingCode { get; set; } = string.Empty;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>关联任务编号</summary>
        [SugarColumn(ColumnName = "related_task_code", ColumnDescription = "关联任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskCode { get; set; }

        /// <summary>关联任务名称</summary>
        [SugarColumn(ColumnName = "related_task_name", ColumnDescription = "关联任务名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskName { get; set; }

        /// <summary>进度跟踪类型(1=项目进度 2=任务进度 3=里程碑进度 4=阶段进度 5=其他进度)</summary>
        [SugarColumn(ColumnName = "project_progress_tracking_type", ColumnDescription = "进度跟踪类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectProgressTrackingType { get; set; } = 1;

        /// <summary>跟踪日期</summary>
        [SugarColumn(ColumnName = "tracking_date", ColumnDescription = "跟踪日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime TrackingDate { get; set; } = DateTime.Today;

        /// <summary>计划进度(%)</summary>
        [SugarColumn(ColumnName = "planned_progress", ColumnDescription = "计划进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedProgress { get; set; } = 0;

        /// <summary>实际进度(%)</summary>
        [SugarColumn(ColumnName = "actual_progress", ColumnDescription = "实际进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualProgress { get; set; } = 0;

        /// <summary>进度偏差(%)</summary>
        [SugarColumn(ColumnName = "progress_variance", ColumnDescription = "进度偏差(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProgressVariance { get; set; } = 0;

        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "planned_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>计划结束日期</summary>
        [SugarColumn(ColumnName = "planned_end_date", ColumnDescription = "计划结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际结束日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>计划工期(天)</summary>
        [SugarColumn(ColumnName = "planned_duration", ColumnDescription = "计划工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? PlannedDuration { get; set; }

        /// <summary>实际工期(天)</summary>
        [SugarColumn(ColumnName = "actual_duration", ColumnDescription = "实际工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ActualDuration { get; set; }

        /// <summary>工期偏差(天)</summary>
        [SugarColumn(ColumnName = "duration_variance", ColumnDescription = "工期偏差(天)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DurationVariance { get; set; } = 0;

        /// <summary>计划工作量(小时)</summary>
        [SugarColumn(ColumnName = "planned_work_hours", ColumnDescription = "计划工作量(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? PlannedWorkHours { get; set; }

        /// <summary>实际工作量(小时)</summary>
        [SugarColumn(ColumnName = "actual_work_hours", ColumnDescription = "实际工作量(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? ActualWorkHours { get; set; }

        /// <summary>工作量偏差(小时)</summary>
        [SugarColumn(ColumnName = "work_hours_variance", ColumnDescription = "工作量偏差(小时)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WorkHoursVariance { get; set; } = 0;

        /// <summary>计划成本</summary>
        [SugarColumn(ColumnName = "planned_cost", ColumnDescription = "计划成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? PlannedCost { get; set; }

        /// <summary>实际成本</summary>
        [SugarColumn(ColumnName = "actual_cost", ColumnDescription = "实际成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualCost { get; set; }

        /// <summary>成本偏差</summary>
        [SugarColumn(ColumnName = "cost_variance", ColumnDescription = "成本偏差", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CostVariance { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>完成的工作内容</summary>
        [SugarColumn(ColumnName = "completed_work", ColumnDescription = "完成的工作内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompletedWork { get; set; }

        /// <summary>进行中的工作</summary>
        [SugarColumn(ColumnName = "ongoing_work", ColumnDescription = "进行中的工作", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OngoingWork { get; set; }

        /// <summary>计划的工作</summary>
        [SugarColumn(ColumnName = "planned_work", ColumnDescription = "计划的工作", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlannedWork { get; set; }

        /// <summary>遇到的问题</summary>
        [SugarColumn(ColumnName = "issues_encountered", ColumnDescription = "遇到的问题", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssuesEncountered { get; set; }

        /// <summary>解决方案</summary>
        [SugarColumn(ColumnName = "solutions", ColumnDescription = "解决方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Solutions { get; set; }

        /// <summary>风险识别</summary>
        [SugarColumn(ColumnName = "risk_identification", ColumnDescription = "风险识别", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskIdentification { get; set; }

        /// <summary>风险应对措施</summary>
        [SugarColumn(ColumnName = "risk_response_measures", ColumnDescription = "风险应对措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskResponseMeasures { get; set; }

        /// <summary>质量检查结果</summary>
        [SugarColumn(ColumnName = "quality_check_results", ColumnDescription = "质量检查结果", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityCheckResults { get; set; }

        /// <summary>质量等级(1=优秀 2=良好 3=合格 4=不合格)</summary>
        [SugarColumn(ColumnName = "quality_grade", ColumnDescription = "质量等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int QualityGrade { get; set; } = 3;

        /// <summary>里程碑达成情况</summary>
        [SugarColumn(ColumnName = "milestone_achievement", ColumnDescription = "里程碑达成情况", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MilestoneAchievement { get; set; }

        /// <summary>是否达成里程碑(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_milestone_achieved", ColumnDescription = "是否达成里程碑", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsMilestoneAchieved { get; set; } = 0;

        /// <summary>进度跟踪人</summary>
        [SugarColumn(ColumnName = "progress_tracker", ColumnDescription = "进度跟踪人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProgressTracker { get; set; }

        /// <summary>进度跟踪人电话</summary>
        [SugarColumn(ColumnName = "progress_tracker_phone", ColumnDescription = "进度跟踪人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProgressTrackerPhone { get; set; }

        /// <summary>进度跟踪人邮箱</summary>
        [SugarColumn(ColumnName = "progress_tracker_email", ColumnDescription = "进度跟踪人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProgressTrackerEmail { get; set; }

        /// <summary>项目进度跟踪状态(0=正常 1=延迟 2=提前 3=暂停 4=终止)</summary>
        [SugarColumn(ColumnName = "project_progress_tracking_status", ColumnDescription = "项目进度跟踪状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectProgressTrackingStatus { get; set; } = 0;

        /// <summary>进度跟踪备注</summary>
        [SugarColumn(ColumnName = "progress_tracking_remark", ColumnDescription = "进度跟踪备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProgressTrackingRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



