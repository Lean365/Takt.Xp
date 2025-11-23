// -----------------------------------------------------------------------------
// <copyright file="TaktHousingFundBracket.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>HousingFundBracket 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Salary;

/// <summary>
/// 住房公积金等级
/// </summary>
[SugarTable("takt_humanresource_housing_fund_bracket", "住房公积金等级表")]
public class TaktHousingFundBracket : TaktBaseEntity
{
    /// <summary>
    /// 等级名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "等级名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 等级描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "等级描述", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 最低缴存基数
    /// </summary>
    [SugarColumn(ColumnName = "min_base", ColumnDescription = "最低缴存基数", ColumnDataType = "decimal(18,2)", IsNullable = false)]
    public decimal MinBase { get; set; }

    /// <summary>
    /// 最高缴存基数（0表示无上限）
    /// </summary>
    [SugarColumn(ColumnName = "max_base", ColumnDescription = "最高缴存基数（0表示无上限）", ColumnDataType = "decimal(18,2)", IsNullable = false)]
    public decimal MaxBase { get; set; }

    /// <summary>
    /// 个人缴存比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "personal_rate", ColumnDescription = "个人缴存比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal PersonalRate { get; set; }

    /// <summary>
    /// 单位缴存比例（百分比）
    /// </summary>
    [SugarColumn(ColumnName = "company_rate", ColumnDescription = "单位缴存比例（百分比）", ColumnDataType = "decimal(5,2)", IsNullable = false)]
    public decimal CompanyRate { get; set; }

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


