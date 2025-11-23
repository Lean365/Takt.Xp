//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginFingerprint.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 登录设备指纹信息模型
//===================================================================

#nullable enable

namespace Takt.Shared.Models;

/// <summary>
/// 登录设备指纹信息
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-12-01
/// </remarks>
public class TaktLoginFingerprint
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// 设备类型
    /// Desktop, Mobile, Tablet, Watch, Console, Server, Other
    /// </summary>
    public string? DeviceType { get; set; }

    /// <summary>
    /// 设备指纹（唯一标识）
    /// </summary>
    public string? LoginFingerprint { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// 时区
    /// </summary>
    public string? Timezone { get; set; }

    /// <summary>
    /// 屏幕分辨率
    /// </summary>
    public string? ScreenResolution { get; set; }

    /// <summary>
    /// 颜色深度
    /// </summary>
    public int? ColorDepth { get; set; }

    /// <summary>
    /// 像素比例
    /// </summary>
    public double? PixelRatio { get; set; }

    /// <summary>
    /// Canvas指纹
    /// </summary>
    public string? CanvasFingerprint { get; set; }

    /// <summary>
    /// WebGL指纹
    /// </summary>
    public string? WebglFingerprint { get; set; }

    /// <summary>
    /// 音频指纹
    /// </summary>
    public string? AudioFingerprint { get; set; }

    /// <summary>
    /// 字体指纹
    /// </summary>
    public string? FontsFingerprint { get; set; }

    /// <summary>
    /// 插件指纹
    /// </summary>
    public string? PluginsFingerprint { get; set; }

    /// <summary>
    /// 触摸支持信息
    /// </summary>
    public string? TouchSupport { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 是否使用VPN
    /// </summary>
    public string? IsVpn { get; set; }

    /// <summary>
    /// 是否使用代理
    /// </summary>
    public string? IsProxy { get; set; }

    /// <summary>
    /// 网络类型
    /// </summary>
    public string? NetworkType { get; set; }

    /// <summary>
    /// 硬件并发数
    /// </summary>
    public int? HardwareConcurrency { get; set; }

    /// <summary>
    /// 设备内存
    /// </summary>
    public string? DeviceMemory { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    /// 是否启用Cookie
    /// </summary>
    public bool? CookieEnabled { get; set; }

    /// <summary>
    /// 不跟踪设置
    /// </summary>
    public string? DoNotTrack { get; set; }
}





