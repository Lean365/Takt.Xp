#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: ITaktModelDestinationService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 机种仕向服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 机种仕向服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktModelDestinationService
    {
        /// <summary>
        /// 获取机种仕向分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        Task<TaktPagedResult<TaktModelDestinationDto>> GetListAsync(TaktModelDestinationQueryDto query);

        /// <summary>
        /// 根据ID获取机种仕向详情
        /// </summary>
        /// <param name="id">机种仕向ID</param>
        /// <returns>返回机种仕向详情</returns>
        Task<TaktModelDestinationDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建机种仕向
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回机种仕向ID</returns>
        Task<long> CreateAsync(TaktModelDestinationCreateDto input);

        /// <summary>
        /// 更新机种仕向
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateAsync(TaktModelDestinationUpdateDto input);

        /// <summary>
        /// 删除机种仕向
        /// </summary>
        /// <param name="id">机种仕向ID</param>
        /// <returns>返回是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除机种仕向
        /// </summary>
        /// <param name="ids">机种仕向ID集合</param>
        /// <returns>返回是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入机种仕向数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "机种仕向信息");

        /// <summary>
        /// 导出机种仕向数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktModelDestinationQueryDto query, string sheetName = "机种仕向信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "机种仕向信息");

        /// <summary>
        /// 根据物料编码获取机种编码
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>机种编码列表</returns>
        Task<List<string>> GetModelCodesByMaterialAsync(string materialCode);

        /// <summary>
        /// 根据物料编码获取仕向编码
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>仕向编码列表</returns>
        Task<List<string>> GetDestinationCodesByMaterialAsync(string materialCode);

        /// <summary>
        /// 根据物料编码获取机种仕向信息
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>机种仕向信息列表</returns>
        Task<List<TaktModelDestinationDto>> GetModelDestinationsByMaterialAsync(string materialCode);
    }
}



