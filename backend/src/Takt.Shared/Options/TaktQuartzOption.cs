//===================================================================
// 项目名 : Takt.Shared.Options
// 文件名 : TaktQuartzOption.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 定时任务配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 定时任务配置选项
    /// </summary>
    public class TaktQuartzOption
    {
        /// <summary>
        /// 配置节点
        /// </summary>
        public const string Position = "TaktQuartz";

        /// <summary>
        /// 是否启用定时任务
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; } = "TaktQuartzScheduler";

        /// <summary>
        /// 表名前缀
        /// </summary>
        public string TablePrefix { get; set; } = "QRTZ_";

        /// <summary>
        /// 序列化类型（json/binary）
        /// </summary>
        public string SerializerType { get; set; } = "json";

        /// <summary>
        /// 是否使用数据库存储
        /// </summary>
        public bool UseDatabase { get; set; }

        /// <summary>
        /// 数据库类型（SQLServer/MySQL/PostgreSQL/Oracle/SQLite）
        /// </summary>
        public string DbType { get; set; } = "SQLServer";

        /// <summary>
        /// 集群配置
        /// </summary>
        public ClusterConfig Cluster { get; set; } = new();

        /// <summary>
        /// 线程池配置
        /// </summary>
        public ThreadPoolConfig ThreadPool { get; set; } = new();
    }

    /// <summary>
    /// 集群配置
    /// </summary>
    public class ClusterConfig
    {
        /// <summary>
        /// 是否启用集群
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 检查间隔（毫秒）
        /// </summary>
        public long CheckinInterval { get; set; } = 15000;

        /// <summary>
        /// 检查超时（毫秒）
        /// </summary>
        public long CheckinMisfireThreshold { get; set; } = 15000;
    }

    /// <summary>
    /// 线程池配置
    /// </summary>
    public class ThreadPoolConfig
    {
        /// <summary>
        /// 最大线程数
        /// </summary>
        public int MaxConcurrency { get; set; } = 10;

        /// <summary>
        /// 线程优先级
        /// </summary>
        public int ThreadPriority { get; set; } = 5;
    }
} 



