//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktInstanceTransService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史服务接口
//===================================================================

using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流实例流转历史服务接口
/// </summary>
public interface ITaktInstanceTransService
{
    /// <summary>
    /// 获取流转历史列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktInstanceTransDto>> GetListAsync(TaktInstanceTransQueryDto query);

    /// <summary>
    /// 根据ID获取流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>流转历史</returns>
    Task<TaktInstanceTransDto?> GetByIdAsync(long id);


    /// <summary>
    /// 删除流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除实例流转
    /// </summary>
    /// <param name="ids">实例流转ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 获取工作流实例的流转历史
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转历史列表</returns>
    Task<List<TaktInstanceTransDto>> GetByInstanceIdAsync(long instanceId);

    /// <summary>
    /// 获取工作流实例的流转历史分页列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktInstanceTransDto>> GetByInstanceIdPagedAsync(long instanceId, TaktInstanceTransQueryDto query);

    /// <summary>
    /// 导出流转历史数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktInstanceTransQueryDto query, string? sheetName, string? fileName);
} 

