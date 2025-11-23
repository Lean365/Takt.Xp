//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 扩展
// 命名空间 : Takt.Domain.IServices.Extensions
// 文件名 : ITaktDeviceIdGenerator.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 设备ID生成器接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================
namespace Takt.Domain.IServices.Extensions;

/// <summary>
/// 设备ID生成器接口
/// </summary>
public interface ITaktDeviceIdGenerator
{
  /// <summary>
  /// 生成设备ID
  /// </summary>
  /// <param name="deviceInfoJson">设备信息JSON</param>
  /// <param name="userId">用户ID</param>
  /// <param name="userAgent">用户代理字符串（可选）</param>
  /// <param name="loginIp">登录IP地址（可选）</param>
  /// <returns>设备ID</returns>
  string GenerateDeviceId(string? deviceInfoJson, string userId, string? userAgent = null, string? loginIp = null);

  /// <summary>
  /// 生成连接ID
  /// </summary>
  /// <param name="deviceId">设备ID</param>
  /// <returns>连接ID</returns>
  string GenerateConnectionId(string deviceId);


}
