#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyOutputDetailController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 组立明细控制器
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy;
using Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立明细控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "组立明细")]
    [ApiController]
    [ApiModule("logistics", "后勤管理")]
    public class TaktAssyOutputDetailController : TaktBaseController
    {
        private readonly ITaktAssyOutputDetailService _assyMpOutputDetailService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assyMpOutputDetailService">组立明细服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktAssyOutputDetailController(
            ITaktAssyOutputDetailService assyMpOutputDetailService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _assyMpOutputDetailService = assyMpOutputDetailService;
        }

        /// <summary>
        /// 获取组立明细分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>组立明细分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktAssyOutputDetailQueryDto query)
        {
            var result = await _assyMpOutputDetailService.GetListAsync(query);
            return Success(result, _localization.L("AssyMpOutputDetail.List.Success"));
        }

        /// <summary>
        /// 获取组立明细详情
        /// </summary>
        /// <param name="assyMpOutputDetailId">组立明细ID</param>
        /// <returns>组立明细详情</returns>
        [HttpGet("{assyMpOutputDetailId}")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:query")]
        public async Task<IActionResult> GetByIdAsync(long assyMpOutputDetailId)
        {
            var result = await _assyMpOutputDetailService.GetByIdAsync(assyMpOutputDetailId);
            return Success(result, _localization.L("AssyMpOutputDetail.Get.Success"));
        }

        /// <summary>
        /// 根据组立日报ID获取明细列表
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>组立明细列表</returns>
        [HttpGet("by-assy-output/{assyMpOutputId}")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:query")]
        public async Task<IActionResult> GetByAssyMpOutputIdAsync(long assyMpOutputId)
        {
            var result = await _assyMpOutputDetailService.GetByAssyMpOutputIdAsync(assyMpOutputId);
            return Success(result, _localization.L("AssyMpOutputDetail.GetByAssyOutput.Success"));
        }

        /// <summary>
        /// 创建组立明细
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>组立明细ID</returns>
        [HttpPost]
        [TaktLog("创建组立明细")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktAssyOutputDetailCreateDto input)
        {
            var result = await _assyMpOutputDetailService.CreateAsync(input);
            return Success(result, _localization.L("AssyMpOutputDetail.Insert.Success"));
        }

        /// <summary>
        /// 批量创建组立明细
        /// </summary>
        /// <param name="inputs">创建对象列表</param>
        /// <returns>是否成功</returns>
        [HttpPost("batch")]
        [TaktLog("批量创建组立明细")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:create")]
        public async Task<IActionResult> CreateRangeAsync([FromBody] List<TaktAssyOutputDetailCreateDto> inputs)
        {
            var result = await _assyMpOutputDetailService.CreateRangeAsync(inputs);
            return Success(result, _localization.L("AssyMpOutputDetail.BatchInsert.Success"));
        }

        /// <summary>
        /// 更新组立明细
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新组立明细")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktAssyOutputDetailUpdateDto input)
        {
            var result = await _assyMpOutputDetailService.UpdateAsync(input);
            return Success(result, _localization.L("AssyMpOutputDetail.Update.Success"));
        }

        /// <summary>
        /// 删除组立明细
        /// </summary>
        /// <param name="assyMpOutputDetailId">组立明细ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{assyMpOutputDetailId}")]
        [TaktLog("删除组立明细")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:delete")]
        public async Task<IActionResult> DeleteAsync(long assyMpOutputDetailId)
        {
            try
            {
                var result = await _assyMpOutputDetailService.DeleteAsync(assyMpOutputDetailId);
                return Success(result, _localization.L("AssyMpOutputDetail.Delete.Success"));
            }
            catch (TaktException ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除组立明细
        /// </summary>
        /// <param name="assyMpOutputDetailIds">组立明细ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] assyMpOutputDetailIds)
        {
            var result = await _assyMpOutputDetailService.BatchDeleteAsync(assyMpOutputDetailIds);
            return Success(result, _localization.L("AssyMpOutputDetail.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入组立明细数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _assyMpOutputDetailService.ImportAsync(stream, "组立明细信息");
            return Success(new { success, fail }, _localization.L("AssyMpOutputDetail.Import.Success"));
        }

        /// <summary>
        /// 导出组立明细数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktAssyOutputDetailQueryDto query)
        {
            var result = await _assyMpOutputDetailService.ExportAsync(query, "AssyOutputDetail");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取组立明细导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:detail:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _assyMpOutputDetailService.GetTemplateAsync("组立明细信息");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}



