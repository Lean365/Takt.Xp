//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDictDataController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典数据控制器
//===================================================================

using Takt.Application.Dtos.Routine.DataDictionary;
using Takt.Application.Services.Routine.DataDictionary;

namespace Takt.WebApi.Controllers.Routine.DataDictionary
{
  /// <summary>
  /// 字典数据控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-17
  /// </remarks>
  [Route("api/[controller]", Name = "字典数据")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktDictDataController : TaktBaseController
  {
    private readonly ITaktDictDataService _dictDataService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataService">字典数据服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    public TaktDictDataController(ITaktDictDataService dictDataService,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization,
        ITaktLogger logger) : base(logger, currentUser, localization)
    {
      _dictDataService = dictDataService;
    }

    /// <summary>
    /// 获取字典数据分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>字典数据分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:dict:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktDictDataQueryDto query)
    {
      var result = await _dictDataService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取字典数据详情
    /// </summary>
    /// <param name="dictDataId">字典数据ID</param>
    /// <returns>字典数据详情</returns>
    [HttpGet("{dictDataId}")]
    [TaktPerm("routine:dict:query")]
    public async Task<IActionResult> GetByIdAsync(long dictDataId)
    {
      var result = await _dictDataService.GetByIdAsync(dictDataId);
      return Success(result);
    }

    /// <summary>
    /// 创建字典数据
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>字典数据ID</returns>
    [HttpPost]
    [TaktLog("创建字典数据")]
    [TaktPerm("routine:dict:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktDictDataCreateDto input)
    {
      var result = await _dictDataService.CreateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktLog("更新字典数据")]
    [TaktPerm("routine:dict:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktDictDataUpdateDto input)
    {
      var result = await _dictDataService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="dictDataId">字典数据ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{dictDataId}")]
    [TaktLog("删除字典数据")]
    [TaktPerm("routine:dict:delete")]
    public async Task<IActionResult> DeleteAsync(long dictDataId)
    {
      var result = await _dictDataService.DeleteAsync(dictDataId);
      return Success(result);
    }

    /// <summary>
    /// 批量删除字典数据
    /// </summary>
    /// <param name="dictDataIds">字典数据ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktLog("批量删除字典数据")]
    [TaktPerm("routine:dict:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] dictDataIds)
    {
      var result = await _dictDataService.BatchDeleteAsync(dictDataIds);
      return Success(result);
    }



    /// <summary>
    /// 获取字典数据导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("routine:dict:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "DictData")
    {
      var result = await _dictDataService.GetTemplateAsync(sheetName);
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入字典数据
    /// </summary>
    /// <param name="file">Excel文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入字典数据")]
    [TaktPerm("routine:dict:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "DictData")
    {
      using var stream = file.OpenReadStream();
      var (success, fail) = await _dictDataService.ImportAsync(stream, sheetName);
      return Success(new { success, fail }, _localization.L("Dict.Import.Success"));
    }

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:dict:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktDictDataQueryDto query, [FromQuery] string sheetName = "DictData")
    {
      var result = await _dictDataService.ExportAsync(query, sheetName);
      var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
          ? "application/zip"
          : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
      var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
      Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
      return File(result.content, contentType, result.fileName);
    }


    /// <summary>
    /// 根据字典类型查询字典数据列表
    /// </summary>
    /// <param name="dictType">字典类型</param>
    /// <returns>字典数据列表</returns>
    [HttpGet("type/{dictType}")]
    [TaktPerm("routine:dict:query")]
    public async Task<IActionResult> GetDictDataByTypeAsync([FromRoute] string dictType)
    {
      if (string.IsNullOrEmpty(dictType))
      {
        return BadRequest("字典类型不能为空");
      }

      var result = await _dictDataService.GetListByDictTypeAsync(dictType);
      return Success(result);
    }

  }
}



