//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMailController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件控制器
//===================================================================

using Takt.Application.Dtos.Routine;
using Takt.Application.Services.Routine;

namespace Takt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 邮件控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "邮件管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    [Authorize]
    public class TaktMailController : TaktBaseController
    {
        private readonly ITaktMailService _mailService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mailService">邮件服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktMailController(
            ITaktMailService mailService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _mailService = mailService;

        }

        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:mail:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktMailQueryDto query)
        {
            var result = await _mailService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="id">邮件ID</param>
        /// <returns>邮件详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:mail:query")]
        public async Task<TaktMailDto> GetByIdAsync(long id)
        {
            return await _mailService.GetByIdAsync(id);
        }

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件ID</returns>
        [HttpPost]
        [TaktPerm("routine:mail:create")]
        public async Task<long> CreateAsync([FromBody] TaktMailCreateDto input)
        {
            return await _mailService.CreateAsync(input);
        }

        /// <summary>
        /// 更新邮件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktPerm("routine:mail:update")]
        public async Task<bool> UpdateAsync([FromBody] TaktMailUpdateDto input)
        {
            return await _mailService.UpdateAsync(input);
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="id">邮件ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktPerm("routine:mail:delete")]
        public async Task<bool> DeleteAsync(long id)
        {
            return await _mailService.DeleteAsync(id);
        }

        /// <summary>
        /// 批量删除邮件
        /// </summary>
        /// <param name="ids">邮件ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:mail:delete")]
        public async Task<bool> BatchDeleteAsync([FromBody] long[] ids)
        {
            return await _mailService.BatchDeleteAsync(ids);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input">发送邮件参数</param>
        /// <returns>是否成功</returns>
        [HttpPost("send")]
        [TaktPerm("routine:mail:send")]
        public async Task<bool> SendAsync([FromBody] TaktMailSendDto input)
        {
            return await _mailService.SendAsync(input);
        }

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="inputs">发送邮件参数集合</param>
        /// <returns>发送结果</returns>
        [HttpPost("batch-send")]
        [TaktPerm("routine:mail:send")]
        public async Task<(int success, int fail)> BatchSendAsync([FromBody] List<TaktMailSendDto> inputs)
        {
            return await _mailService.BatchSendAsync(inputs);
        }

        /// <summary>
        /// 标记邮件已读
        /// </summary>
        /// <param name="id">邮件ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/read")]
        [TaktPerm("routine:mail:read")]
        public async Task<bool> MarkAsReadAsync(long id)
        {
            return await _mailService.MarkAsReadAsync(id);
        }

        /// <summary>
        /// 标记所有邮件已读
        /// </summary>
        /// <returns>标记的邮件数量</returns>
        [HttpPost("read-all")]
        [TaktPerm("routine:mail:read")]
        public async Task<int> MarkAllAsReadAsync()
        {
            return await _mailService.MarkAllAsReadAsync(_currentUser.UserId);
        }

        /// <summary>
        /// 标记邮件未读
        /// </summary>
        /// <param name="id">邮件ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("{id}/unread")]
        [TaktPerm("routine:mail:read")]
        public async Task<bool> MarkAsUnreadAsync(long id)
        {
            return await _mailService.MarkAsUnreadAsync(id);
        }

        /// <summary>
        /// 标记所有邮件未读
        /// </summary>
        /// <returns>标记的邮件数量</returns>
        [HttpPost("unread-all")]
        [TaktPerm("routine:mail:read")]
        public async Task<int> MarkAllAsUnreadAsync()
        {
            return await _mailService.MarkAllAsUnreadAsync(_currentUser.UserId);
        }

        /// <summary>
        /// 导出邮件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:mail:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<FileResult> ExportAsync([FromQuery] TaktMailQueryDto query)
        {
            var data = await _mailService.ExportAsync(query);
            return File(data.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", data.fileName);
        }
    }
}




