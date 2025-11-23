//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktQuartzService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 定时任务服务接口
//===================================================================

using System.Threading.Tasks;
using Takt.Application.Dtos.Routine;
using Takt.Shared.Models;

namespace Takt.Application.Services.Routine.Quartz
{
    /// <summary>
    /// 定时任务服务接口
    /// </summary>
    public interface ITaktQuartzService
    {
        /// <summary>
        /// 获取定时任务分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<TaktPagedResult<TaktQuartzDto>> GetListAsync(TaktQuartzQueryDto query);

        /// <summary>
        /// 获取定时任务详情
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>任务详情</returns>
        Task<TaktQuartzDto> GetByIdAsync(long taskId);

        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>任务ID</returns>
        Task<long> CreateAsync(TaktQuartzCreateDto input);

        /// <summary>
        /// 更新定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(long taskId, TaktQuartzDto input);

        /// <summary>
        /// 删除定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long taskId);

        /// <summary>
        /// 批量删除定时任务
        /// </summary>
        /// <param name="taskIds">任务ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] taskIds);

        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> StartAsync(long taskId);

        /// <summary>
        /// 停止定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> StopAsync(long taskId);

        /// <summary>
        /// 立即执行定时任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否成功</returns>
        Task<bool> ExecuteAsync(long taskId);

        /// <summary>
        /// 导出定时任务数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktQuartzQueryDto query, string sheetName = "定时任务数据");

        /// <summary>
        /// 获取指定用户的定时任务状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy);
    }
} 




