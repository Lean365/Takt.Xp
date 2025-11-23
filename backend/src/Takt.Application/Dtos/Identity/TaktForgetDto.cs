//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPasswordRecoveryDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 找回密码相关DTO
//===================================================================

namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 身份验证请求DTO
    /// </summary>
    public class TaktIdentityVerificationRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 验证码
        /// </summary>
        public int Captcha { get; set; }

        /// <summary>
        /// 验证码Token
        /// </summary>
        public string CaptchaToken { get; set; } = string.Empty;
    }

    /// <summary>
    /// 身份验证响应DTO
    /// </summary>
    public class TaktIdentityVerificationResponse
    {
        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 验证Token
        /// </summary>
        public string VerificationToken { get; set; } = string.Empty;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 发送验证码请求DTO
    /// </summary>
    public class TaktSendVerificationCodeRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// 发送验证码响应DTO
    /// </summary>
    public class TaktSendVerificationCodeResponse
    {
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 验证码有效期（分钟）
        /// </summary>
        public int ExpirationMinutes { get; set; } = 10;
    }

    /// <summary>
    /// 邮箱验证请求DTO
    /// </summary>
    public class TaktEmailVerificationRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; } = string.Empty;
    }

    /// <summary>
    /// 邮箱验证响应DTO
    /// </summary>
    public class TaktEmailVerificationResponse
    {
        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 重置Token
        /// </summary>
        public string ResetToken { get; set; } = string.Empty;

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// 密码重置请求DTO
    /// </summary>
    public class TaktPasswordResetRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 重置Token
        /// </summary>
        public string ResetToken { get; set; } = string.Empty;

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;
    }

    /// <summary>
    /// 密码重置响应DTO
    /// </summary>
    public class TaktPasswordResetResponse
    {
        /// <summary>
        /// 是否重置成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}



