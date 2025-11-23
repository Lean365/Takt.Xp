//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 安全认证
// 命名空间 : Takt.Domain.IServices.Security
// 文件名 : ITaktLoginPolicy.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 登录策略接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.IServices.Security
{
  /// <summary>
  /// 登录策略接口
  /// </summary>
  public interface ITaktLoginPolicy
  {
    /// <summary>
    /// 验证登录尝试
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>(是否允许登录, 剩余等待时间(秒))</returns>
    Task<(bool allowed, int? remainingSeconds)> ValidateLoginAttemptAsync(string username);

    /// <summary>
    /// 验证管理员登录尝试(使用更严格的策略)
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>(是否允许登录, 剩余等待时间(秒))</returns>
    Task<(bool allowed, int? remainingSeconds)> ValidateAdminLoginAttemptAsync(string userName);

    /// <summary>
    /// 记录登录失败
    /// </summary>
    /// <param name="username">用户名</param>
    Task RecordFailedLoginAsync(string username);

    /// <summary>
    /// 记录管理员登录失败(使用更严格的策略)
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>任务</returns>
    Task RecordAdminFailedLoginAsync(string userName);

    /// <summary>
    /// 记录登录成功
    /// </summary>
    /// <param name="username">用户名</param>
    Task RecordSuccessfulLoginAsync(string username);

    /// <summary>
    /// 获取剩余登录尝试次数
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>剩余次数</returns>
    Task<int> GetRemainingAttemptsAsync(string userName);

    /// <summary>
    /// 获取账户锁定剩余时间(秒)
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns>剩余秒数</returns>
    Task<int> GetLockoutRemainingSecondsAsync(string userName);
  }
}



