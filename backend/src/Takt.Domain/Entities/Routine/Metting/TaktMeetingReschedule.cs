//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktMeetingReschedule.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议改期管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议改期管理实体
    /// </summary>
    [SugarTable("Takt_routine_meeting_reschedule", "会议改期管理")]
    [SugarIndex("index_reschedule_meeting_id", nameof(MeetingId), OrderByType.Asc)]
    [SugarIndex("index_reschedule_user_id", nameof(RescheduleUserId), OrderByType.Asc)]
    [SugarIndex("index_reschedule_time", nameof(RescheduleTime), OrderByType.Desc)]
    public class TaktMeetingReschedule : TaktBaseEntity
    {
        /// <summary>
        /// 会议ID
        /// </summary>
        [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "会议ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long MeetingId { get; set; }

        /// <summary>
        /// 改期人ID
        /// </summary>
        [SugarColumn(ColumnName = "reschedule_user_id", ColumnDescription = "改期人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long RescheduleUserId { get; set; }

        /// <summary>
        /// 改期时间
        /// </summary>
        [SugarColumn(ColumnName = "reschedule_time", ColumnDescription = "改期时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime RescheduleTime { get; set; }

        /// <summary>
        /// 改期原因
        /// </summary>
        [SugarColumn(ColumnName = "reschedule_reason", ColumnDescription = "改期原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RescheduleReason { get; set; }

        /// <summary>
        /// 原开始时间
        /// </summary>
        [SugarColumn(ColumnName = "original_start_time", ColumnDescription = "原开始时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime OriginalStartTime { get; set; }

        /// <summary>
        /// 原结束时间
        /// </summary>
        [SugarColumn(ColumnName = "original_end_time", ColumnDescription = "原结束时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime OriginalEndTime { get; set; }

        /// <summary>
        /// 原会议地点
        /// </summary>
        [SugarColumn(ColumnName = "original_location", ColumnDescription = "原会议地点", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? OriginalLocation { get; set; }

        /// <summary>
        /// 原会议室ID
        /// </summary>
        [SugarColumn(ColumnName = "original_room_id", ColumnDescription = "原会议室ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? OriginalRoomId { get; set; }

        /// <summary>
        /// 新开始时间
        /// </summary>
        [SugarColumn(ColumnName = "new_start_time", ColumnDescription = "新开始时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime NewStartTime { get; set; }

        /// <summary>
        /// 新结束时间
        /// </summary>
        [SugarColumn(ColumnName = "new_end_time", ColumnDescription = "新结束时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime NewEndTime { get; set; }

        /// <summary>
        /// 新会议地点
        /// </summary>
        [SugarColumn(ColumnName = "new_location", ColumnDescription = "新会议地点", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? NewLocation { get; set; }

        /// <summary>
        /// 新会议室ID
        /// </summary>
        [SugarColumn(ColumnName = "new_room_id", ColumnDescription = "新会议室ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? NewRoomId { get; set; }

        /// <summary>
        /// 改期状态（0：待确认，1：已确认，2：已拒绝，3：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "改期状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 确认人ID
        /// </summary>
        [SugarColumn(ColumnName = "confirm_user_id", ColumnDescription = "确认人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ConfirmUserId { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        [SugarColumn(ColumnName = "confirm_time", ColumnDescription = "确认时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 确认说明
        /// </summary>
        [SugarColumn(ColumnName = "confirm_comments", ColumnDescription = "确认说明", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmComments { get; set; }

        /// <summary>
        /// 通知状态（0：未通知，1：已通知，2：通知失败）
        /// </summary>
        [SugarColumn(ColumnName = "notify_status", ColumnDescription = "通知状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NotifyStatus { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        [SugarColumn(ColumnName = "notify_time", ColumnDescription = "通知时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NotifyTime { get; set; }

        /// <summary>
        /// 导航属性：会议
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MeetingId))]
        public TaktMeeting? Meeting { get; set; }

        /// <summary>
        /// 导航属性：改期人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(RescheduleUserId))]
        public TaktUser? RescheduleUser { get; set; }

        /// <summary>
        /// 导航属性：确认人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ConfirmUserId))]
        public TaktUser? ConfirmUser { get; set; }
    }
} 


