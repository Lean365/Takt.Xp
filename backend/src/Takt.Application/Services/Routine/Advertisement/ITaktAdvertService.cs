//===================================================================
// 项目名: Takt.Application
// 文件名: ITaktAdvertService.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 广告服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Advertisement;

namespace Takt.Application.Services.Routine.Advertisement
{
    /// <summary>
    /// 广告服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktAdvertService
    {
        /// <summary>
        /// 获取广告分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>广告分页列表</returns>
        Task<TaktPagedResult<TaktAdvertDto>> GetListAsync(TaktAdvertQueryDto query);

        /// <summary>
        /// 获取广告详情
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>广告详情</returns>
        Task<TaktAdvertDto> GetByIdAsync(long advertId);

        /// <summary>
        /// 创建广告
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>广告ID</returns>
        Task<long> CreateAsync(TaktAdvertCreateDto input);

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktAdvertUpdateDto input);

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long advertId);

        /// <summary>
        /// 批量删除广告
        /// </summary>
        /// <param name="ids">广告ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入广告数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName);

        /// <summary>
        /// 导出广告数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktAdvertQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取广告导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

        /// <summary>
        /// 更新广告状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktAdvertStatusDto input);

        /// <summary>
        /// 审核广告
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        Task<bool> AuditAsync(TaktAdvertAuditDto input);

        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="input">发布对象</param>
        /// <returns>是否成功</returns>
        Task<bool> PublishAsync(TaktAdvertPublishDto input);

        /// <summary>
        /// 下线广告
        /// </summary>
        /// <param name="input">下线对象</param>
        /// <returns>是否成功</returns>
        Task<bool> OfflineAsync(TaktAdvertOfflineDto input);

        /// <summary>
        /// 更新广告统计信息
        /// </summary>
        /// <param name="input">统计信息对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatsAsync(TaktAdvertStatsDto input);

        /// <summary>
        /// 获取广告统计信息
        /// </summary>
        /// <returns>广告统计信息</returns>
        Task<TaktAdvertStatisticsDto> GetStatisticsAsync();

        /// <summary>
        /// 获取广告模板列表
        /// </summary>
        /// <returns>广告模板列表</returns>
        Task<List<TaktAdvertTemplateDto>> GetTemplateListAsync();

        /// <summary>
        /// 根据模板创建广告
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>广告ID</returns>
        Task<long> CreateFromTemplateAsync(long templateId);

        /// <summary>
        /// 复制广告
        /// </summary>
        /// <param name="advertId">源广告ID</param>
        /// <returns>新广告ID</returns>
        Task<long> CopyAsync(long advertId);

        /// <summary>
        /// 获取前台展示的广告列表
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <param name="limit">限制数量</param>
        /// <returns>广告列表</returns>
        Task<List<TaktAdvertDto>> GetFrontendListAsync(string position, int limit = 10);

        /// <summary>
        /// 记录广告点击
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> RecordClickAsync(long advertId);

        /// <summary>
        /// 记录广告展示
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> RecordShowAsync(long advertId);

        /// <summary>
        /// 记录广告关闭
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        Task<bool> RecordCloseAsync(long advertId);
    }
}

