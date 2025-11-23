#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Routine.Project;
using Takt.Domain.Entities.Routine.Metting;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTeamSchedule.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 团队协作日程实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Schedule
{
    /// <summary>
    /// 团队协作日程实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录团队协作日程的基本信息
    /// </remarks>
    [SugarTable("Takt_routine_team_schedule", "团队协作日程表")]
    [SugarIndex("ix_team_schedule_code", nameof(TeamScheduleCode), OrderByType.Asc, true)]
    public class TaktTeamSchedule : TaktBaseEntity
    {
        /// <summary>团队协作日程编号</summary>
        [SugarColumn(ColumnName = "team_schedule_code", ColumnDescription = "团队协作日程编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TeamScheduleCode { get; set; } = string.Empty;

        /// <summary>团队协作日程标题</summary>
        [SugarColumn(ColumnName = "team_schedule_title", ColumnDescription = "团队协作日程标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TeamScheduleTitle { get; set; } = string.Empty;

        /// <summary>团队协作日程描述</summary>
        [SugarColumn(ColumnName = "team_schedule_description", ColumnDescription = "团队协作日程描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamScheduleDescription { get; set; }

        /// <summary>计划开始时间</summary>
        [SugarColumn(ColumnName = "planned_start_time", ColumnDescription = "计划开始时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PlannedStartTime { get; set; } = DateTime.Now;

        /// <summary>计划结束时间</summary>
        [SugarColumn(ColumnName = "planned_end_time", ColumnDescription = "计划结束时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime PlannedEndTime { get; set; } = DateTime.Now.AddHours(2);

        /// <summary>协作地点</summary>
        [SugarColumn(ColumnName = "collaboration_location", ColumnDescription = "协作地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationLocation { get; set; }

        /// <summary>团队负责人</summary>
        [SugarColumn(ColumnName = "team_leader", ColumnDescription = "团队负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamLeader { get; set; }

        /// <summary>参与人员</summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Participants { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;        

        /// <summary>团队协作日程状态(0=未开始 1=进行中 2=已完成 3=已暂停 4=已取消)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "团队协作日程状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>关联项目ID</summary>
        [SugarColumn(ColumnName = "related_project_id", ColumnDescription = "关联项目ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? RelatedProjectId { get; set; }

        /// <summary>关联会议ID</summary>
        [SugarColumn(ColumnName = "related_meeting_id", ColumnDescription = "关联会议ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? RelatedMeetingId { get; set; }

        /// <summary>关联项目</summary>
        [Navigate(NavigateType.OneToOne, nameof(RelatedProjectId))]
        public TaktProject? RelatedProject { get; set; }

        /// <summary>关联会议</summary>
        [Navigate(NavigateType.OneToOne, nameof(RelatedMeetingId))]
        public TaktMeeting? RelatedMeeting { get; set; }


    }
} 



