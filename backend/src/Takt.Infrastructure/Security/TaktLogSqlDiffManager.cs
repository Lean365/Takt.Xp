//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogSqlDiffManager.cs
// 创建者 : Claude
// 创建时间: 2025-08-20
// 版本号 : V.0.0.1
// 描述    : SQL差异日志管理器
//===================================================================

using Takt.Domain.Entities.Logging;
using Takt.Infrastructure.Data.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Takt.Infrastructure.Security;

/// <summary>
/// SQL差异日志管理器
/// </summary>
public class TaktLogSqlDiffManager : ITaktSqlDiffLogManager
{
    private readonly ITaktLogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITaktCurrentUser _currentUser;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志记录器</param>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="currentUser">当前用户</param>
    public TaktLogSqlDiffManager(
        ITaktLogger logger,
        IServiceProvider serviceProvider,
        ITaktCurrentUser currentUser)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _currentUser = currentUser;
    }

    /// <summary>
    /// 记录数据变更日志
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="changeType">变更类型</param>
    /// <param name="businessData">业务数据</param>
    /// <param name="beforeData">变更前数据</param>
    /// <param name="afterData">变更后数据</param>
    /// <param name="executeSql">执行的SQL</param>
    /// <param name="sqlParameters">SQL参数</param>
    public async Task LogDbDiffAsync(
        string tableName,
        string changeType,
        string? businessData = null,
        string? beforeData = null,
        string? afterData = null,
        string? executeSql = null,
        string? sqlParameters = null)
    {
        var userName = !string.IsNullOrEmpty(_currentUser?.UserName) ? _currentUser.UserName : "Takt365";
        var id=SnowFlakeSingle.Instance.NextId();//也可以在程序中直接获取ID
        var log = new TaktSqlDiffLog
        {
            Id = id,
            TableName = tableName,
            OperBy = userName,
            DiffType = changeType,
            BusinessData = businessData,
            BeforeData = beforeData,
            AfterData = afterData,
            ExecuteSql = executeSql,
            SqlParameters = sqlParameters,
            CreateBy = userName,
            CreateTime = DateTime.Now
        };

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TaktIdentityDBContext>();
        await context.Client.Insertable(log).ExecuteCommandAsync();
    }

    /// <summary>
    /// 处理SqlSugar差异日志事件
    /// </summary>
    /// <param name="diffLog">差异日志模型</param>
    /// <param name="dbName">数据库名称</param>
    public async Task HandleSqlSugarDiffLogEventAsync(DiffLogModel diffLog, string dbName)
    {
        var tableName = diffLog.AfterData?.FirstOrDefault()?.TableName ?? diffLog.BeforeData?.FirstOrDefault()?.TableName ?? "Unknown";
        if (tableName.ToLower().Contains("diff_log") || tableName.ToLower().Contains("audit"))
        {
            return;
        }

        var beforeData = diffLog.BeforeData?.FirstOrDefault();
        var afterData = diffLog.AfterData?.FirstOrDefault();
        
        var beforeDataJson = ExtractReadableData(beforeData);
        var afterDataJson = ExtractReadableData(afterData);
        
        var executeSql = diffLog.Sql;
        var sqlParameters = ExtractReadableParameters(diffLog.Parameters);
        
        var businessData = ExtractBusinessData(beforeData, afterData, diffLog.DiffType);
        
        await LogDbDiffAsync(
            tableName: tableName,
            changeType: diffLog.DiffType.ToString(),
            businessData: businessData,
            beforeData: beforeDataJson,
            afterData: afterDataJson,
            executeSql: executeSql,
            sqlParameters: sqlParameters
        );
    }

    /// <summary>
    /// 提取业务数据
    /// </summary>
    /// <param name="beforeData">变更前数据</param>
    /// <param name="afterData">变更后数据</param>
    /// <param name="diffType">变更类型</param>
    /// <returns>业务数据</returns> 
    private string? ExtractBusinessData(object? beforeData, object? afterData, DiffType diffType)
    {
        var businessInfo = new
        {
            DiffType = diffType.ToString(),
            Timestamp = DateTime.Now,
            Summary = GetDataChangeSummary(diffType)
        };
        
                    return JsonConvert.SerializeObject(businessInfo);
    }

    /// <summary>
    /// 获取数据变更摘要
    /// </summary>
    /// <param name="diffType">变更类型</param>
    /// <returns>变更摘要</returns>
    private string GetDataChangeSummary(DiffType diffType)
    {
        var diffTypeString = diffType.ToString().ToLower();
        
        if (diffTypeString.Contains("insert"))
            return "新增数据";
        else if (diffTypeString.Contains("update"))
            return "更新数据";
        else if (diffTypeString.Contains("delete"))
            return "删除数据";
        else
            return "未知操作";
    }

    /// <summary>
    /// 提取可读性更好的数据
    /// </summary>
    /// <param name="data">原始数据</param>
    /// <returns>可读性更好的JSON数据</returns>
    private string? ExtractReadableData(object? data)
    {
        if (data == null) return null;

        try
        {
            // 如果是SqlSugar的DiffLogTableInfo类型，提取Columns中的关键信息
            var dataType = data.GetType();
            var columnsProperty = dataType.GetProperty("Columns");
            
            if (columnsProperty != null)
            {
                var columns = columnsProperty.GetValue(data) as IEnumerable<object>;
                if (columns != null)
                {
                    var readableData = new Dictionary<string, object?>();
                    
                    foreach (var column in columns)
                    {
                        var columnType = column.GetType();
                        var columnNameProp = columnType.GetProperty("ColumnName");
                        var valueProp = columnType.GetProperty("Value");
                        var isPrimaryKeyProp = columnType.GetProperty("IsPrimaryKey");
                        
                        if (columnNameProp != null && valueProp != null)
                        {
                            var columnName = columnNameProp.GetValue(column)?.ToString();
                            var value = valueProp.GetValue(column);
                            var isPrimaryKey = isPrimaryKeyProp?.GetValue(column) as bool? ?? false;
                            
                            if (!string.IsNullOrEmpty(columnName))
                            {
                                // 只保留有值的字段，跳过空值和默认值
                                if (value != null && !IsDefaultValue(value))
                                {
                                    readableData[columnName] = value;
                                    if (isPrimaryKey)
                                    {
                                        readableData[$"{columnName}_IsPK"] = true;
                                    }
                                }
                            }
                        }
                    }
                    
                                return JsonConvert.SerializeObject(readableData, new JsonSerializerSettings 
            { 
                Formatting = Formatting.None
            });
                }
            }
            
            // 如果不是预期的类型，直接序列化
                        return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.None
            });
        }
        catch
        {
            return data.ToString();
        }
    }

    /// <summary>
    /// 判断是否为默认值
    /// </summary>
    /// <param name="value">值</param>
    /// <returns>是否为默认值</returns>
    private bool IsDefaultValue(object value)
    {
        if (value == null) return true;
        
        var valueStr = value.ToString();
        if (string.IsNullOrEmpty(valueStr)) return true;
        if (valueStr == "0" || valueStr == "0.00" || valueStr == "0.000" || valueStr == "0.00000") return true;
        if (valueStr == "{}" || valueStr == "[]") return true;
        
        return false;
    }

    /// <summary>
    /// 提取可读性更好的SQL参数
    /// </summary>
    /// <param name="parameters">原始参数</param>
    /// <returns>可读性更好的参数JSON</returns>
    private string? ExtractReadableParameters(object? parameters)
    {
        if (parameters == null) return null;

        try
        {
            // 如果是SqlParameter数组或集合
            if (parameters is IEnumerable<object> paramList)
            {
                var readableParams = new Dictionary<string, object?>();
                
                foreach (var param in paramList)
                {
                    var paramType = param.GetType();
                    var paramNameProp = paramType.GetProperty("ParameterName");
                    var valueProp = paramType.GetProperty("Value");
                    
                    if (paramNameProp != null && valueProp != null)
                    {
                        var paramName = paramNameProp.GetValue(param)?.ToString();
                        var value = valueProp.GetValue(param);
                        
                        if (!string.IsNullOrEmpty(paramName))
                        {
                            readableParams[paramName] = value;
                        }
                    }
                }
                
                return JsonConvert.SerializeObject(readableParams, new JsonSerializerSettings 
                { 
                    Formatting = Formatting.None
                });
            }
            
            // 如果是其他类型，直接序列化
            return JsonConvert.SerializeObject(parameters, new JsonSerializerSettings 
            { 
                Formatting = Formatting.None
            });
        }
        catch
        {
            return parameters.ToString();
        }
    }
}



