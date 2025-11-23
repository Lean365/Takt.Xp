//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSingleUserLoginCleanupJob.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 17:30
// 版本号 : V0.0.1
// 描述   : 单用户登录清理任务
//===================================================================

using Takt.Domain.IServices.Security;
using Quartz;

namespace Takt.Infrastructure.Jobs
{
    /// <summary>
    /// 单用户登录清理任务
    /// </summary>
    public class TaktSingleUserLoginCleanupJob : IJob
    {
        private readonly ITaktSingleUserLoginService _singleUserLoginService;
        private readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktSingleUserLoginCleanupJob(
            ITaktSingleUserLoginService singleUserLoginService,
            ITaktLogger logger)
        {
            _singleUserLoginService = singleUserLoginService;
            _logger = logger;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("开始执行单用户登录清理任务");

                var cleanedCount = await _singleUserLoginService.CleanupExpiredSessionsAsync();

                _logger.Info("单用户登录清理任务执行完成 - 清理会话数: {Count}", cleanedCount);
            }
            catch (Exception ex)
            {
                _logger.Error("执行单用户登录清理任务时发生错误", ex);
            }
        }
    }
}




