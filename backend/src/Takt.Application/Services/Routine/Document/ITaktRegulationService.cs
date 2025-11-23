//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktRegulationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 规章制度服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Document.Regulations
{
    /// <summary>
    /// 规章制度服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktRegulationService
    {
        /// <summary>
        /// 获取规章制度分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>规章制度分页列表</returns>
        Task<TaktPagedResult<TaktRegulationDto>> GetListAsync(TaktRegulationQueryDto query);

        /// <summary>
        /// 获取规章制度详情
        /// </summary>
        /// <param name="id">规章制度ID</param>
        /// <returns>规章制度详情</returns>
        Task<TaktRegulationDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建规章制度
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>规章制度ID</returns>
        Task<long> CreateAsync(TaktRegulationCreateDto input);

        /// <summary>
        /// 更新规章制度
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktRegulationUpdateDto input);

        /// <summary>
        /// 删除规章制度
        /// </summary>
        /// <param name="id">规章制度ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除规章制度
        /// </summary>
        /// <param name="regulationIds">规章制度ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] regulationIds);

        /// <summary>
        /// 获取规章制度树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        Task<List<TaktRegulationDto>> GetTreeAsync(long? parentId = null);

        /// <summary>
        /// 导入规章制度数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出规章制度数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktRegulationQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);
    }
} 



