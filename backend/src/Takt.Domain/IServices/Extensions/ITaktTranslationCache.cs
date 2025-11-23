//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktTranslationCache.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 翻译缓存服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;

namespace Takt.Domain.IServices.Extensions
{
  /// <summary>
  /// 翻译缓存服务接口
  /// </summary>
  public interface ITaktTranslationCache
  {
    /// <summary>
    /// 获取指定语言和键的翻译
    /// </summary>
    /// <param name="langCode">语言代码</param>
    /// <param name="key">翻译键</param>
    /// <returns>翻译值，如果不存在则返回null</returns>
    string? GetTranslation(string langCode, string key);

    /// <summary>
    /// 重新加载翻译数据
    /// </summary>
    Task ReloadAsync();
  }
}



