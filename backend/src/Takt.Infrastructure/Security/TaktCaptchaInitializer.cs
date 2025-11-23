using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Security;

/// <summary>
/// 验证码初始化服务
/// </summary>
public class TaktCaptchaInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 日志服务
    /// </summary>
    protected readonly ITaktLogger _logger;

    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly TaktCaptchaOptions _options;
    private readonly HttpClient _httpClient;
    private readonly string _backgroundImagesPath;
    private readonly string _templatePath;

    /// <summary>
    /// 初始化验证码初始化服务
    /// </summary>
    public TaktCaptchaInitializer(
        IServiceProvider serviceProvider,
        IWebHostEnvironment webHostEnvironment,
        IOptions<TaktCaptchaOptions> options,
        IHttpClientFactory httpClientFactory,
        ITaktLogger logger
)
    {
        _serviceProvider = serviceProvider;
        _webHostEnvironment = webHostEnvironment;
        _options = options.Value;
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;

        _backgroundImagesPath = Path.Combine(_webHostEnvironment.WebRootPath, _options.Slider.BackgroundImages.StoragePath);
        _templatePath = Path.Combine(_webHostEnvironment.WebRootPath, _options.Slider.BackgroundImages.Template.TemplatePath);
    }

    /// <summary>
    /// 启动验证码初始化服务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>初始化任务</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.Info("开始初始化验证码服务...");
        try
        {
            // 初始化资源
            await InitializeResourcesAsync();

            // 初始化服务
            using var scope = _serviceProvider.CreateScope();
            var captchaService = scope.ServiceProvider.GetRequiredService<ITaktCaptchaService>();

            // 生成一个验证码来验证服务
            _logger.Info("正在生成首个验证码以验证服务...");
            await captchaService.GenerateSliderAsync();

            _logger.Info("验证码服务初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error("初始化验证码服务时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 停止验证码初始化服务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>停止任务</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task InitializeResourcesAsync()
    {
        try
        {
            _logger.Info("开始初始化验证码资源...");

            // 确保 wwwroot 目录存在
            if (!Directory.Exists(_webHostEnvironment.WebRootPath))
            {
                _logger.Warn("wwwroot 目录不存在: {Path}", TaktMaskUtils.MaskPath(_webHostEnvironment.WebRootPath));
                Directory.CreateDirectory(_webHostEnvironment.WebRootPath);
                _logger.Info("已创建 wwwroot 目录");
            }

            // 验证模板
            await ValidateTemplatesAsync();

            // 初始化背景图片
            await InitializeBackgroundImagesAsync();

            _logger.Info("验证码资源初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error("初始化验证码资源时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    private async Task ValidateTemplatesAsync()
    {
        try
        {
            _logger.Info("开始验证滑块验证码模板...");

            // 检查并创建模板目录
            if (!await Task.Run(() => Directory.Exists(_templatePath)))
            {
                _logger.Info("创建模板目录: {Path}", TaktMaskUtils.MaskPath(_templatePath));
                await Task.Run(() => Directory.CreateDirectory(_templatePath));
            }

            // 检查每个模板组目录
            var validGroupCount = 0;
            for (int i = 1; i <= _options.Slider.BackgroundImages.Template.GroupCount; i++)
            {
                var groupPath = Path.Combine(_templatePath, i.ToString());
                _logger.Info("检查模板组目录 {Group}: {Path}", i, TaktMaskUtils.MaskPath(groupPath));

                if (!await Task.Run(() => Directory.Exists(groupPath)))
                {
                    _logger.Warn("模板组目录 {Group} 不存在: {Path}", i, TaktMaskUtils.MaskPath(groupPath));
                    continue;
                }

                // 检查模板组中的图片文件
                var holePath = Path.Combine(groupPath, "hole.png");
                var sliderPath = Path.Combine(groupPath, "slider.png");

                if (!await Task.Run(() => File.Exists(holePath)))
                {
                    _logger.Warn("模板组 {Group} 缺少挖空背景图: {Path}", i, TaktMaskUtils.MaskPath(holePath));
                    continue;
                }

                if (!await Task.Run(() => File.Exists(sliderPath)))
                {
                    _logger.Warn("模板组 {Group} 缺少滑块图片: {Path}", i, TaktMaskUtils.MaskPath(sliderPath));
                    continue;
                }

                validGroupCount++;
            }

            var isValid = validGroupCount > 0;
            _logger.Info("模板验证{Result}, 有效模板组数量: {Count}",
                isValid ? "通过" : "失败", validGroupCount);

            if (!isValid)
            {
                throw new InvalidOperationException("验证码模板验证失败");
            }

            _logger.Info("滑块验证码模板验证完成");
        }
        catch (Exception ex)
        {
            _logger.Error("验证滑块验证码模板时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    private async Task InitializeBackgroundImagesAsync()
    {
        try
        {
            // 检查目录中的图片数量
            _logger.Info("开始检查背景图片目录: {Path}", TaktMaskUtils.MaskPath(_backgroundImagesPath));
            var existingFiles = Directory.GetFiles(_backgroundImagesPath, $"*{_options.Slider.BackgroundImages.FileExtension}");
            _logger.Info("当前背景图片数量: {Count}", existingFiles.Length);

            // 如果配置了启动时重新下载，则删除现有图片
            if (_options.Slider.BackgroundImages.RedownloadOnStartup && existingFiles.Length > 0)
            {
                _logger.Info("配置为启动时重新下载图片，正在删除现有图片...");
                foreach (var file in existingFiles)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("删除图片文件时发生错误: {File}", TaktMaskUtils.MaskPath(file), ex.Message);
                    }
                }
                existingFiles = Array.Empty<string>();
                _logger.Info("现有图片已删除");
            }

            // 如果图片数量不足，则下载新图片
            if (existingFiles.Length < _options.Slider.BackgroundImages.MinCount)
            {
                _logger.Info("背景图片数量不足{Count}张，开始下载新图片", _options.Slider.BackgroundImages.MinCount);
                await DownloadBackgroundImagesAsync();
            }
            else
            {
                _logger.Info("背景图片数量已足够，无需下载");
            }
        }
        catch (Exception ex)
        {
            _logger.Error("检查背景图片时发生错误: {Message}", ex.Message);
            throw;
        }
    }

    private async Task DownloadBackgroundImagesAsync()
    {
        // 检查目录中的图片数量
        var existingFiles = Directory.GetFiles(_backgroundImagesPath, $"*{_options.Slider.BackgroundImages.FileExtension}");
        if (existingFiles.Length >= _options.Slider.BackgroundImages.MinCount)
        {
            _logger.Debug("背景图片数量已足够，跳过下载");
            return;
        }

        _logger.Info("开始下载滑块验证码背景图片，当前已有图片数量: {Count}", existingFiles.Length);

        var downloadedCount = 0;
        var neededCount = _options.Slider.BackgroundImages.MinCount - existingFiles.Length;

        for (int i = 0; i < neededCount; i++)
        {
            var fileName = $"{Guid.NewGuid()}{_options.Slider.BackgroundImages.FileExtension}";
            var filePath = Path.Combine(_backgroundImagesPath, fileName);

            try
            {
                // 替换URL中的占位符
                var url = _options.Slider.BackgroundImages.DownloadUrl
                    .Replace("{width}", _options.Slider.Width.ToString())
                    .Replace("{height}", _options.Slider.Height.ToString());

                _logger.Info("开始下载背景图片: {Url}", url);

                // 确保目录存在
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                // 下载图片
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.Error("下载背景图片失败: {StatusCode}", response.StatusCode);
                    continue;
                }

                // 保存图片
                await using var stream = await response.Content.ReadAsStreamAsync();
                await using var fileStream = File.Create(filePath);
                await stream.CopyToAsync(fileStream);

                _logger.Info("背景图片下载成功: {Path}", TaktMaskUtils.MaskPath(filePath));
                downloadedCount++;
            }
            catch (Exception ex)
            {
                _logger.Error("下载背景图片时发生错误: {Message}", ex.Message);
            }
        }

        _logger.Info("背景图片下载完成，成功下载 {Count} 张图片", downloadedCount);
    }
}

