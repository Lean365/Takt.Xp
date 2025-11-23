using Takt.Shared.Options;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Takt.Infrastructure.SignalR.Cache
{
    /// <summary>
    /// SignalR内存缓存实现
    /// </summary>
    /// <remarks>
    /// 使用.NET内置的IMemoryCache实现SignalR的缓存服务
    /// 适用于单机部署或不需要分布式缓存的场景
    /// 包含内存溢出保护机制
    /// </remarks>
    public class TaktSignalRMemoryCache : ITaktSignalRCacheService, IDisposable
    {
        /// <summary>
        /// 内存缓存实例
        /// </summary>
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 用户连接映射字典
        /// key: 用户ID
        /// value: 该用户的所有连接ID列表
        /// 使用ConcurrentDictionary确保线程安全
        /// </summary>
        private readonly ConcurrentDictionary<long, List<string>> _userConnections;

        /// <summary>
        /// 缓存清理定时器
        /// </summary>
        private readonly Timer _cleanupTimer;

        /// <summary>
        /// 缓存大小计数器
        /// </summary>
        private long _cacheSize;

        /// <summary>
        /// 缓存配置选项
        /// </summary>
        private readonly TaktSignalRCacheOptions _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache">内存缓存实例</param>
        /// <param name="options">缓存配置选项</param>
        public TaktSignalRMemoryCache(
            IMemoryCache cache,
            IOptions<TaktSignalRCacheOptions> options)
        {
            _cache = cache;
            _options = options.Value;
            _userConnections = new ConcurrentDictionary<long, List<string>>();
            _cacheSize = 0;

            // 如果启用了自动清理，创建定时清理任务
            if (_options.EnableAutoCleanup)
            {
                _cleanupTimer = new Timer(
                    CleanupCache,
                    null,
                    TimeSpan.FromMinutes(_options.CleanupIntervalMinutes),
                    TimeSpan.FromMinutes(_options.CleanupIntervalMinutes)
                );
            }
        }

        /// <summary>
        /// 将用户信息保存到缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="user">用户信息</param>
        /// <param name="expiry">过期时间，如果不指定则使用默认值</param>
        public async Task SetAsync(string key, TaktSignalROnline user, TimeSpan? expiry = null)
        {
            // 检查用户连接数限制
            if (_options.EnableConnectionLimit)
            {
                var userConnections = await GetConnectionsAsync(user.UserId);
                if (userConnections.Count >= _options.MaxConnectionsPerUser)
                {
                    throw new InvalidOperationException(
                        $"用户 {user.UserId} 的连接数已达到上限 {_options.MaxConnectionsPerUser}");
                }
            }

            // 检查总用户数限制
            if (_options.EnableUserLimit && _userConnections.Count >= _options.MaxUsers)
            {
                // 尝试清理过期连接
                await CleanupExpiredConnectionsAsync();
                if (_userConnections.Count >= _options.MaxUsers)
                {
                    throw new InvalidOperationException(
                        $"系统在线用户数已达到上限 {_options.MaxUsers}");
                }
            }

            // 检查缓存大小限制
            if (_options.EnableSizeLimit)
            {
                var estimatedSize = EstimateObjectSize(user);
                var maxSize = _options.MaxCacheSizeMB * 1024 * 1024L;
                if (Interlocked.Add(ref _cacheSize, estimatedSize) > maxSize)
                {
                    Interlocked.Add(ref _cacheSize, -estimatedSize);
                    // 尝试清理过期数据
                    await CleanupExpiredConnectionsAsync();
                    if (Interlocked.Add(ref _cacheSize, estimatedSize) > maxSize)
                    {
                        throw new InvalidOperationException("缓存大小超出限制");
                    }
                }
            }

            var options = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(expiry ?? TimeSpan.FromMinutes(_options.DefaultCacheMinutes))
                .RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    // 当缓存项被移除时，更新缓存大小
                    if (_options.EnableSizeLimit && value is TaktSignalROnline u)
                    {
                        Interlocked.Add(ref _cacheSize, -EstimateObjectSize(u));
                    }
                });

            _cache.Set(key, user, options);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 从缓存获取用户信息
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>用户信息，如果不存在则返回null</returns>
        public async Task<TaktSignalROnline?> GetAsync(string key)
        {
            _cache.TryGetValue<TaktSignalROnline>(key, out var user);
            if (user != null)
            {
                // 更新最后访问时间
                await SetAsync(key, user);
            }
            return await Task.FromResult(user);
        }

        /// <summary>
        /// 从缓存中删除用户信息
        /// </summary>
        /// <param name="key">缓存键</param>
        public async Task RemoveAsync(string key)
        {
            if (_options.EnableSizeLimit && _cache.TryGetValue<TaktSignalROnline>(key, out var user))
            {
                Interlocked.Add(ref _cacheSize, -EstimateObjectSize(user));
            }
            _cache.Remove(key);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取所有匹配模式的键
        /// </summary>
        /// <param name="pattern">匹配模式</param>
        /// <remarks>
        /// 由于IMemoryCache不支持模式匹配，这里通过遍历用户连接字典来模拟实现
        /// </remarks>
        /// <returns>匹配的键列表</returns>
        public async Task<List<string>> GetKeysByPatternAsync(string pattern)
        {
            var keys = new List<string>();
            foreach (var userConnections in _userConnections.Values)
            {
                foreach (var connectionId in userConnections)
                {
                    keys.Add($"signalr:user:{connectionId}");
                }
            }
            return await Task.FromResult(keys);
        }

        /// <summary>
        /// 设置用户的连接ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="connections">连接ID列表</param>
        public async Task SetConnectionsAsync(long userId, List<string> connections)
        {
            // 限制每个用户的最大连接数
            if (_options.EnableConnectionLimit && connections.Count > _options.MaxConnectionsPerUser)
            {
                connections = connections.Take(_options.MaxConnectionsPerUser).ToList();
            }

            _userConnections.AddOrUpdate(userId, connections, (_, _) => connections);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取用户的连接ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>连接ID列表，如果用户不存在则返回空列表</returns>
        public async Task<List<string>> GetConnectionsAsync(long userId)
        {
            return await Task.FromResult(
                _userConnections.TryGetValue(userId, out var connections)
                    ? connections
                    : new List<string>()
            );
        }

        /// <summary>
        /// 删除用户的连接ID列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        public async Task RemoveConnectionsAsync(long userId)
        {
            if (_userConnections.TryRemove(userId, out var connections))
            {
                foreach (var connectionId in connections)
                {
                    await RemoveAsync($"signalr:user:{connectionId}");
                }
            }
        }

        /// <summary>
        /// 清理过期连接
        /// </summary>
        private async Task CleanupExpiredConnectionsAsync()
        {
            var expiredTime = DateTime.Now.AddMinutes(-_options.DefaultCacheMinutes);
            var keysToRemove = new List<string>();

            foreach (var userConnections in _userConnections.Values)
            {
                foreach (var connectionId in userConnections)
                {
                    var key = $"signalr:user:{connectionId}";
                    if (_cache.TryGetValue<TaktSignalROnline>(key, out var user))
                    {
                        if (user.LastActivity < expiredTime)
                        {
                            keysToRemove.Add(key);
                        }
                    }
                }
            }

            foreach (var key in keysToRemove)
            {
                await RemoveAsync(key);
            }
        }

        /// <summary>
        /// 定时清理缓存的回调方法
        /// </summary>
        private void CleanupCache(object? state)
        {
            _ = CleanupExpiredConnectionsAsync();
        }

        /// <summary>
        /// 估算对象大小
        /// </summary>
        private long EstimateObjectSize(TaktSignalROnline user)
        {
            // 粗略估算每个用户对象占用的内存大小
            return 8 + // UserId (long)
                   (user.ConnectionId?.Length ?? 0) * 2 + // ConnectionId (string)
                   (user.ClientIp?.Length ?? 0) * 2 + // ClientIp (string)
                   (user.UserAgent?.Length ?? 0) * 2 + // UserAgent (string)
                   8 + // LastActiveTime (DateTime)
                   100; // 其他开销
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        public async Task<List<TaktSignalROnline>> GetOnlineUsersAsync()
        {
            var users = new List<TaktSignalROnline>();
            foreach (var userConnections in _userConnections.Values)
            {
                foreach (var connectionId in userConnections)
                {
                    if (_cache.TryGetValue<TaktSignalROnline>($"signalr:user:{connectionId}", out var user))
                    {
                        users.Add(user);
                    }
                }
            }
            return await Task.FromResult(users);
        }

        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        public async Task UpdateLastActivityAsync(string connectionId, DateTime lastActivity)
        {
            if (_cache.TryGetValue<TaktSignalROnline>($"signalr:user:{connectionId}", out var user))
            {
                user.LastActivity = lastActivity;
                await SetAsync($"signalr:user:{connectionId}", user);
            }
        }

        /// <summary>
        /// 更新用户心跳时间
        /// </summary>
        public async Task UpdateHeartbeatAsync(string connectionId, DateTime heartbeat)
        {
            if (_cache.TryGetValue<TaktSignalROnline>($"signalr:user:{connectionId}", out var user))
            {
                user.LastHeartbeat = heartbeat;
                await SetAsync($"signalr:user:{connectionId}", user);
            }
        }

        /// <summary>
        /// 设置用户在线状态
        /// </summary>
        public async Task SetOnlineStatusAsync(string connectionId, int status)
        {
            if (_cache.TryGetValue<TaktSignalROnline>($"signalr:user:{connectionId}", out var user))
            {
                user.OnlineStatus = status;
                await SetAsync($"signalr:user:{connectionId}", user);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _cleanupTimer?.Dispose();
        }
    }
}

