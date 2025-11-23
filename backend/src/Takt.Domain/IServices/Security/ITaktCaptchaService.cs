//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 安全认证
// 命名空间 : Takt.Domain.IServices.Security
// 文件名 : ITaktCaptchaService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 验证码服务接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================
namespace Takt.Domain.IServices.Security;

/// <summary>
/// 验证码服务接口
/// </summary>
public interface ITaktCaptchaService
{
  /// <summary>
  /// 生成滑块验证码
  /// </summary>
  /// <returns>背景图、滑块图、验证token</returns>
  Task<(string bgImage, string sliderImage, string token)> GenerateSliderAsync();

  /// <summary>
  /// 验证滑块
  /// </summary>
  /// <param name="token">验证token</param>
  /// <param name="xOffset">x轴偏移量</param>
  /// <returns>验证结果</returns>
  Task<bool> ValidateSliderAsync(string token, int xOffset);

  /// <summary>
  /// 标记验证码为已使用
  /// </summary>
  Task MarkAsUsedAsync(string token);

  /// <summary>
  /// 收集行为数据
  /// </summary>
  /// <param name="userId">用户ID</param>
  /// <param name="data">行为数据</param>
  /// <returns>行为token</returns>
  Task<string> CollectBehaviorDataAsync(string userId, BehaviorData data);

  /// <summary>
  /// 验证行为特征
  /// </summary>
  /// <param name="token">行为token</param>
  /// <returns>验证结果</returns>
  Task<bool> ValidateBehaviorAsync(string token);

  /// <summary>
  /// 验证行为特征（用于登录验证）
  /// </summary>
  /// <param name="token">行为token</param>
  /// <returns>验证结果</returns>
  Task<bool> ValidateBehaviorForLoginAsync(string token);
}

/// <summary>
/// 行为数据
/// </summary>
public class BehaviorData
{
  /// <summary>
  /// 鼠标轨迹点集合
  /// </summary>
  public List<Point> MouseTrack { get; set; } = new();

  /// <summary>
  /// 按键间隔集合(毫秒)
  /// </summary>
  public List<int> KeyIntervals { get; set; } = new();

  /// <summary>
  /// 操作时长(毫秒)
  /// </summary>
  public int Duration { get; set; }
}

/// <summary>
/// 坐标点
/// </summary>
public class Point
{
  /// <summary>
  /// X坐标
  /// </summary>
  public int X { get; set; }

  /// <summary>
  /// Y坐标
  /// </summary>
  public int Y { get; set; }

  /// <summary>
  /// 时间戳
  /// </summary>
  public long Timestamp { get; set; }
}
