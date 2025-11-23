//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktLoginLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志服务接口
//===================================================================

using System.Threading.Tasks;
using Takt.Shared.Models;
using Takt.Application.Dtos.Logging;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 登录日志服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface ITaktLoginLogService
    {
        /// <summary>
        /// 获取登录日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<TaktPagedResult<TaktLoginLogDto>> GetListAsync(TaktLoginLogQueryDto query);

        /// <summary>
        /// 获取登录日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>登录日志详情</returns>
        Task<TaktLoginLogDto> GetByIdAsync(long logId);

        /// <summary>
        /// 清空登录日志
        /// </summary>
        /// <returns>是否成功</returns>
        Task<bool> ClearUpAsync();

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>任务</returns>
        Task UnlockUserAsync(long userId);

        /// <summary>
        /// 导出登录日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktLoginLogQueryDto query, string sheetName);


    }
} 





