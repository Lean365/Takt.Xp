//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIdentitySeedTranslation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : Identity本地化资源数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// Identity本地化资源数据提供类
/// </summary>
public class TaktIdentitySeedTranslation
{
    /// <summary>
    /// 获取Identity翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<TaktTranslation> GetIdentityTranslations()
    {
        return new List<TaktTranslation>
        {
            // 用户相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.NotFound", TransValue = "用户不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.NotFound", TransValue = "User not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.NotFound", TransValue = "ユーザーが見つかりません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.Disabled", TransValue = "用户已被禁用", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.Disabled", TransValue = "User has been disabled", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.Disabled", TransValue = "ユーザーは無効になっています", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.InvalidCredentials", TransValue = "用户名或密码错误", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.InvalidCredentials", TransValue = "Invalid username or password", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.InvalidCredentials", TransValue = "ユーザー名またはパスワードが間違っています", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.InvalidCaptcha", TransValue = "验证码错误", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.InvalidCaptcha", TransValue = "Invalid captcha", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.InvalidCaptcha", TransValue = "キャプチャが間違っています", TransType = 1,  },


            // 认证相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginStart", TransValue = "开始处理登录请求: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginStart", TransValue = "Processing login request: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginStart", TransValue = "ログインリクエストの処理を開始: {0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ExistingUserLogin", TransValue = "检测到已在线用户登录请求: UserId={0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ExistingUserLogin", TransValue = "Detected existing user login request: UserId={0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ExistingUserLogin", TransValue = "既存ユーザーのログインリクエストを検出: UserId={0}", TransType = 1,  },


            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.UserValidationStart", TransValue = "开始验证用户登录请求: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.UserValidationStart", TransValue = "Starting user validation: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.UserValidationStart", TransValue = "ユーザー検証を開始: {0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.PasswordValidationStart", TransValue = "开始验证密码: UserId={0}, PasswordLength={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.PasswordValidationStart", TransValue = "Starting password validation: UserId={0}, PasswordLength={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.PasswordValidationStart", TransValue = "パスワード検証を開始: UserId={0}, PasswordLength={1}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.PasswordValidationResult", TransValue = "密码验证结果: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.PasswordValidationResult", TransValue = "Password validation result: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.PasswordValidationResult", TransValue = "パスワード検証結果: {0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.CaptchaValidationStart", TransValue = "开始验证验证码: Token={0}, Offset={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.CaptchaValidationStart", TransValue = "Starting captcha validation: Token={0}, Offset={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.CaptchaValidationStart", TransValue = "キャプチャ検証を開始: Token={0}, Offset={1}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.CaptchaValidationResult", TransValue = "验证码验证结果: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.CaptchaValidationResult", TransValue = "Captcha validation result: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.CaptchaValidationResult", TransValue = "キャプチャ検証結果: {0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.GetUserRolesAndPermissions", TransValue = "开始获取用户角色和权限: UserId={0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.GetUserRolesAndPermissions", TransValue = "Getting user roles and permissions: UserId={0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.GetUserRolesAndPermissions", TransValue = "ユーザーロールと権限を取得: UserId={0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.UserRolesAndPermissionsResult", TransValue = "用户角色和权限获取完成: RolesCount={0}, PermissionsCount={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.UserRolesAndPermissionsResult", TransValue = "User roles and permissions retrieved: RolesCount={0}, PermissionsCount={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.UserRolesAndPermissionsResult", TransValue = "ユーザーロールと権限を取得完了: RolesCount={0}, PermissionsCount={1}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.GenerateTokens", TransValue = "开始生成访问令牌", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.GenerateTokens", TransValue = "Generating access tokens", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.GenerateTokens", TransValue = "アクセストークンを生成中", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.TokensGenerated", TransValue = "令牌生成完成: AccessTokenLength={0}, RefreshToken={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.TokensGenerated", TransValue = "Tokens generated: AccessTokenLength={0}, RefreshToken={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.TokensGenerated", TransValue = "トークン生成完了: AccessTokenLength={0}, RefreshToken={1}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.CacheRefreshToken", TransValue = "开始缓存刷新令牌: Key=refresh_token:{0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.CacheRefreshToken", TransValue = "Caching refresh token: Key=refresh_token:{0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.CacheRefreshToken", TransValue = "リフレッシュトークンをキャッシュ: Key=refresh_token:{0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.RefreshTokenCached", TransValue = "刷新令牌缓存完成", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.RefreshTokenCached", TransValue = "Refresh token cached", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.RefreshTokenCached", TransValue = "リフレッシュトークンのキャッシュ完了", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ProcessDeviceInfo", TransValue = "准备处理登录设备日志", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ProcessDeviceInfo", TransValue = "Processing device extension information", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ProcessDeviceInfo", TransValue = "デバイス拡張情報を処理中", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.DeviceInfoProcessed", TransValue = "登录设备日志处理完成", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.DeviceInfoProcessed", TransValue = "Device extension information processed", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.DeviceInfoProcessed", TransValue = "デバイス拡張情報の処理完了", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ProcessLoginInfo", TransValue = "准备处理登录环境日志信息", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ProcessLoginInfo", TransValue = "Processing login extension information", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ProcessLoginInfo", TransValue = "ログイン拡張情報を処理中", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginInfoProcessed", TransValue = "登录环境日志信息处理完成", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginInfoProcessed", TransValue = "Login extension information processed", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginInfoProcessed", TransValue = "ログイン拡張情報の処理完了", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ProcessLoginLog", TransValue = "准备处理登录日志", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ProcessLoginLog", TransValue = "Processing login log", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ProcessLoginLog", TransValue = "ログインログを処理中", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginLogProcessed", TransValue = "登录日志处理完成", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginLogProcessed", TransValue = "Login log processed", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginLogProcessed", TransValue = "ログインログの処理完了", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginSuccess", TransValue = "登录成功: UserId={0}, UserName={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginSuccess", TransValue = "Login successful: UserId={0}, UserName={1}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginSuccess", TransValue = "ログイン成功: UserId={0}, UserName={1}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.LoginError", TransValue = "登录过程中发生错误: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.LoginError", TransValue = "Error during login: {0}", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.LoginError", TransValue = "ログイン中にエラーが発生: {0}", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.ServerError", TransValue = "服务器内部错误", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.ServerError", TransValue = "Internal server error", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.ServerError", TransValue = "サーバー内部エラー", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Auth.InvalidRefreshToken", TransValue = "刷新令牌无效或已过期", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Auth.InvalidRefreshToken", TransValue = "Invalid or expired refresh token", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Auth.InvalidRefreshToken", TransValue = "無効または期限切れのリフレッシュトークン", TransType = 1,  },

            // 设备相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.Unknown", TransValue = "未知设备", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.Unknown", TransValue = "Unknown device", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.Unknown", TransValue = "不明なデバイス", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownModel", TransValue = "未知型号", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownModel", TransValue = "Unknown model", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownModel", TransValue = "不明なモデル", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownVersion", TransValue = "未知版本", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownVersion", TransValue = "Unknown version", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownVersion", TransValue = "不明なバージョン", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownResolution", TransValue = "未知分辨率", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownResolution", TransValue = "Unknown resolution", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownResolution", TransValue = "不明な解像度", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownIP", TransValue = "未知", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownIP", TransValue = "Unknown", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownIP", TransValue = "不明", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.UnknownLocation", TransValue = "未知", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.UnknownLocation", TransValue = "Unknown", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.UnknownLocation", TransValue = "不明", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.Offline", TransValue = "离线设备", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.Offline", TransValue = "Offline device", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.Offline", TransValue = "オフラインデバイス", TransType = 1,  },

            // 用户相关翻译 - 根据实际代码中使用的翻译键
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.NotFound", TransValue = "用户不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.Username.Required", TransValue = "用户名不能为空", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.Password.Invalid", TransValue = "密码格式不正确", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.Password.Incorrect", TransValue = "密码不正确", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.User.SelectRequired", TransValue = "请选择要操作的用户", TransType = 1,  },

            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.NotFound", TransValue = "User not found", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.Username.Required", TransValue = "Username is required", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.Password.Invalid", TransValue = "Invalid password format", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.Password.Incorrect", TransValue = "Incorrect password", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.User.SelectRequired", TransValue = "Please select users to operate", TransType = 1,  },

            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.NotFound", TransValue = "ユーザーが見つかりません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.Username.Required", TransValue = "ユーザー名は必須です", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.Password.Invalid", TransValue = "パスワード形式が正しくありません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.Password.Incorrect", TransValue = "パスワードが正しくありません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.User.SelectRequired", TransValue = "操作するユーザーを選択してください", TransType = 1,  },

            // 角色相关翻译 - 根据实际代码中使用的翻译键
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.CreateFailed", TransValue = "创建角色失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.HasUsers", TransValue = "角色下存在用户，无法删除", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.SelectRequired", TransValue = "请选择要操作的角色", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.ImportEmpty", TransValue = "导入数据不能为空", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.ImportFailed", TransValue = "导入失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.ExportFailed", TransValue = "导出失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.Log.ImportDataFailed", TransValue = "导入数据失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Role.Log.ExportDataFailed", TransValue = "导出数据失败", TransType = 1,  },

            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.CreateFailed", TransValue = "Failed to create role", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.HasUsers", TransValue = "Role has users, cannot delete", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.SelectRequired", TransValue = "Please select roles to operate", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.ImportEmpty", TransValue = "Import data cannot be empty", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.ImportFailed", TransValue = "Import failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.ExportFailed", TransValue = "Export failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.Log.ImportDataFailed", TransValue = "Failed to import data", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Role.Log.ExportDataFailed", TransValue = "Failed to export data", TransType = 1,  },

            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.CreateFailed", TransValue = "ロール作成に失敗しました", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.HasUsers", TransValue = "ロールにユーザーが存在するため削除できません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.SelectRequired", TransValue = "操作するロールを選択してください", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.ImportEmpty", TransValue = "インポートデータは空にできません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.ImportFailed", TransValue = "インポートに失敗しました", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.ExportFailed", TransValue = "エクスポートに失敗しました", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.Log.ImportDataFailed", TransValue = "データのインポートに失敗しました", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Role.Log.ExportDataFailed", TransValue = "データのエクスポートに失敗しました", TransType = 1,  },

            // 设备相关翻译 - 根据实际代码中使用的翻译键
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Identity.Device.DefaultIP", TransValue = "未知IP", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Identity.Device.DefaultIP", TransValue = "Unknown IP", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Identity.Device.DefaultIP", TransValue = "不明なIP", TransType = 1,  }
        };
    }
}




