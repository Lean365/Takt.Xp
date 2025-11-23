//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSessionOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:00
// 版本号 : V0.0.1
// 描述    : 会话配置选项
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Shared.Options;

/// <summary>
/// 会话配置选项
/// </summary>
public class TaktSessionOptions : IValidatableObject
{
    #region 单用户登录配置

    /// <summary>
    /// 是否启用单用户登录
    /// 启用后，用户只能在一个设备上登录，新设备登录时会被阻止
    /// </summary>
    public bool SingleUserLoginEnabled { get; set; } = false;



    /// <summary>
    /// 是否启用设备指纹验证
    /// </summary>
    public bool EnableDeviceFingerprint { get; set; } = true;

    /// <summary>
    /// 是否启用IP地址验证
    /// </summary>
    public bool EnableIpValidation { get; set; } = false;

    /// <summary>
    /// 是否启用地理位置验证
    /// </summary>
    public bool EnableLocationValidation { get; set; } = false;

    /// <summary>
    /// 心跳检测间隔（秒）
    /// </summary>
    public int HeartbeatIntervalSeconds { get; set; } = 30;

    /// <summary>
    /// 离线检测延迟（秒）
    /// </summary>
    public int OfflineDetectionDelaySeconds { get; set; } = 60;

    #endregion

    #region 会话管理配置

    /// <summary>
    /// 会话超时时间(分钟)
    /// </summary>
    public int TimeoutMinutes { get; set; } = 30;

    /// <summary>
    /// 是否启用滑动过期
    /// </summary>
    public bool EnableSlidingExpiration { get; set; } = true;

    /// <summary>
    /// 是否启用绝对过期
    /// </summary>
    public bool EnableAbsoluteExpiration { get; set; } = true;

    /// <summary>
    /// 绝对过期时间(小时)
    /// </summary>
    public int AbsoluteExpirationHours { get; set; } = 24;

    /// <summary>
    /// 会话过期时间(分钟)
    /// </summary>
    public int SessionExpiryMinutes { get; set; } = 60;

    #endregion

    #region 计算属性

    /// <summary>
    /// 获取实际的最大并发会话数
    /// 单用户登录模式下为1，多设备模式下为3
    /// </summary>
    public int ActualMaxConcurrentSessions => SingleUserLoginEnabled ? 1 : 3;

    /// <summary>
    /// 获取实际是否允许多设备登录
    /// 单用户登录模式下为false，多设备模式下为true
    /// </summary>
    public bool ActualAllowMultipleDevices => !SingleUserLoginEnabled;

    #endregion

    #region 配置验证

    /// <summary>
    /// 验证配置项的正确性
    /// </summary>
    /// <param name="validationContext">验证上下文</param>
    /// <returns>验证结果列表</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        // 验证单用户登录配置逻辑一致性
        if (SingleUserLoginEnabled)
        {
            // 当启用单用户登录时，检查配置是否正确
            if (ActualMaxConcurrentSessions != 1)
            {
                results.Add(new ValidationResult(
                    "启用单用户登录时，ActualMaxConcurrentSessions必须为1",
                    new[] { nameof(ActualMaxConcurrentSessions) }));
            }

            if (ActualAllowMultipleDevices != false)
            {
                results.Add(new ValidationResult(
                    "启用单用户登录时，ActualAllowMultipleDevices必须为false",
                    new[] { nameof(ActualAllowMultipleDevices) }));
            }
        }
        else
        {
            // 当未启用单用户登录时，检查配置是否正确
            if (ActualMaxConcurrentSessions <= 1)
            {
                results.Add(new ValidationResult(
                    "未启用单用户登录时，ActualMaxConcurrentSessions必须大于1",
                    new[] { nameof(ActualMaxConcurrentSessions) }));
            }

            if (ActualAllowMultipleDevices != true)
            {
                results.Add(new ValidationResult(
                    "未启用单用户登录时，ActualAllowMultipleDevices必须为true",
                    new[] { nameof(ActualAllowMultipleDevices) }));
            }
        }

        // 验证其他配置项
        if (TimeoutMinutes <= 0)
        {
            results.Add(new ValidationResult(
                "会话超时时间必须大于0",
                new[] { nameof(TimeoutMinutes) }));
        }

        if (HeartbeatIntervalSeconds <= 0)
        {
            results.Add(new ValidationResult(
                "心跳检测间隔必须大于0",
                new[] { nameof(HeartbeatIntervalSeconds) }));
        }

        if (OfflineDetectionDelaySeconds <= 0)
        {
            results.Add(new ValidationResult(
                "离线检测延迟必须大于0",
                new[] { nameof(OfflineDetectionDelaySeconds) }));
        }

        return results;
    }

    #endregion
} 




