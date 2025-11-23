//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSessionSecurityMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 会话安全中间件
//===================================================================

using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Takt.Shared.Options;

namespace Takt.Infrastructure.Middleware
{
    /// <summary>
    /// 会话安全中间件,用于防止会话劫持
    /// </summary>
    public class TaktSessionSecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly TaktSecurityOptions _options;
        private const string SessionIdHeader = "X-Session-Id";
        private const string SessionTokenHeader = "X-Session-Token";

        /// <summary>
        /// 初始化会话安全中间件
        /// </summary>
        /// <param name="next">请求委托</param>
        /// <param name="cache">分布式缓存</param>
        /// <param name="options">安全配置选项</param>
        public TaktSessionSecurityMiddleware(
            RequestDelegate next,
            IDistributedCache cache,
            IOptions<TaktSecurityOptions> options)
        {
            _next = next;
            _cache = cache;
            _options = options.Value;
        }

        /// <summary>
        /// 调用中间件处理会话安全
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // 跳过验证码相关的请求
            if (IsCaptchaRequest(context))
            {
                await _next(context);
                return;
            }

            // 1. 获取会话ID和Token
            var sessionId = context.Request.Headers[SessionIdHeader].ToString();
            var sessionToken = context.Request.Headers[SessionTokenHeader].ToString();

            if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(sessionToken))
            {
                // 生成新的会话ID和Token
                (sessionId, sessionToken) = GenerateSessionCredentials();
                context.Response.Headers[SessionIdHeader] = sessionId;
                context.Response.Headers[SessionTokenHeader] = sessionToken;
            }
            else
            {
                // 验证会话
                if (!await ValidateSessionAsync(sessionId, sessionToken))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(new { message = "会话已失效" });
                    return;
                }

                // 检测会话劫持
                if (await DetectSessionHijackingAsync(context, sessionId))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsJsonAsync(new { message = "检测到异常访问" });
                    return;
                }

                // 更新会话状态
                await UpdateSessionStateAsync(context, sessionId);
            }

            await _next(context);
        }

        /// <summary>
        /// 生成会话凭证
        /// </summary>
        private (string sessionId, string sessionToken) GenerateSessionCredentials()
        {
            var sessionId = GenerateSecureToken();
            var sessionToken = GenerateSecureToken();
            return (sessionId, sessionToken);
        }

        /// <summary>
        /// 生成安全Token
        /// </summary>
        private string GenerateSecureToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 验证会话
        /// </summary>
        private async Task<bool> ValidateSessionAsync(string sessionId, string sessionToken)
        {
            var cachedToken = await _cache.GetStringAsync($"session:token:{sessionId}");
            return !string.IsNullOrEmpty(cachedToken) && cachedToken == sessionToken;
        }

        /// <summary>
        /// 检测会话劫持
        /// </summary>
        private async Task<bool> DetectSessionHijackingAsync(HttpContext context, string sessionId)
        {
            var cachedInfo = await _cache.GetStringAsync($"session:info:{sessionId}");
            if (string.IsNullOrEmpty(cachedInfo))
                return false;

            var currentInfo = GetClientInfo(context);
            return cachedInfo != currentInfo;
        }

        /// <summary>
        /// 更新会话状态
        /// </summary>
        private async Task UpdateSessionStateAsync(HttpContext context, string sessionId)
        {
            var clientInfo = GetClientInfo(context);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.SessionExpirationMinutes)
            };

            await _cache.SetStringAsync($"session:info:{sessionId}", clientInfo, options);
        }

        /// <summary>
        /// 获取客户端信息
        /// </summary>
        private string GetClientInfo(HttpContext context)
        {
            return $"{context.Connection.RemoteIpAddress}|{context.Request.Headers["User-Agent"]}";
        }

        /// <summary>
        /// 检查是否是验证码相关请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>是否是验证码请求</returns>
        private bool IsCaptchaRequest(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();
            return path != null && (path.Contains("/Taktcaptcha/") || path.Contains("/Taktloginmethod/"));
        }
    }
} 




