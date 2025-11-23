#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowOptions.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 工作流配置选项
    /// </summary>
    public class TaktWorkflowOptions
    {
        /// <summary>
        /// 最大执行天数
        /// </summary>
        public int MaxExecutionDays { get; set; } = 7;

        /// <summary>
        /// 自动执行配置
        /// </summary>
        public AutoExecutionOptions AutoExecution { get; set; } = new();

        /// <summary>
        /// 超时提醒配置
        /// </summary>
        public TimeoutReminderOptions TimeoutReminder { get; set; } = new();

        /// <summary>
        /// 清理配置
        /// </summary>
        public CleanupOptions Cleanup { get; set; } = new();
    }

    /// <summary>
    /// 自动执行配置
    /// </summary>
    public class AutoExecutionOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 执行间隔（分钟）
        /// </summary>
        public int IntervalMinutes { get; set; } = 2;
    }

    /// <summary>
    /// 超时提醒配置
    /// </summary>
    public class TimeoutReminderOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 执行间隔（分钟）
        /// </summary>
        public int IntervalMinutes { get; set; } = 5;

        /// <summary>
        /// 提醒小时数
        /// </summary>
        public int ReminderHours { get; set; } = 24;
    }

    /// <summary>
    /// 清理配置
    /// </summary>
    public class CleanupOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 保留天数
        /// </summary>
        public int RetentionDays { get; set; } = 90;

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; } = "0 0 2 * * ?";
    }
} 


