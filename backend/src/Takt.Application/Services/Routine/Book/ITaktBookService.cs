//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktBookService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 图书服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Book;

namespace Takt.Application.Services.Routine.Book
{
    /// <summary>
    /// 图书服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktBookService
    {
        /// <summary>
        /// 获取图书分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>图书分页列表</returns>
        Task<TaktPagedResult<TaktBookDto>> GetListAsync(TaktBookQueryDto query);

        /// <summary>
        /// 获取图书详情
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <returns>图书详情</returns>
        Task<TaktBookDto> GetByIdAsync(long bookId);

        /// <summary>
        /// 获取图书选项列表
        /// </summary>
        /// <returns>图书选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();        

        /// <summary>
        /// 创建图书
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>图书ID</returns>
        Task<long> CreateAsync(TaktBookCreateDto input);

        /// <summary>
        /// 更新图书
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktBookUpdateDto input);

        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long bookId);

        /// <summary>
        /// 批量删除图书
        /// </summary>
        /// <param name="bookIds">图书ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] bookIds);

        /// <summary>
        /// 导入图书数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName);

        /// <summary>
        /// 导出图书数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktBookQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);


    }
}




