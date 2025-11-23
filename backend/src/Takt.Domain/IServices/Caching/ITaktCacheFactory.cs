//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 缓存
// 命名空间 : Takt.Domain.IServices.Caching
// 文件名 : ITaktCacheFactory.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 缓存工厂接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.IServices.Caching
{
  /// <summary>
  /// 缓存工厂接口
  /// </summary>
  public interface ITaktCacheFactory
  {
    /// <summary>
    /// 获取内存缓存
    /// </summary>
    ITaktMemoryCache Memory { get; }

    /// <summary>
    /// 获取Redis缓存
    /// </summary>
    ITaktRedisCache Redis { get; }
  }
}



