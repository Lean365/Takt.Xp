//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktRedisCache.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : Redis缓存实现
//===================================================================

using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using Takt.Domain.IServices.Caching;

namespace Takt.Infrastructure.Caching
{
    /// <summary>
    /// Redis缓存实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktRedisCache : ITaktRedisCache
    {
        /// <summary>
        /// 分布式缓存接口
        /// </summary>
        private readonly IDistributedCache _cache;

        /// <summary>
        /// 连接到Redis服务器
        /// </summary>
        private readonly ConnectionMultiplexer _connection;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache">分布式缓存接口</param>
        /// <param name="connection">连接到Redis服务器</param>
        public TaktRedisCache(IDistributedCache cache, ConnectionMultiplexer connection)
        {
            _cache = cache;
            _connection = connection;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns>异步任务</returns>
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var options = new DistributedCacheEntryOptions();
            if (expiry.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiry;
            }

            var jsonData = JsonConvert.SerializeObject(value);
            await _cache.SetStringAsync(key, jsonData, options);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值，不存在返回默认值</returns>
        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(jsonData))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>异步任务</returns>
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>是否存在</returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await GetAsync<string>(key) != null;
        }

        /// <summary>
        /// 根据模式搜索键
        /// </summary>
        /// <param name="pattern">搜索模式</param>
        /// <returns>匹配的键列表</returns>
        public Task<List<string>> SearchKeysAsync(string pattern)
        {
            var server = _connection.GetServer(_connection.GetEndPoints().First());
            var keys = server.Keys(pattern: pattern);
            return Task.FromResult(keys.Select(k => k.ToString()).ToList());
        }
    }
} 



