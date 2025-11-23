// -----------------------------------------------------------------------------
// <copyright file="TaktEmployeeBenefitDto.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>EmployeeBenefit DTO</description>
// -----------------------------------------------------------------------------

#nullable enable

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.HumanResource.Benefit;

/// <summary>
/// 员工福利基础 DTO
/// </summary>
public class TaktEmployeeBenefitDto
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
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public decimal? Amount { get; set; }

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

    /// <summary>
    /// 员工信息
    /// </summary>
    public Employee.TaktEmployeeDto? Employee { get; set; }

    /// <summary>
    /// 福利项目
    /// </summary>
    public TaktBenefitItemDto? BenefitItem { get; set; }
}

/// <summary>
/// 员工福利查询 DTO
/// </summary>
public class TaktEmployeeBenefitQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 员工ID
    /// </summary>
    public long? EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    public long? BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期开始
    /// </summary>
    public DateTime? GrantDateStart { get; set; }

    /// <summary>
    /// 发放日期结束
    /// </summary>
    public DateTime? GrantDateEnd { get; set; }
}

/// <summary>
/// 员工福利创建 DTO
/// </summary>
public class TaktEmployeeBenefitCreateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [Required(ErrorMessage = "公司代码不能为空")]
    [StringLength(10, ErrorMessage = "公司代码长度不能超过10个字符")]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [Required(ErrorMessage = "员工ID不能为空")]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    [Required(ErrorMessage = "福利项目ID不能为空")]
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "金额必须大于等于0")]
    public decimal? Amount { get; set; }
}

/// <summary>
/// 员工福利更新 DTO
/// </summary>
public class TaktEmployeeBenefitUpdateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    [Required(ErrorMessage = "员工ID不能为空")]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    [Required(ErrorMessage = "福利项目ID不能为空")]
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "金额必须大于等于0")]
    public decimal? Amount { get; set; }
}

/// <summary>
/// 员工福利删除 DTO
/// </summary>
public class TaktEmployeeBenefitDeleteDto
{
    /// <summary>
    /// 主键ID列表
    /// </summary>
    [Required(ErrorMessage = "主键ID列表不能为空")]
    public List<long> Ids { get; set; } = new();
}

/// <summary>
/// 员工福利导入 DTO
/// </summary>
public class TaktEmployeeBenefitImportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [Required(ErrorMessage = "公司代码不能为空")]
    [StringLength(10, ErrorMessage = "公司代码长度不能超过10个字符")]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [Required(ErrorMessage = "员工ID不能为空")]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    [Required(ErrorMessage = "福利项目ID不能为空")]
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "金额必须大于等于0")]
    public decimal? Amount { get; set; }
}

/// <summary>
/// 员工福利导出 DTO
/// </summary>
public class TaktEmployeeBenefitExportDto
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
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public decimal? Amount { get; set; }

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
/// 员工福利模板 DTO
/// </summary>
public class TaktEmployeeBenefitTemplateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    /// 福利项目ID
    /// </summary>
    public long BenefitItemId { get; set; }

    /// <summary>
    /// 发放日期
    /// </summary>
    public DateTime? GrantDate { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public decimal? Amount { get; set; }
} 

