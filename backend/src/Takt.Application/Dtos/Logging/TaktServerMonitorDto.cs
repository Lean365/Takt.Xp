//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServerDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器信息DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Logging;

/// <summary>
/// 磁盘信息DTO
/// </summary>
public class TaktDiskInfo
{
    /// <summary>
    /// 驱动器名称
    /// </summary>
    public string DriveName { get; set; } = string.Empty;

    /// <summary>
    /// 总空间(字节)
    /// </summary>
    public long TotalSpace { get; set; }

    /// <summary>
    /// 已用空间(字节)
    /// </summary>
    public long UsedSpace { get; set; }

    /// <summary>
    /// 可用空间(字节)
    /// </summary>
    public long FreeSpace { get; set; }

    /// <summary>
    /// 使用率(%)
    /// </summary>
    public double UsageRate { get; set; }
}

/// <summary>
/// 服务器信息DTO
/// </summary>
public class TaktServerMonitorDto
{
    /// <summary>
    /// 构造函数，初始化默认值
    /// </summary>
    public TaktServerMonitorDto()
    {
        OsName = "Unknown";
        OsArchitecture = "Unknown";
        OsVersion = "Unknown";
        ProcessorName = "Unknown";
        ProcessorCount = 0;
        SystemStartTime = DateTime.Now;
        SystemUptime = 0;
        DotNetRuntimeVersion = string.Empty;
        ClrVersion = string.Empty;
        DotNetRuntimeDirectory = string.Empty;
    }

    /// <summary>
    /// CPU使用率
    /// </summary>
    public double CpuUsage { get; set; }

    /// <summary>
    /// 总内存(GB)
    /// </summary>
    public double TotalMemory { get; set; }

    /// <summary>
    /// 已用内存(GB)
    /// </summary>
    public double UsedMemory { get; set; }

    /// <summary>
    /// 内存使用率
    /// </summary>
    public double MemoryUsage { get; set; }

    /// <summary>
    /// 总磁盘空间(GB)
    /// </summary>
    public double TotalDiskSpace { get; set; }

    /// <summary>
    /// 已用磁盘空间(GB)
    /// </summary>
    public double UsedDiskSpace { get; set; }

    /// <summary>
    /// 磁盘使用率
    /// </summary>
    public double DiskUsage { get; set; }

    /// <summary>
    /// 磁盘信息列表
    /// </summary>
    public List<TaktDiskInfo> DiskInfos { get; set; } = new();

    /// <summary>
    /// 操作系统
    /// </summary>
    [Required]
    public required string OsName { get; set; }

    /// <summary>
    /// 系统架构
    /// </summary>
    [Required]
    public required string OsArchitecture { get; set; }

    /// <summary>
    /// 系统版本
    /// </summary>
    [Required]
    public required string OsVersion { get; set; }

    /// <summary>
    /// 处理器信息
    /// </summary>
    [Required]
    public required string ProcessorName { get; set; }

    /// <summary>
    /// 处理器核心数
    /// </summary>
    public int ProcessorCount { get; set; }

    /// <summary>
    /// 系统启动时间
    /// </summary>
    public DateTime SystemStartTime { get; set; }

    /// <summary>
    /// 系统运行时间(天)
    /// </summary>
    public double SystemUptime { get; set; }

    /// <summary>
    /// .NET运行时版本
    /// </summary>
    [Required]
    public required string DotNetRuntimeVersion { get; set; }

    /// <summary>
    /// CLR版本
    /// </summary>
    [Required]
    public required string ClrVersion { get; set; }

    /// <summary>
    /// .NET运行时目录
    /// </summary>
    [Required]
    public required string DotNetRuntimeDirectory { get; set; }
}


/// <summary>
/// 网络信息DTO
/// </summary>
public class TaktNetworkDto
{
    /// <summary>
    /// 构造函数，初始化默认值
    /// </summary>
    public TaktNetworkDto()
    {
        AdapterName = string.Empty;
        MacAddress = string.Empty;
        IpAddress = string.Empty;
        IpLocation = string.Empty;
        BytesSent = 0;
        BytesReceived = 0;
        SendRate = 0;
        ReceiveRate = 0;
    }

    /// <summary>
    /// 网卡名称
    /// </summary>
    [Required]
    public required string AdapterName { get; set; }

    /// <summary>
    /// MAC地址
    /// </summary>
    [Required]
    public required string MacAddress { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [Required]
    public required string IpAddress { get; set; }

    /// <summary>
    /// IP地址位置信息
    /// </summary>
    [Required]
    public required string IpLocation { get; set; }

    /// <summary>
    /// 发送字节数
    /// </summary>
    public long BytesSent { get; set; }

    /// <summary>
    /// 接收字节数
    /// </summary>
    public long BytesReceived { get; set; }

    /// <summary>
    /// 发送速率(KB/s)
    /// </summary>
    public double SendRate { get; set; }

    /// <summary>
    /// 接收速率(KB/s)
    /// </summary>
    public double ReceiveRate { get; set; }
}







