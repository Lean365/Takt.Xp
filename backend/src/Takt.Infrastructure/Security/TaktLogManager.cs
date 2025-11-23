//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogManager.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 异常日志管理器实现
// 修改说明: 只保留异常日志功能，移除差异日志功能
//===================================================================

using Takt.Domain.Entities.Logging;
using Takt.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Takt.Infrastructure.Security;

/// <summary>
/// 异常日志管理器实现
/// </summary>
public class TaktLogManager : ITaktExceptionLogManager
{
    private readonly ITaktLogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITaktCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLogManager(
        ITaktLogger logger,
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
    }

    /// <summary>
    /// 记录异常日志
    /// </summary>
    public async Task LogExceptionAsync(
        Exception ex,
        string? method = null,
        string? parameters = null)
    {
        var log = new TaktExceptionLog
        {
            LogLevel = 4, // 异常统一使用错误级别
            UserId = _currentUser.UserId,
            UserName = _currentUser.UserName,
            Method = method ?? _httpContextAccessor.HttpContext?.Request.Path ?? "Unknown",
            Parameters = parameters,
            ExceptionType = ex.GetType().FullName ?? "Unknown",
            ExceptionMessage = ex.Message ?? "No message",
            StackTrace = ex.StackTrace ?? "No stack trace",
            IpAddress = GetClientIpAddress(),
            UserAgent = GetUserAgent(),
            CreateBy = _currentUser.UserName,
            CreateTime = DateTime.Now
        };

        try
        {
            // 从服务提供器获取身份数据库上下文
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TaktIdentityDBContext>();
            await context.Client.Insertable(log).ExecuteCommandAsync();
            
            _logger.Error($"记录异常日志成功: {_currentUser.UserName} 在执行 {method ?? "Unknown"} 时发生异常", ex);
        }
        catch (Exception innerEx)
        {
            _logger.Error("记录异常日志失败", innerEx);
            // 不抛出异常，避免影响主业务流程
        }
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    private string GetClientIpAddress()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return "127.0.0.1";

        var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                 context.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                 context.Connection.RemoteIpAddress?.ToString();

        return ip ?? "127.0.0.1";
    }

    /// <summary>
    /// 获取客户端UserAgent
    /// </summary>
    private string GetUserAgent()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
    }
}




