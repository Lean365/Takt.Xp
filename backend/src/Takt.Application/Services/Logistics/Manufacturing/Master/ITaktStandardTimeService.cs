//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktStandardTimeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 标准工时服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktStandardTimeService
    {
        /// <summary>
        /// 获取标准工时分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>标准工时分页列表</returns>
        Task<TaktPagedResult<TaktStandardTimeDto>> GetListAsync(TaktStandardTimeQueryDto query);

        /// <summary>
        /// 获取标准工时详情
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>标准工时详情</returns>
        Task<TaktStandardTimeDto> GetByIdAsync(long standardTimeId);

        /// <summary>
        /// 创建标准工时
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>标准工时ID</returns>
        Task<long> CreateAsync(TaktStandardTimeCreateDto input);

        /// <summary>
        /// 更新标准工时
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktStandardTimeUpdateDto input);

        /// <summary>
        /// 删除标准工时
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long standardTimeId);

        /// <summary>
        /// 批量删除标准工时
        /// </summary>
        /// <param name="ids">标准工时ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入标准工时数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "StandardTime");

        /// <summary>
        /// 导出标准工时数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktStandardTimeQueryDto query, string sheetName = "StandardTime");

        /// <summary>
        /// 获取标准工时导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "StandardTime");

        /// <summary>
        /// 更新标准工时状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktStandardTimeStatusDto input);

        /// <summary>
        /// 获取标准工时选项列表
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>标准工时选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync(string materialCode);



        /// <summary>
        /// 审核标准工时
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <param name="approver">审核人</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveAsync(long standardTimeId, string approver);

        /// <summary>
        /// 计算转换后标准工时
        /// </summary>
        /// <param name="standardMinutes">标准工时(分钟)</param>
        /// <param name="standardShorts">标准点数</param>
        /// <param name="pointsToMinutesRate">点数转分钟汇率</param>
        /// <returns>转换后标准工时(分钟)</returns>
        decimal CalculateConvertedMinutes(decimal standardMinutes, int standardShorts, decimal pointsToMinutesRate);

        /// <summary>
        /// 获取标准工时变更记录
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>变更记录列表</returns>
        Task<List<TaktStandardTimeChangeLogDto>> GetChangeLogsAsync(long standardTimeId);
    }
}




