//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPasswordPolicyOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:00
// 版本号 : V0.0.1
// 描述    : 密码策略配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// 密码策略配置选项
    /// </summary>
    public class TaktPasswordPolicyOptions
    {
        /// <summary>
        /// 最小长度
        /// </summary>
        public int MinLength { get; set; } = 8;

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; } = 20;

        /// <summary>
        /// 是否需要数字
        /// </summary>
        public bool RequireDigit { get; set; } = true;

        /// <summary>
        /// 是否需要小写字母
        /// </summary>
        public bool RequireLowercase { get; set; } = true;

        /// <summary>
        /// 是否需要大写字母
        /// </summary>
        public bool RequireUppercase { get; set; } = true;

        /// <summary>
        /// 是否需要特殊字符
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; } = true;

        /// <summary>
        /// 密码过期天数
        /// </summary>
        public int ExpirationDays { get; set; } = 90;

        /// <summary>
        /// 密码历史记录数
        /// </summary>
        public int HistoryCount { get; set; } = 3;

        /// <summary>
        /// 是否启用密码历史记录
        /// </summary>
        public bool EnablePasswordHistory { get; set; } = true;

        /// <summary>
        /// 是否启用密码过期
        /// </summary>
        public bool EnablePasswordExpiration { get; set; } = true;

        /// <summary>
        /// 是否需要特殊字符
        /// </summary>
        public bool RequireSpecialChar { get; set; } = true;

        /// <summary>
        /// 密码历史记录数
        /// </summary>
        public int PasswordHistoryCount { get; set; } = 5;

        /// <summary>
        /// 密码过期天数
        /// </summary>
        public int PasswordExpirationDays { get; set; } = 90;

        /// <summary>
        /// 默认密码
        /// </summary>
        public required string DefaultPassword { get; set; }
    }
} 




