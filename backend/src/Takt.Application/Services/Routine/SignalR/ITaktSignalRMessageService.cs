//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : ITaktSignalMessageService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线消息服务接口
//===================================================================

using Takt.Application.Dtos.Routine.SignalR;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.SignalR;

/// <summary>
/// 在线消息服务接口
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public interface ITaktSignalRMessageService
{
    /// <summary>
    /// 获取在线消息分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSignalRMessageDto>> GetListAsync(TaktSignalMessageQueryDto query);

    /// <summary>
    /// 获取消息详情
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>消息详情</returns>
    Task<TaktSignalRMessageDto> GetMessageAsync(long id);

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="input">消息信息</param>
    /// <returns>消息ID</returns>
    Task<long> CreateMessageAsync(TaktSignalMessageCreateDto input);

    /// <summary>
    /// 更新消息
    /// </summary>
    /// <param name="input">消息更新信息</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateMessageAsync(TaktSignalMessageUpdateDto input);

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteMessageAsync(long id);

    /// <summary>
    /// 批量删除消息
    /// </summary>
    /// <param name="ids">消息ID列表</param>
    /// <returns>删除成功的数量</returns>
    Task<int> BatchDeleteMessagesAsync(List<long> ids);

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> MarkAsReadAsync(long id);

    /// <summary>
    /// 更新消息已读状态
    /// </summary>
    /// <param name="input">已读状态更新信息</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateReadStatusAsync(TaktSignalMessageReadStatusUpdateDto input);

    /// <summary>
    /// 标记所有消息为已读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>标记的消息数量</returns>
    Task<int> MarkAllAsReadAsync(long userId);

    /// <summary>
    /// 标记消息为未读
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> MarkAsUnreadAsync(long id);

    /// <summary>
    /// 标记所有消息为未读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>标记的消息数量</returns>
    Task<int> MarkAllAsUnreadAsync(long userId);

    /// <summary>
    /// 清理过期消息
    /// </summary>
    /// <param name="days">保留天数</param>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredMessagesAsync(int days = 7);

    /// <summary>
    /// 导出在线消息数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktSignalMessageQueryDto query, string sheetName = "在线消息信息");
} 




