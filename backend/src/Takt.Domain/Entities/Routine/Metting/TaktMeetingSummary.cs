//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktMeetingSummary.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议纪要管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议纪要管理实体
    /// </summary>
    [SugarTable("Takt_routine_meeting_summary", "会议纪要管理")]
    [SugarIndex("index_summary_meeting_id", nameof(MeetingId), OrderByType.Asc)]
    [SugarIndex("index_summary_recorder_id", nameof(RecorderId), OrderByType.Asc)]
    [SugarIndex("index_summary_status", nameof(Status), OrderByType.Asc)]
    public class TaktMeetingSummary : TaktBaseEntity
    {
        /// <summary>
        /// 会议ID
        /// </summary>
        [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "会议ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long MeetingId { get; set; }

        /// <summary>
        /// 记录人ID
        /// </summary>
        [SugarColumn(ColumnName = "recorder_id", ColumnDescription = "记录人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long RecorderId { get; set; }

        /// <summary>
        /// 纪要标题
        /// </summary>
        [SugarColumn(ColumnName = "summary_title", ColumnDescription = "纪要标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string SummaryTitle { get; set; } = string.Empty;

        /// <summary>
        /// 会议内容摘要
        /// </summary>
        [SugarColumn(ColumnName = "content_summary", ColumnDescription = "会议内容摘要", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ContentSummary { get; set; }

        /// <summary>
        /// 会议决议
        /// </summary>
        [SugarColumn(ColumnName = "resolutions", ColumnDescription = "会议决议", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Resolutions { get; set; }

        /// <summary>
        /// 行动项
        /// </summary>
        [SugarColumn(ColumnName = "action_items", ColumnDescription = "行动项", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ActionItems { get; set; }

        /// <summary>
        /// 参会人员
        /// </summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参会人员", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Participants { get; set; }

        /// <summary>
        /// 缺席人员
        /// </summary>
        [SugarColumn(ColumnName = "absentees", ColumnDescription = "缺席人员", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Absentees { get; set; }

        /// <summary>
        /// 纪要状态（0：草稿，1：待审核，2：已审核，3：已发布，4：已归档）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "纪要状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [SugarColumn(ColumnName = "reviewer_id", ColumnDescription = "审核人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ReviewerId { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [SugarColumn(ColumnName = "review_time", ColumnDescription = "审核时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ReviewTime { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        [SugarColumn(ColumnName = "review_comments", ColumnDescription = "审核意见", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReviewComments { get; set; }

        /// <summary>
        /// 发布人ID
        /// </summary>
        [SugarColumn(ColumnName = "publisher_id", ColumnDescription = "发布人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? PublisherId { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [SugarColumn(ColumnName = "publish_time", ColumnDescription = "发布时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 是否公开（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsPublic { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }

        /// <summary>
        /// 导航属性：会议
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MeetingId))]
        public TaktMeeting? Meeting { get; set; }

        /// <summary>
        /// 导航属性：记录人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(RecorderId))]
        public TaktUser? Recorder { get; set; }

        /// <summary>
        /// 导航属性：审核人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ReviewerId))]
        public TaktUser? Reviewer { get; set; }

        /// <summary>
        /// 导航属性：发布人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PublisherId))]
        public TaktUser? Publisher { get; set; }
    }
} 


