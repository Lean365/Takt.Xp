#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktSignalMessageDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-20
// 版本号: V0.0.1
// 描述: 在线消息数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.SignalR;

/// <summary>
/// 在线消息基础DTO
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalRMessageDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalRMessageDto()
    {
        Content = string.Empty;
        IsRead = 0;
        ReadTime = DateTime.Now;
    }

    /// <summary>
    /// ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SignalRMessageId { get; set; }


    /// <summary>
    /// 发送者ID
    /// </summary>

    [JsonConverter(typeof(ValueToStringConverter))]
    public long SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>

    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>

    public string Content { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>

    public int IsRead { get; set; }

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }

    /// <summary>
    /// 用户代理信息
    /// </summary>
    public string? UserAgent { get; set; }

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
/// 在线消息查询对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalMessageQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalMessageQueryDto()
    {
        // 不需要初始化 MessageType，因为它已经是可空类型
    }

    /// <summary>
    /// 发送者ID
    /// </summary>
    [AdaptMember("SenderId")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    [AdaptMember("ReceiverId")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>

    public int? MessageType { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>

    public int? IsRead { get; set; }
}

/// <summary>
/// 在线消息创建对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalMessageCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalMessageCreateDto()
    {
        Content = string.Empty;
        IsRead = 0;
    }

    /// <summary>
    /// 发送者ID
    /// </summary>

    public long SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>

    public long ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>

    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>

    public string Content { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>

    public int IsRead { get; set; }

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }

    /// <summary>
    /// 用户代理信息
    /// </summary>
    public string? UserAgent { get; set; }
}

/// <summary>
/// 在线消息更新对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalMessageUpdateDto : TaktSignalMessageCreateDto
{
    /// <summary>
    /// ID
    /// </summary>  
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SignalRMessageId { get; set; }
}

/// <summary>
/// 在线消息已读状态更新对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalMessageReadStatusUpdateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SignalRMessageId { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>
    public int IsRead { get; set; }

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime? ReadTime { get; set; }

    /// <summary>
    /// 用户代理信息
    /// </summary>
    public string? UserAgent { get; set; }
}

/// <summary>
/// 在线消息导出对象
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalMessageExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSignalMessageExportDto()
    {
        Content = string.Empty;
        IsRead = 0;
        ReadTime = DateTime.Now;
    }



    /// <summary>
    /// 发送者ID
    /// </summary>
    public long SenderId { get; set; }

    /// <summary>
    /// 接收者ID
    /// </summary>
    public long ReceiverId { get; set; }

    /// <summary>
    /// 消息类型
    /// </summary>
    public int MessageType { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 是否已读（0未读 1已读）
    /// </summary>
    public int IsRead { get; set; }

    /// <summary>
    /// 阅读时间
    /// </summary>
    public DateTime ReadTime { get; set; }

        /// <summary>
    /// 用户代理信息
    /// </summary>
    public string? UserAgent { get; set; }
}

