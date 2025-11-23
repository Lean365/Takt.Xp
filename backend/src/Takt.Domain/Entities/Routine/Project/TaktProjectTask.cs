#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectTask.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目任务实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目任务实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目任务的相关信息，包括任务信息、进度、依赖关系等
    /// </remarks>
    [SugarTable("Takt_routine_project_task", "项目任务表")]
    [SugarIndex("ix_project_task_code", nameof(ProjectTaskCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_task", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectTaskCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_task_status", nameof(ProjectTaskStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_task_type", nameof(ProjectTaskType), OrderByType.Asc, false)]
    public class TaktProjectTask : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目任务编号</summary>
        [SugarColumn(ColumnName = "project_task_code", ColumnDescription = "项目任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectTaskCode { get; set; } = string.Empty;

        /// <summary>任务名称</summary>
        [SugarColumn(ColumnName = "task_name", ColumnDescription = "任务名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TaskName { get; set; } = string.Empty;

        /// <summary>任务类型(1=需求分析 2=设计 3=开发 4=测试 5=部署 6=文档 7=培训 8=其他)</summary>
        [SugarColumn(ColumnName = "project_task_type", ColumnDescription = "任务类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectTaskType { get; set; } = 1;

        /// <summary>任务描述</summary>
        [SugarColumn(ColumnName = "task_description", ColumnDescription = "任务描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskDescription { get; set; }

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>父任务编号</summary>
        [SugarColumn(ColumnName = "parent_task_code", ColumnDescription = "父任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentTaskCode { get; set; }

        /// <summary>父任务名称</summary>
        [SugarColumn(ColumnName = "parent_task_name", ColumnDescription = "父任务名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentTaskName { get; set; }

        /// <summary>任务层级</summary>
        [SugarColumn(ColumnName = "task_level", ColumnDescription = "任务层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int TaskLevel { get; set; } = 1;

        /// <summary>任务顺序</summary>
        [SugarColumn(ColumnName = "task_sequence", ColumnDescription = "任务顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int TaskSequence { get; set; } = 1;

        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "planned_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>计划结束日期</summary>
        [SugarColumn(ColumnName = "planned_end_date", ColumnDescription = "计划结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>计划工期(天)</summary>
        [SugarColumn(ColumnName = "planned_duration", ColumnDescription = "计划工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? PlannedDuration { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际结束日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>实际工期(天)</summary>
        [SugarColumn(ColumnName = "actual_duration", ColumnDescription = "实际工期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ActualDuration { get; set; }

        /// <summary>任务进度(%)</summary>
        [SugarColumn(ColumnName = "task_progress", ColumnDescription = "任务进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TaskProgress { get; set; } = 0;

        /// <summary>任务优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "task_priority", ColumnDescription = "任务优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int TaskPriority { get; set; } = 2;

        /// <summary>任务难度(1=简单 2=一般 3=困难 4=极难)</summary>
        [SugarColumn(ColumnName = "task_difficulty", ColumnDescription = "任务难度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int TaskDifficulty { get; set; } = 2;

        /// <summary>任务状态(0=未开始 1=进行中 2=已完成 3=已暂停 4=已取消)</summary>
        [SugarColumn(ColumnName = "project_task_status", ColumnDescription = "任务状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectTaskStatus { get; set; } = 0;

        /// <summary>任务负责人</summary>
        [SugarColumn(ColumnName = "task_manager", ColumnDescription = "任务负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskManager { get; set; }

        /// <summary>任务负责人电话</summary>
        [SugarColumn(ColumnName = "task_manager_phone", ColumnDescription = "任务负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskManagerPhone { get; set; }

        /// <summary>任务负责人邮箱</summary>
        [SugarColumn(ColumnName = "task_manager_email", ColumnDescription = "任务负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskManagerEmail { get; set; }

        /// <summary>任务执行人</summary>
        [SugarColumn(ColumnName = "task_executor", ColumnDescription = "任务执行人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskExecutor { get; set; }

        /// <summary>任务执行人电话</summary>
        [SugarColumn(ColumnName = "task_executor_phone", ColumnDescription = "任务执行人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskExecutorPhone { get; set; }

        /// <summary>任务执行人邮箱</summary>
        [SugarColumn(ColumnName = "task_executor_email", ColumnDescription = "任务执行人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskExecutorEmail { get; set; }

        /// <summary>任务团队</summary>
        [SugarColumn(ColumnName = "task_team", ColumnDescription = "任务团队", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskTeam { get; set; }

        /// <summary>前置任务</summary>
        [SugarColumn(ColumnName = "predecessor_tasks", ColumnDescription = "前置任务", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PredecessorTasks { get; set; }

        /// <summary>后置任务</summary>
        [SugarColumn(ColumnName = "successor_tasks", ColumnDescription = "后置任务", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SuccessorTasks { get; set; }

        /// <summary>依赖关系</summary>
        [SugarColumn(ColumnName = "dependency_relationship", ColumnDescription = "依赖关系", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DependencyRelationship { get; set; }

        /// <summary>任务里程碑(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_milestone", ColumnDescription = "任务里程碑", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsMilestone { get; set; } = 0;

        /// <summary>任务关键路径(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_critical_path", ColumnDescription = "任务关键路径", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCriticalPath { get; set; } = 0;

        /// <summary>任务完成标准</summary>
        [SugarColumn(ColumnName = "task_completion_criteria", ColumnDescription = "任务完成标准", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskCompletionCriteria { get; set; }

        /// <summary>任务验收标准</summary>
        [SugarColumn(ColumnName = "task_acceptance_criteria", ColumnDescription = "任务验收标准", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskAcceptanceCriteria { get; set; }

        /// <summary>任务风险</summary>
        [SugarColumn(ColumnName = "task_risks", ColumnDescription = "任务风险", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskRisks { get; set; }

        /// <summary>风险等级(1=低 2=中 3=高 4=极高)</summary>
        [SugarColumn(ColumnName = "risk_level", ColumnDescription = "风险等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int RiskLevel { get; set; } = 1;

        /// <summary>任务备注</summary>
        [SugarColumn(ColumnName = "task_remark", ColumnDescription = "任务备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



