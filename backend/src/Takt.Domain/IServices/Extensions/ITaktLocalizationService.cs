//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktLocalizationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 本地化服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System.Globalization;

namespace Takt.Domain.IServices.Extensions;

/// <summary>
/// 本地化服务接口
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public interface ITaktLocalizationService
{
  /// <summary>
  /// 获取本地化文本
  /// </summary>
  /// <param name="key">本地化键</param>
  /// <param name="args">格式化参数</param>
  /// <returns>本地化文本</returns>
  Task<string> GetLocalizedStringAsync(string key, params object[] args);

  /// <summary>
  /// 获取当前语言文化
  /// </summary>
  /// <returns>当前语言文化</returns>
  CultureInfo GetCurrentCulture();

  /// <summary>
  /// 设置当前语言文化
  /// </summary>
  /// <param name="culture">语言文化</param>
  void SetCurrentCulture(CultureInfo culture);

  /// <summary>
  /// 设置当前语言
  /// </summary>
  /// <param name="langCode">语言代码</param>
  void SetLanguage(string langCode);

  /// <summary>
  /// 从数据库刷新本地化缓存
  /// </summary>
  Task RefreshLocalizationCacheAsync();

  /// <summary>
  /// 获取当前语言的翻译值
  /// </summary>
  string L(string key, params object[] args);

  /// <summary>
  /// 获取指定语言的翻译值
  /// </summary>
  string L(string langCode, string key, params object[] args);
}



