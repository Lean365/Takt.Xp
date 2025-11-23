using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Takt.Infrastructure.Identity;

/// <summary>
/// 设备ID生成器实现
/// </summary>
public class TaktDeviceIdGenerator : ITaktDeviceIdGenerator
{
    /// <summary>
    /// 生成设备ID
    /// </summary>
    /// <param name="deviceInfoJson">设备信息JSON</param>
    /// <param name="userId">用户ID</param>
    /// <param name="userAgent">用户代理字符串（可选）</param>
    /// <param name="loginIp">登录IP地址（可选）</param>
    /// <returns>设备ID</returns>
    public string GenerateDeviceId(string? deviceInfoJson, string userId, string? userAgent = null, string? loginIp = null)
    {
        // 提取设备特征
        var deviceFeatures = ExtractDeviceFeatures(deviceInfoJson);

        // 如果提供了后端信息，则使用后端信息覆盖前端信息
        var finalUserAgent = !string.IsNullOrEmpty(userAgent) ? userAgent : deviceFeatures.userAgent;
        var finalLoginIp = !string.IsNullOrEmpty(loginIp) ? loginIp : deviceFeatures.loginIp;

        // 重新构建浏览器信息，使用后端提供的UserAgent
        var browserInfo = $"{deviceFeatures.browserType}:{finalUserAgent}:canvas{deviceFeatures.canvasFingerprint}:webgl{deviceFeatures.webglFingerprint}:audio{deviceFeatures.audioFingerprint}:fonts{deviceFeatures.fontsFingerprint}:plugins{deviceFeatures.pluginsFingerprint}:touch{deviceFeatures.touchSupport}:cookie{deviceFeatures.cookieEnabled}:dnt{deviceFeatures.doNotTrack}";

        // 生成设备ID（使用完整的32位哈希值）
        // 注意：不使用deviceFeatures.deviceId，因为前端传递的deviceId可能重复
        // 完全基于设备特征生成唯一ID
        return GenerateHash(
            string.Join(":", new[]
            {
                // 使用前端传递的LoginFingerprint作为主要唯一标识
                deviceFeatures.deviceFingerprint ?? "unknown",
                deviceFeatures.deviceType,
                deviceFeatures.deviceModel,
                deviceFeatures.osType,
                userId,
                // 添加硬件信息增加唯一性
                deviceFeatures.resolution ?? "unknown",
                browserInfo,
                // 添加IP地址确保不同电脑生成不同设备ID
                finalLoginIp ?? "unknown"
            })
        );
    }

    /// <summary>
    /// 生成连接ID
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>连接ID</returns>
    public string GenerateConnectionId(string deviceId)
    {
        // 生成连接ID（32位）
        return GenerateHash(
            string.Join(":", new[]
            {
                deviceId,
                DateTime.UtcNow.Ticks.ToString("x16"),  // 16进制格式的时间戳
                Guid.NewGuid().ToString("N"),           // 无分隔符的GUID
                Random.Shared.Next().ToString("x8")     // 随机数
            })
        ).Substring(0, 32);
    }


