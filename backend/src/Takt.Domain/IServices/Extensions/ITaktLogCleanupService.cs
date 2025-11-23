#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktLogCleanupService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 日志清理服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using Takt.Shared.Options;

namespace Takt.Domain.IServices.Extensions
{
  /// <summary>
  /// 日志清理服务接口
  /// </summary>
  public interface ITaktLogCleanupService
  {
    /// <summary>
    /// 获取日志清理配置
    /// </summary>
    Task<TaktLogCleanupOptions> GetConfigAsync();

    /// <summary>
    /// 执行日志清理
    /// </summary>
    Task CleanupAsync();
  }
}




