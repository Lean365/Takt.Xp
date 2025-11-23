//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLocalizationMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化中间件
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Infrastructure.Middleware;

/// <summary>
/// 本地化中间件
/// </summary>
public class TaktLocalizationMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 日志服务
    /// </summary>
    protected readonly ITaktLogger _logger;

    private const string LANGUAGE_CACHE_KEY = "CurrentLanguage";
    private const string DEFAULT_LANGUAGE = "zh-CN";

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="logger">日志服务</param>
    public TaktLocalizationMiddleware(RequestDelegate next,
        ITaktLogger logger
            )
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// 处理请求
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 检查连接状态
            if (context.RequestAborted.IsCancellationRequested)
            {
                await _next(context);
                return;
            }

            // 检查是否需要设置语言
            if (!ShouldSetLanguage(context))
            {
                await _next(context);
                return;
            }

            // 从请求作用域中获取本地化服务
            var localizationService = context.RequestServices.GetRequiredService<ITaktLocalizationService>();

            // 获取语言设置
            var language = GetLanguage(context);

            try
            {
                // 设置语言
                localizationService.SetLanguage(language);

                // 缓存语言设置
                context.Items[LANGUAGE_CACHE_KEY] = language;

                // 更新Cookie
                UpdateLanguageCookie(context, language);
            }
            catch (Exception ex)
            {
                _logger.Error("设置语言失败: {Language}", language, ex.Message);
                // 使用默认语言
                localizationService.SetLanguage(DEFAULT_LANGUAGE);
            }
        }
        catch (OperationCanceledException)
        {
            // 请求被取消，这是正常情况
            _logger.Debug("请求被客户端取消");
        }
        catch (Exception ex)
        {
            _logger.Error("本地化中间件执行失败", ex.Message);
            // 不要重新抛出异常，避免影响正常请求处理
        }

        await _next(context);
    }

    /// <summary>
    /// 检查是否需要设置语言
    /// </summary>
    private bool ShouldSetLanguage(HttpContext context)
    {
        // 1. 如果是静态文件请求，不需要设置语言
        if (context.Request.Path.StartsWithSegments("/static") ||
            context.Request.Path.StartsWithSegments("/favicon.ico"))
        {
            return false;
        }

        // 2. 如果已经设置过语言，不需要重复设置
        if (context.Items.ContainsKey(LANGUAGE_CACHE_KEY))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 获取语言设置
    /// </summary>
    private string GetLanguage(HttpContext context)
    {
        try
        {
            // 1. 从请求头获取语言
            var langHeader = context.Request.Headers["Accept-Language"].ToString();
            if (!string.IsNullOrEmpty(langHeader))
            {
                var language = langHeader.Split(',')[0];
                _logger.Debug("Language from header: {Language}", language);
                return language;
            }

            // 2. 从Cookie获取语言
            if (context.Request.Cookies.TryGetValue("lang", out var langCookie))
            {
                _logger.Debug("Language from cookie: {Language}", langCookie);
                return langCookie;
            }

            // 3. 返回默认语言
            _logger.Debug("Using default language: {Language}", DEFAULT_LANGUAGE);
            return DEFAULT_LANGUAGE;
        }
        catch (Exception ex)
        {
            _logger.Error("Error getting language setting", ex.Message);
            return DEFAULT_LANGUAGE;
        }
    }

    /// <summary>
    /// 更新语言Cookie
    /// </summary>
    private void UpdateLanguageCookie(HttpContext context, string language)
    {
        // 如果Cookie不存在或者值不同，则更新Cookie
        if (!context.Request.Cookies.TryGetValue("lang", out var currentLang) ||
            currentLang != language)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddYears(1),
                HttpOnly = false,     // 允许JavaScript访问
                Secure = true,        // 要求HTTPS
                SameSite = SameSiteMode.None,  // 允许跨站点请求
                Path = "/",
                IsEssential = true    // 标记为必要Cookie
            };

            // 在响应中设置Cookie
            context.Response.Cookies.Append("lang", language, cookieOptions);

            // 记录Cookie设置日志
            _logger.Info(
                "Language cookie updated: Language={Language}, SameSite={SameSite}, Secure={Secure}, HttpOnly={HttpOnly}",
                language,
                cookieOptions.SameSite,
                cookieOptions.Secure,
                cookieOptions.HttpOnly
            );
        }
    }
}



