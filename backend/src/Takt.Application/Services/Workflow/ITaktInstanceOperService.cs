//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktInstanceOperService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录服务接口
//===================================================================

using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流实例操作记录服务接口
/// </summary>
public interface ITaktInstanceOperService
{
    /// <summary>
    /// 获取操作记录列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktInstanceOperDto>> GetListAsync(TaktInstanceOperQueryDto query);

    /// <summary>
    /// 根据ID获取操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>操作记录</returns>
    Task<TaktInstanceOperDto?> GetByIdAsync(long id);

    /// <summary>
    /// 创建工作流审批操作
    /// </summary>
    /// <param name="dto">审批DTO</param>
    /// <returns>操作记录ID</returns>
    Task<long> CreateApproveAsync(TaktInstanceApproveDto dto);

    /// <summary>
    /// 创建操作记录
    /// </summary>
    /// <param name="dto">创建DTO</param>
    /// <returns>操作记录ID</returns>
    Task<long> CreateAsync(TaktInstanceOperCreateDto dto);

    /// <summary>
    /// 删除操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除实例操作
    /// </summary>
    /// <param name="ids">实例操作ID数组</param>
    /// <returns>是否全部成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 获取我的操作记录列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktInstanceOperDto>> GetMyOperationsAsync(long userId, TaktInstanceOperQueryDto query);


    /// <summary>
    /// 导出操作记录数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    Task<(string fileName, byte[] content)> ExportAsync(TaktInstanceOperQueryDto query, string? sheetName, string? fileName);
} 

