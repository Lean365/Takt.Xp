// -----------------------------------------------------------------------------
// <copyright file="TaktHoliday.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Holiday 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Attendance;

/// <summary>
/// 假期实体
/// </summary>
[SugarTable("takt_humanresource_holiday", "假期表")]
[SugarIndex("ix_holiday_year", nameof(Year), OrderByType.Asc)]
[SugarIndex("ix_holiday_date", nameof(HolidayDate), OrderByType.Asc)]
public class TaktHoliday : TaktBaseEntity
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;    
    /// <summary>
    /// 假期编号
    /// </summary>
    [SugarColumn(ColumnName = "holiday_no", ColumnDescription = "假期编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string HolidayNo { get; set; } = string.Empty;

    /// <summary>
    /// 年份
    /// </summary>
    [SugarColumn(ColumnName = "year", ColumnDescription = "年份", ColumnDataType = "int", IsNullable = false)]
    public int Year { get; set; }

    /// <summary>
    /// 假期名称
    /// </summary>
    [SugarColumn(ColumnName = "holiday_name", ColumnDescription = "假期名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string HolidayName { get; set; } = string.Empty;

    /// <summary>
    /// 假期日期
    /// </summary>
    [SugarColumn(ColumnName = "holiday_date", ColumnDescription = "假期日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime HolidayDate { get; set; }

    /// <summary>
    /// 假期类型(1=法定节假日 2=调休放假 3=公司放假 4=其他)
    /// </summary>
    [SugarColumn(ColumnName = "holiday_type", ColumnDescription = "假期类型", ColumnDataType = "int", IsNullable = false)]
    public int HolidayType { get; set; }

    /// <summary>
    /// 是否调休(1=是 2=否)
    /// </summary>
    [SugarColumn(ColumnName = "is_adjustment", ColumnDescription = "是否调休", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int IsAdjustment { get; set; }

    /// <summary>
    /// 调休上班日期
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_work_date", ColumnDescription = "调休上班日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? AdjustmentWorkDate { get; set; }

    /// <summary>
    /// 调休说明
    /// </summary>
    [SugarColumn(ColumnName = "adjustment_description", ColumnDescription = "调休说明", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? AdjustmentDescription { get; set; }

    /// <summary>
    /// 假期描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "假期描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 状态(1=启用 2=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }
}



