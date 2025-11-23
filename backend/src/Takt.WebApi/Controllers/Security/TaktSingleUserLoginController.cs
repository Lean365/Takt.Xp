//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSingleUserLoginController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 17:30
// 版本号 : V0.0.1
// 描述   : 单用户登录管理控制器
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Takt.WebApi.Controllers.Security
{
  /// <summary>
  /// 单用户登录管理控制器
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  [ApiModule("identity", "身份认证")]
  public class TaktSingleUserLoginController : TaktBaseController
  {
    private readonly ITaktSingleUserLoginService _singleUserLoginService;
    private readonly TaktSessionOptions _sessionOptions;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktSingleUserLoginController(
        ITaktSingleUserLoginService singleUserLoginService,
        IOptions<TaktSessionOptions> sessionOptions,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
      _singleUserLoginService = singleUserLoginService;
      _sessionOptions = sessionOptions.Value;
    }

    /// <summary>
    /// 获取单用户登录配置
    /// </summary>
    /// <returns>配置信息</returns>
    [HttpGet("config")]
    [TaktPerm("security:singleuserlogin:query")]
    public IActionResult GetConfig()
    {
      var config = new
      {
        SingleUserLogin = new
        {
          Enabled = _sessionOptions.SingleUserLoginEnabled,
          EnableDeviceFingerprint = _sessionOptions.EnableDeviceFingerprint,
          EnableIpValidation = _sessionOptions.EnableIpValidation,
          EnableLocationValidation = _sessionOptions.EnableLocationValidation,
          HeartbeatIntervalSeconds = _sessionOptions.HeartbeatIntervalSeconds,
          OfflineDetectionDelaySeconds = _sessionOptions.OfflineDetectionDelaySeconds
        },
        Session = new
        {
          // 显示实际生效的配置值
          ActualMaxConcurrentSessions = _sessionOptions.ActualMaxConcurrentSessions,
          ActualAllowMultipleDevices = _sessionOptions.ActualAllowMultipleDevices,
          // 显示配置值
          TimeoutMinutes = _sessionOptions.TimeoutMinutes,
          EnableSlidingExpiration = _sessionOptions.EnableSlidingExpiration,
          EnableAbsoluteExpiration = _sessionOptions.EnableAbsoluteExpiration,
          AbsoluteExpirationHours = _sessionOptions.AbsoluteExpirationHours,
          SessionExpiryMinutes = _sessionOptions.SessionExpiryMinutes
        },
        // 配置说明
        Description = new
        {
          SingleUserLoginMode = "单用户登录模式：用户只能在一个设备上登录，其他设备将被踢出",
          MultiDeviceMode = "多设备登录模式：用户可以在多个设备上同时登录，受MaxConcurrentSessions限制",
          ConfigConflict = "注意：启用单用户登录后，AllowMultipleDevices和MaxConcurrentSessions配置将被忽略"
        }
      };

      return Success(config, "获取配置成功");
    }


    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="reason">下线原因</param>
    /// <returns>操作结果</returns>
    [HttpPost("force-offline/{userId}")]
    [TaktLog("强制用户下线")]
    [TaktPerm("security:singleuserlogin:manage")]
    public async Task<IActionResult> ForceUserOffline(long userId, [FromQuery] string reason = "管理员强制下线")
    {
      try
      {
        var result = await _singleUserLoginService.ForceUserOfflineAsync(userId, reason);
        if (result)
        {
          return Success(true, "强制用户下线成功");
        }
        else
        {
          return Error("强制用户下线失败");
        }
      }
      catch (Exception ex)
      {
        _logger.Error("强制用户下线失败 - 用户ID: {UserId}", userId, ex);
        return Error("强制用户下线失败");
      }
    }

    /// <summary>
    /// 检查用户登录状态
    /// </summary>
    /// <param name="request">检查登录请求</param>
    /// <returns>检查结果</returns>
    [HttpPost("check-login")]
    [TaktPerm("security:singleuserlogin:query")]
    public async Task<IActionResult> CheckUserLogin([FromBody] CheckLoginRequest request)
    {
      try
      {
        var (canLogin, reason) = await _singleUserLoginService.CheckUserLoginAsync(
            request.UserId,
            request.DeviceId,
            request.IpAddress);

        return Success(new { canLogin, reason }, "检查登录状态成功");
      }
      catch (Exception ex)
      {
        _logger.Error("检查用户登录状态失败 - 用户ID: {UserId}", request.UserId, ex);
        return Error("检查登录状态失败");
      }
    }

    /// <summary>
    /// 清理过期会话
    /// </summary>
    /// <returns>清理结果</returns>
    [HttpPost("cleanup-sessions")]
    [TaktLog("清理过期会话")]
    [TaktPerm("security:singleuserlogin:manage")]
    public async Task<IActionResult> CleanupExpiredSessions()
    {
      try
      {
        var cleanedCount = await _singleUserLoginService.CleanupExpiredSessionsAsync();
        return Success(new { cleanedCount }, $"清理过期会话成功，共清理 {cleanedCount} 个会话");
      }
      catch (Exception ex)
      {
        _logger.Error("清理过期会话失败", ex);
        return Error("清理过期会话失败");
      }
    }

    /// <summary>
    /// 验证设备指纹
    /// </summary>
    /// <param name="request">验证请求</param>
    /// <returns>验证结果</returns>
    [HttpPost("validate-fingerprint")]
    [TaktPerm("security:singleuserlogin:query")]
    public async Task<IActionResult> ValidateDeviceFingerprint([FromBody] ValidateFingerprintRequest request)
    {
      try
      {
        var isValid = await _singleUserLoginService.ValidateDeviceFingerprintAsync(
            request.UserId,
            request.DeviceId,
            request.Fingerprint);

        return Success(new { isValid }, "验证设备指纹成功");
      }
      catch (Exception ex)
      {
        _logger.Error("验证设备指纹失败 - 用户ID: {UserId}, 设备ID: {DeviceId}", request.UserId, request.DeviceId, ex);
        return Error("验证设备指纹失败");
      }
    }
  }

  /// <summary>
  /// 检查登录请求
  /// </summary>
  public class CheckLoginRequest
  {
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }
  }

  /// <summary>
  /// 验证设备指纹请求
  /// </summary>
  public class ValidateFingerprintRequest
  {
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>
    /// 设备指纹
    /// </summary>
    public string Fingerprint { get; set; } = string.Empty;
  }
}





