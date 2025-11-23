#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLanguage.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言实体
//===================================================================

namespace Takt.Domain.Entities.Routine.I18n
{
    /// <summary>
    /// 语言实体
    /// </summary>
    [SugarTable("Takt_routine_i18n_language", "语言信息")]
    [SugarIndex("ix_lang_code", nameof(LangCode), OrderByType.Asc, true)]
    [SugarIndex("ix_lang_name", nameof(LangName), OrderByType.Asc, false)]
    [SugarIndex("ix_is_builtin", nameof(IsBuiltin), OrderByType.Asc, false)]
    [SugarIndex("ix_is_default", nameof(IsDefault), OrderByType.Asc, false)]
    [SugarIndex("ix_status", nameof(I18nStatus), OrderByType.Asc, false)]
    public class TaktLanguage : TaktBaseEntity
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [SugarColumn(ColumnName = "lang_code", ColumnDescription = "语言代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        [SugarColumn(ColumnName = "lang_name", ColumnDescription = "语言名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        [SugarColumn(ColumnName = "lang_icon", ColumnDescription = "语言图标", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LangIcon { get; set; }

        /// <summary>
        /// 语言内置（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_builtin", ColumnDescription = "内置", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsBuiltin { get; set; } = 1;

        /// <summary>
        /// 是否默认语言（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_default", ColumnDescription = "默认", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsDefault { get; set; } = 1;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "i18n_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int I18nStatus { get; set; } = 0;

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 翻译列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktTranslation.LangCode))]
        public List<TaktTranslation>? TranslationList { get; set; }
    }
}



