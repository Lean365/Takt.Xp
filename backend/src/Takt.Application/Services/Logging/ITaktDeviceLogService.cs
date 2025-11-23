//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktDeviceLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:45
// 版本号 : V0.0.1
// 描述   : 设备日志服务接口
//===================================================================

using Takt.Application.Dtos.Logging;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 设备日志服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public interface ITaktDeviceLogService
    {
        /// <summary>
        /// 获取设备日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>设备日志分页列表</returns>
        Task<TaktPagedResult<TaktDeviceLogDto>> GetListAsync(TaktDeviceLogQueryDto query);

        /// <summary>
        /// 获取设备日志详情
        /// </summary>
        /// <param name="id">设备日志ID</param>
        /// <returns>设备日志详情</returns>
        Task<TaktDeviceLogDto> GetByIdAsync(long id);

        /// <summary>
        /// 删除设备日志
        /// </summary>
        /// <param name="id">设备日志ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除设备日志
        /// </summary>
        /// <param name="ids">设备日志ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 记录设备登录日志
        /// </summary>
        /// <param name="input">设备日志信息</param>
        /// <returns>是否成功</returns>
        Task<bool> RecordDeviceLoginAsync(TaktDeviceLogDto input);

        /// <summary>
        /// 更新设备在线状态
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="isOnline">是否在线</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateOnlineStatusAsync(string deviceId, bool isOnline);

        /// <summary>
        /// 获取用户在线设备数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>在线设备数量</returns>
        Task<int> GetOnlineDeviceCountAsync(long userId);

        /// <summary>
        /// 更新今日在线时段
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="onlinePeriods">在线时段列表</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateTodayOnlinePeriodsAsync(string deviceId, List<string> onlinePeriods);

        /// <summary>
        /// 更新连续登录天数
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="continuousDays">连续登录天数</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateContinuousLoginDaysAsync(string deviceId, int continuousDays);

        /// <summary>
        /// 更新登录次数
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="loginCount">登录次数</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateLoginCountAsync(string deviceId, int loginCount);

        /// <summary>
        /// 清空设备日志数据
        /// </summary>
        /// <param name="query">清空条件</param>
        /// <returns>是否成功</returns>
        Task<bool> ClearUpAsync(TaktDeviceLogQueryDto query);

        /// <summary>
        /// 导出设备日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktDeviceLogQueryDto query, string sheetName);
    }
}






