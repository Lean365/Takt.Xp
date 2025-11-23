//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMonitoringOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 监控配置选项
//===================================================================

namespace Takt.Shared.Options;

/// <summary>
/// 监控配置选项
/// </summary>
public class TaktMonitoringOptions
{
    /// <summary>
    /// 是否启用监控
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 刷新间隔(秒)
    /// </summary>
    public int RefreshInterval { get; set; }

    /// <summary>
    /// 进程过滤器
    /// </summary>
    public ProcessFilterOptions ProcessFilter { get; set; }

    /// <summary>
    /// 网络过滤器
    /// </summary>
    public NetworkFilterOptions NetworkFilter { get; set; }

    /// <summary>
    /// 服务过滤器
    /// </summary>
    public ServiceFilterOptions ServiceFilter { get; set; }

    /// <summary>
    /// 告警阈值
    /// </summary>
    public AlertThresholdOptions Alerts { get; set; }

    /// <summary>
    /// 初始化监控配置选项，设置默认值
    /// </summary>
    public TaktMonitoringOptions()
    {
        ProcessFilter = new ProcessFilterOptions();
        NetworkFilter = new NetworkFilterOptions();
        ServiceFilter = new ServiceFilterOptions();
        Alerts = new AlertThresholdOptions();
        Enabled = true;
        RefreshInterval = 30;
    }
}

/// <summary>
/// 进程过滤器选项
/// </summary>
public class ProcessFilterOptions
{
    /// <summary>
    /// 是否排除系统进程
    /// </summary>
    public bool ExcludeSystemProcesses { get; set; }

    /// <summary>
    /// 最小内存使用量(MB)
    /// </summary>
    public int MinMemoryUsageMB { get; set; }

    /// <summary>
    /// 最大进程数量
    /// </summary>
    public int MaxProcessCount { get; set; }
}

/// <summary>
/// 网络过滤器选项
/// </summary>
public class NetworkFilterOptions
{
    /// <summary>
    /// 是否排除回环接口
    /// </summary>
    public bool ExcludeLoopback { get; set; }

    /// <summary>
    /// 是否排除禁用的接口
    /// </summary>
    public bool ExcludeDisabled { get; set; }
}

/// <summary>
/// 服务过滤器选项
/// </summary>
public class ServiceFilterOptions
{
    /// <summary>
    /// 是否排除禁用的服务
    /// </summary>
    public bool ExcludeDisabled { get; set; }

    /// <summary>
    /// 是否排除系统服务
    /// </summary>
    public bool ExcludeSystemServices { get; set; }
}

/// <summary>
/// 告警阈值选项
/// </summary>
public class AlertThresholdOptions
{
    /// <summary>
    /// CPU使用率阈值(%)
    /// </summary>
    public int CpuThreshold { get; set; }

    /// <summary>
    /// 内存使用率阈值(%)
    /// </summary>
    public int MemoryThreshold { get; set; }

    /// <summary>
    /// 磁盘使用率阈值(%)
    /// </summary>
    public int DiskThreshold { get; set; }
} 




