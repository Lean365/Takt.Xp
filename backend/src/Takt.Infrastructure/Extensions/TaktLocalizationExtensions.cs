//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLocalizationExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化扩展方法
//===================================================================

using Takt.Domain.IServices.Extensions;
using Takt.Infrastructure.Services;
using Takt.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Takt.Infrastructure.Services.Local;
namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 本地化扩展方法
/// </summary>
public static class TaktLocalizationExtensions
{
    /// <summary>
    /// 添加本地化服务
    /// </summary>
    public static IServiceCollection AddTaktLocalization(this IServiceCollection services)
    {
        // 注册HttpContext访问器
        services.AddHttpContextAccessor();

        // 注册本地化相关服务
        services.AddScoped<ITaktTranslationCache, TaktTranslationCache>();     // 翻译缓存服务
        services.AddScoped<ITaktLocalizationService, TaktLocalizationService>();  // 本地化服务
        
        return services;
    }

    /// <summary>
    /// 使用本地化服务
    /// </summary>
    public static IApplicationBuilder UseTaktLocalization(this IApplicationBuilder app)
    {
        // 添加本地化中间件
        app.UseMiddleware<TaktLocalizationMiddleware>();

        return app;
    }
} 



