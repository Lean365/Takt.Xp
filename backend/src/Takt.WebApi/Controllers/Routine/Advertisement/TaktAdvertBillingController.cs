//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAdvertBillingController.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 广告计费控制器
//===================================================================

using Takt.Application.Dtos.Routine.Advertisement;
using Takt.Application.Services.Routine.Advertisement;

namespace Takt.WebApi.Controllers.Routine.Advertisement
{
    /// <summary>
    /// 广告计费控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-19
    /// </remarks>
    [Route("api/[controller]", Name = "广告计费")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktAdvertBillingController : TaktBaseController
    {
        private readonly ITaktAdvertBillingService _advertBillingService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="advertBillingService">广告计费服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktAdvertBillingController(
            ITaktAdvertBillingService advertBillingService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _advertBillingService = advertBillingService;
        }

        /// <summary>
        /// 获取广告计费分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>广告计费分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:advertbilling:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktAdvertBillingQueryDto query)
        {
            var result = await _advertBillingService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取广告计费详情
        /// </summary>
        /// <param name="billingId">广告计费ID</param>
        /// <returns>广告计费详情</returns>
        [HttpGet("{billingId}")]
        [TaktPerm("routine:advertbilling:detail")]
        public async Task<IActionResult> GetByIdAsync(long billingId)
        {
            var result = await _advertBillingService.GetByIdAsync(billingId);
            return Success(result);
        }

        /// <summary>
        /// 创建广告计费
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>广告计费ID</returns>
        [HttpPost]
        [TaktLog("创建广告计费")]
        [TaktPerm("routine:advertbilling:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktAdvertBillingCreateDto input)
        {
            var result = await _advertBillingService.CreateAsync(input);
            return Success(result, _localization.L("AdvertBilling.Create.Success"));
        }

        /// <summary>
        /// 更新广告计费
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新广告计费")]
        [TaktPerm("routine:advertbilling:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktAdvertBillingUpdateDto input)
        {
            var result = await _advertBillingService.UpdateAsync(input);
            return Success(result, _localization.L("AdvertBilling.Update.Success"));
        }

        /// <summary>
        /// 删除广告计费
        /// </summary>
        /// <param name="billingId">广告计费ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{billingId}")]
        [TaktLog("删除广告计费")]
        [TaktPerm("routine:advertbilling:delete")]
        public async Task<IActionResult> DeleteAsync(long billingId)
        {
            var result = await _advertBillingService.DeleteAsync(billingId);
            return Success(result, _localization.L("AdvertBilling.Delete.Success"));
        }

        /// <summary>
        /// 批量删除广告计费
        /// </summary>
        /// <param name="ids">广告计费ID列表</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktLog("批量删除广告计费")]
        [TaktPerm("routine:advertbilling:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _advertBillingService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("AdvertBilling.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取广告计费导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:advertbilling:export")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
        {
            var result = await _advertBillingService.GetTemplateAsync(sheetName, fileName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 导入广告计费数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktLog("导入广告计费")]
        [TaktPerm("routine:advertbilling:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string? sheetName = null)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _advertBillingService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("AdvertBilling.Import.Success"));
        }

        /// <summary>
        /// 导出广告计费数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [TaktLog("导出广告计费")]
        [TaktPerm("routine:advertbilling:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktAdvertBillingQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
        {
            var result = await _advertBillingService.ExportAsync(query, sheetName, fileName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 更新广告计费状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新广告计费状态")]
        [TaktPerm("routine:advertbilling:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktAdvertBillingStatusDto input)
        {
            var result = await _advertBillingService.UpdateStatusAsync(input);
            return Success(result, _localization.L("AdvertBilling.UpdateStatus.Success"));
        }

        /// <summary>
        /// 审核广告计费
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("audit")]
        [TaktLog("审核广告计费")]
        [TaktPerm("routine:advertbilling:audit")]
        public async Task<IActionResult> AuditAsync([FromBody] TaktAdvertBillingAuditDto input)
        {
            var result = await _advertBillingService.AuditAsync(input);
            return Success(result, _localization.L("AdvertBilling.Logging.Success"));
        }

        /// <summary>
        /// 启动广告计费
        /// </summary>
        /// <param name="input">启动对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("start")]
        [TaktLog("启动广告计费")]
        [TaktPerm("routine:advertbilling:update")]
        public async Task<IActionResult> StartAsync([FromBody] TaktAdvertBillingStartDto input)
        {
            var result = await _advertBillingService.StartAsync(input);
            return Success(result, _localization.L("AdvertBilling.Start.Success"));
        }

        /// <summary>
        /// 结束广告计费
        /// </summary>
        /// <param name="input">结束对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("end")]
        [TaktLog("结束广告计费")]
        [TaktPerm("routine:advertbilling:update")]
        public async Task<IActionResult> EndAsync([FromBody] TaktAdvertBillingEndDto input)
        {
            var result = await _advertBillingService.EndAsync(input);
            return Success(result, _localization.L("AdvertBilling.End.Success"));
        }

        /// <summary>
        /// 更新广告计费统计信息
        /// </summary>
        /// <param name="input">统计信息对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("stats")]
        [TaktLog("更新广告计费统计")]
        [TaktPerm("routine:advertbilling:update")]
        public async Task<IActionResult> UpdateStatsAsync([FromBody] TaktAdvertBillingStatsDto input)
        {
            var result = await _advertBillingService.UpdateStatsAsync(input);
            return Success(result, _localization.L("AdvertBilling.UpdateStats.Success"));
        }

        /// <summary>
        /// 获取广告计费统计信息
        /// </summary>
        /// <returns>广告计费统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:advertbilling:list")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _advertBillingService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




