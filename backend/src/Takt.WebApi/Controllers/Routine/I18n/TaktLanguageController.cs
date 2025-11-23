//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLanguageController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 多语言管理控制器
//===================================================================

using Takt.Application.Dtos.Routine.I18n;
using Takt.Application.Services.Routine.I18n;

namespace Takt.WebApi.Controllers.Routine.I18n
{
  /// <summary>
  /// 多语言管理控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-22
  /// 功能说明:
  /// 1. 提供多语言配置的增删改查功能
  /// 2. 支持多语言数据的导入导出
  /// 3. 管理系统支持的语言列表
  /// 4. 设置默认语言和语言状态
  /// </remarks>
  [Route("api/[controller]", Name = "语言")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktLanguageController : TaktBaseController
  {
    private readonly ITaktLanguageService _languageService;

    /// <summary>
    /// 构造函数，注入所需服务
    /// </summary>
    /// <param name="languageService">语言服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    public TaktLanguageController(
        ITaktLanguageService languageService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _languageService = languageService;
    }

    /// <summary>
    /// 获取语言分页列表
    /// </summary>
    /// <remarks>
    /// 支持按语言名称、代码、状态等条件筛选
    /// 返回分页后的语言配置列表
    /// </remarks>
    /// <param name="query">查询条件</param>
    /// <returns>语言分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:i18n:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktLanguageQueryDto query)
    {
      var result = await _languageService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取语言详情
    /// </summary>
    /// <remarks>
    /// 根据语言ID获取单个语言配置的详细信息
    /// </remarks>
    /// <param name="LanguageId">语言ID</param>
    /// <returns>语言详情</returns>
    [HttpGet("{LanguageId}")]
    [TaktPerm("routine:i18n:query")]
    public async Task<IActionResult> GetByIdAsync(long LanguageId)
    {
      var result = await _languageService.GetByIdAsync(LanguageId);
      return Success(result);
    }

    /// <summary>
    /// 获取语言选项列表
    /// </summary>
    /// <returns>语言选项列表</returns>
    [HttpGet("options")]

    public async Task<IActionResult> GetOptionsAsync()
    {
      var result = await _languageService.GetOptionsAsync();
      return Success(result);
    }

    /// <summary>
    /// 创建语言配置
    /// </summary>
    /// <remarks>
    /// 新增一个语言配置，包括语言代码、名称、排序等信息
    /// </remarks>
    /// <param name="input">创建对象</param>
    /// <returns>新创建的语言ID</returns>
    [HttpPost]
    [TaktPerm("routine:i18n:create")]
    [TaktLog("创建语言")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktLanguageCreateDto input)
    {
      var result = await _languageService.CreateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新语言配置
    /// </summary>
    /// <remarks>
    /// 修改现有语言配置的信息，如名称、排序、状态等
    /// </remarks>
    /// <param name="input">更新对象</param>
    /// <returns>更新结果</returns>
    [HttpPut]
    [TaktPerm("routine:i18n:update")]
    [TaktLog("更新语言")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktLanguageUpdateDto input)
    {
      var result = await _languageService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除语言配置
    /// </summary>
    /// <remarks>
    /// 删除指定ID的语言配置
    /// 注意：删除前会检查该语言是否被使用
    /// </remarks>
    /// <param name="LanguageId">语言ID</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{LanguageId}")]
    [TaktPerm("routine:i18n:delete")]
    [TaktLog("删除语言")]
    public async Task<IActionResult> DeleteAsync(long LanguageId)
    {
      var result = await _languageService.DeleteAsync(LanguageId);
      return Success(result);
    }

    /// <summary>
    /// 批量删除语言配置
    /// </summary>
    /// <remarks>
    /// 批量删除多个语言配置
    /// 注意：会检查所有语言是否可以删除
    /// </remarks>
    /// <param name="LanguageIds">语言ID集合</param>
    /// <returns>批量删除结果</returns>
    [HttpDelete("batch")]
    [TaktPerm("routine:i18n:delete")]
    [TaktLog("批量删除语言")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] LanguageIds)
    {
      var result = await _languageService.BatchDeleteAsync(LanguageIds);
      return Success(result);
    }

    /// <summary>
    /// 更新语言状态
    /// </summary>
    /// <remarks>
    /// 修改语言的启用/禁用状态
    /// 状态：0-启用，1-禁用
    /// </remarks>
    /// <param name="LanguageId">语言ID</param>
    /// <param name="status">状态值</param>
    /// <returns>更新结果</returns>
    [HttpPut("{LanguageId}/status")]
    [TaktLog("更新语言状态")]
    [TaktPerm("routine:i18n:update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatusAsync(long LanguageId, [FromQuery] int status)
    {
      var input = new TaktLanguageStatusDto
      {
        LanguageId = LanguageId,
        I18nStatus = status
      };
      var result = await _languageService.UpdateStatusAsync(input);
      return Success(result);
    }


    /// <summary>
    /// 获取语言导入模板
    /// </summary>
    /// <remarks>
    /// 下载语言数据导入的Excel模板
    /// 模板包含标准的字段格式和示例数据
    /// </remarks>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("routine:i18n:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Language")
    {
      var result = await _languageService.GetTemplateAsync(sheetName);
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入语言数据
    /// </summary>
    /// <remarks>
    /// 从Excel文件导入语言配置数据
    /// 支持批量导入多条语言记录
    /// </remarks>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果，包含成功和失败数量</returns>
    [HttpPost("import")]
    [TaktLog("导入语言数据")]
    [TaktPerm("routine:i18n:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Language")
    {
      using var stream = file.OpenReadStream();
      var (success, fail) = await _languageService.ImportAsync(stream, sheetName);
      return Success(new { success, fail }, _localization.L("Language.Import.Success"));
    }

    /// <summary>
    /// 导出语言数据
    /// </summary>
    /// <remarks>
    /// 将语言配置数据导出为Excel文件
    /// 支持按条件筛选要导出的数据
    /// </remarks>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:i18n:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktLanguageQueryDto query, [FromQuery] string sheetName = "Language")
    {
      var result = await _languageService.ExportAsync(query, sheetName);
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



