#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktSignalROnlineDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-20
// 版本号: V0.0.1
// 描述: 在线用户数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.SignalR;

/// <summary>
/// 在线用户基础DTO
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalROnlineDto()
    {
        ConnectionId = string.Empty;
        DeviceId = string.Empty;
        ClientIp = string.Empty;
        ClientBrowser = string.Empty;
        UserAgent = string.Empty;
        LastActivity = DateTime.Now;
        LastHeartbeat = DateTime.Now;
        OnlineStatus = 0;
        CreateTime = DateTime.Now;
        UpdateTime = DateTime.Now;
    }

    /// <summary>
    /// ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SignalROnlineId { get; set; }
    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>

    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>

    public string ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>

    public string DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>

    public string ClientIp { get; set; }

    /// <summary>
    /// 客户端浏览器
    /// </summary>

    public string ClientBrowser { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>

    public string UserAgent { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 在线状态（0=在线 1=离线）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }

    /// <summary>
    /// 总在线时长（分钟）
    /// </summary>
    public int TotalOnlineMinutes { get; set; }

    /// <summary>
    /// 今日在线时长（分钟）
    /// </summary>
    public int TodayOnlineMinutes { get; set; }

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
/// 在线用户查询对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalROnlineQueryDto()
    {
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 在线状态（0=在线 1=离线）
    /// </summary>
    public int? OnlineStatus { get; set; }
}

/// <summary>
/// 在线用户创建对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalROnlineCreateDto()
    {
        ConnectionId = string.Empty;
        DeviceId = string.Empty;
        ClientIp = string.Empty;
        ClientBrowser = string.Empty;
        UserAgent = string.Empty;
        OnlineStatus = 0;
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]    
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>
    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

    /// <summary>
    /// 客户端浏览器
    /// </summary>
    public string ClientBrowser { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// 在线状态（0=在线 1=离线）
    /// </summary>
    public int OnlineStatus { get; set; }
        /// <summary>
    /// 总在线时长（分钟）
    /// </summary>
    public int TotalOnlineMinutes { get; set; }

    /// <summary>
    /// 今日在线时长（分钟）
    /// </summary>
    public int TodayOnlineMinutes { get; set; }
}

/// <summary>
/// 在线用户更新对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineUpdateDto : TaktSignalROnlineCreateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SignalROnlineId { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }

}

/// <summary>
/// 在线用户状态更新对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineStatusUpdateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SignalROnlineId { get; set; }

    /// <summary>
    /// 在线状态（0=在线 1=离线）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime? LastActivity { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime? LastHeartbeat { get; set; }
        /// <summary>
    /// 总在线时长（分钟）
    /// </summary>
    public int TotalOnlineMinutes { get; set; }

    /// <summary>
    /// 今日在线时长（分钟）
    /// </summary>
    public int TodayOnlineMinutes { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 在线用户导出对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalROnlineExportDto()
    {
        ConnectionId = string.Empty;
        DeviceId = string.Empty;
        ClientIp = string.Empty;
        ClientBrowser = string.Empty;
        UserAgent = string.Empty;
        LastActivity = DateTime.Now;
        LastHeartbeat = DateTime.Now;
        OnlineStatus = 0;
    }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 用户组ID
    /// </summary>
    public long GroupId { get; set; }

    /// <summary>
    /// 连接ID
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }

    /// <summary>
    /// 客户端浏览器
    /// </summary>
    public string ClientBrowser { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    public string UserAgent { get; set; }

    /// <summary>
    /// 最后活动时间
    /// </summary>
    public DateTime LastActivity { get; set; }

    /// <summary>
    /// 在线状态（0=在线 1=离线）
    /// </summary>
    public int OnlineStatus { get; set; }

    /// <summary>
    /// 最后心跳时间
    /// </summary>
    public DateTime LastHeartbeat { get; set; }

    /// <summary>
    /// 总在线时长（分钟）
    /// </summary>
    public int TotalOnlineMinutes { get; set; }

    /// <summary>
    /// 今日在线时长（分钟）
    /// </summary>
    public int TodayOnlineMinutes { get; set; }
}

