//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktCurrentUser.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 当前用户接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.IServices.Extensions
{
  /// <summary>
  /// 当前用户接口
  /// </summary>
  /// <remarks>
  /// 提供当前用户信息的访问，包括：
  /// 1. 用户基本信息（ID、名称、类型等）
  /// 2. 角色和权限
  /// 3. 认证状态
  /// </remarks>
  public interface ITaktCurrentUser
  {
    /// <summary>
    /// 用户ID
    /// </summary>
    long UserId { get; }

    /// <summary>
    /// 用户名
    /// </summary>
    string UserName { get; }

    /// <summary>
    /// 昵称
    /// </summary>
    string NickName { get; }

    /// <summary>
    /// 英文名称
    /// </summary>
    string EnglishName { get; }

    /// <summary>
    /// 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户）
    /// </summary>
    int UserType { get; }



    /// <summary>
    /// 用户角色列表
    /// </summary>
    string[] Roles { get; }

    /// <summary>
    /// 用户权限列表
    /// </summary>
    string[] Permissions { get; }

    /// <summary>
    /// 是否已认证
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// 是否为超级管理员
    /// </summary>
    bool IsAdmin { get; }
  }
}



