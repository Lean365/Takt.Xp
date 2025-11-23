//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRateLimitMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 限流中间件
//===================================================================

using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http;

namespace Takt.Infrastructure.Middleware
{
    /// <summary>
    /// 限流中间件,基于令牌桶算法实现
    /// </summary>
    public class TaktRateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, TokenBucket> _buckets = new();
        private static readonly ConcurrentDictionary<string, TokenBucket> _captchaBuckets = new();

        private const int DEFAULT_CAPACITY = 100;
        private const int DEFAULT_REFILL_RATE = 10;
        private const int CAPTCHA_CAPACITY = 20;
        private const int CAPTCHA_REFILL_RATE = 5;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        public TaktRateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>异步任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var path = context.Request.Path.Value ?? "";
            
            // 验证码接口使用单独的限流规则，不区分大小写
            var isCaptchaRequest = path.Contains("/TaktCaptcha/", StringComparison.OrdinalIgnoreCase);

            var bucket = isCaptchaRequest
                ? _captchaBuckets.GetOrAdd(ip, _ => new TokenBucket(CAPTCHA_CAPACITY, CAPTCHA_REFILL_RATE))
                : _buckets.GetOrAdd(ip, _ => new TokenBucket(DEFAULT_CAPACITY, DEFAULT_REFILL_RATE));

            if (!bucket.TryTake())
            {
                context.Response.StatusCode = 429;
                var remainingSeconds = bucket.GetRefillTime();
                await context.Response.WriteAsJsonAsync(new { 
                    message = "请求过于频繁,请稍后再试",
                    remainingSeconds
                });
                return;
            }

            await _next(context);
        }

        /// <summary>
        /// 令牌桶
        /// </summary>
        private class TokenBucket
        {
            private readonly int _capacity;
            private readonly int _refillRate;
            private double _tokens;
            private DateTime _lastRefill;
            private readonly object _lock = new();

            public TokenBucket(int capacity, int refillRate)
            {
                _capacity = capacity;
                _refillRate = refillRate;
                _tokens = capacity;
                _lastRefill = DateTime.UtcNow;
            }

            public bool TryTake()
            {
                lock (_lock)
                {
                    RefillTokens();
                    if (_tokens < 1) return false;
                    _tokens--;
                    return true;
                }
            }

            public int GetRefillTime()
            {
                lock (_lock)
                {
                    RefillTokens();
                    if (_tokens >= 1) return 0;
                    return (int)Math.Ceiling((1 - _tokens) / _refillRate);
                }
            }

            private void RefillTokens()
            {
                var now = DateTime.UtcNow;
                var elapsed = (now - _lastRefill).TotalSeconds;
                var tokensToAdd = elapsed * _refillRate;
                _tokens = Math.Min(_capacity, _tokens + tokensToAdd);
                _lastRefill = now;
            }
        }
    }
} 



