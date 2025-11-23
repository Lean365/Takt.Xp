using Takt.Shared.Options;
using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// 验证码中间件
/// </summary>
public class TaktCaptchaMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;
    private readonly ITaktCaptchaService _captchaService;
    private readonly TaktCaptchaOptions _options;
    private readonly string _loginFailCountPrefix = "login:fail:count:";
    private readonly string _captchaValidPrefix = "captcha:valid:";

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next"></param>
    /// <param name="cache"></param>
    /// <param name="captchaService"></param>
    /// <param name="options"></param>
    public TaktCaptchaMiddleware(
        RequestDelegate next,
        IDistributedCache cache,
        ITaktCaptchaService captchaService,
        IOptions<TaktCaptchaOptions> options)
    {
        _next = next;
        _cache = cache;
        _captchaService = captchaService;
        _options = options.Value;
    }

    /// <summary>
    /// 处理请求
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        // 跳过验证码相关的接口
        if (IsCaptchaRequest(context))
        {
            await _next(context);
            return;
        }

        // 1. 检查是否需要验证码
        if (await ShouldRequireCaptchaAsync(context))
        {
            // 2. 验证验证码
            if (!await ValidateCaptchaAsync(context))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "请完成验证码验证" });
                return;
            }
        }

        // 3. 继续处理请求
        await _next(context);

        // 4. 处理登录失败
        if (IsLoginRequest(context) && !IsLoginSuccess(context))
        {
            await IncrementLoginFailCountAsync(context);
        }
    }

    /// <summary>
    /// 检查是否需要验证码
    /// </summary>
    private async Task<bool> ShouldRequireCaptchaAsync(HttpContext context)
    {
        if (!IsLoginRequest(context))
        {
            return false;
        }

        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var failCount = await GetLoginFailCountAsync(ip);
        return failCount >= _options.LoginFailThreshold;
    }

    /// <summary>
    /// 验证验证码
    /// </summary>
    private async Task<bool> ValidateCaptchaAsync(HttpContext context)
    {
        var token = context.Request.Headers["X-Captcha-Token"].ToString();
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        var cacheKey = $"{_captchaValidPrefix}{token}";
        var validResult = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(validResult))
        {
            return JsonConvert.DeserializeObject<bool>(validResult);
        }

        return false;
    }

    /// <summary>
    /// 是否是验证码相关请求
    /// </summary>
    private bool IsCaptchaRequest(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();
        return path != null && path.Contains("/Taktcaptcha/");
    }

    /// <summary>
    /// 是否是登录请求
    /// </summary>
    private bool IsLoginRequest(HttpContext context)
    {
        return context.Request.Path.Value?.EndsWith("/login") == true &&
               context.Request.Method == "POST";
    }

    /// <summary>
    /// 是否登录成功
    /// </summary>
    private bool IsLoginSuccess(HttpContext context)
    {
        return context.Response.StatusCode == StatusCodes.Status200OK;
    }

    /// <summary>
    /// 获取登录失败次数
    /// </summary>
    private async Task<int> GetLoginFailCountAsync(string ip)
    {
        var cacheKey = $"{_loginFailCountPrefix}{ip}";
        var countStr = await _cache.GetStringAsync(cacheKey);
        return string.IsNullOrEmpty(countStr) ? 0 : int.Parse(countStr);
    }

    /// <summary>
    /// 增加登录失败次数
    /// </summary>
    private async Task IncrementLoginFailCountAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var cacheKey = $"{_loginFailCountPrefix}{ip}";
        var count = await GetLoginFailCountAsync(ip) + 1;

        await _cache.SetStringAsync(
            cacheKey,
            count.ToString(),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.LoginFailExpireMinutes)
            }
        );
    }
}

