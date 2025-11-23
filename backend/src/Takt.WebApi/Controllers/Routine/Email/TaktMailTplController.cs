//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMailTplController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件模板控制器
//===================================================================

namespace Takt.WebApi.Controllers.Routine.Email
{
    /// <summary>
    /// 邮件模板控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "邮件模板管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    [Authorize]
    public class TaktMailTplController : TaktBaseController
    {
        private readonly ITaktMailTplService _mailTplService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mailTplService">邮件模板服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktMailTplController(
            ITaktMailTplService mailTplService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _mailTplService = mailTplService;
        }

        /// <summary>
        /// 获取邮件模板分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件模板分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:mailtpl:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktMailTplQueryDto query)
        {
            var result = await _mailTplService.GetListAsync(query);
            return Success(result, _localization.L("MailTpl.List.Success"));
        }

        /// <summary>
        /// 获取邮件模板详情
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <returns>邮件模板详情</returns>
        [HttpGet("{tmplId}")]
        [TaktPerm("routine:mailtpl:query")]
        public async Task<IActionResult> GetByIdAsync(long tmplId)
        {
            var result = await _mailTplService.GetByIdAsync(tmplId);
            return Success(result, _localization.L("MailTpl.Get.Success"));
        }

        /// <summary>
        /// 创建邮件模板
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件模板ID</returns>
        [HttpPost]
        [TaktLog("创建邮件模板")]
        [TaktPerm("routine:mailtpl:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktMailTplCreateDto input)
        {
            var result = await _mailTplService.CreateAsync(input);
            return Success(result, _localization.L("MailTpl.Insert.Success"));
        }

        /// <summary>
        /// 更新邮件模板
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("{tmplId}")]
        [TaktLog("更新邮件模板")]
        [TaktPerm("routine:mailtpl:update")]
        public async Task<IActionResult> UpdateAsync(long tmplId, [FromBody] TaktMailTplDto input)
        {
            var result = await _mailTplService.UpdateAsync(tmplId, input);
            return Success(result, _localization.L("MailTpl.Update.Success"));
        }

        /// <summary>
        /// 删除邮件模板
        /// </summary>
        /// <param name="tmplId">模板ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{tmplId}")]
        [TaktLog("删除邮件模板")]
        [TaktPerm("routine:mailtpl:delete")]
        public async Task<IActionResult> DeleteAsync(long tmplId)
        {
            var result = await _mailTplService.DeleteAsync(tmplId);
            return Success(result, _localization.L("MailTpl.Delete.Success"));
        }

        /// <summary>
        /// 批量删除邮件模板
        /// </summary>
        /// <param name="tmplIds">模板ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktLog("批量删除邮件模板")]
        [TaktPerm("routine:mailtpl:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] tmplIds)
        {
            var result = await _mailTplService.BatchDeleteAsync(tmplIds);
            return Success(result, _localization.L("MailTpl.BatchDelete.Success"));
        }

        /// <summary>
        /// 导出邮件模板数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:mailtpl:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktMailTplQueryDto query, [FromQuery] string sheetName = "邮件模板数据")
        {
            var result = await _mailTplService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 导入邮件模板数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktLog("导入邮件模板数据")]
        [TaktPerm("routine:mailtpl:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "邮件模板数据")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");
            using var stream = file.OpenReadStream();
            // 假设服务层有ImportAsync方法，若无请补充
            var (success, fail) = await _mailTplService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("MailTpl.Import.Success"));
        }

        /// <summary>
        /// 获取邮件模板导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:mailtpl:import")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "邮件模板数据")
        {
            var result = await _mailTplService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}




