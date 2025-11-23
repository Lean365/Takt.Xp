//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLocalizationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 本地化服务实现
//===================================================================

using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

namespace Takt.Infrastructure.Services.Local;

/// <summary>
/// 本地化服务实现
/// </summary>
public class TaktLocalizationService : ITaktLocalizationService
{
    /// <summary>
    /// 日志服务
    /// </summary>
    protected readonly ITaktLogger _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITaktTranslationCache _translationCache;
    private readonly IDistributedCache _distributedCache;
    private const string DefaultLanguage = "zh-CN";
    private const string LanguageHeader = "Accept-Language";
    private const string LanguageContextKey = "CurrentLanguage";
    private const int CacheExpirationMinutes = 30;

    private static readonly ConcurrentDictionary<string, CultureInfo> _cultureCache
        = new ConcurrentDictionary<string, CultureInfo>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktLocalizationService(
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktTranslationCache translationCache,

        IDistributedCache distributedCache)
    {
        _httpContextAccessor = httpContextAccessor;
        _translationCache = translationCache;
        _logger = logger;
        _distributedCache = distributedCache;
    }

    /// <summary>
    /// 获取当前语言的翻译值
    /// </summary>
    public string L(string key, params object[] args)
    {
        try
        {
            return L(CurrentLanguage, key, args);
        }
        catch (Exception ex)
        {
            _logger.Error("获取翻译失败: {Key}", key, ex.Message);
            return key;
        }
    }

    /// <summary>
    /// 获取指定语言的翻译值
    /// </summary>
    public string L(string langCode, string key, params object[] args)
    {
        if (string.IsNullOrEmpty(key))
            return string.Empty;

        try
        {
            // 1. 尝试从分布式缓存获取(根据配置可能是Redis或Memory)
            try
            {
                var cacheKey = $"Trans:{langCode}:{key}";
                var cachedBytes = _distributedCache.Get(cacheKey);
                if (cachedBytes != null)
                {
                    var cachedTranslation = System.Text.Encoding.UTF8.GetString(cachedBytes);
                    return args.Length > 0 ? string.Format(cachedTranslation, args) : cachedTranslation;
                }
            }
            catch (Exception ex)
            {
                // Redis连接失败时记录警告并继续
                _logger.Warn("从分布式缓存获取翻译失败，将从翻译服务获取: {LangCode}, {Key}", langCode, key, ex.Message);
            }

            // 2. 从翻译服务获取
            var translation = _translationCache.GetTranslation(langCode, key);
            if (translation != null)
            {
                try
                {
                    // 尝试保存到分布式缓存
                    var options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CacheExpirationMinutes)
                    };
                    _distributedCache.Set(
                        $"Trans:{langCode}:{key}",
                        System.Text.Encoding.UTF8.GetBytes(translation),
                        options);
                }
                catch (Exception ex)
                {
                    // Redis连接失败时只记录警告
                    _logger.Warn("保存翻译到分布式缓存失败: {LangCode}, {Key}", langCode, key, ex.Message);
                }
                return args.Length > 0 ? string.Format(translation, args) : translation;
            }

            // 3. 如果找不到,尝试使用默认语言
            if (langCode != DefaultLanguage)
            {
                return L(DefaultLanguage, key, args);
            }

            // 4. 如果还是找不到,返回键名
            return key;
        }
        catch (Exception ex)
        {
            _logger.Error("获取翻译失败: {LangCode}, {Key}", langCode, key, ex.Message);
            return key;
        }
    }

    /// <summary>
    /// 获取当前语言代码
    /// </summary>
    public string CurrentLanguage
    {
        get
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return DefaultLanguage;

            // 1. 从HttpContext.Items中获取（由中间件设置）
            if (context.Items.TryGetValue(LanguageContextKey, out var cachedLang))
            {
                return cachedLang?.ToString() ?? DefaultLanguage;
            }

            // 2. 从请求头获取
            var langHeader = context.Request.Headers[LanguageHeader].ToString();
            if (!string.IsNullOrEmpty(langHeader))
            {
                var lang = langHeader.Split(',')[0];
                context.Items[LanguageContextKey] = lang;
                return lang;
            }

            // 3. 从Cookie获取
            if (context.Request.Cookies.TryGetValue("lang", out var langCookie))
            {
                context.Items[LanguageContextKey] = langCookie;
                return langCookie;
            }

            // 4. 使用默认语言
            context.Items[LanguageContextKey] = DefaultLanguage;
            return DefaultLanguage;
        }
    }

    /// <summary>
    /// 设置当前语言
    /// </summary>
    public void SetLanguage(string langCode)
    {
        if (string.IsNullOrEmpty(langCode))
            throw new ArgumentNullException(nameof(langCode));

        var context = _httpContextAccessor.HttpContext;
        if (context == null)
            return;

        try
        {
            // 1. 更新HttpContext.Items
            context.Items[LanguageContextKey] = langCode;

            // 2. 设置当前线程的文化信息
            var culture = _cultureCache.GetOrAdd(langCode, code => new CultureInfo(code));
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            _logger.Info("语言设置成功: {LangCode}", langCode);
        }
        catch (Exception ex)
        {
            _logger.Error("设置语言失败: {LangCode}", langCode, ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 重新加载翻译
    /// </summary>
    public async Task ReloadTranslationsAsync()
    {
        try
        {
            await _translationCache.ReloadAsync();
            _logger.Info("翻译缓存重新加载成功");
        }
        catch (Exception ex)
        {
            _logger.Error("重新加载翻译失败", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    public Task<string> GetLocalizedStringAsync(string key, params object[] args)
    {
        return Task.FromResult(L(key, args));
    }

    /// <summary>
    /// 获取当前文化信息
    /// </summary>
    public CultureInfo GetCurrentCulture()
    {
        return _cultureCache.GetOrAdd(CurrentLanguage, code => new CultureInfo(code));
    }

    /// <summary>
    /// 设置当前文化信息
    /// </summary>
    public void SetCurrentCulture(CultureInfo culture)
    {
        if (culture == null)
            throw new ArgumentNullException(nameof(culture));

        SetLanguage(culture.Name);
    }

    /// <summary>
    /// 刷新本地化缓存
    /// </summary>
    public async Task RefreshLocalizationCacheAsync()
    {
        try
        {
            await _translationCache.ReloadAsync();
            _cultureCache.Clear();
            _logger.Info("本地化缓存刷新成功");
        }
        catch (Exception ex)
        {
            _logger.Error("刷新本地化缓存失败", ex.Message);
            throw;
        }
    }
}



