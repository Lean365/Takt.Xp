//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktProdOrderService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 生产工单服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktProdOrderService
    {
        /// <summary>
        /// 获取生产工单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>生产工单分页列表</returns>
        Task<TaktPagedResult<TaktProdOrderDto>> GetListAsync(TaktProdOrderQueryDto query);

        /// <summary>
        /// 获取生产工单详情
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>生产工单详情</returns>
        Task<TaktProdOrderDto> GetByIdAsync(long prodOrderId);

        /// <summary>
        /// 创建生产工单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>生产工单ID</returns>
        Task<long> CreateAsync(TaktProdOrderCreateDto input);

        /// <summary>
        /// 更新生产工单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktProdOrderUpdateDto input);

        /// <summary>
        /// 删除生产工单
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long prodOrderId);

        /// <summary>
        /// 批量删除生产工单
        /// </summary>
        /// <param name="ids">生产工单ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入生产工单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "ProdOrder");

        /// <summary>
        /// 导出生产工单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktProdOrderQueryDto query, string sheetName = "ProdOrder");

        /// <summary>
        /// 获取生产工单导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "ProdOrder");

        /// <summary>
        /// 更新生产工单状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktProdOrderStatusDto input);

        /// <summary>
        /// 获取生产工单选项列表
        /// </summary>
        /// <returns>生产工单选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();





        /// <summary>
        /// 更新生产工单数量
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <param name="producedQuantity">已生产数量</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateProducedQuantityAsync(long prodOrderId, decimal producedQuantity);



        /// <summary>
        /// 获取生产工单变更记录
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>变更记录列表</returns>
        Task<List<TaktProdOrderChangeLogDto>> GetChangeLogsAsync(long prodOrderId);



    }
}




