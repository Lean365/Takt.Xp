using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.Caching;
using Takt.Domain.IServices.SignalR;

namespace Takt.Infrastructure.SignalR.Cache
{
    /// <summary>
    /// SignalR Redis缓存实现
    /// </summary>
    /// <remarks>
    /// 使用Redis实现SignalR的缓存服务
    /// 适用于分布式部署场景，支持多实例之间的数据同步
    /// </remarks>
    public class TaktSignalRRedisCache : ITaktSignalRCacheService
    {
        /// <summary>
        /// Redis缓存服务实例
        /// </summary>
        private readonly ITaktRedisCache _cache;

        /// <summary>
        /// 默认缓存过期时间（分钟）
        /// </summary>
        private const int DEFAULT_CACHE_MINUTES = 30;

        /// <summary>
        /// 用户连接列表的键前缀
        /// </summary>
        private const string USER_CONNECTIONS_PREFIX = "signalr:connections:";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache">Redis缓存服务实例</param>
        public TaktSignalRRedisCache(ITaktRedisCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 将用户信息保存到Redis缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="user">用户信息</param>
        /// <param name="expiry">过期时间，如果不指定则使用默认值</param>
        public async Task SetAsync(string key, TaktSignalROnline user, TimeSpan? expiry = null)
        {
            await _cache.SetAsync(key, user, expiry ?? TimeSpan.FromMinutes(DEFAULT_CACHE_MINUTES));
        }

        /// <summary>
        /// 从Redis缓存获取用户信息
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>用户信息，如果不存在则返回null</returns>
        public async Task<TaktSignalROnline?> GetAsync(string key)
        {
            return await _cache.GetAsync<TaktSignalROnline>(key);
        }

        /// <summary>
        /// 从Redis缓存中删除用户信息
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// 获取所有匹配模式的键
        /// </summary>
        /// <param name="pattern">匹配模式</param>
        /// <remarks>
        /// 使用Redis的KEYS命令实现模式匹配
        /// 注意：在生产环境中应谨慎使用，因为KEYS命令可能会影响Redis性能
        /// </remarks>
        /// <returns>匹配的键列表</returns>
        public async Task<List<string>> GetKeysByPatternAsync(string pattern)
        {
            return await _cache.SearchKeysAsync(pattern);
        }

        /// <summary>
        /// 设置用户的连接ID列表到Redis
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connections">连接ID列表</param>
        public async Task SetConnectionsAsync(long userId, List<string> connections)
        {
            var key = $"{USER_CONNECTIONS_PREFIX}{userId}";
            await _cache.SetAsync(key, connections);
        }

        /// <summary>
        /// 从Redis获取用户的连接ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>连接ID列表，如果用户不存在则返回空列表</returns>
        public async Task<List<string>> GetConnectionsAsync(long userId)
        {
            var key = $"{USER_CONNECTIONS_PREFIX}{userId}";
            return await _cache.GetAsync<List<string>>(key) ?? new List<string>();
        }

        /// <summary>
        /// 从Redis删除用户的连接ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        public async Task RemoveConnectionsAsync(long userId)
        {
            var key = $"{USER_CONNECTIONS_PREFIX}{userId}";
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        public async Task<List<TaktSignalROnline>> GetOnlineUsersAsync()
        {
            var keys = await _cache.SearchKeysAsync("signalr:user:*");
            var users = new List<TaktSignalROnline>();
            foreach (var key in keys)
            {
                if (await _cache.GetAsync<TaktSignalROnline>(key) is TaktSignalROnline user)
                {
                    users.Add(user);
                }
            }
            return users;
        }

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        public async Task UpdateLastActivityAsync(string connectionId, DateTime lastActivity)
        {
            var key = $"signalr:user:{connectionId}";
            if (await _cache.GetAsync<TaktSignalROnline>(key) is TaktSignalROnline user)
            {
                user.LastActivity = lastActivity;
                await _cache.SetAsync(key, user);
            }
        }

        /// <summary>
        /// 更新用户心跳时间
        /// </summary>
        public async Task UpdateHeartbeatAsync(string connectionId, DateTime heartbeat)
        {
            var key = $"signalr:user:{connectionId}";
            if (await _cache.GetAsync<TaktSignalROnline>(key) is TaktSignalROnline user)
            {
                user.LastHeartbeat = heartbeat;
                await _cache.SetAsync(key, user);
            }
        }

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        public async Task SetOnlineStatusAsync(string connectionId, int status)
        {
            var key = $"signalr:user:{connectionId}";
            if (await _cache.GetAsync<TaktSignalROnline>(key) is TaktSignalROnline user)
            {
                user.OnlineStatus = status;
                await _cache.SetAsync(key, user);
            }
        }
    }
}
