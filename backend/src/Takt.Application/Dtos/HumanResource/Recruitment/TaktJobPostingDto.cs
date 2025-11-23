// -----------------------------------------------------------------------------
// <copyright file="TaktJobPostingDto.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>JobPosting DTO</description>
// -----------------------------------------------------------------------------

#nullable enable

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.HumanResource.Recruitment;

/// <summary>
/// 职位发布基础 DTO
/// </summary>
public class TaktJobPostingDto
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
    /// 发布编号
    /// </summary>
    public string PostingCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位标题
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 职位ID
    /// </summary>
    public long PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long DepartmentId { get; set; }

    /// <summary>
    /// 招聘人数
    /// </summary>
    public int Headcount { get; set; }

    /// <summary>
    /// 已招聘人数
    /// </summary>
    public int HiredCount { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    public string? JobDescription { get; set; }

    /// <summary>
    /// 职位要求
    /// </summary>
    public string? JobRequirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    public int? MinExperience { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }

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
    /// 职位信息
    /// </summary>
    public Organization.TaktPositionDto? Position { get; set; }

    /// <summary>
    /// 部门信息
    /// </summary>
    public Organization.TaktDepartmentDto? Department { get; set; }

    /// <summary>
    /// 招聘负责人
    /// </summary>
    public Employee.TaktEmployeeDto? Recruiter { get; set; }
}

/// <summary>
/// 职位发布查询 DTO
/// </summary>
public class TaktJobPostingQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 发布编号
    /// </summary>
    public string? PostingCode { get; set; }

    /// <summary>
    /// 职位标题
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// 职位ID
    /// </summary>
    public long? PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DepartmentId { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }
}

/// <summary>
/// 职位发布创建 DTO
/// </summary>
public class TaktJobPostingCreateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [Required(ErrorMessage = "公司代码不能为空")]
    [StringLength(10, ErrorMessage = "公司代码长度不能超过10个字符")]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 发布编号
    /// </summary>
    [Required(ErrorMessage = "发布编号不能为空")]
    [StringLength(20, ErrorMessage = "发布编号长度不能超过20个字符")]
    public string PostingCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位标题
    /// </summary>
    [Required(ErrorMessage = "职位标题不能为空")]
    [StringLength(200, ErrorMessage = "职位标题长度不能超过200个字符")]
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 职位ID
    /// </summary>
    [Required(ErrorMessage = "职位ID不能为空")]
    public long PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [Required(ErrorMessage = "部门ID不能为空")]
    public long DepartmentId { get; set; }

    /// <summary>
    /// 招聘人数
    /// </summary>
    [Required(ErrorMessage = "招聘人数不能为空")]
    [Range(1, int.MaxValue, ErrorMessage = "招聘人数必须大于0")]
    public int Headcount { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    [StringLength(100, ErrorMessage = "工作地点长度不能超过100个字符")]
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "薪资范围下限必须大于等于0")]
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "薪资范围上限必须大于等于0")]
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    [StringLength(2000, ErrorMessage = "职位描述长度不能超过2000个字符")]
    public string? JobDescription { get; set; }

    /// <summary>
    /// 职位要求
    /// </summary>
    [StringLength(2000, ErrorMessage = "职位要求长度不能超过2000个字符")]
    public string? JobRequirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    [Range(1, 5, ErrorMessage = "最低学历要求值必须在1-5之间")]
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "最低工作经验必须大于等于0")]
    public int? MinExperience { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    [Required(ErrorMessage = "发布状态不能为空")]
    [Range(1, 4, ErrorMessage = "发布状态值必须在1-4之间")]
    public int Status { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }
}

/// <summary>
/// 职位发布更新 DTO
/// </summary>
public class TaktJobPostingUpdateDto
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 发布编号
    /// </summary>
    [Required(ErrorMessage = "发布编号不能为空")]
    [StringLength(20, ErrorMessage = "发布编号长度不能超过20个字符")]
    public string PostingCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位标题
    /// </summary>
    [Required(ErrorMessage = "职位标题不能为空")]
    [StringLength(200, ErrorMessage = "职位标题长度不能超过200个字符")]
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 职位ID
    /// </summary>
    [Required(ErrorMessage = "职位ID不能为空")]
    public long PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [Required(ErrorMessage = "部门ID不能为空")]
    public long DepartmentId { get; set; }

    /// <summary>
    /// 招聘人数
    /// </summary>
    [Required(ErrorMessage = "招聘人数不能为空")]
    [Range(1, int.MaxValue, ErrorMessage = "招聘人数必须大于0")]
    public int Headcount { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    [StringLength(100, ErrorMessage = "工作地点长度不能超过100个字符")]
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "薪资范围下限必须大于等于0")]
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "薪资范围上限必须大于等于0")]
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    [StringLength(2000, ErrorMessage = "职位描述长度不能超过2000个字符")]
    public string? JobDescription { get; set; }

    /// <summary>
    /// 职位要求
    /// </summary>
    [StringLength(2000, ErrorMessage = "职位要求长度不能超过2000个字符")]
    public string? JobRequirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    [Range(1, 5, ErrorMessage = "最低学历要求值必须在1-5之间")]
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "最低工作经验必须大于等于0")]
    public int? MinExperience { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    [Required(ErrorMessage = "发布状态不能为空")]
    [Range(1, 4, ErrorMessage = "发布状态值必须在1-4之间")]
    public int Status { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }
}

