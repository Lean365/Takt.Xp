//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktIdentitySessionManager.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 身份认证会话管理接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Takt.Domain.Models.Identity;

namespace Takt.Domain.IServices.Extensions
{
  /// <summary>
  /// 身份认证会话管理接口
  /// </summary>
  public interface ITaktIdentitySessionManager
  {
    /// <summary>
    /// 创建会话
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="userName">用户名</param>
    /// <param name="ipAddress">IP地址</param>
    /// <param name="userAgent">用户代理</param>
    /// <returns>会话ID</returns>
    Task<string> CreateSessionAsync(string userId, string userName, string ipAddress, string userAgent);

    /// <summary>
    /// 获取会话信息
    /// </summary>
    /// <param name="sessionId">会话ID</param>
    /// <returns>会话信息</returns>
    Task<TaktIdentitySessionInfo> GetSessionInfoAsync(string sessionId);

    /// <summary>
    /// 获取用户所有会话
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>会话信息列表</returns>
    Task<List<TaktIdentitySessionInfo>> GetUserSessionsAsync(string userId);

    /// <summary>
    /// 更新会话访问时间
    /// </summary>
    /// <param name="sessionId">会话ID</param>
    Task UpdateSessionAccessTimeAsync(string sessionId);

    /// <summary>
    /// 移除会话
    /// </summary>
    /// <param name="sessionId">会话ID</param>
    Task RemoveSessionAsync(string sessionId);

    /// <summary>
    /// 移除用户所有会话
    /// </summary>
    /// <param name="userId">用户ID</param>
    Task RemoveUserSessionsAsync(string userId);
  }
}



