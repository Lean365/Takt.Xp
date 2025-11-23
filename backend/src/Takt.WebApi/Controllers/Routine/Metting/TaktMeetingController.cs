//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMeetingController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 会议控制器
//===================================================================

using Takt.Application.Services.Routine.Metting;

namespace Takt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 会议控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "会议管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktMeetingController : TaktBaseController
    {
        private readonly ITaktMeetingService _meetingService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="meetingService">会议服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktMeetingController(
            ITaktMeetingService meetingService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _meetingService = meetingService;
        }

        /// <summary>
        /// 获取会议分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>会议分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:meeting:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktMeetingQueryDto query)
        {
            var result = await _meetingService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取会议详情
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <returns>会议详情</returns>
        [HttpGet("{meetingId}")]
        public async Task<IActionResult> GetByIdAsync(long meetingId)
        {
            var result = await _meetingService.GetByIdAsync(meetingId);
            return Success(result);
        }

        /// <summary>
        /// 创建会议
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>会议ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TaktMeetingCreateDto input)
        {
            var result = await _meetingService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新会议
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktMeetingUpdateDto input)
        {
            var result = await _meetingService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="meetingId">会议ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{meetingId}")]
        public async Task<IActionResult> DeleteAsync(long meetingId)
        {
            var result = await _meetingService.DeleteAsync(meetingId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除会议
        /// </summary>
        /// <param name="meetingIds">会议ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] meetingIds)
        {
            var result = await _meetingService.BatchDeleteAsync(meetingIds);
            return Success(result);
        }

        /// <summary>
        /// 导入会议数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "会议信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _meetingService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出会议数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktMeetingQueryDto query, [FromQuery] string sheetName = "会议信息")
        {
            var result = await _meetingService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "会议信息")
        {
            var result = await _meetingService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}




