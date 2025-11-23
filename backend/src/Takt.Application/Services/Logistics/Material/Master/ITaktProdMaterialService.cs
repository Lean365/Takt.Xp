//===================================================================
// 项目名 : Takt.Application
// 文件名 : ITaktProdMaterialService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述: 生产物料服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Material.Master;
using Takt.Shared.Models;

namespace Takt.Application.Services.Logistics.Material.Master
{
    /// <summary>
    /// 生产物料服务接口
    /// </summary>
    public interface ITaktProdMaterialService
    {
        /// <summary>
        /// 获取生产物料分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        Task<TaktPagedResult<TaktProdMaterialDto>> GetListAsync(TaktProdMaterialQueryDto query);

        /// <summary>
        /// 根据ID获取生产物料详情
        /// </summary>
        /// <param name="id">生产物料ID</param>
        /// <returns>返回生产物料详情</returns>
        Task<TaktProdMaterialDto> GetByIdAsync(long id);

        /// <summary>
        /// 根据工厂编码和物料编码获取生产物料详情
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <param name="materialCode">物料编码</param>
        /// <returns>返回生产物料详情</returns>
        Task<TaktProdMaterialDto> GetByPlantAndMaterialCodeAsync(string plantCode, string materialCode);

        /// <summary>
        /// 创建生产物料
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回生产物料ID</returns>
        Task<long> CreateAsync(TaktProdMaterialCreateDto input);

        /// <summary>
        /// 更新生产物料
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateAsync(TaktProdMaterialUpdateDto input);

        /// <summary>
        /// 删除生产物料
        /// </summary>
        /// <param name="id">生产物料ID</param>
        /// <returns>返回是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除生产物料
        /// </summary>
        /// <param name="ids">生产物料ID集合</param>
        /// <returns>返回是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 更新生产物料状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktProdMaterialStatusDto input);

        /// <summary>
        /// 导入生产物料数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "生产物料信息");

        /// <summary>
        /// 导出生产物料数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktProdMaterialQueryDto query, string sheetName = "生产物料信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "生产物料信息");

        /// <summary>
        /// 获取生产物料选项列表
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回选项列表</returns>
        Task<List<TaktProdMaterialDto>> GetOptionsAsync(string plantCode, string? keyword = null);


    }
}


