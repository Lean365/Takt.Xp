//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktCacheOptions.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 16:30
// 版本号 : V0.0.1
// 描述    : 缓存配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 缓存配置选项
    /// </summary>
    public class TaktCacheOptions
    {
        /// <summary>
        /// 缓存提供程序类型
        /// </summary>
        public CacheProviderType Provider { get; set; } = CacheProviderType.Memory;

        /// <summary>
        /// 默认过期时间（分钟）
        /// </summary>
        public int DefaultExpirationMinutes { get; set; } = 30;

        /// <summary>
        /// 是否启用滑动过期
        /// </summary>
        public bool EnableSlidingExpiration { get; set; } = true;

        /// <summary>
        /// 是否启用多级缓存
        /// </summary>
        public bool EnableMultiLevelCache { get; set; } = false;

        /// <summary>
        /// 内存缓存配置
        /// </summary>
        public TaktMemoryCacheOptions Memory { get; set; } = new();

        /// <summary>
        /// Redis缓存配置
        /// </summary>
        public TaktRedisCacheOptions Redis { get; set; } = new();
    }

    /// <summary>
    /// 内存缓存配置选项
    /// </summary>
    public class TaktMemoryCacheOptions
    {
        /// <summary>
        /// 压缩阈值（字节）
        /// </summary>
        public long CompactionThreshold { get; set; } = 1024 * 1024; // 1MB

        /// <summary>
        /// 扫描频率（秒）
        /// </summary>
        public int ExpirationScanFrequency { get; set; } = 60;

        /// <summary>
        /// 大小限制（字节）
        /// </summary>
        public long SizeLimit { get; set; } = 100 * 1024 * 1024; // 100MB
    }

    /// <summary>
    /// Redis缓存配置选项
    /// </summary>
    public class TaktRedisCacheOptions
    {
        /// <summary>
        /// 是否启用Redis缓存
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; } = "Lean.Takt";

        /// <summary>
        /// 默认数据库
        /// </summary>
        public int DefaultDatabase { get; set; } = 0;

        /// <summary>
        /// 连接超时时间(毫秒)
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// 同步超时时间(毫秒)
        /// </summary>
        public int SyncTimeout { get; set; } = 5000;

        /// <summary>
        /// 是否允许管理员操作
        /// </summary>
        public bool AllowAdmin { get; set; }

        /// <summary>
        /// 是否使用SSL
        /// </summary>
        public bool Ssl { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 是否启用压缩
        /// </summary>
        public bool EnableCompression { get; set; } = true;

        /// <summary>
        /// 压缩阈值（字节）
        /// </summary>
        public int CompressionThreshold { get; set; } = 1024; // 1KB
    }

    /// <summary>
    /// 缓存提供程序类型
    /// </summary>
    public enum CacheProviderType
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        Memory = 0,

        /// <summary>
        /// Redis缓存
        /// </summary>
        Redis = 1,

        /// <summary>
        /// 多级缓存
        /// </summary>
        MultiLevel = 2
    }
} 




