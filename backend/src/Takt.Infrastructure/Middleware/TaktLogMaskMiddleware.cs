//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogMaskMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:40
// 版本号 : V0.0.1
// 描述   : 日志遮罩中间件
//===================================================================

using Takt.Shared.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// 日志遮罩中间件
/// </summary>
public class TaktLogMaskMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TaktLogMaskMiddleware> _logger;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public TaktLogMaskMiddleware(RequestDelegate next, ILogger<TaktLogMaskMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
/// <summary>
/// 执行中间件
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 记录请求开始（遮罩敏感信息）
            LogRequestStart(context);

            await _next(context);

            // 记录请求完成（遮罩敏感信息）
            LogRequestComplete(context);
        }
        catch (Exception ex)
        {
            // 记录异常（遮罩敏感信息）
            LogException(context, ex);
            throw;
        }
    }

    /// <summary>
    /// 记录请求开始
    /// </summary>
    private void LogRequestStart(HttpContext context)
    {
        var request = context.Request;
        var maskedPath = TaktMaskUtils.MaskSensitiveInfo(request.Path.Value);
        var maskedQueryString = TaktMaskUtils.MaskSensitiveInfo(request.QueryString.Value);
        var maskedHeaders = MaskHeaders(request.Headers);

        _logger.LogInformation(
            "请求开始: {Method} {Path} {QueryString} | Headers: {Headers}",
            request.Method,
            maskedPath,
            maskedQueryString,
            maskedHeaders
        );
    }

    /// <summary>
    /// 记录请求完成
    /// </summary>
    private void LogRequestComplete(HttpContext context)
    {
        var response = context.Response;
        var request = context.Request;
        var maskedPath = TaktMaskUtils.MaskSensitiveInfo(request.Path.Value);

        _logger.LogInformation(
            "请求完成: {Method} {Path} | Status: {StatusCode}",
            request.Method,
            maskedPath,
            response.StatusCode
        );
    }

    /// <summary>
    /// 记录异常
    /// </summary>
    private void LogException(HttpContext context, Exception ex)
    {
        var request = context.Request;
        var maskedPath = TaktMaskUtils.MaskSensitiveInfo(request.Path.Value);
        var maskedException = TaktMaskUtils.MaskException(ex);

        _logger.LogError(
            ex,
            "请求异常: {Method} {Path} | Exception: {Exception}",
            request.Method,
            maskedPath,
            maskedException
        );
    }

    /// <summary>
    /// 遮罩请求头中的敏感信息
    /// </summary>
    private string MaskHeaders(IHeaderDictionary headers)
    {
        var sensitiveHeaders = new[] { "authorization", "cookie", "x-csrf-token", "x-api-key" };
        var maskedHeaders = new List<string>();

        foreach (var header in headers)
        {
            var headerName = header.Key.ToLower();
            var headerValue = header.Value.ToString();

            if (sensitiveHeaders.Contains(headerName))
            {
                maskedHeaders.Add($"{header.Key}=***");
            }
            else
            {
                maskedHeaders.Add($"{header.Key}={TaktMaskUtils.MaskSensitiveInfo(headerValue)}");
            }
        }

        return string.Join("; ", maskedHeaders);
    }
}

/// <summary>
/// 日志遮罩中间件扩展
/// </summary>
public static class TaktLogMaskMiddlewareExtensions
{
    /// <summary>
    /// 添加日志遮罩中间件
    /// </summary>
    public static IApplicationBuilder UseLogMasking(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TaktLogMaskMiddleware>();
    }
}





