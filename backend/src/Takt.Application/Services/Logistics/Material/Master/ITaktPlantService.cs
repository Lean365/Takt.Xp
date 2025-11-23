//===================================================================
// 项目名 : Takt.Application
// 文件名 : ITaktPlantService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述: 工厂服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Material.Master;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Material.Master
{
    /// <summary>
    /// 工厂服务接口
    /// </summary>
    public interface ITaktPlantService
    {
        /// <summary>
        /// 获取工厂分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        Task<TaktPagedResult<TaktPlantDto>> GetListAsync(TaktPlantQueryDto query);

        /// <summary>
        /// 根据ID获取工厂详情
        /// </summary>
        /// <param name="id">工厂ID</param>
        /// <returns>返回工厂详情</returns>
        Task<TaktPlantDto> GetByIdAsync(long id);

        /// <summary>
        /// 根据工厂编码获取工厂详情
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <returns>返回工厂详情</returns>
        Task<TaktPlantDto> GetByPlantCodeAsync(string plantCode);

        /// <summary>
        /// 创建工厂
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回工厂ID</returns>
        Task<long> CreateAsync(TaktPlantCreateDto input);

        /// <summary>
        /// 更新工厂
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateAsync(TaktPlantUpdateDto input);

        /// <summary>
        /// 删除工厂
        /// </summary>
        /// <param name="id">工厂ID</param>
        /// <returns>返回是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除工厂
        /// </summary>
        /// <param name="ids">工厂ID集合</param>
        /// <returns>返回是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 更新工厂状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktPlantStatusDto input);

        /// <summary>
        /// 导入工厂数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "工厂信息");

        /// <summary>
        /// 导出工厂数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktPlantQueryDto query, string sheetName = "工厂信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "工厂信息");

        /// <summary>
        /// 获取工厂选项列表
        /// </summary>
        /// <returns>工厂选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();


    }
}


