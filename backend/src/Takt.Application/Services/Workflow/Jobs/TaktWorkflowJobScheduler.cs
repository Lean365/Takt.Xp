#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowJobScheduler.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流Jobs调度器配置
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.IServices;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl.Matchers;

namespace Takt.Application.Services.Workflow.Jobs
{
    /// <summary>
    /// 工作流Jobs调度器配置
    /// </summary>
    public static class TaktWorkflowJobScheduler
    {
        /// <summary>
        /// 配置工作流Jobs
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        /// <returns>异步任务</returns>
        public static async Task ConfigureWorkflowJobsAsync(IScheduler scheduler, ITaktLogger logger, TaktWorkflowOptions workflowOptions)
        {
            try
            {
                // 配置自动执行任务
                if (workflowOptions.AutoExecution.Enabled)
                {
                    await ConfigureAutoExecutionJobAsync(scheduler, logger, workflowOptions);
                }

                // 配置超时提醒任务
                if (workflowOptions.TimeoutReminder.Enabled)
                {
                    await ConfigureTimeoutReminderJobAsync(scheduler, logger, workflowOptions);
                }

                // 配置清理任务
                if (workflowOptions.Cleanup.Enabled)
                {
                    await ConfigureCleanupJobAsync(scheduler, logger, workflowOptions);
                }

                logger.Info("工作流Jobs配置完成");
            }
            catch (Exception ex)
            {
                logger.Error("配置工作流Jobs失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 配置自动执行任务
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        /// <returns>异步任务</returns>
        private static async Task ConfigureAutoExecutionJobAsync(IScheduler scheduler, ITaktLogger logger, TaktWorkflowOptions workflowOptions)
        {
            var jobKey = new JobKey("WorkflowAutoExecutionJob", "WorkflowJobs");
            var triggerKey = new TriggerKey("WorkflowAutoExecutionTrigger", "WorkflowJobs");

            // 创建Job
            var job = JobBuilder.Create<TaktAutoExecutionJob>()
                .WithIdentity(jobKey)
                .Build();

            // 创建触发器 - 根据配置的间隔时间执行
            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(workflowOptions.AutoExecution.IntervalMinutes)
                    .RepeatForever())
                .Build();

            // 调度Job
            await scheduler.ScheduleJob(job, trigger);
            logger.Info($"自动执行任务已配置，每{workflowOptions.AutoExecution.IntervalMinutes}分钟执行一次");
        }

        /// <summary>
        /// 配置超时提醒任务
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        /// <returns>异步任务</returns>
        private static async Task ConfigureTimeoutReminderJobAsync(IScheduler scheduler, ITaktLogger logger, TaktWorkflowOptions workflowOptions)
        {
            var jobKey = new JobKey("WorkflowTimeoutReminderJob", "WorkflowJobs");
            var triggerKey = new TriggerKey("WorkflowTimeoutReminderTrigger", "WorkflowJobs");

            // 创建Job
            var job = JobBuilder.Create<TaktTimeoutReminderJob>()
                .WithIdentity(jobKey)
                .Build();

            // 创建触发器 - 根据配置的间隔时间执行
            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(workflowOptions.TimeoutReminder.IntervalMinutes)
                    .RepeatForever())
                .Build();

            // 调度Job
            await scheduler.ScheduleJob(job, trigger);
            logger.Info($"超时提醒任务已配置，每{workflowOptions.TimeoutReminder.IntervalMinutes}分钟执行一次，最大执行天数：{workflowOptions.MaxExecutionDays}天");
        }

        /// <summary>
        /// 配置清理任务
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <param name="workflowOptions">工作流配置选项</param>
        /// <returns>异步任务</returns>
        private static async Task ConfigureCleanupJobAsync(IScheduler scheduler, ITaktLogger logger, TaktWorkflowOptions workflowOptions)
        {
            var jobKey = new JobKey("WorkflowCleanupJob", "WorkflowJobs");
            var triggerKey = new TriggerKey("WorkflowCleanupTrigger", "WorkflowJobs");

            // 创建Job
            var job = JobBuilder.Create<TaktCleanupJob>()
                .WithIdentity(jobKey)
                .Build();

            // 创建触发器 - 根据配置的Cron表达式执行
            var trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithCronSchedule(workflowOptions.Cleanup.CronExpression)
                .Build();

            // 调度Job
            await scheduler.ScheduleJob(job, trigger);
            logger.Info($"清理任务已配置，Cron表达式：{workflowOptions.Cleanup.CronExpression}，保留天数：{workflowOptions.Cleanup.RetentionDays}天");
        }

        /// <summary>
        /// 暂停所有工作流Jobs
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <returns>异步任务</returns>
        public static async Task PauseAllWorkflowJobsAsync(IScheduler scheduler, ITaktLogger logger)
        {
            try
            {
                var jobGroup = "WorkflowJobs";
                var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(jobGroup));
                
                foreach (var jobKey in jobKeys)
                {
                    await scheduler.PauseJob(jobKey);
                }
                
                logger.Info("所有工作流Jobs已暂停");
            }
            catch (Exception ex)
            {
                logger.Error("暂停工作流Jobs失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 恢复所有工作流Jobs
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <returns>异步任务</returns>
        public static async Task ResumeAllWorkflowJobsAsync(IScheduler scheduler, ITaktLogger logger)
        {
            try
            {
                var jobGroup = "WorkflowJobs";
                var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(jobGroup));
                
                foreach (var jobKey in jobKeys)
                {
                    await scheduler.ResumeJob(jobKey);
                }
                
                logger.Info("所有工作流Jobs已恢复");
            }
            catch (Exception ex)
            {
                logger.Error("恢复工作流Jobs失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取工作流Jobs状态
        /// </summary>
        /// <param name="scheduler">Quartz调度器</param>
        /// <param name="logger">日志服务</param>
        /// <returns>Jobs状态信息字典</returns>
        public static async Task<Dictionary<string, object>> GetWorkflowJobsStatusAsync(IScheduler scheduler, ITaktLogger logger)
        {
            try
            {
                var jobGroup = "WorkflowJobs";
                var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(jobGroup));
                var statusInfo = new Dictionary<string, object>();

                foreach (var jobKey in jobKeys)
                {
                    var jobDetail = await scheduler.GetJobDetail(jobKey);
                    var triggers = await scheduler.GetTriggersOfJob(jobKey);
                    
                    var triggerList = new List<object>();
                    foreach (var trigger in triggers)
                    {
                        var triggerState = await scheduler.GetTriggerState(trigger.Key);
                        triggerList.Add(new
                        {
                            TriggerName = trigger.Key.Name,
                            TriggerGroup = trigger.Key.Group,
                            NextFireTime = trigger.GetNextFireTimeUtc()?.LocalDateTime,
                            PreviousFireTime = trigger.GetPreviousFireTimeUtc()?.LocalDateTime,
                            State = triggerState
                        });
                    }

                    var jobStatus = new
                    {
                        JobName = jobKey.Name,
                        JobGroup = jobKey.Group,
                        Description = jobDetail?.Description,
                        Triggers = triggerList
                    };

                    statusInfo[jobKey.Name] = jobStatus;
                }

                return statusInfo;
            }
            catch (Exception ex)
            {
                logger.Error("获取工作流Jobs状态失败", ex);
                throw;
            }
        }
    }
} 


