//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 安全认证
// 命名空间 : Takt.Domain.IServices.Security
// 文件名 : ITaktSingleUserLoginService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 单用户登录服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using Takt.Shared.Models;

namespace Takt.Domain.IServices.Security
{
  /// <summary>
  /// 单用户登录服务接口
  /// </summary>
  /// <remarks>
  /// 核心设计原则：
  /// 1. 新登录阻断：检测到活跃会话时，新登录立即失败
  /// 2. 旧会话保护：已存在的业务操作不受任何影响
  /// 3. 状态强一致：通过数据库事务保证原子性
  /// 4. 操作可审计：完整记录登录拦截事件，数据库追踪方式
  /// </remarks>
  public interface ITaktSingleUserLoginService
  {
    /// <summary>
    /// 检查用户是否可以登录 - 严格新登录阻断机制
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    /// <param name="ipAddress">IP地址</param>
    /// <param name="deviceFingerprint">设备指纹</param>
    /// <returns>是否可以登录及原因</returns>
    Task<(bool canLogin, string? reason)> CheckUserLoginAsync(long userId, string deviceId, string? ipAddress = null, TaktLoginFingerprint? deviceFingerprint = null);

    /// <summary>
    /// 处理用户登录 - 状态强一致性保证
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    /// <param name="connectionId">连接ID</param>
    /// <param name="ipAddress">IP地址</param>
    /// <param name="userAgent">用户代理</param>
    /// <param name="userName">用户名</param>
    /// <returns>处理结果</returns>
    Task<TaktApiResult> HandleUserLoginAsync(long userId, string deviceId, string connectionId, string? ipAddress = null, string? userAgent = null, string? userName = null);

    /// <summary>
    /// 处理用户登出 - 旧会话保护
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="connectionId">连接ID</param>
    /// <param name="userName">用户名</param>
    /// <returns>是否成功</returns>
    Task<bool> HandleUserLogoutAsync(long userId, string connectionId, string? userName = null);

    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="reason">下线原因</param>
    /// <returns>是否成功</returns>
    Task<bool> ForceUserOfflineAsync(long userId, string reason = "管理员强制下线");

    /// <summary>
    /// 检查设备是否被允许
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    /// <returns>是否允许</returns>
    Task<bool> IsDeviceAllowedAsync(long userId, string deviceId);

    /// <summary>
    /// 验证设备指纹
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="deviceId">设备ID</param>
    /// <param name="fingerprint">设备指纹</param>
    /// <returns>是否有效</returns>
    Task<bool> ValidateDeviceFingerprintAsync(long userId, string deviceId, string fingerprint);

    /// <summary>
    /// 清理过期会话
    /// </summary>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredSessionsAsync();
  }
}
