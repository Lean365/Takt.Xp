//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIdentityCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2025-01-11
// 版本号 : V0.0.1
// 描述    : 身份认证服务集合扩展
//===================================================================

using Takt.Application.Services.Identity;
using Takt.Domain.IServices.Security;
using Takt.Infrastructure.Authentication;
using Takt.Infrastructure.Identity;
using Takt.Infrastructure.Security;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 身份认证服务集合扩展
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册身份认证相关的所有服务，包括：
    /// 1. 核心身份认证服务 - 登录、JWT处理、设备管理等
    /// 2. 短信和二维码认证服务 - 短信验证、二维码登录等
    /// 3. 用户和权限管理服务 - 用户、角色、部门、岗位、菜单等
    /// </remarks>
    public static class TaktIdentityCollectionExtensions
    {
        /// <summary>
        /// 添加身份认证服务
        /// </summary>
        /// <remarks>
        /// 注册身份认证相关的所有服务，包括：
        /// 1. 核心身份认证服务 - 登录、JWT处理、设备管理等
        /// 2. 短信和二维码认证服务 - 短信验证、二维码登录等
        /// 3. 用户和权限管理服务 - 用户、角色、部门、岗位、菜单等
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            // 核心身份认证服务
            services.AddScoped<ITaktAuthService, TaktAuthService>();         // 登录服务

            services.AddScoped<ITaktJwtHandler, TaktJwtHandler>();            // JWT令牌处理
            services.AddScoped<ITaktDeviceIdGenerator, TaktDeviceIdGenerator>(); // 设备ID生成器





            // 短信和二维码认证服务已移除


            // 用户和权限管理
            services.AddScoped<ITaktUserService, TaktUserService>();          // 用户管理
            services.AddScoped<ITaktRoleService, TaktRoleService>();          // 角色管理
            services.AddScoped<ITaktDeptService, TaktDeptService>();          // 部门管理
            services.AddScoped<ITaktPostService, TaktPostService>();          // 岗位管理
            services.AddScoped<ITaktMenuService, TaktMenuService>();          // 菜单管理

            // 安全策略服务
            services.AddScoped<ITaktLoginPolicy, TaktLoginPolicy>();          // 登录策略
            services.AddScoped<ITaktPasswordPolicy, TaktPasswordPolicy>();    // 密码策略

            // 会话管理服务
            services.AddScoped<ITaktIdentitySessionManager, TaktSessionManager>(); // 身份会话管理器
            services.AddScoped<ITaktSingleUserLoginService, TaktSingleUserLoginService>(); // 单用户登录服务

            // 验证服务
            services.AddScoped<ITaktCaptchaService, TaktCaptchaService>();    // 验证码服务
            // OAuth服务已移除

            // 找回密码服务
            services.AddScoped<ITaktForgetService, TaktForgetService>();      // 找回密码服务

            return services;
        }
    }
}


