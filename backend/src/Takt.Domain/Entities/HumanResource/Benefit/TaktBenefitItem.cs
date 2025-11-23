// -----------------------------------------------------------------------------
// <copyright file="TaktBenefitItem.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>BenefitItem 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Benefit;

/// <summary>
/// 福利项目实体
/// </summary>
[SugarTable("takt_humanresource_benefit_item", "福利项目表")]
[SugarIndex("ix_benefit_item_code", nameof(ItemCode), OrderByType.Asc, true)]
public class TaktBenefitItem : TaktBaseEntity
{
    /// <summary>
    /// 项目编码
    /// </summary>
    [SugarColumn(ColumnName = "item_code", ColumnDescription = "项目编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 项目名称
    /// </summary>
    [SugarColumn(ColumnName = "item_name", ColumnDescription = "项目名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 项目描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "项目描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }
}



