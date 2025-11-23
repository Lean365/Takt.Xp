//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedSchedule.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 日程管理种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.Schedule;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 日程管理种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供日程管理的默认种子数据
/// 2. 包含一条示例日程记录
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedSchedule
{
    /// <summary>
    /// 获取默认日程数据
    /// </summary>
    /// <returns>日程数据列表</returns>
    public List<TaktSchedule> GetDefaultSchedules()
    {
        return new List<TaktSchedule>
        {
            new TaktSchedule
            {
                Title = "系统培训会议",
                Content = "新员工系统使用培训，包括基础功能操作和常见问题解答。",
                StartTime = DateTime.Now.AddDays(1).AddHours(9), // 明天上午9点
                EndTime = DateTime.Now.AddDays(1).AddHours(11), // 明天上午11点
                ScheduleType = 1, // 1-重要日程
                Status = 0, // 0-未开始
                IsAllDay = 0, // 0-否
                RemindMinutes = 30, // 提前30分钟提醒
                RepeatType = 0, // 0-不重复
                RepeatEndTime = null, // 重复结束时间
                Location = "会议室A", // 地点

            }
        };
    }
}