/// <summary>
/// 职位发布删除 DTO
/// </summary>
public class TaktJobPostingDeleteDto
{
    /// <summary>
    /// 主键ID列表
    /// </summary>
    [Required(ErrorMessage = "主键ID列表不能为空")]
    public List<long> Ids { get; set; } = new();
}

/// <summary>
/// 职位发布导入 DTO
/// </summary>
public class TaktJobPostingImportDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [Required(ErrorMessage = "公司代码不能为空")]
    [StringLength(10, ErrorMessage = "公司代码长度不能超过10个字符")]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 发布编号
    /// </summary>
    [Required(ErrorMessage = "发布编号不能为空")]
    [StringLength(20, ErrorMessage = "发布编号长度不能超过20个字符")]
    public string PostingCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位标题
    /// </summary>
    [Required(ErrorMessage = "职位标题不能为空")]
    [StringLength(200, ErrorMessage = "职位标题长度不能超过200个字符")]
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 职位ID
    /// </summary>
    [Required(ErrorMessage = "职位ID不能为空")]
    public long PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [Required(ErrorMessage = "部门ID不能为空")]
    public long DepartmentId { get; set; }

    /// <summary>
    /// 招聘人数
    /// </summary>
    [Required(ErrorMessage = "招聘人数不能为空")]
    [Range(1, int.MaxValue, ErrorMessage = "招聘人数必须大于0")]
    public int Headcount { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    [StringLength(100, ErrorMessage = "工作地点长度不能超过100个字符")]
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "薪资范围下限必须大于等于0")]
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "薪资范围上限必须大于等于0")]
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    [StringLength(2000, ErrorMessage = "职位描述长度不能超过2000个字符")]
    public string? JobDescription { get; set; }

    /// <summary>
    /// 职位要求
    /// </summary>
    [StringLength(2000, ErrorMessage = "职位要求长度不能超过2000个字符")]
    public string? JobRequirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    [Range(1, 5, ErrorMessage = "最低学历要求值必须在1-5之间")]
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "最低工作经验必须大于等于0")]
    public int? MinExperience { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    [Required(ErrorMessage = "发布状态不能为空")]
    [Range(1, 4, ErrorMessage = "发布状态值必须在1-4之间")]
    public int Status { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }
}

/// <summary>
/// 职位发布导出 DTO
/// </summary>
public class TaktJobPostingExportDto
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
    /// 发布编号
    /// </summary>
    public string PostingCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位标题
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 职位ID
    /// </summary>
    public long PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long DepartmentId { get; set; }

    /// <summary>
    /// 招聘人数
    /// </summary>
    public int Headcount { get; set; }

    /// <summary>
    /// 已招聘人数
    /// </summary>
    public int HiredCount { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    public string? JobDescription { get; set; }

    /// <summary>
    /// 职位要求
    /// </summary>
    public string? JobRequirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    public int? MinExperience { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }

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
/// 职位发布模板 DTO
/// </summary>
public class TaktJobPostingTemplateDto
{
    /// <summary>
    /// 公司代码
    /// </summary>
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 发布编号
    /// </summary>
    public string PostingCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位标题
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// 职位ID
    /// </summary>
    public long PositionId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long DepartmentId { get; set; }

    /// <summary>
    /// 招聘人数
    /// </summary>
    public int Headcount { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    public string? JobDescription { get; set; }

    /// <summary>
    /// 职位要求
    /// </summary>
    public string? JobRequirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    public int? MinExperience { get; set; }

    /// <summary>
    /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime? PublishTime { get; set; }

    /// <summary>
    /// 截止时间
    /// </summary>
    public DateTime? Deadline { get; set; }

    /// <summary>
    /// 招聘负责人ID
    /// </summary>
    public long? RecruiterId { get; set; }
} 

