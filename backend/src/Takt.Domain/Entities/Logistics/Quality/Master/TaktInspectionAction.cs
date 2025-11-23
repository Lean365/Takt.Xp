//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInspectionAction.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 处理措施实体，符合ISO9001质量管理体系标准
//===================================================================

using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Quality.Master;

/// <summary>
/// 处理措施实体
/// 符合ISO9001质量管理体系标准
/// </summary>
[SugarTable("Takt_logistics_quality_inspection_action", TableDescription = "处理措施表")]
public class TaktInspectionAction : TaktBaseEntity
{
    /// <summary>
    /// 处理措施代码（如：返工、返修、报废、让步接收、退货）
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "处理措施代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 处理措施名称（如：返工处理、返修处理、报废处理、让步接收、退货处理）
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "处理措施名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 处理措施描述（如：对不合格品进行返工处理）
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "处理措施描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 关联检验结果代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_result_code", ColumnDescription = "关联检验结果代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionResultCode { get; set; }

    /// <summary>
    /// 关联检验标准代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_standard_code", ColumnDescription = "关联检验标准代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionStandardCode { get; set; }

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


