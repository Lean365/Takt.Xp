//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSchemeController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流定义控制器
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 工作流定义控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流定义")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class TaktSchemeController : TaktBaseController
{
    private readonly ITaktSchemeService _schemeService;

    public TaktSchemeController(ITaktSchemeService schemeService, 
    ITaktLogger logger, 
    ITaktCurrentUser currentUser, 
    ITaktLocalizationService localizationService) : 
    base(logger, currentUser, localizationService)
    {
        _schemeService = schemeService ?? throw new ArgumentNullException(nameof(schemeService));
    }

    /// <summary>
    /// 获取工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPerm("workflow:manage:scheme:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktSchemeQueryDto query)
    {
        try
        {
            var result = await _schemeService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>工作流定义</returns>
    [HttpGet("{id}")]
    [TaktPerm("workflow:manage:scheme:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _schemeService.GetByIdAsync(id);
            if (result == null)
                return Error("工作流定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据键获取工作流定义
    /// </summary>
    /// <param name="schemeKey">工作流定义键</param>
    /// <returns>工作流定义</returns>
    [HttpGet("key/{schemeKey}")]
    [TaktPerm("workflow:manage:scheme:query")]
    public async Task<IActionResult> GetByKeyAsync(string schemeKey)
    {
        try
        {
            var result = await _schemeService.GetByKeyAsync(schemeKey);
            if (result == null)
                return Error("工作流定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取当前用户的工作流定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("current")]
    [TaktPerm("workflow:manage:scheme:list")]
    public async Task<IActionResult> GetCurrentAsync([FromQuery] TaktSchemeQueryDto query)
    {
        try
        {
            var result = await _schemeService.GetCurrentAsync(_currentUser.UserId, query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取工作流定义选项列表
    /// </summary>
    /// <returns>工作流定义选项列表</returns>
    [HttpGet("options")]
    [TaktPerm("workflow:manage:scheme:list")]
    public async Task<IActionResult> GetOptionsAsync()
    {
        try
        {
            var result = await _schemeService.GetOptionsAsync();
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建工作流定义
    /// </summary>
    /// <param name="input">工作流定义创建DTO</param>
    /// <returns>工作流定义ID</returns>
    [HttpPost]
    [TaktPerm("workflow:manage:scheme:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktSchemeCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _schemeService.CreateAsync(input);
            return Success(id, "工作流定义创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新工作流定义
    /// </summary>
    /// <param name="input">工作流定义更新DTO</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("workflow:manage:scheme:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktSchemeUpdateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var result = await _schemeService.UpdateAsync(input.SchemeId, input);
            if (result)
                return Success(true, "工作流定义更新成功");
            else
                return Error("工作流定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 克隆工作流定义
    /// </summary>
    /// <param name="input">工作流定义克隆信息</param>
    /// <returns>新工作流定义ID</returns>
    [HttpPost("clone")]
    [TaktPerm("workflow:manage:scheme:create")]
    public async Task<IActionResult> CloneAsync([FromBody] TaktSchemeCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _schemeService.CloneAsync(input);
            return Success(id, "工作流定义克隆成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除工作流定义
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktPerm("workflow:manage:scheme:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _schemeService.DeleteAsync(id);
            if (result)
                return Success(true, "工作流定义删除成功");
            else
                return Error("工作流定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除工作流定义
    /// </summary>
    /// <param name="ids">工作流定义ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("workflow:manage:scheme:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的工作流定义");

            var result = await _schemeService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除工作流定义成功");
            else
                return Error("批量删除工作流定义失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新工作流定义状态
    /// </summary>
    /// <param name="id">工作流定义ID</param>
    /// <param name="status">新状态</param>
    /// <param name="reason">原因</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/status")]
    [TaktPerm("workflow:manage:scheme:update")]
    public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] int status, [FromQuery] string? reason = null)
    {
        try
        {
            var result = await _schemeService.UpdateStatusAsync(id, status, reason);
            if (result)
                return Success(true, "工作流定义状态更新成功");
            else
                return Error("工作流定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("workflow:manage:scheme:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var result = await _schemeService.GetTemplateAsync(sheetName, fileName);
        return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入工作流定义数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入工作流定义")]
    [TaktPerm("workflow:manage:scheme:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string? sheetName = null)
    {
        using var stream = file.OpenReadStream();
        var (success, fail) = await _schemeService.ImportAsync(stream, sheetName);
        return Success(new { success, fail }, _localization.L("Workflow.Scheme.Import.Success"));
    }

    /// <summary>
    /// 导出工作流定义数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("workflow:manage:scheme:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktSchemeQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var result = await _schemeService.ExportAsync(query, sheetName, fileName);
        var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? "application/zip"
            : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
        var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
        Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
        return File(result.content, contentType, result.fileName);
    }
}

