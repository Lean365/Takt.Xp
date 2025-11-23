//===================================================================
// 项目名 : Takt.Xp
// 文件名 : SystemRestartOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-21 10:00
// 版本号 : V0.0.1
// 描述   : 系统重启配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 系统重启配置选项
    /// </summary>
    public class TaktRestartOptions
    {
        /// <summary>
        /// 是否清除用户会话
        /// </summary>
        public bool ClearUserSessions { get; set; } = true;

        /// <summary>
        /// 是否清除缓存
        /// </summary>
        public bool ClearCache { get; set; } = true;

        /// <summary>
        /// 是否清除实时通信连接
        /// </summary>
        public bool ClearRealTimeConnections { get; set; } = true;

        /// <summary>
        /// 是否清除系统状态
        /// </summary>
        public bool ClearSystemStatus { get; set; } = true;

        /// <summary>
        /// 是否清除安全相关信息
        /// </summary>
        public bool ClearSecurityInfo { get; set; } = true;

        /// <summary>
        /// 是否清除临时数据
        /// </summary>
        public bool ClearTempData { get; set; } = true;

        /// <summary>
        /// 是否清除性能监控数据
        /// </summary>
        public bool ClearPerformanceData { get; set; } = true;

        /// <summary>
        /// 是否清除分布式状态
        /// </summary>
        public bool ClearDistributedState { get; set; } = true;

        /// <summary>
        /// 是否清除 token 黑名单
        /// </summary>
        public bool ClearTokenBlacklist { get; set; } = false;
    }
} 




