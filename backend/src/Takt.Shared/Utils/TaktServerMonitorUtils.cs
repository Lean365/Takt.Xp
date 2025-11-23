//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServerMonitorUtils.cs
// 创建者 : Claude
// 创建时间: 2024-02-18 11:30
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : 服务器监控工具类，提供系统资源监控、进程管理、网络监控等功能
//===================================================================

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Takt.Shared.Utils;

/// <summary>
/// 服务器监控工具类
/// </summary>
/// <remarks>
/// 提供以下功能：
/// 1. 系统资源监控：CPU、内存、磁盘使用情况
/// 2. 进程管理：获取进程列表、CPU使用率等
/// 3. 网络监控：网络接口信息、流量统计
/// </remarks>
public static class TaktServerMonitorUtils
{
    private static readonly Dictionary<string, (long LastBytesReceived, long LastBytesSent, DateTime LastCheck)> _networkStats;
    private static readonly Process _currentProcess;
    private static readonly DateTime _startTime;

    static TaktServerMonitorUtils()
    {
        _networkStats = new Dictionary<string, (long, long, DateTime)>();
        _currentProcess = Process.GetCurrentProcess();
        _startTime = DateTime.Now;
    }

    /// <summary>
    /// 获取系统CPU使用率
    /// </summary>
    /// <returns>CPU使用率百分比（0-100）</returns>
    /// <remarks>
    /// 返回所有CPU核心的平均使用率
    /// 值范围：0-100
    /// 精确到小数点后2位
    /// </remarks>
    public static double GetSystemCpuUsage()
    {
        try
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetProcesses().Sum(p => 
            {
                try 
                {
                    return p.TotalProcessorTime.TotalMilliseconds;
                }
                catch 
                {
                    return 0;
                }
            });

            Thread.Sleep(1000); // 采样间隔1秒

            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetProcesses().Sum(p => 
            {
                try 
                {
                    return p.TotalProcessorTime.TotalMilliseconds;
                }
                catch 
                {
                    return 0;
                }
            });

            var cpuUsedMs = endCpuUsage - startCpuUsage;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds * Environment.ProcessorCount;

            var cpuUsageTotal = cpuUsedMs / (totalMsPassed * Environment.ProcessorCount) * 100;

            return Math.Round(cpuUsageTotal, 2);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    /// <summary>
    /// 获取物理内存信息
    /// </summary>
    /// <returns>物理内存总量和可用物理内存，单位：字节</returns>
    /// <remarks>
    /// 使用LibreHardwareMonitor获取内存信息
    /// 返回值单位为字节，如需其他单位请自行转换
    /// </remarks>
    public static (ulong Total, ulong Available) GetPhysicalMemory()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var memoryStatus = new MEMORYSTATUSEX();
                if (GlobalMemoryStatusEx(memoryStatus))
                {
                    return (memoryStatus.ullTotalPhys, memoryStatus.ullAvailPhys);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var memInfo = File.ReadAllLines("/proc/meminfo");
                ulong total = 0, available = 0;

                foreach (var line in memInfo)
                {
                    if (line.StartsWith("MemTotal:"))
                    {
                        total = ParseMemInfo(line) * 1024; // Convert KB to bytes
                    }
                    else if (line.StartsWith("MemAvailable:"))
                    {
                        available = ParseMemInfo(line) * 1024; // Convert KB to bytes
                    }
                }

                if (total > 0)
                {
                    return (total, available);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                var output = ExecuteCommand("sysctl", "-n hw.memsize hw.usermem");
                var values = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (values.Length >= 2 && ulong.TryParse(values[0], out var total) && ulong.TryParse(values[1], out var available))
                {
                    return (total, available);
                }
            }
        }
        catch (Exception)
        {
            // 记录错误但继续执行
        }
        return (0, 0);
    }