    /// <summary>
    /// 从JSON中提取设备特征
    /// </summary>
    private (string deviceId, string deviceType, string deviceModel, string osType, string? deviceFingerprint, string? resolution, string? browserInfo, string? loginIp, string userAgent, string browserType, string canvasFingerprint, string webglFingerprint, string audioFingerprint, string fontsFingerprint, string pluginsFingerprint, string touchSupport, string cookieEnabled, string doNotTrack)
        ExtractDeviceFeatures(string? deviceInfoJson)
    {
        if (string.IsNullOrEmpty(deviceInfoJson))
        {
            return ("unknown", "unknown", "unknown", "unknown", null, null, null, null, "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown");
        }

        try
        {
            using var doc = JsonDocument.Parse(deviceInfoJson);
            var root = doc.RootElement;

            var deviceId = GetJsonValue(root, "DeviceId", "unknown");
            var deviceType = GetJsonValue(root, "DeviceType", "unknown");

            // 提取更多独特字段，确保不同设备生成不同ID
            var hardwareConcurrency = GetJsonValue(root, "HardwareConcurrency", "unknown");
            var deviceMemory = GetJsonValue(root, "DeviceMemory", "unknown");
            var platform = GetJsonValue(root, "Platform", "unknown");
            var osType = GetJsonValue(root, "OsType", GetJsonValue(root, "Os", "unknown"));
            var deviceFingerprint = GetJsonValueNullable(root, "LoginFingerprint", null);

            // 提取更多独特特征
            var resolution = GetJsonValueNullable(root, "Resolution", GetJsonValueNullable(root, "ScreenResolution", null));
            var browserType = GetJsonValue(root, "BrowserType", GetJsonValue(root, "Browser", "unknown"));
            var userAgent = GetJsonValue(root, "UserAgent", "unknown");
            var loginIp = GetJsonValueNullable(root, "LoginIp", null);

            // 提取更多独特字段
            var isVpn = GetJsonValue(root, "IsVpn", "unknown");
            var isProxy = GetJsonValue(root, "IsProxy", "unknown");
            var networkType = GetJsonValue(root, "NetworkType", "unknown");
            var colorDepth = GetJsonValue(root, "ColorDepth", "unknown");
            var pixelRatio = GetJsonValue(root, "PixelRatio", "unknown");
            var language = GetJsonValue(root, "Language", "unknown");
            var timezone = GetJsonValue(root, "Timezone", "unknown");
            var canvasFingerprint = GetJsonValue(root, "CanvasFingerprint", "unknown");
            var webglFingerprint = GetJsonValue(root, "WebglFingerprint", "unknown");
            var audioFingerprint = GetJsonValue(root, "AudioFingerprint", "unknown");
            var fontsFingerprint = GetJsonValue(root, "FontsFingerprint", "unknown");
            var pluginsFingerprint = GetJsonValue(root, "PluginsFingerprint", "unknown");
            var touchSupport = GetJsonValue(root, "TouchSupport", "unknown");
            var cookieEnabled = GetJsonValue(root, "CookieEnabled", "unknown");
            var doNotTrack = GetJsonValue(root, "DoNotTrack", "unknown");

            // 构建更详细的设备模型，包含所有独特信息
            var deviceModel = $"{platform}-{hardwareConcurrency}cores-{deviceMemory}-{colorDepth}bit-{pixelRatio}x-{networkType}-vpn{isVpn}-proxy{isProxy}-lang{language}-tz{timezone}";

            // 构建更详细的浏览器信息
            var browserInfo = $"{browserType}:{userAgent}:canvas{canvasFingerprint}:webgl{webglFingerprint}:audio{audioFingerprint}:fonts{fontsFingerprint}:plugins{pluginsFingerprint}:touch{touchSupport}:cookie{cookieEnabled}:dnt{doNotTrack}";

            return (deviceId, deviceType, deviceModel, osType, deviceFingerprint, resolution, browserInfo, loginIp, userAgent, browserType, canvasFingerprint, webglFingerprint, audioFingerprint, fontsFingerprint, pluginsFingerprint, touchSupport, cookieEnabled, doNotTrack);
        }
        catch
        {
            return ("unknown", "unknown", "unknown", "unknown", null, null, null, null, "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown", "unknown");
        }
    }

    /// <summary>
    /// 安全获取JSON值
    /// </summary>
    private string GetJsonValue(JsonElement element, string propertyName, string defaultValue)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            return property.GetString() ?? defaultValue;
        }
        return defaultValue;
    }

    /// <summary>
    /// 安全获取JSON值（可空版本）
    /// </summary>
    private string? GetJsonValueNullable(JsonElement element, string propertyName, string? defaultValue)
    {
        if (element.TryGetProperty(propertyName, out var property))
        {
            return property.GetString();
        }
        return defaultValue;
    }

    /// <summary>
    /// 生成SHA256哈希
    /// </summary>
    private string GenerateHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }


}
