#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktChangeMeeting.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 设变会议实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Change
{
    /// <summary>
    /// 设变会议实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录设计变更会议的相关信息，包括会议安排、参与人员、会议内容、决议等
    /// </remarks>
    [SugarTable("Takt_logistics_bom_change_meeting", "设变会议表")]
    [SugarIndex("ix_change_meeting_code", nameof(ChangeMeetingCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_change_meeting", nameof(CompanyCode), OrderByType.Asc, nameof(ChangeMeetingCode), OrderByType.Asc, false)]
    [SugarIndex("ix_change_meeting_status", nameof(MeetingStatus), OrderByType.Asc, false)]
    public class TaktChangeMeeting : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>设变会议编号</summary>
        [SugarColumn(ColumnName = "change_meeting_code", ColumnDescription = "设变会议编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeMeetingCode { get; set; } = string.Empty;

        /// <summary>设变会议标题</summary>
        [SugarColumn(ColumnName = "change_meeting_title", ColumnDescription = "设变会议标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeMeetingTitle { get; set; } = string.Empty;

        /// <summary>设变会议类型(1=设计变更会议 2=工艺变更会议 3=材料变更会议 4=其他变更会议)</summary>
        [SugarColumn(ColumnName = "change_meeting_type", ColumnDescription = "设变会议类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ChangeMeetingType { get; set; } = 1;

        /// <summary>相关设计变更编号</summary>
        [SugarColumn(ColumnName = "related_change_code", ColumnDescription = "相关设计变更编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedChangeCode { get; set; }

        /// <summary>相关产品编号</summary>
        [SugarColumn(ColumnName = "related_product_code", ColumnDescription = "相关产品编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedProductCode { get; set; }

        /// <summary>相关产品名称</summary>
        [SugarColumn(ColumnName = "related_product_name", ColumnDescription = "相关产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedProductName { get; set; }

        /// <summary>会议日期</summary>
        [SugarColumn(ColumnName = "meeting_date", ColumnDescription = "会议日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime MeetingDate { get; set; } = DateTime.Today;

        /// <summary>会议开始时间</summary>
        [SugarColumn(ColumnName = "meeting_start_time", ColumnDescription = "会议开始时间", ColumnDataType = "time", IsNullable = false)]
        public TimeSpan MeetingStartTime { get; set; } = TimeSpan.Zero;

        /// <summary>会议结束时间</summary>
        [SugarColumn(ColumnName = "meeting_end_time", ColumnDescription = "会议结束时间", ColumnDataType = "time", IsNullable = true)]
        public TimeSpan? MeetingEndTime { get; set; }

        /// <summary>会议地点</summary>
        [SugarColumn(ColumnName = "meeting_location", ColumnDescription = "会议地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingLocation { get; set; }

        /// <summary>会议主持人</summary>
        [SugarColumn(ColumnName = "meeting_host", ColumnDescription = "会议主持人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingHost { get; set; }

        /// <summary>会议记录人</summary>
        [SugarColumn(ColumnName = "meeting_recorder", ColumnDescription = "会议记录人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingRecorder { get; set; }

        /// <summary>参与人员</summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Participants { get; set; }

        /// <summary>参与部门</summary>
        [SugarColumn(ColumnName = "participant_departments", ColumnDescription = "参与部门", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParticipantDepartments { get; set; }

        /// <summary>会议议题</summary>
        [SugarColumn(ColumnName = "meeting_topics", ColumnDescription = "会议议题", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingTopics { get; set; }

        /// <summary>会议内容</summary>
        [SugarColumn(ColumnName = "meeting_content", ColumnDescription = "会议内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingContent { get; set; }

        /// <summary>会议决议</summary>
        [SugarColumn(ColumnName = "meeting_resolution", ColumnDescription = "会议决议", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingResolution { get; set; }

        /// <summary>后续行动</summary>
        [SugarColumn(ColumnName = "follow_up_actions", ColumnDescription = "后续行动", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FollowUpActions { get; set; }

        /// <summary>负责人</summary>
        [SugarColumn(ColumnName = "responsible_person", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePerson { get; set; }

        /// <summary>完成期限</summary>
        [SugarColumn(ColumnName = "completion_deadline", ColumnDescription = "完成期限", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CompletionDeadline { get; set; }

        /// <summary>会议状态(0=未开始 1=进行中 2=已完成 3=已取消)</summary>
        [SugarColumn(ColumnName = "meeting_status", ColumnDescription = "会议状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MeetingStatus { get; set; } = 0;

        /// <summary>会议重要性(1=一般 2=重要 3=紧急)</summary>
        [SugarColumn(ColumnName = "meeting_importance", ColumnDescription = "会议重要性", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int MeetingImportance { get; set; } = 2;

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>会议纪要文件</summary>
        [SugarColumn(ColumnName = "meeting_minutes_file", ColumnDescription = "会议纪要文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeetingMinutesFile { get; set; }

        /// <summary>相关附件</summary>
        [SugarColumn(ColumnName = "related_attachments", ColumnDescription = "相关附件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedAttachments { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



