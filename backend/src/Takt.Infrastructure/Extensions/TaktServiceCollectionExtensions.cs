//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServiceCollectionExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 17:30
// 版本号 : V.0.0.1
// 描述    : 服务集合扩展
//===================================================================

using Takt.Application.Services.Logging;
using Takt.Application.Services.Extensions;
using Takt.Application.Services.Routine.SignalR;
using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.IServices.Caching;
using Takt.Infrastructure.Caching;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Extensions;
using Takt.Infrastructure.Logging;
using Takt.Infrastructure.Security;
using Takt.Infrastructure.Security.Filters;
using Takt.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

// 添加代码生成相关服务的命名空间

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 服务集合扩展类
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册系统所需的所有服务，包括：
    /// 1. 领域层服务 - 包含核心业务逻辑
    /// 2. 应用层服务 - 处理用户交互和业务流程
    /// 3. 基础设施服务 - 提供技术支持和底层实现
    /// 4. 工作流服务 - 处理业务流程自动化
    /// </remarks>
    public static class TaktServiceCollectionExtensions
    {


        /// <summary>
        /// 添加领域层服务
        /// </summary>
        /// <remarks>
        /// 注册领域层相关的所有服务，包括：
        /// 1. 仓储服务 - 数据访问抽象
        /// 2. 安全服务 - 登录和密码策略
        /// 3. 会话服务 - 用户会话管理
        /// 4. 验证服务 - 验证码认证
        /// 5. 日志服务 - 系统日志管理
        /// 6. 本地化服务 - 多语言支持
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            // 添加仓储工厂 - 支持多库模式
            services.AddScoped<ITaktRepositoryFactory, TaktRepositoryFactory>();
            // 注意：TaktRepository<T> 不直接注册，而是通过 TaktRepositoryFactory 来创建
            // 因为每个仓储实例需要特定的 SqlSugarScope（来自不同的数据库上下文）

            // 添加缓存服务
            services.AddCacheServices();        // 缓存服务
            services.AddLogServices();          // 日志服务
            services.AddTaktLocalization();     // 本地化服务

            // 注册权限日志记录器
            services.AddScoped<TaktPermissionLogger>();

            // 添加服务器监控服务
            services.AddScoped<ITaktServerMonitorService, TaktServerMonitorService>();

            return services;
        }

        /// <summary>
        /// 添加应用层服务
        /// </summary>
        /// <remarks>
        /// 注册应用层相关的所有服务，包括：
        /// 1. 身份认证服务 - 用户认证和授权
        /// 2. 审计日志服务 - 系统操作记录
        /// 3. 系统管理服务 - 系统配置和维护
        /// 4. 实时通信服务 - 即时消息和通知
        /// 5. 工作流服务 - 业务流程管理
        /// 6. 会计服务 - 财务会计和管理会计
        /// 7. 人力资源服务 - 人员管理和组织架构
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <param name="webHostEnvironment">Web主机环境</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddIdentityServices();    // 身份认证服务

            services.AddWorkflowServices(configuration);    // 工作流服务
            services.AddRoutineServices(configuration);     // 常规业务服务
            services.AddLogisticsServices();  // 物流服务
            services.AddAccountingServices(); // 会计服务
            services.AddHumanResourceServices();      // 人力资源服务
            services.AddGeneratorServices(configuration, webHostEnvironment);   // 代码生成服务

            return services;
        }



        /// <summary>
        /// 添加缓存服务
        /// </summary>
        private static IServiceCollection AddCacheServices(this IServiceCollection services)
        {
            // 内存缓存
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            // 缓存服务
            services.AddScoped<ITaktMemoryCache, TaktMemoryCache>();
            services.AddScoped<TaktCacheConfigManager>();

            return services;
        }

        /// <summary>
        /// 添加日志服务
        /// </summary>
        private static IServiceCollection AddLogServices(this IServiceCollection services)
        {
            // 添加日志记录器 - 只在这里注册一次
            services.AddSingleton<ITaktLogger, TaktNLogger>();
            services.AddScoped<ITaktLogCleanupService, TaktLogCleanupService>();
            services.AddScoped<ITaktLogArchiveService, TaktLogArchiveService>();

            // 注册日志管理器
            services.AddScoped<ITaktExceptionLogManager, TaktLogManager>();
            services.AddScoped<ITaktOperLogManager, TaktLogOperManager>();
            services.AddScoped<ITaktSqlDiffLogManager, TaktLogSqlDiffManager>();

            // 注册操作日志服务
            services.AddScoped<TaktOperLogService>();
            services.AddScoped<ITaktOperLogService, TaktOperLogService>();

            // 注册其他日志服务
            services.AddScoped<ITaktLoginLogService, TaktLoginLogService>();
            services.AddScoped<ITaktExceptionLogService, TaktExceptionLogService>();
            services.AddScoped<ITaktSqlDiffLogService, TaktSqlDiffLogService>();
            services.AddScoped<ITaktQuartzLogService, TaktQuartzLogService>();
            services.AddScoped<ITaktDeviceLogService, TaktDeviceLogService>(); // 设备日志服务

            // 配置控制器拦截
            services.AddControllers(options =>
            {
                options.Filters.Add<TaktLogActionFilter>();
            });

            return services;
        }

        /// <summary>
        /// 添加基础设施服务
        /// </summary>
        /// <remarks>
        /// 注册基础设施层相关的所有服务，包括：
        /// 1. 数据库上下文 - 数据访问
        /// 2. 认证服务 - JWT认证
        /// 3. 缓存服务 - Redis和内存缓存
        /// 4. 日志服务 - 系统日志记录
        /// 5. SignalR 服务 - 实时通信和在线用户管理
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // 添加 HttpClient 工厂
            services.AddHttpClient();

            // 添加 SignalR 服务 - 统一注册所有 SignalR 相关服务
            services.AddTaktSignalR(configuration);

            // 配置数据库
            var dbConfig = configuration.GetSection("Database").Get<TaktDbOptions>()
                ?? throw new ArgumentException("数据库配置节点不能为空");

            // 验证 Multi 配置
            if (!dbConfig.Multi)
            {
                throw new InvalidOperationException("Multi 配置不允许设置为 false，当前项目要求必须启用多数据库模式");
            }

            // 注册 TaktDbOptions - 绑定 Database 节点
            services.Configure<TaktDbOptions>(options =>
            {
                // 绑定 Database 节点
                configuration.GetSection("Database").Bind(options);
            });



            // 获取各个数据库的连接字符串
            var businessConnectionString = configuration.GetConnectionString("BusinessDB")
                ?? throw new ArgumentException("BusinessDB连接字符串不能为空");
            var identityConnectionString = configuration.GetConnectionString("IdentityDB")
                ?? throw new ArgumentException("IdentityDB连接字符串不能为空");
            var workflowConnectionString = configuration.GetConnectionString("WorkflowDB")
                ?? throw new ArgumentException("WorkflowDB连接字符串不能为空");
            var generatorConnectionString = configuration.GetConnectionString("GeneratorDB")
                ?? throw new ArgumentException("GeneratorDB连接字符串不能为空");

            // 记录遮罩后的连接字符串信息（用于调试，不暴露敏感信息）
            Console.WriteLine($"[数据库配置] 业务数据库连接: {TaktMaskUtils.MaskSensitiveInfo(businessConnectionString)}");
            Console.WriteLine($"[数据库配置] 身份数据库连接: {TaktMaskUtils.MaskSensitiveInfo(identityConnectionString)}");
            Console.WriteLine($"[数据库配置] 工作流数据库连接: {TaktMaskUtils.MaskSensitiveInfo(workflowConnectionString)}");
            Console.WriteLine($"[数据库配置] 代码生成数据库连接: {TaktMaskUtils.MaskSensitiveInfo(generatorConnectionString)}");

            // 按照SqlSugar官方文档的多库AOP设置方式，创建主数据库实例
            var mainDb = new SqlSugarScope(new List<ConnectionConfig>
            {
                new ConnectionConfig()
                {
                    ConfigId = "BusinessDB",
                    ConnectionString = businessConnectionString,
                    DbType = dbConfig.DbType,
                    IsAutoCloseConnection = dbConfig.IsAutoCloseConnection,
                    InitKeyType = InitKeyType.Attribute
                },
                new ConnectionConfig()
                {
                    ConfigId = "IdentityDB",
                    ConnectionString = identityConnectionString,
                    DbType = dbConfig.DbType,
                    IsAutoCloseConnection = dbConfig.IsAutoCloseConnection,
                    InitKeyType = InitKeyType.Attribute
                },
                new ConnectionConfig()
                {
                    ConfigId = "WorkflowDB",
                    ConnectionString = workflowConnectionString,
                    DbType = dbConfig.DbType,
                    IsAutoCloseConnection = dbConfig.IsAutoCloseConnection,
                    InitKeyType = InitKeyType.Attribute
                },
                new ConnectionConfig()
                {
                    ConfigId = "GeneratorDB",
                    ConnectionString = generatorConnectionString,
                    DbType = dbConfig.DbType,
                    IsAutoCloseConnection = dbConfig.IsAutoCloseConnection,
                    InitKeyType = InitKeyType.Attribute
                }
            },
            db =>
            {
                // 按照SqlSugar官方文档，在构造函数中配置AOP
                if (dbConfig.SqlDiffLog)
                {
                    // 按照官方文档，使用GetConnection而不是GetConnectionScope
                    // 为BusinessDB配置差异日志AOP
                    db.GetConnection("BusinessDB").Aop.OnDiffLogEvent = async (it) =>
                    {
                        await HandleDiffLogEventViaManager(it, "BusinessDB", services);
                    };

                    // 为IdentityDB配置差异日志AOP
                    db.GetConnection("IdentityDB").Aop.OnDiffLogEvent = async (it) =>
                    {
                        await HandleDiffLogEventViaManager(it, "IdentityDB", services);
                    };

                    // 为WorkflowDB配置差异日志AOP
                    db.GetConnection("WorkflowDB").Aop.OnDiffLogEvent = async (it) =>
                    {
                        await HandleDiffLogEventViaManager(it, "WorkflowDB", services);
                    };

                    // 为GeneratorDB配置差异日志AOP
                    db.GetConnection("GeneratorDB").Aop.OnDiffLogEvent = async (it) =>
                    {
                        await HandleDiffLogEventViaManager(it, "GeneratorDB", services);
                    };

                    Console.WriteLine("[数据库配置] 所有数据库AOP事件处理器配置完成");
                }
            });

            // 差异日志管理器已通过依赖注入注册，无需静态初始化

            // 注册主数据库实例为单例
            services.AddSingleton<SqlSugarScope>(mainDb);

            // 预先获取所有数据库连接，避免重复创建
            var businessConnection = mainDb.GetConnectionScope("BusinessDB");
            var identityConnection = mainDb.GetConnectionScope("IdentityDB");
            var workflowConnection = mainDb.GetConnectionScope("WorkflowDB");
            var generatorConnection = mainDb.GetConnectionScope("GeneratorDB");

            // 注册业务数据库上下文
            services.AddScoped<TaktBusinessDbContext>(sp => new TaktBusinessDbContext(
                businessConnection,
                sp.GetRequiredService<IOptions<TaktDbOptions>>(),
                sp.GetRequiredService<ITaktLogger>(),
                sp.GetRequiredService<ITaktCurrentUser>(),
                sp.GetRequiredService<IServiceProvider>(),
                sp.GetRequiredService<IConfiguration>()
            ));

            // 注册身份数据库上下文
            services.AddScoped<TaktIdentityDBContext>(sp => new TaktIdentityDBContext(
                identityConnection,
                sp.GetRequiredService<IOptions<TaktDbOptions>>(),
                sp.GetRequiredService<ITaktLogger>(),
                sp.GetRequiredService<ITaktCurrentUser>(),
                sp.GetRequiredService<IServiceProvider>(),
                sp.GetRequiredService<IConfiguration>()
            ));

            // 注册工作流数据库上下文
            services.AddScoped<TaktWorkflowDbContext>(sp => new TaktWorkflowDbContext(
                workflowConnection,
                sp.GetRequiredService<IOptions<TaktDbOptions>>(),
                sp.GetRequiredService<ITaktLogger>(),
                sp.GetRequiredService<ITaktCurrentUser>(),
                sp.GetRequiredService<IServiceProvider>(),
                sp.GetRequiredService<IConfiguration>()
            ));

            // 注册代码生成数据库上下文
            services.AddScoped<TaktGeneratorDbContext>(sp => new TaktGeneratorDbContext(
                generatorConnection,
                sp.GetRequiredService<IOptions<TaktDbOptions>>(),
                sp.GetRequiredService<ITaktLogger>(),
                sp.GetRequiredService<ITaktCurrentUser>(),
                sp.GetRequiredService<IServiceProvider>(),
                sp.GetRequiredService<IConfiguration>()
            ));

            // 为接口提供默认实现
            services.AddScoped<ITaktDbContext>(sp => sp.GetRequiredService<TaktBusinessDbContext>());

            // 差异日志管理器已在AddLogServices中注册，这里不需要重复注册

            // 其他基础设施服务
            // services.AddScoped<ITaktCurrentUser, TaktCurrentUser>(); // 已在Program.cs中注册，这里不需要重复注册

            return services;
        }



        /// <summary>
        /// 通过差异日志管理器处理差异日志事件
        /// </summary>
        private static async Task HandleDiffLogEventViaManager(DiffLogModel diffLog, string dbName, IServiceCollection services)
        {
            try
            {
                // 从服务集合获取差异日志管理器
                var serviceProvider = services.BuildServiceProvider();
                var diffLogManager = serviceProvider.GetService<ITaktSqlDiffLogManager>();

                if (diffLogManager != null)
                {
                    // 调用差异日志管理器处理事件
                    await diffLogManager.HandleSqlSugarDiffLogEventAsync(diffLog, dbName);
                }
                else
                {
                    Console.WriteLine($"[AOP] 错误：无法获取差异日志管理器服务");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AOP] 通过管理器处理差异日志事件失败: 数据库={dbName}, 错误={TaktMaskUtils.MaskSensitiveInfo(ex.Message)}");
            }
        }

        // 差异日志处理逻辑已迁移到 TaktLogSqlDiffManager
    }
}






