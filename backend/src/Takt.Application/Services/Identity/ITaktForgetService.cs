//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktPasswordRecoveryService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 找回密码服务接口
//===================================================================

using Takt.Application.Dtos.Identity;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 找回密码服务接口
    /// </summary>
    public interface ITaktForgetService
    {
        /// <summary>
        /// 验证用户身份
        /// </summary>
        /// <param name="request">身份验证请求</param>
        /// <returns>身份验证响应</returns>
        Task<TaktIdentityVerificationResponse> VerifyIdentityAsync(TaktIdentityVerificationRequest request);

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="request">发送验证码请求</param>
        /// <returns>发送验证码响应</returns>
        Task<TaktSendVerificationCodeResponse> SendVerificationCodeAsync(TaktSendVerificationCodeRequest request);

        /// <summary>
        /// 验证邮箱验证码
        /// </summary>
        /// <param name="request">邮箱验证请求</param>
        /// <returns>邮箱验证响应</returns>
        Task<TaktEmailVerificationResponse> VerifyEmailCodeAsync(TaktEmailVerificationRequest request);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request">密码重置请求</param>
        /// <returns>密码重置响应</returns>
        Task<TaktPasswordResetResponse> ResetPasswordAsync(TaktPasswordResetRequest request);
    }
} 



