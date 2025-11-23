#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: ITaktOperationRateService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 稼动率服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 稼动率服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktOperationRateService
    {
        /// <summary>
        /// 获取稼动率分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        Task<TaktPagedResult<TaktOperationRateDto>> GetListAsync(TaktOperationRateQueryDto query);

        /// <summary>
        /// 根据ID获取稼动率详情
        /// </summary>
        /// <param name="id">稼动率ID</param>
        /// <returns>返回稼动率详情</returns>
        Task<TaktOperationRateDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建稼动率
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回稼动率ID</returns>
        Task<long> CreateAsync(TaktOperationRateCreateDto input);

        /// <summary>
        /// 更新稼动率
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateAsync(TaktOperationRateUpdateDto input);

        /// <summary>
        /// 删除稼动率
        /// </summary>
        /// <param name="id">稼动率ID</param>
        /// <returns>返回是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除稼动率
        /// </summary>
        /// <param name="ids">稼动率ID集合</param>
        /// <returns>返回是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 更新稼动率状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktOperationRateStatusDto input);

        /// <summary>
        /// 导入稼动率数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "稼动率信息");

        /// <summary>
        /// 导出稼动率数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktOperationRateQueryDto query, string sheetName = "稼动率信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "稼动率信息");
    }
}



