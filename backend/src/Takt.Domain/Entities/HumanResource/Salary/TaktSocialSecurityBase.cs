// -----------------------------------------------------------------------------
// <copyright file="TaktSocialSecurityBase.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>SocialSecurityBase 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Salary;

/// <summary>
/// 社会保险基数
/// </summary>
[SugarTable("takt_humanresource_social_security_base", "社会保险基数表")]
public class TaktSocialSecurityBase : TaktBaseEntity
{
    /// <summary>
    /// 基数名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "基数名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 基数描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "基数描述", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 最低缴费基数
    /// </summary>
    [SugarColumn(ColumnName = "min_base", ColumnDescription = "最低缴费基数", ColumnDataType = "decimal(18,2)", IsNullable = false)]
    public decimal MinBase { get; set; }

    /// <summary>
    /// 最高缴费基数（0表示无上限）
    /// </summary>
    [SugarColumn(ColumnName = "max_base", ColumnDescription = "最高缴费基数（0表示无上限）", ColumnDataType = "decimal(18,2)", IsNullable = false)]
    public decimal MaxBase { get; set; }

    /// <summary>
    /// 养老保险个人比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "pension_personal_rate", ColumnDescription = "养老保险个人比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal PensionPersonalRate { get; set; }

    /// <summary>
    /// 养老保险单位比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "pension_company_rate", ColumnDescription = "养老保险单位比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal PensionCompanyRate { get; set; }

    /// <summary>
    /// 医疗保险个人比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "medical_personal_rate", ColumnDescription = "医疗保险个人比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal MedicalPersonalRate { get; set; }

    /// <summary>
    /// 医疗保险单位比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "medical_company_rate", ColumnDescription = "医疗保险单位比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal MedicalCompanyRate { get; set; }

    /// <summary>
    /// 失业保险个人比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "unemployment_personal_rate", ColumnDescription = "失业保险个人比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal UnemploymentPersonalRate { get; set; }

    /// <summary>
    /// 失业保险单位比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "unemployment_company_rate", ColumnDescription = "失业保险单位比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal UnemploymentCompanyRate { get; set; }

    /// <summary>
    /// 工伤保险单位比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "injury_company_rate", ColumnDescription = "工伤保险单位比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal InjuryCompanyRate { get; set; }

    /// <summary>
    /// 生育保险单位比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "maternity_company_rate", ColumnDescription = "生育保险单位比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal MaternityCompanyRate { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false)]
    public int SortOrder { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "bit", IsNullable = false, DefaultValue = "1")]
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期（null表示永久有效）
    /// </summary>
    [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ExpiryDate { get; set; }


}


