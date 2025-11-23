namespace Takt.Shared.Options
{
    /// <summary>
    /// SignalR缓存配置选项
    /// </summary>
    public class TaktSignalRCacheOptions
    {
        /// <summary>
        /// 默认缓存过期时间（分钟）
        /// </summary>
        public int DefaultCacheMinutes { get; set; } = 30;

        /// <summary>
        /// 最大缓存用户数
        /// </summary>
        public int MaxUsers { get; set; } = 10000;

        /// <summary>
        /// 每个用户最大连接数
        /// </summary>
        public int MaxConnectionsPerUser { get; set; } = 5;

        /// <summary>
        /// 最大缓存大小（MB）
        /// </summary>
        public int MaxCacheSizeMB { get; set; } = 100;

        /// <summary>
        /// 清理间隔时间（分钟）
        /// </summary>
        public int CleanupIntervalMinutes { get; set; } = 5;

        /// <summary>
        /// 是否启用缓存大小限制
        /// </summary>
        public bool EnableSizeLimit { get; set; } = true;

        /// <summary>
        /// 是否启用用户数限制
        /// </summary>
        public bool EnableUserLimit { get; set; } = true;

        /// <summary>
        /// 是否启用连接数限制
        /// </summary>
        public bool EnableConnectionLimit { get; set; } = true;

        /// <summary>
        /// 是否启用自动清理
        /// </summary>
        public bool EnableAutoCleanup { get; set; } = true;
    }
} 

