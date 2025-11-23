//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInspectionResult.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 检验结果实体，符合ISO9001质量管理体系标准
//===================================================================

using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Quality.Master;

/// <summary>
/// 检验结果实体
/// 符合ISO9001质量管理体系标准
/// </summary>
[SugarTable("Takt_logistics_quality_inspection_result", TableDescription = "检验结果表")]
public class TaktInspectionResult : TaktBaseEntity
{
    /// <summary>
    /// 检验结果代码（如：PASS、FAIL、PENDING）
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "检验结果代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 检验结果名称（如：合格、不合格、待判定）
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "检验结果名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 检验结果描述（如：产品符合质量标准要求）
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "检验结果描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 关联检验标准代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_standard_code", ColumnDescription = "关联检验标准代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionStandardCode { get; set; }

    /// <summary>
    /// 关联检验项目代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_item_code", ColumnDescription = "关联检验项目代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionItemCode { get; set; }

    /// <summary>
    /// 关联检验方法代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_method_code", ColumnDescription = "关联检验方法代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionMethodCode { get; set; }

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


