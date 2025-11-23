//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginLogController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志控制器
//===================================================================

using System.ComponentModel;
using Takt.Application.Dtos.Logging;
using Takt.Application.Services.Logging;

namespace Takt.WebApi.Controllers.Logging
{
  /// <summary>
  /// 登录日志控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-20
  /// </remarks>
  [Route("api/[controller]", Name = "登录日志")]
  [ApiController]
  [ApiModule("logging", "审计日志")]
  public class TaktLoginLogController : TaktBaseController
  {
    private readonly ITaktLoginLogService _loginLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="loginLogService">登录日志服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktLoginLogController(
        ITaktLoginLogService loginLogService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _loginLogService = loginLogService;
    }

    /// <summary>
    /// 获取登录日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>登录日志分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("logging:sqldifflog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktLoginLogQueryDto query)
    {
      var result = await _loginLogService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取登录日志详情
    /// </summary>
    /// <param name="logId">日志ID</param>
    /// <returns>登录日志详情</returns>
    [HttpGet("{logId}")]
    [TaktPerm("logging:sqldifflog:query")]
    public async Task<IActionResult> GetByIdAsync(long logId)
    {
      var result = await _loginLogService.GetByIdAsync(logId);
      return Success(result);
    }

    /// <summary>
    /// 清空登录日志
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpDelete("cleanup")]
    [TaktPerm("logging:loginlog:cleanup")]
    public async Task<IActionResult> CleanupAsync()
    {
      var result = await _loginLogService.ClearUpAsync();
      return Success(result);
    }

    /// <summary>
    /// 导出登录日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的Excel文件</returns>
    [HttpGet("export")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [TaktPerm("logging:sqldifflog:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktLoginLogQueryDto query, [FromQuery] string sheetName = "登录日志")
    {
      var result = await _loginLogService.ExportAsync(query, sheetName);
      var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
          ? "application/zip"
          : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
      var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
      Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
      return File(result.content, contentType, result.fileName);
    }

    [HttpPost("unlock/{userId}")]
    [Description("解锁用户")]
    [TaktPerm("logging:sqldifflog:unlock")]
    public async Task<TaktApiResult> UnlockUserAsync([FromRoute] long userId)
    {
      await _loginLogService.UnlockUserAsync(userId);
      return TaktApiResult.Success();
    }
  }
}





