//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktJwtCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2024-12-01 15:00
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : JWT认证服务集合扩展
//===================================================================

using Takt.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// JWT认证服务集合扩展
/// </summary>
public static class TaktJwtCollectionExtensions
{
    /// <summary>
    /// 添加JWT认证服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // 配置 JWT 认证
        var jwtSettings = configuration.GetSection("Jwt").Get<TaktJwtOptions>()
            ?? throw new InvalidOperationException("JWT配置节点不能为空");

        services.Configure<TaktJwtOptions>(configuration.GetSection("Jwt"));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    NameClaimType = "unm",
                    RoleClaimType = "rol",
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateTokenReplay = true,
                    RequireAudience = true,
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };

                // 添加自定义的声明类型映射
                options.MapInboundClaims = false;

                // 添加 JWT 事件处理
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ITaktLogger>();
                        var claims = context.Principal?.Claims.ToList();
                        //logger.Info("JWT认证成功，所有声明: {@Claims}", claims?.Select(c => new { c.Type, c.Value }));

                        var userName = context.Principal?.FindFirst("unm")?.Value;

                        //logger.Info("JWT认证成功: UserId={UserId}, UserName={UserName}",
                        //    context.Principal?.FindFirst("uid")?.Value,
                        //    userName);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ITaktLogger>();
                        logger.Error("JWT认证失败: {Message}, Exception: {Exception}",
                            context.Exception.Message,
                            context.Exception);
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ITaktLogger>();

                        // 从 Authorization 头获取 JWT Bearer Token（前后端分离项目标准做法）
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                        {
                            var token = authHeader.Substring("Bearer ".Length).Trim();
                            context.Token = token;
                            //logger.Info("从Authorization头获取JWT Token: {Token}", token.Substring(0, Math.Min(20, token.Length)));
                            return Task.CompletedTask;
                        }

                        // 处理 SignalR 的 JWT 认证
                        var path = context.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/signalr/Takthub"))
                        {
                            // SignalR 也使用 Authorization 头
                            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                            {
                                var token = authHeader.Substring("Bearer ".Length).Trim();
                                context.Token = token;
                                //logger.Info("SignalR 从Authorization头获取JWT Token: {Token}", token.Substring(0, Math.Min(20, token.Length)));
                            }
                        }

                        //logger.Info("收到JWT认证请求: Path={Path}, Token={Token}",
                        //    context.Request.Path,
                        //    context.Token?.Substring(0, Math.Min(20, context.Token?.Length ?? 0)));

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ITaktLogger>();
                        logger.Warn("JWT认证挑战: {Error}, {ErrorDescription}",
                            context.Error, context.ErrorDescription);
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }

    // JWT Bearer Token 模式，无需 Cookie 管理方法
}



