//===================================================================
// 项目名 : Takt.WebApi
// 文件名 : TaktQuartzLogController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 任务日志控制器
//===================================================================

using Takt.Application.Dtos.Logging;
using Takt.Application.Services.Logging;

namespace Takt.WebApi.Controllers.Logging
{
  /// <summary>
  /// 任务日志控制器
  /// </summary>
  [Route("api/[controller]", Name = "任务日志")]
  [ApiController]
  [ApiModule("logging", "审计日志")]
  public class TaktQuartzLogController : TaktBaseController
  {
    private readonly ITaktQuartzLogService _quartzLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="quartzLogService">任务日志服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    public TaktQuartzLogController(
        ITaktQuartzLogService quartzLogService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _quartzLogService = quartzLogService;
    }

    /// <summary>
    /// 获取任务日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>任务日志分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("logging:quartzlog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktQuartzLogQueryDto query)
    {
      var result = await _quartzLogService.GetPagedAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取任务日志详情
    /// </summary>
    /// <param name="logId">日志ID</param>
    /// <returns>任务日志详情</returns>
    [HttpGet("{logId}")]
    [TaktPerm("logging:quartzlog:query")]
    public async Task<IActionResult> GetByIdAsync(long logId)
    {
      var result = await _quartzLogService.GetByIdAsync(logId);
      return Success(result);
    }

    /// <summary>
    /// 清空任务日志
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpDelete("cleanup")]
    [TaktPerm("logging:quartzlog:cleanup")]
    public async Task<IActionResult> CleanupAsync()
    {
      var result = await _quartzLogService.ClearUpAsync();
      return Success(result);
    }

    /// <summary>
    /// 导出任务日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的Excel文件</returns>
    [HttpGet("export")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [TaktPerm("logging:quartzlog:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktQuartzLogQueryDto query, [FromQuery] string sheetName = "任务日志数据")
    {
      var result = await _quartzLogService.ExportAsync(query, sheetName);
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




