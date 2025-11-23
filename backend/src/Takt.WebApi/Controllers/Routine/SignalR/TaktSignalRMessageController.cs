//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSignalMessageController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 在线消息控制器
//===================================================================

using Takt.Application.Dtos.Routine.SignalR;
using Takt.Application.Services.Routine.SignalR;

namespace Takt.WebApi.Controllers.Routine.SignalR
{
  /// <summary>
  /// 在线消息控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-20
  /// </remarks>
  [Route("api/[controller]", Name = "在线消息")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  public class TaktSignalRMessageController : TaktBaseController
  {
    private readonly ITaktSignalRMessageService _onlineMessageService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineMessageService">在线消息服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktSignalRMessageController(
        ITaktSignalRMessageService onlineMessageService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _onlineMessageService = onlineMessageService;
    }

    /// <summary>
    /// 获取在线消息分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>在线消息分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:message:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktSignalMessageQueryDto query)
    {
      var result = await _onlineMessageService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取消息详情
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>消息详情</returns>
    [HttpGet("{id}")]
    [TaktPerm("routine:message:query")]
    public async Task<IActionResult> GetMessageAsync(long id)
    {
      var result = await _onlineMessageService.GetMessageAsync(id);
      return Success(result);
    }

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="input">消息信息</param>
    /// <returns>消息ID</returns>
    [HttpPost]
    [TaktPerm("routine:message:create")]
    public async Task<IActionResult> CreateMessageAsync([FromBody] TaktSignalMessageCreateDto input)
    {
      var result = await _onlineMessageService.CreateMessageAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新消息
    /// </summary>
    /// <param name="input">消息更新信息</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("routine:message:update")]
    public async Task<IActionResult> UpdateMessageAsync([FromBody] TaktSignalMessageUpdateDto input)
    {
      var result = await _onlineMessageService.UpdateMessageAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktPerm("routine:message:delete")]
    public async Task<IActionResult> DeleteMessageAsync(long id)
    {
      var result = await _onlineMessageService.DeleteMessageAsync(id);
      return Success(result);
    }

    /// <summary>
    /// 批量删除消息
    /// </summary>
    /// <param name="ids">消息ID列表</param>
    /// <returns>删除成功的数量</returns>
    [HttpDelete("batch")]
    [TaktPerm("routine:message:delete")]
    public async Task<IActionResult> BatchDeleteMessagesAsync([FromBody] List<long> ids)
    {
      var result = await _onlineMessageService.BatchDeleteMessagesAsync(ids);
      return Success(result);
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/read")]
    [TaktPerm("routine:message:read")]
    public async Task<IActionResult> MarkAsReadAsync(long id)
    {
      var result = await _onlineMessageService.MarkAsReadAsync(id);
      return Success(result);
    }

    /// <summary>
    /// 更新消息已读状态
    /// </summary>
    /// <param name="input">已读状态更新信息</param>
    /// <returns>是否成功</returns>
    [HttpPut("read-status")]
    [TaktPerm("routine:message:read")]
    public async Task<IActionResult> UpdateReadStatusAsync([FromBody] TaktSignalMessageReadStatusUpdateDto input)
    {
      var result = await _onlineMessageService.UpdateReadStatusAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 标记所有消息为已读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>标记的消息数量</returns>
    [HttpPut("read-all/{userId}")]
    [TaktPerm("routine:message:read")]
    public async Task<IActionResult> MarkAllAsReadAsync(long userId)
    {
      var result = await _onlineMessageService.MarkAllAsReadAsync(userId);
      return Success(result);
    }

    /// <summary>
    /// 标记消息为未读
    /// </summary>
    /// <param name="id">消息ID</param>
    /// <returns>是否成功</returns>
    [HttpPut("{id}/unread")]
    [TaktPerm("routine:message:read")]
    public async Task<IActionResult> MarkAsUnreadAsync(long id)
    {
      var result = await _onlineMessageService.MarkAsUnreadAsync(id);
      return Success(result);
    }

    /// <summary>
    /// 标记所有消息为未读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>标记的消息数量</returns>
    [HttpPut("unread-all/{userId}")]
    [TaktPerm("routine:message:read")]
    public async Task<IActionResult> MarkAllAsUnreadAsync(long userId)
    {
      var result = await _onlineMessageService.MarkAllAsUnreadAsync(userId);
      return Success(result);
    }

    /// <summary>
    /// 清理过期消息
    /// </summary>
    /// <param name="days">保留天数</param>
    /// <returns>清理数量</returns>
    [HttpPost("cleanup")]
    [TaktPerm("routine:message:cleanup")]
    public async Task<IActionResult> CleanupExpiredMessagesAsync([FromQuery] int days = 7)
    {
      var result = await _onlineMessageService.CleanupExpiredMessagesAsync(days);
      return Success(result);
    }

    /// <summary>
    /// 导出在线消息数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的Excel文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:message:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktSignalMessageQueryDto query, [FromQuery] string sheetName = "在线消息信息")
    {
      var result = await _onlineMessageService.ExportAsync(query, sheetName);
      return Success(result);
    }
  }
}



