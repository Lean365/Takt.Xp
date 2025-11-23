#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExceptionLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 异常日志实体
//===================================================================

namespace Takt.Domain.Entities.Logging
{
    /// <summary>
    /// 异常日志实体
    /// </summary>
    [SugarTable("Takt_audit_exception_log", "异常日志")]
    public class TaktExceptionLog : TaktBaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        [SugarColumn(ColumnName = "log_level", ColumnDescription = "日志级别", ColumnDataType = "int", IsNullable = false)]
        public int LogLevel { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 方法
        /// </summary>
        [SugarColumn(ColumnName = "method", ColumnDescription = "方法", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 参数
        /// </summary>
        [SugarColumn(ColumnName = "parameters", ColumnDescription = "参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Parameters { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        [SugarColumn(ColumnName = "exception_type", ColumnDescription = "异常类型", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ExceptionType { get; set; } = string.Empty;

        /// <summary>
        /// 异常消息
        /// </summary>
        [SugarColumn(ColumnName = "exception_message", ColumnDescription = "异常消息", Length = -1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ExceptionMessage { get; set; } = string.Empty;

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        [SugarColumn(ColumnName = "stack_trace", ColumnDescription = "堆栈跟踪", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StackTrace { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserAgent { get; set; } = string.Empty;
    }
}




