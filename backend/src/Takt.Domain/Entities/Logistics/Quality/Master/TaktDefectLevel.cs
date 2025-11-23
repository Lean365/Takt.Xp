//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDefectLevel.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 缺陷等级字典实体，符合ISO9001质量管理体系标准
//===================================================================

using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Quality.Master;

/// <summary>
/// 缺陷等级字典实体
/// 符合ISO9001质量管理体系标准
/// </summary>
[SugarTable("Takt_logistics_quality_defect_level", TableDescription = "缺陷等级字典表")]
public class TaktDefectLevel : TaktBaseEntity
{
    /// <summary>
    /// 缺陷等级代码（如：CRI, MAJ, MIN）
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "缺陷等级代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷等级名称（如：严重、主要、次要）
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "缺陷等级名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷等级描述（如：影响产品安全或法规符合性）
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "缺陷等级描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
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


