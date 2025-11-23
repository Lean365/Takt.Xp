//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktFileService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述   : 文件服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Document;

namespace Takt.Application.Services.Routine.Document
{
    /// <summary>
    /// 文件服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktFileService
    {
        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件分页列表</returns>
        Task<TaktPagedResult<TaktFileDto>> GetListAsync(TaktFileQueryDto query);

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>文件详情</returns>
        Task<TaktFileDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件ID</returns>
        Task<long> CreateAsync(TaktFileCreateDto input);

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktFileUpdateDto input);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id">文件ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] fileIds);

        /// <summary>
        /// 导入文件数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出文件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktFileQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 更新文件状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktStatusDto input);
    }
} 



