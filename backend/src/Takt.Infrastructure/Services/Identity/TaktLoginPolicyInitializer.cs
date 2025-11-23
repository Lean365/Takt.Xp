//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginPolicyInitializer.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述    : 登录策略初始化服务
//===================================================================

using System.Collections;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace Takt.Infrastructure.Services.Identity
{
    /// <summary>
    /// 登录策略初始化服务
    /// </summary>
    /// <remarks>
    /// 本服务负责在应用程序启动时执行以下操作：
    /// 1. 清除所有登录限制相关的缓存
    /// 2. 重置所有用户的登录尝试计数
    /// 3. 清除所有验证码要求标记
    /// </remarks>
    public class TaktLoginPolicyInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 缓存键前缀常量
        /// </summary>
        private const string LOGIN_ATTEMPT_PREFIX = "login_attempt:";

        private const string LAST_LOGIN_PREFIX = "last_login:";
        private const string CAPTCHA_REQUIRED_PREFIX = "captcha_required:";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider">服务提供者</param>
        /// <param name="logger">日志服务</param>
        public TaktLoginPolicyInitializer(
            IServiceProvider serviceProvider,
            ITaktLogger logger
)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 启动时执行
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>异步任务</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.Info("[登录策略] 应用启动，开始清除所有登录限制缓存");
                using var scope = _serviceProvider.CreateScope();
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();

                if (cache is MemoryCache memoryCache)
                {
                    // 获取内存缓存的所有项
                    var field = typeof(MemoryCache).GetProperty("EntriesCollection",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    var collection = field?.GetValue(memoryCache);
                    var items = collection?.GetType().GetProperty("Items")?.GetValue(collection);
                    var keys = new List<string>();

                    _logger.Info("[登录策略] 正在扫描缓存项...");

                    // 查找所有需要清除的缓存项
                    if (items is IDictionary entries)
                    {
                        foreach (DictionaryEntry entry in entries)
                        {
                            var key = entry.Key?.ToString();
                            if (key != null)
                            {
                                _logger.Info("[登录策略] 发现缓存项: {Key}", key);
                                if (key.StartsWith(LOGIN_ATTEMPT_PREFIX) ||
                                    key.StartsWith(LAST_LOGIN_PREFIX) ||
                                    key.StartsWith(CAPTCHA_REQUIRED_PREFIX))
                                {
                                    keys.Add(key);
                                    _logger.Info("[登录策略] 标记需要清除的缓存项: {Key}", key);
                                }
                            }
                        }
                    }
                    else
                    {
                        _logger.Warn("[登录策略] 无法获取缓存项集合");
                    }

                    // 清除所有找到的缓存项
                    int clearedCount = 0;
                    foreach (var key in keys)
                    {
                        try
                        {
                            cache.Remove(key);
                            clearedCount++;
                            _logger.Info("[登录策略] 已清除缓存项: {Key}", key);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("[登录策略] 清除缓存项 {Key} 时发生错误", key, ex.Message);
                        }
                    }

                    _logger.Info("[登录策略] 清除完成，共清除 {Count} 个缓存项", clearedCount);

                    // 验证清除结果
                    var remainingKeys = new List<string>();
                    if (items is IDictionary entriesAfterClear)
                    {
                        foreach (DictionaryEntry entry in entriesAfterClear)
                        {
                            var key = entry.Key?.ToString();
                            if (key != null && (
                                key.StartsWith(LOGIN_ATTEMPT_PREFIX) ||
                                key.StartsWith(LAST_LOGIN_PREFIX) ||
                                key.StartsWith(CAPTCHA_REQUIRED_PREFIX)))
                            {
                                remainingKeys.Add(key);
                            }
                        }
                    }

                    if (remainingKeys.Count > 0)
                    {
                        _logger.Warn("[登录策略] 清除后仍有 {Count} 个相关缓存项: {Keys}",
                            remainingKeys.Count, string.Join(", ", remainingKeys));
                    }
                    else
                    {
                        _logger.Info("[登录策略] 验证通过，所有相关缓存项已清除");
                    }
                }

                await Task.CompletedTask;
                _logger.Info("[登录策略] 初始化完成");
            }
            catch (Exception ex)
            {
                _logger.Error("[登录策略] 初始化过程中发生错误", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 停止时执行
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>异步任务</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Info("[登录策略] 服务停止");
            return Task.CompletedTask;
        }
    }
}



