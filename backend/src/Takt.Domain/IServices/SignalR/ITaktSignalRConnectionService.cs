//===================================================================
// 项目名 : Takt.Xp
// 模块名 : SignalR
// 命名空间 : Takt.Domain.IServices.SignalR
// 文件名 : ITaktSignalRConnectionService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : SignalR连接服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using Takt.Domain.Entities.Logging;
using Takt.Domain.Entities.Routine.SignalR;

namespace Takt.Domain.IServices.SignalR
{
  /// <summary>
  /// SignalR连接服务接口 - 专注于连接管理和设备状态管理
  /// </summary>
  /// <remarks>
  /// 此接口专门用于管理SignalR连接相关的功能，包括：
  /// 1. 用户设备连接管理
  /// 2. 连接状态监控
  /// 3. 设备活动跟踪
  /// 4. 连接断开处理
  /// 
  /// 注意：此接口与ITaktSignalROnlineService不同，
  /// ITaktSignalROnlineService专注于在线用户数据管理（Application层），
  /// 而此接口专注于连接技术管理（Infrastructure层）
  /// </remarks>
  public interface ITaktSignalRConnectionService
  {
    /// <summary>
    /// 添加用户设备
    /// </summary>
    /// <param name="device">设备信息</param>
    /// <param name="connectionId">连接ID</param>
    /// <param name="userAgent">用户代理信息</param>
    /// <param name="userName">用户名</param>
    Task AddUserDevice(TaktDeviceLog device, string connectionId, string? userAgent = null, string? userName = null);

    /// <summary>
    /// 获取用户在线设备列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>在线设备列表</returns>
    Task<List<TaktSignalROnline>> GetUserOnlineDevicesAsync(string userId);


    /// <summary>
    /// 移除用户所有设备
    /// </summary>
    /// <param name="userId">用户ID</param>
    Task RemoveUserAllDevices(string userId);

    /// <summary>
    /// 移除用户设备
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    Task RemoveUserDevice(string userId, string deviceId);

    /// <summary>
    /// 获取用户设备
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="connectionId">连接ID</param>
    /// <returns>设备信息</returns>
    Task<TaktDeviceLog> GetUserDeviceAsync(string userId, string connectionId);

    /// <summary>
    /// 检查用户是否在线
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否在线</returns>
    bool IsUserOnline(string userId);

    /// <summary>
    /// 更新设备活动时间
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    void UpdateDeviceActivity(string userId, string deviceId);

    /// <summary>
    /// 获取所有在线设备
    /// </summary>
    /// <returns>在线设备列表</returns>
    List<TaktSignalROnline> GetAllOnlineDevices();

    /// <summary>
    /// 保存在线用户
    /// </summary>
    /// <param name="user">在线用户信息</param>
    /// <param name="userName">用户名</param>
    Task SaveOnlineUserAsync(TaktSignalROnline user, string? userName = null);

    /// <summary>
    /// 获取在线用户信息
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>在线用户信息</returns>
    Task<TaktSignalROnline> GetOnlineUserAsync(string connectionId);

    /// <summary>
    /// 根据设备ID获取在线用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    /// <returns>在线用户信息</returns>
    Task<TaktSignalROnline> GetOnlineUserByDeviceAsync(long userId, string deviceId);

    /// <summary>
    /// 获取设备信息
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>设备ID</returns>
    Task<string> GetDeviceInfo(string connectionId);

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteOnlineUserAsync(string connectionId);

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>连接ID列表</returns>
    Task<List<string>> GetConnectionIdsAsync(long userId);

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    Task UpdateUserLastActiveTimeAsync(string connectionId);

    /// <summary>
    /// 更新用户心跳时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    Task UpdateUserHeartbeatAsync(string connectionId);

    /// <summary>
    /// 批量更新所有在线用户的心跳时间
    /// </summary>
    /// <returns>更新的记录数</returns>
    Task<int> UpdateAllOnlineUsersHeartbeatAsync();

    /// <summary>
    /// 确保在线用户记录存在
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceInfo">设备信息</param>
    /// <param name="userName">用户名</param>
    Task EnsureOnlineUserRecordAsync(string connectionId, long userId, TaktDeviceLog deviceInfo, string? userName = null);

    /// <summary>
    /// 断开用户连接
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    Task DisconnectUserAsync(string connectionId);
  }
}





