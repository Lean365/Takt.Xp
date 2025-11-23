//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDefectType.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 缺陷类型字典实体，符合ISO9001质量管理体系标准
//===================================================================

using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Quality.Master;

/// <summary>
/// 缺陷类型字典实体
/// 符合ISO9001质量管理体系标准
/// </summary>
[SugarTable("Takt_logistics_quality_defect_type", TableDescription = "缺陷类型字典表")]
public class TaktDefectType : TaktBaseEntity
{
    /// <summary>
    /// 缺陷类型代码（如：外观、尺寸、功能、材料）
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "缺陷类型代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷类型名称（如：外观缺陷、尺寸偏差、功能失效、材料问题）
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "缺陷类型名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷类型描述（如：产品外观不符合标准要求）
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "缺陷类型描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
    /// <summary>
    /// 状态
    /// 0-禁用 1-启用
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; } = 1;
}


