//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktJwtOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 17:30
// 版本号 : V0.0.1
// 描述    : JWT配置选项
//===================================================================

namespace Takt.Shared.Options
{
    /// <summary>
    /// JWT配置选项
    /// </summary>
    public class TaktJwtOptions
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public required string SecretKey { get; set; }

        /// <summary>
        /// 发行者
        /// </summary>
        public required string Issuer { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public required string Audience { get; set; }

        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int ExpirationMinutes { get; set; } = 30;

        /// <summary>
        /// 刷新令牌过期时间(天)
        /// </summary>
        public int RefreshTokenExpirationDays { get; set; } = 7;
    }
} 




