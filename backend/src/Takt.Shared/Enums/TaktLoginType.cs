//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktLoginType.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录类型枚举
//===================================================================

namespace Takt.Shared.Enums
{
    /// <summary>
    /// 登录类型枚举
    /// </summary>
    public enum TaktLoginType
    {
        /// <summary>
        /// 其他
        /// </summary>
        Other = 0,

        /// <summary>
        /// 用户名密码
        /// </summary>
        Password = 1,

        // SMS和OAuth相关登录类型已移除

        /// <summary>
        /// 指纹登录
        /// </summary>
        Fingerprint = 15,

        /// <summary>
        /// 人脸识别登录
        /// </summary>
        FaceRecognition = 16,

        /// <summary>
        /// 声纹登录
        /// </summary>
        VoicePrint = 17,



        /// <summary>
        /// 单点登录(SSO)
        /// </summary>
        SSO = 19,

        // OAuth2.0登录已移除

        /// <summary>
        /// OpenID Connect登录
        /// </summary>
        OpenIDConnect = 21,

        /// <summary>
        /// SAML登录
        /// </summary>
        SAML = 22,

        /// <summary>
        /// LDAP登录
        /// </summary>
        LDAP = 23,

        /// <summary>
        /// 证书登录
        /// </summary>
        Certificate = 24,

        /// <summary>
        /// 硬件令牌登录
        /// </summary>
        HardwareToken = 25,

        /// <summary>
        /// 动态口令登录
        /// </summary>
        TOTP = 26,

        /// <summary>
        /// 手机号一键登录
        /// </summary>
        OneClick = 27,

        /// <summary>
        /// 免密登录
        /// </summary>
        Passwordless = 28,

        /// <summary>
        /// 游客登录
        /// </summary>
        Guest = 29,

        /// <summary>
        /// 匿名登录
        /// </summary>
        Anonymous = 30
    }
} 




