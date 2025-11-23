//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktSchemeService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流定义服务接口
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Shared.Models;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流定义服务接口
/// </summary>
public interface ITaktSchemeService
{
    /// <summary>
    /// 获取工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSchemeDto>> GetListAsync(TaktSchemeQueryDto query);

    /// <summary>
    /// 根据ID获取工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>工作流定义</returns>
    Task<TaktSchemeDto?> GetByIdAsync(long id);

    /// <summary>
    /// 根据键获取工作流定义
    /// </summary>
    /// <param name="schemeKey">工作流定义键</param>
    /// <returns>工作流定义</returns>
    Task<TaktSchemeDto?> GetByKeyAsync(string schemeKey);

    /// <summary>
    /// 获取当前用户的工作流定义列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktSchemeDto>> GetCurrentAsync(long userId, TaktSchemeQueryDto query);

    /// <summary>
    /// 获取工作流定义选项列表
    /// </summary>
    /// <returns>工作流定义选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();

    /// <summary>
    /// 创建工作流定义
    /// </summary>
    /// <param name="dto">工作流定义创建DTO</param>
    /// <returns>工作流定义ID</returns>
    Task<long> CreateAsync(TaktSchemeCreateDto dto);

    /// <summary>
    /// 更新工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="dto">工作流定义更新DTO</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(long id, TaktSchemeUpdateDto dto);

    /// <summary>
    /// 克隆工作流定义
    /// </summary>
    /// <param name="dto">工作流定义克隆信息</param>
    /// <returns>新工作流定义ID</returns>
    Task<long> CloneAsync(TaktSchemeCreateDto dto);

    /// <summary>
    /// 删除工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除工作流定义
    /// </summary>
    /// <param name="ids">工作流定义ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 更新工作流定义状态
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="status">新状态</param>
    /// <param name="reason">原因</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(long id, int status, string? reason = null);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);

    /// <summary>
    /// 导入工作流定义数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName);

    /// <summary>
    /// 导出工作流定义数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktSchemeQueryDto query, string? sheetName, string? fileName);
} 


