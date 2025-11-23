//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktAssyOutputService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 组立日报服务接口
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy;

namespace Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立日报服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktAssyOutputService
    {
        /// <summary>
        /// 获取组立日报分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>组立日报分页列表</returns>
        Task<TaktPagedResult<TaktAssyOutputDto>> GetListAsync(TaktAssyOutputQueryDto query);

        /// <summary>
        /// 获取组立日报详情
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>组立日报详情</returns>
        Task<TaktAssyOutputDto> GetByIdAsync(long assyMpOutputId);

        /// <summary>
        /// 创建组立日报
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>组立日报ID</returns>
        Task<long> CreateAsync(TaktAssyOutputCreateDto input);

        /// <summary>
        /// 更新组立日报
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktAssyOutputUpdateDto input);

        /// <summary>
        /// 删除组立日报
        /// </summary>
        /// <param name="assyOutputId">组立日报ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long assyOutputId);

        /// <summary>
        /// 批量删除组立日报
        /// </summary>
        /// <param name="ids">组立日报ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);

        /// <summary>
        /// 导入组立日报数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "组立日报信息");

        /// <summary>
        /// 导出组立日报数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktAssyOutputQueryDto query, string sheetName = "组立日报信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "组立日报信息");
    }
}



