//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktMeetingParticipant.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议参与者管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议参与者管理实体
    /// </summary>
    [SugarTable("Takt_routine_meeting_participant", "会议参与者管理")]
    [SugarIndex("index_participant_meeting_id", nameof(MeetingId), OrderByType.Asc)]
    [SugarIndex("index_participant_user_id", nameof(UserId), OrderByType.Asc)]
    [SugarIndex("index_participant_type", nameof(ParticipantType), OrderByType.Asc)]
    [SugarIndex("index_participant_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("index_participant_sign_in_time", nameof(SignInTime), OrderByType.Asc)]
    [SugarIndex("index_participant_attendance_status", nameof(AttendanceStatus), OrderByType.Asc)]
    public class TaktMeetingParticipant : TaktBaseEntity
    {
        /// <summary>
        /// 会议ID
        /// </summary>
        [SugarColumn(ColumnName = "meeting_id", ColumnDescription = "会议ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long MeetingId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 参与者类型（0：主持人，1：参与人，2：列席人员，3：记录人，4：旁听人员）
        /// </summary>
        [SugarColumn(ColumnName = "participant_type", ColumnDescription = "参与者类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ParticipantType { get; set; }

        /// <summary>
        /// 参与状态（0：未确认，1：已确认，2：已拒绝，3：已取消，4：已签到，5：已签退）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "参与状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 是否必须参加（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_required", ColumnDescription = "是否必须参加", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsRequired { get; set; }

        /// <summary>
        /// 是否需要签到（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_sign_in", ColumnDescription = "是否需要签到", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedSignIn { get; set; }

        /// <summary>
        /// 是否需要提醒（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "need_reminder", ColumnDescription = "是否需要提醒", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NeedReminder { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提醒时间", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        [SugarColumn(ColumnName = "confirm_time", ColumnDescription = "确认时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 拒绝时间
        /// </summary>
        [SugarColumn(ColumnName = "reject_time", ColumnDescription = "拒绝时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? RejectTime { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        [SugarColumn(ColumnName = "reject_reason", ColumnDescription = "拒绝原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RejectReason { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_time", ColumnDescription = "签到时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? SignInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_time", ColumnDescription = "签退时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? SignOutTime { get; set; }

        /// <summary>
        /// 签到方式（0：手动签到，1：二维码签到，2：人脸识别，3：指纹签到，4：刷卡签到）
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_method", ColumnDescription = "签到方式", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? SignInMethod { get; set; }

        /// <summary>
        /// 签退方式（0：手动签退，1：自动签退，2：强制签退）
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_method", ColumnDescription = "签退方式", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? SignOutMethod { get; set; }

        /// <summary>
        /// 签到位置
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_location", ColumnDescription = "签到位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInLocation { get; set; }

        /// <summary>
        /// 签退位置
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_location", ColumnDescription = "签退位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutLocation { get; set; }

        /// <summary>
        /// 签到IP地址
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_ip", ColumnDescription = "签到IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInIp { get; set; }

        /// <summary>
        /// 签退IP地址
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_ip", ColumnDescription = "签退IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutIp { get; set; }

        /// <summary>
        /// 签到设备
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_device", ColumnDescription = "签到设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInDevice { get; set; }

        /// <summary>
        /// 签退设备
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_device", ColumnDescription = "签退设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutDevice { get; set; }

        /// <summary>
        /// 签到状态（0：正常，1：迟到，2：早退，3：缺勤，4：请假）
        /// </summary>
        [SugarColumn(ColumnName = "attendance_status", ColumnDescription = "签到状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AttendanceStatus { get; set; }

        /// <summary>
        /// 参会时长（分钟）
        /// </summary>
        [SugarColumn(ColumnName = "attendance_duration", ColumnDescription = "参会时长", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? AttendanceDuration { get; set; }

        /// <summary>
        /// 是否有效签到（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_valid", ColumnDescription = "是否有效签到", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsValid { get; set; }

        /// <summary>
        /// 无效原因
        /// </summary>
        [SugarColumn(ColumnName = "invalid_reason", ColumnDescription = "无效原因", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvalidReason { get; set; }

        /// <summary>
        /// 签到说明
        /// </summary>
        [SugarColumn(ColumnName = "sign_in_comments", ColumnDescription = "签到说明", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignInComments { get; set; }

        /// <summary>
        /// 签退说明
        /// </summary>
        [SugarColumn(ColumnName = "sign_out_comments", ColumnDescription = "签退说明", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SignOutComments { get; set; }

        /// <summary>
        /// 照片路径
        /// </summary>
        [SugarColumn(ColumnName = "photo_path", ColumnDescription = "照片路径", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? PhotoPath { get; set; }

        /// <summary>
        /// 导航属性：会议
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(MeetingId))]
        public TaktMeeting? Meeting { get; set; }

        /// <summary>
        /// 导航属性：用户
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public TaktUser? User { get; set; }
    }
} 


