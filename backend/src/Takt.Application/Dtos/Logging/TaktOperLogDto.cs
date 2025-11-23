//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOperLogDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Logging
{
    /// <summary>
    /// 操作日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktOperLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperLogDto()
        {
            OperTitle = string.Empty;
            OperBy = string.Empty;
            OperType = string.Empty;
            OperModule = string.Empty;
            OperLocation = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperDuration = 0;
            OperStatus = 0;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperUserAgent = string.Empty;
            OperRequestUrl = string.Empty;
            OperReferer = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long OperLogId { get; set; }

        /// <summary>
        /// 操作标题（新增、删除、更新等）
        /// </summary>
        public string OperTitle { get; set; }

        /// <summary>
        /// 操作人（记录执行操作的用户名，如：admin、张三等）
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 操作类型（新增、修改、删除、查询）
        /// </summary>
        public string OperType { get; set; }

        /// <summary>
        /// 操作模块（如：用户管理、角色管理、菜单管理等）
        /// </summary>
        public string OperModule { get; set; }

        /// <summary>
        /// 操作位置（IP地址对应的地理位置，如：北京市、上海市、广东省深圳市等）
        /// </summary>
        public string? OperLocation { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键（如：用户ID、角色ID、菜单ID等，用于标识具体操作的业务对象）
        /// </summary>
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法（如：GET、POST、PUT、DELETE等HTTP方法）
        /// </summary>
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数（记录请求中的关键参数，如：用户ID、角色名称等，敏感信息会被过滤）
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数（记录API响应的关键信息，如：操作结果、影响行数、错误代码等）
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? OperUserAgent { get; set; }

        /// <summary>
        /// 请求URL（记录完整的请求地址，如：/api/TaktUser/2、/api/TaktRole等，用于追踪具体的API端点）
        /// </summary>
        public string? OperRequestUrl { get; set; }

        /// <summary>
        /// 请求来源（记录HTTP Referer头，如：https://localhost:3000/user/list，用于追踪用户从哪个页面发起的操作）
        /// </summary>
        public string? OperReferer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 操作日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktOperLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperLogQueryDto()
        {
            OperTitle = string.Empty;
            OperType = string.Empty;
            OperModule = string.Empty;
            OperTableName = string.Empty;
            OperIpAddress = string.Empty;
            OperBy = string.Empty;
        }

        /// <summary>
        /// 操作标题
        /// </summary>
        public string OperTitle { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperType { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string OperModule { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string OperTableName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 操作状态
        /// </summary>
        public int? OperStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 操作日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktOperLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperLogExportDto()
        {
            OperTitle = string.Empty;
            OperBy = string.Empty;
            OperType = string.Empty;
            OperModule = string.Empty;
            OperLocation = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperDuration = 0;
            OperStatus = 0;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperUserAgent = string.Empty;
            OperRequestUrl = string.Empty;
            OperReferer = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 操作标题
        /// </summary>
        public string OperTitle { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperType { get; set; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string OperModule { get; set; }

        /// <summary>
        /// 操作位置
        /// </summary>
        public string? OperLocation { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? OperUserAgent { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        public string? OperRequestUrl { get; set; }

        /// <summary>
        /// 请求来源
        /// </summary>
        public string? OperReferer { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 操作日志创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktOperLogCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperLogCreateDto()
        {
            OperTitle = string.Empty;
            OperBy = string.Empty;
            OperType = string.Empty;
            OperModule = string.Empty;
            OperLocation = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperDuration = 0;
            OperStatus = 0;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperUserAgent = string.Empty;
            OperRequestUrl = string.Empty;
            OperReferer = string.Empty;
        }

        /// <summary>
        /// 操作标题
        /// </summary>
        public string OperTitle { get; set; }

        /// <summary>
        /// 操作人（记录执行操作的用户名，如：admin、张三等）
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 操作类型（新增、修改、删除、查询）
        /// </summary>
        public string OperType { get; set; }

        /// <summary>
        /// 操作模块（如：用户管理、角色管理、菜单管理等）
        /// </summary>
        public string OperModule { get; set; }

        /// <summary>
        /// 操作位置（IP地址对应的地理位置，如：北京市、上海市、广东省深圳市等）
        /// </summary>
        public string? OperLocation { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键（如：用户ID、角色ID、菜单ID等，用于标识具体操作的业务对象）
        /// </summary>
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法（如：GET、POST、PUT、DELETE等HTTP方法）
        /// </summary>
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数（记录请求中的关键参数，如：用户ID、角色名称等，敏感信息会被过滤）
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数（记录API响应的关键信息，如：操作结果、影响行数、错误代码等）
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? OperUserAgent { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        public string? OperRequestUrl { get; set; }

        /// <summary>
        /// 请求来源
        /// </summary>
        public string? OperReferer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 操作日志更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktOperLogUpdateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperLogUpdateDto()
        {
            OperTitle = string.Empty;
            OperBy = string.Empty;
            OperType = string.Empty;
            OperModule = string.Empty;
            OperLocation = string.Empty;
            OperTableName = string.Empty;
            OperBusinessKey = string.Empty;
            OperRequestMethod = string.Empty;
            OperRequestParam = string.Empty;
            OperResponseParam = string.Empty;
            OperDuration = 0;
            OperStatus = 0;
            OperErrorMsg = string.Empty;
            OperIpAddress = string.Empty;
            OperUserAgent = string.Empty;
            OperRequestUrl = string.Empty;
            OperReferer = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        public long OperLogId { get; set; }

        /// <summary>
        /// 操作标题
        /// </summary>
        public string OperTitle { get; set; }

        /// <summary>
        /// 操作人（记录执行操作的用户名，如：admin、张三等）
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 操作类型（新增、修改、删除、查询）
        /// </summary>
        public string OperType { get; set; }

        /// <summary>
        /// 操作模块（如：用户管理、角色管理、菜单管理等）
        /// </summary>
        public string OperModule { get; set; }

        /// <summary>
        /// 操作位置（IP地址对应的地理位置，如：北京市、上海市、广东省深圳市等）
        /// </summary>
        public string? OperLocation { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string OperTableName { get; set; }

        /// <summary>
        /// 业务主键（如：用户ID、角色ID、菜单ID等，用于标识具体操作的业务对象）
        /// </summary>
        public string OperBusinessKey { get; set; }

        /// <summary>
        /// 请求方法（如：GET、POST、PUT、DELETE等HTTP方法）
        /// </summary>
        public string OperRequestMethod { get; set; }

        /// <summary>
        /// 请求参数（记录请求中的关键参数，如：用户ID、角色名称等，敏感信息会被过滤）
        /// </summary>
        public string? OperRequestParam { get; set; }

        /// <summary>
        /// 返回参数（记录API响应的关键信息，如：操作结果、影响行数、错误代码等）
        /// </summary>
        public string? OperResponseParam { get; set; }

        /// <summary>
        /// 操作用时（毫秒）
        /// </summary>
        public long OperDuration { get; set; } = 0;

        /// <summary>
        /// 操作状态（0正常 1异常）
        /// </summary>
        public int OperStatus { get; set; } = 0;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? OperErrorMsg { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string OperIpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? OperUserAgent { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        public string? OperRequestUrl { get; set; }

        /// <summary>
        /// 请求来源
        /// </summary>
        public string? OperReferer { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}




