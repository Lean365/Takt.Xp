//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTranslationController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译控制器
//===================================================================

using Takt.Application.Dtos.Routine.I18n;
using Takt.Application.Services.Routine.I18n;

namespace Takt.WebApi.Controllers.Routine.I18n
{
  /// <summary>
  /// 翻译控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-22
  /// </remarks>
  [Route("api/[controller]", Name = "翻译")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktTranslationController : TaktBaseController
  {
    private readonly ITaktTranslationService _translationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationService">翻译服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>


    public TaktTranslationController(
        ITaktTranslationService translationService,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization,
        ITaktLogger logger) : base(logger, currentUser, localization)
    {
      _translationService = translationService;
    }

    /// <summary>
    /// 获取翻译分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>翻译分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:i18n:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktTranslationQueryDto query)
    {
      var result = await _translationService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取翻译详情
    /// </summary>
    /// <param name="TranslationId">翻译ID</param>
    /// <returns>翻译详情</returns>
    [HttpGet("{TranslationId}")]
    [TaktPerm("routine:i18n:query")]
    public async Task<IActionResult> GetByIdAsync(long TranslationId)
    {
      var result = await _translationService.GetByIdAsync(TranslationId);
      return Success(result);
    }

    /// <summary>
    /// 创建翻译
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>翻译ID</returns>
    [HttpPost]
    [TaktPerm("routine:i18n:create")]
    [TaktLog("创建翻译")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktTranslationCreateDto input)
    {
      var result = await _translationService.CreateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新翻译
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("routine:i18n:update")]
    [TaktLog("更新翻译")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktTranslationUpdateDto input)
    {
      var result = await _translationService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除翻译
    /// </summary>
    /// <param name="TranslationId">翻译ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{TranslationId}")]
    [TaktPerm("routine:i18n:delete")]
    [TaktLog("删除翻译")]
    public async Task<IActionResult> DeleteAsync(long TranslationId)
    {
      var result = await _translationService.DeleteAsync(TranslationId);
      return Success(result);
    }

    /// <summary>
    /// 批量删除翻译
    /// </summary>
    /// <param name="TranslationIds">翻译ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("routine:i18n:delete")]
    [TaktLog("批量删除翻译")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] TranslationIds)
    {
      var result = await _translationService.BatchDeleteAsync(TranslationIds);
      return Success(result);
    }

    /// <summary>
    /// 获取翻译导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("routine:i18n:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Translation")
    {
      var result = await _translationService.GetTemplateAsync(sheetName);
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入翻译数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入翻译数据")]
    [TaktPerm("routine:i18n:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Translation")
    {
      using var stream = file.OpenReadStream();
      var (success, fail) = await _translationService.ImportAsync(stream, sheetName);
      return Success(new { success, fail }, _localization.L("Translation.Import.Success"));
    }

    /// <summary>
    /// 导出翻译数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:i18n:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktTranslationQueryDto query, [FromQuery] string sheetName = "Translation")
    {
      var result = await _translationService.ExportAsync(query, sheetName);
      var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
          ? "application/zip"
          : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
      var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
      Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
      return File(result.content, contentType, result.fileName);
    }



    /// <summary>
    /// 获取指定语言的翻译值
    /// </summary>
    /// <param name="langCode">语言代码</param>
    /// <param name="transKey">翻译键</param>
    /// <returns>翻译值</returns>
    [HttpGet("value")]
    public async Task<IActionResult> GetTransValueAsync([FromQuery] string langCode, [FromQuery] string transKey)
    {
      var result = await _translationService.GetTransValueAsync(langCode, transKey);
      return Success(result);
    }

    /// <summary>
    /// 获取指定类型的翻译列表
    /// </summary>
    /// <param name="langCode">语言代码</param>
    /// <param name="transType">翻译类型，0表示获取所有翻译</param>
    /// <returns>翻译列表</returns>
    [HttpGet("module")]
    public async Task<IActionResult> GetListByModuleAsync([FromQuery] string langCode, [FromQuery] int transType = 0)
    {
      var result = await _translationService.GetListByModuleAsync(langCode, transType);
      return Success(result);
    }

    /// <summary>
    /// 获取转置后的翻译数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>转置后的翻译数据</returns>
    /// <remarks>
    /// 返回数据结构:
    /// {
    ///   "rows": [
    ///     {
    ///       "transKey": "key1",
    ///       "zh-CN": "中文翻译",
    ///       "en-US": "English Translation"
    ///     },
    ///     {
    ///       "transKey": "key2",
    ///       "zh-CN": "中文翻译2",
    ///       "en-US": "English Translation2"
    ///     }
    ///   ],
    ///   "totalNum": 100,
    ///   "pageIndex": 1,
    ///   "pageSize": 10
    /// }
    /// </remarks>
    [HttpGet("transposed")]
    [TaktPerm("routine:i18n:list")]

    public async Task<IActionResult> GetTransposedDataAsync([FromQuery] TaktTransposedQueryDto query)
    {
      var result = await _translationService.GetTransposedDataAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取转置后的翻译详情
    /// </summary>
    /// <param name="transKey">翻译键</param>
    /// <returns>转置后的翻译详情</returns>
    /// <remarks>
    /// 返回数据结构:
    /// {
    ///   "transKey": "key1",
    ///   "TransType": "模块1",
    ///   "orderNum": 1,
    ///   "status": 0,
    ///   "remark": "备注",
    ///   "createBy": "创建人",
    ///   "createTime": "2024-03-20 10:00:00",
    ///   "updateBy": "更新人",
    ///   "updateTime": "2024-03-20 11:00:00",
    ///   "translations": {
    ///     "zh-CN": {
    ///       "translationId": 1,
    ///       "langCode": "zh-CN",
    ///       "transValue": "中文翻译",
    ///       "status": 0
    ///     },
    ///     "en-US": {
    ///       "translationId": 2,
    ///       "langCode": "en-US",
    ///       "transValue": "English Translation",
    ///       "status": 0
    ///     }
    ///   }
    /// }
    /// </remarks>
    [HttpGet("transposed/{transKey}")]
    [TaktPerm("routine:i18n:query")]
    public async Task<IActionResult> GetTransposedDetailAsync(string transKey)
    {
      var result = await _translationService.GetTransposedDetailAsync(transKey);
      return Success(result);
    }

    /// <summary>
    /// 创建转置翻译数据
    /// </summary>
    /// <param name="input">转置数据创建对象</param>
    /// <returns>是否成功</returns>
    [HttpPost("transposed")]
    [TaktPerm("routine:i18n:create")]
    [TaktLog("创建转置翻译数据")]
    public async Task<IActionResult> CreateTransposedAsync([FromBody] TaktTransposedDto input)
    {
      var result = await _translationService.CreateTransposedAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新转置翻译数据
    /// </summary>
    /// <param name="input">转置数据更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut("transposed")]
    [TaktPerm("routine:i18n:update")]
    [TaktLog("更新转置翻译数据")]
    public async Task<IActionResult> UpdateTransposedAsync([FromBody] TaktTransposedDto input)
    {
      var result = await _translationService.UpdateTransposedAsync(input);
      return Success(result);
    }
  }
}



