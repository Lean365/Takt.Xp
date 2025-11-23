using System.Security.Cryptography;
using Newtonsoft.Json;
using Takt.Application.Services;
using Takt.Shared.Options;
using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Point = SixLabors.ImageSharp.Point;
using Microsoft.AspNetCore.Http;

namespace Takt.Infrastructure.Security;

/// <summary>
/// 验证码服务实现
/// </summary>
public class TaktCaptchaService : TaktBaseService, ITaktCaptchaService
{
    private readonly IDistributedCache _cache;
    private readonly TaktCaptchaOptions _options;
    private readonly string _sliderCachePrefix = "slider:";
    private readonly string _behaviorCachePrefix = "behavior:";
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _backgroundImagesPath;
    private readonly string _templatePath;

    /// <summary>
    /// 初始化验证码服务
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="options">验证码选项</param>
    /// <param name="webHostEnvironment">Web主机环境</param>
    /// <param name="logger">日志记录器</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktCaptchaService(
        IDistributedCache cache,
        IOptions<TaktCaptchaOptions> options,
        IWebHostEnvironment webHostEnvironment,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,         
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _logger.Info("开始构造验证码服务...");

        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

        _templatePath = Path.Combine(_webHostEnvironment.WebRootPath, _options.Slider.BackgroundImages.Template.TemplatePath);
        _backgroundImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, _options.Slider.BackgroundImages.StoragePath);

        _logger.Info("验证码服务构造完成");
    }

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    public async Task<(string bgImage, string sliderImage, string token)> GenerateSliderAsync()
    {
        // 随机选择一组模板
        var random = new Random();
        var groupIndex = random.Next(1, _options.Slider.BackgroundImages.Template.GroupCount + 1);
        var groupPath = Path.Combine(_templatePath, groupIndex.ToString());

        // 构建模板文件路径
        var holePath = Path.Combine(groupPath, "hole.png");
        var sliderPath = Path.Combine(groupPath, "slider.png");

        if (!File.Exists(holePath) || !File.Exists(sliderPath))
        {
            throw new InvalidOperationException("验证码模板文件不存在");
        }

        // 加载模板图片
        using var holeImage = await Image.LoadAsync<Rgba32>(holePath);
        using var sliderImage = await Image.LoadAsync<Rgba32>(sliderPath);

        // 随机选择一张背景图片tl
        var backgroundFiles = Directory.GetFiles(_backgroundImagesPath, $"*{_options.Slider.BackgroundImages.FileExtension}");
        if (backgroundFiles.Length == 0)
        {
            throw new Exception("没有可用的背景图片");
        }

        var selectedBackground = backgroundFiles[random.Next(backgroundFiles.Length)];
        using var bgImage = await Image.LoadAsync<Rgba32>(selectedBackground);

        // 调整背景图片大小
        var targetWidth = _options.Slider.Width;  // 350px
        var targetHeight = _options.Slider.Height; // 150px
        bgImage.Mutate(x => x.Resize(targetWidth, targetHeight));

        // 使用配置中的滑块尺寸
        var sliderSize = _options.Slider.SliderWidth; // 滑块的实际尺寸

        // 计算有效的X坐标范围（hole在背景图片内的位置）
        var minX = 0; // hole可以在背景图片的最左端
        var maxX = targetWidth - sliderSize; // 最大位置为背景宽度减去滑块宽度

        // 生成随机的hole位置，Y坐标固定在中间位置
        var xPos = random.Next(minX, maxX);
        var yPos = (targetHeight - sliderSize) / 2;

        _logger.Info("生成滑块验证码 - 背景尺寸: {Width}x{Height}, 滑块尺寸: {SliderSize}, hole位置范围: {MinX}-{MaxX}, hole位置: ({X}, {Y})",
            targetWidth, targetHeight, sliderSize, minX, maxX, xPos, yPos);

        // 创建一个新的背景图副本，用于应用挖空效果
        using var processedBgImage = bgImage.Clone();

        // 在背景图上应用挖空效果
        processedBgImage.Mutate(x => x.DrawImage(holeImage, new Point(xPos, yPos), 0.8f));

        // 创建滑块图片（保持原始大小）
        using var finalSliderImage = new Image<Rgba32>(sliderSize, sliderSize);
        finalSliderImage.Mutate(x =>
        {
            x.Clear(Color.Transparent);
            x.DrawImage(sliderImage, new Point(0, 0), 1f);
        });

        // 生成验证token和缓存数据
        var token = GenerateToken();
        var cacheData = new SliderCacheData
        {
            X = xPos,
            Y = yPos,
            CreatedAt = DateTime.UtcNow
        };

        // 缓存验证数据
        await _cache.SetStringAsync(
            $"{_sliderCachePrefix}{token}",
            JsonConvert.SerializeObject(cacheData),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Slider.ExpirationMinutes)
            }
        );

        return (
            ImageToBase64(processedBgImage),
            ImageToBase64(finalSliderImage),
            token
        );
    }

    /// <summary>
    /// 验证滑块
    /// </summary>
    public async Task<bool> ValidateSliderAsync(string token, int xOffset)
    {
        _logger.Info("验证Token详情 - 接收到的Token: {Token}, 时间: {Time}", token, DateTime.Now);

        var cacheKey = $"{_sliderCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheValue))
        {
            _logger.Warn("验证失败：Token无效或已过期 - Token: {Token}, 缓存Key: {CacheKey}", token, cacheKey);
            return false;
        }

                    var cacheData = JsonConvert.DeserializeObject<SliderCacheData>(cacheValue);
        if (cacheData == null)
        {
            _logger.Warn("验证失败：缓存数据无效 - Token: {Token}", token);
            return false;
        }

        // 验证是否过期
        var timeSinceCreation = DateTime.UtcNow - cacheData.CreatedAt;
        if (timeSinceCreation.TotalMinutes > _options.Slider.ExpirationMinutes)
        {
            _logger.Warn("验证失败：验证码已过期 - Token: {Token}, 创建时间: {CreatedAt}, 当前时间: {Now}, 已过时间: {TimeSinceCreation:g}",
                token,
                cacheData.CreatedAt,
                DateTime.UtcNow,
                timeSinceCreation);
            await _cache.RemoveAsync(cacheKey);
            return false;
        }

        // 验证是否已被使用
        if (cacheData.IsVerified)
        {
            _logger.Warn("验证失败：验证码已被使用 - Token: {Token}", token);
            return false;
        }

        // 验证偏移量是否在容差范围内
        var difference = Math.Abs(xOffset - cacheData.X);
        var isValid = difference <= _options.Slider.Tolerance;

        _logger.Info(
            "验证详情 - hole位置: {HoleX}, 滑块在背景图片内位置: {SliderX}, 差值: {Difference}, 容差: {Tolerance}, 结果: {Result}",
            cacheData.X,
            xOffset,
            difference,
            _options.Slider.Tolerance,
            isValid ? "验证通过" : "验证失败"
        );

        return isValid;
    }

    /// <summary>
    /// 标记验证码已使用
    /// </summary>
    public async Task MarkAsUsedAsync(string token)
    {
        var cacheKey = $"{_sliderCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cacheValue))
        {
            var cacheData = JsonConvert.DeserializeObject<SliderCacheData>(cacheValue);
            if (cacheData != null)
            {
                cacheData.IsVerified = true;
                await _cache.SetStringAsync(
                    cacheKey,
                    JsonConvert.SerializeObject(cacheData),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Slider.ExpirationMinutes)
                    }
                );
                _logger.Info("验证码已标记为已使用 - Token: {Token}", token);
            }
        }
    }

    /// <summary>
    /// 收集行为数据
    /// </summary>
    public async Task<string> CollectBehaviorDataAsync(string userId, BehaviorData data)
    {
        var token = GenerateToken();
        var cacheData = new BehaviorCacheData
        {
            UserId = userId,
            Data = data,
            CreatedAt = DateTime.UtcNow,
            Score = CalculateBehaviorScore(data)
        };

        await _cache.SetStringAsync(
            $"{_behaviorCachePrefix}{token}",
            JsonConvert.SerializeObject(cacheData),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Behavior.DataExpirationMinutes)
            }
        );

        return token;
    }

    /// <summary>
    /// 验证行为特征
    /// </summary>
    public async Task<bool> ValidateBehaviorAsync(string token)
    {
        _logger.Info("开始验证行为特征: Token={Token}", token);
        
        var cacheKey = $"{_behaviorCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheValue))
        {
            _logger.Warn("行为验证缓存未找到: Token={Token}", token);
            return false;
        }

        var cacheData = JsonConvert.DeserializeObject<BehaviorCacheData>(cacheValue);
        if (cacheData == null)
        {
            _logger.Warn("行为验证缓存数据解析失败: Token={Token}", token);
            return false;
        }

        // 验证是否过期
        if ((DateTime.UtcNow - cacheData.CreatedAt).TotalMinutes > _options.Behavior.DataExpirationMinutes)
        {
            _logger.Warn("行为验证数据已过期: Token={Token}, CreatedAt={CreatedAt}", token, cacheData.CreatedAt);
            await _cache.RemoveAsync(cacheKey);
            return false;
        }

        // 验证行为分数是否达到阈值
        var isValid = cacheData.Score >= _options.Behavior.ScoreThreshold;
        
        _logger.Info("行为验证结果: Token={Token}, Score={Score}, Threshold={Threshold}, IsValid={IsValid}", 
            token, cacheData.Score, _options.Behavior.ScoreThreshold, isValid);

        // 验证通过后标记为已验证，但不删除缓存，等待登录时使用
        if (isValid)
        {
            cacheData.IsVerified = true;
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.Behavior.DataExpirationMinutes)
            };
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(cacheData), options);
        }
        else
        {
            // 验证失败时删除缓存
            await _cache.RemoveAsync(cacheKey);
        }

        return isValid;
    }

    /// <summary>
    /// 验证行为特征（用于登录验证）
    /// </summary>
    public async Task<bool> ValidateBehaviorForLoginAsync(string token)
    {
        _logger.Info("开始验证行为特征（登录）: Token={Token}", token);
        
        var cacheKey = $"{_behaviorCachePrefix}{token}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cacheValue))
        {
            _logger.Warn("行为验证缓存未找到（登录）: Token={Token}", token);
            return false;
        }

        var cacheData = JsonConvert.DeserializeObject<BehaviorCacheData>(cacheValue);
        if (cacheData == null)
        {
            _logger.Warn("行为验证缓存数据解析失败（登录）: Token={Token}", token);
        }

        // 验证是否过期
        if ((DateTime.UtcNow - cacheData.CreatedAt).TotalMinutes > _options.Behavior.DataExpirationMinutes)
        {
            _logger.Warn("行为验证数据已过期（登录）: Token={Token}, CreatedAt={CreatedAt}", token, cacheData.CreatedAt);
            await _cache.RemoveAsync(cacheKey);
            return false;
        }

        // 检查是否已经通过行为验证
        if (!cacheData.IsVerified)
        {
            _logger.Warn("行为验证未通过（登录）: Token={Token}, IsVerified={IsVerified}", token, cacheData.IsVerified);
            return false;
        }

        _logger.Info("行为验证通过（登录）: Token={Token}, Score={Score}", token, cacheData.Score);

        // 登录验证成功后删除缓存
        await _cache.RemoveAsync(cacheKey);

        return true;
    }

    #region 私有方法

    /// <summary>
    /// 生成验证token
    /// </summary>
    private static string GenerateToken()
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
    }

    /// <summary>
    /// 计算行为分数
    /// </summary>
    private double CalculateBehaviorScore(BehaviorData data)
    {
        if (_options.Behavior.EnableMachineLearning && !string.IsNullOrEmpty(_options.Behavior.ModelPath))
        {
            // TODO: 使用机器学习模型计算分数
            return 0.9;
        }

        var score = 0.0;

        // 基础分数：只要有操作就给基础分
        score += 0.2;

        // 1. 检查操作时长是否合理
        if (data.Duration >= 500 && data.Duration <= 5000)
        {
            score += 0.3;
        }
        else if (data.Duration > 0 && data.Duration < 10000)
        {
            // 放宽时长限制，给部分分数
            score += 0.1;
        }

        // 2. 分析鼠标轨迹
        if (data.MouseTrack.Count >= 10)
        {
            score += 0.3;

            // 检查轨迹是否平滑
            var smoothScore = CalculateTrackSmoothness(data.MouseTrack);
            score += smoothScore * 0.2;
        }
        else if (data.MouseTrack.Count >= 5)
        {
            // 降低轨迹点数要求，给部分分数
            score += 0.2;
        }

        // 3. 分析按键间隔（可选，不影响主要验证）
        if (data.KeyIntervals.Count >= 2)
        {
            score += 0.1;
        }

        _logger.Info("行为验证分数计算详情: 基础分=0.2, 时长={Duration}ms, 轨迹点数={TrackCount}, 按键间隔数={KeyIntervalCount}, 最终分数={Score}", 
            data.Duration, data.MouseTrack.Count, data.KeyIntervals.Count, score);

        return Math.Min(score, 1.0);
    }

    /// <summary>
    /// 计算轨迹平滑度
    /// </summary>
    private double CalculateTrackSmoothness(List<Domain.IServices.Security.Point> track)
    {
        if (track.Count < 3)
        {
            return 0;
        }

        var smoothCount = 0;
        for (int i = 1; i < track.Count - 1; i++)
        {
            var prev = track[i - 1];
            var curr = track[i];
            var next = track[i + 1];

            // 计算两个向量的夹角
            var v1x = curr.X - prev.X;
            var v1y = curr.Y - prev.Y;
            var v2x = next.X - curr.X;
            var v2y = next.Y - curr.Y;

            var cos = (v1x * v2x + v1y * v2y) /
                     (Math.Sqrt(v1x * v1x + v1y * v1y) * Math.Sqrt(v2x * v2x + v2y * v2y));

            // 夹角小于45度认为是平滑的
            if (cos > 0.7)
            {
                smoothCount++;
            }
        }

        return (double)smoothCount / (track.Count - 2);
    }

    /// <summary>
    /// 将图片转换为Base64字符串
    /// </summary>
    private static string ImageToBase64<TPixel>(Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>
    {
        using var ms = new MemoryStream();
        image.SaveAsPng(ms);
        return $"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}";
    }

    #endregion 私有方法

    #region 私有类

    private class SliderCacheData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }
    }

    private class BehaviorCacheData
    {
        public string UserId { get; set; } = string.Empty;
        public BehaviorData Data { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public double Score { get; set; }
        public bool IsVerified { get; set; }
    }

    #endregion 私有类
}

