//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktProjectService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 项目服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Takt.Shared.Models;
using Takt.Application.Dtos.Routine;

namespace Takt.Application.Services.Routine.Project
{
    /// <summary>
    /// 项目服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktProjectService
    {
        /// <summary>
        /// 获取项目分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>项目分页列表</returns>
        Task<TaktPagedResult<TaktProjectDto>> GetListAsync(TaktProjectQueryDto query);

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>项目详情</returns>
        Task<TaktProjectDto> GetByIdAsync(long projectId);

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>项目ID</returns>
        Task<long> CreateAsync(TaktProjectCreateDto input);

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktProjectUpdateDto input);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long projectId);

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="projectIds">项目ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] projectIds);

        /// <summary>
        /// 导入项目数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "项目信息");

        /// <summary>
        /// 导出项目数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktProjectQueryDto query, string sheetName = "项目信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "项目信息");

        /// <summary>
        /// 获取指定用户参与的项目状态统计
        /// </summary>
        /// <param name="participant">参与者</param>
        /// <returns>状态-数量字典</returns>
        Task<Dictionary<int, int>> GetParticipantProjectStatisticsAsync(string participant);
    }
} 




