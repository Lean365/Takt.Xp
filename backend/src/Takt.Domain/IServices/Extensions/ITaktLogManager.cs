//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktLogManager.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 日志管理接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System.Text.Json.Nodes;

namespace Takt.Domain.IServices.Extensions;

/// <summary>
/// 基础日志管理接口
/// </summary>
public interface ITaktLogManager
{
  /// <summary>
  /// 记录错误日志
  /// </summary>
  void Error(string message, Exception ex);

  /// <summary>
  /// 记录严重错误日志
  /// </summary>
  void Fatal(string message, Exception ex);
}

/// <summary>
/// 审计日志管理接口
/// </summary>
public interface ITaktAuditLogManager
{
  /// <summary>
  /// 记录审计日志
  /// </summary>
  Task LogAuditAsync(
      string module,
      string operation,
      string method,
      string? parameters = null,
      string? result = null,
      long elapsed = 0);
}

/// <summary>
/// 差异日志管理接口
/// </summary>
public interface ITaktSqlDiffLogManager
{
  /// <summary>
  /// 记录数据变更日志
  /// </summary>
  Task LogDbDiffAsync(
      string tableName,
      string changeType,
      string? businessData = null,
      string? beforeData = null,
      string? afterData = null,
      string? executeSql = null,
      string? sqlParameters = null);

  /// <summary>
  /// 处理SqlSugar差异日志事件
  /// </summary>
  /// <param name="diffLog">SqlSugar差异日志模型</param>
  /// <param name="dbName">数据库名称</param>
  Task HandleSqlSugarDiffLogEventAsync(DiffLogModel diffLog, string dbName);
}

/// <summary>
/// 操作日志管理接口
/// </summary>
public interface ITaktOperLogManager
{
  /// <summary>
  /// 记录操作日志
  /// </summary>
  Task LogOperationAsync(
      string tableName,
      string OperType,
      string businessKey,
      string? requestParam = null,
      string? location = null,
      bool success = true,
      string? errorMsg = null);
}

/// <summary>
/// 异常日志管理接口
/// </summary>
public interface ITaktExceptionLogManager
{
  /// <summary>
  /// 记录异常日志
  /// </summary>
  Task LogExceptionAsync(
      Exception ex,
      string? method = null,
      string? parameters = null);
}



