//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktLoginService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录服务接口
//===================================================================

using System.Threading.Tasks;
using Takt.Application.Dtos.Identity;
using Takt.Domain.Entities.Identity;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 登录服务接口
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-22
/// </remarks>
public interface ITaktAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    Task<TaktLoginResultDto> LoginAsync(TaktAuthDto loginDto);

    /// <summary>
    /// 用户登出
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> LogoutAsync(long userId);

    /// <summary>
    /// 用户登出（带设备信息）
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="logoutDto">登出信息</param>
    /// <returns>是否成功</returns>
    Task<bool> LogoutAsync(long userId, TaktLogoutDto logoutDto);

    /// <summary>
    /// 刷新访问令牌
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>新的访问令牌</returns>
    Task<TaktLoginResultDto> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// 获取用户盐值
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>用户盐值信息</returns>
    Task<(string Salt, int Iterations)?> GetUserSaltAsync(string username);

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户信息</returns>
    Task<TaktUserInfoDto> GetUserInfoAsync(long userId);

} 



