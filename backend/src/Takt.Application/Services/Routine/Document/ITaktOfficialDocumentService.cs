//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktOfficialDocumentService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 公文文档服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Document
{
    /// <summary>
    /// 公文文档服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktOfficialDocumentService
    {
        /// <summary>
        /// 获取公文文档分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>公文文档分页列表</returns>
        Task<TaktPagedResult<TaktOfficialDocumentDto>> GetListAsync(TaktOfficialDocumentQueryDto query);

        /// <summary>
        /// 获取公文文档详情
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>公文文档详情</returns>
        Task<TaktOfficialDocumentDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建公文文档
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文档ID</returns>
        Task<long> CreateAsync(TaktOfficialDocumentCreateDto input);

        /// <summary>
        /// 更新公文文档
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktOfficialDocumentUpdateDto input);

        /// <summary>
        /// 删除公文文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除公文文档
        /// </summary>
        /// <param name="documentIds">文档ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] documentIds);

        /// <summary>
        /// 获取公文文档树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        Task<List<TaktOfficialDocumentDto>> GetTreeAsync(long? parentId = null);

        /// <summary>
        /// 导入公文文档数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出公文文档数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktOfficialDocumentQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 处理公文文档请求审批完成后的自动更新
        /// </summary>
        /// <param name="requestId">请求ID</param>
        /// <returns>更新结果</returns>
        Task<TaktApiResult<bool>> ProcessRequestCompletionAsync(long requestId);
    }
} 



