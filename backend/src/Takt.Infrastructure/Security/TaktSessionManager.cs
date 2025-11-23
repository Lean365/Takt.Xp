//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSessionManager.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 会话管理实现
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.IServices.Caching;
using Takt.Domain.Models.Identity;
using Takt.Domain.Models.SignalR;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Security
{
    /// <summary>
    /// 会话管理实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktSessionManager :
        ITaktIdentitySessionManager
    {
        private readonly TaktSessionOptions _defaultOptions;
        private readonly ITaktRedisCache _cache;
        private readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cache"></param>
        /// <param name="repositoryFactory"></param>
        public TaktSessionManager(
            IOptions<TaktSessionOptions> options,
            ITaktRedisCache cache,
            ITaktRepositoryFactory repositoryFactory)
        {
            _defaultOptions = options.Value;
            _cache = cache;
            _repositoryFactory = repositoryFactory;
        }

        private TaktSessionOptions GetOptions()
        {
            // 直接返回配置文件中的默认值，不再依赖数据库配置表
            return _defaultOptions;
        }

        /// <summary>
        /// 创建会话 (私有实现)
        /// </summary>
        private async Task<string> CreateIdentitySessionAsync(string userId, string userName, string ipAddress, string userAgent)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var options = GetOptions();

            if (!options.ActualAllowMultipleDevices)
            {
                await ((ITaktIdentitySessionManager)this).RemoveUserSessionsAsync(userId);
            }
            else
            {
                var sessions = await GetUserIdentitySessionsAsync(userId);
                if (sessions.Count >= options.ActualMaxConcurrentSessions)
                {
                    // 移除最早的会话
                    var oldestSession = sessions.OrderBy(s => s.LastAccessTime).First();
                    await ((ITaktIdentitySessionManager)this).RemoveSessionAsync(oldestSession.SessionId);
                }
            }

            var sessionId = Guid.NewGuid().ToString("N");
            var sessionInfo = new TaktIdentitySessionInfo
            {
                SessionId = sessionId,
                UserId = userId,
                UserName = userName,
                IpAddress = ipAddress ?? "Unknown",
                UserAgent = userAgent ?? "Unknown",
                LastAccessTime = DateTime.Now,
                LoginTime = DateTime.Now
            };

            await SaveSessionInfoAsync(userId, sessionId, sessionInfo);
            await AddToUserSessionsAsync(userId, sessionId);

            return sessionId;
        }

        /// <summary>
        /// 创建会话 (私有实现)
        /// </summary>
        private async Task<TaktSignalRSessionInfo> CreateServiceSessionAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var sessionId = Guid.NewGuid().ToString("N");
            var refreshToken = Guid.NewGuid().ToString("N");
            var sessionInfo = new TaktSignalRSessionInfo
            {
                SessionId = sessionId,
                RefreshToken = refreshToken,
                ExpiresIn = _defaultOptions.SessionExpiryMinutes
            };

            await SaveServiceSessionInfoAsync(userId, sessionId, sessionInfo);
            await AddToUserSessionsAsync(userId, sessionId);

            return sessionInfo;
        }

        /// <summary>
        /// 获取会话信息 (私有实现)
        /// </summary>
        private async Task<TaktIdentitySessionInfo> GetIdentitySessionInfoAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return null;

            var key = $"session:{sessionId}";
            var sessionInfo = await _cache.GetAsync<TaktIdentitySessionInfo>(key);
            return sessionInfo;
        }

        /// <summary>
        /// 获取用户所有会话 (私有实现)
        /// </summary>
        private async Task<List<TaktIdentitySessionInfo>> GetUserIdentitySessionsAsync(string userId)
        {
            var sessionIds = await GetUserSessionIdsAsync(userId);
            var sessions = new List<TaktIdentitySessionInfo>();

            foreach (var sessionId in sessionIds)
            {
                var sessionInfo = await GetIdentitySessionInfoAsync(sessionId);
                if (sessionInfo != null)
                {
                    sessions.Add(sessionInfo);
                }
            }

            return sessions;
        }

        /// <summary>
        /// 创建会话 (Domain.IServices.Identity.ITaktIdentitySessionManager)
        /// </summary>
        async Task<string> ITaktIdentitySessionManager.CreateSessionAsync(string userId, string userName, string ipAddress, string userAgent)
        {
            return await CreateIdentitySessionAsync(userId, userName, ipAddress, userAgent);
        }

        /// <summary>
        /// 获取会话信息 (Domain.IServices.Identity.ITaktIdentitySessionManager)
        /// </summary>
        async Task<TaktIdentitySessionInfo> ITaktIdentitySessionManager.GetSessionInfoAsync(string sessionId)
        {
            return await GetIdentitySessionInfoAsync(sessionId);
        }

        /// <summary>
        /// 获取用户所有会话 (Domain.IServices.Identity.ITaktIdentitySessionManager)
        /// </summary>
        async Task<List<TaktIdentitySessionInfo>> ITaktIdentitySessionManager.GetUserSessionsAsync(string userId)
        {
            return await GetUserIdentitySessionsAsync(userId);
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        public async Task<TaktSignalRSessionInfo> CreateSessionAsync(string userId)
        {
            return await CreateServiceSessionAsync(userId);
        }



        /// <summary>
        /// 更新会话访问时间
        /// </summary>
        async Task ITaktIdentitySessionManager.UpdateSessionAccessTimeAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return;

            var sessionInfo = await GetIdentitySessionInfoAsync(sessionId);
            if (sessionInfo != null)
            {
                sessionInfo.LastAccessTime = DateTime.Now;
                await SaveSessionInfoAsync(sessionInfo.UserId, sessionId, sessionInfo);
            }
        }

        /// <summary>
        /// 移除会话
        /// </summary>
        async Task ITaktIdentitySessionManager.RemoveSessionAsync(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
                return;

            var sessionInfo = await GetIdentitySessionInfoAsync(sessionId);
            if (sessionInfo != null)
            {
                await _cache.RemoveAsync($"session:{sessionId}");
                await RemoveFromUserSessionsAsync(sessionInfo.UserId, sessionId);
            }
        }

        /// <summary>
        /// 移除用户所有会话
        /// </summary>
        async Task ITaktIdentitySessionManager.RemoveUserSessionsAsync(string userId)
        {
            var sessionIds = await GetUserSessionIdsAsync(userId);
            foreach (var sessionId in sessionIds)
            {
                await ((ITaktIdentitySessionManager)this).RemoveSessionAsync(sessionId);
            }
        }

        /// <summary>
        /// 保存会话信息
        /// </summary>
        private async Task SaveSessionInfoAsync(string userId, string sessionId, TaktIdentitySessionInfo sessionInfo)
        {
            var key = $"session:{sessionId}";
            var expiry = _defaultOptions.EnableSlidingExpiration
                ? TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes)
                : TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes * 2);
            await _cache.SetAsync(key, sessionInfo, expiry);
        }

        /// <summary>
        /// 保存服务会话信息
        /// </summary>
        private async Task SaveServiceSessionInfoAsync(string userId, string sessionId, TaktSignalRSessionInfo sessionInfo)
        {
            var key = $"service:session:{sessionId}";
            var expiry = TimeSpan.FromMinutes(_defaultOptions.SessionExpiryMinutes);
            await _cache.SetAsync(key, sessionInfo, expiry);
        }

        /// <summary>
        /// 添加到用户会话列表
        /// </summary>
        private async Task AddToUserSessionsAsync(string userId, string sessionId)
        {
            var key = $"user:sessions:{userId}";
            var sessions = await GetUserSessionIdsAsync(userId);
            sessions.Add(sessionId);
            await _cache.SetAsync(key, sessions);
        }

        /// <summary>
        /// 从用户会话列表移除
        /// </summary>
        private async Task RemoveFromUserSessionsAsync(string userId, string sessionId)
        {
            var key = $"user:sessions:{userId}";
            var sessions = await GetUserSessionIdsAsync(userId);
            sessions.Remove(sessionId);
            await _cache.SetAsync(key, sessions);
        }

        /// <summary>
        /// 获取用户会话ID列表
        /// </summary>
        private async Task<List<string>> GetUserSessionIdsAsync(string userId)
        {
            var key = $"user:sessions:{userId}";
            return await _cache.GetAsync<List<string>>(key) ?? new List<string>();
        }

        /// <summary>
        /// 清理过期会话
        /// </summary>
        private async Task CleanupExpiredSessionsAsync()
        {
            var options = GetOptions();
            var allSessions = await GetAllSessionsAsync();
            var now = DateTime.Now;

            foreach (var session in allSessions)
            {
                var isExpired = false;

                if (options.EnableSlidingExpiration)
                {
                    var idleTime = now - session.LastAccessTime;
                    if (idleTime.TotalMinutes > options.SessionExpiryMinutes)
                    {
                        isExpired = true;
                    }
                }

                if (options.EnableAbsoluteExpiration)
                {
                    var sessionAge = now - session.LoginTime;
                    if (sessionAge.TotalHours > options.AbsoluteExpirationHours)
                    {
                        isExpired = true;
                    }
                }

                if (isExpired)
                {
                    await ((ITaktIdentitySessionManager)this).RemoveSessionAsync(session.SessionId);
                }
            }
        }

        /// <summary>
        /// 获取所有会话
        /// </summary>
        private async Task<List<TaktIdentitySessionInfo>> GetAllSessionsAsync()
        {
            var pattern = "session:*";
            var keys = await _cache.SearchKeysAsync(pattern);
            var sessions = new List<TaktIdentitySessionInfo>();

            foreach (var key in keys)
            {
                var session = await _cache.GetAsync<TaktIdentitySessionInfo>(key);
                if (session != null)
                {
                    sessions.Add(session);
                }
            }

            return sessions;
        }
    }
}




