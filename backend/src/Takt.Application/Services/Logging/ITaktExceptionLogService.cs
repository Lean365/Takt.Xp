//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktExceptionLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志服务接口
//===================================================================

using System.Threading.Tasks;
using Takt.Shared.Models;
using Takt.Application.Dtos.Logging;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 异常日志服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface ITaktExceptionLogService
    {
        /// <summary>
        /// 获取异常日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<TaktPagedResult<TaktExceptionLogDto>> GetListAsync(TaktExceptionLogQueryDto query);

        /// <summary>
        /// 获取异常日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>异常日志详情</returns>
        Task<TaktExceptionLogDto> GetByIdAsync(long logId);

        /// <summary>
        /// 清空异常日志
        /// </summary>
        /// <returns>是否成功</returns>
        Task<bool> ClearUpAsync();

        /// <summary>
        /// 导出异常日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
       Task<(string fileName, byte[] content)> ExportAsync(TaktExceptionLogQueryDto query, string sheetName = "异常日志数据");


    }
} 





