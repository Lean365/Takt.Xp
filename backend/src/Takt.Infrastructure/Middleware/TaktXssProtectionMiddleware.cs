//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktXssProtectionMiddleware.cs
// 创建者 : Claude
// 创建时间: 2024-12-01 16:00
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : XSS防护中间件
//===================================================================

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Takt.Shared.Options;
using Takt.Domain.IServices;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// XSS防护中间件
/// </summary>
/// <remarks>
/// 提供跨站脚本攻击防护功能：
/// 1. 请求参数XSS检测和过滤
/// 2. 响应头安全策略设置
/// 3. 恶意脚本检测和阻止
/// </remarks>
public class TaktXssProtectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TaktSecurityOptions _options;
    private readonly ITaktLogger _logger;

    // XSS 攻击模式检测正则表达式
    private static readonly Regex[] XssPatterns = {
        new(@"<script[^>]*>.*?</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"javascript:", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"on\w+\s*=", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<iframe[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<object[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<embed[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<link[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<meta[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"expression\s*\(", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"vbscript:", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"data:text/html", RegexOptions.IgnoreCase | RegexOptions.Compiled),
        new(@"<style[^>]*>.*?</style>", RegexOptions.IgnoreCase | RegexOptions.Compiled)
    };

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="options">安全配置选项</param>
    /// <param name="logger">日志服务</param>
    public TaktXssProtectionMiddleware(
        RequestDelegate next,
        IOptions<TaktSecurityOptions> options,
        ITaktLogger logger)
    {
        _next = next;
        _options = options.Value;
        _logger = logger;
    }

    /// <summary>
    /// 调用中间件处理XSS防护
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 1. 设置安全响应头
            SetSecurityHeaders(context);

            // 2. 检查请求参数中的XSS攻击
            if (await DetectXssInRequest(context))
            {
                _logger.Warn("[XSS防护] 检测到XSS攻击尝试: IP={IP}, Path={Path}, UserAgent={UserAgent}",
                    context.Connection.RemoteIpAddress?.ToString(),
                    context.Request.Path,
                    context.Request.Headers["User-Agent"].ToString());

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { 
                    message = "检测到恶意请求，已被安全系统阻止",
                    code = "XSS_DETECTED"
                });
                return;
            }

            // 3. 继续处理请求
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error("[XSS防护] 中间件处理异常: {Message}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 设置安全响应头
    /// </summary>
    private void SetSecurityHeaders(HttpContext context)
    {
        var response = context.Response;

        // X-XSS-Protection: 启用浏览器XSS过滤器
        response.Headers["X-XSS-Protection"] = "1; mode=block";

        // X-Content-Type-Options: 防止MIME类型嗅探
        response.Headers["X-Content-Type-Options"] = "nosniff";

        // X-Frame-Options: 防止点击劫持
        response.Headers["X-Frame-Options"] = "DENY";

        // Referrer-Policy: 控制引用信息泄露
        response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

        // Content-Security-Policy: 内容安全策略，防止XSS攻击
        response.Headers["Content-Security-Policy"] = 
            "default-src 'self'; " +
            "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://cdn.jsdelivr.net; " +
            "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; " +
            "font-src 'self' https://fonts.gstatic.com; " +
            "img-src 'self' data: https:; " +
            "connect-src 'self' https: wss:; " +
            "frame-ancestors 'none'; " +
            "base-uri 'self'; " +
            "form-action 'self'";

        // 移除重复的CSP配置，使用上面更完整的版本

        // Permissions-Policy: 权限策略
        response.Headers["Permissions-Policy"] = 
            "geolocation=(), microphone=(), camera=(), payment=(), usb=(), magnetometer=(), gyroscope=(), speaker=()";
    }

    /// <summary>
    /// 检测请求中的XSS攻击
    /// </summary>
    private async Task<bool> DetectXssInRequest(HttpContext context)
    {
        var request = context.Request;

        // 检查查询参数
        foreach (var param in request.Query)
        {
            if (ContainsXssPattern(param.Value.ToString()))
            {
                _logger.Warn("[XSS防护] 查询参数包含XSS模式: {Key}={Value}", 
                    param.Key, param.Value.ToString());
                return true;
            }
        }

        // 检查表单数据
        if (request.HasFormContentType)
        {
            var form = await request.ReadFormAsync();
            foreach (var field in form)
            {
                if (ContainsXssPattern(field.Value.ToString()))
                {
                    _logger.Warn("[XSS防护] 表单字段包含XSS模式: {Key}={Value}", 
                        field.Key, field.Value.ToString());
                    return true;
                }
            }
        }

        // 检查请求头
        foreach (var header in request.Headers)
        {
            if (header.Key.StartsWith("X-") || 
                header.Key.Equals("User-Agent", StringComparison.OrdinalIgnoreCase) ||
                header.Key.Equals("Referer", StringComparison.OrdinalIgnoreCase))
            {
                if (ContainsXssPattern(header.Value.ToString()))
                {
                    _logger.Warn("[XSS防护] 请求头包含XSS模式: {Key}={Value}", 
                        header.Key, header.Value.ToString());
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// 检查字符串是否包含XSS攻击模式
    /// </summary>
    private bool ContainsXssPattern(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        foreach (var pattern in XssPatterns)
        {
            if (pattern.IsMatch(input))
            {
                return true;
            }
        }

        return false;
    }
}



