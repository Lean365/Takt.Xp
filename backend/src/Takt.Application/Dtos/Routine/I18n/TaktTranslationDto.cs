//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktTranslationDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译数据传输对象
//===================================================================
namespace Takt.Application.Dtos.Routine.I18n
{
    /// <summary>
    /// 翻译基础DTO
    /// </summary>
    public class TaktTranslationDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTranslationDto()
        {
            LangCode = string.Empty;
            TransKey = string.Empty;
            TransValue = string.Empty;
            TransType = 0;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            DeleteBy = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long TranslationId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别
        /// </summary>
        public int TransType { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 翻译内置（0否 1是）
        /// </summary>
        public int TransBuiltin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 翻译查询DTO
    /// </summary>
    public class TaktTranslationQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 语言代码
        /// </summary>

        public string? LangCode { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>

        public string? TransKey { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>

        public string? TransValue { get; set; }

        /// <summary>
        /// 翻译类别
        /// </summary>

        public int? TransType { get; set; }

    }

    /// <summary>
    /// 翻译创建DTO
    /// </summary>
    public class TaktTranslationCreateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>

        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>

        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>

        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别
        /// </summary>

        public int TransType { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 翻译内置（0否 1是）
        /// </summary>
        public int TransBuiltin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string? Remark { get; set; }
    }

    /// <summary>
    /// 翻译更新DTO
    /// </summary>
    public class TaktTranslationUpdateDto : TaktTranslationCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long TranslationId { get; set; }
    }

    /// <summary>
    /// 翻译导入DTO
    /// </summary>
    public class TaktTranslationImportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别
        /// </summary>
        public int TransType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 翻译导出DTO
    /// </summary>
    public class TaktTranslationExportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别
        /// </summary>
        public int TransType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 翻译模板DTO
    /// </summary>
    public class TaktTranslationTemplateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译值
        /// </summary>
        public string TransValue { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别
        /// </summary>
        public int TransType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 转置后的翻译数据DTO
    /// </summary>
    public class TaktTransposedDto
    {
        /// <summary>
        /// 翻译键
        /// </summary>
        public string TransKey { get; set; } = string.Empty;

        /// <summary>
        /// 翻译类别
        /// </summary>
        public int TransType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string? CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string? UpdateTime { get; set; }

        /// <summary>
        /// 各语言的翻译信息
        /// </summary>
        public Dictionary<string, TaktTranslationLangDto> Translations { get; set; } = new Dictionary<string, TaktTranslationLangDto>();
    }

    /// <summary>
    /// 翻译语言DTO
    /// </summary>
    public class TaktTranslationLangDto
    {
        /// <summary>
        /// 翻译ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long TranslationId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string? LangCode { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>
        public string? TransValue { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

    }

    /// <summary>
    /// 转置查询DTO
    /// </summary>
    public class TaktTransposedQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 翻译键
        /// </summary>

        public string? TransKey { get; set; }

        /// <summary>
        /// 翻译值
        /// </summary>

        public string? TransValue { get; set; }

        /// <summary>
        /// 翻译类别
        /// </summary>
        public int TransType { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }
}


