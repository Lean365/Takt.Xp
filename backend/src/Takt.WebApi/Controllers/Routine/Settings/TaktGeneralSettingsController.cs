//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGeneralSettingsController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 通用设置控制器
//===================================================================

using Takt.Application.Dtos.Routine.Settings;
using Takt.Application.Services.Routine.Settings;

namespace Takt.WebApi.Controllers.Routine.Settings
{
  /// <summary>
  /// 通用设置控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-20
  /// </remarks>
  [Route("api/[controller]", Name = "通用设置")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktGeneralSettingsController : TaktBaseController
  {
    private readonly ITaktGeneralSettingsService _generalSettingsService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="generalSettingsService">通用设置服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktGeneralSettingsController(
        ITaktGeneralSettingsService generalSettingsService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _generalSettingsService = generalSettingsService;
    }

    /// <summary>
    /// 获取通用设置分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>通用设置分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:settings:list")]
    public async Task<IActionResult> GetPagedListAsync([FromQuery] TaktGeneralSettingsQueryDto query)
    {
      var result = await _generalSettingsService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取通用设置详情
    /// </summary>
    /// <param name="settingsId">设置ID</param>
    /// <returns>通用设置详情</returns>
    [HttpGet("{settingsId}")]
    [TaktPerm("routine:settings:query")]
    public async Task<IActionResult> GetByIdAsync(long settingsId)
    {
      var result = await _generalSettingsService.GetByIdAsync(settingsId);
      return Success(result);
    }

    /// <summary>
    /// 创建通用设置
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>设置ID</returns>
    [HttpPost]
    [TaktLog("创建通用设置")]
    [TaktPerm("routine:settings:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktGeneralSettingsCreateDto input)
    {
      var result = await _generalSettingsService.CreateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新通用设置
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktLog("更新通用设置")]
    [TaktPerm("routine:settings:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktGeneralSettingsUpdateDto input)
    {
      var result = await _generalSettingsService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除通用设置
    /// </summary>
    /// <param name="settingsId">设置ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{settingsId}")]
    [TaktLog("删除通用设置")]
    [TaktPerm("routine:settings:delete")]
    public async Task<IActionResult> DeleteAsync(long settingsId)
    {
      var result = await _generalSettingsService.DeleteAsync(settingsId);
      return Success(result);
    }

    /// <summary>
    /// 批量删除通用设置
    /// </summary>
    /// <param name="settingsIds">设置ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktLog("批量删除通用设置")]
    [TaktPerm("routine:settings:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] settingsIds)
    {
      var result = await _generalSettingsService.BatchDeleteAsync(settingsIds);
      return Success(result);
    }

    /// <summary>
    /// 更新通用设置状态
    /// </summary>
    /// <param name="settingsId">设置ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    [HttpPut("{settingsId}/status")]
    [TaktLog("更新通用设置状态")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [TaktPerm("routine:settings:update")]
    public async Task<IActionResult> UpdateStatusAsync(long settingsId, [FromQuery] int status)
    {
      var input = new TaktGeneralSettingsStatusDto
      {
        SettingsId = settingsId,
        Status = status
      };
      var result = await _generalSettingsService.UpdateStatusAsync(input);
      return Success(result);
    }


    /// <summary>
    /// 获取通用设置导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("routine:settings:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "GeneralSettings")
    {
      var result = await _generalSettingsService.GetTemplateAsync(sheetName);
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入通用设置数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入通用设置")]
    [TaktPerm("routine:settings:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "GeneralSettings")
    {
      using var stream = file.OpenReadStream();
      var (success, fail) = await _generalSettingsService.ImportAsync(stream, sheetName);
      return Success(new { success, fail }, _localization.L("Core.Config.Import.Success"));
    }

    /// <summary>
    /// 导出通用设置数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:settings:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktGeneralSettingsQueryDto query, [FromQuery] string sheetName = "GeneralSettings")
    {
      var result = await _generalSettingsService.ExportAsync(query, sheetName);
      var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
          ? "application/zip"
          : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
      var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
      Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
      return File(result.content, contentType, result.fileName);
    }
  }
}



