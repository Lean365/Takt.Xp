//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPermMiddleware.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 权限中间件
//===================================================================

using Takt.Domain.Entities.Identity;
using Takt.Infrastructure.Security;
using Takt.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Takt.Infrastructure.Middleware
{
    /// <summary>
    /// 权限中间件,用于验证用户是否拥有访问权限
    /// </summary>
    public class TaktPermMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 服务提供者
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">请求委托</param>
        /// <param name="logger">日志记录器</param>
        /// <param name="serviceProvider">服务提供者</param>
        public TaktPermMiddleware(RequestDelegate next,
            ITaktLogger logger,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>异步任务</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path.Value;
            var requestMethod = context.Request.Method;
            var userAgent = context.Request.Headers.UserAgent.ToString();
            var clientIp = GetClientIpAddress(context);
            
            _logger.Debug("[权限中间件] 开始处理请求: {Path} {Method} | IP: {ClientIp} | UserAgent: {UserAgent}", 
                requestPath, requestMethod, clientIp, userAgent);

            // 跳过验证码相关的请求
            if (IsCaptchaRequest(context))
            {
                _logger.Debug("[权限中间件] 验证码请求，跳过权限验证: {Path}", requestPath);
                await _next(context);
                return;
            }

            // 获取当前请求的Endpoint
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                _logger.Debug("[权限中间件] 未找到Endpoint,跳过权限验证: {Path}", requestPath);
                await _next(context);
                return;
            }

            // 检查是否有AllowAnonymous特性
            var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
            if (allowAnonymous != null)
            {
                _logger.Debug("[权限中间件] 发现AllowAnonymous特性,完全跳过权限验证: {Path}", requestPath);
                await _next(context);
                return;
            }

            // 获取权限特性
            var permAttribute = endpoint.Metadata.GetMetadata<TaktPermAttribute>();
            if (permAttribute == null)
            {
                _logger.Debug("[权限中间件] 未找到权限特性,跳过权限验证: {Path}", requestPath);
                await _next(context);
                return;
            }

            _logger.Debug("[权限中间件] 需要的权限: {Permission} | Path: {Path} | Method: {Method}", 
                permAttribute.Permission, requestPath, requestMethod);

            // 检查用户是否已认证
            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                _logger.Warn("[权限中间件] 用户未认证,返回401 | Path: {Path} | Method: {Method} | IP: {ClientIp}", 
                    requestPath, requestMethod, clientIp);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "未认证" });
                return;
            }

            // 仅从Claims中获取用户ID
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "uid");
            if (userIdClaim == null)
            {
                _logger.Warn("[权限中间件] 未找到uid Claim | Path: {Path} | Method: {Method} | IP: {ClientIp}", 
                    requestPath, requestMethod, clientIp);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "无效的认证信息" });
                return;
            }

            if (!long.TryParse(userIdClaim.Value, out long userId))
            {
                _logger.Warn("[权限中间件] uid Claim格式无效: {Value} | Path: {Path} | Method: {Method} | IP: {ClientIp}", 
                    userIdClaim.Value, requestPath, requestMethod, clientIp);
                context.Response.StatusCode = 401;
                await context.Response.WriteAsJsonAsync(new { message = "无效的用户ID" });
                return;
            }

            var isAdmin = context.User.Claims.Any(c => c.Type == "adm" && c.Value == "true");
            var userName = context.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "未知用户";

            _logger.Debug("[权限中间件] 用户信息: UserId={UserId}, UserName={UserName}, IsAdmin={IsAdmin} | Path: {Path}", 
                userId, userName, isAdmin, requestPath);

            if (isAdmin)
            {
                _logger.Debug("[权限中间件] 用户是管理员，自动通过权限验证 | UserId: {UserId} | Path: {Path}", 
                    userId, requestPath);
                await _next(context);
                return;
            }

            // 从请求作用域获取仓储工厂
            var repositoryFactory = context.RequestServices.GetRequiredService<ITaktRepositoryFactory>();
            var userRepository = repositoryFactory.GetAuthRepository<TaktUser>();

            // 直接获取用户权限列表
            var permissions = await userRepository.GetUserPermissionsAsync(userId);

            _logger.Debug("[权限中间件] 用户 {UserId} 拥有的权限数量: {PermissionCount} | Path: {Path}", 
                userId, permissions.Count, requestPath);

            // 检查具体权限
            var hasPermission = permissions.Contains(permAttribute.Permission);
            
            if (hasPermission)
            {
                _logger.Info("[权限中间件] 权限验证通过 | UserId: {UserId} | UserName: {UserName} | 权限: {Permission} | Path: {Path} | Method: {Method} | IP: {ClientIp}", 
                    userId, userName, permAttribute.Permission, requestPath, requestMethod, clientIp);
            }
                         else
             {
                 // 记录权限验证失败 - 从请求作用域获取权限日志记录器
                 var permissionLogger = context.RequestServices.GetRequiredService<TaktPermissionLogger>();
                 await permissionLogger.LogPermissionCheckFailure(permAttribute.Permission, requestPath, requestMethod, 
                     permissions, userId, userName);
                 
                 context.Response.StatusCode = 403;
                 await context.Response.WriteAsJsonAsync(new { 
                     message = "无访问权限", 
                     requiredPermission = permAttribute.Permission,
                     userId,
                     path = requestPath,
                     method = requestMethod
                 });
                 return;
             }

            await _next(context);
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

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <param name="context">HTTP上下文</param>
        /// <returns>客户端IP地址</returns>
        private string GetClientIpAddress(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                     context.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                     context.Connection.RemoteIpAddress?.ToString() ??
                     "未知IP";
            
            return ip;
        }
    }
}



