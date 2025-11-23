//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSecurityOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:00
// 版本号 : V0.0.1
// 描述    : 安全配置选项
//===================================================================

namespace Takt.Shared.Options;

/// <summary>
/// 安全配置选项
/// </summary>
public class TaktSecurityOptions
{
    /// <summary>
    /// 密码策略选项
    /// </summary>
    public TaktPasswordPolicyOptions? PasswordPolicy { get; set; }

    /// <summary>
    /// 登录策略选项
    /// </summary>
    public TaktLoginPolicyOptions? LoginPolicy { get; set; }

    /// <summary>
    /// 会话选项
    /// </summary>
    public TaktSessionOptions? Session { get; set; }

    /// <summary>
    /// CSRF Token过期时间(分钟)
    /// </summary>
    public int CsrfTokenExpirationMinutes { get; set; } = 30;

    /// <summary>
    /// 会话过期时间(分钟)
    /// </summary>
    public int SessionExpirationMinutes { get; set; } = 30;
}




