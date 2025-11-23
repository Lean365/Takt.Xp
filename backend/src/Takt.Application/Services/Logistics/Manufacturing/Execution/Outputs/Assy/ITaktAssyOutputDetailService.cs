#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktAssyOutputDetailService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 组立明细服务接口
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy;

namespace Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立明细服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktAssyOutputDetailService
    {
        /// <summary>
        /// 获取组立明细分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>组立明细分页列表</returns>
        Task<TaktPagedResult<TaktAssyOutputDetailDto>> GetListAsync(TaktAssyOutputDetailQueryDto query);

        /// <summary>
        /// 获取组立明细详情
        /// </summary>
        /// <param name="assyMpOutputDetailId">组立明细ID</param>
        /// <returns>组立明细详情</returns>
        Task<TaktAssyOutputDetailDto> GetByIdAsync(long assyMpOutputDetailId);

        /// <summary>
        /// 根据组立日报ID获取明细列表
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>组立明细列表</returns>
        Task<List<TaktAssyOutputDetailDto>> GetByAssyMpOutputIdAsync(long assyMpOutputId);

        /// <summary>
        /// 创建组立明细
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>组立明细ID</returns>
        Task<long> CreateAsync(TaktAssyOutputDetailCreateDto input);

        /// <summary>
        /// 批量创建组立明细
        /// </summary>
        /// <param name="inputs">创建对象列表</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateRangeAsync(List<TaktAssyOutputDetailCreateDto> inputs);

        /// <summary>
        /// 更新组立明细
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktAssyOutputDetailUpdateDto input);

        /// <summary>
        /// 删除组立明细
        /// </summary>
        /// <param name="assyMpOutputDetailId">组立明细ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long assyMpOutputDetailId);

        /// <summary>
        /// 根据组立日报ID删除明细
        /// </summary>
        /// <param name="assyOutputId">组立日报ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteByAssyOutputIdAsync(long assyOutputId);

        /// <summary>
        /// 批量删除组立明细
        /// </summary>
        /// <param name="assyOutputDetailIds">组立明细ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] assyOutputDetailIds);

        /// <summary>
        /// 导入组立明细数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "组立明细信息");

        /// <summary>
        /// 导出组立明细数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktAssyOutputDetailQueryDto query, string sheetName = "组立明细信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "组立明细信息");
    }
} 



