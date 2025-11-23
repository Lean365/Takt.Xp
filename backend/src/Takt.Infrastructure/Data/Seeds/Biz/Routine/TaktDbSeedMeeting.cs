//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedMeeting.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 会议管理种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.Metting;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 会议管理种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供会议管理的默认种子数据
/// 2. 包含一条示例会议记录、会议参与者记录、会议室记录和会议纪要记录
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedMeeting
{
    /// <summary>
    /// 获取默认会议数据
    /// </summary>
    /// <returns>会议数据列表</returns>
    public List<TaktMeeting> GetDefaultMeetings()
    {
        return new List<TaktMeeting>
        {
            new TaktMeeting
            {
                Title = "项目启动会议",
                Content = "讨论新项目Takt.Xp的启动计划，确定项目目标、时间安排和人员分工。",
                MeetingType = 4, // 4-项目会议
                Status = 1, // 1-已发布
                StartTime = DateTime.Now.AddDays(2).AddHours(14), // 后天下午2点
                EndTime = DateTime.Now.AddDays(2).AddHours(16), // 后天下午4点
                IsAllDay = 0, // 0-否
                Location = "会议室B", // 会议地点
                RoomId = 1, // 会议室ID
                OrganizerId = 1958104363528491008, // 组织者ID (admin用户)
                HostId = 1958104363528491008, // 主持人ID (admin用户)
                RecorderId = 1958104363528491008, // 记录人ID (admin用户)
                Participants = "admin,user1,user2", // 参与人员
                NeedSignIn = 1, // 1-需要签到
                RemindMinutes = 30, // 提前30分钟提醒
                MeetingLink = null, // 会议链接
                MeetingPassword = null, // 会议密码
                IsPublic = 1, // 1-公开
                ActualStartTime = null, // 实际开始时间
                ActualEndTime = null, // 实际结束时间
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取默认会议参与者数据
    /// </summary>
    /// <returns>会议参与者数据列表</returns>
    public List<TaktMeetingParticipant> GetDefaultMeetingParticipants()
    {
        return new List<TaktMeetingParticipant>
        {
            new TaktMeetingParticipant
            {
                MeetingId = 1, // 关联到第一个会议
                UserId = 1958104363528491008, // admin用户ID
                ParticipantType = 0, // 0-主持人
                Status = 1, // 1-已确认
                IsRequired = 1, // 1-必须参加
                NeedSignIn = 1, // 1-需要签到
                NeedReminder = 1, // 1-需要提醒
                RemindMinutes = 30, // 提前30分钟提醒
                ConfirmTime = DateTime.Now, // 确认时间
                SignInTime = null, // 签到时间
                SignOutTime = null, // 签退时间
                SignInMethod = null, // 签到方式
                SignOutMethod = null, // 签退方式
                SignInLocation = null, // 签到位置
                SignOutLocation = null, // 签退位置
                SignInIp = null, // 签到IP地址
                SignOutIp = null, // 签退IP地址
                SignInDevice = null, // 签到设备
                SignOutDevice = null, // 签退设备
                AttendanceStatus = 0, // 0-正常
                AttendanceDuration = null, // 参会时长
                IsValid = 1, // 1-有效签到
                InvalidReason = null, // 无效原因
                SignInComments = null, // 签到说明
                SignOutComments = null, // 签退说明
                PhotoPath = null, // 照片路径
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取默认会议室数据
    /// </summary>
    /// <returns>会议室数据列表</returns>
    public List<TaktMeetingRoom> GetDefaultMeetingRooms()
    {
        return new List<TaktMeetingRoom>
        {
            new TaktMeetingRoom
            {
                RoomName = "会议室B",
                RoomCode = "MR001",
                Location = "3楼东侧",
                RoomType = 1, // 1-中型会议室
                Capacity = 20, // 容纳20人
                Status = 0, // 0-正常
                SupportVideo = 1, // 1-支持视频会议
                EquipmentConfig = "投影仪、白板、音响系统、视频会议设备",
                UsageInstructions = "请提前预约，使用完毕后请保持整洁",
                BookingRules = "工作日8:00-18:00可预约，提前2小时预约",
                OpenTime = "08:00",
                CloseTime = "18:00",
                ManagerId = 1958104363528491008, // 负责人ID (admin用户)
                Remarks = "适合中小型会议和培训",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取默认会议纪要数据
    /// </summary>
    /// <returns>会议纪要数据列表</returns>
    public List<TaktMeetingSummary> GetDefaultMeetingSummaries()
    {
        return new List<TaktMeetingSummary>
        {
            new TaktMeetingSummary
            {
                MeetingId = 1, // 关联到第一个会议
                RecorderId = 1958104363528491008, // 记录人ID (admin用户)
                SummaryTitle = "Takt.Xp项目启动会议纪要",
                ContentSummary = "本次会议主要讨论了Takt.Xp项目的启动计划，确定了项目目标、时间安排和人员分工。",
                Status = 1, // 1-已发布
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}


