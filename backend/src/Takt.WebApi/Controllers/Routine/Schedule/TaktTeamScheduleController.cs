using Microsoft.AspNetCore.Mvc;
using Takt.Application.Dtos.Routine.Schedule;
using Takt.Application.Services.Routine.Schedule;
using Takt.Shared.Models;
using Takt.Domain.IServices.Extensions;
using Takt.Shared.Enums;

namespace Takt.WebApi.Controllers.Routine.Schedule
{
    /// <summary>
    /// 团队协作日程控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "团队协作日程管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktTeamScheduleController : TaktBaseController
    {
        private readonly ITaktTeamScheduleService _teamScheduleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="teamScheduleService">团队协作日程服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktTeamScheduleController(
            ITaktTeamScheduleService teamScheduleService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _teamScheduleService = teamScheduleService;
        }

        /// <summary>
        /// 获取团队协作日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>团队协作日程分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:teamschedule:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktTeamScheduleQueryDto query)
        {
            var result = await _teamScheduleService.GetListAsync(query);
            return Success(result, _localization.L("TeamSchedule.List.Success"));
        }

        /// <summary>
        /// 获取团队协作日程详情
        /// </summary>
        /// <param name="teamScheduleId">团队协作日程ID</param>
        /// <returns>团队协作日程详情</returns>
        [HttpGet("{teamScheduleId}")]
        [TaktPerm("routine:teamschedule:query")]
        public async Task<IActionResult> GetByIdAsync(long teamScheduleId)
        {
            var result = await _teamScheduleService.GetByIdAsync(teamScheduleId);
            return Success(result, _localization.L("TeamSchedule.Get.Success"));
        }

        /// <summary>
        /// 获取当前用户的团队协作日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>团队协作日程分页列表</returns>
        [HttpGet("my")]
        [TaktPerm("routine:teamschedule:query")]
        public async Task<IActionResult> GetMyTeamSchedulesAsync([FromQuery] TaktTeamScheduleQueryDto query)
        {
            var result = await _teamScheduleService.GetMyTeamSchedulesAsync(query);
            return Success(result, _localization.L("TeamSchedule.MyList.Success"));
        }

        /// <summary>
        /// 创建团队协作日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>团队协作日程ID</returns>
        [HttpPost]
        [TaktLog("创建团队协作日程")]
        [TaktPerm("routine:teamschedule:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktTeamScheduleCreateDto input)
        {
            var result = await _teamScheduleService.CreateAsync(input);
            return Success(result, _localization.L("TeamSchedule.Insert.Success"));
        }

        /// <summary>
        /// 更新团队协作日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新团队协作日程")]
        [TaktPerm("routine:teamschedule:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktTeamScheduleUpdateDto input)
        {
            var result = await _teamScheduleService.UpdateAsync(input);
            return Success(result, _localization.L("TeamSchedule.Update.Success"));
        }

        /// <summary>
        /// 删除团队协作日程
        /// </summary>
        /// <param name="teamScheduleId">团队协作日程ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{teamScheduleId}")]
        [TaktLog("删除团队协作日程")]
        [TaktPerm("routine:teamschedule:delete")]
        public async Task<IActionResult> DeleteAsync(long teamScheduleId)
        {
            var result = await _teamScheduleService.DeleteAsync(teamScheduleId);
            return Success(result, _localization.L("TeamSchedule.Delete.Success"));
        }

        /// <summary>
        /// 批量删除团队协作日程
        /// </summary>
        /// <param name="teamScheduleIds">团队协作日程ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:teamschedule:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] teamScheduleIds)
        {
            var result = await _teamScheduleService.BatchDeleteAsync(teamScheduleIds);
            return Success(result, _localization.L("TeamSchedule.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入团队协作日程数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("routine:teamschedule:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "团队协作日程信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _teamScheduleService.ImportAsync(stream, sheetName);
            return Success(result, _localization.L("TeamSchedule.Import.Success"));
        }

        /// <summary>
        /// 导出团队协作日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:teamschedule:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktTeamScheduleQueryDto query, [FromQuery] string sheetName = "团队协作日程信息")
        {
            var result = await _teamScheduleService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:teamschedule:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "团队协作日程信息")
        {
            var result = await _teamScheduleService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 



