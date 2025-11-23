//===================================================================
// 项目名 : Takt.Shared.Helpers
// 文件名 : TaktQuartzHelper.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 定时任务帮助类
//===================================================================

using Takt.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Takt.Shared.Helpers
{
    /// <summary>
    /// 定时任务帮助类
    /// </summary>
    public static class TaktQuartzHelper
    {
        private static IScheduler? _scheduler;
        private static readonly object _lock = new();

        /// <summary>
        /// 初始化调度器
        /// </summary>
        /// <param name="option">Quartz配置选项</param>
        public static async Task InitializeAsync(TaktQuartzOption option)
        {
            if (_scheduler != null) return;

            lock (_lock)
            {
                if (_scheduler != null) return;

                var properties = new NameValueCollection
                {
                    ["quartz.scheduler.instanceName"] = option.InstanceName,
                    ["quartz.serializer.type"] = option.SerializerType,
                    ["quartz.threadPool.maxConcurrency"] = option.ThreadPool.MaxConcurrency.ToString(),
                    ["quartz.threadPool.threadPriority"] = option.ThreadPool.ThreadPriority.ToString()
                };

                if (option.UseDatabase)
                {
                    properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
                    properties["quartz.jobStore.driverDelegateType"] = GetDriverDelegateType(option.DbType);
                    properties["quartz.jobStore.tablePrefix"] = option.TablePrefix;
                    properties["quartz.jobStore.useProperties"] = "true";
                }

                if (option.Cluster.Enabled)
                {
                    properties["quartz.jobStore.clustered"] = "true";
                    properties["quartz.scheduler.instanceId"] = "AUTO";
                    properties["quartz.jobStore.clusterCheckinInterval"] = option.Cluster.CheckinInterval.ToString();
                    properties["quartz.jobStore.misfireThreshold"] = option.Cluster.CheckinMisfireThreshold.ToString();
                }

                var factory = new StdSchedulerFactory(properties);
                _scheduler = factory.GetScheduler().Result;
            }

            if (option.Enabled)
            {
                await _scheduler.Start();
            }
        }

        /// <summary>
        /// 添加或更新作业
        /// </summary>
        /// <typeparam name="T">作业类型</typeparam>
        /// <param name="jobName">作业名称</param>
        /// <param name="groupName">组名称</param>
        /// <param name="cronExpression">Cron表达式</param>
        /// <param name="description">描述</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> AddOrUpdateJobAsync<T>(string jobName, string groupName, string cronExpression, string? description = null) where T : IJob
        {
            if (_scheduler == null)
                throw new InvalidOperationException("调度器未初始化，请先调用 InitializeAsync 方法");

            try
            {
                var job = JobBuilder.Create<T>()
                    .WithIdentity(jobName, groupName)
                    .WithDescription(description)
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity($"{jobName}_trigger", groupName)
                    .WithCronSchedule(cronExpression)
                    .WithDescription(description)
                    .Build();

                if (await _scheduler.CheckExists(job.Key))
                {
                    await _scheduler.DeleteJob(job.Key);
                }

                await _scheduler.ScheduleJob(job, trigger);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"添加或更新作业时发生错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 暂停作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <param name="groupName">组名称</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> PauseJobAsync(string jobName, string groupName)
        {
            if (_scheduler == null)
                throw new InvalidOperationException("调度器未初始化，请先调用 InitializeAsync 方法");

            try
            {
                var jobKey = new JobKey(jobName, groupName);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.PauseJob(jobKey);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"暂停作业时发生错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 恢复作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <param name="groupName">组名称</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> ResumeJobAsync(string jobName, string groupName)
        {
            if (_scheduler == null)
                throw new InvalidOperationException("调度器未初始化，请先调用 InitializeAsync 方法");

            try
            {
                var jobKey = new JobKey(jobName, groupName);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.ResumeJob(jobKey);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"恢复作业时发生错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <param name="groupName">组名称</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> DeleteJobAsync(string jobName, string groupName)
        {
            if (_scheduler == null)
                throw new InvalidOperationException("调度器未初始化，请先调用 InitializeAsync 方法");

            try
            {
                var jobKey = new JobKey(jobName, groupName);
                if (await _scheduler.CheckExists(jobKey))
                {
                    return await _scheduler.DeleteJob(jobKey);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除作业时发生错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 立即执行作业一次
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <param name="groupName">组名称</param>
        /// <returns>是否成功</returns>
        public static async Task<bool> TriggerJobAsync(string jobName, string groupName)
        {
            if (_scheduler == null)
                throw new InvalidOperationException("调度器未初始化，请先调用 InitializeAsync 方法");

            try
            {
                var jobKey = new JobKey(jobName, groupName);
                if (await _scheduler.CheckExists(jobKey))
                {
                    await _scheduler.TriggerJob(jobKey);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"触发作业时发生错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取数据库驱动代理类型
        /// </summary>
        private static string GetDriverDelegateType(string dbType)
        {
            return dbType.ToLower() switch
            {
                "sqlserver" => "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
                "mysql" => "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz",
                "postgresql" => "Quartz.Impl.AdoJobStore.PostgreSQLDelegate, Quartz",
                "oracle" => "Quartz.Impl.AdoJobStore.OracleDelegate, Quartz",
                "sqlite" => "Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz",
                _ => throw new ArgumentException($"不支持的数据库类型: {dbType}")
            };
        }
    }
} 



