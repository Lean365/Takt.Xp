#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTranslation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译实体
//===================================================================
namespace Takt.Domain.Entities.Routine.I18n
{
    /// <summary>
    /// 翻译实体
    /// </summary>
    [SugarTable("Takt_routine_i18n_translation", "翻译信息")]
    [SugarIndex("ix_lang_code", nameof(LangCode), OrderByType.Asc, false)]
    [SugarIndex("ix_trans_key", nameof(TransKey), OrderByType.Asc, false)]
    public class TaktTranslation : TaktBaseEntity
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        [SugarColumn(ColumnName = "lang_code", ColumnDescription = "语言代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        [SugarColumn(ColumnName = "trans_value", ColumnDescription = "翻译值", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别（0是前端，1是后端）
        /// </summary>
        [SugarColumn(ColumnName = "trans_type", ColumnDescription = "翻译类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TransType { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 语言实体
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(LangCode))]
        public TaktLanguage? LanguageEntity { get; set; }
    }
}



