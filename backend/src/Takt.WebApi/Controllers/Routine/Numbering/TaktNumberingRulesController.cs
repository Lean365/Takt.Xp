//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNumberingRulesController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 单号规则控制器
//===================================================================

using Takt.Application.Dtos.Routine.Numbering;
using Takt.Application.Services.Routine.Numbering;

namespace Takt.WebApi.Controllers.Routine.Numbering
{
  /// <summary>
  /// 单号规则控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-03-07
  /// </remarks>
  [Route("api/[controller]", Name = "单号规则管理")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktNumberingRulesController : TaktBaseController
  {
    private readonly ITaktNumberingRulesService _numberRuleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="numberRuleService">单号规则服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktNumberingRulesController(
        ITaktNumberingRulesService numberRuleService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _numberRuleService = numberRuleService;
    }

    /// <summary>
    /// 获取单号规则分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>单号规则分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:numberrule:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktNumberingRulesQueryDto query)
    {
      var result = await _numberRuleService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取单号规则详情
    /// </summary>
    /// <param name="numberRuleId">单号规则ID</param>
    /// <returns>单号规则详情</returns>
    [HttpGet("{numberRuleId}")]
    [TaktPerm("routine:numberrule:query")]
    public async Task<IActionResult> GetByIdAsync(long numberRuleId)
    {
      var result = await _numberRuleService.GetByIdAsync(numberRuleId);
      return Success(result);
    }

    /// <summary>
    /// 获取单号规则选项列表
    /// </summary>
    /// <returns>单号规则选项列表</returns>
    [HttpGet("options")]
    [TaktPerm("routine:numberrule:query")]
    public async Task<IActionResult> GetOptionsAsync()
    {
      var result = await _numberRuleService.GetOptionsAsync();
      return Success(result);
    }

    /// <summary>
    /// 创建单号规则
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>单号规则ID</returns>
    [HttpPost]
    [TaktPerm("routine:numberrule:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktNumberingRulesCreateDto input)
    {
      var result = await _numberRuleService.CreateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新单号规则
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("routine:numberrule:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktNumberingRulesUpdateDto input)
    {
      var result = await _numberRuleService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除单号规则
    /// </summary>
    /// <param name="numberRuleId">单号规则ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{numberRuleId}")]
    [TaktPerm("routine:numberrule:delete")]
    public async Task<IActionResult> DeleteAsync(long numberRuleId)
    {
      var result = await _numberRuleService.DeleteAsync(numberRuleId);
      return Success(result);
    }

    /// <summary>
    /// 批量删除单号规则
    /// </summary>
    /// <param name="numberRuleIds">单号规则ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("routine:numberrule:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] numberRuleIds)
    {
      var result = await _numberRuleService.BatchDeleteAsync(numberRuleIds);
      return Success(result);
    }

    /// <summary>
    /// 更新单号规则状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut("status")]
    [TaktPerm("routine:numberrule:update")]
    public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktNumberingRulesStatusDto input)
    {
      var result = await _numberRuleService.UpdateStatusAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 获取单号规则导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("routine:numberrule:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "NumberRule")
    {
      var result = await _numberRuleService.GetTemplateAsync(sheetName);
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入单号规则数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入单号规则")]
    [TaktPerm("routine:numberrule:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "NumberRule")
    {
      using var stream = file.OpenReadStream();
      var (success, fail) = await _numberRuleService.ImportAsync(stream, sheetName);
      return Success(new { success, fail }, _localization.L("NumberRule.Import.Success"));
    }

    /// <summary>
    /// 导出单号规则数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:numberrule:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktNumberingRulesQueryDto query, [FromQuery] string sheetName = "NumberRule")
    {
      var result = await _numberRuleService.ExportAsync(query, sheetName);
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



