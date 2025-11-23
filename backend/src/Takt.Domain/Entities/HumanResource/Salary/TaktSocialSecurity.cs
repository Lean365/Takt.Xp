// -----------------------------------------------------------------------------
// <copyright file="TaktSocialSecurity.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>SocialSecurity 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Salary;

/// <summary>
/// 社保实体
/// </summary>
[SugarTable("takt_humanresource_social_security", "社保表")]
[SugarIndex("ix_social_security_employee", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_social_security_month", nameof(SocialMonth), OrderByType.Asc)]
public class TaktSocialSecurity : TaktBaseEntity
{
    /// <summary>
    /// 社保编号
    /// </summary>
    [SugarColumn(ColumnName = "social_no", ColumnDescription = "社保编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string SocialNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工ID
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 社保年月(如202406)
    /// </summary>
    [SugarColumn(ColumnName = "social_month", ColumnDescription = "社保年月", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
    public string SocialMonth { get; set; } = string.Empty;

    /// <summary>
    /// 社保基数
    /// </summary>
    [SugarColumn(ColumnName = "social_base", ColumnDescription = "社保基数", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal SocialBase { get; set; }

    /// <summary>
    /// 养老保险个人缴纳
    /// </summary>
    [SugarColumn(ColumnName = "pension_personal", ColumnDescription = "养老保险个人缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? PensionPersonal { get; set; }

    /// <summary>
    /// 养老保险单位缴纳
    /// </summary>
    [SugarColumn(ColumnName = "pension_company", ColumnDescription = "养老保险单位缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? PensionCompany { get; set; }

    /// <summary>
    /// 医疗保险个人缴纳
    /// </summary>
    [SugarColumn(ColumnName = "medical_personal", ColumnDescription = "医疗保险个人缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? MedicalPersonal { get; set; }

    /// <summary>
    /// 医疗保险单位缴纳
    /// </summary>
    [SugarColumn(ColumnName = "medical_company", ColumnDescription = "医疗保险单位缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? MedicalCompany { get; set; }

    /// <summary>
    /// 失业保险个人缴纳
    /// </summary>
    [SugarColumn(ColumnName = "unemployment_personal", ColumnDescription = "失业保险个人缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? UnemploymentPersonal { get; set; }

    /// <summary>
    /// 失业保险单位缴纳
    /// </summary>
    [SugarColumn(ColumnName = "unemployment_company", ColumnDescription = "失业保险单位缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? UnemploymentCompany { get; set; }

    /// <summary>
    /// 工伤保险单位缴纳
    /// </summary>
    [SugarColumn(ColumnName = "injury_company", ColumnDescription = "工伤保险单位缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? InjuryCompany { get; set; }

    /// <summary>
    /// 生育保险单位缴纳
    /// </summary>
    [SugarColumn(ColumnName = "maternity_company", ColumnDescription = "生育保险单位缴纳", ColumnDataType = "decimal(10,2)", IsNullable = true)]
    public decimal? MaternityCompany { get; set; }

    /// <summary>
    /// 个人缴纳总额
    /// </summary>
    [SugarColumn(ColumnName = "personal_total", ColumnDescription = "个人缴纳总额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal PersonalTotal { get; set; }

    /// <summary>
    /// 单位缴纳总额
    /// </summary>
    [SugarColumn(ColumnName = "company_total", ColumnDescription = "单位缴纳总额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
    public decimal CompanyTotal { get; set; }

    /// <summary>
    /// 缴纳状态(1=未缴纳 2=已缴纳 3=已补缴)
    /// </summary>
    [SugarColumn(ColumnName = "payment_status", ColumnDescription = "缴纳状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int PaymentStatus { get; set; }

    /// <summary>
    /// 缴纳时间
    /// </summary>
    [SugarColumn(ColumnName = "payment_time", ColumnDescription = "缴纳时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? PaymentTime { get; set; }

    /// <summary>
    /// 员工信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
    public virtual Employee.TaktEmployee? Employee { get; set; }
}

