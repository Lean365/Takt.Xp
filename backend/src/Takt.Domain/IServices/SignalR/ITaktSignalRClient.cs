//===================================================================
// 项目名 : Takt.Xp
// 模块名 : SignalR
// 命名空间 : Takt.Domain.IServices.SignalR
// 文件名 : ITaktSignalRClient.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : SignalR客户端接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.IServices.SignalR
{
  /// <summary>
  /// SignalR客户端接口 - 专注于客户端接收方法
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-24
  /// </remarks>
  public interface ITaktSignalRClient
  {
    #region 基础连接管理

    /// <summary>
    /// 接收消息
    /// </summary>
    Task ReceiveMessage(object message);

    /// <summary>
    /// 接收心跳
    /// </summary>
    Task ReceiveHeartbeat(DateTime timestamp);

    /// <summary>
    /// 强制下线通知
    /// </summary>
    /// <param name="message">通知消息</param>
    Task ForceOffline(string message);

    /// <summary>
    /// 登录尝试通知
    /// </summary>
    /// <param name="deviceInfo">尝试登录的设备信息</param>
    /// <param name="attemptTime">尝试登录时间</param>
    Task LoginAttemptDetected(object deviceInfo, DateTime attemptTime);

    #endregion

    #region 用户状态管理

    /// <summary>
    /// 用户上线通知
    /// </summary>
    Task UserOnline(long userId);

    /// <summary>
    /// 用户下线通知
    /// </summary>
    Task UserOffline(long userId);

    #endregion

    #region 群组管理

    /// <summary>
    /// 加入群组通知
    /// </summary>
    Task JoinedGroup(string connectionId, string groupName);

    /// <summary>
    /// 离开群组通知
    /// </summary>
    Task LeftGroup(string connectionId, string groupName);

    #endregion

    #region 系统通知

    /// <summary>
    /// 接收系统广播
    /// </summary>
    Task ReceiveBroadcast(TaktRealTimeNotification notification);

    /// <summary>
    /// 接收个人通知
    /// </summary>
    Task ReceivePersonalNotice(TaktRealTimeNotification notification);

    #endregion
  }



  /// <summary>
  /// SignalR Hub 接口
  /// </summary>
  public interface ITaktSignalRHub
  {
    /// <summary>
    /// 断开客户端连接
    /// </summary>
    Task DisconnectClientAsync(string connectionId);

    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="reason">下线原因</param>
    Task ForceUserOffline(long userId, string reason = "管理员强制下线");

    /// <summary>
    /// 批量强制用户下线
    /// </summary>
    /// <param name="userIds">用户ID列表</param>
    /// <param name="reason">下线原因</param>
    Task ForceUsersOffline(List<long> userIds, string reason = "管理员批量强制下线");

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="content">消息内容</param>
    /// <returns></returns>
    Task SendMessageAsync(string userId, string content);
  }
}


