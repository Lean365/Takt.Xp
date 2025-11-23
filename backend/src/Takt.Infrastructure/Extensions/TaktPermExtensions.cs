//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPermExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 权限中间件扩展方法
//===================================================================

using Takt.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 权限中间件扩展方法
    /// </summary>
    public static class TaktPermExtensions
    {
        /// <summary>
        /// 使用权限中间件
        /// </summary>
        /// <param name="app">应用程序构建器</param>
        /// <returns>应用程序构建器</returns>
        public static IApplicationBuilder UseTaktPerm(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TaktPermMiddleware>();
        }
    }
}



