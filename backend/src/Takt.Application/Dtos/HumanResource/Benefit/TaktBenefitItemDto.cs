// -----------------------------------------------------------------------------
// <copyright file="TaktBenefitItemDto.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>BenefitItem DTO</description>
// -----------------------------------------------------------------------------

#nullable enable

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.HumanResource.Benefit;

/// <summary>
/// 福利项目基础 DTO
/// </summary>
public class TaktBenefitItemDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目编码
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public long CreateUserId { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    public long? UpdateUserId { get; set; }
}

/// <summary>
/// 福利项目查询 DTO
/// </summary>
public class TaktBenefitItemQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 项目编码
    /// </summary>
    public string? ItemCode { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string? ItemName { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    public int? Status { get; set; }
}

/// <summary>
/// 福利项目创建 DTO
/// </summary>
public class TaktBenefitItemCreateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [Required(ErrorMessage = "公司代码不能为空")]
    [StringLength(10, ErrorMessage = "公司代码长度不能超过10个字符")]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目编码
    /// </summary>
    [Required(ErrorMessage = "项目编码不能为空")]
    [StringLength(20, ErrorMessage = "项目编码长度不能超过20个字符")]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    [Required(ErrorMessage = "项目名称不能为空")]
    [StringLength(100, ErrorMessage = "项目名称长度不能超过100个字符")]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    [StringLength(500, ErrorMessage = "项目描述长度不能超过500个字符")]
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(1, 2, ErrorMessage = "状态值必须在1-2之间")]
    public int Status { get; set; }
}

/// <summary>
/// 福利项目更新 DTO
/// </summary>
public class TaktBenefitItemUpdateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 项目编码
    /// </summary>
    [Required(ErrorMessage = "项目编码不能为空")]
    [StringLength(20, ErrorMessage = "项目编码长度不能超过20个字符")]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    [Required(ErrorMessage = "项目名称不能为空")]
    [StringLength(100, ErrorMessage = "项目名称长度不能超过100个字符")]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    [StringLength(500, ErrorMessage = "项目描述长度不能超过500个字符")]
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(1, 2, ErrorMessage = "状态值必须在1-2之间")]
    public int Status { get; set; }
}

/// <summary>
/// 福利项目删除 DTO
/// </summary>
public class TaktBenefitItemDeleteDto
{
    /// <summary>
    /// 主键ID列表
    /// </summary>
    [Required(ErrorMessage = "主键ID列表不能为空")]
    public List<long> Ids { get; set; } = new();
}

/// <summary>
/// 福利项目导入 DTO
/// </summary>
public class TaktBenefitItemImportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [Required(ErrorMessage = "公司代码不能为空")]
    [StringLength(10, ErrorMessage = "公司代码长度不能超过10个字符")]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目编码
    /// </summary>
    [Required(ErrorMessage = "项目编码不能为空")]
    [StringLength(20, ErrorMessage = "项目编码长度不能超过20个字符")]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    [Required(ErrorMessage = "项目名称不能为空")]
    [StringLength(100, ErrorMessage = "项目名称长度不能超过100个字符")]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    [StringLength(500, ErrorMessage = "项目描述长度不能超过500个字符")]
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(1, 2, ErrorMessage = "状态值必须在1-2之间")]
    public int Status { get; set; }
}

/// <summary>
/// 福利项目导出 DTO
/// </summary>
public class TaktBenefitItemExportDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目编码
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// 福利项目模板 DTO
/// </summary>
public class TaktBenefitItemTemplateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目编码
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    public int Status { get; set; }
} 

