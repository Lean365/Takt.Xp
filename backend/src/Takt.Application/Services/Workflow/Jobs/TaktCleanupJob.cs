#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCleanupJob.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流清理任务
//===================================================================

using Takt.Application.Services.Workflow;
using Takt.Shared.Options;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.IServices;
using Takt.Domain.Repositories;
using Microsoft.Extensions.Options;
using Quartz;

namespace Takt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流清理任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class TaktCleanupJob : IJob
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        private readonly ITaktRepositoryFactory _repositoryFactory;
        
        /// <summary>
        /// 日志服务
        /// </summary>
        private readonly ITaktLogger _logger;
        
        /// <summary>
        /// 工作流配置选项
        /// </summary>
        private readonly TaktWorkflowOptions _workflowOptions;

        /// <summary>
        /// 获取工作流实例仓储
        /// </summary>
        private ITaktRepository<TaktInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<TaktInstance>();

        /// <summary>
        /// 获取工作流操作记录仓储
        /// </summary>
        private ITaktRepository<TaktInstanceOper> InstanceOperRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceOper>();

        /// <summary>
        /// 获取工作流流转记录仓储
        /// </summary>
        private ITaktRepository<TaktInstanceTrans> InstanceTransRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceTrans>();
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        /// <exception cref="ArgumentNullException">当repositoryFactory为null时抛出</exception>
        public TaktCleanupJob(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IOptions<TaktWorkflowOptions> workflowOptions)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
            _workflowOptions = workflowOptions.Value;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context">任务上下文</param>
        /// <returns>异步任务</returns>
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("开始执行工作流清理任务");

                if (!_workflowOptions.Cleanup.Enabled)
                {
                    _logger.Info("清理功能已禁用");
                    return;
                }

                await CleanupExpiredDataAsync();

                _logger.Info("工作流清理任务执行完成");
            }
            catch (Exception ex)
            {
                _logger.Error("工作流清理任务执行失败", ex);
                throw; // 重新抛出异常，让Quartz处理
            }
        }

        /// <summary>
        /// 清理过期数据
        /// </summary>
        /// <returns>异步任务</returns>
        private async Task CleanupExpiredDataAsync()
        {
            try
            {
                var cleanupDate = DateTime.Now.AddDays(-_workflowOptions.Cleanup.RetentionDays);
                int cleanedCount = 0;

                // 清理已完成的实例
                var completedInstances = await InstanceRepository.GetListAsync(
                    i => i.Status == 2 && // 2 表示已完成
                         i.EndTime.HasValue && 
                         i.EndTime < cleanupDate);

                if (completedInstances.Any())
                {
                    var instanceIds = completedInstances.Select(i => i.Id).ToList();
                    
                    // 删除相关的操作记录
                    await InstanceOperRepository.DeleteAsync(o => instanceIds.Contains(o.InstanceId));

                    // 删除相关的流转记录
                    await InstanceTransRepository.DeleteAsync(t => instanceIds.Contains(t.InstanceId));

                    // 删除实例
                    await InstanceRepository.DeleteAsync(i => instanceIds.Contains(i.Id));

                    cleanedCount = cleanedCount + completedInstances.Count;
                    _logger.Info($"清理了 {completedInstances.Count} 个已完成的工作流实例");
                }

                // 清理已终止的实例
                var terminatedInstances = await InstanceRepository.GetListAsync(
                    i => i.Status == 3 && // 3 表示已终止
                         i.EndTime.HasValue && 
                         i.EndTime < cleanupDate);

                if (terminatedInstances.Any())
                {
                    var instanceIds = terminatedInstances.Select(i => i.Id).ToList();
                    
                    // 删除相关的操作记录
                    await InstanceOperRepository.DeleteAsync(o => instanceIds.Contains(o.InstanceId));

                    // 删除相关的流转记录
                    await InstanceTransRepository.DeleteAsync(t => instanceIds.Contains(t.InstanceId));

                    // 删除实例
                    await InstanceRepository.DeleteAsync(i => instanceIds.Contains(i.Id));

                    cleanedCount = cleanedCount + terminatedInstances.Count;
                    _logger.Info($"清理了 {terminatedInstances.Count} 个已终止的工作流实例");
                }

                if (cleanedCount > 0)
                {
                    _logger.Info($"工作流清理任务完成，共清理了 {cleanedCount} 个实例及相关数据");
                }
                else
                {
                    _logger.Info("工作流清理任务完成，没有需要清理的数据");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("工作流清理任务执行失败", ex);
            }
        }
    }
} 


