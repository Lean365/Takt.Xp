//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 缓存
// 命名空间 : Takt.Domain.IServices.Caching
// 文件名 : ITaktMemoryCache.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 内存缓存服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Takt.Domain.IServices.Caching
{
  /// <summary>
  /// 内存缓存服务接口
  /// </summary>
  public interface ITaktMemoryCache
  {
    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    Task<T?> GetAsync<T>(string key);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expiration">过期时间</param>
    /// <returns>是否设置成功</returns>
    Task<bool> SetAsync<T>(string key, T value, TimeSpan expiration);

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <returns>是否移除成功</returns>
    Task<bool> RemoveAsync(string key);

    /// <summary>
    /// 清空缓存
    /// </summary>
    /// <returns>是否清空成功</returns>
    Task<bool> ClearAsync();

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存数据类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值，不存在返回默认值</returns>
    T? Get<T>(string key);

    /// <summary>
    /// 判断缓存是否存在
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <returns>是否存在</returns>
    bool Exists(string key);

    /// <summary>
    /// 根据模式搜索键
    /// </summary>
    /// <param name="pattern">搜索模式</param>
    /// <returns>匹配的键列表</returns>
    List<string> SearchKeys(string pattern);

    /// <summary>
    /// 获取或添加缓存
    /// </summary>
    /// <typeparam name="T">缓存数据类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="factory">数据获取工厂</param>
    /// <param name="expiry">过期时间</param>
    /// <returns>缓存值</returns>
    T GetOrAdd<T>(string key, Func<T> factory, TimeSpan? expiry = null);
  }
}



