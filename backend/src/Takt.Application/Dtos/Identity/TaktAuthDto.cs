//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAuthDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录数据传输对象
//===================================================================

#nullable enable

using System.ComponentModel.DataAnnotations;
using Takt.Shared.Enums;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logging;

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 登录请求DTO
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-22
/// </remarks>
public class TaktAuthDto
{

    /// <summary>
    /// 用户名
    /// </summary>

    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>

    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 验证码Token
    /// </summary>
    public string? CaptchaToken { get; set; }

    /// <summary>
    /// 验证码偏移量
    /// </summary>
    public int CaptchaOffset { get; set; }

    /// <summary>
    /// 验证码类型（Slider/Behavior）
    /// </summary>
    public string? CaptchaType { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string LoginIp { get; set; } = string.Empty;

    /// <summary>
    /// 用户代理
    /// </summary>
    public string UserAgent { get; set; } = string.Empty;

    /// <summary>
    /// 登录类型
    /// </summary>
    public TaktLoginType LoginType { get; set; } = TaktLoginType.Password;

    /// <summary>
    /// 登录来源
    /// </summary>

    public int LoginSource { get; set; }

    /// <summary>
    /// 设备类型（PC、Android、iOS、MacOS、Linux、其他）
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 登录指纹信息
    /// </summary>
    public TaktLoginFingerprint? LoginFingerprint { get; set; }

}

/// <summary>
/// 登录结果传输对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-22
/// </remarks>
public class TaktLoginResultDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLoginResultDto()
    {
        AccessToken = string.Empty;
        RefreshToken = string.Empty;
        UserInfo = new TaktUserInfoDto();
    }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; } = true;

    /// <summary>
    /// 访问令牌
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 过期时间(秒)
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// 用户信息
    /// </summary>
    public TaktUserInfoDto UserInfo { get; set; }
}

/// <summary>
/// 登出DTO
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-12-01
/// </remarks>
public class TaktLogoutDto
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public string? DeviceType { get; set; }
    
    /// <summary>
    /// 设备ID
    /// </summary>
    public string? DeviceId { get; set; }
    
    /// <summary>
    /// 登录指纹信息（与登录时完全一致）
    /// </summary>
    public TaktLoginFingerprint? LoginFingerprint { get; set; }
}

/// <summary>
/// 用户信息DTO
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-22
/// </remarks>
public class TaktUserInfoDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktUserInfoDto()
    {
        UserName = string.Empty;
        NickName = string.Empty;
        FullName = string.Empty;
        RealName = string.Empty;
        EnglishName = string.Empty;
        Avatar = string.Empty;
        Roles = new List<string>();
        Permissions = new List<string>();
    }



    /// <summary>
    /// 用户ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 全名
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 英文名称
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 角色列表
    /// </summary>
    public List<string> Roles { get; set; }

    /// <summary>
    /// 权限列表
    /// </summary>
    public List<string> Permissions { get; set; }
}






