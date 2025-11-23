//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktScheduleController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 日程控制器
//===================================================================

using Takt.Application.Dtos.Routine;
using Takt.Application.Services.Routine;

namespace Takt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 日程控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "日程管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktScheduleController : TaktBaseController
    {
        private readonly ITaktScheduleService _scheduleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scheduleService">日程服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktScheduleController(
            ITaktScheduleService scheduleService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// 获取日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>日程分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:schedule:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktScheduleQueryDto query)
        {
            var result = await _scheduleService.GetListAsync(query);
            return Success(result, _localization.L("Schedule.List.Success"));
        }

        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>日程详情</returns>
        [HttpGet("{scheduleId}")]
        [TaktPerm("routine:schedule:query")]
        public async Task<IActionResult> GetByIdAsync(long scheduleId)
        {
            var result = await _scheduleService.GetByIdAsync(scheduleId);
            return Success(result, _localization.L("Schedule.Get.Success"));
        }

        /// <summary>
        /// 获取当前用户的日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>日程分页列表</returns>
        [HttpGet("my")]
        [TaktPerm("routine:schedule:query")]
        public async Task<IActionResult> GetMySchedulesAsync([FromQuery] TaktScheduleQueryDto query)
        {
            var result = await _scheduleService.GetMySchedulesAsync(query);
            return Success(result, _localization.L("Schedule.MyList.Success"));
        }

        /// <summary>
        /// 创建日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>日程ID</returns>
        [HttpPost]
        [TaktLog("创建日程")]
        [TaktPerm("routine:schedule:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktScheduleCreateDto input)
        {
            var result = await _scheduleService.CreateAsync(input);
            return Success(result, _localization.L("Schedule.Insert.Success"));
        }

        /// <summary>
        /// 更新日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新日程")]
        [TaktPerm("routine:schedule:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktScheduleUpdateDto input)
        {
            var result = await _scheduleService.UpdateAsync(input);
            return Success(result, _localization.L("Schedule.Update.Success"));
        }

        /// <summary>
        /// 删除日程
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{scheduleId}")]
        [TaktLog("删除日程")]
        [TaktPerm("routine:schedule:delete")]
        public async Task<IActionResult> DeleteAsync(long scheduleId)
        {
            var result = await _scheduleService.DeleteAsync(scheduleId);
            return Success(result, _localization.L("Schedule.Delete.Success"));
        }

        /// <summary>
        /// 批量删除日程
        /// </summary>
        /// <param name="scheduleIds">日程ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:schedule:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] scheduleIds)
        {
            var result = await _scheduleService.BatchDeleteAsync(scheduleIds);
            return Success(result, _localization.L("Schedule.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入日程数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("routine:schedule:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "日程信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _scheduleService.ImportAsync(stream, sheetName);
            return Success(result, _localization.L("Schedule.Import.Success"));
        }

        /// <summary>
        /// 导出日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:schedule:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktScheduleQueryDto query, [FromQuery] string sheetName = "日程信息")
        {
            var result = await _scheduleService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:schedule:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "日程信息")
        {
            var result = await _scheduleService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 




