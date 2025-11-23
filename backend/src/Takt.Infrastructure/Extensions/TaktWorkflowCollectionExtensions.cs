//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServiceCollectionExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-23 12:00
// 版本号 : V0.0.1
// 描述    : 工作流服务扩展方法
//===================================================================

using Takt.Application.Services.Workflow;
using Takt.Application.Services.Workflow.Engine;
using Takt.Application.Services.Workflow.Engine.Cache;
using Takt.Application.Services.Workflow.Engine.Expressions;
using Takt.Application.Services.Workflow.Engine.Resolvers;
using Takt.Application.Services.Workflow.Jobs;
using Takt.Shared.Options;
using Microsoft.Extensions.Options;
using Quartz;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 工作流服务扩展方法
    /// </summary>
    public static class TaktCollectionExtensions
    {
        /// <summary>
        /// 添加工作流缓存服务
        /// </summary>
        /// <remarks>
        /// 注册工作流相关的缓存服务，包括：
        /// 1. 内存缓存服务
        /// 2. 工作流缓存服务
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        private static IServiceCollection AddWorkflowCacheServices(this IServiceCollection services)
        {
            // 注册工作流缓存服务
            services.AddScoped<ITaktWorkflowCache, TaktWorkflowMemoryCache>();
            return services;
        }

        /// <summary>
        /// 添加工作流配置选项
        /// </summary>
        /// <remarks>
        /// 注册工作流相关的配置选项，包括：
        /// 1. 工作流配置选项
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        private static IServiceCollection AddWorkflowOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // 注册工作流配置选项
            services.Configure<TaktWorkflowOptions>(configuration.GetSection("Workflow"));
            return services;
        }

        /// <summary>
        /// 添加工作流Quartz作业
        /// </summary>
        /// <remarks>
        /// 注册工作流相关的Quartz作业，包括：
        /// 1. 自动执行作业
        /// 2. 超时提醒作业
        /// 3. 清理任务作业
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        private static IServiceCollection AddWorkflowJobs(this IServiceCollection services)
        {
            // 注册工作流Quartz作业
            services.AddScoped<TaktAutoExecutionJob>();
            services.AddScoped<TaktTimeoutReminderJob>();
            services.AddScoped<TaktCleanupJob>();
            return services;
        }

        /// <summary>
        /// 添加工作流服务
        /// </summary>
        /// <remarks>
        /// 注册工作流相关的所有服务,包括:
        /// 1. 工作流引擎
        /// 2. 工作流定义服务
        /// 3. 工作流实例服务
        /// 4. 工作流表单服务
        /// 5. 工作流实例操作服务
        /// 6. 工作流实例流转服务
        /// 7. 表达式引擎
        /// 8. 审批人解析器
        /// 9. 缓存服务
        /// 10. Quartz作业
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddWorkflowServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 添加工作流配置选项
            services.AddWorkflowOptions(configuration);

            // 添加工作流缓存服务
            services.AddWorkflowCacheServices();

            // 添加工作流Quartz作业
            services.AddWorkflowJobs();

            // 注册工作流引擎,负责工作流的整体调度和执行
            services.AddScoped<ITaktWorkflowEngine, TaktWorkflowEngine>();

            // 注册工作流核心服务
            services.AddScoped<ITaktSchemeService, TaktSchemeService>();         // 工作流定义管理
            services.AddScoped<ITaktInstanceService, TaktInstanceService>();     // 工作流实例管理
            services.AddScoped<ITaktFormService, TaktFormService>();            // 工作流表单管理
            services.AddScoped<ITaktInstanceOperService, TaktInstanceOperService>(); // 工作流实例操作管理
            services.AddScoped<ITaktInstanceTransService, TaktInstanceTransService>(); // 工作流实例流转管理

            // 注册工作流表达式引擎,用于解析和执行条件表达式
            services.AddScoped<ITaktExpressionEngine, TaktExpressionEngine>();

            // 注册工作流审批人解析器,用于动态解析审批人
            services.AddScoped<ITaktApproverResolver, TaktApproverResolver>();

            return services;
        }

        /// <summary>
        /// 配置工作流Quartz调度器
        /// </summary>
        /// <remarks>
        /// 配置工作流相关的Quartz调度器，包括：
        /// 1. 自动执行作业调度
        /// 2. 超时提醒作业调度
        /// 3. 清理任务作业调度
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddWorkflowScheduler(this IServiceCollection services, IConfiguration configuration)
        {
            // 获取工作流配置
            var workflowOptions = configuration.GetSection("Workflow").Get<TaktWorkflowOptions>();

            // 配置Quartz调度器
            services.AddQuartz(q =>
            {
                // 配置自动执行作业
                if (workflowOptions?.AutoExecution?.Enabled == true)
                {
                    var autoExecutionJobKey = new JobKey("TaktAutoExecutionJob");
                    q.AddJob<TaktAutoExecutionJob>(opts => opts.WithIdentity(autoExecutionJobKey));

                    q.AddTrigger(opts => opts
                        .ForJob(autoExecutionJobKey)
                        .WithIdentity("TaktAutoExecutionJobTrigger")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInMinutes(workflowOptions.AutoExecution.IntervalMinutes)
                            .RepeatForever()));
                }

                // 配置超时提醒作业
                if (workflowOptions?.TimeoutReminder?.Enabled == true)
                {
                    var timeoutReminderJobKey = new JobKey("TaktTimeoutReminderJob");
                    q.AddJob<TaktTimeoutReminderJob>(opts => opts.WithIdentity(timeoutReminderJobKey));

                    q.AddTrigger(opts => opts
                        .ForJob(timeoutReminderJobKey)
                        .WithIdentity("TaktTimeoutReminderJobTrigger")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInMinutes(workflowOptions.TimeoutReminder.IntervalMinutes)
                            .RepeatForever()));
                }

                // 配置清理任务作业
                if (workflowOptions?.Cleanup?.Enabled == true)
                {
                    var cleanupJobKey = new JobKey("TaktCleanupJob");
                    q.AddJob<TaktCleanupJob>(opts => opts.WithIdentity(cleanupJobKey));

                    if (!string.IsNullOrEmpty(workflowOptions.Cleanup.CronExpression))
                    {
                        q.AddTrigger(opts => opts
                            .ForJob(cleanupJobKey)
                            .WithIdentity("TaktCleanupJobTrigger")
                            .WithCronSchedule(workflowOptions.Cleanup.CronExpression));
                    }
                    else
                    {
                        q.AddTrigger(opts => opts
                            .ForJob(cleanupJobKey)
                            .WithIdentity("TaktCleanupJobTrigger")
                            .WithSimpleSchedule(x => x
                                .WithIntervalInHours(24) // 默认每天执行一次
                                .RepeatForever()));
                    }
                }
            });

            // 添加Quartz托管服务
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}




