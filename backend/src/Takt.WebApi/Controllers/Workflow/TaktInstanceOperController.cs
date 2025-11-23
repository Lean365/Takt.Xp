//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstanceOperController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录控制器
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流实例操作记录控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流操作记录")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class TaktInstanceOperController : TaktBaseController
{
    private readonly ITaktInstanceOperService _instanceOperService;

    public TaktInstanceOperController(
        ITaktInstanceOperService instanceOperService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localizationService) : base(logger, currentUser, localizationService)
    {
        _instanceOperService = instanceOperService ?? throw new ArgumentNullException(nameof(instanceOperService));
    }

    /// <summary>
    /// 获取工作流实例操作列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPerm("workflow:manage:oper:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktInstanceOperQueryDto query)
    {
        try
        {
            var result = await _instanceOperService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流实例操作
    /// </summary>
    /// <param name="id">工作流实例操作ID</param>
    /// <returns>工作流实例操作</returns>
    [HttpGet("{id}")]
    [TaktPerm("workflow:manage:oper:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _instanceOperService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流实例操作不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建审批操作
    /// </summary>
    /// <param name="input">审批参数</param>
    /// <returns>操作ID</returns>
    [HttpPost("approve")]
    [TaktPerm("workflow:manage:oper:create")]
    public async Task<IActionResult> CreateApproveAsync([FromBody] TaktInstanceApproveDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _instanceOperService.CreateApproveAsync(input);
            return Success(id, "审批操作创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建工作流实例操作
    /// </summary>
    /// <param name="input">工作流实例操作创建DTO</param>
    /// <returns>工作流实例操作ID</returns>
    [HttpPost]
    [TaktPerm("workflow:manage:oper:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktInstanceOperCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _instanceOperService.CreateAsync(input);
            return Success(id, "工作流实例操作创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除工作流实例操作
    /// </summary>
    /// <param name="id">工作流实例操作ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktPerm("workflow:manage:oper:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _instanceOperService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流实例操作删除成功");
            else
                return Error("工作流实例操作不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流实例操作
    /// </summary>
    /// <param name="ids">工作流实例操作ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("workflow:manage:oper:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流实例操作");

            var result = await _instanceOperService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流实例操作成功");
            else
                return Error("批量删除工作流实例操作失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取我的操作列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("my/{userId}")]
    [TaktPerm("workflow:manage:oper:list")]
    public async Task<IActionResult> GetMyOperationsAsync(long userId, [FromQuery] TaktInstanceOperQueryDto query)
    {
        try
        {
            var result = await _instanceOperService.GetMyOperationsAsync(userId, query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据实例ID获取操作列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>操作列表</returns>
    [HttpGet("instance/{instanceId}")]
    [TaktPerm("workflow:manage:oper:query")]
    public async Task<IActionResult> GetByInstanceIdAsync(long instanceId)
    {
        try
        {
            var query = new TaktInstanceOperQueryDto { InstanceId = instanceId };
            var result = await _instanceOperService.GetListAsync(query);
            return Success(result.Rows);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 导出工作流实例操作数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("workflow:manage:oper:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktInstanceOperQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var result = await _instanceOperService.ExportAsync(query, sheetName, fileName);
        var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? "application/zip"
            : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
        var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
        Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
        return File(result.content, contentType, result.fileName);
    }
} 

