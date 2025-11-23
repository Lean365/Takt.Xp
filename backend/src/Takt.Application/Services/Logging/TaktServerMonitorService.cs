#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServerMonitorService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述   : 服务器监控服务实现
//===================================================================

using System.Runtime.InteropServices;
using Takt.Application.Dtos.Logging;
using Takt.Shared.Utils;
using Takt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logging;

/// <summary>
/// 服务器监控服务实现
/// </summary>
/// <remarks>
/// 提供以下功能：
/// 1. 获取服务器基本信息：操作系统、处理器、内存等
/// 2. 获取网络接口信息：网卡、IP地址、流量等
/// 3. 实时监控系统资源使用情况
/// </remarks>
public class TaktServerMonitorService : TaktBaseService, ITaktServerMonitorService
{
    private readonly DateTime _startTime;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktServerMonitorService(

        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _startTime = DateTime.Now;
    }

    /// <summary>
    /// 获取服务器基本信息
    /// </summary>
    /// <returns>服务器基本信息DTO对象</returns>
    /// <remarks>
    /// 包含以下信息：
    /// - 操作系统信息：名称、版本、架构
    /// - 处理器信息：名称、核心数
    /// - 系统运行时间
    /// - .NET运行时信息
    /// - 资源使用情况：CPU、内存、磁盘
    /// </remarks>
    public async Task<TaktServerMonitorDto> GetServerInfoAsync()
    {
        return await Task.Run(() =>
        {
            var dto = new TaktServerMonitorDto
            {
                // 操作系统信息
                OsName = RuntimeInformation.OSDescription,
                OsArchitecture = RuntimeInformation.OSArchitecture.ToString(),
                OsVersion = RuntimeInformation.OSDescription,

                // 处理器信息
                ProcessorName = TaktServerMonitorUtils.GetProcessorName(),
                ProcessorCount = Environment.ProcessorCount,

                // 系统运行时间
                SystemStartTime = TaktServerMonitorUtils.GetSystemStartTime(),
                SystemUptime = (DateTime.Now - TaktServerMonitorUtils.GetSystemStartTime()).TotalDays,

                // .NET运行时信息
                DotNetRuntimeVersion = RuntimeInformation.FrameworkDescription,
                ClrVersion = Environment.Version.ToString(),
                DotNetRuntimeDirectory = RuntimeEnvironment.GetRuntimeDirectory()
            };

            try
            {
                // 获取内存信息（转换为字节）
                var (totalMemory, availableMemory) = TaktServerMonitorUtils.GetPhysicalMemory();
                dto.TotalMemory = totalMemory;
                dto.UsedMemory = totalMemory - availableMemory;
                dto.MemoryUsage = (double)dto.UsedMemory / dto.TotalMemory * 100;

                // 获取CPU使用率
                dto.CpuUsage = TaktServerMonitorUtils.GetSystemCpuUsage();

                // 获取所有磁盘信息
                var drives = DriveInfo.GetDrives().Where(d => d.IsReady);
                dto.DiskInfos = drives.Select(d => new TaktDiskInfo
                {
                    DriveName = d.Name,
                    TotalSpace = d.TotalSize,
                    UsedSpace = d.TotalSize - d.AvailableFreeSpace,
                    FreeSpace = d.AvailableFreeSpace,
                    UsageRate = (double)(d.TotalSize - d.AvailableFreeSpace) / d.TotalSize * 100
                }).ToList();
            }
            catch (Exception ex)
            {
                // 记录错误日志
                _logger.Error(L("Audit.ServerMonitor.GetServerInfoFailed", ex.Message), ex);
            }

            return dto;
        });
    }

    /// <summary>
    /// 获取网络信息
    /// </summary>
    /// <returns>网络信息DTO对象列表</returns>
    /// <remarks>
    /// 包含以下信息：
    /// - 网络适配器名称
    /// - MAC地址
    /// - IP地址及地理位置
    /// - 网络流量：发送和接收速率
    /// </remarks>
    public async Task<List<TaktNetworkDto>> GetNetworkInfoAsync()
    {
        var networkInfoList = new List<TaktNetworkDto>();

        try
        {
            // 获取所有网络接口信息
            var interfaces = TaktServerMonitorUtils.GetNetworkInterfaces();
            foreach (var ni in interfaces)
            {
                var networkInfo = new TaktNetworkDto
                {
                    AdapterName = ni.Name,
                    MacAddress = ni.MacAddress,
                    IpAddress = ni.IpAddress,
                    IpLocation = L("Audit.ServerMonitor.UnknownLocation")
                };

                // 获取IP地理位置信息
                networkInfo.IpLocation = await GetLocationAsync(networkInfo.IpAddress);

                // 获取网络流量信息（KB/s）
                var (sendRate, receiveRate) = TaktServerMonitorUtils.GetNetworkInterfaceRates(ni.Name);
                networkInfo.SendRate = sendRate;
                networkInfo.ReceiveRate = receiveRate;

                networkInfoList.Add(networkInfo);
            }
        }
        catch (Exception ex)
        {
            _logger.Error(L("Audit.ServerMonitor.GetNetworkInfoFailed", ex.Message), ex);
        }

        return networkInfoList;
    }

    /// <summary>
    /// 获取IP地址的地理位置信息
    /// </summary>
    /// <param name="ipAddress">IP地址</param>
    /// <returns>地理位置描述</returns>
    /// <remarks>
    /// 使用TaktIpLocationUtils工具类获取IP地理位置信息
    /// </remarks>
    private async Task<string> GetLocationAsync(string ipAddress)
    {
        try
        {
            return await TaktIpUtils.GetLocationAsync(ipAddress);
        }
        catch (Exception ex)
        {
            _logger.Error(L("Audit.ServerMonitor.GetLocationFailed", ipAddress, ex.Message), ex);
            return L("Audit.ServerMonitor.UnknownLocation");
        }
    }
}





