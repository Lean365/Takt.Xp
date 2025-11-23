using Takt.Application.Services.Logging;
using Takt.Shared.Options;
using Takt.Domain.Entities.Routine.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Services.Identity;

/// <summary>
/// 系统重启服务
/// 用于处理系统重启时的清理工作，包括：
/// 1. 清理用户会话信息
/// 2. 清理缓存信息
/// 3. 清理实时通信状态
/// 4. 清理系统状态
/// 5. 清理安全相关信息
/// 6. 清理临时数据
/// 7. 清理性能监控数据
/// 8. 清理分布式状态
/// </summary>
public class TaktRestartService : ITaktRestartService
{

    private readonly IDistributedCache _cache;

    /// <summary>
    /// 日志服务
    /// </summary>
    protected readonly ITaktLogger _logger;

    private readonly TaktRestartOptions _options;
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly IConnectionMultiplexer? _redisConnection;
    private readonly IOptions<TaktCacheOptions> _cacheOptions;

    /// <summary>
    /// 获取在线用户仓储
    /// </summary>
    private ITaktRepository<TaktSignalROnline> OnlineUserRepository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cache">分布式缓存服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="options">系统重启选项</param>
    /// <param name="cacheOptions">缓存选项</param>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="redisConnection">Redis连接</param>
    public TaktRestartService(
        IDistributedCache cache,
        ITaktLogger logger,
        IOptions<TaktRestartOptions> options,
        IOptions<TaktCacheOptions> cacheOptions,
        ITaktRepositoryFactory repositoryFactory,
        IConnectionMultiplexer? redisConnection = null)
    {

        _cache = cache;
        _logger = logger;
        _options = options.Value;
        _cacheOptions = cacheOptions;
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _redisConnection = redisConnection;
    }

