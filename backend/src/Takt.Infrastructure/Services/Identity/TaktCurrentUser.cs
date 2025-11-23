//===================================================================
// 项目名 : Takt.Xp
// 文件名 : CurrentUser.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 当前用户实现类
//===================================================================

using System.Security.Claims;
using Takt.Shared.Exceptions;
using Takt.Shared.Constants;
using Takt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Takt.Infrastructure.Services.Identity
{
    /// <summary>
    /// 当前用户实现类
    /// </summary>
    /// <remarks>
    /// 用于获取当前登录用户的相关信息，包括：
    /// 1. 用户ID
    /// 2. 用户名
    /// 3. 认证状态
    /// 4. 用户类型
    /// 5. 英文名称
    /// 6. 角色和权限
    /// </remarks>
    public class TaktCurrentUser : ITaktCurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="logger">日志记录器</param>
        public TaktCurrentUser(IHttpContextAccessor httpContextAccessor, ITaktLogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <summary>
        /// 获取当前用户ID
        /// </summary>
        public long UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("uid");
                return userIdClaim != null ? long.Parse(userIdClaim.Value) : 0;
            }
        }

        /// <summary>
        /// 获取当前用户名
        /// </summary>
        public string UserName
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    _logger.Warn("TaktCurrentUser.UserName: HTTP上下文为空，返回默认值 Takt365");
                    return "Takt365";
                }

                var user = httpContext.User;
                if (user == null || !user.Identity?.IsAuthenticated == true)
                {
                    _logger.Warn("TaktCurrentUser.UserName: 用户未认证，返回默认值 Takt365");
                    return "Takt365";
                }

                var userNameClaim = user.FindFirst("unm");
                if (userNameClaim == null)
                {
                    _logger.Warn("TaktCurrentUser.UserName: 未找到 'unm' claim，返回默认值 Takt365");
                    return "Takt365";
                }

                var userName = userNameClaim.Value;
                _logger.Info("TaktCurrentUser.UserName: 成功获取用户名: {UserName}", userName);
                return userName;
            }
        }

        /// <summary>
        /// 获取当前用户昵称
        /// </summary>
        public string NickName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("nnm")?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取当前用户英文名称
        /// </summary>
        public string EnglishName
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirst("enm")?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// 获取当前用户类型
        /// </summary>
        public int UserType
        {
            get
            {
                var userTypeClaim = _httpContextAccessor.HttpContext?.User.FindFirst("typ");
                return userTypeClaim != null ? int.Parse(userTypeClaim.Value) : 1; // 默认为普通用户
            }
        }



        /// <summary>
        /// 获取当前用户角色列表
        /// </summary>
        public string[] Roles
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindAll("rol").Select(c => c.Value).ToArray() ?? Array.Empty<string>();
            }
        }

        /// <summary>
        /// 获取当前用户权限列表
        /// </summary>
        public string[] Permissions
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindAll("pms").Select(c => c.Value).ToArray() ?? Array.Empty<string>();
            }
        }

        /// <summary>
        /// 获取当前用户是否已认证
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
            }
        }

        /// <summary>
        /// 获取当前用户是否为超级管理员
        /// </summary>
        public bool IsAdmin
        {
            get
            {
                return Roles.Contains("Takt365");
            }
        }
    }
} 