    /// <summary>
    /// 获取处理器名称
    /// </summary>
    /// <returns>处理器名称，如获取失败则返回"Unknown"</returns>
    /// <remarks>
    /// 返回完整的处理器名称，包含制造商、型号等信息
    /// </remarks>
    public static string GetProcessorName()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
                if (key != null)
                {
                    return key.GetValue("ProcessorNameString")?.ToString() ?? "Unknown";
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var cpuInfo = File.ReadAllLines("/proc/cpuinfo");
                var modelName = cpuInfo.FirstOrDefault(line => line.StartsWith("model name"));
                if (modelName != null)
                {
                    return modelName.Split(':')[1].Trim();
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return ExecuteCommand("sysctl", "-n machdep.cpu.brand_string").Trim();
            }
        }
        catch { }
        return "Unknown";
    }

    /// <summary>
    /// 获取系统启动时间
    /// </summary>
    /// <returns>系统启动的时间点</returns>
    /// <remarks>
    /// 通过Environment.TickCount64计算系统启动时间
    /// 精确到毫秒级别
    /// </remarks>
    public static DateTime GetSystemStartTime()
    {
        return DateTime.Now - TimeSpan.FromMilliseconds(Environment.TickCount64);
    }

    /// <summary>
    /// 获取网络接口信息
    /// </summary>
    /// <returns>网络接口信息列表，每项包含接口名称、IP地址和MAC地址</returns>
    /// <remarks>
    /// 返回所有可用的网络接口信息
    /// IP地址格式为IPv4地址
    /// MAC地址格式为12位十六进制数
    /// </remarks>
    public static List<(string Name, string IpAddress, string MacAddress)> GetNetworkInterfaces()
    {
        var interfaces = new List<(string Name, string IpAddress, string MacAddress)>();
        try
        {
            // 获取所有网络接口
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up &&
                            (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                             ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211));

            foreach (var ni in networkInterfaces)
            {
                var ipProps = ni.GetIPProperties();
                var ipAddress = ipProps.UnicastAddresses
                    .FirstOrDefault(addr => addr.Address.AddressFamily == AddressFamily.InterNetwork)
                    ?.Address.ToString() ?? string.Empty;
                
                var macAddress = string.Join(":", ni.GetPhysicalAddress()
                    .GetAddressBytes()
                    .Select(b => b.ToString("X2")));

                interfaces.Add((ni.Name, ipAddress, macAddress));
            }
        }
        catch (Exception)
        {
            // 记录错误但继续执行
        }
        return interfaces;
    }

    /// <summary>
    /// 获取网络接口的流量信息
    /// </summary>
    /// <param name="interfaceName">网络接口名称</param>
    /// <returns>发送和接收速率（KB/s）的元组</returns>
    /// <remarks>
    /// 返回值中：
    /// SendRate: 发送速率，单位KB/s
    /// ReceiveRate: 接收速率，单位KB/s
    /// </remarks>
    public static (double SendRate, double ReceiveRate) GetNetworkInterfaceRates(string interfaceName)
    {
        try
        {
            var ni = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(n => n.Name == interfaceName);

            if (ni != null)
            {
                var stats = ni.GetIPv4Statistics();
                var now = DateTime.Now;

                if (!_networkStats.ContainsKey(interfaceName))
                {
                    _networkStats[interfaceName] = (stats.BytesReceived, stats.BytesSent, now);
                    return (0, 0);
                }

                var (lastBytesReceived, lastBytesSent, lastCheck) = _networkStats[interfaceName];
                var timeDiff = (now - lastCheck).TotalSeconds;

                if (timeDiff > 0)
                {
                    var receiveDiff = stats.BytesReceived - lastBytesReceived;
                    var sendDiff = stats.BytesSent - lastBytesSent;

                    _networkStats[interfaceName] = (stats.BytesReceived, stats.BytesSent, now);

                    return (
                        Math.Round(sendDiff / timeDiff / 1024, 2),     // KB/s
                        Math.Round(receiveDiff / timeDiff / 1024, 2)    // KB/s
                    );
                }
            }
        }
        catch (Exception)
        {
            // 记录错误但继续执行
        }
        return (0, 0);
    }

    /// <summary>
    /// 获取系统内存使用率
    /// </summary>
    /// <returns>内存使用率百分比（0-100）</returns>
    /// <remarks>
    /// 计算方式：已用内存/总内存 * 100
    /// 值范围：0-100
    /// 精确到小数点后2位
    /// </remarks>
    public static double GetSystemMemoryUsage()
    {
        try
        {
            var totalMemory = GetPhysicalMemory().Total;
            if (totalMemory > 0)
            {
                var availableMemory = GetPhysicalMemory().Available;
                var usedMemory = totalMemory - availableMemory;
                return Math.Round((double)usedMemory / totalMemory * 100, 2);
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// Windows内存状态结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public MEMORYSTATUSEX()
        {
            dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
        }
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);

    private static string ExecuteCommand(string command, string arguments)
    {
        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }
        catch
        {
            return string.Empty;
        }
    }

    private static ulong ParseMemInfo(string line)
    {
        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2 && ulong.TryParse(parts[1], out var value))
        {
            return value;
        }
        return 0;
    }
} 






