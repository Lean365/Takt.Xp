using Takt.Application.Dtos.Security;
using Takt.Domain.IServices.Security;
using Takt.Shared.Options;
using Takt.Shared.Models;
using Microsoft.Extensions.Options;

namespace Takt.WebApi.Controllers.Security;

/// <summary>
/// 验证码控制器
/// </summary>
[Route("api/[controller]", Name = "验证码")]
[ApiController]
[ApiModule("identity", "身份认证")]
public class TaktCaptchaController : TaktBaseController
{
    private readonly ITaktCaptchaService _captchaService;
    private readonly TaktCaptchaOptions _captchaOptions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="captchaOptions">验证码配置选项</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktCaptchaController(
        ITaktCaptchaService captchaService,
        IOptions<TaktCaptchaOptions> captchaOptions,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, currentUser, localization)
    {
        _captchaService = captchaService;
        _captchaOptions = captchaOptions.Value;
        _logger.Info("验证码控制器已创建");
    }

    /// <summary>
    /// 获取验证码配置信息
    /// </summary>
    /// <param name="scenario">验证码场景（login/register/password-recovery）</param>
    /// <returns>验证码配置信息</returns>
    [HttpGet("config")]
    [AllowAnonymous]
    public ActionResult<TaktApiResult<CaptchaConfigDto>> GetCaptchaConfig([FromQuery] string? scenario = null)
    {
        _logger.Info("获取验证码配置信息，场景: {Scenario}", scenario ?? "未指定");
        _logger.Info("验证码类型配置: {Type}", _captchaOptions.Type);
        _logger.Info("滑块配置: Width={Width}, Height={Height}, SliderWidth={SliderWidth}, Tolerance={Tolerance}, ExpirationMinutes={ExpirationMinutes}", 
            _captchaOptions.Slider.Width, _captchaOptions.Slider.Height, _captchaOptions.Slider.SliderWidth, 
            _captchaOptions.Slider.Tolerance, _captchaOptions.Slider.ExpirationMinutes);
        
        // 验证并限制验证码类型只能是 Behavior 或 Slider
        var validType = _captchaOptions.Type?.Trim();
        if (string.IsNullOrEmpty(validType) || 
            (validType != "Behavior" && validType != "Slider"))
        {
            _logger.Warn("配置的验证码类型 '{Type}' 无效，使用配置文件中的默认值", _captchaOptions.Type);
            // 如果配置无效，使用配置文件中的默认值，而不是硬编码
            validType = _captchaOptions.Type?.Trim() ?? "Behavior";
        }
        
        // 根据场景调整验证码配置
        var adjustedConfig = AdjustCaptchaConfigForScenario(validType, scenario);
        
        var config = new CaptchaConfigDto
        {
            Enabled = _captchaOptions.Enabled,
            Type = adjustedConfig.Type,
            Slider = new SliderConfigDto
            {
                Width = adjustedConfig.Slider.Width,
                Height = adjustedConfig.Slider.Height,
                SliderWidth = adjustedConfig.Slider.SliderWidth,
                Tolerance = adjustedConfig.Slider.Tolerance,
                ExpirationMinutes = adjustedConfig.Slider.ExpirationMinutes
            },
            Behavior = new BehaviorConfigDto
            {
                ScoreThreshold = adjustedConfig.Behavior.ScoreThreshold,
                DataExpirationMinutes = adjustedConfig.Behavior.DataExpirationMinutes,
                EnableMachineLearning = adjustedConfig.Behavior.EnableMachineLearning
            }
        };
        
        _logger.Info("返回的验证码配置: 场景={Scenario}, Type={Type}, Slider.Width={SliderWidth}, Behavior.ScoreThreshold={ScoreThreshold}", 
            scenario, config.Type, config.Slider.Width, config.Behavior.ScoreThreshold);
        
        return Ok(TaktApiResult<CaptchaConfigDto>.Success(config));
    }

    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    [HttpGet("slider")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<SliderCaptchaDto>>> GenerateSliderAsync()
    {
        _logger.Info("开始生成滑块验证码");
        try
        {
            var (bgImage, sliderImage, token) = await _captchaService.GenerateSliderAsync();
            _logger.Info("滑块验证码生成成功 - Token: {Token}, 时间: {Time}", token, DateTime.Now);
            var result = new SliderCaptchaDto
            {
                BackgroundImage = bgImage,
                SliderImage = sliderImage,
                Token = token
            };
            return Ok(TaktApiResult<SliderCaptchaDto>.Success(result));
        }
        catch (Exception ex)
        {
            _logger.Error("生成滑块验证码时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 验证滑块
    /// </summary>
    [HttpPost("slider/validate")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<CaptchaResultDto>>> ValidateSliderAsync([FromBody] SliderValidateDto request)
    {
        _logger.Info("开始验证滑块: Token={Token}, XOffset={XOffset}", request.Token, request.XOffset);
        try
        {
            var isValid = await _captchaService.ValidateSliderAsync(request.Token, request.XOffset);
            _logger.Info("滑块验证结果: {Result}", isValid ? "验证通过" : "验证失败");
            var result = new CaptchaResultDto
            {
                Success = isValid,
                Message = isValid ? "验证通过" : "验证失败"
            };
            return Ok(TaktApiResult<CaptchaResultDto>.Success(result));
        }
        catch (Exception ex)
        {
            _logger.Error("验证滑块时发生错误", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 收集行为数据
    /// </summary>
    [HttpPost("behavior/collect")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<string>>> CollectBehaviorDataAsync([FromBody] BehaviorDataDto request)
    {
        var token = await _captchaService.CollectBehaviorDataAsync(request.UserId, new Domain.IServices.Security.BehaviorData
        {
            MouseTrack = request.MouseTrack.Select(p => new Domain.IServices.Security.Point
            {
                X = p.X,
                Y = p.Y,
                Timestamp = p.Timestamp
            }).ToList(),
            KeyIntervals = request.KeyIntervals,
            Duration = request.Duration
        });
        return Ok(TaktApiResult<string>.Success(token));
    }

    /// <summary>
    /// 验证行为特征
    /// </summary>
    [HttpPost("behavior/validate")]
    [AllowAnonymous]
    public async Task<ActionResult<TaktApiResult<CaptchaResultDto>>> ValidateBehaviorAsync([FromBody] BehaviorValidateDto request)
    {
        _logger.Info("开始验证行为特征: Token={Token}", request.Token);
        
        if (string.IsNullOrEmpty(request.Token))
        {
            _logger.Warn("行为验证Token为空");
            var result = new CaptchaResultDto
            {
                Success = false,
                Message = "验证Token无效"
            };
            return Ok(TaktApiResult<CaptchaResultDto>.Success(result));
        }

        try
        {
            var isValid = await _captchaService.ValidateBehaviorAsync(request.Token);
            _logger.Info("行为验证结果: {Result}", isValid ? "验证通过" : "验证失败");
            
            var result = new CaptchaResultDto
            {
                Success = isValid,
                Message = isValid ? "验证通过" : "验证失败"
            };
            return Ok(TaktApiResult<CaptchaResultDto>.Success(result));
        }
        catch (Exception ex)
        {
            _logger.Error("验证行为特征时发生错误: {Message}", ex.Message);
            var result = new CaptchaResultDto
            {
                Success = false,
                Message = "验证过程中发生错误"
            };
            return Ok(TaktApiResult<CaptchaResultDto>.Success(result));
        }
    }

    /// <summary>
    /// 根据场景调整验证码配置
    /// </summary>
    /// <param name="baseType">基础验证码类型</param>
    /// <param name="scenario">场景</param>
    /// <returns>调整后的配置</returns>
    private (string Type, SliderOptions Slider, BehaviorOptions Behavior) AdjustCaptchaConfigForScenario(string baseType, string? scenario)
    {
        var adjustedType = baseType;
        var adjustedSlider = new SliderOptions
        {
            Width = _captchaOptions.Slider.Width,
            Height = _captchaOptions.Slider.Height,
            SliderWidth = _captchaOptions.Slider.SliderWidth,
            Tolerance = _captchaOptions.Slider.Tolerance,
            ExpirationMinutes = _captchaOptions.Slider.ExpirationMinutes
        };
        var adjustedBehavior = new BehaviorOptions
        {
            ScoreThreshold = _captchaOptions.Behavior.ScoreThreshold,
            DataExpirationMinutes = _captchaOptions.Behavior.DataExpirationMinutes,
            EnableMachineLearning = _captchaOptions.Behavior.EnableMachineLearning
        };

        switch (scenario?.ToLower())
        {
            case "login":
                _logger.Info("[验证码场景] 用户登录场景");
                // 登录场景：使用默认配置，但可以稍微降低难度
                adjustedSlider.Tolerance = Math.Min(adjustedSlider.Tolerance + 2, 20);
                adjustedBehavior.ScoreThreshold = Math.Max(adjustedBehavior.ScoreThreshold - 0.1, 0.6);
                break;

            case "register":
                _logger.Info("[验证码场景] 用户注册场景");
                // 注册场景：降低难度，提高用户体验
                adjustedSlider.Tolerance = Math.Min(adjustedSlider.Tolerance + 5, 25);
                adjustedBehavior.ScoreThreshold = Math.Max(adjustedBehavior.ScoreThreshold - 0.2, 0.5);
                adjustedSlider.ExpirationMinutes = Math.Max(adjustedSlider.ExpirationMinutes + 2, 7);
                break;

            case "password-recovery":
                _logger.Info("[验证码场景] 密码取回场景");
                // 密码取回场景：中等难度，平衡安全性和用户体验
                adjustedSlider.Tolerance = Math.Min(adjustedSlider.Tolerance + 3, 22);
                adjustedBehavior.ScoreThreshold = Math.Max(adjustedBehavior.ScoreThreshold - 0.15, 0.55);
                break;

            default:
                _logger.Info("[验证码场景] 未指定场景，使用默认配置");
                break;
        }

        return (adjustedType, adjustedSlider, adjustedBehavior);
    }
}

