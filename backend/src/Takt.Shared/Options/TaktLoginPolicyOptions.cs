//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginPolicyOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:00
// 版本号 : V0.0.1
// 描述    : 登录策略配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 登录策略配置选项
    /// </summary>
    public class TaktLoginPolicyOptions
    {
        /// <summary>
        /// 最大失败次数
        /// </summary>
        public int MaxFailedAttempts { get; set; } = 5;

        /// <summary>
        /// 锁定时间(分钟)
        /// </summary>
        public int LockoutMinutes { get; set; } = 30;

        /// <summary>
        /// 是否启用登录限制
        /// </summary>
        public bool EnableLoginRestriction { get; set; } = true;

        /// <summary>
        /// 需要验证码的失败次数阈值
        /// </summary>
        public int CaptchaRequiredAttempts { get; set; } = 3;

        /// <summary>
        /// 验证码有效期(分钟)
        /// </summary>
        public int CaptchaRequiredMinutes { get; set; } = 5;

        /// <summary>
        /// 重复登录检测时间窗口(分钟)
        /// </summary>
        public int RepeatLoginMinutes { get; set; } = 5;
    }
} 




