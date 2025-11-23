//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktAdvertBillingService.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 广告计费服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Advertisement;

namespace Takt.Application.Services.Routine.Advertisement
{
    /// <summary>
    /// 广告计费服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-19
    /// </remarks>
    public interface ITaktAdvertBillingService
    {
        /// <summary>
        /// 获取广告计费分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>广告计费分页列表</returns>
        Task<TaktPagedResult<TaktAdvertBillingDto>> GetListAsync(TaktAdvertBillingQueryDto query);

        /// <summary>
        /// 获取广告计费详情
        /// </summary>
        /// <param name="billingId">计费ID</param>
        /// <returns>广告计费详情</returns>
        Task<TaktAdvertBillingDto> GetByIdAsync(long billingId);

        /// <summary>
        /// 创建广告计费
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>计费ID</returns>
        Task<long> CreateAsync(TaktAdvertBillingCreateDto input);

        /// <summary>
        /// 更新广告计费
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktAdvertBillingUpdateDto input);

        /// <summary>
        /// 删除广告计费
        /// </summary>
        /// <param name="billingId">计费ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long billingId);

        /// <summary>
        /// 批量删除广告计费
        /// </summary>
        /// <param name="ids">计费ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 获取广告计费导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

        /// <summary>
        /// 导入广告计费数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName);

        /// <summary>
        /// 导出广告计费数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktAdvertBillingQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 更新广告计费状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktAdvertBillingStatusDto input);

        /// <summary>
        /// 审核广告计费
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        Task<bool> AuditAsync(TaktAdvertBillingAuditDto input);

        /// <summary>
        /// 启动广告计费
        /// </summary>
        /// <param name="input">启动对象</param>
        /// <returns>是否成功</returns>
        Task<bool> StartAsync(TaktAdvertBillingStartDto input);

        /// <summary>
        /// 结束广告计费
        /// </summary>
        /// <param name="input">结束对象</param>
        /// <returns>是否成功</returns>
        Task<bool> EndAsync(TaktAdvertBillingEndDto input);

        /// <summary>
        /// 更新广告计费统计信息
        /// </summary>
        /// <param name="input">统计信息对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatsAsync(TaktAdvertBillingStatsDto input);

        /// <summary>
        /// 获取广告计费统计信息
        /// </summary>
        /// <returns>广告计费统计信息</returns>
        Task<TaktAdvertBillingStatisticsDto> GetStatisticsAsync();
    }
}


