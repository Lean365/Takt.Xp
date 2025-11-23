//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktDictDataService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据服务接口
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktDictDataService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据服务接口
//===================================================================

using Takt.Application.Dtos.Routine.DataDictionary;

namespace Takt.Application.Services.Routine.DataDictionary
{
    /// <summary>
    /// 字典数据服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public interface ITaktDictDataService
    {
        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        Task<TaktPagedResult<TaktDictDataDto>> GetListAsync(TaktDictDataQueryDto query);

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="id">字典数据ID</param>
        /// <returns>字典数据详情</returns>
        Task<TaktDictDataDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典数据ID</returns>
        Task<long> CreateAsync(TaktDictDataCreateDto input);

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktDictDataUpdateDto input);

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long dictDataId);

        /// <summary>
        /// 批量删除字典数据
        /// </summary>
        /// <param name="dictDataIds">字典数据ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] dictDataIds);

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktDictDataQueryDto query, string sheetName = "TaktDictData");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "TaktDictData");


        /// <summary>
        /// 获取字典数据列表
        /// </summary>
        /// <returns>字典数据列表</returns>
        Task<List<TaktDictDataDto>> GetListAsync();

        /// <summary>
        /// 根据字典类型获取字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        Task<List<TaktDictDataDto>> GetListByDictTypeAsync(string dictType);
    }
}



