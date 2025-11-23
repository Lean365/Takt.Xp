//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktMailTplService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件模板服务接口
//===================================================================

using System.Threading.Tasks;
using Takt.Shared.Models;
using Takt.Application.Dtos.Routine;
using System.IO;

namespace Takt.Application.Services.Routine.Email
{
    /// <summary>
    /// 邮件模板服务接口
    /// </summary>
    public interface ITaktMailTplService
    {
        /// <summary>
        /// 获取邮件模板分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件模板分页列表</returns>
        Task<TaktPagedResult<TaktMailTplDto>> GetListAsync(TaktMailTplQueryDto query);

        /// <summary>
        /// 获取邮件模板详情
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <returns>邮件模板详情</returns>
        Task<TaktMailTplDto> GetByIdAsync(long tmplId);

        /// <summary>
        /// 创建邮件模板
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件模板ID</returns>
        Task<long> CreateAsync(TaktMailTplCreateDto input);

        /// <summary>
        /// 更新邮件模板
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(long tmplId, TaktMailTplDto input);

        /// <summary>
        /// 删除邮件模板
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long tmplId);

        /// <summary>
        /// 批量删除邮件模板
        /// </summary>
        /// <param name="tmplIds">模板ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] tmplIds);

        /// <summary>
        /// 导出邮件模板数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktMailTplQueryDto query, string sheetName = "邮件模板数据");

        /// <summary>
        /// 导入邮件模板数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "邮件模板数据");

        /// <summary>
        /// 获取邮件模板导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "邮件模板数据");

        /// <summary>
        /// 获取指定用户的邮件模板状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy);
    }
} 




