//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRegistrationOptions.cs
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : V1.0.0
// 描述    : 用户注册配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 用户注册配置选项
    /// </summary>
    public class TaktRegistrationOptions
    {
        /// <summary>
        /// 是否启用注册功能
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 注册是否需要邮箱验证
        /// </summary>
        public bool RequireEmailVerification { get; set; }

        /// <summary>
        /// 注册是否需要手机验证
        /// </summary>
        public bool RequirePhoneVerification { get; set; }

        /// <summary>
        /// 是否允许匿名注册
        /// </summary>
        public bool AllowAnonymousRegistration { get; set; }

        /// <summary>
        /// 是否需要管理员审批
        /// </summary>
        public bool RequireAdminApproval { get; set; }

        /// <summary>
        /// 注册后默认角色
        /// </summary>
        public string DefaultRole { get; set; }

        /// <summary>
        /// 注册后默认状态（0:待激活, 1:正常, 2:锁定）
        /// </summary>
        public int DefaultStatus { get; set; }

        /// <summary>
        /// 注册验证码有效期（分钟）
        /// </summary>
        public int VerificationCodeExpirationMinutes { get; set; }

        /// <summary>
        /// 注册频率限制（每小时最大注册次数）
        /// </summary>
        public int RegistrationRateLimit { get; set; }

        /// <summary>
        /// IP注册频率限制（每小时最大注册次数）
        /// </summary>
        public int IpRegistrationRateLimit { get; set; }
    }
} 


