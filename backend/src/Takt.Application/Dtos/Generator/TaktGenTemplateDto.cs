#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenTemplateDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-24
// 版本号 : V0.0.1
// 描述    : 代码生成模板DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Generator;

/// <summary>
/// 代码生成模板DTO
/// </summary>
public class TaktGenTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTemplateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        FileName = string.Empty;
        CreateBy = string.Empty;
        UpdateBy = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenTemplateId { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; }

    /// <summary>
    /// ORM框架类型
    /// </summary>
    [Required(ErrorMessage = "ORM框架类型不能为空")]
    public int TemplateOrmType { get; set; }

    /// <summary>
    /// 生成模板分类
    /// </summary>
    [Required(ErrorMessage = "生成模板分类不能为空")]
    public int TemplateCodeType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; }

    /// <summary>
    /// 编程语言
    /// </summary>
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; }

    /// <summary>
    /// 模板内容
    /// </summary>
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成配置名称
    /// </summary>
    [Required(ErrorMessage = "文件名称不能为空")]
    [StringLength(100, ErrorMessage = "文件名称长度不能超过100个字符")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 0;

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
/// 代码生成模板查询DTO
/// </summary>
public class TaktGenTemplateQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 模板名称
    /// </summary>
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string? TemplateName { get; set; }

    /// <summary>
    /// ORM框架类型
    /// </summary>
    public int? TemplateOrmType { get; set; }

    /// <summary>
    /// 生成模板分类
    /// </summary>
    public int? TemplateCodeType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>
    public int? TemplateCategory { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// 代码生成模板创建DTO
/// </summary>
public class TaktGenTemplateCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTemplateCreateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        FileName = string.Empty;
    }

    /// <summary>
    /// 模板名称
    /// </summary>
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; }

    /// <summary>
    /// ORM框架类型
    /// </summary>
    [Required(ErrorMessage = "ORM框架类型不能为空")]
    public int TemplateOrmType { get; set; }

    /// <summary>
    /// 生成模板分类
    /// </summary>
    [Required(ErrorMessage = "生成模板分类不能为空")]
    public int TemplateCodeType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; }

    /// <summary>
    /// 编程语言
    /// </summary>
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; }

    /// <summary>
    /// 模板内容
    /// </summary>
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成配置名称
    /// </summary>
    [Required(ErrorMessage = "文件名称不能为空")]
    [StringLength(100, ErrorMessage = "文件名称长度不能超过100个字符")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }
}

/// <summary>
/// 代码生成模板更新DTO
/// </summary>
public class TaktGenTemplateUpdateDto : TaktGenTemplateCreateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    [AdaptMember("Id")]
    public long GenTemplateId { get; set; }
}

/// <summary>
/// 代码生成模板导入DTO
/// </summary>
public class TaktGenTemplateImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTemplateImportDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        FileName = string.Empty;
    }


    /// <summary>
    /// 模板名称
    /// </summary>

    public string TemplateName { get; set; }

    /// <summary>
    /// ORM框架类型
    /// </summary>

    public int TemplateOrmType { get; set; }

    /// <summary>
    /// 生成模板分类
    /// </summary>
    public int TemplateCodeType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>

    public int TemplateCategory { get; set; } 

    /// <summary>
    /// 编程语言
    /// </summary>

    public int TemplateLanguage { get; set; } 

    /// <summary>
    /// 版本号
    /// </summary>

    public int TemplateVersion { get; set; } 

    /// <summary>
    /// 模板内容
    /// </summary>

    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成配置名称
    /// </summary>

    public string FileName { get; set; } 

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>

    public int Status { get; set; }
}

/// <summary>
/// 代码生成模板导出DTO
/// </summary>
public class TaktGenTemplateExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTemplateExportDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        FileName = string.Empty;
    }

    /// <summary>
    /// 主键ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenTemplateId { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>

    public string TemplateName { get; set; }

    /// <summary>
    /// ORM框架类型
    /// </summary>

    public int TemplateOrmType { get; set; }

    /// <summary>
    /// 生成模板分类
    /// </summary>

    public int TemplateCodeType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>

    public int TemplateCategory { get; set; } 

    /// <summary>
    /// 编程语言
    /// </summary>

    public int TemplateLanguage { get; set; } 

    /// <summary>
    /// 版本号
    /// </summary>

    public int TemplateVersion { get; set; } 

    /// <summary>
    /// 模板内容
    /// </summary>

    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成配置名称
    /// </summary>

    public string FileName { get; set; } 

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>

    public int Status { get; set; }
}

/// <summary>
/// 代码生成模板模板DTO
/// </summary>
public class TaktGenTemplateTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenTemplateTemplateDto()
    {
        TemplateName = string.Empty;
        TemplateContent = string.Empty;
        FileName = string.Empty;
    }

    /// <summary>
    /// 模板名称
    /// </summary>

    public string TemplateName { get; set; }

    /// <summary>
    /// ORM框架类型
    /// </summary>

    public int TemplateOrmType { get; set; }

    /// <summary>
    /// 生成模板分类
    /// </summary>

    public int TemplateCodeType { get; set; }

    /// <summary>
    /// 模板分类
    /// </summary>

    public int TemplateCategory { get; set; } 

    /// <summary>
    /// 编程语言
    /// </summary>

    public int TemplateLanguage { get; set; } 

    /// <summary>
    /// 版本号
    /// </summary>

    public int TemplateVersion { get; set; } 

    /// <summary>
    /// 模板内容
    /// </summary>

    public string TemplateContent { get; set; }

    /// <summary>
    /// 生成配置名称
    /// </summary>

    public string FileName { get; set; } 

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>

    public int Status { get; set; }
}

/// <summary>
/// 代码生成模板状态DTO
/// </summary>
public class TaktGenTemplateStatusDto
{
    /// <summary>
    /// 模板ID
    /// </summary>
    [Required(ErrorMessage = "模板ID不能为空")]
    [AdaptMember("Id")]
    public long GenTemplateId { get; set; }

    /// <summary>
    /// 状态（0：停用，1：正常）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public int Status { get; set; }
}



