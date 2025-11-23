//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktServerMonitorService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器监控服务接口
//===================================================================

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktServerMonitorService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器监控服务接口
//===================================================================

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktServerMonitorService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器监控服务接口
//===================================================================

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktServerMonitorService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 服务器监控服务接口
//===================================================================

using Takt.Application.Dtos.Logging;

namespace Takt.Application.Services.Logging;

/// <summary>
/// 服务器监控服务接口
/// </summary>
public interface ITaktServerMonitorService
{
    /// <summary>
    /// 获取服务器基本信息
    /// </summary>
    /// <returns>服务器基本信息</returns>
    Task<TaktServerMonitorDto> GetServerInfoAsync();

    /// <summary>
    /// 获取网络信息
    /// </summary>
    /// <returns>网络信息列表</returns>
    Task<List<TaktNetworkDto>> GetNetworkInfoAsync();
}




