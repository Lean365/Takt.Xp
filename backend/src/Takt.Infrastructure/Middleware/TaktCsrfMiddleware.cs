//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCsrfMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : CSRF防护中间件
//===================================================================

using System.Security.Cryptography;
using Takt.Shared.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Takt.Infrastructure.Middleware
{
    /// <summary>
    /// CSRF防护中间件
    /// </summary>
    public class TaktCsrfMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly TaktSecurityOptions _options;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        private const string CsrfTokenHeader = "X-CSRF-Token";
        private const string CsrfTokenCookie = "XSRF-TOKEN";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="cache"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public TaktCsrfMiddleware(
            RequestDelegate next,
            IDistributedCache cache,
            IOptions<TaktSecurityOptions> options,
            ITaktLogger logger

)
        {
            _next = next;
            _cache = cache;
            _options = options.Value;
            _logger = logger;
        }

        /// <summary>
        /// 调用中间件处理CSRF防护
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>处理结果</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.Info("[CSRF] ===== CSRF中间件开始处理请求 =====");
            _logger.Info("[CSRF] 请求方法: {Method}, 路径: {Path}", context.Request.Method, context.Request.Path);
            _logger.Debug("[CSRF] Processing request: {Method} {Path}", context.Request.Method, context.Request.Path);

            // 1. 检查是否需要跳过CSRF验证
            if (ShouldSkipCsrfCheck(context))
            {
                _logger.Info("[CSRF] 跳过CSRF检查，直接传递给下一个中间件");
                await _next(context);
                return;
            }

            _logger.Info("[CSRF] 开始CSRF验证流程");

            // 2. 获取现有的Cookie Token
            var cookieToken = context.Request.Cookies[CsrfTokenCookie];

            // 3. 检查并生成CSRF令牌（对所有有JWT Token的请求）
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                _logger.Debug("[CSRF] 检测到JWT Token，检查CSRF令牌状态");
                _logger.Debug("[CSRF] 当前Cookie中的CSRF令牌: {Token}", cookieToken ?? "无");
                
                // 如果没有Cookie Token，生成新的
                if (string.IsNullOrEmpty(cookieToken))
                {
                    var token = GenerateCsrfToken();
                    _logger.Info("[CSRF] ===== 生成新的CSRF令牌 =====");
                    _logger.Info("[CSRF] 令牌值: {Token}", token);
                    _logger.Debug("[CSRF] 生成新的CSRF令牌: {Token}", token);
                    
                    var cookieOptions = GetCookieOptions(context);
                    _logger.Info("[CSRF] Cookie配置: HttpOnly={HttpOnly}, Secure={Secure}, SameSite={SameSite}, Path={Path}, MaxAge={MaxAge}", 
                        cookieOptions.HttpOnly, cookieOptions.Secure, cookieOptions.SameSite, cookieOptions.Path, cookieOptions.MaxAge);
                    _logger.Debug("[CSRF] Cookie配置: HttpOnly={HttpOnly}, Secure={Secure}, SameSite={SameSite}, Path={Path}, MaxAge={MaxAge}", 
                        cookieOptions.HttpOnly, cookieOptions.Secure, cookieOptions.SameSite, cookieOptions.Path, cookieOptions.MaxAge);
                    
                    context.Response.Cookies.Append(CsrfTokenCookie, token, cookieOptions);
                    context.Response.Headers.Append(CsrfTokenHeader, token);
                    await CacheTokenAsync(token);
                    
                    _logger.Info("[CSRF] ===== CSRF令牌设置完成 =====");
                    _logger.Info("[CSRF] Cookie名称: {CookieName}", CsrfTokenCookie);
                    _logger.Info("[CSRF] 响应头名称: {HeaderName}", CsrfTokenHeader);
                    _logger.Debug("[CSRF] CSRF令牌已设置到Cookie和响应头");
                    
                    // 更新cookieToken变量，供后续验证使用
                    cookieToken = token;
                }
                else
                {
                    // 如果已有Token，刷新缓存时间
                    await CacheTokenAsync(cookieToken);
                    _logger.Debug("[CSRF] 刷新现有CSRF令牌缓存: {Token}", cookieToken);
                }
            }
            else
            {
                _logger.Debug("[CSRF] 未检测到JWT Token，跳过CSRF令牌生成");
            }

            // 4. 处理GET请求 - 直接放行
            if (context.Request.Method == "GET")
            {
                await _next(context);
                return;
            }

            // 4. 处理非GET请求
            var requestToken = context.Request.Headers[CsrfTokenHeader].ToString();

            _logger.Debug("[CSRF] Request Token: {Token}", requestToken);
            _logger.Debug("[CSRF] Cookie Token: {Token}", cookieToken);

            // 开发环境：简化CSRF验证，只记录不阻止
            var isDevelopment = context.RequestServices.GetService<IHostEnvironment>()?.IsDevelopment() ?? false;
            if (isDevelopment)
            {
                if (string.IsNullOrEmpty(requestToken))
                {
                    _logger.Info("[CSRF] 开发环境：请求头中缺少CSRF令牌，但允许继续");
                }
                else if (string.IsNullOrEmpty(cookieToken))
                {
                    _logger.Info("[CSRF] 开发环境：Cookie中缺少CSRF令牌，但允许继续");
                }
                else if (requestToken != cookieToken)
                {
                    _logger.Info("[CSRF] 开发环境：CSRF令牌不匹配，但允许继续");
                }
                
                // 开发环境直接放行
                await _next(context);
                return;
            }

            // 生产环境：严格验证
            if (string.IsNullOrEmpty(requestToken) || string.IsNullOrEmpty(cookieToken))
            {
                _logger.Warn("[CSRF] 生产环境：缺少CSRF令牌，拒绝请求");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token验证失败" });
                return;
            }

            if (requestToken != cookieToken)
            {
                _logger.Warn("[CSRF] Token mismatch - Request: {RequestToken}, Cookie: {CookieToken}",
                    requestToken, cookieToken);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token验证失败" });
                return;
            }

            // 验证Token是否在缓存中
            var cacheKey = $"csrf:token:{cookieToken}";
            var cachedToken = await _cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cachedToken))
            {
                _logger.Warn("[CSRF] Token not found in cache");
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { message = "CSRF Token已过期" });
                return;
            }

            // Token验证通过，刷新过期时间
            await CacheTokenAsync(cookieToken);
            await _next(context);
        }

        /// <summary>
        /// 生成CSRF Token
        /// </summary>
        private string GenerateCsrfToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 缓存Token
        /// </summary>
        private async Task CacheTokenAsync(string token)
        {
            try
            {
                var cacheKey = $"csrf:token:{token}";
                _logger.Debug("[CSRF] 开始缓存Token: {Key}", cacheKey);

                await _cache.SetStringAsync(
                    cacheKey,
                    token,
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes)
                    }
                );

                // 验证Token是否成功缓存
                var cachedValue = await _cache.GetStringAsync(cacheKey);
                _logger.Debug("[CSRF] Token缓存结果: {Value}", cachedValue);
            }
            catch (Exception ex)
            {
                _logger.Error("[CSRF] Token缓存失败", ex.Message);
                throw;
            }
        }

        private bool ShouldSkipCsrfCheck(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            var method = context.Request.Method;
            _logger.Debug("[CSRF] Checking path: {Method} {Path}", method, path);

            // 如果路径为空，不跳过验证
            if (string.IsNullOrEmpty(path))
            {
                _logger.Debug("[CSRF] Path is empty, not skipping");
                return false;
            }

            // 处理查询参数
            var pathWithoutQuery = path.Split('?')[0];
            _logger.Debug("[CSRF] Path without query: {Path}", pathWithoutQuery);

            // 0. 开发环境CSRF验证（保持安全）
            var isDevelopment = context.RequestServices.GetService<IHostEnvironment>()?.IsDevelopment() ?? false;
            if (isDevelopment)
            {
                _logger.Debug("[CSRF] 开发环境，启用CSRF验证");
            }

            // 1. 检查是否是SignalR请求
            if (pathWithoutQuery.StartsWith("/signalr"))
            {
                _logger.Debug("[CSRF] Skipping SignalR path: {Path}", pathWithoutQuery);
                return true;
            }

            // 2. 检查是否是OPTIONS请求
            if (method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                _logger.Debug("[CSRF] Skipping OPTIONS request");
                return true;
            }

            // 3. 检查是否是其他需要跳过的路径（只跳过真正不需要CSRF保护的路径）
            var skipPaths = new[]
            {
                "/api/Taktauth/login",      // 登录接口
                "/api/Taktauth/logout",     // 登出接口
                "/api/Taktauth/refresh-token", // 刷新令牌接口
                "/api/Taktauth/check-login",   // 检查登录状态接口
                "/api/Taktloginmethod",        // 登录方式配置接口
                "/api/TaktSignalROnline/force-offline", // 强制下线接口
                "/api/Taktcaptcha",            // 验证码相关接口
                "/swagger",
                "/_framework",
                "/_vs"
            };

            foreach (var skipPath in skipPaths)
            {
                if (pathWithoutQuery.StartsWith(skipPath, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.Debug("[CSRF] Skipping path: {Path} matches pattern: {Pattern}", pathWithoutQuery, skipPath);
                    return true;
                }
            }

            _logger.Debug("[CSRF] Path requires CSRF validation: {Path}", pathWithoutQuery);
            return false;
        }

        private CookieOptions GetCookieOptions(HttpContext context)
        {
            var isDevelopment = context.RequestServices.GetService<IHostEnvironment>()?.IsDevelopment() ?? false;
            var isHttps = context.Request.IsHttps;
            
            // 智能Cookie配置
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,  // CSRF令牌需要前端JavaScript读取
                Path = "/",        // 全局路径
                MaxAge = TimeSpan.FromMinutes(_options.CsrfTokenExpirationMinutes),
                Domain = null,     // 使用当前域名
                IsEssential = true // 标记为必要Cookie
            };
            
            // 开发环境：确保Cookie能被前端正确读取
            if (isDevelopment)
            {
                cookieOptions.Secure = false;  // 开发环境不使用HTTPS要求
                cookieOptions.SameSite = SameSiteMode.Lax;  // 允许跨站请求携带Cookie
                cookieOptions.Domain = null;  // 使用当前域名
            }
            else
            {
                // 生产环境：严格设置
                cookieOptions.Secure = isHttps;  // 只有HTTPS时才设置Secure
                cookieOptions.SameSite = isHttps ? SameSiteMode.Strict : SameSiteMode.Lax;
            }
            
            // 添加调试日志
            _logger.Info("[CSRF] Cookie配置详情: HttpOnly={HttpOnly}, Secure={Secure}, SameSite={SameSite}, Path={Path}, MaxAge={MaxAge}, Domain={Domain}, IsEssential={IsEssential}",
                cookieOptions.HttpOnly, cookieOptions.Secure, cookieOptions.SameSite, 
                cookieOptions.Path, cookieOptions.MaxAge, cookieOptions.Domain, cookieOptions.IsEssential);
            
            return cookieOptions;
        }
    }
}




