using System.Text;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.IServices.Extensions;
using Takt.Domain.IServices.Caching;
using Takt.Domain.IServices.Security;
using Takt.Domain.IServices.SignalR;
using Takt.Infrastructure.Caching;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Data.Seeds;
using Takt.Infrastructure.Extensions;
using Takt.Infrastructure.Jobs;
using Takt.Infrastructure.Security;
using Takt.Infrastructure.Services.Identity;
using Takt.Infrastructure.SignalR.Cache;
using Takt.WebApi.Middlewares;
using Takt.Application.Services.Logging;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using Quartz;
using SqlSugar;


var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.Setup()
                      .LoadConfigurationFromFile("nlog.config")
                      .GetCurrentClassLogger();
try
{
  logger.Info("正在初始化应用程序...");

  // 添加NLog支持
  builder.Logging.ClearProviders();
  builder.Host.UseNLog();

  // 注册NLog.ILogger
  builder.Services.AddSingleton<NLog.ILogger>(sp => LogManager.GetCurrentClassLogger());

  // 配置SignalR日志
  builder.Logging.AddFilter("Microsoft.AspNetCore.SignalR", Microsoft.Extensions.Logging.LogLevel.Debug);
  builder.Logging.AddFilter("Microsoft.AspNetCore.Http.Connections", Microsoft.Extensions.Logging.LogLevel.Debug);

  // 配置 Kestrel 服务器
  var serverConfig = builder.Configuration.GetSection("Server").Get<TaktServerConfig>()
      ?? throw new InvalidOperationException("服务器配置节点不能为空");

  if (serverConfig.HttpPort <= 0)
  {
    throw new InvalidOperationException("HTTP端口配置无效");
  }

  if (serverConfig.UseHttps && serverConfig.HttpsPort <= 0)
  {
    throw new InvalidOperationException("HTTPS端口配置无效");
  }

  // 配置数据库
  var dbConfig = builder.Configuration.GetSection("Database").Get<TaktDbOptions>() ?? throw new InvalidOperationException("数据库配置节点不能为空");

  // 配置雪花ID
  if (dbConfig.SnowflakeId.Enable)
  {
    // 程序启动时执行一次，设置雪花ID的WorkId
    SnowFlakeSingle.WorkId = dbConfig.SnowflakeId.WorkId;
    logger.Info($"雪花ID已启用，WorkId: {dbConfig.SnowflakeId.WorkId}");
  }
  else
  {
    logger.Info("雪花ID未启用，使用自增ID");
  }



  builder.Services.Configure<IISServerOptions>(options =>
  {
    options.MaxRequestBodySize = null; // 移除请求体大小限制
  });

  builder.Services.Configure<KestrelServerOptions>(options =>
  {
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
    options.Limits.MaxRequestHeadersTotalSize = 65536; // 64KB (从32KB增加到64KB)
    options.Limits.MaxRequestLineSize = 8192; // 8KB
  });

  builder.WebHost.ConfigureKestrel(options =>
  {
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
    options.Limits.MaxRequestHeadersTotalSize = 65536; // 64KB (从32KB增加到64KB)
    options.Limits.MaxRequestLineSize = 8192; // 8KB
  });

  // 配置服务器URL
  if (serverConfig.UseHttps)
  {
    builder.WebHost.UseUrls(
        $"https://localhost:{serverConfig.HttpsPort}",
        $"http://localhost:{serverConfig.HttpPort}"
    );
    logger.Info($"已启用HTTPS，监听端口: {serverConfig.HttpsPort}");
    logger.Info($"HTTP请求将被重定向到HTTPS");
  }
  else
  {
    builder.WebHost.UseUrls($"http://localhost:{serverConfig.HttpPort}");
    logger.Info($"仅使用HTTP，监听端口: {serverConfig.HttpPort}");
  }

  // 配置 JSON 序列化
  JsonConvert.DefaultSettings = () => new JsonSerializerSettings
  {
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    NullValueHandling = NullValueHandling.Ignore,
    DateFormatString = "yyyy-MM-dd HH:mm:ss"
  };

  // 添加CORS服务
  var corsOrigins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
  var corsMethods = builder.Configuration.GetSection("Cors:Methods").Get<string[]>() ?? new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" };
  var corsHeaders = builder.Configuration.GetSection("Cors:Headers").Get<string[]>() ?? new[] { "*" };
  var allowCredentials = builder.Configuration.GetSection("Cors:AllowCredentials").Get<bool>();

  builder.Services.AddCors(options =>
  {
    options.AddPolicy("TaktPolicy", builder =>
      {
        builder
              .WithOrigins(corsOrigins)
              .WithMethods(corsMethods)
              .WithHeaders("Content-Type", "Authorization", "Accept", "Accept-Language", "X-Requested-With", "X-XSRF-TOKEN", "X-CSRF-Token")
              .SetIsOriginAllowedToAllowWildcardSubdomains()
              .WithExposedHeaders("Content-Disposition", "X-Device-Id", "X-Device-Name", "X-Device-Type", "X-Device-Model",
                  "X-OS-Type", "X-OS-Version", "X-Browser-Type", "X-Browser-Version",
                  "X-Resolution", "X-Location", "X-Device-Token");
      });
  });

  // 添加控制器服务
  builder.Services.AddControllers(options =>
  {
    options.EnableEndpointRouting = true;
  })

  .AddNewtonsoftJson(opt =>
  {
    // 设置 JSON 序列化为 camelCase
    //opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    // 忽略循环引用
    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

    // 不改变字段大小（SqlSugar官方推荐配置）
    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    // 忽略空值
    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

    // 日期格式化
    opt.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
  })
  .ConfigureApiBehaviorOptions(options =>
  {
    options.SuppressModelStateInvalidFilter = true;
  });

  // 配置文件上传限制
  builder.Services.Configure<FormOptions>(options =>
  {
    var fileUploadConfig = builder.Configuration.GetSection("FileUpload");
    options.MultipartBodyLengthLimit = fileUploadConfig.GetValue<int>("MultipartBodyLengthLimit", 268435456); // 256MB
    options.MultipartHeadersLengthLimit = fileUploadConfig.GetValue<int>("MultipartHeadersLengthLimit", 32768); // 32KB
    options.MultipartBoundaryLengthLimit = fileUploadConfig.GetValue<int>("MultipartBoundaryLengthLimit", 128); // 128 bytes
    options.ValueLengthLimit = fileUploadConfig.GetValue<int>("ValueLengthLimit", 10485760); // 10MB
    options.KeyLengthLimit = fileUploadConfig.GetValue<int>("KeyLengthLimit", 1024); // 1KB
    options.BufferBody = true;
    options.MemoryBufferThreshold = 1048576; // 1MB
  });

  // 添加 Antiforgery 服务
  builder.Services.AddAntiforgery(options =>
  {
    options.HeaderName = "X-CSRF-Token";
    options.Cookie.Name = "XSRF-TOKEN";
    options.Cookie.HttpOnly = false;
    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
    options.Cookie.SameSite = builder.Environment.IsDevelopment() ? SameSiteMode.Lax : SameSiteMode.Strict;
  });

  // 添加 HttpClient 服务
  builder.Services.AddHttpClient();

  // 添加 Swagger 服务
  builder.Services.AddTaktSwagger();

  // 配置安全选项
  builder.Services.Configure<TaktSecurityOptions>(builder.Configuration.GetSection("Security"));

  // 配置注册选项
  builder.Services.Configure<TaktRegistrationOptions>(builder.Configuration.GetSection("Security:Registration"));

  // 配置密码策略选项
  builder.Services.Configure<TaktPasswordPolicyOptions>(builder.Configuration.GetSection("Security:PasswordPolicy"));

  // 配置验证码选项
  builder.Services.Configure<TaktCaptchaOptions>(builder.Configuration.GetSection("Captcha"));

  // SMS和OAuth配置已移除

  // 配置日志清理和归档选项
  builder.Services.Configure<TaktLogCleanupOptions>(builder.Configuration.GetSection("LogCleanup"));
  builder.Services.Configure<TaktLogArchiveOptions>(builder.Configuration.GetSection("LogArchive"));

  // 配置文件上传选项
  builder.Services.Configure<FileUploadOptions>(builder.Configuration.GetSection("FileUpload"));

  // 配置Excel选项
  builder.Services.Configure<TaktExcelOptions>(builder.Configuration.GetSection("Excel"));

  // 注册当前用户服务
  builder.Services.AddScoped<ITaktCurrentUser, TaktCurrentUser>();

  // Cookie服务已集成到JWT扩展中，无需单独注册

  // 添加基础设施服务
  builder.Services.AddInfrastructure(builder.Configuration);

  // 添加领域服务
  builder.Services.AddDomainServices();

  // 添加应用服务
  builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);

  // 添加本地化服务
  builder.Services.AddTaktLocalization();

  // 配置 JWT 认证
  builder.Services.AddTaktJwtAuthentication(builder.Configuration);

  // 配置SignalR
  builder.Services.AddTaktSignalR(builder.Configuration);

  builder.Services.AddMemoryCache();

  // 配置单用户登录（使用TaktSessionOptions）
  builder.Services.Configure<TaktSessionOptions>(builder.Configuration.GetSection("Security:Session"));

  // 根据配置选择SignalR缓存实现
  var cacheSettings = builder.Configuration.GetSection("Cache").Get<TaktCacheOptions>();

  // 注册缓存配置管理器（改为Scoped生命周期）
  builder.Services.AddScoped<TaktCacheConfigManager>();

  // 注册内存缓存
  builder.Services.AddScoped<ITaktMemoryCache, TaktMemoryCache>();

  if (cacheSettings?.Provider == CacheProviderType.Redis &&
      cacheSettings?.Redis?.Enabled == true &&
      !string.IsNullOrEmpty(cacheSettings.Redis.ConnectionString))
  {
    // 配置Redis连接
    builder.Services.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = cacheSettings.Redis.ConnectionString;
      options.InstanceName = cacheSettings.Redis.InstanceName;
    });
    builder.Services.AddScoped<ITaktRedisCache, TaktRedisCache>();
    builder.Services.AddScoped<ITaktSignalRCacheService, TaktSignalRRedisCache>();
  }
  else
  {
    builder.Services.AddScoped<ITaktRedisCache, TaktNullRedisCache>();
    builder.Services.AddScoped<ITaktSignalRCacheService, TaktSignalRMemoryCache>();
  }

  // 注册缓存工厂
  builder.Services.AddScoped<ITaktCacheFactory, TaktCacheFactory>();

  // 注册HttpContext访问器
  builder.Services.AddHttpContextAccessor();

  // 注册种子数据服务
  builder.Services.AddTaktSeeds();

  // 添加后台服务
  builder.Services.AddHostedService<TaktLoginPolicyInitializer>();
  // 添加验证码初始化服务
  builder.Services.AddHostedService<TaktCaptchaInitializer>();

  // 注册邮件配置
  builder.Services.Configure<TaktMailOption>(builder.Configuration.GetSection(TaktMailOption.Position));

  // 注册定时任务配置
  builder.Services.Configure<TaktQuartzOption>(builder.Configuration.GetSection(TaktQuartzOption.Position));

  // 配置Quartz服务
  builder.Services.AddQuartz(q =>
  {
    // 从配置中获取Quartz设置
    var quartzOptions = builder.Configuration.GetSection(TaktQuartzOption.Position).Get<TaktQuartzOption>();
    if (quartzOptions != null)
    {
      // 配置Quartz调度器
      q.UseSimpleTypeLoader();
      q.UseInMemoryStore();
      q.UseDefaultThreadPool(tp =>
        {
          tp.MaxConcurrency = quartzOptions.ThreadPool.MaxConcurrency;
        });

      // 添加在线用户清理任务
      var jobKey = new JobKey("OnlineUserCleanupJob");
      q.AddJob<TaktSignalROnlineCleanupJob>(opts => opts.WithIdentity(jobKey));
      q.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity("OnlineUserCleanupTrigger")
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(1) // 每分钟执行一次
                .RepeatForever())
        );

      // 添加日志清理任务
      var logCleanupJobKey = new JobKey("LogCleanupJob");
      q.AddJob<TaktLogCleanupJob>(opts => opts.WithIdentity(logCleanupJobKey));
      q.AddTrigger(opts => opts
            .ForJob(logCleanupJobKey)
            .WithIdentity("LogCleanupTrigger")
            .WithSimpleSchedule(x => x
                .WithIntervalInHours(24) // 每24小时执行一次
                .RepeatForever())
        );


    }
  });

  // 添加Quartz托管服务
  builder.Services.AddQuartzHostedService(options =>
  {
    options.WaitForJobsToComplete = true;
  });

  // 注册IScheduler服务
  builder.Services.AddSingleton(provider =>
  {
    var schedulerFactory = provider.GetRequiredService<ISchedulerFactory>();
    return schedulerFactory.GetScheduler().GetAwaiter().GetResult();
  });

  // 初始化邮件服务
  builder.Services.AddTransient(provider =>
  {
    TaktMailHelper.Initialize(provider);
    return provider;
  });

  // 初始化定时任务
  builder.Services.AddTransient(async provider =>
  {
    var options = provider.GetRequiredService<IOptions<TaktQuartzOption>>();
    await TaktQuartzHelper.InitializeAsync(options.Value);
    return provider;
  });

  // 注册系统重启服务
  builder.Services.AddScoped<ITaktRestartService, TaktRestartService>();

  // 注册系统重启配置选项
  builder.Services.Configure<TaktRestartOptions>(
      builder.Configuration.GetSection("SystemRestart"));

  builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
  builder.Services.AddMvc()
      .AddViewLocalization()
      .AddDataAnnotationsLocalization();

  // 租户审计日志服务已移除

  var app = builder.Build();

  // 初始化IP位置查询工具（失败不影响应用启动）
  try
  {
    var ipUtilsInitialized = TaktIpUtils.SetWebHostEnvironment(app.Environment);
    if (ipUtilsInitialized)
    {
      logger.Info("IP位置查询工具初始化成功");
    }
    else
    {
      logger.Warn("IP位置查询工具初始化失败，IP位置查询功能将不可用");
    }
  }
  catch (Exception ex)
  {
    logger.Error(ex, "IP位置查询工具初始化失败，IP位置查询功能将不可用。错误: {0}", ex.Message);
    // 不抛出异常，允许应用继续启动
  }

  // 初始化所有系统组件（数据库、种子数据、系统清理）
  using var scope = app.Services.CreateScope();
  var TaktLogger = scope.ServiceProvider.GetRequiredService<ITaktLogger>();
  await app.InitializeAllAsync(dbConfig, builder.Configuration, TaktLogger);

  // 配置HTTP请求管道
  if (app.Environment.IsDevelopment() && serverConfig.EnableSwagger)
  {
    app.UseTaktSwagger();
    logger.Info("Swagger已启用");
  }

  // 1. 异常处理中间件（必须放在最前面，以捕获后续中间件中的所有异常）
  app.UseTaktExceptionHandler();

  // 2. 会话安全中间件（处理会话相关的安全机制）
  app.UseTaktSessionSecurity();

  // 3. SQL注入防护中间件（防止SQL注入攻击）
  app.UseTaktSqlInjection();

  // 4. 跨域资源共享中间件（如果启用）
  if (serverConfig.EnableCors)
  {
    app.UseCors("TaktPolicy");
    logger.Info("CORS已启用");
  }

  // 5. 开启访问静态文件/wwwroot目录文件
  app.UseStaticFiles();
  logger.Info("静态文件中间件已启用");

  // 6. CSRF防护中间件（防止跨站请求伪造攻击）
  app.UseTaktCsrf();
  logger.Info("CSRF防护已启用");

  // 7. HTTPS重定向中间件（如果启用HTTPS）
  if (serverConfig.UseHttps)
  {
    app.UseHttpsRedirection();
    logger.Info("已启用HTTPS重定向");
  }

  // 8. XSS防护中间件（检测和阻止跨站脚本攻击）
  app.UseTaktXssProtection();

  // 9. 速率限制中间件（控制请求频率，防止DoS攻击）
  app.UseTaktRateLimit();

  // 10. 区分大小写路由中间件（确保路由匹配时区分大小写）
  app.UseTaktCaseSensitiveRoute();

  // 11. 添加路由中间件
  app.UseRouting();

  // 12. 添加认证中间件
  app.UseAuthentication();
  app.UseAuthorization();

  // 13. 添加租户中间件

  // 14. 添加权限中间件
  app.UseTaktPerm();

  // 15. 添加本地化中间件
  app.UseTaktLocalization();

  // 16. 租户审计日志中间件已移除

  // 17. 注册所有控制器路由和SignalR Hub
  app.MapControllers();
  app.UseTaktSignalR();

  // 配置Excel帮助类（用于处理Excel导入导出功能）
  var excelOptions = app.Services.GetRequiredService<IOptions<TaktExcelOptions>>();
  TaktExcelHelper.Configure(excelOptions);

  logger.Info("应用程序启动成功");
  app.Run();
}
catch (Exception ex)
{
  logger.Fatal(ex, "应用程序启动失败");
  throw;
}
finally
{
  LogManager.Shutdown();
}


