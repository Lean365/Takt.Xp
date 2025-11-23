//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 常量
// 命名空间 : Takt.Shared.Constants
// 文件名 : TaktConstants.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 常量定义
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Shared.Constants
{
  /// <summary>
  /// 系统常量
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-17
  /// </remarks>
  public static class TaktConstants
  {
    /// <summary>
    /// 错误码
    /// </summary>
    public static class ErrorCodes
    {
      /// <summary>
      /// 成功
      /// </summary>
      public const string Success = "200";

      /// <summary>
      /// 未授权
      /// </summary>
      public const string Unauthorized = "401";

      /// <summary>
      /// 禁止访问
      /// </summary>
      public const string Forbidden = "403";

      /// <summary>
      /// 未找到
      /// </summary>
      public const string NotFound = "404";

      /// <summary>
      /// 服务器错误
      /// </summary>
      public const string ServerError = "500";

      /// <summary>
      /// 用户已停用
      /// </summary>
      public const string UserDisabled = "USER_001";

      /// <summary>
      /// 验证失败
      /// </summary>
      public const string ValidationFailed = "400";

      /// <summary>
      /// 验证码错误
      /// </summary>
      public const string InvalidCaptcha = "1001";

      /// <summary>
      /// 需要验证码
      /// </summary>
      public const string CaptchaRequired = "1005";

      /// <summary>
      /// 无效参数
      /// </summary>
      public const string InvalidParameter = "1002";

      /// <summary>
      /// 用户不存在
      /// </summary>
      public const string UserNotFound = "1003";

    }

    /// <summary>
    /// 日期时间格式
    /// </summary>
    public static class DateTimeFormats
    {
      /// <summary>
      /// 标准日期格式
      /// </summary>
      public const string StandardDate = "yyyy-MM-dd";

      /// <summary>
      /// 标准时间格式
      /// </summary>
      public const string StandardTime = "HH:mm:ss";

      /// <summary>
      /// 标准日期时间格式
      /// </summary>
      public const string StandardDateTime = "yyyy-MM-dd HH:mm:ss";

      /// <summary>
      /// 完整日期时间格式
      /// </summary>
      public const string FullDateTime = "yyyy-MM-dd HH:mm:ss.fff";
    }
  }
}




