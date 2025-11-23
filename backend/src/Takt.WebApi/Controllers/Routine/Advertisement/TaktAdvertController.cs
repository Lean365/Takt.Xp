//===================================================================
// 项目名: Takt.WebApi
// 文件名: TaktAdvertController.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 广告控制器
//===================================================================

using Takt.Application.Dtos.Routine.Advertisement;
using Takt.Application.Services.Routine.Advertisement;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Advertisement
{
    /// <summary>
    /// 广告控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "广告管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktAdvertController : TaktBaseController
    {
        private readonly ITaktAdvertService _advertService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="advertService">广告服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktAdvertController(
            ITaktAdvertService advertService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _advertService = advertService;
        }

        /// <summary>
        /// 获取广告分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>广告分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:advert:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktAdvertQueryDto query)
        {
            var result = await _advertService.GetListAsync(query);
            return Success(result, _localization.L("Advert.List.Success"));
        }

        /// <summary>
        /// 获取广告详情
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>广告详情</returns>
        [HttpGet("{advertId}")]
        [TaktPerm("routine:advert:query")]
        public async Task<IActionResult> GetByIdAsync(long advertId)
        {
            var result = await _advertService.GetByIdAsync(advertId);
            return Success(result, _localization.L("Advert.Get.Success"));
        }

        /// <summary>
        /// 创建广告
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>广告ID</returns>
        [HttpPost]
        [TaktLog("创建广告")]
        [TaktPerm("routine:advert:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktAdvertCreateDto input)
        {
            var result = await _advertService.CreateAsync(input);
            return Success(result, _localization.L("Advert.Insert.Success"));
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新广告")]
        [TaktPerm("routine:advert:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktAdvertUpdateDto input)
        {
            var result = await _advertService.UpdateAsync(input);
            return Success(result, _localization.L("Advert.Update.Success"));
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{advertId}")]
        [TaktLog("删除广告")]
        [TaktPerm("routine:advert:delete")]
        public async Task<IActionResult> DeleteAsync(long advertId)
        {
            var result = await _advertService.DeleteAsync(advertId);
            return Success(result, _localization.L("Advert.Delete.Success"));
        }

        /// <summary>
        /// 批量删除广告
        /// </summary>
        /// <param name="ids">广告ID列表</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktLog("批量删除广告")]
        [TaktPerm("routine:advert:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _advertService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("Advert.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入广告数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktLog("导入广告数据")]
        [TaktPerm("routine:advert:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string? sheetName = null)
        {
            if (file == null || file.Length == 0)
                return Error(_localization.L("Common.FileRequired"));

            using var stream = file.OpenReadStream();
            var (success, fail) = await _advertService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("Advert.Import.Success"));
        }

        /// <summary>
        /// 导出广告数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:advert:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktAdvertQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
        {
            var result = await _advertService.ExportAsync(query, sheetName, fileName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取广告导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:advert:template")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
        {
            var result = await _advertService.GetTemplateAsync(sheetName, fileName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新广告状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新广告状态")]
        [TaktPerm("routine:advert:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktAdvertStatusDto input)
        {
            var result = await _advertService.UpdateStatusAsync(input);
            return Success(result, _localization.L("Advert.StatusUpdate.Success"));
        }

        /// <summary>
        /// 审核广告
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("audit")]
        [TaktLog("审核广告")]
        [TaktPerm("routine:advert:audit")]
        public async Task<IActionResult> AuditAsync([FromBody] TaktAdvertAuditDto input)
        {
            var result = await _advertService.AuditAsync(input);
            return Success(result, _localization.L("Advert.Logging.Success"));
        }

        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="input">发布对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("publish")]
        [TaktLog("发布广告")]
        [TaktPerm("routine:advert:publish")]
        public async Task<IActionResult> PublishAsync([FromBody] TaktAdvertPublishDto input)
        {
            var result = await _advertService.PublishAsync(input);
            return Success(result, _localization.L("Advert.Publish.Success"));
        }

        /// <summary>
        /// 下线广告
        /// </summary>
        /// <param name="input">下线对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("offline")]
        [TaktLog("下线广告")]
        [TaktPerm("routine:advert:offline")]
        public async Task<IActionResult> OfflineAsync([FromBody] TaktAdvertOfflineDto input)
        {
            var result = await _advertService.OfflineAsync(input);
            return Success(result, _localization.L("Advert.Offline.Success"));
        }

        /// <summary>
        /// 更新广告统计信息
        /// </summary>
        /// <param name="input">统计信息对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("stats")]
        [TaktPerm("routine:advert:update")]
        public async Task<IActionResult> UpdateStatsAsync([FromBody] TaktAdvertStatsDto input)
        {
            var result = await _advertService.UpdateStatsAsync(input);
            return Success(result, _localization.L("Advert.StatsUpdate.Success"));
        }

        /// <summary>
        /// 获取广告统计信息
        /// </summary>
        /// <returns>广告统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:advert:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _advertService.GetStatisticsAsync();
            return Success(result, _localization.L("Advert.Statistics.Success"));
        }

        /// <summary>
        /// 获取广告模板列表
        /// </summary>
        /// <returns>广告模板列表</returns>
        [HttpGet("templates")]
        [TaktPerm("routine:advert:query")]
        public async Task<IActionResult> GetTemplateListAsync()
        {
            var result = await _advertService.GetTemplateListAsync();
            return Success(result, _localization.L("Advert.Templates.Success"));
        }

        /// <summary>
        /// 根据模板创建广告
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <returns>广告ID</returns>
        [HttpPost("template/{templateId}")]
        [TaktLog("根据模板创建广告")]
        [TaktPerm("routine:advert:create")]
        public async Task<IActionResult> CreateFromTemplateAsync(long templateId)
        {
            var result = await _advertService.CreateFromTemplateAsync(templateId);
            return Success(result, _localization.L("Advert.CreateFromTemplate.Success"));
        }

        /// <summary>
        /// 复制广告
        /// </summary>
        /// <param name="advertId">源广告ID</param>
        /// <returns>新广告ID</returns>
        [HttpPost("copy/{advertId}")]
        [TaktLog("复制广告")]
        [TaktPerm("routine:advert:create")]
        public async Task<IActionResult> CopyAsync(long advertId)
        {
            var result = await _advertService.CopyAsync(advertId);
            return Success(result, _localization.L("Advert.Copy.Success"));
        }

        /// <summary>
        /// 获取前台展示的广告列表
        /// </summary>
        /// <param name="position">广告位置</param>
        /// <param name="limit">限制数量</param>
        /// <returns>广告列表</returns>
        [HttpGet("frontend")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFrontendListAsync([FromQuery] string position, [FromQuery] int limit = 10)
        {
            var result = await _advertService.GetFrontendListAsync(position, limit);
            return Success(result, _localization.L("Advert.FrontendList.Success"));
        }

        /// <summary>
        /// 记录广告点击
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("click/{advertId}")]
        [AllowAnonymous]
        public async Task<IActionResult> RecordClickAsync(long advertId)
        {
            var result = await _advertService.RecordClickAsync(advertId);
            return Success(result, _localization.L("Advert.ClickRecord.Success"));
        }

        /// <summary>
        /// 记录广告展示
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("show/{advertId}")]
        [AllowAnonymous]
        public async Task<IActionResult> RecordShowAsync(long advertId)
        {
            var result = await _advertService.RecordShowAsync(advertId);
            return Success(result, _localization.L("Advert.ShowRecord.Success"));
        }

        /// <summary>
        /// 记录广告关闭
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("close/{advertId}")]
        [AllowAnonymous]
        public async Task<IActionResult> RecordCloseAsync(long advertId)
        {
            var result = await _advertService.RecordCloseAsync(advertId);
            return Success(result, _localization.L("Advert.CloseRecord.Success"));
        }
    }
}



