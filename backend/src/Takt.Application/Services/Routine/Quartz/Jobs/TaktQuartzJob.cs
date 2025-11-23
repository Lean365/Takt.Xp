//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktQuartzJob.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 定时任务执行器
//===================================================================

using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Takt.Application.Services.Routine.Quartz.Jobs
{
    /// <summary>
    /// 定时任务执行器
    /// </summary>
    [DisallowConcurrentExecution]
    public class TaktQuartzJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        private readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzJob(
            IServiceProvider serviceProvider,
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpClientFactory httpClientFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _httpClient = httpClientFactory.CreateClient("QuartzJob");
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public async Task Execute(IJobExecutionContext context)
        {
            var taskId = context.JobDetail.JobDataMap.GetLong("taskId");
            if (taskId <= 0)
            {
                _logger.Error("任务ID为空，无法执行任务");
                return;
            }

            var taskRepository = _repositoryFactory.GetBusinessRepository<TaktQuartz>();
            var task = await taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                _logger.Error($"未找到ID为{taskId}的任务");
                return;
            }

            var startTime = DateTime.Now;
            var success = false;
            var message = string.Empty;

            try
            {
                switch (task.QuartzType)
                {
                    case 1: // 程序集
                        await ExecuteAssemblyAsync(task);
                        break;

                    case 2: // 网络请求
                        await ExecuteHttpRequestAsync(task);
                        break;

                    case 3: // SQL语句
                        await ExecuteSqlAsync(task);
                        break;

                    default:
                        throw new TaktException($"不支持的任务类型: {task.QuartzType}");
                }

                success = true;
                message = "执行成功";
            }
            catch (Exception ex)
            {
                success = false;
                message = $"执行失败: {ex.Message}";
                _logger.Error($"执行任务[{task.QuartzName}]失败", ex.Message);
            }

            var endTime = DateTime.Now;
            var elapsed = (int)(endTime - startTime).TotalMilliseconds;

            // 更新任务信息
            task.QuartzLastRunTime = startTime;
            task.QuartzExecuteCount++;
            await taskRepository.UpdateAsync(task);

            // 记录执行日志
            var log = new TaktQuartzLog
            {
                QuartzId = task.Id,
                QuartzName = task.QuartzName,
                QuartzGroupName = task.QuartzGroupName,
                QuartzType = task.QuartzType,
                QuartzExecuteTime = startTime,
                QuartzExecuteDuration = elapsed,
                Status = success ? 1 : 0,
                QuartzExecuteMessage = message,
                QuartzErrorInfo = success ? null : message
            };

            var logRepository = _repositoryFactory.GetBusinessRepository<TaktQuartzLog>();
            await logRepository.CreateAsync(log);
        }

        /// <summary>
        /// 执行程序集
        /// </summary>
        private async Task ExecuteAssemblyAsync(TaktQuartz task)
        {
            if (string.IsNullOrEmpty(task.QuartzAssemblyName) || string.IsNullOrEmpty(task.QuartzClassName))
                throw new TaktException("程序集名称或类名为空");

            try
            {
                var assembly = System.Reflection.Assembly.Load(task.QuartzAssemblyName);
                var type = assembly.GetType(task.QuartzClassName);
                if (type == null)
                    throw new TaktException($"未找到类型: {task.QuartzClassName}");

                var instance = ActivatorUtilities.CreateInstance(_serviceProvider, type);
                var method = type.GetMethod("Execute");
                if (method == null)
                    throw new TaktException($"未找到Execute方法");

                if (method.ReturnType == typeof(Task))
                {
                    await (Task)method.Invoke(instance, new object[] { task.QuartzExecuteParams });
                }
                else
                {
                    method.Invoke(instance, new object[] { task.QuartzExecuteParams });
                }
            }
            catch (Exception ex)
            {
                throw new TaktException($"执行程序集失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 执行HTTP请求
        /// </summary>
        private async Task ExecuteHttpRequestAsync(TaktQuartz task)
        {
            if (string.IsNullOrEmpty(task.QuartzApiUrl))
                throw new TaktException("API地址为空");

            try
            {
                var method = new HttpMethod(task.QuartzRequestMethod ?? "GET");
                var request = new HttpRequestMessage(method, task.QuartzApiUrl);

                if (!string.IsNullOrEmpty(task.QuartzExecuteParams))
                {
                    if (method == HttpMethod.Get)
                    {
                        // 对于GET请求，参数应该已经包含在URL中
                    }
                    else
                    {
                        request.Content = new StringContent(task.QuartzExecuteParams, System.Text.Encoding.UTF8, "application/json");
                    }
                }

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new TaktException($"HTTP请求失败: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {
                throw new TaktException($"执行HTTP请求失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        private async Task ExecuteSqlAsync(TaktQuartz task)
        {
            if (string.IsNullOrEmpty(task.QuartzSql))
                throw new TaktException("SQL语句为空");

            try
            {
                var taskRepository = _repositoryFactory.GetBusinessRepository<TaktQuartz>();
                var result = await taskRepository.SqlSugarClient.Ado.ExecuteCommandAsync(task.QuartzSql);
                if (result <= 0)
                {
                    throw new TaktException("SQL执行未影响任何行");
                }
            }
            catch (Exception ex)
            {
                throw new TaktException($"执行SQL失败: {ex.Message}", ex);
            }
        }
    }
}



