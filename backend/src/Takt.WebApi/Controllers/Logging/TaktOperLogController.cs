//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOperLogController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 操作日志控制器
//===================================================================

using Takt.Application.Dtos.Logging;
using Takt.Application.Services.Logging;

namespace Takt.WebApi.Controllers.Logging
{
  /// <summary>
  /// 操作日志控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-20
  /// </remarks>
  [Route("api/[controller]", Name = "操作日志")]
  [ApiController]
  [ApiModule("logging", "审计日志")]
  public class TaktOperLogController : TaktBaseController
  {
    private readonly ITaktOperLogService _operLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="operLogService">操作日志服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktOperLogController(
        ITaktOperLogService operLogService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _operLogService = operLogService;
    }

    /// <summary>
    /// 获取操作日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>操作日志分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("logging:operlog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktOperLogQueryDto query)
    {
      var result = await _operLogService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取操作日志详情
    /// </summary>
    /// <param name="logId">日志ID</param>
    /// <returns>操作日志详情</returns>
    [HttpGet("{logId}")]
    [TaktPerm("logging:operlog:query")]
    public async Task<IActionResult> GetByIdAsync(long logId)
    {
      var result = await _operLogService.GetByIdAsync(logId);
      return Success(result);
    }

    /// <summary>
    /// 清空操作日志
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpDelete("cleanup")]
    [TaktPerm("logging:operlog:cleanup")]
    public async Task<IActionResult> CleanupAsync()
    {
      var result = await _operLogService.ClearUpAsync();
      return Success(result);
    }

    /// <summary>
    /// 导出操作日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [TaktPerm("logging:operlog:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktOperLogQueryDto query, [FromQuery] string sheetName = "操作日志")
    {
      var result = await _operLogService.ExportAsync(query, sheetName);
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





