//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExceptionLogController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志控制器
//===================================================================

using Takt.Application.Dtos.Logging;
using Takt.Application.Services.Logging;

namespace Takt.WebApi.Controllers.Logging
{
  /// <summary>
  /// 异常日志控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-20
  /// </remarks>
  [Route("api/[controller]", Name = "异常日志")]
  [ApiController]
  [ApiModule("logging", "审计日志")]
  public class TaktExceptionLogController : TaktBaseController
  {
    private readonly ITaktExceptionLogService _exceptionLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="exceptionLogService">异常日志服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktExceptionLogController(
        ITaktExceptionLogService exceptionLogService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _exceptionLogService = exceptionLogService;
    }

    /// <summary>
    /// 获取异常日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>异常日志分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("logging:exceptionlog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktExceptionLogQueryDto query)
    {
      var result = await _exceptionLogService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取异常日志详情
    /// </summary>
    /// <param name="logId">日志ID</param>
    /// <returns>异常日志详情</returns>
    [HttpGet("{logId}")]
    [TaktPerm("logging:exceptionlog:query")]
    public async Task<IActionResult> GetByIdAsync(long logId)
    {
      var result = await _exceptionLogService.GetByIdAsync(logId);
      return Success(result);
    }

    /// <summary>
    /// 清空异常日志
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpDelete("cleanup")]
    [TaktPerm("logging:exceptionlog:cleanup")]
    public async Task<IActionResult> CleanupAsync()
    {
      var result = await _exceptionLogService.ClearUpAsync();
      return Success(result);
    }

    /// <summary>
    /// 导出异常日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的Excel文件</returns>
    [HttpGet("export")]
    [TaktPerm("logging:exceptionlog:export")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktExceptionLogQueryDto query, [FromQuery] string sheetName = "异常日志数据")
    {
      var result = await _exceptionLogService.ExportAsync(query, sheetName);
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





