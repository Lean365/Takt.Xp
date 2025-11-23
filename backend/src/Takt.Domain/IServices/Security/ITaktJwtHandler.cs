//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 安全认证
// 命名空间 : Takt.Domain.IServices.Security
// 文件名 : ITaktJwtHandler.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : JWT处理器接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================
using System.Threading.Tasks;
using Takt.Domain.Entities.Identity;

namespace Takt.Domain.IServices.Security;

/// <summary>
/// JWT处理器接口
/// </summary>
public interface ITaktJwtHandler
{
  /// <summary>
  /// 生成访问令牌
  /// </summary>
  /// <param name="user">用户信息</param>
  /// <param name="roles">角色列表</param>
  /// <param name="permissions">权限列表</param>
  /// <returns>访问令牌</returns>
  Task<string> GenerateAccessTokenAsync(TaktUser user, string[] roles, string[] permissions);

  /// <summary>
  /// 生成刷新令牌
  /// </summary>
  /// <returns>刷新令牌</returns>
  Task<string> GenerateRefreshTokenAsync();

  /// <summary>
  /// 验证访问令牌
  /// </summary>
  /// <param name="token">访问令牌</param>
  /// <returns>验证结果</returns>
  Task<bool> ValidateAccessTokenAsync(string token);

  /// <summary>
  /// 验证刷新令牌
  /// </summary>
  /// <param name="token">刷新令牌</param>
  /// <returns>验证结果</returns>
  Task<bool> ValidateRefreshTokenAsync(string token);

  /// <summary>
  /// 从令牌中获取用户ID
  /// </summary>
  /// <param name="token">访问令牌</param>
  /// <returns>用户ID</returns>
  long GetUserIdFromToken(string token);
}
