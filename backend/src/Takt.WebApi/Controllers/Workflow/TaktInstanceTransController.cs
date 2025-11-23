//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstanceTransController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史控制器
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流实例流转历史控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流流转历史")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class TaktInstanceTransController : TaktBaseController
{
    private readonly ITaktInstanceTransService _instanceTransService;

    public TaktInstanceTransController(
        ITaktInstanceTransService instanceTransService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localizationService) : base(logger, currentUser, localizationService)
    {
        _instanceTransService = instanceTransService ?? throw new ArgumentNullException(nameof(instanceTransService));
    }

    /// <summary>
    /// 获取工作流实例流转列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPerm("workflow:manage:trans:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktInstanceTransQueryDto query)
    {
        try
        {
            var result = await _instanceTransService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流实例流转
    /// </summary>
    /// <param name="id">工作流实例流转ID</param>
    /// <returns>工作流实例流转</returns>
    [HttpGet("{id}")]
    [TaktPerm("workflow:manage:trans:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _instanceTransService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流实例流转不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }


    /// <summary>
    /// 删除工作流实例流转
    /// </summary>
    /// <param name="id">工作流实例流转ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktPerm("workflow:manage:trans:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _instanceTransService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流实例流转删除成功");
            else
                return Error("工作流实例流转不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流实例流转
    /// </summary>
    /// <param name="ids">工作流实例流转ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("workflow:manage:trans:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流实例流转");

            var result = await _instanceTransService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流实例流转成功");
            else
                return Error("批量删除工作流实例流转失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据实例ID获取流转列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转列表</returns>
    [HttpGet("instance/{instanceId}")]
    [TaktPerm("workflow:manage:trans:query")]
    public async Task<IActionResult> GetByInstanceIdAsync(long instanceId)
    {
        try
        {
            var result = await _instanceTransService.GetByInstanceIdAsync(instanceId);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据实例ID获取流转分页列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("instance/{instanceId}/paged")]
    [TaktPerm("workflow:manage:trans:query")]
    public async Task<IActionResult> GetByInstanceIdPagedAsync(long instanceId, [FromQuery] TaktInstanceTransQueryDto query)
    {
        try
        {
            var result = await _instanceTransService.GetByInstanceIdPagedAsync(instanceId, query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }


    /// <summary>
    /// 导出工作流实例流转数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("workflow:manage:trans:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktInstanceTransQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var result = await _instanceTransService.ExportAsync(query, sheetName, fileName);
        var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? "application/zip"
            : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
        var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
        Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
        return File(result.content, contentType, result.fileName);
    }

    /// <summary>
    /// 获取我的待办任务（当前用户为处理人且状态为待处理）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页待办任务</returns>
    [HttpGet("my/todo")]
    [TaktPerm("workflow:manage:trans:todo")]
    public async Task<IActionResult> GetUserTodoListAsync([FromQuery] TaktInstanceTransQueryDto query)
    {
        // 这里假设NodeId或其它业务字段可用于筛选当前用户的待办
        query.Status = 0; // 0=待处理
        query.CreateBy = _currentUser.UserName;
        var result = await _instanceTransService.GetListAsync(query);
        return Success(result);
    }

    /// <summary>
    /// 获取我的已办任务（当前用户为处理人且状态为已处理）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页已办任务</returns>
    [HttpGet("my/done")]
    [TaktPerm("workflow:manage:trans:done")]
    public async Task<IActionResult> GetUserDoneListAsync([FromQuery] TaktInstanceTransQueryDto query)
    {
        query.Status = 1; // 1=已处理
        query.CreateBy = _currentUser.UserName;
        var result = await _instanceTransService.GetListAsync(query);
        return Success(result);
    }

    /// <summary>
    /// 获取我的流程列表（当前用户发起的流程）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页流程列表</returns>
    [HttpGet("my/process")]
    [TaktPerm("workflow:manage:trans:process")]
    public async Task<IActionResult> GetUserProcessListAsync([FromQuery] TaktInstanceTransQueryDto query)
    {
        // 查询当前用户发起的流程，通常Status为0表示进行中
        query.CreateBy = _currentUser.UserName;
        var result = await _instanceTransService.GetListAsync(query);
        return Success(result);
    }
} 

