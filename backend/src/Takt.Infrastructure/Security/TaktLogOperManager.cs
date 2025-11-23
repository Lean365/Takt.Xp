//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogOperManager.cs
// 创建者 : Claude
// 创建时间: 2025-08-20
// 版本号 : V.0.0.1
// 描述    : 操作日志管理器
//===================================================================

using Takt.Domain.Entities.Logging;
using Takt.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json; // Added for JsonConvert

namespace Takt.Infrastructure.Security;

/// <summary>
/// 操作日志管理器
/// </summary>
public class TaktLogOperManager : ITaktOperLogManager
{
    private readonly ITaktLogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITaktCurrentUser _currentUser;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLogOperManager(
        ITaktLogger logger,
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;
        _configuration = configuration;
    }

    /// <summary>
    /// 记录操作日志
    /// </summary>
    public async Task LogOperationAsync(
        string tableName,
        string OperType,
        string businessKey,
        string? requestParam = null,
        string? location = null,
        bool success = true,
        string? errorMsg = null)
    {
        // 记录操作开始时间
        var startTime = DateTime.UtcNow;

        try
        {
            // 生成雪花ID
            var id = SnowFlakeSingle.Instance.NextId();
            
            var log = new TaktOperLog
            {
                Id = id, // 设置雪花ID
                OperBy = _currentUser?.UserName ?? "System", // 操作人
                OperTitle = GetOperTitle(OperType, tableName),
                OperType = OperType,
                OperModule = GetOperModule(tableName),
                OperLocation = location,
                OperTableName = tableName,
                OperBusinessKey = businessKey,
                OperRequestMethod = _httpContextAccessor.HttpContext?.Request.Method ?? "Unknown",
                OperRequestParam = requestParam,
                OperResponseParam = success ? "操作成功" : $"操作失败: {errorMsg}",
                OperDuration = 0, // 先设置为0，后续计算
                OperStatus = success ? 0 : 1,
                OperErrorMsg = errorMsg,
                OperIpAddress = GetClientIpAddress(),
                OperUserAgent = GetUserAgent(),
                OperRequestUrl = GetRequestUrl(),
                OperReferer = GetReferer(),
                CreateBy = _currentUser?.UserName ?? "System",
                CreateTime = DateTime.UtcNow
            };

            // 从服务提供器获取身份数据库上下文
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TaktIdentityDBContext>();
            
            _logger.Debug($"开始记录操作日志: 表名={tableName}, 操作类型={OperType}, 业务主键={businessKey}");
            
            // 计算实际耗时
            var actualDuration = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
            log.OperDuration = Math.Max(actualDuration, 1); // 确保至少为1毫秒
            
            await context.Client.Insertable(log).ExecuteCommandAsync();
            _logger.Info($"记录操作日志成功: {_currentUser?.UserName ?? "System"} 在 {tableName} 执行了 {OperType} 操作，耗时={log.OperDuration}ms");
        }
        catch (Exception ex)
        {
            _logger.Error($"记录操作日志失败: 表名={tableName}, 操作类型={OperType}, 业务主键={businessKey}, 错误={ex.Message}", ex);
            
            // 不抛出异常，避免影响主业务流程
            // 但记录到系统日志中
            try
            {
                _logger.Error($"操作日志记录失败详情: {JsonConvert.SerializeObject(new { tableName, OperType, businessKey, error = ex.Message })}");
            }
            catch
            {
                // 忽略序列化错误
            }
        }
    }

    /// <summary>
    /// 获取操作标题（新增、删除、更新+表名）
    /// </summary>
    private string GetOperTitle(string OperType, string tableName)
    {
        var typeText = OperType.ToLower() switch
        {
            "create" or "insert" or "add" => "新增",
            "update" or "modify" or "edit" or "alter" => "修改",
            "delete" or "remove" or "drop" => "删除",
            "query" or "select" or "get" => "查询",
            _ => OperType
        };

        return $"{typeText}{tableName}";
    }

    /// <summary>
    /// 获取操作模块（实体所在目录名称）
    /// </summary>
    private string GetOperModule(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
            return tableName;

        // 直接返回表名，保持简单
        return tableName;
    }

    /// <summary>
    /// 获取请求URL
    /// </summary>
    private string? GetRequestUrl()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return null;

        var path = context.Request.Path.Value;
        var query = context.Request.QueryString.Value;

        if (string.IsNullOrEmpty(query))
            return path;

        return path + query;
    }

    /// <summary>
    /// 获取请求来源
    /// </summary>
    private string? GetReferer()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.Request.Headers["Referer"].ToString();
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    private string GetClientIpAddress()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return "127.0.0.1";

        var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                 context.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                 context.Connection.RemoteIpAddress?.ToString();

        return ip ?? "127.0.0.1";
    }

    /// <summary>
    /// 获取客户端UserAgent
    /// </summary>
    private string GetUserAgent()
    {
        var context = _httpContextAccessor.HttpContext;
        return context?.Request.Headers["User-Agent"].ToString() ?? "Unknown";
    }
}



