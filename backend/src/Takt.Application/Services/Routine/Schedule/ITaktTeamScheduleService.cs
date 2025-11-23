#nullable enable

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Takt.Shared.Models;
using Takt.Application.Dtos.Routine.Schedule;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktTeamScheduleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 团队协作日程服务接口
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Application.Services.Routine.Schedule
{
    /// <summary>
    /// 团队协作日程服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktTeamScheduleService
    {
        /// <summary>
        /// 获取团队协作日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>团队协作日程分页列表</returns>
        Task<TaktPagedResult<TaktTeamScheduleDto>> GetListAsync(TaktTeamScheduleQueryDto query);

        /// <summary>
        /// 获取团队协作日程详情
        /// </summary>
        /// <param name="teamScheduleId">团队协作日程ID</param>
        /// <returns>团队协作日程详情</returns>
        Task<TaktTeamScheduleDto> GetByIdAsync(long teamScheduleId);

        /// <summary>
        /// 创建团队协作日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>团队协作日程ID</returns>
        Task<long> CreateAsync(TaktTeamScheduleCreateDto input);

        /// <summary>
        /// 更新团队协作日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktTeamScheduleUpdateDto input);

        /// <summary>
        /// 删除团队协作日程
        /// </summary>
        /// <param name="teamScheduleId">团队协作日程ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long teamScheduleId);

        /// <summary>
        /// 批量删除团队协作日程
        /// </summary>
        /// <param name="teamScheduleIds">团队协作日程ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] teamScheduleIds);

        /// <summary>
        /// 导入团队协作日程数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "团队协作日程信息");

        /// <summary>
        /// 导出团队协作日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktTeamScheduleQueryDto query, string sheetName = "团队协作日程信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "团队协作日程信息");

        /// <summary>
        /// 获取指定用户的团队协作日程状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy);

        /// <summary>
        /// 获取当前用户的团队协作日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>团队协作日程分页列表</returns>
        Task<TaktPagedResult<TaktTeamScheduleDto>> GetMyTeamSchedulesAsync(TaktTeamScheduleQueryDto query);
    }
} 




