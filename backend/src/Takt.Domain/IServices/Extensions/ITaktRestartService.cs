//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktRestartService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 系统重启服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================
namespace Takt.Domain.IServices.Extensions
{
  /// <summary>
  /// 系统重启服务接口
  /// </summary>
  public interface ITaktRestartService
  {
    /// <summary>
    /// 执行系统重启清理
    /// </summary>
    /// <returns>清理结果，true表示成功，false表示失败</returns>
    Task<bool> ExecuteRestartCleanupAsync();
  }
}
