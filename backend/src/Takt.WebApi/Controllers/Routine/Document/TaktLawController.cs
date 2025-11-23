//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLawController.cs
// 创建者 : Takt(Claude AI)
// 创建时间:2024-12-01 10:00// 版本号 : V1.0
// 描述   : 法律法规控制器
//===================================================================

using Takt.Application.Dtos.Routine.Document;
using Takt.Application.Services.Routine.Document;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Document
{
    /// <summary>
    /// 法律法规控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "法律法规")]
    [ApiController]
    [ApiModule("routine", "法律法规管理")]
    public class TaktLawController : TaktBaseController
    {
        private readonly ITaktLawService _lawService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lawService">法律法规服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktLawController(
            ITaktLawService lawService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _lawService = lawService;
        }

        /// <summary>
        /// 获取法律法规分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>法律法规分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:document:law:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktLawQueryDto query)
        {
            var result = await _lawService.GetListAsync(query);
            return Success(result, _localization.L("Law.List.Success"));
        }

        /// <summary>
        /// 获取法律法规详情
        /// </summary>
        /// <param name="id">法律法规ID</param>
        /// <returns>法律法规详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:document:law:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _lawService.GetByIdAsync(id);
            return Success(result, _localization.L("Law.Get.Success"));
        }

        /// <summary>
        /// 创建法律法规
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>法律法规ID</returns>
        [HttpPost]
        [TaktLog("创建法律法规")]
        [TaktPerm("routine:document:law:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktLawCreateDto input)
        {
            var result = await _lawService.CreateAsync(input);
            return Success(result, _localization.L("Law.Insert.Success"));
        }

        /// <summary>
        /// 更新法律法规
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新法律法规")]
        [TaktPerm("routine:document:law:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktLawUpdateDto input)
        {
            var result = await _lawService.UpdateAsync(input);
            return Success(result, _localization.L("Law.Update.Success"));
        }

        /// <summary>
        /// 删除法律法规
        /// </summary>
        /// <param name="id">法律法规ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除法律法规")]
        [TaktPerm("routine:document:law:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _lawService.DeleteAsync(id);
            return Success(result, _localization.L("Law.Delete.Success"));
        }

        /// <summary>
        /// 批量删除法律法规
        /// </summary>
        /// <param name="ids">法律法规ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:document:law:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _lawService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("Law.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取法律法规树形结构
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns>树形结构</returns>
        [HttpGet("tree")]
        [TaktPerm("routine:document:law:query")]
        public async Task<IActionResult> GetTreeAsync([FromQuery] long? parentId = null)
        {
            var result = await _lawService.GetTreeAsync(parentId);
            return Success(result, _localization.L("Law.Tree.Success"));
        }

        /// <summary>
        /// 导入法律法规数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("routine:document:law:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _lawService.ImportAsync(stream, "TaktLaw");
            return Success(new { success, fail }, _localization.L("Law.Import.Success"));
        }

        /// <summary>
        /// 导出法律法规数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:document:law:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktLawQueryDto query)
        {
            var result = await _lawService.ExportAsync(query, "TaktLaw");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:document:law:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _lawService.GetTemplateAsync("TaktLaw");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 



