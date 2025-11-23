// -----------------------------------------------------------------------------
// <copyright file="TaktPosition.cs" company="YourCompanyName">
//     Copyright (c) YourCompanyName. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Position 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// 职位实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-23
/// 功能说明:
/// 1. 存储职位基本信息
/// 2. 支持职位层级结构
/// 3. 关联职位职责和要求
/// </remarks>
[SugarTable("takt_humanresource_position", "职位表")]
[SugarIndex("ix_position_pos_code", nameof(PosCode), OrderByType.Asc, true)]
[SugarIndex("ix_position_category", nameof(CategoryId), OrderByType.Asc)]
[SugarIndex("ix_position_status", nameof(Status), OrderByType.Asc)]
public class TaktPosition : TaktBaseEntity
{
    /// <summary>
    /// 职位编码
    /// </summary>
    [SugarColumn(ColumnName = "pos_code", ColumnDescription = "职位编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string PosCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位名称
    /// </summary>
    [SugarColumn(ColumnName = "pos_name", ColumnDescription = "职位名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string PosName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 职位类别ID
    /// </summary>
    [SugarColumn(ColumnName = "category_id", ColumnDescription = "职位类别ID", ColumnDataType = "bigint", IsNullable = false)]
    public long CategoryId { get; set; }

    /// <summary>
    /// 职位级别(1=初级 2=中级 3=高级 4=专家 5=资深专家)
    /// </summary>
    [SugarColumn(ColumnName = "pos_level", ColumnDescription = "职位级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PosLevel { get; set; }

    /// <summary>
    /// 职位序列(1=管理序列 2=技术序列 3=专业序列 4=操作序列)
    /// </summary>
    [SugarColumn(ColumnName = "pos_sequence", ColumnDescription = "职位序列", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int PosSequence { get; set; }

    /// <summary>
    /// 职位描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "职位描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 职位职责
    /// </summary>
    [SugarColumn(ColumnName = "responsibilities", ColumnDescription = "职位职责", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? Responsibilities { get; set; }

    /// <summary>
    /// 任职要求
    /// </summary>
    [SugarColumn(ColumnName = "requirements", ColumnDescription = "任职要求", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? Requirements { get; set; }

    /// <summary>
    /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    [SugarColumn(ColumnName = "min_education", ColumnDescription = "最低学历要求", ColumnDataType = "int", IsNullable = true)]
    public int? MinEducation { get; set; }

    /// <summary>
    /// 最低工作经验(年)
    /// </summary>
    [SugarColumn(ColumnName = "min_experience", ColumnDescription = "最低工作经验", ColumnDataType = "int", IsNullable = true)]
    public int? MinExperience { get; set; }

    /// <summary>
    /// 薪资范围下限
    /// </summary>
    [SugarColumn(ColumnName = "salary_min", ColumnDescription = "薪资范围下限", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? SalaryMin { get; set; }

    /// <summary>
    /// 薪资范围上限
    /// </summary>
    [SugarColumn(ColumnName = "salary_max", ColumnDescription = "薪资范围上限", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? SalaryMax { get; set; }

    /// <summary>
    /// 职位人数
    /// </summary>
    [SugarColumn(ColumnName = "employee_count", ColumnDescription = "职位人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EmployeeCount { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; }
    /// <summary>
    /// 职位状态(0=正常 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "职位状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }


    /// <summary>
    /// 职位员工
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Employee.TaktEmployee.PositionId))]
    public virtual List<Employee.TaktEmployee>? Employees { get; set; }
}




