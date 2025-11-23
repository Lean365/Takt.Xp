//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogCleanupJob.cs
// 创建者 : Claude
// 创建时间: 2025-08-20
// 版本号 : V0.0.1
// 描述    : 日志清理定时任务
//===================================================================

using Takt.Domain.IServices.Extensions;
using Quartz;

namespace Takt.Infrastructure.Jobs
{
    /// <summary>
    /// 日志清理定时任务
    /// </summary>
    /// <remarks>
    /// 该任务每24小时执行一次，用于清理过期的日志数据。
    /// 主要功能：
    /// 1. 清理超过保留天数的操作日志
    /// 2. 清理超过保留天数的登录日志
    /// 3. 清理超过保留天数的异常日志
    /// 4. 清理超过保留天数的数据库差异日志
    /// </remarks>
    [DisallowConcurrentExecution] // 禁止并发执行
    public class TaktLogCleanupJob : IJob
    {
        /// <summary>
        /// 日志清理服务
        /// </summary>
        private readonly ITaktLogCleanupService _logCleanupService;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logCleanupService">日志清理服务</param>
        /// <param name="logger">日志记录器</param>
        public TaktLogCleanupJob(
            ITaktLogCleanupService logCleanupService,
            ITaktLogger logger)
        {
            _logCleanupService = logCleanupService ?? throw new ArgumentNullException(nameof(logCleanupService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 执行清理任务
        /// </summary>
        /// <param name="context">任务上下文</param>
        /// <returns>异步任务</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("开始执行日志清理任务");

                // 执行日志清理
                await _logCleanupService.CleanupAsync();

                _logger.Info("日志清理任务执行完成");
            }
            catch (Exception ex)
            {
                _logger.Error("日志清理任务执行失败", ex);
                throw; // 重新抛出异常，让Quartz处理
            }
        }
    }
}


