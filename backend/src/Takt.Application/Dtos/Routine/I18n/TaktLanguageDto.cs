//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktLanguageDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言数据传输对象
//===================================================================
namespace Takt.Application.Dtos.Routine.I18n
{
    /// <summary>
    /// 语言基础DTO
    /// </summary>
    public class TaktLanguageDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLanguageDto()
        {
            LangCode = string.Empty;
            LangName = string.Empty;
            LangIcon = string.Empty;
            TranslationList = new List<TaktTranslationDto>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long LanguageId { get; set; }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int I18nStatus { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }



        /// <summary>
        /// 语言内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; }

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

        /// <summary>
        /// 翻译列表
        /// </summary>
        public List<TaktTranslationDto>? TranslationList { get; set; }
    }

    /// <summary>
    /// 语言查询DTO
    /// </summary>
    public class TaktLanguageQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 语言代码
        /// </summary>

        public string? LangCode { get; set; }

        /// <summary>
        /// 语言名称
        /// </summary>

        public string? LangName { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int? IsDefault { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int? I18nStatus { get; set; }

        /// <summary>
        /// 语言内置（0否 1是）
        /// </summary>
        public int? IsBuiltin { get; set; }
    }

    /// <summary>
    /// 语言创建DTO
    /// </summary>
    public class TaktLanguageCreateDto
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLanguageCreateDto()
        {
            LangCode = string.Empty;
            LangName = string.Empty;
            LangIcon = string.Empty;
            TranslationList = new List<TaktTranslationCreateDto>();
        }
        /// <summary>
        /// 语言代码
        /// </summary>

        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>

        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>

        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>

        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int I18nStatus { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }


        /// <summary>
        /// 语言内置（0否 1是）
        /// </summary>
        public int IsBuiltin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string? Remark { get; set; }

        /// <summary>
        /// 翻译列表
        /// </summary>
        public List<TaktTranslationCreateDto>? TranslationList { get; set; }
    }

    /// <summary>
    /// 语言更新DTO
    /// </summary>
    public class TaktLanguageUpdateDto : TaktLanguageCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long LanguageId { get; set; }
    }

    /// <summary>
    /// 语言导入DTO
    /// </summary>
    public class TaktLanguageImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLanguageImportDto()
        {
            LangCode = string.Empty;
            LangName = string.Empty;
            TranslationList = new List<TaktTranslationImportDto>();
        }

        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int I18nStatus { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 翻译列表
        /// </summary>
        public List<TaktTranslationImportDto>? TranslationList { get; set; }
    }

    /// <summary>
    /// 语言导出DTO
    /// </summary>
    public class TaktLanguageExportDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int I18nStatus { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 语言模板DTO
    /// </summary>
    public class TaktLanguageTemplateDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标
        /// </summary>
        public string? LangIcon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int I18nStatus { get; set; }

        /// <summary>
        /// 是否默认语言（0否 1是）
        /// </summary>
        public int IsDefault { get; set; }
    }

    /// <summary>
    /// 语言状态DTO
    /// </summary>
    public class TaktLanguageStatusDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long LanguageId { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>

        public int I18nStatus { get; set; }
    }

    /// <summary>
    /// 语言选项DTO
    /// </summary>
    public class TaktLanguageOptionDto
    {
        /// <summary>
        /// 语言代码
        /// </summary>
        public string LangCode { get; set; } = string.Empty;

        /// <summary>
        /// 语言名称
        /// </summary>
        public string LangName { get; set; } = string.Empty;

        /// <summary>
        /// 语言图标    
        /// </summary>
        public string LangIcon { get; set; } = string.Empty;
    }
}