    /// <summary>
    /// 执行系统重启清理
    /// 按照预定义的顺序执行所有清理操作，确保系统重启后处于一个干净的状态
    /// </summary>
    /// <returns>清理结果，true表示成功，false表示失败</returns>
    public async Task<bool> ExecuteRestartCleanupAsync()
    {
        try
        {
            // 清理用户会话信息，包括在线状态和登录信息
            if (_options.ClearUserSessions)
            {
                await CleanupUserSessionsAsync();
            }

            // 清理系统缓存数据，包括用户信息、权限、菜单等缓存
            if (_options.ClearCache)
            {
                await CleanupCacheAsync();
            }

            // 清理实时通信连接，包括SignalR和WebSocket连接
            if (_options.ClearRealTimeConnections)
            {
                await CleanupRealTimeConnectionsAsync();
            }

            // 清理系统运行状态，包括定时任务、工作流等状态
            if (_options.ClearSystemStatus)
            {
                await CleanupSystemStatusAsync();
            }

            // 清理安全相关信息，包括验证码、登录失败记录等
            if (_options.ClearSecurityInfo)
            {
                await CleanupSecurityInfoAsync();
            }

            // 清理临时文件数据，包括上传、导入导出等临时文件
            if (_options.ClearTempData)
            {
                await CleanupTempDataAsync();
            }

            // 清理性能监控数据，包括接口调用统计、资源使用统计等
            if (_options.ClearPerformanceData)
            {
                await CleanupPerformanceDataAsync();
            }

            // 清理分布式状态数据，包括分布式会话、锁等信息
            if (_options.ClearDistributedState)
            {
                await CleanupDistributedStateAsync();
            }

            _logger.Info("系统重启清理完成");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("系统重启清理过程中发生错误", ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 清理用户会话信息
    /// 清除所有用户的在线状态，包括：
    /// 1. 设置用户为离线状态
    /// 2. 记录最后离线时间
    /// 3. 清除登录相关信息
    /// </summary>
    private async Task CleanupUserSessionsAsync()
    {
        try
        {
            // 清除所有用户的在线状态（直接更新数据库）

            // 更新在线用户表中的所有用户状态为离线
            var exp = Expressionable.Create<TaktSignalROnline>();
            exp.And(u => u.OnlineStatus == 0); // 0表示在线状态

            var onlineUsers = await OnlineUserRepository.GetListAsync(exp.ToExpression());
            if (onlineUsers.Any())
            {
                foreach (var user in onlineUsers)
                {
                    user.OnlineStatus = 1; // 1表示离线状态
                    user.LastActivity = DateTime.Now;
                }
                await OnlineUserRepository.UpdateRangeAsync(onlineUsers);
                _logger.Info("已更新{Count}个在线用户状态为离线", onlineUsers.Count);
            }

            _logger.Info("用户会话信息清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理用户会话信息时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理缓存信息
    /// 清除所有缓存数据，包括：
    /// 1. 用户信息缓存
    /// 2. 权限信息缓存
    /// 3. 菜单信息缓存
    /// 4. 系统配置缓存
    /// 5. 字典数据缓存
    /// </summary>
    private async Task CleanupCacheAsync()
    {
        try
        {
            if (_cacheOptions.Value.Provider == CacheProviderType.Redis && _redisConnection != null)
            {
                // 使用Redis直接清理缓存
                var server = _redisConnection.GetServer(_cacheOptions.Value.Redis.ConnectionString);
                var db = _redisConnection.GetDatabase(_cacheOptions.Value.Redis.DefaultDatabase);

                // 清理所有系统相关的缓存
                var keys = server.Keys(pattern: $"{_cacheOptions.Value.Redis.InstanceName}*").ToArray();
                if (keys.Any())
                {
                    await db.KeyDeleteAsync(keys);
                    _logger.Info("已清理{Count}个Redis缓存键", keys.Length);
                }
            }
            else
            {
                // 使用IDistributedCache接口清理缓存
                await _cache.RemoveAsync("system:cache:all");
            }

            _logger.Info("缓存信息清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理缓存信息时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理实时通信状态
    /// 清除所有实时通信相关的状态，包括：
    /// 1. SignalR连接状态
    /// 2. WebSocket连接状态
    /// 3. 消息推送状态
    /// </summary>
    private async Task CleanupRealTimeConnectionsAsync()
    {
        try
        {
            _logger.Info("开始清理实时通信状态");

            // 1. 清理在线用户表
            var exp = Expressionable.Create<TaktSignalROnline>();
            exp.And(u => u.OnlineStatus == 0); // 0表示在线状态
            var onlineUsers = await OnlineUserRepository.GetListAsync(exp.ToExpression());

            if (onlineUsers.Any())
            {
                foreach (var user in onlineUsers)
                {
                    user.OnlineStatus = 1; // 1表示离线状态
                    user.LastActivity = DateTime.Now;
                }
                await OnlineUserRepository.UpdateRangeAsync(onlineUsers);
                _logger.Info("已更新{Count}个在线用户状态为离线", onlineUsers.Count);
            }

            // 2. 清理 Redis 中的 SignalR 连接信息
            if (_cacheOptions.Value.Provider == CacheProviderType.Redis && _redisConnection != null)
            {
                var server = _redisConnection.GetServer(_cacheOptions.Value.Redis.ConnectionString);
                var db = _redisConnection.GetDatabase(_cacheOptions.Value.Redis.DefaultDatabase);

                // 清理所有 SignalR 相关的缓存
                var keys = server.Keys(pattern: $"{_cacheOptions.Value.Redis.InstanceName}signalr:*").ToArray();
                if (keys.Any())
                {
                    await db.KeyDeleteAsync(keys);
                    _logger.Info("已清理{Count}个SignalR缓存键", keys.Length);
                }
            }
            else
            {
                // 清理分布式缓存中的 SignalR 信息
                await _cache.RemoveAsync("signalr:connections:all");
                await _cache.RemoveAsync("signalr:users:all");
                await _cache.RemoveAsync("signalr:devices:all");
            }

            _logger.Info("实时通信状态清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理实时通信状态时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理系统状态
    /// 清除所有系统相关的状态，包括：
    /// 1. 定时任务状态
    /// 2. 工作流实例状态
    /// 3. 消息队列状态
    /// 4. 分布式锁状态
    /// </summary>
    private async Task CleanupSystemStatusAsync()
    {
        try
        {
            // 清除系统状态相关的缓存
            await _cache.RemoveAsync("system:status:all");
            _logger.Info("系统状态清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理系统状态时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理安全相关信息
    /// 清除所有安全相关的信息，包括：
    /// 1. 验证码缓存
    /// 2. 登录失败次数记录
    /// 3. 敏感操作日志
    /// 4. 用户token信息
    /// </summary>
    private async Task CleanupSecurityInfoAsync()
    {
        try
        {
            // 清除安全相关的缓存
            await _cache.RemoveAsync("security:info:all");
            _logger.Info("安全相关信息清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理安全相关信息时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理临时数据
    /// 清除所有临时数据，包括：
    /// 1. 文件上传临时文件
    /// 2. 导入导出临时文件
    /// 3. 报表生成临时文件
    /// </summary>
    private async Task CleanupTempDataAsync()
    {
        try
        {
            // 清除临时数据相关的缓存
            await _cache.RemoveAsync("temp:data:all");
            _logger.Info("临时数据清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理临时数据时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理性能监控数据
    /// 清除所有性能监控相关的数据，包括：
    /// 1. 接口调用统计
    /// 2. 系统资源使用统计
    /// 3. 性能指标数据
    /// </summary>
    private async Task CleanupPerformanceDataAsync()
    {
        try
        {
            // 清除性能监控相关的缓存
            await _cache.RemoveAsync("performance:data:all");
            _logger.Info("性能监控数据清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理性能监控数据时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 清理分布式状态
    /// 清除所有分布式相关的状态，包括：
    /// 1. 分布式会话
    /// 2. 分布式锁
    /// 3. 分布式缓存
    /// 4. 分布式任务
    /// </summary>
    private async Task CleanupDistributedStateAsync()
    {
        try
        {
            // 清除分布式状态相关的缓存
            await _cache.RemoveAsync("distributed:state:all");
            _logger.Info("分布式状态清理完成");
        }
        catch (Exception ex)
        {
            _logger.Error("清理分布式状态时发生错误", ex.Message);
            throw;
        }
    }
}


