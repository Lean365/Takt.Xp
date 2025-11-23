#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTimeoutReminderJob.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流超时提醒任务
//===================================================================

using Takt.Application.Services.Workflow.Engine;
using Takt.Shared.Options;
using Microsoft.Extensions.Options;
using Quartz;
using Newtonsoft.Json;


namespace Takt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流超时提醒任务
    /// </summary>
    [DisallowConcurrentExecution]
    public class TaktTimeoutReminderJob : IJob
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
        /// 工作流引擎
        /// </summary>
        private readonly ITaktWorkflowEngine _workflowEngine;

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
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowEngine">工作流引擎</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        /// <exception cref="ArgumentNullException">当repositoryFactory为null时抛出</exception>
        public TaktTimeoutReminderJob(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            ITaktWorkflowEngine workflowEngine,
            IOptions<TaktWorkflowOptions> workflowOptions)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
            _workflowEngine = workflowEngine;
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
                _logger.Info("开始执行工作流超时提醒任务");

                if (!_workflowOptions.TimeoutReminder.Enabled)
                {
                    _logger.Info("超时提醒功能已禁用");
                    return;
                }

                await CheckTimeoutInstancesAsync();

                _logger.Info("工作流超时提醒任务执行完成");
            }
            catch (Exception ex)
            {
                _logger.Error("工作流超时提醒任务执行失败", ex);
                throw; // 重新抛出异常，让Quartz处理
            }
        }

        /// <summary>
        /// 检查超时的工作流实例
        /// </summary>
        /// <returns>异步任务</returns>
        private async Task CheckTimeoutInstancesAsync()
        {
            try
            {
                // 获取运行中且可能超时的工作流实例
                var timeoutInstances = await InstanceRepository.GetListAsync(
                    i => i.Status == 1 && // 1 表示运行中
                         i.StartTime.HasValue &&
                         i.StartTime.Value.AddHours(_workflowOptions.TimeoutReminder.ReminderHours) < DateTime.Now); // 超过配置的提醒小时数

                foreach (var instance in timeoutInstances)
                {
                    await ProcessTimeoutInstanceAsync(instance);
                }

                if (timeoutInstances.Any())
                {
                    _logger.Info($"检查到 {timeoutInstances.Count} 个可能超时的工作流实例");
                }
                else
                {
                    _logger.Info("没有超时的工作流实例");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("检查超时工作流实例失败", ex);
            }
        }

        /// <summary>
        /// 处理超时的工作流实例
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <returns>异步任务</returns>
        private async Task ProcessTimeoutInstanceAsync(TaktInstance instance)
        {
            try
            {
                // 检查是否真的超时
                var timeoutHours = _workflowOptions.TimeoutReminder.ReminderHours;
                if (instance.StartTime?.AddHours(timeoutHours) > DateTime.Now)
                {
                    return; // 未超时
                }

                // 检查是否超过最大执行天数
                var maxExecutionDays = _workflowOptions.MaxExecutionDays;
                if (instance.StartTime?.AddDays(maxExecutionDays) <= DateTime.Now)
                {
                    // 超过最大执行天数，自动停止
                    await AutoStopWorkflowAsync(instance);
                }
                else
                {
                    // 未超过最大执行天数，只发送提醒
                    await CreateTimeoutReminderAsync(instance);
                    await SendTimeoutNotificationAsync(instance);
                }

                _logger.Info($"工作流实例[{instance.Id}]超时处理完成");
            }
            catch (Exception ex)
            {
                _logger.Error($"处理超时工作流实例[{instance.Id}]失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 自动停止工作流
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <returns>异步任务</returns>
        private async Task AutoStopWorkflowAsync(TaktInstance instance)
        {
            try
            {
                _logger.Warn($"工作流实例[{instance.Id}]超过最大执行天数[{_workflowOptions.MaxExecutionDays}]天，自动停止");

                // 调用工作流引擎终止实例
                await _workflowEngine.TerminateAsync(instance.Id, $"超过最大执行天数{_workflowOptions.MaxExecutionDays}天，系统自动停止");

                // 记录自动停止操作
                var stopRecord = new TaktInstanceOper
                {
                    InstanceId = instance.Id,
                    NodeId = instance.CurrentNodeId ?? "",
                    NodeName = instance.CurrentNodeName ?? "",
                    OperType = 10, // 10 表示自动停止
                    OperatorId = 0, // 系统操作
                    OperatorName = "System",
                    OperOpinion = $"超过最大执行天数{_workflowOptions.MaxExecutionDays}天，系统自动停止",
                    OperData = JsonConvert.SerializeObject(new
                    {
                        MaxExecutionDays = _workflowOptions.MaxExecutionDays,
                        StopTime = DateTime.Now,
                        InstanceStartTime = instance.StartTime,
                        ExecutionDays = (DateTime.Now - instance.StartTime)?.TotalDays
                    })
                };

                await InstanceOperRepository.CreateAsync(stopRecord);

                _logger.Info($"工作流实例[{instance.Id}]自动停止成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"自动停止工作流实例[{instance.Id}]失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建超时提醒记录
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <returns>异步任务</returns>
        private async Task CreateTimeoutReminderAsync(TaktInstance instance)
        {
            try
            {
                var reminderRecord = new TaktInstanceOper
                {
                    InstanceId = instance.Id,
                    NodeId = instance.CurrentNodeId ?? "",
                    NodeName = instance.CurrentNodeName ?? "",
                    OperType = 9, // 9 表示超时提醒
                    OperatorId = 0, // 系统操作
                    OperatorName = "System",
                    OperOpinion = $"工作流实例已运行{_workflowOptions.TimeoutReminder.ReminderHours}小时，请注意及时处理",
                    OperData = JsonConvert.SerializeObject(new
                    {
                        ReminderHours = _workflowOptions.TimeoutReminder.ReminderHours,
                        ReminderTime = DateTime.Now,
                        InstanceStartTime = instance.StartTime,
                        ExecutionHours = (DateTime.Now - instance.StartTime)?.TotalHours
                    })
                };

                await InstanceOperRepository.CreateAsync(reminderRecord);
                _logger.Info($"工作流实例[{instance.Id}]超时提醒记录创建成功");
            }
            catch (Exception ex)
            {
                _logger.Error($"创建工作流实例[{instance.Id}]超时提醒记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 发送超时通知
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <returns>异步任务</returns>
        private async Task SendTimeoutNotificationAsync(TaktInstance instance)
        {
            try
            {
                // 这里可以集成消息通知系统，如邮件、短信、钉钉等
                // 暂时只记录日志
                _logger.Warn($"工作流实例[{instance.Id}]超时提醒：实例标题[{instance.InstanceTitle}]，当前节点[{instance.CurrentNodeName}]，已运行{_workflowOptions.TimeoutReminder.ReminderHours}小时");

                // TODO: 实现具体的通知逻辑
                // 例如：发送邮件给相关人员、发送钉钉消息等
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"发送工作流实例[{instance.Id}]超时通知失败: {ex.Message}", ex);
            }
        }
    }
}


