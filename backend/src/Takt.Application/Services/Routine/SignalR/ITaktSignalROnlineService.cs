//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : ITaktSignalROnlineService.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线用户服务接口
//===================================================================

using Takt.Application.Dtos.Routine.SignalR;
using Takt.Shared.Models;
using System.Linq.Expressions;

namespace Takt.Application.Services.Routine.SignalR;

/// <summary>
/// 在线用户服务接口
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public interface ITaktSignalROnlineService
{
    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSignalROnlineDto>> GetListAsync(TaktSignalROnlineQueryDto query);

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>连接ID列表</returns>
    Task<List<string>> GetConnectionIdsAsync(long userId);

    /// <summary>
    /// 根据条件获取在线用户信息
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>在线用户信息</returns>
    Task<TaktSignalROnlineDto> GetFirstAsync(Expression<Func<TaktSignalROnlineDto, bool>> predicate);

    /// <summary>
    /// 获取所有在线用户
    /// </summary>
    /// <returns>在线用户列表</returns>
    Task<List<TaktSignalROnlineDto>> GetAllAsync();

    /// <summary>
    /// 创建在线用户
    /// </summary>
    /// <param name="input">在线用户信息</param>
    /// <returns>在线用户ID</returns>
    Task<long> CreateOnlineUserAsync(TaktSignalROnlineCreateDto input);

    /// <summary>
    /// 更新在线用户信息
    /// </summary>
    /// <param name="input">在线用户更新信息</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(TaktSignalROnlineUpdateDto input);

    /// <summary>
    /// 更新在线用户状态
    /// </summary>
    /// <param name="input">状态更新信息</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(TaktSignalROnlineStatusUpdateDto input);

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteOnlineUserAsync(long id);

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    /// <param name="ids">在线用户ID列表</param>
    /// <returns>删除成功的数量</returns>
    Task<int> BatchDeleteOnlineUsersAsync(List<long> ids);

    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>是否成功</returns>
    Task<bool> ForceOfflineAsync(string deviceId);

    /// <summary>
    /// 更新所有在线用户的心跳时间
    /// </summary>
    /// <returns>更新的记录数</returns>
    Task<int> UpdateHeartbeatAsync();

    /// <summary>
    /// 清理过期用户
    /// </summary>
    /// <param name="minutes">超时时间(分钟)</param>
    /// <returns>清理数量</returns>
    Task<int> CleanupExpiredUsersAsync(int minutes = 20);

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktSignalROnlineQueryDto query, string sheetName = "在线用户信息");
} 




