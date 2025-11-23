//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSignalROnlineCleanupJob.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线用户清理任务 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Routine.SignalR;
using Quartz;

namespace Takt.Infrastructure.Jobs
{
    /// <summary>
    /// 在线用户清理任务
    /// </summary>
    /// <remarks>
    /// 该任务每分钟执行一次，用于清理超时未发送心跳的用户。
    /// 主要功能：
    /// 1. 检测超过5分钟未更新心跳的用户
    /// 2. 将这些用户标记为离线状态
    /// 3. 更新用户的最后活动时间
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    [DisallowConcurrentExecution] // 禁止并发执行
    public class TaktSignalROnlineCleanupJob : IJob
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 获取在线用户仓储
        /// </summary>
        private ITaktRepository<TaktSignalROnline> Repository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志记录器</param>
        public TaktSignalROnlineCleanupJob(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
        }

        /// <summary>
        /// 执行清理任务
        /// </summary>
        /// <param name="context">任务执行上下文</param>
        /// <returns>异步任务</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("开始执行在线用户清理任务");

                // 只进行清理，不自动更新心跳
                // 获取超过5分钟未更新心跳的用户
                var timeout = DateTime.Now.AddMinutes(-5);
                
                // 添加调试日志，先查询所有在线用户
                var allOnlineUsers = await Repository.GetListAsync(u => u.OnlineStatus == 0);
                _logger.Debug("当前在线用户总数: {Count}", allOnlineUsers?.Count ?? 0);
                
                if (allOnlineUsers?.Any() == true)
                {
                    _logger.Debug("在线用户详情:");
                    foreach (var user in allOnlineUsers.Take(5)) // 只显示前5个用户的信息
                    {
                        _logger.Debug("  用户ID: {UserId}, 设备ID: {DeviceId}, 最后心跳: {LastHeartbeat}, 最后活动: {LastActivity}, 连接ID: {ConnectionId}",
                            user.UserId, user.DeviceId, user.LastHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"), 
                            user.LastActivity.ToString("yyyy-MM-dd HH:mm:ss"), user.ConnectionId);
                    }
                }
                
                // 查询超时用户（只查询在线状态的用户）
                var timeoutUsers = await Repository.GetListAsync(u => u.OnlineStatus == 0 && u.LastHeartbeat < timeout);
                _logger.Debug("超过5分钟未心跳的在线用户总数: {Count}", timeoutUsers?.Count ?? 0);
                
                if (timeoutUsers?.Any() == true)
                {
                    _logger.Debug("超时用户详情:");
                    foreach (var user in timeoutUsers.Take(5)) // 只显示前5个用户的信息
                    {
                        _logger.Debug("  用户ID: {UserId}, 设备ID: {DeviceId}, 在线状态: {OnlineStatus}, 最后心跳: {LastHeartbeat}, 最后活动: {LastActivity}",
                            user.UserId, user.DeviceId, user.OnlineStatus, user.LastHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"), 
                            user.LastActivity.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                
                // 现在应用完整的查询条件
                var finalTimeoutUsers = await Repository.GetListAsync(
                    u => u.OnlineStatus == 0 && u.LastHeartbeat < timeout
                );

                if (finalTimeoutUsers?.Any() == true)
                {
                    _logger.Info("发现{Count}个超时用户，开始批量更新", finalTimeoutUsers.Count);

                    // 开启事务
                    try
                    {
                        var now = DateTime.Now;
                        foreach (var user in finalTimeoutUsers)
                        {
                            user.OnlineStatus = 1;
                            user.LastActivity = now;
                            // 审计字段由 TaktSignalRRepository 统一处理
                        }
                        await Repository.UpdateRangeAsync(finalTimeoutUsers);

                        _logger.Info("成功清理{Count}个超时用户", finalTimeoutUsers.Count);

                        // 记录每个用户的详细信息
                        foreach (var user in finalTimeoutUsers)
                        {
                        _logger.Info(
                            "用户{UserId}被标记为离线 - 最后心跳时间:{LastHeartbeat}, 连接ID:{ConnectionId}",
                            user.UserId,
                            user.LastHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"),
                            user.ConnectionId ?? "未知");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("批量更新用户状态时发生错误", ex.Message);
                        throw;
                    }
                }
                else
                {
                    _logger.Info("没有发现需要清理的超时用户");
                    
                    // 如果没有找到超时用户，提供更多诊断信息
                    if (allOnlineUsers?.Any() == true)
                    {
                        var oldestHeartbeat = allOnlineUsers.Min(u => u.LastHeartbeat);
                        var newestHeartbeat = allOnlineUsers.Max(u => u.LastHeartbeat);
                        var oldestActivity = allOnlineUsers.Min(u => u.LastActivity);
                        var newestActivity = allOnlineUsers.Max(u => u.LastActivity);
                        
                        _logger.Debug("在线用户时间范围分析:");
                        _logger.Debug("  最后心跳时间范围: {OldestHeartbeat} 到 {NewestHeartbeat}", 
                            oldestHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"), 
                            newestHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"));
                        _logger.Debug("  最后活动时间范围: {OldestActivity} 到 {NewestActivity}", 
                            oldestActivity.ToString("yyyy-MM-dd HH:mm:ss"), 
                            newestActivity.ToString("yyyy-MM-dd HH:mm:ss"));
                        _logger.Debug("  超时阈值时间: {Timeout}", timeout.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("执行在线用户清理任务时发生错误", ex.Message);
                throw;
            }
        }
    }
}



