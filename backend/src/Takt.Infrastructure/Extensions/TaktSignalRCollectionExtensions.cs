//===================================================================
// 项目名 : Takt.Infrastructure
// 文件名 : TaktSignalRCollectionExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : SignalR扩展类
//===================================================================

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Takt.Domain.IServices.SignalR;
using Takt.Infrastructure.SignalR;
using Takt.Infrastructure.SignalR.Cache;
using Takt.Infrastructure.SignalR.Notification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Takt.Shared.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Connections;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// SignalR扩展类
    /// </summary>
    public static class TaktSignalRCollectionExtensions
    {
        /// <summary>
        /// 添加SignalR服务 - 统一注册所有SignalR相关服务
        /// </summary>
        public static IServiceCollection AddTaktSignalR(this IServiceCollection services, IConfiguration configuration)
        {
            // 获取SignalR配置
            var signalRConfig = configuration.GetSection("SignalR").Get<SignalRConfig>();

            // 配置SignalR日志
            services.AddLogging(logging =>
            {
                logging.AddFilter("Microsoft.AspNetCore.SignalR", 
                    signalRConfig.EnableDetailedErrors ? LogLevel.Debug : LogLevel.Information);
                logging.AddFilter("Microsoft.AspNetCore.Http.Connections", 
                    signalRConfig.EnableDetailedErrors ? LogLevel.Debug : LogLevel.Information);
            });

            // 配置SignalR
            services.AddSignalR(options =>
            {
                // 基本设置
                options.EnableDetailedErrors = signalRConfig.EnableDetailedErrors;
                options.MaximumReceiveMessageSize = signalRConfig.MaximumReceiveMessageSize;
                options.HandshakeTimeout = TimeSpan.FromSeconds(signalRConfig.HandshakeTimeout);
                options.KeepAliveInterval = TimeSpan.FromSeconds(signalRConfig.KeepAliveInterval);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(signalRConfig.ClientTimeoutInterval);
                options.StreamBufferCapacity = signalRConfig.StreamBufferCapacity;
            })
            .AddHubOptions<TaktSignalRHub>(options =>
            {
                options.EnableDetailedErrors = signalRConfig.EnableDetailedErrors;
                options.MaximumReceiveMessageSize = signalRConfig.MaximumReceiveMessageSize;
                options.HandshakeTimeout = TimeSpan.FromSeconds(signalRConfig.HandshakeTimeout);
                options.KeepAliveInterval = TimeSpan.FromSeconds(signalRConfig.KeepAliveInterval);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(signalRConfig.ClientTimeoutInterval);
            })
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.PayloadSerializerOptions.WriteIndented = false;
            });

            // 注册所有 SignalR 相关服务
            // 注意：所有 SignalR 相关服务都在这里统一注册，包括：
            // 1. Infrastructure 层服务：连接服务、Hub、通知服务
            // 2. Application 层服务：在线用户服务、在线消息服务
            services.AddScoped<ITaktSignalRConnectionService, TaktSignalRConnectionService>(); // SignalR连接服务
            services.AddScoped<ITaktSignalRHub, TaktSignalRHub>();
            services.AddScoped<ITaktSignalRNotificationService, TaktSignalRNotificationService>(); // 通知服务
            services.AddScoped<ITaktSignalRMessageService, TaktSignalRMessageService>(); // 在线消息服务
            services.AddScoped<ITaktSignalROnlineService, TaktSignalROnlineService>(); // 在线用户服务
            services.AddScoped<ITaktSignalRClient>(sp =>
            {
                var hubContext = sp.GetRequiredService<IHubContext<TaktSignalRHub, ITaktSignalRClient>>();
                return hubContext.Clients.All;
            });

            // 配置 SignalR 缓存选项
            services.Configure<TaktSignalRCacheOptions>(
                configuration.GetSection("SignalRCache"));

            // JWT 认证配置已移至 Program.cs 中统一处理，避免重复配置

            return services;
        }

        /// <summary>
        /// 使用SignalR
        /// </summary>
        public static IApplicationBuilder UseTaktSignalR(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            var signalRConfig = configuration.GetSection("SignalR").Get<SignalRConfig>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TaktSignalRHub>("/signalr/Takthub", options =>
                {
                    options.CloseOnAuthenticationExpiration = signalRConfig.Authentication.RequireAuthentication;
                    options.ApplicationMaxBufferSize = signalRConfig.MaximumReceiveMessageSize;
                    options.TransportMaxBufferSize = signalRConfig.MaximumReceiveMessageSize;
                });
            });

            return app;
        }
    }
}



