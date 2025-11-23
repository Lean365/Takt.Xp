//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSignalROnlineController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 在线用户控制器
//===================================================================

global using Takt.Domain.IServices.Extensions;
using Takt.Application.Dtos.Routine.SignalR;
using Takt.Application.Services.Routine.SignalR;

namespace Takt.WebApi.Controllers.Routine.SignalR
{
  /// <summary>
  /// 在线用户控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-20
  /// </remarks>
  [Route("api/[controller]", Name = "在线用户")]
  [ApiController]
  [ApiModule("routine", "日常事务")]
  [Authorize]
  public class TaktSignalROnlineController : TaktBaseController
  {
    private readonly ITaktSignalROnlineService _onlineUserService;


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineUserService">在线用户服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="logger">日志服务</param>
    public TaktSignalROnlineController(
        ITaktSignalROnlineService onlineUserService,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization,
        ITaktLogger logger) : base(logger, currentUser, localization)
    {
      _onlineUserService = onlineUserService;

    }

    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>在线用户分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("routine:online:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktSignalROnlineQueryDto query)
    {
      // 确保查询对象不为空
      query ??= new TaktSignalROnlineQueryDto();

      // 设置默认分页参数
      if (query.PageIndex <= 0) query.PageIndex = 1;
      if (query.PageSize <= 0) query.PageSize = 10;

      var result = await _onlineUserService.GetListAsync(query);
      return Success(result);
    }

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>连接ID列表</returns>
    [HttpGet("connection-ids/{userId}")]
    [TaktPerm("routine:online:list")]
    public async Task<IActionResult> GetConnectionIdsAsync(long userId)
    {
      var result = await _onlineUserService.GetConnectionIdsAsync(userId);
      return Success(result);
    }

    /// <summary>
    /// 根据条件获取在线用户信息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>在线用户信息</returns>
    [HttpGet("first/{userId}")]
    [TaktPerm("routine:online:list")]
    public async Task<IActionResult> GetFirstAsync(long userId)
    {
      var result = await _onlineUserService.GetFirstAsync(u => u.UserId == userId);
      return Success(result);
    }

    /// <summary>
    /// 获取所有在线用户
    /// </summary>
    /// <returns>在线用户列表</returns>
    [HttpGet("all")]
    [TaktPerm("routine:online:list")]
    public async Task<IActionResult> GetAllAsync()
    {
      var result = await _onlineUserService.GetAllAsync();
      return Success(result);
    }

    /// <summary>
    /// 创建在线用户
    /// </summary>
    /// <param name="input">在线用户信息</param>
    /// <returns>在线用户ID</returns>
    [HttpPost]
    [TaktPerm("routine:online:create")]
    public async Task<IActionResult> CreateOnlineUserAsync([FromBody] TaktSignalROnlineCreateDto input)
    {
      var result = await _onlineUserService.CreateOnlineUserAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新在线用户信息
    /// </summary>
    /// <param name="input">在线用户更新信息</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [TaktPerm("routine:online:update")]
    public async Task<IActionResult> UpdateAsync([FromBody] TaktSignalROnlineUpdateDto input)
    {
      var result = await _onlineUserService.UpdateAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 更新在线用户状态
    /// </summary>
    /// <param name="input">状态更新信息</param>
    /// <returns>是否成功</returns>
    [HttpPut("status")]
    [TaktPerm("routine:online:update")]
    public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktSignalROnlineStatusUpdateDto input)
    {
      var result = await _onlineUserService.UpdateStatusAsync(input);
      return Success(result);
    }

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktPerm("routine:online:delete")]
    public async Task<IActionResult> DeleteOnlineUserAsync(long id)
    {
      var result = await _onlineUserService.DeleteOnlineUserAsync(id);
      return Success(result);
    }

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    /// <param name="ids">在线用户ID列表</param>
    /// <returns>删除成功的数量</returns>
    [HttpDelete("batch-delete")]
    [TaktPerm("routine:online:delete")]
    public async Task<IActionResult> BatchDeleteOnlineUsersAsync([FromBody] List<long> ids)
    {
      var result = await _onlineUserService.BatchDeleteOnlineUsersAsync(ids);
      return Success(result);
    }

    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>是否成功</returns>
    [HttpPost("force-offline")]
    [TaktPerm("routine:online:delete")]
    public async Task<IActionResult> ForceOfflineAsync([FromBody] string deviceId)
    {
      var result = await _onlineUserService.ForceOfflineAsync(deviceId);
      return Success(result);
    }

    /// <summary>
    /// 更新所有在线用户的心跳时间
    /// </summary>
    [HttpPost("heartbeat")]
    public async Task<IActionResult> UpdateHeartbeatAsync()
    {
      var count = await _onlineUserService.UpdateHeartbeatAsync();
      return Success(count);
    }

    /// <summary>
    /// 清理过期用户
    /// </summary>
    /// <param name="minutes">超时时间(分钟)</param>
    /// <returns>清理数量</returns>
    [HttpPost("cleanup")]
    [TaktPerm("routine:online:cleanup")]
    public async Task<IActionResult> CleanupExpiredUsersAsync([FromQuery] int minutes = 20)
    {
      var result = await _onlineUserService.CleanupExpiredUsersAsync(minutes);
      return Success(result);
    }

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导出的Excel文件</returns>
    [HttpGet("export")]
    [TaktPerm("routine:online:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktSignalROnlineQueryDto query, [FromQuery] string sheetName = "在线用户信息")
    {
      var result = await _onlineUserService.ExportAsync(query, sheetName);
      return Success(result);
    }
  }
}



