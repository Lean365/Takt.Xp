namespace Takt.Application.Dtos.Security;

/// <summary>
/// 滑块验证码生成结果
/// </summary>
public class SliderCaptchaDto
{
    /// <summary>
    /// 背景图片(Base64)
    /// </summary>
    public string? BackgroundImage { get; set; }

    /// <summary>
    /// 滑块图片(Base64)
    /// </summary>
    public string? SliderImage { get; set; }

    /// <summary>
    /// 验证令牌
    /// </summary>
    public string? Token { get; set; }
}

/// <summary>
/// 滑块验证请求
/// </summary>
public class SliderValidateDto
{
    /// <summary>
    /// 验证令牌
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// X轴偏移量
    /// </summary>
    public int XOffset { get; set; }
}

/// <summary>
/// 行为数据点
/// </summary>
public class BehaviorPointDto
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

/// <summary>
/// 行为数据
/// </summary>
public class BehaviorDataDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// 鼠标轨迹
    /// </summary>
    public List<BehaviorPointDto> MouseTrack { get; set; } = new();

    /// <summary>
    /// 按键间隔(毫秒)
    /// </summary>
    public List<int> KeyIntervals { get; set; } = new();

    /// <summary>
    /// 操作时长(毫秒)
    /// </summary>
    public int Duration { get; set; }
}

/// <summary>
/// 行为验证请求
/// </summary>
public class BehaviorValidateDto
{
    /// <summary>
    /// 验证令牌
    /// </summary>
    public string? Token { get; set; }
}

/// <summary>
/// 验证结果
/// </summary>
public class CaptchaResultDto
{
    /// <summary>
    /// 是否通过验证
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// 验证码配置DTO
/// </summary>
public class CaptchaConfigDto
{
    /// <summary>
    /// 是否启用验证码
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 验证码类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 滑块验证码配置
    /// </summary>
    public SliderConfigDto Slider { get; set; } = new();

    /// <summary>
    /// 行为验证配置
    /// </summary>
    public BehaviorConfigDto Behavior { get; set; } = new();
}

/// <summary>
/// 滑块验证码配置DTO
/// </summary>
public class SliderConfigDto
{
    /// <summary>
    /// 背景图片宽度
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 背景图片高度
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 滑块宽度
    /// </summary>
    public int SliderWidth { get; set; }

    /// <summary>
    /// 验证容差(像素)
    /// </summary>
    public int Tolerance { get; set; }

    /// <summary>
    /// 缓存过期时间(分钟)
    /// </summary>
    public int ExpirationMinutes { get; set; }
}

/// <summary>
/// 行为验证配置DTO
/// </summary>
public class BehaviorConfigDto
{
    /// <summary>
    /// 验证通过的最低分数
    /// </summary>
    public double ScoreThreshold { get; set; }

    /// <summary>
    /// 行为数据缓存时间(分钟)
    /// </summary>
    public int DataExpirationMinutes { get; set; }

    /// <summary>
    /// 是否启用机器学习模型
    /// </summary>
    public bool EnableMachineLearning { get; set; }
}
