//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedLanguage.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 语言数据种子数据
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 语言数据种子数据
/// </summary>
public class TaktDbSeedLanguage
{
    /// <summary>
    /// 获取默认语言数据
    /// </summary>
    /// <returns>语言数据列表</returns>
    public List<TaktLanguage> GetDefaultLanguages()
    {
        return new List<TaktLanguage>
        {
            new TaktLanguage { LangCode = "ar-SA", LangName = "العربية", LangIcon = "🇸🇦", OrderNum = 1, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "en-US", LangName = "English", LangIcon = "🇺🇸", OrderNum = 2, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "fr-FR", LangName = "Français", LangIcon = "🇫🇷", OrderNum = 3, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "ja-JP", LangName = "日本語", LangIcon = "🇯🇵", OrderNum = 4, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "ko-KR", LangName = "한국어", LangIcon = "🇰🇷", OrderNum = 5, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "ru-RU", LangName = "Русский", LangIcon = "🇷🇺", OrderNum = 6, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "es-ES", LangName = "Español", LangIcon = "🇪🇸", OrderNum = 7, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "zh-CN", LangName = "简体中文", LangIcon = "🇨🇳", OrderNum = 8, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 },
            new TaktLanguage { LangCode = "zh-TW", LangName = "繁體中文", LangIcon = "🇹🇼", OrderNum = 9, I18nStatus=0, IsDefault = 0, IsBuiltin = 1 }
        };
    }
}

