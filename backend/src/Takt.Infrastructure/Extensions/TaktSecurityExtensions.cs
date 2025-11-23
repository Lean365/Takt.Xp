//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSecurityExtensions.cs
// 创建者 : Claude
// 创建时间: 2024-12-01 15:30
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : 安全相关中间件扩展方法集合
//===================================================================

using Microsoft.AspNetCore.Builder;
using Takt.Infrastructure.Middleware;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 安全相关中间件扩展方法集合
/// </summary>
/// <remarks>
/// 提供CSRF防护、限流、SQL注入防护、会话安全等安全中间件的扩展方法
/// </remarks>
public static class TaktSecurityExtensions
{
    #region CSRF防护

    /// <summary>
    /// 使用CSRF防护中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    /// <remarks>
    /// 防止跨站请求伪造攻击，确保请求来自合法的源
    /// </remarks>
    public static IApplicationBuilder UseTaktCsrf(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TaktCsrfMiddleware>();
    }

    #endregion

    #region 限流控制

    /// <summary>
    /// 使用限流中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    /// <remarks>
    /// 控制请求频率，防止DoS攻击和资源滥用
    /// </remarks>
    public static IApplicationBuilder UseTaktRateLimit(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TaktRateLimitMiddleware>();
    }

    #endregion

    #region SQL注入防护

    /// <summary>
    /// 使用SQL注入防护中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    /// <remarks>
    /// 检测和阻止SQL注入攻击，保护数据库安全
    /// </remarks>
    public static IApplicationBuilder UseTaktSqlInjection(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TaktSqlInjectionMiddleware>();
    }

    #endregion

    #region 会话安全

    /// <summary>
    /// 使用会话安全中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    /// <remarks>
    /// 处理会话相关的安全机制，包括会话验证和安全管理
    /// </remarks>
    public static IApplicationBuilder UseTaktSessionSecurity(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TaktSessionSecurityMiddleware>();
    }

    #endregion

    #region XSS防护

    /// <summary>
    /// 使用XSS防护中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    /// <remarks>
    /// 检测和阻止跨站脚本攻击，设置安全响应头
    /// </remarks>
    public static IApplicationBuilder UseTaktXssProtection(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TaktXssProtectionMiddleware>();
    }

    #endregion
}


