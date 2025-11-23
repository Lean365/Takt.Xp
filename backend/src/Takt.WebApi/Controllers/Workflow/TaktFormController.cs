//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFormController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单控制器
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Application.Services.Workflow;

namespace Takt.WebApi.Controllers.Workflow;

/// <summary>
/// 表单控制器
/// </summary>
[ApiController]
[Route("api/[controller]", Name = "工作流表单")]
[ApiModule("workflow", "工作流管理")]
[Authorize]
public class TaktFormController : TaktBaseController
{
    private readonly ITaktFormService _formService;

    public TaktFormController(ITaktFormService formService,
    ITaktLogger logger,
    ITaktCurrentUser currentUser,
    ITaktLocalizationService localizationService) :
    base(logger, currentUser, localizationService)
    {
        _formService = formService ?? throw new ArgumentNullException(nameof(formService));
    }

    /// <summary>
    /// 获取表单定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("list")]
    [TaktPerm("workflow:manage:form:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktFormQueryDto query)
    {
        try
        {
            var result = await _formService.GetListAsync(query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据ID获取表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>表单定义</returns>
    [HttpGet("{id}")]
    [TaktPerm("workflow:manage:form:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        try
        {
            var result = await _formService.GetByIdAsync(id);
            if (result == null)
                return Error("表单定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 根据键获取表单定义
    /// </summary>
    /// <param name="formKey">表单键</param>
    /// <returns>表单定义</returns>
    [HttpGet("key/{formKey}")]
    [TaktPerm("workflow:manage:form:query")]
    public async Task<IActionResult> GetByKeyAsync(string formKey)
    {
        try
        {
            var result = await _formService.GetByKeyAsync(formKey);
            if (result == null)
                return Error("表单定义不存在");

            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取当前用户的表单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    [HttpGet("current/{userId}")]
    [TaktPerm("workflow:manage:form:list")]
    public async Task<IActionResult> GetCurrentAsync(long userId, [FromQuery] TaktFormQueryDto query)
    {
        try
        {
            var result = await _formService.GetCurrentAsync(userId, query);
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 获取表单定义选项列表
    /// </summary>
    /// <returns>表单定义选项列表</returns>
    [HttpGet("options")]
    [TaktPerm("workflow:manage:form:query")]
    public async Task<IActionResult> GetOptionsAsync()
    {
        try
        {
            var result = await _formService.GetOptionsAsync();
            return Success(result);
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 创建表单定义
    /// </summary>
    /// <param name="input">表单定义创建DTO</param>
    /// <returns>表单定义ID</returns>
    [HttpPost]
    [TaktPerm("workflow:manage:form:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktFormCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _formService.CreateAsync(input);
            return Success(id, "表单定义创建成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新表单定义
    /// </summary>
    /// <param name="input">表单定义更新DTO</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("workflow:manage:form:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktFormUpdateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var result = await _formService.UpdateAsync(input.FormId, input);
            if (result)
                return Success(true, "表单定义更新成功");
            else
                return Error("表单定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 克隆表单定义
    /// </summary>
    /// <param name="input">表单定义克隆信息</param>
    /// <returns>新表单定义ID</returns>
    [HttpPost("clone")]
    [TaktPerm("workflow:manage:form:create")]
    public async Task<IActionResult> CloneAsync([FromBody] TaktFormCreateDto input)
    {
        try
        {
            if (input == null)
                return Error("请求参数不能为空");

            var id = await _formService.CloneAsync(input);
            return Success(id, "表单定义克隆成功");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 删除表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktPerm("workflow:manage:form:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        try
        {
            var result = await _formService.DeleteAsync(id);
            if (result)
                return Success(true, "表单定义删除成功");
            else
                return Error("表单定义不存在");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 批量删除表单定义
    /// </summary>
    /// <param name="ids">表单定义ID数组</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("workflow:manage:form:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
        try
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择要删除的表单定义");

            var result = await _formService.BatchDeleteAsync(ids);
            if (result)
                return Success(true, "批量删除表单定义成功");
            else
                return Error("批量删除表单定义失败");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }

    /// <summary>
    /// 更新表单状态
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="status">新状态</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/status")]
    [TaktPerm("workflow:manage:form:update")]
    public async Task<IActionResult> UpdateStatusAsync(long id, [FromQuery] int status)
    {
        try
        {
            var result = await _formService.UpdateStatusAsync(id, status);
            if (result)
                return Success(true, "表单状态更新成功");
            else
                return Error("表单定义不存在");
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
    [TaktPerm("workflow:manage:form:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var result = await _formService.GetTemplateAsync(sheetName, fileName);
        return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入表单数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入表单定义")]
    [TaktPerm("workflow:manage:form:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string? sheetName = null)
    {
        using var stream = file.OpenReadStream();
        var (success, fail) = await _formService.ImportAsync(stream, sheetName);
        return Success(new { success, fail }, _localization.L("Workflow.Form.Import.Success"));
    }

    /// <summary>
    /// 导出表单数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("workflow:manage:form:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktFormQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
    {
        var result = await _formService.ExportAsync(query, sheetName, fileName);
        var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
            ? "application/zip"
            : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
        var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
        Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
        return File(result.content, contentType, result.fileName);
    }
}

