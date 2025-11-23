//===================================================================
// 项目名 : Takt.Xp
// 模块名 : SignalR
// 命名空间 : Takt.Domain.Models.SignalR
// 文件名 : TaktSignalRSessionInfo.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : SignalR会话信息
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================
namespace Takt.Domain.Models.SignalR
{
  /// <summary>
  /// SignalR会话信息
  /// </summary>
  public class TaktSignalRSessionInfo
  {
    /// <summary>
    /// 会话ID
    /// </summary>
    public string? SessionId { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// 过期时间（分钟）
    /// </summary>
    public int ExpiresIn { get; set; }
  }
}
