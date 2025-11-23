//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktLoginStatus.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录状态枚举
//===================================================================

namespace Takt.Shared.Enums
{
  /// <summary>
  /// 登录状态枚举
  /// </summary>
  public enum TaktLoginStatus
  {
    /// <summary>
    /// 成功
    /// </summary>
    Success = 0,

    /// <summary>
    /// 失败
    /// </summary>
    Failed = 1,

    /// <summary>
    /// 账号被锁定
    /// </summary>
    Locked = 2,

    /// <summary>
    /// 账号被禁用
    /// </summary>
    Disabled = 3,

    /// <summary>
    /// 验证码错误
    /// </summary>
    InvalidCode = 4,

    /// <summary>
    /// 密码错误
    /// </summary>
    InvalidPassword = 5,

    /// <summary>
    /// 用户不存在
    /// </summary>
    UserNotFound = 6,

    /// <summary>
    /// 离线
    /// </summary>
    Offline = 1
    }
} 




