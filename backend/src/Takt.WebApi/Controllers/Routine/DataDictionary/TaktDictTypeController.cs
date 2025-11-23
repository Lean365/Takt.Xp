//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDictTypeController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型控制器
//===================================================================

using Takt.Application.Dtos.Routine.DataDictionary;
using Takt.Application.Services.Routine.DataDictionary;

namespace Takt.WebApi.Controllers.Routine.DataDictionary
{
  /// <summary>
  /// 字典类型控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-17
  /// </remarks>
  [Route("api/[controller]", Name = "字典类型")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktDictTypeController : TaktBaseController
  {
    private readonly ITaktDictTypeService _dictTypeService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeService">字典类型服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktDictTypeController(
        ITaktDictTypeService dictTypeService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _dictTypeService = dictTypeService;
    }

    /// <summary>
    /// 获取字典类型分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>字典类型分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:dict:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktDictTypeQueryDto query)
    {
      var result = await _dictTypeService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取字典类型详情
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns>字典类型详情</returns>
    [HttpGet("{dictTypeId}")]
    [TaktPerm("routine:dict:query")]
    public async Task<IActionResult> GetByIdAsync(long dictTypeId)
    {
      var result = await _dictTypeService.GetByIdAsync(dictTypeId);
      return Success(result);
    }

    /// <summary>
    /// 根据字典类型获取详情
    /// </summary>
    /// <param name="type">字典类型</param>
    /// <returns>字典类型详情</returns>
    [HttpGet("type/{type}")]
    [TaktPerm("routine:dict:query")]
    public async Task<IActionResult> GetByTypeAsync(string type)
    {
      if (string.IsNullOrEmpty(type))
      {
        return Error("字典类型不能为空");
      }
      var result = await _dictTypeService.GetByTypeAsync(type);
      return Success(result);
    }

    /// <summary>
    /// 创建字典类型
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>字典类型ID</returns>
    [HttpPost]
    [TaktPerm("routine:dict:create")]
    public async Task<IActionResult> CreateAsync([FromBody] TaktDictTypeCreateDto input)
    {
      var result = await _dictTypeService.CreateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新字典类型
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("routine:dict:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktDictTypeUpdateDto input)
    {
      var result = await _dictTypeService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{dictTypeId}")]
    [TaktPerm("routine:dict:delete")]
    public async Task<IActionResult> DeleteAsync(long dictTypeId)
    {
      var result = await _dictTypeService.DeleteAsync(dictTypeId);
      return Success(result);
    }

    /// <summary>
    /// 批量删除字典类型
    /// </summary>
    /// <param name="dictTypeIds">字典类型ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktPerm("routine:dict:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] dictTypeIds)
    {
      var result = await _dictTypeService.BatchDeleteAsync(dictTypeIds);
      return Success(result);
    }

    /// <summary>
    /// 更新字典类型状态
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    [HttpPut("{dictTypeId}/status")]
    [TaktLog("更新字典类型状态")]
    [TaktPerm("routine:dict:update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatusAsync(long dictTypeId, [FromQuery] int status)
    {
      var input = new TaktDictTypeStatusDto
      {
        DictTypeId = dictTypeId,
        DictStatus = status
      };
      var result = await _dictTypeService.UpdateStatusAsync(input);
      return Success(result);
    }


    /// <summary>
    /// 获取字典类型导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    [HttpGet("template")]
    [TaktPerm("routine:dict:export")]
    public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "DictType")
    {
      var result = await _dictTypeService.GetTemplateAsync(sheetName);
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }

    /// <summary>
    /// 导入字典类型
    /// </summary>
    /// <param name="file">字典类型数据文件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    [TaktLog("导入字典类型")]
    [TaktPerm("routine:dict:import")]
    public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "DictType")
    {
      using var stream = file.OpenReadStream();
      var (success, fail) = await _dictTypeService.ImportAsync(stream, sheetName);
      return Success(new { success, fail }, _localization.L("DictType.Import.Success"));
    }

    /// <summary>
    /// 导出字典类型
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件或zip文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:dict:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktDictTypeQueryDto query, [FromQuery] string sheetName = "DictType")
    {
      var result = await _dictTypeService.ExportAsync(query, sheetName);
      var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
          ? "application/zip"
          : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
      var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
      Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
      return File(result.content, contentType, result.fileName);
    }



    /// <summary>
    /// 执行字典SQL脚本
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns>字典数据列表</returns>
    [HttpGet("executeSql/{dictTypeId}")]
    [TaktPerm("routine:dict:list")]
    public async Task<IActionResult> ExecuteDictSqlAsync(long dictTypeId)
    {
      var dictType = await _dictTypeService.GetByIdAsync(dictTypeId);
      if (dictType == null)
      {
        throw new TaktException("字典类型不存在");
      }

      if (string.IsNullOrEmpty(dictType.SqlScript))
      {
        throw new TaktException("SQL脚本为空");
      }

      var result = await _dictTypeService.ExecuteDictSqlAsync(dictType.SqlScript);

      // 填充字典类型信息
      foreach (var item in result)
      {
        item.DictType = dictType.DictType;
      }

      return Success(result);
    }
  }
}



