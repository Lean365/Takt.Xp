//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogActionFilter.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V.0.0.1
// 描述    : 操作日志过滤器
//===================================================================

using System.Text.Json;
using Takt.Domain.IServices.Extensions;
using Takt.Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Takt.Infrastructure.Security.Filters;

/// <summary>
/// 操作日志过滤器
/// </summary>
public class TaktLogActionFilter : IAsyncActionFilter
{
    private readonly ITaktOperLogManager _logManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITaktCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLogActionFilter(
        ITaktOperLogManager logManager,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser)
    {
        _logManager = logManager;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
    }

    /// <summary>
    /// 执行过滤器
    /// </summary>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logAttr = context.ActionDescriptor.EndpointMetadata.OfType<TaktLogAttribute>().FirstOrDefault();
        if (logAttr == null)
        {
            await next();
            return;
        }

        try
        {
            await next();
            var result = context.Result;
            
            // 记录操作日志
            await LogOperationAsync(context, logAttr, true);
        }
        catch (Exception ex)
        {
            await LogOperationAsync(context, logAttr, false, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    private async Task LogOperationAsync(ActionExecutingContext context, TaktLogAttribute logAttr, bool success, string? errorMsg = null)
    {
        try
        {
            var tableName = GetTableName(context);
            var OperType = GetOperType(context);
            var businessKey = GetBusinessKey(context);
            var requestParam = GetRequestParam(context);

            var location = GetLocation(context);
            
            await _logManager.LogOperationAsync(
                tableName: tableName,
                OperType: OperType,
                businessKey: businessKey,
                requestParam: requestParam,
                location: location,
                success: success,
                errorMsg: errorMsg
            );
        }
        catch (Exception ex)
        {
            // 记录日志失败不应该影响主业务流程
            // 只记录到系统日志中，不抛出异常
            try
            {
                var logger = context.HttpContext.RequestServices.GetService<ITaktLogger>();
                logger?.Error($"操作日志记录失败: {ex.Message}", ex);
            }
            catch
            {
                // 忽略所有错误，确保不影响主业务流程
            }
        }
    }

    /// <summary>
    /// 获取表名
    /// </summary>
    private string GetTableName(ActionExecutingContext context)
    {
        var controllerType = context.Controller.GetType();
        var TransType = controllerType.Name.Replace("Controller", "");
        return TransType;
    }

    /// <summary>
    /// 获取操作类型
    /// </summary>
    private string GetOperType(ActionExecutingContext context)
    {
        var methodName = context.ActionDescriptor.RouteValues["action"]?.ToLower() ?? string.Empty;
        if (methodName.StartsWith("create") || methodName.StartsWith("add"))
            return "create";
        if (methodName.StartsWith("update") || methodName.StartsWith("modify"))
            return "update";
        if (methodName.StartsWith("delete") || methodName.StartsWith("remove"))
            return "delete";
        if (methodName.StartsWith("get") || methodName.StartsWith("query"))
            return "query";
        return "other";
    }

    /// <summary>
    /// 获取业务主键
    /// </summary>
    private string GetBusinessKey(ActionExecutingContext context)
    {
        var args = context.ActionArguments;
        if (args.Count == 0) return string.Empty;

        // 尝试从参数中获取各种可能的主键
        var primaryKeyNames = new[] { "Id", "ID", "id", "UserId", "RoleId", "MenuId", "DeptId", "PostId" };
        
        foreach (var arg in args.Values)
        {
            if (arg == null) continue;

            foreach (var keyName in primaryKeyNames)
            {
                var idProperty = arg.GetType().GetProperty(keyName);
                if (idProperty != null)
                {
                    var idValue = idProperty.GetValue(arg);
                    if (idValue != null && !string.IsNullOrEmpty(idValue.ToString()))
                        return idValue.ToString() ?? string.Empty;
                }
            }
        }

        // 如果没有找到主键，尝试从路由参数获取
        var routeValues = context.RouteData.Values;
        if (routeValues.ContainsKey("id"))
        {
            var routeId = routeValues["id"]?.ToString();
            if (!string.IsNullOrEmpty(routeId))
                return routeId;
        }

        return string.Empty;
    }

    /// <summary>
    /// 获取请求参数
    /// </summary>
    private string GetRequestParam(ActionExecutingContext context)
    {
        try
        {
            var args = context.ActionArguments;
            if (args.Count == 0) return string.Empty;

            var safeParams = new Dictionary<string, object>();
            foreach (var kvp in args)
            {
                if (kvp.Value == null) continue;
                
                var paramName = kvp.Key;
                var paramValue = kvp.Value;
                
                // 过滤敏感信息
                if (IsSensitiveParameter(paramName, paramValue))
                {
                    safeParams[paramName] = "[已过滤]";
                }
                else
                {
                    // 只记录关键字段，避免记录整个对象
                    safeParams[paramName] = ExtractKeyFields(paramValue);
                }
            }

            return JsonConvert.SerializeObject(safeParams);
        }
        catch
        {
            return string.Empty;
        }
    }
    
    /// <summary>
    /// 检查是否为敏感参数
    /// </summary>
    private bool IsSensitiveParameter(string paramName, object paramValue)
    {
        var sensitiveNames = new[] { "password", "Password", "PASSWORD", "token", "Token", "TOKEN", "secret", "Secret", "SECRET" };
        var sensitiveTypes = new[] { "string", "System.String" };
        
        return sensitiveNames.Any(name => paramName.Contains(name)) ||
               sensitiveTypes.Contains(paramValue.GetType().Name);
    }
    
    /// <summary>
    /// 提取关键字段
    /// </summary>
    private object ExtractKeyFields(object obj)
    {
        try
        {
            if (obj == null) return null;
            
            var objType = obj.GetType();
            
            // 如果是基本类型，直接返回
            if (objType.IsPrimitive || objType == typeof(string) || objType == typeof(DateTime))
                return obj;
            
            // 如果是复杂对象，智能提取字段
            var result = new Dictionary<string, object>();
            var properties = objType.GetProperties();
            
            foreach (var property in properties)
            {
                try
                {
                    // 跳过复杂类型和集合类型
                    if (ShouldSkipProperty(property))
                        continue;
                    
                    var value = property.GetValue(obj);
                    if (value != null && !IsDefaultValue(value, property.PropertyType))
                    {
                        result[property.Name] = value;
                    }
                }
                catch
                {
                    // 忽略单个属性访问错误
                    continue;
                }
            }
            
            // 如果提取的字段太多，只保留前10个
            if (result.Count > 10)
            {
                var limitedResult = new Dictionary<string, object>();
                var count = 0;
                foreach (var kvp in result)
                {
                    if (count >= 10) break;
                    limitedResult[kvp.Key] = kvp.Value;
                    count++;
                }
                return limitedResult;
            }
            
            return result.Count > 0 ? result : obj.ToString();
        }
        catch
        {
            return obj.ToString();
        }
    }
    
    /// <summary>
    /// 判断是否应该跳过某个属性
    /// </summary>
    private bool ShouldSkipProperty(System.Reflection.PropertyInfo property)
    {
        // 跳过只读属性
        if (!property.CanRead) return true;
        
        // 跳过复杂类型
        var propertyType = property.PropertyType;
        if (propertyType.IsClass && 
            propertyType != typeof(string) && 
            propertyType != typeof(DateTime) &&
            propertyType != typeof(DateTimeOffset) &&
            propertyType != typeof(Guid) &&
            propertyType != typeof(TimeSpan))
            return true;
        
        // 跳过集合类型
        if (propertyType.IsGenericType && 
            (propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
             propertyType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
             propertyType.GetGenericTypeDefinition() == typeof(List<>) ||
             propertyType.GetGenericTypeDefinition() == typeof(Array)))
            return true;
        
        // 跳过导航属性（通常以特定模式命名）
        var propertyName = property.Name;
        if (propertyName.EndsWith("s") || // 复数形式
            propertyName.EndsWith("List") ||
            propertyName.EndsWith("Collection") ||
            propertyName.EndsWith("Items"))
            return true;
        
        return false;
    }
    
    /// <summary>
    /// 判断是否为默认值
    /// </summary>
    private bool IsDefaultValue(object value, Type type)
    {
        if (value == null) return true;
        
        if (type == typeof(string))
            return string.IsNullOrEmpty((string)value);
        
        if (type == typeof(int) || type == typeof(long) || type == typeof(short))
            return Convert.ToInt64(value) == 0;
        
        if (type == typeof(double) || type == typeof(float) || type == typeof(decimal))
            return Convert.ToDouble(value) == 0.0;
        
        if (type == typeof(bool))
            return (bool)value == false;
        
        if (type == typeof(DateTime))
            return (DateTime)value == default(DateTime);
        
        if (type == typeof(Guid))
            return (Guid)value == Guid.Empty;
        
        return false;
    }
    
    /// <summary>
    /// 获取操作位置
    /// </summary>
    private string GetLocation(ActionExecutingContext context)
    {
        try
        {
            var controllerName = context.Controller.GetType().Name.Replace("Controller", "");
            var actionName = context.ActionDescriptor.RouteValues["action"] ?? "Unknown";
            var area = context.RouteData.Values.ContainsKey("area") ? context.RouteData.Values["area"]?.ToString() : "";
            
            var location = string.IsNullOrEmpty(area) ? $"{controllerName}.{actionName}" : $"{area}.{controllerName}.{actionName}";
            return location;
        }
        catch
        {
            return "Unknown";
        }
    }
} 



