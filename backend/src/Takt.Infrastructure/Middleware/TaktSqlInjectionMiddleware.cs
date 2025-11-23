//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSqlInjectionMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : SQL注入防护中间件
//===================================================================

using System.Text.RegularExpressions;
using Takt.Shared.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Middleware
{
    /// <summary>
    /// SQL注入防护中间件
    /// </summary>
    public class TaktSqlInjectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TaktSecurityOptions _options;

        private static readonly Regex SqlInjectionPattern = new(
            @"(\b(select|insert|update|delete|drop|truncate|exec|declare|union|create|alter)\b\s+[\w\*])|(--)|(\/\*)|(\b(and|or)\b\s+\w+\s*=)|(\b(xp_cmdshell|sp_executesql)\b)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public TaktSqlInjectionMiddleware(
            RequestDelegate next,
            IOptions<TaktSecurityOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        /// 调用中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // 1. 检查是否需要跳过SQL注入检查
            if (ShouldSkipSqlInjectionCheck(context))
            {
                await _next(context);
                return;
            }

            // 2. 检查请求方法
            if (context.Request.Method != "GET")
            {
                // 3. 检查是否是文件上传请求
                if (IsFileUploadRequest(context))
                {
                    await _next(context);
                    return;
                }

                // 4. 读取请求体
                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                // 5. 检查请求参数
                if (ContainsSqlInjection(body) ||
                    ContainsSqlInjection(context.Request.QueryString.Value))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { message = "检测到潜在的SQL注入攻击" });
                    return;
                }
            }
            else
            {
                // 检查查询字符串
                if (ContainsSqlInjection(context.Request.QueryString.Value))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new { message = "检测到潜在的SQL注入攻击" });
                    return;
                }
            }

            await _next(context);
        }

        /// <summary>
        /// 检查是否是文件上传请求
        /// </summary>
        private bool IsFileUploadRequest(HttpContext context)
        {
            var contentType = context.Request.ContentType;
            if (string.IsNullOrEmpty(contentType))
                return false;

            return contentType.Contains("multipart/form-data") || 
                   contentType.Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        /// <summary>
        /// 检查是否需要跳过SQL注入检查
        /// </summary>
        private bool ShouldSkipSqlInjectionCheck(HttpContext context)
        {
            // 1. 跳过登录和认证相关的路径
            var path = context.Request.Path.Value?.ToLower();
            if (path != null && (
                path.Contains("/login") ||
                path.Contains("/auth") ||
                path.Contains("/token") ||
                path.Contains("/Taktcaptcha") ||
                path.Contains("/Taktloginmethod")))
            {
                return true;
            }

            // 2. 跳过静态文件
            if (path != null && (
                path.EndsWith(".js") ||
                path.EndsWith(".css") ||
                path.EndsWith(".html") ||
                path.EndsWith(".jpg") ||
                path.EndsWith(".png") ||
                path.EndsWith(".gif")))
            {
                return true;
            }

            // 3. 跳过文件上传接口
            if (path != null && path.Contains("/import"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 检查是否包含SQL注入攻击
        /// </summary>
        private bool ContainsSqlInjection(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // 1. 检查SQL关键字
            if (SqlInjectionPattern.IsMatch(input))
                return true;

            // 2. 检查特殊字符组合
            if (input.Contains("--") || 
                input.Contains("/*") || 
                input.Contains("*/") ||
                input.Contains("@@") ||
                input.Contains("char(") ||
                input.Contains("convert(") ||
                input.Contains("concat("))
                return true;

            // 3. 检查Unicode编码
            if (input.Contains("\\u") || input.Contains("%u"))
                return true;

            return false;
        }
    }
}




