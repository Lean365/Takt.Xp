#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSqlDiffLogController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 差异日志控制器
//===================================================================

using Takt.Application.Dtos.Logging;
using Takt.Application.Services.Logging;

namespace Takt.WebApi.Controllers.Logging
{
  /// <summary>
  /// 差异日志控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-19
  /// </remarks>
  [Route("api/[controller]", Name = "差异日志")]
  [ApiController]
  [ApiModule("logging", "审计日志")]
  public class TaktSqlDiffLogController : TaktBaseController
  {
    private readonly ITaktSqlDiffLogService _sqlDiffLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sqlDiffLogService">差异日志服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktSqlDiffLogController(
        ITaktSqlDiffLogService sqlDiffLogService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _sqlDiffLogService = sqlDiffLogService;
    }

    /// <summary>
    /// 获取差异日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>差异日志分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("logging:sqldifflog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktSqlDiffLogQueryDto query)
    {
      var result = await _sqlDiffLogService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取差异日志详情
    /// </summary>
    /// <param name="logId">日志ID</param>
    /// <returns>差异日志详情</returns>
    [HttpGet("{logId}")]
    [TaktPerm("logging:sqldifflog:query")]
    public async Task<IActionResult> GetByIdAsync(long logId)
    {
      var result = await _sqlDiffLogService.GetByIdAsync(logId);
      return Success(result);
    }

    /// <summary>
    /// 清空差异日志
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpDelete("cleanup")]
    [TaktPerm("logging:sqldifflog:cleanup")]
    public async Task<IActionResult> CleanupAsync()
    {
      var result = await _sqlDiffLogService.ClearUpAsync();
      return Success(result);
    }

    /// <summary>
    /// 导出差异日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [TaktPerm("logging:sqldifflog:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktSqlDiffLogQueryDto query, [FromQuery] string sheetName = "差异日志")
    {
      var result = await _sqlDiffLogService.ExportAsync(query, sheetName);
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





