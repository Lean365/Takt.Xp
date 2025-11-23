//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRegulationController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01 10:00
// 版本号 : V0.0.1
// 描述   : 规章制度控制器
//===================================================================

using Takt.Application.Dtos.Routine.Document;
using Takt.Application.Services.Routine.Document.Regulations;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Document
{
    /// <summary>
    /// 规章制度控制器
    /// </summary>
    [Route("api/[controller]", Name = "规章制度")]
    [ApiController]
    [ApiModule("routine", "规章制度管理")]
    public class TaktRegulationController : TaktBaseController
    {
        private readonly ITaktRegulationService _regulationService;

        public TaktRegulationController(
            ITaktRegulationService regulationService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _regulationService = regulationService;
        }

        [HttpGet("list")]
        [TaktPerm("routine:document:regulation:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktRegulationQueryDto query)
        {
            var result = await _regulationService.GetListAsync(query);
            return Success(result, _localization.L("Regulation.List.Success"));
        }

        [HttpGet("{id}")]
        [TaktPerm("routine:document:regulation:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _regulationService.GetByIdAsync(id);
            return Success(result, _localization.L("Regulation.Get.Success"));
        }

        [HttpPost]
        [TaktLog("创建规章制度")]
        [TaktPerm("routine:document:regulation:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktRegulationCreateDto input)
        {
            var result = await _regulationService.CreateAsync(input);
            return Success(result, _localization.L("Regulation.Insert.Success"));
        }

        [HttpPut]
        [TaktLog("更新规章制度")]
        [TaktPerm("routine:document:regulation:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktRegulationUpdateDto input)
        {
            var result = await _regulationService.UpdateAsync(input);
            return Success(result, _localization.L("Regulation.Update.Success"));
        }

        [HttpDelete("{id}")]
        [TaktLog("删除规章制度")]
        [TaktPerm("routine:document:regulation:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _regulationService.DeleteAsync(id);
            return Success(result, _localization.L("Regulation.Delete.Success"));
        }

        [HttpDelete("batch")]
        [TaktPerm("routine:document:regulation:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _regulationService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("Regulation.BatchDelete.Success"));
        }

        [HttpGet("tree")]
        [TaktPerm("routine:document:regulation:query")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] long? parentId = null)
        {
            var result = await _regulationService.GetTreeAsync(parentId);
            return Success(result, _localization.L("Regulation.Tree.Success"));
        }

        [HttpPost("import")]
        [TaktPerm("routine:document:regulation:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _regulationService.ImportAsync(stream, "TaktRegulation");
            return Success(new { success, fail }, _localization.L("Regulation.Import.Success"));
        }

        [HttpGet("export")]
        [TaktPerm("routine:document:regulation:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktRegulationQueryDto query)
        {
            var result = await _regulationService.ExportAsync(query, "TaktRegulation");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        [HttpGet("template")]
        [TaktPerm("routine:document:regulation:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _regulationService.GetTemplateAsync("TaktRegulation");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}



