#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyOutputController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 组立日报控制器
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy;
using Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立日报控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "组立日报")]
    [ApiController]
    [ApiModule("logistics", "后勤管理")]
    public class TaktAssyOutputController : TaktBaseController
    {
        private readonly ITaktAssyOutputService _assyMpOutputService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assyMpOutputService">组立日报服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktAssyOutputController(
            ITaktAssyOutputService assyMpOutputService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _assyMpOutputService = assyMpOutputService;
        }

        /// <summary>
        /// 获取组立日报分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>组立日报分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktAssyOutputQueryDto query)
        {
            var result = await _assyMpOutputService.GetListAsync(query);
            return Success(result, _localization.L("AssyMpOutput.List.Success"));
        }

        /// <summary>
        /// 获取组立日报详情
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>组立日报详情</returns>
        [HttpGet("{assyMpOutputId}")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:query")]
        public async Task<IActionResult> GetByIdAsync(long assyMpOutputId)
        {
            var result = await _assyMpOutputService.GetByIdAsync(assyMpOutputId);
            return Success(result, _localization.L("AssyMpOutput.Get.Success"));
        }

        /// <summary>
        /// 创建组立日报
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>组立日报ID</returns>
        [HttpPost]
        [TaktLog("创建组立日报")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktAssyOutputCreateDto input)
        {
            var result = await _assyMpOutputService.CreateAsync(input);
            return Success(result, _localization.L("AssyMpOutput.Insert.Success"));
        }

        /// <summary>
        /// 更新组立日报
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新组立日报")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktAssyOutputUpdateDto input)
        {
            var result = await _assyMpOutputService.UpdateAsync(input);
            return Success(result, _localization.L("AssyMpOutput.Update.Success"));
        }

        /// <summary>
        /// 删除组立日报
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{assyMpOutputId}")]
        [TaktLog("删除组立日报")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:delete")]
        public async Task<IActionResult> DeleteAsync(long assyMpOutputId)
        {
            try
            {
                var result = await _assyMpOutputService.DeleteAsync(assyMpOutputId);
                return Success(result, _localization.L("AssyMpOutput.Delete.Success"));
            }
            catch (TaktException ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除组立日报
        /// </summary>
        /// <param name="assyMpOutputIds">组立日报ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] assyMpOutputIds)
        {
            var result = await _assyMpOutputService.BatchDeleteAsync(assyMpOutputIds);
            return Success(result, _localization.L("AssyMpOutput.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入组立日报数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _assyMpOutputService.ImportAsync(stream, "组立日报信息");
            return Success(new { success, fail }, _localization.L("AssyMpOutput.Import.Success"));
        }

        /// <summary>
        /// 导出组立日报数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktAssyOutputQueryDto query)
        {
            var result = await _assyMpOutputService.ExportAsync(query, "AssyOutput");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取组立日报导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("logistics:manufacturing:execution:output:workshop1:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _assyMpOutputService.GetTemplateAsync("组立日报信息");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}



