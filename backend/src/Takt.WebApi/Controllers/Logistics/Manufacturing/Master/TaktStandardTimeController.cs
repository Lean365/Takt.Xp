#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktStandardTimeController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 标准工时控制器
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Application.Services.Logistics.Manufacturing.Master;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "标准工时")]
    [ApiController]
    [ApiModule("logistics", "后勤管理")]
    public class TaktStandardTimeController : TaktBaseController
    {
        private readonly ITaktStandardTimeService _standardTimeService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="standardTimeService">标准工时服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktStandardTimeController(
            ITaktStandardTimeService standardTimeService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _standardTimeService = standardTimeService;
        }

        /// <summary>
        /// 获取标准工时分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>标准工时分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("logistics:manufacturing:master:standardtime:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktStandardTimeQueryDto query)
        {
            var result = await _standardTimeService.GetListAsync(query);
            return Success(result, _localization.L("StandardTime.List.Success"));
        }

        /// <summary>
        /// 获取标准工时详情
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>标准工时详情</returns>
        [HttpGet("{standardTimeId}")]
        [TaktPerm("logistics:manufacturing:master:standardtime:query")]
        public async Task<IActionResult> GetByIdAsync(long standardTimeId)
        {
            var result = await _standardTimeService.GetByIdAsync(standardTimeId);
            return Success(result, _localization.L("StandardTime.Get.Success"));
        }

        /// <summary>
        /// 创建标准工时
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>标准工时ID</returns>
        [HttpPost]
        [TaktLog("创建标准工时")]
        [TaktPerm("logistics:manufacturing:master:standardtime:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktStandardTimeCreateDto input)
        {
            var result = await _standardTimeService.CreateAsync(input);
            return Success(result, _localization.L("StandardTime.Insert.Success"));
        }

        /// <summary>
        /// 更新标准工时
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新标准工时")]
        [TaktPerm("logistics:manufacturing:master:standardtime:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktStandardTimeUpdateDto input)
        {
            var result = await _standardTimeService.UpdateAsync(input);
            return Success(result, _localization.L("StandardTime.Update.Success"));
        }

        /// <summary>
        /// 删除标准工时
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{standardTimeId}")]
        [TaktLog("删除标准工时")]
        [TaktPerm("logistics:manufacturing:master:standardtime:delete")]
        public async Task<IActionResult> DeleteAsync(long standardTimeId)
        {
            try
            {
                var result = await _standardTimeService.DeleteAsync(standardTimeId);
                return Success(result, _localization.L("StandardTime.Delete.Success"));
            }
            catch (TaktException ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除标准工时
        /// </summary>
        /// <param name="standardTimeIds">标准工时ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("logistics:manufacturing:master:standardtime:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] standardTimeIds)
        {
            var result = await _standardTimeService.BatchDeleteAsync(standardTimeIds);
            return Success(result, _localization.L("StandardTime.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入标准工时数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("logistics:manufacturing:master:standardtime:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _standardTimeService.ImportAsync(stream, "标准工时信息");
            return Success(new { success, fail }, _localization.L("StandardTime.Import.Success"));
        }

        /// <summary>
        /// 导出标准工时数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("logistics:manufacturing:master:standardtime:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktStandardTimeQueryDto query)
        {
            var result = await _standardTimeService.ExportAsync(query, "StandardTime");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取标准工时导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("logistics:manufacturing:master:standardtime:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _standardTimeService.GetTemplateAsync("标准工时信息");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新标准工时状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新标准工时状态")]
        [TaktPerm("logistics:manufacturing:master:standardtime:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktStandardTimeStatusDto input)
        {
            var result = await _standardTimeService.UpdateStatusAsync(input);
            return Success(result, _localization.L("StandardTime.StatusUpdate.Success"));
        }

        /// <summary>
        /// 获取标准工时选项列表
        /// </summary>
        /// <param name="materialCode">物料编码（可选）</param>
        /// <returns>标准工时选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("logistics:manufacturing:master:standardtime:list")]
        public async Task<IActionResult> GetOptionsAsync([FromQuery] string? materialCode = null)
        {
            var result = await _standardTimeService.GetOptionsAsync(materialCode ?? string.Empty);
            return Success(result, _localization.L("StandardTime.Options.Success"));
        }



        /// <summary>
        /// 审核标准工时
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <param name="approver">审核人</param>
        /// <returns>是否成功</returns>
        [HttpPut("{standardTimeId}/approve")]
        [TaktLog("审核标准工时")]
        [TaktPerm("logistics:manufacturing:master:standardtime:approve")]
        public async Task<IActionResult> ApproveAsync(long standardTimeId, [FromBody] string approver)
        {
            var result = await _standardTimeService.ApproveAsync(standardTimeId, approver);
            return Success(result, _localization.L("StandardTime.Approve.Success"));
        }

        /// <summary>
        /// 计算转换后标准工时
        /// </summary>
        /// <param name="standardMinutes">标准工时(分钟)</param>
        /// <param name="standardShorts">标准点数</param>
        /// <param name="pointsToMinutesRate">点数转分钟汇率</param>
        /// <returns>转换后标准工时(分钟)</returns>
        [HttpGet("calculate")]
        [TaktPerm("logistics:manufacturing:master:standardtime:query")]
        public IActionResult CalculateConvertedMinutesAsync(decimal standardMinutes, int standardShorts, decimal pointsToMinutesRate)
        {
            var result = _standardTimeService.CalculateConvertedMinutes(standardMinutes, standardShorts, pointsToMinutesRate);
            return Success(result, _localization.L("StandardTime.Calculate.Success"));
        }

        /// <summary>
        /// 获取标准工时变更记录
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>变更记录列表</returns>
        [HttpGet("{standardTimeId}/changelogs")]
        [TaktPerm("logistics:manufacturing:master:standardtime:query")]
        public async Task<IActionResult> GetChangeLogsAsync(long standardTimeId)
        {
            var result = await _standardTimeService.GetChangeLogsAsync(standardTimeId);
            return Success(result, _localization.L("StandardTime.ChangeLogs.Success"));
        }
    }
}




