//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPermissionLogger.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述    : 权限日志记录器
//===================================================================

using Takt.Domain.IServices.Extensions;
using Takt.Domain.Entities.Logging;
using Takt.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Takt.Infrastructure.Security
{
    /// <summary>
    /// 权限日志记录器
    /// </summary>
    public class TaktPermissionLogger
    {
        private readonly ITaktLogger _logger;
        private readonly ITaktDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="context">数据库上下文</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        public TaktPermissionLogger(
            ITaktLogger logger,
            ITaktDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 记录权限验证失败
        /// </summary>
        /// <param name="requiredPermission">需要的权限</param>
        /// <param name="path">请求路径</param>
        /// <param name="method">请求方法</param>
        /// <param name="userPermissions">用户拥有的权限</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        public async Task LogPermissionCheckFailure(string requiredPermission, string path, string method, 
            List<string> userPermissions, long userId, string userName)
        {
            // 记录权限不足的详细信息到控制台日志
            _logger.Warn("[权限验证] 权限不足 | 用户: {UserId}({UserName}) | 缺少权限: {Permission} | 路径: {Path} | 方法: {Method}",
                userId, userName, requiredPermission, path, method);

            // 记录用户权限信息（用于调试）
            var userPermissionsStr = string.Join(", ", userPermissions.Take(10));
            _logger.Error("[权限验证] 用户权限详情 | 用户: {UserId} | 权限数量: {Count} | 权限列表: {Permissions}",
                userId, userPermissions.Count, userPermissionsStr);

            // 记录到异常日志表
            await LogPermissionExceptionToDatabase(requiredPermission, path, method, userPermissions, userId, userName);
        }

        /// <summary>
        /// 记录权限验证成功
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="path">请求路径</param>
        /// <param name="method">请求方法</param>
        public void LogPermissionCheckSuccess(string permission, string path, string method)
        {
            _logger.Info("[权限验证] 权限验证通过 | 权限: {Permission} | 路径: {Path} | 方法: {Method}",
                permission, path, method);
        }

        /// <summary>
        /// 记录权限验证跳过
        /// </summary>
        /// <param name="reason">跳过原因</param>
        /// <param name="path">请求路径</param>
        /// <param name="method">请求方法</param>
        public void LogPermissionCheckSkipped(string reason, string path, string method)
        {
            _logger.Debug("[权限验证] 跳过验证 | 原因: {Reason} | 路径: {Path} | 方法: {Method}",
                reason, path, method);
        }

        /// <summary>
        /// 记录管理员权限验证
        /// </summary>
        /// <param name="path">请求路径</param>
        /// <param name="method">请求方法</param>
        public void LogAdminPermissionCheck(string path, string method)
        {
            _logger.Debug("[权限验证] 管理员权限验证 | 路径: {Path} | 方法: {Method}", path, method);
        }

        /// <summary>
        /// 将权限异常记录到数据库
        /// </summary>
        private async Task LogPermissionExceptionToDatabase(string requiredPermission, string path, string method, 
            List<string> userPermissions, long userId, string userName)
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                var clientIp = GetClientIpAddress(context);
                var userAgent = context?.Request.Headers.UserAgent.ToString() ?? "未知";

                // 构建异常消息
                var exceptionMessage = $"用户 {userName}(ID:{userId}) 缺少权限 '{requiredPermission}' 访问 {method} {path}";
                
                // 构建参数信息
                var parameters = JsonConvert.SerializeObject(new
                {
                    RequiredPermission = requiredPermission,
                    UserPermissions = userPermissions.Take(20).ToList(),
                    UserPermissionCount = userPermissions.Count,
                    RequestPath = path,
                    RequestMethod = method
                });

                var exceptionLog = new TaktExceptionLog
                {
                    LogLevel = 3, // Warning级别
                    UserId = userId,
                    UserName = userName,
                    Method = $"{method} {path}",
                    Parameters = parameters,
                    ExceptionType = "PermissionDeniedException",
                    ExceptionMessage = exceptionMessage,
                    StackTrace = null, // 权限不足不是异常堆栈
                    IpAddress = clientIp,
                    UserAgent = userAgent,
                    CreateBy = userName,
                    CreateTime = DateTime.Now
                };

                await _context.Client.Insertable(exceptionLog).ExecuteCommandAsync();
                _logger.Info("[权限验证] 权限异常已记录到数据库 | 用户: {UserId} | 权限: {Permission}", 
                    userId, requiredPermission);
            }
            catch (Exception ex)
            {
                _logger.Error("[权限验证] 记录权限异常到数据库失败 | 用户: {UserId} | 权限: {Permission}", ex);
            }
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        private string GetClientIpAddress(HttpContext? context)
        {
            if (context == null) return "未知IP";

            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                     context.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                     context.Connection.RemoteIpAddress?.ToString() ??
                     "未知IP";
            
            return ip;
        }
    }
}



