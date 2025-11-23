#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOperLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 15:30
// 版本号 : V.0.0.1
// 描述    : 操作日志实体
//===================================================================

namespace Takt.Domain.Entities.Logging
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    [SugarTable("Takt_audit_oper_log", "操作日志")]
    public class TaktOperLog : TaktBaseEntity
    {
        /// <summary>
        /// 操作标题（新增、删除、更新等）
        /// </summary>
        [SugarColumn(ColumnName = "oper_title", ColumnDescription = "操作标题", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperTitle { get; set; } = string.Empty;

        /// <summary>
        /// 操作人（记录执行操作的用户名，如：admin、张三等）
        /// </summary>
        [SugarColumn(ColumnName = "oper_by", ColumnDescription = "操作人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperBy { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型（新增、修改、删除、查询）
        /// </summary>
        [SugarColumn(ColumnName = "oper_type", ColumnDescription = "操作类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperType { get; set; } = string.Empty;

        /// <summary>
        /// 操作模块（如：用户管理、角色管理、菜单管理等）
        /// </summary>
        [SugarColumn(ColumnName = "oper_module", ColumnDescription = "操作模块", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperModule { get; set; } = string.Empty;

        /// <summary>
        /// 操作位置（IP地址对应的地理位置，如：北京市、上海市、广东省深圳市等）
        /// </summary>
        [SugarColumn(ColumnName = "oper_location", ColumnDescription = "操作位置", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperLocation { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn(ColumnName = "oper_table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperTableName { get; set; } = string.Empty;

        /// <summary>
        /// 业务主键（如：用户ID、角色ID、菜单ID等，用于标识具体操作的业务对象）
        /// </summary>
        [SugarColumn(ColumnName = "oper_business_key", ColumnDescription = "业务主键", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperBusinessKey { get; set; } = string.Empty;

        /// <summary>
        /// 请求方法（如：GET、POST、PUT、DELETE等HTTP方法）
        /// </summary>
        [SugarColumn(ColumnName = "oper_request_method", ColumnDescription = "请求方法", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperRequestMethod { get; set; } = string.Empty;

        /// <summary>
        /// 请求参数（记录请求中的关键参数，如：用户ID、角色名称等，敏感信息会被过滤）
        /// </summary>
        [SugarColumn(ColumnName = "oper_request_param", ColumnDescription = "请求参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数（记录API响应的关键信息，如：操作结果、影响行数、错误代码等）
        /// </summary>
        [SugarColumn(ColumnName = "oper_response_param", ColumnDescription = "返回参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        [SugarColumn(ColumnName = "oper_duration", ColumnDescription = "操作用时（毫秒）", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        [SugarColumn(ColumnName = "oper_status", ColumnDescription = "操作状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        [SugarColumn(ColumnName = "oper_error_msg", ColumnDescription = "错误消息", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "oper_ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperIpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "oper_user_agent", ColumnDescription = "用户代理", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperUserAgent { get; set; }

        /// <summary>
        /// 请求URL（记录完整的请求地址，如：/api/TaktUser/2、/api/TaktRole等，用于追踪具体的API端点）
        /// </summary>
        [SugarColumn(ColumnName = "oper_request_url", ColumnDescription = "请求URL", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperRequestUrl { get; set; }

        /// <summary>
        /// 请求来源（记录HTTP Referer头，如：https://localhost:3000/user/list，用于追踪用户从哪个页面发起的操作）
        /// </summary>
        [SugarColumn(ColumnName = "oper_referer", ColumnDescription = "请求来源", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperReferer { get; set; }
    }
}




