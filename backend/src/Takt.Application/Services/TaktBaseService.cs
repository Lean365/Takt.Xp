#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBaseService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 基础服务实现
//===================================================================

using Takt.Domain.IServices;
using Takt.Domain.IServices.Extensions;
using Takt.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Takt.Application.Services;

/// <summary>
/// 基础服务实现
/// </summary>
public class TaktBaseService
{
    /// <summary>
    /// HTTP上下文访问器
    /// </summary>
    protected readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly ITaktLogger _logger;
    /// <summary>
    /// 当前用户服务
    /// </summary>
    protected readonly ITaktCurrentUser _currentUser;
    /// <summary>
    /// 本地化服务
    /// </summary>
    protected readonly ITaktLocalizationService _localization;


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>

    /// <param name="localization">本地化服务</param>
    public TaktBaseService(
        ITaktLogger logger, 
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,

        ITaktLocalizationService localization)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        ArgumentNullException.ThrowIfNull(currentUser);

        ArgumentNullException.ThrowIfNull(localization);

        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _currentUser = currentUser;

        _localization = localization;
    }



    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    protected string L(string key, params object[] args)
    {
        return _localization.L(key, args);
    }

    /// <summary>
    /// 获取本地化字符串
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="culture">文化</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    protected string L(string key, string culture, params object[] args)
    {
        return _localization.L(key, culture, args);
    }

    /// <summary>
    /// 获取本地化字符串（异步）
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="args">参数</param>
    /// <returns>本地化字符串</returns>
    protected Task<string> GetLocalizedStringAsync(string key, params object[] args)
    {
        return _localization.GetLocalizedStringAsync(key, args);
    }

    /// <summary>
    /// 获取当前文化
    /// </summary>
    /// <returns>当前文化</returns>
    protected CultureInfo GetCurrentCulture()
    {
        return _localization.GetCurrentCulture();
    }

    /// <summary>
    /// 设置当前文化
    /// </summary>
    /// <param name="culture">文化</param>
    protected void SetCurrentCulture(CultureInfo culture)
    {
        _localization.SetCurrentCulture(culture);
    }

    /// <summary>
    /// 设置语言
    /// </summary>
    /// <param name="language">语言</param>
    protected void SetLanguage(string language)
    {
        _localization.SetLanguage(language);
    }

    /// <summary>
    /// 刷新本地化缓存
    /// </summary>
    protected Task RefreshLocalizationCacheAsync()
    {
        return _localization.RefreshLocalizationCacheAsync();
    }
}



