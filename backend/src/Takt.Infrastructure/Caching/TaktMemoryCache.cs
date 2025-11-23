//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMemoryCache.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 16:30
// 版本号 : V0.0.1
// 描述    : 内存缓存实现
//===================================================================

using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Takt.Domain.IServices.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Takt.Infrastructure.Caching
{
    /// <summary>
    /// 内存缓存实现
    /// </summary>
    public class TaktMemoryCache : ITaktMemoryCache
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, DateTime?> _keys;
        private readonly MemoryCacheEntryOptions _defaultOptions;
        private readonly TaktCacheConfigManager _configManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMemoryCache(
            IMemoryCache cache,
            TaktCacheConfigManager configManager)
        {
            _cache = cache;
            _configManager = configManager;
            _keys = new ConcurrentDictionary<string, DateTime?>();
            _defaultOptions = new MemoryCacheEntryOptions();
            InitializeDefaultOptions();
        }

        private async void InitializeDefaultOptions()
        {
            try
            {
                var options = await _configManager.GetCurrentOptionsAsync();
                if (options != null)
                {
                    _defaultOptions.SlidingExpiration = options.EnableSlidingExpiration
                        ? TimeSpan.FromMinutes(options.DefaultExpirationMinutes)
                        : null;
                    _defaultOptions.Size = options.Memory?.SizeLimit;
                }
            }
            catch (Exception)
            {
                // 如果配置获取失败，使用默认值
                _defaultOptions.SlidingExpiration = TimeSpan.FromMinutes(30); // 默认30分钟
                _defaultOptions.Size = 1000; // 默认大小限制
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        public bool Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var options = new MemoryCacheEntryOptions();
                if (expiry.HasValue)
                {
                    options.AbsoluteExpirationRelativeToNow = expiry;
                }
                else
                {
                    options = _defaultOptions;
                }

                options.RegisterPostEvictionCallback(OnPostEviction);
                _cache.Set(key, value, options);
                _keys.TryAdd(key, expiry.HasValue ? DateTime.Now.Add(expiry.Value) : null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        public T? Get<T>(string key)
        {
            return _cache.TryGetValue(key, out object? value) ? (T?)value : default;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        public bool Remove(string key)
        {
            try
            {
                _cache.Remove(key);
                _keys.TryRemove(key, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        /// <summary>
        /// 根据模式搜索键
        /// </summary>
        public List<string> SearchKeys(string pattern)
        {
            var regex = new Regex(pattern.Replace("*", ".*"));
            return _keys.Keys.Where(k => regex.IsMatch(k)).ToList();
        }

        /// <summary>
        /// 获取或添加缓存
        /// </summary>
        public T GetOrAdd<T>(string key, Func<T> factory, TimeSpan? expiry = null)
        {
            if (_cache.TryGetValue(key, out object? value) && value != null)
            {
                return (T)value;
            }

            var result = factory();
            Set(key, result, expiry);
            return result;
        }

        /// <summary>
        /// 缓存过期回调
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        /// <param name="state"></param>
        private void OnPostEviction(object key, object? value, EvictionReason reason, object? state)
        {
            if (key is string strKey)
            {
                _keys.TryRemove(strKey, out _);
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T?> GetAsync<T>(string key)
        {
            return await Task.FromResult(Get<T>(key));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var options = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = expiration
                    };

                    options.RegisterPostEvictionCallback(OnPostEviction);
                    _cache.Set(key, value, options);
                    _keys.TryAdd(key, DateTime.Now.Add(expiration));
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        public async Task<bool> RemoveAsync(string key)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _cache.Remove(key);
                    _keys.TryRemove(key, out _);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ClearAsync()
        {
            try
            {
                if (_cache is MemoryCache memoryCache)
                {
                    memoryCache.Compact(1.0);
                }
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// 判断缓存是否存在（异步）
        /// </summary>
        public async Task<bool> ExistsAsync(string key)
        {
            return await Task.FromResult(Exists(key));
        }

        /// <summary>
        /// 根据模式搜索键（异步）
        /// </summary>
        public async Task<List<string>> SearchKeysAsync(string pattern)
        {
            return await Task.FromResult(SearchKeys(pattern));
        }
    }
}



