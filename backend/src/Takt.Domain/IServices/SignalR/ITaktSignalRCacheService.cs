//===================================================================
// 项目名 : Takt.Xp
// 模块名 : SignalR
// 命名空间 : Takt.Domain.IServices.SignalR
// 文件名 : ITaktSignalRCacheService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : SignalR缓存服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using Takt.Domain.Entities.Routine.SignalR;

namespace Takt.Domain.IServices.SignalR
{
  /// <summary>
  /// SignalR缓存服务接口
  /// </summary>
  public interface ITaktSignalRCacheService
  {
    /// <summary>
    /// 保存用户信息到缓存
    /// </summary>
    Task SetAsync(string key, TaktSignalROnline user, TimeSpan? expiry = null);

    /// <summary>
    /// 从缓存获取用户信息
    /// </summary>
    Task<TaktSignalROnline?> GetAsync(string key);

    /// <summary>
    /// 从缓存删除用户信息
    /// </summary>
    Task RemoveAsync(string key);

    /// <summary>
    /// 获取所有匹配模式的键
    /// </summary>
    Task<List<string>> GetKeysByPatternAsync(string pattern);

    /// <summary>
    /// 设置用户连接列表
    /// </summary>
    Task SetConnectionsAsync(long userId, List<string> connections);

    /// <summary>
    /// 获取用户连接列表
    /// </summary>
    Task<List<string>> GetConnectionsAsync(long userId);

    /// <summary>
    /// 删除用户连接列表
    /// </summary>
    Task RemoveConnectionsAsync(long userId);

    /// <summary>
    /// 获取在线用户列表
    /// </summary>
    Task<List<TaktSignalROnline>> GetOnlineUsersAsync();

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    Task UpdateLastActivityAsync(string connectionId, DateTime lastActivity);

    /// <summary>
    /// 更新用户心跳时间
    /// </summary>
    Task UpdateHeartbeatAsync(string connectionId, DateTime heartbeat);

    /// <summary>
    /// 设置用户在线状态
    /// </summary>
    Task SetOnlineStatusAsync(string connectionId, int status);
  }
}
