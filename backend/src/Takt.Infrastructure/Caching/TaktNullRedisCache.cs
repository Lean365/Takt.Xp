//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : NullRedisCache.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-24 16:30
// 版本号 : V0.0.1
// 描述    : Redis缓存空实现
//===================================================================

using Takt.Domain.IServices.Caching;

namespace Takt.Infrastructure.Caching
{
    /// <summary>
    /// Redis缓存空实现
    /// </summary>
    /// <remarks>
    /// 当Redis未配置时使用此实现，所有操作都返回空或默认值
    /// </remarks>
    public class TaktNullRedisCache : ITaktRedisCache
    {
        /// <summary>
        /// 设置缓存（空实现）
        /// </summary>
        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取缓存（空实现）
        /// </summary>
        public Task<T?> GetAsync<T>(string key)
        {
            return Task.FromResult<T?>(default);
        }

        /// <summary>
        /// 移除缓存（空实现）
        /// </summary>
        public Task RemoveAsync(string key)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 判断缓存是否存在（空实现）
        /// </summary>
        public Task<bool> ExistsAsync(string key)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// 根据模式搜索键（空实现）
        /// </summary>
        public Task<List<string>> SearchKeysAsync(string pattern)
        {
            return Task.FromResult(new List<string>());
        }
    }
} 



