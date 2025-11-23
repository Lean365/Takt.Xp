namespace Takt.Shared.Options
{
    /// <summary>
    /// 日志归档配置选项
    /// </summary>
    public class TaktLogArchiveOptions
    {
        /// <summary>
        /// 是否启用自动归档
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 归档周期(月)
        /// </summary>
        public int ArchivePeriodMonths { get; set; } = 1;

        /// <summary>
        /// 归档文件保存路径
        /// </summary>
        public string ArchivePath { get; set; } = "Logs/Archive";

        /// <summary>
        /// 归档文件命名格式
        /// </summary>
        public string FileNameFormat { get; set; } = "logs_{0:yyyy_MM}.zip";

        /// <summary>
        /// 每次归档的最大记录数
        /// </summary>
        public int BatchSize { get; set; } = 1000;

        /// <summary>
        /// 归档执行时间(Cron表达式)
        /// </summary>
        public string CronExpression { get; set; } = "0 0 1 1 * ?"; // 每月1号凌晨1点
    }
} 

