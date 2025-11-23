// -----------------------------------------------------------------------------
// <copyright file="TaktTrainingCourse.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>TrainingCourse 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Training;

/// <summary>
/// 培训课程实体
/// </summary>
[SugarTable("takt_humanresource_training_course", "培训课程表")]
[SugarIndex("ix_training_course_code", nameof(CourseCode), OrderByType.Asc, true)]
public class TaktTrainingCourse : TaktBaseEntity
{
    /// <summary>
    /// 课程编码
    /// </summary>
    [SugarColumn(ColumnName = "course_code", ColumnDescription = "课程编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string CourseCode { get; set; } = string.Empty;

    /// <summary>
    /// 课程名称
    /// </summary>
    [SugarColumn(ColumnName = "course_name", ColumnDescription = "课程名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string CourseName { get; set; } = string.Empty;

    /// <summary>
    /// 课程类别ID
    /// </summary>
    [SugarColumn(ColumnName = "category_id", ColumnDescription = "课程类别ID", ColumnDataType = "bigint", IsNullable = false)]
    public long CategoryId { get; set; }

    /// <summary>
    /// 课程描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "课程描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 课程状态(1=启用 2=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "课程状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }

}



