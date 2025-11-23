//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktLogArchiveService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 日志归档服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================
using Takt.Shared.Options;

namespace Takt.Domain.IServices.Extensions
{
  /// <summary>
  /// 日志归档服务接口
  /// </summary>
  public interface ITaktLogArchiveService
  {
    /// <summary>
    /// 获取日志归档配置
    /// </summary>
    Task<TaktLogArchiveOptions> GetConfigAsync();

    /// <summary>
    /// 执行日志归档
    /// </summary>
    Task ArchiveAsync();

    /// <summary>
    /// 获取归档文件列表
    /// </summary>
    Task<List<string>> GetArchiveFilesAsync();

    /// <summary>
    /// 删除归档文件
    /// </summary>
    Task DeleteArchiveFileAsync(string fileName);
  }
}

