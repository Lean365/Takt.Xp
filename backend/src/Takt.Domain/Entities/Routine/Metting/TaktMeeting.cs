//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktMeeting.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议管理实体
    /// </summary>
    [SugarTable("Takt_routine_meeting", "会议管理")]
    [SugarIndex("index_meeting_title", nameof(Title), OrderByType.Asc)]
    [SugarIndex("index_meeting_type", nameof(MeetingType), OrderByType.Asc)]
    [SugarIndex("index_meeting_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("index_meeting_start_time", nameof(StartTime), OrderByType.Asc)]
    [SugarIndex("index_meeting_organizer_id", nameof(OrganizerId), OrderByType.Asc)]

    [SugarIndex("index_meeting_room_id", nameof(RoomId), OrderByType.Asc)]
    public class TaktMeeting : TaktBaseEntity
    {
        /// <summary>
        /// 会议标题
        /// </summary>
        [SugarColumn(ColumnName = "title", ColumnDescription = "会议标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 会议内容
        /// </summary>
        [SugarColumn(ColumnName = "content", ColumnDescription = "会议内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Content { get; set; }

        /// <summary>
        /// 会议类型（0：普通会议，1：重要会议，2：紧急会议，3：培训会议，4：项目会议，5：部门会议）
        /// </summary>
        [SugarColumn(ColumnName = "meeting_type", ColumnDescription = "会议类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MeetingType { get; set; }



        /// <summary>
        /// 会议状态（0：草稿，1：已发布，2：进行中，3：已完成，4：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "会议状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否全天（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_all_day", ColumnDescription = "是否全天", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsAllDay { get; set; }

        /// <summary>
        /// 会议地点
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "会议地点", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Location { get; set; }

        /// <summary>
        /// 会议室ID
        /// </summary>
        [SugarColumn(ColumnName = "room_id", ColumnDescription = "会议室ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? RoomId { get; set; }

        /// <summary>
        /// 组织者ID
        /// </summary>
        [SugarColumn(ColumnName = "organizer_id", ColumnDescription = "组织者ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long OrganizerId { get; set; }

        /// <summary>
        /// 主持人ID
        /// </summary>
        [SugarColumn(ColumnName = "host_id", ColumnDescription = "主持人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? HostId { get; set; }

        /// <summary>
        /// 记录人ID
        /// </summary>
        [SugarColumn(ColumnName = "recorder_id", ColumnDescription = "记录人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? RecorderId { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人员", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Participants { get; set; }

        /// <summary>
        /// 是否需要签到（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_sign_in", ColumnDescription = "是否需要签到", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedSignIn { get; set; }





        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提醒时间", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? RemindMinutes { get; set; }



        /// <summary>
        /// 会议链接（视频会议）
        /// </summary>
        [SugarColumn(ColumnName = "meeting_link", ColumnDescription = "会议链接", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingLink { get; set; }

        /// <summary>
        /// 会议密码
        /// </summary>
        [SugarColumn(ColumnName = "meeting_password", ColumnDescription = "会议密码", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MeetingPassword { get; set; }



        /// <summary>
        /// 是否公开（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsPublic { get; set; }



        /// <summary>
        /// 实际开始时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualEndTime { get; set; }



        /// <summary>
        /// 导航属性：组织者
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(OrganizerId))]
        public TaktUser? Organizer { get; set; }

        /// <summary>
        /// 导航属性：主持人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(HostId))]
        public TaktUser? Host { get; set; }

        /// <summary>
        /// 导航属性：记录人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(RecorderId))]
        public TaktUser? Recorder { get; set; }
    }
}


