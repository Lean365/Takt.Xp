#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : LogCleanupOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 日志清理策略配置
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 日志清理策略配置
    /// </summary>
    public class TaktLogCleanupOptions
    {
        /// <summary>
        /// 是否启用自动清理
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 清理频率(小时)
        /// </summary>
        public int Interval { get; set; } = 24;

        /// <summary>
        /// 审计日志保留天数
        /// </summary>
        public int AuditLogRetentionDays { get; set; } = 90;

        /// <summary>
        /// 操作日志保留天数
        /// </summary>
        public int OperLogRetentionDays { get; set; } = 90;

        /// <summary>
        /// 登录日志保留天数
        /// </summary>
        public int LoginLogRetentionDays { get; set; } = 90;

        /// <summary>
        /// 异常日志保留天数
        /// </summary>
        public int ExceptionLogRetentionDays { get; set; } = 90;

        /// <summary>
        /// 差异日志保留天数
        /// </summary>
        public int DbDiffLogRetentionDays { get; set; } = 90;

        /// <summary>
        /// 每次清理的最大记录数
        /// </summary>
        public int BatchSize { get; set; } = 1000;
    }
} 




