namespace Takt.WebApi.Middlewares
{
    /// <summary>
    /// 大小写敏感路由中间件
    /// </summary>
    public class TaktCaseSensitiveRouteMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        public TaktCaseSensitiveRouteMiddleware(RequestDelegate next,
                ITaktLogger logger
                )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var routePattern = (endpoint as RouteEndpoint)?.RoutePattern.RawText;
                var requestPath = context.Request.Path.Value?.TrimStart('/');

                _logger.Info($"[路由中间件] 请求路径: {context.Request.Path.Value}, 路由模式: {routePattern}");

                if (!string.IsNullOrEmpty(routePattern) && !string.IsNullOrEmpty(requestPath))
                {
                    var routeParts = routePattern.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var requestParts = requestPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

                    // 确保有足够的部分进行比较
                    if (requestParts.Length >= 2 && routeParts.Length >= 2)
                    {
                        // 获取控制器名称
                        var routeController = routeParts[1];
                        if (routeController == "[controller]")
                        {
                            // 如果是[controller]占位符，从endpoint获取实际的控制器名称
                            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>();
                            if (controllerActionDescriptor != null)
                            {
                                routeController = controllerActionDescriptor.ControllerName;
                                _logger.Info($"[路由中间件] 控制器名称: {routeController}");
                            }
                        }

                        var requestController = requestParts[1];

                        // 严格大小写匹配控制器名称
                        if (requestController != routeController)
                        {
                            _logger.Warn($"[路由中间件] 控制器名称大小写不匹配. 期望: {routeController}, 实际: {requestController}");
                            context.Response.StatusCode = StatusCodes.Status404NotFound;
                            await context.Response.WriteAsJsonAsync(new { code = 404, msg = $"路由不存在，控制器名称应为: {routeController}" });
                            return;
                        }

                        // 如果路由包含action，比较action名称
                        if (routeParts.Length >= 3 && requestParts.Length >= 3)
                        {
                            var expectedAction = routeParts[2];
                            var actualAction = requestParts[2];

                            // 如果期望的action不是路由参数（不包含{和}），则进行严格大小写匹配
                            if (!expectedAction.Contains("{") && expectedAction != actualAction)
                            {
                                _logger.Warn($"[路由中间件] Action名称大小写不匹配. 期望: {expectedAction}, 实际: {actualAction}");
                                context.Response.StatusCode = StatusCodes.Status404NotFound;
                                await context.Response.WriteAsJsonAsync(new { code = 404, msg = $"路由不存在，Action名称应为: {expectedAction}" });
                                return;
                            }
                        }
                    }
                }
            }

            await _next(context);
        }
    }

    /// <summary>
    /// 中间件扩展方法
    /// </summary>
    public static class TaktCaseSensitiveRouteMiddlewareExtensions
    {
        public static IApplicationBuilder UseTaktCaseSensitiveRoute(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TaktCaseSensitiveRouteMiddleware>();
        }
    }
}
