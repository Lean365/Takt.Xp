#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: ITaktPersonnelOperationRateService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 人员稼动率服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 人员稼动率服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktPersonnelOperationRateService
    {
        /// <summary>
        /// 获取人员稼动率分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        Task<TaktPagedResult<TaktPersonnelOperationRateDto>> GetListAsync(TaktPersonnelOperationRateQueryDto query);

        /// <summary>
        /// 根据ID获取人员稼动率详情
        /// </summary>
        /// <param name="id">人员稼动率ID</param>
        /// <returns>返回人员稼动率详情</returns>
        Task<TaktPersonnelOperationRateDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建人员稼动率
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回人员稼动率ID</returns>
        Task<long> CreateAsync(TaktPersonnelOperationRateCreateDto input);

        /// <summary>
        /// 更新人员稼动率
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateAsync(TaktPersonnelOperationRateUpdateDto input);

        /// <summary>
        /// 删除人员稼动率
        /// </summary>
        /// <param name="id">人员稼动率ID</param>
        /// <returns>返回是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除人员稼动率
        /// </summary>
        /// <param name="ids">人员稼动率ID集合</param>
        /// <returns>返回是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 更新人员稼动率状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktPersonnelOperationRateStatusDto input);

        /// <summary>
        /// 导入人员稼动率数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "人员稼动率信息");

        /// <summary>
        /// 导出人员稼动率数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktPersonnelOperationRateQueryDto query, string sheetName = "人员稼动率信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "人员稼动率信息");
    }
}



