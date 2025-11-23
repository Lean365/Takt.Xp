//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDeviceLogController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:45
// 版本号 : V0.0.1
// 描述   : 设备日志控制器
//===================================================================

using Takt.Application.Dtos.Logging;
using Takt.Application.Services.Logging;

namespace Takt.WebApi.Controllers.Logging
{
  /// <summary>
  /// 设备日志控制器
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-22
  /// </remarks>
  [Route("api/[controller]", Name = "设备日志")]
  [ApiController]
  [ApiModule("logging", "审计日志")]
  public class TaktDeviceLogController : TaktBaseController
  {
    private readonly ITaktDeviceLogService _deviceLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deviceLogService">设备日志服务</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktDeviceLogController(
        ITaktDeviceLogService deviceLogService,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _deviceLogService = deviceLogService;
    }

    /// <summary>
    /// 获取设备日志分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>设备日志分页列表</returns>
    [HttpGet("list")]
    [TaktPerm("logging:devicelog:list")]
    public async Task<IActionResult> GetListAsync([FromQuery] TaktDeviceLogQueryDto query)
    {
      var result = await _deviceLogService.GetListAsync(query);
      return Success(result, _localization.L("DeviceLog.List.Success"));
    }

    /// <summary>
    /// 获取设备日志详情
    /// </summary>
    /// <param name="id">设备日志ID</param>
    /// <returns>设备日志详情</returns>
    [HttpGet("{id}")]
    [TaktPerm("logging:devicelog:query")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
      var result = await _deviceLogService.GetByIdAsync(id);
      return Success(result, _localization.L("DeviceLog.Get.Success"));
    }

    /// <summary>
    /// 删除设备日志
    /// </summary>
    /// <param name="id">设备日志ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [TaktLog("删除设备日志")]
    [TaktPerm("logging:devicelog:delete")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
      try
      {
        var result = await _deviceLogService.DeleteAsync(id);
        return Success(result, _localization.L("DeviceLog.Delete.Success"));
      }
      catch (TaktException ex)
      {
        return Error(ex.Message);
      }
    }

    /// <summary>
    /// 批量删除设备日志
    /// </summary>
    /// <param name="ids">设备日志ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [TaktLog("批量删除设备日志")]
    [TaktPerm("logging:devicelog:delete")]
    public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
    {
      var result = await _deviceLogService.BatchDeleteAsync(ids);
      return Success(result, _localization.L("DeviceLog.BatchDelete.Success"));
    }

    /// <summary>
    /// 记录设备登录日志
    /// </summary>
    /// <param name="input">设备日志信息</param>
    /// <returns>是否成功</returns>
    [HttpPost("record-login")]
    [TaktLog("记录设备登录")]
    [TaktPerm("logging:devicelog:create")]
    public async Task<IActionResult> RecordDeviceLoginAsync([FromBody] TaktDeviceLogDto input)
    {
      var result = await _deviceLogService.RecordDeviceLoginAsync(input);
      return Success(result, _localization.L("DeviceLog.RecordLogin.Success"));
    }

    /// <summary>
    /// 更新设备在线状态
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <param name="isOnline">是否在线</param>
    /// <returns>是否成功</returns>
    [HttpPut("{deviceId}/online-status")]
    [TaktLog("更新设备在线状态")]
    [TaktPerm("logging:devicelog:update")]
    public async Task<IActionResult> UpdateOnlineStatusAsync(string deviceId, [FromQuery] bool isOnline)
    {
      var result = await _deviceLogService.UpdateOnlineStatusAsync(deviceId, isOnline);
      return Success(result, _localization.L("DeviceLog.UpdateOnlineStatus.Success"));
    }

    /// <summary>
    /// 获取用户在线设备数量
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>在线设备数量</returns>
    [HttpGet("online-count/{userId}")]
    [TaktPerm("logging:devicelog:query")]
    public async Task<IActionResult> GetOnlineDeviceCountAsync(long userId)
    {
      var result = await _deviceLogService.GetOnlineDeviceCountAsync(userId);
      return Success(result, _localization.L("DeviceLog.GetOnlineCount.Success"));
    }

    /// <summary>
    /// 更新今日在线时段
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <param name="onlinePeriods">在线时段列表</param>
    /// <returns>是否成功</returns>
    [HttpPut("{deviceId}/online-periods")]
    [TaktLog("更新今日在线时段")]
    [TaktPerm("logging:devicelog:update")]
    public async Task<IActionResult> UpdateTodayOnlinePeriodsAsync(string deviceId, [FromBody] List<string> onlinePeriods)
    {
      var result = await _deviceLogService.UpdateTodayOnlinePeriodsAsync(deviceId, onlinePeriods);
      return Success(result, _localization.L("DeviceLog.UpdateOnlinePeriods.Success"));
    }

    /// <summary>
    /// 更新连续登录天数
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <param name="continuousDays">连续登录天数</param>
    /// <returns>是否成功</returns>
    [HttpPut("{deviceId}/continuous-days")]
    [TaktLog("更新连续登录天数")]
    [TaktPerm("logging:devicelog:update")]
    public async Task<IActionResult> UpdateContinuousLoginDaysAsync(string deviceId, [FromQuery] int continuousDays)
    {
      var result = await _deviceLogService.UpdateContinuousLoginDaysAsync(deviceId, continuousDays);
      return Success(result, _localization.L("DeviceLog.UpdateContinuousDays.Success"));
    }

    /// <summary>
    /// 更新登录次数
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <param name="loginCount">登录次数</param>
    /// <returns>是否成功</returns>
    [HttpPut("{deviceId}/login-count")]
    [TaktLog("更新登录次数")]
    [TaktPerm("logging:devicelog:update")]
    public async Task<IActionResult> UpdateLoginCountAsync(string deviceId, [FromQuery] int loginCount)
    {
      var result = await _deviceLogService.UpdateLoginCountAsync(deviceId, loginCount);
      return Success(result, _localization.L("DeviceLog.UpdateLoginCount.Success"));
    }

    /// <summary>
    /// 清空设备日志数据
    /// </summary>
    /// <param name="query">清空条件</param>
    /// <returns>是否成功</returns>
    [HttpDelete("cleanup")]
    [TaktLog("清空设备日志")]
    [TaktPerm("logging:devicelog:cleanup")]
    public async Task<IActionResult> CleanupAsync([FromQuery] TaktDeviceLogQueryDto query)
    {
      var result = await _deviceLogService.ClearUpAsync(query);
      return Success(result, _localization.L("DeviceLog.Clear.Success"));
    }

    /// <summary>
    /// 导出设备日志数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>Excel文件</returns>
    [HttpGet("export")]
    [TaktPerm("logging:devicelog:export")]
    public async Task<IActionResult> ExportAsync([FromQuery] TaktDeviceLogQueryDto query)
    {
      var result = await _deviceLogService.ExportAsync(query, "DeviceLog");
      Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
      return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
    }
  }
}






