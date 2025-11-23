//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktQuartzService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 定时任务服务实现
//===================================================================

using Takt.Application.Services.Routine.Quartz.Jobs;
using Takt.Domain.IServices.SignalR;
using Takt.Shared.Models;
using Takt.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Quartz;
namespace Takt.Application.Services.Routine.Quartz
{
    /// <summary>
    /// 定时任务服务实现
    /// </summary>
    public class TaktQuartzService : TaktBaseService, ITaktQuartzService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取定时任务仓储
        /// </summary>
        private ITaktRepository<TaktQuartz> TaskRepository => _repositoryFactory.GetBusinessRepository<TaktQuartz>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktQuartzService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            ITaktSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取定时任务分页列表
        /// </summary>
        public async Task<TaktPagedResult<TaktQuartzDto>> GetListAsync(TaktQuartzQueryDto query)
        {
            var exp = Expressionable.Create<TaktQuartz>();

            if (!string.IsNullOrEmpty(query.QuartzName))
                exp.And(x => x.QuartzName.Contains(query.QuartzName));

            if (!string.IsNullOrEmpty(query.QuartzGroupName))
                exp.And(x => x.QuartzGroupName == query.QuartzGroupName);

            if (query.QuartzType.HasValue)
                exp.And(x => x.QuartzType == query.QuartzType.Value);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            if (!string.IsNullOrEmpty(query.CreateBy))
                exp.And(x => x.CreateBy == query.CreateBy);

            var result = await TaskRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new TaktPagedResult<TaktQuartzDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktQuartzDto>>()
            };
        }

        /// <summary>
        /// 获取定时任务详情
        /// </summary>
        public async Task<TaktQuartzDto> GetByIdAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            return task.Adapt<TaktQuartzDto>();
        }

        /// <summary>
        /// 创建定时任务
        /// </summary>
        public async Task<long> CreateAsync(TaktQuartzCreateDto input)
        {
            var task = input.Adapt<TaktQuartz>();
            task.CreateTime = DateTime.Now;

            // 验证任务是否已存在
            var existingTask = await TaskRepository.GetFirstAsync(x => x.QuartzName == input.QuartzName && x.QuartzGroupName == input.QuartzGroupName);
            if (existingTask != null)
                throw new TaktException(L("Quartz.AlreadyExists", input.QuartzName));

            var result = await TaskRepository.CreateAsync(task);
            if (result <= 0)
                throw new TaktException(L("Quartz.CreateFailed"));

            // 发送实时通知
            await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
            {
                Type = TaktMessageType.System,
                Title = L("Quartz.Created"),
                Content = L("Quartz.CreatedContent", task.QuartzName),
                Timestamp = DateTime.Now,
                Data = task
            });

            // 如果任务状态为启用，则立即启动
            if (task.Status == 1)
                await StartAsync(task.Id);

            return task.Id;
        }

        /// <summary>
        /// 更新定时任务
        /// </summary>
        public async Task<bool> UpdateAsync(long taskId, TaktQuartzDto input)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            // 如果任务正在运行，需要先停止
            if (task.Status == 1)
                await StopAsync(taskId);

            // 更新任务信息
            input.Adapt(task);
            var result = await TaskRepository.UpdateAsync(task);
            if (result <= 0)
                throw new TaktException(L("Quartz.UpdateFailed"));

            // 发送实时通知
            await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
            {
                Type = TaktMessageType.System,
                Title = L("Quartz.Updated"),
                Content = L("Quartz.UpdatedContent", task.QuartzName),
                Timestamp = DateTime.Now,
                Data = task
            });

            // 如果更新后的状态为启用，则重新启动任务
            if (task.Status == 1)
                await StartAsync(taskId);

            return true;
        }

        /// <summary>
        /// 删除定时任务
        /// </summary>
        public async Task<bool> DeleteAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            // 如果任务正在运行，需要先停止
            if (task.Status == 1)
                await StopAsync(taskId);

            var result = await TaskRepository.DeleteAsync(taskId);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Quartz.Deleted"),
                    Content = L("Quartz.DeletedContent", task.QuartzName),
                    Timestamp = DateTime.Now,
                    Data = task
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除定时任务
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] taskIds)
        {
            if (taskIds == null || taskIds.Length == 0)
                throw new TaktException(L("Quartz.SelectToDelete"));

            foreach (var taskId in taskIds)
            {
                await DeleteAsync(taskId);
            }
            return true;
        }

        /// <summary>
        /// 执行定时任务
        /// </summary>
        public async Task<bool> ExecuteAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            try
            {
                return await TaktQuartzHelper.TriggerJobAsync(task.QuartzName, task.QuartzGroupName);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.ExecuteFailed", taskId), ex);
                throw new TaktException(L("Quartz.ExecuteFailed", taskId));
            }
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        public async Task<bool> StartAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            try
            {
                var result = await TaktQuartzHelper.AddOrUpdateJobAsync<TaktQuartzJob>(task.QuartzName, task.QuartzGroupName, task.QuartzCronExpression);
                
                if (result)
                {
                    // 发送实时通知
                    await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = L("Quartz.Started"),
                        Content = L("Quartz.StartedContent", task.QuartzName),
                        Timestamp = DateTime.Now,
                        Data = task
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.StartFailed", taskId), ex);
                throw new TaktException(L("Quartz.StartFailed", taskId));
            }
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public async Task<bool> StopAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            try
            {
                var result = await TaktQuartzHelper.DeleteJobAsync(task.QuartzName, task.QuartzGroupName);
                
                if (result)
                {
                    // 发送实时通知
                    await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = L("Quartz.Stopped"),
                        Content = L("Quartz.StoppedContent", task.QuartzName),
                        Timestamp = DateTime.Now,
                        Data = task
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.StopFailed", taskId), ex);
                throw new TaktException(L("Quartz.StopFailed", taskId));
            }
        }

        /// <summary>
        /// 暂停定时任务
        /// </summary>
        public async Task<bool> PauseAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            try
            {
                var result = await TaktQuartzHelper.PauseJobAsync(task.QuartzName, task.QuartzGroupName);
                
                if (result)
                {
                    // 发送实时通知
                    await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = L("Quartz.Paused"),
                        Content = L("Quartz.PausedContent", task.QuartzName),
                        Timestamp = DateTime.Now,
                        Data = task
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.PauseFailed", taskId), ex);
                throw new TaktException(L("Quartz.PauseFailed", taskId));
            }
        }

        /// <summary>
        /// 恢复定时任务
        /// </summary>
        public async Task<bool> ResumeAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            try
            {
                var result = await TaktQuartzHelper.ResumeJobAsync(task.QuartzName, task.QuartzGroupName);
                
                if (result)
                {
                    // 发送实时通知
                    await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = L("Quartz.Resumed"),
                        Content = L("Quartz.ResumedContent", task.QuartzName),
                        Timestamp = DateTime.Now,
                        Data = task
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.ResumeFailed", taskId), ex);
                throw new TaktException(L("Quartz.ResumeFailed", taskId));
            }
        }

        /// <summary>
        /// 立即执行一次定时任务
        /// </summary>
        public async Task<bool> TriggerAsync(long taskId)
        {
            var task = await TaskRepository.GetByIdAsync(taskId);
            if (task == null)
                throw new TaktException(L("Quartz.NotFound", taskId));

            try
            {
                var result = await TaktQuartzHelper.TriggerJobAsync(task.QuartzName, task.QuartzGroupName);
                
                if (result)
                {
                    // 发送实时通知
                    await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = L("Quartz.Triggered"),
                        Content = L("Quartz.TriggeredContent", task.QuartzName),
                        Timestamp = DateTime.Now,
                        Data = task
                    });
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.TriggerFailed", taskId), ex);
                throw new TaktException(L("Quartz.TriggerFailed", taskId));
            }
        }

        /// <summary>
        /// 导出定时任务数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktQuartzQueryDto query, string sheetName = "Quartz")
        {
            try
            {
                var list = await TaskRepository.GetListAsync(KpQuartzQueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktQuartzExportDto>>(), sheetName, L("Quartz.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Quartz.ExportFailed"), ex);
                throw new TaktException(L("Quartz.ExportFailed"));
            }
        }

        private Expression<Func<TaktQuartz, bool>> KpQuartzQueryExpression(TaktQuartzQueryDto query)
        {
            return Expressionable.Create<TaktQuartz>()
                .AndIF(!string.IsNullOrEmpty(query.QuartzName), x => x.QuartzName.Contains(query.QuartzName))
                .AndIF(!string.IsNullOrEmpty(query.QuartzGroupName), x => x.QuartzGroupName == query.QuartzGroupName)
                .AndIF(query.QuartzType.HasValue, x => x.QuartzType == query.QuartzType.Value)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .AndIF(!string.IsNullOrEmpty(query.CreateBy), x => x.CreateBy == query.CreateBy)
                .ToExpression();
        }

        /// <summary>
        /// 获取指定用户的定时任务状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy)
        {
            if (string.IsNullOrEmpty(createBy))
                throw new ArgumentNullException(nameof(createBy));

            var stats = await TaskRepository.AsQueryable()
                .Where(x => x.CreateBy == createBy)
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }
    }
}




